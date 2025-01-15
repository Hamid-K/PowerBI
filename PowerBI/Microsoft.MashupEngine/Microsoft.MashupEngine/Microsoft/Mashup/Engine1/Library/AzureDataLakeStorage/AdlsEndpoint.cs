using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ED0 RID: 3792
	internal sealed class AdlsEndpoint
	{
		// Token: 0x060064BD RID: 25789 RVA: 0x00158DC1 File Offset: 0x00156FC1
		private AdlsEndpoint(Uri uri)
		{
			this.uri = uri;
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x00158DD0 File Offset: 0x00156FD0
		public string BaseEndpoint
		{
			get
			{
				return this.uri.GetLeftPart(UriPartial.Authority);
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x00158DE0 File Offset: 0x00156FE0
		public string FileSystem
		{
			get
			{
				string text = this.uri.AbsolutePath.Trim(new char[] { '/' });
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				int num = text.IndexOf('/');
				return Uri.UnescapeDataString((num >= 0) ? text.Substring(0, num) : text);
			}
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x00158E30 File Offset: 0x00157030
		public string FileSystemEndpoint
		{
			get
			{
				if (this.FileSystem == null)
				{
					return null;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", this.BaseEndpoint, this.FileSystem);
			}
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x00158E57 File Offset: 0x00157057
		public string AbsoluteUri
		{
			get
			{
				return this.uri.AbsoluteUri;
			}
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x00158E64 File Offset: 0x00157064
		public string Directory
		{
			get
			{
				string text = this.uri.AbsolutePath.Trim(new char[] { '/' });
				if (!string.IsNullOrEmpty(text) && text.Contains('/'))
				{
					int num = text.IndexOf('/');
					return Uri.UnescapeDataString(text.Substring(num + 1));
				}
				return null;
			}
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x00158EB8 File Offset: 0x001570B8
		public static string GetFolderUrl(List<AdlsEndpoint> adlsEndpoints)
		{
			if (adlsEndpoints.Count != 1)
			{
				return adlsEndpoints[0].BaseEndpoint;
			}
			return adlsEndpoints[0].AbsoluteUri;
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x00158EDC File Offset: 0x001570DC
		public static bool TryCreateWithFileSystem(TextValue endpointTextValue, out AdlsEndpoint obj)
		{
			return AdlsEndpoint.TryCreateWithFileSystem(endpointTextValue.String, out obj);
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x00158EEC File Offset: 0x001570EC
		public static bool TryCreateWithFileSystem(string endpoint, out AdlsEndpoint result)
		{
			result = null;
			Uri uri;
			if (Uri.TryCreate(endpoint, UriKind.Absolute, out uri))
			{
				result = new AdlsEndpoint(uri);
				if (result.FileSystemEndpoint == null)
				{
					result = null;
				}
			}
			return result != null;
		}

		// Token: 0x040036FD RID: 14077
		private const char separator = '/';

		// Token: 0x040036FE RID: 14078
		private readonly Uri uri;
	}
}
