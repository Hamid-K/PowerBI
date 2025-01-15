using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000A RID: 10
	public class PathMatcher : IPathMatcher
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000026A4 File Offset: 0x000008A4
		public PathMatcher(string regexPath)
		{
			if (regexPath == null)
			{
				throw new ArgumentNullException("path");
			}
			string text = string.Format("^({0})", regexPath);
			this._urlRegex = new Regex(text, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026E2 File Offset: 0x000008E2
		public virtual bool IsMatch(string url, List<KeyValuePair<string, string[]>> queryParams)
		{
			return this._urlRegex.IsMatch(url);
		}

		// Token: 0x04000036 RID: 54
		private const string BaseRegex = "^({0})";

		// Token: 0x04000037 RID: 55
		private readonly Regex _urlRegex;
	}
}
