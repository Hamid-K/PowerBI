using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x0200008A RID: 138
	internal sealed class TimeOfDayConvert : PrimitiveTypeConverter
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0000EADD File Offset: 0x0000CCDD
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToTimeOfDay(text);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		internal override string ToString(object instance)
		{
			return ((TimeOfDay)instance).ToString();
		}
	}
}
