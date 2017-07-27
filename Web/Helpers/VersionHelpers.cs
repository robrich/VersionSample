namespace VersionSample.Web.Helpers {
    using System.Web.Mvc;
    using VersionSample.Web.Repositories;

    public static class VersionHelpers {
		private static readonly AssemblyInfoRepository assemblyInfoRepository = new AssemblyInfoRepository();
        
		public static string GitHash(this HtmlHelper _) => assemblyInfoRepository.GitHash();
        public static string BuildDate(this HtmlHelper _) => assemblyInfoRepository.BuildDate();
        public static string MachineName(this HtmlHelper _) => assemblyInfoRepository.MachineName();
    }
}
