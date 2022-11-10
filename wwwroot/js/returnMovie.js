function CargarPaginasDevolucion() {
    SearchReturnTemp();
}

function AddMovieReturn() {
    // console.log("Funcion de agregar pelicula temporal activada")
    var movieID = $("#MovieID").val();
    $.ajax({
        type: "POST",
        url: "../../Returns/AddMovieReturn",
        data: { MovieID: movieID },
        success: function (resultado) {
            if (resultado == true) {
                // console.log("Se guardo la pelicula correctamente");
                alert("Se guardo la pelicula correctamente");
                $("#staticBackdrop").modal("hide");
                SearchReturnTemp();
                Location.href = "../../Returns/Create"
            } else {
                alert("No se pudo agregar la pelicula, intente nuevamente");
                // console.log("No se pudo agregar la pelicula, intente nuevamente");
            }
        },
        error: function(_result) {
            console.log("Error debido a: " + _result)
        },
    });
}

function CancelReturn() {
    $.ajax({
        type: "POST",
        url: "../../Returns/CancelReturn",
        data: {},
        success: function(resultado){
            if(resultado = true)
            {
                location.href = "../../Returns/Create";
            }
        },
        error(result){

        }
    })
}

function SearchReturnTemp() {
    $.ajax({
        type: "GET",
        url: "../../Returns/SearchMovieTemp",
        data: {},
        success: function(ListadoMovieTemp){
            // console.log(ListadoMovieTmp)
            $.each(ListadoMovieTemp, function(index, item){
                $("#Return").append(
                    "<tr>" +
                    "<th>" + item.movieName + "<th>" +
                    "<th>" +
                    "<button class='btn btn-danger' onclick='QuitarMovieReturn(" + item.movieID + ");'>Quitar Pelicula</button>" +
                    "</th>" +
                    "</tr>"
                );
            });
        },
        error(result){

        }
    })
}

function QuitarMovieReturn(id){
    $.ajax({
        type: "POST",
        url: "../../Returns/QuitarMovie",
        data: {MovieID: id},
        success: function(resultado){
            if(resultado == true){
                location.href = "../../Returns/Create";
            }
        },
        error(result){
            
        }
    })
}

function SearchMovieReturn(ReturnID) {
    $('#MovieReturn').empty();
    $.ajax({
        type: "POST",
        url: "../../Returns/SearchMovieReturn",
        data: {ReturnID: ReturnID},
        success: function(ListadoMovie){
            // console.log(ListadoMovie)
            $.each(ListadoMovie, function(index, item){
                $("#MovieReturn").append(
                    "<tr>" +
                    "<th>" + item.movieName + "<th>" +
                    "</tr>"
                );
            });
        },
        error(result){

        }
    })
}
