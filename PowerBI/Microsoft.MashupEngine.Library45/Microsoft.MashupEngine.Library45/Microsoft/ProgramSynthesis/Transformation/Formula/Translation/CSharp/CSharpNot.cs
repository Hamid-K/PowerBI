using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200196D RID: 6509
	internal class CSharpNot : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600D41D RID: 54301 RVA: 0x002D2568 File Offset: 0x002D0768
		public CSharpNot(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x0600D41E RID: 54302 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x0600D41F RID: 54303 RVA: 0x002D258C File Offset: 0x002D078C
		public FormulaExpression Subject { get; }

		// Token: 0x0600D420 RID: 54304 RVA: 0x002D2594 File Offset: 0x002D0794
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpNot(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D421 RID: 54305 RVA: 0x002D25A7 File Offset: 0x002D07A7
		protected override string ToCodeString()
		{
			return string.Format("!{0}", this.Subject);
		}
	}
}
