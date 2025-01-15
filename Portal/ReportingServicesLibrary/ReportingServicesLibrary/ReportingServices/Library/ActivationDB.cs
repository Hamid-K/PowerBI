using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000014 RID: 20
	internal sealed class ActivationDB : KeyStorage, IDisposable
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000036BC File Offset: 0x000018BC
		public ActivationDB()
			: base(new ConnectionManager())
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003768 File Offset: 0x00001968
		public ActivationDB(ConnectionManager manager)
			: base(manager)
		{
			this.m_disconnectStorage = false;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003814 File Offset: 0x00001A14
		public void WillDispose()
		{
			if (this.m_disconnectStorage)
			{
				this.ConnectionManager.WillDisconnectStorage();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003829 File Offset: 0x00001A29
		public void Dispose()
		{
			if (this.m_disconnectStorage)
			{
				base.DisconnectStorage();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000383C File Offset: 0x00001A3C
		public void DeleteEncryptedContent()
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteEncryptedContent", base.UnverifiedConnection))
			{
				DataTable dataTable = this.CreateConfigParameterList(this.encryptedConfigSettingsToDelete);
				instrumentedSqlCommand.AddParameter("@ConfigNamesToDelete", SqlDbType.Structured, dataTable);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
			List<Guid> list = new List<Guid>(50);
			this.ListSubscriptionIDs(list);
			foreach (Guid guid in list)
			{
				this.DeleteSubscriptionEncryptedContent(guid);
			}
			this.ClearEncryptedKPIRecords();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000038F0 File Offset: 0x00001AF0
		private void ClearEncryptedKPIRecords()
		{
			string text = "SELECT ItemID, Property FROM Catalog WITH (NOLOCK) WHERE Type=@itemType";
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				XmlDocument xmlDocument = new XmlDocument();
				Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
				instrumentedSqlCommand.AddParameter("@itemType", SqlDbType.Int, 11);
				xmlDocument.PreserveWhitespace = false;
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
					string empty = string.Empty;
					try
					{
						xmlDocument.LoadXml(keyValuePair.Value);
						if (Convert.ToBoolean(xmlDocument.DocumentElement.GetElementsByTagName("EncryptedValues")[0].InnerText))
						{
							this.ClearEncryptedKPIField(xmlDocument, "Status.Value");
							this.ClearEncryptedKPIField(xmlDocument, "TrendSet.Value");
							this.ClearEncryptedKPIField(xmlDocument, "Value.Value");
							this.UpdateKPIRecord(keyValuePair.Key, xmlDocument.OuterXml);
						}
					}
					catch (Exception ex)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, "DeleteEncryptedData (KPI): Could not retrieve EncryptedValues field: {0}", new object[] { ex.Message });
					}
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003AD4 File Offset: 0x00001CD4
		private void ClearEncryptedKPIField(XmlDocument doc, string noteToEmpty)
		{
			try
			{
				doc.DocumentElement.GetElementsByTagName(noteToEmpty)[0].InnerText = "";
			}
			catch (Exception ex)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption (KPI): Operation field on field {0} - Error: {1}", new object[] { noteToEmpty, ex.Message });
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003B38 File Offset: 0x00001D38
		private int UpdateKPIRecord(Guid key, string value)
		{
			int num = -1;
			try
			{
				string text = "UPDATE [Catalog] SET [Property] = @value WHERE [ItemID] = @key";
				using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
				{
					instrumentedSqlCommand.AddParameter("@key", SqlDbType.UniqueIdentifier, key);
					instrumentedSqlCommand.AddParameter("@value", SqlDbType.NText, value);
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "DeleteEncrypted (KPI): Update Operation failed - Error: {0}", new object[] { ex.Message });
			}
			return num;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003BCC File Offset: 0x00001DCC
		private void ListSubscriptionIDs(List<Guid> subscriptionList)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListSubscriptionIDs", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						subscriptionList.Add(guid);
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C38 File Offset: 0x00001E38
		private void InvalidateSubscriptionAsMissingEncryptedContent(Guid subscriptionID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("InvalidateSubscription", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Flags", InActiveFlags.MissingExtensionEncryptedSettings);
				instrumentedSqlCommand.AddParameter("@LastStatus", SqlDbType.NVarChar, RepLibRes.SubscriptionMissingEncryptedContent);
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003CB8 File Offset: 0x00001EB8
		private void DeleteSubscriptionEncryptedContent(Guid subscriptionID)
		{
			string text;
			Settings settings;
			SettingImpl[] array;
			int num;
			this.GetSettingsForReencryption(subscriptionID, out text, out settings, out array, out num);
			if (array.Length == 0)
			{
				return;
			}
			SettingImpl[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Value = null;
			}
			string text2 = ParameterValueOrFieldReference.ThisArrayToXml(settings.ToSoapParameterValueOrFieldReferenceArray());
			this.SetReencryptedSubscriptionInfo(subscriptionID, text2, Encryption.CurrentVersion);
			this.InvalidateSubscriptionAsMissingEncryptedContent(subscriptionID);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003D20 File Offset: 0x00001F20
		public string GetSettingsForReencryption(Guid subscriptionID, out string deliveryExtensionName, out Settings allSettings, out SettingImpl[] encryptedSettings, out int version)
		{
			string text;
			this.GetSubscriptionInfoForReencryption(subscriptionID, out deliveryExtensionName, out text, out version);
			List<string> encryptedSettingsNames = this.GetEncryptedSettingsNames(deliveryExtensionName);
			ParameterValueOrFieldReference[] array = ParameterValueOrFieldReference.XmlToThisArray(text, false);
			allSettings = new Settings();
			allSettings.FromSoapParameterValueArray(array);
			List<SettingImpl> list = new List<SettingImpl>();
			if (encryptedSettingsNames != null)
			{
				foreach (string text2 in encryptedSettingsNames)
				{
					SettingImpl settingImpl = allSettings[text2];
					if (settingImpl != null && !settingImpl.UseField && settingImpl.Value != null)
					{
						list.Add(settingImpl);
					}
				}
			}
			encryptedSettings = list.ToArray();
			return deliveryExtensionName;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private List<string> GetEncryptedSettingsNames(string deliveryExtensionName)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(deliveryExtensionName) || this.m_encryptedSettingsForExtension.TryGetValue(deliveryExtensionName, out list))
			{
				return list;
			}
			list = new List<string>();
			this.m_encryptedSettingsForExtension.Add(deliveryExtensionName, list);
			IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(deliveryExtensionName, "Delivery");
			if (newInstanceExtensionClass == null)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Failed to load extension. Not reencrypting settings for this extension in subscriptions. Extension='{0}'", new object[] { deliveryExtensionName });
				return list;
			}
			IDeliveryExtension deliveryExtension = newInstanceExtensionClass as IDeliveryExtension;
			if (deliveryExtension == null)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Reencryption: Extension is not a delivery provider. Not reencrypting settings for this extension in subscriptions. Extension='{0}'", new object[] { deliveryExtensionName });
				return list;
			}
			Setting[] array = ProviderManager.InitDeliveryExtension(deliveryExtension, true, null, null);
			if (array == null)
			{
				return list;
			}
			foreach (Setting setting in array)
			{
				if (setting.Encrypted)
				{
					list.Add(setting.Name);
				}
			}
			return list;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal void ListInstallations(out List<string> machineNameList, out List<string> instanceNameList, out List<string> installationIDList, out List<byte> flagsList)
		{
			machineNameList = new List<string>(2);
			instanceNameList = new List<string>(2);
			installationIDList = new List<string>(2);
			flagsList = new List<byte>(2);
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListInstallations", base.UnverifiedConnection))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						string text = null;
						if (!dataReader.IsDBNull(0))
						{
							text = dataReader.GetString(0);
						}
						machineNameList.Add(text);
						string text2 = null;
						if (!dataReader.IsDBNull(1))
						{
							text2 = dataReader.GetString(1);
						}
						instanceNameList.Add(text2);
						string text3 = null;
						if (!dataReader.IsDBNull(2))
						{
							text3 = dataReader.GetGuid(2).ToString();
						}
						installationIDList.Add(text3);
						byte b = 0;
						if (!dataReader.IsDBNull(3))
						{
							b = (byte)dataReader.GetInt32(3);
						}
						flagsList.Add(b);
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003FB4 File Offset: 0x000021B4
		internal void ListInfoForReencryption(List<Guid> dataSourceList, List<Guid> subscriptionList, List<Guid> installationList, List<byte[]> publicKeys, List<Guid> userIDs, List<KeyValuePair<string, string>> configSettings, List<long> dataModelDataSourceList)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListInfoForReencryption", null))
			{
				DataTable dataTable = this.CreateConfigParameterList(this.configSettingsToEncrypt);
				instrumentedSqlCommand.AddParameter("@ConfigNames", SqlDbType.Structured, dataTable);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						dataSourceList.Add(guid);
					}
					dataReader.NextResult();
					while (dataReader.Read())
					{
						Guid guid2 = dataReader.GetGuid(0);
						subscriptionList.Add(guid2);
					}
					dataReader.NextResult();
					while (dataReader.Read())
					{
						Guid guid3 = dataReader.GetGuid(0);
						byte[] array = DataReaderHelper.ReadAllBytes(dataReader, 1);
						installationList.Add(guid3);
						publicKeys.Add(array);
					}
					dataReader.NextResult();
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(dataReader.GetOrdinal("Name"));
						string string2 = dataReader.GetString(dataReader.GetOrdinal("Value"));
						configSettings.Add(new KeyValuePair<string, string>(@string, string2));
					}
					dataReader.NextResult();
					while (dataReader.Read())
					{
						Guid guid4 = dataReader.GetGuid(0);
						userIDs.Add(guid4);
					}
					dataReader.NextResult();
					while (dataReader.Read())
					{
						long @int = dataReader.GetInt64(0);
						dataModelDataSourceList.Add(@int);
					}
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004138 File Offset: 0x00002338
		private DataTable CreateConfigParameterList(string[] configSettings)
		{
			string text = "ConfigName";
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(text, typeof(string));
			foreach (string text2 in configSettings)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[text] = text2;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000041A0 File Offset: 0x000023A0
		internal bool GetDatasourceInfoForReencryption(Guid dsid, out int credentialRetrieval, out byte[] connectionString, out byte[] originalConnectionString, out byte[] userName, out byte[] password, out int version)
		{
			connectionString = null;
			originalConnectionString = null;
			userName = null;
			password = null;
			version = Encryption.CurrentVersion;
			credentialRetrieval = 1;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDatasourceInfoForReencryption", null))
			{
				instrumentedSqlCommand.AddParameter("@DSID", SqlDbType.UniqueIdentifier, dsid);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						connectionString = DataReaderHelper.ReadAllBytes(dataReader, 0);
					}
					if (!dataReader.IsDBNull(1))
					{
						originalConnectionString = DataReaderHelper.ReadAllBytes(dataReader, 1);
					}
					if (!dataReader.IsDBNull(2))
					{
						userName = DataReaderHelper.ReadAllBytes(dataReader, 2);
					}
					if (!dataReader.IsDBNull(3))
					{
						password = DataReaderHelper.ReadAllBytes(dataReader, 3);
					}
					if (!dataReader.IsDBNull(4))
					{
						credentialRetrieval = dataReader.GetInt32(4);
					}
					version = dataReader.GetInt32(5);
				}
			}
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004298 File Offset: 0x00002498
		internal void SetReencryptedDatasourceInfo(Guid dsid, int credentialRetrieval, byte[] connectionString, byte[] originalConnectionString, byte[] userName, byte[] password, int version)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetReencryptedDatasourceInfo", null))
			{
				instrumentedSqlCommand.AddParameter("@DSID", SqlDbType.UniqueIdentifier, dsid);
				if (connectionString != null)
				{
					instrumentedSqlCommand.AddParameter("@ConnectionString", SqlDbType.Image, connectionString);
				}
				if (originalConnectionString != null)
				{
					instrumentedSqlCommand.AddParameter("@OriginalConnectionString", SqlDbType.Image, originalConnectionString);
				}
				if (userName != null)
				{
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.Image, userName);
				}
				if (password != null)
				{
					instrumentedSqlCommand.AddParameter("@Password", SqlDbType.Image, password);
				}
				instrumentedSqlCommand.AddParameter("@Version", SqlDbType.Int, version);
				instrumentedSqlCommand.AddParameter("@CredentialRetrieval", SqlDbType.Int, credentialRetrieval);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000435C File Offset: 0x0000255C
		internal bool GetDataModelDatasourceForReencryption(long dsid, out byte[] connectionString, out byte[] userName, out byte[] password)
		{
			connectionString = null;
			userName = null;
			password = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataModelDatasourceForReencryption", null))
			{
				instrumentedSqlCommand.AddParameter("@DSID", SqlDbType.BigInt, dsid);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						connectionString = DataReaderHelper.ReadAllBytes(dataReader, 0);
					}
					if (!dataReader.IsDBNull(1))
					{
						userName = DataReaderHelper.ReadAllBytes(dataReader, 1);
					}
					if (!dataReader.IsDBNull(2))
					{
						password = DataReaderHelper.ReadAllBytes(dataReader, 2);
					}
				}
			}
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004414 File Offset: 0x00002614
		internal void SetReencryptedDataModelDatasource(long dsid, byte[] connectionString, byte[] userName, byte[] password)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetReencryptedDataModelDataSource", null))
			{
				instrumentedSqlCommand.AddParameter("@DSID", SqlDbType.BigInt, dsid);
				if (connectionString != null)
				{
					instrumentedSqlCommand.AddParameter("@ConnectionString", SqlDbType.VarBinary, connectionString);
				}
				if (userName != null)
				{
					instrumentedSqlCommand.AddParameter("@Username", SqlDbType.VarBinary, userName);
				}
				if (password != null)
				{
					instrumentedSqlCommand.AddParameter("@Password", SqlDbType.VarBinary, password);
				}
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000449C File Offset: 0x0000269C
		internal bool GetSubscriptionInfoForReencryption(Guid subscriptionID, out string deliveryExtension, out string extensionSettingsXml, out int version)
		{
			deliveryExtension = null;
			extensionSettingsXml = null;
			version = Encryption.CurrentVersion;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSubscriptionInfoForReencryption", null))
			{
				instrumentedSqlCommand.AddParameter("@SubscriptionID", SqlDbType.UniqueIdentifier, subscriptionID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						deliveryExtension = dataReader.GetString(0);
					}
					if (!dataReader.IsDBNull(1))
					{
						extensionSettingsXml = dataReader.GetString(1);
					}
					version = dataReader.GetInt32(2);
				}
			}
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004550 File Offset: 0x00002750
		internal bool GetUserServiceTokenForReencryption(Guid userToken, out string encryptedUserServiceToken)
		{
			encryptedUserServiceToken = string.Empty;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetUserServiceTokenForReencryption", null))
			{
				instrumentedSqlCommand.AddParameter("@UserID", SqlDbType.UniqueIdentifier, userToken);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						return false;
					}
					if (!dataReader.IsDBNull(0))
					{
						encryptedUserServiceToken = dataReader.GetString(0);
					}
				}
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000045E0 File Offset: 0x000027E0
		internal void SetReencryptedSubscriptionInfo(Guid subscriptionID, string extensionSettingsXml, int version)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetReencryptedSubscriptionInfo", null))
			{
				instrumentedSqlCommand.AddParameter("@SubscriptionID", SqlDbType.UniqueIdentifier, subscriptionID);
				if (extensionSettingsXml != null)
				{
					instrumentedSqlCommand.AddParameter("@ExtensionSettings", SqlDbType.NText, extensionSettingsXml);
				}
				instrumentedSqlCommand.AddParameter("@Version", SqlDbType.Int, version);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004658 File Offset: 0x00002858
		internal void SetConfigurationInfoValue(string configName, string configValue)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetConfigurationInfoValue", null))
			{
				instrumentedSqlCommand.AddParameter("@ConfigValue", SqlDbType.NVarChar, configValue);
				instrumentedSqlCommand.AddParameter("@ConfigName", SqlDbType.NVarChar, configName);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000046B4 File Offset: 0x000028B4
		internal void SetReencryptedUserServiceToken(Guid userID, string serviceToken)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetReencryptedUserServiceToken", null))
			{
				instrumentedSqlCommand.AddParameter("@UserID", SqlDbType.UniqueIdentifier, userID);
				instrumentedSqlCommand.AddParameter("@ServiceToken", SqlDbType.NText, serviceToken);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x04000095 RID: 149
		private bool m_disconnectStorage = true;

		// Token: 0x04000096 RID: 150
		private readonly string[] configSettingsToEncrypt = new string[] { "OAuthClientSecret", "ClientSecret" };

		// Token: 0x04000097 RID: 151
		private readonly string[] encryptedConfigSettingsToDelete = new string[] { "OAuthClientSecret", "ClientId", "AppObjectId", "TenantName", "TenantId", "ClientSecret", "ResourceUrl", "AuthorizationUrl", "TokenUrl", "RedirectUrls" };

		// Token: 0x04000098 RID: 152
		private Dictionary<string, List<string>> m_encryptedSettingsForExtension = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
	}
}
