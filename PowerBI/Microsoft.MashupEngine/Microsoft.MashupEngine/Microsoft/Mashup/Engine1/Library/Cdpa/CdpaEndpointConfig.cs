using System;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA6 RID: 3494
	public abstract class CdpaEndpointConfig
	{
		// Token: 0x06005F24 RID: 24356 RVA: 0x00148130 File Offset: 0x00146330
		public static bool TryGetEndpointConfig(string url, out CdpaEndpointConfig endpointConfig)
		{
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeHttps)
			{
				string text = uri.GetLeftPart(UriPartial.Authority).Replace(Uri.UriSchemeHttps + "://", string.Empty);
				foreach (CdpaEndpointConfig cdpaEndpointConfig in CdpaEndpointConfig.cdpaSettings)
				{
					if (cdpaEndpointConfig.Matches(text))
					{
						endpointConfig = cdpaEndpointConfig;
						return true;
					}
				}
			}
			endpointConfig = null;
			return false;
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001481A4 File Offset: 0x001463A4
		protected CdpaEndpointConfig(string resourceUri, OAuthSettings aadSettings)
		{
			this.resourceUri = resourceUri;
			this.aadSettings = aadSettings;
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06005F26 RID: 24358 RVA: 0x001481BA File Offset: 0x001463BA
		public string ResourceUri
		{
			get
			{
				return this.resourceUri;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x001481C2 File Offset: 0x001463C2
		public OAuthSettings AadSettings
		{
			get
			{
				return this.aadSettings;
			}
		}

		// Token: 0x06005F28 RID: 24360
		public abstract bool Matches(string host);

		// Token: 0x0400342E RID: 13358
		private const string schemeSeparator = "://";

		// Token: 0x0400342F RID: 13359
		private static readonly CdpaEndpointConfig[] cdpaSettings = new CdpaEndpointConfig[]
		{
			new CdpaEndpointConfig.SubdomainCdpaEndpointConfig(".pi.dynamics.com", "cd34d57a-a3ef-48b1-b84b-9686f0f7c099", Microsoft.Mashup.OAuth.AadSettings.CommonSettings),
			new CdpaEndpointConfig.ExactDomainCdpaEndpointConfig("api.ei.trafficmanager.net", "cd34d57a-a3ef-48b1-b84b-9686f0f7c099", Microsoft.Mashup.OAuth.AadSettings.CommonSettings),
			new CdpaEndpointConfig.ExactDomainCdpaEndpointConfig("api.int.ei.trafficmanager.net", "cd34d57a-a3ef-48b1-b84b-9686f0f7c099", Microsoft.Mashup.OAuth.AadSettings.CommonSettings)
		};

		// Token: 0x04003430 RID: 13360
		private readonly string resourceUri;

		// Token: 0x04003431 RID: 13361
		private readonly OAuthSettings aadSettings;

		// Token: 0x02000DA7 RID: 3495
		private class SubdomainCdpaEndpointConfig : CdpaEndpointConfig
		{
			// Token: 0x06005F2A RID: 24362 RVA: 0x00148229 File Offset: 0x00146429
			public SubdomainCdpaEndpointConfig(string host, string resourceUri, OAuthSettings aadSettings)
				: base(resourceUri, aadSettings)
			{
				this.host = host;
			}

			// Token: 0x06005F2B RID: 24363 RVA: 0x0014823A File Offset: 0x0014643A
			public override bool Matches(string host)
			{
				return host.EndsWith(this.host, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04003432 RID: 13362
			private readonly string host;
		}

		// Token: 0x02000DA8 RID: 3496
		private class ExactDomainCdpaEndpointConfig : CdpaEndpointConfig
		{
			// Token: 0x06005F2C RID: 24364 RVA: 0x00148249 File Offset: 0x00146449
			public ExactDomainCdpaEndpointConfig(string host, string resourceUri, OAuthSettings aadSettings)
				: base(resourceUri, aadSettings)
			{
				this.host = host;
			}

			// Token: 0x06005F2D RID: 24365 RVA: 0x0014825A File Offset: 0x0014645A
			public override bool Matches(string host)
			{
				return host.Equals(this.host, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04003433 RID: 13363
			private readonly string host;
		}
	}
}
