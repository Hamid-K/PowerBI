using System;
using System.Xml.Linq;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007E RID: 126
	internal sealed class XDocumentTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000E9B1 File Offset: 0x0000CBB1
		internal override object Parse(string text)
		{
			if (text.Length <= 0)
			{
				return new XDocument();
			}
			return XDocument.Parse(text);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000E84A File Offset: 0x0000CA4A
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
