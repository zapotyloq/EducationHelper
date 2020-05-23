// Получение всехпо ajax-запросу
function GetAllun() {
    $.ajax({
        url: '/users/GetForNew/' + newId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseun(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleteun() {

    $.ajax({
        url: '/users/RemoveFromNew?newId=' + newId + '&userId=' + userId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllun();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
var userId = 0;
// вывод полученных данных на экран
function WriteResponseun(ds) {
    strResult = "";
    $('#data_un').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td><td><button class='btn btn-danger'>Удалить</button></td></tr>"
        $('#data_un').append(strResult);
    });
    $('#data_un button').bind('click', function () {
        userId = $(this).parent().parent().attr('id');
        Deleteun();
    });
}
$('#addun').bind('click', function () {
    GetAllaun();
});
function GetAllaun() {
    $.ajax({
        url: '/users/GetUsersIsNotNew/' + newId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseaun(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
function WriteResponseaun(ds) {
    strResult = "";
    $('#data_aun').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td></tr>"
        $('#data_aun').append(strResult);
    });
    $('#data_aun tr').bind('click', function () {
        userId = $(this).attr('id');
        Addaun();
    });
}

function Addaun() {

    $.ajax({
        url: '/users/AddToNew?userId=' + userId + '&newId=' + newId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllaun();
            GetAllun();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}