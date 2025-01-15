using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000E RID: 14
	internal class KeyStorage : Storage
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public KeyStorage(ConnectionManager manager)
		{
			this.ConnectionManager = manager;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public void SetKeysForInstallation(Guid installationID, byte[] symmetricKey, byte[] publicKey)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetKeysForInstallation", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@InstallationID", installationID);
				if (symmetricKey != null)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SymmetricKey", symmetricKey);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@PublicKey", publicKey);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004B2C File Offset: 0x00002D2C
		public void SavePowerBIInformation(string clientId, string clientSecret, string appObjectId, string tenantName, string tenantId, string resourceUrl, string authUrl, string tokenUrl, string redirectUrls)
		{
			this.DeletePowerBIInformation();
			string text = "INSERT INTO ConfigurationInfo (ConfigInfoID, Name, Value)\r\n                                VALUES (newid(), 'ClientId', @clientIdValue), \r\n                                       (newid(), 'AppObjectId', @appObjectIdValue), \r\n                                       (newid(), 'TenantName', @tenantNameValue),\r\n                                       (newid(), 'TenantId', @tenantIdValue), \r\n                                       (newid(), 'ClientSecret', @clientSecretValue), \r\n                                       (newid(), 'ResourceUrl', @resourceUrlValue),\r\n                                       (newid(), 'AuthorizationUrl',  @authUrlValue),\r\n                                       (newid(), 'TokenUrl', @tokenUrlValue),\r\n                                       (newid(), 'RedirectUrls', @redirectUrlsValue);";
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@clientIdValue", clientId);
				instrumentedSqlCommand.Parameters.AddWithValue("@appObjectIdValue", appObjectId);
				instrumentedSqlCommand.Parameters.AddWithValue("@tenantNameValue", tenantName);
				instrumentedSqlCommand.Parameters.AddWithValue("@tenantIdValue", tenantId);
				instrumentedSqlCommand.Parameters.AddWithValue("@clientSecretValue", clientSecret);
				instrumentedSqlCommand.Parameters.AddWithValue("@resourceUrlValue", resourceUrl);
				instrumentedSqlCommand.Parameters.AddWithValue("@authUrlValue", authUrl);
				instrumentedSqlCommand.Parameters.AddWithValue("@tokenUrlValue", tokenUrl);
				instrumentedSqlCommand.Parameters.AddWithValue("@redirectUrlsValue", redirectUrls);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004C18 File Offset: 0x00002E18
		public void GetPowerBIInformation(out string clientId, out string appObjectId, out string tenantName, out string tenantId, out string resourceUrl, out string authUrl, out string tokenUrl, out string redirectUrls)
		{
			string text = "SELECT Name, Value FROM ConfigurationInfo\r\n                                where [NAME] IN ('ClientId', \r\n                                                'AppObjectId', \r\n                                                'TenantName', \r\n                                                'TenantId', \r\n                                                'ClientSecret', \r\n                                                'ResourceUrl', \r\n                                                'AuthorizationUrl', \r\n                                                'TokenUrl', \r\n                                                'RedirectUrls');";
			InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text);
			clientId = null;
			appObjectId = null;
			tenantName = null;
			tenantId = null;
			resourceUrl = null;
			authUrl = null;
			tokenUrl = null;
			redirectUrls = null;
			string text2 = null;
			using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
			{
				while (dataReader.Read())
				{
					string text3 = dataReader["Value"] as string;
					string text4 = dataReader["Name"] as string;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text4);
					if (num <= 942516643U)
					{
						if (num <= 453348411U)
						{
							if (num != 160393366U)
							{
								if (num == 453348411U)
								{
									if (text4 == "TokenUrl")
									{
										tokenUrl = text3;
									}
								}
							}
							else if (text4 == "AppObjectId")
							{
								appObjectId = text3;
							}
						}
						else if (num != 710209030U)
						{
							if (num == 942516643U)
							{
								if (text4 == "RedirectUrls")
								{
									redirectUrls = text3;
								}
							}
						}
						else if (text4 == "TenantName")
						{
							tenantName = text3;
						}
					}
					else if (num <= 2059514915U)
					{
						if (num != 1890720815U)
						{
							if (num == 2059514915U)
							{
								if (text4 == "ClientId")
								{
									clientId = text3;
								}
							}
						}
						else if (text4 == "AuthorizationUrl")
						{
							authUrl = text3;
						}
					}
					else if (num != 2111347446U)
					{
						if (num != 2359032832U)
						{
							if (num == 4116816302U)
							{
								if (text4 == "ResourceUrl")
								{
									resourceUrl = text3;
								}
							}
						}
						else if (text4 == "TenantId")
						{
							tenantId = text3;
						}
					}
					else if (text4 == "ClientSecret")
					{
						text2 = text3;
					}
				}
			}
			if (clientId == null || appObjectId == null || tenantName == null || tenantId == null || resourceUrl == null || authUrl == null || tokenUrl == null || redirectUrls == null || text2 == null)
			{
				throw new ServerConfigurationErrorException("Invalid PBI Configuration");
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004E48 File Offset: 0x00003048
		public void UpdatePowerBIInformation(string redirectUrls)
		{
			string text = "DELETE FROM ConfigurationInfo\r\n                                where [NAME] IN ('RedirectUrls');";
			string text2 = "INSERT INTO ConfigurationInfo (ConfigInfoID, Name, Value)\r\n                                VALUES (newid(), 'RedirectUrls', @redirectUrlsValue);";
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text + text2))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@redirectUrlsValue", redirectUrls);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004EA4 File Offset: 0x000030A4
		public void DeletePowerBIInformation()
		{
			string text = "DELETE FROM ConfigurationInfo\r\n                                where [NAME] IN ('ClientId', \r\n                                                'AppObjectId', \r\n                                                'TenantName', \r\n                                                'TenantId', \r\n                                                'ClientSecret', \r\n                                                'ResourceUrl', \r\n                                                'AuthorizationUrl', \r\n                                                'TokenUrl', \r\n                                                'RedirectUrls');";
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004EE4 File Offset: 0x000030E4
		public void UpdateClientSecret(string oldValue, string newValue)
		{
			string text = "UPDATE [dbo].[ConfigurationInfo]\r\n                               SET [Value] = @NewValue\r\n                             WHERE Name = 'ClientSecret'\r\n\t                            AND cast(Value as nvarchar(max)) = @OldValue;";
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@NewValue", newValue);
				instrumentedSqlCommand.Parameters.AddWithValue("@OldValue", oldValue);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004F48 File Offset: 0x00003148
		public void DeleteKey(Guid installationID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteKey", base.UnverifiedConnection))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("InstallationID", installationID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004FA4 File Offset: 0x000031A4
		public KeyStorage.AnnoucedKeyResults GetAnnouncedKey(Guid installationID)
		{
			KeyStorage.AnnoucedKeyResults annoucedKeyResults2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetAnnouncedKey", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("InstallationID", installationID);
				KeyStorage.AnnoucedKeyResults annoucedKeyResults = null;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read() && !dataReader.IsDBNull(0))
					{
						byte[] array = DataReaderHelper.ReadAllBytes(dataReader, 0);
						string text = null;
						string text2 = null;
						if (!dataReader.IsDBNull(1))
						{
							text = dataReader.GetString(1);
						}
						if (!dataReader.IsDBNull(2))
						{
							text2 = dataReader.GetString(2);
						}
						annoucedKeyResults = new KeyStorage.AnnoucedKeyResults(array, text, text2);
					}
				}
				if (annoucedKeyResults == null)
				{
					throw new RemotePublicKeyUnavailableException();
				}
				annoucedKeyResults2 = annoucedKeyResults;
			}
			return annoucedKeyResults2;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000506C File Offset: 0x0000326C
		internal int AnnouncePublicKeyOrGetSymmetricKey(Guid installationID, byte[] publicKey, out byte[] symmetricKey)
		{
			string machineName = Environment.MachineName;
			int num2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AnnounceOrGetKey", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@MachineName", machineName);
				instrumentedSqlCommand.Parameters.AddWithValue("@InstanceName", Globals.Configuration.InstanceName);
				instrumentedSqlCommand.Parameters.AddWithValue("@InstallationID", installationID);
				instrumentedSqlCommand.Parameters.AddWithValue("@PublicKey", publicKey);
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@NumAnnouncedServices", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				string text = null;
				symmetricKey = null;
				byte[] array = null;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new ServerConfigurationErrorException("Cannot read matching key record");
					}
					if (!dataReader.IsDBNull(0))
					{
						text = dataReader.GetString(0);
					}
					if (!dataReader.IsDBNull(1))
					{
						symmetricKey = DataReaderHelper.ReadAllBytes(dataReader, 1);
					}
					if (!dataReader.IsDBNull(2))
					{
						array = DataReaderHelper.ReadAllBytes(dataReader, 2);
					}
					if (dataReader.Read())
					{
						throw new ServerConfigurationErrorException("More than one matching key record present");
					}
				}
				int num = (int)sqlParameter.Value;
				if (num > 1)
				{
					if (RSTrace.CryptoTrace.TraceInfo)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Performing sku validation : Scale-Out");
					}
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ScaleOut);
				}
				if (string.Compare(machineName, text, StringComparison.OrdinalIgnoreCase) != 0 && this.ByteArraysAreEqual(publicKey, array))
				{
					using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("SetMachineName", null))
					{
						instrumentedSqlCommand2.Parameters.AddWithValue("@MachineName", machineName);
						instrumentedSqlCommand2.Parameters.AddWithValue("@InstallationID", installationID);
						instrumentedSqlCommand2.ExecuteNonQuery();
					}
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005280 File Offset: 0x00003480
		public void ValidateScaleoutEdition()
		{
			if (this.GetKeyCount() > 1)
			{
				if (RSTrace.CryptoTrace.TraceInfo)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Performing sku validation : scale-out");
				}
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ScaleOut);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000052B8 File Offset: 0x000034B8
		public int GetKeyCount()
		{
			int num;
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery("select count(*) from (select distinct [InstallationID] from [keys] where [SymmetricKey] is not null and [client] >= 0) as A"))
			{
				num = (int)instrumentedSqlCommand.ExecuteScalar();
			}
			return num;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000052FC File Offset: 0x000034FC
		private bool ByteArraysAreEqual(byte[] x, byte[] y)
		{
			if (x == null != (y == null))
			{
				return false;
			}
			if (x == null)
			{
				return true;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0200004B RID: 75
		public class AnnoucedKeyResults
		{
			// Token: 0x06000283 RID: 643 RVA: 0x0000AEE4 File Offset: 0x000090E4
			public AnnoucedKeyResults(byte[] key, string machineName, string instanceName)
			{
				this.m_announcedKey = key;
				this.m_machineName = machineName;
				this.m_instanceName = instanceName;
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000284 RID: 644 RVA: 0x0000AF01 File Offset: 0x00009101
			public byte[] AnnouncedKey
			{
				get
				{
					return this.m_announcedKey;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AF09 File Offset: 0x00009109
			public string MachineName
			{
				get
				{
					return this.m_machineName;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000286 RID: 646 RVA: 0x0000AF11 File Offset: 0x00009111
			public string InstanceName
			{
				get
				{
					return this.m_instanceName;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000287 RID: 647 RVA: 0x0000AF1C File Offset: 0x0000911C
			public string FullName
			{
				get
				{
					string text = this.m_instanceName;
					if (this.m_machineName != null)
					{
						text = this.m_machineName;
						if (!string.IsNullOrEmpty(this.m_instanceName))
						{
							text = text + "\\" + this.m_instanceName;
						}
					}
					return text;
				}
			}

			// Token: 0x040001E2 RID: 482
			private byte[] m_announcedKey;

			// Token: 0x040001E3 RID: 483
			private string m_machineName;

			// Token: 0x040001E4 RID: 484
			private string m_instanceName;
		}
	}
}
