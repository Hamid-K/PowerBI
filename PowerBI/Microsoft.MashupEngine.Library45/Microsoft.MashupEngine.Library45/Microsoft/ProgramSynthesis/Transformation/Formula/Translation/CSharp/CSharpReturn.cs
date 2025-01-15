using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200196C RID: 6508
	internal class CSharpReturn : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600D418 RID: 54296 RVA: 0x002D2517 File Offset: 0x002D0717
		public CSharpReturn(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x0600D419 RID: 54297 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x0600D41A RID: 54298 RVA: 0x002D253B File Offset: 0x002D073B
		public FormulaExpression Subject { get; }

		// Token: 0x0600D41B RID: 54299 RVA: 0x002D2543 File Offset: 0x002D0743
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpReturn(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D41C RID: 54300 RVA: 0x002D2556 File Offset: 0x002D0756
		protected override string ToCodeString()
		{
			return string.Format("return {0}", this.Subject);
		}
	}
}
