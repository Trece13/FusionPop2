$(document).ready(function () {
    RedimencionExplorer();
    $('#Cuerpo').show(500);
    var myPage = "<%= namePage %>";
    //$('#LblHome').html(<%= namePage %>" - " + " Phoenix  Operation Portal ");
    //HomeTitle();
    $('#lblPage').show("1000");
    $('#LblHome').show("slow");
    var hora = $("#LblDate1");
    var months = [
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
            "November", "December"
        ];
    setInterval(function () {
        
        var HoraActual = new Date();
        var Format = "Date : " + HoraActual.getDate() + " " + months[HoraActual.getMonth()] + " " + HoraActual.getFullYear() + ", " + HoraActual.getHours() + ":" + (HoraActual.getMinutes() >= 10 ? HoraActual.getMinutes() : "0" + HoraActual.getMinutes()) + " " + (HoraActual.getHours() >= 12 ? 'PM' : 'AM');
        hora.html(Format);
    },
            100);

    
});

function HomeTitle() {
    if ($('#LblTitleMainMenu')[0]) {
        $('#LblHome').show("slow");
    } else {
        $('#LblHome').hide();
    }
}

function navegador() {
    var agente = window.navigator.userAgent;
    var navegadores = ["Chrome", "Firefox", "Safari", "Opera", "Trident", "MSIE", "Edge"];
    for (var i in navegadores) {
        if (agente.indexOf(navegadores[i]) != -1) {
            return navegadores[i];
        }
        else {
            return "Explorer"
        }
    }
}

function RedimencionExplorer() {
    var Navegador = navegador();
    if (Navegador == "Explorer") {
        $("#Navp").css({ 'zoom': '100%' });
        $("#footer").css({ 'zoom': '100%' });
    }
}