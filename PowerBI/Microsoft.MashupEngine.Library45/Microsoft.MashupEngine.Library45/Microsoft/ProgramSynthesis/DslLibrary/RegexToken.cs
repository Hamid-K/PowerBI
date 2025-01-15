using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007F2 RID: 2034
	public sealed class RegexToken : AbstractRegexToken
	{
		// Token: 0x06002B5D RID: 11101 RVA: 0x0007920C File Offset: 0x0007740C
		public RegexToken(Regex regex, string name, int score, double logPrior, Func<string, double> evaluateLogLikelihood, bool isSymbol = true, bool useForLearning = true, string canonicalRepresentation = null)
			: base(regex, name, score, logPrior, evaluateLogLikelihood, isSymbol, useForLearning, canonicalRepresentation)
		{
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0007922C File Offset: 0x0007742C
		public RegexToken(string pattern, string name, int score, double logPrior, Func<string, double> evaluateLogLikelihood, bool isSymbol = true, bool useForLearning = true, string canonicalRepresentation = null)
			: base(pattern, name, score, logPrior, evaluateLogLikelihood, isSymbol, useForLearning, canonicalRepresentation)
		{
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0007924C File Offset: 0x0007744C
		public override Token Clone()
		{
			return new RegexToken(base.Regex.ToString(), base.Name, base.Score, base.LogPrior, new Func<string, double>(base.EvaluateLogLikelihood), base.IsSymbol, base.UseForLearning, base.CanonicalRepresentation);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0007929C File Offset: 0x0007749C
		internal override XElement ToXml()
		{
			XElement xelement = new XElement("Token").WithAttribute("name", base.Name).WithAttribute("score", base.Score).WithAttribute("isSymbol", base.IsSymbol);
			xelement.Add(new XCData(base.Regex.ToString()));
			return xelement;
		}
	}
}
