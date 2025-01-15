using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D3 RID: 6355
	internal class PowerFxFunc : FormulaFunc
	{
		// Token: 0x0600CF70 RID: 53104 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public PowerFxFunc(string name, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600CF71 RID: 53105 RVA: 0x002B47F8 File Offset: 0x002B29F8
		public PowerFxFunc(string name, params FormulaExpression[] arguments)
			: base(name, arguments)
		{
		}

		// Token: 0x0600CF72 RID: 53106 RVA: 0x002B4802 File Offset: 0x002B2A02
		public PowerFxFunc(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
			: base(name, argumentSeparator, arguments)
		{
		}

		// Token: 0x0600CF73 RID: 53107 RVA: 0x002C4023 File Offset: 0x002C2223
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxFunc(base.Name, base.ArgumentSeparator, base.Children.Accept(visitor));
		}
	}
}
