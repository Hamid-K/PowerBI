using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000056 RID: 86
	internal sealed class UInt64ArrayTypeInfo : TraceLoggingTypeInfo<ulong[]>
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000C757 File Offset: 0x0000A957
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C768 File Offset: 0x0000A968
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong[] value)
		{
			collector.AddArray(value);
		}
	}
}
