using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000081 RID: 129
	internal sealed class DateTimeTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000E9EA File Offset: 0x0000CBEA
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToDateTime(text);
		}
	}
}
