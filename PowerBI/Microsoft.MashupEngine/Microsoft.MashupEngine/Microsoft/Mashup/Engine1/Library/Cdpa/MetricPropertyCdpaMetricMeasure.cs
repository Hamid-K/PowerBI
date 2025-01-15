using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF4 RID: 3572
	[DataContract]
	internal class MetricPropertyCdpaMetricMeasure : CdpaMetricMeasure
	{
		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06006046 RID: 24646 RVA: 0x0014967D File Offset: 0x0014787D
		// (set) Token: 0x06006047 RID: 24647 RVA: 0x00149685 File Offset: 0x00147885
		[DataMember(Name = "propertyName", IsRequired = true)]
		public string PropertyName { get; set; }

		// Token: 0x06006048 RID: 24648 RVA: 0x00149690 File Offset: 0x00147890
		public override CdpaMetricMeasure Union(CdpaMetricMeasure other)
		{
			MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = other as MetricPropertyCdpaMetricMeasure;
			if (metricPropertyCdpaMetricMeasure != null && this.PropertyName == metricPropertyCdpaMetricMeasure.PropertyName)
			{
				MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure2 = new MetricPropertyCdpaMetricMeasure
				{
					PropertyName = this.PropertyName
				};
				Dictionary<string, CdpaOperation> dictionary = new Dictionary<string, CdpaOperation>();
				foreach (CdpaOperation cdpaOperation in base.Operations)
				{
					dictionary[cdpaOperation.Name] = cdpaOperation;
				}
				foreach (CdpaOperation cdpaOperation2 in metricPropertyCdpaMetricMeasure.Operations)
				{
					dictionary[cdpaOperation2.Name] = cdpaOperation2;
				}
				metricPropertyCdpaMetricMeasure2.Operations = dictionary.Values.ToArray<CdpaOperation>();
				return metricPropertyCdpaMetricMeasure2;
			}
			throw new NotSupportedException();
		}
	}
}
