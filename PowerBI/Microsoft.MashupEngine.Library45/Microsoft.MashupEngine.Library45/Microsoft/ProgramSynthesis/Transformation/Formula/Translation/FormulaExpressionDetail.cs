using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017FE RID: 6142
	public class FormulaExpressionDetail
	{
		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x0600CA02 RID: 51714 RVA: 0x002B3538 File Offset: 0x002B1738
		// (set) Token: 0x0600CA03 RID: 51715 RVA: 0x002B3540 File Offset: 0x002B1740
		public IReadOnlyList<FormulaExpressionDetail> Ancestors { get; set; }

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x0600CA04 RID: 51716 RVA: 0x002B3549 File Offset: 0x002B1749
		// (set) Token: 0x0600CA05 RID: 51717 RVA: 0x002B3551 File Offset: 0x002B1751
		public int Depth { get; set; }

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x0600CA06 RID: 51718 RVA: 0x002B355A File Offset: 0x002B175A
		// (set) Token: 0x0600CA07 RID: 51719 RVA: 0x002B3562 File Offset: 0x002B1762
		public FormulaExpression Node { get; set; }

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x0600CA08 RID: 51720 RVA: 0x002B356B File Offset: 0x002B176B
		// (set) Token: 0x0600CA09 RID: 51721 RVA: 0x002B3573 File Offset: 0x002B1773
		public int Order { get; set; }

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x0600CA0A RID: 51722 RVA: 0x002B357C File Offset: 0x002B177C
		public FormulaExpressionDetail Parent
		{
			get
			{
				IReadOnlyList<FormulaExpressionDetail> ancestors = this.Ancestors;
				if (ancestors == null)
				{
					return null;
				}
				return ancestors.LastOrDefault<FormulaExpressionDetail>();
			}
		}
	}
}
