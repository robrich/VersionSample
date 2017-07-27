namespace VersionSample.Web.Repositories {
    using System;
    using System.Linq;
    using System.Reflection;

    public class AssemblyInfoRepository {
        private static string gitHashPrivate = null;
        private static string buildDatePrivate = null;

        private static readonly object lockObject = new object();
        private static bool valuesSet = false;

        // FRAGILE: static because they're baked in at compile time and then never change
        // FRAGILE: ASSUME: these values are the same for all dlls in this solution
        private static void GetValues() {
            if (!valuesSet) {
                lock (lockObject) {
                    if (!valuesSet) {
                        Assembly assembly = Assembly.GetExecutingAssembly();

                        // Git hash is in AssemblyInformationalVersion -- build puts it there
                        AssemblyInformationalVersionAttribute desc = (
                            from a in assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                            select a as AssemblyInformationalVersionAttribute
                        ).FirstOrDefault();
                        if (desc != null) {
                            gitHashPrivate = (desc.InformationalVersion ?? "");
                        }

                        Version ver = assembly.GetName().Version;

                        try {
                            // http://channel9.msdn.com/forums/Coffeehouse/255737-AssemblyInfo-Version-Numbers/
                            int build = ver.Build;
                            int rev = ver.Revision;
                            DateTime buildDate = new DateTime(2000, 1, 1);
                            buildDate = buildDate.AddDays(build);
                            buildDate = buildDate.AddSeconds(rev * 2);
                            buildDatePrivate = buildDate.ToString("G");
                        } catch (Exception) {
                            // TODO: log, it wasn't a valid build date, did they write a custom version number?
                            buildDatePrivate = null;
                        }
                        valuesSet = true;
                    }
                }
            }
        }

        public string GitHash() {
            GetValues();
            return gitHashPrivate;
        }

        public string BuildDate() {
            GetValues();
            return buildDatePrivate;
        }

        public string MachineName() {
            return Environment.MachineName;
        }

    }
}
