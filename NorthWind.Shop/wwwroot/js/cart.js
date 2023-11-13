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
				})
            }
        },
        error: function (error) {
            Swal.fire({
                title: 'Giỏ hàng',
                text: 'Đặt hàng không thành công',
                icon: 'info',
                timer: 2000
            })
        }
});
}