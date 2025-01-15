using System;
using System.ServiceProcess;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000DD RID: 221
	public static class WinStarter
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00015D08 File Offset: 0x00013F08
		public static int Run<T>(string[] args) where T : ApplicationRoot, new()
		{
			Library.Initialize();
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Run() invoked. args received are {0}", new object[] { string.Join(",", args) });
			int num = 0;
			ApplicationHostType applicationHostType = WinStarter.DetermineApplicationType(args);
			T t = new T();
			Debugging.AddApplicationRoot(t);
			if (applicationHostType == ApplicationHostType.WinService)
			{
				ServiceBase.Run(new WinService<T>(t, args));
			}
			else
			{
				if (applicationHostType == ApplicationHostType.WinAspNet)
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Web applications cannot be started using WinStarter.Run. Use WinStarter.RunAsync");
					throw new InvalidOperationException("Web applications cannot be started using WinStarter.Run. Use WinStarter.RunAsync");
				}
				num = new WinApplication<T>(t).Run(args);
			}
			return num;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00015D98 File Offset: 0x00013F98
		public static IWinStarterRunAsyncContext RunAsync<T>(string[] args) where T : ApplicationRoot, new()
		{
			Library.Initialize();
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RunAsync() invoked. args received are {0}", new object[] { string.Join(",", args) });
			ApplicationHostType applicationHostType = WinStarter.DetermineApplicationType(args);
			T t = new T();
			Debugging.AddApplicationRoot(t);
			IApplicationRootHost applicationRootHost;
			if (applicationHostType == ApplicationHostType.WinService)
			{
				WinService<T> service = new WinService<T>(t, args);
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					ServiceBase.Run(service);
				}, WaitOrNot.DontWait, "ServiceBase.Run");
				applicationRootHost = service;
			}
			else if (applicationHostType == ApplicationHostType.WinAspNet)
			{
				WinAspNet<T> winAspNet = new WinAspNet<T>(t);
				winAspNet.RunAsync();
				applicationRootHost = winAspNet;
			}
			else
			{
				WinApplication<T> application = new WinApplication<T>(t);
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					application.Run(args);
				}, WaitOrNot.DontWait, "WinApplication.Run");
				applicationRootHost = application;
			}
			return new WinStarter.WinStarterRunAsyncContext(applicationRootHost);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00015E87 File Offset: 0x00014087
		public static void StopRunAsync([NotNull] IWinStarterRunAsyncContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IWinStarterRunAsyncContext>(context, "context");
			context.StopAndWaitForShutdownToComplete();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00015E9A File Offset: 0x0001409A
		private static ApplicationHostType DetermineApplicationType(string[] args)
		{
			if (WinStarter.IsRunningAsService(new ApplicationSwitches(args, ApplicationSwitchesTypes.CommandLine | ApplicationSwitchesTypes.AppConfig)))
			{
				return ApplicationHostType.WinService;
			}
			if (WinStarter.IsRunningAsWebApp(new ApplicationSwitches(ApplicationSwitchesTypes.WebConfig)))
			{
				return ApplicationHostType.WinAspNet;
			}
			return ApplicationHostType.WinApplication;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00015EBC File Offset: 0x000140BC
		private static bool IsRunningAsService(IApplicationSwitches appSwitches)
		{
			appSwitches.RegisterSwitch("RunAsService", "srv", "Specifies if the Application should run as a Windows service", ParameterType.Boolean, false, "false");
			return appSwitches.GetBoolSwitch("srv");
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00015EE5 File Offset: 0x000140E5
		private static bool IsRunningAsWebApp(IApplicationSwitches appSwitches)
		{
			appSwitches.RegisterSwitch("RunAsWebApp", "web", "Specifies if the Application should run as a web application", ParameterType.Boolean, false, "false");
			return appSwitches.GetBoolSwitch("web");
		}

		// Token: 0x020005D8 RID: 1496
		internal class WinStarterRunAsyncContext : IWinStarterRunAsyncContext
		{
			// Token: 0x17000702 RID: 1794
			// (get) Token: 0x06002BBD RID: 11197 RVA: 0x0009B104 File Offset: 0x00099304
			public ApplicationRoot ApplicationRoot
			{
				get
				{
					return this.m_applicationRootHost.ApplicationRoot;
				}
			}

			// Token: 0x06002BBE RID: 11198 RVA: 0x0009B111 File Offset: 0x00099311
			public WinStarterRunAsyncContext(IApplicationRootHost host)
			{
				this.m_applicationRootHost = host;
			}

			// Token: 0x06002BBF RID: 11199 RVA: 0x0009B12C File Offset: 0x0009932C
			public void StopAndWaitForShutdownToComplete()
			{
				string text = "StopAndWaitForShutdownToComplete cannot be called more than once";
				if (this.m_applicationRootHost == null)
				{
					throw new InvalidOperationException(text);
				}
				this.m_applicationRootHost.RequestShutdown(null);
				this.m_applicationRootHost.WaitForStateToComplete(BlockState.Uninitialized);
				this.m_applicationRootHost = null;
			}

			// Token: 0x06002BC0 RID: 11200 RVA: 0x0009B16D File Offset: 0x0009936D
			public void WaitForStartToComplete()
			{
				this.m_applicationRootHost.WaitForStateToComplete(BlockState.Started);
			}

			// Token: 0x04000FEB RID: 4075
			private static readonly TimeSpan s_pollSleepInterval = TimeSpan.FromSeconds(1.0);

			// Token: 0x04000FEC RID: 4076
			private static readonly TimeSpan s_pollMaxWaitInterval = TimeSpan.FromSeconds(120.0);

			// Token: 0x04000FED RID: 4077
			private InterlockedBool m_applicationRootInitialized = new InterlockedBool(false);

			// Token: 0x04000FEE RID: 4078
			private IApplicationRootHost m_applicationRootHost;
		}
	}
}
