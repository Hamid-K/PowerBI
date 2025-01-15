using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200006C RID: 108
	internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000CC75 File Offset: 0x0000AE75
		public NullableTypeInfo(List<Type> recursionCheck)
		{
			this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
		{
			bool flag = value != null;
			collector.AddScalar(flag);
			T t = (flag ? value.Value : default(T));
			this.valueInfo.WriteData(collector, ref t);
		}

		// Token: 0x04000101 RID: 257
		private readonly TraceLoggingTypeInfo<T> valueInfo;
	}
}
