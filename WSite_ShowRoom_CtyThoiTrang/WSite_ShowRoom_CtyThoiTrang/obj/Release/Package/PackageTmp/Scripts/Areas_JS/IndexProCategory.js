<script>
    $(document).ready(function () {

        $('body').on('click', '#BtnDeleteAll', function (e) {
            e.preventDefault();
            var str = "";
            var checkbox = $(this).parents('.card').find('tr td input:checkbox');
            var i = 0;
            checkbox.each(function () {
                if (this.checked) {
                    var _id = $(this).val();
                    if (i === 0) {
                        str += _id;
                    } else {
                        str += "," + _id;
                    }
                    i++;
                } else {
                    checkbox.attr('selected', '');
                }
            });
            if (str.length > 0) {
                var conf = confirm('Bạn có muốn xóa các bản ghi này hay không?');
                if (conf === true) {
                    $.ajax({
                        url: '/admin/news/deleteAll',
                        type: 'POST',
                        data: { ids: str },
                        success: function (rs) {
                            if (rs.success) {
                                location.reload();
                            }
                        }
                    });
                }
            }
        });

    $('body').on('change', '#SelectAll', function () {
                var checkStatus = this.checked;
    var checkbox = $(this).parents('.card-body').find('tr td input:checkbox');
    checkbox.each(function () {
        this.checked = checkStatus;
    if (this.checked) {
        checkbox.attr('selected', 'checked');
                    } else {
        checkbox.attr('selected', '#btnDelete', function () {
            var id = $(this).data('id');
            var conf = confirm('Bạn Có Muốn Xóa Không');
            if (conf === true) {
                $.ajax({

                });
            }

        });
                    }
                });
            });


    $('body').on('click','')






        });
</script>