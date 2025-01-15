using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE6 RID: 3558
	[DataContract]
	internal class EventCountCdpaMetricMeasure : CdpaMetricMeasure
	{
		// Token: 0x06006021 RID: 24609 RVA: 0x00149544 File Offset: 0x00147744
		public override CdpaMetricMeasure Union(CdpaMetricMeasure other)
		{
			EventCountCdpaMetricMeasure eventCountCdpaMetricMeasure = other as EventCountCdpaMetricMeasure;
			if (eventCountCdpaMetricMeasure != null && eventCountCdpaMetricMeasure.Operations.Count == 1 && eventCountCdpaMetricMeasure.Operations[0] is CdpaEventCountOperation)
			{
				return this;
			}
			throw new NotSupportedException();
		}
	}
}
