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

function QuitarMovie(id){
    console.log("Peli eliminada");
    console.log(id);

    $.ajax({
        type: "POST",
        url: "../../Rentals/QuitarMovie",
        data: {MovieID: id},
        success: function(resultado) {
            if(resultado == true){
                location.href="../../Rentals/Create";
            }
        },
        error(result){
            console.log(result);
        }
    })
}


