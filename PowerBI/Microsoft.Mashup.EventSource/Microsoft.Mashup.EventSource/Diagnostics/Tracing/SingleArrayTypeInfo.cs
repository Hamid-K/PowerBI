using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005B RID: 91
	internal sealed class SingleArrayTypeInfo : TraceLoggingTypeInfo<float[]>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000C80F File Offset: 0x0000AA0F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C820 File Offset: 0x0000AA20
		public override void WriteData(TraceLoggingDataCollector collector, ref float[] value)
		{
			collector.AddArray(value);
		}
	}
}
