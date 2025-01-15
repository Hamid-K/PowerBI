using System;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F82 RID: 3970
	internal abstract class AdobeAnalyticsRequestBase : AdobeAnalyticsRequest
	{
		// Token: 0x06006895 RID: 26773 RVA: 0x001677AD File Offset: 0x001659AD
		protected AdobeAnalyticsRequestBase(string apiVersion, string company, string baseUri, string route, string query = null)
			: base(baseUri, route, query)
		{
			this.apiVersion = apiVersion;
			this.company = company;
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x001677C8 File Offset: 0x001659C8
		protected override string CreateCacheKey()
		{
			BinaryValue binaryValue = base.Content ?? AdobeAnalyticsRequestBase.EmptyJsonObject;
			return this.persistentCacheKey.Qualify(Encoding.Default.GetString(binaryValue.AsBytes), this.apiVersion, this.company, this.route, this.query);
		}

		// Token: 0x04003997 RID: 14743
		private static readonly BinaryValue EmptyJsonObject = BinaryValue.New(Encoding.UTF8.GetBytes("{}"));

		// Token: 0x04003998 RID: 14744
		protected readonly string apiVersion;

		// Token: 0x04003999 RID: 14745
		protected readonly string company;
	}
}
