using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000085 RID: 133
	internal sealed class UInt64TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000EA45 File Offset: 0x0000CC45
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt64(text);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000EA52 File Offset: 0x0000CC52
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((ulong)instance);
		}
	}
}
