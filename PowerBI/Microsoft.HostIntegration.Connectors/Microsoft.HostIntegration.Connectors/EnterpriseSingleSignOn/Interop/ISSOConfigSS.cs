using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A4 RID: 1188
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("92A8E726-A83B-4ef6-8D77-197558227B0D")]
	[CoClass(typeof(SSOConfigOM))]
	[ComImport]
	public interface ISSOConfigSS
	{
		// Token: 0x06002913 RID: 10515
		void GenerateSecret(string backupFile, string filePassword, string filePasswordReminder);

		// Token: 0x06002914 RID: 10516
		void BackupSecret(string backupFile, string filePassword, string filePasswordReminder);

		// Token: 0x06002915 RID: 10517
		void GetFilePasswordReminder(string restoreFile, out string filePasswordReminder);

		// Token: 0x06002916 RID: 10518
		void RestoreSecret(string restoreFile, string filePassword);
	}
}
