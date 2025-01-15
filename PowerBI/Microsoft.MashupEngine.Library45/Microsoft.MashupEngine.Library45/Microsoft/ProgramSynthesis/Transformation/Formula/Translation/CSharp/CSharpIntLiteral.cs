using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200194A RID: 6474
	internal class CSharpIntLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600D397 RID: 54167 RVA: 0x002D19D0 File Offset: 0x002CFBD0
		public CSharpIntLiteral(int value)
			: base(Convert.ToDouble(value))
		{
			this.Type = typeof(int);
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x0600D398 RID: 54168 RVA: 0x002D19EE File Offset: 0x002CFBEE
		public override Type Type { get; }

		// Token: 0x0600D399 RID: 54169 RVA: 0x002D19F6 File Offset: 0x002CFBF6
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpIntLiteral(Convert.ToInt32(base.Value));
		}
	}
}
