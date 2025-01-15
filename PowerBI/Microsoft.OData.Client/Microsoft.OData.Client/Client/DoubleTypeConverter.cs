using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000072 RID: 114
	internal sealed class DoubleTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000E8A6 File Offset: 0x0000CAA6
		internal override object Parse(string text)
		{
			return XmlConvert.ToDouble(text);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E8B3 File Offset: 0x0000CAB3
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((double)instance);
		}
	}
}
