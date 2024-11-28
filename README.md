This is a redistribution of the existing Eureka Helper Plugin. This is essentially the same plugin, however maintained and distributed by me instead of Snowy.

In the short term I will be maintaining the plugin as is with feature work being secondary to maintenance. This may change at any time, however once Snowy returns I intend to return all maintainer status to them.

## Installation

To use the plugin, please install the custom repository with the following link:

https://raw.githubusercontent.com/KangasZ/DalamudPluginRepository/main/plugin_repository.json

Please do not include my projects in other custom repos like Aku API.

## Description
EurekaHelper is a plugin for [XIVLauncher](https://goatcorp.github.io/). It's a handy plugin that enhances your Eureka gameplay with an In-Game Eureka Tracker and convenient quality-of-life features.

EurekaHelper allows you to effortlessly join or create an [Eureka Tracker](https://ffxiv-eureka.com/) instance within the game. It also provides a user-friendly graphical user interface (GUI) that closely resembles the website's interface.

To access the main window, simply type any of the following commands: `/eurekahelper`, `/ehelper`, or `/eh`. All available commands are listed in the "About" tab.  

Leave a star ⭐ on this repository if you have enjoyed using the plugin!

## Commands
Following is a list of all available commands for the plugin.
| Command | Description |
|:-------:|-------------|
| `/ehelper` or `/eurekahelper` or `/eh` | Opens the main window |
| `/etrackers` | Attempts to get a list of public trackers for the current instance in the same datacenter |
| `/arisu` | Display next weather and time for Crab, Cassie & Skoll<br>![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/0b8d6af7-cf68-40c2-972c-dd194dd43c2a) |
| `/erelic` | Opens the [Relic Window](#relic-window) which will allow you to track your Eureka relic progression |
| `/ealarms` | Opens the [Alarms Window](#alarms-window). You will be able to set custom alarms for weather/time in here! |

## Features
### FFXIV Eureka Tracker GUI
The main window of the plugin which displays the in-game FFXIV Eureka Tracker. You can manage the tracker from this window.
| Not Connected to Tracker | Connected To Tracker |
|:-------:|----------------------|
| ![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/b3b3ef48-407c-4cd6-be35-6f421e5a5b14) | ![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/1ff96e27-5216-4059-8648-38845dbf0943) |

### Elementals Manager
Manage all seen Elementals from this tab! All known Elementals position in game are listed [here](https://github.com/snooooowy/EurekaHelper/issues/13).  
Feel free to add new Elemental positions in the link above or you can DM me on Discord (@snorux)  
![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/5655c0f7-7df5-44ba-846c-58f87d542429)

### Instance Tracker
Want to know which Eureka instance you are in? Want to instance hop? You can use this feature to determine the current Eureka instance ID.  
⚠️ **Do read the disclaimer before usage.** ⚠️  
![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/0a321213-5bd0-47c2-8727-21ef97c98ca2)

### Relic Helper
Keep track of your completed Eureka Relics using this feature. The window also shows the number of items required for each relic stage!  
![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/36b6f09c-596b-48fb-a978-ae912b94efe8)

### Alarms Manager
Interested in farming money NMs (e.g Skoll, Crab, Cassie) or lockboxes? You can set an alarm for weather/time conditions using this feature.  
You will receive an in-game notification whenever the Alarm triggers.  
![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/ae7e7c57-6ac2-4848-bf59-9991eaa57867)  
![image](https://github.com/snooooowy/EurekaHelper/assets/34697265/64460d46-f3ff-4ba9-b960-26533f3cb494)

### Cutomizable Configurations
A highly customizable configuration for the main [Eureka Tracker](#ffxiv-eureka-tracker-gui) window.  
![image](https://user-images.githubusercontent.com/34697265/235935187-97466b2a-7d35-485d-aee0-23f5da3d0955.png)

## Known Issues
As of current, if you have `Payload Options` set to `Click to shout` and have `Chat2` plugin installed, the game will freeze once you click on the payload.  
An issue has been made and you can keep track of it [here](https://github.com/ascclemens/plugin-issues/issues/60).