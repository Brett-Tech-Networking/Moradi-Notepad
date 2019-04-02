var profilePicture = $('.picture-container')[0];
var introText = $('.text-container')[0];
var ageCounter = $('#age-counter');

var isMobile = (screen && screen.width < 640);

// Do not allow mobile devices to interact with the particles - it will cause random lag
var particleSettings = {"particles":{"number":{"value":60,"density":{"enable":true,"value_area":800}},"color":{"value":"#ffffff"},"shape":{"type":"circle","stroke":{"width":0,"color":"#000000"},"polygon":{"nb_sides":5},"image":{"src":"img/github.svg","width":100,"height":100}},"opacity":{"value":0.5,"random":false,"anim":{"enable":false,"speed":1,"opacity_min":0.1,"sync":false}},"size":{"value":3,"random":true,"anim":{"enable":false,"speed":40,"size_min":0.1,"sync":false}},"line_linked":{"enable":true,"distance":150,"color":"#ffffff","opacity":0.4,"width":1},"move":{"enable":true,"speed":4,"direction":"none","random":false,"straight":false,"out_mode":"out","bounce":false,"attract":{"enable":false,"rotateX":600,"rotateY":1200}}},"interactivity":{"detect_on":"canvas","events":{"onhover":{"enable":!isMobile,"mode":"grab"},"onclick":{"enable":false,"mode":"push"},"resize":true},"modes":{"grab":{"distance":150,"line_linked":{"opacity":1}},"bubble":{"distance":400,"size":40,"duration":2,"opacity":8,"speed":3},"repulse":{"distance":200,"duration":0.4},"push":{"particles_nb":4},"remove":{"particles_nb":2}}},"retina_detect":true};
var birthday = '2001-01-30';

setInterval(function () {
    calcAge(birthday);
}, 2000);

calcAge(birthday);
function calcAge(dateString) {
    var b = new Date(dateString);
    ageCounter.text(((Date.now() - b) / (31557600000)).toFixed(7));
}

particlesJS('pjs', particleSettings);
