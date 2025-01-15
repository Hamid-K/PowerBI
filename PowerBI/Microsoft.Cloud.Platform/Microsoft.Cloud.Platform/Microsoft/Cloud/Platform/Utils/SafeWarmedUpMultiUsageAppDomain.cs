using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000175 RID: 373
	public class SafeWarmedUpMultiUsageAppDomain
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00021F2C File Offset: 0x0002012C
		public SafeWarmedUpMultiUsageAppDomain(SafeWarmedUpObjectManager safeWarmedUpObjectManager, ITraceSource tracer, Type marshallerType, int newDomainId, int maxSafeCalls)
		{
			this.m_owner = safeWarmedUpObjectManager;
			this.m_appDomain = null;
			this.m_marshallerType = marshallerType;
			this.m_managementLock = new object();
			this.m_id = (long)newDomainId;
			this.m_createdHandlesCount = 0;
			this.m_tracer = tracer;
			this.m_maxSafeCalls = maxSafeCalls;
			this.m_refCount = 1;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00021F88 File Offset: 0x00020188
		public string Identification
		{
			get
			{
				return string.Concat(new object[]
				{
					this.GetHashCode().ToString(),
					"(",
					this.m_id,
					") ",
					this.MetadataSummary()
				});
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00021FD8 File Offset: 0x000201D8
		public void InitAndWarmup()
		{
			this.m_tracer.TraceInformation("SafeWarmedUpMultiUsageAppDomain {0} InitAndWarmup called", new object[] { this.Identification });
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
			appDomainSetup.DisallowBindingRedirects = false;
			appDomainSetup.DisallowCodeDownload = true;
			appDomainSetup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			UtilsContext.Current.RunWithClearContext(delegate
			{
				this.m_appDomain = AppDomain.CreateDomain("SWUMUAD #" + this.m_id, null, appDomainSetup);
				using (ISafeWarmedUpObjectMarshaler safeWarmedUpObjectMarshaler = (ISafeWarmedUpObjectMarshaler)this.m_appDomain.CreateInstanceAndUnwrap(this.m_marshallerType.Assembly.FullName, this.m_marshallerType.FullName))
				{
					safeWarmedUpObjectMarshaler.WarmUp();
				}
			});
			this.m_tracer.TraceInformation("SafeWarmedUpMultiUsageAppDomain {0}: InitAndWarmup done", new object[] { this.Identification });
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00022098 File Offset: 0x00020298
		public ISafeWarmedUpObjectMarshaler GetNewMarshaler(object creationData)
		{
			ISafeWarmedUpObjectMarshaler returnedMarshaller = null;
			ISafeWarmedUpObjectMarshaler newMarshaler = null;
			object managementLock = this.m_managementLock;
			lock (managementLock)
			{
				UtilsContext.Current.RunWithClearContext(delegate
				{
					newMarshaler = (ISafeWarmedUpObjectMarshaler)this.m_appDomain.CreateInstanceAndUnwrap(this.m_marshallerType.Assembly.FullName, this.m_marshallerType.FullName);
				});
			}
			UtilsContext.Current.RunWithClearContext(delegate
			{
				using (DisposeController disposeController = new DisposeController(newMarshaler))
				{
					if (newMarshaler.TryInitializeUnSafeObject(creationData))
					{
						disposeController.PreventDispose();
						returnedMarshaller = newMarshaler;
					}
				}
			});
			if (returnedMarshaller == null)
			{
				this.m_tracer.TraceError("SafeWarmedUpMultiUsageAppDomain ({0}): Marshaler.initialization failed", new object[] { this.Identification });
			}
			else
			{
				this.m_tracer.TraceInformation("SafeWarmedUpMultiUsageAppDomain ({0}): Marshaler initialization completed", new object[] { this.Identification });
			}
			return returnedMarshaller;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00022170 File Offset: 0x00020370
		public void AccountForUsage(out bool isMaintenanceRequired)
		{
			int num = Interlocked.Increment(ref this.m_createdHandlesCount);
			isMaintenanceRequired = num >= this.m_maxSafeCalls && num % this.m_maxSafeCalls == 0;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000221A2 File Offset: 0x000203A2
		public void Reserve()
		{
			Interlocked.Increment(ref this.m_refCount);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000221B0 File Offset: 0x000203B0
		public void Release()
		{
			int num = Interlocked.Decrement(ref this.m_refCount);
			if (num <= 0)
			{
				if (num < 0)
				{
					this.m_tracer.TraceError("m_refCount={0} in Domain {1} with {2} allocations", new object[] { num, this.Identification, this.m_createdHandlesCount });
				}
				this.UnloadAppDomain();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002220D File Offset: 0x0002040D
		public bool ShouldRetire
		{
			get
			{
				return this.m_createdHandlesCount >= this.m_maxSafeCalls;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00022220 File Offset: 0x00020420
		public string MetadataSummary()
		{
			int createdHandlesCount = this.m_createdHandlesCount;
			int refCount = this.m_refCount;
			return string.Format("Created:{0}, RefCount:{1}", createdHandlesCount, refCount);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00022254 File Offset: 0x00020454
		private void UnloadAppDomain()
		{
			AppDomain appDomainToUnload = Interlocked.Exchange<AppDomain>(ref this.m_appDomain, null);
			if (appDomainToUnload != null)
			{
				try
				{
					UtilsContext.Current.RunWithClearContext(delegate
					{
						AppDomain.Unload(appDomainToUnload);
					});
				}
				catch (Exception ex) when (!ex.IsFatal())
				{
					this.m_tracer.TraceError("UnloadAppDomain AppDomain.Unload() failed with exception: {0}", new object[] { ex.Message });
				}
				this.m_tracer.TraceInformation("UnloadAppDomain of {0} done", new object[] { this.Identification });
				this.m_owner.OnAppDomainUnload();
			}
		}

		// Token: 0x040003C3 RID: 963
		private object m_managementLock;

		// Token: 0x040003C4 RID: 964
		private Type m_marshallerType;

		// Token: 0x040003C5 RID: 965
		private AppDomain m_appDomain;

		// Token: 0x040003C6 RID: 966
		private ITraceSource m_tracer;

		// Token: 0x040003C7 RID: 967
		private SafeWarmedUpObjectManager m_owner;

		// Token: 0x040003C8 RID: 968
		private int m_createdHandlesCount;

		// Token: 0x040003C9 RID: 969
		private int m_refCount;

		// Token: 0x040003CA RID: 970
		private readonly int m_maxSafeCalls;

		// Token: 0x040003CB RID: 971
		private readonly long m_id;
	}
}
