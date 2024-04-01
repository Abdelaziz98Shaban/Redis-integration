$(document).ready(function () {
    //init menu
    $(".sidebar-menu").metisMenu({
        preventDefault: false,
    });

    // menu scroll
    $(".fixed .sidebar-scroll").slimScroll({
        height: "auto",
        position: "left",
    });

    //SVG converter
    var mySVGsToInject = document.querySelectorAll("img.svg");
    SVGInjector(mySVGsToInject);

    //Menu Trigger Btn
    $(".menu-btn button").on("click", function () {
        if ($("body").hasClass("mini-menu")) {
            $("body").removeClass("mini-menu");
            $("body").addClass("menu-resize");
            setTimeout(function () {
                $("body").removeClass("menu-resize");
            }, 400);
        } else {
            $("body").addClass("mini-menu");
        }
    });

    //date range picker
    var start = moment().subtract(30, "days");
    var end = moment();
    $('input[name="dates"]').daterangepicker({
        startDate: start,
        endDate: end,
        locale: {
            format: "YYYY-MM-DD",
        },
    });

    //date SINGLE picker
    $('input[name="single-date"]').daterangepicker({
        singleDatePicker: true,
        locale: {
            format: "YYYY-MM-DD",
        },
    });

    //menu active link
    var title =
        $(".title-data").data("main") != null &&
            $(".title-data").data("main") != ""
            ? $(".title-data").data("main")
            : $(".title-data").text();
    var $this = $(".sidebar .sidebar-menu li a span:contains(" + title + ")");
    var all = $(".sidebar .sidebar-menu li");

    all.removeClass("active");
    $this.parents("li").addClass("active");
    $this.parents("li").find("a.has-arrow").attr("aria-expanded", "true");
    $this.parents("li ul").addClass("mm-show");
    var breadcrumb = $(".breadcrumb");
    $this.parents("li").each(function () {
        var element = $(this).find("a").first().text();
        var elementHref = $(this).first().find("a").first().attr("href");
        $(
            '<li class="breadcrumb-item" aria-current="page"><a href="' +
            elementHref +
            '">' +
            element +
            "</a></li>"
        ).insertAfter(breadcrumb.find("li:first"));
    });

    //datetime picker
    $(".datetimepicker").datetimepicker({
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: "fa fa-angle-left",
            next: "fa fa-angle-right",
            today: "fa fa-circle-thin",
            clear: "fa fa-trash",
            close: "fa fa-minus",
        },
    });
    //bootstrap select
    $(".bootstrap-select, .select__picker").selectpicker();

    //select2 select
    $(".select2").select2({
        theme: "bootstrap4",
    });

    //selectize
    $(".selectize").selectize({
        plugins: ["remove_button"],
        delimiter: ",",
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input,
            };
        },
    });

    //Jquery Steps
    $(".wizard").steps({
        headerTag: ".w-title",
        bodyTag: ".w-body",
        transitionEffect: "fade",
        titleTemplate: '<span class="number">#index#</span> #title#',
        // enableAllSteps: true,
    });

    //touchspin
    $(".touchspin").TouchSpin({
        buttondown_class: "btn btn-secondary",
        buttonup_class: "btn btn-secondary",
    });

    //lightGallery
    const openImgs = $("[id*=btn__imgs_]");
    $(".lightGallery").lightGallery();
    if (openImgs.length > 0) {
        openImgs.on("click", function () {
            $(this).parents(".pending").find("li[id*=firstImage_]").trigger("click");
        });
    }

    // bootstrap borthday
    if ($(".birthdate.en").length > 0) {
        $(".birthdate.en").bootstrapBirthday({
            widget: {
                wrapper: {
                    tag: "div",
                    class: "birthdate row",
                },
                wrapperYear: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                wrapperMonth: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                wrapperDay: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                selectYear: {
                    name: "birthday[year]",
                    class: "form-control input-sm",
                },
                selectMonth: {
                    name: "birthday[month]",
                    class: "form-control input-sm",
                },
                selectDay: {
                    name: "birthday[day]",
                    class: "form-control input-sm",
                },
            },
            text: {
                dateFormat: "littleEndian",
                year: "Year",
                month: "Month",
                day: "Day",
                months: {
                    short: [
                        "Jan",
                        "Feb",
                        "Mar",
                        "Apr",
                        "May",
                        "Jun",
                        "Jul",
                        "Aug",
                        "Sep",
                        "Oct",
                        "Nov",
                        "Dec",
                    ],
                    long: [
                        "January",
                        "February",
                        "March",
                        "April",
                        "May",
                        "June",
                        "July",
                        "August",
                        "September",
                        "October",
                        "November",
                        "December",
                    ],
                },
            },
        });
    }
    if ($(".birthdate.ar").length > 0) {
        $(".birthdate.ar").bootstrapBirthday({
            widget: {
                wrapper: {
                    tag: "div",
                    class: "row",
                },
                wrapperYear: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                wrapperMonth: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                wrapperDay: {
                    use: true,
                    tag: "span",
                    class: "col",
                },
                selectYear: {
                    name: "birthday[year]",
                    class: "form-control input-sm",
                },
                selectMonth: {
                    name: "birthday[month]",
                    class: "form-control input-sm",
                },
                selectDay: {
                    name: "birthday[day]",
                    class: "form-control input-sm",
                },
            },
            text: {
                dateFormat: "littleEndian",
                year: "سنة",
                month: "شهر",
                day: "يوم",
                months: {
                    short: [
                        "يناير",
                        "فبراير",
                        "مارس",
                        "ابريل",
                        "مايو",
                        "يونيو",
                        "يوليو",
                        "اغسطس",
                        "سبتمبر",
                        "اكتوبر",
                        "نوفمبر",
                        "ديسمبر",
                    ],
                    long: [
                        "يناير",
                        "فبراير",
                        "مارس",
                        "ابريل",
                        "مايو",
                        "يونيو",
                        "يوليو",
                        "اغسطس",
                        "سبتمبر",
                        "اكتوبر",
                        "نوفمبر",
                        "ديسمبر",
                    ],
                },
            },
        });
    }

    // Morries Charts Graphs
    // Morris.Area({
    //   element: 'OrderGraph',
    //   data: [{
    //     period: '2019-01',
    //     element1: 2100,
    //     element2: 224,
    //     element3: 116
    //   }, {
    //     period: '2019-02',
    //     element1: 1700,
    //     element2: 323,
    //     element3: 112
    //   }, {
    //     period: '2019-03',
    //     element1: 2450,
    //     element2: 533,
    //     element3: 214
    //   }, {
    //     period: '2019-04',
    //     element1: 1960,
    //     element2: 415,
    //     element3: 109
    //   }, {
    //     period: '2019-05',
    //     element1: 2400,
    //     element2: 622,
    //     element3: 311
    //   }, {
    //     period: '2019-06',
    //     element1: 2470,
    //     element2: 523,
    //     element3: 315
    //   }, {
    //     period: '2019-07',
    //     element1: 2367,
    //     element2: 634,
    //     element3: 227
    //   }, {
    //     period: '2019-08',
    //     element1: 2700,
    //     element2: 425,
    //     element3: 312
    //   }, {
    //     period: '2019-09',
    //     element1: 2800,
    //     element2: 723,
    //     element3: 316
    //   }, {
    //     period: '2019-10',
    //     element1: 2800,
    //     element2: 428,
    //     element3: 218
    //   }],
    //   xkey: 'period',
    //   ykeys: ['element1', 'element2', 'element3'],
    //   labels: ['New Orders', 'Return Orders', 'New Orders'],
    //   pointSize: 0,
    //   lineWidth:0,
    //   fillOpacity: 1,
    //     pointStrokeColors: ['#5FBCE9 ', '#FAD075', '#94CA86'],
    //   behaveLikeLine: true,
    //   grid: false,
    //   hideHover: 'auto',
    //     lineColors: ['#5FBCE9 ', '#FAD075', '#94CA86'],
    //   resize: true,
    //   redraw: true,
    //   smooth: true,
    //   gridTextColor:'#878787',
    //   gridTextFamily:"Montserrat",
    // });

    // Morris.Donut({
    //   element: 'PaymentGraph',
    //   data: [{
    //     label: "Cash On Delivary",
    //     value: 2000
    //   },{
    //     label: "Online Payment",
    //     value: 112
    //   }, {
    //     label: "Vodafone Cash",
    //     value: 240
    //   }, {
    //     label: "Fawry Payment",
    //     value: 340
    //   }

    // ],
    //     colors: ['#549743', '#b31e8d', '#f58920', '#0078b3','#dd7d53'],
    //   resize: true,
    //   labelColor: '#878787',
    // });
    // $("div svg text").attr("style","font-family: Montserrat").attr("font-weight","400");

    // Morris.Bar({
    //   element: 'SalesGraph',
    //   data: [{
    //     device: 'Shaimaa',
    //     geekbench: 2340
    //   }, {
    //     device: 'Marwa',
    //     geekbench: 2240
    //   }, {
    //     device: 'Noha',
    //     geekbench: 2200
    //   }, {
    //     device: 'Asmaa',
    //     geekbench: 2150
    //   }, {
    //     device: 'Esraa',
    //     geekbench: 2100
    //   }, {
    //     device: 'Aml',
    //     geekbench: 2050
    //   }, {
    //     device: 'Marwa',
    //     geekbench: 2000
    //   }, {
    //     device: 'Noha',
    //     geekbench: 1950
    //   }, {
    //     device: 'Asmaa',
    //     geekbench: 1900
    //   }, {
    //     device: 'Esraa',
    //     geekbench: 1850
    //   }, {
    //     device: 'Aml',
    //     geekbench: 1800
    //   }],
    //   xkey: 'device',
    //   ykeys: ['geekbench'],
    //   labels: ['Geekbench'],
    //   barRatio: 0.4,
    //   xLabelAngle: 35,
    //   pointSize: 1,
    //   barOpacity: 1,
    //     pointStrokeColors: ['#0078b3'],
    //   behaveLikeLine: true,
    //   grid: false,
    //   gridTextColor:'#878787',
    //   hideHover: 'auto',
    //     barColors: ['#0078b3'],
    //   resize: true,
    //   gridTextFamily:"Montserrat"
    // });

    // Morris.Donut({
    //   element: 'ShippingGraph',
    //   data: [{
    //     label: "Transporters",
    //     value: 80
    //   }, {
    //     label: "Aramex",
    //     value: 20
    //   }

    // ],
    //     colors: ['#0078b3', '#b31e8d'],
    //   resize: true,
    //   labelColor: '#878787',
    // });
    // $("div svg text").attr("style","font-family: Montserrat").attr("font-weight","400");



});

