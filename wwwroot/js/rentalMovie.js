window.onload = CargarPagina();

function CargarPagina(){
    SearchMovieTemp();
}

function CancelRental() {
    //console.log("Cancelar alquiler");

    $.ajax({
        type: "POST",
        url: "../../Rentals/CancelRental",
        data: {},
        success: function(resultado) {
            if(resultado == true){
                location.href="../../Rentals/Index";
            }
        },
        error(result){
            console.log(result);
        }
    })
}


function SearchMovieTemp() {
    //console.log("Busca la movie");

    $("#tableMovies").empty();

    $.ajax({
        type: "GET",
        url: "../../Rentals/SearchMovieTemp",
        data: {},
        success: function(ListadoMovieTemp) {

            $.each(ListadoMovieTemp, function(index, item){
                $("#tableMovies").append(
                    "<tr>" +
                        "<th>" + item.movieName + "</th>" +
                        "<th></th>" +
                    "</tr>"
                );
            });

        },
        error(result){
            console.log(result);
        }
    })
}