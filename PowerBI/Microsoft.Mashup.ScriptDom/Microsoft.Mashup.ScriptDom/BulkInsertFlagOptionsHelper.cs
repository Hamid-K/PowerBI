using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200010A RID: 266
	internal class BulkInsertFlagOptionsHelper : OptionsHelper<BulkInsertOptionKind>
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x0009065C File Offset: 0x0008E85C
		private BulkInsertFlagOptionsHelper()
		{
			base.AddOptionMapping(BulkInsertOptionKind.NoTriggers, "NO_TRIGGERS");
			base.AddOptionMapping(BulkInsertOptionKind.KeepIdentity, "KEEPIDENTITY");
			base.AddOptionMapping(BulkInsertOptionKind.KeepNulls, "KEEPNULLS");
			base.AddOptionMapping(BulkInsertOptionKind.TabLock, "TABLOCK");
			base.AddOptionMapping(BulkInsertOptionKind.CheckConstraints, "CHECK_CONSTRAINTS");
			base.AddOptionMapping(BulkInsertOptionKind.FireTriggers, "FIRE_TRIGGERS");
		}

		// Token: 0x04000B7A RID: 2938
		internal static readonly BulkInsertFlagOptionsHelper Instance = new BulkInsertFlagOptionsHelper();
	}
}
