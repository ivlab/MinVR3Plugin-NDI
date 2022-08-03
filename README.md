# Template-UnityPackage

Use this template repository to start a new project that you want to use as a Unity Package.  The template is already setup with these key features:
- Follows Unity requirements and recommendations for how to structure a package that works with the Unity Package Manager.  This package can be installed / removed from a Unity project using the Package Manager, and all the pieces are in the correct place for the Package Manager UI to work (i.e., clicking the View Documentation link works, clicking View License and View Changelog works, installing any Samples included with the package works.
- Follows IV/LAB conventions for naming so all packages produced by the lab show up in the same place within the Package Manager.
- Installation instructions describe the lab's current best practices for real-only and development use.
- Includes separate C# assemblies for Runtime, Editor, and Samples so these can be included individually within projects.
- Comes pre-configured for generating a documentation website using [DocFx](https://dotnet.github.io/docfx/index.html) that, depending on where your repo is hosted, can be served on either pages.github.com or pages.github.umn.edu.


## Background on Unity Packages

You can find more instructions and lab notes on working with Unity packages in the [Making Unity Packages](https://docs.google.com/document/d/1BWo-OIJx3uG72XyvIiO-t1jVDnXKFhoxj-o5VYO5Gq0/edit?usp=sharing) document in the ivlab-drive.  Note that Unity's handling of Packages is still relatively new and has evolved a lot over the past years.  So, you will see evidence in the notes from these lab meetings of us trying to make sense of the best way for us to work with them, and some of the information may be out of date.  It is safe to assume that what you see in this repository reflects the latest thinking in the lab :)

When converting an existing Unity project to a package, the most important change is to remember that you need to put any Editor scripts in a separate assembly.  In other words, most of the scripts you would normally put in Assets/Scripts should just be copied directly to Runtime/Scripts, and that's all you need to do to convert them into a package.  However, if you have any Editor directories that you would normally store under Assets/Scripts/**/Editor, those now need to go inside Editor/Scripts, which will be in the package's Editor (not Runtime) assembly.  Packages will not compile correctly if you mix the Editor and Runtime scripts in the same assembly.  

The difference between the Samples assembly and the Runtime assembly is more of a conceptual difference.  The Runtime assembly should be like a library of code that we expect to reuse in several projects.  The Samples assembly includes one or more examples of how that library could be used.  So, generally speaking, Samples is the place where you are likely to see Unity scene files and maybe a few application-specific scripts.  And, Runtime is the place where you are likely to see lower-level scripts and prefabs that could be reused in multiple Unity scenes.


## Using the Template to Create Your Package

Before getting started, think of a name for your package. IVLab Unity package names should follow the convention "YourPackage-UnityPackage", where "YourPackage" is the name you have picked - for example, "OBJImport-UnityPackage".  

Once you have a name in mind, click the "Use this template" button in this GitHub repository, and name your new repo accordingly.

Now, clone your repository on your local computer and make the following edits.  Generally speaking, we want to replace the places where you see "Template" with "YourPackage".  These steps highlight the important areas where you need to be sure to do this:

1. In `package.json`:
    - For the "name" field, replace "edu.umn.cs.ivlab.template" with "edu.umn.cs.ivlab.yourpackage".
    - For the "displayName" field, replace "Template" with "Your Package"
    - If your package requires a different version of Unity, feel free to edit that field to point to the minimal version of Unity that is supported.
    - For the "author" field, you can switch the contact email to be your own if you like, but please leave the author and url as the default IV/LAB ones so that all the lab's software will show up under the same heading in Unity's Package Manager.
    - Finally, fill in the "description" field with something useful.
2. Rename `Runtime/IVLab.Template.Runtime.asmdef` and its `.meta` file to match your package name (only change the `Template` part, but you can add namespaces with extra dots if you need).  To do this with git, use ```git mv Runtime/IVLab.Template.Runtime.asmdef Runtime/IVLab.YourPackage.Runtime.asmdef```
3. Open the new `Runtime/IVLab.YourPackage.Runtime.asmdef` file in a text editor and edit the assembly `name` to match the asmdef file name.
4. Repeat steps 2-3 for Editor/IVLab.Template.Editor.asmdef and Samples~/IVLab.Template.Samples.asmdef.  These two assemblies "depend upon" the Runtime assembly. So, when you open them in your text editor, in addition to changing the name field, also update the references field with the new name of your Runtime assembly.
5. Divide the scripts and other assets you want to include in your package between the Runtime, Editor, and Samples~ directories, using the guidelines described above for what should go where.  You can remove or rename and edit the placeholder files in these directories, like ExampleClass.cs, if you want.  It's a good idea to make sure all your code is defined within a namespace, like IVLab.YourPackage.  The template includes an example of using a IVLab.Template namespace in  [ExampleClass.cs](Runtime/Scripts/ExampleClass.cs) and an override to document the namespace in [DocumentationSrc~/apidocs-overrides/IVLab.Template.md](DocumentationSrc~/apidocs-overrides/IVLab.Template.md).  You can rename this file using `git mv` to add some documentation for your namespace if you wish.
6. Before committing your package to github, start a new Unity project and just move the entire package repo folder into the new project's Assets folder.  Open or switch to Unity and let it load and compile the scripts in your package.   This serves two purposes: (1) You can make sure there are no errors in your scripts.  (2) This forces Unity to generate .meta files for all of the files in your package.   *When you push your package up to github, you WILL want to add the .meta files that Unity automatically generates to your github commit.*  You do not see those files in this template already because they are supposed to hold a unique id, and that id will not be unique if we put it into a template that gets copied to create all of our various packages!
7. If there are no errors and you can see that meta files have been generated, save all the changes you have made and push them to your package repo on github.  These commands should work for this:
  ```
  cd MyProject/Assets/MyPackage (or similar to get the root of your package)
  git add -A
  git commit -m "modified template to adapt to this project and added meta files"
  git push
  ```
8. At this point, you are ready for your first test of installing the package via the Unity package manager.  Create another new Unity project to make sure you have a fresh start.  Then, follow the instructions in [README_INSTALL.md](README_INSTALL.md) to try installing the package the same way you will ask your users to.
9. When you are ready to turn to writing some documentation for your package, follow the instructions in [README_INSTALL.md](README_INSTALL.md) to work with your package in "edit mode".  Then, follow the instructions in [README_DOCUMENTATION.md](README_DOCUMENTATION.md).  You will need to change a few more instances of "Template" to "YourProject" inside the DocumentationSrc~ directory and setup github pages to host your site.
    
Additional Notes: 
- After you get your package working and are ready to share it with others, you will probably want to delete the instructions in this file and replace them with your own readme info.  You may wish to keep the instructions in [README_INSTALL.md](README_INSTALL.md) and [README_DOCUMENTATION.md](README_DOCUMENTATION.md) as guides for users and developers.
- If you do not want to include the documentation, editor assembly, or samples assembly in your package, you can safely remove those directories from your git repo.  They can be challenging to setup, so it is useful to default to including them in the template, but, technically, the only required part of a Unity package is the Runtime assembly.
- The template includes placeholder [LICENSE.md](LICENSE.md) and [CHANGELOG.md](CHANGELOG.md) files that are loaded when you click the "View Licences" and "View Changelog" links in Unity's Package Manager.  
    - The one-line placeholder license is really a copyright statement that the UMN Office of Tech Commercialization (OTC) recommends as a default when you are not sure if/how you would like to license the code.  Before changing this and releasing the code publicly, please discuss this with Dan so that we can follow the latest recommendations on open source licensing from the OTC.
    - Maintaining a changelog in addition to the git history may be overkill for small packages used only within the lab, but, even if it is not used, it is nice to have a placeholder for this and the license to avoid an error when clicking the "View changelog" link that Unity enables for every package.


## What's Up with the ~s in the Directory Names?
The tilde tells Unity to treat each folder as a [Hidden Asset (scroll to bottom of the page)](https://docs.unity3d.com/Manual/SpecialFolders.html).  By convention, Unity expects documentation for packages to be placed in a directory named `Documentation~` and samples to be placed in a directory named `Samples~`.  We follow the same convention for the source files used to build the documentation, which are in `DocumentationSrc~`.  These directories are visible if you navigate to them using a file browser, but they are hidden in the Project Explorer window in the Unity Editor.  (A bit confusing, since the Project Explorer looks exactly like a file browser.)  Apparently, Unity does not want you to view these files through the Project Explorer; they want you to view the Documentation for packages by selecting the package in the Package Manager and clicking the View Documentation link.  And, they want you to install samples from the package in your project by using the checkboxes under Samples in the Package Manager.  One advantage of this approach is that Unity does not generate .meta files for hidden assets, and it is helpful to not have to worry about .meta files when building the documentation website.

*Tip: * Be careful when working with these funky directory names in the unix shell. You need to escape the tilda character by writing `\~` instead of `~`. So, to change directories into the `Documentation~` folder, you would type `cd Documentation\~` or, in most unix shells, you will be able to start typing `cd Do` and press tab to auto-complete the directory name and let it handle escaping the tilde for you.

## Modifying This Template Project

- Be careful not to commit any .meta files to the original template repository used by the whole lab sice as soon as the template is used more than once, the "unique id" in each .meta file will no longer be unique, making it impossible to include two packages generated from this template in the same project.
