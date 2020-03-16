document.addEventListener("DOMContentLoaded", function () {
    
    // Url params
    let url_term;
    let url_accredited;
    let url_national;
    let url_categories;

    // DOM Elements
    const charitySearch = document.getElementById('charity-search-submit');
    const charitySearchInput = document.getElementById('PlaceHolderSearchText');
    const charitySearchResultsTerm = document.getElementById('search-results-term');
    const filterAccredited = document.getElementById('filterAccredited');

    const getParams = function () {
        // Get params from url
        const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);

        // Store all params from search params object
        url_term = urlParams.get("term");
        url_accredited = urlParams.get("accredited");
        url_national = urlParams.get("national");
        url_categories = urlParams.getAll("category");

    }

    const updateDOM = function () {
        // Term
        url_term && charitySearchInput.setAttribute("value", url_term);
        url_term && (charitySearchResultsTerm.innerHTML = url_term);

        // Accredited
        // National
        // Categories
    }
    // url_categories && url_categories.forEach(category => { });

    url_accredited && (filterAccredited.checked = url_accredited);

    
    
    const redirect = function (event, inputValue) {

        event.preventDefault();
        let url = '/search/?term=';
        url += inputValue;

        const filterAccredited = document.getElementById('filterAccredited');
        const isAccredited = filterAccredited.checked;

        if (isAccredited) {
            url += "&accredited=true";
        }

        window.location.href = url;
    }

    // Add event listeners
    charitySearch && charitySearch.addEventListener('click', function (e) { redirect(e, charitySearchInput.value) })
});