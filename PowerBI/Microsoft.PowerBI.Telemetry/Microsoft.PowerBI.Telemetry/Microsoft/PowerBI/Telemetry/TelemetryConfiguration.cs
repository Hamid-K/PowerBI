using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.PowerBI.Telemetry.Utils;
using Microsoft.Win32;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000023 RID: 35
	public class TelemetryConfiguration : ITelemetryConfiguration
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002F88 File Offset: 0x00001188
		public TelemetryConfiguration(string appVersion, List<ILoggerService> loggers, string appDirName, string appName, string fullPath = null, bool useStringHashCodeForDeviceId = false)
		{
			this.productVersion = ((!string.IsNullOrEmpty(appVersion)) ? appVersion : "Unknown");
			if (string.IsNullOrEmpty(fullPath))
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				string text = ((!string.IsNullOrEmpty(appDirName)) ? appDirName : "Power BI Desktop");
				this.appDirPath = Path.Combine(folderPath, text);
			}
			else
			{
				this.appDirPath = fullPath;
			}
			this.application = ((!string.IsNullOrEmpty(appName)) ? appName : "WinDesktop");
			if (loggers != null)
			{
				this.Loggers = loggers;
			}
			else
			{
				this.Loggers = new List<ILoggerService>();
			}
			this.useStringHashCodeForDeviceId = useStringHashCodeForDeviceId;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003021 File Offset: 0x00001221
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003029 File Offset: 0x00001229
		public List<ILoggerService> Loggers { get; protected set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x00003034 File Offset: 0x00001234
		public bool GetOrCreateAppDir(out DirectoryInfo dirInfo)
		{
			dirInfo = null;
			try
			{
				using (GlobalMutex.CreateAndAquire("Global\\PBITelemetryConfigMutex", 10000))
				{
					dirInfo = new DirectoryInfo(this.appDirPath);
					if (dirInfo != null && !dirInfo.Exists)
					{
						dirInfo.Create();
					}
				}
			}
			catch (TimeoutException)
			{
			}
			catch (Exception ex)
			{
				if (ex is ArgumentNullException || ex is SecurityException || ex is ArgumentException || ex is PathTooLongException || ex is GlobalMutexException || ex is IOException)
				{
					return false;
				}
				throw;
			}
			return dirInfo != null && dirInfo.Exists;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000030F4 File Offset: 0x000012F4
		public HostData HostData
		{
			get
			{
				if (string.IsNullOrEmpty(this.userId))
				{
					this.userId = this.GetUserId(out this.newUser);
				}
				return new HostData(this.GetDeviceId() ?? "Unknown", this.GetSessionId() ?? "Unknown", this.application ?? "Unknown", this.productVersion ?? "Unknown", "InProc", this.userId ?? "Unknown", this.GetIsInternalUser(), !this.newUser, Thread.CurrentThread.CurrentCulture.Name, Thread.CurrentThread.CurrentUICulture.Name, this.GetAuthenticatedUserId() ?? "Unknown", this.GetApplicationType(), this.GetTenantId());
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000031C0 File Offset: 0x000013C0
		protected virtual string GetUserId(out bool createdNew)
		{
			createdNew = false;
			string text = null;
			DirectoryInfo directoryInfo;
			if (this.GetOrCreateAppDir(out directoryInfo))
			{
				try
				{
					using (GlobalMutex.CreateAndAquire("Global\\PBITelemetryConfigMutex", 10000))
					{
						FileInfo fileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, "conf_uid"));
						if (fileInfo.Exists && !FileUtils.ReadFileContent(out text, fileInfo.FullName))
						{
							text = "Unknown";
						}
						if (string.IsNullOrEmpty(text))
						{
							text = Guid.NewGuid().ToString();
							if (!FileUtils.WriteToFile(text, fileInfo.FullName, true))
							{
								text = "Unknown";
							}
							else
							{
								createdNew = true;
							}
						}
					}
				}
				catch (TimeoutException)
				{
					text = "Unknown";
				}
				catch (Exception ex)
				{
					if (!(ex is ArgumentNullException) && !(ex is SecurityException) && !(ex is ArgumentException) && !(ex is UnauthorizedAccessException) && !(ex is PathTooLongException) && !(ex is NotSupportedException) && !(ex is GlobalMutexException))
					{
						throw;
					}
					text = "Unknown";
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Unknown";
			}
			return text;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000032FC File Offset: 0x000014FC
		protected virtual string GetDeviceId()
		{
			string text = null;
			RegistryView registryView = RegistryView.Default;
			if (FileUtils.IsWow64Process())
			{
				registryView = RegistryView.Registry64;
			}
			if (!TelemetryConfiguration.TryGetMachineGuid(registryView, out text))
			{
				text = null;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Unknown";
			}
			else if (this.useStringHashCodeForDeviceId)
			{
				text = text.GetHashCode().ToString("x", CultureInfo.InvariantCulture);
			}
			else
			{
				text = TelemetryConfiguration.GetHash(text);
				int num = 8;
				if (text != null && text.Length > num)
				{
					text = text.Substring(0, num);
				}
			}
			return text;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003378 File Offset: 0x00001578
		protected virtual bool GetIsInternalUser()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\SQMClient"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("MSFTInternal");
						return value is int && (int)value == 1;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ArgumentNullException || ex is ObjectDisposedException || ex is SecurityException || ex is IOException || ex is UnauthorizedAccessException)
				{
					return false;
				}
				throw;
			}
			return false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003418 File Offset: 0x00001618
		protected virtual string GetSessionId()
		{
			if (string.IsNullOrEmpty(this.sessionId))
			{
				this.sessionId = Guid.NewGuid().ToString();
			}
			return this.sessionId;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003451 File Offset: 0x00001651
		protected virtual string GetAuthenticatedUserId()
		{
			return "Unknown";
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003458 File Offset: 0x00001658
		protected virtual AppType GetApplicationType()
		{
			return AppType.Unknown;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000345B File Offset: 0x0000165B
		protected virtual string GetTenantId()
		{
			return "Unknown";
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003464 File Offset: 0x00001664
		private static bool TryGetMachineGuid(RegistryView registryView, out string registryKeyValue)
		{
			registryKeyValue = null;
			object obj;
			if (FileUtils.TryGetRegistryKeyValue(RegistryHive.LocalMachine, registryView, "SOFTWARE\\Microsoft\\Cryptography", "MachineGuid", out obj) && obj != null)
			{
				registryKeyValue = (string)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000349C File Offset: 0x0000169C
		private static string GetHash(string value)
		{
			string text;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] bytes = Encoding.ASCII.GetBytes(value);
				text = Convert.ToBase64String(sha256Cng.ComputeHash(bytes));
			}
			return text;
		}

		// Token: 0x0400005D RID: 93
		private readonly bool useStringHashCodeForDeviceId;

		// Token: 0x0400005E RID: 94
		private string productVersion;

		// Token: 0x0400005F RID: 95
		protected string appDirPath;

		// Token: 0x04000060 RID: 96
		private string application;

		// Token: 0x04000061 RID: 97
		private string sessionId;

		// Token: 0x04000062 RID: 98
		private bool newUser;

		// Token: 0x04000063 RID: 99
		private string userId;
	}
}
