using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000041 RID: 65
	internal sealed class ByteTypeInfo : TraceLoggingTypeInfo<byte>
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x0000C40F File Offset: 0x0000A60F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C41F File Offset: 0x0000A61F
		public override void WriteData(TraceLoggingDataCollector collector, ref byte value)
		{
			collector.AddScalar(value);
		}
	}
}
