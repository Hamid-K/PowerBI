using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200052C RID: 1324
	public interface ITextColumn
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001DB4 RID: 7604
		// (set) Token: 0x06001DB5 RID: 7605
		bool First { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001DB6 RID: 7606
		// (set) Token: 0x06001DB7 RID: 7607
		int Index { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001DB8 RID: 7608
		// (set) Token: 0x06001DB9 RID: 7609
		bool Last { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001DBA RID: 7610
		// (set) Token: 0x06001DBB RID: 7611
		int Width { get; set; }

		// Token: 0x06001DBC RID: 7612
		IReadOnlyList<string> Lines(ITextRow row);

		// Token: 0x06001DBD RID: 7613
		void Render(StringBuilder output, ITextRow row, int partitionIndex);
	}
}
