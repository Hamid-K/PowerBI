using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003F RID: 63
	internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000C3D2 File Offset: 0x0000A5D2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C3DE File Offset: 0x0000A5DE
		public override object GetData(object value)
		{
			return null;
		}
	}
}
