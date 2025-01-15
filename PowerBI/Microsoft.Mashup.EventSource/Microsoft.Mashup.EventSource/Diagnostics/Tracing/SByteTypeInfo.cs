using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000042 RID: 66
	internal sealed class SByteTypeInfo : TraceLoggingTypeInfo<sbyte>
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000C431 File Offset: 0x0000A631
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C441 File Offset: 0x0000A641
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte value)
		{
			collector.AddScalar(value);
		}
	}
}
