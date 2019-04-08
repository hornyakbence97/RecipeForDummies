$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

var ingredients = [];
function AddIngredient(item, number, si) {

    var obj = { "Name": item, "Number": number, "Si": si };

    ingredients.push(obj);

    $("#ing").append("<li>" + obj.Name + ": " + obj.Number + " " + obj.Si + "</li>");

}

var instructions = [];
function AddInstruction(item) {
    var l = instructions.length;
    var obj = { "Insturction" : item, "Number" : l};
    instructions.push(obj);
    $("#inst").append("<li>" + obj.Number + ". : " + obj.Insturction + "</li>");
}

$("#IngredButton").on("click", function () {
    if ($("#IngredTemp").val() !== "" && $("#IngredTempNum").val() > 0 && $("#IngredSi").val() !== "") {
        AddIngredient($("#IngredTemp").val(), $("#IngredTempNum").val(), $("#IngredSi").val());

        $("#IngredTemp").val("");
        $("#IngredTempNum").val(1);

    }

});

$("#InsButton").on("click", function () {
    if ($("#InsTemp").val() !== "") {
        AddInstruction($("#InsTemp").val());

        $("#InsTemp").val("");

    }

});

function UploadRecipes() {
    var formData = $('#UploadRecipe').serializeObject();
     var mycheckboxVal = formData.Category;

    if (!$.isArray(mycheckboxVal)) {
        formData.Category = [mycheckboxVal];
    }

    //then you serialize
    var jsonData = JSON.stringify(formData.Category);
    $("#CategoryListJson").val(jsonData);
    $("#IngredientsJson").val(JSON.stringify(ingredients));
    $("#InsturctionsJson").val(JSON.stringify(instructions));

    if ($("#CategoryListJson").val() !== "[null]") {
        $('#UploadRecipe').submit();
    }
    else {
        alert("Select categories!");
    }
    console.log(jsonData);
}

$("#UploadButton").on("click", function () { UploadRecipes(); });




function RateIt(rate, recipeId, userId, url) {
    $.post(url, { rate: rate, recipeId: recipeId, userId: userId });
    setTimeout(function () { location.reload(); }, 200);
}

function SendComment(recipeId, userId, url) {
    var comment = $("#comment").val();

    $.post(url, { recipeId: recipeId, userId: userId, comment: comment });
    setTimeout(function () { location.reload(); }, 200);
   
}


function LoadInstructions(json) {

    var obje = JSON.parse(json.replace(/&quot;/g, '"'));

    var l = obje.length;

    for (var i = 0; i < l; i++) {
        $("#instructions").append('<div class="alert alert-info"><strong>' + (obje[i].Number+1) + '</strong>. step : ' + obje[i].Insturction + '</div>');
    }
}
var ingredtientsLoaded = null;
function LoadIngredients(json) {
    var obje = JSON.parse(json.replace(/&quot;/g, '"'));
    ingredtientsLoaded = obje;
    var l = obje.length;

    for (var i = 0; i < l; i++) {
        $("#ingredientsList").append('<li class="list-group-item">' + obje[i].Number + ' ' + obje[i].Si + ' : ' + obje[i].Name + '</li>');
    }
}

function LoadIngredientsWithExistingData(obje) {

    $("#ingredientsList").empty();

    var l = obje.length;

    for (var i = 0; i < l; i++) {
        $("#ingredientsList").append('<li class="list-group-item">' + obje[i].Number + ' ' + obje[i].Si + ' : ' + obje[i].Name + '</li>');
    }
}

var howManyPerson = 0;
function SetSiToOther(original, newOne) {
    var o = [];
    for (var i = 0; i < ingredtientsLoaded.length; i++) {
        var obj = { Number: ingredtientsLoaded[i].Number, Si: ingredtientsLoaded[i].Si, Name: ingredtientsLoaded[i].Name };
        o[i] = obj;
    }
    for (var i = 0; i < o.length; i++) {
        o[i].Number = (newOne / original) * o[i].Number;
    }
    $("#subForHowMany").empty();
    $("#subForHowMany").append(" Required ingredients for " + howManyPerson + " people:");
    LoadIngredientsWithExistingData(o);
}

