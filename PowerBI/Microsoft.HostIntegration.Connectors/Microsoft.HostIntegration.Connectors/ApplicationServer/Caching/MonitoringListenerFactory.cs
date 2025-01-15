using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A8 RID: 168
	internal static class MonitoringListenerFactory
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00014045 File Offset: 0x00012245
		internal static IMonitoringListener CreateListener(bool isEmpty, DataCache dataCache, CacheOperationType operationType)
		{
			if (isEmpty)
			{
				return MonitoringListenerFactory._emptyListenerInstance;
			}
			return new MonitoringListenerFactory.MonitoringListener(dataCache, operationType);
		}

		// Token: 0x0400030B RID: 779
		internal const string RequestListPropertyName = "Requests";

		// Token: 0x0400030C RID: 780
		private static MonitoringListenerFactory.EmptyListener _emptyListenerInstance = new MonitoringListenerFactory.EmptyListener();

		// Token: 0x020000A9 RID: 169
		private sealed class MonitoringListener : IMonitoringListener
		{
			// Token: 0x060003FF RID: 1023 RVA: 0x00014063 File Offset: 0x00012263
			internal MonitoringListener(DataCache dataCache, CacheOperationType operationType)
			{
				this._dataCache = dataCache;
				this._cacheOperationEventArgs = new CacheOperationCompletedEventArgs(operationType);
				this._requestTrackerList = (this._dataCache.IsRequestTrackingEnabled ? new List<IRequestTracker>(1) : null);
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0001409C File Offset: 0x0001229C
			void IMonitoringListener.Listen(Action innerDelegate)
			{
				try
				{
					CacheOperationStartedEventArgs cacheOperationStartedEventArgs = CacheOperationStartedEventArgs.FromCompletedEventArgs(this._cacheOperationEventArgs);
					this._dataCache.OnCacheOperationStarted(cacheOperationStartedEventArgs);
					innerDelegate();
				}
				catch (Exception ex)
				{
					this._cacheOperationEventArgs.ExceptionObject = ex;
					throw;
				}
				finally
				{
					if (this._requestTrackerList != null)
					{
						this._cacheOperationEventArgs.OperationContext.Properties["Requests"] = new ReadOnlyCollection<IRequestTracker>(this._requestTrackerList);
					}
					this._dataCache.OnCacheOperationCompleted(this._cacheOperationEventArgs);
				}
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x00014138 File Offset: 0x00012338
			TResult IMonitoringListener.Listen<TResult>(Func<TResult> innerDelegate)
			{
				TResult tresult;
				try
				{
					CacheOperationStartedEventArgs cacheOperationStartedEventArgs = CacheOperationStartedEventArgs.FromCompletedEventArgs(this._cacheOperationEventArgs);
					this._dataCache.OnCacheOperationStarted(cacheOperationStartedEventArgs);
					tresult = innerDelegate();
				}
				catch (Exception ex)
				{
					this._cacheOperationEventArgs.ExceptionObject = ex;
					throw;
				}
				finally
				{
					if (this._requestTrackerList != null)
					{
						this._cacheOperationEventArgs.OperationContext.Properties["Requests"] = new ReadOnlyCollection<IRequestTracker>(this._requestTrackerList);
					}
					this._dataCache.OnCacheOperationCompleted(this._cacheOperationEventArgs);
				}
				return tresult;
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x000141D4 File Offset: 0x000123D4
			void IMonitoringListener.AddTrackerInfo(IRequestTracker tracker)
			{
				if (tracker != null && this._requestTrackerList != null)
				{
					this._requestTrackerList.Add(tracker);
				}
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x000141ED File Offset: 0x000123ED
			bool IMonitoringListener.IsRequestTrackingSupported()
			{
				return this._requestTrackerList != null;
			}

			// Token: 0x0400030D RID: 781
			private readonly DataCache _dataCache;

			// Token: 0x0400030E RID: 782
			private readonly CacheOperationCompletedEventArgs _cacheOperationEventArgs;

			// Token: 0x0400030F RID: 783
			private readonly IList<IRequestTracker> _requestTrackerList;
		}

		// Token: 0x020000AA RID: 170
		private sealed class EmptyListener : IMonitoringListener
		{
			// Token: 0x06000404 RID: 1028 RVA: 0x000141FB File Offset: 0x000123FB
			void IMonitoringListener.Listen(Action innerDelegate)
			{
				innerDelegate();
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x00014203 File Offset: 0x00012403
			TResult IMonitoringListener.Listen<TResult>(Func<TResult> innerDelegate)
			{
				return innerDelegate();
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x000036A9 File Offset: 0x000018A9
			void IMonitoringListener.AddTrackerInfo(IRequestTracker tracker)
			{
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x00006F04 File Offset: 0x00005104
			bool IMonitoringListener.IsRequestTrackingSupported()
			{
				return false;
			}
		}
	}
}
