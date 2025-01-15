using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000077 RID: 119
	internal sealed class SingleTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000E91B File Offset: 0x0000CB1B
		internal override object Parse(string text)
		{
			return XmlConvert.ToSingle(text);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000E928 File Offset: 0x0000CB28
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((float)instance);
		}
	}
}
