using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F80 RID: 3968
	internal abstract class AdobeAnalyticsRequest
	{
		// Token: 0x06006888 RID: 26760
		protected abstract Value CreateContent();

		// Token: 0x06006889 RID: 26761
		protected abstract string CreateCacheKey();

		// Token: 0x0600688A RID: 26762
		protected abstract RecordValue CreateHeaders();

		// Token: 0x0600688B RID: 26763 RVA: 0x00167652 File Offset: 0x00165852
		protected AdobeAnalyticsRequest(string baseUri, string route, string query = null)
		{
			this.baseUri = baseUri;
			this.route = route;
			this.query = query;
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x0600688C RID: 26764 RVA: 0x0016767C File Offset: 0x0016587C
		public BinaryValue Content
		{
			get
			{
				if (!this.contentInitialized)
				{
					Value value = this.CreateContent();
					if (value != null)
					{
						this.content = JsonModule.Json.FromValue.Invoke(value).AsBinary;
					}
					this.contentInitialized = true;
				}
				return this.content;
			}
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x0600688D RID: 26765 RVA: 0x001676BE File Offset: 0x001658BE
		public string CacheKey
		{
			get
			{
				if (this.cacheKey == null)
				{
					this.cacheKey = this.CreateCacheKey();
				}
				return this.cacheKey;
			}
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x001676DA File Offset: 0x001658DA
		public RecordValue Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = this.CreateHeaders();
				}
				return this.headers;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x0600688F RID: 26767 RVA: 0x001676F8 File Offset: 0x001658F8
		public Uri Uri
		{
			get
			{
				if (this.uri == null)
				{
					UriBuilder uriBuilder = new UriBuilder(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", this.baseUri, this.route));
					if (!string.IsNullOrEmpty(this.query))
					{
						uriBuilder.Query = this.query;
					}
					this.uri = uriBuilder.Uri;
				}
				return this.uri;
			}
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0016775F File Offset: 0x0016595F
		public static AdobeAnalyticsRequest NewDiscoveryRequest(string clientId)
		{
			return new AdobeAnalyticsRequest.AdobeAnalyticsDiscoveryRequest(clientId);
		}

		// Token: 0x0400398D RID: 14733
		private bool contentInitialized;

		// Token: 0x0400398E RID: 14734
		private BinaryValue content;

		// Token: 0x0400398F RID: 14735
		private string cacheKey;

		// Token: 0x04003990 RID: 14736
		private RecordValue headers;

		// Token: 0x04003991 RID: 14737
		protected readonly string baseUri;

		// Token: 0x04003992 RID: 14738
		protected readonly string route;

		// Token: 0x04003993 RID: 14739
		protected readonly string query;

		// Token: 0x04003994 RID: 14740
		private Uri uri;

		// Token: 0x04003995 RID: 14741
		protected readonly PersistentCacheKey persistentCacheKey = PersistentCacheKey.AdobeAnalytics;

		// Token: 0x02000F81 RID: 3969
		private class AdobeAnalyticsDiscoveryRequest : AdobeAnalyticsRequest
		{
			// Token: 0x06006891 RID: 26769 RVA: 0x00167767 File Offset: 0x00165967
			public AdobeAnalyticsDiscoveryRequest(string clientId)
				: base("https://analytics.adobe.io", "discovery/me", null)
			{
				this.clientId = clientId;
			}

			// Token: 0x06006892 RID: 26770 RVA: 0x00167781 File Offset: 0x00165981
			protected override RecordValue CreateHeaders()
			{
				return RecordValue.New(Keys.New("x-api-key"), new Value[] { TextValue.New(this.clientId) });
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x001677A6 File Offset: 0x001659A6
			protected override string CreateCacheKey()
			{
				return "{}";
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x000020FA File Offset: 0x000002FA
			protected override Value CreateContent()
			{
				return null;
			}

			// Token: 0x04003996 RID: 14742
			private readonly string clientId;
		}
	}
}
