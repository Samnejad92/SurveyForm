// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function displayContent(event, contentNameID) {
    event.preventDefault()
    let content =
        document.getElementsByClassName("contentClass");
    let totalCount = content.length;

    for (let count = 0;
        count < totalCount; count++) {
        content[count].style.display = "none";
    }

    let links =
        document.getElementsByClassName("linkClass");
    totalLinks = links.length;

    for (let count = 0;
        count < totalLinks; count++) {
        links[count].classList.remove("active");
    }

    document.getElementById(contentNameID)
        .style.display = "block";

    event.currentTarget.classList.add("active");
}
$("input:checkbox").on('click', function () {
    validateFormFields()
    // in the handler, 'this' refers to the box clicked on
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
})
$("input:radio").on('click', function () {
    validateFormFields()
})
//$("#select-unit").on('change', function () {
//    validateFormFields()
//})
function copyContent() {
    var content1 = document.getElementById('1').innerHTML;
    document.getElementById('hiddenInput-1').value = content1;
    
    var content2 = document.getElementById('2').innerHTML;
    document.getElementById('hiddenInput-2').value = content2;

    var content3 = document.getElementById('3').innerHTML;
    document.getElementById('hiddenInput-3').value = content3;

    var content4 = document.getElementById('4').innerHTML;
    document.getElementById('hiddenInput-4').value = content4;

    var content5 = document.getElementById('5').innerHTML;
    document.getElementById('hiddenInput-5').value = content5;
    return true; // Allow form submission
}

$('.accordion-button').click(function () {
    // $(this).next().slideToggle(300);
    // $('.accordion-button').not($(this).next()).slideUp(300);
    $(this).toggleClass('active');
    $('.accordion-button').not($(this)).removeClass('active');
});

new TomSelect("#select-unit", {
    create: false,
    sortField: {
        field: "text",
        direction: "asc"
    }
});

$(document).ready(function () {
    document.querySelector("ul>li>button").classList.add("active")
    document.querySelector("#tabsDiv>div").style.display = "block"
    $("#surveyForm").on("submit", function (event) {
        event.preventDefault();
        processFormSubmission()
    });
});
function validateFormFields() {
    let isValid = true;
    $(".error").remove();
    if (document.getElementById('select-unit').value == '-1') {
        isValid = false;
        $("div.ts-wrapper").after("&emsp;<span class='error'>وارد کردن این فیلد ضروری است.</span>");
    }
    if (!$("input[name='sex']:checked").val()) {
        isValid = false;
        $("h6:contains('جنسیت')").before("<span class='error'>لطفا جنسیت را انتخاب کنید.</span>");
    }
    if ($("input[name='f[1][]']:checked").length === 0) {
        isValid = false;
        $("h6:contains('سن:')").before("<span class='error'>لطفا سن خود را انتخاب نمایید.</span>");
    }
    if ($("input[name='fo[1][]']:checked").length === 0) {
        isValid = false;
        $("h6:contains('سابقه کار')").before("<span class='error'>لطفا میزان سابقه کار خود را انتخاب کنید.</span>");
    }
    if ($("input[name='foo[1][]']:checked").length === 0) {
        isValid = false;
        $("h6:contains('تحصیلات')").before("<span class='error'>لطفا سطح تحصیلات خود را انتخاب کنید.</span>");
    }
    if ($("input[name='foob[1][]']:checked").length === 0) {
        isValid = false;
        $("h6:contains('پست سازمانی')").before("<span class='error'>لطفا پست سازمانی خود را انتخاب نمایید.</span>");
    }
    return isValid;
}
function validateQuestions() {
    let isValid2 = true;
    let errorMessage = '';
    const questions = document.querySelectorAll('tbody tr');

    questions.forEach((question) => {
        const checkboxes = question.querySelectorAll('input[type="checkbox"]');
        const isChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);
        const row = question.closest('tr');

        if (!isChecked) {
            isValid2 = false;
            errorMessage = `لطفا موارد مشخص گردیده با رنگ قرمز را پاسخ دهید.`;
            if (row) {
                const headers = row.querySelectorAll('th');
                headers.forEach(header => {
                    header.style.color = '#ff5959';
                });
            }
        } else {
            if (row) {
                const headers = row.querySelectorAll('th');
                headers.forEach(header => {
                    header.style.color = '';
                });
            }
        }
    });
    return { isValid2, errorMessage };
}
function processFormSubmission() {
    const isValid = validateFormFields();
    const { isValid2, errorMessage } = validateQuestions();
    const errorDiv = document.getElementById('error-message');

    if (!isValid && isValid2) {
        errorDiv.innerHTML = errorMessage;
    } else if (isValid && isValid2) {
        errorDiv.innerHTML = '';
        alert('ذخیره اطلاعات...');
        document.getElementById("surveyForm").submit();
    }
    console.log(isValid);
    console.log(isValid2);
}

function initializeCheckboxListeners() {
    const questions = document.querySelectorAll('tbody tr')

    questions.forEach(question => {
        const checkboxes = question.querySelectorAll('input[type="checkbox"]')

        checkboxes.forEach(checkbox => {
            checkbox.addEventListener('change', validateQuestions)
        });
    });
}

document.addEventListener('DOMContentLoaded', initializeCheckboxListeners);
