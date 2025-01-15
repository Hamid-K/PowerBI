using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187E RID: 6270
	internal class PythonEnumeration : FormulaExpression
	{
		// Token: 0x0600CC55 RID: 52309 RVA: 0x002B9294 File Offset: 0x002B7494
		public PythonEnumeration(FormulaExpression selector, FormulaExpression index, FormulaExpression iterator, FormulaExpression condition)
		{
			this.Selector = selector;
			this.Index = index;
			this.Iterator = iterator;
			this.Condition = condition;
			IEnumerable<FormulaExpression> enumerable = new FormulaExpression[] { this.Selector, this.Index, this.Iterator };
			if (this.Condition != null)
			{
				enumerable = enumerable.Concat(this.Condition.Yield<FormulaExpression>());
			}
			base.Children = enumerable.ToList<FormulaExpression>();
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x0600CC56 RID: 52310 RVA: 0x002B9312 File Offset: 0x002B7512
		public FormulaExpression Condition { get; }

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x0600CC57 RID: 52311 RVA: 0x002B931A File Offset: 0x002B751A
		public FormulaExpression Index { get; }

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x0600CC58 RID: 52312 RVA: 0x002B9322 File Offset: 0x002B7522
		public FormulaExpression Iterator { get; }

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x0600CC59 RID: 52313 RVA: 0x002B932A File Offset: 0x002B752A
		public FormulaExpression Selector { get; }

		// Token: 0x0600CC5A RID: 52314 RVA: 0x002B9332 File Offset: 0x002B7532
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Selector.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression2 = this.Index.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression3 = this.Iterator.Accept<FormulaExpression>(visitor);
			FormulaExpression condition = this.Condition;
			return new PythonEnumeration(formulaExpression, formulaExpression2, formulaExpression3, (condition != null) ? condition.Accept<FormulaExpression>(visitor) : null);
		}

		// Token: 0x0600CC5B RID: 52315 RVA: 0x002B9370 File Offset: 0x002B7570
		protected override string ToCodeString()
		{
			if (!(this.Condition == null))
			{
				return string.Format("[{0} for {1} in {2} if {3}]", new object[] { this.Selector, this.Index, this.Iterator, this.Condition });
			}
			return string.Format("[{0} for {1} in {2}]", this.Selector, this.Index, this.Iterator);
		}
	}
}
