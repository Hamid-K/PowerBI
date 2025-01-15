using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000046 RID: 70
	internal sealed class UInt32TypeInfo : TraceLoggingTypeInfo<uint>
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
		public override void WriteData(TraceLoggingDataCollector collector, ref uint value)
		{
			collector.AddScalar(value);
		}
	}
}
