using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BC RID: 188
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class MetricData : Domain, ISerializableWithWriter
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00016D4C File Offset: 0x00014F4C
		public MetricData DeepClone()
		{
			MetricData metricData = new MetricData();
			metricData.ver = this.ver;
			foreach (DataPoint dataPoint in this.metrics)
			{
				metricData.metrics.Add(dataPoint.DeepClone());
			}
			Utils.CopyDictionary<string>(this.properties, metricData.properties);
			return metricData;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016DC8 File Offset: 0x00014FC8
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("metrics", this.metrics.ToList<ISerializableWithWriter>());
			serializationWriter.WriteProperty("properties", this.properties);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00016E07 File Offset: 0x00015007
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00016E0F File Offset: 0x0001500F
		public int ver { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00016E18 File Offset: 0x00015018
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00016E20 File Offset: 0x00015020
		public IList<DataPoint> metrics { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00016E29 File Offset: 0x00015029
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00016E31 File Offset: 0x00015031
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x0600060E RID: 1550 RVA: 0x00016E3A File Offset: 0x0001503A
		public MetricData()
			: this("AI.MetricData", "MetricData")
		{
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00016E4C File Offset: 0x0001504C
		protected MetricData(string fullName, string name)
		{
			this.ver = 2;
			this.metrics = new List<DataPoint>();
			this.properties = new ConcurrentDictionary<string, string>();
		}
	}
}
