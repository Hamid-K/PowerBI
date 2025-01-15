using System;
using NLog.Internal;

namespace NLog.Common
{
	// Token: 0x020001BB RID: 443
	public static class ConversionHelpers
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x0003546D File Offset: 0x0003366D
		public static bool TryParseEnum<TEnum>(string inputValue, out TEnum resultValue, TEnum defaultValue = default(TEnum)) where TEnum : struct
		{
			if (string.IsNullOrEmpty(inputValue))
			{
				resultValue = defaultValue;
				return true;
			}
			if (!ConversionHelpers.TryParse<TEnum>(inputValue, true, out resultValue))
			{
				resultValue = defaultValue;
				return false;
			}
			return true;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00035494 File Offset: 0x00033694
		internal static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
		{
			return ConversionHelpers.TryParse<TEnum>(value, false, out result);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0003549E File Offset: 0x0003369E
		internal static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, ignoreCase, out result);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000354A8 File Offset: 0x000336A8
		private static bool TryParseEnum_net3<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			if (!typeFromHandle.IsEnum())
			{
				throw new ArgumentException("Type '" + typeFromHandle.FullName + "' is not an enum");
			}
			if (StringHelpers.IsNullOrWhiteSpace(value))
			{
				result = default(TEnum);
				return false;
			}
			bool flag;
			try
			{
				result = (TEnum)((object)Enum.Parse(typeFromHandle, value, ignoreCase));
				flag = true;
			}
			catch (Exception)
			{
				result = default(TEnum);
				flag = false;
			}
			return flag;
		}
	}
}
