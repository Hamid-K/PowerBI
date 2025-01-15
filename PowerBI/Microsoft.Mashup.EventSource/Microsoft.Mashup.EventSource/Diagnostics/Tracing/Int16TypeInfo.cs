using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000043 RID: 67
	internal sealed class Int16TypeInfo : TraceLoggingTypeInfo<short>
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000C453 File Offset: 0x0000A653
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C463 File Offset: 0x0000A663
		public override void WriteData(TraceLoggingDataCollector collector, ref short value)
		{
			collector.AddScalar(value);
		}
	}
}
