$(function(){
    //When your first group of radio buttons has changed
    $('input:radio[name="ProductClass"]').change(function () {
        //Determine if your other section should be displayed based on the selected value of your group
        var shouldPanelBeDisplayed = $(this).val() == "2";
        //Determines if your panel should be displayed or not
        $("#other-group").toggle(shouldPanelBeDisplayed);
        $("#other-group1").toggle(shouldPanelBeDisplayed);
        $("#other-group2").toggle(shouldPanelBeDisplayed);
        $("#other-group3").toggle(shouldPanelBeDisplayed);
    });
                
    $('input:radio[name="ProductClass"]').change(function () {
        //Determine if your other section should be displayed based on the selected value of your group
        var shouldPanelBeDisplayed = $(this).val() == "1";

        //Determines if your panel should be displayed or not
        $("#other-group4").toggle(shouldPanelBeDisplayed);
        $("#other-group5").toggle(shouldPanelBeDisplayed);
                    
    });
});

$(document).ready(function () {
    $("#other-group").hide();
});
$(document).ready(function () {
    $("#other-group1").hide();
});
$(document).ready(function () {
    $("#other-group2").hide();
});
$(document).ready(function () {
    $("#other-group3").hide();
});
$(document).ready(function () {
    $("#other-group4").hide();
});
$(document).ready(function () {
    $("#other-group5").hide();
});

