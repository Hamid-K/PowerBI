using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A9 RID: 6313
	internal class PowerQueryVariable : FormulaVariable
	{
		// Token: 0x0600CE0D RID: 52749 RVA: 0x002BF983 File Offset: 0x002BDB83
		public PowerQueryVariable(string name)
			: base(name)
		{
		}

		// Token: 0x0600CE0E RID: 52750 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CE0F RID: 52751 RVA: 0x002BF98C File Offset: 0x002BDB8C
		public override FormulaExpression CloneWith(string name)
		{
			return new PowerQueryVariable(name);
		}

		// Token: 0x0600CE10 RID: 52752 RVA: 0x002BF994 File Offset: 0x002BDB94
		public static string EscapeVariableName(string name)
		{
			if (!name.Split(new char[] { '.' }).All((string part) => PowerQueryVariable._keywordOrIdentifier.IsMatch(part) && !PowerQueryVariable._keywords.Contains(part)))
			{
				return "#" + PowerQueryStringLiteral.EscapeString(name);
			}
			return name;
		}

		// Token: 0x0600CE11 RID: 52753 RVA: 0x002BF9EA File Offset: 0x002BDBEA
		protected override string ToCodeString()
		{
			return PowerQueryVariable.EscapeVariableName(base.Name);
		}

		// Token: 0x0400508C RID: 20620
		private static readonly Regex _keywordOrIdentifier = new Regex("^[_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Nd}\\p{Pc}\\p{Mn}\\p{Mc}\\p{Cf}]*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x0400508D RID: 20621
		private static readonly HashSet<string> _keywords = new HashSet<string>
		{
			"and", "as", "each", "else", "error", "false", "if", "in", "is", "let",
			"meta", "not", "null", "or", "otherwise", "section", "shared", "then", "true", "try",
			"type", "#binary", "#date", "#datetime", "#datetimezone", "#duration", "#infinity", "#nan", "#sections", "#shared",
			"#table", "#time"
		};
	}
}
