currentUrl = $(location).attr('pathname');
tag = $(document);
if (tag.find(".validation-summary-errors").find("div.closeNote").length == 0) {
    text = $(document).find(".validation-summary-errors").text();    
    tag.find(".validation-summary-errors").html("  " + text);
    tag.find(".validation-summary-errors").prepend("<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><i class=\"icon-remove\"></i></button>");
    tag.find(".validation-summary-errors").prepend("<strong><i class=\"icon-remove\"></i></strong>");    
    tag.find(".validation-summary-errors").toggleClass("alert alert-danger")
}

function widthGetter(largura, margem) {
    return function () {
        var a = 100;
        var c = largura - 5;
        var b = (largura - margem);
        porcentagem = (a * b) / c;

        console.log("a: " +a);
        console.log("b: " + b);
        console.log("a*b: " + porcentagem);
        console.log("c: " +c);
        return porcentagem + "%";
    };
}

function alteraTamanho() {
    // Campos <input /> em geral
    $("input").each(function () {

        // Pega a id do input
        var id = Object;
        id = $(this).attr("id");

        // Se tiver id continua, um input sempre tem que ter um id.
        if (id !== "") {

            if ($(this).is(":visible")) {
                // Pega o tamanho da DIV pai <div class="spanX" />
                var tamanho = parseFloat($(this).parent("div").width());
                
                if ($(this).attr("type") == "text") {
                    $(this).css("width", widthGetter(tamanho, 12));
                }
            }

        }
    });
}

// Begin Funcoes da Grid

function style_edit_form(form) {
    //enable datepicker on "sdate" field and switches for "stock" field
    form.find('input[name=sdate]').datepicker({ format: 'yyyy-mm-dd', autoclose: true })
        .end().find('input[name=stock]')
              .addClass('ace ace-switch ace-switch-5').wrap('<label class="inline" />').after('<span class="lbl"></span>');

    //update buttons classes
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove();//ui-icon, s-icon
    buttons.eq(0).addClass('btn-primary').prepend('<i class="icon-ok"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>')

    buttons = form.next().find('.navButton a');
    buttons.find('.ui-icon').remove();
    buttons.eq(0).append('<i class="icon-chevron-left"></i>');
    buttons.eq(1).append('<i class="icon-chevron-right"></i>');
    $("#editmodgrid-table").css("width", "auto");    
}

//switch element when editing inline
function aceSwitch(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=checkbox]')
                .wrap('<label class="inline" />')
            .addClass('ace ace-switch ace-switch-5')
            .after('<span class="lbl"></span>');
    }, 0);    
}
//enable datepicker
function pickDate(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=text]')
                .datepicker({ format: 'yyyy-mm-dd', autoclose: true });
    }, 0);
}

function style_search_filters(form) {
    form.find('.delete-rule').val('X');
    form.find('.add-rule').addClass('btn btn-xs btn-primary');
    form.find('.add-group').addClass('btn btn-xs btn-success');
    form.find('.delete-group').addClass('btn btn-xs btn-danger');
}
function style_search_form(form) {
    var dialog = form.closest('.ui-jqdialog');
    var buttons = dialog.find('.EditTable')
    buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'icon-retweet');
    buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'icon-comment-alt');
    buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'icon-search');
}

function style_delete_form(form) {
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove();//ui-icon, s-icon
    buttons.eq(0).addClass('btn-danger').prepend('<i class="icon-trash"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>')
    $("#delmodgrid-table").css("width", "auto");
}


function beforeDeleteCallback(e) {
    var form = $(e[0]);
    if (form.data('styled')) return false;

    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
    style_delete_form(form);

    form.data('styled', true);
}

function beforeEditCallback(e) {
    var form = $(e[0]);
    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
    style_edit_form(form);
}


//replace icons with FontAwesome icons like above
function updatePagerIcons(table) {
    var replacement =
    {
        'ui-icon-seek-first': 'icon-double-angle-left bigger-140',
        'ui-icon-seek-prev': 'icon-angle-left bigger-140',
        'ui-icon-seek-next': 'icon-angle-right bigger-140',
        'ui-icon-seek-end': 'icon-double-angle-right bigger-140'
    };
    $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

        if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
    })
}

function enableTooltips(table) {
    $('.navtable .ui-pg-button').tooltip({ container: 'body' });
    $(table).find('.ui-pg-div').tooltip({ container: 'body' });
}

function updateActionIcons(table) {
    var replacement =
    {
        'ui-icon-pencil': 'icon-pencil blue',
        'ui-icon-trash': 'icon-trash red',
        'ui-icon-disk': 'icon-ok green',
        'ui-icon-cancel': 'icon-remove red'
    };
    $(table).find('.ui-pg-div span.ui-icon').each(function () {
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));
        if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
    })
}

// End Funcoes da Grid

$("#btnSettings").on('click', function () {
    location.href = "/Portal/Configuracoes/Configuracao";
})

$(document).ready(function () {
    alteraTamanho();
    //$("div[id^='jEditButton']").attr("onclick", "");    
});