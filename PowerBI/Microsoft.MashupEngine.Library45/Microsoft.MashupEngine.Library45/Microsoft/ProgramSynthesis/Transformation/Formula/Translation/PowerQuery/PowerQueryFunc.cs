using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189D RID: 6301
	internal class PowerQueryFunc : FormulaFunc, IFormulaTyped
	{
		// Token: 0x0600CDEA RID: 52714 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public PowerQueryFunc(string name, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600CDEB RID: 52715 RVA: 0x002B47F8 File Offset: 0x002B29F8
		public PowerQueryFunc(string name, params FormulaExpression[] arguments)
			: base(name, arguments)
		{
		}

		// Token: 0x0600CDEC RID: 52716 RVA: 0x002BF757 File Offset: 0x002BD957
		public PowerQueryFunc(string name, Type type, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
			this.Type = type;
		}

		// Token: 0x0600CDED RID: 52717 RVA: 0x002BF76D File Offset: 0x002BD96D
		public PowerQueryFunc(string name, Type type, params FormulaExpression[] arguments)
			: base(name, arguments)
		{
			this.Type = type;
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x0600CDEE RID: 52718 RVA: 0x002BF77E File Offset: 0x002BD97E
		public Type Type { get; }

		// Token: 0x0600CDEF RID: 52719 RVA: 0x002BF786 File Offset: 0x002BD986
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryFunc(base.Name, base.Children.Accept(visitor));
		}
	}
}
