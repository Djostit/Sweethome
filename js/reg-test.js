document.getElementById('register-btn').addEventListener('click', function() {
    var fullName = document.getElementById('fullName').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var passwordRepeat = document.getElementById('passwordRepeat').value;

    var data = {
        fullName: fullName,
        email: email,
        password: password,
        passwordRepeat: passwordRepeat
    };

    var json = JSON.stringify(data);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://example.com/api/register');
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function() {
        if (xhr.status === 200) {
            // Успешно зарегистрировано
        } else {
            // Ошибка регистрации
        }
    };
    xhr.send(json);
});