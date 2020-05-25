function GetAllc() {
    $.ajax({
        url: '/classes',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponseC(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Добавление новой
function Addc() {
    // получаем значения
    var data = {
        numb: parseInt($('#edit_c input[name=numb]').val()),
        sign: $('#edit_c input[name=sign]').val()
    };

    $.ajax({
        url: '/classes',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllc();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deletec(id) {

    $.ajax({
        url: '/classes/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllc();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// редактирование
function Editc() {
    // получаем новые значения
    var d = {
        id: parseInt($('#edit_c input[name=number]').val()),
        numb: parseInt($('#edit_c input[name=numb]').val()),
        sign: $('#edit_c input[name=sign]').val()
    };
    $.ajax({
        url: '/classes',
        type: 'PUT',
        data: JSON.stringify(d),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllc();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
var classId = 0;
function WriteResponseC(ds) {
    strResult = "";
    $('#data_c').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.numb + "</td><td>" +
            d.sign + "</td><td><button class='btn btn-primary' data-toggle='modal' data-target='#classDetailModal'>Подробнее</button></td></tr>"
        $('#data_c').append(strResult);
    });
    $('#data_c tr').bind('click', function () {
        $('#edit_c input[name=number]').val($(this).attr('id'));
        $('#edit_c input[name=numb]').val($(this).children('td:eq(0)').text());
        $('#edit_c input[name=sign]').val($(this).children('td:eq(1)').text());
    });
    $('#data_c button').bind('click', function () {
        classId = $(this).parent().parent().attr('id');
        var tr = $(this).parent().parent();
        var className = tr.children('td:eq(0)').text() + tr.children('td:eq(1)').text();
        $('#classDetailModalLabel').text(className);
        GetAlluc();
    });
}
$('#edit_c input[name=add]').on('click', function () {
    Addc();
});
$('#edit_c input[name=del]').on('click', function () {
    var id = $('#edit_c input[name=number]').val();
    Deletec(id);
});
$('#edit_c input[name=upd]').on('click', function () {
    Editc();
});