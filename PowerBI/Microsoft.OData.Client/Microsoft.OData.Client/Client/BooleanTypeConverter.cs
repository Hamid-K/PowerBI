using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006D RID: 109
	internal sealed class BooleanTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0000E7CF File Offset: 0x0000C9CF
		internal override object Parse(string text)
		{
			return XmlConvert.ToBoolean(text);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((bool)instance);
		}
	}
}
