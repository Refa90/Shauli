
$(".translate").each(function () {
    $(this).click(function () {

        var prev = $(this).prev();
        var currentText = prev.text();

        $.ajax({
            method: "GET",
            url: "/Translate/Translate?text=" + currentText
        }).done(function (data) {
            prev.text(data);
        }).error(function (err) {
            console.log('err: ' + err);
        });
    });
});