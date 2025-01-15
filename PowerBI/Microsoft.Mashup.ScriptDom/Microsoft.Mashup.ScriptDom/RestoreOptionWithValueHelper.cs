using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	internal class RestoreOptionWithValueHelper : OptionsHelper<RestoreOptionKind>
	{
		// Token: 0x0600211A RID: 8474 RVA: 0x0015C7EC File Offset: 0x0015A9EC
		private RestoreOptionWithValueHelper()
		{
			base.AddOptionMapping(RestoreOptionKind.File, TSqlTokenType.File);
			base.AddOptionMapping(RestoreOptionKind.Stats, "STATS");
			base.AddOptionMapping(RestoreOptionKind.StopAt, "STOPAT");
			base.AddOptionMapping(RestoreOptionKind.MediaName, "MEDIANAME");
			base.AddOptionMapping(RestoreOptionKind.MediaPassword, "MEDIAPASSWORD");
			base.AddOptionMapping(RestoreOptionKind.Password, "PASSWORD");
			base.AddOptionMapping(RestoreOptionKind.BlockSize, "BLOCKSIZE");
			base.AddOptionMapping(RestoreOptionKind.BufferCount, "BUFFERCOUNT");
			base.AddOptionMapping(RestoreOptionKind.MaxTransferSize, "MAXTRANSFERSIZE");
			base.AddOptionMapping(RestoreOptionKind.Standby, "STANDBY");
			base.AddOptionMapping(RestoreOptionKind.EnhancedIntegrity, "ENHANCEDINTEGRITY");
			base.AddOptionMapping(RestoreOptionKind.SnapshotRestorePhase, "SNAPSHOTRESTOREPHASE");
		}

		// Token: 0x040018C1 RID: 6337
		internal static readonly RestoreOptionWithValueHelper Instance = new RestoreOptionWithValueHelper();
	}
}
