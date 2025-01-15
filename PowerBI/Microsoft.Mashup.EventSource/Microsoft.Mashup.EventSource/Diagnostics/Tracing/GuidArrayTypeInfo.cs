using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000066 RID: 102
	internal sealed class GuidArrayTypeInfo : TraceLoggingTypeInfo<Guid[]>
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000CA3E File Offset: 0x0000AC3E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CA4F File Offset: 0x0000AC4F
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid[] value)
		{
			collector.AddArray(value);
		}
	}
}
