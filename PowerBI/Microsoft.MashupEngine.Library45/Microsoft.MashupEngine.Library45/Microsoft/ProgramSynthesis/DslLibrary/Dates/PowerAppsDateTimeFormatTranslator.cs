using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200087B RID: 2171
	internal class PowerAppsDateTimeFormatTranslator : DateTimeFormatTranslator
	{
		// Token: 0x06002F7B RID: 12155 RVA: 0x0008B43D File Offset: 0x0008963D
		public PowerAppsDateTimeFormatTranslator()
			: base(DateTimeFormat.Target.PowerAppsFormatting)
		{
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0008B448 File Offset: 0x00089648
		private static bool HasValidMinuteAndMonthParts(DateTimeFormat format)
		{
			bool flag = false;
			foreach (DateTimeFormatPart dateTimeFormatPart in format.AllFormatParts)
			{
				if (dateTimeFormatPart.MatchedPart.HasValue)
				{
					if (!flag && dateTimeFormatPart.MatchedPart.Value == DateTimePart.Minute)
					{
						return false;
					}
					if (flag && dateTimeFormatPart.MatchedPart.Value == DateTimePart.Month)
					{
						return false;
					}
					if (PowerAppsDateTimeFormatTranslator.HourParts.Contains(dateTimeFormatPart.MatchedPart.Value))
					{
						flag = true;
					}
					else if (dateTimeFormatPart.MatchedPart.Value.GetKind() != DateTimePartKind.Time)
					{
						flag = false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0008B510 File Offset: 0x00089710
		private static bool HasValidHourParts(DateTimeFormat format)
		{
			bool flag = format.AllFormatParts.Any((DateTimeFormatPart part) => part.MatchedPart.HasValue && part.MatchedPart.Value == DateTimePart.Period);
			foreach (DateTimeFormatPart dateTimeFormatPart in format.AllFormatParts)
			{
				if (dateTimeFormatPart.MatchedPart.HasValue)
				{
					if (dateTimeFormatPart.MatchedPart.Value == DateTimePart.Hour && flag)
					{
						return false;
					}
					if (dateTimeFormatPart.MatchedPart.Value == DateTimePart.HourInPeriod && !flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0008B5C8 File Offset: 0x000897C8
		internal static bool IsValid(DateTimeFormat format)
		{
			return PowerAppsDateTimeFormatTranslator.HasValidHourParts(format) && PowerAppsDateTimeFormatTranslator.HasValidMinuteAndMonthParts(format);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0008B5DC File Offset: 0x000897DC
		public override Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>? TranslateWithSegmentInfo(DateTimeFormat format, bool strict)
		{
			if (!PowerAppsDateTimeFormatTranslator.IsValid(format))
			{
				return null;
			}
			return base.TranslateWithSegmentInfo(format, strict);
		}

		// Token: 0x04001733 RID: 5939
		private static readonly ISet<DateTimePart> HourParts = new HashSet<DateTimePart>
		{
			DateTimePart.Hour,
			DateTimePart.HourInPeriod
		};
	}
}
