using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001850 RID: 6224
	internal class PythonFunc : FormulaFunc, IFormulaTyped
	{
		// Token: 0x0600CBB1 RID: 52145 RVA: 0x002B867C File Offset: 0x002B687C
		public PythonFunc(string name, Type returnType, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments)
		{
			this.Type = returnType;
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x0600CBB2 RID: 52146 RVA: 0x002B868D File Offset: 0x002B688D
		public Type Type { get; }

		// Token: 0x0600CBB3 RID: 52147 RVA: 0x002B8695 File Offset: 0x002B6895
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonFunc(base.Name, this.Type, base.Children.Accept(visitor));
		}

		// Token: 0x0600CBB4 RID: 52148 RVA: 0x002B86B4 File Offset: 0x002B68B4
		public PythonFunc With(string name = null, Type type = null, IEnumerable<FormulaExpression> arguments = null)
		{
			return new PythonFunc(name ?? base.Name, type ?? this.Type, arguments ?? base.Children);
		}
	}
}
