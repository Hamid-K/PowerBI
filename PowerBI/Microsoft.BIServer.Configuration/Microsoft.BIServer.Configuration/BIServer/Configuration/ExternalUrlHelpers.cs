using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000006 RID: 6
	public static class ExternalUrlHelpers
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021FC File Offset: 0x000003FC
		public static Uri CalculateUrl(Application application, string externalReportServerUrl, string externalUrlRoot, int secureConnectionLevel)
		{
			if (application == null || application.URLs.Count<URL>() == 0 || string.IsNullOrWhiteSpace(application.VirtualDirectory))
			{
				return null;
			}
			if (!string.IsNullOrWhiteSpace(externalReportServerUrl))
			{
				return new UriBuilder(externalReportServerUrl)
				{
					Path = application.VirtualDirectory
				}.Uri;
			}
			if (!string.IsNullOrWhiteSpace(externalUrlRoot))
			{
				return new UriBuilder(externalUrlRoot)
				{
					Path = application.VirtualDirectory
				}.Uri;
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			string text8 = null;
			URL[] urls = application.URLs;
			for (int i = 0; i < urls.Length; i++)
			{
				string text9;
				string text10;
				string text11;
				string text12;
				string text13;
				ExternalUrlHelpers.ParseUrlPrefix(urls[i].UrlString, out text9, out text10, out text11, out text12, out text13);
				bool flag = string.CompareOrdinal(text9, Uri.UriSchemeHttps) == 0;
				IPAddress ipaddress;
				if (string.CompareOrdinal(text10, "*") == 0 || string.CompareOrdinal(text10, "+") == 0)
				{
					string text14 = Environment.MachineName + ":" + text11;
					if (flag)
					{
						if (text6 == null)
						{
							text6 = text14;
						}
					}
					else if (text3 == null)
					{
						text3 = text14;
					}
				}
				else if (IPAddress.TryParse(text10, out ipaddress))
				{
					string text15 = text10 + ":" + text11;
					if (flag)
					{
						if (text7 == null)
						{
							text7 = text15;
						}
					}
					else if (text4 == null)
					{
						text4 = text15;
					}
				}
				else if (text5 == null || text8 == null)
				{
					string text16 = text10 + ":" + text11;
					if (flag)
					{
						if (text8 == null)
						{
							text8 = text16;
						}
					}
					else if (text5 == null)
					{
						text5 = text16;
					}
				}
			}
			if (text6 != null)
			{
				text2 = text6;
			}
			else if (text7 != null)
			{
				text2 = text7;
			}
			else if (text8 != null)
			{
				text2 = text8;
			}
			if (text3 != null)
			{
				text = text3;
			}
			else if (text4 != null)
			{
				text = text4;
			}
			else if (text5 != null)
			{
				text = text5;
			}
			string text17;
			string text18;
			if (secureConnectionLevel > 0)
			{
				if (text2 != null)
				{
					text17 = text2;
					text18 = Uri.UriSchemeHttps;
				}
				else
				{
					text17 = text;
					text18 = Uri.UriSchemeHttp;
				}
			}
			else if (text != null)
			{
				text17 = text;
				text18 = Uri.UriSchemeHttp;
			}
			else
			{
				text17 = text2;
				text18 = Uri.UriSchemeHttps;
			}
			if (text17 == null)
			{
				return null;
			}
			if (text18 == null)
			{
				return null;
			}
			return new Uri(text18 + Uri.SchemeDelimiter + text17 + application.VirtualDirectory);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023FC File Offset: 0x000005FC
		private static bool ParseUrlPrefix(string url, out string scheme, out string host, out string port, out string prefix, out string path)
		{
			scheme = null;
			prefix = null;
			path = null;
			host = null;
			port = null;
			Match match = Regex.Match(url, "^(?<prefix>(?<scheme>.+)://(?<host>[^/]+):(?<port>[0-9]+))/?(?<path>.*)");
			if (!match.Success)
			{
				return false;
			}
			scheme = match.Groups["scheme"].Value;
			prefix = match.Groups["prefix"].Value;
			path = match.Groups["path"].Value;
			host = match.Groups["host"].Value;
			port = match.Groups["port"].Value;
			return true;
		}
	}
}
