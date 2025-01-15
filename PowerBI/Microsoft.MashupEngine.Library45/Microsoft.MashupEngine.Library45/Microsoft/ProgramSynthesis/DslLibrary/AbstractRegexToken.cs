using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007EA RID: 2026
	public abstract class AbstractRegexToken : Token
	{
		// Token: 0x06002B31 RID: 11057 RVA: 0x00078A14 File Offset: 0x00076C14
		protected AbstractRegexToken(Regex regex, string name, int score, double logPrior, Func<string, double> evaluateLogLikelihood, bool isSymbol = true, bool useForLearning = true, string canonicalRepresentation = null)
			: base(name, score, logPrior, isSymbol, useForLearning, canonicalRepresentation, evaluateLogLikelihood)
		{
			this.Regex = regex;
			base.Score = score;
			if (!base.IsDynamicToken && canonicalRepresentation != null && !this.MatchesEntireString(canonicalRepresentation))
			{
				throw new ArgumentException("canonicalRepresentation should match token regex for token " + name);
			}
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x00078A68 File Offset: 0x00076C68
		protected AbstractRegexToken(string pattern, string name, int score, double logPrior, Func<string, double> evaluateLogLikelihood, bool isSymbol = true, bool useForLearning = true, string canonicalRepresentation = null)
			: this(AbstractRegexToken.BuildRegex(pattern), name, score, logPrior, evaluateLogLikelihood, isSymbol, useForLearning, canonicalRepresentation)
		{
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00078A90 File Offset: 0x00076C90
		private static Regex BuildRegex(string pattern)
		{
			Regex regex;
			try
			{
				regex = new Regex(pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			}
			catch (ArgumentException)
			{
				regex = new Regex(pattern, RegexOptions.Compiled);
			}
			return regex;
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x00078AC4 File Offset: 0x00076CC4
		public Regex Regex { get; }

		// Token: 0x06002B35 RID: 11061 RVA: 0x00078ACC File Offset: 0x00076CCC
		public override IEnumerable<PositionMatch> GetMatches(string content)
		{
			IEnumerable<Match> enumerable = this.Regex.NonCachingMatches(content);
			Func<Match, Optional<PositionMatch>> func;
			if ((func = AbstractRegexToken.<>O.<0>__From) == null)
			{
				func = (AbstractRegexToken.<>O.<0>__From = new Func<Match, Optional<PositionMatch>>(PositionMatch.From));
			}
			return enumerable.SelectMany(func);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00078AFC File Offset: 0x00076CFC
		public sealed override bool MatchesEntireString(string target)
		{
			Match match = this.Regex.Match(target);
			return match.Success && match.Index == 0 && match.Length == target.Length;
		}

		// Token: 0x020007EB RID: 2027
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040014B9 RID: 5305
			public static Func<Match, Optional<PositionMatch>> <0>__From;
		}
	}
}
