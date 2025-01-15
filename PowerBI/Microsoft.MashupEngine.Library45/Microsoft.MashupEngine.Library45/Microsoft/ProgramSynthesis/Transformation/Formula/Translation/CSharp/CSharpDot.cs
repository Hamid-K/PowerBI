using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001958 RID: 6488
	internal class CSharpDot : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600D3C9 RID: 54217 RVA: 0x002D1EB4 File Offset: 0x002D00B4
		public CSharpDot(FormulaExpression subject, FormulaExpression accessor, bool conditionalAccess)
		{
			this.Subject = subject;
			this.Accessor = accessor;
			this.ConditionalAccess = conditionalAccess;
			base.Children = new FormulaExpression[] { subject, accessor };
			IFormulaTyped formulaTyped = accessor as IFormulaTyped;
			if (formulaTyped != null)
			{
				this.Type = formulaTyped.Type;
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x0600D3CA RID: 54218 RVA: 0x002D1F06 File Offset: 0x002D0106
		public FormulaExpression Accessor { get; }

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x0600D3CB RID: 54219 RVA: 0x002D1F0E File Offset: 0x002D010E
		public bool ConditionalAccess { get; }

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x0600D3CC RID: 54220 RVA: 0x002D1F16 File Offset: 0x002D0116
		public FormulaExpression Subject { get; }

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x0600D3CD RID: 54221 RVA: 0x002D1F1E File Offset: 0x002D011E
		public Type Type { get; }

		// Token: 0x0600D3CE RID: 54222 RVA: 0x002D1F26 File Offset: 0x002D0126
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpDot(this.Subject.Accept<FormulaExpression>(visitor), this.Accessor.Accept<FormulaExpression>(visitor), this.ConditionalAccess);
		}

		// Token: 0x0600D3CF RID: 54223 RVA: 0x002D1F4B File Offset: 0x002D014B
		protected override string ToCodeString()
		{
			if (!this.ConditionalAccess)
			{
				return string.Format("{0}.{1}", this.Subject, this.Accessor);
			}
			return string.Format("{0}?.{1}", this.Subject, this.Accessor);
		}
	}
}
