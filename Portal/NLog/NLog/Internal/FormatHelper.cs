using System;
using System.Collections;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x0200011C RID: 284
	internal static class FormatHelper
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00024B78 File Offset: 0x00022D78
		internal static string ConvertToString(object o, IFormatProvider formatProvider)
		{
			if (formatProvider == null)
			{
				if (FormatHelper.SkipFormattableToString(o))
				{
					return ((o != null) ? o.ToString() : null) ?? string.Empty;
				}
				if (o is IFormattable)
				{
					LoggingConfiguration configuration = LogManager.Configuration;
					if (configuration != null)
					{
						formatProvider = configuration.DefaultCultureInfo;
					}
				}
			}
			return Convert.ToString(o, formatProvider);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00024BC8 File Offset: 0x00022DC8
		private static bool SkipFormattableToString(object o)
		{
			TypeCode typeCode = Convert.GetTypeCode(o);
			return typeCode == TypeCode.Empty || typeCode == TypeCode.String;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00024BEC File Offset: 0x00022DEC
		internal static string TryFormatToString(object value, string format, IFormatProvider formatProvider)
		{
			if (FormatHelper.SkipFormattableToString(value))
			{
				return ((value != null) ? value.ToString() : null) ?? string.Empty;
			}
			IFormattable formattable;
			if ((formattable = value as IFormattable) != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			if (value is IEnumerable)
			{
				return null;
			}
			return value.ToString() ?? string.Empty;
		}
	}
}
