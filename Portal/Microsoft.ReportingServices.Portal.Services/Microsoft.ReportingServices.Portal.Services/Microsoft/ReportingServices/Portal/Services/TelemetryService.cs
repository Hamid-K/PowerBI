using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Principal;
using Microsoft.BIServer.Telemetry.Helpers;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Editions;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.Win32;
using Model;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x02000020 RID: 32
	internal sealed class TelemetryService : ITelemetryService
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public TelemetryService(ITelemetryConfigurationFactory factory, IPortalConfigurationManager configManager, ISystemService systemService)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			if (configManager == null)
			{
				throw new ArgumentNullException("configManager");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			this._telemetryFactory = factory;
			this._configManager = configManager;
			this._systemService = systemService;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000C83A File Offset: 0x0000AA3A
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000C841 File Offset: 0x0000AA41
		private static string Edition { get; set; } = Sku.GetInstalledSku(Globals.Configuration.InstanceID).GetStrings().ShortName;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000C849 File Offset: 0x0000AA49
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000C850 File Offset: 0x0000AA50
		private static string Host { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000C858 File Offset: 0x0000AA58
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000C85F File Offset: 0x0000AA5F
		private static string Build { get; set; } = Globals.Configuration.ServerProductVersion;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000C867 File Offset: 0x0000AA67
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000C86E File Offset: 0x0000AA6E
		private static string MachineId { get; set; } = TelemetryUtils.GetMachineId();

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000C876 File Offset: 0x0000AA76
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000C87D File Offset: 0x0000AA7D
		private static int NumberOfProcessors { get; set; } = TelemetryService.GetNumberOfProcessors();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000C885 File Offset: 0x0000AA85
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000C88C File Offset: 0x0000AA8C
		private static int NumberOfCores { get; set; } = TelemetryService.GetNumberOfCores();

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000C894 File Offset: 0x0000AA94
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000C89B File Offset: 0x0000AA9B
		private static bool IsVirtualMachine { get; set; } = TelemetryService.IsThisVirtualMachine();

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000C8A3 File Offset: 0x0000AAA3
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000C8AA File Offset: 0x0000AAAA
		private static int CountInstances { get; set; } = TelemetryService.GetNumberOfInstances();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000C8B2 File Offset: 0x0000AAB2
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000C8B9 File Offset: 0x0000AAB9
		private static int Count11xInstances { get; set; } = TelemetryService.GetNumberOfInstancesAtVersion("11");

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		private static int Count12xInstances { get; set; } = TelemetryService.GetNumberOfInstancesAtVersion("12");

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		private static int Count13xInstances { get; set; } = TelemetryService.GetNumberOfInstancesAtVersion("13");

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000C8DF File Offset: 0x0000AADF
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		private static int Count14xInstances { get; set; } = TelemetryService.GetNumberOfInstancesAtVersion("14");

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		public TelemetryHostData GetSystemProperties(IIdentity identity)
		{
			TelemetryHostData telemetryHostData = new TelemetryHostData();
			ITelemetryConfiguration telemetryConfiguration = (ITelemetryConfiguration)this._telemetryFactory.Create();
			telemetryHostData.IsEnabled = telemetryConfiguration.IsEnabled;
			if (telemetryHostData.IsEnabled)
			{
				telemetryHostData.ExternalUser = (!TelemetryUtils.IsInternal()).ToString(CultureInfo.InvariantCulture);
				telemetryHostData.IsPublicBuild = telemetryConfiguration.IsPublicBuild;
				telemetryHostData.InstallationId = this._configManager.Current.InstallationId.ToString("D");
				telemetryHostData.Build = TelemetryService.Build;
				telemetryHostData.Host = TelemetryService.Host;
				telemetryHostData.HashedUserId = telemetryConfiguration.GetSHA256Hash(identity.Name);
				telemetryHostData.Edition = TelemetryService.Edition;
				telemetryHostData.AuthenticationTypes = ((AuthenticationTypes)this._configManager.Current.AuthenticationTypes).ToString();
				telemetryHostData.NumberOfProcessors = TelemetryService.NumberOfProcessors;
				telemetryHostData.NumberOfCores = TelemetryService.NumberOfCores;
				telemetryHostData.IsVirtualMachine = TelemetryService.IsVirtualMachine;
				telemetryHostData.MachineId = TelemetryService.MachineId;
				telemetryHostData.CountInstances = TelemetryService.CountInstances;
				telemetryHostData.Count14xInstances = TelemetryService.Count14xInstances;
				telemetryHostData.Count13xInstances = TelemetryService.Count13xInstances;
				telemetryHostData.Count12xInstances = TelemetryService.Count12xInstances;
				telemetryHostData.Count11xInstances = TelemetryService.Count11xInstances;
				telemetryHostData.ProductSku = (this._systemService.IsBiServer() ? ProductSku.SSRSPBI.ToString() : ProductSku.SSRS.ToString());
				telemetryHostData.PortalVersion = "2";
			}
			return telemetryHostData;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000CA71 File Offset: 0x0000AC71
		private static int GetNumberOfInstances()
		{
			return TelemetryService.GetInstanceNames().Count<string>();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		private static IEnumerable<string> GetInstanceNames()
		{
			RegistryKey sqmKey = null;
			List<string> instanceNames = new List<string>();
			RevertImpersonationContext.Run(delegate
			{
				try
				{
					sqmKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Microsoft SQL Server\\Instance Names\\RS");
					if (sqmKey != null)
					{
						foreach (string text in sqmKey.GetValueNames())
						{
							string text2 = sqmKey.GetValue(text) as string;
							if (!string.IsNullOrEmpty(text2))
							{
								instanceNames.Add(text2);
							}
						}
					}
				}
				finally
				{
					if (sqmKey != null)
					{
						sqmKey.Close();
					}
				}
			});
			return instanceNames;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		private static int GetNumberOfInstancesAtVersion(string versionStartsWith)
		{
			int num = 0;
			foreach (string text in TelemetryService.GetInstanceNames())
			{
				string versionOfInstance = TelemetryService.GetVersionOfInstance(text);
				if (!string.IsNullOrEmpty(versionOfInstance) && versionOfInstance.StartsWith(versionStartsWith))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000CB10 File Offset: 0x0000AD10
		private static string GetVersionOfInstance(string instanceName)
		{
			RegistryKey sqmKey = null;
			string version = null;
			RevertImpersonationContext.Run(delegate
			{
				try
				{
					sqmKey = Registry.LocalMachine.OpenSubKey(string.Format("Software\\Microsoft\\Microsoft SQL Server\\{0}\\MSSQLServer\\CurrentVersion", instanceName));
					if (sqmKey != null)
					{
						version = sqmKey.GetValue("CurrentVersion") as string;
					}
				}
				finally
				{
					if (sqmKey != null)
					{
						sqmKey.Close();
					}
				}
			});
			if (!string.IsNullOrEmpty(version))
			{
				return version;
			}
			return string.Empty;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000CB64 File Offset: 0x0000AD64
		private static int GetNumberOfProcessors()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
			int num = 0;
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				num += int.Parse(managementObject["NumberOfProcessors"].ToString());
			}
			return num;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		private static int GetNumberOfCores()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
			int num = 0;
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				num += int.Parse(managementObject["NumberOfCores"].ToString());
			}
			return num;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CC44 File Offset: 0x0000AE44
		private static bool IsThisVirtualMachine()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
			bool flag = false;
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				flag = string.Compare(managementObject["Model"].ToString(), "Virtual Machine") == 0 || string.Compare(managementObject["Model"].ToString(), "VMware Virtual Platform") == 0;
			}
			return flag;
		}

		// Token: 0x0400007D RID: 125
		private readonly ITelemetryConfigurationFactory _telemetryFactory;

		// Token: 0x0400007E RID: 126
		private readonly IPortalConfigurationManager _configManager;

		// Token: 0x0400007F RID: 127
		private readonly ISystemService _systemService;
	}
}
