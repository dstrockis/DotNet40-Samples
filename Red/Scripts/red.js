function refreshUserGroups(url, target) {
    $.ajax({
        type: "GET",
        url: url,
        dataType: "text",
    }).then(function(result) {
        $(target).html(result);
        console.log(result);
    }).fail(function(result) {
        console.log(result);
    });
};


