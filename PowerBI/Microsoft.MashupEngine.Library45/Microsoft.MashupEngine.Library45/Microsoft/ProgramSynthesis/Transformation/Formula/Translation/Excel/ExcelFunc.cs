using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191A RID: 6426
	internal class ExcelFunc : FormulaFunc
	{
		// Token: 0x0600D1E2 RID: 53730 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public ExcelFunc(string name, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D1E3 RID: 53731 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public ExcelFunc(string name, params FormulaExpression[] arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D1E4 RID: 53732 RVA: 0x002B47E8 File Offset: 0x002B29E8
		public ExcelFunc(string name, string argumentSeparator, params FormulaExpression[] arguments)
			: base(name, argumentSeparator, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D1E5 RID: 53733 RVA: 0x002B47E8 File Offset: 0x002B29E8
		public ExcelFunc(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
			: base(name, argumentSeparator, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D1E6 RID: 53734 RVA: 0x002CC40A File Offset: 0x002CA60A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelFunc(base.Name, base.ArgumentSeparator, base.Children.Accept(visitor));
		}
	}
}
