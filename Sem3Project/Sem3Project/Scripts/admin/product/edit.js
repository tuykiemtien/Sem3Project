
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

        let product = {};
        product.ProductID = $('#edit').val();
        product.ProductName = $('#productName').val();
        product.SupplierID = $('#SupplierID').val();
        product.CategoryID = $('#CategoryID').val();
        product.QuantityPerUnit = $('#QuantityPerUnit').val();
        product.UnitPrice = $('#UnitPrice').val();
        product.UnitsInStock = $('#UnitsInStock').val();
        product.UnitsOnOrder = $('#UnitsOnOrder').val();
        product.ReorderLevel = $('#ReorderLevel').val();
        //product.Discontinued = $('#Discontinued').val();

       
        let check = true;
        if (product.ProductName == '') {
            check = false;
            $('#ProductName-valid').text('This field can not empty');
        }
        else {
            $('#ProductName-valid').text('');
        }

        if (product.QuantityPerUnit == '') {
            check = false;
            $('#QuantityPerUnit-valid').text('This field can not empty');
        }
        else {
            $('#QuantityPerUnit-valid').text('');
        }

        if (product.UnitPrice == '') {
            check = false;
            $('#UnitPrice-valid').text('This field can not empty');
        }
        else {
            $('#UnitPricee-valid').text('');
        }

        if (product.UnitsInStock == '') {
            check = false;
            $('#UnitsInStock-valid').text('This field can not empty');
        }
        else {
            $('#UnitsInStock-valid').text('');
        }

        if (product.UnitsOnOrder == '') {
            check = false;
            $('#UnitsOnOrder-valid').text('This field can not empty');
        }
        else {
            $('#UnitsOnOrder-valid').text('');
        }

        if (product.ReorderLevel == '') {
            check = false;
            $('#ReorderLevel-valid').text('This field can not empty');
        }
        else {
            $('#ReorderLevel-valid').text('');
        }

        //if (product.Discontinued == '') {
        //    check = false;
        //    $('#Discontinued-valid').text('This field can not empty');
        //}
        //else {
        //    $('#Discontinued-valid').text('');
        //}

        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            //var file = $('#employee-image').get(0).files[0];
            //formData.append("File", file);
            formData.append('ProductID', product.ProductID);
            formData.append('ProductName', product.ProductName);
            formData.append('SupplierID', product.SupplierID);
            formData.append('CategoryID', product.CategoryID);
            formData.append('QuantityPerUnit', product.QuantityPerUnit);
            formData.append('UnitPrice', product.UnitPrice);
            formData.append('UnitsInStock', product.UnitsInStock);
            formData.append('UnitsOnOrder', product.UnitsOnOrder);
            formData.append('ReorderLevel', product.ReorderLevel);
            //formData.append('Discontinued', product.Discontinued);
            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Product/Update',
                type: 'POST',
                data: formData, // modify
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.Ok) {
                        alert('Update success!!');
                        $('#data-table').DataTable().ajax.reload();
                        $('#dashboardModalWrapper').modal('toggle');
                    }
                    else {
                        $('#update-valid').text('Insert error');
                    }
                },
                error: function () {
                    $('#update-valid').text('Server error');
                }
            });
        }
    });

})

