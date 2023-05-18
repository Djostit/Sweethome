document.getElementById('register-btn').addEventListener('click', function() {
  const fullName = $('#fullName').val();
  const email = $('#email').val();
  const password = $('#password').val();
  const passwordRepeat = $('#passwordRepeat').val();
  const photo = $('#photo')[0].files[0];

  if (fullName === '' || 
      email === '' || 
      password === '' || 
      passwordRepeat === '' || 
      photo === undefined) {
    alert('Пустые поля!');
    return;
  }
  if(password != passwordRepeat) {
    alert('Пароли не совпадают');
    return;
  }
  const reader = new FileReader();
  reader.readAsBinaryString(photo);
  reader.onload = function() {
    const data = {
      fullName: fullName,
      email: email,
      password: password,
      photo: btoa(reader.result)
    }
    $.ajax({
      url: 'http://localhost:5237/api/User/Registration',
      method: 'POST',
      data: JSON.stringify(data),
      contentType: 'application/json',
      success: function(data) {
        alert(data);
      },
      error: function(error) {
        alert(error);
      }
    });
  };
});