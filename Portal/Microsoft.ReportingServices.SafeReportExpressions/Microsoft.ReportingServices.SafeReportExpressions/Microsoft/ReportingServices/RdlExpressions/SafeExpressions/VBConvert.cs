using System;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000009 RID: 9
	internal static class VBConvert
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000021E9 File Offset: 0x000003E9
		public static object ChangeType(object value, Type conversionType)
		{
			if (value != null)
			{
				return Conversion.CTypeDynamic(value, conversionType);
			}
			if (!conversionType.IsValueType)
			{
				return null;
			}
			return Activator.CreateInstance(conversionType);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002206 File Offset: 0x00000406
		public static bool ConvertToBoolean(object value)
		{
			if (((value != null) ? value.GetType() : null) == typeof(string))
			{
				return (bool)VBConvert.ChangeType(value, typeof(bool));
			}
			return Convert.ToBoolean(value);
		}
	}
}
