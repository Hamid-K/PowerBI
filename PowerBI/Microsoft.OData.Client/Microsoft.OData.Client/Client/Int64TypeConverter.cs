using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000076 RID: 118
	internal sealed class Int64TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E901 File Offset: 0x0000CB01
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt64(text);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000E90E File Offset: 0x0000CB0E
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((long)instance);
		}
	}
}
