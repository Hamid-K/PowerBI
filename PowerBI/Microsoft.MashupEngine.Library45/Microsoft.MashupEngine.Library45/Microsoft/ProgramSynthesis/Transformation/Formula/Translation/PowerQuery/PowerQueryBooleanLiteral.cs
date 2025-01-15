using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189F RID: 6303
	internal class PowerQueryBooleanLiteral : FormulaBooleanLiteral
	{
		// Token: 0x0600CDF3 RID: 52723 RVA: 0x002B89D6 File Offset: 0x002B6BD6
		public PowerQueryBooleanLiteral(bool value)
			: base(value)
		{
		}

		// Token: 0x0600CDF4 RID: 52724 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CDF5 RID: 52725 RVA: 0x002BF7F3 File Offset: 0x002BD9F3
		protected override string ToCodeString()
		{
			if (!base.Value)
			{
				return "false";
			}
			return "true";
		}
	}
}
