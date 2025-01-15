using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200002C RID: 44
	internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<ElementType>
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000B3A1 File Offset: 0x000095A1
		public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B3CC File Offset: 0x000095CC
		public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				foreach (ElementType elementType in value)
				{
					this.elementInfo.WriteData(collector, ref elementType);
					num2++;
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B444 File Offset: 0x00009644
		public override object GetData(object value)
		{
			IterableType iterableType = (IterableType)((object)value);
			List<object> list = new List<object>();
			foreach (ElementType elementType in iterableType)
			{
				list.Add(this.elementInfo.GetData(elementType));
			}
			return list.ToArray();
		}

		// Token: 0x040000BE RID: 190
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
