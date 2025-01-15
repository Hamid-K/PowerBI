using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001970 RID: 6512
	internal class CSharpNull : FormulaExpression
	{
		// Token: 0x0600D429 RID: 54313 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D42A RID: 54314 RVA: 0x002BF808 File Offset: 0x002BDA08
		protected override string ToCodeString()
		{
			return "null";
		}
	}
}
