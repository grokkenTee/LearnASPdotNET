
function doSearch(page) {
    var url = $("#searchInput").prop("action");
    var input = $("#searchInput").serializeArray();
    input.push({ "name": "page", "value": page });

    $.ajax({
        type: "GET",
        url: url,
        data: input,
        success: function (data) {
            $("#searchResult").html(data)
        }
    });
}

function clearForm($form) {
    $form.find(':input').not(':button, :submit, :reset, :hidden, :checkbox, :radio, :select').val('');
    $form.find(':checkbox, :radio, :select').prop('checked', false);
}
