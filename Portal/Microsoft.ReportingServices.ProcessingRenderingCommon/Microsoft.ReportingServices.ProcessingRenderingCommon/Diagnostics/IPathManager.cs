using System;
using System.Collections.Specialized;
using System.Text;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004D RID: 77
	public interface IPathManager
	{
		// Token: 0x06000240 RID: 576
		string RelativePathToAbsolutePath(string relativePath, string reportPath);

		// Token: 0x06000241 RID: 577
		bool IsSupportedUrl(string path, bool checkProtocol, out bool isInternal);

		// Token: 0x06000242 RID: 578
		string EnsureReportNamePath(string reportNamePath);

		// Token: 0x06000243 RID: 579
		StringBuilder ConstructUrlBuilder(IPathTranslator pathTranslator, string serverVirtualFolderUrl, string itemPath, bool alreadyEscaped, bool addItemPathAsQuery, bool forceAddItemPathAsQuery);

		// Token: 0x06000244 RID: 580
		void ExtractFromUrl(string url, out string path, out NameValueCollection queryParameters);
	}
}
