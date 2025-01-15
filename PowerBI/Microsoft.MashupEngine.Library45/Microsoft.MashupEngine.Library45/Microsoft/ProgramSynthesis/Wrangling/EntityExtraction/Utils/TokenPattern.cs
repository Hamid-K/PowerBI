using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001AB RID: 427
	public class TokenPattern : IEquatable<TokenPattern>
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0001BB44 File Offset: 0x00019D44
		public TokenPattern(string contentPattern, string leftContextPattern = null, string rightContextPattern = null, bool captureLeftContext = false, bool captureRightContext = false, params RegexOptions[] regexOptions)
		{
			this.ContentPattern = contentPattern;
			this.LeftContextPattern = (string.IsNullOrEmpty(leftContextPattern) ? string.Empty : leftContextPattern);
			this.RightContextPattern = (string.IsNullOrEmpty(rightContextPattern) ? string.Empty : rightContextPattern);
			this.CaptureLeftContext = captureLeftContext;
			this.CaptureRightContext = captureRightContext;
			this._CheckPatterns();
			StringBuilder stringBuilder = new StringBuilder();
			if (this.LeftContextPattern.Length > 0)
			{
				stringBuilder.Append(this.CaptureLeftContext ? FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1})", new object[] { "__LeftContext__", this.LeftContextPattern })) : FormattableString.Invariant(FormattableStringFactory.Create("(?:{0})", new object[] { this.LeftContextPattern })));
			}
			stringBuilder.Append(FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1})", new object[] { "__Content__", this.ContentPattern })));
			if (this.RightContextPattern.Length > 0)
			{
				stringBuilder.Append(this.CaptureRightContext ? FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1})", new object[] { "__RightContext__", this.RightContextPattern })) : FormattableString.Invariant(FormattableStringFactory.Create("(?:{0})", new object[] { this.RightContextPattern })));
			}
			this.RegexOptions = regexOptions.Aggregate(RegexOptions.ExplicitCapture | RegexOptions.Compiled, (RegexOptions a, RegexOptions b) => a | b);
			this.RegexToMatch = new Regex(stringBuilder.ToString(), this.RegexOptions);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001BCDE File Offset: 0x00019EDE
		public static string LeftContextGroupName
		{
			get
			{
				return "__LeftContext__";
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001BCE5 File Offset: 0x00019EE5
		public static string RightContextGroupName
		{
			get
			{
				return "__RightContext__";
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public static string ContentGroupName
		{
			get
			{
				return "__Content__";
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001BCF3 File Offset: 0x00019EF3
		public string LeftContextPattern { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001BCFB File Offset: 0x00019EFB
		public string ContentPattern { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001BD03 File Offset: 0x00019F03
		public string RightContextPattern { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001BD0B File Offset: 0x00019F0B
		public bool CaptureLeftContext { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001BD13 File Offset: 0x00019F13
		public bool CaptureRightContext { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001BD1B File Offset: 0x00019F1B
		public Regex RegexToMatch { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001BD23 File Offset: 0x00019F23
		public RegexOptions RegexOptions { get; }

		// Token: 0x06000971 RID: 2417 RVA: 0x0001BD2B File Offset: 0x00019F2B
		public bool Equals(TokenPattern other)
		{
			return other == this || (other != null && this.RegexToMatch.ToString() == other.RegexToMatch.ToString() && this.RegexOptions == other.RegexOptions);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001BD68 File Offset: 0x00019F68
		private void _CheckPattern(string pattern)
		{
			if (pattern.Contains(FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>", new object[] { "__LeftContext__" }))) || pattern.Contains(FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>", new object[] { "__RightContext__" }))) || pattern.Contains(FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>", new object[] { "__Content__" }))))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("One or more input patterns to {0} contains forbidden capture group names\\n", new object[] { base.GetType() })) + FormattableString.Invariant(FormattableStringFactory.Create("Forbidden group names are: \"{0}\", \"{1}\", and \"{2}\"", new object[] { "__LeftContext__", "__RightContext__", "__Content__" })));
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001BE3A File Offset: 0x0001A03A
		private void _CheckPatterns()
		{
			this._CheckPattern(this.LeftContextPattern);
			this._CheckPattern(this.RightContextPattern);
			this._CheckPattern(this.ContentPattern);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001BE60 File Offset: 0x0001A060
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals((TokenPattern)other)));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001BE8E File Offset: 0x0001A08E
		public override int GetHashCode()
		{
			return this.RegexToMatch.ToString().GetHashCode() * 24919;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001BEA6 File Offset: 0x0001A0A6
		public TokenPattern Clone()
		{
			return new TokenPattern(this.ContentPattern, this.LeftContextPattern, this.RightContextPattern, this.CaptureLeftContext, this.CaptureRightContext, Array.Empty<RegexOptions>());
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		public IEnumerable<TokenPatternMatch> Matches(string s)
		{
			int position = 0;
			while (position <= s.Length)
			{
				Match i = this.RegexToMatch.Match(s, position);
				if (!i.Success)
				{
					yield break;
				}
				yield return new TokenPatternMatch(i, s, this);
				if (position == s.Length)
				{
					yield break;
				}
				position = i.Groups[TokenPattern.ContentGroupName].Index + i.Groups[TokenPattern.ContentGroupName].Length;
				i = null;
			}
			yield break;
		}

		// Token: 0x0400047B RID: 1147
		private const string CLeftContextGroupName = "__LeftContext__";

		// Token: 0x0400047C RID: 1148
		private const string CRightContextGroupName = "__RightContext__";

		// Token: 0x0400047D RID: 1149
		private const string CContentGroupName = "__Content__";
	}
}
