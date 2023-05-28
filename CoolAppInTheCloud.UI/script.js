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

function login(username, password, loginForm, contentArea) {
  fetch("https://localhost:44382/security/createToken", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username, password }),
  })
    .then((response) => response.json())
    .then((data) => {
      const { token } = data;

      if (token) {
        // Store the token in localStorage or sessionStorage
        localStorage.setItem("jwtToken", token);

        // Hide the login form and show the content area
        loginForm.style.display = "none";
        contentArea.style.display = "block";
      } else {
        // Show an error message or handle authentication failure
        console.log("Authentication failed");
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}


