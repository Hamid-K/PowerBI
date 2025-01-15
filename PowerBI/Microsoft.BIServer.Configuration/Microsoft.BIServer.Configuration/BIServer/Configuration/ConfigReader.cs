using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000015 RID: 21
	public sealed class ConfigReader
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002A98 File Offset: 0x00000C98
		public static ConfigReader Current
		{
			get
			{
				if (ConfigReader._global == null)
				{
					try
					{
						ConfigReader.Current = new ConfigReader(StaticConfig.Current.GetOrException("rsConfigFilePath"));
					}
					catch (Exception ex)
					{
						Logger.Error(ex, "Exception thrown while reading configuration: {0}", new object[] { ex });
						throw;
					}
				}
				return ConfigReader._global;
			}
			set
			{
				ConfigReader._global = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public static string GenerateRsConnectionStringFromSqlInstance(string sqlInstance)
		{
			return new SqlConnectionStringBuilder
			{
				DataSource = sqlInstance,
				InitialCatalog = "ReportServer",
				IntegratedSecurity = true,
				ApplicationName = "Report Server"
			}.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public void ReloadCurrent()
		{
			try
			{
				ConfigReader.Current = new ConfigReader(this._configFileFullPath);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception thrown while reading configuration: {0}", new object[] { ex });
				throw;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002B18 File Offset: 0x00000D18
		public Guid InstallationId
		{
			get
			{
				return this._installationId.Value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002B25 File Offset: 0x00000D25
		public string RsConnectionString
		{
			get
			{
				return this._rsConnectionString.Value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002B32 File Offset: 0x00000D32
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002B3A File Offset: 0x00000D3A
		public string InstanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				this._instanceId = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002B43 File Offset: 0x00000D43
		public string EncryptedCatalogUser
		{
			get
			{
				return this.ReadString("LogonUser");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B50 File Offset: 0x00000D50
		public string CatalogUser
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EncryptedCatalogUser))
				{
					return this.DecryptConfigData(this.EncryptedCatalogUser, "LogonUser");
				}
				return null;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002B72 File Offset: 0x00000D72
		public string EncryptedCatalogDomain
		{
			get
			{
				return this.ReadString("LogonDomain");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B7F File Offset: 0x00000D7F
		public string CatalogDomain
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EncryptedCatalogDomain))
				{
					return this.DecryptConfigData(this.EncryptedCatalogDomain, "LogonDomain");
				}
				return null;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public string EncryptedCatalogCred
		{
			get
			{
				return this.ReadString("LogonCred");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002BAE File Offset: 0x00000DAE
		public string CatalogCred
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EncryptedCatalogCred))
				{
					return this.DecryptConfigData(this.EncryptedCatalogCred, "LogonCred");
				}
				return null;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public CatalogConnectionType ConnectionType
		{
			get
			{
				return (CatalogConnectionType)Enum.Parse(typeof(CatalogConnectionType), this.ReadString("ConnectionType"));
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public IEnumerable<Application> Applications
		{
			get
			{
				return this._applications.Value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002BFE File Offset: 0x00000DFE
		public ServerConfiguration ServerConfiguration
		{
			get
			{
				return this._serverConfiguration.Value;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C0C File Offset: 0x00000E0C
		private static AuthenticationSchemes Convert(AuthenticationTypes authenticationTypes)
		{
			int num = Enum.GetValues(typeof(AuthenticationTypes)).Cast<AuthenticationTypes>().ToArray<AuthenticationTypes>()
				.Max((AuthenticationTypes t) => (int)t);
			AuthenticationSchemes authenticationSchemes = AuthenticationSchemes.None;
			while (num != 0)
			{
				AuthenticationTypes authenticationTypes2 = authenticationTypes & (AuthenticationTypes)num;
				if (authenticationTypes2 <= AuthenticationTypes.Custom)
				{
					switch (authenticationTypes2)
					{
					case AuthenticationTypes.None:
						break;
					case AuthenticationTypes.RSWindowsNegotiate:
					case AuthenticationTypes.RSWindowsKerberos:
						authenticationSchemes |= AuthenticationSchemes.Negotiate;
						break;
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos:
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsNTLM:
					case AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM:
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM:
						goto IL_00AD;
					case AuthenticationTypes.RSWindowsNTLM:
						authenticationSchemes |= AuthenticationSchemes.Ntlm;
						break;
					case AuthenticationTypes.RSWindowsBasic:
						authenticationSchemes |= AuthenticationSchemes.Basic;
						break;
					default:
						if (authenticationTypes2 != AuthenticationTypes.Custom)
						{
							goto IL_00AD;
						}
						goto IL_009D;
					}
				}
				else
				{
					if (authenticationTypes2 == AuthenticationTypes.RSForms)
					{
						throw new NotSupportedException();
					}
					if (authenticationTypes2 != AuthenticationTypes.OAuth)
					{
						goto IL_00AD;
					}
					goto IL_009D;
				}
				IL_00C3:
				num >>= 1;
				continue;
				IL_009D:
				authenticationSchemes |= AuthenticationSchemes.Anonymous;
				goto IL_00C3;
				IL_00AD:
				throw new ArgumentOutOfRangeException("authenticationTypes", authenticationTypes2, "Unknown Authentication Type");
			}
			return authenticationSchemes;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public ConfigReader(string configFileFullPath)
		{
			Logger.Debug("Attempting to read Configuration from file {0} -> {1}", new object[]
			{
				configFileFullPath,
				Path.GetFullPath(configFileFullPath)
			});
			if (!File.Exists(configFileFullPath))
			{
				throw new ConfigException.MissingConfigFile(string.Format("Missing file at path {0}", Path.GetFullPath(configFileFullPath)));
			}
			this._configFileFullPath = configFileFullPath;
			this._rootElement = XElement.Load(configFileFullPath);
			this._webConfigFile = new WebConfigFile(configFileFullPath.Replace("rsreportserver.config", "web.config"));
			this.Init();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002D6C File Offset: 0x00000F6C
		public ConfigReader(string configFileFullPath, string webConfigFileFullPath)
		{
			Logger.Debug("Attempting to read Configuration from file {0} -> {1}", new object[]
			{
				configFileFullPath,
				Path.GetFullPath(configFileFullPath)
			});
			if (!File.Exists(configFileFullPath))
			{
				throw new ConfigException.MissingConfigFile(string.Format("Missing file at path {0}", Path.GetFullPath(configFileFullPath)));
			}
			Logger.Info("Attempting to read Web Configuration from file {0} -> {1}", new object[]
			{
				webConfigFileFullPath,
				Path.GetFullPath(webConfigFileFullPath)
			});
			if (!File.Exists(webConfigFileFullPath))
			{
				throw new ConfigException.MissingConfigFile(string.Format("Missing file at path {0}", Path.GetFullPath(webConfigFileFullPath)));
			}
			this._rootElement = XElement.Load(configFileFullPath);
			this._webConfigFile = new WebConfigFile(webConfigFileFullPath);
			this.Init();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002E13 File Offset: 0x00001013
		public ConfigReader(Stream source, string webConfigFileFullPath = null)
		{
			this._rootElement = XElement.Load(source);
			if (webConfigFileFullPath != null)
			{
				this._webConfigFile = new WebConfigFile(webConfigFileFullPath);
			}
			this.Init();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002E3C File Offset: 0x0000103C
		public IEnumerable<string> GetApplicationHostUrls(string applicationName)
		{
			Application application = this.Applications.FirstOrDefault((Application a) => a.Name == applicationName);
			if (application != null)
			{
				return application.URLs.Select((URL url) => url.UrlString);
			}
			return null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002EA0 File Offset: 0x000010A0
		public string GetApplicationVirtualServerDirectory(string applicationName)
		{
			Application application = this.Applications.FirstOrDefault((Application a) => a.Name == applicationName);
			if (application != null)
			{
				return application.VirtualDirectory;
			}
			return null;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002EE0 File Offset: 0x000010E0
		private void Init()
		{
			this._instanceId = this.ReadString("InstanceId");
			this._applications = new Lazy<IEnumerable<Application>>(() => this.ReadApplications());
			this._installationId = new Lazy<Guid>(() => this.ReadGuid("InstallationID"));
			this._rsConnectionString = new Lazy<string>(() => new SqlConnectionStringBuilder(this.DecryptConfigData(this.ReadString("Dsn"), "Dsn"))
			{
				ApplicationName = Process.GetCurrentProcess().ProcessName,
				MaxPoolSize = this.GetServiceSettings().MaxCatalogConnectionPoolSizePerProcess
			}.ConnectionString);
			this._serverConfiguration = new Lazy<ServerConfiguration>(() => new ServerConfiguration
			{
				LoginUrl = this._webConfigFile.GetLoginUrl(),
				FormsCookieName = this._webConfigFile.GetFormsCookieName(),
				FormsCookieTimeoutInMinutes = this._webConfigFile.GetFormsCookieTimeoutMinutes(),
				FormsCookieSlidingExpiration = this._webConfigFile.GetFormsCookieSlidingExpiration(),
				FormsCookiePath = this._webConfigFile.GetFormsCookiePath(),
				AuthenticationMode = this.GetAuthenticationMode(),
				AuthenticationTypes = this.GetAuthenticationTypes(),
				PassthroughCookies = this.GetPassthroughCookies(),
				AuthenticationExtensions = this.GetAuthenticationExtensions(),
				AuthenticationSchemes = ConfigReader.Convert(this.GetAuthenticationTypes()),
				BasicAuthenticationLogonType = this.GetBasicAuthenticationLogonType(),
				BasicAuthenticationDomain = this.GetBasicAuthenticationDomain(),
				BasicAuthenticationRealm = this.GetBasicAuthenticationRealm(),
				AuthPersistence = this.GetAuthPersistence(),
				MaxActiveReqForOneUser = this.ReadSimpleSettingWithValidation("MaxActiveReqForOneUser", 20, 1, int.MaxValue),
				ReportServerUrls = this.GetApplicationHostUrls("ReportServerWebService"),
				WebAppUrls = this.GetApplicationHostUrls("ReportServerWebApp"),
				ReportServerVirtualDirectory = this.GetApplicationVirtualServerDirectory("ReportServerWebService"),
				ReportServerWebAppVirtualDirectory = this.GetApplicationVirtualServerDirectory("ReportServerWebApp"),
				MachineKey = this.GetMachineKeys(),
				ReportServerUrlOverride = this.GetReportServerUrl(),
				PortalUrlOverride = this.GetUrlRoot(),
				SecureConnectionLevel = this.GetSecureConnectionLevel(),
				Service = this.GetServiceSettings()
			});
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F5C File Offset: 0x0000115C
		private AuthenticationType GetAuthenticationMode()
		{
			AuthenticationType authenticationType = (AuthenticationType)Enum.Parse(typeof(AuthenticationType), this._webConfigFile.GetAuthenticationType(), true);
			if (authenticationType == AuthenticationType.None && this.GetAuthenticationTypes().HasFlag(AuthenticationTypes.OAuth))
			{
				return AuthenticationType.OAuth;
			}
			return authenticationType;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002FAC File Offset: 0x000011AC
		private string DecryptConfigData(string encryptedData, string element)
		{
			string text = null;
			try
			{
				text = MachineKeyCrypto.Instance.DecryptToString(encryptedData);
			}
			catch (Exception ex)
			{
				throw new Exception("FailedToDecryptConfigInformation: " + element, ex);
			}
			return text;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002FF0 File Offset: 0x000011F0
		private IEnumerable<Application> ReadApplications()
		{
			if (!this.ReadElementsXPath<XElement>(this._rootElement, "/URLReservations").Any<XElement>())
			{
				return Enumerable.Empty<Application>();
			}
			return this.ReadRequiredElementsXPath<XElement>(this._rootElement, "/URLReservations/Application", "Application under URLReservations").Select(delegate(XElement app)
			{
				this.ReadRequiredElementsXPath<XElement>(app, "URLs", "URLs under Application");
				IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(app, "URLs/URL");
				IEnumerable<URL> enumerable2 = new List<URL>();
				if (enumerable != null && enumerable.Any<XElement>())
				{
					enumerable2 = enumerable.Select((XElement urlEntry) => URL.Create(ConfigReader.ReadString(urlEntry, "UrlString", true), ConfigReader.ReadString(urlEntry, "AccountName", false), ConfigReader.ReadString(urlEntry, "AccountSid", true)));
				}
				return new Application(ConfigReader.ReadString(app, "Name", true), ConfigReader.ReadString(app, "VirtualDirectory", true), enumerable2.ToArray<URL>());
			});
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003044 File Offset: 0x00001244
		private ServiceSettings GetServiceSettings()
		{
			XElement xelement = this.ReadElementsXPath<XElement>(this._rootElement, "/Service").FirstOrDefault<XElement>();
			ServiceSettings serviceSettings = new ServiceSettings();
			if (xelement != null)
			{
				serviceSettings.MaxQueueThreads = this.ReadIntSettingWithValidation("MaxQueueThreads", xelement, 0, int.MaxValue);
				serviceSettings.PollingInterval = this.ReadIntSettingWithValidation("PollingInterval", xelement, 1, int.MaxValue);
				serviceSettings.IsEventService = this.ReadBoolSettingWithValidation("IsEventService", xelement, false);
				serviceSettings.IsDataModelRefreshService = this.ReadBoolSettingWithValidation("IsDataModelRefreshService", xelement, true);
				serviceSettings.MaxCatalogConnectionPoolSizePerProcess = this.GetMaxPoolsize(xelement);
			}
			return serviceSettings;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030D8 File Offset: 0x000012D8
		private int GetMaxPoolsize(XElement serviceSettings)
		{
			int num = this.ReadIntSettingWithValidation("MaxCatalogConnectionPoolSizePerProcess", serviceSettings, 0, int.MaxValue);
			if (num == 0)
			{
				num = 100;
			}
			Logger.Info(string.Format("Catalog max connection pool size: {0}", num), Array.Empty<object>());
			return num;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000311C File Offset: 0x0000131C
		private MachineKeySettings GetMachineKeys()
		{
			MachineKeySettings machineKeySettings = null;
			XElement xelement = this.ReadElementsXPath<XElement>(this._rootElement, "/MachineKey").FirstOrDefault<XElement>();
			if (xelement != null)
			{
				machineKeySettings = new MachineKeySettings();
				try
				{
					machineKeySettings.Validation = xelement.Attribute("Validation").Value;
					machineKeySettings.ValidationKey = xelement.Attribute("ValidationKey").Value;
					machineKeySettings.Decryption = xelement.Attribute("Decryption").Value;
					machineKeySettings.DecryptionKey = xelement.Attribute("DecryptionKey").Value;
				}
				catch (Exception)
				{
					throw new Exception("Unable to parse MachineKey.");
				}
			}
			return machineKeySettings;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000031DC File Offset: 0x000013DC
		private Guid ReadGuid(string fieldName)
		{
			string text = this.ReadString(fieldName);
			if (!string.IsNullOrEmpty(text))
			{
				return new Guid(text);
			}
			return Guid.Empty;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003205 File Offset: 0x00001405
		private string ReadString(string fieldName)
		{
			return ConfigReader.ReadString(this._rootElement, fieldName, false);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003214 File Offset: 0x00001414
		private string ReadStringAtPath(string path, string defaultValue)
		{
			string text = defaultValue;
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, path);
			if (enumerable != null && enumerable.Count<XElement>() > 0)
			{
				text = enumerable.First<XElement>().Value;
			}
			return text;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000324C File Offset: 0x0000144C
		private static string ReadString(XElement element, string fieldName, bool elementRequired = false)
		{
			XElement xelement = element.Element(fieldName);
			if (xelement != null)
			{
				return xelement.Value;
			}
			if (!elementRequired)
			{
				return null;
			}
			throw new ConfigException.MissingConfigFileEntry(string.Format("Required config entry is missing: {0} under {1}.", fieldName, element.Name));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000328C File Offset: 0x0000148C
		private static string ReadAttribute(XElement element, string attributeName, bool attributeRequired = false)
		{
			XAttribute xattribute = element.Attribute(attributeName);
			if (xattribute != null)
			{
				return xattribute.Value;
			}
			if (!attributeRequired)
			{
				return null;
			}
			throw new ConfigException.MissingConfigFileEntry(string.Format("Required config entry is missing: {0} under {1}.", attributeName, element.Name));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000032CC File Offset: 0x000014CC
		public Dictionary<string, string> ReadKeyValueSettings()
		{
			return this._rootElement.Descendants("Add").ToDictionary((XElement s) => ConfigReader.ReadAttribute(s, "Key", true), (XElement s) => ConfigReader.ReadAttribute(s, "Value", true));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003334 File Offset: 0x00001534
		private string ReadSimpleSetting(string key)
		{
			XElement xelement = this._rootElement.Descendants("Add").FirstOrDefault((XElement s) => ConfigReader.ReadAttribute(s, "Key", true) == key);
			if (xelement == null)
			{
				return null;
			}
			return ConfigReader.ReadAttribute(xelement, "Value", true);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003386 File Offset: 0x00001586
		private int ReadIntSettingWithValidation(string element, XElement configSection, int defaultValue, int minValue, int maxValue)
		{
			if (configSection.Element(element) == null)
			{
				return defaultValue;
			}
			return this.ReadIntSettingWithValidation(element, configSection, minValue, maxValue);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033A4 File Offset: 0x000015A4
		private int ReadIntSettingWithValidation(string element, XElement configSection, int minValue, int maxValue = 2147483647)
		{
			return this.TryGetInt(element, minValue, maxValue, configSection.Element(element).Value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033C4 File Offset: 0x000015C4
		private bool ReadBoolSettingWithValidation(string element, XElement configSection, bool defaultValue = false)
		{
			bool flag = defaultValue;
			if (configSection.Element(element) == null)
			{
				return flag;
			}
			if (!bool.TryParse(configSection.Element(element).Value, out flag))
			{
				throw new ConfigException.InvalidSettingValue(string.Format("Setting {0} has an invalid value.", element));
			}
			return flag;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003410 File Offset: 0x00001610
		private int ReadSimpleSettingWithValidation(string key, int defaultValue, int minValue, int maxValue = 2147483647)
		{
			string text = this.ReadSimpleSetting(key);
			if (string.IsNullOrEmpty(text))
			{
				return defaultValue;
			}
			return this.TryGetInt(key, minValue, maxValue, text);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000343C File Offset: 0x0000163C
		private int TryGetInt(string key, int minValue, int maxValue, string settingValue)
		{
			int num;
			if (!int.TryParse(settingValue, out num))
			{
				throw new ConfigException.InvalidSettingValue(string.Format("Setting {0} has an invalid value.", key));
			}
			if (num < minValue || num > maxValue)
			{
				throw new ConfigException.InvalidSettingValue(string.Format("Setting {0} has a value out of the allowed range.", key));
			}
			return num;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000347F File Offset: 0x0000167F
		private IEnumerable<T> ReadElementsXPath<T>(XElement element, string elementQuery) where T : XObject
		{
			return this.ReadElementsXPath<T>(element, elementQuery, null);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000348A File Offset: 0x0000168A
		private IEnumerable<T> ReadRequiredElementsXPath<T>(XElement element, string elementQuery, string missingElementMessage) where T : XObject
		{
			return this.ReadElementsXPath<T>(element, elementQuery, missingElementMessage);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003498 File Offset: 0x00001698
		private IEnumerable<T> ReadElementsXPath<T>(XElement element, string elementQuery, string missingElementMessage) where T : XObject
		{
			IEnumerable<T> enumerable = ((IEnumerable)element.XPathEvaluate(elementQuery)).Cast<T>();
			if (missingElementMessage != null && !enumerable.Any<T>())
			{
				throw new ConfigException.MissingConfigFileEntry(string.Format("Required config entry is missing: {0}.", missingElementMessage));
			}
			return enumerable;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000034D4 File Offset: 0x000016D4
		private IReadOnlyCollection<Extension> GetAuthenticationExtensions()
		{
			List<Extension> list = new List<Extension>();
			foreach (XElement xelement in this.ReadElementsXPath<XElement>(this._rootElement, "/Extensions/Authentication/Extension"))
			{
				Extension extension = new Extension();
				string[] array = xelement.Attribute("Type").Value.Split(new char[] { ',' });
				extension.Name = xelement.Attribute("Name").Value;
				extension.Class = array[0];
				extension.Assembly = array[1];
				extension.Type = "Authentication";
				IEnumerable<XElement> enumerable = xelement.Descendants("Configuration");
				if (enumerable.Any<XElement>())
				{
					StringBuilder innerXml = new StringBuilder();
					enumerable.Nodes<XElement>().ToList<XNode>().ForEach(delegate(XNode node)
					{
						innerXml.Append(node.ToString());
					});
					extension.Configuration = innerXml.ToString();
				}
				list.Add(extension);
			}
			return list;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003600 File Offset: 0x00001800
		private AuthenticationTypes GetAuthenticationTypes()
		{
			AuthenticationTypes authenticationTypes = AuthenticationTypes.None;
			foreach (XElement xelement in this.ReadElementsXPath<XElement>(this._rootElement, "/Authentication/AuthenticationTypes/*"))
			{
				AuthenticationTypes authenticationTypes2 = AuthenticationTypes.None;
				if (!Enum.TryParse<AuthenticationTypes>(xelement.Name.LocalName, out authenticationTypes2))
				{
					throw new NotSupportedException("Extension not supported.");
				}
				authenticationTypes |= authenticationTypes2;
			}
			return authenticationTypes;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000367C File Offset: 0x0000187C
		private LogonType GetBasicAuthenticationLogonType()
		{
			LogonType logonType = LogonType.Network;
			string text = "/Authentication/AuthenticationTypes/RSWindowsBasic/LogonMethod";
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, text);
			if (enumerable != null && enumerable.Count<XElement>() > 0)
			{
				Enum.TryParse<LogonType>(enumerable.FirstOrDefault<XElement>().Value, out logonType);
			}
			return logonType;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000036C0 File Offset: 0x000018C0
		private string GetBasicAuthenticationDomain()
		{
			string text = string.Empty;
			string text2 = "/Authentication/AuthenticationTypes/RSWindowsBasic/DefaultDomain";
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, text2);
			if (enumerable != null && enumerable.Count<XElement>() > 0)
			{
				text = enumerable.FirstOrDefault<XElement>().Value.ToString();
			}
			return text;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003708 File Offset: 0x00001908
		private string GetBasicAuthenticationRealm()
		{
			string text = string.Empty;
			string text2 = "/Authentication/AuthenticationTypes/RSWindowsBasic/Realm";
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, text2);
			if (enumerable != null && enumerable.Count<XElement>() > 0)
			{
				text = enumerable.FirstOrDefault<XElement>().Value.ToString();
			}
			return text;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003750 File Offset: 0x00001950
		private bool GetAuthPersistence()
		{
			bool flag = false;
			string text = "/Authentication/EnableAuthPersistence";
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, text);
			if (enumerable != null && enumerable.Count<XElement>() > 0)
			{
				bool.TryParse(enumerable.FirstOrDefault<XElement>().Value.ToString(), out flag);
			}
			return flag;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003798 File Offset: 0x00001998
		private StringCollection GetPassthroughCookies()
		{
			StringCollection stringCollection = new StringCollection();
			IEnumerable<XElement> enumerable = this.ReadElementsXPath<XElement>(this._rootElement, "/UI/CustomAuthenticationUI/PassThroughCookies/PassThroughCookie");
			if (!enumerable.Any<XElement>())
			{
				return stringCollection;
			}
			foreach (XElement xelement in enumerable)
			{
				stringCollection.Add(xelement.Value);
			}
			return stringCollection;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000380C File Offset: 0x00001A0C
		internal string GetReportServerUrl()
		{
			return this.ReadStringAtPath("/UI/ReportServerUrl", string.Empty);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000381E File Offset: 0x00001A1E
		private string GetUrlRoot()
		{
			return this.ReadStringAtPath("/Service/UrlRoot", string.Empty);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003830 File Offset: 0x00001A30
		private int GetSecureConnectionLevel()
		{
			return this.ReadSimpleSettingWithValidation("SecureConnectionLevel", 0, 0, 3);
		}

		// Token: 0x04000098 RID: 152
		private static ConfigReader _global;

		// Token: 0x04000099 RID: 153
		private readonly XElement _rootElement;

		// Token: 0x0400009A RID: 154
		private readonly WebConfigFile _webConfigFile;

		// Token: 0x0400009B RID: 155
		private readonly string _configFileFullPath;

		// Token: 0x0400009C RID: 156
		private const string UrlReservationsQuery = "/URLReservations";

		// Token: 0x0400009D RID: 157
		private const string ApplicationsQuery = "/URLReservations/Application";

		// Token: 0x0400009E RID: 158
		private const string ApplicationUrlsQuery = "URLs/URL";

		// Token: 0x0400009F RID: 159
		private const string UrlsQuery = "URLs";

		// Token: 0x040000A0 RID: 160
		private const string ReportServerUrlQuery = "/UI/ReportServerUrl";

		// Token: 0x040000A1 RID: 161
		private const string UrlRootQuery = "/Service/UrlRoot";

		// Token: 0x040000A2 RID: 162
		private const string ServiceSettingsQuery = "/Service";

		// Token: 0x040000A3 RID: 163
		private const string AuthenticationTypesQuery = "/Authentication/AuthenticationTypes/*";

		// Token: 0x040000A4 RID: 164
		private const string PassThroughCookiesQuery = "/UI/CustomAuthenticationUI/PassThroughCookies/PassThroughCookie";

		// Token: 0x040000A5 RID: 165
		private const string AuthenticationExtensionQuery = "/Extensions/Authentication/Extension";

		// Token: 0x040000A6 RID: 166
		private const string MachineKeyQuery = "/MachineKey";

		// Token: 0x040000A7 RID: 167
		private Lazy<Guid> _installationId;

		// Token: 0x040000A8 RID: 168
		private Lazy<string> _rsConnectionString;

		// Token: 0x040000A9 RID: 169
		private string _instanceId;

		// Token: 0x040000AA RID: 170
		private Lazy<IEnumerable<Application>> _applications;

		// Token: 0x040000AB RID: 171
		private Lazy<ServerConfiguration> _serverConfiguration;

		// Token: 0x040000AC RID: 172
		public const string ReportServerDbName = "ReportServer";
	}
}
