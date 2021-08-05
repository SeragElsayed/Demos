$(document).ready(function () {
    $('#DeleteEventConfirmationModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);//Button which is clicked
        var clickedButtonId = button.data().id;//Get id of the button
        // set id to the hidden input field in the form.
        var oldHref = $("#modalDelteBtn").attr("href")//.text(clickedButtonId);
        $("#modalDelteBtn").attr("href", `${oldHref}/${clickedButtonId}`)
    });
});