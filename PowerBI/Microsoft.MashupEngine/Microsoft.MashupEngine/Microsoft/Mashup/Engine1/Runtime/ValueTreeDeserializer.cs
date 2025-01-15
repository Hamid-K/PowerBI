using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016BD RID: 5821
	public class ValueTreeDeserializer : ValueDeserializer
	{
		// Token: 0x06009417 RID: 37911 RVA: 0x001E8E4C File Offset: 0x001E704C
		public ValueTreeDeserializer(IValueReader reader)
			: base(reader)
		{
		}

		// Token: 0x06009418 RID: 37912 RVA: 0x001E8E58 File Offset: 0x001E7058
		public static Value DeserializeValue(byte[] value)
		{
			Value value2;
			using (MemoryStream memoryStream = new MemoryStream(value))
			{
				using (StreamValueReader streamValueReader = new StreamValueReader(memoryStream))
				{
					ValueTreeDeserializer valueTreeDeserializer = new ValueTreeDeserializer(streamValueReader);
					int asInteger = valueTreeDeserializer.ReadNumber().AsInteger32;
					bool flag;
					if (asInteger != 1)
					{
						if (asInteger != 2)
						{
							throw new InvalidOperationException("serialized data was invalid");
						}
						flag = false;
					}
					else
					{
						flag = valueTreeDeserializer.ReadLogical().AsBoolean;
					}
					if (flag)
					{
						throw new NotImplementedException();
					}
					value2 = valueTreeDeserializer.Read();
				}
			}
			return value2;
		}
	}
}
