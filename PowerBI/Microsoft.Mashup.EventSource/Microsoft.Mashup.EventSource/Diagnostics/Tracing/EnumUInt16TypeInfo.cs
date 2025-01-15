using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005F RID: 95
	internal sealed class EnumUInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000C8DA File Offset: 0x0000AADA
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C8EA File Offset: 0x0000AAEA
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ushort>.Cast<EnumType>(value));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		public override object GetData(object value)
		{
			return (ushort)value;
		}
	}
}
