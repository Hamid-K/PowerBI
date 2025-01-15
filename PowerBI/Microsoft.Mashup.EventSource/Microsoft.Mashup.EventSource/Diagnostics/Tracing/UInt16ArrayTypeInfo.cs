using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000052 RID: 82
	internal sealed class UInt16ArrayTypeInfo : TraceLoggingTypeInfo<ushort[]>
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000C6CE File Offset: 0x0000A8CE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C6DE File Offset: 0x0000A8DE
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort[] value)
		{
			collector.AddArray(value);
		}
	}
}
