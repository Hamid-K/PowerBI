using System;
using System.Xml.Linq;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007F RID: 127
	internal sealed class XElementTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		internal override object Parse(string text)
		{
			return XElement.Parse(text);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000E84A File Offset: 0x0000CA4A
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
