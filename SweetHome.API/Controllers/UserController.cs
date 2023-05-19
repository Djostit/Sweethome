using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetHome.API.Data;
using SweetHome.API.Data.Models;

namespace SweetHome.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly SweethomeContext _context;
    public UserController(ILogger<UserController> logger, SweethomeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("Registration")]
    public async Task<IActionResult> Registration([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null)
        {
            _logger.LogInformation("Пользователь с такой почтой уже существует.");
            return BadRequest("Пользователь с такой почтой уже существует.");
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // сохраняем изображение в файл
            // var fileName = "photo.png"; 
            // System.IO.File.WriteAllBytes(fileName, user.Photo); // сохраняем файл
            _logger.LogInformation($"Метод Registration был успешно вызван");
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost("Authorization")]
    public async Task<IActionResult> Authorization([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if(existingUser == null)
                return BadRequest("Пользователь не найден.");
            else if(!BCrypt.Net.BCrypt.Verify(existingUser.Password, user.Password))
                return BadRequest("Пользователь не найден.");
            else
                return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
