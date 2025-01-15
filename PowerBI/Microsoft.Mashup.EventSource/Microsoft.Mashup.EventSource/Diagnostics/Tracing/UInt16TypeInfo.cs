using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000044 RID: 68
	internal sealed class UInt16TypeInfo : TraceLoggingTypeInfo<ushort>
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000C475 File Offset: 0x0000A675
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000C485 File Offset: 0x0000A685
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort value)
		{
			collector.AddScalar(value);
		}
	}
}
