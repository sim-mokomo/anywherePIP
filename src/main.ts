import {BrowserView, BrowserWindow, ipcMain} from "electron"
import { app } from "electron"
import path from "path";

void app.whenReady().then(() => {
	const window = new BrowserWindow(
		{
			width: 400,
			height: 300,
			frame: false,
			webPreferences : {
				preload: path.join(__dirname, "index-preload.js")
			}
		})

	void window.loadFile("src/index.html")
	window.setAlwaysOnTop(true)

	const view = new BrowserView()
	window.setBrowserView(view)
	const windowBounds = window.getBounds()
	view.setBounds({ x: 0, y: 40, width: windowBounds.width, height: windowBounds.height})
	window.on("resized", () => {
		const windowBounds = window.getBounds()
		view.setBounds({ x: 0, y: 40, width: windowBounds.width, height: windowBounds.height})
	})

	void view.webContents.loadURL('https://www.youtube.com/')

	app.on("window-all-closed", () => {
		app.quit()
	})

	ipcMain.on("search-url-request", (event, url) => {
		void view.webContents.loadURL(url)
	})

	ipcMain.on("close-window-request", (event, _) => {
		window.close()
	})

	ipcMain.on("minimise-window-request", (event, _) => {
		window.minimize()
	})

	ipcMain.on("maximize-window-request", (event, _) => {
		window.maximize()
	})
})
