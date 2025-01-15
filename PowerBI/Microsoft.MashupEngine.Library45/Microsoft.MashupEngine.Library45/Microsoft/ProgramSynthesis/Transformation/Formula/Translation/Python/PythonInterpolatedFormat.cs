using System;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001853 RID: 6227
	internal class PythonInterpolatedFormat : FormulaExpression
	{
		// Token: 0x0600CBBD RID: 52157 RVA: 0x002B87A4 File Offset: 0x002B69A4
		public PythonInterpolatedFormat(FormulaExpression input, FormulaExpression mask, string currencySymbol, bool currencyPrefix)
		{
			this.Mask = mask;
			this.Input = input;
			this.CurrencySymbol = currencySymbol;
			this.CurrencyPrefix = currencyPrefix;
			base.Children = new FormulaExpression[] { this.Input, this.Mask };
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x0600CBBE RID: 52158 RVA: 0x002B87F2 File Offset: 0x002B69F2
		public bool CurrencyPrefix { get; }

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x0600CBBF RID: 52159 RVA: 0x002B87FA File Offset: 0x002B69FA
		public string CurrencySymbol { get; }

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x0600CBC0 RID: 52160 RVA: 0x002B8802 File Offset: 0x002B6A02
		public FormulaExpression Input { get; }

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x0600CBC1 RID: 52161 RVA: 0x002B880A File Offset: 0x002B6A0A
		public FormulaExpression Mask { get; }

		// Token: 0x0600CBC2 RID: 52162 RVA: 0x002B8812 File Offset: 0x002B6A12
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonInterpolatedFormat(this.Input.Accept<FormulaExpression>(visitor), this.Mask.Accept<FormulaExpression>(visitor), this.CurrencySymbol, this.CurrencyPrefix);
		}

		// Token: 0x0600CBC3 RID: 52163 RVA: 0x002B8840 File Offset: 0x002B6A40
		protected override string ToCodeString()
		{
			string text = null;
			string text2 = null;
			if (this.CurrencySymbol != null)
			{
				text = (this.CurrencyPrefix ? this.CurrencySymbol : null);
				text2 = ((!this.CurrencyPrefix) ? this.CurrencySymbol : null);
			}
			string text3 = string.Format("{0}{{{1}:{2}}}{3}", new object[] { text, this.Input, this.Mask, text2 });
			return "f" + text3.ToPythonLiteral();
		}
	}
}
