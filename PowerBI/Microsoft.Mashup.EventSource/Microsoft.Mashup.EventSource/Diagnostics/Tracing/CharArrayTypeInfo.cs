using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000059 RID: 89
	internal sealed class CharArrayTypeInfo : TraceLoggingTypeInfo<char[]>
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		public override void WriteData(TraceLoggingDataCollector collector, ref char[] value)
		{
			collector.AddArray(value);
		}
	}
}
