$(document).ready(function () { 
    console.log("crap.js v1.1 inintialized");

    var userLang = navigator.language || navigator.userLanguage;

    if (localStorage.getItem("selectedLang") === null) {
        console.log('selectedLang is empty');
        localStorage.setItem('selectedLang', userLang);
        console.log(`selectedLang now is ${localStorage.selectedLang}`);
    }

    $.setRussianLang = function () {
        $("#lang_en_btn").removeClass("active");
        $("#lang_ru_btn").addClass("active");
        $("[data-localize]").localize("site", { language: "ru", pathPrefix: "/lib/jquery-localize/lang/" });
    };

    $.setEnglishLang = function () {
        $("#lang_ru_btn").removeClass("active");
        $("#lang_en_btn").addClass("active");
        $("[data-localize]").localize("site", { language: "en", pathPrefix: "/lib/jquery-localize/lang/" });
    };

    switch (localStorage.selectedLang) {
        case "ru-RU":
            $.setRussianLang();
        break;
//      case "en-US":
//          $.setEnglishLang();
//          break;
        default:
            $.setEnglishLang();
    }

     
    $("#lang_ru_btn").click(function (e) {
        console.log("lang_ru_btn");
        userLang = "ru-RU";
        localStorage.setItem('selectedLang', userLang);
        console.log(`selectedLang is ${localStorage.selectedLang}`);
        $.setRussianLang();
    });

    $("#lang_en_btn").click(function (e) {
        console.log("lang_en_btn");
        userLang = "en-US";
        localStorage.setItem('selectedLang', userLang);
        console.log(`selectedLang is ${localStorage.selectedLang}`);
        $.setEnglishLang();
    });
  
});

