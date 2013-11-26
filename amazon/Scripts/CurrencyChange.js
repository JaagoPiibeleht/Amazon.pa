/// <reference path="jquery-2.0.3.intellisense.js" />
var from = "USD";
function changePrices(sel) {
    var to = sel.options[sel.selectedIndex].label;
    var url = "/Pea/Currency/" + from + "/" + to;
    $.get(url, function (data) {
        $("currency").val(from).attr("selected", false);
        $("currency").val(to).attr("selected", true);
        from = to;

        $(".price").each(function (i) {
            if ($(this).text() != "Unavailable") {                
                var price = $(this).html();
                var newprice = Number(price) * data;
                $(this).html(newprice.toFixed(2));
            }
        });
    });
}