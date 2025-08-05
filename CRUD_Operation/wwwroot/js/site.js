// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
 
        // Update last updated time
        document.addEventListener('DOMContentLoaded', function() {
            const lastUpdatedElement = document.getElementById('lastUpdated');
        if (lastUpdatedElement) {
                const now = new Date();
        lastUpdatedElement.textContent = now.toLocaleDateString('en-US', {
            year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
                });
            }
        });

        // Back to top functionality
        function scrollToTop() {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        }

        // Show/hide back to top button
        window.addEventListener('scroll', function() {
            const backToTopBtn = document.querySelector('.back-to-top');
        if (backToTopBtn) {
                if (window.pageYOffset > 300) {
            backToTopBtn.classList.add('visible');
                } else {
            backToTopBtn.classList.remove('visible');
                }
            }
        });

        // Newsletter subscription (you can connect this to your backend)
        document.addEventListener('DOMContentLoaded', function() {
            const newsletterBtn = document.querySelector('.newsletter-input .btn');
        const newsletterInput = document.querySelector('.newsletter-input .form-control');

        if (newsletterBtn && newsletterInput) {
            newsletterBtn.addEventListener('click', function () {
                const email = newsletterInput.value.trim();
                if (email && isValidEmail(email)) {
                    // Here you would typically send the email to your backend
                    showToast('Thank you for subscribing to our newsletter!', 'success');
                    newsletterInput.value = '';
                } else {
                    showToast('Please enter a valid email address.', 'error');
                }
            });

        newsletterInput.addEventListener('keypress', function(e) {
                    if (e.key === 'Enter') {
            newsletterBtn.click();
                    }
                });
            }
        });

        // Email validation
        function isValidEmail(email) {
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
        }

        // Simple toast notification
        function showToast(message, type = 'info') {
            const toast = document.createElement('div');
        toast.className = `toast-notification toast-${type}`;
        toast.innerHTML = `
        <div class="toast-content">
            <i class="bi bi-${type === 'success' ? 'check-circle-fill' : 'exclamation-triangle-fill'}"></i>
            <span>${message}</span>
        </div>
        `;

        // Add toast styles
        toast.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: ${type === 'success' ? '#48bb78' : '#f56565'};
        color: white;
        padding: 1rem 1.5rem;
        border-radius: 8px;
        box-shadow: 0 4px 20px rgba(0,0,0,0.15);
        z-index: 10000;
        opacity: 0;
        transform: translateX(100%);
        transition: all 0.3s ease;
        `;

        document.body.appendChild(toast);

            // Animate in
            setTimeout(() => {
            toast.style.opacity = '1';
        toast.style.transform = 'translateX(0)';
            }, 100);

            // Remove after 3 seconds
            setTimeout(() => {
            toast.style.opacity = '0';
        toast.style.transform = 'translateX(100%)';
                setTimeout(() => {
                    if (document.body.contains(toast)) {
            document.body.removeChild(toast);
                    }
                }, 300);
            }, 3000);
        }


