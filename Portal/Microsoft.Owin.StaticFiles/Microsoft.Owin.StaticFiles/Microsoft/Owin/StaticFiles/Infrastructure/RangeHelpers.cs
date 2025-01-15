using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Owin.StaticFiles.Infrastructure
{
	// Token: 0x02000016 RID: 22
	internal static class RangeHelpers
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003778 File Offset: 0x00001978
		internal static bool TryParseRanges(string rangeHeader, out IList<Tuple<long?, long?>> parsedRanges)
		{
			parsedRanges = null;
			if (string.IsNullOrWhiteSpace(rangeHeader) || !rangeHeader.StartsWith("bytes=", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string[] subRanges = rangeHeader.Substring("bytes=".Length).Replace(" ", string.Empty).Split(new char[] { ',' });
			List<Tuple<long?, long?>> ranges = new List<Tuple<long?, long?>>();
			for (int i = 0; i < subRanges.Length; i++)
			{
				long? first = null;
				long? second = null;
				string subRange = subRanges[i];
				int dashIndex = subRange.IndexOf('-');
				if (dashIndex < 0)
				{
					return false;
				}
				if (dashIndex == 0)
				{
					string remainder = subRange.Substring(1);
					if (!RangeHelpers.TryParseLong(remainder, out second))
					{
						return false;
					}
				}
				else if (dashIndex == subRange.Length - 1)
				{
					string remainder2 = subRange.Substring(0, subRange.Length - 1);
					if (!RangeHelpers.TryParseLong(remainder2, out first))
					{
						return false;
					}
				}
				else
				{
					string firstString = subRange.Substring(0, dashIndex);
					string secondString = subRange.Substring(dashIndex + 1, subRange.Length - dashIndex - 1);
					if (!RangeHelpers.TryParseLong(firstString, out first) || !RangeHelpers.TryParseLong(secondString, out second) || first.Value > second.Value)
					{
						return false;
					}
				}
				ranges.Add(new Tuple<long?, long?>(first, second));
			}
			if (ranges.Count > 0)
			{
				parsedRanges = ranges;
				return true;
			}
			return false;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038C4 File Offset: 0x00001AC4
		private static bool TryParseLong(string input, out long? result)
		{
			long temp;
			if (!string.IsNullOrWhiteSpace(input) && long.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out temp))
			{
				result = new long?(temp);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003900 File Offset: 0x00001B00
		internal static IList<Tuple<long, long>> NormalizeRanges(IList<Tuple<long?, long?>> ranges, long length)
		{
			IList<Tuple<long, long>> normalizedRanges = new List<Tuple<long, long>>(ranges.Count);
			int i = 0;
			while (i < ranges.Count)
			{
				Tuple<long?, long?> range = ranges[i];
				long? start = range.Item1;
				long? end = range.Item2;
				if (start != null)
				{
					if (start.Value < length)
					{
						if (end == null || end.Value >= length)
						{
							end = new long?(length - 1L);
							goto IL_00E0;
						}
						goto IL_00E0;
					}
				}
				else if (end.Value != 0L)
				{
					long bytes = Math.Min(end.Value, length);
					start = new long?(length - bytes);
					end = start + bytes - 1L;
					goto IL_00E0;
				}
				IL_00F9:
				i++;
				continue;
				IL_00E0:
				normalizedRanges.Add(new Tuple<long, long>(start.Value, end.Value));
				goto IL_00F9;
			}
			return normalizedRanges;
		}
	}
}