//category add:
$("#addcategory").on("click", function () {
    var c = $("#Category").val();

    $.post("CreateCategory?Category=" + c);
    $("#closeModal").click();
    $("#clist").append('<label><input type="checkbox" value="'+c+'" id="'+c+'" name="Category">'+c+'</label>');
});


//favourite:
function AddFavourite(id, userid) {
    $.post("../AddFavourite?id=" + id + "&userid=" + userid);
    $("#favourite").val("Done");
}



//autocomplete:


    function autocomplete(inp, arr) {
  /*the autocomplete function takes two arguments,
  the text field element and an array of possible autocompleted values:*/
  var currentFocus;
    /*execute a function when someone writes in the text field:*/
  inp.addEventListener("input", function(e) {
      var a, b, i, val = this.value;
    /*close any already open lists of autocompleted values*/
    closeAllLists();
      if (!val) { return false;}
    currentFocus = -1;
    /*create a DIV element that will contain the items (values):*/
    a = document.createElement("DIV");
    a.setAttribute("id", this.id + "autocomplete-list");
    a.setAttribute("class", "autocomplete-items");
    /*append the DIV element as a child of the autocomplete container:*/
    this.parentNode.appendChild(a);
    /*for each item in the array...*/
      for (i = 0; i < arr.length; i++) {
        /*check if the item starts with the same letters as the text field value:*/
        if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
        /*create a DIV element for each matching element:*/
        b = document.createElement("DIV");
    /*make the matching letters bold:*/
    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
    b.innerHTML += arr[i].substr(val.length);
    /*insert a input field that will hold the current array item's value:*/
          b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
    /*execute a function when someone clicks on the item value (DIV element):*/
          b.addEventListener("click", function(e) {
            /*insert the value for the autocomplete text field:*/
            inp.value = this.getElementsByTagName("input")[0].value;
        /*close the list of autocompleted values,
        (or any other open lists of autocompleted values:*/
        closeAllLists();
    });
    a.appendChild(b);
  }
}
});
/*execute a function presses a key on the keyboard:*/
  inp.addEventListener("keydown", function(e) {
      var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
      if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
        /*and and make the current item more visible:*/
        addActive(x);
      } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
        /*and and make the current item more visible:*/
        addActive(x);
      } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
          e.preventDefault();
         
        if (currentFocus > -1) {
          /*and simulate a click on the "active" item:*/
            if (x) x[currentFocus].click();
            $("#adding").click();
      }
    }
});
  function addActive(x) {
    /*a function to classify an item as "active":*/
    if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
    if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
      }
  function removeActive(x) {
    /*a function to remove the "active" class from all autocomplete items:*/
    for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
      }
  function closeAllLists(elmnt) {
    /*close all autocomplete lists in the document,
    except the one passed as an argument:*/
    var x = document.getElementsByClassName("autocomplete-items");
    for (var i = 0; i < x.length; i++) {
      if (elmnt != x[i] && elmnt != inp) {
            x[i].parentNode.removeChild(x[i]);
        }
      }
    }
    /*execute a function when someone clicks in the document:*/
  document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
      }

var ingAllList = [];
$.get("GetAllIngredientsList", function (data) {
    ingAllList = $.makeArray(data);
    autocomplete(document.getElementById("ingredientsug"), ingAllList);
    $("#ingredientsug").focus();
});

var iing = [];
$("#adding").on("click", function () {
    var ing = $("#ingredientsug").val();
    if ($.inArray(ing, iing) == -1 && ing.length > 1) {
        iing.push(ing);
        $("#cbl").append(' <label><input checked type="checkbox" value="' + ing + '" id="' + ing + '" name="Ingredients">' + ing + '</label>');
        $("#ingredientsug").val("");
        $("#ingredientsug").focus();
    }
});
