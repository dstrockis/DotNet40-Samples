function handleErrors(error) {
    console.log(error.statusText);
}

function doPost(url, jsonObject) {

    var ajaxOptions = {
        url: url,
        type: 'post',
        contentType: 'application/json',
        dataType: 'json',
        cache: false,
        data: JSON.stringify(jsonObject)
    };

    return $.ajax(ajaxOptions).fail(handleErrors);
}

function test() {
    var user = {
        userName: 'user1',
        password:'pswd',
        confirmPassword: 'pswd'
    }

    console.log('call update user' + bew );

    doPost('account/UpdateUser', user).done(function () {
        console.log('after update user');
        alert('after update user');
    });
}