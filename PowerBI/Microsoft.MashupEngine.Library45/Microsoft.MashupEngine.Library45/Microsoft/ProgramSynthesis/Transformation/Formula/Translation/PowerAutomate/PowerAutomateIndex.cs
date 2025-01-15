using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001909 RID: 6409
	internal class PowerAutomateIndex : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600D0F6 RID: 53494 RVA: 0x002C8ECF File Offset: 0x002C70CF
		public PowerAutomateIndex(FormulaExpression subject, FormulaExpression instance)
		{
			this.Subject = subject;
			this.Instance = instance;
			base.Children = new FormulaExpression[] { this.Subject, this.Instance };
		}

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x0600D0F7 RID: 53495 RVA: 0x002C8F03 File Offset: 0x002C7103
		public FormulaExpression Instance { get; }

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x0600D0F8 RID: 53496 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x0600D0F9 RID: 53497 RVA: 0x002C8F0B File Offset: 0x002C710B
		public FormulaExpression Subject { get; }

		// Token: 0x0600D0FA RID: 53498 RVA: 0x002C8F13 File Offset: 0x002C7113
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateIndex(this.Subject.Accept<FormulaExpression>(visitor), this.Instance.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D0FB RID: 53499 RVA: 0x002C8F32 File Offset: 0x002C7132
		protected override string ToCodeString()
		{
			return string.Format("{0}[{1}]", this.Subject, this.Instance);
		}
	}
}
