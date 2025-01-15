using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000058 RID: 88
	internal sealed class UIntPtrArrayTypeInfo : TraceLoggingTypeInfo<UIntPtr[]>
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
