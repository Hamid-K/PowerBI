using System;
using System.Collections.Specialized;
using System.Web;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A4 RID: 420
	public static class ExtendedUri
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x000257E7 File Offset: 0x000239E7
		public static string AppendRelativeUri([NotNull] string left, [NotNull] string right)
		{
			return left.TrimEnd(new char[] { '/' }) + "/" + right.TrimStart(new char[] { '/' });
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00025818 File Offset: 0x00023A18
		public static void SetQueryParameter(this UriBuilder uriBuilder, string name, string value)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<UriBuilder>(uriBuilder, "uriBuilder");
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			nameValueCollection.Set(name, value);
			uriBuilder.Query = nameValueCollection.ToString();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00025850 File Offset: 0x00023A50
		public static string GetQueryParameter(this UriBuilder uriBuilder, string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<UriBuilder>(uriBuilder, "uriBuilder");
			return HttpUtility.ParseQueryString(uriBuilder.Query).Get(name);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00025870 File Offset: 0x00023A70
		public static void RemoveQueryParameter(this UriBuilder uriBuilder, string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<UriBuilder>(uriBuilder, "uriBuilder");
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			nameValueCollection.Remove(name);
			uriBuilder.Query = nameValueCollection.ToString();
		}
	}
}
