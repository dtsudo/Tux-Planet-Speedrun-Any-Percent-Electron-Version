const { app, BrowserWindow, Menu } = require('electron')
const path = require('path')

Menu.setApplicationMenu(null);

const createWindow = () => {
	let win = new BrowserWindow({
		width: 1000,
		height: 700,
		useContentSize: true,
		minWidth: 25,
		minHeight: 25,
		title: "Tux Planet Speedrun Any%",
		icon: path.join(__dirname, "icon128x128.png")
	});
	
	win.loadFile("TuxPlanetSpeedrunAnyPercent.html");
};

app.whenReady().then(() => {
	createWindow();
});
