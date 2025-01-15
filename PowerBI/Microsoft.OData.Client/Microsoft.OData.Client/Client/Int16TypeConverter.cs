using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000074 RID: 116
	internal sealed class Int16TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000E8CD File Offset: 0x0000CACD
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt16(text);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000E8DA File Offset: 0x0000CADA
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((short)instance);
		}
	}
}
