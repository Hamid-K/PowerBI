using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001950 RID: 6480
	internal class CSharpUnaryMinus : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600D3B2 RID: 54194 RVA: 0x002D1D54 File Offset: 0x002CFF54
		public CSharpUnaryMinus(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { subject };
		}

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x0600D3B3 RID: 54195 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x0600D3B4 RID: 54196 RVA: 0x002D1D73 File Offset: 0x002CFF73
		public FormulaExpression Subject { get; }

		// Token: 0x0600D3B5 RID: 54197 RVA: 0x002D1D7B File Offset: 0x002CFF7B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpUnaryMinus(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D3B6 RID: 54198 RVA: 0x002D1D8E File Offset: 0x002CFF8E
		protected override string ToCodeString()
		{
			return string.Format("-{0}", this.Subject);
		}
	}
}
