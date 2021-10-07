 
$(document).ready(function () {


    let loadFuncttion = function () {
        $('#userTable').DataTable({
            "filter": true,
            "processing": true,
            "destroy": true,
            "paging": true,
            "searching": true,
            "ajax": {
                "url": "Users/IndexJson",
                "type": "Get",
                "dataType": "json"
            },

            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "Surname", "name": "Surname", "autoWidth": true },
                { "data": "Username", "name": "Username", "autoWidth": true },
                { "data": "Email", "name": "Email", "autoWidth": true },
                { "data": "Password", "name": "Password", "autoWidth": true },

                {
                    "data": "Id",
                    "render": function (data) {
                        return `<a onclick = "showInEdit('Users/Edit/${data}','Update User')"
                                 class= "btn btn-info text-white" > <i class="fas fa-pencil-alt"></i> Edit</a >`
                    },





                    "name": "Edit"
                },
                {
                    "data": "Id",
                    "render": function (data) {
                        return ` <form action = "Users/Delete/${data}" method = "Post" onsubmit = "return jQueryAjaxDelete(this)" class= "d-inline" >
                                    <input type="hidden" data-val="true" data-val-required="The Id field is required." id="item_Id" name="item.Id" value="${data}">
                                        <input type="submit" value="Delete" class="btn btn-danger" />
						         </form>`
                    },
                    "name": "Delete"


                        
                }
            ]


        });
    }

    loadFuncttion();

    showInPopup = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#add-modal .modal-body').html(res);
                $('#add-modal .modal-title').html(title);
                $('#add-modal').modal('show');
            }
        })
    }

    showInEdit = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#edit-modal .modal-body').html(res);
                $('#edit-modal .modal-title').html(title);
                $('#edit-modal').modal('show');
            }
        })
    }

    jQueryAjaxEdit = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        loadFuncttion();
                        $('#edit-modal .modal-body').html('');
                        $('#edit-modal .modal-title').html('');
                        $('#edit-modal').modal('hide');
                    }
                    else
                        $('#edit-modal .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }


    jQueryAjaxAdd = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        loadFuncttion();
                        $('#add-modal .modal-body').html('');
                        $('#add-modal .modal-title').html('');
                        $('#add-modal').modal('hide');
                    }
                    else
                        $('#add-modal .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }


    jQueryAjaxDelete = form => {
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function () {
                        loadFuncttion();
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }


});

