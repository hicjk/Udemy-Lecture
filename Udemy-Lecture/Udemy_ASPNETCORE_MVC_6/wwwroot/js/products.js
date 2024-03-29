﻿let dataTable;

$(document).ready(function () {
    $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}"
                            class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a onclick=Delete('/Admin/Product/Delete/${data}')
                            class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                        `
                },
                "width": "15%"
            }
        ]
    });
});

function Delete(url) {

    let isDelete = confirm("삭제하시겠습니까?");

    if (isDelete) {
        $.ajax({
            url: url,
            type: "DELETE",
            success: function (data) {
                if (data.success) {
                    console.log(data.message);
                } else {
                    console.log(data.message);
                }
            }
        })
    }
}