using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E0A RID: 3594
	[DataContract]
	internal class CdpaSeriesRequest : CdpaRequestWithTimeConfigurationAndResponseProtocol
	{
		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x0014A391 File Offset: 0x00148591
		// (set) Token: 0x060060DB RID: 24795 RVA: 0x0014A399 File Offset: 0x00148599
		[DataMember(Name = "metric", IsRequired = true)]
		public CdpaMetricConfiguration Metric { get; set; }

		// Token: 0x060060DC RID: 24796 RVA: 0x0014A3A4 File Offset: 0x001485A4
		public override CdpaRequestWithTimeConfigurationAndResponseProtocol ShallowCopy()
		{
			return new CdpaSeriesRequest
			{
				Metric = this.Metric,
				Granularity = base.Granularity,
				TimeRange = base.TimeRange,
				ResponseFormat = base.ResponseFormat,
				ResponseProtocol = base.ResponseProtocol,
				ExtraParameters = base.ExtraParameters
			};
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x0014A3FE File Offset: 0x001485FE
		public CdpaSeriesRequest Filter(CdpaPropertyFilterOrGroup filter)
		{
			CdpaSeriesRequest cdpaSeriesRequest = (CdpaSeriesRequest)this.ShallowCopy();
			cdpaSeriesRequest.Metric = this.Metric.Filter(filter);
			return cdpaSeriesRequest;
		}
	}
}
