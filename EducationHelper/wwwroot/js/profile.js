var userId = 0;
function GetProfile() {
    $.ajax({
        url: '/users/profile',
        type: 'GET',
        dataType: 'json',
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            userId = data.id;
            $('#profile tr:eq(0) td:eq(1)').text(data.login);
            $('#profile tr:eq(1) td:eq(1)').text(data.surname);
            $('#profile tr:eq(2) td:eq(1)').text(data.firstname);
            $('#profile tr:eq(3) td:eq(1)').text(data.email);
            $('#profile tr:eq(4) td:eq(1)').text(data.phone);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
$('#profile-tab').bind('click', function(){
    GetProfile();
});
$('#eprofilebtn').bind('click', function () {
    $('#eprofile [name=surname]').val($('#profile tr:eq(1) td:eq(1)').text());
    $('#eprofile [name=firstname]').val($('#profile tr:eq(2) td:eq(1)').text());
    $('#eprofile [name=email]').val($('#profile tr:eq(3) td:eq(1)').text());
    $('#eprofile [name=phone]').val($('#profile tr:eq(4) td:eq(1)').text());
});
$('#savep').bind('click', function () {
    var d = {
        id: parseInt(userId),
        surname: $('#eprofile [name=surname]').val(),
        firstname: $('#eprofile [name=firstname]').val(),
        email: $('#eprofile [name=email]').val(),
        phone: $('#eprofile [name=phone]').val(),
    };
    $.ajax({
        url: '/users/Profile',
        type: 'PUT',
        data: JSON.stringify(d),
        contentType: "application/json;charset=utf-8",
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            GetProfile()
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
});