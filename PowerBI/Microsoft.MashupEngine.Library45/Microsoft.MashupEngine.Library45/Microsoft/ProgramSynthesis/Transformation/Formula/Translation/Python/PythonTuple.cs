using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001881 RID: 6273
	internal class PythonTuple : FormulaExpression
	{
		// Token: 0x0600CC68 RID: 52328 RVA: 0x002B9664 File Offset: 0x002B7864
		public PythonTuple(params FormulaExpression[] items)
		{
			this.Items = items.TakeWhile((FormulaExpression i) => i != null).ToReadOnlyList<FormulaExpression>();
			base.Children = this.Items;
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x0600CC69 RID: 52329 RVA: 0x002B96B3 File Offset: 0x002B78B3
		public IReadOnlyList<FormulaExpression> Items { get; }

		// Token: 0x0600CC6A RID: 52330 RVA: 0x002B96BB File Offset: 0x002B78BB
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonTuple(base.Children.Accept(visitor).ToArray<FormulaExpression>());
		}

		// Token: 0x0600CC6B RID: 52331 RVA: 0x002B96D3 File Offset: 0x002B78D3
		protected override string ToCodeString()
		{
			return string.Join<FormulaExpression>(", ", this.Items);
		}
	}
}
