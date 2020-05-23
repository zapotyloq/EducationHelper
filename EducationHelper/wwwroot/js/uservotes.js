// Получение всехпо ajax-запросу
function GetAlluv() {
    $.ajax({
        url: '/users/GetForVote/' + voteId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseuv(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleteuv() {

    $.ajax({
        url: '/users/RemoveFromVote?voteId=' + voteId + '&userId=' + userId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlluv();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
var userId = 0;
// вывод полученных данных на экран
function WriteResponseuv(ds) {
    strResult = "";
    $('#data_uv').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td><td><button class='btn btn-danger'>Удалить</button></td></tr>"
        $('#data_uv').append(strResult);
    });
    $('#data_uv button').bind('click', function () {
        userId = $(this).parent().parent().attr('id');
        Deleteuv();
    });
};
$('#adduv').bind('click', function () {
    GetAllauv();
});
function GetAllauv() {
    $.ajax({
        url: '/users/GetUsersIsNotVote/' + voteId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseauv(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
function WriteResponseauv(ds) {
    strResult = "";
    $('#data_auv').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td></tr>"
        $('#data_auv').append(strResult);
    });
    $('#data_auv tr').bind('click', function () {
        userId = $(this).attr('id');
        Addauv();
    });
}

function Addauv() {

    $.ajax({
        url: '/users/AddToVote?userId=' + userId + '&voteId=' + voteId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllauv();
            GetAlluv();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}