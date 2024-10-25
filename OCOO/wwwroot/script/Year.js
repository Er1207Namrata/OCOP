const selectYear = document.getElementById('year');
const currentYear = new Date().getFullYear();
const startYear = '2023'; // Change this value to set the starting year
const endYear = currentYear + 10;   // Change this value to set the ending year

for (let year = startYear; year <= endYear; year++) {
    const option = document.createElement('option');
    option.value = year;
    option.text = year;
    selectYear.appendChild(option);

    if (year === currentYear) {
        option.selected = true;
    }
}
