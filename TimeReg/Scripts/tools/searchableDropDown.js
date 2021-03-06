//not in use anymore

$(function () {
    $.widget("custom.combobox", {

        _create: function () {
            this.wrapper = $("<span>")
              .addClass("custom-combobox")
              .attr("id", "combobox" + this.element.attr("id"))
              .insertAfter(this.element);


            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
            this.input.click(function (event) {
                var currentValue = $(this).val();
                if (currentValue == "") {
                    $(this).autocomplete('search', '');
                } else $(this).autocomplete('search', $(this).val());
            });
            this.input.change(function (event) {
                var currentValue = $(this).val();
                if (currentValue == "") {
                    $(this).autocomplete('search', '');
                } else $(this).autocomplete('search', $(this).val());
            });
        },


        _createAutocomplete: function () {
           
            var selected = this.element.children(":selected"),
              value = selected.val() ? selected.text() : "";

            this.input = $("<input>")
              .appendTo(this.wrapper)
              .val(value)
              .attr("title", "")
              .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left form-control")
              .autocomplete({
                  delay: 0,
                  minLength: 0,
                  change: function (event, ui) {

                      try{
                          //Triggers the Ajax call on the change event
                          DropDown_IndexChange($(this).closest('span').attr('id'));
                      } catch (err) {
                          console.log(err);
                      }
                   

                  },
                  source: $.proxy(this, "_source"),
                  select: function (event, ui) {

                      try {
                          //Triggers the Ajax call on the change event
                          DropDown_IndexChange($(this).closest('span').attr('id'));
                      } catch (err) {
                          console.log(err);
                      }



                  },
              })
              .tooltip({
                  classes: {
                      "ui-tooltip": "ui-state-highlight"
                  }
              });

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                },

                autocompletechange: "_removeIfInvalid"
            });

            //This triggers a validity check on change - if it mathces top one it gets a succesful SQL call
            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    this._trigger("change", event, {
                        item: ui.item.option
                    });
                },

                autocompletechange: "_removeIfInvalid"
            });



        },

        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;
            //Added for testing dynamic page.
            console.log(this);
            $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "Show All Items")
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("custom-combobox-toggle ui-corner-right")
              .on("mousedown", function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .on("click", function () {
                  input.trigger("focus");

                  // Close if already visible
                  if (wasOpen) {
                      return;
                  }

                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value == request.term && this.value != "") {

                    return {
                        label: text,
                        value: text,
                        option: this,
                        select: true


                    };
                }


                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            // Remove invalid value
            this.input
              .val("")
              .attr("title", value + " didn't match any item")
              .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";

            return;
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        },

    });

    $(".selectpicker").combobox();
    //$("custom-combobox").focusout(function () {
    //    Alert("triggered");
    //});
});



