using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200010B RID: 267
	internal class BulkInsertIntOptionsHelper : OptionsHelper<BulkInsertOptionKind>
	{
		// Token: 0x0600149A RID: 5274 RVA: 0x000906C8 File Offset: 0x0008E8C8
		private BulkInsertIntOptionsHelper()
		{
			base.AddOptionMapping(BulkInsertOptionKind.MaxErrors, "MAXERRORS");
			base.AddOptionMapping(BulkInsertOptionKind.FirstRow, "FIRSTROW");
			base.AddOptionMapping(BulkInsertOptionKind.LastRow, "LASTROW");
			base.AddOptionMapping(BulkInsertOptionKind.BatchSize, "BATCHSIZE");
			base.AddOptionMapping(BulkInsertOptionKind.CodePage, "CODEPAGE");
			base.AddOptionMapping(BulkInsertOptionKind.RowsPerBatch, "ROWS_PER_BATCH");
			base.AddOptionMapping(BulkInsertOptionKind.KilobytesPerBatch, "KILOBYTES_PER_BATCH");
		}

		// Token: 0x04000B7B RID: 2939
		internal static readonly BulkInsertIntOptionsHelper Instance = new BulkInsertIntOptionsHelper();
	}
}
