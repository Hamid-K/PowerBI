using System;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E3 RID: 227
	public interface ITelemetry
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000851 RID: 2129
		// (set) Token: 0x06000852 RID: 2130
		DateTimeOffset Timestamp { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000853 RID: 2131
		TelemetryContext Context { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000854 RID: 2132
		// (set) Token: 0x06000855 RID: 2133
		IExtension Extension { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000856 RID: 2134
		// (set) Token: 0x06000857 RID: 2135
		string Sequence { get; set; }

		// Token: 0x06000858 RID: 2136
		void Sanitize();

		// Token: 0x06000859 RID: 2137
		ITelemetry DeepClone();

		// Token: 0x0600085A RID: 2138
		void SerializeData(ISerializationWriter serializationWriter);
	}
}
