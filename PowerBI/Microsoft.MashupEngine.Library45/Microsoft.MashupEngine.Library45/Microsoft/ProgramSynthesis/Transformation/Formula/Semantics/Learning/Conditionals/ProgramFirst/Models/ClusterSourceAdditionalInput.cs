using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001725 RID: 5925
	public class ClusterSourceAdditionalInput
	{
		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x0600C531 RID: 50481 RVA: 0x002A705B File Offset: 0x002A525B
		// (set) Token: 0x0600C532 RID: 50482 RVA: 0x002A7063 File Offset: 0x002A5263
		public int Position { get; set; }

		// Token: 0x0600C533 RID: 50483 RVA: 0x002A706C File Offset: 0x002A526C
		public override string ToString()
		{
			return string.Format("{0,2}: {1}", this.Position, this.Input);
		}

		// Token: 0x04004D37 RID: 19767
		public IRow Input;
	}
}
