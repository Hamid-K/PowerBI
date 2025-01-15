using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000E RID: 14
	internal static class EnumHelpers
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023C0 File Offset: 0x000005C0
		public static bool TryStringToEnum(Type enumType, string enumString, ref Enum value)
		{
			int num;
			if (string.IsNullOrEmpty(enumString) || int.TryParse(enumString, out num))
			{
				return false;
			}
			if (Enum.IsDefined(enumType, enumString))
			{
				value = (Enum)Enum.Parse(enumType, enumString);
				return true;
			}
			return false;
		}
	}
}
