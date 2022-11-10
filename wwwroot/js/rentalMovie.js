window.onload = CargarPagina();

function CargarPagina() {

  SearchMovieTemp();

}




function AddMovieTemp() {
  // console.log("Guardo la pelicula")

  var movieID = $('#MovieID').val();

  $.ajax({
    type: "POST",
    url: "../../Rentals/AddMovieTemp",
    data: { MovieID: movieID },
    success: function (resultado) {
      if (resultado == true) {
        //  console.log("guardo bien la Movie en el temporal.");
        $('#staticBackdrop').modal('hide')

        SearchMovieTemp();

        location.href = "../../Rentals/Create";

      }
      else {
        alert("No  se pudo guardar la pelicula, intente nuevamente.");
        // console.log("No  se pudo guardar el libro, intente nuevamente.") 
      }

    },
    error: function (result) {
      console.log("Error debido a: " + result);
    }

  });

}


function CancelarRental() {
  console.log("Cancelar Alquiler")


  $.ajax({
    type: "POST",
    url: "../../Rentals/AddMovieTemp",
    data: {},
    success: function (resultado) {
      if (resultado == true) {
        location.href = "../../Rentals/Create"
      }

    },
    Error(result) {

    }



  });
}

function QuitarMovie(id) {
  console.log("Cancelar Alquiler")


  $.ajax({
    type: "POST",
    url: "../../Rentals/QuitarMovie",
    data: { MovieID: id },
    success: function (resultado) {
      if (resultado == true) {
        location.href = "../../Rentals/Create"
      }

    },
    Error(result) {

    }



  });
}


function SearchMovieTemp() {
  // console.log('Busca la pelicula.')
  $.ajax({
    type: "GET",
    url: "../../Rentals/SearchMovieTemp",
    data: {},
    success: function (ListadoMovieTemp) {
      // console.log(ListadoMovieTemp);
      $.each(ListadoMovieTemp, function (index, item) {
        $("#tablesMovies").append(
          "<tr>" +
          "<th>" + item.movieName + "<th>" +
          "<th>" +
          "<button class='btn btn-danger' onclick='QuitarMovie(" + item.movieID + ");'>Quitar Pelicula</button>" +
          "</th>" +
          "</tr>"
        );
      });


    },
    Error(result) {

    }

  });
};

function SearchMovie(rentalID) {
  // console.log('Busca la pelicula.')
  $('#tableMovie').empty();
  $.ajax({
    type: "POST",
    url: "../../Rentals/SearchMovie",
    data: { RentalID: rentalID },
    success: function (ListadoMovie) {
      // console.log(ListadoMovie);
      $.each(ListadoMovie, function (index, item) {
        $("#tableMovie").append(
          "<tr>" +
          "<th>" + item.movieName + "<th>" +
          "</tr>"
        );
      });


    },
    Error(result) {

    }

  });
};





