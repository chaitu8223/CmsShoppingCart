$(document).ready(function () {
    $("a.confirmatiodelete").click(function () {
        if (!confirm("Do you want to delete")) {
            return false;
        }
    });
});