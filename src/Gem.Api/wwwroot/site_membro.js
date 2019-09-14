const uri = "api/membro";

let membros = null;

function getCount(data) {

  const el = $("#counter");

  let nome = "membro";

  if (data) {

    if (data > 1) {

      nome = "membros";

    }

    el.text(data + " " + nome);

  } else {

    el.text("No " + nome);

  }

}



$(document).ready(function() {

  getData();

});



function getData() {

  $.ajax({

    type: "GET",

    url: uri,

    cache: false,

    success: function(data) {

      const tBody = $("#membros");



      $(tBody).empty();



      getCount(data.length);



      $.each(data, function(key, item) {

        const tr = $("<tr></tr>")

          .append(

            $("<td></td>").append(

              $("<input/>", {

                type: "checkbox",

                disabled: true,

                checked: item.status

              })

            )

          )

          .append($("<td></td>").text(item.nome))

          .append(

            $("<td></td>").append(

              $("<button>Edit</button>").on("click", function() {

                editItem(item.id);

              })

            )

          )

          .append(

            $("<td></td>").append(

              $("<button>Delete</button>").on("click", function() {

                deleteItem(item.id);

              })

            )

          );



        tr.appendTo(tBody);

      });



      membros = data;

    }

  });

}



function addItem() {

  const item = {

    nome: $("#add-nome").val()

  };



  $.ajax({

    type: "POST",

    accepts: "application/json",

    url: uri,

    contentType: "application/json",

    data: JSON.stringify(item),

    error: function(jqXHR, textStatus, errorThrown) {

      alert("Something went wrong!");

    },

    success: function(result) {

      getData();

      $("#add-nome").val("");

    }

  });

}



function deleteItem(id) {

  $.ajax({

    url: uri + "/" + id,

    type: "DELETE",

    success: function(result) {

      getData();

    }

  });

}



function editItem(id) {

  $.each(membros, function(key, item) {

    if (item.id === id) {

      $("#edit-nome").val(item.nome);

      $("#edit-id").val(item.id);

      $("#edit-status")[0].checked = item.status;

    }

  });

  $("#spoiler").css({ display: "block" });

}



$(".my-form").on("submit", function() {

  const item = {

    nome: $("#edit-nome").val(),

    status: $("#edit-status").is(":checked"),

    id: $("#edit-id").val()

  };



  $.ajax({

    url: uri + "/" + $("#edit-id").val(),

    type: "PUT",

    accepts: "application/json",

    contentType: "application/json",

    data: JSON.stringify(item),

    success: function(result) {

      getData();

    }

  });



  closeInput();

  return false;

});



function closeInput() {

  $("#spoiler").css({ display: "none" });

}