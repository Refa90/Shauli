$(document).ready(function () {
    $('#ImageUpload').hide();
    $('#enable_image_upload').change(function () {
        $('#ImageUpload').toggle()
    });

    $('#VideoUpload').hide();
    $('#enable_video_upload').change(function () {
        $('#VideoUpload').toggle()
    });
});