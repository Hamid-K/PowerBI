using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000047 RID: 71
	internal sealed class Int64TypeInfo : TraceLoggingTypeInfo<long>
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000C4DB File Offset: 0x0000A6DB
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		public override void WriteData(TraceLoggingDataCollector collector, ref long value)
		{
			collector.AddScalar(value);
		}
	}
}
