using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005D RID: 93
	internal sealed class EnumSByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000C86A File Offset: 0x0000AA6A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C87A File Offset: 0x0000AA7A
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<sbyte>.Cast<EnumType>(value));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C88D File Offset: 0x0000AA8D
		public override object GetData(object value)
		{
			return (sbyte)value;
		}
	}
}
