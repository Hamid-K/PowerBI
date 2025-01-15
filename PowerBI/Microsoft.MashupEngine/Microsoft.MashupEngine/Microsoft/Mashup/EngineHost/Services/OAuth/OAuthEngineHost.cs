using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services.OAuth
{
	// Token: 0x02001B59 RID: 7001
	internal static class OAuthEngineHost
	{
		// Token: 0x17002BF5 RID: 11253
		// (get) Token: 0x0600AF4B RID: 44875 RVA: 0x0023E6A8 File Offset: 0x0023C8A8
		// (set) Token: 0x0600AF4C RID: 44876 RVA: 0x0023E6AF File Offset: 0x0023C8AF
		internal static IHttpUriRewritingService HttpUriRewritingService { get; set; }

		// Token: 0x0600AF4D RID: 44877 RVA: 0x0023E6B8 File Offset: 0x0023C8B8
		public static IEngineHost Create(IEngine engine, Guid? activityId = null, string correlationId = null)
		{
			OAuthEngineHost.ResourcePermissionService resourcePermissionService = new OAuthEngineHost.ResourcePermissionService();
			PersistentCache persistentCache = new NullPersistentCache();
			IObjectCache objectCache = new NullObjectCache();
			ICacheSet cacheSet = new CacheSet
			{
				ObjectCache = objectCache,
				PersistentCache = persistentCache,
				PersistentObjectCache = new PersistentObjectCache(objectCache, persistentCache)
			};
			ICacheSets cacheSets = new CacheSets
			{
				Metadata = cacheSet,
				Data = cacheSet
			};
			MutableEngineHost mutableEngineHost = new MutableEngineHost();
			mutableEngineHost.Add(new SimpleEngineHost<IEngine>(engine));
			mutableEngineHost.Add(new SimpleEngineHost<ITracingService>(EvaluatorTracing.Service));
			mutableEngineHost.Add(new SimpleEngineHost<IEvaluationConstants>(new EvaluationConstants(activityId ?? Guid.NewGuid(), correlationId ?? string.Empty, null).AddTraceConstant("HostProcessId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture), false)));
			mutableEngineHost.Add(new SimpleEngineHost<ICultureService>(new CultureService(mutableEngineHost, CultureInfo.CurrentCulture.Name)));
			mutableEngineHost.Add(new SimpleEngineHost<ICurrentTimeService>(new CurrentTimeService(null)));
			mutableEngineHost.Add(new SimpleEngineHost<ITimeZoneService>(MinimalEngineHost.LocalTimeZoneService));
			mutableEngineHost.Add(new SimpleEngineHost<IResourcePermissionService>(resourcePermissionService));
			mutableEngineHost.Add(new SimpleEngineHost<IExtensibilityService>(resourcePermissionService));
			mutableEngineHost.Add(new SimpleEngineHost<IResourcePathService>(new ResourcePathService()));
			mutableEngineHost.Add(new SimpleEngineHost<ICacheSets>(cacheSets));
			mutableEngineHost.Add(new SimpleEngineHost<IProgressService>(new OAuthEngineHost.ProgressService()));
			mutableEngineHost.Add(new SimpleEngineHost<ILibraryService>(LibraryService.GetInstance()));
			mutableEngineHost.Add(new SimpleEngineHost<IConfigurationPropertyService>(new ConfigurationPropertyService(null)));
			mutableEngineHost.Add(new SimpleEngineHost<IHttpUriRewritingService>(OAuthEngineHost.HttpUriRewritingService));
			return mutableEngineHost;
		}

		// Token: 0x02001B5A RID: 7002
		private class ResourcePermissionService : IResourcePermissionService, IExtensibilityService
		{
			// Token: 0x0600AF4E RID: 44878 RVA: 0x0023E841 File Offset: 0x0023CA41
			public bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
			{
				if (resource.Kind == "Web")
				{
					credentials = new ResourceCredentialCollection(resource, Array.Empty<IResourceCredential>());
					return true;
				}
				credentials = null;
				return false;
			}

			// Token: 0x17002BF6 RID: 11254
			// (get) Token: 0x0600AF4F RID: 44879 RVA: 0x000020FA File Offset: 0x000002FA
			public IResource CurrentResource
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BF7 RID: 11255
			// (get) Token: 0x0600AF50 RID: 44880 RVA: 0x000020FA File Offset: 0x000002FA
			public ResourceCredentialCollection CurrentCredentials
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BF8 RID: 11256
			// (get) Token: 0x0600AF51 RID: 44881 RVA: 0x00002139 File Offset: 0x00000339
			public bool ImpersonateResource
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600AF52 RID: 44882 RVA: 0x0000336E File Offset: 0x0000156E
			public void RefreshCredential(bool forceRefresh)
			{
			}
		}

		// Token: 0x02001B5B RID: 7003
		private class ProgressService : IProgressService
		{
			// Token: 0x0600AF54 RID: 44884 RVA: 0x0023E868 File Offset: 0x0023CA68
			public IHostProgress GetHostProgress(string progressType, string dataSource)
			{
				return new OAuthEngineHost.ProgressService.Progress();
			}

			// Token: 0x02001B5C RID: 7004
			internal class Progress : IHostProgress
			{
				// Token: 0x0600AF56 RID: 44886 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordBytesRead(long bytesRead)
				{
				}

				// Token: 0x0600AF57 RID: 44887 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordBytesWritten(long bytesWritten)
				{
				}

				// Token: 0x0600AF58 RID: 44888 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordRowsRead(long rowsRead)
				{
				}

				// Token: 0x0600AF59 RID: 44889 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordRowsWritten(long rowsWritten)
				{
				}

				// Token: 0x0600AF5A RID: 44890 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordRowRead()
				{
				}

				// Token: 0x0600AF5B RID: 44891 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordRowWritten()
				{
				}

				// Token: 0x0600AF5C RID: 44892 RVA: 0x0000336E File Offset: 0x0000156E
				public void StartRequest()
				{
				}

				// Token: 0x0600AF5D RID: 44893 RVA: 0x0000336E File Offset: 0x0000156E
				public void StopRequest()
				{
				}

				// Token: 0x0600AF5E RID: 44894 RVA: 0x0000336E File Offset: 0x0000156E
				public void RecordPercentComplete(int percent)
				{
				}
			}
		}
	}
}
