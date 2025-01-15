using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000058 RID: 88
	internal sealed class DomainModelManager : IDomainModelManager, IDisposable
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000863C File Offset: 0x0000683C
		internal DomainModelManager(Lazy<INaturalLanguageServicesFactory> serviceFactory, Func<string> getConceptualSchemaXml, Func<string> getLinguisticSchemaJson, IBulkMeasureExpressionProvider measureExpressionProvider, IDataIndexBuilder indexBuilder, IDataIndexDisposer indexDisposer, LuciaSessionOptions options, IDataIndexStore indexStore = null, IColumnStatisticsDiscovererWrapper columnStatisticsDiscoverer = null, bool awaitOnStoreAndDelete = false, TimeSpan? updateDatabaseContextDelay = null, TimeSpan? buildNewIndexDelay = null, bool skipIndexStoreUpdate = false, IFeatureSwitchProvider featureSwitchProvider = null)
		{
			this.m_serviceFactory = serviceFactory;
			this.m_getConceptualSchemaXml = getConceptualSchemaXml;
			this.m_getLinguisticSchemaJson = getLinguisticSchemaJson;
			this.m_measureExpressionProvider = measureExpressionProvider;
			this.m_indexBuilder = indexBuilder;
			this.m_indexDisposer = indexDisposer;
			this.m_luciaSessionOptions = options;
			this.m_indexStore = indexStore;
			this.m_columnStatisticsDiscoverer = columnStatisticsDiscoverer;
			this.m_skipIndexStoreUpdate = skipIndexStoreUpdate;
			this.m_featureSwitchProvider = featureSwitchProvider ?? FeatureSwitchProvider.Empty;
			this.m_databaseContextManager = new DisposableResourceManager<SelfDeletingDatabaseContext>(this.CreateNewDatabaseContext(null, false, true, null));
			this.m_indexManager = new DisposableResourceManager<DomainModelManager.DataIndexWithKey>(this.CreateEmptyDataIndex());
			this.m_awaitOnStoreAndDelete = awaitOnStoreAndDelete;
			this.m_updateDatabaseContextDelay = updateDatabaseContextDelay;
			this.m_buildNewIndexDelay = buildNewIndexDelay;
			this.Status = DomainModelManagerStatus.Uninitialized;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008709 File Offset: 0x00006909
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00008716 File Offset: 0x00006916
		public DomainModelManagerStatus Status
		{
			get
			{
				return (DomainModelManagerStatus)Volatile.Read(ref this.m_status);
			}
			private set
			{
				this.m_status = (long)value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000871F File Offset: 0x0000691F
		public Exception VerifyRuntimeException
		{
			get
			{
				return this.m_verifyRuntimeException;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00008727 File Offset: 0x00006927
		private bool IsFaultedOnVerifyRuntime
		{
			get
			{
				return this.Status == DomainModelManagerStatus.FaultedOnVerifyRuntime;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008733 File Offset: 0x00006933
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000873C File Offset: 0x0000693C
		public async Task<IReference<IDatabaseContext>> GetDatabaseContextAsync()
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			return await this.m_databaseContextManager.AcquireAsync();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008780 File Offset: 0x00006980
		public async Task<IReference<IDataIndexContainer>> GetDataIndexAsync()
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			return await this.m_indexManager.AcquireAsync();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000087C3 File Offset: 0x000069C3
		public Task NotifyModelChanging()
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			if (this.IsFaultedOnVerifyRuntime)
			{
				return Task.FromResult<bool>(false);
			}
			return this.QueueUpdateModelTask(delegate(CancellationToken token)
			{
				this.UpdateFinalStatus();
				return Task.FromResult<bool>(true);
			});
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008800 File Offset: 0x00006A00
		public Task NotifyModelChanged(string filePath, DateTime lastModifiedTime, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate, Action domainModelUpdatedCallback, bool luciaSessionInUse)
		{
			DomainModelManager.<>c__DisplayClass36_0 CS$<>8__locals1 = new DomainModelManager.<>c__DisplayClass36_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.filePath = filePath;
			CS$<>8__locals1.luciaSessionInUse = luciaSessionInUse;
			CS$<>8__locals1.schemaItemsToInvalidate = schemaItemsToInvalidate;
			CS$<>8__locals1.lastModifiedTime = lastModifiedTime;
			CS$<>8__locals1.domainModelUpdatedCallback = domainModelUpdatedCallback;
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			if (this.IsFaultedOnVerifyRuntime)
			{
				return Task.FromResult<bool>(false);
			}
			return this.QueueUpdateModelTask(delegate(CancellationToken token)
			{
				DomainModelManager.<>c__DisplayClass36_0.<<NotifyModelChanged>b__0>d <<NotifyModelChanged>b__0>d;
				<<NotifyModelChanged>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<NotifyModelChanged>b__0>d.<>4__this = CS$<>8__locals1;
				<<NotifyModelChanged>b__0>d.token = token;
				<<NotifyModelChanged>b__0>d.<>1__state = -1;
				<<NotifyModelChanged>b__0>d.<>t__builder.Start<DomainModelManager.<>c__DisplayClass36_0.<<NotifyModelChanged>b__0>d>(ref <<NotifyModelChanged>b__0>d);
				return <<NotifyModelChanged>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000887A File Offset: 0x00006A7A
		public Task NotifyFileDiscarded()
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			if (this.IsFaultedOnVerifyRuntime)
			{
				return Task.CompletedTask;
			}
			return this.QueueUpdateModelTask(async delegate(CancellationToken token)
			{
				Exception ex2;
				try
				{
					IReference<DomainModelManager.DataIndexWithKey> reference = await this.m_indexManager.AcquireAsync();
					using (IReference<DomainModelManager.DataIndexWithKey> currentIndex = reference)
					{
						ExploreTracer.Instance.TraceInformation("DomainModelManager: File is not saved, trying to remove the index.");
						await this.m_indexManager.ReplaceAsync(this.CreateEmptyDataIndex());
						DataIndexCacheKey indexKey = currentIndex.Value.IndexKey;
						if (indexKey != null)
						{
							TaskAwaiter<bool> taskAwaiter = this.m_indexStore.DeleteIndexAsync(indexKey, token).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								await taskAwaiter;
								TaskAwaiter<bool> taskAwaiter2;
								taskAwaiter = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter<bool>);
							}
							if (taskAwaiter.GetResult())
							{
								ExploreTracer.Instance.TraceInformation("DomainModelManager: Index was successfully removed.");
							}
							else
							{
								ExploreTracer.Instance.TraceInformation("DomainModelManager: Failed to remove the index.");
							}
						}
					}
					IReference<DomainModelManager.DataIndexWithKey> currentIndex = null;
				}
				catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingExceptionWithUnwrap(ex, out ex2))
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex2))
					{
						ExceptionDispatchInfo.Capture(ex2).Throw();
					}
					if (ex is OperationCanceledException)
					{
						throw;
					}
					ExploreHostUtils.TraceLuciaModelChangeException(ex2, this.m_luciaSessionOptions);
				}
			});
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000088B8 File Offset: 0x00006AB8
		public Task NotifyFilePathChanged(string oldFilePath, string newFilePath)
		{
			DomainModelManager.<>c__DisplayClass38_0 CS$<>8__locals1 = new DomainModelManager.<>c__DisplayClass38_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.oldFilePath = oldFilePath;
			CS$<>8__locals1.newFilePath = newFilePath;
			if (string.IsNullOrEmpty(CS$<>8__locals1.newFilePath) || string.Equals(CS$<>8__locals1.oldFilePath, CS$<>8__locals1.newFilePath, StringComparison.OrdinalIgnoreCase))
			{
				return Task.CompletedTask;
			}
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("DomainModelManager");
			}
			if (this.IsFaultedOnVerifyRuntime)
			{
				return Task.CompletedTask;
			}
			return this.QueueUpdateModelTask(delegate(CancellationToken token)
			{
				DomainModelManager.<>c__DisplayClass38_0.<<NotifyFilePathChanged>b__0>d <<NotifyFilePathChanged>b__0>d;
				<<NotifyFilePathChanged>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<NotifyFilePathChanged>b__0>d.<>4__this = CS$<>8__locals1;
				<<NotifyFilePathChanged>b__0>d.token = token;
				<<NotifyFilePathChanged>b__0>d.<>1__state = -1;
				<<NotifyFilePathChanged>b__0>d.<>t__builder.Start<DomainModelManager.<>c__DisplayClass38_0.<<NotifyFilePathChanged>b__0>d>(ref <<NotifyFilePathChanged>b__0>d);
				return <<NotifyFilePathChanged>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008944 File Offset: 0x00006B44
		private static bool TryReplaceFilePathKey(IndexLookupKey indexLookupKey, string oldFilePath, string newFilePath, out IndexLookupKey result)
		{
			ImmutableList<DataIndexCacheKey> immutableList = indexLookupKey.Alternates;
			bool flag = false;
			if (!string.IsNullOrEmpty(oldFilePath))
			{
				immutableList = immutableList.Remove(DataIndexCacheKey.Create(oldFilePath));
				flag = immutableList.Count != indexLookupKey.Alternates.Count;
			}
			if (!string.IsNullOrEmpty(newFilePath))
			{
				DataIndexCacheKey dataIndexCacheKey = DataIndexCacheKey.Create(newFilePath);
				if (!immutableList.Contains(dataIndexCacheKey))
				{
					immutableList = immutableList.Add(dataIndexCacheKey);
					flag = true;
				}
			}
			if (flag)
			{
				result = new IndexLookupKey(indexLookupKey.Primary, immutableList);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000089BF File Offset: 0x00006BBF
		private DomainModelManager.DataIndexWithKey CreateEmptyDataIndex()
		{
			return new DomainModelManager.DataIndexWithKey(DataIndex.Empty, null, null, this.m_indexDisposer);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000089D4 File Offset: 0x00006BD4
		private async Task<bool> BuildNewIndex(IDatabaseContext databaseContext, IndexLookupKey indexKey, CancellationToken token, DataIndexCacheKey oldIndexKey = null)
		{
			DomainModelManager.<>c__DisplayClass41_0 CS$<>8__locals1 = new DomainModelManager.<>c__DisplayClass41_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.oldIndexKey = oldIndexKey;
			DataIndex dataIndex = DataIndex.Empty;
			if (indexKey != null)
			{
				dataIndex = await this.m_indexBuilder.BuildIndexAsync(databaseContext, token);
				if (dataIndex == null)
				{
					return false;
				}
			}
			await this.m_indexManager.ReplaceAsync(new DomainModelManager.DataIndexWithKey(dataIndex, (indexKey != null) ? indexKey.Primary : null, indexKey, this.m_indexDisposer));
			if (this.m_indexStore != null && !this.m_skipIndexStoreUpdate)
			{
				Task task = this.m_storeIndexTaskManager.Queue(delegate(CancellationToken storeToken)
				{
					DomainModelManager.<>c__DisplayClass41_0.<<BuildNewIndex>b__0>d <<BuildNewIndex>b__0>d;
					<<BuildNewIndex>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<BuildNewIndex>b__0>d.<>4__this = CS$<>8__locals1;
					<<BuildNewIndex>b__0>d.storeToken = storeToken;
					<<BuildNewIndex>b__0>d.<>1__state = -1;
					<<BuildNewIndex>b__0>d.<>t__builder.Start<DomainModelManager.<>c__DisplayClass41_0.<<BuildNewIndex>b__0>d>(ref <<BuildNewIndex>b__0>d);
					return <<BuildNewIndex>b__0>d.<>t__builder.Task;
				});
				Task task2 = Task.FromResult<bool>(false);
				if (CS$<>8__locals1.oldIndexKey != null)
				{
					task2 = Task.Run<bool>(() => CS$<>8__locals1.<>4__this.m_indexStore.DeleteIndexAsync(CS$<>8__locals1.oldIndexKey, CancellationToken.None));
				}
				if (this.m_awaitOnStoreAndDelete)
				{
					await Task.WhenAll(new Task[] { task, task2 });
				}
			}
			return true;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008A38 File Offset: 0x00006C38
		private ComputeDataIndexCacheKeyResult ComputeIndexKey(IDatabaseContext context, DateTime lastModifiedTime, CancellationToken token, out DataIndexCacheKey keyWithoutTags)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal) { 
			{
				"LastModifiedTimeTag",
				XmlConvert.ToString(lastModifiedTime, XmlDateTimeSerializationMode.Unspecified)
			} };
			IManagementService managementService = this.m_serviceFactory.Value.CreateManagementService(this.m_featureSwitchProvider, LinguisticSchemaServicesBuilderOptions.None);
			ComputeDataIndexCacheKeyResult computeDataIndexCacheKeyResult = managementService.ComputeDataIndexCacheKey(context, token, dictionary);
			DataIndexCacheKey dataIndexCacheKey;
			if (computeDataIndexCacheKeyResult.CacheKey != null)
			{
				ComputeDataIndexCacheKeyResult computeDataIndexCacheKeyResult2 = managementService.ComputeDataIndexCacheKey(context, token, null);
				dataIndexCacheKey = ((computeDataIndexCacheKeyResult2 != null) ? computeDataIndexCacheKeyResult2.CacheKey : null);
			}
			else
			{
				dataIndexCacheKey = null;
			}
			keyWithoutTags = dataIndexCacheKey;
			return computeDataIndexCacheKeyResult;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008AA8 File Offset: 0x00006CA8
		private SelfDeletingDatabaseContext CreateNewDatabaseContext(string filePath, bool luciaSessionInUse, bool isEmptyContext = false, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate = null)
		{
			string text = Guid.NewGuid().ToString();
			StatisticsAnnotationProviderCreator statisticsAnnotationProviderCreator;
			if (this.m_columnStatisticsDiscoverer == null)
			{
				statisticsAnnotationProviderCreator = null;
			}
			else
			{
				statisticsAnnotationProviderCreator = (IConceptualSchema schema, CancellationToken cancellationToken) => this.CreateStatisticsAnnotationProvider(schema, filePath, luciaSessionInUse, schemaItemsToInvalidate, cancellationToken);
			}
			string text2 = (isEmptyContext ? null : this.m_getConceptualSchemaXml());
			string text3;
			if (!isEmptyContext)
			{
				Func<string> getLinguisticSchemaJson = this.m_getLinguisticSchemaJson;
				text3 = ((getLinguisticSchemaJson != null) ? getLinguisticSchemaJson() : null);
			}
			else
			{
				text3 = null;
			}
			return new SelfDeletingDatabaseContext(text, text2, text3, this.m_measureExpressionProvider, statisticsAnnotationProviderCreator, this.m_serviceFactory, this.m_featureSwitchProvider);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008B45 File Offset: 0x00006D45
		private void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this.m_disposed, 1L) == 0L)
			{
				this.m_indexManager.Dispose();
				this.m_databaseContextManager.Dispose();
				this.m_updateModelTaskManager.Dispose();
				this.m_storeIndexTaskManager.Dispose();
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008B88 File Offset: 0x00006D88
		private Task QueueUpdateModelTask(Func<CancellationToken, Task> task)
		{
			Task task2;
			Exception ex2;
			try
			{
				task2 = this.m_updateModelTaskManager.Queue(task);
			}
			catch (TaskSchedulerException ex) when (!AsynchronousExceptionDetection.IsStoppingExceptionWithUnwrap(ex, out ex2))
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				ExploreHostUtils.TraceLuciaModelChangeException(ex2, this.m_luciaSessionOptions);
				this.UpdateFinalStatus();
				task2 = Task.FromResult<bool>(false);
			}
			return task2;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008C00 File Offset: 0x00006E00
		private void UpdateFinalStatus()
		{
			if (this.Status == DomainModelManagerStatus.UpdatingDataIndex)
			{
				this.Status = DomainModelManagerStatus.FailedToUpdateDataIndex;
				return;
			}
			if (this.Status == DomainModelManagerStatus.UpdatingDatabaseContext)
			{
				this.Status = DomainModelManagerStatus.FailedToUpdateDatabaseContext;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008C28 File Offset: 0x00006E28
		private IStatisticsAnnotationProvider CreateStatisticsAnnotationProvider(IConceptualSchema schema, string filePath, bool luciaSessionInUse, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate, CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			IStatisticsAnnotationProvider statisticsAnnotationProvider;
			try
			{
				IStatisticsAnnotationProvider result = this.m_columnStatisticsDiscoverer.DiscoverAsync(schema, filePath, luciaSessionInUse, DateTime.UtcNow, DomainModelManager.ColumnStatisticsRefreshInterval, cancellationToken, schemaItemsToInvalidate).Result;
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("DiscoverAsync", "Success", null, stopwatch.Elapsed.ToString()));
				statisticsAnnotationProvider = result;
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("DiscoverAsync", "Failed", ex.ToTraceString(), stopwatch.Elapsed.ToString()));
				throw;
			}
			return statisticsAnnotationProvider;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private static bool IsFatal(ComputeDataIndexCacheKeyWarnings warning)
		{
			return warning != ComputeDataIndexCacheKeyWarnings.None && warning != ComputeDataIndexCacheKeyWarnings.EmptyLinguisticSchema;
		}

		// Token: 0x0400010D RID: 269
		private const string LastModifiedTimeTag = "LastModifiedTimeTag";

		// Token: 0x0400010E RID: 270
		internal static readonly TimeSpan ColumnStatisticsRefreshInterval = TimeSpan.FromDays(7.0);

		// Token: 0x0400010F RID: 271
		private readonly SingleRunningTaskManager m_updateModelTaskManager = new SingleRunningTaskManager();

		// Token: 0x04000110 RID: 272
		private readonly SingleRunningTaskManager m_storeIndexTaskManager = new SingleRunningTaskManager();

		// Token: 0x04000111 RID: 273
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x04000112 RID: 274
		private readonly Func<string> m_getConceptualSchemaXml;

		// Token: 0x04000113 RID: 275
		private readonly Func<string> m_getLinguisticSchemaJson;

		// Token: 0x04000114 RID: 276
		private readonly IBulkMeasureExpressionProvider m_measureExpressionProvider;

		// Token: 0x04000115 RID: 277
		private readonly IDataIndexBuilder m_indexBuilder;

		// Token: 0x04000116 RID: 278
		private readonly IDataIndexDisposer m_indexDisposer;

		// Token: 0x04000117 RID: 279
		private readonly IDataIndexStore m_indexStore;

		// Token: 0x04000118 RID: 280
		private readonly IColumnStatisticsDiscovererWrapper m_columnStatisticsDiscoverer;

		// Token: 0x04000119 RID: 281
		private readonly bool m_skipIndexStoreUpdate;

		// Token: 0x0400011A RID: 282
		private readonly DisposableResourceManager<SelfDeletingDatabaseContext> m_databaseContextManager;

		// Token: 0x0400011B RID: 283
		private readonly DisposableResourceManager<DomainModelManager.DataIndexWithKey> m_indexManager;

		// Token: 0x0400011C RID: 284
		private readonly bool m_awaitOnStoreAndDelete;

		// Token: 0x0400011D RID: 285
		private readonly TimeSpan? m_updateDatabaseContextDelay;

		// Token: 0x0400011E RID: 286
		private readonly TimeSpan? m_buildNewIndexDelay;

		// Token: 0x0400011F RID: 287
		private readonly LuciaSessionOptions m_luciaSessionOptions;

		// Token: 0x04000120 RID: 288
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x04000121 RID: 289
		private bool m_storeChecked;

		// Token: 0x04000122 RID: 290
		private long m_disposed;

		// Token: 0x04000123 RID: 291
		private long m_status;

		// Token: 0x04000124 RID: 292
		private Exception m_verifyRuntimeException;

		// Token: 0x020000B3 RID: 179
		internal sealed class DataIndexWithKey : IDataIndexContainer, IDisposable
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000D347 File Offset: 0x0000B547
			internal DataIndexWithKey(DataIndex index, DataIndexCacheKey indexKey, IndexLookupKey lookupKey, IDataIndexDisposer indexDisposer)
			{
				this.Index = index;
				this.IndexKey = indexKey;
				this.LookupKey = lookupKey;
				this.m_indexDisposer = indexDisposer;
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000D36C File Offset: 0x0000B56C
			public DataIndex Index { get; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D374 File Offset: 0x0000B574
			internal DataIndexCacheKey IndexKey { get; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D37C File Offset: 0x0000B57C
			internal IndexLookupKey LookupKey { get; }

			// Token: 0x0600040F RID: 1039 RVA: 0x0000D384 File Offset: 0x0000B584
			internal void SuppressDispose()
			{
				this._suppressDispose = true;
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000D38D File Offset: 0x0000B58D
			public void Dispose()
			{
				if (!this._suppressDispose)
				{
					this.m_indexDisposer.Dispose(this.Index);
				}
			}

			// Token: 0x0400024E RID: 590
			private readonly IDataIndexDisposer m_indexDisposer;

			// Token: 0x0400024F RID: 591
			private bool _suppressDispose;
		}
	}
}
