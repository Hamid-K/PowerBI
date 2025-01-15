using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D90 RID: 7568
	internal class ConcatenateRegex : TTextAlternativeSelector
	{
		// Token: 0x0600FE50 RID: 65104 RVA: 0x00364E7E File Offset: 0x0036307E
		private ConcatenateRegex()
		{
		}

		// Token: 0x17002A5F RID: 10847
		// (get) Token: 0x0600FE51 RID: 65105 RVA: 0x00364E86 File Offset: 0x00363086
		public static ConcatenateRegex Instance { get; } = new ConcatenateRegex();

		// Token: 0x0600FE52 RID: 65106 RVA: 0x00364E90 File Offset: 0x00363090
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			r r;
			if (Language.Build.Node.Is.r(p, out r))
			{
				RegularExpression value = r.Value;
				if (value != null)
				{
					if (value.Tokens.Length > 1)
					{
						if (!value.Tokens.Any((Token t) => !(t is AbstractRegexToken)))
						{
							string text = ReadablePythonTranslator.TokenArray2PythonVariableName(value.Tokens.Cast<AbstractRegexToken>());
							RegexToken regexToken = new RegexToken(value.Regex.ToString(), text, 0, 1.0, null, true, true, null);
							return Language.Build.Node.Rule.r(new RegularExpression(new RegexToken[] { regexToken }, 0)).Node.Yield<ProgramNode>();
						}
					}
					return null;
				}
			}
			return null;
		}
	}
}
