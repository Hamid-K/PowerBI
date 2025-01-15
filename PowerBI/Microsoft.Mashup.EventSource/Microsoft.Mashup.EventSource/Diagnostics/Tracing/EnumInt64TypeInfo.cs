using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000062 RID: 98
	internal sealed class EnumInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000C982 File Offset: 0x0000AB82
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C993 File Offset: 0x0000AB93
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<long>.Cast<EnumType>(value));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C9A6 File Offset: 0x0000ABA6
		public override object GetData(object value)
		{
			return (long)value;
		}
	}
}
