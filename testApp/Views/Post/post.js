/**
 * Created by seymour.h on 04/27/2016.
 */
$(document).ready(function () {
    $('#comment').click(function () {
        var data = $('#formComment').serialize();
      //  var url = "@Url.Action("AddComment","Post")";
        $.post(url, data).done(function () {
            location.reload();
        });
    });

    $('#reply').click(function () {
        var id = $(this).val();
        $('#replyModal').modal('show');
        $('#replySubmit').click(function () {
            var data = $('#replyForm').serialize();
            data += 'parentId=' + id;
          //  var url = "@Url.Action("AddCommentToComment", "Post")";
            $('#replySubmit').unbind('click');
            $.post(url, data).done(function () {
                location.reload();
            });
        });
    });
});