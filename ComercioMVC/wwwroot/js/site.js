// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Función para implementar data tables
var tabla;
function FilterTable() {
    $(document).ready(function () {
        //Llamar a datatable
        tabla = $('#tblBodegas').DataTable({
            language: {
                "decimal": "",
                "emptyTable": "No se han encontrado registros",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            }
        });
    });
};


function DeleteBodega(id) {
    $(document).ready(function () {
        swal({
            title: "¿Estás seguro de Eliminar la Bodega?",
            text: "Este registro no se podrá recuperar",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((borrar) => {
            if (borrar) {
                debugger
                $.ajax({                
                    type: 'POST',
                    url: 'Bodega/Delete/' + id,
                    success: function (data) {
                        if (data.success) {
                            var url = 'Bodega/Index';
                            window.location.href = url;
                        }
                    }
                });
            }
        });
    });
};

function DeleteCategoria(id) {
    $(document).ready(function () {
        swal({
            title: "¿Estás seguro de Eliminar la Categoria?",
            text: "Este registro no se podrá recuperar",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((borrar) => {
            if (borrar) {
                debugger
                $.ajax({
                    type: 'POST',
                    url: 'Categoria/Delete/' + id,
                    success: function (data) {
                        if (data.success) {
                            var url = 'Categoria/Index';
                            window.location.href = url;
                        }
                    }
                });
            }
        });
    });
};


function DeleteMarca(id) {
    $(document).ready(function () {
        swal({
            title: "¿Estás seguro de Eliminar la Marca?",
            text: "Este registro no se podrá recuperar",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((borrar) => {
            if (borrar) {
                debugger
                $.ajax({
                    type: 'POST',
                    url: 'Marca/Delete/' + id,
                    success: function (data) {
                        if (data.success) {
                            var url = 'Marca/Index';
                            window.location.href = url;
                        }
                    }
                });
            }
        });
    });
};
