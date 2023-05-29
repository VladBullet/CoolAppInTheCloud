document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.querySelector("#login-form");
  const contentArea = document.querySelector("#content");
  const logoutButton = document.querySelector("#logout-btn");
  const usernameInput = document.querySelector("#username");
  const passwordInput = document.querySelector("#password");

  loginForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const username = usernameInput.value;
    const password = passwordInput.value;

    // Make an API request to authenticate the user
    login(username, password, loginForm, contentArea);
  });

  logoutButton.addEventListener("click", function () {
    // Remove the token from localStorage or sessionStorage
    localStorage.removeItem("jwtToken");

    // Show the login form and hide the content area
    loginForm.style.display = "block";
    contentArea.style.display = "none";

    // Clear the username and password fields
    usernameInput.value = "";
    passwordInput.value = "";
  });

  // Check if a token exists in localStorage or sessionStorage
  const token = localStorage.getItem("jwtToken");

  if (token) {
    // Hide the login form and show the content area
    loginForm.style.display = "none";
    contentArea.style.display = "block";
  } else {
    // Show the login form and hide the content area
    loginForm.style.display = "block";
    contentArea.style.display = "none";
  }
});

$(document).on("click", ".deletePerson", function (e) {
  const target = e.target.closest(".deletePerson"); // Or any other selector.
  console.log(target, "target");
  if (target) {
    var tr = $(this).closest("tr");
    var id = $(tr).attr("id");
    console.log(id);
    deletePerson(id);
    var value = $("#tableRefresh").val();
    $("#tableRefresh").val(value + 1);
    $("#tableRefresh").trigger("change");
  }
});

$(document).on("change", "#tableRefresh", function (e) {
  console.log("changes", $(this).val());
  fetchPeopleList();
});


