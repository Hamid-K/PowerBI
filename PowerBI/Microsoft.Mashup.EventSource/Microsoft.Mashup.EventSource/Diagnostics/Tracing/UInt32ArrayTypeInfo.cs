using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000054 RID: 84
	internal sealed class UInt32ArrayTypeInfo : TraceLoggingTypeInfo<uint[]>
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000C712 File Offset: 0x0000A912
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C722 File Offset: 0x0000A922
		public override void WriteData(TraceLoggingDataCollector collector, ref uint[] value)
		{
			collector.AddArray(value);
		}
	}
}
