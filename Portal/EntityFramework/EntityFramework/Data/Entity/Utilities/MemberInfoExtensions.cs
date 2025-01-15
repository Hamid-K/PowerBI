using System;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008D RID: 141
	internal static class MemberInfoExtensions
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public static object GetValue(this MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (!(propertyInfo != null))
			{
				return ((FieldInfo)memberInfo).GetValue(null);
			}
			return propertyInfo.GetValue(null, null);
		}
	}
}
