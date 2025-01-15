using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Owin;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Microsoft.BIServer.Telemetry.Helpers
{
	// Token: 0x02000009 RID: 9
	public static class TelemetryUtils
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002328 File Offset: 0x00000528
		public static bool IsInternal()
		{
			string unsafeHostName = TelemetryUtils.GetUnsafeHostName();
			foreach (string text in new string[] { ".microsoft.com", ".sys-sqlsvr.local" })
			{
				if (unsafeHostName.EndsWith(text, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return TelemetryUtils.IsInternalRegistry;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002378 File Offset: 0x00000578
		private static bool IsInternalRegistry
		{
			get
			{
				if (!TelemetryUtils._isInternalRegistrySet)
				{
					TelemetryUtils._isInternalRegistrySet = true;
					string text = "SOFTWARE\\Policies\\Microsoft\\";
					RegistryKey registryKey = null;
					try
					{
						RegistryKey registryKey2;
						registryKey = (registryKey2 = Registry.LocalMachine.OpenSubKey(text));
						try
						{
							if (registryKey != null)
							{
								object value = registryKey.GetValue("MSFTInternal");
								TelemetryUtils._isInternalRegistry = value is int && (int)value == 1;
							}
						}
						finally
						{
							if (registryKey2 != null)
							{
								((IDisposable)registryKey2).Dispose();
							}
						}
					}
					catch (Exception)
					{
					}
					finally
					{
						if (registryKey != null)
						{
							registryKey.Close();
						}
					}
				}
				return TelemetryUtils._isInternalRegistry;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002418 File Offset: 0x00000618
		public static string GetSHA256Hash(string input)
		{
			if (input == null)
			{
				input = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				foreach (byte b in sha256Cng.ComputeHash(Encoding.UTF8.GetBytes(input)))
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000249C File Offset: 0x0000069C
		public static string GetUnsafeHostName()
		{
			return Dns.GetHostEntry("LocalHost").HostName.ToLowerInvariant();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024B4 File Offset: 0x000006B4
		public static string NormalizeName(IOwinRequest request)
		{
			string value = request.Path.Value;
			string method = request.Method;
			IEnumerable<string> enumerable = from p in value.Split(new char[] { '/' })
				where !Regex.IsMatch(p, "\\w{8}-\\w{4}-\\w{4}-\\w{4}-\\w{12}")
				where !Regex.IsMatch(p, "^[\\w,\\s-\\.]+\\.[A-Za-z]{3,4}$")
				select p;
			string text = string.Join("/", enumerable);
			return method + " " + text;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002548 File Offset: 0x00000748
		public static string GetProductVersion()
		{
			string text = null;
			string location = Assembly.GetExecutingAssembly().Location;
			if (!string.IsNullOrEmpty(location))
			{
				text = FileVersionInfo.GetVersionInfo(location).ProductVersion;
			}
			return text;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002578 File Offset: 0x00000778
		public static Dictionary<string, string> GetMachineDetails()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				dictionary.Add("OSCaption", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "Caption"));
				dictionary.Add("OSLocale", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "Locale"));
				dictionary.Add("OSBuildNumber", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "BuildNumber"));
			}
			catch (Exception)
			{
			}
			return dictionary;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F0 File Offset: 0x000007F0
		public static Dictionary<string, string> GetDiagnosticMachineDetails()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				dictionary.Add("OSCodeSet", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "CodeSet"));
				dictionary.Add("OSDataExecutionPrevention_SupportPolicy", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "DataExecutionPrevention_SupportPolicy"));
				dictionary.Add("OSSericePack", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "CSDVersion"));
				dictionary.Add("OSCurrentTimeZone", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "CurrentTimeZone"));
				dictionary.Add("OSFreePhysicalMemory", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "FreePhysicalMemory"));
				dictionary.Add("OSProductSuite", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "OSProductSuite"));
				dictionary.Add("OSSku", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "OperatingSystemSKU"));
				dictionary.Add("OSLanguage", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "OSLanguage"));
				dictionary.Add("OSSystemDirectory", TelemetryUtils.GetManagementProperty("Win32_OperatingSystem", "SystemDirectory"));
				dictionary.Add("NumberOfCores", TelemetryUtils.GetManagementProperty("Win32_Processor", "NumberOfCores"));
				dictionary.Add("MaxClockSpeed", TelemetryUtils.GetManagementProperty("Win32_Processor", "MaxClockSpeed"));
				dictionary.Add("RSInstances", string.Join(";", TelemetryUtils.GetInstanceNames("RS")));
				dictionary.Add("ASInstances", string.Join(";", TelemetryUtils.GetInstanceNames("AS")));
				dictionary.Add("SQLInstances", string.Join(";", TelemetryUtils.GetInstanceNames("SQL")));
			}
			catch (Exception)
			{
			}
			return dictionary;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027A0 File Offset: 0x000009A0
		private static IEnumerable<string> GetInstanceNames(string product)
		{
			RegistryKey registryKey = null;
			List<string> list = new List<string>();
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Microsoft SQL Server\\Instance Names\\" + product);
				if (registryKey != null)
				{
					foreach (string text in registryKey.GetValueNames())
					{
						string text2 = registryKey.GetValue(text) as string;
						if (!string.IsNullOrEmpty(text2))
						{
							list.Add(text2);
						}
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return list;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002828 File Offset: 0x00000A28
		public static string GetMachineId()
		{
			return TelemetryUtils.GetSHA256Hash(string.Concat(new string[]
			{
				TelemetryUtils.HostName,
				TelemetryUtils.SqmMachineId,
				TelemetryUtils.GetManagementProperty("Win32_BaseBoard", "SerialNumber"),
				TelemetryUtils.GetManagementProperty("Win32_Processor", "ProcessorId"),
				TelemetryUtils.GetManagementProperty("Win32_BIOS", "SerialNumber"),
				TelemetryUtils.GetManagementProperty("Win32_PhysicalMedia", "SerialNumber")
			})).ToUpperInvariant().Substring(0, 32);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028AC File Offset: 0x00000AAC
		public static string GetSerializedEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> measurments = null)
		{
			string text = ((properties == null) ? string.Empty : JsonConvert.SerializeObject(properties));
			string text2 = ((measurments == null) ? string.Empty : JsonConvert.SerializeObject(measurments));
			return string.Format("{0} {1} {2}", eventName, text, text2);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028E8 File Offset: 0x00000AE8
		private static string HostName
		{
			get
			{
				string text = string.Empty;
				try
				{
					text = Dns.GetHostEntry(Environment.MachineName).HostName;
				}
				catch (Exception)
				{
				}
				return text;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002924 File Offset: 0x00000B24
		private static string SqmMachineId
		{
			get
			{
				string text = null;
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\SQMClient");
					if (registryKey == null)
					{
						text = registryKey.GetValue("MachineId") as string;
					}
					if (!string.IsNullOrEmpty(text))
					{
						text = text.Replace("{", "").Replace("}", "");
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029AC File Offset: 0x00000BAC
		private static string GetManagementProperty(string table, string property)
		{
			string empty = string.Empty;
			try
			{
				foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher(string.Format("SELECT {0} FROM {1}", property, table)).Get())
				{
					foreach (PropertyData propertyData in managementBaseObject.Properties)
					{
						if (propertyData.Value != null)
						{
							return propertyData.Value.ToString();
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return empty;
		}

		// Token: 0x0400002E RID: 46
		private static bool _isInternalRegistry;

		// Token: 0x0400002F RID: 47
		private static bool _isInternalRegistrySet;
	}
}
