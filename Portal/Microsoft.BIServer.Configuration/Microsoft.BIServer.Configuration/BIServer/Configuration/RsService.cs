using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000021 RID: 33
	public class RsService
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004E71 File Offset: 0x00003071
		public RsService(string serviceName)
		{
			this._serviceName = serviceName;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004E80 File Offset: 0x00003080
		public void SetServiceAccountPassword(AccountCredentials accountCredentials)
		{
			string text = accountCredentials.DomainUser;
			string text2 = accountCredentials.Password;
			if (string.IsNullOrEmpty(text))
			{
				text = RsService.NtServiceName + "\\" + this._serviceName;
				text2 = string.Empty;
			}
			using (ManagementObject managementObject = new ManagementObject(new ManagementPath(string.Format("Win32_Service.Name='{0}'", this._serviceName))))
			{
				object[] array = new object[11];
				array[6] = text;
				if (!string.IsNullOrEmpty(text2))
				{
					array[7] = text2;
				}
				managementObject.InvokeMethod("Change", array);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004F1C File Offset: 0x0000311C
		public void Start()
		{
			using (ServiceController serviceController = new ServiceController(this._serviceName))
			{
				serviceController.Start();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004F58 File Offset: 0x00003158
		public void Stop()
		{
			if (!this.AskReportServerToShutDown(TimeSpan.FromSeconds(180.0)))
			{
				Logger.Info("Report server not responding to shutdown commands. Forcing an end to the report server service.", Array.Empty<object>());
				this.ForcibleShutdownReportServer();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F88 File Offset: 0x00003188
		private bool AskReportServerToShutDown(TimeSpan timeout)
		{
			try
			{
				using (ServiceController serviceController = new ServiceController(this._serviceName))
				{
					if (serviceController.Status == ServiceControllerStatus.StartPending)
					{
						serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
					}
					Process[] array = this.GetProcesses().ToArray<Process>();
					if (serviceController.Status == ServiceControllerStatus.Running)
					{
						serviceController.Stop();
					}
					if (serviceController.Status != ServiceControllerStatus.Stopped)
					{
						serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					}
					foreach (Process process in array)
					{
						Logger.Debug("Process '{0}' still running after service stopped. Waiting...", new object[] { process.Id });
						process.WaitForExit((int)timeout.TotalMilliseconds);
						if (!process.HasExited)
						{
							Logger.Debug("Process '{0}' still running after wait. Force quitting process.", new object[] { process.Id });
							try
							{
								process.Kill();
							}
							catch (Exception ex)
							{
								Logger.Debug(ex, "Failed to kill process '{0}'.", new object[] { process.Id });
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.Fatal(ex2, "Exception encountered shutting down report server.", Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000050C0 File Offset: 0x000032C0
		private IEnumerable<Process> GetProcesses()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT ProcessId FROM Win32_Service WHERE Name='{0}'", this._serviceName));
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				uint num = (uint)((ManagementObject)managementBaseObject)["ProcessId"];
				Process process = null;
				try
				{
					process = Process.GetProcessById((int)num);
				}
				catch (Exception ex)
				{
					Logger.Warning(ex, string.Format("Service process '{0}' not found", num), Array.Empty<object>());
				}
				if (process != null)
				{
					yield return process;
				}
			}
			ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000050D0 File Offset: 0x000032D0
		private void ForcibleShutdownReportServer()
		{
			foreach (Process process in this.GetProcesses())
			{
				try
				{
					process.Kill();
				}
				catch (Exception ex)
				{
					Logger.Fatal(ex, "Exception encountered killing report server process.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000513C File Offset: 0x0000333C
		public static string NtServiceName
		{
			get
			{
				string text;
				if ((text = RsService._ntServiceName) == null)
				{
					text = (RsService._ntServiceName = RsService.GetLocalizedNtService());
				}
				return text;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005154 File Offset: 0x00003354
		private static string GetLocalizedNtService()
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier("S-1-5-80");
			byte[] array = new byte[securityIdentifier.BinaryLength];
			securityIdentifier.GetBinaryForm(array, 0);
			StringBuilder stringBuilder = new StringBuilder(128);
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder(128);
			uint capacity2 = (uint)stringBuilder2.Capacity;
			RsService.sidNameUsage sidNameUsage;
			RsService.LookupAccountSid(null, array, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUsage);
			return stringBuilder.ToString();
		}

		// Token: 0x0600011D RID: 285
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out RsService.sidNameUsage peUsage);

		// Token: 0x040000E5 RID: 229
		private static string _ntServiceName;

		// Token: 0x040000E6 RID: 230
		private readonly string _serviceName;

		// Token: 0x040000E7 RID: 231
		private const string NtServiceSid = "S-1-5-80";

		// Token: 0x040000E8 RID: 232
		private const int DefaultTimeoutInSeconds = 180;

		// Token: 0x0200004F RID: 79
		private enum sidNameUsage
		{
			// Token: 0x040001D9 RID: 473
			SidTypeUser = 1,
			// Token: 0x040001DA RID: 474
			SidTypeGroup,
			// Token: 0x040001DB RID: 475
			SidTypeDomain,
			// Token: 0x040001DC RID: 476
			SidTypeAlias,
			// Token: 0x040001DD RID: 477
			SidTypeWellKnownGroup,
			// Token: 0x040001DE RID: 478
			SidTypeDeletedAccount,
			// Token: 0x040001DF RID: 479
			SidTypeInvalid,
			// Token: 0x040001E0 RID: 480
			SidTypeUnknown,
			// Token: 0x040001E1 RID: 481
			SidTypeComputer
		}
	}
}
