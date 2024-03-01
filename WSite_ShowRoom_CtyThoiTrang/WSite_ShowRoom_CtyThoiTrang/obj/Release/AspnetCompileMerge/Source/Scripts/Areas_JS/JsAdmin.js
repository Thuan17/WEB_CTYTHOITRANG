﻿$(document).ready(function () {
    $('body').on('click', '.btnAddList', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var soluong = 1;
       
        $.ajax({
            url: '/admin/seller/AddListProduct',
            type: 'POST',
            data: { id: id, soluong: soluong },
            success: function (rs) {
                if (rs.Success) {
                    if (rs.Code == 1) {
                        LoadList();
                        const Toast = Swal.mixin({
                            toast: true,
                            position: "top-end",
                            showConfirmButton: false,
                            timer: 3000,
                            timerProgressBar: true,
                            didOpen: (toast) => {
                                toast.onmouseenter = Swal.stopTimer;
                                toast.onmouseleave = Swal.resumeTimer;
                            }
                        });

                        Toast.fire({
                            icon: "success",
                            title: "Thêm thành công"
                        });
                    }
                }
                else {
                }

            }

        });

    });




    //cap nhaap so luong san pham trong gio hang
    $('body').on('input', '.Quantity', function (e) {
        var productId = $(this).attr('id');
        var newQuantity = $(this).val();
        //alert(productId);
        //alert( newQuantity);
        if (newQuantity <= 0) {
            Swal.fire({
                title: "Ban có muốn bỏ sản phẩm này",
                text: "",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Đồng ý"
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/admin/Seller/Delete',
                        type: 'POST',
                        data: { id: productId },
                        success: function (rs) {
                            if (rs.Success) {
                                if (rs.Code == 1) {
                                    $('#checkout_items').html(rs.Count);
                                    LoadList();
                                    const Toast = Swal.mixin({
                                        toast: true,
                                        position: "top-end",
                                        showConfirmButton: false,
                                        timer: 3000,
                                        timerProgressBar: true,
                                        didOpen: (toast) => {
                                            toast.onmouseenter = Swal.stopTimer;
                                            toast.onmouseleave = Swal.resumeTimer;
                                        }
                                    });

                                    Toast.fire({
                                        icon: "success",
                                        title: "Xoá thành công"
                                    });
                                }
                            }
                        }
                    });




                }
            });
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/admin/seller/UpdateQuanTity',
                data: {
                    id: productId,
                    quantity: newQuantity
                },
                success: function (result) {
                    console.log(result);
                    if (result.Success) {
                        console.log('Cập nhật số lượng thành công');
                    } else {
                        console.log('Có lỗi xảy ra: ' + result.msg);
                    }
                },
                error: function (error) {
                    console.log('Lỗi Ajax: ' + error.statusText);
                }
            });

        }

    });




    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');


        Swal.fire({
            title: "Bạn có chắc?",
            text: "Muốn xóa sản phẩn này!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Xóa !"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/admin/Seller/Delete',
                    type: 'POST',
                    data: { id: id },
                    success: function (rs) {
                        if (rs.Success) {
                            if (rs.Code == 1) {
                                $('#checkout_items').html(rs.Count);
                                LoadList();
                                const Toast = Swal.mixin({
                                    toast: true,
                                    position: "top-end",
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                    didOpen: (toast) => {
                                        toast.onmouseenter = Swal.stopTimer;
                                        toast.onmouseleave = Swal.resumeTimer;
                                    }
                                });

                                Toast.fire({
                                    icon: "success",
                                    title: "Xoá thành công"
                                });
                            }
                        }
                    }
                });
            }
        });

    });
});


function LoadList() {
    $.ajax({
        url: '/admin/seller/Partail_ProductSeller',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }

    });
}
