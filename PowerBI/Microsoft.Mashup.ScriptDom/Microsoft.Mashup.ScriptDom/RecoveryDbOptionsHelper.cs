using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	internal class RecoveryDbOptionsHelper : OptionsHelper<RecoveryDatabaseOptionKind>
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000BB4D File Offset: 0x00009D4D
		private RecoveryDbOptionsHelper()
		{
			base.AddOptionMapping(RecoveryDatabaseOptionKind.Full, "FULL");
			base.AddOptionMapping(RecoveryDatabaseOptionKind.BulkLogged, "BULK_LOGGED");
			base.AddOptionMapping(RecoveryDatabaseOptionKind.Simple, "SIMPLE");
		}

		// Token: 0x040003E6 RID: 998
		internal static readonly RecoveryDbOptionsHelper Instance = new RecoveryDbOptionsHelper();
	}
}
