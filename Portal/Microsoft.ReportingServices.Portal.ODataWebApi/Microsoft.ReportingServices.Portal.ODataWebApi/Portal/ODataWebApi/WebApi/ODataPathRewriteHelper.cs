using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.WebApi
{
	// Token: 0x0200000A RID: 10
	internal static class ODataPathRewriteHelper
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000026AF File Offset: 0x000008AF
		public static string Rewrite(string odataPath)
		{
			odataPath = ODataPathRewriteHelper.UrlEncodeComponent(ODataPathRewriteHelper.PathComponent, odataPath, "/");
			odataPath = ODataPathRewriteHelper.UrlEncodeComponent(ODataPathRewriteHelper.SearchComponent, odataPath, string.Empty);
			return odataPath;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D8 File Offset: 0x000008D8
		private static string UrlEncodeComponent(Regex regex, string odataPath, string valueIfEmpty)
		{
			MatchCollection matchCollection = regex.Matches(odataPath);
			if (matchCollection.Count > 0)
			{
				Group group = matchCollection[0].Groups["val"];
				string text = odataPath.Substring(0, group.Index);
				string text2 = odataPath.Substring(group.Index + group.Length);
				string text3 = WebUtility.UrlEncode(WebUtility.UrlDecode((string.IsNullOrWhiteSpace(group.Value) ? valueIfEmpty : group.Value).Replace("'", "''"))).Replace("+", "%20");
				return text + text3 + text2;
			}
			return odataPath;
		}

		// Token: 0x0400003C RID: 60
		public static Regex PathComponent = new Regex("\\((?:%20)*Path(?:%20)*=(?:%20)*'(?<val>.*?)'(?:%20)*\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400003D RID: 61
		public static Regex SearchComponent = new Regex("(?<=\\(searchText=')(?<val>.*?)(?='\\))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
