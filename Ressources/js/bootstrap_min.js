From: =?Windows-1252?Q?Enregistr=E9_par_Internet_Explorer=A011?=
Subject: 
Date: Wed, 31 Jan 2018 23:33:13 +0100
MIME-Version: 1.0
Content-Type: text/html;
	charset="Windows-1252"
Content-Transfer-Encoding: quoted-printable
Content-Location: https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js
X-MimeOLE: Produced By Microsoft MimeOLE V6.1.7601.24000

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML><HEAD><META content=3D"IE=3D5.0000" =
http-equiv=3D"X-UA-Compatible">

<META http-equiv=3D"Content-Type" content=3D"text/html; =
charset=3Dwindows-1252">
<META name=3D"GENERATOR" content=3D"MSHTML 11.00.9600.18894"></HEAD>
<BODY>/*! * Bootstrap v3.3.7 (http://getbootstrap.com) * Copyright =
2011-2016=20
Twitter, Inc. * Licensed under the MIT license */ =
if("undefined"=3D=3Dtypeof=20
jQuery)throw new Error("Bootstrap's JavaScript requires=20
jQuery");+function(a){"use strict";var b=3Da.fn.jquery.split("=20
")[0].split(".");if(b[0]&lt;2&amp;&amp;b[1]&lt;9||1=3D=3Db[0]&amp;&amp;9=3D=
=3Db[1]&amp;&amp;b[2]&lt;1||b[0]&gt;3)throw=20
new Error("Bootstrap's JavaScript requires jQuery version 1.9.1 or =
higher, but=20
lower than version 4")}(jQuery),+function(a){"use strict";function =
b(){var=20
a=3Ddocument.createElement("bootstrap"),b=3D{WebkitTransition:"webkitTran=
sitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd=20
otransitionend",transition:"transitionend"};for(var c in b)if(void=20
0!=3D=3Da.style[c])return{end:b[c]};return!1}a.fn.emulateTransitionEnd=3D=
function(b){var=20
c=3D!1,d=3Dthis;a(this).one("bsTransitionEnd",function(){c=3D!0});var=20
e=3Dfunction(){c||a(d).trigger(a.support.transition.end)};return=20
setTimeout(e,b),this},a(function(){a.support.transition=3Db(),a.support.t=
ransition&amp;&amp;(a.event.special.bsTransitionEnd=3D{bindType:a.support=
.transition.end,delegateType:a.support.transition.end,handle:function(b){=
if(a(b.target).is(this))return=20
b.handleObj.handler.apply(this,arguments)}})})}(jQuery),+function(a){"use=
=20
strict";function b(b){return this.each(function(){var=20
c=3Da(this),e=3Dc.data("bs.alert");e||c.data("bs.alert",e=3Dnew=20
d(this)),"string"=3D=3Dtypeof b&amp;&amp;e[b].call(c)})}var=20
c=3D'[data-dismiss=3D"alert"]',d=3Dfunction(b){a(b).on("click",c,this.clo=
se)};d.VERSION=3D"3.3.7",d.TRANSITION_DURATION=3D150,d.prototype.close=3D=
function(b){function=20
c(){g.detach().trigger("closed.bs.alert").remove()}var=20
e=3Da(this),f=3De.attr("data-target");f||(f=3De.attr("href"),f=3Df&amp;&a=
mp;f.replace(/.*(?=3D#[^\s]*$)/,""));var=20
g=3Da("#"=3D=3D=3Df?[]:f);b&amp;&amp;b.preventDefault(),g.length||(g=3De.=
closest(".alert")),g.trigger(b=3Da.Event("close.bs.alert")),b.isDefaultPr=
evented()||(g.removeClass("in"),a.support.transition&amp;&amp;g.hasClass(=
"fade")?g.one("bsTransitionEnd",c).emulateTransitionEnd(d.TRANSITION_DURA=
TION):c())};var=20
e=3Da.fn.alert;a.fn.alert=3Db,a.fn.alert.Constructor=3Dd,a.fn.alert.noCon=
flict=3Dfunction(){return=20
a.fn.alert=3De,this},a(document).on("click.bs.alert.data-api",c,d.prototy=
pe.close)}(jQuery),+function(a){"use=20
strict";function b(b){return this.each(function(){var=20
d=3Da(this),e=3Dd.data("bs.button"),f=3D"object"=3D=3Dtypeof=20
b&amp;&amp;b;e||d.data("bs.button",e=3Dnew=20
c(this,f)),"toggle"=3D=3Db?e.toggle():b&amp;&amp;e.setState(b)})}var=20
c=3Dfunction(b,d){this.$element=3Da(b),this.options=3Da.extend({},c.DEFAU=
LTS,d),this.isLoading=3D!1};c.VERSION=3D"3.3.7",c.DEFAULTS=3D{loadingText=
:"loading..."},c.prototype.setState=3Dfunction(b){var=20
c=3D"disabled",d=3Dthis.$element,e=3Dd.is("input")?"val":"html",f=3Dd.dat=
a();b+=3D"Text",null=3D=3Df.resetText&amp;&amp;d.data("resetText",d[e]())=
,setTimeout(a.proxy(function(){d[e](null=3D=3Df[b]?this.options[b]:f[b]),=
"loadingText"=3D=3Db?(this.isLoading=3D!0,d.addClass(c).attr(c,c).prop(c,=
!0)):this.isLoading&amp;&amp;(this.isLoading=3D!1,d.removeClass(c).remove=
Attr(c).prop(c,!1))},this),0)},c.prototype.toggle=3Dfunction(){var=20
a=3D!0,b=3Dthis.$element.closest('[data-toggle=3D"buttons"]');if(b.length=
){var=20
c=3Dthis.$element.find("input");"radio"=3D=3Dc.prop("type")?(c.prop("chec=
ked")&amp;&amp;(a=3D!1),b.find(".active").removeClass("active"),this.$ele=
ment.addClass("active")):"checkbox"=3D=3Dc.prop("type")&amp;&amp;(c.prop(=
"checked")!=3D=3Dthis.$element.hasClass("active")&amp;&amp;(a=3D!1),this.=
$element.toggleClass("active")),c.prop("checked",this.$element.hasClass("=
active")),a&amp;&amp;c.trigger("change")}else=20
this.$element.attr("aria-pressed",!this.$element.hasClass("active")),this=
.$element.toggleClass("active")};var=20
d=3Da.fn.button;a.fn.button=3Db,a.fn.button.Constructor=3Dc,a.fn.button.n=
oConflict=3Dfunction(){return=20
a.fn.button=3Dd,this},a(document).on("click.bs.button.data-api",'[data-to=
ggle^=3D"button"]',function(c){var=20
d=3Da(c.target).closest(".btn");b.call(d,"toggle"),a(c.target).is('input[=
type=3D"radio"],=20
input[type=3D"checkbox"]')||(c.preventDefault(),d.is("input,button")?d.tr=
igger("focus"):d.find("input:visible,button:visible").first().trigger("fo=
cus"))}).on("focus.bs.button.data-api=20
blur.bs.button.data-api",'[data-toggle^=3D"button"]',function(b){a(b.targ=
et).closest(".btn").toggleClass("focus",/^focus(in)?$/.test(b.type))})}(j=
Query),+function(a){"use=20
strict";function b(b){return this.each(function(){var=20
d=3Da(this),e=3Dd.data("bs.carousel"),f=3Da.extend({},c.DEFAULTS,d.data()=
,"object"=3D=3Dtypeof=20
b&amp;&amp;b),g=3D"string"=3D=3Dtypeof =
b?b:f.slide;e||d.data("bs.carousel",e=3Dnew=20
c(this,f)),"number"=3D=3Dtypeof=20
b?e.to(b):g?e[g]():f.interval&amp;&amp;e.pause().cycle()})}var=20
c=3Dfunction(b,c){this.$element=3Da(b),this.$indicators=3Dthis.$element.f=
ind(".carousel-indicators"),this.options=3Dc,this.paused=3Dnull,this.slid=
ing=3Dnull,this.interval=3Dnull,this.$active=3Dnull,this.$items=3Dnull,th=
is.options.keyboard&amp;&amp;this.$element.on("keydown.bs.carousel",a.pro=
xy(this.keydown,this)),"hover"=3D=3Dthis.options.pause&amp;&amp;!("ontouc=
hstart"in=20
document.documentElement)&amp;&amp;this.$element.on("mouseenter.bs.carous=
el",a.proxy(this.pause,this)).on("mouseleave.bs.carousel",a.proxy(this.cy=
cle,this))};c.VERSION=3D"3.3.7",c.TRANSITION_DURATION=3D600,c.DEFAULTS=3D=
{interval:5e3,pause:"hover",wrap:!0,keyboard:!0},c.prototype.keydown=3Dfu=
nction(a){if(!/input|textarea/i.test(a.target.tagName)){switch(a.which){c=
ase=20
37:this.prev();break;case=20
39:this.next();break;default:return}a.preventDefault()}},c.prototype.cycl=
e=3Dfunction(b){return=20
b||(this.paused=3D!1),this.interval&amp;&amp;clearInterval(this.interval)=
,this.options.interval&amp;&amp;!this.paused&amp;&amp;(this.interval=3Dse=
tInterval(a.proxy(this.next,this),this.options.interval)),this},c.prototy=
pe.getItemIndex=3Dfunction(a){return=20
this.$items=3Da.parent().children(".item"),this.$items.index(a||this.$act=
ive)},c.prototype.getItemForDirection=3Dfunction(a,b){var=20
c=3Dthis.getItemIndex(b),d=3D"prev"=3D=3Da&amp;&amp;0=3D=3D=3Dc||"next"=3D=
=3Da&amp;&amp;c=3D=3Dthis.$items.length-1;if(d&amp;&amp;!this.options.wra=
p)return=20
b;var e=3D"prev"=3D=3Da?-1:1,f=3D(c+e)%this.$items.length;return=20
this.$items.eq(f)},c.prototype.to=3Dfunction(a){var=20
b=3Dthis,c=3Dthis.getItemIndex(this.$active=3Dthis.$element.find(".item.a=
ctive"));if(!(a&gt;this.$items.length-1||a&lt;0))return=20
this.sliding?this.$element.one("slid.bs.carousel",function(){b.to(a)}):c=3D=
=3Da?this.pause().cycle():this.slide(a&gt;c?"next":"prev",this.$items.eq(=
a))},c.prototype.pause=3Dfunction(b){return=20
b||(this.paused=3D!0),this.$element.find(".next,=20
.prev").length&amp;&amp;a.support.transition&amp;&amp;(this.$element.trig=
ger(a.support.transition.end),this.cycle(!0)),this.interval=3DclearInterv=
al(this.interval),this},c.prototype.next=3Dfunction(){if(!this.sliding)re=
turn=20
this.slide("next")},c.prototype.prev=3Dfunction(){if(!this.sliding)return=
=20
this.slide("prev")},c.prototype.slide=3Dfunction(b,d){var=20
e=3Dthis.$element.find(".item.active"),f=3Dd||this.getItemForDirection(b,=
e),g=3Dthis.interval,h=3D"next"=3D=3Db?"left":"right",i=3Dthis;if(f.hasCl=
ass("active"))return=20
this.sliding=3D!1;var=20
j=3Df[0],k=3Da.Event("slide.bs.carousel",{relatedTarget:j,direction:h});i=
f(this.$element.trigger(k),!k.isDefaultPrevented()){if(this.sliding=3D!0,=
g&amp;&amp;this.pause(),this.$indicators.length){this.$indicators.find(".=
active").removeClass("active");var=20
l=3Da(this.$indicators.children()[this.getItemIndex(f)]);l&amp;&amp;l.add=
Class("active")}var=20
m=3Da.Event("slid.bs.carousel",{relatedTarget:j,direction:h});return=20
a.support.transition&amp;&amp;this.$element.hasClass("slide")?(f.addClass=
(b),f[0].offsetWidth,e.addClass(h),f.addClass(h),e.one("bsTransitionEnd",=
function(){f.removeClass([b,h].join("=20
")).addClass("active"),e.removeClass(["active",h].join("=20
")),i.sliding=3D!1,setTimeout(function(){i.$element.trigger(m)},0)}).emul=
ateTransitionEnd(c.TRANSITION_DURATION)):(e.removeClass("active"),f.addCl=
ass("active"),this.sliding=3D!1,this.$element.trigger(m)),g&amp;&amp;this=
.cycle(),this}};var=20
d=3Da.fn.carousel;a.fn.carousel=3Db,a.fn.carousel.Constructor=3Dc,a.fn.ca=
rousel.noConflict=3Dfunction(){return=20
a.fn.carousel=3Dd,this};var e=3Dfunction(c){var=20
d,e=3Da(this),f=3Da(e.attr("data-target")||(d=3De.attr("href"))&amp;&amp;=
d.replace(/.*(?=3D#[^\s]+$)/,""));if(f.hasClass("carousel")){var=20
g=3Da.extend({},f.data(),e.data()),h=3De.attr("data-slide-to");h&amp;&amp=
;(g.interval=3D!1),b.call(f,g),h&amp;&amp;f.data("bs.carousel").to(h),c.p=
reventDefault()}};a(document).on("click.bs.carousel.data-api","[data-slid=
e]",e).on("click.bs.carousel.data-api","[data-slide-to]",e),a(window).on(=
"load",function(){a('[data-ride=3D"carousel"]').each(function(){var=20
c=3Da(this);b.call(c,c.data())})})}(jQuery),+function(a){"use =
strict";function=20
b(b){var=20
c,d=3Db.attr("data-target")||(c=3Db.attr("href"))&amp;&amp;c.replace(/.*(=
?=3D#[^\s]+$)/,"");return=20
a(d)}function c(b){return this.each(function(){var=20
c=3Da(this),e=3Dc.data("bs.collapse"),f=3Da.extend({},d.DEFAULTS,c.data()=
,"object"=3D=3Dtypeof=20
b&amp;&amp;b);!e&amp;&amp;f.toggle&amp;&amp;/show|hide/.test(b)&amp;&amp;=
(f.toggle=3D!1),e||c.data("bs.collapse",e=3Dnew=20
d(this,f)),"string"=3D=3Dtypeof b&amp;&amp;e[b]()})}var=20
d=3Dfunction(b,c){this.$element=3Da(b),this.options=3Da.extend({},d.DEFAU=
LTS,c),this.$trigger=3Da('[data-toggle=3D"collapse"][href=3D"#'+b.id+'"],=
[data-toggle=3D"collapse"][data-target=3D"#'+b.id+'"]'),this.transitionin=
g=3Dnull,this.options.parent?this.$parent=3Dthis.getParent():this.addAria=
AndCollapsedClass(this.$element,this.$trigger),this.options.toggle&amp;&a=
mp;this.toggle()};d.VERSION=3D"3.3.7",d.TRANSITION_DURATION=3D350,d.DEFAU=
LTS=3D{toggle:!0},d.prototype.dimension=3Dfunction(){var=20
a=3Dthis.$element.hasClass("width");return=20
a?"width":"height"},d.prototype.show=3Dfunction(){if(!this.transitioning&=
amp;&amp;!this.$element.hasClass("in")){var=20
b,e=3Dthis.$parent&amp;&amp;this.$parent.children(".panel").children(".in=
,=20
.collapsing");if(!(e&amp;&amp;e.length&amp;&amp;(b=3De.data("bs.collapse"=
),b&amp;&amp;b.transitioning))){var=20
f=3Da.Event("show.bs.collapse");if(this.$element.trigger(f),!f.isDefaultP=
revented()){e&amp;&amp;e.length&amp;&amp;(c.call(e,"hide"),b||e.data("bs.=
collapse",null));var=20
g=3Dthis.dimension();this.$element.removeClass("collapse").addClass("coll=
apsing")[g](0).attr("aria-expanded",!0),this.$trigger.removeClass("collap=
sed").attr("aria-expanded",!0),this.transitioning=3D1;var=20
h=3Dfunction(){this.$element.removeClass("collapsing").addClass("collapse=
=20
in")[g](""),this.transitioning=3D0,this.$element.trigger("shown.bs.collap=
se")};if(!a.support.transition)return=20
h.call(this);var=20
i=3Da.camelCase(["scroll",g].join("-"));this.$element.one("bsTransitionEn=
d",a.proxy(h,this)).emulateTransitionEnd(d.TRANSITION_DURATION)[g](this.$=
element[0][i])}}}},d.prototype.hide=3Dfunction(){if(!this.transitioning&a=
mp;&amp;this.$element.hasClass("in")){var=20
b=3Da.Event("hide.bs.collapse");if(this.$element.trigger(b),!b.isDefaultP=
revented()){var=20
c=3Dthis.dimension();this.$element[c](this.$element[c]())[0].offsetHeight=
,this.$element.addClass("collapsing").removeClass("collapse=20
in").attr("aria-expanded",!1),this.$trigger.addClass("collapsed").attr("a=
ria-expanded",!1),this.transitioning=3D1;var=20
e=3Dfunction(){this.transitioning=3D0,this.$element.removeClass("collapsi=
ng").addClass("collapse").trigger("hidden.bs.collapse")};return=20
a.support.transition?void=20
this.$element[c](0).one("bsTransitionEnd",a.proxy(e,this)).emulateTransit=
ionEnd(d.TRANSITION_DURATION):e.call(this)}}},d.prototype.toggle=3Dfuncti=
on(){this[this.$element.hasClass("in")?"hide":"show"]()},d.prototype.getP=
arent=3Dfunction(){return=20
a(this.options.parent).find('[data-toggle=3D"collapse"][data-parent=3D"'+=
this.options.parent+'"]').each(a.proxy(function(c,d){var=20
e=3Da(d);this.addAriaAndCollapsedClass(b(e),e)},this)).end()},d.prototype=
.addAriaAndCollapsedClass=3Dfunction(a,b){var=20
c=3Da.hasClass("in");a.attr("aria-expanded",c),b.toggleClass("collapsed",=
!c).attr("aria-expanded",c)};var=20
e=3Da.fn.collapse;a.fn.collapse=3Dc,a.fn.collapse.Constructor=3Dd,a.fn.co=
llapse.noConflict=3Dfunction(){return=20
a.fn.collapse=3De,this},a(document).on("click.bs.collapse.data-api",'[dat=
a-toggle=3D"collapse"]',function(d){var=20
e=3Da(this);e.attr("data-target")||d.preventDefault();var=20
f=3Db(e),g=3Df.data("bs.collapse"),h=3Dg?"toggle":e.data();c.call(f,h)})}=
(jQuery),+function(a){"use=20
strict";function b(b){var=20
c=3Db.attr("data-target");c||(c=3Db.attr("href"),c=3Dc&amp;&amp;/#[A-Za-z=
]/.test(c)&amp;&amp;c.replace(/.*(?=3D#[^\s]*$)/,""));var=20
d=3Dc&amp;&amp;a(c);return d&amp;&amp;d.length?d:b.parent()}function=20
c(c){c&amp;&amp;3=3D=3D=3Dc.which||(a(e).remove(),a(f).each(function(){va=
r=20
d=3Da(this),e=3Db(d),f=3D{relatedTarget:this};e.hasClass("open")&amp;&amp=
;(c&amp;&amp;"click"=3D=3Dc.type&amp;&amp;/input|textarea/i.test(c.target=
.tagName)&amp;&amp;a.contains(e[0],c.target)||(e.trigger(c=3Da.Event("hid=
e.bs.dropdown",f)),c.isDefaultPrevented()||(d.attr("aria-expanded","false=
"),e.removeClass("open").trigger(a.Event("hidden.bs.dropdown",f)))))}))}f=
unction=20
d(b){return this.each(function(){var=20
c=3Da(this),d=3Dc.data("bs.dropdown");d||c.data("bs.dropdown",d=3Dnew=20
g(this)),"string"=3D=3Dtypeof b&amp;&amp;d[b].call(c)})}var=20
e=3D".dropdown-backdrop",f=3D'[data-toggle=3D"dropdown"]',g=3Dfunction(b)=
{a(b).on("click.bs.dropdown",this.toggle)};g.VERSION=3D"3.3.7",g.prototyp=
e.toggle=3Dfunction(d){var=20
e=3Da(this);if(!e.is(".disabled, :disabled")){var=20
f=3Db(e),g=3Df.hasClass("open");if(c(),!g){"ontouchstart"in=20
document.documentElement&amp;&amp;!f.closest(".navbar-nav").length&amp;&a=
mp;a(document.createElement("div")).addClass("dropdown-backdrop").insertA=
fter(a(this)).on("click",c);var=20
h=3D{relatedTarget:this};if(f.trigger(d=3Da.Event("show.bs.dropdown",h)),=
d.isDefaultPrevented())return;e.trigger("focus").attr("aria-expanded","tr=
ue"),f.toggleClass("open").trigger(a.Event("shown.bs.dropdown",h))}return=
!1}},g.prototype.keydown=3Dfunction(c){if(/(38|40|27|32)/.test(c.which)&a=
mp;&amp;!/input|textarea/i.test(c.target.tagName)){var=20
d=3Da(this);if(c.preventDefault(),c.stopPropagation(),!d.is(".disabled,=20
:disabled")){var=20
e=3Db(d),g=3De.hasClass("open");if(!g&amp;&amp;27!=3Dc.which||g&amp;&amp;=
27=3D=3Dc.which)return=20
27=3D=3Dc.which&amp;&amp;e.find(f).trigger("focus"),d.trigger("click");va=
r h=3D"=20
li:not(.disabled):visible =
a",i=3De.find(".dropdown-menu"+h);if(i.length){var=20
j=3Di.index(c.target);38=3D=3Dc.which&amp;&amp;j&gt;0&amp;&amp;j--,40=3D=3D=
c.which&amp;&amp;j<i.length-1&&j++,~j||(j=3D0),i.eq(j).trigger("focus")}}=
}};var=20
a=3D'this;this.$element.hide(),this.backdrop(function(){a.$body.removeCla=
ss("modal-open"),a.resetAdjustments(),a.resetScrollbar(),a.$element.trigg=
er("hidden.bs.modal")})},c.prototype.removeBackdrop=3Dfunction(){this.$ba=
ckdrop&amp;&amp;this.$backdrop.remove(),this.$backdrop=3Dnull},c.prototyp=
e.backdrop=3Dfunction(b){var'=20
b&&b()},c.prototype.handleupdate=3D"function(){this.adjustDialog()},c.pro=
totype.adjustDialog=3Dfunction(){var"=20
g=3D'function(){d.removeBackdrop(),b&amp;&amp;b()};a.support.transition&a=
mp;&amp;this.$element.hasClass("fade")?this.$backdrop.one("bsTransitionEn=
d",g).emulateTransitionEnd(c.BACKDROP_TRANSITION_DURATION):g()}else'=20
if(!this.isshown&&this.$backdrop){this.$backdrop.removeclass("in");var=3D=
""=20
this.ignorebackdropclick?void(this.ignorebackdropclick=3D'!1):void(a.targ=
et=3D=3D=3Da.currentTarget&amp;&amp;("static"=3D=3Dthis.options.backdrop?=
this.$element[0].focus():this.hide()))},this)),f&amp;&amp;this.$backdrop[=
0].offsetWidth,this.$backdrop.addClass("in"),!b)return;f?this.$backdrop.o=
ne("bsTransitionEnd",b).emulateTransitionEnd(c.BACKDROP_TRANSITION_DURATI=
ON):b()}else'=20
"+e).appendto(this.$body),this.$element.on("click.dismiss.bs.modal",a.pro=
xy(function(a){return=3D""=20
f=3D'a.Event("shown.bs.modal",{relatedTarget:b});e?d.$dialog.one("bsTrans=
itionEnd",function(){d.$element.trigger("focus").trigger(f)}).emulateTran=
sitionEnd(c.TRANSITION_DURATION):d.$element.trigger("focus").trigger(f)})=
)},c.prototype.hide=3Dfunction(b){b&amp;&amp;b.preventDefault(),b=3Da.Eve=
nt("hide.bs.modal"),this.$element.trigger(b),this.isShown&amp;&amp;!b.isD=
efaultPrevented()&amp;&amp;(this.isShown=3D!1,this.escape(),this.resize()=
,a(document).off("focusin.bs.modal"),this.$element.removeClass("in").off(=
"click.dismiss.bs.modal").off("mouseup.dismiss.bs.modal"),this.$dialog.of=
f("mousedown.dismiss.bs.modal"),a.support.transition&amp;&amp;this.$eleme=
nt.hasClass("fade")?this.$element.one("bsTransitionEnd",a.proxy(this.hide=
Modal,this)).emulateTransitionEnd(c.TRANSITION_DURATION):this.hideModal()=
)},c.prototype.enforceFocus=3Dfunction(){a(document).off("focusin.bs.moda=
l").on("focusin.bs.modal",a.proxy(function(a){document=3D=3D=3Da.target||=
this.$element[0]=3D=3D=3Da.target||this.$element.has(a.target).length||th=
is.$element.trigger("focus")},this))},c.prototype.escape=3Dfunction(){thi=
s.isShown&amp;&amp;this.options.keyboard?this.$element.on("keydown.dismis=
s.bs.modal",a.proxy(function(a){27=3D=3Da.which&amp;&amp;this.hide()},thi=
s)):this.isShown||this.$element.off("keydown.dismiss.bs.modal")},c.protot=
ype.resize=3Dfunction(){this.isShown?a(window).on("resize.bs.modal",a.pro=
xy(this.handleUpdate,this)):a(window).off("resize.bs.modal")},c.prototype=
.hideModal=3Dfunction(){var'=20
d=3D"this,e=3Da.Event(&quot;show.bs.modal&quot;,{relatedTarget:b});this.$=
element.trigger(e),this.isShown||e.isDefaultPrevented()||(this.isShown=3D=
!0,this.checkScrollbar(),this.setScrollbar(),this.$body.addClass(&quot;mo=
dal-open&quot;),this.escape(),this.resize(),this.$element.on(&quot;click.=
dismiss.bs.modal&quot;,'[data-dismiss=3D&quot;modal&quot;]',a.proxy(this.=
hide,this)),this.$dialog.on(&quot;mousedown.dismiss.bs.modal&quot;,functi=
on(){d.$element.one(&quot;mouseup.dismiss.bs.modal&quot;,function(b){a(b.=
target).is(d.$element)&amp;&amp;(d.ignoreBackdropClick=3D!0)})}),this.bac=
kdrop(function(){var"=20
e=3D'a(this),f=3De.data("bs.modal"),g=3Da.extend({},c.DEFAULTS,e.data(),"=
object"=3D=3Dtypeof'=20
this.isshown?this.hide():this.show(a)},c.prototype.show=3D"function(b){va=
r" =
c=3D'function(b,c){this.options=3Dc,this.$body=3Da(document.body),this.$e=
lement=3Da(b),this.$dialog=3Dthis.$element.find(".modal-dialog"),this.$ba=
ckdrop=3Dnull,this.isShown=3Dnull,this.originalBodyPad=3Dnull,this.scroll=
barWidth=3D0,this.ignoreBackdropClick=3D!1,this.options.remote&amp;&amp;t=
his.$element.find(".modal-content").load(this.options.remote,a.proxy(func=
tion(){this.$element.trigger("loaded.bs.modal")},this))};c.VERSION=3D"3.3=
.7",c.TRANSITION_DURATION=3D300,c.BACKDROP_TRANSITION_DURATION=3D150,c.DE=
FAULTS=3D{backdrop:!0,keyboard:!0,show:!0},c.prototype.toggle=3Dfunction(=
a){return'=20
b?f[b](d):g.show&&f.show(d)})}var=3D"" c(this,g)),"string"=3D"=3Dtypeof" =

b&&b);f||e.data("bs.modal",f=3D"new" this.each(function(){var=3D"" =
b(b,d){return=3D""=20
strict";function=3D""=20
form",function(a){a.stoppropagation()}).on("click.bs.dropdown.data-api",f=
,g.prototype.toggle).on("keydown.bs.dropdown.data-api",f,g.prototype.keyd=
own).on("keydown.bs.dropdown.data-api",".dropdown-menu",g.prototype.keydo=
wn)}(jquery),+function(a){"use=3D""=20
a.fn.dropdown=3D'h,this},a(document).on("click.bs.dropdown.data-api",c).o=
n("click.bs.dropdown.data-api",".dropdown'=20
h=3D"a.fn.dropdown;a.fn.dropdown=3Dd,a.fn.dropdown.Constructor=3Dg,a.fn.d=
ropdown.noConflict=3Dfunction(){return">document.documentElement.clientHe=
ight;this.$element.css({paddingLeft:!this.bodyIsOverflowing&amp;&amp;a?th=
is.scrollbarWidth:"",paddingRight:this.bodyIsOverflowing&amp;&amp;!a?this=
.scrollbarWidth:""})},c.prototype.resetAdjustments=3Dfunction(){this.$ele=
ment.css({paddingLeft:"",paddingRight:""})},c.prototype.checkScrollbar=3D=
function(){var=20
a=3Dwindow.innerWidth;if(!a){var=20
b=3Ddocument.documentElement.getBoundingClientRect();a=3Db.right-Math.abs=
(b.left)}this.bodyIsOverflowing=3Ddocument.body.clientWidth<a,this.scroll=
barwidth=3Dthis.measurescrollbar()},c.prototype.setscrollbar=3Dfunction()=
{var=20
class=3D"tooltip" role=3D"tooltip" =
a=3D'parseInt(this.$body.css("padding-right")||0,10);this.originalBodyPad=
=3Ddocument.body.style.paddingRight||"",this.bodyIsOverflowing&amp;&amp;t=
his.$body.css("padding-right",a+this.scrollbarWidth)},c.prototype.resetSc=
rollbar=3Dfunction(){this.$body.css("padding-right",this.originalBodyPad)=
},c.prototype.measureScrollbar=3Dfunction(){var'=20
d=3D"a.fn.modal;a.fn.modal=3Db,a.fn.modal.Constructor=3Dc,a.fn.modal.noCo=
nflict=3Dfunction(){return"=20
c=3D"function(a,b){this.type=3Dnull,this.options=3Dnull,this.enabled=3Dnu=
ll,this.timeout=3Dnull,this.hoverState=3Dnull,this.$element=3Dnull,this.i=
nState=3Dnull,this.init(&quot;tooltip&quot;,a,b)};c.VERSION=3D&quot;3.3.7=
&quot;,c.TRANSITION_DURATION=3D150,c.DEFAULTS=3D{animation:!0,placement:&=
quot;top&quot;,selector:!1,template:'<div"=20
this.each(function(){var=3D"" strict";function=3D"" =
b&&e[b]())})}var=3D""=20
c(this,f)),"string"=3D"=3Dtypeof" =
.test(b)||(e||d.data("bs.tooltip",e=3D"new"=20
destroy|hide=3D"" b&&b;!e&&=3D"" b(b){return=3D"" =
a.fn.modal=3D"d,this},a(document).on(&quot;click.bs.modal.data-api&quot;,=
'[data-toggle=3D&quot;modal&quot;]',function(c){var"=20
this.$body[0].removechild(a),b};var=3D"" =
b=3D"a.offsetWidth-a.clientWidth;return">
<DIV class=3D"tooltip-arrow"></DIV>
<DIV class=3D"tooltip-inner"></DIV>',trigger:"hover=20
focus",title:"",delay:0,html:!1,container:!1,viewport:{selector:"body",pa=
dding:0}},c.prototype.init=3Dfunction(b,c,d){if(this.enabled=3D!0,this.ty=
pe=3Db,this.$element=3Da(c),this.options=3Dthis.getOptions(d),this.$viewp=
ort=3Dthis.options.viewport&amp;&amp;a(a.isFunction(this.options.viewport=
)?this.options.viewport.call(this,this.$element):this.options.viewport.se=
lector||this.options.viewport),this.inState=3D{click:!1,hover:!1,focus:!1=
},this.$element[0]instanceof=20
document.constructor&amp;&amp;!this.options.selector)throw new =
Error("`selector`=20
option must be specified when initializing "+this.type+" on the =
window.document=20
object!");for(var e=3Dthis.options.trigger.split(" =
"),f=3De.length;f--;){var=20
g=3De[f];if("click"=3D=3Dg)this.$element.on("click."+this.type,this.optio=
ns.selector,a.proxy(this.toggle,this));else=20
if("manual"!=3Dg){var=20
h=3D"hover"=3D=3Dg?"mouseenter":"focusin",i=3D"hover"=3D=3Dg?"mouseleave"=
:"focusout";this.$element.on(h+"."+this.type,this.options.selector,a.prox=
y(this.enter,this)),this.$element.on(i+"."+this.type,this.options.selecto=
r,a.proxy(this.leave,this))}}this.options.selector?this._options=3Da.exte=
nd({},this.options,{trigger:"manual",selector:""}):this.fixTitle()},c.pro=
totype.getDefaults=3Dfunction(){return=20
c.DEFAULTS},c.prototype.getOptions=3Dfunction(b){return=20
b=3Da.extend({},this.getDefaults(),this.$element.data(),b),b.delay&amp;&a=
mp;"number"=3D=3Dtypeof=20
b.delay&amp;&amp;(b.delay=3D{show:b.delay,hide:b.delay}),b},c.prototype.g=
etDelegateOptions=3Dfunction(){var=20
b=3D{},c=3Dthis.getDefaults();return=20
this._options&amp;&amp;a.each(this._options,function(a,d){c[a]!=3Dd&amp;&=
amp;(b[a]=3Dd)}),b},c.prototype.enter=3Dfunction(b){var=20
c=3Db instanceof=20
this.constructor?b:a(b.currentTarget).data("bs."+this.type);return =
c||(c=3Dnew=20
this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTa=
rget).data("bs."+this.type,c)),b=20
instanceof=20
a.Event&amp;&amp;(c.inState["focusin"=3D=3Db.type?"focus":"hover"]=3D!0),=
c.tip().hasClass("in")||"in"=3D=3Dc.hoverState?void(c.hoverState=3D"in"):=
(clearTimeout(c.timeout),c.hoverState=3D"in",c.options.delay&amp;&amp;c.o=
ptions.delay.show?void(c.timeout=3DsetTimeout(function(){"in"=3D=3Dc.hove=
rState&amp;&amp;c.show()},c.options.delay.show)):c.show())},c.prototype.i=
sInStateTrue=3Dfunction(){for(var=20
a in=20
this.inState)if(this.inState[a])return!0;return!1},c.prototype.leave=3Dfu=
nction(b){var=20
c=3Db instanceof=20
this.constructor?b:a(b.currentTarget).data("bs."+this.type);if(c||(c=3Dne=
w=20
this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTa=
rget).data("bs."+this.type,c)),b=20
instanceof=20
a.Event&amp;&amp;(c.inState["focusout"=3D=3Db.type?"focus":"hover"]=3D!1)=
,!c.isInStateTrue())return=20
clearTimeout(c.timeout),c.hoverState=3D"out",c.options.delay&amp;&amp;c.o=
ptions.delay.hide?void(c.timeout=3DsetTimeout(function(){"out"=3D=3Dc.hov=
erState&amp;&amp;c.hide()},c.options.delay.hide)):c.hide()},c.prototype.s=
how=3Dfunction(){var=20
b=3Da.Event("show.bs."+this.type);if(this.hasContent()&amp;&amp;this.enab=
led){this.$element.trigger(b);var=20
d=3Da.contains(this.$element[0].ownerDocument.documentElement,this.$eleme=
nt[0]);if(b.isDefaultPrevented()||!d)return;var=20
e=3Dthis,f=3Dthis.tip(),g=3Dthis.getUID(this.type);this.setContent(),f.at=
tr("id",g),this.$element.attr("aria-describedby",g),this.options.animatio=
n&amp;&amp;f.addClass("fade");var=20
h=3D"function"=3D=3Dtypeof=20
this.options.placement?this.options.placement.call(this,f[0],this.$elemen=
t[0]):this.options.placement,i=3D/\s?auto?\s?/i,j=3Di.test(h);j&amp;&amp;=
(h=3Dh.replace(i,"")||"top"),f.detach().css({top:0,left:0,display:"block"=
}).addClass(h).data("bs."+this.type,this),this.options.container?f.append=
To(this.options.container):f.insertAfter(this.$element),this.$element.tri=
gger("inserted.bs."+this.type);var=20
k=3Dthis.getPosition(),l=3Df[0].offsetWidth,m=3Df[0].offsetHeight;if(j){v=
ar=20
n=3Dh,o=3Dthis.getPosition(this.$viewport);h=3D"bottom"=3D=3Dh&amp;&amp;k=
.bottom+m&gt;o.bottom?"top":"top"=3D=3Dh&amp;&amp;k.top-m<o.top?"bottom":=
"right"=3D=3Dh&&k.right+l>o.width?"left":"left"=3D=3Dh&amp;&amp;k.left-l<=
o.left?"right":h,f.removeclass(n).addclass(h)}var=20
a=3D'e.hoverState;e.$element.trigger("shown.bs."+e.type),e.hoverState=3Dn=
ull,"out"=3D=3Da&amp;&amp;e.leave(e)};a.support.transition&amp;&amp;this.=
$tip.hasClass("fade")?f.one("bsTransitionEnd",q).emulateTransitionEnd(c.T=
RANSITION_DURATION):q()}},c.prototype.applyPlacement=3Dfunction(b,c){var'=
=20
f=3D"window.SVGElement&amp;&amp;c" =
d=3D'this.tip(),e=3Dd[0].offsetWidth,f=3Dd[0].offsetHeight,g=3DparseInt(d=
.css("margin-top"),10),h=3DparseInt(d.css("margin-left"),10);isNaN(g)&amp=
;&amp;(g=3D0),isNaN(h)&amp;&amp;(h=3D0),b.top+=3Dg,b.left+=3Dh,a.offset.s=
etOffset(d[0],a.extend({using:function(a){d.css({top:Math.round(a.top),le=
ft:Math.round(a.left)})}},b),0),d.addClass("in");var'=20
e=3D'this,f=3Da(this.$tip),g=3Da.Event("hide.bs."+this.type);if(this.$ele=
ment.trigger(g),!g.isDefaultPrevented())return'=20
c=3D'b[0],d=3D"BODY"=3D=3Dc.tagName,e=3Dc.getBoundingClientRect();null=3D=
=3De.width&amp;&amp;(e=3Da.extend({},e,{width:e.right-e.left,height:e.bot=
tom-e.top}));var'=20
h=3D"b.top-f-g.scroll,i=3Db.top+f-g.scroll+d;h<g.top?e.top=3Dg.top-h:i" =
e;var=3D""=20
a.extend({},e,h,i,g)},c.prototype.getcalculatedoffset=3D'function(a,b,c,d=
){return"bottom"=3D=3Da?{top:b.top+b.height,left:b.left+b.width/2-c/2}:"t=
op"=3D=3Da?{top:b.top-d,left:b.left+b.width/2-c/2}:"left"=3D=3Da?{top:b.t=
op+b.height/2-d/2,left:b.left-c}:{top:b.top+b.height/2-d/2,left:b.left+b.=
width}},c.prototype.getViewportAdjustedDelta=3Dfunction(a,b,c,d){var'=20
window.svgelement,g=3D"d?{top:0,left:0}:f?null:b.offset(),h=3D{scroll:d?d=
ocument.documentElement.scrollTop||document.body.scrollTop:b.scrollTop()}=
,i=3Dd?{width:a(window).width(),height:a(window).height()}:null;return"=20
instanceof=3D"" =
this.gettitle()},c.prototype.getposition=3D"function(b){b=3Db||this.$elem=
ent;var"=20
a.attr("data-original-title"))&&a.attr("data-original-title",a.attr("titl=
e")||"").attr("title","")},c.prototype.hascontent=3D"function(){return"=20
f.removeclass("in"),a.support.transition&&f.hasclass("fade")?f.one("bstra=
nsitionend",d).emulatetransitionend(c.transition_duration):d(),this.hover=
state=3D"null,this},c.prototype.fixTitle=3Dfunction(){var"=20
d(){"in"!=3D'e.hoverState&amp;&amp;f.detach(),e.$element&amp;&amp;e.$elem=
ent.removeAttr("aria-describedby").trigger("hidden.bs."+e.type),b&amp;&am=
p;b()}var'=20
right")},c.prototype.hide=3D"function(b){function" left=3D"" bottom=3D"" =
top=3D"" in=3D""=20
l=3D'/top|bottom/.test(c),m=3Dl?2*k.left-e+i:2*k.top-f+j,n=3Dl?"offsetWid=
th":"offsetHeight";d.offset(b),this.replaceArrow(m,d[0][n],l)},c.prototyp=
e.replaceArrow=3Dfunction(a,b,c){this.arrow().css(c?"left":"top",50*(1-a/=
b)+"%").css(c?"top":"left","")},c.prototype.setContent=3Dfunction(){var' =

k=3D"this.getViewportAdjustedDelta(c,b,i,j);k.left?b.left+=3Dk.left:b.top=
+=3Dk.top;var"=20
i=3D'd[0].offsetWidth,j=3Dd[0].offsetHeight;"top"=3D=3Dc&amp;&amp;j!=3Df&=
amp;&amp;(b.top=3Db.top+f-j);var'=20
q=3D"function(){var" =
p=3D"this.getCalculatedOffset(h,k,l,m);this.applyPlacement(p,h);var">g.to=
p+g.height&amp;&amp;(e.top=3Dg.top+g.height-i)}else{var=20
j=3Db.left-f,k=3Db.left+f+c;j<g.left?e.left=3Dg.left-j:k>g.right&amp;&amp=
;(e.left=3Dg.left+g.width-k)}return=20
e},c.prototype.getTitle=3Dfunction(){var =
a,b=3Dthis.$element,c=3Dthis.options;return=20
a=3Db.attr("data-original-title")||("function"=3D=3Dtypeof=20
c.title?c.title.call(b[0]):c.title)},c.prototype.getUID=3Dfunction(a){do =

a+=3D~~(1e6*Math.random());while(document.getElementById(a));return=20
a},c.prototype.tip=3Dfunction(){if(!this.$tip&amp;&amp;(this.$tip=3Da(thi=
s.options.template),1!=3Dthis.$tip.length))throw=20
new Error(this.type+" `template` option must consist of exactly 1 =
top-level=20
element!");return this.$tip},c.prototype.arrow=3Dfunction(){return=20
this.$arrow=3Dthis.$arrow||this.tip().find(".tooltip-arrow")},c.prototype=
.enable=3Dfunction(){this.enabled=3D!0},c.prototype.disable=3Dfunction(){=
this.enabled=3D!1},c.prototype.toggleEnabled=3Dfunction(){this.enabled=3D=
!this.enabled},c.prototype.toggle=3Dfunction(b){var=20
c=3Dthis;b&amp;&amp;(c=3Da(b.currentTarget).data("bs."+this.type),c||(c=3D=
new=20
this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTa=
rget).data("bs."+this.type,c))),b?(c.inState.click=3D!c.inState.click,c.i=
sInStateTrue()?c.enter(c):c.leave(c)):c.tip().hasClass("in")?c.leave(c):c=
.enter(c)},c.prototype.destroy=3Dfunction(){var=20
a=3Dthis;clearTimeout(this.timeout),this.hide(function(){a.$element.off("=
."+a.type).removeData("bs."+a.type),a.$tip&amp;&amp;a.$tip.detach(),a.$ti=
p=3Dnull,a.$arrow=3Dnull,a.$viewport=3Dnull,a.$element=3Dnull})};var=20
d=3Da.fn.tooltip;a.fn.tooltip=3Db,a.fn.tooltip.Constructor=3Dc,a.fn.toolt=
ip.noConflict=3Dfunction(){return=20
a.fn.tooltip=3Dd,this}}(jQuery),+function(a){"use strict";function =
b(b){return=20
this.each(function(){var =
d=3Da(this),e=3Dd.data("bs.popover"),f=3D"object"=3D=3Dtypeof=20
b&amp;&amp;b;!e&amp;&amp;/destroy|hide/.test(b)||(e||d.data("bs.popover",=
e=3Dnew=20
c(this,f)),"string"=3D=3Dtypeof b&amp;&amp;e[b]())})}var=20
c=3Dfunction(a,b){this.init("popover",a,b)};if(!a.fn.tooltip)throw new=20
Error("Popover requires=20
tooltip.js");c.VERSION=3D"3.3.7",c.DEFAULTS=3Da.extend({},a.fn.tooltip.Co=
nstructor.DEFAULTS,{placement:"right",trigger:"click",content:"",template=
:'
<DIV class=3D"popover" role=3D"tooltip">
<DIV class=3D"arrow"></DIV>
<H3 class=3D"popover-title"></H3>
<DIV=20
class=3D"popover-content"></DIV></DIV>'}),c.prototype=3Da.extend({},a.fn.=
tooltip.Constructor.prototype),c.prototype.constructor=3Dc,c.prototype.ge=
tDefaults=3Dfunction(){return=20
c.DEFAULTS},c.prototype.setContent=3Dfunction(){var=20
a=3Dthis.tip(),b=3Dthis.getTitle(),c=3Dthis.getContent();a.find(".popover=
-title")[this.options.html?"html":"text"](b),a.find(".popover-content").c=
hildren().detach().end()[this.options.html?"string"=3D=3Dtypeof=20
c?"html":"append":"text"](c),a.removeClass("fade top bottom left right=20
in"),a.find(".popover-title").html()||a.find(".popover-title").hide()},c.=
prototype.hasContent=3Dfunction(){return=20
this.getTitle()||this.getContent()},c.prototype.getContent=3Dfunction(){v=
ar=20
a=3Dthis.$element,b=3Dthis.options;return=20
a.attr("data-content")||("function"=3D=3Dtypeof=20
b.content?b.content.call(a[0]):b.content)},c.prototype.arrow=3Dfunction()=
{return=20
this.$arrow=3Dthis.$arrow||this.tip().find(".arrow")};var=20
d=3Da.fn.popover;a.fn.popover=3Db,a.fn.popover.Constructor=3Dc,a.fn.popov=
er.noConflict=3Dfunction(){return=20
a.fn.popover=3Dd,this}}(jQuery),+function(a){"use strict";function=20
b(c,d){this.$body=3Da(document.body),this.$scrollElement=3Da(a(c).is(docu=
ment.body)?window:c),this.options=3Da.extend({},b.DEFAULTS,d),this.select=
or=3D(this.options.target||"")+"=20
.nav li &gt;=20
a",this.offsets=3D[],this.targets=3D[],this.activeTarget=3Dnull,this.scro=
llHeight=3D0,this.$scrollElement.on("scroll.bs.scrollspy",a.proxy(this.pr=
ocess,this)),this.refresh(),this.process()}function=20
c(c){return this.each(function(){var=20
d=3Da(this),e=3Dd.data("bs.scrollspy"),f=3D"object"=3D=3Dtypeof=20
c&amp;&amp;c;e||d.data("bs.scrollspy",e=3Dnew =
b(this,f)),"string"=3D=3Dtypeof=20
c&amp;&amp;e[c]()})}b.VERSION=3D"3.3.7",b.DEFAULTS=3D{offset:10},b.protot=
ype.getScrollHeight=3Dfunction(){return=20
this.$scrollElement[0].scrollHeight||Math.max(this.$body[0].scrollHeight,=
document.documentElement.scrollHeight)},b.prototype.refresh=3Dfunction(){=
var=20
b=3Dthis,c=3D"offset",d=3D0;this.offsets=3D[],this.targets=3D[],this.scro=
llHeight=3Dthis.getScrollHeight(),a.isWindow(this.$scrollElement[0])||(c=3D=
"position",d=3Dthis.$scrollElement.scrollTop()),this.$body.find(this.sele=
ctor).map(function(){var=20
b=3Da(this),e=3Db.data("target")||b.attr("href"),f=3D/^#./.test(e)&amp;&a=
mp;a(e);return=20
f&amp;&amp;f.length&amp;&amp;f.is(":visible")&amp;&amp;[[f[c]().top+d,e]]=
||null}).sort(function(a,b){return=20
a[0]-b[0]}).each(function(){b.offsets.push(this[0]),b.targets.push(this[1=
])})},b.prototype.process=3Dfunction(){var=20
a,b=3Dthis.$scrollElement.scrollTop()+this.options.offset,c=3Dthis.getScr=
ollHeight(),d=3Dthis.options.offset+c-this.$scrollElement.height(),e=3Dth=
is.offsets,f=3Dthis.targets,g=3Dthis.activeTarget;if(this.scrollHeight!=3D=
c&amp;&amp;this.refresh(),b&gt;=3Dd)return=20
g!=3D(a=3Df[f.length-1])&amp;&amp;this.activate(a);if(g&amp;&amp;b<e[0])r=
eturn=20
this.activetarget=3D"null,this.clear();for(a=3De.length;a--;)g!=3Df[a]&am=
p;&amp;b">=3De[a]&amp;&amp;(void=20
0=3D=3D=3De[a+1]||b<e[a+1])&&this.activate(f[a])},b.prototype.activate=3D=
function(b){ =
d=3D"a.fn.scrollspy;a.fn.scrollspy=3Dc,a.fn.scrollspy.Constructor=3Db,a.f=
n.scrollspy.noConflict=3Dfunction(){return"=20
e=3D'c.find(".active:last' =
c=3D"this.selector+'[data-target=3D&quot;'+b+'&quot;],'+this.selector+'[h=
ref=3D&quot;'+b+'&quot;]',d=3Da(c).parents(&quot;li&quot;).addClass(&quot=
;active&quot;);d.parent(&quot;.dropdown-menu&quot;).length&amp;&amp;(d=3D=
d.closest(&quot;li.dropdown&quot;).addClass(&quot;active&quot;)),d.trigge=
r(&quot;activate.bs.scrollspy&quot;)},b.prototype.clear=3Dfunction(){a(th=
is.selector).parentsUntil(this.options.target,&quot;.active&quot;).remove=
Class(&quot;active&quot;)};var"=20
this.each(function(){var=3D"" strict";function=3D"" =
h=3D'a(d);this.activate(b.closest("li"),c),this.activate(h,h.parent(),fun=
ction(){e.trigger({type:"hidden.bs.tab",relatedTarget:b[0]}),b.trigger({t=
ype:"shown.bs.tab",relatedTarget:e[0]})})}}},c.prototype.activate=3Dfunct=
ion(b,d,e){function'=20
b(b){return=3D"" =
b=3D'a(this);c.call(b,b.data())})})}(jQuery),+function(a){"use'=20
this.activetarget=3D"b,this.clear();var" =
f(){g.removeclass("active").find("=3D""=20
a"),f=3D'a.Event("hide.bs.tab",{relatedTarget:b[0]}),g=3Da.Event("show.bs=
.tab",{relatedTarget:e[0]});if(e.trigger(f),b.trigger(g),!g.isDefaultPrev=
ented()&amp;&amp;!f.isDefaultPrevented()){var'=20
b&&e[b]()})}var=3D"" c(this)),"string"=3D"=3Dtypeof" =
a.fn.scrollspy=3D"d,this},a(window).on(&quot;load.bs.scrollspy.data-api&q=
uot;,function(){a('[data-spy=3D&quot;scroll&quot;]').each(function(){var"=
>=20
.dropdown-menu &gt;=20
.active").removeClass("active").end().find('[data-toggle=3D"tab"]').attr(=
"aria-expanded",!1),b.addClass("active").find('[data-toggle=3D"tab"]').at=
tr("aria-expanded",!0),h?(b[0].offsetWidth,b.addClass("in")):b.removeClas=
s("fade"),b.parent(".dropdown-menu").length&amp;&amp;b.closest("li.dropdo=
wn").addClass("active").end().find('[data-toggle=3D"tab"]').attr("aria-ex=
panded",!0),e&amp;&amp;e()}var=20
g=3Dd.find("&gt;=20
.active"),h=3De&amp;&amp;a.support.transition&amp;&amp;(g.length&amp;&amp=
;g.hasClass("fade")||!!d.find("&gt;=20
.fade").length);g.length&amp;&amp;h?g.one("bsTransitionEnd",f).emulateTra=
nsitionEnd(c.TRANSITION_DURATION):f(),g.removeClass("in")};var=20
d=3Da.fn.tab;a.fn.tab=3Db,a.fn.tab.Constructor=3Dc,a.fn.tab.noConflict=3D=
function(){return=20
a.fn.tab=3Dd,this};var=20
e=3Dfunction(c){c.preventDefault(),b.call(a(this),"show")};a(document).on=
("click.bs.tab.data-api",'[data-toggle=3D"tab"]',e).on("click.bs.tab.data=
-api",'[data-toggle=3D"pill"]',e)}(jQuery),+function(a){"use=20
strict";function b(b){return this.each(function(){var=20
d=3Da(this),e=3Dd.data("bs.affix"),f=3D"object"=3D=3Dtypeof=20
b&amp;&amp;b;e||d.data("bs.affix",e=3Dnew =
c(this,f)),"string"=3D=3Dtypeof=20
b&amp;&amp;e[b]()})}var=20
c=3Dfunction(b,d){this.options=3Da.extend({},c.DEFAULTS,d),this.$target=3D=
a(this.options.target).on("scroll.bs.affix.data-api",a.proxy(this.checkPo=
sition,this)).on("click.bs.affix.data-api",a.proxy(this.checkPositionWith=
EventLoop,this)),this.$element=3Da(b),this.affixed=3Dnull,this.unpin=3Dnu=
ll,this.pinnedOffset=3Dnull,this.checkPosition()};c.VERSION=3D"3.3.7",c.R=
ESET=3D"affix=20
affix-top=20
affix-bottom",c.DEFAULTS=3D{offset:0,target:window},c.prototype.getState=3D=
function(a,b,c,d){var=20
e=3Dthis.$target.scrollTop(),f=3Dthis.$element.offset(),g=3Dthis.$target.=
height();if(null!=3Dc&amp;&amp;"top"=3D=3Dthis.affixed)return=20
e<c&&"top";if("bottom"=3D=3Dthis.affixed)return =
h=3D"null=3D=3Dthis.affixed,i=3Dh?e:f.top,j=3Dh?g:b;return"=20
null!=3D'c?!(e+this.unpin<=3Df.top)&amp;&amp;"bottom":!(e+g<=3Da-d)&amp;&=
amp;"bottom";var'>=3Da-d&amp;&amp;"bottom"},c.prototype.getPinnedOffset=3D=
function(){if(this.pinnedOffset)return=20
this.pinnedOffset;this.$element.removeClass(c.RESET).addClass("affix");va=
r=20
a=3Dthis.$target.scrollTop(),b=3Dthis.$element.offset();return=20
this.pinnedOffset=3Db.top-a},c.prototype.checkPositionWithEventLoop=3Dfun=
ction(){setTimeout(a.proxy(this.checkPosition,this),1)},c.prototype.check=
Position=3Dfunction(){if(this.$element.is(":visible")){var=20
b=3Dthis.$element.height(),d=3Dthis.options.offset,e=3Dd.top,f=3Dd.bottom=
,g=3DMath.max(a(document).height(),a(document.body).height());"object"!=3D=
typeof=20
d&amp;&amp;(f=3De=3Dd),"function"=3D=3Dtypeof=20
e&amp;&amp;(e=3Dd.top(this.$element)),"function"=3D=3Dtypeof=20
f&amp;&amp;(f=3Dd.bottom(this.$element));var=20
h=3Dthis.getState(g,b,e,f);if(this.affixed!=3Dh){null!=3Dthis.unpin&amp;&=
amp;this.$element.css("top","");var=20
i=3D"affix"+(h?"-"+h:""),j=3Da.Event(i+".bs.affix");if(this.$element.trig=
ger(j),j.isDefaultPrevented())return;this.affixed=3Dh,this.unpin=3D"botto=
m"=3D=3Dh?this.getPinnedOffset():null,this.$element.removeClass(c.RESET).=
addClass(i).trigger(i.replace("affix","affixed")+".bs.affix")}"bottom"=3D=
=3Dh&amp;&amp;this.$element.offset({top:g-b-f})}};var=20
d=3Da.fn.affix;a.fn.affix=3Db,a.fn.affix.Constructor=3Dc,a.fn.affix.noCon=
flict=3Dfunction(){return=20
a.fn.affix=3Dd,this},a(window).on("load",function(){a('[data-spy=3D"affix=
"]').each(function(){var=20
c=3Da(this),d=3Dc.data();d.offset=3Dd.offset||{},null!=3Dd.offsetBottom&a=
mp;&amp;(d.offset.bottom=3Dd.offsetBottom),null!=3Dd.offsetTop&amp;&amp;(=
d.offset.top=3Dd.offsetTop),b.call(c,d)})})}(jQuery);</c&&"top";if("botto=
m"=3D=3Dthis.affixed)return></e[a+1])&&this.activate(f[a])},b.prototype.a=
ctivate=3Dfunction(b){></e[0])return></g.left?e.left=3Dg.left-j:k></o.lef=
t?"right":h,f.removeclass(n).addclass(h)}var></o.top?"bottom":"right"=3D=3D=
h&&k.right+l></a,this.scrollbarwidth=3Dthis.measurescrollbar()},c.prototy=
pe.setscrollbar=3Dfunction(){var></i.length-1&&j++,~j||(j=3D0),i.eq(j).tr=
igger("focus")}}}};var></BODY></HTML>
