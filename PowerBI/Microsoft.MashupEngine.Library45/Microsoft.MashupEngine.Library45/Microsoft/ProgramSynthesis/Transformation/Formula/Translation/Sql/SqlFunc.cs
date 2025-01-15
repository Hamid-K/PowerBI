using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001819 RID: 6169
	internal class SqlFunc : FormulaFunc
	{
		// Token: 0x0600CA8D RID: 51853 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public SqlFunc(string name, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600CA8E RID: 51854 RVA: 0x002B47E8 File Offset: 0x002B29E8
		public SqlFunc(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
			: base(name, argumentSeparator, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600CA8F RID: 51855 RVA: 0x002B47F8 File Offset: 0x002B29F8
		public SqlFunc(string name, params FormulaExpression[] arguments)
			: base(name, arguments)
		{
		}

		// Token: 0x0600CA90 RID: 51856 RVA: 0x002B4802 File Offset: 0x002B2A02
		public SqlFunc(string name, string argumentSeparator, params FormulaExpression[] arguments)
			: base(name, argumentSeparator, arguments)
		{
		}

		// Token: 0x0600CA91 RID: 51857 RVA: 0x002B480D File Offset: 0x002B2A0D
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlFunc(base.Name, base.ArgumentSeparator, base.Children.Accept(visitor));
		}
	}
}
