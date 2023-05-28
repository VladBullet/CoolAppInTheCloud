const baseURL = "https://localhost:44382/api/";

// Fetch and display the list of people
function fetchPeopleList() {
  fetch(baseURL + "people/getAllPeople", {
    headers: {
      Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
    },
  })
    .then((response) => response.json())
    .then((data) => {
      data.forEach((person) => {
        renderPerson(person);
      });
    })
    .catch((error) => {
      console.error("Error fetching people:", error);
    });
}

function renderPerson(person) {
  // Create a new table row
  const row = document.createElement("tr");

  // Iterate over each property of the person object
  Object.values(person).forEach((value) => {
    // Create a new table cell
    const cell = document.createElement("td");
    cell.textContent = Array.isArray(value) ? value.join(", ") : value;

    // Append the cell to the row
    row.appendChild(cell);
  });

  // Append the row to the table body
  const tableBody = document.getElementById("personTableBody");
  tableBody.appendChild(row);
}

// Event listener for search input
document.getElementById("search-input").addEventListener("input", () => {
  const searchInput = document
    .getElementById("search-input")
    .value.toLowerCase();
  const peopleList = document.getElementById("people-list");
  const peopleItems = peopleList.getElementsByTagName("li");

  for (let i = 0; i < peopleItems.length; i++) {
    const personName = peopleItems[i].textContent.toLowerCase();
    if (personName.includes(searchInput)) {
      peopleItems[i].style.display = "block";
    } else {
      peopleItems[i].style.display = "none";
    }
  }
});

// Call the fetchPeopleList function when the content is displayed
document.getElementById("content").style.display = "block";
fetchPeopleList();

function filterPersons(searchTerm) {
  // Make an API request to retrieve the filtered person data
  fetch(
    baseURL + `people/searchByFilter?search=${encodeURIComponent(searchTerm)}`
  )
    .then((response) => response.json())
    .then((data) => {
      // Clear the table
      clearTable();

      // Render each person in the table
      data.forEach((person) => {
        renderPerson(person);
      });
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}

// Event listener for the search button click
const searchButton = document.getElementById("searchButton");
searchButton.addEventListener("click", () => {
  const searchInput = document.getElementById("searchInput");
  const searchTerm = searchInput.value.trim();

  // Clear the search input
  searchInput.value = "";

  // Filter the person data based on the search term
  filterPersons(searchTerm);
});
