let loadFuncttion = function () {
    $('#userTable').DataTable({
        "processing": true,
        "filter": true,
        "destroy": true,
        "ajax": {
            "url": "Users/IndexJson",
            "type": "Get",
            "dataType": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false,
            }],


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
                    return `<a class='btn btn-warning' style="display:inline-block; width:100%" href='Users/Edit/${data}'>Edit</a>`
                },
            },
            {
                "data": "Id",

                "render": function (data) {
                    return `<a class='btn btn-danger' style="display:inline-block; width:100%" href='Users/Delete/${data}'>Remove</a>`
                },
            }
        ]


    });
}
