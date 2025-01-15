using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D9D RID: 7581
	public static class SimplificationForReadability
	{
		// Token: 0x0600FE89 RID: 65161 RVA: 0x00365F60 File Offset: 0x00364160
		public static Program Apply(Program p, IEnumerable<Constraint<IRow, object>> examples, IEnumerable<IRow> additionalInputs = null)
		{
			IEnumerable<Example<IRow, object>> enumerable = (additionalInputs ?? new List<IRow>()).Select((IRow x) => new Example<IRow, object>(x, p.Run(x), false));
			IEnumerable<Constraint<IRow, object>> enumerable2 = examples ?? new List<Constraint<IRow, object>>();
			examples = enumerable2.Concat(enumerable).ToList<Constraint<IRow, object>>();
			additionalInputs = null;
			p = new RegexRemover(examples).GetAlternative(p) ?? p;
			return new TTextAlternativeSelector[]
			{
				ErreRewrite.Instance,
				SimplifyRegex.Instance,
				GeneralizedErreRewrite.Instance,
				ConcatenateRegex.Instance,
				SimplifyNumberFormat.Instance,
				SimplifyNumberFormatDetails.Instance,
				GeneralizeDateTimeParsingFormat.Instance,
				GeneralizeDateTimeOutputFormat.Instance
			}.Aggregate(p, (Program newP, TTextAlternativeSelector simplifier) => simplifier.Run(newP, examples, additionalInputs));
		}
	}
}
