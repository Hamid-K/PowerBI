using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000074 RID: 116
	internal abstract class TraceLoggingTypeInfo<DataType> : TraceLoggingTypeInfo
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000E2BE File Offset: 0x0000C4BE
		protected TraceLoggingTypeInfo()
			: base(typeof(DataType))
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		protected TraceLoggingTypeInfo(string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
			: base(typeof(DataType), name, level, opcode, keywords, tags)
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000E2E9 File Offset: 0x0000C4E9
		public static TraceLoggingTypeInfo<DataType> Instance
		{
			get
			{
				return TraceLoggingTypeInfo<DataType>.instance ?? TraceLoggingTypeInfo<DataType>.InitInstance();
			}
		}

		// Token: 0x060002DE RID: 734
		public abstract void WriteData(TraceLoggingDataCollector collector, ref DataType value);

		// Token: 0x060002DF RID: 735 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		public override void WriteObjectData(TraceLoggingDataCollector collector, object value)
		{
			DataType dataType = ((value == null) ? default(DataType) : ((DataType)((object)value)));
			this.WriteData(collector, ref dataType);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E328 File Offset: 0x0000C528
		internal static TraceLoggingTypeInfo<DataType> GetInstance(List<Type> recursionCheck)
		{
			if (TraceLoggingTypeInfo<DataType>.instance == null)
			{
				int count = recursionCheck.Count;
				TraceLoggingTypeInfo<DataType> traceLoggingTypeInfo = Statics.CreateDefaultTypeInfo<DataType>(recursionCheck);
				Interlocked.CompareExchange<TraceLoggingTypeInfo<DataType>>(ref TraceLoggingTypeInfo<DataType>.instance, traceLoggingTypeInfo, null);
				recursionCheck.RemoveRange(count, recursionCheck.Count - count);
			}
			return TraceLoggingTypeInfo<DataType>.instance;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E36B File Offset: 0x0000C56B
		private static TraceLoggingTypeInfo<DataType> InitInstance()
		{
			return TraceLoggingTypeInfo<DataType>.GetInstance(new List<Type>());
		}

		// Token: 0x0400014E RID: 334
		private static TraceLoggingTypeInfo<DataType> instance;
	}
}
