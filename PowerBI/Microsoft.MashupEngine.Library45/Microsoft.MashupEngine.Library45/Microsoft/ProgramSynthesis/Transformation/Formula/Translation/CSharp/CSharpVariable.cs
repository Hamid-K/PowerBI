using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200196A RID: 6506
	internal class CSharpVariable : FormulaVariable, IFormulaTyped
	{
		// Token: 0x0600D40E RID: 54286 RVA: 0x002D247F File Offset: 0x002D067F
		public CSharpVariable(string name, Type type)
			: base(name)
		{
			this.Type = type;
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x0600D40F RID: 54287 RVA: 0x002D248F File Offset: 0x002D068F
		public Type Type { get; }

		// Token: 0x0600D410 RID: 54288 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D411 RID: 54289 RVA: 0x002D2497 File Offset: 0x002D0697
		public override FormulaExpression CloneWith(string name)
		{
			return new CSharpVariable(name, this.Type);
		}
	}
}
