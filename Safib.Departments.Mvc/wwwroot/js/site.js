$(document).ready(function () {
    var fileUpload = $("#file-upload");
    var filePath = $("#file-path");
    var uploadButton = $("#btn-file-upload");
    var syncButton = $("#btn-sync")

    uploadButton.click(function () {
        fileUpload.click();
    });

    fileUpload.change(function () {
        if (this.files[0].size > 2097152) {
            alert("Размер файла не должен превышать 2 Мб");
            this.value = "";
            syncButton.addClass("disabled");
        } else {
            var fileName = $(this).val().split('\\')[$(this).val().split('\\').length - 1];
            syncButton.removeClass("disabled");
            filePath.html("<b>Выбранный файл: </b>" + fileName);
        }

        $("#import-result").empty();
    });

    syncButton.click(function () {
        syncButton.addClass("disabled");
        var formData = new FormData();
        formData.append("File", $(fileUpload)[0].files[0]);
        fetch("/upload", {
            method: "post",
            body: formData
        })
        .then(response => {
            return response.text();                
        })
        .then(result => {
            $("#import-result").html(result);
            $(fileUpload)[0].value = null;
            filePath.empty();
        });
    });
});