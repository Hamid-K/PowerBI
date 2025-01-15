using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF0 RID: 3824
	internal class AzureBlobsService
	{
		// Token: 0x0600656D RID: 25965 RVA: 0x0015C1B0 File Offset: 0x0015A3B0
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
				uri = new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}.blob.core.windows.net", @string.ToLowerInvariant()));
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			if (containerName != null)
			{
				uriBuilder.Path = containerName.String.ToLowerInvariant() + "/";
			}
			return TextValue.New(uriBuilder.Uri.ToString());
		}

		// Token: 0x04003792 RID: 14226
		private const string BaseUri = "https://{0}.blob.core.windows.net";
	}
}
