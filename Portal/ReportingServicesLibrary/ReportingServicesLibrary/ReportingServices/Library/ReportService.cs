using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.HostingInterfaces;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022A RID: 554
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	internal sealed class ReportService : ServiceBase, IRsService
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0004B35C File Offset: 0x0004955C
		public string ServiceDisplayName
		{
			get
			{
				if (this.m_serviceDisplayName == null)
				{
					this.m_serviceDisplayName = this.GetServiceDisplayName();
				}
				return this.m_serviceDisplayName;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004B378 File Offset: 0x00049578
		public ReportService()
		{
			if (ReportService.m_reportService != null)
			{
				throw new InvalidOperationException();
			}
			ReportService.m_reportService = this;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004B3A4 File Offset: 0x000495A4
		private void SleepForAttach(string[] args)
		{
			foreach (string text in args)
			{
				if (text.StartsWith("/Sleep:", StringComparison.OrdinalIgnoreCase) || text.StartsWith("-Sleep:", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						int num = 0;
						if (!int.TryParse(text.Substring(7).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
						{
							num = 60000;
							Console.WriteLine("Invalid Sleep argument; please provide a positive integer number between 1000 and {0}", 60000);
						}
						else if (num > 60000)
						{
							num = 60000;
						}
						Console.WriteLine("Sleeping for {0} ms", num);
						Thread.Sleep(num);
						break;
					}
					catch
					{
						break;
					}
				}
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004B45C File Offset: 0x0004965C
		public void StartService(string[] args)
		{
			if (Global.ApplicationMode)
			{
				this.OnStart(Environment.GetCommandLineArgs());
				return;
			}
			this.StartWebAppChildProcess();
			if (Global.Hosted)
			{
				base.ServiceName = this.ServiceDisplayName;
				base.ServiceMainCallback(0, IntPtr.Zero);
				base.Dispose();
				return;
			}
			ServiceBase.Run(new ServiceBase[] { this });
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004B4B8 File Offset: 0x000496B8
		private void StartWebAppChildProcess()
		{
			this._webAppStartupTracker.Wait();
			try
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Attempting to start WebApp service: {0}", new object[] { ReportService.WebAppFileName });
				}
				Process process = new Process();
				process.EnableRaisingEvents = true;
				process.StartInfo.FileName = ReportService.WebAppFileName;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.WorkingDirectory = ReportService.WebAppPath;
				process.StartInfo.Arguments = string.Empty;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.RedirectStandardInput = true;
				process.Exited += delegate(object o, EventArgs eventArgs)
				{
					if (Global.ApplicationMode)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Child Process Died. Check the log for further information: {0}", new object[] { ReportService.WebAppFileName });
						base.Stop();
						return;
					}
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Child Process Died. Restarting it. Check the log for further information: {0}", new object[] { ReportService.WebAppFileName });
					this.StartWebAppChildProcess();
				};
				if (!process.Start() && RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "WebApp Process failed to start.");
				}
				ProcessBinder.Bind(process.Id);
			}
			catch (Exception ex)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "WebApp Process failed to start. Exception: " + ex);
				}
				Process.GetCurrentProcess().Kill();
				throw ex;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004B5DC File Offset: 0x000497DC
		public void StopService()
		{
			this.OnStop();
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004B5E4 File Offset: 0x000497E4
		public void MarkProcessAsActive()
		{
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "ReportService::MarkProcessAsActive - Mark the WindowsService (worker) AppDomain as active.");
			}
			if (this.m_serviceController != null)
			{
				this.m_serviceController.MarkProcessAsActive();
				return;
			}
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "ReportService::MarkProcessAsActive - m_serviceController is null; Cannot mark the WindowsService (worker) AppDomain as active.");
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004B640 File Offset: 0x00049840
		protected override void OnStart(string[] args)
		{
			if (!Global.Hosted)
			{
				this.SleepForAttach(args);
			}
			try
			{
				this.m_serviceController = new ServiceAppDomainController();
				this.m_serviceController.Start();
			}
			catch (Exception ex)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Windows service failed to start. Exception: " + ex.ToString());
				}
				if (Global.ApplicationMode)
				{
					Process.GetCurrentProcess().Kill();
				}
				throw;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004B6C0 File Offset: 0x000498C0
		protected override void OnStop()
		{
			if (this.m_serviceController != null)
			{
				this.m_serviceController.Stop();
			}
			this.m_serviceController = null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004B6DC File Offset: 0x000498DC
		private string GetServiceDisplayName()
		{
			try
			{
				string fileName = Process.GetCurrentProcess().MainModule.FileName;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services"))
				{
					foreach (string text in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
						{
							object value = registryKey2.GetValue("ImagePath");
							if (value != null)
							{
								string text2 = value as string;
								if (text2 != null && text2.Contains(fileName))
								{
									string text3 = (string)registryKey2.GetValue("DisplayName");
									if (text3 == null)
									{
										text3 = text;
									}
									return text3;
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			return "SQL Server Reporting Services";
		}

		// Token: 0x04000714 RID: 1812
		private static ReportService m_reportService = null;

		// Token: 0x04000715 RID: 1813
		private ServiceAppDomainController m_serviceController;

		// Token: 0x04000716 RID: 1814
		private string m_serviceDisplayName;

		// Token: 0x04000717 RID: 1815
		private readonly OncePerPeriodHelper _webAppStartupTracker = new OncePerPeriodHelper(OncePerPeriodHelper.DefaultPeriod);

		// Token: 0x04000718 RID: 1816
		private static readonly string WebAppPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Portal";

		// Token: 0x04000719 RID: 1817
		private static readonly string WebAppFileName = ReportService.WebAppPath + "\\RSPortal.exe";
	}
}
