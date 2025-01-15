using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000040 RID: 64
	internal sealed class BooleanTypeInfo : TraceLoggingTypeInfo<bool>
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		public override void WriteData(TraceLoggingDataCollector collector, ref bool value)
		{
			collector.AddScalar(value);
		}
	}
}
