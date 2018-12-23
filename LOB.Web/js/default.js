$(document).ready(function() {
    //alert("The document is ready");
    var orderUid = $('#btn-delete-cart').attr("data-order-id");
    var name = $('#btn-add-to-cart').attr("data-name");
    var productUid = $('#btn-add-to-cart').attr("data-product-id");
    var quantityNumber = $('#btn-add-to-cart').attr("data-quantity");

    var ddlQuantity = document.getElementById('ddlQuantity');
    for (var i = 1; i <= quantityNumber; i++) {
        var o = document.createElement("option");
        o.value = i;
        o.innerHTML = i;
        ddlQuantity.appendChild(o);
    }

    function addToCart(data) {
        $.ajax({
            type: "PUT",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "../../api/v1/cart/putproduct",
            data: JSON.stringify(data),
            success: function (response) {
                //$('#comments').html(response);
                //location.reload();
                //alert("success");
            },
            error: function (r) {
                alert("error: " + r);
            }
        });
    }

    $('#btn-add-to-cart').bind('click', function (event) {
        var shopId = $('#btnShop').data('shop-id');
        var quantity = $('#ddlQuantity option:selected').val();
        var data = { OrderUid: orderUid, ProductUid: productUid, Quantity: quantity, ShopId: shopId }
        addToCart(data);
        //alert(window.location.hostname);
        //var message = name + " в количестве <b>" + quantity + "</b> шт. успешно добавлен в корзину";
        //$('#added-message').html(message);
        //alert("The product " + id + " with " + selectedQuantity + " added to cart");
    });

    function deleteCart() {
        $.ajax({
            type: "DELETE",
            url: "../../api/cart/deleteorder?id=" + orderUid,
            success: function(response) {
                //alert("success");
            },
            error: function(r) {
                alert("error: " + r);
            }
        });
    }

    $('#btn-delete-cart').bind('click', function (event) {
        deleteCart();
    });

    //$.ajax({
    //    type: "GET",
    //    dataType: "html",
    //    contentType: "text/html",
    //    url: "comments.ashx?p=",
    //    success: function(response) {
    //        $('#comments').html(response);

    //        $('.popbox').display({
    //            name: 'btnSend',
    //            open: '.open',
    //            box: '.box',
    //            close: '.close'
    //        });
    //    }
    //});


    
});