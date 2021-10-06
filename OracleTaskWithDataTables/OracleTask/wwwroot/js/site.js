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

                //{
                //    "data": "Id",
                //    "render": function (data) {
                //        return `<a class='btn btn-warning' style="display:inline-block; width:100%" href='Users/Edit/${data}'
                //                data-toggle="ajax-modal" data-target="#editUser">${data}</a>`
                //    },
                //    "name": "Edit"
                //},
                //{
                //    "data": "Id",
                //    "render": function (data) {
                //        return `<a class='btn btn-danger' style="display:inline-block; width:100%" href='Users/Delete/${data}'>${data}</a>`
                //    },
                //    "name": "Delete"
                //}
            ]


        });
    }

    loadFuncttion();


    var holderElement = $('#PlaceHolder');

    var editHolderElement = $('.EditHolder');


    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            holderElement.html(data);
            holderElement.find('.modal').modal('show');

        })
    })




    editHolderElement.on('click', '[data-save="modal"]', function (event) {

        event.preventDefault();

        var form = $(this).parents('.modal').find('form');

        var actionUrl = form.attr('action');

        var sendData = form.serialize();

        $.post(actionUrl, sendData).done(function (data) {
            holderElement.find('.modal').modal('hide');
        });
        loadFuncttion();

    });


    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            holderElement.html(data);
            holderElement.find('.modal').modal('show');

        })
    })




    holderElement.on('click', '[data-save="modal"]', function (event) {

        event.preventDefault();

        var form = $(this).parents('.modal').find('form');

        var actionUrl = form.attr('action');

        var sendData = form.serialize();

        $.post(actionUrl, sendData).done(function (data) {
            holderElement.find('.modal').modal('hide');
        });
        loadFuncttion();

    });

});

