using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007A RID: 122
	internal sealed class CharTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000E957 File Offset: 0x0000CB57
		internal override object Parse(string text)
		{
			return XmlConvert.ToChar(text);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E964 File Offset: 0x0000CB64
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((char)instance);
		}
	}
}
