using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	internal class SimpleDbOptionsHelper : OptionsHelper<DatabaseOptionKind>
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		private SimpleDbOptionsHelper()
		{
			base.AddOptionMapping(DatabaseOptionKind.Online, "ONLINE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.Offline, "OFFLINE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.SingleUser, "SINGLE_USER", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.RestrictedUser, "RESTRICTED_USER", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.MultiUser, "MULTI_USER", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.ReadOnly, "READ_ONLY", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.ReadWrite, "READ_WRITE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(DatabaseOptionKind.Emergency, "EMERGENCY", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(DatabaseOptionKind.EnableBroker, "ENABLE_BROKER", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(DatabaseOptionKind.DisableBroker, "DISABLE_BROKER", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(DatabaseOptionKind.NewBroker, "NEW_BROKER", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(DatabaseOptionKind.ErrorBrokerConversations, "ERROR_BROKER_CONVERSATIONS", SqlVersionFlags.TSql90AndAbove);
		}

		// Token: 0x040005C6 RID: 1478
		internal static readonly SimpleDbOptionsHelper Instance = new SimpleDbOptionsHelper();
	}
}
