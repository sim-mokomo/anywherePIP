import {BrowserView, BrowserWindow} from "electron"
import { app } from "electron"

void app.whenReady().then(() => {
	const window = new BrowserWindow({ titleBarStyle: "hiddenInset"})

	void window.loadFile("src/index.html")

	app.on("window-all-closed", () => {
		app.quit()
	})
})
