using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000536 RID: 1334
	public interface ITextRow
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001E0F RID: 7695
		// (set) Token: 0x06001E10 RID: 7696
		bool First { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001E11 RID: 7697
		// (set) Token: 0x06001E12 RID: 7698
		int? Id { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001E13 RID: 7699
		// (set) Token: 0x06001E14 RID: 7700
		int Index { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001E15 RID: 7701
		// (set) Token: 0x06001E16 RID: 7702
		bool Last { get; set; }

		// Token: 0x06001E17 RID: 7703
		void Render(StringBuilder output, IReadOnlyList<ITextColumn> columns);
	}
}
