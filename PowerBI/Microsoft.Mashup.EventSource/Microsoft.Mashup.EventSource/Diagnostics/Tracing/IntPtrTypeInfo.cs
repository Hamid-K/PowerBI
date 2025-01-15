using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000049 RID: 73
	internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000C521 File Offset: 0x0000A721
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000C535 File Offset: 0x0000A735
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
