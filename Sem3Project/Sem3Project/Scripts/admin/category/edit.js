
$(document).ready(function () {
    //$('#cancel').click(function (e) {
    //    e.preventDefault();
    //    var self = $('#cancel').val();

    //    if (confirm('Are you sure you want to cancel')) {
    //        window.location = '/Admin/Employee/Index';
    //    }

    //});

    $('#edit').click(function (e) {
        e.preventDefault();

        let category = {};
        category.CategoryID = $('#edit').val();
        category.CategoryName = $('#last-name').val();
        category.Description = $('#first-name').val();
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
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            if (image != '') {
                var file = $('#employee-image').get(0).files[0];
                formData.append("File", file);
            }

            formData.append('EmployeeID', category.CategoryID);
            formData.append('LastName', category.CategoryName);
            formData.append('FirstName', category.Description);

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Category/Update',
                type: 'POST',
                data: formData, // modify
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.Ok) {
                        alert('Update success!!');
                        $('#data-table').DataTable().ajax.reload();
                    }
                    else {
                        $('#update-valid').text('Update error');
                    }
                },
                error: function () {
                    $('#update-valid').text('Server error');
                }

            });
            
        }

    });
})

