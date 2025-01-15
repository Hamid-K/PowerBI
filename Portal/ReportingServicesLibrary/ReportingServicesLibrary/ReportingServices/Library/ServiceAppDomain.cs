using System;
using System.Diagnostics;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022E RID: 558
	internal class ServiceAppDomain : MarshalByRefObject, IServiceAppDomain
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x00005C88 File Offset: 0x00003E88
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004B8F4 File Offset: 0x00049AF4
		public void StartService(IServiceAppDomainController appDomainController, bool firstStart)
		{
			Globals.InitConfiguration(new RSConfigurationFileManager(StaticConfig.Current.GetOrDefault("rsConfigFilePath", string.Empty)), RunningApplication.WindowsService);
			if (Globals.Configuration.ConnectionStringSet)
			{
				try
				{
					ServiceController.EnsureConfigurationFromDB();
				}
				catch (Exception ex)
				{
					if (this.m_tracer.TraceError)
					{
						this.m_tracer.Trace(TraceLevel.Error, "Error initializing configuration from the database: " + ex.ToString());
					}
				}
			}
			Globals.InitServer(firstStart);
			if (ServiceController.Current == null)
			{
				ServiceController.Current = new ServiceController(appDomainController);
			}
			ExecutionLog.Init();
			ServiceController.Current.StartService(firstStart);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004B998 File Offset: 0x00049B98
		public void EndService(Globals.ServiceStopMode stopMode)
		{
			if (ServiceController.Current != null)
			{
				ServiceController.Current.EndService(stopMode);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0004B9AC File Offset: 0x00049BAC
		public bool StillProcessing
		{
			get
			{
				bool flag = false;
				if (ServiceController.Current != null)
				{
					flag = ServiceController.Current.StillProcessing;
				}
				return flag;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004B9CE File Offset: 0x00049BCE
		public bool IsServiceStarted
		{
			get
			{
				return ServiceController.Current != null && ServiceController.Current.ServiceStarted;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0004B9E3 File Offset: 0x00049BE3
		public bool IsServiceWorking
		{
			get
			{
				return ServiceController.Current != null && ServiceController.Current.IsServiceWorking;
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004B9F8 File Offset: 0x00049BF8
		public void Trace(TraceLevel level, string message)
		{
			switch (level)
			{
			case TraceLevel.Error:
				if (!this.m_tracer.TraceError)
				{
					return;
				}
				break;
			case TraceLevel.Warning:
				if (!this.m_tracer.TraceWarning)
				{
					return;
				}
				break;
			case TraceLevel.Info:
				if (!this.m_tracer.TraceInfo)
				{
					return;
				}
				break;
			case TraceLevel.Verbose:
				if (!this.m_tracer.TraceVerbose)
				{
					return;
				}
				break;
			default:
				return;
			}
			this.m_tracer.Trace(level, message);
		}

		// Token: 0x0400071D RID: 1821
		private RSTrace m_tracer = RSTrace.ServiceControllerTracer;
	}
}
