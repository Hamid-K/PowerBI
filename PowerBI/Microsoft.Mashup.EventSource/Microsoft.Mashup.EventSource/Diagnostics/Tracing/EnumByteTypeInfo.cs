using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005C RID: 92
	internal sealed class EnumByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000C832 File Offset: 0x0000AA32
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C842 File Offset: 0x0000AA42
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<byte>.Cast<EnumType>(value));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000C855 File Offset: 0x0000AA55
		public override object GetData(object value)
		{
			return (byte)value;
		}
	}
}
