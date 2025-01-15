using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200010C RID: 268
	internal class BulkInsertStringOptionsHelper : OptionsHelper<BulkInsertOptionKind>
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x00090740 File Offset: 0x0008E940
		private BulkInsertStringOptionsHelper()
		{
			base.AddOptionMapping(BulkInsertOptionKind.FieldTerminator, "FIELDTERMINATOR");
			base.AddOptionMapping(BulkInsertOptionKind.RowTerminator, "ROWTERMINATOR");
			base.AddOptionMapping(BulkInsertOptionKind.FormatFile, "FORMATFILE");
			base.AddOptionMapping(BulkInsertOptionKind.ErrorFile, "ERRORFILE");
			base.AddOptionMapping(BulkInsertOptionKind.CodePage, "CODEPAGE");
			base.AddOptionMapping(BulkInsertOptionKind.DataFileType, "DATAFILETYPE");
		}

		// Token: 0x04000B7C RID: 2940
		internal static readonly BulkInsertStringOptionsHelper Instance = new BulkInsertStringOptionsHelper();
	}
}
