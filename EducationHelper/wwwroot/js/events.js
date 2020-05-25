function GetAlle() {
    $.ajax({
        url: '/events',
        type: 'GET',
        dataType: 'json',
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponsee(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Добавление новой
function Adde() {
    // получаем значения
    var data = {
        name: $('#edit_e input[name=nam]').val(),
        description: $('#edit_e input[name=desc]').val()
    };

    $.ajax({
        url: '/events',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAlle();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deletee(id) {

    $.ajax({
        url: '/events/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlle();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// редактирование
function Edite() {
    // получаем новые значения
    var d = {
        id: parseInt($('#edit_e input[name=number]').val()),
        name: $('#edit_e input[name=nam]').val(),
        description: $('#edit_e input[name=desc]').val()
    };
    $.ajax({
        url: '/events',
        type: 'PUT',
        data: JSON.stringify(d),
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlle();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
var eventId = 0;
function WriteResponsee(ds) {
    strResult = "";
    $('#data_e').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.name + "</td><td>" +
            d.description + "</td><td><button class='btn btn-primary' data-toggle='modal' data-target='#eventDetailModal'>Подробнее</button></td></tr>"
        $('#data_e').append(strResult);
    });
    $('#data_e tr').bind('click', function () {
        $('#edit_e input[name=number]').val($(this).attr('id'));
        $('#edit_e input[name=nam]').val($(this).children('td:eq(0)').text());
        $('#edit_e input[name=desc]').val($(this).children('td:eq(1)').text());
    });
    $('#data_e button').bind('click', function () {
        eventId = $(this).parent().parent().attr('id');
        var tr = $(this).parent().parent();
        var eventName = tr.children('td:eq(0)').text();
        $('#eventDetailModalLabel').text(eventName);
        GetAllue();
    });
}
$('#edit_e input[name=add]').on('click', function () {
    Adde();
});
$('#edit_e input[name=del]').on('click', function () {
    var id = $('#edit_e input[name=number]').val();
    Deletee(id);
});
$('#edit_e input[name=upd]').on('click', function () {
    Edite();
});