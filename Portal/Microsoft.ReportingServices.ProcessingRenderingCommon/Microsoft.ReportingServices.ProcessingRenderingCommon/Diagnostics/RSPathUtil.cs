using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000061 RID: 97
	public sealed class RSPathUtil : IPathManager
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000A668 File Offset: 0x00008868
		public static IPathManager GetPathManager()
		{
			return new RSPathUtil();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A670 File Offset: 0x00008870
		public static bool IsReportServerPathOrUrl(string pathOrUrl, bool checkProtocol, out bool isRelative)
		{
			if (RSPathUtil.GetPathManager().IsSupportedUrl(pathOrUrl, checkProtocol, out isRelative))
			{
				if (!isRelative)
				{
					string reportServerVirtualDirectory = ProcessingContext.Configuration.ReportServerVirtualDirectory;
					if (string.IsNullOrEmpty(reportServerVirtualDirectory))
					{
						return false;
					}
					Uri uri = new Uri(pathOrUrl);
					Uri uri2 = new Uri(reportServerVirtualDirectory);
					if (uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) != uri2.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) || string.Compare(uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped), uri2.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped), StringComparison.OrdinalIgnoreCase) != 0)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A6EB File Offset: 0x000088EB
		private RSPathUtil()
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A6F4 File Offset: 0x000088F4
		string IPathManager.RelativePathToAbsolutePath(string path, string reportPath)
		{
			if (new Uri(path, UriKind.RelativeOrAbsolute).IsAbsoluteUri)
			{
				return path;
			}
			string text;
			if (reportPath.Length == 0)
			{
				text = "/";
			}
			else
			{
				text = reportPath;
			}
			return new Uri(new Uri("c:" + text), path).GetComponents(UriComponents.Path, UriFormat.Unescaped).Substring(2);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A74C File Offset: 0x0000894C
		public bool IsSupportedUrl(string path, bool checkProtocol)
		{
			bool flag;
			return ((IPathManager)this).IsSupportedUrl(path, checkProtocol, out flag);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A764 File Offset: 0x00008964
		bool IPathManager.IsSupportedUrl(string path, bool checkProtocol, out bool isInternal)
		{
			isInternal = false;
			if (path.StartsWith("HTTP:", StringComparison.OrdinalIgnoreCase) || path.StartsWith("HTTPS:", StringComparison.OrdinalIgnoreCase) || path.StartsWith("FTP:", StringComparison.OrdinalIgnoreCase) || path.StartsWith("MAILTO:", StringComparison.OrdinalIgnoreCase) || path.StartsWith("NEWS:", StringComparison.OrdinalIgnoreCase) || path.StartsWith("FILE:", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (RSPathUtil.ContainsOtherProtocol(path))
			{
				return !checkProtocol;
			}
			isInternal = true;
			return true;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A7DD File Offset: 0x000089DD
		string IPathManager.EnsureReportNamePath(string reportNamePath)
		{
			return reportNamePath;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A7E0 File Offset: 0x000089E0
		StringBuilder IPathManager.ConstructUrlBuilder(IPathTranslator pathTranslator, string serverVirtualFolderUrl, string itemPath, bool alreadyEscaped, bool addItemPathAsQuery, bool forceAddItemPathAsQuery)
		{
			if (!alreadyEscaped)
			{
				if (string.IsNullOrEmpty(serverVirtualFolderUrl))
				{
					serverVirtualFolderUrl = "http://reportserver";
				}
				else
				{
					serverVirtualFolderUrl = new Uri(serverVirtualFolderUrl).AbsoluteUri;
				}
			}
			string text = UrlUtil.UrlEncode(itemPath);
			StringBuilder stringBuilder = new StringBuilder(serverVirtualFolderUrl);
			if (addItemPathAsQuery)
			{
				stringBuilder.Append("?");
			}
			stringBuilder.Append(text);
			return stringBuilder;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A836 File Offset: 0x00008A36
		void IPathManager.ExtractFromUrl(string url, out string path, out NameValueCollection queryParameters)
		{
			RSPathUtil.ExtractFromUrl(url, out path, out queryParameters);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A840 File Offset: 0x00008A40
		public static void ExtractFromUrl(string url, out string path, out NameValueCollection queryParameters)
		{
			path = string.Empty;
			queryParameters = null;
			Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				Uri uri2 = uri;
				uri = new Uri(RSPathUtil.m_absoluteUri, uri2);
			}
			string components = uri.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);
			if (components == null)
			{
				return;
			}
			queryParameters = HttpUtility.ParseQueryString(components);
			if (queryParameters == null)
			{
				return;
			}
			if (queryParameters[null] != null)
			{
				path = queryParameters[null];
				queryParameters.Remove(null);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A8AC File Offset: 0x00008AAC
		private static bool ContainsOtherProtocol(string path)
		{
			Uri absoluteUri = RSPathUtil.m_absoluteUri;
			return new Uri(absoluteUri, path).Scheme != absoluteUri.Scheme;
		}

		// Token: 0x04000165 RID: 357
		public static readonly RSPathUtil Instance = new RSPathUtil();

		// Token: 0x04000166 RID: 358
		private static readonly Uri m_absoluteUri = new Uri("http://q");
	}
}
