using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186A RID: 6250
	internal class PythonDot : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600CC09 RID: 52233 RVA: 0x002B8DA0 File Offset: 0x002B6FA0
		public PythonDot(FormulaExpression subject, FormulaExpression accessor)
		{
			this.Subject = subject;
			this.Accessor = accessor;
			base.Children = new FormulaExpression[] { subject, accessor };
			IFormulaTyped formulaTyped = accessor as IFormulaTyped;
			if (formulaTyped != null)
			{
				this.Type = formulaTyped.Type;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x0600CC0A RID: 52234 RVA: 0x002B8DEB File Offset: 0x002B6FEB
		public FormulaExpression Accessor { get; }

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x0600CC0B RID: 52235 RVA: 0x002B8DF3 File Offset: 0x002B6FF3
		public FormulaExpression Subject { get; }

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x0600CC0C RID: 52236 RVA: 0x002B8DFB File Offset: 0x002B6FFB
		public Type Type { get; }

		// Token: 0x0600CC0D RID: 52237 RVA: 0x002B8E03 File Offset: 0x002B7003
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDot(this.Subject.Accept<FormulaExpression>(visitor), this.Accessor.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CC0E RID: 52238 RVA: 0x002B8E22 File Offset: 0x002B7022
		protected override string ToCodeString()
		{
			return string.Format("{0}.{1}", this.Subject, this.Accessor);
		}
	}
}
