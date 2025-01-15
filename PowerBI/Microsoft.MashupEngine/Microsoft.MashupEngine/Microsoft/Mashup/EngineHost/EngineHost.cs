using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DocumentHost;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.EngineHost.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001964 RID: 6500
	public static class EngineHost
	{
		// Token: 0x0600A506 RID: 42246 RVA: 0x0022208F File Offset: 0x0022028F
		static EngineHost()
		{
			RemoteServiceFactories.Factories = EngineHost.Factories.ToArray<IRemoteServiceFactory>();
		}

		// Token: 0x0600A507 RID: 42247 RVA: 0x002220AC File Offset: 0x002202AC
		public static IMutableEvaluationSession CreateSession(EvaluationSettings settings, IEngine engine)
		{
			EngineHost.EvaluationSession evaluationSession = new EngineHost.EvaluationSession();
			if (settings.Session != null)
			{
				evaluationSession.EngineHost.Add(new SimpleEngineHost<ISessionService>(new SessionService(settings.Session)));
			}
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IEngine>(engine));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IThreadPoolService>(new ThreadPoolService()));
			CultureService cultureService = new CultureService(evaluationSession.EngineHost, settings.DefaultCulture ?? CultureInfo.CurrentCulture.Name);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ICultureService>(cultureService));
			IEvaluationConstants evaluationConstants = new EvaluationConstants(settings.ActivityId ?? Guid.NewGuid(), settings.CorrelationId, null).AddTraceConstant("HostProcessId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture), false);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IEvaluationConstants>(evaluationConstants));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ICurrentTimeService>(new CurrentTimeService(settings.UtcNow)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITimeZoneService>(MinimalEngineHost.LocalTimeZoneService));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IUniqueIdService>(new UniqueIdService()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IPackageSectionConfigValidator>(PackageSectionConfigValidator.Instance));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IDocumentValidator>(new DocumentValidator(PackageSectionConfigValidator.Instance)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ICredentialService>(new CredentialService(new CredentialManager(evaluationSession.EngineHost, settings.Credentials, settings.AllowAutomaticCredentials, settings.AllowWindowsAuthentication, settings.ThreadIdentity))));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IResourcePermissionService>(new ResourcePermissionService(evaluationSession.EngineHost)));
			IQueryPermissionService queryPermissionService = new QueryPermissionService(new QueryPermissionManager(settings.QueryPermissions ?? new QueryPermission[0]));
			if (settings.AllowNativeQueries)
			{
				queryPermissionService = new AllowNativeQueryPermissionService(queryPermissionService);
			}
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IQueryPermissionService>(queryPermissionService));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IResourcePathService>(new ResourcePathService()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IRedirectPolicyService>(new RedirectPolicyService(settings.LegacyRedirects)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IActionPermissionService>(new ActionPermissionService(settings.AllowActions)));
			IFirewallRuleService firewallRuleService = new FirewallRuleService(new FirewallRuleManager(settings.FirewallRules ?? new FirewallRule[0]));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IFirewallRuleService>(firewallRuleService));
			if (settings.ConnectionGovernanceManager != null)
			{
				evaluationSession.EngineHost.Add(new SimpleEngineHost<IConnectionGovernanceService>(ConnectionGovernanceService.New(settings.ConnectionGovernanceManager)));
			}
			TempPageService tempPageService = new TempPageService();
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITempPageService>(tempPageService));
			evaluationSession.AddDisposable(tempPageService);
			int num = 0;
			object obj;
			if (settings.ConfigurationProperties != null && settings.ConfigurationProperties.TryGetValue("PersistentCache.ImplementationVersion", out obj) && obj is int)
			{
				num = (int)obj;
			}
			PersistentCacheConfig persistentCacheConfig = EngineHost.CreateCacheConfig(settings.CachePath, settings.Session, settings.DataCache, settings.MaxWorkingSetInMB, num);
			PersistentCacheConfig persistentCacheConfig2 = null;
			if (settings.MetadataCache != null)
			{
				persistentCacheConfig = persistentCacheConfig.Qualify("Data");
				persistentCacheConfig2 = EngineHost.CreateCacheConfig(settings.CachePath, settings.Session, settings.MetadataCache, settings.MaxWorkingSetInMB, num).Qualify("Metadata");
			}
			string text = (string.IsNullOrEmpty(settings.Session) ? null : PathMethods.ConvertToFilename(settings.Session));
			ITempDirectoryConfig tempDirectoryConfig = new TempDirectoryConfig((text == null) ? settings.TempPath : Path.Combine(settings.TempPath, text), settings.MaxTempSize);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITempDirectoryConfig>(tempDirectoryConfig));
			if (!string.IsNullOrEmpty(settings.Session))
			{
				HashSet<string> hashSet = new HashSet<string>();
				HashSet<string> hashSet2 = new HashSet<string>();
				if (persistentCacheConfig2 != null && (persistentCacheConfig2.Mode & PersistentCacheMode.Disk) != (PersistentCacheMode)0)
				{
					hashSet.Add(settings.CachePath);
					hashSet2.Add(persistentCacheConfig2.Directory);
				}
				if ((persistentCacheConfig.Mode & PersistentCacheMode.Disk) != (PersistentCacheMode)0)
				{
					hashSet.Add(settings.CachePath);
					hashSet2.Add(persistentCacheConfig.Directory);
				}
				hashSet.Add(settings.TempPath);
				hashSet2.Add(tempDirectoryConfig.TempDirectoryPath);
				EvaluationDirectoryManager evaluationDirectoryManager = new EvaluationDirectoryManager(hashSet.ToArray<string>(), hashSet2.ToArray<string>(), evaluationConstants);
				evaluationSession.AddDisposable(evaluationDirectoryManager);
			}
			CompoundCacheConfig compoundCacheConfig = null;
			CacheSet cacheSet = null;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("EngineHost/CreateSession/CacheManager", evaluationSession.EngineHost, TraceEventType.Information, null))
			{
				bool flag = !string.IsNullOrEmpty(settings.NamedMetadataCache);
				bool flag2 = CacheGroupManager.IsInitialized();
				hostTrace.Add("Initialized", flag2, false);
				hostTrace.Add("NamedMetadataCache", flag, false);
				hostTrace.Add("CacheGroup", settings.CacheGroup, false);
				if (flag2 && !string.IsNullOrEmpty(settings.CacheGroup))
				{
					CacheManager cacheManager = CacheGroupManager.GetOrCreateCacheGroupManager(settings.CacheGroup).CreateCacheManager(evaluationSession.EngineHost, engine);
					evaluationSession.EngineHost.Add(new SimpleEngineHost<ICacheManager>(cacheManager));
					evaluationSession.AddDisposable(cacheManager);
					if (flag)
					{
						if (!cacheManager.TryCreateCache(settings.NamedMetadataCache, out compoundCacheConfig, out cacheSet))
						{
							cacheSet = null;
						}
						hostTrace.Add("Status", (cacheSet != null) ? "Found" : "NotFound", false);
					}
				}
				else if (flag)
				{
					hostTrace.Add("Status", "NotUsed", false);
				}
			}
			CacheSet cacheSet2 = EngineHost.CreateCacheSet(PersistentCachePolicy.CacheKind.Data, persistentCacheConfig, evaluationConstants, tempPageService, out persistentCacheConfig);
			if (cacheSet == null && persistentCacheConfig2 != null)
			{
				cacheSet = EngineHost.CreateCacheSet(PersistentCachePolicy.CacheKind.Metadata, persistentCacheConfig2, evaluationConstants, tempPageService, out persistentCacheConfig2);
			}
			cacheSet = cacheSet ?? cacheSet2;
			if (compoundCacheConfig == null && persistentCacheConfig2 != null)
			{
				compoundCacheConfig = CompoundCacheConfig.New(persistentCacheConfig2.Directory, persistentCacheConfig2);
			}
			CacheSetsConfig cacheSetsConfig = new CacheSetsConfig
			{
				Metadata = compoundCacheConfig,
				Data = CompoundCacheConfig.New(persistentCacheConfig.Directory, persistentCacheConfig)
			};
			evaluationSession.EngineHost.Add(new SimpleEngineHost<CacheSetsConfig>(cacheSetsConfig));
			CacheSets cacheSets = new CacheSets
			{
				Metadata = cacheSet,
				Data = cacheSet2
			};
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ICacheSets>(cacheSets));
			evaluationSession.AddDisposable(cacheSets);
			EvaluationTempDirectory evaluationTempDirectory = new EvaluationTempDirectory(tempDirectoryConfig, evaluationConstants);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITempDirectoryService>(evaluationTempDirectory));
			ValueBufferService valueBufferService = new ValueBufferService(evaluationSession.EngineHost, engine);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IValueBufferingService>(valueBufferService));
			IFirewallPlanCreator firewallPlanCreator = new FirewallPlanCreator();
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IFirewallPlanCreator>(firewallPlanCreator));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IFirewallPlanMinimizer>(new FirewallPlanMinimizer(firewallPlanCreator)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IPartitionedDocumentDisplayNameService>(new PartitionedDocumentDisplayNameService()));
			IEmbeddedValueLoggingService embeddedValueLoggingService = new EmbeddedValueLoggingService(evaluationSession);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IEmbeddedValueLoggingService>(embeddedValueLoggingService));
			ManyProgressReader manyProgressReader = new ManyProgressReader();
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IProgressReader>(manyProgressReader));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IManyProgressReader>(manyProgressReader));
			ProgressRecorder progressRecorder = new ProgressRecorder();
			manyProgressReader.AddReader(progressRecorder);
			PartitionProgressService partitionProgressService = new PartitionProgressService(progressRecorder);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IPartitionProgressService>(partitionProgressService));
			evaluationSession.AddDisposable(partitionProgressService);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IPartitionStackFrameExtendedInfo>(new PartitionStackFrameExtendedInfo()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IPartitionedDocumentSourceErrorExceptionService>(new PartitionedDocumentSourceErrorExceptionService(engine)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IVariableService>(new VariableService()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ISamplingService>(new HostSamplingService(delegate
			{
			})));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IFeatureLoggingService>(new FeatureLoggingService()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ILibraryService>(LibraryService.GetInstance()));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IApplicationConfigurationService>(new ApplicationConfigurationService()));
			LifetimeService lifetimeService = new LifetimeService();
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ILifetimeService>(lifetimeService));
			evaluationSession.AddDisposable(lifetimeService);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ICancellationService>(new CancellationService()));
			FoldingFailureService foldingFailureService = new FoldingFailureService(settings.ThrowOnFoldingFailure, settings.ThrowOnVolatileFunctions);
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IFoldingFailureService>(foldingFailureService));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IKnownExceptionService>(KnownExceptionService.Instance));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<IConfigurationPropertyService>(new ConfigurationPropertyService(settings.ConfigurationProperties)));
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITraitTrackingService>(new TraitTrackingService(engine)));
			if (settings.TracingOptions != null && settings.TracingOptions.Length != 0)
			{
				evaluationSession.EngineHost.Add(new SimpleEngineHost<ITracingOptions>(new EngineHost.TracingOptions(settings.TracingOptions)));
			}
			evaluationSession.EngineHost.Add(new SimpleEngineHost<ITraitPrivacyService>(new TraitPrivacyService()));
			MipService.TryAddService(evaluationSession.EngineHost, evaluationSession.EngineHost);
			if (settings.GCBetweenEvaluations)
			{
				evaluationSession.EngineHost.Add(new SimpleEngineHost<GarbageCollectionService>(new GarbageCollectionService(evaluationConstants)));
			}
			return evaluationSession;
		}

		// Token: 0x0600A508 RID: 42248 RVA: 0x0022297C File Offset: 0x00220B7C
		private static PersistentCacheConfig CreateCacheConfig(string cachePath, string session, CacheSettings settings, int maxWorkingSetInMB, int persistentCacheVersion)
		{
			string text;
			bool flag;
			if (!string.IsNullOrEmpty(settings.Identity))
			{
				text = Path.Combine(cachePath, PathMethods.ConvertToFilename(settings.Identity));
				flag = false;
			}
			else if (!string.IsNullOrEmpty(session))
			{
				text = Path.Combine(cachePath, PathMethods.ConvertToFilename(session));
				flag = false;
			}
			else
			{
				text = cachePath;
				flag = true;
			}
			long maxCacheSize = settings.MaxCacheSize;
			long num = maxCacheSize / 2L;
			int num2;
			if (maxWorkingSetInMB != -1)
			{
				num2 = (int)((double)maxWorkingSetInMB * 0.05 * 1024.0 * 1024.0);
			}
			else
			{
				num2 = (int)Math.Min(1048576L, maxCacheSize);
			}
			int num3 = num2 / 2;
			DateTime dateTime = ((settings.CacheTTL != null) ? DateTime.UtcNow.SafeAdd(-settings.CacheTTL.Value) : DateTime.MinValue);
			CacheVersion cacheVersion = null;
			return new PersistentCacheConfig(settings.CacheMode, text, maxCacheSize, num, settings.MaxCacheEntrySize.GetValueOrDefault(maxCacheSize), num2, num3, settings.RefreshData, flag, true, dateTime, cacheVersion, settings.CacheEncryptionCertificateThumbprint, persistentCacheVersion);
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x00222A84 File Offset: 0x00220C84
		private static CacheSet CreateCacheSet(PersistentCachePolicy.CacheKind kind, PersistentCacheConfig cacheConfig, IEvaluationConstants evaluationConstants, ITempPageService tempPageService, out PersistentCacheConfig newCacheConfig)
		{
			IClearableObjectCache clearableObjectCache = new ClearableObjectCache(PersistentCachePolicy.CreateObjectCache(kind, cacheConfig, evaluationConstants));
			CacheVersion cacheVersion;
			ClearablePersistentCache clearablePersistentCache = new ClearablePersistentCache(PersistentCachePolicy.CreatePersistentCache(kind, cacheConfig, evaluationConstants, tempPageService, out cacheVersion));
			IPersistentObjectCache persistentObjectCache = new PersistentObjectCache(clearableObjectCache, clearablePersistentCache);
			newCacheConfig = cacheConfig.ReplaceMinVersion(cacheVersion);
			return new CacheSet
			{
				ObjectCache = clearableObjectCache,
				PersistentCache = clearablePersistentCache,
				PersistentObjectCache = persistentObjectCache
			};
		}

		// Token: 0x040055F2 RID: 22002
		private const double ObjectCachePercentOfMaxWorkingSet = 0.05;

		// Token: 0x040055F3 RID: 22003
		public static readonly IEnumerable<IRemoteServiceFactory> Factories = DefaultRemoteServiceFactories.Factories;

		// Token: 0x02001965 RID: 6501
		private sealed class EvaluationSession : IMutableEvaluationSession, IEvaluationSession, IDisposable, ICredentialService, IEngineHost
		{
			// Token: 0x0600A50A RID: 42250 RVA: 0x00222ADC File Offset: 0x00220CDC
			public EvaluationSession()
			{
				this.identity = Guid.NewGuid().ToString();
				this.disposables = new List<IDisposable>();
				this.engineHost = new MutableEngineHost();
			}

			// Token: 0x17002A22 RID: 10786
			// (get) Token: 0x0600A50B RID: 42251 RVA: 0x00222B1E File Offset: 0x00220D1E
			public string Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x17002A23 RID: 10787
			// (get) Token: 0x0600A50C RID: 42252 RVA: 0x00222B26 File Offset: 0x00220D26
			public MutableEngineHost EngineHost
			{
				get
				{
					return this.engineHost;
				}
			}

			// Token: 0x17002A24 RID: 10788
			// (get) Token: 0x0600A50D RID: 42253 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			IEngineHost IEvaluationSession.EngineHost
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002A25 RID: 10789
			// (get) Token: 0x0600A50E RID: 42254 RVA: 0x00222B2E File Offset: 0x00220D2E
			public IProgressReader ProgressReader
			{
				get
				{
					return this.engineHost.QueryService<IProgressReader>();
				}
			}

			// Token: 0x0600A50F RID: 42255 RVA: 0x00222B3B File Offset: 0x00220D3B
			public void AddDisposable(IDisposable disposable)
			{
				this.disposables.Add(disposable);
			}

			// Token: 0x0600A510 RID: 42256 RVA: 0x00222B49 File Offset: 0x00220D49
			public ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
			{
				return this.CredentialService.RefreshCredential(resource, forceRefresh);
			}

			// Token: 0x0600A511 RID: 42257 RVA: 0x00222B58 File Offset: 0x00220D58
			public void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection updatedCredential)
			{
				this.CredentialService.UpdateExchangeCredential(resource, updatedCredential);
			}

			// Token: 0x0600A512 RID: 42258 RVA: 0x00222B67 File Offset: 0x00220D67
			public bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
			{
				return this.CredentialService.TryGetCredentials(resource, out credentials);
			}

			// Token: 0x0600A513 RID: 42259 RVA: 0x00222B76 File Offset: 0x00220D76
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(ICredentialService))
				{
					return (T)((object)this);
				}
				return this.engineHost.QueryService<T>();
			}

			// Token: 0x0600A514 RID: 42260 RVA: 0x00222BA8 File Offset: 0x00220DA8
			public void Dispose()
			{
				for (int i = this.disposables.Count - 1; i >= 0; i--)
				{
					this.disposables[i].Dispose();
				}
				this.disposables.Clear();
			}

			// Token: 0x17002A26 RID: 10790
			// (get) Token: 0x0600A515 RID: 42261 RVA: 0x00222BE9 File Offset: 0x00220DE9
			private ICredentialService CredentialService
			{
				get
				{
					return this.engineHost.QueryService<ICredentialService>();
				}
			}

			// Token: 0x040055F4 RID: 22004
			private readonly string identity;

			// Token: 0x040055F5 RID: 22005
			private readonly List<IDisposable> disposables;

			// Token: 0x040055F6 RID: 22006
			private readonly MutableEngineHost engineHost;
		}

		// Token: 0x02001966 RID: 6502
		private sealed class TracingOptions : ITracingOptions
		{
			// Token: 0x0600A516 RID: 42262 RVA: 0x00222BF6 File Offset: 0x00220DF6
			public TracingOptions(string[] options)
			{
				this.options = options;
			}

			// Token: 0x17002A27 RID: 10791
			// (get) Token: 0x0600A517 RID: 42263 RVA: 0x00222C05 File Offset: 0x00220E05
			public IEnumerable<string> Keys
			{
				get
				{
					return this.options;
				}
			}

			// Token: 0x0600A518 RID: 42264 RVA: 0x00222C0D File Offset: 0x00220E0D
			public bool IsEnabled(string key)
			{
				return this.options.Contains(key);
			}

			// Token: 0x040055F7 RID: 22007
			private readonly string[] options;
		}
	}
}
