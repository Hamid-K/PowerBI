using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000048 RID: 72
	internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000C4FE File Offset: 0x0000A6FE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C50F File Offset: 0x0000A70F
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
		{
			collector.AddScalar(value);
		}
	}
}
