// Function to handle the "Add Person" button click
function handleAddPersonClick() {
  const personName = prompt("Enter the name of the person:");
  if (personName) {
    const newPerson = { name: personName };

    fetch("/api/people", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      },
      body: JSON.stringify(newPerson),
    })
      .then((response) => {
        if (response.ok) {
          // Refresh the people list
          fetchPeopleList();
        } else {
          throw new Error("Failed to add person.");
        }
      })
      .catch((error) => {
        console.error("Error adding person:", error);
      });
  }
}

// Event listener for the "Add Person" button click
document
  .getElementById("add-person-btn")
  .addEventListener("click", handleAddPersonClick);
