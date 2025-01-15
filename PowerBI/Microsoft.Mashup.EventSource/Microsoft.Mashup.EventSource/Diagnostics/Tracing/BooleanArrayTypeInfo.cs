using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200004E RID: 78
	internal sealed class BooleanArrayTypeInfo : TraceLoggingTypeInfo<bool[]>
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000C5D9 File Offset: 0x0000A7D9
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C5ED File Offset: 0x0000A7ED
		public override void WriteData(TraceLoggingDataCollector collector, ref bool[] value)
		{
			collector.AddArray(value);
		}
	}
}
