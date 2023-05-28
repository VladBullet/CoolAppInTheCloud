const baseURL = "https://localhost:44382/api/";

// Fetch and display the list of people
async function fetchPeopleList() {
  await fetch(baseURL + "people/getAllPeople", {
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
  row.setAttribute("id", person.id);

  // Iterate over each property of the person object
  Object.values(person).forEach((value, i) => {
    // Create a new table cell
    if (i == 0) {
    } else {
      const cell = document.createElement("td");
      cell.textContent = Array.isArray(value) ? value.join(", ") : value;

      // Append the cell to the row
      row.appendChild(cell);
    }
  });
  const actionCell = document.createElement("td");
  actionCell.innerHTML =
    '<button class="btn btn-danger deletePerson";><i class="fa-solid fa-trash"></i></button>';
  row.appendChild(actionCell);
  // Append the row to the table body
  const tableBody = document.getElementById("personTableBody");
  tableBody.appendChild(row);
}

// Call the fetchPeopleList function when the content is displayed
document.getElementById("content").style.display = "block";
fetchPeopleList();

function filterPersons(searchTerm) {
  // Make an API request to retrieve the filtered person data
  fetch(
    baseURL + `people/searchByFilter?filter=${encodeURIComponent(searchTerm)}`,
    {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
      },
    }
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
searchButton.addEventListener("click", async () => {
  const searchInput = document.getElementById("search-input");
  const searchTerm = searchInput.value.trim();

  // Clear the search input
  searchInput.value = "";

  // Filter the person data based on the search term
  if (searchTerm == "") await fetchPeopleList();
  else filterPersons(searchTerm);
});

function clearTable() {
  const tableBody = document.getElementById("personTableBody");
  while (tableBody.firstChild) {
    tableBody.removeChild(tableBody.firstChild);
  }
}

async function deletePerson(id) {
  fetch(baseURL + "people/deletePerson", {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
    },
    body: JSON.stringify(id),
  })
    .then(function (response) {
      if (response.ok) {
        // Handle the successful response, such as showing a success message or updating the table
        console.log("Person deleted successfully");
      } else {
        // Handle the error response, such as displaying an error message
        console.log(response);
        throw new Error("Error deleting person!");
      }
    })
    .then(async (data) => {
      await fetchPeopleList();
    })
    .catch(function (error) {
      console.error("Error deleting person!", error);
    });
}
