// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    console.log(1);
    $.get('Home/NewsViews', function (result) {
        $('#holder').html(result);
    })
    $("#b1").click(
        $.get('Home/NewsViews', function (result) {
            $('#holder').html(result);
        }));
});