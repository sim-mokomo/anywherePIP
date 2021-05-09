window.onload = () => {
    const closeButtonDOM = document.getElementById("close-button")
    if(closeButtonDOM == null){
        return
    }

    closeButtonDOM.addEventListener("click", () => {
        window.api.CloseWindowRequest()
    })

    const minimiseButtonDOM = document.getElementById("minimise-button")
    if(minimiseButtonDOM == null){
        return;
    }
    minimiseButtonDOM.addEventListener("click", ()=> {
        window.api.MinimiseWindowRequest()
    })

    const maximiseButtonDOM = document.getElementById("maximise-button")
    if(maximiseButtonDOM == null){
        return;
    }

    maximiseButtonDOM.addEventListener("click", () => {
        window.api.MaximiseWindowRequest()
    })

    const searchConfirmButtonDOM = document.getElementById("search-url-submit-button")
    if(searchConfirmButtonDOM == null){
        return
    }

    searchConfirmButtonDOM.addEventListener("click", () => {
        const inputfieldDOM: HTMLInputElement = <HTMLInputElement>(
            document.getElementById("search-url-input-field")
        )
        if (inputfieldDOM == null) {
            return
        }
        window.api.SearchUrlRequest(inputfieldDOM.value)
        inputfieldDOM.value = ""
    })
}