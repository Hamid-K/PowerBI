using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001DA7 RID: 7591
	internal abstract class TTextAlternativeSelector
	{
		// Token: 0x0600FEB0 RID: 65200
		protected abstract IEnumerable<ProgramNode> GetAlternatives(ProgramNode p);

		// Token: 0x0600FEB1 RID: 65201 RVA: 0x00366AE4 File Offset: 0x00364CE4
		internal Program Run(Program program, IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs = null)
		{
			return program.SelectAlternative(Loader.Instance, new Func<ProgramNode, IEnumerable<ProgramNode>>(this.GetAlternatives), constraints, additionalInputs);
		}
	}
}
