using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000809 RID: 2057
	public static class SubstringUtils
	{
		// Token: 0x06002C0B RID: 11275 RVA: 0x0007B91F File Offset: 0x00079B1F
		public static bool StartsWith(this Substring fullString, Substring start)
		{
			return (fullString.Source == start.Source && fullString.Start == start.Start && fullString.End >= start.End) || fullString.Value.StartsWith(start.Value, StringComparison.Ordinal);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x0007B95F File Offset: 0x00079B5F
		public static bool StartsWith(this Substring fullString, string start)
		{
			return (ulong)fullString.Length >= (ulong)((long)start.Length) && (long)fullString.Source.IndexOf(start, (int)fullString.Start, start.Length, StringComparison.Ordinal) == (long)((ulong)fullString.Start);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0007B996 File Offset: 0x00079B96
		public static bool EndsWith(this Substring fullString, Substring end)
		{
			return (fullString.Source == end.Source && fullString.End == end.End && fullString.Start >= end.Start) || fullString.Value.EndsWith(end.Value, StringComparison.Ordinal);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0007B9D6 File Offset: 0x00079BD6
		public static bool EndsWith(this Substring fullString, string end)
		{
			return (ulong)fullString.Length >= (ulong)((long)end.Length) && fullString.Value.EndsWith(end, StringComparison.Ordinal);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0007B9F8 File Offset: 0x00079BF8
		public static Optional<uint> PositionOf(this Substring substring, string needle, uint start, uint end, StringComparison stringComparison = StringComparison.Ordinal)
		{
			if (substring == null)
			{
				throw new ArgumentNullException("substring");
			}
			if (start < substring.Start)
			{
				throw new ArgumentException("Start must be inside substring (substring.Start <= start <= substring.End).", "start");
			}
			if (end < start || end > substring.End)
			{
				throw new ArgumentException("End must be inside substring and after start (substring.Start <= end <= substring.End).", "end");
			}
			int num = substring.Source.IndexOf(needle, (int)start, (int)(end - start), stringComparison);
			if (num >= 0)
			{
				return ((uint)num).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0007BA6C File Offset: 0x00079C6C
		public static Optional<uint> PositionOf(this Substring substring, string needle, uint start, StringComparison stringComparison = StringComparison.Ordinal)
		{
			return substring.PositionOf(needle, start, substring.End, stringComparison);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0007BA7D File Offset: 0x00079C7D
		public static Optional<uint> PositionOf(this Substring substring, string needle, StringComparison stringComparison = StringComparison.Ordinal)
		{
			return substring.PositionOf(needle, substring.Start, substring.End, stringComparison);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0007BA94 File Offset: 0x00079C94
		public static Optional<uint> PositionOf(this Substring substring, char c)
		{
			int num = substring.Source.IndexOf(c, (int)substring.Start, (int)(substring.End - substring.Start));
			if (num >= 0)
			{
				return ((uint)((long)num - (long)((ulong)substring.Start))).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0007BADC File Offset: 0x00079CDC
		public static Optional<uint> PositionOfAny(this Substring substring, char[] anyOf)
		{
			int num = substring.Source.IndexOfAny(anyOf, (int)substring.Start, (int)(substring.End - substring.Start));
			if (num >= 0)
			{
				return ((uint)((long)num - (long)((ulong)substring.Start))).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0007BB24 File Offset: 0x00079D24
		public static Optional<uint> IndexOfRelative(this Substring substring, string needle, uint start, uint end, StringComparison stringComparison = StringComparison.Ordinal)
		{
			if (substring == null)
			{
				throw new ArgumentNullException("substring");
			}
			if (start > substring.Length)
			{
				throw new ArgumentException("Start must be inside substring (0 <= start <= substring.Length).", "start");
			}
			if (end < start || end > substring.Length)
			{
				throw new ArgumentException("End must be inside substring and after start (start <= end <= substring.Length).", "end");
			}
			int num = substring.Source.IndexOf(needle, (int)(start + substring.Start), (int)(end - start), stringComparison);
			if (num >= 0)
			{
				return ((uint)((long)num - (long)((ulong)substring.Start))).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0007BBA9 File Offset: 0x00079DA9
		public static Optional<uint> IndexOfRelative(this Substring substring, string needle, uint start = 0U, StringComparison stringComparison = StringComparison.Ordinal)
		{
			return substring.IndexOfRelative(needle, start, substring.Length, stringComparison);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0007BBBA File Offset: 0x00079DBA
		public static Optional<uint> LastPositionOf(this Substring substring, string needle, StringComparison stringComparison = StringComparison.Ordinal)
		{
			return substring.LastPositionOf(needle, substring.End, stringComparison);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0007BBCC File Offset: 0x00079DCC
		public static Optional<uint> LastPositionOf(this Substring substring, string needle, uint start, StringComparison stringComparison = StringComparison.Ordinal)
		{
			if (substring == null)
			{
				throw new ArgumentNullException("substring");
			}
			if (start < substring.Start || start > substring.End)
			{
				throw new ArgumentException("Start must be inside substring (substring.Start <= start <= substring.End).", "start");
			}
			int num = substring.Source.LastIndexOf(needle, (int)start, stringComparison);
			if (num >= 0)
			{
				return ((uint)num).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0007BC28 File Offset: 0x00079E28
		public static Optional<char> MaybeFirstChar(this Substring substring)
		{
			if (substring.Length <= 0U)
			{
				return Optional<char>.Nothing;
			}
			return substring.Source[(int)substring.Start].Some<char>();
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0007BC4F File Offset: 0x00079E4F
		public static Optional<char> MaybeLastChar(this Substring substring)
		{
			if (substring.Length <= 0U)
			{
				return Optional<char>.Nothing;
			}
			return substring.Source[(int)(substring.End - 1U)].Some<char>();
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0007BC78 File Offset: 0x00079E78
		public static Optional<char> MaybePreviousChar(this Substring substring)
		{
			if (substring.Start <= 0U)
			{
				return Optional<char>.Nothing;
			}
			return substring.Source[(int)(substring.Start - 1U)].Some<char>();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0007BCA1 File Offset: 0x00079EA1
		public static Optional<char> MaybeNextChar(this Substring substring)
		{
			if ((long)substring.Source.Length <= (long)((ulong)substring.End))
			{
				return Optional<char>.Nothing;
			}
			return substring.Source[(int)substring.End].Some<char>();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0007BCD4 File Offset: 0x00079ED4
		public static bool IsNullOrWhiteSpace(this Substring substring)
		{
			return substring == null || string.IsNullOrWhiteSpace(substring.Value);
		}
	}
}
