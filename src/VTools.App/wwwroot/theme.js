window.toggleTheme = function () {
    var next = document.documentElement.getAttribute('data-theme') === 'dark' ? 'light' : 'dark';
    localStorage.setItem('theme', next);
    document.documentElement.setAttribute('data-theme', next);
};

new MutationObserver(function () {
    var saved = localStorage.getItem('theme') || 'light';
    if (document.documentElement.getAttribute('data-theme') !== saved) {
        document.documentElement.setAttribute('data-theme', saved);
    }
}).observe(document.documentElement, { attributes: true, attributeFilter: ['data-theme'] });
