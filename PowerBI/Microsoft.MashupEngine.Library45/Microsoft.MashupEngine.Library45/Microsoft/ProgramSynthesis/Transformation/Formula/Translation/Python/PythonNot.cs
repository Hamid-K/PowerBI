using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187C RID: 6268
	internal class PythonNot : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CC4B RID: 52299 RVA: 0x002B91EF File Offset: 0x002B73EF
		public PythonNot(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x0600CC4C RID: 52300 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x0600CC4D RID: 52301 RVA: 0x002B9213 File Offset: 0x002B7413
		public FormulaExpression Subject { get; }

		// Token: 0x0600CC4E RID: 52302 RVA: 0x002B921B File Offset: 0x002B741B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonNot(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CC4F RID: 52303 RVA: 0x002B922E File Offset: 0x002B742E
		protected override string ToCodeString()
		{
			return string.Format("not {0}", this.Subject);
		}
	}
}
