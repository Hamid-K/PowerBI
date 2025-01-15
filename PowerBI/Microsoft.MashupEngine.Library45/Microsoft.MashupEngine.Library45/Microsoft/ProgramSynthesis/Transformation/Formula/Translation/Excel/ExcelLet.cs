using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191B RID: 6427
	internal class ExcelLet : ExcelFunc
	{
		// Token: 0x0600D1E7 RID: 53735 RVA: 0x002CC429 File Offset: 0x002CA629
		public ExcelLet(IEnumerable<FormulaExpression> arguments)
			: base("Let", arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D1E8 RID: 53736 RVA: 0x002CC43C File Offset: 0x002CA63C
		public ExcelLet(string argumentSeparator, IEnumerable<FormulaExpression> arguments)
			: base("Let", argumentSeparator, arguments.ToArray<FormulaExpression>())
		{
		}
	}
}
