using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000060 RID: 96
	internal sealed class EnumInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000C912 File Offset: 0x0000AB12
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C922 File Offset: 0x0000AB22
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<int>.Cast<EnumType>(value));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C935 File Offset: 0x0000AB35
		public override object GetData(object value)
		{
			return (int)value;
		}
	}
}
