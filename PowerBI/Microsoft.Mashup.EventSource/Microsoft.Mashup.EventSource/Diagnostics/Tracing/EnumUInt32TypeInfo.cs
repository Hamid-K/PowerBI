using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000061 RID: 97
	internal sealed class EnumUInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C95A File Offset: 0x0000AB5A
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<uint>.Cast<EnumType>(value));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C96D File Offset: 0x0000AB6D
		public override object GetData(object value)
		{
			return (uint)value;
		}
	}
}
