using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000080 RID: 128
	internal sealed class DateTimeOffsetTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToDateTimeOffset(text);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000E9DD File Offset: 0x0000CBDD
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((DateTimeOffset)instance);
		}
	}
}
