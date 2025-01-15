using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Tokenization
{
	// Token: 0x020016BC RID: 5820
	public static class TokenExtensions
	{
		// Token: 0x0600C227 RID: 49703 RVA: 0x0029D8B8 File Offset: 0x0029BAB8
		public static string Render(this IDictionary<Example<IRow, object>, Token[]> subject)
		{
			return subject.Keys.Select((Example<IRow, object> key) => subject[key].Select((Token i) => i.ToString()).RenderNumbered(1).Concat((Environment.NewLine + "---").Yield<string>())).RenderNumbered(1);
		}
	}
}
