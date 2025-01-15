using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018ED RID: 6381
	internal class PowerFxAccessor : FormulaExpression
	{
		// Token: 0x0600CFC3 RID: 53187 RVA: 0x002C4575 File Offset: 0x002C2775
		public PowerFxAccessor(string name)
		{
			this.Name = name;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x0600CFC4 RID: 53188 RVA: 0x002C4590 File Offset: 0x002C2790
		public string Name { get; }

		// Token: 0x0600CFC5 RID: 53189 RVA: 0x002C4598 File Offset: 0x002C2798
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxAccessor(this.Name);
		}

		// Token: 0x0600CFC6 RID: 53190 RVA: 0x002C45A5 File Offset: 0x002C27A5
		protected override string ToCodeString()
		{
			return this.Name;
		}
	}
}
