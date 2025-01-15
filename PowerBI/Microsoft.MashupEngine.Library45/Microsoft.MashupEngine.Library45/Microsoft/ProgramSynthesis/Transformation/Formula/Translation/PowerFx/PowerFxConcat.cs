using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DD RID: 6365
	internal class PowerFxConcat : FormulaConcat
	{
		// Token: 0x0600CF8C RID: 53132 RVA: 0x002B48DD File Offset: 0x002B2ADD
		public PowerFxConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x0600CF8D RID: 53133 RVA: 0x002BF95D File Offset: 0x002BDB5D
		public override string Symbol
		{
			get
			{
				return "&";
			}
		}

		// Token: 0x0600CF8E RID: 53134 RVA: 0x002C42EB File Offset: 0x002C24EB
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
