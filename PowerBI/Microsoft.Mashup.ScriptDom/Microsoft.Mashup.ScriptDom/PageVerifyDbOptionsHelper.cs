using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200008B RID: 139
	internal class PageVerifyDbOptionsHelper : OptionsHelper<PageVerifyDatabaseOptionKind>
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000B72B File Offset: 0x0000992B
		private PageVerifyDbOptionsHelper()
		{
			base.AddOptionMapping(PageVerifyDatabaseOptionKind.None, "NONE");
			base.AddOptionMapping(PageVerifyDatabaseOptionKind.Checksum, "CHECKSUM");
			base.AddOptionMapping(PageVerifyDatabaseOptionKind.TornPageDetection, "TORN_PAGE_DETECTION");
		}

		// Token: 0x04000379 RID: 889
		internal static readonly PageVerifyDbOptionsHelper Instance = new PageVerifyDbOptionsHelper();
	}
}
