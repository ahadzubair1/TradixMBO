// let direction = 1; // 1 for right to left, -1 for left to right
//
// // Function to continuously move the slider
// function scroll() {
//   const container = document.getElementById("scroll-container");
//   container.scrollLeft += direction; // Adjust scroll speed as needed
//
//   if (direction === 1 && container.scrollLeft >= (container.scrollWidth - container.clientWidth)) {
//     direction = -1;
//   } else if (direction === -1 && container.scrollLeft <= 0) {
//     direction = 1;
//   }
// }
//
// // Call scroll function repeatedly
// setInterval(scroll, 20); // Adjust scroll speed as needed


// In your Javascript (external .js resource or <script> tag)
// Function to show Offcanvas after a delay
function showOffcanvasWithDelay() {
    var offcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasExample'));
    setTimeout(function () {
        offcanvas.show();
    }, 5000); // 10 seconds delay
}

// Show Offcanvas after page load
window.addEventListener('load', showOffcanvasWithDelay);


window.addEventListener("load", function () {
    // Add smooth scrolling to all links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
});

const text = `Your journey to a transformed financial future starts here.
    Welcome to Tradiix, where possibilities become profits!`;

let index = 0;
let line = 0;
const speed = 50; // typing speed in milliseconds

function typeWriter() {
    if (index < text.length) {
        const char = text.charAt(index);
        const div = document.getElementById("typewriter");

        if (char === '\n') {
            line++;
        }

        div.innerHTML += char;

        index++;
        setTimeout(typeWriter, speed);
    }
}

typeWriter();