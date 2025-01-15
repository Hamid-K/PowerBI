using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using RSManagedCrypto;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000013 RID: 19
	internal sealed class Reencryptor : IDisposable
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002C44 File Offset: 0x00000E44
		internal Reencryptor()
		{
			this.m_newKeyManager = new RSCrypto();
			this.m_newKeyManager.CreateSymmetricKey();
			this.m_database = new ActivationDB();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C6E File Offset: 0x00000E6E
		public void WillDispose()
		{
			this.m_database.WillDispose();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C7B File Offset: 0x00000E7B
		public void Dispose()
		{
			if (this.m_newKeyManager != null)
			{
				this.m_newKeyManager.Dispose();
				this.m_newKeyManager = null;
			}
			if (this.m_database != null)
			{
				this.m_database.Dispose();
				this.m_database = null;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CB4 File Offset: 0x00000EB4
		internal void ReencryptSecureInformation()
		{
			List<Guid> list = new List<Guid>(100);
			List<Guid> list2 = new List<Guid>(50);
			List<Guid> list3 = new List<Guid>(2);
			List<byte[]> list4 = new List<byte[]>(2);
			List<KeyValuePair<string, string>> list5 = new List<KeyValuePair<string, string>>();
			List<long> list6 = new List<long>();
			List<Guid> list7 = new List<Guid>();
			this.m_database.ListInfoForReencryption(list, list2, list3, list4, list7, list5, list6);
			this.ReencryptSecuredKPIs();
			foreach (Guid guid in list)
			{
				this.ReencryptDatasource(guid);
			}
			foreach (Guid guid2 in list2)
			{
				this.ReencryptSubscription(guid2);
			}
			foreach (Guid guid3 in list7)
			{
				this.ReencryptUserToken(guid3);
			}
			foreach (KeyValuePair<string, string> keyValuePair in list5)
			{
				this.ReencryptConfigInfoEntry(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (long num in list6)
			{
				this.ReencryptDataModelDatasource(num);
			}
			Guid installationID = Globals.Configuration.InstallationID;
			bool flag = false;
			for (int i = 0; i < list3.Count; i++)
			{
				bool flag2 = list3[i] == installationID;
				if (flag2)
				{
					flag = true;
				}
				this.ReencryptKey(list4[i], list3[i], flag2);
			}
			if (!flag)
			{
				throw new ReportServerNotActivatedException(null);
			}
			this.m_database.Commit();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002ECC File Offset: 0x000010CC
		private void ReencryptSecuredKPIs()
		{
			string text = "SELECT ItemID, Property FROM Catalog WITH (NOLOCK) WHERE Type=@itemType";
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.m_database.NewStandardSqlCommandQuery(text))
			{
				XmlDocument xmlDocument = new XmlDocument();
				Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
				string empty = string.Empty;
				instrumentedSqlCommand.AddParameter("@itemType", SqlDbType.Int, 11);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					int ordinal = dataReader.GetOrdinal("ItemID");
					int ordinal2 = dataReader.GetOrdinal("Property");
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(ordinal);
						string text2 = string.Empty;
						if (!dataReader.IsDBNull(ordinal2))
						{
							text2 = dataReader.GetString(ordinal2);
							dictionary.Add(guid, text2);
						}
					}
				}
				foreach (KeyValuePair<Guid, string> keyValuePair in dictionary)
				{
					xmlDocument.LoadXml(keyValuePair.Value);
					string empty2 = string.Empty;
					try
					{
						if (Convert.ToBoolean(xmlDocument.DocumentElement.GetElementsByTagName("EncryptedValues")[0].InnerText))
						{
							this.ReencryptKPIField(xmlDocument, "Status.Value");
							this.ReencryptKPIField(xmlDocument, "TrendSet.Value");
							this.ReencryptKPIField(xmlDocument, "Value.Value");
							this.UpdateKPIRecord(keyValuePair.Key, xmlDocument.OuterXml);
						}
					}
					catch (Exception ex)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: - could not retrive encryptedvalue field.  Error: {0}", new object[] { ex.Message });
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030B4 File Offset: 0x000012B4
		private void ReencryptKPIField(XmlDocument doc, string nodeToReencrypt)
		{
			try
			{
				XmlNode xmlNode = doc.DocumentElement.GetElementsByTagName(nodeToReencrypt)[0];
				string text;
				this.ReencryptString(xmlNode.InnerText, out text);
				xmlNode.InnerText = text;
			}
			catch (Exception ex)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption (KPI): Operation field on field {0} - Error: {1}", new object[] { nodeToReencrypt, ex.Message });
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003124 File Offset: 0x00001324
		private int UpdateKPIRecord(Guid key, string value)
		{
			int num = -1;
			try
			{
				string text = "UPDATE [Catalog] SET [Property] = @value WHERE [ItemID] = @key";
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.m_database.NewStandardSqlCommandQuery(text))
				{
					instrumentedSqlCommand.AddParameter("@key", SqlDbType.UniqueIdentifier, key);
					instrumentedSqlCommand.AddParameter("@value", SqlDbType.NText, value);
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption (KPI): Update Operation failed - Error: {0}", new object[] { ex.Message });
			}
			return num;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000031C0 File Offset: 0x000013C0
		private void ReencryptDatasource(Guid dsid)
		{
			int num;
			byte[] array;
			byte[] array2;
			byte[] array3;
			byte[] array4;
			int num2;
			this.m_database.GetDatasourceInfoForReencryption(dsid, out num, out array, out array2, out array3, out array4, out num2);
			byte[] array5 = null;
			byte[] array6 = null;
			byte[] array7 = null;
			byte[] array8 = null;
			if (!this.ReencryptData(num2, array, "ConnectionString", out array5) || !this.ReencryptData(num2, array2, "OriginalConnectionString", out array6) || !this.ReencryptData(num2, array3, "UserName", out array7) || !this.ReencryptData(num2, array4, "Password", out array8))
			{
				num = 1;
				array5 = null;
				array6 = null;
				array7 = null;
				array8 = null;
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to decrypt datasource information. DSID={0}", new object[] { dsid });
			}
			this.m_database.SetReencryptedDatasourceInfo(dsid, num, array5, array6, array7, array8, Encryption.CurrentVersion);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003288 File Offset: 0x00001488
		private void ReencryptDataModelDatasource(long dsid)
		{
			byte[] array;
			byte[] array2;
			byte[] array3;
			this.m_database.GetDataModelDatasourceForReencryption(dsid, out array, out array2, out array3);
			byte[] array4 = null;
			byte[] array5 = null;
			byte[] array6 = null;
			int currentVersion = Encryption.CurrentVersion;
			if (!this.ReencryptData(currentVersion, array, "ConnectionString", out array4) || !this.ReencryptData(currentVersion, array2, "UserName", out array5) || !this.ReencryptData(currentVersion, array3, "Password", out array6))
			{
				array4 = null;
				array5 = null;
				array6 = null;
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to decrypt datamodeldatasource. DSID={0}", new object[] { dsid });
			}
			this.m_database.SetReencryptedDataModelDatasource(dsid, array4, array5, array6);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003328 File Offset: 0x00001528
		private void ReencryptConfigInfoEntry(string configName, string configValue)
		{
			string text = string.Empty;
			Reencryptor.ReencryptionStatus reencryptionStatus;
			if (Reencryptor.IsGuid(configValue))
			{
				Guid guid = new Guid(configValue);
				byte[] array = guid.ToByteArray();
				text = Convert.ToBase64String(this.m_newKeyManager.EncryptData(array));
				reencryptionStatus = Reencryptor.ReencryptionStatus.TookPlace;
			}
			else
			{
				reencryptionStatus = this.ReencryptString(configValue, out text);
			}
			if (reencryptionStatus == Reencryptor.ReencryptionStatus.TookPlace)
			{
				this.m_database.SetConfigurationInfoValue(configName, text);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003384 File Offset: 0x00001584
		private void ReencryptUserToken(Guid userToken)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			this.m_database.GetUserServiceTokenForReencryption(userToken, out empty);
			if (this.ReencryptString(empty, out empty2) == Reencryptor.ReencryptionStatus.TookPlace)
			{
				this.m_database.SetReencryptedUserServiceToken(userToken, empty2);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033C8 File Offset: 0x000015C8
		private void ReencryptSubscription(Guid subscriptionID)
		{
			string text;
			Settings settings;
			SettingImpl[] array;
			int num;
			this.m_database.GetSettingsForReencryption(subscriptionID, out text, out settings, out array, out num);
			if (array.Length == 0)
			{
				return;
			}
			bool flag = false;
			foreach (SettingImpl settingImpl in array)
			{
				string text2;
				Reencryptor.ReencryptionStatus reencryptionStatus = this.ReencryptString(num, settingImpl.Value, settingImpl.Name, out text2);
				if (reencryptionStatus != Reencryptor.ReencryptionStatus.NotNeeded)
				{
					if (reencryptionStatus == Reencryptor.ReencryptionStatus.Failure)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to decrypt delivery provider setting. Setting removed. SubscriptionID='{0}', Provider='{1}', Setting='{2}'", new object[] { subscriptionID, text, settingImpl.Name });
					}
					settingImpl.Value = text2;
					flag = true;
				}
			}
			if (flag)
			{
				string text3 = ParameterValueOrFieldReference.ThisArrayToXml(settings.ToSoapParameterValueOrFieldReferenceArray());
				this.m_database.SetReencryptedSubscriptionInfo(subscriptionID, text3, Encryption.CurrentVersion);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003490 File Offset: 0x00001690
		private void ReencryptKey(byte[] publicKey, Guid installationID, bool isThisInstallation)
		{
			string text = installationID.ToString();
			byte[] array = null;
			try
			{
				array = this.m_newKeyManager.ExportSymmetricKey(publicKey);
			}
			catch (COMException ex)
			{
				if (isThisInstallation)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to export symmetric key for current installation! InstallationID='{0}'", new object[] { text });
					throw new FailedToExportSymmetricKeyException();
				}
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to export symmetric key. Installation will no longer have access to secure information. InstallationID='{0}', Exception='{1}'", new object[]
				{
					text,
					ex.ToString()
				});
				array = null;
			}
			this.m_database.SetKeysForInstallation(installationID, array, publicKey);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003528 File Offset: 0x00001728
		private static bool IsGuid(string testString)
		{
			return !string.IsNullOrEmpty(testString) && new Regex("^[A-Fa-f0-9]{32}$|^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$").Match(testString).Success;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000354C File Offset: 0x0000174C
		private Reencryptor.ReencryptionStatus ReencryptString(string encryptedString, out string reencryptedString)
		{
			if (encryptedString == null || encryptedString.Length == 0)
			{
				reencryptedString = encryptedString;
				return Reencryptor.ReencryptionStatus.NotNeeded;
			}
			byte[] array = null;
			try
			{
				byte[] array2 = Convert.FromBase64String(encryptedString);
				array = CatalogEncryption.Instance.Decrypt(array2, true);
				byte[] array3 = this.m_newKeyManager.EncryptData(array);
				reencryptedString = Convert.ToBase64String(array3);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2146893819)
				{
					reencryptedString = null;
					return Reencryptor.ReencryptionStatus.Failure;
				}
				throw;
			}
			catch (Exception)
			{
				reencryptedString = null;
				return Reencryptor.ReencryptionStatus.Failure;
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
				}
			}
			return Reencryptor.ReencryptionStatus.TookPlace;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000035EC File Offset: 0x000017EC
		private Reencryptor.ReencryptionStatus ReencryptString(int version, string encryptedString, string tag, out string reencryptedString)
		{
			if (encryptedString == null || encryptedString.Length == 0)
			{
				reencryptedString = encryptedString;
				return Reencryptor.ReencryptionStatus.NotNeeded;
			}
			byte[] array = Convert.FromBase64String(encryptedString);
			byte[] array2;
			if (!this.ReencryptData(version, array, tag, out array2))
			{
				reencryptedString = null;
				return Reencryptor.ReencryptionStatus.Failure;
			}
			reencryptedString = Convert.ToBase64String(array2);
			return Reencryptor.ReencryptionStatus.TookPlace;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003630 File Offset: 0x00001830
		private bool ReencryptData(int version, byte[] data, string tag, out byte[] reencryptedData)
		{
			reencryptedData = null;
			if (data == null)
			{
				return true;
			}
			byte[] array = null;
			try
			{
				try
				{
					array = CatalogEncryption.Instance.Decrypt(version, data, null);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146893819)
					{
						return false;
					}
					throw;
				}
				catch (RSException)
				{
					return false;
				}
				reencryptedData = this.m_newKeyManager.EncryptData(array);
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
				}
			}
			return true;
		}

		// Token: 0x04000093 RID: 147
		private RSCrypto m_newKeyManager;

		// Token: 0x04000094 RID: 148
		private ActivationDB m_database;

		// Token: 0x02000434 RID: 1076
		internal enum ReencryptionStatus
		{
			// Token: 0x04000F0D RID: 3853
			NotNeeded,
			// Token: 0x04000F0E RID: 3854
			TookPlace,
			// Token: 0x04000F0F RID: 3855
			Failure
		}
	}
}
