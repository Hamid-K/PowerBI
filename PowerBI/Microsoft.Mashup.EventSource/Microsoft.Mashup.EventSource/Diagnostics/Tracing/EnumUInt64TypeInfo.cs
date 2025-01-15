using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000063 RID: 99
	internal sealed class EnumUInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000C9BB File Offset: 0x0000ABBB
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ulong>.Cast<EnumType>(value));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C9DF File Offset: 0x0000ABDF
		public override object GetData(object value)
		{
			return (ulong)value;
		}
	}
}
