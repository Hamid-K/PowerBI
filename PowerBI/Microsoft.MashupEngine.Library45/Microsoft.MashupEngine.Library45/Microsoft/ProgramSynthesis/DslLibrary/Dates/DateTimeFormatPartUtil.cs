using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000838 RID: 2104
	public static class DateTimeFormatPartUtil
	{
		// Token: 0x06002DA0 RID: 11680 RVA: 0x0008236B File Offset: 0x0008056B
		public static bool HasNonDelimitedNumbers(this DateTimeFormat dateTimeFormat)
		{
			return dateTimeFormat.FormatParts.HasNonDelimitedNumbers();
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00082378 File Offset: 0x00080578
		public static bool HasNonDelimitedNumbers(this IReadOnlyList<DateTimeFormatPart> dateTimeFormatParts)
		{
			return dateTimeFormatParts.Windowed((DateTimeFormatPart a, DateTimeFormatPart b) => (a.IsNumeric || a.IsNumericAtEnd) && b.IsNumeric).Any((bool b) => b);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000823D0 File Offset: 0x000805D0
		public static bool IsNumericIncludingAtEnd(this IReadOnlyList<DateTimeFormatPart> dateTimeFormatParts)
		{
			if (dateTimeFormatParts.SkipLast(1).All((DateTimeFormatPart fp) => fp.IsNumeric))
			{
				return (from last in dateTimeFormatParts.MaybeLast<DateTimeFormatPart>()
					select last.IsNumeric || last.IsNumericAtEnd).OrElse(false);
			}
			return false;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0008243C File Offset: 0x0008063C
		internal static DateTimeFormatMatch Sequence(this DateTimeFormatMatch first, DateTimeFormatMatch second)
		{
			if (!first.Region.WholeRegion.Equals(second.Region.WholeRegion))
			{
				return null;
			}
			if (first.Region.End != second.Region.Start)
			{
				return null;
			}
			IEnumerable<DateTimeFormatPart> enumerable = first.DateTimeFormat.FormatParts.Concat(second.DateTimeFormat.FormatParts);
			Optional<PartialDateTime> optional = first.PartialDateTime.CombineWith(second.PartialDateTime);
			if (!optional.HasValue)
			{
				return null;
			}
			return new DateTimeFormatMatch(first.Region.WholeRegion.Slice(first.Region.Start, second.Region.End), new DateTimeFormat(enumerable), optional.Value);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000824F3 File Offset: 0x000806F3
		public static bool AllowsLeadingZeros(this DateTimeFormatPart part)
		{
			FormatAttributes attributes = part.Attributes;
			return attributes != null && attributes.Attributes.ContainsKey("allowLeadingZeros");
		}
	}
}
