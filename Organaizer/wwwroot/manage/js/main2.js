let deletebutton = document.querySelectorAll(".delete-button")

deletebutton.forEach(btn => btn.addEventListener("click", (e) => {
    e.preventDefault()
    let url = btn.getAttribute("href")
    Swal.fire({
        title: 'Əminsiniz?',
        text: "Bunu geri qaytara bilməyəcəksiniz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        window.location.reload(true)
                    }
                    else {

                        alert("Not found")
                    }
                }
            )
        }
    })
}))
