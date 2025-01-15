using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001966 RID: 6502
	internal class CSharpStringRange : FormulaExpression
	{
		// Token: 0x0600D3FB RID: 54267 RVA: 0x002D223E File Offset: 0x002D043E
		public CSharpStringRange(FormulaExpression start, FormulaExpression end)
		{
			this.Start = start;
			this.End = end;
			base.Children = new FormulaExpression[] { this.Start, this.End };
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x0600D3FC RID: 54268 RVA: 0x002D2272 File Offset: 0x002D0472
		public FormulaExpression End { get; }

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x0600D3FD RID: 54269 RVA: 0x002D227A File Offset: 0x002D047A
		public FormulaExpression Start { get; }

		// Token: 0x0600D3FE RID: 54270 RVA: 0x002D2282 File Offset: 0x002D0482
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpEqual(this.Start.Accept<FormulaExpression>(visitor), this.End.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D3FF RID: 54271 RVA: 0x002D22A4 File Offset: 0x002D04A4
		protected override string ToCodeString()
		{
			CSharpIntLiteral csharpIntLiteral = this.Start as CSharpIntLiteral;
			string text;
			if (csharpIntLiteral == null || csharpIntLiteral.Value >= 0.0)
			{
				FormulaExpression start = this.Start;
				text = ((start != null) ? start.ToString() : null);
			}
			else
			{
				text = string.Format("^{0}", -csharpIntLiteral.Value);
			}
			CSharpIntLiteral csharpIntLiteral2 = this.End as CSharpIntLiteral;
			string text2;
			if (csharpIntLiteral2 == null || csharpIntLiteral2.Value >= 0.0)
			{
				FormulaExpression end = this.End;
				text2 = ((end != null) ? end.ToString() : null);
			}
			else
			{
				text2 = string.Format("^{0}", -csharpIntLiteral2.Value);
			}
			string text3 = text2;
			return text + ".." + text3;
		}
	}
}
