using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F8 RID: 504
	public class PathTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x00021198 File Offset: 0x0001F398
		[JsonConstructor]
		public PathTokenizer()
			: base(OverlapStrategy.Subsumption, (TokenPatternMatch m) => null, new TokenPattern[]
			{
				new TokenPattern(PathTokenizer.WindowsAbsolutePathPattern, PathTokenizer.UnquotedWindowsLeftContext, PathTokenizer.UnquotedWindowsRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.WindowsRelativePathPattern, PathTokenizer.UnquotedWindowsLeftContext, PathTokenizer.UnquotedWindowsRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.UnixAbsolutePathPattern, PathTokenizer.UnquotedUnixLeftContext, PathTokenizer.UnquotedUnixRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.UnixRelativePathPattern, PathTokenizer.UnquotedUnixLeftContext, PathTokenizer.UnquotedUnixRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.QuotedWindowsAbsolutePathPattern, PathTokenizer.QuotedWindowsLeftContext, PathTokenizer.QuotedWindowsRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.QuotedWindowsRelativePathPattern, PathTokenizer.QuotedWindowsLeftContext, PathTokenizer.QuotedWindowsRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.QuotedUnixAbsolutePathPattern, PathTokenizer.QuotedUnixLeftContext, PathTokenizer.QuotedUnixRightContext, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PathTokenizer.QuotedUnixRelativePathPattern, PathTokenizer.QuotedUnixLeftContext, PathTokenizer.QuotedUnixRightContext, false, false, Array.Empty<RegexOptions>())
			})
		{
			this.Initialize();
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000212C7 File Offset: 0x0001F4C7
		private IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			yield return new PathToken(m.Source, m.Start, m.End);
			Group group = m.FullMatch.Groups["PathComponent"];
			string text = (m.Value.StartsWith("\"") ? m.Value.Substring(1, m.Value.Length - 2) : m.Value);
			if (!text.EndsWith("\\") && !text.EndsWith("/") && group.Success)
			{
				int count = group.Captures.Count;
				Capture capture = group.Captures[count - 1];
				if (capture.Length == 0)
				{
					yield break;
				}
				string[] array = capture.Value.Split(new char[] { '.' });
				string text2;
				string text3;
				if (array.Length == 1)
				{
					text2 = array[0];
					text3 = "";
				}
				else
				{
					text2 = array[0];
					text3 = string.Join(".", array.Skip(1));
				}
				yield return new FileNameToken(m.Source, capture.Index, capture.Index + capture.Length, text2, text3);
			}
			yield break;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000212D7 File Offset: 0x0001F4D7
		private void Initialize()
		{
			base.TokenFactory = new RegexBasedTokenizer.TokenFactoryDelegate(this.ProcessMatch);
		}

		// Token: 0x04000575 RID: 1397
		private const string PathComponentGroupName = "PathComponent";

		// Token: 0x04000576 RID: 1398
		private const string DriveLetter = "[a-zA-Z]+\\:";

		// Token: 0x04000577 RID: 1399
		private const string WindowsPathSeparator = "\\";

		// Token: 0x04000578 RID: 1400
		private const string UnixPathSeparator = "/";

		// Token: 0x04000579 RID: 1401
		private const string OpenQuotePattern = "(?:\\p{Pi}|\")";

		// Token: 0x0400057A RID: 1402
		private const string CloseQuotePattern = "(?:\\p{Pf}|\")";

		// Token: 0x0400057B RID: 1403
		private static readonly string PathComponentWithWhiteSpace = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>(?:[\\p{{L}}\\p{{N}}\\.\\s_-])+)", new object[] { "PathComponent" }));

		// Token: 0x0400057C RID: 1404
		private static readonly string PathComponentWithoutWhiteSpace = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>(?:[\\p{{L}}\\p{{N}}\\._-])+)", new object[] { "PathComponent" }));

		// Token: 0x0400057D RID: 1405
		private static readonly string WindowsPathSeparatorPattern = Regex.Escape("\\");

		// Token: 0x0400057E RID: 1406
		private static readonly string UnquotedWindowsLeftContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:^|[^\\d\\p{{L}}{0}\"\\p{{Pi}}])", new object[] { PathTokenizer.WindowsPathSeparatorPattern }));

		// Token: 0x0400057F RID: 1407
		private static readonly string UnquotedWindowsRightContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:$|[^\\p{{L}}\\d{0}\"\\p{{Pf}}])", new object[] { PathTokenizer.WindowsPathSeparatorPattern }));

		// Token: 0x04000580 RID: 1408
		private static readonly string QuotedWindowsLeftContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:^|[^\\d\\p{{L}}{0}])", new object[] { PathTokenizer.WindowsPathSeparatorPattern }));

		// Token: 0x04000581 RID: 1409
		private static readonly string QuotedWindowsRightContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:$|[^\\d\\p{{L}}{0}])", new object[] { PathTokenizer.WindowsPathSeparatorPattern }));

		// Token: 0x04000582 RID: 1410
		private static readonly string UnquotedUnixLeftContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:^|[^\\d\\p{{L}}{0}\"\\p{{Pi}}])", new object[] { "/" }));

		// Token: 0x04000583 RID: 1411
		private static readonly string UnquotedUnixRightContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:$|[^\\d{0}\"\\p{{Pf}}])", new object[] { "/" }));

		// Token: 0x04000584 RID: 1412
		private static readonly string QuotedUnixLeftContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:^|[^\\d\\p{{L}}{0}])", new object[] { "/" }));

		// Token: 0x04000585 RID: 1413
		private static readonly string QuotedUnixRightContext = FormattableString.Invariant(FormattableStringFactory.Create("(?:$|[^\\d\\p{{L}}{0}])", new object[] { "/" }));

		// Token: 0x04000586 RID: 1414
		private static readonly string WindowsAbsolutePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("({0}(?:{1}(?:{2}))+{3}?)", new object[]
		{
			"[a-zA-Z]+\\:",
			PathTokenizer.WindowsPathSeparatorPattern,
			PathTokenizer.PathComponentWithoutWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern
		}));

		// Token: 0x04000587 RID: 1415
		private static readonly string WindowsRelativePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("({0}(?:{1}{2})+{3}?)", new object[]
		{
			PathTokenizer.PathComponentWithoutWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern,
			PathTokenizer.PathComponentWithoutWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern
		}));

		// Token: 0x04000588 RID: 1416
		private static readonly string UnixAbsolutePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?:(?:{0}{1})+{2}?)", new object[]
		{
			"/",
			PathTokenizer.PathComponentWithoutWhiteSpace,
			"/"
		}));

		// Token: 0x04000589 RID: 1417
		private static readonly string UnixRelativePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?:{0}(?:{1}{2})+{3}?)", new object[]
		{
			PathTokenizer.PathComponentWithoutWhiteSpace,
			"/",
			PathTokenizer.PathComponentWithoutWhiteSpace,
			"/"
		}));

		// Token: 0x0400058A RID: 1418
		private static readonly string QuotedWindowsAbsolutePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}({1}(?:{2}(?:{3}))+{4}?){5}", new object[]
		{
			"(?:\\p{Pi}|\")",
			"[a-zA-Z]+\\:",
			PathTokenizer.WindowsPathSeparatorPattern,
			PathTokenizer.PathComponentWithWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern,
			"(?:\\p{Pf}|\")"
		}));

		// Token: 0x0400058B RID: 1419
		private static readonly string QuotedWindowsRelativePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}({1}(?:{2}{3})+{4}?){5}", new object[]
		{
			"(?:\\p{Pi}|\")",
			PathTokenizer.PathComponentWithWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern,
			PathTokenizer.PathComponentWithWhiteSpace,
			PathTokenizer.WindowsPathSeparatorPattern,
			"(?:\\p{Pf}|\")"
		}));

		// Token: 0x0400058C RID: 1420
		private static readonly string QuotedUnixAbsolutePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}(?:(?:{1}{2})+{3}?){4}", new object[]
		{
			"(?:\\p{Pi}|\")",
			"/",
			PathTokenizer.PathComponentWithWhiteSpace,
			"/",
			"(?:\\p{Pf}|\")"
		}));

		// Token: 0x0400058D RID: 1421
		private static readonly string QuotedUnixRelativePathPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}(?:{1}(?:{2}{3})+{4}?){5}", new object[]
		{
			"(?:\\p{Pi}|\")",
			PathTokenizer.PathComponentWithWhiteSpace,
			"/",
			PathTokenizer.PathComponentWithWhiteSpace,
			"/",
			"(?:\\p{Pf}|\")"
		}));
	}
}
