"use strict";

const toastorg = $("#Toasts > .toast").clone();
$("#Toasts > .toast").remove();

const cl = "d-none";
const loading = $("#loading");

const PreloadOn = () => {
    loading.removeClass(cl);
};

const PreloadOff = () => {
    loading.addClass(cl);
};

const SuccessToast = (title, msg, time, autohide) => {
    let options = {};

    if (time !== null && time !== undefined && time !== "")
        options.delay = time;
    if (autohide !== null && autohide !== undefined && autohide !== "")
        options.autohide = autohide;

    let toast = toastorg.clone();
    toast.addClass("bg-gradient-success text-white");
    $(".horizontal", toast).addClass("light");
    $(".toast-header", toast).addClass("text-white");
    if (title !== null && title !== "" && title !== undefined)
        $(".toast-title", toast).html(title);
    $(".toast-body", toast).html(msg);
    $("#Toasts").append(toast);
    toast.toast(options);
    toast.toast("show");
};

const DangerToast = (title, msg, time, autohide) => {
    let options = {};

    if (time !== null && time !== undefined && time !== "")
        options.delay = time;
    if (autohide !== null && autohide !== undefined && autohide !== "")
        options.autohide = autohide;

    let toast = toastorg.clone();
    toast.addClass("bg-gradient-danger text-white");
    $(".horizontal", toast).addClass("light");
    $(".toast-header", toast).addClass("text-white");
    if (title !== null && title !== "" && title !== undefined)
        $(".toast-title", toast).html(title);
    $(".toast-body", toast).html(msg);
    $("#Toasts").append(toast);
    toast.toast(options);
    toast.toast("show");
};

const WarningToast = (title, msg, time, autohide) => {
    let options = {};

    if (time !== null && time !== undefined && time !== "")
        options.delay = time;
    if (autohide !== null && autohide !== undefined && autohide !== "")
        options.autohide = autohide;

    let toast = toastorg.clone();
    toast.addClass("bg-gradient-warning text-black");
    $(".horizontal", toast).addClass("dark");
    $(".toast-header", toast).addClass("text-black");
    if (title !== null && title !== "" && title !== undefined)
        $(".toast-title", toast).html(title);
    $(".toast-body", toast).html(msg);
    $("#Toasts").append(toast);
    toast.toast(options);
    toast.toast("show");
};

const InfoToast = (title, msg, time, autohide) => {
    let options = {};

    if (time !== null && time !== undefined && time !== "")
        options.delay = time;
    if (autohide !== null && autohide !== undefined && autohide !== "")
        options.autohide = autohide;

    let toast = toastorg.clone();
    toast.addClass("bg-gradient-info text-white");
    $(".horizontal", toast).addClass("light");
    $(".toast-header", toast).addClass("text-white");
    if (title !== null && title !== "" && title !== undefined)
        $(".toast-title", toast).html(title);
    $(".toast-body", toast).html(msg);
    $("#Toasts").append(toast);
    toast.toast(options);
    toast.toast("show");
};

function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

if ($.validator !== undefined) {
    $.validator.setDefaults({
        ignore: [],
        highlight: (element, errorClass, validClass) => {
            if (validClass !== undefined && validClass !== null && validClass !== "") {
                $(element, element.form).closest("div").removeClass("is-valid").addClass("is-invalid");
                $(element, element.form).removeClass("is-valid").addClass("is-invalid");
            }
            else {
                $(element, element.form).closest("div").removeClass("is-valid").removeClass("is-invalid");
                $(element, element.form).removeClass("is-valid").removeClass("is-invalid");
            }
        },
        unhighlight: (element, errorClass, validClass) => {
            if (validClass !== undefined && validClass !== null && validClass !== "") {
                $(element, element.form).closest("div").removeClass("is-invalid").addClass("is-valid");
                $(element, element.form).removeClass("is-invalid").addClass("is-valid");
            }
            else {
                $(element, element.form).closest("div").removeClass("is-valid").removeClass("is-invalid");
                $(element, element.form).removeClass("is-valid").removeClass("is-invalid");
            }
        },
        submitHandler: (form) => {
            form.submit();
        }
    });
}

