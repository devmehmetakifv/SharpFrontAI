// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const areas = ['header', 'body', 'sidebar', 'footer'];
const prompts = new Map();

for (const area of areas) {
    document.getElementById(area).addEventListener('click', function (event) {
        if (!event.target.closest('#dynamicInput')) {
            const inputGroup = createInputField(area);
            const existingInput = document.getElementById('dynamicInput');
            if (existingInput) {
                existingInput.remove();
            }
            this.appendChild(inputGroup);
        }
    }, { capture: true }); // Add the capture option to the event listener
}

function createInputField(area) {
    const inputGroup = document.createElement('div');
    inputGroup.classList.add('input-group');
    inputGroup.id = 'dynamicInput';

    const inputField = document.createElement('input');
    inputField.type = 'text';
    inputField.classList.add('form-control', 'border-3', 'border-white');
    inputField.placeholder = "Enter your prompt...";
    inputField.setAttribute('aria-label', "Prompt input field");

    const saveButton = document.createElement('button');
    saveButton.classList.add('btn', 'btn-outline-secondary', 'text-white', 'border-3', 'border-white');
    saveButton.type = 'button';
    saveButton.textContent = 'Save';

    inputGroup.appendChild(inputField);
    inputGroup.appendChild(saveButton);
    inputField.focus();

    saveButton.addEventListener('click', function () {
        const prompt = inputField.value;
        prompts.set(area, prompt);
        const existingInput = document.getElementById('dynamicInput');
        if (existingInput) {
            existingInput.remove();
            const activeDiv = document.getElementById(area)
            activeDiv.style.backgroundColor = '#9c9c9c';
        }
    });

    return inputGroup;
}

function generate() {
    //ERROR HERE. NEED FIX!
    const promptData = JSON.stringify(Object.fromEntries(prompts));
    $.ajax({
        url: '/Home/ReceivePrompts',
        type: "POST",
        data: { promptModel: promptData },
        contentType: "application/json"
    });
}

