using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.PowerBI.Explore.Resources
{
	// Token: 0x02000002 RID: 2
	public class ExploreResources
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Stream TryGetResourceStream(string relativeUrl)
		{
			bool flag = string.IsNullOrEmpty(relativeUrl);
			Stream stream;
			if (flag)
			{
				stream = null;
			}
			else
			{
				relativeUrl = ExploreResources.ReconstructRelativeUrl(relativeUrl);
				relativeUrl = ExploreResources.assembly.GetManifestResourceNames().FirstOrDefault((string p) => p.ToLowerInvariant() == relativeUrl.ToLowerInvariant());
				bool flag2 = relativeUrl == null;
				if (flag2)
				{
					stream = null;
				}
				else
				{
					try
					{
						stream = ExploreResources.assembly.GetManifestResourceStream(relativeUrl);
					}
					catch (FileNotFoundException)
					{
						stream = null;
					}
				}
			}
			return stream;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		private static string ReconstructRelativeUrl(string relativeUrl)
		{
			bool flag = string.IsNullOrEmpty(relativeUrl) || !ExploreResources.stringModifers.Any((string r) => Regex.Match(relativeUrl, r).Success);
			string text;
			if (flag)
			{
				text = relativeUrl;
			}
			else
			{
				ExploreResources.stringModifers.ForEach(delegate(string r)
				{
					relativeUrl = Regex.Replace(relativeUrl, r, string.Empty);
				});
				text = relativeUrl;
			}
			return text;
		}

		// Token: 0x04000001 RID: 1
		private static readonly Assembly assembly = typeof(ExploreResources).Assembly;

		// Token: 0x04000002 RID: 2
		private static readonly List<string> stringModifers = new List<string> { "[.][.]*/", "\\?.*" };
	}
}
