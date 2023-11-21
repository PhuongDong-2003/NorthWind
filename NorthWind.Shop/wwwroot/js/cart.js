function CheckOut()
{
    $.ajax({
        url: "/Cart/CheckOut",
        type: "POST",
        success: function (data) {   
            if(data.success)
            {
                Swal.fire({
					title: 'Giỏ hàng',
					text: 'Đặt hàng thành công',
					icon: 'info', 
					timer: 2000
				}).then(function () {
                    location.reload();
                });
            }
        },
        error: function (error) {
            Swal.fire({
                title: 'Giỏ hàng',
                text: 'Đặt hàng không thành công ',
                icon: 'info',
                timer: 2000
            })
        }  
});

    $.ajax({
        url: '/Cart/Cart', 
        type: 'GET',
        success: function (data) {
            console.log('Success:', data);
        },
        error: function (error) {
          
            console.error('Error:', error);
        }
    });

}


function QuantityChange(input, productID)
{
    var newValue = input.value;
    console.log(newValue);
    $.ajax({
        url: "/Cart/UpdateCart",
        type: "POST",
        data: { productId: productID, quantity: newValue}, 
        success: function (data) {
            if (data.success) {
                console.log('Data sent successfully:', data);
            } 
            else 
            {
                console.log('Data sent Failed:', data);
            }
        },
        error: function (error) {
            alert("Add Cart failed");
        }
});

}
