using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185A RID: 6234
	internal class PythonBooleanLiteral : FormulaBooleanLiteral
	{
		// Token: 0x0600CBD8 RID: 52184 RVA: 0x002B89D6 File Offset: 0x002B6BD6
		public PythonBooleanLiteral(bool value)
			: base(value)
		{
		}

		// Token: 0x0600CBD9 RID: 52185 RVA: 0x002B89DF File Offset: 0x002B6BDF
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonBooleanLiteral(base.Value);
		}

		// Token: 0x0600CBDA RID: 52186 RVA: 0x002B89EC File Offset: 0x002B6BEC
		protected override string ToCodeString()
		{
			if (!base.Value)
			{
				return "False";
			}
			return "True";
		}
	}
}
