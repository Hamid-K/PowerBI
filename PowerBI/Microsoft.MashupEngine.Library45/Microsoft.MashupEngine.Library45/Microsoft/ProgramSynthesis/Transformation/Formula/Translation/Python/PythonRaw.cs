using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001878 RID: 6264
	internal class PythonRaw : FormulaExpression
	{
		// Token: 0x0600CC3C RID: 52284 RVA: 0x002B9114 File Offset: 0x002B7314
		public PythonRaw(IEnumerable<FormulaExpression> children)
		{
			base.Children = children.ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x0600CC3D RID: 52285 RVA: 0x002B9128 File Offset: 0x002B7328
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonRaw(base.Children.Accept(visitor));
		}

		// Token: 0x0600CC3E RID: 52286 RVA: 0x002B913B File Offset: 0x002B733B
		protected override string ToCodeString()
		{
			return string.Concat(base.Children.Select((FormulaExpression i) => i.ToString()));
		}
	}
}
