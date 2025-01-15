using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200004C RID: 76
	internal sealed class SingleTypeInfo : TraceLoggingTypeInfo<float>
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000C590 File Offset: 0x0000A790
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		public override void WriteData(TraceLoggingDataCollector collector, ref float value)
		{
			collector.AddScalar(value);
		}
	}
}
