using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000050 RID: 80
	internal sealed class SByteArrayTypeInfo : TraceLoggingTypeInfo<sbyte[]>
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000C68A File Offset: 0x0000A88A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C69A File Offset: 0x0000A89A
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte[] value)
		{
			collector.AddArray(value);
		}
	}
}
