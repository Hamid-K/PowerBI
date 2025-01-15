using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000057 RID: 87
	internal sealed class IntPtrArrayTypeInfo : TraceLoggingTypeInfo<IntPtr[]>
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000C77A File Offset: 0x0000A97A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C78E File Offset: 0x0000A98E
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
