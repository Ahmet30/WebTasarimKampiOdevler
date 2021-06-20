// görev 1
$("#gorev1 > button").click(function() {
    for (let index = 0; index < 3; index++) {
        $('#gorev1 > ul').append('<li>'+$('h2:eq('+index+')').text()+'</li>');
    }
});
// görev 2
$("#gorev2 > button").click(function() {
    var text =  document.getElementsByTagName('h2').length;
    $('#gorev2 > input').val(text);
});
// görev 3
$("#gorev3 > button").click(function() {
    $('#gorev3 > input').val($('h1').text());
});
// görev 4
$("#gorev4 > button").click(function() {
    for (let index = 0; index <  document.getElementsByTagName('h2').length ; index++) {
        var textLenght = (' ('+$('.sutun div>p:eq('+index+')').text().length+' karakter)');
        $('.sutun div>h2:eq('+index+')').append(textLenght);
    }
});
// görev 5
$("#gorev5 > button").click(function() {
    $('.sutun div>h2:odd').css({"color":"blue"});
    $('.sutun div>h2:even').css({"color":"orange"});
    $('h1').css({"color":"red"});
});
// görev 6

// görev 7
$("#gorev7 > button").click(function() {
    for (let index = 0; index < $(".sutun div>h2:contains(can)").length ; index++) {
        $("#gorev7 >ul").append('<li>'+$('.sutun div>h2:contains(can):eq('+index+')').text()+'</li>');
    }
});
// görev 8
$("#gorev8 > button").click(function() {
        $.get( "lorem.html", function( data ) {
            $( ".sutun>article" ).prepend( '<h1>'+data+'</h1>' );
            alert( "Load was performed." );
        });
});
// görev 9
$("h2").mouseover(function() {
    $('#gorev9 > input').val($(this).text());
});
// görev 10 //tam olmadi
$("#gorev10 > button").click(function() {
    $('body').append('<img src="/check.png" width="100"; height="100"; position: fixed; margin-left: 50px; margin-bottom: 50px;>');
});