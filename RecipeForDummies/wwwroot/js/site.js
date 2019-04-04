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

