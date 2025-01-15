using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.WmiProvider;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000230 RID: 560
	internal class RPCDelegationHandler : INativeServerDelegation
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x0004BA63 File Offset: 0x00049C63
		public RPCDelegationHandler(ConfigurationChangeEventHandler restartService)
		{
			this.m_restartService = restartService;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004BA74 File Offset: 0x00049C74
		public int ApplyKey(string encryptPassword, byte[] encryptedKey, out string[] extendedErrors)
		{
			extendedErrors = null;
			int num;
			try
			{
				Activation.SetKeyForThisInstallation(encryptedKey, encryptPassword);
				this.RestartWindowsService();
				num = 0;
			}
			catch (Exception ex)
			{
				int hrforException = Marshal.GetHRForException(ex);
				if (!(ex is RSException))
				{
					if (hrforException == -2147022651)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, ex.ToString());
					}
					else
					{
						Dumper.Current.DumpHere(ex);
					}
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = hrforException;
			}
			return num;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004BAEC File Offset: 0x00049CEC
		public int ExtractKey(string encryptPassword, out byte[] encryptedKey, out string[] extendedErrors)
		{
			encryptedKey = null;
			extendedErrors = null;
			int num;
			try
			{
				ConnectionManager.Init();
				encryptedKey = Activation.ExtractKey(encryptPassword);
				num = 0;
			}
			catch (Exception ex)
			{
				int hrforException = Marshal.GetHRForException(ex);
				if (!(ex is RSException))
				{
					if (hrforException == -2147022651)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, ex.ToString());
					}
					else
					{
						Dumper.Current.DumpHere(ex);
					}
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = hrforException;
			}
			return num;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0004BB64 File Offset: 0x00049D64
		public int DeleteKey(string installationID, out string[] extendedErrors)
		{
			extendedErrors = null;
			int num;
			try
			{
				Guid guid = Globals.ParseGuidParameter(installationID, "InstallationID");
				Activation.DeleteKey(guid);
				if (guid == Globals.Configuration.InstallationID)
				{
					this.RestartWindowsService();
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004BBD8 File Offset: 0x00049DD8
		public int DeleteEncryptedContent(out string[] extendedErrors)
		{
			extendedErrors = null;
			int num;
			try
			{
				Activation.DeleteEncryptedContent();
				this.RestartWindowsService();
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004BC30 File Offset: 0x00049E30
		public int ActivateService(string targetInstallationID, out string[] extendedErrors)
		{
			extendedErrors = null;
			int num;
			try
			{
				ConnectionManager.Init();
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ScaleOut);
				Activation.ActivateService(Globals.ParseGuidParameter(targetInstallationID, "InstallationID"));
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004BCA4 File Offset: 0x00049EA4
		public int CatalogEncrypt(byte[] decryptedData, out byte[] encryptedData, out string[] extendedErrors)
		{
			encryptedData = null;
			extendedErrors = null;
			byte[] array = null;
			int num;
			try
			{
				ConnectionManager.Init();
				array = MachineKeyEncryption.Instance.Decrypt(decryptedData, true);
				encryptedData = CatalogEncryption.Instance.Encrypt(array, null);
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			finally
			{
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = 0;
					}
				}
			}
			return num;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0004BD38 File Offset: 0x00049F38
		public int CatalogDecrypt(byte[] encryptedData, bool useSalt, out byte[] decryptedData, out string[] extendedErrors)
		{
			decryptedData = null;
			extendedErrors = null;
			int num;
			try
			{
				ConnectionManager.Init();
				byte[] array = CatalogEncryption.Instance.Decrypt(encryptedData, useSalt);
				if (array != null)
				{
					decryptedData = MachineKeyEncryption.Instance.Encrypt(array);
				}
				extendedErrors = null;
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		public int SavePowerBIInformation(string clientId, string clientSecret, string appObjectId, string tenantName, string tenantId, string resourceUrl, string authUrl, string tokenUrl, string redirectUrls)
		{
			int num;
			try
			{
				ConnectionManager.Init();
				string text = CatalogEncryption.Instance.EncryptToString(clientSecret);
				using (ActivationDB activationDB = new ActivationDB())
				{
					activationDB.WillDispose();
					activationDB.SavePowerBIInformation(clientId, text, appObjectId, tenantName, tenantId, resourceUrl, authUrl, tokenUrl, redirectUrls);
					activationDB.Commit();
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0004BE40 File Offset: 0x0004A040
		public int GetPowerBIInformation(out string clientId, out string appObjectId, out string tenantName, out string tenantId, out string resourceUrl, out string authUrl, out string tokenUrl, out string redirectUrls)
		{
			int num;
			try
			{
				ConnectionManager.Init();
				using (ActivationDB activationDB = new ActivationDB())
				{
					activationDB.WillDispose();
					activationDB.GetPowerBIInformation(out clientId, out appObjectId, out tenantName, out tenantId, out resourceUrl, out authUrl, out tokenUrl, out redirectUrls);
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				clientId = null;
				appObjectId = null;
				tenantName = null;
				tenantId = null;
				resourceUrl = null;
				authUrl = null;
				tokenUrl = null;
				redirectUrls = null;
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0004BED8 File Offset: 0x0004A0D8
		public int UpdatePowerBIInformation(string redirectUrls)
		{
			int num;
			try
			{
				ConnectionManager.Init();
				using (ActivationDB activationDB = new ActivationDB())
				{
					activationDB.WillDispose();
					activationDB.UpdatePowerBIInformation(redirectUrls);
					activationDB.Commit();
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0004BF50 File Offset: 0x0004A150
		public int DeletePowerBIInformation()
		{
			int num;
			try
			{
				ConnectionManager.Init();
				using (ActivationDB activationDB = new ActivationDB())
				{
					activationDB.WillDispose();
					activationDB.DeletePowerBIInformation();
					activationDB.Commit();
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		public int ListReportServersInDB(out string[] machineNames, out string[] instanceNames, out string[] installationIDs, out byte[] flags, out string[] extendedErrors)
		{
			machineNames = null;
			instanceNames = null;
			installationIDs = null;
			flags = null;
			extendedErrors = null;
			int num;
			try
			{
				Activation.ListReportServersInDB(out machineNames, out instanceNames, out installationIDs, out flags);
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004C028 File Offset: 0x0004A228
		public int ReencryptSecureInformation(out string[] extendedErrors)
		{
			extendedErrors = null;
			if (RSTrace.CryptoTrace.TraceInfo)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Info, "ReencryptSecureInformation: Starting.");
			}
			int num;
			try
			{
				ConnectionManager.Init();
				try
				{
					using (Reencryptor reencryptor = new Reencryptor())
					{
						reencryptor.WillDispose();
						reencryptor.ReencryptSecureInformation();
					}
				}
				finally
				{
					this.RestartWindowsService();
				}
				if (RSTrace.CryptoTrace.TraceInfo)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Info, "ReencryptSecureInformation: Done.");
				}
				num = 0;
			}
			catch (Exception ex)
			{
				if (!(ex is RSException))
				{
					Dumper.Current.DumpHere(ex);
				}
				extendedErrors = this.ConvertExceptionToMessageList(ex);
				num = Marshal.GetHRForException(ex);
			}
			return num;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		public int GetInstanceVersion(out string instanceVersion)
		{
			instanceVersion = ConnectionManager.ReportingServicesVersionNumber;
			return 0;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		public int GetCredentials(int credentialsType, Guid dataSourceId, out string userName, out string domainName, out string password)
		{
			userName = null;
			domainName = null;
			password = null;
			switch (credentialsType)
			{
			case 1:
				this.GetDataSourceCredentials(dataSourceId, out userName, out domainName, out password);
				break;
			case 2:
				this.GetCatalogCredentials(out userName, out domainName, out password);
				break;
			case 3:
				this.GetUnattendedCredentials(out userName, out domainName, out password);
				break;
			default:
				throw new InternalCatalogException("unexpected credentials type" + credentialsType.ToString(CultureInfo.InvariantCulture));
			}
			return 0;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004C171 File Offset: 0x0004A371
		public int GetInstanceBuildVersion(out string instanceBuildVersion)
		{
			instanceBuildVersion = Globals.Configuration.ServerProductVersion;
			return 0;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004C180 File Offset: 0x0004A380
		public int GetInstanceUpgradeScript(string databaseName, out string instanceUpgradeScript)
		{
			instanceUpgradeScript = UpgradeScripts.GetUpgradeScript(databaseName);
			return 0;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004C18B File Offset: 0x0004A38B
		private void GetUnattendedCredentials(out string userName, out string domainName, out string password)
		{
			userName = Globals.Configuration.SurrogateUserName;
			domainName = Globals.Configuration.SurrogateDomain;
			password = Globals.Configuration.SurrogatePassword;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004C1B1 File Offset: 0x0004A3B1
		private void GetCatalogCredentials(out string userName, out string domainName, out string password)
		{
			userName = Globals.Configuration.CatalogUser;
			domainName = Globals.Configuration.CatalogDomain;
			password = Globals.Configuration.CatalogCred;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
		private void GetDataSourceCredentials(Guid dataSourceId, out string userName, out string domainName, out string password)
		{
			ActivationDB activationDB = null;
			try
			{
				activationDB = new ActivationDB();
				activationDB.WillDispose();
				int num;
				byte[] array;
				byte[] array2;
				byte[] array3;
				byte[] array4;
				int num2;
				activationDB.GetDatasourceInfoForReencryption(dataSourceId, out num, out array, out array2, out array3, out array4, out num2);
				DataSourceHelper dataSourceHelper = new DataSourceHelper(array3, array4, DataProtection.Instance);
				userName = dataSourceHelper.GetUserName();
				domainName = dataSourceHelper.GetDomainName();
				password = dataSourceHelper.GetPassword();
			}
			finally
			{
				activationDB.Dispose();
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004C24C File Offset: 0x0004A44C
		private string[] ConvertExceptionToMessageList(Exception e)
		{
			List<string> list = new List<string>();
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				RSException ex2 = ex as RSException;
				if (ex2 != null)
				{
					list.Add(RepLibRes.RPCErrorStringFormat(ex2.Message, ex2.Code.ToString()));
				}
				else
				{
					list.Add(ex.Message);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004C2B0 File Offset: 0x0004A4B0
		private void RestartWindowsService()
		{
			new Thread(new ThreadStart(this.RestartWindowsServiceThreadMain)).Start();
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004C2C8 File Offset: 0x0004A4C8
		private void RestartWindowsServiceThreadMain()
		{
			this.m_restartService(this, new ConfigurationChangeEventArgs(null, false));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004C2DD File Offset: 0x0004A4DD
		public int ListInstalledSharePointVersions(out string[] versionTokens)
		{
			versionTokens = null;
			return 0;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004C2E4 File Offset: 0x0004A4E4
		public int GetDatabaseVersionDisplayName(string version, out string displayName)
		{
			if (!(version == "C.0.6.54"))
			{
				if (!(version == "C.0.8.43"))
				{
					if (!(version == "C.0.8.54"))
					{
						if (!(version == "C.0.9.18"))
						{
							if (!(version == "C.0.9.34"))
							{
								displayName = string.Empty;
								return -2147220960;
							}
							displayName = "SQL Server 2008 CTP6";
						}
						else
						{
							displayName = "SQL Server 2008 CTP5";
						}
					}
					else
					{
						displayName = "SQL Server 2005 SP2";
					}
				}
				else
				{
					displayName = "SQL Server 2005 SP1";
				}
			}
			else
			{
				displayName = "SQL Server 2000 SP2";
			}
			return 0;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004C370 File Offset: 0x0004A570
		public int GetAdminSiteUrl(out string adminSiteUrl)
		{
			adminSiteUrl = string.Empty;
			int num;
			try
			{
				throw new WMIProviderException((ErrorCodes)2147746353U);
			}
			catch (WMIProviderException ex)
			{
				num = (int)ex.ErrorCode;
			}
			catch (ServerConfigurationErrorException)
			{
				num = Convert.ToInt32((ErrorCodes)2147746356U, CultureInfo.InvariantCulture);
			}
			catch (RSException ex2)
			{
				num = (int)ex2.Code;
			}
			catch (Exception ex3)
			{
				if (!(ex3 is RSException))
				{
					Dumper.Current.DumpHere(ex3);
				}
				num = Marshal.GetHRForException(ex3);
			}
			catch
			{
				Dumper.Current.DumpHere(null);
				num = -2147467259;
			}
			return num;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004C42C File Offset: 0x0004A62C
		public int GetSharePointIntegration(out bool isSharePointIntegrated)
		{
			isSharePointIntegrated = false;
			return 0;
		}

		// Token: 0x0400071E RID: 1822
		private const int E_FAIL = -2147467259;

		// Token: 0x0400071F RID: 1823
		private const int S_OK = 0;

		// Token: 0x04000720 RID: 1824
		private ConfigurationChangeEventHandler m_restartService;
	}
}
