// Получение всехпо ajax-запросу
function GetAllue() {
    $.ajax({
        url: '/users/GetForEvent/' + eventId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseue(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleteue() {

    $.ajax({
        url: '/users/RemoveFromEvent?eventId=' + eventId + '&userId=' + userId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllue();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
var userId = 0;
// вывод полученных данных на экран
function WriteResponseue(ds) {
    strResult = "";
    $('#data_ue').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td><td><button class='btn btn-danger'>Удалить</button></td></tr>"
        $('#data_ue').append(strResult);
    });
    $('#data_ue button').bind('click', function () {
        userId = $(this).parent().parent().attr('id');
        Deleteue();
    });
}
$('#addue').bind('click', function () {
    GetAllaue();
});
function GetAllaue() {
    $.ajax({
        url: '/users/GetUsersIsNotEvent/' + eventId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseaue(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
function WriteResponseaue(ds) {
    strResult = "";
    $('#data_aue').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td></tr>"
        $('#data_aue').append(strResult);
    });
    $('#data_aue tr').bind('click', function () {
        userId = $(this).attr('id');
        Addaue();
    });
}

function Addaue() {

    $.ajax({
        url: '/users/AddToEvent?userId=' + userId + '&eventId=' + eventId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllaue();
            GetAllue();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}