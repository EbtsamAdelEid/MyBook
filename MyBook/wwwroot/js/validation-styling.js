//jQuary Unobtrusive validaion default

$.validator.setDefaults({
    errorClass: "",
    validClass: "",

    highlight: function (element , _errorClass , _validClass){
        $(element).addClass("is-invalid").removeClass("is-valid");
        $(element.form).find("[data-valmsg-form" +element.id+"]" ).addClass("invalid-feedback");
    },

    unhighlight: function (element , _errorClass , _validClass){
        $(element).addClass("is-valid").removeClass("is-invalid");
        $(element.form).find("[data-valmsg-form" +element.id+"]" ).removeClass("invalid-feedback");
    },

});
