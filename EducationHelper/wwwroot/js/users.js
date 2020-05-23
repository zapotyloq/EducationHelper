$(function () {
    GetAll();

    $("#upd").click(function (event) {
        event.preventDefault();
        Edit();
    });

    $("#add").click(function (event) {
        event.preventDefault();
        Add();
    });

});
// Получение всехпо ajax-запросу
function GetAll() {
    $.ajax({
        url: '/users',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Добавление новой
function Add() {
    // получаем значения
    var user = {
        login: $('input[name=login]').val(),
        password: $('input[name=pass]').val(),
        surname: $('input[name=surname]').val(),
        firstname: $('input[name=firstname]').val(),
        role: $('select[name=role]').val() 
    };

    $.ajax({
        url: '/users',
        type: 'POST',
        data: JSON.stringify(user),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAll();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Delete(id) {

    $.ajax({
        url: '/users/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAll();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// редактирование
function Edit() {
    // получаем новые значения
    var user = {
        id: parseInt($('input[name=number]').val()),
        login: $('input[name=login]').val(),
        password: $('input[name=pass]').val(),
        surname: $('input[name=surname]').val(),
        firstname: $('input[name=firstname]').val(),
        role: $('select[name=role]').val() 
    };
    $.ajax({
        url: '/users',
        type: 'PUT',
        data: JSON.stringify(user),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAll();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
function WriteResponse(users) {
    strResult = "";
    $('#data_u').html('');
    $.each(users, function (index, user) {
        strResult = "<tr id='" + user.id + "'><td> " + user.login + "</td><td>" +
            user.password + "</td><td>" + user.surname + "</td><td>" + user.firstname + "</td><td>" + user.role + "</td></tr>"
        $('#data_u').append(strResult);
    });
    $('#data_u tr').bind('click', function () {
        $('input[name=number]').val($(this).attr('id'));
        $('input[name=login]').val($(this).children('td:eq(0)').text());
        $('input[name=pass]').val($(this).children('td:eq(1)').text());
        $('input[name=surname]').val($(this).children('td:eq(2)').text());
        $('input[name=firstname]').val($(this).children('td:eq(3)').text()); 
        $('select[name=role]').val($(this).children('td:eq(4)').text());   
    });
}
// обработчик удаления
function DeleteItem() {
    // получаем id удаляемого объекта
    var id = $('input[name=number]').val();
    Delete(id);
}
// обработчик редактирования
function EditItem(el) {
    // получаем id редактируемого объекта
    var id = $('#number').val();
    Get(id);
};
$('#edit_u input[name=add]').on('click', function () {
    Add();
});
$('#edit_u input[name=del]').on('click', function () {
    DeleteItem();
});
$('#edit_u input[name=upd]').on('click', function () {
    Edit();
})

