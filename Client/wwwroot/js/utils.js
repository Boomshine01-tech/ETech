// Fonction pour scroller vers un élément
window.scrollToElement = (id) => {
    const element = document.getElementById(id);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};

// Fonction pour scroller le carousel avec ElementReference et direction
window.scrollCarousel = (element, direction) => {
    if (element) {
        const itemWidth = element.querySelector('.carousel-item')?.offsetWidth || 320;
        const gap = 16; // 1rem = 16px
        const scrollAmount = (itemWidth + gap) * direction;
        
        element.scrollBy({ left: scrollAmount, behavior: 'smooth' });
    }
};

// Fonction pour toggle "voir plus"
window.toggleMore = (show) => {
    const element = document.getElementById('morePartenaire');
    if (element) {
        element.style.display = show ? 'block' : 'none';
    }
};

// Fonction pour scroller vers une section
window.scrollToSection = (id) => {
    const element = document.getElementById(id);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};

// Initialiser les animations au scroll avec Intersection Observer
window.initScrollAnimations = () => {
    const observerOptions = {
        root: null,
        rootMargin: '0px',
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                
                // Animer les enfants avec délai
                const items = entry.target.querySelectorAll('.scroll-animate-item');
                items.forEach((item, index) => {
                    setTimeout(() => {
                        item.classList.add('visible');
                    }, index * 100);
                });
            }
        });
    }, observerOptions);

    // Observer les sections principales
    document.querySelectorAll('.scroll-animate').forEach(el => {
        observer.observe(el);
    });

    // Observer les items individuels qui ne sont pas dans une section
    document.querySelectorAll('.scroll-animate-item').forEach(el => {
        if (!el.closest('.scroll-animate')) {
            observer.observe(el);
        }
    });
};

// Initialiser automatiquement quand le DOM est prêt
document.addEventListener('DOMContentLoaded', () => {
    window.initScrollAnimations();
});
