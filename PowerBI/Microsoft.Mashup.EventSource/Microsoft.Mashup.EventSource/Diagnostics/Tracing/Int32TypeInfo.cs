using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000045 RID: 69
	internal sealed class Int32TypeInfo : TraceLoggingTypeInfo<int>
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000C497 File Offset: 0x0000A697
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		public override void WriteData(TraceLoggingDataCollector collector, ref int value)
		{
			collector.AddScalar(value);
		}
	}
}
