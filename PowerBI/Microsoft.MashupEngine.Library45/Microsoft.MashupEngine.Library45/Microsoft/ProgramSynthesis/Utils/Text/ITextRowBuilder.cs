using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200052B RID: 1323
	public interface ITextRowBuilder
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001DA8 RID: 7592
		IReadOnlyList<ITextRow> Rows { get; }

		// Token: 0x06001DA9 RID: 7593
		ITextRowBuilder AddBorderRow();

		// Token: 0x06001DAA RID: 7594
		ITextRowBuilder AddDataRow(params object[] cells);

		// Token: 0x06001DAB RID: 7595
		ITextRowBuilder AddDataRow(IReadOnlyList<object> row, int? rowId = null);

		// Token: 0x06001DAC RID: 7596
		ITextRowBuilder AddDataRows(IReadOnlyList<IReadOnlyList<object>> rows, int? startRowId = null);

		// Token: 0x06001DAD RID: 7597
		ITextRowBuilder AddDoubleBorderRow();

		// Token: 0x06001DAE RID: 7598
		ITextRowBuilder AddEllipsisRow(string indicator = "...", int indicatorColumnIndex = 0);

		// Token: 0x06001DAF RID: 7599
		ITextRowBuilder AddHeadingRow();

		// Token: 0x06001DB0 RID: 7600
		ITextRowBuilder ClearRows();

		// Token: 0x06001DB1 RID: 7601
		ITextRowBuilder InsertRow(int index, ITextRow row);

		// Token: 0x06001DB2 RID: 7602
		ITextRowBuilder RemoveRow(int index);

		// Token: 0x06001DB3 RID: 7603
		string Render();
	}
}
