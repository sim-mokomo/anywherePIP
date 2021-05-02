import { BrowserWindow } from "electron"
import { app } from "electron"

void app.whenReady().then(() => {
	const rootWindow = new BrowserWindow({})

	void rootWindow.loadFile("src/index.html")

	app.on("window-all-closed", () => {
		app.quit()
	})
})
