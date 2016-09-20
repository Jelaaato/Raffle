
function drawARaffle(t) {
    $(".winning p").removeClass("pitch"),
    $(t.button).unbind("click").css("cursor", "default"),
    Math.ceil(1e3 * Math.random()) + 100,
    slotMachine(t, 1, list)
}

//displays slot machine
function creatSlotMachine(t) {
    var n, s, i, o, a, c; !function () {
        n = $("<div>", { "class": "slotMachine" }).appendTo(t), s = $("<div>", { "class": "winning" }).appendTo(n), i = $("<div>", { "class": "list" }).appendTo(s), o = $("<div>", { "class": "frame" }).appendTo(n), c = $("<img>", { "class": "joysticks", src: "/Images/joysticks.png" }).appendTo(n), a = $("<img>", { "class": "button", src: "/Images/button1.png" }).appendTo(n)
    }(); var e = {}; return e.slotMachine = n, e.winning = s, e.list = i, e.frame = o, e.button = a, e.joysticks = c, e
}

//enables the joystick to be pressed
function animateButton(t) {
    t.animate({ top: "255px" }, 450, "swing", function () { t.animate({ top: "178px" }, 800, "linear") })
}

// loads list of participants
function putList(t, n) {
    for (var s = 0; 302 > s; s++) { var i = n.shift(); n.push(i), $("<p>", { "class": "candidate", text: i }).appendTo(t) }
}

//enables shuffling
function slotMachine(t, n) {
    var s = 70 * n; t.addClass("candidatenum").css("transform", "translateY(" + s + "px)")
}

//does the raffle, captures winner

function drawARaffle(t, n) {
    t.list.children("p").removeClass("pitch"),
    $(t.button).unbind("click").css("cursor", "default");
    var s = Math.ceil(200 * Math.random()) + 90;
    slotMachine(t.list, s),
    setTimeout(function () {
        $(t.list.children("p")[297 - s]).addClass("pitch");
        for (var i = $(t.list.children("p")[297 - s]).html(), o = $(".pitch").html(), a = n.length, c = 0; a > c; c++) o == n[c];
        setTimeout(function () {
            $(".screen span").html(i),
            $(".screen").css("display", "flex"),
            $(".prize").show("nomal"), // displays winner
            $(t.button).on("click", function () {  // enables joystick to be clicked again after raffle
                t.list.remove(),
                t.list = $("<div>", { "class": "list" }).appendTo(t.winning),
                t.joysticks.addClass("joysticksAnimate"),
                setTimeout(function () {
                    t.joysticks.removeClass("joysticksAnimate")
                }, 1850)
                , putList(t.list, n)
                , animateButton($(t.button))
                , drawARaffle(t, n)
            }).css("cursor", "pointer")
        }, 400)
    }, 15500)

}
var list;