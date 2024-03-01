$(document).ready(function () {
    $("#query").on("input", function () {
        var query = $(this).val();
        var searchResultsContainer = $("#searchResults");

        // Call your backend API to get matching product results
        $.ajax({
            url: "/api/search", // Replace with your actual API endpoint
            data: { query: query },
            success: function (data) {
                displaySearchResults(data);
            }
        });

        function displaySearchResults(results) {
            searchResultsContainer.empty();

            if (results.length > 0) {
                for (var i = 0; i < results.length; i++) {
                    var result = results[i];
                    var item = $("<div>").addClass("search-result-item");
                    var link = $("<a>").attr("href", "/Product/Detail/" + result.id);
                    var image = $("<img>").attr("src", result.imageUrl).attr("alt", result.name);
                    var name = $("<span>").text(result.name);

                    link.append(image, name);
                    item.append(link);
                    searchResultsContainer.append(item);
                }
                searchResultsContainer.show();
            } else {
                searchResultsContainer.hide();
            }
        }
    });

    // Close dropdown when clicking outside the search form
    $(document).on("click", function (e) {
        if (!$(e.target).closest("#searchBar").length) {
            $("#searchResults").hide();
        }
    });
});
