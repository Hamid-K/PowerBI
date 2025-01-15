using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001948 RID: 6472
	internal class CSharpStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600D38F RID: 54159 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public CSharpStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600D390 RID: 54160 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D391 RID: 54161 RVA: 0x002D198E File Offset: 0x002CFB8E
		protected override string ToCodeString()
		{
			return base.Value.ToCSharpLiteral();
		}
	}
}