function DeleteRow($url) {
    swal({
        title: "Your Item will be deleted!",
        text: "Are you sure to proceed?",
        type: "warning",
        buttons: true,
        showCancelButton: true,
        cancelButtonColor: "#DD6B55",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Remove My Item!",
        cancelButtonText: "I am not sure!",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((isConfirm) => {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: $url,
                success: function (data) {
                    setTimeout(function () {
                        location.reload();

                    }, 500);
                }
            });
            swal("Item Removed!", "Your Item is removed!", "success");

        }
        else {
            swal("Hurray", "Item is not removed!", "error");
        }
    });

}
//$("img").on("error", function () {
//    var resize = $(this).attr("src").split("?")[1];
//    $(this).attr(
//        "src",
//        "/images/placeholder.png" +
//        (resize != null ? "?" + resize : "?w=500&h=500&mode=crop&scale=both")
//    );
//    $(this).addClass("img-fluid");
//});



function Copy(text) {

    navigator.clipboard.writeText(text)
        .then(() => {
            console.log('Text copied to clipboard');

            alertify.success("Text copied to clipboard")
        })
        .catch(err => {
            // This can happen if the user denies clipboard permissions:
            console.error('Could not copy text: ', err);
            alertify.error("Could not copy text")

        });

}

function newPrintBody(div) {

    var OldIframe = $("[name=frame1]");
    if (OldIframe != null && OldIframe.length > 0) {
        OldIframe.remove();
    }
    var contents = div;
    // var contents = div;
    var frame1 = $('<iframe />');
    frame1[0].name = "frame1";
    frame1.css({ "position": "absolute", "top": "-1000000000px" });
    $("body").append(frame1);
    var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
    frameDoc.document.open();
    frameDoc.document.write(contents);
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
    }, 500);

}
