Version Sample
==============

Git hash into assembly on build, read it back onto the site


Features of this sample
-----------------------

1. `build.bat` will build and publish the site to the `dist` directory.  One step in the build is to grab the git hash and put it into the [`AssemblyInformationalVersion`](https://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin) attribute.

2. `AssemblyInfoRepository.cs` reads the currently running `AssemblyInformationalVersion`.

3. `Helpers/VersionHelpers.cs` is HTML helpers that views can use to render these details.

4. `_Layout.cshtml` shows the git hash.  When debugging in Visual Studio, it just says "GITHASH".


About AssemblyInformationalVersion
----------------------------------

Unlike the AssemblyVersion attribute, the [`AssemblyInformationalVersion`](https://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin) attribute doesn't need to be numbers.  Right-click on a dll, choose properties, and switch to the version tab, and you'll see both the version number and this informational version.


About the Version attribute
---------------------------

If you set the version to `1.0.*`, the build will seed the version based on the build date.  `AssemblyInfoRepository.cs` pulls this version, and infers the build date.  This is also displayed on the site.


Best Practices
--------------

1. Consider carefully the security implications of showing this data on your site.

2. Bake critical details like this into the small text in the footer of your site.  When a customer sends a screenshot of your website, grab the git hash of the build they're using, `git checkout` that version, and now you're using exactly the version of code they did.  Repro faster.

3. If publishing to NuGet or Octopus Deploy, you'll need a real [Semantic Version](http://semver.org/) like `1.2.3`, not `1.0.6416.38401` like the auto-incrementing version.  Consider grabbing the CI's build number or another specifically incrementing value, and setting the `Version` attribute.


License
-------

MIT
