function GetProfile() {
    $.ajax({
        url: '/users/profile',
        type: 'GET',
        dataType: 'json',
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey),  // передача токена в заголовке
        },
        success: function (data) {
            $('#profile tr:eq(0) td:eq(1)').text(data.login);
            $('#profile tr:eq(1) td:eq(1)').text(data.firstname);
            $('#profile tr:eq(2) td:eq(1)').text(data.surname);
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