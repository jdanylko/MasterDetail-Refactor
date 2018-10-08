$(function() {

    function productDialog() { return $(".product-dialog"); }
    function productGrid() { return $("table", productDialog()); }

    function orderGrid() { return $(".order-grid"); }
    function selectButton() { return $(".select-button", productDialog()); }
    function selectedProducts() { return $("input:checked", productDialog()); }

    function displayProductGrid() {

        $.post("/Home/ProductGrid", {})
            .done(function(html) {
                var body = $(".modal-body", productDialog());
                $(body).replaceWith(html);
            });
    }

    // Setup the product dialog
    productDialog().on("show.bs.modal", null, null, displayProductGrid);

    selectButton().on("click",
        function() {
            // Grab the existing product ids
            var selectedIds = $("td:nth-child(1)", orderGrid())
                .map((index, elem) => {
                    var row = $(elem).closest("tr");
                    return { ProductId: $("td:nth-child(1) :input", row).val() };
                });

            // get the list of selected checkboxes.
            var dialogProducts = selectedProducts()
                .map((index, inputCtrl) => {

                    var value = $(inputCtrl).val();
                    // For each row that's "mapped", return an object that
                    //  describes each <td> in the row.
                    return {
                        ProductId: value
                    };
                })
                .get();

            // Add the selected ids to the map.
            selectedIds.map((index, value) => {
                dialogProducts.push(value);
            });

            var postData = {
                Cart: {
                    CustomerId: 1, // Strictly for demo purposes.
                    CartId: 1,     // Ditto
                    Items: Array.from(dialogProducts)
                }
            };

            // Hide the dialog.
            productDialog().modal("hide");

            $(".loading-status").removeClass("hidden").show();

            $.post("/Home/OrderGrid", postData).done(function(html) {

                $(".loading-status").hide();

                orderGrid().replaceWith(html);
            });


        });
})