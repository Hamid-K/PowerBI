using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000106 RID: 262
	internal class BackupOptionsWithValueHelper : OptionsHelper<BackupOptionKind>
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x00090584 File Offset: 0x0008E784
		private BackupOptionsWithValueHelper()
		{
			base.AddOptionMapping(BackupOptionKind.Stats, "STATS");
			base.AddOptionMapping(BackupOptionKind.Standby, "STANDBY");
			base.AddOptionMapping(BackupOptionKind.ExpireDate, "EXPIREDATE");
			base.AddOptionMapping(BackupOptionKind.RetainDays, "RETAINDAYS");
			base.AddOptionMapping(BackupOptionKind.Name, "NAME");
			base.AddOptionMapping(BackupOptionKind.Description, "DESCRIPTION");
			base.AddOptionMapping(BackupOptionKind.Password, "PASSWORD", SqlVersionFlags.TSqlUnder110);
			base.AddOptionMapping(BackupOptionKind.MediaName, "MEDIANAME");
			base.AddOptionMapping(BackupOptionKind.MediaDescription, "MEDIADESCRIPTION");
			base.AddOptionMapping(BackupOptionKind.MediaPassword, "MEDIAPASSWORD", SqlVersionFlags.TSqlUnder110);
			base.AddOptionMapping(BackupOptionKind.BlockSize, "BLOCKSIZE");
			base.AddOptionMapping(BackupOptionKind.BufferCount, "BUFFERCOUNT");
			base.AddOptionMapping(BackupOptionKind.MaxTransferSize, "MAXTRANSFERSIZE");
			base.AddOptionMapping(BackupOptionKind.EnhancedIntegrity, "ENHANCEDINTEGRITY");
		}

		// Token: 0x04000B66 RID: 2918
		internal static readonly BackupOptionsWithValueHelper Instance = new BackupOptionsWithValueHelper();
	}
}
