using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187D RID: 6269
	internal class PythonNegate : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CC50 RID: 52304 RVA: 0x002B9240 File Offset: 0x002B7440
		public PythonNegate(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x0600CC51 RID: 52305 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x0600CC52 RID: 52306 RVA: 0x002B9264 File Offset: 0x002B7464
		public FormulaExpression Subject { get; }

		// Token: 0x0600CC53 RID: 52307 RVA: 0x002B926C File Offset: 0x002B746C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonNot(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CC54 RID: 52308 RVA: 0x002B927F File Offset: 0x002B747F
		protected override string ToCodeString()
		{
			return string.Format("~{0}", this.Subject);
		}
	}
}
