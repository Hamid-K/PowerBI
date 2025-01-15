using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000065 RID: 101
	internal sealed class GuidTypeInfo : TraceLoggingTypeInfo<Guid>
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000CA17 File Offset: 0x0000AC17
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid value)
		{
			collector.AddScalar(value);
		}
	}
}
