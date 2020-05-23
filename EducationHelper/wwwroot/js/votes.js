// Получение всехпо ajax-запросу
$(function () {
    GetAllv();
});
function GetAllv() {
    $.ajax({
        url: '/votes',
        type: 'GET',
        dataType: 'json',
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponsev(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Добавление новой
function Addv() {
    // получаем значения
    var data = {
        name: $('#edit_v input[name=nam]').val(),
        description: $('#edit_v input[name=desc]').val()
    };

    $.ajax({
        url: '/votes',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllv();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deletev(id) {

    $.ajax({
        url: '/votes/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllv();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// редактирование
function Editv() {
    // получаем новые значения
    var d = {
        id: parseInt($('#edit_v input[name=number]').val()),
        name: $('#edit_v input[name=nam]').val(),
        description: $('#edit_v input[name=desc]').val()
    };
    $.ajax({
        url: '/votes',
        type: 'PUT',
        data: JSON.stringify(d),
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAllv();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
var voteId = 0;
function WriteResponsev(ds) {
    strResult = "";
    $('#data_v').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.name + "</td><td>" +
            d.description + "</td><td><button class='btn btn-primary' data-toggle='modal' data-target='#voteDetailModal'>Подробнее</button></td></tr>"
        $('#data_v').append(strResult);
    });
    $('#data_v tr').bind('click', function () {
        $('#edit_v input[name=number]').val($(this).attr('id'));
        $('#edit_v input[name=nam]').val($(this).children('td:eq(0)').text());
        $('#edit_v input[name=desc]').val($(this).children('td:eq(1)').text());
    });
    $('#data_v button').bind('click', function () {
        voteId = $(this).parent().parent().attr('id');
        var tr = $(this).parent().parent();
        var voteName = tr.children('td:eq(0)').text();
        $('#voteDetailModalLabel').text(voteName);
        GetAlluv();
        GetAlluo();
    });
}
$('#edit_v input[name=add]').on('click', function () {
    Addv();
});
$('#edit_v input[name=del]').on('click', function () {
    var id = $('#edit_v input[name=number]').val();
    Deletev(id);
});
$('#edit_v input[name=upd]').on('click', function () {
    Editv();
});