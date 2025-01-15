using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001967 RID: 6503
	internal class CSharpIndex : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600D400 RID: 54272 RVA: 0x002D2351 File Offset: 0x002D0551
		public CSharpIndex(FormulaExpression subject, FormulaExpression index, Type type)
		{
			base.Children = new FormulaExpression[] { subject, index };
			this.Subject = subject;
			this.Index = index;
			this.Type = type;
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x0600D401 RID: 54273 RVA: 0x002D2382 File Offset: 0x002D0582
		public FormulaExpression Index { get; }

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x0600D402 RID: 54274 RVA: 0x002D238A File Offset: 0x002D058A
		public FormulaExpression Subject { get; }

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x0600D403 RID: 54275 RVA: 0x002D2392 File Offset: 0x002D0592
		public Type Type { get; }

		// Token: 0x0600D404 RID: 54276 RVA: 0x002D239A File Offset: 0x002D059A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpIndex(this.Subject.Accept<FormulaExpression>(visitor), this.Index.Accept<FormulaExpression>(visitor), this.Type);
		}

		// Token: 0x0600D405 RID: 54277 RVA: 0x002D23C0 File Offset: 0x002D05C0
		protected override string ToCodeString()
		{
			CSharpIntLiteral csharpIntLiteral = this.Index as CSharpIntLiteral;
			string text;
			if (csharpIntLiteral == null || csharpIntLiteral.Value >= 0.0)
			{
				FormulaExpression index = this.Index;
				text = ((index != null) ? index.ToString() : null);
			}
			else
			{
				text = string.Format("^{0}", -csharpIntLiteral.Value);
			}
			string text2 = text;
			return string.Format("{0}[{1}]", this.Subject, text2);
		}
	}
}
