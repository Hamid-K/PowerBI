using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000A RID: 10
	internal sealed class EnumAccessor
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002244 File Offset: 0x00000444
		public bool IsSupportedEnum(string enumName)
		{
			string text = enumName.ToUpperInvariant();
			return EnumAccessor.SupportedEnums.Contains(text);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002264 File Offset: 0x00000464
		public object GetValue(string enumName, string valueName)
		{
			string text = enumName.ToUpperInvariant();
			if (!(text == "DATEINTERVAL"))
			{
				if (!(text == "MIDPOINTROUNDING"))
				{
					if (!(text == "DATEFORMAT"))
					{
						throw new NotSupportedException("The specified name of enum is not supported: " + text);
					}
					DateFormat dateFormat;
					if (!Enum.TryParse<DateFormat>(valueName, true, out dateFormat))
					{
						throw new ArgumentException("The specified value " + valueName + " is not supported for a DateFormat enum");
					}
					return dateFormat;
				}
				else
				{
					MidpointRounding midpointRounding;
					if (!Enum.TryParse<MidpointRounding>(valueName, true, out midpointRounding))
					{
						throw new ArgumentException("The specified value " + valueName + " is not supported for a MidpointRounding enum");
					}
					return midpointRounding;
				}
			}
			else
			{
				DateInterval dateInterval;
				if (!Enum.TryParse<DateInterval>(valueName, true, out dateInterval))
				{
					throw new ArgumentException("The specified value " + valueName + " is not supported for a DateInterval enum");
				}
				return dateInterval;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000232C File Offset: 0x0000052C
		public Type GetEnumItemType(string enumName)
		{
			string text = enumName.ToUpperInvariant();
			if (text == "DATEINTERVAL")
			{
				return typeof(DateInterval);
			}
			if (text == "MIDPOINTROUNDING")
			{
				return typeof(MidpointRounding);
			}
			if (!(text == "DATEFORMAT"))
			{
				throw new NotSupportedException("The specified name of enum is not supported: " + text);
			}
			return typeof(DateFormat);
		}

		// Token: 0x04000004 RID: 4
		private const string DateIntervalToken = "DATEINTERVAL";

		// Token: 0x04000005 RID: 5
		private const string MidpointRoundingToken = "MIDPOINTROUNDING";

		// Token: 0x04000006 RID: 6
		private const string DateFormatToken = "DATEFORMAT";

		// Token: 0x04000007 RID: 7
		private static readonly HashSet<string> SupportedEnums = new HashSet<string> { "DATEINTERVAL", "MIDPOINTROUNDING", "DATEFORMAT" };
	}
}
