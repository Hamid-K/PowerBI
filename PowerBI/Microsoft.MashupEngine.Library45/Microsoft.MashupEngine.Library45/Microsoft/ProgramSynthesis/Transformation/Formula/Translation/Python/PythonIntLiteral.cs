using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001857 RID: 6231
	internal class PythonIntLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CBCC RID: 52172 RVA: 0x002B88EE File Offset: 0x002B6AEE
		public PythonIntLiteral(int value)
			: base(Convert.ToDouble(value))
		{
			this.Type = typeof(int);
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x0600CBCD RID: 52173 RVA: 0x002B890C File Offset: 0x002B6B0C
		public override Type Type { get; }

		// Token: 0x0600CBCE RID: 52174 RVA: 0x002B8914 File Offset: 0x002B6B14
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonIntLiteral(Convert.ToInt32(base.Value));
		}
	}
}
