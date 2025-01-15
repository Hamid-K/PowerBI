using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000027 RID: 39
	internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000AD1A File Offset: 0x00008F1A
		public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000AD29 File Offset: 0x00008F29
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AD48 File Offset: 0x00008F48
		public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				num2 = value.Length;
				for (int i = 0; i < value.Length; i++)
				{
					this.elementInfo.WriteData(collector, ref value[i]);
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AD94 File Offset: 0x00008F94
		public override object GetData(object value)
		{
			ElementType[] array = (ElementType[])value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array[i]);
			}
			return array2;
		}

		// Token: 0x040000B0 RID: 176
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
