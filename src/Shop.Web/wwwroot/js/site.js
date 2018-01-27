// Write your JavaScript code.

(function () {
    function init() {
        $('.add-to-cart').click(function () {
            var productId = $(this).data('id');
            addCartItem(productId);
        })

        $('.remove-from-cart').click(function () {
            var item = $(this);
            var productId = item.data('id');
            var quantity = parseInt(item.data('quantity'));
            var itemsCount = parseInt($('#items-count').text());
            var totalPrice = parseFloat($('#total-price').text());
            var productTotalPrice = parseFloat($(`#total-price-${productId}`).text());
            var productUniPrice = parseFloat(item.data('unit-price'));

            $('#total-price').text(totalPrice - productUniPrice);

            removeCartItem(productId).then(function (response) {
                if (quantity === 1) {
                    item.parent().parent().fadeOut();
                    $('#items-count').text(--itemsCount);

                    return;
                }
                var updateQuantity = --quantity;
                item.data('quantity', updateQuantity);       
                $(`#quantity-${productId}`).text(updateQuantity);
                $(`#total-price-${productId}`).text(productTotalPrice - productUniPrice));
            });
        })
    }
    init();

    function addCartItem(productId) {
        $.post(`cart/items/${productId}`, response => {
            console.log(`Product with id: ${productId} was added to the cart.`);
        });
    }

    //AJAX
    //Promise JS, Task C#
    //Return 'promise'
    function removeCartItem(productId) {
        return $.ajax({
            type: 'DELETE',
            url: `cart/items/${productId}`
        })
    }
})();