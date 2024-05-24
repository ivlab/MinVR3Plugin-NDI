
8/3/22:
Instead of needing to manually edit the manifest.json file as described below, I added the files the NDI library needs to access directly to this project.  They are:
Runtime/ThirdParty/Plugin/System.*
If these cause trouble on any platforms, you could try to delete them manually and then follow the instructions below.


To resolve this error:
```
Library/PackageCache/edu.umn.cs.ivlab.minvr3.ndi@bd6c27564b/Runtime/ThirdParty/Interop/Find.cs(26,12): error CS0246: The type or namespace name 'Span<>' could not be found (are you missing a using directive or an assembly reference?)
```

You need to add this to your Packages/manifest.json file:

```
{
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": [
        "org.nuget"
      ]
    }
  ],
  
  "dependencies": {
    "org.nuget.system.memory": "4.5.3"
  }
}
```

# MinVR3Plugin-NDI
This plugin interfaces with the open source Klak implementation of the NDI network video streaming protocol.  This makes it possible to stream Unity textures, including renderbuffers, across the network.  We have used this in the Bell Museum's planetarium to render from a Unity app running on one machine to their planetarium display software running on another machine.

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
