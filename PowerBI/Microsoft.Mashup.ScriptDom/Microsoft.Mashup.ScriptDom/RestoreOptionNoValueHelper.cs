using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	internal class RestoreOptionNoValueHelper : OptionsHelper<RestoreOptionKind>
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0009106C File Offset: 0x0008F26C
		private RestoreOptionNoValueHelper()
		{
			base.AddOptionMapping(RestoreOptionKind.NoLog, "NO_LOG");
			base.AddOptionMapping(RestoreOptionKind.Checksum, "CHECKSUM");
			base.AddOptionMapping(RestoreOptionKind.NoChecksum, "NO_CHECKSUM");
			base.AddOptionMapping(RestoreOptionKind.ContinueAfterError, "CONTINUE_AFTER_ERROR");
			base.AddOptionMapping(RestoreOptionKind.StopOnError, "STOP_ON_ERROR");
			base.AddOptionMapping(RestoreOptionKind.Unload, "UNLOAD");
			base.AddOptionMapping(RestoreOptionKind.NoUnload, "NOUNLOAD");
			base.AddOptionMapping(RestoreOptionKind.Rewind, "REWIND");
			base.AddOptionMapping(RestoreOptionKind.NoRewind, "NOREWIND");
			base.AddOptionMapping(RestoreOptionKind.Stats, "STATS");
			base.AddOptionMapping(RestoreOptionKind.NoRecovery, "NORECOVERY");
			base.AddOptionMapping(RestoreOptionKind.Recovery, "RECOVERY");
			base.AddOptionMapping(RestoreOptionKind.Replace, "REPLACE");
			base.AddOptionMapping(RestoreOptionKind.Restart, "RESTART");
			base.AddOptionMapping(RestoreOptionKind.Verbose, "VERBOSE");
			base.AddOptionMapping(RestoreOptionKind.LoadHistory, "LOADHISTORY");
			base.AddOptionMapping(RestoreOptionKind.DboOnly, "DBO_ONLY", SqlVersionFlags.TSqlUnder110);
			base.AddOptionMapping(RestoreOptionKind.RestrictedUser, "RESTRICTED_USER");
			base.AddOptionMapping(RestoreOptionKind.Partial, "PARTIAL");
			base.AddOptionMapping(RestoreOptionKind.Snapshot, "SNAPSHOT");
			base.AddOptionMapping(RestoreOptionKind.KeepReplication, "KEEP_REPLICATION");
			base.AddOptionMapping(RestoreOptionKind.Online, "ONLINE");
			base.AddOptionMapping(RestoreOptionKind.CommitDifferentialBase, "COMMIT_DIFFERENTIAL_BASE");
			base.AddOptionMapping(RestoreOptionKind.SnapshotImport, "SNAPSHOT_IMPORT");
			base.AddOptionMapping(RestoreOptionKind.NewBroker, "NEW_BROKER");
			base.AddOptionMapping(RestoreOptionKind.EnableBroker, "ENABLE_BROKER");
			base.AddOptionMapping(RestoreOptionKind.ErrorBrokerConversations, "ERROR_BROKER_CONVERSATIONS");
		}

		// Token: 0x040011AB RID: 4523
		internal static readonly RestoreOptionNoValueHelper Instance = new RestoreOptionNoValueHelper();
	}
}
