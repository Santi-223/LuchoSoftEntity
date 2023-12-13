let url = 'http://localhost:5267/api/Usuario/Autenticar';

const validarUsuario = async () => {
    let email = document.getElementById('correoacceso').value;
    let contraseña = document.getElementById('contrasenaacceso').value;

    let usuario = {
        email: email,
        contraseña: contraseña,
    };

    fetch(url, {
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify(usuario), // Convertir a formato JSON
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then((resp) => {
            if (resp.status === 200) {
                // Si el código de estado es 200 OK (éxito), procesa la respuesta
                return resp.json();
            } else if (resp.status === 401) {
                // Si el código de estado es 401 Unauthorized (error), maneja el error
                return resp.json().then(errorData => {
                    throw new Error(`Error ${resp.status}: ${errorData.title}`);
                });
            } else {
                // Otros códigos de estado pueden ser manejados aquí
                throw new Error(`Error ${resp.status}: ${resp.statusText}`);
            }
        })
        .then(json => {
            // Si la respuesta es exitosa (status 200), procesa los datos
            console.log(json.token); // El token JWT
            console.log(json.resultado); // El indicador booleano
            console.log(json.mensaje); // El mensaje de la API
            console.log(json.emailUsuario); // El email del usuario

            Swal.fire({
                title: "Bienvenido a LuchoSoft",
                icon: 'success',
                showCancelButton: false, // Evita que aparezca el botón "Cancelar"
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.isConfirmed) {
                    // El usuario hizo clic en "OK"
                    window.location.href = '/Home/Index'; // Redireccionar después del clic en OK
                }
            });

            // Resto del código para manejar la respuesta de la API
        })
        .catch(error => {
            // Manejo de errores
            console.error('Error en la solicitud:', error);
            // Si hay un error durante la solicitud
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Usuario no registrado',
                timer: 3000


            })
        });
};

function validacionacceso() {

    let correox = document.getElementById('correoacceso').value;
    let correo = correox.toLowerCase();
    let contrasena = document.getElementById('contrasenaacceso').value

    let caracteres = /^[a-zA-Z0-9_@.-ñÑ]+$/;


    if (correo === "" || contrasena === "") {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Por favor, complete todos los campos.',
            timer: 3000
        });


    } else if (!caracteres.test(correo) || contrasena === "") {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Por favor, ingrese caracteres válidos.',
            timer: 3000

        });

    } else{

        validarUsuario();
    }
}
function validacionrecpw() {

    let correox = document.getElementById('correoacceso').value;
    let correo = correox.toLowerCase();
    let contrasena = document.getElementById('contrasenaacceso').value
    let contrasena2 = document.getElementById('contrasenaacceso2').value

    let caracteres = /^[a-zA-Z0-9_@.-ñÑ]+$/;



    if (correo === "" || contrasena === "") {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Por favor, complete todos los campos.',
            timer: 3000
        });


    } else if (contrasena < 9999999) {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'La contraseña debe tener mínimo 8 caracteres.',
            timer: 3000

        });

    } else if (!caracteres.test(correo)) {

        Swal.fire({
            icon: 'error',
            title: 'Alerta',
            text: 'Por favor, ingrese caracteres válidos.',
            timer: 10000

        });

    }
    else if (correo === "arley@gmail.com" && contrasena != contrasena2) {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Las contraseñas tienen que ser iguales.',
            timer: 3000


        });

    } else if (correo === "arley@gmail.com" && contrasena === contrasena2) {

        Swal.fire({
            icon: 'info',
            title: 'Revisa tu correo electrónico',
            text: 'Te hemos enviado un mensaje para validar que eres tú.',
            timer: 10000


        });
    } else {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Usuario no registrado',
            timer: 3000


        })
    }

}

