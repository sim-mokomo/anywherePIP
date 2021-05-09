import {contextBridge, ipcRenderer} from "electron"

contextBridge.exposeInMainWorld(
    "api", {
        SearchUrlRequest: (url:string) => {
            ipcRenderer.send("search-url-request", url)
        },
        CloseWindowRequest: () => {
            ipcRenderer.send("close-window-request")
        },
        MinimiseWindowRequest: () => {
            ipcRenderer.send("minimise-window-request")
        },
        MaximiseWindowRequest: () => {
            ipcRenderer.send("maximize-window-request")
        }
    }
);