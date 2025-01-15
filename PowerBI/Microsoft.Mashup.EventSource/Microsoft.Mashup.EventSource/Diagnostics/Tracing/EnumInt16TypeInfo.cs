using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005E RID: 94
	internal sealed class EnumInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000C8A2 File Offset: 0x0000AAA2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C8B2 File Offset: 0x0000AAB2
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<short>.Cast<EnumType>(value));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C8C5 File Offset: 0x0000AAC5
		public override object GetData(object value)
		{
			return (short)value;
		}
	}
}
