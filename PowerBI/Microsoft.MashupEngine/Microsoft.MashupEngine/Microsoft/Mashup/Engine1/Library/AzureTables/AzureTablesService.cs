using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureTables
{
	// Token: 0x02000EBC RID: 3772
	internal static class AzureTablesService
	{
		// Token: 0x06006432 RID: 25650 RVA: 0x00156BD8 File Offset: 0x00154DD8
		public static TextValue GetHttpUri(TextValue accountName, TextValue containerName = null)
		{
			string @string = accountName.String;
			Uri uri;
			if (@string.Contains("."))
			{
				uri = new Uri(@string);
			}
			else
			{
				uri = new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}.table.core.windows.net", @string.ToLowerInvariant()));
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			if (containerName != null)
			{
				uriBuilder.Path = containerName.String.ToLowerInvariant();
			}
			return TextValue.New(uriBuilder.Uri.ToString());
		}

		// Token: 0x040036B5 RID: 14005
		private const string BaseUri = "https://{0}.table.core.windows.net";
	}
}
