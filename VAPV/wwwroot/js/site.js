document.addEventListener("DOMContentLoaded", function () {
    var toggler = document.querySelector('.navbar-toggler');
    var navbarNav = document.querySelector('#navbarNav');

    toggler.addEventListener('click', function () {
        console.log('Hamburger clicked');
    });

    navbarNav.addEventListener('show.bs.collapse', function () {
        console.log('Navbar is being shown');
    });

    navbarNav.addEventListener('hide.bs.collapse', function () {
        console.log('Navbar is being hidden');
    });
});
