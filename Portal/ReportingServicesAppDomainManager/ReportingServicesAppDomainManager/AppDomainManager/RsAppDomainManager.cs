using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.HostingInterfaces;

namespace Microsoft.ReportingServices.AppDomainManager
{
	// Token: 0x0200000C RID: 12
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class RsAppDomainManager : AppDomainManager, IRsManagedCallback, IServiceProvider
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000254B File Offset: 0x0000074B
		public RsAppDomainManager()
		{
			this.m_timer.Start();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002586 File Offset: 0x00000786
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000258C File Offset: 0x0000078C
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public static void TraceShutdownMessage(AppDomain appDomain)
		{
			if (!RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				return;
			}
			try
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} is about to be unloaded.", appDomain.Id, appDomain.FriendlyName);
				HttpRuntime httpRuntime = null;
				string text2 = null;
				try
				{
					httpRuntime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
				}
				catch
				{
				}
				if (httpRuntime != null)
				{
					text2 = (string)httpRuntime.GetType().InvokeMember("_shutDownMessage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, httpRuntime, null, CultureInfo.InvariantCulture);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text = text + " ShutDownMessage: " + text2;
				}
				Console.WriteLine(text);
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, text);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002668 File Offset: 0x00000868
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public static AppDomain GetDefaultAppDomain()
		{
			return Globals.GetDefaultDomain();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002670 File Offset: 0x00000870
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override AppDomain CreateDomain(string appDomainName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			if (this.m_rsAppDomainType != RsAppDomainType.Default && RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Creating appdomain '{0}' from within a non-default AppDomain.", new object[] { appDomainName });
			}
			if (!RsAppDomainManager.IsRsAppDomainType(appDomainName, appDomainInfo))
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain creation is not supported in this version of Reporting Services. (AppDomain Name={0})", new object[] { appDomainName });
				}
				throw new NotYetSupportedException();
			}
			RSTrace.AppDomainManagerTracer.Assert(appDomainInfo != null, "appDomainInfo != null");
			appDomainInfo.PrivateBinPath = Path.Combine(appDomainInfo.ApplicationBase, "bin");
			appDomainInfo.AppDomainInitializerArguments = new string[] { appDomainName };
			AppDomain appDomain = AppDomainManager.CreateDomainHelper(appDomainName, securityInfo, appDomainInfo);
			RsAppDomainType appDomainType = RsAppDomainManager.GetAppDomainType(appDomainName);
			this.RegisterAppDomain(appDomainType, appDomain);
			((RsAppDomainManager)appDomain.DomainManager).SetDefaultAppDomainManager(this);
			string text = string.Format(CultureInfo.InvariantCulture, "Appdomain STARTED: id='{0}'; name='{1}'", appDomain.Id, appDomainName);
			Console.WriteLine(text);
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
			}
			return appDomain;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002780 File Offset: 0x00000980
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override void InitializeNewDomain(AppDomainSetup appDomainInfo)
		{
			RSTraceInternal.SetAsTrace();
			base.InitializationFlags = AppDomainManagerInitializationOptions.RegisterWithHost;
			this.m_rsAppDomainType = RsAppDomainType.Unknown;
			if (appDomainInfo != null)
			{
				if (appDomainInfo.AppDomainInitializerArguments != null)
				{
					this.m_rsAppDomainType = RsAppDomainManager.GetAppDomainType(appDomainInfo.AppDomainInitializerArguments[0]);
				}
				else
				{
					this.m_rsAppDomainType = RsAppDomainType.Default;
				}
			}
			if (this.m_rsAppDomainType != RsAppDomainType.ReportServer)
			{
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RsAppDomainManager.RemoteCertificateValidationCallback);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027E4 File Offset: 0x000009E4
		private static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			HttpWebRequest httpWebRequest = sender as HttpWebRequest;
			if (httpWebRequest != null && null != httpWebRequest.RequestUri && RSTrace.AppDomainManagerTracer.TraceError)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Remote certificate error {0} encountered for url {1}.", new object[]
				{
					sslPolicyErrors,
					httpWebRequest.RequestUri.ToString()
				});
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002848 File Offset: 0x00000A48
		private static bool IsRsAppDomainType(string appDomainName, AppDomainSetup appDomainInfo)
		{
			RsAppDomainType appDomainType = RsAppDomainManager.GetAppDomainType(appDomainName);
			return appDomainType - RsAppDomainType.WindowsService <= 1 && appDomainInfo != null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002868 File Offset: 0x00000A68
		private static RsAppDomainType GetAppDomainType(string appDomainName)
		{
			if (appDomainName == null)
			{
				return RsAppDomainType.Default;
			}
			if (appDomainName.StartsWith(RsAppDomainType.WindowsService.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return RsAppDomainType.WindowsService;
			}
			if (appDomainName.StartsWith(RsAppDomainType.ReportServer.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return RsAppDomainType.ReportServer;
			}
			return RsAppDomainType.Unknown;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028AF File Offset: 0x00000AAF
		private void SetDefaultAppDomainManager(IServiceProvider serviceProvider)
		{
			this.m_serviceContainer = new ServiceContainer(serviceProvider);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028C0 File Offset: 0x00000AC0
		public static bool ArgsContainSwitch(string[] args, string checkSwitch)
		{
			foreach (string text in args)
			{
				if (string.Compare(text, "/" + checkSwitch, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "-" + checkSwitch, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000290C File Offset: 0x00000B0C
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void ServiceMain(int argCount, string[] args, IRsUnmanagedCallback rsUnmanagedCallback)
		{
			if (RsAppDomainManager.ArgsContainSwitch(args, "app") && RsAppDomainManager.ArgsContainSwitch(args, "attach"))
			{
				Console.WriteLine("Hit any key to start...");
				Console.ReadKey();
				Console.WriteLine("Starting...");
			}
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "Wrong domain appDomainType {0}", new object[] { this.m_rsAppDomainType });
			RSTrace.AppDomainManagerTracer.Assert(rsUnmanagedCallback != null, "No unmanaged callback");
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsService == null, "Service already started");
			try
			{
				this.m_serviceContainer = new ServiceContainer();
				this.m_serviceContainer.AddService(typeof(IRsManagedCallback), this);
				this.m_serviceContainer.AddService(typeof(IRsUnmanagedCallback), rsUnmanagedCallback);
				Console.WriteLine("Entered managed ServiceMain in {0}.", AppDomain.CurrentDomain.FriendlyName);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Entered managed ServiceMain in {0}.", new object[] { AppDomain.CurrentDomain.FriendlyName });
				}
				AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
				assemblyName.Name = "ReportingServicesLibrary";
				this.m_rsService = (IRsService)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assemblyName.FullName, "Microsoft.ReportingServices.Library.ReportService");
				this.m_rsService.StartService(args);
			}
			catch (Exception ex)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Failed to start Report Server Windows Service: {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AA8 File Offset: 0x00000CA8
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void UnloadAllAppDomains()
		{
			RSTrace.AppDomainManagerTracer.Assert(RsAppDomainType.Unknown != this.m_rsAppDomainType, "RsAppDomainType.Unknown == m_rsAppDomainType");
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			if (this.m_rsService != null)
			{
				this.m_rsService.StopService();
			}
			this.UnloadAppDomain(RsAppDomainType.ReportServer, false);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B04 File Offset: 0x00000D04
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void UnloadAppDomain(RsAppDomainType appDomainType, bool memoryRecycle)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			AppDomain appDomain = this.GetAppDomain(appDomainType);
			if (appDomain == null)
			{
				return;
			}
			this.UnloadAppDomain(appDomain, memoryRecycle);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B40 File Offset: 0x00000D40
		private void RegisterAppDomain(RsAppDomainType appDomainType, AppDomain appDomain)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Registering AppDomain: type='{0}'; id='{1}'; Name='{2}'", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
			}
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_appDomains == null)
				{
					this.m_appDomains = new RsAppDomainManager.AppDomainEntry[Enum.GetValues(typeof(RsAppDomainType)).Length - 2];
				}
				if (this.m_appDomains[(int)appDomainType].appDomain != null && RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					string text = this.m_appDomains[(int)appDomainType].appDomainName ?? string.Empty;
					int id = this.m_appDomains[(int)appDomainType].Id;
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Registering AppDomain: overriding the registration of the following AppDomain - type='{0}'; id='{1}'; Name='{2}'", new object[] { appDomainType, id, text });
				}
				this.m_appDomains[(int)appDomainType].appDomain = appDomain;
				this.m_appDomains[(int)appDomainType].appDomainName = appDomain.FriendlyName;
				this.m_appDomains[(int)appDomainType].Id = appDomain.Id;
				this.EnsureAppDomainLifeCycleManagementEntry(appDomain.Id);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CC4 File Offset: 0x00000EC4
		private void UnregisterAppDomain(int id)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Unregistering AppDomain id='{0}'", id);
			Console.WriteLine(text);
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
			}
			if (this.m_appDomains != null)
			{
				object sync = this.m_sync;
				lock (sync)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Unregistering AppDomain: searching for AppDomain id '{0}'", id);
					Console.WriteLine(text);
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
					}
					for (int i = 0; i < this.m_appDomains.Length; i++)
					{
						if (id == this.m_appDomains[i].Id)
						{
							text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} unregistered.", this.m_appDomains[i].Id, this.m_appDomains[i].appDomainName);
							Console.WriteLine(text);
							if (RSTrace.AppDomainManagerTracer.TraceInfo)
							{
								RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
							}
							this.m_appDomains[i].appDomain = null;
							this.m_appDomains[i].appDomainName = null;
							this.m_appDomains[i].Id = -1;
							this.EnsureAppDomainLifeCycleManagementEntry(id);
							this.AppDomainsLifeCycleManagement[id].unregistered++;
							return;
						}
					}
				}
			}
			text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} was not registered. Its registration was probably already replaced with a new AppDomain (since this one is getting recycled).", id);
			Console.WriteLine(text);
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E90 File Offset: 0x00001090
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void UnloadAppDomain(AppDomain appDomain, bool memoryRecycle)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			if (appDomain == null)
			{
				return;
			}
			try
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAppDomain: {0}, '{1}'", new object[] { appDomain.Id, appDomain.FriendlyName });
				}
				if (this.m_appDomains != null)
				{
					this.UnregisterAppDomain(appDomain.Id);
				}
				RsAppDomainType appDomainType = RsAppDomainManager.GetAppDomainType(appDomain.FriendlyName);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAppDomain {0}: {1}, '{2}'", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
				}
				if (appDomain.IsFinalizingForUnload())
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAppDomain {0}: {1}, '{2}' - IsFinalizingForUnload is 'true'; bailing out", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
					}
				}
				else if (this.UnloadStarted(appDomain.Id))
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAppDomain {0}: {1}, '{2}' - UnloadStarted is 'true'; bailing out", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
					}
				}
				else if (RsAppDomainType.ReportServer == appDomainType)
				{
					if (!this.IsInitialized(appDomain.Id))
					{
						this.SetToBeUnloaded(appDomain.Id);
					}
					else
					{
						this.UnloadAspDomain(appDomain, appDomainType, memoryRecycle);
						if (memoryRecycle)
						{
							int id = appDomain.Id;
							int num = 1000 * Globals.Configuration.DBQueryTimeout;
							while (!this.IsUnloaded(id) && num > 0)
							{
								if (RSTrace.AppDomainManagerTracer.TraceInfo)
								{
									RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "RsAppDomainManager::UnloadAppDomain '{0}' - Memory pressure detected; Waiting for the AppDomain to really unload; Mark the WindowsService (worker) AppDomain as active.", new object[] { id });
								}
								this.m_rsService.MarkProcessAsActive();
								int num2 = Math.Min(num, 5000);
								num -= num2;
								RSTrace.AppDomainManagerTracer.Assert(num2 >= 0, "UnloadAspDomain: Negative wait time");
								Thread.Sleep(num2);
							}
							if (!this.IsUnloaded(id))
							{
								string text = string.Format(CultureInfo.InvariantCulture, "AppDomain:{0} unloading timed out.", id);
								Console.WriteLine(text);
								if (RSTrace.AppDomainManagerTracer.TraceWarning)
								{
									RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, text);
								}
							}
						}
					}
				}
				else
				{
					this.SetUnloadStarted(appDomain.Id);
					RsAppDomainManager.TraceShutdownMessage(appDomain);
					string text2 = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} unloading.", appDomain.Id, appDomain.FriendlyName);
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text2);
					Console.WriteLine(text2);
					if (memoryRecycle)
					{
						RSEventLog.Current.WriteInformation(Event.AppDomainMaxMemoryLimitReached, new object[] { appDomain.FriendlyName });
						AppDomain.Unload(appDomain);
					}
					else
					{
						new AsyncDomainUnloading(appDomain, this.m_rsService).Start();
					}
				}
			}
			catch (AppDomainUnloadedException)
			{
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031A8 File Offset: 0x000013A8
		private void UnloadAspDomain(AppDomain appDomain, RsAppDomainType appDomainType, bool memoryRecycle)
		{
			RSTrace.AppDomainManagerTracer.Assert(RsAppDomainType.ReportServer == appDomainType);
			try
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAspDomain {0}: {1}, '{2}'", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
				}
				if (appDomain.IsFinalizingForUnload())
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAspDomain {0}: {1}, '{2}' - IsFinalizingForUnload is 'true'; bailing out", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
					}
				}
				else if (this.UnloadStarted(appDomain.Id))
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "UnloadAspDomain {0}: {1}, '{2}' - UnloadStarted is 'true'; bailing out", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
					}
				}
				else
				{
					this.SetUnloadStarted(appDomain.Id);
					string text = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} unloading.", appDomain.Id, appDomain.FriendlyName);
					Console.WriteLine(text);
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
					}
					if (memoryRecycle)
					{
						this.SetMemoryRecycle(appDomain.Id);
						RSEventLog.Current.WriteInformation(Event.AppDomainMaxMemoryLimitReached, new object[] { appDomain.FriendlyName });
					}
					appDomain.DoCallBack(delegate
					{
						HttpRuntime.UnloadAppDomain();
					});
				}
			}
			catch (AppDomainUnloadedException)
			{
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003368 File Offset: 0x00001568
		public object GetService(Type serviceType)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_serviceContainer != null, "ServiceContainer not initialized");
			return this.m_serviceContainer.GetService(serviceType);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003390 File Offset: 0x00001590
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public object CreateHttpRuntime(RsAppDomainType appDomainType, string vdir, string pdir, ref int domainId)
		{
			AppDomain appDomain = null;
			AppDomain appDomain2 = null;
			object obj;
			try
			{
				appDomain2 = this.GetAppDomain(appDomainType);
				if (this.m_rsHttpRuntime == null)
				{
					AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
					assemblyName.Name = "RsHttpRuntime";
					this.m_rsHttpRuntime = (IRsHttpRuntime)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assemblyName.FullName, "ReportingServicesHttpRuntime.RsHttpRuntime");
				}
				IRsHttpRuntime rsHttpRuntime = (IRsHttpRuntime)this.m_rsHttpRuntime.Create(appDomainType, vdir, pdir, ref domainId);
				try
				{
					appDomain = (AppDomain)rsHttpRuntime.GetAppDomain();
					if (appDomain.IsFinalizingForUnload() || this.UnloadStarted(appDomain.Id))
					{
						if (RSTrace.AppDomainManagerTracer.TraceInfo)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Create HttpRuntime: AppDomain {0} ('{1}') has been unloaded by ASP.NET.", new object[] { appDomain.Id, appDomain.FriendlyName });
						}
						this.UnregisterAppDomain(appDomain.Id);
						return null;
					}
					if (this.ToBeUnloaded(appDomain.Id))
					{
						if (RSTrace.AppDomainManagerTracer.TraceInfo)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Create HttpRuntime: AppDomain {0} ('{1}') is already unregistered since its ToBeUnloaded flag is set; unload it.", new object[] { appDomain.Id, appDomain.FriendlyName });
						}
						try
						{
							this.UnloadAspDomain(appDomain, appDomainType, false);
						}
						catch (Exception ex)
						{
							if (RSTrace.AppDomainManagerTracer.TraceError)
							{
								RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain:{0} failed to unload. Error: {1}.", new object[]
								{
									appDomain.FriendlyName,
									ex.ToString()
								});
							}
						}
						return null;
					}
				}
				catch (AppDomainUnloadedException)
				{
					return null;
				}
				appDomain.DoCallBack(delegate
				{
					ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RsAppDomainManager.RemoteCertificateValidationCallback);
				});
				this.SetInitialized(appDomain.Id);
				obj = rsHttpRuntime;
			}
			catch (Exception ex2)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain {0} error: {1}.", new object[]
					{
						appDomainType,
						ex2.ToString()
					});
				}
				try
				{
					appDomain = this.GetAppDomain(appDomainType);
					if (appDomain != appDomain2)
					{
						if (RSTrace.AppDomainManagerTracer.TraceError)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain {0} id {1} ('{2}') was created. Unloading it...", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
						}
						try
						{
							this.UnloadAspDomain(appDomain, appDomainType, false);
						}
						catch (Exception ex3)
						{
							if (RSTrace.AppDomainManagerTracer.TraceError)
							{
								RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain:{0} failed to unload. Error: {1}.", new object[]
								{
									appDomain.FriendlyName,
									ex3.ToString()
								});
							}
						}
					}
				}
				catch (Exception)
				{
				}
				throw new ReportServerAppDomainManagerException(ex2, appDomainType.ToString(), "Failed to create Report Server HTTP Runtime");
			}
			return obj;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036B8 File Offset: 0x000018B8
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void NotifyMemoryPressure(RsMemoryPressureLevel pressureLevel, long kBytesToFree)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Received NotifyMemoryPressure(pressureLevel={0}, kBytesToFree={1})", new object[] { pressureLevel, kBytesToFree });
			}
			if (this.m_appDomains == null)
			{
				return;
			}
			try
			{
				long num;
				if (this.m_memUtil.PerformGcIfTimePassed(true, this.m_timer, out num))
				{
					kBytesToFree -= num;
					if (kBytesToFree <= 0L && RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Induced Garbage Collection released {0}KB and satisfied memory shrink request.", new object[] { num });
					}
				}
				RsAppDomainType[] array = new RsAppDomainType[2];
				int num2 = 0;
				array[num2++] = RsAppDomainType.WindowsService;
				array[num2++] = RsAppDomainType.ReportServer;
				long num3 = 0L;
				int num4 = 0;
				while (num4 < num2 && kBytesToFree > 0L)
				{
					try
					{
						AppDomain appDomain = null;
						object sync = this.m_sync;
						lock (sync)
						{
							appDomain = this.m_appDomains[(int)array[num4]].appDomain;
						}
						if (this.CanSendMemoryNotificationToDomain(appDomain, array[num4]))
						{
							bool flag2 = pressureLevel == RsMemoryPressureLevel.HighPressure || pressureLevel == RsMemoryPressureLevel.ExceedingLimit;
							FreeMemoryRequestContext freeMemoryRequestContext = new FreeMemoryRequestContext(kBytesToFree, flag2);
							FreeMemoryRequest freeMemoryRequest = new FreeMemoryRequest(freeMemoryRequestContext);
							appDomain.DoCallBack(new CrossAppDomainDelegate(freeMemoryRequest.Initiate));
							if (freeMemoryRequestContext.WasShrinkAttempted && RSTrace.AppDomainManagerTracer.TraceVerbose)
							{
								RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Appdomain ({0}) attempted to free {1} KB.", new object[]
								{
									array[num4],
									freeMemoryRequestContext.EstimatedKBytesFreed
								});
							}
							if (freeMemoryRequestContext.WasShrinkAttempted)
							{
								kBytesToFree -= freeMemoryRequestContext.EstimatedKBytesFreed;
							}
							num3 += freeMemoryRequestContext.TotalAuditedKb;
							if (pressureLevel == RsMemoryPressureLevel.LowPressure && array[num4] == RsAppDomainType.WindowsService && freeMemoryRequestContext.WasShrinkAttempted)
							{
								break;
							}
						}
					}
					catch (AppDomainUnloadedException)
					{
						if (RSTrace.AppDomainManagerTracer.TraceVerbose)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Shrink notification failed due to unloaded appdomain: {0}", new object[] { array[num4].ToString() });
						}
					}
					catch (RemotingException)
					{
						if (RSTrace.AppDomainManagerTracer.TraceVerbose)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Shrink notification failed due to remoting disconnected for appdomain: {0}", new object[] { array[num4].ToString() });
						}
					}
					num4++;
				}
				long num5 = GC.GetTotalMemory(false) / 1024L;
				if (num4 == 2 && (long)((double)num5 * 0.6) > num3)
				{
					long num6;
					this.m_memUtil.PerformGcIfTimePassed(false, this.m_timer, out num6);
				}
			}
			catch (ThreadAbortException)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "Shrink notification failed due to Thread Abort.");
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Error while performing memory shrink: {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003A04 File Offset: 0x00001C04
		private bool CanSendMemoryNotificationToDomain(AppDomain targetDomain, RsAppDomainType domainType)
		{
			if (domainType != RsAppDomainType.ReportServer && domainType != RsAppDomainType.WindowsService)
			{
				return false;
			}
			if (targetDomain == null)
			{
				return false;
			}
			bool flag = !this.ToBeUnloaded(targetDomain.Id) && !this.UnloadStarted(targetDomain.Id) && !targetDomain.IsFinalizingForUnload();
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Shrink notification {0} be sent to appdomain '{1}' (id = '{2}') since it is being unloaded now or it is marked for an upcoming unload operation", new object[]
				{
					flag ? "can" : "cannot",
					targetDomain.FriendlyName,
					targetDomain.Id
				});
			}
			return flag;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003A98 File Offset: 0x00001C98
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public AppDomain GetAppDomain(RsAppDomainType appDomainType)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			RSTrace.AppDomainManagerTracer.Assert(appDomainType > RsAppDomainType.Default, "RsAppDomainType.Default == appDomainType");
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomain for AppDomain type '{0}'", new object[] { appDomainType });
			}
			if (this.m_appDomains == null)
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "GetAppDomain: m_appDomains is not initialized");
				}
				return null;
			}
			AppDomain appDomain = this.m_appDomains[(int)appDomainType].appDomain;
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				if (appDomain == null)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomain for AppDomain type '{0}' did not find an AppDomain", new object[] { appDomainType });
				}
				else
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomain for AppDomain type '{0}' found AppDomain {1} ('{2}')", new object[] { appDomainType, appDomain.Id, appDomain.FriendlyName });
				}
			}
			return appDomain;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003B9C File Offset: 0x00001D9C
		private int GetAppDomainId(RsAppDomainType appDomainType)
		{
			RSTrace.AppDomainManagerTracer.Assert(this.m_rsAppDomainType == RsAppDomainType.Default, "RsAppDomainType.Default != m_rsAppDomainType");
			RSTrace.AppDomainManagerTracer.Assert(appDomainType > RsAppDomainType.Default, "RsAppDomainType.Default == appDomainType");
			bool traceVerbose = RSTrace.AppDomainManagerTracer.TraceVerbose;
			if (this.m_appDomains == null)
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "GetAppDomainId: m_appDomains is not initialized");
				}
				return -1;
			}
			int id = this.m_appDomains[(int)appDomainType].Id;
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				if (id == -1)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomainId for AppDomain type '{0}' did not find an AppDomain", new object[] { appDomainType });
				}
				else
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomainId for AppDomain type '{0}' found AppDomain {1}", new object[] { appDomainType, id });
				}
			}
			return id;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003C71 File Offset: 0x00001E71
		public RsAppDomainType GetAppDomainType()
		{
			return this.m_rsAppDomainType;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003C7C File Offset: 0x00001E7C
		public RsAppDomainType GetAppDomainTypeFromId(int appDomainId)
		{
			if (this.m_appDomains == null)
			{
				return RsAppDomainType.Unknown;
			}
			RsAppDomainType rsAppDomainType = RsAppDomainType.Unknown;
			string text = string.Format(CultureInfo.InvariantCulture, "Found no AppDomain for id {0}.", appDomainId);
			object sync = this.m_sync;
			lock (sync)
			{
				int i = 0;
				while (i < this.m_appDomains.Length)
				{
					if (appDomainId == this.m_appDomains[i].Id)
					{
						string appDomainName = this.m_appDomains[i].appDomainName;
						if (this.m_appDomains[i].appDomain != null && appDomainName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "Found AppDomain '{0}' for id {1}.", appDomainName, appDomainId);
							rsAppDomainType = (RsAppDomainType)i;
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			Console.WriteLine(text);
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
			}
			return rsAppDomainType;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D70 File Offset: 0x00001F70
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public int GetAppDomainUniqueId(RsAppDomainType appDomainType)
		{
			RSTrace.AppDomainManagerTracer.Assert(RsAppDomainType.ReportServer == appDomainType);
			if (this.m_appDomains == null)
			{
				return 0;
			}
			int num = 0;
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_appDomains[(int)appDomainType].appDomain != null)
				{
					int num2 = 0;
					string appDomainName = this.m_appDomains[(int)appDomainType].appDomainName;
					int num3 = appDomainName.LastIndexOf('_');
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "GetAppDomainUniqueId: AppDomain '{0}' type is '{1}' (last '_' occurrence is at {2})", new object[] { appDomainName, appDomainType, num3 });
					}
					if (num3 > 0 && num3 + 1 < appDomainName.Length && int.TryParse(appDomainName.Substring(num3 + 1, 1), out num2))
					{
						num = ((num2 != 0) ? 0 : 1);
					}
				}
			}
			return num;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E64 File Offset: 0x00002064
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void OnAppDomainUnload(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "OnAppDomainUnload for AppDomain id '{0}'", new object[] { appDomainId });
			}
			this.UnregisterAppDomain(appDomainId);
			object sync = this.m_sync;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				this.AppDomainsLifeCycleManagement[appDomainId].onAppDomainUnloadWasCalledAt = DateTime.Now;
				Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> unloadedAppDomains = this.GetUnloadedAppDomains();
				if (unloadedAppDomains.Count > 10)
				{
					foreach (KeyValuePair<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> keyValuePair in unloadedAppDomains)
					{
						DateTime onAppDomainUnloadWasCalledAt = keyValuePair.Value.onAppDomainUnloadWasCalledAt;
						if (2 * Globals.Configuration.DBQueryTimeout <= DateTime.Now.Subtract(onAppDomainUnloadWasCalledAt).Seconds)
						{
							this.AppDomainsLifeCycleManagement.Remove(keyValuePair.Key);
						}
					}
				}
				unloadedAppDomains.Clear();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003F84 File Offset: 0x00002184
		private void SetToBeUnloaded(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "SetToBeUnloaded for AppDomain id '{0}'", new object[] { appDomainId });
			}
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "SetToBeUnloaded aborted since supplied AppDomain id is -1");
				}
				return;
			}
			object sync = this.m_sync;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				Interlocked.Increment(ref this.AppDomainsLifeCycleManagement[appDomainId].toBeUnloaded);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SetToBeUnloaded AppDomain id '{0}': toBeUnloaded = {1}", new object[]
					{
						appDomainId,
						this.AppDomainsLifeCycleManagement[appDomainId].toBeUnloaded
					});
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000406C File Offset: 0x0000226C
		private bool ToBeUnloaded(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "ToBeUnloaded for AppDomain id '{0}'", new object[] { appDomainId });
			}
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "ToBeUnloaded aborted since supplied AppDomain id is -1");
				}
				return false;
			}
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				flag2 = 0 < this.AppDomainsLifeCycleManagement[appDomainId].toBeUnloaded;
			}
			return flag2;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004110 File Offset: 0x00002310
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void SetInitialized()
		{
			object sync = this.m_sync;
			lock (sync)
			{
				this.SetInitialized(this.GetAppDomainId(RsAppDomainType.WindowsService));
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004158 File Offset: 0x00002358
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public bool IsInitialized()
		{
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				flag2 = this.IsInitialized(this.GetAppDomainId(RsAppDomainType.WindowsService));
			}
			return flag2;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000041A4 File Offset: 0x000023A4
		private void SetInitialized(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "SetInitialized for AppDomain id '{0}'", new object[] { appDomainId });
			}
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "SetInitialized aborted since supplied AppDomain id is -1");
				}
				return;
			}
			object sync = this.m_sync;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				Interlocked.Increment(ref this.AppDomainsLifeCycleManagement[appDomainId].initialized);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SetInitialized AppDomain id '{0}': initialized = {1}", new object[]
					{
						appDomainId,
						this.AppDomainsLifeCycleManagement[appDomainId].initialized
					});
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000428C File Offset: 0x0000248C
		private bool IsInitialized(int appDomainId)
		{
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "IsInitialized aborted since supplied AppDomain id is -1");
				}
				return false;
			}
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				bool traceVerbose = RSTrace.AppDomainManagerTracer.TraceVerbose;
				flag2 = 0 < this.AppDomainsLifeCycleManagement[appDomainId].initialized;
			}
			return flag2;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004310 File Offset: 0x00002510
		private bool IsUnloaded(int appDomainId)
		{
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "IsUnloaded aborted since supplied AppDomain id is -1");
				}
				return true;
			}
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				flag2 = this.AppDomainsLifeCycleManagement[appDomainId].onAppDomainUnloadWasCalledAt != DateTime.MinValue;
			}
			return flag2;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004390 File Offset: 0x00002590
		public void SetUnloadStarted(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "SetUnloadStarted for AppDomain id '{0}'", new object[] { appDomainId });
			}
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "SetUnloadStarted aborted since supplied AppDomain id is -1");
				}
				return;
			}
			object sync = this.m_sync;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				Interlocked.Increment(ref this.AppDomainsLifeCycleManagement[appDomainId].unloadStarted);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SetUnloadStarted AppDomain id '{0}': unloadStarted = {1}", new object[]
					{
						appDomainId,
						this.AppDomainsLifeCycleManagement[appDomainId].unloadStarted
					});
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004478 File Offset: 0x00002678
		private bool UnloadStarted(int appDomainId)
		{
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "UnloadStarted aborted since supplied AppDomain id is -1");
				}
				return false;
			}
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				flag2 = 0 < this.AppDomainsLifeCycleManagement[appDomainId].unloadStarted;
			}
			return flag2;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000044F4 File Offset: 0x000026F4
		private void SetMemoryRecycle(int appDomainId)
		{
			if (RSTrace.AppDomainManagerTracer.TraceVerbose)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "SetMemoryRecycle for AppDomain id '{0}'", new object[] { appDomainId });
			}
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "SetMemoryRecycle aborted since supplied AppDomain id is -1");
				}
				return;
			}
			object sync = this.m_sync;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				Interlocked.Increment(ref this.AppDomainsLifeCycleManagement[appDomainId].memoryRecycle);
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SetMemoryRecycle AppDomain id '{0}': memoryRecycle = {1}", new object[]
					{
						appDomainId,
						this.AppDomainsLifeCycleManagement[appDomainId].memoryRecycle
					});
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000045DC File Offset: 0x000027DC
		public bool MemoryRecycle(int appDomainId)
		{
			if (appDomainId == -1)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "MemoryRecycle aborted since supplied AppDomain id is -1");
				}
				return false;
			}
			object sync = this.m_sync;
			bool flag2;
			lock (sync)
			{
				this.EnsureAppDomainLifeCycleManagementEntry(appDomainId);
				flag2 = 0 < this.AppDomainsLifeCycleManagement[appDomainId].memoryRecycle;
			}
			return flag2;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004658 File Offset: 0x00002858
		private Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> AppDomainsLifeCycleManagement
		{
			get
			{
				if (this.m_appDomainsLifeCycleManagement == null)
				{
					Interlocked.CompareExchange<Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry>>(ref this.m_appDomainsLifeCycleManagement, new Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry>(), null);
				}
				return this.m_appDomainsLifeCycleManagement;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000467C File Offset: 0x0000287C
		private void EnsureAppDomainLifeCycleManagementEntry(int appDomainId)
		{
			if (this.AppDomainsLifeCycleManagement.ContainsKey(appDomainId))
			{
				return;
			}
			if (RSTrace.AppDomainManagerTracer.TraceInfo)
			{
				RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Creating a new AppDomainLifeCycleManagementEntry for AppDomain id '{0}'", new object[] { appDomainId });
			}
			this.AppDomainsLifeCycleManagement[appDomainId] = new RsAppDomainManager.AppDomainLifeCycleManagementEntry
			{
				unregistered = 0,
				unloadStarted = 0,
				toBeUnloaded = 0,
				initialized = 0,
				memoryRecycle = 0,
				onAppDomainUnloadWasCalledAt = DateTime.MinValue
			};
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004704 File Offset: 0x00002904
		private Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> GetUnloadedAppDomains()
		{
			Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> dictionary = new Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry>();
			foreach (KeyValuePair<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> keyValuePair in this.AppDomainsLifeCycleManagement)
			{
				if (keyValuePair.Value.onAppDomainUnloadWasCalledAt != DateTime.MinValue)
				{
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Detected an unloaded AppDomain with id '{0}'", new object[] { keyValuePair.Key });
					}
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x04000044 RID: 68
		private RsAppDomainManager.AppDomainEntry[] m_appDomains;

		// Token: 0x04000045 RID: 69
		private Dictionary<int, RsAppDomainManager.AppDomainLifeCycleManagementEntry> m_appDomainsLifeCycleManagement;

		// Token: 0x04000046 RID: 70
		private object m_sync = new object();

		// Token: 0x04000047 RID: 71
		private IRsService m_rsService;

		// Token: 0x04000048 RID: 72
		private const string ReportServiceAssemblyName = "ReportingServicesLibrary";

		// Token: 0x04000049 RID: 73
		private const string ReportServiceTypeName = "Microsoft.ReportingServices.Library.ReportService";

		// Token: 0x0400004A RID: 74
		private IRsHttpRuntime m_rsHttpRuntime;

		// Token: 0x0400004B RID: 75
		private const string HttpRuntimeAssemblyName = "RsHttpRuntime";

		// Token: 0x0400004C RID: 76
		private const string HttpRuntimeTypeName = "ReportingServicesHttpRuntime.RsHttpRuntime";

		// Token: 0x0400004D RID: 77
		private Stopwatch m_timer = new Stopwatch();

		// Token: 0x0400004E RID: 78
		private MemoryMonitorUtilities m_memUtil = new MemoryMonitorUtilities();

		// Token: 0x0400004F RID: 79
		private RsAppDomainType m_rsAppDomainType = RsAppDomainType.Unknown;

		// Token: 0x04000050 RID: 80
		private ServiceContainer m_serviceContainer;

		// Token: 0x0200000D RID: 13
		private struct AppDomainEntry
		{
			// Token: 0x04000051 RID: 81
			public AppDomain appDomain;

			// Token: 0x04000052 RID: 82
			public string appDomainName;

			// Token: 0x04000053 RID: 83
			public int Id;
		}

		// Token: 0x0200000E RID: 14
		private class AppDomainLifeCycleManagementEntry
		{
			// Token: 0x04000054 RID: 84
			public int unregistered;

			// Token: 0x04000055 RID: 85
			public int unloadStarted;

			// Token: 0x04000056 RID: 86
			public int toBeUnloaded;

			// Token: 0x04000057 RID: 87
			public int initialized;

			// Token: 0x04000058 RID: 88
			public int memoryRecycle;

			// Token: 0x04000059 RID: 89
			public DateTime onAppDomainUnloadWasCalledAt;
		}
	}
}