if ($.fn.dataTable !== undefined) {
    $.fn.dataTable.ext.errMode = null;

    $("table").on("error.dt", function (event, settings, techNote, message) {
        const _jqXHR = settings.jqXHR;
        if (_jqXHR.status === 404)
            WarningToast(null, "Favor de notificar a sistemas");
        else if (_jqXHR.status === 401)
            window.location.href = urlapplication;
        else if (_jqXHR.statusText !== "abort" && _jqXHR.responseText.toLowerCase().indexOf("anti-forgery") > -1)
            window.location.reload()
        else if (_jqXHR.statusText !== "abort")
            DangerToast("Error", _jqXHR.status === 200 ? message : _jqXHR.responseText, null, false);
        else
            DangerToast(null, "Ha ocurrido algo inesperado favor de intentarlo de nuevo, si el error persiste favor de comunicarse a sistemas");
    });

    $.fn.DataTable.defaults.fnDrawCallback = function (settings) {
        let tbl = this;
        $(tbl).DataTable().columns.adjust();
        let tt = $(`[data-bs-toggle="tooltip"]`, tbl);
        if (tt.length > 0)
            tt.tooltip();
    };

    $.fn.DataTable.defaults.fnInitComplete = function (settings, json) {
        if ($.fn.select2 !== undefined)
            $(this).closest(".dataTables_wrapper").find(".dataTables_length select").select2({ minimumResultsForSearch: Infinity });
    };

    $.extend(true, $.fn.dataTable.ext.classes, {
        sFilterInput: "form-control form-control-sm",
        sLengthSelect: "form-select form-select-sm dliselect2"
    });

    $.extend(true, $.fn.dataTable.defaults, {
        responsive: false,
        pagingType: "full_numbers",
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        pageLength: 5,
        oLanguage: {
            sZeroRecords: "No se encontraron resultados",
            sEmptyTable: "Ningún dato disponible en esta tabla",
            sInfo: "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            sInfoEmpty: "Mostrando registros del 0 al 0 de un total de 0 registros",
            sInfoFiltered: "(filtrado de un total de _MAX_ registros)",
            sInfoPostFix: "",
            sUrl: "",
            sInfoThousands: ",",
            sLoadingRecords: "",
            oAria: {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            searchPlaceholder: "Buscar registros",
            sSearch: "Buscar: _INPUT_",
            sLengthMenu: "Mostrar </label> _MENU_",
            sLength: "dataTables_length",
            oPaginate: {
                sFirst: `<i class="fas fa-backward-fast"></i><span class="sr-only">Primero</span>`,
                sPrevious: `<i class="fas fa-backward-step"></i><span class="sr-only">Anterior</span>`,
                sNext: `<i class="fas fa-forward-step"></i><span class="sr-only">Siguiente</span>`,
                sLast: `<i class="fas fa-forward-fast"></i><span class="sr-only">Último</span>`
            }
        }
    });
}

if ($.fn.select2 !== undefined) {
    $.fn.select2.defaults.set("theme", "bootstrap-5");
    $.fn.select2.defaults.set("dropdownCssClass", "fc fc-sm");

    const select2 = $.fn.select2;
    $.fn.select2 = function (options) {
        if (this.length > 0) {
            return $($.map(this, (_element, _index) => {
                let _options = options || {};

                if (typeof _options !== "string") {
                    $.extend(true, _options, {
                        dropdownCssClass: "fc fc-sm"
                    });

                    if ((_options.dropdownParent === null || _options.dropdownParent === undefined) && $(_element).closest(".tab-pane").length === 0)
                        _options.dropdownParent = $(_element).parent();
                }

                return select2.call($(_element), _options)[0];
            }));
        }
        else
            return select2.call(this, options);
    };

    $(".dlwsselect2").select2();

    $(".dliselect2").select2({
        minimumResultsForSearch: Infinity
    });
}

var win = navigator.platform.indexOf('Win') > -1;
if (win && document.querySelector('#sidenav-scrollbar')) {
    var options = {
        damping: '0.5'
    }
    Scrollbar.init(document.querySelector('#sidenav-scrollbar'), options);
}

if (typeof (simpleDatatables) !== "undefined") {
    if ($("#datatable-basic").length > 0) {
        const dataTableBasic = new simpleDatatables.DataTable("#datatable-basic", {
            searchable: true,
            fixedHeight: false
        });
    }

    if ($("#datatable-search").length > 0) {
        const dataTableSearch = new simpleDatatables.DataTable("#datatable-search", {
            searchable: true,
            fixedHeight: true
        });
    }
}

$("#btnCambiaEsp").click(function () {
    let selectedEsp = $("#cmbEsp").val();
    $.ajax({
        type: 'POST',
        url: urlapplication + 'CambioEspecialidad',
        data: { "espSeleccionada": selectedEsp },
        success: function (data) {
            swal("Acción Satisfactoria", "Se cambio de Especialidad Satisfactoriamente", "success");
            document.location.reload();
        }
    });
});

$(window).on("load", () => {
    PreloadOff();
});