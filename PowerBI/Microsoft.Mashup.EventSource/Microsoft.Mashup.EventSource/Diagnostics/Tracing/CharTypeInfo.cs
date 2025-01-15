using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200004D RID: 77
	internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000C5C7 File Offset: 0x0000A7C7
		public override void WriteData(TraceLoggingDataCollector collector, ref char value)
		{
			collector.AddScalar(value);
		}
	}
}
