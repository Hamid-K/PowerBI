using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.Exceptions;
using Microsoft.BIServer.HostingEnvironment.ManagementAdapter;
using Newtonsoft.Json;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000047 RID: 71
	[CLSCompliant(false)]
	public class HostingState
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x000062FC File Offset: 0x000044FC
		public static Dictionary<string, string> FormatForProcessConfig(Dictionary<string, bool> configSwitches, string rsConfigFilePath, ManagementState managementState, Dictionary<string, Uri> urls)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string fullPath = Path.GetFullPath(rsConfigFilePath);
			if (!File.Exists(fullPath))
			{
				throw new ArgumentException("Service configuration file not found [{0}]", fullPath);
			}
			HostingState.StoreEncryptionKeyInEnvironment(managementState, dictionary);
			HostingState.StoreProcessProperty(dictionary, "rsConfigFilePath", fullPath);
			HostingState.StoreProcessProperty(dictionary, "Hosting-databaseValidationStatus", managementState.DatabaseValidationStatus.ToString());
			foreach (KeyValuePair<string, Uri> keyValuePair in urls)
			{
				HostingState.StoreProcessProperty(dictionary, "Hosting-url-" + keyValuePair.Key, keyValuePair.Value.ToString());
			}
			HostingState.StoreProcessProperty(dictionary, "Hosting-configSwitches", HostingState.ObjectToJsonTransportableString<Dictionary<string, bool>>(configSwitches));
			return dictionary;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000063CC File Offset: 0x000045CC
		private static void StoreEncryptionKeyInEnvironment(ManagementState managementState, Dictionary<string, string> processConfigInfo)
		{
			if (managementState.EncryptionKey != null && managementState.EncryptionKey.Length != 0)
			{
				HostingState.StoreProcessProperty(processConfigInfo, "Hosting-encryptionKey", Convert.ToBase64String(managementState.EncryptionKey));
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000063F5 File Offset: 0x000045F5
		private static void StoreProcessProperty(Dictionary<string, string> processConfigInfo, string key, string value)
		{
			Logger.Debug(key, new object[] { value, "Storing to SubProcess Environment" });
			processConfigInfo[key] = value;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006417 File Offset: 0x00004617
		private static byte[] FetchEncryptionKeyFromEnvironment()
		{
			string orDefault = StaticConfig.Current.GetOrDefault("Hosting-encryptionKey", null);
			if (string.IsNullOrWhiteSpace(orDefault))
			{
				throw new HostingEnvironmentException("Symmetric Key not found in Hosted Process Context", Array.Empty<object>());
			}
			return Convert.FromBase64String(orDefault);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006448 File Offset: 0x00004648
		private static string ObjectToJsonTransportableString<T>(T configSwitches)
		{
			string text = JsonConvert.SerializeObject(configSwitches);
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006471 File Offset: 0x00004671
		private static T JsonTransportableStringToObject<T>(string encoded)
		{
			return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(encoded)));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006488 File Offset: 0x00004688
		public static void InitializeInHostedProcess()
		{
			StaticConfig current = StaticConfig.Current;
			string orException = current.GetOrException("rsConfigFilePath");
			DatabaseValidationStatus databaseValidationStatus = HostingState.ParseDatabaseValidationStatus(current.GetOrException("Hosting-databaseValidationStatus"));
			Uri uri = new Uri(current.GetOrDefault("Hosting-url-ManagementService", "http://managementUrl/NotSet"));
			Uri uri2 = new Uri(current.GetOrDefault("Hosting-url-ReportServerWebApp", "http://portalUrl/NotSet"));
			ICrypto crypto = NoCrypto.Instance;
			if (databaseValidationStatus == DatabaseValidationStatus.Valid)
			{
				byte[] array = HostingState.FetchEncryptionKeyFromEnvironment();
				SymmetricKeyCrypto.Instance.SetPublicKeyEncryptedSymmetricKey(array);
				crypto = SymmetricKeyCrypto.Instance;
			}
			HostingState._hostedProcessInstance = new HostingState(orException, crypto, databaseValidationStatus, new ManagementService(uri), uri2);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000651C File Offset: 0x0000471C
		public static void InitializeInTestProcess()
		{
			HostingState.InitializeInTestProcess(NoCrypto.Instance, DatabaseValidationStatus.Valid, null, new Uri("//localhost/portal"), ".");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006539 File Offset: 0x00004739
		public static void InitializeInTestProcess(ICrypto crypto, DatabaseValidationStatus databaseValidationStatus, IManagementService managementService, Uri portalUrl, string rsConfigFilePath = ".")
		{
			HostingState._hostedProcessInstance = new HostingState(rsConfigFilePath, crypto, databaseValidationStatus, managementService, portalUrl);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000654B File Offset: 0x0000474B
		public static HostingState Current
		{
			get
			{
				if (HostingState._hostedProcessInstance == null)
				{
					HostingState.InitializeInHostedProcess();
				}
				return HostingState._hostedProcessInstance;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000655E File Offset: 0x0000475E
		private HostingState(string rsConfigFilePath, ICrypto catalogCrypto, DatabaseValidationStatus databaseValidationStatus, IManagementService managementService, Uri portalUrl)
		{
			Logger.Info("Setting up Hosted Process State", Array.Empty<object>());
			this._rsConfigFilePath = rsConfigFilePath;
			this._catalogCrypto = catalogCrypto;
			this._databaseValidationStatus = databaseValidationStatus;
			this._portalUrl = portalUrl;
			this._managementService = managementService;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000659C File Offset: 0x0000479C
		private static DatabaseValidationStatus ParseDatabaseValidationStatus(string str)
		{
			DatabaseValidationStatus databaseValidationStatus;
			if (!Enum.TryParse<DatabaseValidationStatus>(str, out databaseValidationStatus))
			{
				throw new ArgumentException("Hosting-databaseValidationStatus was not a valid DatabaseValidationStatus enumeration");
			}
			return databaseValidationStatus;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000065BF File Offset: 0x000047BF
		public string RsConfigFilePath
		{
			get
			{
				return this._rsConfigFilePath;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000065C7 File Offset: 0x000047C7
		public DatabaseValidationStatus DatabaseValidationStatus
		{
			get
			{
				return this._databaseValidationStatus;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000065CF File Offset: 0x000047CF
		public bool IsConfigSwitchEnabled(ConfigSwitches configSetting, bool defaultSwitchValue)
		{
			return StaticConfig.Current.GetOrDefault(configSetting.ToString(CultureInfo.InvariantCulture), defaultSwitchValue);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000065EE File Offset: 0x000047EE
		public IManagementService ManagementService
		{
			get
			{
				return this._managementService;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000065F6 File Offset: 0x000047F6
		public Uri PortalUrl
		{
			get
			{
				return this._portalUrl;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000065FE File Offset: 0x000047FE
		public ICrypto CatalogCrypto
		{
			get
			{
				return this._catalogCrypto;
			}
		}

		// Token: 0x04000100 RID: 256
		private static HostingState _hostedProcessInstance;

		// Token: 0x04000101 RID: 257
		private readonly string _rsConfigFilePath;

		// Token: 0x04000102 RID: 258
		private readonly ICrypto _catalogCrypto;

		// Token: 0x04000103 RID: 259
		private readonly DatabaseValidationStatus _databaseValidationStatus;

		// Token: 0x04000104 RID: 260
		private readonly Uri _portalUrl;

		// Token: 0x04000105 RID: 261
		private readonly IManagementService _managementService;

		// Token: 0x04000106 RID: 262
		public const string RsConfigFilePathKey = "rsConfigFilePath";

		// Token: 0x04000107 RID: 263
		private const string HostingCatalogCryptoBlobKey = "Hosting-encryptionKey";

		// Token: 0x04000108 RID: 264
		private const string HostingDbstatusKey = "Hosting-databaseValidationStatus";

		// Token: 0x04000109 RID: 265
		private const string HostingConfigSwitchesKey = "Hosting-configSwitches";
	}
}
