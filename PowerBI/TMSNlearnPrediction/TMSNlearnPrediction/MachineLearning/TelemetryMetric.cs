using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning
{
	// Token: 0x02000147 RID: 327
	public sealed class TelemetryMetric : TelemetryMessage
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0002318C File Offset: 0x0002138C
		public TelemetryMetric(string name, double value, IDictionary<string, string> properties = null)
		{
			this.Name = name;
			this.Value = value;
			this.Properties = properties;
		}

		// Token: 0x04000360 RID: 864
		public readonly string Name;

		// Token: 0x04000361 RID: 865
		public readonly double Value;

		// Token: 0x04000362 RID: 866
		public readonly IDictionary<string, string> Properties;
	}
}
