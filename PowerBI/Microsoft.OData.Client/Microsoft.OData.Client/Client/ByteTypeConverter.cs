using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006E RID: 110
	internal sealed class ByteTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000E7F1 File Offset: 0x0000C9F1
		internal override object Parse(string text)
		{
			return XmlConvert.ToByte(text);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((byte)instance);
		}
	}
}
