using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200194C RID: 6476
	internal class CSharpDecimalLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600D39E RID: 54174 RVA: 0x002D1A76 File Offset: 0x002CFC76
		public CSharpDecimalLiteral(decimal value)
			: base(Convert.ToDouble(value))
		{
			this.Type = typeof(decimal);
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x0600D39F RID: 54175 RVA: 0x002D1A94 File Offset: 0x002CFC94
		public override Type Type { get; }

		// Token: 0x0600D3A0 RID: 54176 RVA: 0x002D1A9C File Offset: 0x002CFC9C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpDecimalLiteral(Convert.ToDecimal(base.Value));
		}

		// Token: 0x0600D3A1 RID: 54177 RVA: 0x002D1AAE File Offset: 0x002CFCAE
		protected override string ToCodeString()
		{
			return base.ToCodeString() + "m";
		}
	}
}
