using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001903 RID: 6403
	internal class PowerAutomateSubstring : PowerAutomateFunc
	{
		// Token: 0x0600D0E1 RID: 53473 RVA: 0x002C8D28 File Offset: 0x002C6F28
		public PowerAutomateSubstring(FormulaExpression subject, FormulaExpression startIndex, FormulaExpression length)
			: base("Substring", Array.Empty<FormulaExpression>())
		{
			this.Subject = subject;
			this.StartIndex = startIndex;
			this.Length = length;
			base.Arguments = new FormulaExpression[] { this.Subject, this.StartIndex, this.Length };
			base.Children = base.Arguments;
		}

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x0600D0E2 RID: 53474 RVA: 0x002C8D8D File Offset: 0x002C6F8D
		public FormulaExpression Length { get; }

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x0600D0E3 RID: 53475 RVA: 0x002C8D95 File Offset: 0x002C6F95
		public FormulaExpression StartIndex { get; }

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x0600D0E4 RID: 53476 RVA: 0x002C8D9D File Offset: 0x002C6F9D
		public FormulaExpression Subject { get; }

		// Token: 0x0600D0E5 RID: 53477 RVA: 0x002C8DA5 File Offset: 0x002C6FA5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateSubstring(this.Subject.Accept<FormulaExpression>(visitor), this.StartIndex.Accept<FormulaExpression>(visitor), this.Length.Accept<FormulaExpression>(visitor));
		}
	}
}
