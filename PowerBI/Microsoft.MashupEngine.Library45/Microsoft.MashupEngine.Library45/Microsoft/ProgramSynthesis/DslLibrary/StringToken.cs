using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000807 RID: 2055
	public class StringToken : AbstractRegexToken
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x0007B721 File Offset: 0x00079921
		public string MatchedString { get; }

		// Token: 0x06002BFF RID: 11263 RVA: 0x0007B72C File Offset: 0x0007992C
		public StringToken(string matchedString, int score, double logPrior, string name = null, bool isSymbol = true, bool useForLearning = true, Func<string, double> evaluateLogLikelihood = null)
			: base(Regex.Escape(matchedString), name, score, logPrior, evaluateLogLikelihood, isSymbol, useForLearning, matchedString)
		{
			this.MatchedString = matchedString;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0007B757 File Offset: 0x00079957
		public override IEnumerable<PositionMatch> GetMatches(string content)
		{
			int pos = 0;
			for (;;)
			{
				int num = content.IndexOf(this.MatchedString, pos, StringComparison.InvariantCulture);
				if (num < 0)
				{
					break;
				}
				pos = num + 1;
				yield return new PositionMatch((uint)num, (uint)this.MatchedString.Length);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0007B76E File Offset: 0x0007996E
		public override Token Clone()
		{
			return new StringToken(this.MatchedString, base.Score, base.LogPrior, base.Name, base.IsSymbol, base.UseForLearning, new Func<string, double>(base.EvaluateLogLikelihood));
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0007B7A8 File Offset: 0x000799A8
		internal override XElement ToXml()
		{
			XElement xelement = new XElement("StringToken").WithAttribute("name", base.Name).WithAttribute("score", base.Score).WithAttribute("isSymbol", base.IsSymbol);
			xelement.Add(new XCData(this.MatchedString.ToString()));
			return xelement;
		}
	}
}
