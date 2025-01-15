using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000012 RID: 18
	internal static class Activation
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static void SetKeyForThisInstallation(byte[] encryptedKey, string encryptPassword)
		{
			SymmetricKeyEncryption.AcquireWriterLock();
			byte[] orRepairPublicKey;
			byte[] array;
			try
			{
				orRepairPublicKey = SymmetricKeyEncryption.GetOrRepairPublicKey();
				array = SymmetricKeyEncryption.PasswordDecryptSymmetricKey(encryptedKey, encryptPassword);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2146893819)
				{
					throw new BackupKeyPasswordInvalidException();
				}
				throw;
			}
			finally
			{
				SymmetricKeyEncryption.ReleaseWriterLock();
			}
			Guid installationID = Globals.Configuration.InstallationID;
			using (ActivationDB activationDB = new ActivationDB())
			{
				activationDB.WillDispose();
				activationDB.SetKeysForInstallation(installationID, array, orRepairPublicKey);
				activationDB.Commit();
			}
			RSEventLog.Current.WriteSuccess(Event.KeyImportSuccessful, Array.Empty<object>());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A9C File Offset: 0x00000C9C
		public static byte[] ExtractKey(string encryptedPassword)
		{
			byte[] array = SymmetricKeyEncryption.PasswordEncryptSymmetricKey(encryptedPassword);
			RSEventLog.Current.WriteSuccess(Event.KeyExtractionSuccessful, Array.Empty<object>());
			return array;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static void DeleteKey(Guid installationID)
		{
			using (ActivationDB activationDB = new ActivationDB())
			{
				activationDB.WillDispose();
				activationDB.DeleteKey(installationID);
				activationDB.Commit();
			}
			RSEventLog.Current.WriteSuccess(Event.KeyDeleteSuccessful, Array.Empty<object>());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B10 File Offset: 0x00000D10
		public static void DeleteEncryptedContent()
		{
			using (ActivationDB activationDB = new ActivationDB())
			{
				activationDB.WillDispose();
				activationDB.DeleteEncryptedContent();
				activationDB.Commit();
			}
			RSEventLog.Current.WriteSuccess(Event.EncryptDataCleaned, Array.Empty<object>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B64 File Offset: 0x00000D64
		public static void ActivateService(Guid targetInstallationID)
		{
			using (ActivationDB activationDB = new ActivationDB())
			{
				activationDB.WillDispose();
				KeyStorage.AnnoucedKeyResults announcedKey = activationDB.GetAnnouncedKey(targetInstallationID);
				byte[] publicKeyEncryptedSymmetricKey = SymmetricKeyEncryption.GetPublicKeyEncryptedSymmetricKey(announcedKey.AnnouncedKey);
				activationDB.SetKeysForInstallation(targetInstallationID, publicKeyEncryptedSymmetricKey, announcedKey.AnnouncedKey);
				activationDB.Commit();
				RSEventLog.Current.WriteSuccess(Event.WebFarmNodeActivated, new object[] { announcedKey.FullName });
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public static void ListReportServersInDB(out string[] machineNames, out string[] instanceNames, out string[] installationIDs, out byte[] flags)
		{
			using (ActivationDB activationDB = new ActivationDB())
			{
				activationDB.WillDispose();
				List<string> list;
				List<string> list2;
				List<string> list3;
				List<byte> list4;
				activationDB.ListInstallations(out list, out list2, out list3, out list4);
				machineNames = list.ToArray();
				instanceNames = list2.ToArray();
				installationIDs = list3.ToArray();
				flags = list4.ToArray();
			}
		}
	}
}
