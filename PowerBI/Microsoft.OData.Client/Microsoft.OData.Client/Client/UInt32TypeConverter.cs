using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000084 RID: 132
	internal sealed class UInt32TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0000EA2B File Offset: 0x0000CC2B
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt32(text);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000EA38 File Offset: 0x0000CC38
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((uint)instance);
		}
	}
}
