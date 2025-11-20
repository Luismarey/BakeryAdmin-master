$(document).ready(function () {
    // Abrir modal vacío para agregar nueva dirección
    $(document).on('click', '#btnAgregarDireccion', function () {
        var $form = $("#direccionForm");
        console.log($form.serialize()); // Para depuración

        //if ($form.length) {
        //    $form[0].reset(); // Reinicia los campos
        //    $form.removeClass('was-validated'); // Limpia validación previa
        //}

        $('#DireccionId').val(0);
        $('#Activo').prop('checked', true);
        $('#direccionesModal').modal('show');
    });

    // Guardar dirección vía AJAX
    $(document).on('click', '#btnGuardarDireccion', function (e) {
        e.preventDefault();

        var $form = $('#direccionForm');

        console.log($form.serialize()); // Para depuración

        if ($form.length && !$form[0].checkValidity()) {
            $form[0].classList.add('was-validated');
            return;
        }

        $.post('/Direcciones/Save', $form.serialize(), function (response) {
            if (response.success) {
                $('#direccionesModal').modal('hide');

                // Recargar lista de direcciones sin recargar toda la página
                $('#listaDirecciones').load('/Direcciones/Listar/' + $('#PersonaId').val());
            } else {
                Swal.fire('Error', response.message, 'error');
            }
        });
    });

    // Editar dirección
    $(document).on('click', '.btn-edit-direccion', function () {
        var id = $(this).data('id');

        $.get('/Direcciones/Obtener/' + id, function (data) {
            $('#DireccionId').val(data.direccionId);
            $('#Zona').val(data.zona);
            $('#Calle').val(data.calle);
            $('#Numero').val(data.numero);
            $('#NombreEdificio').val(data.nombreEdificio);
            $('#Referencia').val(data.referencia);
            $('#Activo').prop('checked', data.activo);

            var $form = $("#direccionForm");
            if ($form.length) {
                $form.removeClass('was-validated');
            }

            $('#direccionesModal').modal('show');
        });
    });

    // Eliminar dirección
    $(document).on('click', '.btn-delete-direccion', function () {
        var id = $(this).data('id');
        Swal.fire({
            title: '¿Desea eliminar esta dirección?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/Direcciones/Eliminar/' + id, function (response) {
                    if (response.success) {
                        $('#listaDirecciones').load('/Direcciones/Listar/' + $('#PersonaId').val());
                    } else {
                        Swal.fire('Error', response.message, 'error');
                    }
                });
            }
        });
    });
});