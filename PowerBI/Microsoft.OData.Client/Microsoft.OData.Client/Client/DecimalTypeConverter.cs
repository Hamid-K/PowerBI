using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000071 RID: 113
	internal sealed class DecimalTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E88C File Offset: 0x0000CA8C
		internal override object Parse(string text)
		{
			return XmlConvert.ToDecimal(text);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E899 File Offset: 0x0000CA99
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((decimal)instance);
		}
	}
}
