$(document).ready(function () {
    $('#create').click(function (e) {
        e.preventDefault();

        let employee = {};
        employee.LastName = $('#last-name').val();
        employee.FirstName = $('#first-name').val();
        let image;
        let check = true;
        if (employee.LastName == '') {
            check = false;
            $('#last-name-valid').text('This field can not empty');
        }
        else {
            $('#last-name-valid').text('');
        }

        if (employee.FirstName == '') {
            check = false;
            $('#first-name-valid').text('This field can not empty');
        }
        else {
            $('#first-name-valid').text('');
        }

        

        image = $('#employee-image').val();
        if (image == '') {
            7
            check = false;
            $('#employee-image-valid').text('This field can not empty');
        }
        else {
            $('#employee-image-valid').text('');
        }
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            var file = $('#employee-image').get(0).files[0];
            formData.append("File", file);
            formData.append('CategoryName', employee.LastName);
            formData.append('Description', employee.FirstName);
          

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Category/Insert',
                type: 'POST',
                data: formData, // modify
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.Ok) {
                        alert('Insert success!!');
                        $('#data-table').DataTable().ajax.reload();
                        $('#dashboardModalWrapper').modal('toggle');
                    }
                    else {
                        $('#create-valid').text('Insert error');
                    }
                },
                error: function () {
                    $('#create-valid').text('Server error');
                }

            });
        }
    });





});
var loadFile = function (event) {
    var output = document.getElementById('image-upload');
    output.src = URL.createObjectURL(event.target.files[0]);
};
