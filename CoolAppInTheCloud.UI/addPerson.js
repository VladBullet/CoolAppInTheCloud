// Add event listener for the form submission
$("#addPersonForm").submit(function (event) {
  event.preventDefault(); // Prevent the form from submitting normally

  var name = $("#name").val();
  var identifiesAs = $("#identifiesAs").val();
  var age = parseInt($("#age").val());
  var city = $("#city").val();
  var country = $("#country").val();
  var occupation = $("#occupation").val();
  var favoriteFoods = $("#favoriteFoods").val().split(",");
  var shoeSize = parseInt($("#shoeSize").val());
  var hairColor = $("#hairColor").val();
  var realHairColor = $("#realHairColor").val();
  var eyeColor = $("#eyeColor").val();
  var watchBrand = $("#watchBrand").val();
  var cellPhoneBrand = $("#cellPhoneBrand").val();
  var favoriteDrink = $("#favoriteDrink").val();
  var beenInKristiansand = $("#beenInKristiansand").is(":checked");
  var likeBaguettes = $("#likeBaguettes").is(":checked");
  var coffeeContainer = $("#coffeeContainer").val();

  // Create a new person object
  var person = {
    Id: "",
    Name: name,
    IdentifiesAs: identifiesAs,
    Age: age,
    City: city,
    Country: country,
    Occupation: occupation,
    FavoriteFoods: favoriteFoods,
    ShoeSize: shoeSize,
    HairColor: hairColor,
    RealHairColor: realHairColor,
    EyeColor: eyeColor,
    WatchBrand: watchBrand,
    CellPhoneBrand: cellPhoneBrand,
    FavoriteDrink: favoriteDrink,
    BeenInKristiansand: beenInKristiansand,
    LikeBaguettes: likeBaguettes,
    CoffeeContainer: coffeeContainer,
  };

  // Send the person data to the API endpoint for adding a new person
  fetch(baseURL + "people/AddPerson", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
    },
    body: JSON.stringify(person),
  })
    .then(function (response) {
      if (response.ok) {
        // Handle the successful response, such as showing a success message or updating the table
        console.log("Person added successfully");
        fetchPeopleList(); // Refresh the table to display the updated data
        $("#addPersonModal").modal("hide"); // Hide the modal after successful submission
      } else {
        // Handle the error response, such as displaying an error message
        console.log(response);
        throw new Error("Error adding person");
      }
    })
    .catch(function (error) {
      console.error("Error adding person:", error);
    });
});
