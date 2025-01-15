using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001944 RID: 6468
	internal class CSharpFunc : FormulaFunc, IFormulaTyped
	{
		// Token: 0x0600D382 RID: 54146 RVA: 0x002D189F File Offset: 0x002CFA9F
		public CSharpFunc(string name, Type returnType, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
			this.Type = returnType;
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x0600D383 RID: 54147 RVA: 0x002D18B5 File Offset: 0x002CFAB5
		public Type Type { get; }

		// Token: 0x0600D384 RID: 54148 RVA: 0x002D18BD File Offset: 0x002CFABD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpFunc(base.Name, this.Type, base.Children.Accept(visitor));
		}

		// Token: 0x0600D385 RID: 54149 RVA: 0x002D18DC File Offset: 0x002CFADC
		public CSharpFunc With(string name = null, Type returnType = null, IEnumerable<FormulaExpression> arguments = null)
		{
			return new CSharpFunc(name ?? base.Name, returnType ?? this.Type, arguments ?? base.Children);
		}
	}
}
