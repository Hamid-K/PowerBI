using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200004A RID: 74
	internal sealed class UIntPtrTypeInfo : TraceLoggingTypeInfo<UIntPtr>
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000C547 File Offset: 0x0000A747
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C55B File Offset: 0x0000A75B
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
