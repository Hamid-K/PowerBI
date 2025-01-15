using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000075 RID: 117
	internal sealed class Int32TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000E8E7 File Offset: 0x0000CAE7
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt32(text);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((int)instance);
		}
	}
}
