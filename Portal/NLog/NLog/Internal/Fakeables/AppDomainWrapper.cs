using System;
using System.Collections.Generic;
using System.Reflection;
using NLog.Common;

namespace NLog.Internal.Fakeables
{
	// Token: 0x02000168 RID: 360
	public class AppDomainWrapper : IAppDomain
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x0002C330 File Offset: 0x0002A530
		public AppDomainWrapper(AppDomain appDomain)
		{
			this._currentAppDomain = appDomain;
			try
			{
				this.BaseDirectory = AppDomainWrapper.LookupBaseDirectory(appDomain) ?? string.Empty;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "AppDomain.BaseDirectory Failed");
				this.BaseDirectory = string.Empty;
			}
			try
			{
				this.ConfigurationFile = AppDomainWrapper.LookupConfigurationFile(appDomain);
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "AppDomain.SetupInformation.ConfigurationFile Failed");
				this.ConfigurationFile = string.Empty;
			}
			try
			{
				this.PrivateBinPath = AppDomainWrapper.LookupPrivateBinPath(appDomain);
			}
			catch (Exception ex3)
			{
				InternalLogger.Warn(ex3, "AppDomain.SetupInformation.PrivateBinPath Failed");
				this.PrivateBinPath = ArrayHelper.Empty<string>();
			}
			this.FriendlyName = appDomain.FriendlyName;
			this.Id = appDomain.Id;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0002C404 File Offset: 0x0002A604
		private static string LookupBaseDirectory(AppDomain appDomain)
		{
			return appDomain.BaseDirectory;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0002C40C File Offset: 0x0002A60C
		private static string LookupConfigurationFile(AppDomain appDomain)
		{
			return appDomain.SetupInformation.ConfigurationFile;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002C41C File Offset: 0x0002A61C
		private static string[] LookupPrivateBinPath(AppDomain appDomain)
		{
			string privateBinPath = appDomain.SetupInformation.PrivateBinPath;
			if (!string.IsNullOrEmpty(privateBinPath))
			{
				return privateBinPath.SplitAndTrimTokens(';');
			}
			return ArrayHelper.Empty<string>();
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0002C44B File Offset: 0x0002A64B
		public static AppDomainWrapper CurrentDomain
		{
			get
			{
				return new AppDomainWrapper(AppDomain.CurrentDomain);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0002C457 File Offset: 0x0002A657
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0002C45F File Offset: 0x0002A65F
		public string BaseDirectory { get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0002C468 File Offset: 0x0002A668
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0002C470 File Offset: 0x0002A670
		public string ConfigurationFile { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0002C479 File Offset: 0x0002A679
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0002C481 File Offset: 0x0002A681
		public IEnumerable<string> PrivateBinPath { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0002C48A File Offset: 0x0002A68A
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0002C492 File Offset: 0x0002A692
		public string FriendlyName { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0002C49B File Offset: 0x0002A69B
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0002C4A3 File Offset: 0x0002A6A3
		public int Id { get; private set; }

		// Token: 0x060010EF RID: 4335 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		public IEnumerable<Assembly> GetAssemblies()
		{
			if (this._currentAppDomain != null)
			{
				return this._currentAppDomain.GetAssemblies();
			}
			return ArrayHelper.Empty<Assembly>();
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060010F0 RID: 4336 RVA: 0x0002C4C7 File Offset: 0x0002A6C7
		// (remove) Token: 0x060010F1 RID: 4337 RVA: 0x0002C4F7 File Offset: 0x0002A6F7
		public event EventHandler<EventArgs> ProcessExit
		{
			add
			{
				if (this.processExitEvent == null && this._currentAppDomain != null)
				{
					this._currentAppDomain.ProcessExit += this.OnProcessExit;
				}
				this.processExitEvent += value;
			}
			remove
			{
				this.processExitEvent -= value;
				if (this.processExitEvent == null && this._currentAppDomain != null)
				{
					this._currentAppDomain.ProcessExit -= this.OnProcessExit;
				}
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060010F2 RID: 4338 RVA: 0x0002C528 File Offset: 0x0002A728
		// (remove) Token: 0x060010F3 RID: 4339 RVA: 0x0002C560 File Offset: 0x0002A760
		private event EventHandler<EventArgs> processExitEvent;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060010F4 RID: 4340 RVA: 0x0002C595 File Offset: 0x0002A795
		// (remove) Token: 0x060010F5 RID: 4341 RVA: 0x0002C5C5 File Offset: 0x0002A7C5
		public event EventHandler<EventArgs> DomainUnload
		{
			add
			{
				if (this.domainUnloadEvent == null && this._currentAppDomain != null)
				{
					this._currentAppDomain.DomainUnload += this.OnDomainUnload;
				}
				this.domainUnloadEvent += value;
			}
			remove
			{
				this.domainUnloadEvent -= value;
				if (this.domainUnloadEvent == null && this._currentAppDomain != null)
				{
					this._currentAppDomain.DomainUnload -= this.OnDomainUnload;
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060010F6 RID: 4342 RVA: 0x0002C5F8 File Offset: 0x0002A7F8
		// (remove) Token: 0x060010F7 RID: 4343 RVA: 0x0002C630 File Offset: 0x0002A830
		private event EventHandler<EventArgs> domainUnloadEvent;

		// Token: 0x060010F8 RID: 4344 RVA: 0x0002C668 File Offset: 0x0002A868
		private void OnDomainUnload(object sender, EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = this.domainUnloadEvent;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0002C688 File Offset: 0x0002A888
		private void OnProcessExit(object sender, EventArgs eventArgs)
		{
			EventHandler<EventArgs> eventHandler = this.processExitEvent;
			if (eventHandler != null)
			{
				eventHandler(sender, eventArgs);
			}
		}

		// Token: 0x04000492 RID: 1170
		private readonly AppDomain _currentAppDomain;
	}
}
