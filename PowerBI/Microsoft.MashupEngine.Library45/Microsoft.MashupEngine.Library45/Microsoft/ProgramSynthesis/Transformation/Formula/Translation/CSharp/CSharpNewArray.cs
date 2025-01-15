using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001972 RID: 6514
	internal class CSharpNewArray : FormulaExpression
	{
		// Token: 0x0600D432 RID: 54322 RVA: 0x002B9114 File Offset: 0x002B7314
		public CSharpNewArray(IEnumerable<FormulaExpression> values)
		{
			base.Children = values.ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x0600D433 RID: 54323 RVA: 0x002D28AD File Offset: 0x002D0AAD
		public IEnumerable<FormulaExpression> Values
		{
			get
			{
				return base.Children;
			}
		}

		// Token: 0x0600D434 RID: 54324 RVA: 0x002D28B5 File Offset: 0x002D0AB5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpNewArray(this.Values.Accept(visitor));
		}

		// Token: 0x0600D435 RID: 54325 RVA: 0x002D28C8 File Offset: 0x002D0AC8
		protected override string ToCodeString()
		{
			return "new[] { " + string.Join(", ", this.Values.Select((FormulaExpression v) => v.ToString())) + " }";
		}
	}
}
