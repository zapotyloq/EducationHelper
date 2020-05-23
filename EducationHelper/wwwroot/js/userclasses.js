// Получение всехпо ajax-запросу
function GetAlluc() {
    $.ajax({
        url: '/users/guic/' + classId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            WriteResponseuC(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleteuc() {

    $.ajax({
        url: '/users/delufc?userId=' + userId + '&classId=' + classId,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAlluc();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
var userId = 0;
// вывод полученных данных на экран
function WriteResponseuC(ds) {
    strResult = "";
    $('#data_uc').html('');
    $.each(ds, function (index, d) {
        var class_ = d.role == 2 ? '' : 'table-primary';
        strResult = "<tr class='" + class_ + "'id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td><td><button class='btn btn-danger'>Удалить</button></td></tr>"
        $('#data_uc').append(strResult);
    });
    $('#data_uc button').bind('click', function () {
        userId = $(this).parent().parent().attr('id');
        Deleteuc();
    });
}
$('#adduc').bind('click', function () {
    GetAllauc();
});
function GetAllauc() {
    $.ajax({
        url: '/users/GetUsersIsNotClass/' + classId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            console.log(data);
            WriteResponseauC(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
function WriteResponseauC(ds) {
    strResult = "";
    $('#data_auc').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.surname + ' ' + d.firstname
            + "</td></tr>"
        $('#data_auc').append(strResult);
    });
    $('#data_auc tr').bind('click', function () {
        userId = $(this).attr('id');
        Addauc();
    });
}

function Addauc() {

    $.ajax({
        url: '/users/addutc?userId=' + userId + '&classId=' + classId,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllauc();
            GetAlluc();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}