using System;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200192B RID: 6443
	internal class ExcelDigitRange : ExcelArray
	{
		// Token: 0x0600D218 RID: 53784 RVA: 0x002CC6FC File Offset: 0x002CA8FC
		public ExcelDigitRange()
		{
			base.Items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Select((int d) => new ExcelNumberLiteral((double)d)).ToList<ExcelNumberLiteral>();
			base.Children = base.Items;
		}
	}
}
