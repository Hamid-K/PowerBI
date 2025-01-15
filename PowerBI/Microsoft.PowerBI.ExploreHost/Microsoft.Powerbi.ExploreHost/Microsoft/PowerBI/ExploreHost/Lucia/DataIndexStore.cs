using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.Packaging;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000051 RID: 81
	internal class DataIndexStore : IDataIndexStore
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00007E14 File Offset: 0x00006014
		internal DataIndexStore(IStreamBasedStorage storage, Lazy<INaturalLanguageServicesFactory> serviceFactory, string workingDirectoryRoot, LuciaSessionOptions luciaSessionOptions, Version dataIndexVersion, bool doSynchronousDelete = false)
			: this(storage, luciaSessionOptions, new DataIndexReader(serviceFactory, workingDirectoryRoot, dataIndexVersion), doSynchronousDelete)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007E2A File Offset: 0x0000602A
		internal DataIndexStore(IStreamBasedStorage storage, LuciaSessionOptions luciaSessionOptions, IDataIndexReader dataIndexReader, bool doSynchronousDelete = false)
		{
			this.m_storage = storage;
			this.m_luciaSessionOptions = luciaSessionOptions;
			this.m_dataIndexReader = dataIndexReader;
			this.m_doSynchronousDelete = doSynchronousDelete;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007E50 File Offset: 0x00006050
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Index", "IndexKey", "MatchedKey" })]
		public Task<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>> GetIndexAsync(IndexLookupKey key, IDatabaseContext databaseContext, CancellationToken token)
		{
			DataIndexStoreTelemetry dataIndexStoreTelemetry = new DataIndexStoreTelemetry(DataIndexStoreActionId.GetIndex, this.m_luciaSessionOptions);
			Stopwatch stopwatch = Stopwatch.StartNew();
			string text = "NotFound";
			Task<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>> task;
			try
			{
				DataIndexCacheKey dataIndexCacheKey = key.Primary;
				DataIndexCacheKey dataIndexCacheKey2 = key.Primary;
				Stream stream = this.OpenExistingStream(dataIndexCacheKey.ToString(), databaseContext, token);
				if (stream == null)
				{
					ExploreTracer.Instance.TraceInformation("Can't find the stream for the primary key. Trying to look up for alternate keys instead.");
					using (ImmutableList<DataIndexCacheKey>.Enumerator enumerator = key.Alternates.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DataIndexCacheKey dataIndexCacheKey3 = enumerator.Current;
							DataIndexCacheKey dataIndexCacheKey4;
							if (this.TryGetMappedPrimaryKey(dataIndexCacheKey3, databaseContext, token, out dataIndexCacheKey4))
							{
								stream = this.OpenExistingStream(dataIndexCacheKey4.ToString(), databaseContext, token);
								if (stream != null)
								{
									ExploreTracer.Instance.TraceInformation(string.Format("Found a stream for the alternate key '{0}'", dataIndexCacheKey3));
									dataIndexCacheKey = dataIndexCacheKey4;
									dataIndexCacheKey2 = dataIndexCacheKey3;
									if (dataIndexCacheKey3.ToString().Contains("{"))
									{
										text = "FoundUsingSecondaryKey";
										break;
									}
									text = "FoundUsingFilePath";
									break;
								}
							}
						}
						goto IL_00E8;
					}
				}
				text = "PrimaryKey";
				IL_00E8:
				if (stream != null)
				{
					using (stream)
					{
						try
						{
							OpenDataIndexResult openDataIndexResult;
							if (this.m_dataIndexReader.TryRead(stream, databaseContext, token, out openDataIndexResult))
							{
								DataIndexTelemetry dataIndexTelemetry = dataIndexStoreTelemetry;
								DataIndexStatistics dataIndexStatistics;
								if (openDataIndexResult == null)
								{
									dataIndexStatistics = null;
								}
								else
								{
									DataIndex dataIndex = openDataIndexResult.DataIndex;
									if (dataIndex == null)
									{
										dataIndexStatistics = null;
									}
									else
									{
										DataIndexMetadata metadata = dataIndex.Metadata;
										dataIndexStatistics = ((metadata != null) ? metadata.Statistics : null);
									}
								}
								dataIndexTelemetry.Statistics = dataIndexStatistics;
								dataIndexStoreTelemetry.Warnings = openDataIndexResult.Warnings;
								if (openDataIndexResult.Warnings == OpenDataIndexWarnings.None)
								{
									return Task.FromResult<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>>(new global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>(openDataIndexResult.DataIndex, dataIndexCacheKey, dataIndexCacheKey2));
								}
							}
						}
						catch (DataIndexException)
						{
							this.EnsureAwaitingDelete(dataIndexCacheKey, token);
							throw;
						}
					}
				}
				dataIndexStoreTelemetry.Status = DataIndexOperationStatus.Failed;
				task = Task.FromResult<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>>(new global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>(null, null, null));
			}
			catch (Exception ex)
			{
				string text2;
				Exception ex2;
				DataIndexStore.GetExceptionDetails(ex, out text2, out ex2);
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				dataIndexStoreTelemetry.AddExceptionDetails(text2, ex2);
				task = Task.FromResult<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>>(new global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>(null, null, null));
			}
			finally
			{
				dataIndexStoreTelemetry.Duration = stopwatch.Elapsed;
				dataIndexStoreTelemetry.Message = "IndexResult=" + text;
				ExploreHostUtils.TraceDataIndexStoreTelemetry(dataIndexStoreTelemetry);
			}
			return task;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000080C4 File Offset: 0x000062C4
		public async Task<bool> StoreIndexAsync(IndexLookupKey key, DataIndex index, CancellationToken token)
		{
			DataIndexCacheKey primarykey = key.Primary;
			bool flag = await this.StoreIndexAsync(primarykey, index, token);
			if (flag)
			{
				foreach (DataIndexCacheKey dataIndexCacheKey in key.Alternates)
				{
					token.ThrowIfCancellationRequested();
					this.StoreAlternateKey(primarykey, dataIndexCacheKey, token);
				}
			}
			return flag;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008120 File Offset: 0x00006320
		public Task<bool> DeleteIndexAsync(DataIndexCacheKey key, CancellationToken token)
		{
			DataIndexStoreTelemetry dataIndexStoreTelemetry = new DataIndexStoreTelemetry(DataIndexStoreActionId.DeleteIndex, this.m_luciaSessionOptions);
			Stopwatch stopwatch = Stopwatch.StartNew();
			Task<bool> task;
			try
			{
				token.ThrowIfCancellationRequested();
				bool flag = this.m_storage.DeleteEntry(key.ToString());
				dataIndexStoreTelemetry.Status = (flag ? DataIndexOperationStatus.Succeeded : DataIndexOperationStatus.Failed);
				task = Task.FromResult<bool>(flag);
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				dataIndexStoreTelemetry.AddExceptionDetails("DeleteIndexAsync: Failed to delete DataIndex", ex);
				task = Task.FromResult<bool>(false);
			}
			finally
			{
				dataIndexStoreTelemetry.Duration = stopwatch.Elapsed;
				ExploreHostUtils.TraceDataIndexStoreTelemetry(dataIndexStoreTelemetry);
			}
			return task;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000081C4 File Offset: 0x000063C4
		public void StoreAlternateKey(DataIndexCacheKey primaryKey, DataIndexCacheKey alternateKey, CancellationToken token)
		{
			try
			{
				Stream stream = this.CreateNewStream(this.GetAlternateKeyLookupKey(alternateKey), token);
				if (stream != null)
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						primaryKey.WriteTo(streamWriter);
					}
				}
			}
			catch (Exception ex)
			{
				string text;
				Exception ex2;
				DataIndexStore.GetExceptionDetails(ex, out text, out ex2);
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008230 File Offset: 0x00006430
		private Stream OpenExistingStream(string key, IDatabaseContext databaseContext, CancellationToken token)
		{
			Stream existingEntry;
			try
			{
				token.ThrowIfCancellationRequested();
				existingEntry = this.m_storage.GetExistingEntry(key);
			}
			catch (Exception ex)
			{
				throw new DataIndexException("OpenExistingStream: Failed to get stream for given cache key: " + databaseContext.DatabaseName, ex);
			}
			return existingEntry;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000827C File Offset: 0x0000647C
		private Stream CreateNewStream(string key, CancellationToken token)
		{
			Stream stream;
			try
			{
				token.ThrowIfCancellationRequested();
				stream = this.m_storage.CreateNewEntry(key, true);
			}
			catch (Exception ex)
			{
				throw new DataIndexException("CreateNewStream: Failed to create stream for given cache key", ex);
			}
			return stream;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000082C0 File Offset: 0x000064C0
		private Task<bool> StoreIndexAsync(DataIndexCacheKey key, DataIndex index, CancellationToken token)
		{
			DataIndexStoreTelemetry dataIndexStoreTelemetry = new DataIndexStoreTelemetry(DataIndexStoreActionId.CreateIndex, this.m_luciaSessionOptions);
			Stopwatch stopwatch = Stopwatch.StartNew();
			Task<bool> task;
			try
			{
				Stream stream = this.CreateNewStream(key.ToString(), token);
				if (stream == null)
				{
					task = Task.FromResult<bool>(false);
				}
				else
				{
					token.ThrowIfCancellationRequested();
					using (stream)
					{
						using (DataIndexPackageWriter dataIndexPackageWriter = DataIndexPackage.CreateWriter(stream))
						{
							index.WriteTo(dataIndexPackageWriter, token);
						}
					}
					task = Task.FromResult<bool>(true);
				}
			}
			catch (Exception ex)
			{
				string text;
				Exception ex2;
				DataIndexStore.GetExceptionDetails(ex, out text, out ex2);
				this.EnsureAwaitingDelete(key, token);
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				dataIndexStoreTelemetry.AddExceptionDetails(text, ex.InnerException);
				task = Task.FromResult<bool>(false);
			}
			finally
			{
				dataIndexStoreTelemetry.Duration = stopwatch.Elapsed;
				ExploreHostUtils.TraceDataIndexStoreTelemetry(dataIndexStoreTelemetry);
			}
			return task;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000083C0 File Offset: 0x000065C0
		private bool TryGetMappedPrimaryKey(DataIndexCacheKey alternateKey, IDatabaseContext databaseContext, CancellationToken token, out DataIndexCacheKey primaryKey)
		{
			bool flag;
			try
			{
				token.ThrowIfCancellationRequested();
				using (Stream stream = this.OpenExistingStream(this.GetAlternateKeyLookupKey(alternateKey), databaseContext, token))
				{
					token.ThrowIfCancellationRequested();
					if (stream == null)
					{
						primaryKey = null;
						flag = false;
					}
					else
					{
						token.ThrowIfCancellationRequested();
						using (StreamReader streamReader = new StreamReader(stream))
						{
							primaryKey = DataIndexCacheKey.Read(streamReader);
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				string text;
				Exception ex2;
				DataIndexStore.GetExceptionDetails(ex, out text, out ex2);
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				primaryKey = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000846C File Offset: 0x0000666C
		private void EnsureAwaitingDelete(DataIndexCacheKey key, CancellationToken token)
		{
			Task<bool> task = Task.Run<bool>(() => this.DeleteIndexAsync(key, token));
			if (this.m_doSynchronousDelete)
			{
				task.Wait();
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000084B2 File Offset: 0x000066B2
		private static void GetExceptionDetails(Exception ex, out string message, out Exception innerException)
		{
			message = string.Empty;
			if (ex is DataIndexException)
			{
				message = ex.Message;
				innerException = ex.InnerException;
				return;
			}
			innerException = ex;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000084D7 File Offset: 0x000066D7
		private string GetAlternateKeyLookupKey(DataIndexCacheKey key)
		{
			return "AlternateKey-" + key.ToString();
		}

		// Token: 0x040000F5 RID: 245
		internal const string AlternateLookupKeyPrefix = "AlternateKey-";

		// Token: 0x040000F6 RID: 246
		private readonly IStreamBasedStorage m_storage;

		// Token: 0x040000F7 RID: 247
		private readonly LuciaSessionOptions m_luciaSessionOptions;

		// Token: 0x040000F8 RID: 248
		private readonly IDataIndexReader m_dataIndexReader;

		// Token: 0x040000F9 RID: 249
		private readonly bool m_doSynchronousDelete;
	}
}
