using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001260 RID: 4704
	internal abstract class ArrayListValue : BufferedListValue
	{
		// Token: 0x06007BF3 RID: 31731 RVA: 0x001AAE0C File Offset: 0x001A900C
		public override Value[] ToArray()
		{
			Value[] array = new Value[this.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this[i];
			}
			return array;
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x001AAE3E File Offset: 0x001A903E
		public override RecordValue ToRecord(Keys keys)
		{
			return RecordValue.New(keys, this.ToArray());
		}
	}
}
