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
function AddIngredient(item, number) {

    var obj = { "Name": item, "Number": number };

    ingredients.push(obj);

    $("#ing").append("<li>" + obj.Name + ": " + obj.Number + "</li>");

}

var instructions = [];
function AddInstruction(item) {
    var l = instructions.length;
    var obj = { "Insturction" : item, "Number" : l};
    instructions.push(obj);
    $("#inst").append("<li>" + obj.Number + ". : " + obj.Insturction + "</li>");
}

$("#IngredButton").on("click", function () {
    if ($("#IngredTemp").val() !== "" && $("#IngredTempNum").val() > 0) {
        AddIngredient($("#IngredTemp").val(), $("#IngredTempNum").val());

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
    
     $('#UploadRecipe').submit();
    console.log(jsonData);
}

$("#UploadButton").on("click", function () { UploadRecipes(); });