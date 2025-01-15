using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001803 RID: 6147
	public class FormulaTransformNodeInfo
	{
		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x0600CA19 RID: 51737 RVA: 0x002B3747 File Offset: 0x002B1947
		// (set) Token: 0x0600CA1A RID: 51738 RVA: 0x002B374F File Offset: 0x002B194F
		public IReadOnlyList<FormulaTransformNodeInfo> Ancestors { get; set; }

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x0600CA1B RID: 51739 RVA: 0x002B3758 File Offset: 0x002B1958
		// (set) Token: 0x0600CA1C RID: 51740 RVA: 0x002B3760 File Offset: 0x002B1960
		public int Depth { get; set; }

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x0600CA1D RID: 51741 RVA: 0x002B3769 File Offset: 0x002B1969
		// (set) Token: 0x0600CA1E RID: 51742 RVA: 0x002B3771 File Offset: 0x002B1971
		public FormulaExpression Node { get; set; }

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x0600CA1F RID: 51743 RVA: 0x002B377A File Offset: 0x002B197A
		// (set) Token: 0x0600CA20 RID: 51744 RVA: 0x002B3782 File Offset: 0x002B1982
		public int Order { get; set; }

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x0600CA21 RID: 51745 RVA: 0x002B378B File Offset: 0x002B198B
		public FormulaTransformNodeInfo Parent
		{
			get
			{
				IReadOnlyList<FormulaTransformNodeInfo> ancestors = this.Ancestors;
				if (ancestors == null)
				{
					return null;
				}
				return ancestors.LastOrDefault<FormulaTransformNodeInfo>();
			}
		}
	}
}
