# IVLab-Template-UnityPackage

This is the Dome Stream / NDI Unity package, given to the IV/LAB by Evans and Sutherland. **This package contains proprietary code, do not distribute!!!**

Refer to the [usage guide](./Usage.pdf) for details on how to use the project, except skip the import steps and instead use the Installation steps below.

## Installation in a Unity Project

### Non-development (read-only) Package use
1. In Unity, open Window -> Package Manager. 
2. Click the ```+``` button
3. Select ```Add package from git URL```
4. Paste ```git@github.umn.edu:ivlab-cs/DomeStreamES-UnityPackage.git``` for the latest package

### Development use in a git-managed project
1. Navigate your terminal or Git tool into your version-controlled Unity project's main folder. 
2. Add this repository as a submodule: ```cd Packages; git submodule add git@github.umn.edu:ivlab-cs/DomeStreamES-UnityPackage.git; git submodule update --init --recursive```
3. See https://git-scm.com/book/en/v2/Git-Tools-Submodules for more details on working with Submodules. 

### Development use in a non git-managed project
1. Navigate your terminal or Git tool into your non version-controlled Unity project's main folder. 
2. Clone this repository into the Assets folder: ```cd Packages; git clone git@github.umn.edu:ivlab-cs/DomeStreamES-UnityPackage.git```