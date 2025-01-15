using System;
using System.IO;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Win32;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000020 RID: 32
	public sealed class InstanceRegistry
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00004B0F File Offset: 0x00002D0F
		public InstanceRegistry(InstanceId instanceId, InstanceName instanceName, IRegistryStore registryStore)
		{
			this._instanceName = instanceName;
			this._instanceId = instanceId;
			this._registryStore = registryStore;
			this._instancePath = this._instanceId.ToString();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004B42 File Offset: 0x00002D42
		public InstanceRegistry(InstanceId instanceId, InstanceName instanceName)
			: this(instanceId, instanceName, new RegistryStore(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Microsoft SQL Server"))
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B5C File Offset: 0x00002D5C
		public void CreateInstance()
		{
			this._registryStore.SetValue(this._instancePath, "", this._instanceName.ToString());
			this._registryStore.SetValue("Instance Names\\RS", this._instanceName.ToString(), this._instanceId.ToString());
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public void RegisterVirtualRootAndConfigFileLocation(RSPath configFilePath)
		{
			this._registryStore.SetValue(this._instancePath + "Setup", "RSVirtualRootServer", "ReportServer");
			this._registryStore.SetValue(this._instancePath + "Setup", "RsConfigFilePath", configFilePath);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004C1A File Offset: 0x00002E1A
		public void RegisterServiceName(string serviceName)
		{
			this._registryStore.SetValue(this._instancePath + "Setup", "ServiceName", serviceName);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004C42 File Offset: 0x00002E42
		public void AddWmiVersion(string version)
		{
			this._registryStore.SetValue(this.CurrentVersionPath(this._instancePath), "CurrentVersion", version);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004C66 File Offset: 0x00002E66
		public void WriteProductVersion(string version)
		{
			this._registryStore.SetValue(this.CurrentVersionPath(this._instancePath), "ProductVersion", version);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004C8A File Offset: 0x00002E8A
		private RSPath CurrentVersionPath(RSPath instancePath)
		{
			return instancePath + "MSSQLServer\\CurrentVersion";
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004C97 File Offset: 0x00002E97
		public string GetProductVersion()
		{
			return this.GetProductVersion(InstanceRegistry.CurrentInstancePath);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public string GetProductVersion(RSPath instancePath)
		{
			return this._registryStore.GetValue(instancePath + "MSSQLServer\\CurrentVersion", "ProductVersion");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004CC6 File Offset: 0x00002EC6
		public static string GetCurrentProductVersion()
		{
			return Registry.GetValue(InstanceRegistry.GetCurrentVersionKey(), "ProductVersion", "Unknown").ToString();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004CE1 File Offset: 0x00002EE1
		public static string GetCurrentVersionKey()
		{
			return Path.Combine("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Microsoft SQL Server", InstanceRegistry.CurrentInstancePath, "MSSQLServer\\CurrentVersion");
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004CFC File Offset: 0x00002EFC
		private static RSPath CurrentInstancePath
		{
			get
			{
				return new RSPath(ConfigReader.Current.InstanceId);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004D0D File Offset: 0x00002F0D
		public void DeleteInstance()
		{
			this._registryStore.DeleteSubKey(null, this._instanceId.ToString());
			this._registryStore.DeleteValue("Instance Names\\RS", this._instanceName.ToString());
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004D44 File Offset: 0x00002F44
		public void SetTelemetryRegistryKeys(bool isEnabled)
		{
			int num = (isEnabled ? 1 : 0);
			this._registryStore.SetValue(this._instancePath + "CPE", "CustomerFeedback", num);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004D80 File Offset: 0x00002F80
		public void SetErrorReportingRegistryKeys(bool isEnabled)
		{
			int num = (isEnabled ? 1 : 0);
			this._registryStore.SetValue(this._instancePath + "CPE", "EnableErrorReporting", num);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004DBB File Offset: 0x00002FBB
		public void SetErrorDumperRegistry(string path)
		{
			this._registryStore.SetValue(this._instancePath + "CPE", "ErrorDumpDir", path);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public void DeleteTelemetryAndErrorReportingKey()
		{
			this._registryStore.DeleteSubKey(this._instancePath, "CPE");
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004E00 File Offset: 0x00003000
		public void SaveInstallRootDirectory(string installDir)
		{
			this._registryStore.SetValue(this._instancePath + "Setup", "InstallRootDirectory", installDir);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004E28 File Offset: 0x00003028
		public string GetInstanceInstallationDirectory()
		{
			string value = this._registryStore.GetValue(this._instancePath + "Setup", "InstallRootDirectory");
			if (value != null)
			{
				return Path.Combine(value, this._instanceId.ToString());
			}
			return null;
		}

		// Token: 0x040000D4 RID: 212
		private const string CpeKey = "CPE";

		// Token: 0x040000D5 RID: 213
		private const string InstallRootDirectoryKey = "InstallRootDirectory";

		// Token: 0x040000D6 RID: 214
		private const string ErrorDumperDirKey = "ErrorDumpDir";

		// Token: 0x040000D7 RID: 215
		private const string EnableErrorReportingKey = "EnableErrorReporting";

		// Token: 0x040000D8 RID: 216
		private const string CustomerFeedbackKey = "CustomerFeedback";

		// Token: 0x040000D9 RID: 217
		private const string SetupKey = "Setup";

		// Token: 0x040000DA RID: 218
		private const string CurrentVersionKey = "CurrentVersion";

		// Token: 0x040000DB RID: 219
		private const string ProductVersionKey = "ProductVersion";

		// Token: 0x040000DC RID: 220
		private const string RSVirtualRootServerKey = "RSVirtualRootServer";

		// Token: 0x040000DD RID: 221
		private const string RsConfigFilePathKey = "RsConfigFilePath";

		// Token: 0x040000DE RID: 222
		private const string ServiceNameKey = "ServiceName";

		// Token: 0x040000DF RID: 223
		private const string InstanceNamesPath = "Instance Names\\RS";

		// Token: 0x040000E0 RID: 224
		private const string RSVirtualRootServerDefault = "ReportServer";

		// Token: 0x040000E1 RID: 225
		private readonly InstanceId _instanceId;

		// Token: 0x040000E2 RID: 226
		private readonly RSPath _instancePath;

		// Token: 0x040000E3 RID: 227
		private readonly IRegistryStore _registryStore;

		// Token: 0x040000E4 RID: 228
		private InstanceName _instanceName;
	}
}
