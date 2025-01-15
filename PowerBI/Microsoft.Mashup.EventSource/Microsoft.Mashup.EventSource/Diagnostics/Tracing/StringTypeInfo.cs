using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000064 RID: 100
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo<string>
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.CountedUtf16String, format));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CA05 File Offset: 0x0000AC05
		public override void WriteData(TraceLoggingDataCollector collector, ref string value)
		{
			collector.AddBinary(value);
		}
	}
}
