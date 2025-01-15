using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001880 RID: 6272
	internal class PythonTernary : FormulaExpression
	{
		// Token: 0x0600CC62 RID: 52322 RVA: 0x002B9594 File Offset: 0x002B7794
		public PythonTernary(FormulaExpression condition, FormulaExpression trueBlock, FormulaExpression falseBlock)
		{
			if (trueBlock == null && falseBlock == null)
			{
				throw new Exception("Invalid PythonTernary state, True and False branches are both null.");
			}
			this.Condition = condition;
			this.TrueBlock = trueBlock;
			this.FalseBlock = falseBlock;
			base.Children = new FormulaExpression[] { this.Condition, this.TrueBlock, this.FalseBlock };
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x0600CC63 RID: 52323 RVA: 0x002B9600 File Offset: 0x002B7800
		public FormulaExpression Condition { get; }

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x0600CC64 RID: 52324 RVA: 0x002B9608 File Offset: 0x002B7808
		public FormulaExpression FalseBlock { get; }

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x0600CC65 RID: 52325 RVA: 0x002B9610 File Offset: 0x002B7810
		public FormulaExpression TrueBlock { get; }

		// Token: 0x0600CC66 RID: 52326 RVA: 0x002B9618 File Offset: 0x002B7818
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonTernary(this.Condition.Accept<FormulaExpression>(visitor), this.TrueBlock.Accept<FormulaExpression>(visitor), this.FalseBlock.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CC67 RID: 52327 RVA: 0x002B9643 File Offset: 0x002B7843
		protected override string ToCodeString()
		{
			return string.Format("{0} if {1} else {2}", this.TrueBlock, this.Condition, this.FalseBlock);
		}
	}
}
