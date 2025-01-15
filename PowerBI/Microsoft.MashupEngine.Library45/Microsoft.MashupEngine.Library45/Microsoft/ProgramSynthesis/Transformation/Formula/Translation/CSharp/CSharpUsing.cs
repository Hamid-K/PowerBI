using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193B RID: 6459
	internal class CSharpUsing : FormulaExpression
	{
		// Token: 0x0600D34D RID: 54093 RVA: 0x002D1104 File Offset: 0x002CF304
		public CSharpUsing(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x0600D34E RID: 54094 RVA: 0x002D1113 File Offset: 0x002CF313
		public string Name { get; }

		// Token: 0x0600D34F RID: 54095 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D350 RID: 54096 RVA: 0x002D111B File Offset: 0x002CF31B
		protected override string ToCodeString()
		{
			return "using " + this.Name + ";";
		}
	}
}
