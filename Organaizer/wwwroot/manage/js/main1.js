let deletebtn = document.querySelectorAll(".delete-image-org")

deletebtn.forEach(btn => btn.addEventListener("click", function (e) {

    btn.parentElement.remove()

}))