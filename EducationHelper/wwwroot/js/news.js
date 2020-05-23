// Получение всехпо ajax-запросу
$(function () {
    GetAlln();
});
function GetAlln() {
    $.ajax({
        url: '/news',
        type: 'GET',
        dataType: 'json',
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponsen(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Добавление новой
function Addn() {
    // получаем значения
    var data = {
        name: $('#edit_n input[name=nam]').val(),
        text: $('#edit_n textarea[name=desc]').val()
    };

    $.ajax({
        url: '/news',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlln();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleten(id) {

    $.ajax({
        url: '/news/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlln();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// редактирование
function Editn() {
    // получаем новые значения
    var d = {
        id: parseInt($('#edit_n input[name=number]').val()),
        name: $('#edit_n input[name=nam]').val(),
        text: $('#edit_n textarea[name=desc]').val()
    };
    $.ajax({
        url: '/news',
        type: 'PUT',
        data: JSON.stringify(d),
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlln();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
var newId = 0;
function WriteResponsen(ds) {
    strResult = "";
    $('#data_n').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.name + "</td><td>" +
            d.text + "</td><td><button class='btn btn-primary' data-toggle='modal' data-target='#newDetailModal'>Подробнее</button></td></tr>"
        $('#data_n').append(strResult);
    });
    $('#data_n tr').bind('click', function () {
        $('#edit_n input[name=number]').val($(this).attr('id'));
        $('#edit_n input[name=nam]').val($(this).children('td:eq(0)').text());
        $('#edit_n textarea[name=desc]').val($(this).children('td:eq(1)').text());
    });
    $('#data_n button').bind('click', function () {
        newId = $(this).parent().parent().attr('id');
        var tr = $(this).parent().parent();
        var newName = tr.children('td:eq(0)').text();
        $('#newDetailModalLabel').text(newName);
        GetAllun();
    });
}
$('#edit_n input[name=add]').on('click', function () {
    Addn();
});
$('#edit_n input[name=del]').on('click', function () {
    var id = $('#edit_n input[name=number]').val();
    Deleten(id);
});
$('#edit_n input[name=upd]').on('click', function () {
    Editn();
});