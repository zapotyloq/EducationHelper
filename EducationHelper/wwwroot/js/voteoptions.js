// Получение всехпо ajax-запросу
function GetAlluo() {
    $.ajax({
        url: '/voteoptions/GetForVote/' + voteId,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            WriteResponseuo(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// Удаление
function Deleteuo(id) {
    $.ajax({
        url: '/voteoptions/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetAlluo();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
// вывод полученных данных на экран
function WriteResponseuo(ds) {
    strResult = "";
    $('#data_uo').html('');
    $.each(ds, function (index, d) {
        strResult = "<tr id='" + d.id + "'><td> " + d.option
            + "</td><td><button class='btn btn-danger'>Удалить</button></td></tr>"
        $('#data_uo').append(strResult);
    });
    $('#data_uo button').bind('click', function () {
        
        Deleteuo($(this).parent().parent().attr('id'));
    });
};

function Addauo() {
    var data = {
        voteId : parseInt(voteId),
        option: $('#labelou').val(),
        npp : 0
    };
    $.ajax({
        url: '/voteoptions',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        data: JSON.stringify(data),
        success: function (data) {
            GetAlluo();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
$('#addauo').bind('click', function () {
    Addauo();
});