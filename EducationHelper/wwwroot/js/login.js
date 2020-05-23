
var tokenKey = "accessToken";
document.getElementById("submit").addEventListener("click", e => {

    e.preventDefault();
    getTokenAsync();
});

async function getTokenAsync() {
    const formData = new FormData();
    formData.append("grant_type", "password");
    formData.append("username", document.getElementById("login").value);
    formData.append("password", document.getElementById("password").value);

    // отправляет запрос и получаем ответ
    const response = await fetch("/token", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: formData
    });
    // получаем данные 
    const data = await response.json();

    // если запрос прошел нормально
    if (response.ok === true) {

        // изменяем содержимое и видимость блоков на странице
        document.getElementById("menu").style.display = "block";
        document.getElementById("login-form").style.display = "none";
        // сохраняем в хранилище sessionStorage токен доступа
        sessionStorage.setItem(tokenKey, data.access_token);
        console.log(data.access_token);
    }
    else {
        // если произошла ошибка, из errorText получаем текст ошибки
        
        console.log("Error: ", response.status, data.errorText);
        document.getElementById("errMsg").innerHTML = data.errorText;
    }
};

document.getElementById("logout").addEventListener("click", e => {
    $('.active').removeClass('active');
    $('.in').removeClass('in');
    e.preventDefault();
    
            document.getElementById("menu").style.display = "none";
            document.getElementById("login-form").style.display = "block";
    sessionStorage.removeItem(tokenKey);
        });

function changeIndex(isAuth) {
    if (isAuth) {
        document.getElementById("menu").style.display = "block";
        document.getElementById("login-form").style.display = "none";
    }
    else {
        document.getElementById("menu").style.display = "none";
        document.getElementById("login-form").style.display = "block";
    }
}

