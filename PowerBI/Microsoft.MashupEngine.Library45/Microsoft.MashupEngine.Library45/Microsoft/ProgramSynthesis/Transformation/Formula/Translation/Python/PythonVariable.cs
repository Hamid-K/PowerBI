using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187A RID: 6266
	internal class PythonVariable : FormulaVariable, IFormulaTyped
	{
		// Token: 0x0600CC42 RID: 52290 RVA: 0x002B9178 File Offset: 0x002B7378
		public PythonVariable(string name, Type type)
			: base(name)
		{
			this.Type = type;
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x0600CC43 RID: 52291 RVA: 0x002B9188 File Offset: 0x002B7388
		public Type Type { get; }

		// Token: 0x0600CC44 RID: 52292 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CC45 RID: 52293 RVA: 0x002B9190 File Offset: 0x002B7390
		public override FormulaExpression CloneWith(string name)
		{
			return new PythonVariable(name, this.Type);
		}
	}
}
