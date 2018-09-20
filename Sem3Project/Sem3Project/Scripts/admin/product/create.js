$(document).ready(function () {
    $('#create').click(function (e) {
        e.preventDefault();

        let product = {};
        product.ProductName = $('#productName').val();
        product.SupplierID = $('#SupplierID').val();
        product.CategoryID = $('#CategoryID').val();
        product.QuantityPerUnit = $('#QuantityPerUnit').val();
        product.UnitPrice = $('#UnitPrice').val();
        product.UnitsInStock = $('#UnitsInStock').val();
        product.UnitsOnOrder = $('#UnitsOnOrder').val();
        product.ReorderLevel = $('#ReorderLevel').val();
        product.Discontinued = $('#Discontinued').val();
        product.ProductImage = $('#ProductImage').val();

        
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

        if (product.Discontinued == '') {
            check = false;
            $('#Discontinued-valid').text('This field can not empty');
        }
        else {
            $('#Discontinued-valid').text('');
        }

       
        if (check) {
            //var formData = new FormData($('form').get(0));
            var formData = new FormData();
            formData.append('productName', product.productName);
            formData.append('SupplierID', product.SupplierID);
            formData.append('CategoryID', product.CategoryID);
            formData.append('QuantityPerUnit', product.QuantityPerUnit);
            formData.append('UnitPrice', product.UnitPrice);
            formData.append('UnitsInStock', product.UnitsInStock);
            formData.append('UnitsOnOrder',  product.UnitsOnOrder );
            formData.append('ReorderLevely', product.ReorderLevely);
            formData.append('Discontinued', product.Discontinued);
            formData.append('ProductImage', product.ProductImage);
       

            //var form = document.querySelector('form');
            //var formData = new FormData(form);
            $.ajax({
                url: '/Admin/Product/Insert',
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
