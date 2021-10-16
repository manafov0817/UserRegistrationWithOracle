$(document).ready(function () {

    var loadUsers = function () {
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


                {
                    "data": "Image.Name",
                    "render": function (data) {
                        return `<div class= "userImage m-auto"" >
                                     <img src="/images/${data}" />
                                </div>`
                    },


                    "name": "Photo"
                },


                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "Surname", "name": "Surname", "autoWidth": true },
                { "data": "Username", "name": "Username", "autoWidth": true },
                { "data": "Email", "name": "Email", "autoWidth": true },
                { "data": "Password", "name": "Password", "autoWidth": true },


                {
                    "data": "Location",
                    "render": function (data) {
                        return `<div class="row">
	                                <a onclick="showInPopupViewLocation('/Users/ViewLocation','${data.Latitude}', '${data.Longitude}','${data.MarkAs}')"
                                                                 class= "btn btn-primary text-white m-auto" >Show near</a>
                                </div>`
                    },
                    "name": "Location"
                },

                {
                    "data": "Id",
                    "render": function (data) {
                        return `<div class="row">
	                                <a onclick = "showInEdit('Users/Edit/${data}','Update User')"
                                                                 class= "btn btn-warning text-white m-auto" >Edit</a>
                                </div>`
                    },
                    "name": "Edit"
                },
                {
                    "data": "Id",
                    "render": function (data) {
                        return ` <form action = "Users/Delete/${data}" method = "Post" onsubmit = "return jQueryAjaxDelete(this)" class= "row justify-content-center align-items-center" >
                                    <input type="hidden" data-val="true" data-val-required="The Id field is required." id="item_Id" name="item.Id" value="${data}">
                                        <input type="submit" value="Delete" class="btn btn-danger" />
						         </form>`
                    },
                    "name": "Delete"
                }
            ]
        });
    }

    var loadCities = function () {
        $('#cityTable').DataTable({
            "filter": true,
            "processing": true,
            "destroy": true,
            "paging": true,
            "searching": false,
            "ajax": {
                "url": "City/IndexJson",
                "type": "Get",
                "dataType": "json"
            },

            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "CountryName", "name": "Country", "autoWidth": true },


                {
                    "data": "Id",
                    "render": function (data) {
                        return `<div class="row">
	                                <a onclick="showInEditUser('City/Edit/${data}','Update City')"
	                                   class="btn btn-warning text-white m-auto ">
		                                <i class="fas fa-pencil-alt"></i>
		                                Edit
	                                </a>
                                </div>`
                    },
                    "name": "Edit"
                },
                {
                    "data": "Id",
                    "render": function (data) {
                        return ` <form action = "City/Delete/${data}" method = "Post" onsubmit = "return jQueryAjaxDeleteCity(this)" class= "d-inline" >
                                    <input type="hidden" data-val="true" data-val-required="The Id field is required." id="item_Id" name="item.Id" value="${data}">
                                        <input type="submit" value="Delete" class="btn btn-danger" />
						         </form>`
                    },
                    "name": "Delete"
                }
            ]
        });
    }

    var loadLocations = function () {
        $('#locationTable').DataTable({
            "filter": true,
            "processing": true,
            "destroy": true,
            "paging": true,
            "searching": true,
            "ajax": {
                "url": "Location/IndexJson",
                "type": "Get",
                "dataType": "json"
            },

            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Latitude", "name": "Latitude", "autoWidth": true },
                { "data": "Longitude", "name": "Longitude", "autoWidth": true },
                { "data": "UserUsername", "name": "User Username", "autoWidth": true },
                { "data": "MarkAs", "name": "Mark As", "autoWidth": true },



                {
                    "data": "Id",
                    "render": function (data) {
                        return `<div class="row">
	                                <a onclick="showInEditUser('City/Edit/${data}','Update City')"
	                                   class="btn btn-warning text-white m-auto ">
		                                <i class="fas fa-pencil-alt"></i>
		                                Edit
	                                </a>
                                </div>`
                    },
                    "name": "Edit"
                },
                {
                    "data": "Id",
                    "render": function (data) {
                        return ` <form action = "City/Delete/${data}" method = "Post" onsubmit = "return jQueryAjaxDeleteCity(this)" class= "d-inline" >
                                    <input type="hidden" data-val="true" data-val-required="The Id field is required." id="item_Id" name="item.Id" value="${data}">
                                        <input type="submit" value="Delete" class="btn btn-danger" />
						         </form>`
                    },
                    "name": "Delete"
                }
            ]
        });
    }

    loadCities();

    loadUsers();

    loadLocations();

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

    showInPopupLocation = (url, latitude, longitude, markAs) => {
        $.ajax({
            url: url,
            data: { latitude, longitude, markAs },
            type: 'get',
            success: function (res) {


                $('#lngVal').val('');
                $('#latVal').val('');
                $('#markVal').val('');

                $('#show-map-modal .modal-body').html(res);
                $('#show-map-modal .modal-title').html(markAs);
                $('#show-map-modal').modal('show');
            }
        })
    }

    showInPopupViewLocation = (url, latitude, longitude, markAs) => {
        $.ajax({
            url: url,
            data: { latitude, longitude, markAs },
            type: 'get',
            success: function (res) {

                $('#viewlng').val('');
                $('#viewlat').val('');
                $('#viewmark').val('');

                $('#show-map-modal .modal-body').html(res);
                $('#show-map-modal .modal-title').html(markAs);
                $('#show-map-modal').modal('show');

            }
        })
    }

    showInPopupCity = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#add-city-modal .modal-body').html(res);
                $('#add-city-modal .modal-title').html(title);
                $('#add-city-modal').modal('show');
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

    showInEditUser = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#edit-city-modal .modal-body').html(res);
                $('#edit-city-modal .modal-title').html(title);
                $('#edit-city-modal').modal('show');
            }
        })
    }

    jQuerySetLocation = form => {

        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {

                        $('#lngVal').val(res.location.longitude);
                        $('#latVal').val(res.location.latitude);
                        $('#markVal').val(res.location.markAs);

                        $('#show-map-modal .modal-body').html('');
                        $('#show-map-modal .modal-title').html('');
                        $('#show-map-modal').modal('hide');

                    }
                    else
                        $('#show-map-modal .modal-body').html(res.html);
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
                        $('#add-modal .modal-body').html('');
                        $('#add-modal .modal-title').html('');
                        $('#add-modal').modal('hide');
                        loadUsers();
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

    jQueryAjaxAddCity = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        loadCities();
                        $('#add-city-modal .modal-body').html('');
                        $('#add-city-modal .modal-title').html('');
                        $('#add-city-modal').modal('hide');
                    }
                    else
                        $('#add-city-modal .modal-body').html(res.html);
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
                        loadUsers();
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

    jQueryAjaxDeleteCity = form => {
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function () {
                        loadCities();
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
                        loadUsers();
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

    $('.js-example-basic-single').select2({
        ajax: {
            url: "City/GetNames",
            type: "get",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                var query = {
                    search: params.term,
                }
                return query;
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        }
    });
});

