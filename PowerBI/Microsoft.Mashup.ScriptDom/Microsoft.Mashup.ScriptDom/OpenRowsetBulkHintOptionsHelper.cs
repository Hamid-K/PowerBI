using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000F0 RID: 240
	internal class OpenRowsetBulkHintOptionsHelper : OptionsHelper<BulkInsertOptionKind>
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0009003B File Offset: 0x0008E23B
		private OpenRowsetBulkHintOptionsHelper()
		{
			base.AddOptionMapping(BulkInsertOptionKind.SingleBlob, "SINGLE_BLOB");
			base.AddOptionMapping(BulkInsertOptionKind.SingleClob, "SINGLE_CLOB");
			base.AddOptionMapping(BulkInsertOptionKind.SingleNClob, "SINGLE_NCLOB");
		}

		// Token: 0x04000AED RID: 2797
		internal static readonly OpenRowsetBulkHintOptionsHelper Instance = new OpenRowsetBulkHintOptionsHelper();
	}
}
