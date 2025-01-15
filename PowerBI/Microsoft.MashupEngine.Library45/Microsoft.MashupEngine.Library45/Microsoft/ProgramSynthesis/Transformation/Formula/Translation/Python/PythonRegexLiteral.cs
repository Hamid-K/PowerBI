using System;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001855 RID: 6229
	internal class PythonRegexLiteral : FormulaRegexLiteral
	{
		// Token: 0x0600CBC6 RID: 52166 RVA: 0x002B88CB File Offset: 0x002B6ACB
		public PythonRegexLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CBC7 RID: 52167 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CBC8 RID: 52168 RVA: 0x002B88D4 File Offset: 0x002B6AD4
		protected override string ToCodeString()
		{
			return base.Value.ToPythonLiteral();
		}
	}
}
