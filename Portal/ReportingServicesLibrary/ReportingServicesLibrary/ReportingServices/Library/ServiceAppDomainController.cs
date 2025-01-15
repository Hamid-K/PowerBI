using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.HostingInterfaces;
using Microsoft.Win32;
using RSRemoteRpcClient;
using RSRemoteRpcServer;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000231 RID: 561
	internal sealed class ServiceAppDomainController : MarshalByRefObject, IServiceAppDomainController, IDisposable
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0004C434 File Offset: 0x0004A634
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public ServiceAppDomainController()
		{
			Native.MemoryStatusEx memoryStatusEx = default(Native.MemoryStatusEx);
			memoryStatusEx.dwLength = 64;
			Native.GlobalMemoryStatusEx(ref memoryStatusEx);
			this.m_totalPhysicalMemory = memoryStatusEx.ullTotalPhys;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00005C88 File Offset: 0x00003E88
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004C4A0 File Offset: 0x0004A6A0
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0004C4F0 File Offset: 0x0004A6F0
		internal IServiceAppDomain WorkerDomainProxy
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				if (this.m_workerDomainProxy == null)
				{
					if (this.m_workerAppDomain == null && RSTrace.ServiceControllerTracer.TraceWarning)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Warning, "Unable to create a WorkerDomainProxy since m_workerAppDomain is null");
					}
					this.m_workerDomainProxy = ServiceAppDomainController.GetServiceAppDomain(this.m_workerAppDomain);
				}
				return this.m_workerDomainProxy;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					if (this.m_workerDomainProxy == null && value == null)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "WorkerDomainProxy: 'null' is replaced with 'null'");
					}
					else if (this.m_workerDomainProxy != null && value == null)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "WorkerDomainProxy: replacing the current proxy reference with 'null'");
					}
					else if (this.m_workerDomainProxy == null && value != null)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "WorkerDomainProxy: replacing 'null' with a proxy reference");
					}
					else if (this.m_workerDomainProxy != value)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "WorkerDomainProxy: replacing a proxy reference with another one");
					}
				}
				this.m_workerDomainProxy = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004C580 File Offset: 0x0004A780
		private string ServiceLocation
		{
			get
			{
				string text = ServiceAppDomainController.m_serviceExeLocation;
				if (text != null)
				{
					string fileName = Path.GetFileName(text);
					if (StringComparer.OrdinalIgnoreCase.Compare(fileName, "BIN") == 0)
					{
						text = Path.GetDirectoryName(text);
					}
				}
				return text;
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004C5B8 File Offset: 0x0004A7B8
		private void ConfigChanged(object sender, ConfigurationChangeEventArgs e)
		{
			if (Interlocked.CompareExchange(ref this.m_configChanged, 1, 0) != 0)
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Config file changed; ignoring it since a previous change has been intercepted.");
				}
				return;
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Config file changed; stopping the RPC server");
			}
			this.StopRPCServer();
			SymmetricKeyEncryption.ResetKeyManager();
			this.m_resetEvent.Set();
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004C624 File Offset: 0x0004A824
		public static int ServiceStartupTimeout
		{
			get
			{
				if (ServiceAppDomainController.m_serviceStartupTimeout != 0)
				{
					return ServiceAppDomainController.m_serviceStartupTimeout;
				}
				ServiceAppDomainController.m_serviceStartupTimeout = 30;
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control"))
					{
						if (registryKey != null)
						{
							object value = registryKey.GetValue("ServicesPipeTimeout");
							if (value != null)
							{
								ServiceAppDomainController.m_serviceStartupTimeout = (int)value / 1000;
							}
						}
					}
				}
				catch (Exception)
				{
				}
				return ServiceAppDomainController.m_serviceStartupTimeout;
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004C6AC File Offset: 0x0004A8AC
		internal void Start()
		{
			Thread thread = new Thread(new ParameterizedThreadStart(this.StartInternal));
			thread.Start(Thread.CurrentThread);
			thread.Join(1000 * (ServiceAppDomainController.ServiceStartupTimeout - 2));
			if (!this.m_continue)
			{
				throw new Exception("Default appdomain failed to initialize.");
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004C6FC File Offset: 0x0004A8FC
		internal void StartInternal(object parentThread)
		{
			try
			{
				ServiceAppDomainController.m_serviceExeLocation = Path.GetDirectoryName(this.EntryAssembly);
				ServiceAppDomainController.m_fullServiceExePath = this.EntryAssembly;
				RSConfigurationManager rsconfigurationManager = new RSConfigurationFileManager(StaticConfig.Current.GetOrDefault("rsConfigFilePath", string.Empty));
				Globals.InitConfiguration(rsconfigurationManager, RunningApplication.WindowsService);
				this.ConfigureConcurrencyLimit();
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
				Globals.InitServer(true);
				IServiceProvider serviceProvider = AppDomain.CurrentDomain.DomainManager as IServiceProvider;
				if (serviceProvider != null)
				{
					this.m_serviceContainer = (IServiceContainer)serviceProvider.GetService(typeof(IServiceContainer));
					this.m_serviceContainer.RemoveService(typeof(RSConfigurationManager));
					this.m_serviceContainer.AddService(typeof(RSConfigurationManager), rsconfigurationManager);
				}
				this.StartServiceInNewAppDomain(true);
				this.ApplyNativeConfiguration();
				this.m_maintenance = new Thread(new ThreadStart(this.ServiceMaintenance));
				this.m_maintenance.Start();
				this.StartProcessMonitoring();
				int num = 1740;
				DateTime dateTime = Process.GetCurrentProcess().StartTime.Add(new TimeSpan(0, 0, ServiceAppDomainController.ServiceStartupTimeout - 5));
				while (!this.m_isRPCServerStarted && DateTime.Now < dateTime)
				{
					if (num == this.m_startRPCServerResult && RSTrace.AppDomainManagerTracer.TraceError)
					{
						string processName = Process.GetCurrentProcess().ProcessName;
						string text = Utilities.RPCEndpointFromInstanceID(Globals.Configuration.InstanceID);
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Either another process {0} instance is already running or another application with the RPC endpoint {1} is running.", new object[] { processName, text });
						break;
					}
					Thread.Sleep(1000);
				}
				if (!this.m_isRPCServerStarted && this.m_startRPCServerResult != 0)
				{
					throw new Exception("RPC Server failed to start.");
				}
			}
			catch (Exception ex2)
			{
				this.m_continue = false;
				RSEventLog.Current.WriteError(Event.AppDomainFailedToInitialize, new object[]
				{
					AppDomain.CurrentDomain.FriendlyName,
					ex2.Message
				});
				string text2 = string.Format(CultureInfo.InvariantCulture, "Appdomain:{0} {1} failed to initialize. Error: {2}.", AppDomain.CurrentDomain.Id, AppDomain.CurrentDomain.FriendlyName, ex2.ToString());
				Console.WriteLine(text2);
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, text2);
				}
				if (!(parentThread as Thread).IsAlive)
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "Terminating the process because the default appdomain failed to initialize.");
					}
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		private string[] GetUrlPrefixes(UrlConfiguration urls)
		{
			string[] array = new string[urls.UrlReservations.Length];
			for (int i = 0; i < urls.UrlReservations.Length; i++)
			{
				array[i] = urls.UrlReservations[i].UrlPrefix;
			}
			return array;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004CA10 File Offset: 0x0004AC10
		private string[] GetExplicitHostNames(string[] prefixes, out bool bIsWildCardPresent)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			bIsWildCardPresent = false;
			list2 = this.GetServiceAccountHost();
			if (list2 != null)
			{
				foreach (string text in list2)
				{
					if (text != null)
					{
						list.Add(text);
					}
				}
			}
			for (int i = 0; i < prefixes.Length; i++)
			{
				string text2;
				string text3;
				string text4;
				string text5;
				string text6;
				RSConfigurationFileManager.ParseUrlPrefix(prefixes[i], out text2, out text3, out text4, out text5, out text6);
				if (text3 != "*" && text3 != "+")
				{
					list.Add("HTTP/" + text3);
					list.Add("HOST/" + text3);
				}
				else
				{
					bIsWildCardPresent = true;
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004CAF0 File Offset: 0x0004ACF0
		private List<string> GetServiceAccountHost()
		{
			DirectorySearcher directorySearcher = null;
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			string name = current.Name;
			SecurityIdentifier securityIdentifier = current.User;
			List<string> list = new List<string>();
			try
			{
				int num = name.IndexOf('\\');
				if (-1 == num)
				{
					directorySearcher = new DirectorySearcher();
				}
				else
				{
					string text = name.Substring(0, num);
					if (securityIdentifier.IsWellKnown(WellKnownSidType.NetworkServiceSid) || securityIdentifier.IsWellKnown(WellKnownSidType.LocalSystemSid) || securityIdentifier.IsWellKnown(WellKnownSidType.LocalServiceSid) || name.StartsWith("NT Service\\", StringComparison.OrdinalIgnoreCase))
					{
						text = Domain.GetComputerDomain().Name;
						securityIdentifier = (SecurityIdentifier)new NTAccount(text, name).Translate(typeof(SecurityIdentifier));
					}
					directorySearcher = new DirectorySearcher(new DirectoryEntry(string.Format(CultureInfo.InvariantCulture, "LDAP://{0}", text)));
				}
				directorySearcher.Filter = string.Format(CultureInfo.InvariantCulture, "(&(ObjectSID={0}))", securityIdentifier);
				directorySearcher.SearchScope = SearchScope.Subtree;
				directorySearcher.ClientTimeout = new TimeSpan(0, 0, 3);
				SearchResultCollection searchResultCollection = directorySearcher.FindAll();
				if (searchResultCollection != null)
				{
					using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							SearchResult searchResult = (SearchResult)obj;
							foreach (object obj2 in searchResult.Properties["servicePrincipalName"])
							{
								string text2 = (string)obj2;
								list.Add(text2);
								RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SPN for the service account is {0}", new object[] { text2 });
							}
							foreach (object obj3 in searchResult.Properties["userAccountControl"])
							{
								if (obj3 != null && RSTrace.AppDomainManagerTracer.TraceInfo)
								{
									RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "The UserAccountControl value for the service account is {0}", new object[] { obj3.ToString() });
								}
							}
							foreach (object obj4 in searchResult.Properties["msDS-AllowedToDelegateTo"])
							{
								string text3 = (string)obj4;
								if (text3 != null && RSTrace.AppDomainManagerTracer.TraceInfo)
								{
									RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "SPNs allowed for constrained delegation {0}", new object[] { text3 });
								}
							}
						}
						goto IL_027B;
					}
				}
				list = null;
				IL_027B:;
			}
			catch
			{
				list = null;
			}
			finally
			{
				if (directorySearcher != null)
				{
					directorySearcher.Dispose();
				}
			}
			return list;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004CE20 File Offset: 0x0004B020
		private void ApplyNativeConfiguration()
		{
			this.SetWebConfiguration();
			this.SetMemoryMonitorConfiguration();
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004CE30 File Offset: 0x0004B030
		private void SetWebConfiguration(RunningApplication rsApplication, bool enabled, string folder)
		{
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback == null)
			{
				return;
			}
			RSBaseConfiguration configuration = Globals.Configuration;
			Dictionary<RunningApplication, UrlConfiguration> urlConfiguration = configuration.UrlConfiguration;
			Microsoft.ReportingServices.Diagnostics.AuthenticationTypes authenticationTypes = configuration.AuthenticationTypes;
			string[] array = new string[0];
			string[] array2 = new string[0];
			bool flag = false;
			string text = string.Empty;
			string text2 = Path.Combine(Path.GetDirectoryName(configuration.ConfigFilePath), folder);
			int num = 0;
			string text3 = null;
			string text4 = null;
			LogonMethod logonMethod = LogonMethod.Cleartext;
			if (rsApplication != RunningApplication.WebService)
			{
				return;
			}
			RsAppDomainType rsAppDomainType = RsAppDomainType.ReportServer;
			if (authenticationTypes != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None)
			{
				if ((authenticationTypes & Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.RSWindowsNegotiate) != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None && !Globals.DisableEPAuthTypes)
				{
					num |= 2;
				}
				if ((authenticationTypes & Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.RSWindowsKerberos) != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None && !Globals.DisableEPAuthTypes)
				{
					num |= 4;
				}
				if ((authenticationTypes & Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.RSWindowsNTLM) != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None && !Globals.DisableEPAuthTypes)
				{
					num |= 8;
				}
				if ((authenticationTypes & Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.RSWindowsBasic) != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None)
				{
					num |= 32;
					text3 = configuration.AuthDomain;
					text4 = configuration.AuthRealm;
					logonMethod = configuration.LogonMethod;
				}
				if ((authenticationTypes & (Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.Custom | Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.RSForms | Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.OAuth)) != Microsoft.ReportingServices.Diagnostics.AuthenticationTypes.None)
				{
					num = 1;
				}
				if (urlConfiguration != null && urlConfiguration.ContainsKey(rsApplication))
				{
					UrlConfiguration urlConfiguration2 = urlConfiguration[rsApplication];
					text = urlConfiguration2.VirtualRoot;
					if (enabled)
					{
						array = this.GetUrlPrefixes(urlConfiguration2);
						array2 = this.GetExplicitHostNames(array, out flag);
					}
				}
				try
				{
					irsUnmanagedCallback.CreateHttpEndpoint(rsAppDomainType, array, array.Length, array2, array2.Length, flag, text, text2, num, (int)logonMethod, text3, text4, configuration.AuthPersistence, (int)configuration.ExtendedProtectionLevel, (int)configuration.ExtendedProtectionScenario, enabled);
				}
				catch (Exception ex)
				{
					if (RSTrace.ServiceControllerTracer.TraceError)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Error creating HTTP endpoint. {0}", new object[] { ex.ToString() });
					}
				}
				return;
			}
			if (RSTrace.ServiceControllerTracer.TraceError)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Authentication mode is not defined.");
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004CFDC File Offset: 0x0004B1DC
		private void SetWebConfiguration()
		{
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback == null)
			{
				return;
			}
			RSBaseConfiguration configuration = Globals.Configuration;
			this.SetWebConfiguration(RunningApplication.WebService, configuration.IsWebServiceEnabled, "ReportServer");
			try
			{
				string fullName = new DirectoryInfo(RSTrace.ServiceControllerTracer.TraceDirectory).FullName;
				irsUnmanagedCallback.SetDumpConfiguration(fullName, configuration.InstanceName, Dumper.ServiceName, configuration.WatsonFlags);
			}
			catch (Exception ex)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Error setting dump configuration. {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004D078 File Offset: 0x0004B278
		private void SetMemoryMonitorConfiguration()
		{
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback == null)
			{
				return;
			}
			RSConfiguration configuration = Globals.Configuration;
			try
			{
				long num = 0L;
				if (!Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.NoMemoryThrottling))
				{
					num = ResourceUtilities.MaxMemoryThresholdMB * 1024L;
				}
				irsUnmanagedCallback.SetMemoryMonitorConfiguration(configuration.WorkingSetMin, configuration.WorkingSetMax, num, configuration.MemoryLimit, configuration.MaxMemoryLimit);
			}
			catch (Exception ex)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Exception setting memory configuration: {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004D118 File Offset: 0x0004B318
		private void StartProcessMonitoring()
		{
			RSTrace.ServiceControllerTracer.Assert(Globals.Configuration.ProcessTimeout >= 0, "ServiceAppDomainController.StartProcessMonitoring: Globals.Configuration.ProcessTimeout >= 0");
			RSTrace.ServiceControllerTracer.Assert(Globals.Configuration.ProcessTimeoutGcExtension >= 0, "ServiceAppDomainController.StartProcessMonitoring: Globals.Configuration.ProcessTimeout >= 0");
			if (Globals.Configuration.ProcessTimeout > 0)
			{
				if (Globals.Configuration.ProcessTimeoutGcExtension == 0 && RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Gc extension timeout is disabled. ProcessTimeoutGcExtension was {0}.", new object[] { Globals.Configuration.ProcessTimeoutGcExtension });
				}
				try
				{
					this.m_rpcServer.StartProcessMonitor(Globals.Configuration.ProcessTimeout, Globals.Configuration.ProcessTimeoutGcExtension);
					return;
				}
				catch (Exception ex)
				{
					if (RSTrace.ServiceControllerTracer.TraceError)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Error starting process monitor thread. {0}", new object[] { ex.ToString() });
					}
					throw;
				}
			}
			if (RSTrace.ServiceControllerTracer.TraceWarning)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Warning, "Process monitor is disabled. ProcessTimeout was set to {0}.", new object[] { Globals.Configuration.ProcessTimeout });
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004D248 File Offset: 0x0004B448
		private void StopProcessMonitoring()
		{
			this.m_rpcServer.StopProcessMonitor();
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004D258 File Offset: 0x0004B458
		private void IgnoreMonitoringElement()
		{
			try
			{
				this.m_rpcServer.IgnoreMonitoringElement();
			}
			catch (Exception ex)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "IgnoreMonitoringElement: caught an exception: {0}", new object[] { ex });
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public bool StartRPCServer(bool firstTime)
		{
			if (this.m_isRPCServerStarted)
			{
				return true;
			}
			string text = Utilities.RPCEndpointFromInstanceID(Globals.Configuration.InstanceID);
			try
			{
				this.m_rpcServer.StartServer(text, (UIntPtr)0, new RPCDelegationHandler(new ConfigurationChangeEventHandler(this.ConfigChanged)), Global.IRsUnmanagedCallback, RSTrace.CatalogTrace.TraceDirectory, Globals.Configuration.InstanceName, Dumper.ServiceName, Globals.Configuration.WatsonFlags, firstTime);
				this.m_isRPCServerStarted = true;
				this.m_startRPCServerResult = 0;
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "RPC Server started. Endpoint name ='" + text + "'");
				}
				return true;
			}
			catch (Exception ex)
			{
				if (firstTime)
				{
					COMException ex2 = ex as COMException;
					if (ex2 != null)
					{
						this.m_startRPCServerResult = ex2.ErrorCode & 65535;
					}
				}
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Exception caught starting RPC server: {0}", new object[] { ex.ToString() });
				}
				RSEventLog.Current.WriteError(Event.RPCFailedStart, Array.Empty<object>());
			}
			return false;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0004D3BC File Offset: 0x0004B5BC
		private void StopRPCServer()
		{
			this.m_rpcServer.StopServer();
			this.m_isRPCServerStarted = false;
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController - RPC Server stopped.");
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004D3EC File Offset: 0x0004B5EC
		private void StartServiceInNewAppDomain(bool firstTime)
		{
			if (this.m_workerAppDomain == null)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				string text = "{0}_{1}";
				object obj = RsAppDomainType.WindowsService;
				long serviceAppDomainInstanceCounter = this.m_serviceAppDomainInstanceCounter;
				this.m_serviceAppDomainInstanceCounter = serviceAppDomainInstanceCounter + 1L;
				string text2 = string.Format(invariantCulture, text, obj, serviceAppDomainInstanceCounter);
				try
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					AppDomainSetup appDomainSetup = new AppDomainSetup();
					appDomainSetup.ApplicationBase = currentDomain.SetupInformation.ApplicationBase;
					appDomainSetup.LoaderOptimization = LoaderOptimization.MultiDomainHost;
					this.m_workerAppDomain = AppDomain.CreateDomain(text2, this.GetDefaultDomainIdentity(), appDomainSetup);
				}
				catch (Exception ex)
				{
					Exception ex3;
					RSEventLog.Current.WriteError(Event.AppDomainFailedToStart, new object[]
					{
						text2,
						ex3.ToString()
					});
					if (RSTrace.AppDomainManagerTracer.TraceError)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "AppDomain {0} failed to start. Error: {1}", new object[]
						{
							text2,
							ex3.ToString()
						});
					}
					if (ExceptionUtils.ContainsException(ex3, (Exception ex) => ex is OutOfMemoryException))
					{
						throw;
					}
				}
			}
			bool flag = false;
			try
			{
				if (this.WorkerDomainProxy != null)
				{
					this.WorkerDomainProxy.StartService(this, firstTime);
					flag = this.WorkerDomainProxy.IsServiceStarted;
				}
			}
			catch (Exception ex2)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Service failed to start. Error: {0}", new object[] { ex2.ToString() });
				}
				if (ExceptionUtils.ContainsException(ex2, (Exception ex) => ex is OutOfMemoryException))
				{
					throw;
				}
			}
			if (!flag)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, "Exiting process because windows service failed to start.");
				}
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004D5B8 File Offset: 0x0004B7B8
		private Evidence GetDefaultDomainIdentity()
		{
			Evidence evidence = new Evidence();
			bool flag = false;
			IEnumerator hostEnumerator = AppDomain.CurrentDomain.Evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Zone)
				{
					flag = true;
				}
				evidence.AddHost(hostEnumerator.Current);
			}
			if (!flag)
			{
				evidence.AddHost(new Zone(SecurityZone.MyComputer));
			}
			IEnumerator assemblyEnumerator = AppDomain.CurrentDomain.Evidence.GetAssemblyEnumerator();
			while (assemblyEnumerator.MoveNext())
			{
				object obj = assemblyEnumerator.Current;
				evidence.AddAssembly(obj);
			}
			return evidence;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0004D638 File Offset: 0x0004B838
		private string EntryAssembly
		{
			get
			{
				if (this.m_entryAssembly == null)
				{
					Assembly callingAssembly = Assembly.GetCallingAssembly();
					this.m_entryAssembly = callingAssembly.Location;
				}
				return this.m_entryAssembly;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004D668 File Offset: 0x0004B868
		internal static IServiceAppDomain GetServiceAppDomain(AppDomain domain)
		{
			if (domain == null)
			{
				return null;
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Creating an instance of class '{0}' from assembly '{1}' in domain '{2}'", new object[]
				{
					ServiceAppDomainController.AppDomainAssemblyName,
					ServiceAppDomainController.ServiceAppDomainClassName,
					domain.FriendlyName
				});
			}
			return (IServiceAppDomain)domain.CreateInstanceAndUnwrap(ServiceAppDomainController.AppDomainAssemblyName, ServiceAppDomainController.ServiceAppDomainClassName);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		private void ServiceMaintenance()
		{
			try
			{
				while (this.m_continue)
				{
					try
					{
						this.ServiceMaintenanceInternal();
					}
					catch (ThreadAbortException)
					{
						Thread.ResetAbort();
					}
				}
			}
			finally
			{
				this.IgnoreMonitoringElement();
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004D71C File Offset: 0x0004B91C
		private void CheckForScheduledRecycle(ref ServiceAppDomainController.Recycle recycle)
		{
			TimeSpan recycleTime = Globals.Configuration.RecycleTime;
			if (recycleTime != TimeSpan.MinValue && DateTime.Now - recycle.lastRecycle > recycleTime)
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Time app domain recycle requested");
				}
				recycle.cycleWebAppDomains = true;
				recycle.cycleWindowsServiceAppDomain = true;
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004D784 File Offset: 0x0004B984
		private void CheckForExceedingMemoryRecycle(ref ServiceAppDomainController.Recycle recycle)
		{
			if (RsMemoryPressureLevel.ExceedingLimit == Global.GetCurrentMemoryLevel())
			{
				int num = 10;
				int num2 = recycle.memoryExceedingLimitCount + 1;
				recycle.memoryExceedingLimitCount = num2;
				if (num == num2)
				{
					recycle.memoryExceedingLimitCount = 0;
					recycle.memoryRecycle = true;
					recycle.ensureWindowsAppDomainUnload = true;
					recycle.cycleWebAppDomains = true;
					recycle.cycleWindowsServiceAppDomain = true;
					if (RSTrace.ServiceControllerTracer.TraceWarning)
					{
						Console.WriteLine("Memory Limits Exceeded.  AppDomain Recycling will be attempted.");
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Warning, "Memory Limits Exceeded.  AppDomain Recycling will be attempted.");
						if (this.m_workerAppDomain != null)
						{
							RSEventLog.Current.WriteInformation(Event.AppDomainMaxMemoryLimitReached, new object[] { this.m_workerAppDomain.FriendlyName });
							return;
						}
					}
				}
			}
			else
			{
				recycle.memoryExceedingLimitCount = 0;
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004D82C File Offset: 0x0004BA2C
		private void CheckForServiceNotWorkingRecycle(ref ServiceAppDomainController.Recycle recycle)
		{
			if (this.WorkerDomainProxy != null && !this.WorkerDomainProxy.IsServiceWorking)
			{
				if (!RpcServer.IsDebuggerAttached())
				{
					if (RSTrace.ServiceControllerTracer.TraceInfo)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling app domain because Windows Service AppDomain is not active.");
					}
					recycle.cycleWindowsServiceAppDomain = true;
					recycle.ensureWindowsAppDomainUnload = true;
					return;
				}
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Polling thread is not working but will not be recycled because we are under a debugger");
				}
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004D89C File Offset: 0x0004BA9C
		private void CheckForConfigChangedRecycle(ref ServiceAppDomainController.Recycle recycle)
		{
			if (Interlocked.CompareExchange(ref this.m_configChanged, 0, 1) != 0)
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "The Config file changed; resetting the config file change tracking data member and recycling all AppDomains");
				}
				recycle.cycleWebAppDomains = true;
				recycle.cycleWindowsServiceAppDomain = true;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
		private void CycleWebAppDomains(ServiceAppDomainController.Recycle recycle)
		{
			AppDomain appDomain = Global.GetAppDomain(RsAppDomainType.ReportServer);
			if (recycle.memoryRecycle)
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling ASP.NET AppDomains with memory recycling, i.e. let the current AppDomains unload before the new ones are created.");
				}
				this.StopHttpEndpoints();
				if (appDomain != null)
				{
					if (RSTrace.ServiceControllerTracer.TraceInfo)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling the ReportServer AppDomain");
					}
					Global.UnloadAppDomain(appDomain, true);
				}
				this.ApplyNativeConfiguration();
				return;
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling ASP.NET AppDomains without memory recycling, i.e. create the new ones before the current ones fully unload.");
			}
			this.ApplyNativeConfiguration();
			if (appDomain != null)
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling the ReportServer AppDomain");
				}
				Global.UnloadAppDomain(appDomain, false);
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004D98C File Offset: 0x0004BB8C
		private void CycleWindowsServiceAppDomain(ref ServiceAppDomainController.Recycle recycle)
		{
			TimeSpan maxAppDomainUnloadTime = Globals.Configuration.MaxAppDomainUnloadTime;
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Recycling the WindowsService AppDomain from the default domain. Max allowed unload time = {0} sec", new object[] { maxAppDomainUnloadTime.TotalSeconds });
			}
			try
			{
				this.InternalStop(ServiceAppDomainController.AppDomainStopMode.RecycleStopPolling);
			}
			finally
			{
				ServiceAppDomainController.DeadDomain deadDomain = new ServiceAppDomainController.DeadDomain();
				deadDomain.TimeDied = DateTime.Now;
				deadDomain.ApplicationDomain = this.m_workerAppDomain;
				deadDomain.WorkerDomainProxy = this.WorkerDomainProxy;
				this.m_deadDomains.Add(deadDomain);
				this.m_workerAppDomain = null;
				this.WorkerDomainProxy = null;
			}
			if (recycle.ensureWindowsAppDomainUnload)
			{
				DateTime now = DateTime.Now;
				if (recycle.memoryRecycle)
				{
					int num = 0;
					while (this.m_continue)
					{
						if (RSTrace.ServiceControllerTracer.TraceInfo)
						{
							RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController::CycleWindowsServiceAppDomain - Memory pressure detected; Mark the WindowsService (worker) AppDomain as active.");
						}
						this.MarkProcessAsActive();
						if (maxAppDomainUnloadTime != TimeSpan.MinValue && DateTime.Now - now > maxAppDomainUnloadTime)
						{
							if (RSTrace.ServiceControllerTracer.TraceInfo)
							{
								RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Forcing unload of domains due to max unload time being reached.");
								break;
							}
							break;
						}
						else
						{
							this.m_resetEvent.WaitOne(1000, false);
							bool flag = num % 30 == 0;
							if (!this.AreAnyDeadDomainsWorking(flag))
							{
								break;
							}
							num++;
						}
					}
				}
				this.UnloadAllDeadDomains(recycle.memoryRecycle);
			}
			if (this.m_continue)
			{
				this.m_configChanged = 0;
				this.StartServiceInNewAppDomain(false);
				recycle.lastRecycle = DateTime.Now;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004DB18 File Offset: 0x0004BD18
		private void ServiceMaintenanceInternal()
		{
			ServiceAppDomainController.Recycle recycle = default(ServiceAppDomainController.Recycle);
			recycle.lastRecycle = DateTime.Now;
			recycle.memoryExceedingLimitCount = 0;
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Total Physical memory: {0}", new object[] { this.m_totalPhysicalMemory });
			}
			do
			{
				recycle.Reset();
				try
				{
					if (this.WorkerDomainProxy == null)
					{
						this.StartServiceInNewAppDomain(false);
						recycle.lastRecycle = DateTime.Now;
					}
					this.CheckForScheduledRecycle(ref recycle);
					this.CheckForExceedingMemoryRecycle(ref recycle);
					this.CheckForServiceNotWorkingRecycle(ref recycle);
					this.CheckForConfigChangedRecycle(ref recycle);
					if (recycle.cycleWebAppDomains)
					{
						this.CycleWebAppDomains(recycle);
					}
					if (recycle.cycleWindowsServiceAppDomain)
					{
						this.CycleWindowsServiceAppDomain(ref recycle);
					}
					this.ClearAnyDeadDomains();
					if (RSTrace.ServiceControllerTracer.TraceVerbose)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Verbose, "ServiceAppDomainController::ServiceMaintenanceInternal - Mark the WindowsService (worker) AppDomain as active.");
					}
					this.MarkProcessAsActive();
				}
				catch (Exception ex)
				{
					Exception ex2;
					if (ExceptionUtils.ContainsException(ex2, (Exception ex) => ex is OutOfMemoryException || ex is ThreadAbortException))
					{
						if (RSTrace.ServiceControllerTracer.TraceInfo)
						{
							RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceMaintenanceInternal: caught an exception: {0}", new object[] { ex2.Message });
						}
						if (ex2 is ThreadAbortException)
						{
							Thread.ResetAbort();
						}
						if (!this.m_continue)
						{
							break;
						}
					}
					else if (ExceptionUtils.IsStoppingException(ex2))
					{
						this.m_continue = false;
						if (RSTrace.ServiceControllerTracer.TraceError)
						{
							RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "ServiceMaintenanceInternal: Exiting process for the following exception: {0}", new object[] { ex2.ToString() });
						}
						Process.GetCurrentProcess().Kill();
						break;
					}
					if (RSTrace.ServiceControllerTracer.TraceError)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "ServiceMaintenanceInternal: Restarting maintenance thread for the following exception: {0}.", new object[] { ex2.ToString() });
					}
				}
				if (!recycle.cycleWindowsServiceAppDomain)
				{
					this.m_resetEvent.WaitOne(1000, false);
				}
			}
			while (this.m_continue);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004DD14 File Offset: 0x0004BF14
		private bool AreAnyDeadDomainsWorking(bool traceProgress)
		{
			bool flag = false;
			foreach (object obj in this.m_deadDomains)
			{
				ServiceAppDomainController.DeadDomain deadDomain = (ServiceAppDomainController.DeadDomain)obj;
				AppDomain applicationDomain = deadDomain.ApplicationDomain;
				IServiceAppDomain workerDomainProxy = deadDomain.WorkerDomainProxy;
				if (RSTrace.ServiceControllerTracer.TraceInfo && traceProgress)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Waiting for domain to finish processing items");
				}
				if (workerDomainProxy.StillProcessing)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
		private void UnloadAllDeadDomains(bool memoryRecycle)
		{
			foreach (object obj in this.m_deadDomains)
			{
				ServiceAppDomainController.DeadDomain deadDomain = (ServiceAppDomainController.DeadDomain)obj;
				AppDomain applicationDomain = deadDomain.ApplicationDomain;
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "AppDomain {0} ('{1}') didn't unload gracefully; force an unload", new object[] { applicationDomain.Id, applicationDomain.FriendlyName });
				}
				IServiceAppDomain workerDomainProxy = deadDomain.WorkerDomainProxy;
				if (workerDomainProxy != null)
				{
					workerDomainProxy.EndService(Globals.ServiceStopMode.FullStop);
				}
				ServiceAppDomainController.UnloadAppDomain(applicationDomain, memoryRecycle);
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController::UnloadAllDeadDomains - unloaded a dead AppDomain; Mark the WindowsService (worker) AppDomain as active.");
				}
				this.MarkProcessAsActive();
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing the collection of dead AppDomains");
			}
			this.m_deadDomains.Clear();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004DE9C File Offset: 0x0004C09C
		private void ClearAnyDeadDomains()
		{
			if (this.m_deadDomains.Count == 0)
			{
				return;
			}
			for (int i = this.m_deadDomains.Count - 1; i >= 0; i--)
			{
				ServiceAppDomainController.DeadDomain deadDomain = (ServiceAppDomainController.DeadDomain)this.m_deadDomains[i];
				AppDomain applicationDomain = deadDomain.ApplicationDomain;
				IServiceAppDomain workerDomainProxy = deadDomain.WorkerDomainProxy;
				string text = string.Empty;
				int num = -1;
				if (applicationDomain != null)
				{
					num = applicationDomain.Id;
					text = applicationDomain.FriendlyName;
				}
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0} ('{1}')", new object[] { num, text });
					if (workerDomainProxy == null)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: the DomainProxy has been disposed already in EndService", new object[] { num });
					}
				}
				bool flag = false;
				TimeSpan maxTimedAppDomainUnload = Globals.Configuration.MaxTimedAppDomainUnload;
				if (maxTimedAppDomainUnload == TimeSpan.MinValue)
				{
					if (RSTrace.ServiceControllerTracer.TraceInfo)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: MaxTimedAppDomainUnload should be configured in order to force an unload herein", new object[] { num });
					}
				}
				else if (DateTime.Now - deadDomain.TimeDied > maxTimedAppDomainUnload)
				{
					flag = true;
				}
				bool flag2 = false;
				if (workerDomainProxy != null)
				{
					flag2 = workerDomainProxy.StillProcessing;
				}
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					if (flag2)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: the AppDomain is still processing", new object[] { num });
						if (!flag)
						{
							RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: the AppDomain has NOT been around long enough to force an unload", new object[] { num });
						}
					}
					else
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: the AppDomain is not processing", new object[] { num });
					}
				}
				if (!flag2 || flag)
				{
					if (RSTrace.ServiceControllerTracer.TraceInfo)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Clearing dead AppDomain {0}: the AppDomain is not processing or its allotted unload time elapsed; forcing an unload...", new object[] { num });
					}
					if (workerDomainProxy != null)
					{
						workerDomainProxy.EndService(Globals.ServiceStopMode.FullStop);
					}
					ServiceAppDomainController.UnloadAppDomain(applicationDomain, false);
					this.m_deadDomains.RemoveAt(i);
				}
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController::ClearAnyDeadDomains - Cleared dead AppDomains; Mark the WindowsService (worker) AppDomain as active.");
				}
				this.MarkProcessAsActive();
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void MarkProcessAsActive()
		{
			if (RSTrace.ServiceControllerTracer.TraceVerbose)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Verbose, "ServiceAppDomainController::MarkProcessAsActive - Mark the WindowsService (worker) AppDomain as active.");
			}
			if (this.m_rpcServer != null)
			{
				this.m_rpcServer.MarkProcessAsActive();
				return;
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "ServiceAppDomainController::MarkProcessAsActive - m_rpcServer is null; Cannot mark the WindowsService (worker) AppDomain as active.");
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004E124 File Offset: 0x0004C324
		internal void Stop()
		{
			try
			{
				this.StopProcessMonitoring();
			}
			catch (Exception ex)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "***********Exception caught shutting down Monitoring thread**************");
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, ex.ToString());
				}
			}
			try
			{
				this.StopRPCServer();
			}
			catch (Exception ex2)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "***********Exception caught shutting down RPC service**************");
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, ex2.ToString());
				}
			}
			try
			{
				this.m_continue = false;
				this.StopHttpEndpoints();
				Global.UnloadAppDomain(RsAppDomainType.ReportServer, false);
				this.m_resetEvent.Set();
				this.InternalStop(ServiceAppDomainController.AppDomainStopMode.ProcessStop);
			}
			catch (Exception ex3)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "***********Exception caught shutting down service**************");
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, ex3.ToString());
				}
			}
			if (RSTrace.ServiceControllerTracer.TraceInfo)
			{
				RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "Service shutting down.");
			}
			((IDisposable)this).Dispose();
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004E248 File Offset: 0x0004C448
		private void InternalStop(ServiceAppDomainController.AppDomainStopMode stopMode)
		{
			try
			{
				if (RSTrace.ServiceControllerTracer.TraceInfo)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "InternalStop: stop the service code running in the worker domain");
				}
				if (this.m_workerAppDomain != null)
				{
					if (RSTrace.ServiceControllerTracer.TraceInfo)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Info, "InternalStop: found a worker domain");
					}
					IServiceAppDomain workerDomainProxy = this.WorkerDomainProxy;
					Globals.ServiceStopMode serviceStopMode = Globals.ServiceStopMode.FullStop;
					if (stopMode == ServiceAppDomainController.AppDomainStopMode.RecycleStopPolling)
					{
						serviceStopMode = Globals.ServiceStopMode.StopPollingOnly;
					}
					workerDomainProxy.EndService(serviceStopMode);
					if (stopMode == ServiceAppDomainController.AppDomainStopMode.ProcessStop)
					{
						ServiceAppDomainController.UnloadAppDomain(this.m_workerAppDomain, false);
					}
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.ServiceControllerTracer.TraceError)
				{
					RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Exception caught in internal stop: {0}", new object[] { ex.ToString() });
				}
				throw;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004E2FC File Offset: 0x0004C4FC
		private static void UnloadAppDomain(AppDomain domain, bool memoryRecycle)
		{
			if (domain == null)
			{
				return;
			}
			int num = 0;
			for (;;)
			{
				try
				{
					Global.UnloadAppDomain(domain, memoryRecycle);
				}
				catch (AppDomainUnloadedException)
				{
				}
				catch (CannotUnloadAppDomainException)
				{
					if (RSTrace.ServiceControllerTracer.TraceError)
					{
						RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Can't unload domain, trying again");
					}
					Thread.Sleep(1000);
					num++;
					if (num > 10)
					{
						if (RSTrace.ServiceControllerTracer.TraceError)
						{
							RSTrace.ServiceControllerTracer.Trace(TraceLevel.Error, "Unable to unload app domain, terminating process");
						}
						Process.GetCurrentProcess().Kill();
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004E390 File Offset: 0x0004C590
		private void StopHttpEndpoints()
		{
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback == null)
			{
				return;
			}
			irsUnmanagedCallback.StopHttpEndpoint(RsAppDomainType.ReportServer);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		private void ConfigureConcurrencyLimit()
		{
			long num;
			long num2;
			long num3;
			SkuUtil.GetConcurrencyLimit(Sku.GetInstalledSku(Globals.Configuration.InstanceID), out num, out num2, out num3);
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback == null)
			{
				return;
			}
			irsUnmanagedCallback.ConfigureConcurrencyLimit(num, num2, num3);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004E3EB File Offset: 0x0004C5EB
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004E3FA File Offset: 0x0004C5FA
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_resetEvent != null)
			{
				this.m_resetEvent.Close();
				this.m_resetEvent = null;
			}
		}

		// Token: 0x04000721 RID: 1825
		private const string ReportServerFolder = "ReportServer";

		// Token: 0x04000722 RID: 1826
		private static string m_serviceExeLocation = null;

		// Token: 0x04000723 RID: 1827
		private static string m_fullServiceExePath = null;

		// Token: 0x04000724 RID: 1828
		private static string AppDomainAssemblyName = Assembly.GetExecutingAssembly().FullName;

		// Token: 0x04000725 RID: 1829
		private RSTrace m_tracer = RSTrace.ServiceControllerTracer;

		// Token: 0x04000726 RID: 1830
		private readonly ulong m_totalPhysicalMemory;

		// Token: 0x04000727 RID: 1831
		private IServiceContainer m_serviceContainer;

		// Token: 0x04000728 RID: 1832
		private IServiceAppDomain m_workerDomainProxy;

		// Token: 0x04000729 RID: 1833
		private AppDomain m_workerAppDomain;

		// Token: 0x0400072A RID: 1834
		private bool m_continue = true;

		// Token: 0x0400072B RID: 1835
		private int m_configChanged;

		// Token: 0x0400072C RID: 1836
		private AutoResetEvent m_resetEvent = new AutoResetEvent(false);

		// Token: 0x0400072D RID: 1837
		private ArrayList m_deadDomains = new ArrayList();

		// Token: 0x0400072E RID: 1838
		private Thread m_maintenance;

		// Token: 0x0400072F RID: 1839
		private RpcServer m_rpcServer = new RpcServer();

		// Token: 0x04000730 RID: 1840
		private static string ServiceAppDomainClassName = typeof(ServiceAppDomain).ToString();

		// Token: 0x04000731 RID: 1841
		private bool m_isRPCServerStarted;

		// Token: 0x04000732 RID: 1842
		private int m_startRPCServerResult;

		// Token: 0x04000733 RID: 1843
		private static int m_serviceStartupTimeout = 0;

		// Token: 0x04000734 RID: 1844
		private const string VirtualAccountPrefix = "NT Service";

		// Token: 0x04000735 RID: 1845
		private long m_serviceAppDomainInstanceCounter;

		// Token: 0x04000736 RID: 1846
		private string m_entryAssembly;

		// Token: 0x020004A9 RID: 1193
		private struct Recycle
		{
			// Token: 0x06002402 RID: 9218 RVA: 0x0008592B File Offset: 0x00083B2B
			public void Reset()
			{
				this.cycleWebAppDomains = false;
				this.cycleWindowsServiceAppDomain = false;
				this.ensureWindowsAppDomainUnload = false;
				this.memoryRecycle = false;
			}

			// Token: 0x04001086 RID: 4230
			public bool cycleWebAppDomains;

			// Token: 0x04001087 RID: 4231
			public bool cycleWindowsServiceAppDomain;

			// Token: 0x04001088 RID: 4232
			public bool ensureWindowsAppDomainUnload;

			// Token: 0x04001089 RID: 4233
			public bool memoryRecycle;

			// Token: 0x0400108A RID: 4234
			public DateTime lastRecycle;

			// Token: 0x0400108B RID: 4235
			public int memoryExceedingLimitCount;
		}

		// Token: 0x020004AA RID: 1194
		private class DeadDomain
		{
			// Token: 0x0400108C RID: 4236
			public DateTime TimeDied = DateTime.MinValue;

			// Token: 0x0400108D RID: 4237
			public AppDomain ApplicationDomain;

			// Token: 0x0400108E RID: 4238
			public IServiceAppDomain WorkerDomainProxy;
		}

		// Token: 0x020004AB RID: 1195
		private enum AppDomainStopMode
		{
			// Token: 0x04001090 RID: 4240
			ProcessStop,
			// Token: 0x04001091 RID: 4241
			RecycleStopPolling
		}
	}
}
