class DataTable {
    constructor(dataOrigin, context) {
        this.dataOrigin = dataOrigin;
        this.context = context;
    }

    fillContext() {
        fetch(this.dataOrigin)
        .then(response => {
            return response.json();
        })
        .then(data => {
            let htmlContent = '';

            let headerContent = '<thead><tr><th> Id</th><th>Quantity</th><th>Name</th><th>Price</th></tr></thead>'
            let row = '';
            for (const object of data) {
                for (const key in object) {
                    row += `<td>${object[key]}</td>`
                }
                htmlContent += `<tr> ${row} </tr>`;
                row = '';
            }
            htmlContent = `${headerContent}<tbody>${htmlContent}</tbody>`;
            this.context.innerHTML = htmlContent;
        })
        .catch(error => {
            console.log(error);
        })
    }
}