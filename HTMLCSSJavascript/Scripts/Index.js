const firstNameEl = document.getElementById("firstName");
const lastNameEl = document.getElementById("lastName");

firstNameEl.textContent = "Filipe";
lastNameEl.textContent = "Silva";

document.getElementById("btnAge").addEventListener("click", () => {
    const input = prompt("Please enter you age.")
    const age = parseInt(input)
    alert(age >= 16 ? "Can drive" : "Can not drive");
})

function useFor() {
    let msg = "";

    for (let i = 0; i < 10; i++) {
        msg += i
    }

    console.log(msg);
}

function useWhile() {
    let msg = "";
    let i = 0;

    while (i < 10) {
        msg += i;
        i++;
    }

    console.log(msg);
}

function useDoWhile() {
    let msg = "";
    let i = 0;

    do {
        msg += i;
        i++;
    } while (i < 10)

    console.log(msg);
}

function useForInArray() {
    let msg = "";
    const array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    for (let i in array) {
        msg += i;
    }

    console.log(msg);
}

function useForInObject() {
    const ob = { a: 1, b: 2 }

    for (let property in ob) {
        let value = ob[property];
        console.log(`${property}: ${value}`);
    }
}

useFor();
useWhile();
useDoWhile();
useForInArray();
useForInObject();