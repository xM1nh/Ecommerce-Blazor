window.ShowDialog = function() {
    document.getElementById('my-dialog').showModal()
}

//let currentIndex = 0;

//const showSlides = () => {
//    const carousel = document.querySelector('.carousel')
//    const totalItems = document.querySelectorAll('.carousel-item').length

//    if (currentIndex < 0) {
//        currentIndex = totalItems - 3;
//    } else if (currentIndex >= totalItems - 2) {
//        currentIndex = 0
//    }

//    const translateValue = (-currentIndex * (100 / 3)).toString() + '%'
//    carousel.style.transform = 'translateX(' + translateValue + ')'
//}

//const prevSlide = () => {
//    currentIndex--
//    showSlides()
//}

//const nextSlide = () => {
//    currentIndex++
//    showSlides()
//}

//showSlides()
