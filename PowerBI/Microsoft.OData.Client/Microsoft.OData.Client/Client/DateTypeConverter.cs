using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x02000089 RID: 137
	internal sealed class DateTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000EAAD File Offset: 0x0000CCAD
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToDate(text);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000EABC File Offset: 0x0000CCBC
		internal override string ToString(object instance)
		{
			return ((Date)instance).ToString();
		}
	}
}
