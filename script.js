"use strict";

const preloader = document.getElementById("preloader");
const nav = document.getElementById("mainNav");
const progressBar = document.getElementById("scrollProgress");
const backToTop = document.getElementById("backToTop");
const themeToggle = document.getElementById("themeToggle");
const root = document.documentElement;
const navLinks = document.querySelectorAll('.navbar .nav-link[href^="#"]');
const sections = document.querySelectorAll("main section[id]");
const counters = document.querySelectorAll(".counter");
const navCollapse = document.getElementById("navbarNav");

const getNavOffset = () => {
  if (!nav) {
    return 0;
  }

  return nav.offsetHeight + 12;
};

window.addEventListener("load", () => {
  if (preloader) {
    preloader.classList.add("hidden");
  }
});

const setTheme = (theme) => {
  root.setAttribute("data-theme", theme);
  localStorage.setItem("portfolio-theme", theme);

  if (themeToggle) {
    themeToggle.innerHTML =
      theme === "dark"
        ? '<i class="bi bi-sun"></i>'
        : '<i class="bi bi-moon-stars"></i>';
  }
};

const savedTheme = localStorage.getItem("portfolio-theme");
if (savedTheme === "dark" || savedTheme === "light") {
  setTheme(savedTheme);
}

if (themeToggle) {
  themeToggle.addEventListener("click", () => {
    const currentTheme = root.getAttribute("data-theme");
    setTheme(currentTheme === "light" ? "dark" : "light");
  });
}

if (navLinks.length > 0) {
  navLinks.forEach((link) => {
    link.addEventListener("click", (event) => {
      const href = link.getAttribute("href") || "";
      const targetId = href.startsWith("#") ? href.slice(1) : "";
      const targetSection = targetId ? document.getElementById(targetId) : null;

      if (!targetSection) {
        return;
      }

      event.preventDefault();
      const targetTop = targetSection.getBoundingClientRect().top + window.scrollY - getNavOffset();
      window.scrollTo({ top: Math.max(targetTop, 0), behavior: "smooth" });

      if (
        navCollapse &&
        window.innerWidth < 992 &&
        navCollapse.classList.contains("show") &&
        typeof bootstrap !== "undefined"
      ) {
        bootstrap.Collapse.getOrCreateInstance(navCollapse).hide();
      }
    });
  });
}

const setActiveNav = () => {
  if (sections.length === 0 || navLinks.length === 0) {
    return;
  }

  const scrollMarker = window.scrollY + getNavOffset() + 20;
  let activeId = "";

  sections.forEach((section) => {
    const top = section.offsetTop;
    const bottom = top + section.offsetHeight;

    if (scrollMarker >= top && scrollMarker < bottom) {
      activeId = section.id;
    }
  });

  if (!activeId && sections.length > 0) {
    activeId = sections[0].id;
  }

  navLinks.forEach((link) => {
    const href = link.getAttribute("href") || "";
    const isActive = href === `#${activeId}`;
    link.classList.toggle("active", isActive);
    if (isActive) {
      link.setAttribute("aria-current", "page");
    } else {
      link.removeAttribute("aria-current");
    }
  });
};

const onScroll = () => {
  const scrollTop = window.scrollY;
  const docHeight = document.documentElement.scrollHeight - window.innerHeight;

  if (nav) {
    nav.classList.toggle("nav-scrolled", scrollTop > 30);
  }

  if (progressBar) {
    const progress = docHeight > 0 ? (scrollTop / docHeight) * 100 : 0;
    progressBar.style.width = `${progress}%`;
  }

  if (backToTop) {
    backToTop.style.display = scrollTop > 450 ? "grid" : "none";
  }

  setActiveNav();
};

window.addEventListener("scroll", onScroll);
onScroll();

if (backToTop) {
  backToTop.addEventListener("click", () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
  });
}

if ("IntersectionObserver" in window) {
  const revealObserver = new IntersectionObserver(
    (entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add("revealed");
          observer.unobserve(entry.target);
        }
      });
    },
    { threshold: 0.15 }
  );

  document.querySelectorAll(".reveal").forEach((element) => revealObserver.observe(element));

  const runCounter = (counter) => {
    const target = Number(counter.dataset.target);
    let current = 0;
    const increment = Math.max(1, Math.ceil(target / 80));

    const step = () => {
      current += increment;
      if (current >= target) {
        counter.textContent = target;
        return;
      }
      counter.textContent = current;
      requestAnimationFrame(step);
    };

    step();
  };

  if (counters.length > 0) {
    const counterObserver = new IntersectionObserver(
      (entries, observer) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            runCounter(entry.target);
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.6 }
    );

    counters.forEach((counter) => counterObserver.observe(counter));
  }
}

document.querySelectorAll('a[data-coming-soon="true"]').forEach((link) => {
  link.addEventListener("click", (event) => {
    event.preventDefault();
  });
});
