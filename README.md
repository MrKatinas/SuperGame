<img src="Images/GameLogo2.png" title="Logo" width="300"> 

[![Unity](https://img.shields.io/badge/Unity-2018.4.0f1-brightgreen.svg)](https://unity3d.com/get-unity/download/archive)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.TXT)

---
## **Table Of Contents**
- [Description](#Description)
- [Images of the game](#Images-Of-The-Game)
- [How To Play](#How-To-Play)
- [Authors](#Authors)

---

## **Description**

My friend and I created a simple clicker, which has special features: we did not use a single  „Update“ function and communication between created „MonoBehaviours“ was done using an event system.

---

## **Images Of The Game**
#### *Main Menu Scene*
[<img src="Images/MainMeniu.png" width="250">](Images/MainMeniu.png)
[<img src="Images/About.png" width="250">](Images/About.png)
[<img src="Images/Settings.png" width="250">](Images/Settings.png)

#### *Game Scene*
[<img src="Images/Game.png" width="250">](Images/Game.png)
[<img src="Images/Mine.gif" width="250">](Images/Mine.gif)
[<img src="Images/Shop.png" width="250">](Images/Shop.png)

---

## **How To Play**
### *PC*
 - Download "PC build" folder
 - Run SuperGame.exe
 - Press Play!
### *Android*
#### [If you have adb](https://stackoverflow.com/questions/7076240/install-an-apk-file-from-command-prompt)
- Run Command Prompt ([CMD](https://www.howtogeek.com/235101/10-ways-to-open-the-command-prompt-in-windows-10/))

- type in CMD: 
```html
    cd <path_to_adb>
    adb install <path_to_apk>

    example: 
    cd AppData\Local\Android\Sdk\platform-tools
    adb install Desktop\demo.apk

    or
    <path_to_adb>\adb install <path_to_apk>

    example: 
    AppData\Local\Android\Sdk\platform-tools\adb install Desktop\demo.apk
```

#### [Without ADB](https://stackoverflow.com/questions/9718104/how-to-install-apk-from-pc)

- Connect Android device to PC via USB cable and turn on USB storage.
- Copy .apk file to attached device's storage.
- Turn off USB storage and disconnect it from PC.
- Check the option Settings → Applications → Unknown sources OR Settings > Security > Unknown Sources.
- Open FileManager app and click on the copied .apk file. 
If you can't fine the apk file try searching or allowing hidden files. 
- It will ask you whether to install this app or not. Click Yes or OK.

---

## **Authors**

- [Monika Pociūtė](https://www.linkedin.com/in/monika-pociute/)
- [Mindaugas Butkus](https://linkedin.com/in/mindaugas-butkus)

[Back To The Top](#Table-Of-Contents)

---