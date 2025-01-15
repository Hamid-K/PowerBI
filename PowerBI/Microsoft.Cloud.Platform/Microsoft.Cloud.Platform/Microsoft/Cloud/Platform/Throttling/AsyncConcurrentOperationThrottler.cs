using System;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.PowerBI.ServiceContracts;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x020000FE RID: 254
	public sealed class AsyncConcurrentOperationThrottler : IDisposable
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00018874 File Offset: 0x00016A74
		public AsyncConcurrentOperationThrottler(long maxThrottlingUnits, long maxOutstandingThrottlingUnitRequests, ThrottlingBehavior behavior, ThrottlerQueueingBehavior queueingBehavior = ThrottlerQueueingBehavior.Fifo)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxThrottlingUnits, "maxThrottlingUnits");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxOutstandingThrottlingUnitRequests, "maxOutstandingThrottlingUnitRequests");
			this.m_availableConcurrentOperations = maxThrottlingUnits;
			this.m_maxConcurrentOperations = maxThrottlingUnits;
			this.m_maxQueuedConcurrentRequests = maxOutstandingThrottlingUnitRequests;
			this.m_throttlingBehavior = behavior;
			if (queueingBehavior == ThrottlerQueueingBehavior.Fifo)
			{
				this.m_queuedConcurrentRequests = new ThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest>();
				return;
			}
			if (queueingBehavior != ThrottlerQueueingBehavior.Priority)
			{
				ExtendedDiagnostics.EnsureInvalidSwitchValue<ThrottlerQueueingBehavior>(queueingBehavior);
				return;
			}
			this.m_queuedConcurrentRequests = new ThrottlerPriorityQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest>();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000188E4 File Offset: 0x00016AE4
		internal bool IsEmpty
		{
			get
			{
				IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
				bool flag2;
				lock (queuedConcurrentRequests)
				{
					flag2 = this.m_availableConcurrentOperations == this.m_maxConcurrentOperations;
				}
				return flag2;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00018930 File Offset: 0x00016B30
		internal long AvailableConcurrentOperations
		{
			get
			{
				IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
				long availableConcurrentOperations;
				lock (queuedConcurrentRequests)
				{
					availableConcurrentOperations = this.m_availableConcurrentOperations;
				}
				return availableConcurrentOperations;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00018974 File Offset: 0x00016B74
		internal long QueuedOperationsCount
		{
			get
			{
				IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
				long numQueuedRequests;
				lock (queuedConcurrentRequests)
				{
					numQueuedRequests = this.m_numQueuedRequests;
				}
				return numQueuedRequests;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000189B8 File Offset: 0x00016BB8
		public Task<IDisposable> ThrottleConcurrentRequestAsync(double priority = 0.0)
		{
			return this.ThrottleConcurrentRequestsAsync(1L, priority);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000189C3 File Offset: 0x00016BC3
		public Task<IDisposable> ThrottleConcurrentRequestsAsync(long numConcurrentRequests, double priority = 0.0)
		{
			TryGetResult<Task<IDisposable>> tryGetResult = this.TryThrottleConcurrentRequestsAsync(numConcurrentRequests, priority);
			if (!tryGetResult)
			{
				throw new OperationThrottledException();
			}
			return tryGetResult.Result;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000189E0 File Offset: 0x00016BE0
		public TryGetResult<Task<IDisposable>> TryThrottleConcurrentRequestsAsync(double priority = 0.0)
		{
			return this.TryThrottleConcurrentRequestsAsync(1L, priority);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000189EC File Offset: 0x00016BEC
		public TryGetResult<Task<IDisposable>> TryThrottleConcurrentRequestsAsync(long numConcurrentRequests, double priority = 0.0)
		{
			ExtendedDiagnostics.EnsureOperation(numConcurrentRequests <= this.m_maxConcurrentOperations, "Cannot request more concurrent capacity than the max number of concurrent requests.");
			IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
			TryGetResult<Task<IDisposable>> tryGetResult;
			lock (queuedConcurrentRequests)
			{
				if (this.m_availableConcurrentOperations >= numConcurrentRequests)
				{
					this.m_availableConcurrentOperations -= numConcurrentRequests;
					tryGetResult = TryGetResult.Create<Task<IDisposable>>(Task.FromResult<IDisposable>(new AsyncConcurrentOperationThrottler.ThrottledOperationReleaser(numConcurrentRequests, this)));
				}
				else if (this.m_throttlingBehavior == ThrottlingBehavior.QueueRequests && this.m_numQueuedRequests + numConcurrentRequests <= this.m_maxQueuedConcurrentRequests)
				{
					AsyncConcurrentOperationThrottler.ConcurrentOperationRequest concurrentOperationRequest = new AsyncConcurrentOperationThrottler.ConcurrentOperationRequest(numConcurrentRequests, priority);
					this.m_queuedConcurrentRequests.Enqueue(concurrentOperationRequest);
					this.m_numQueuedRequests += numConcurrentRequests;
					tryGetResult = TryGetResult.Create<Task<IDisposable>>(concurrentOperationRequest.TaskHandle.Task);
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("Insufficient throttling capacity available. Requested: {0} Max units per key (queued + running): {1}", new object[]
					{
						numConcurrentRequests,
						this.m_maxQueuedConcurrentRequests + this.m_maxConcurrentOperations
					});
					tryGetResult = TryGetResult.Failed<Task<IDisposable>>();
				}
			}
			return tryGetResult;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00018AF4 File Offset: 0x00016CF4
		public void Dispose()
		{
			this.ReleaseAllRequests(MaxKeysExceededBehvaior.FailRequests);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00018B00 File Offset: 0x00016D00
		internal void ReleaseAllRequests(MaxKeysExceededBehvaior behavior)
		{
			IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
			AsyncConcurrentOperationThrottler.ConcurrentOperationRequest[] array;
			lock (queuedConcurrentRequests)
			{
				array = new AsyncConcurrentOperationThrottler.ConcurrentOperationRequest[this.m_queuedConcurrentRequests.Count];
				int num = 0;
				while (this.m_queuedConcurrentRequests.Count > 0)
				{
					array[num++] = this.m_queuedConcurrentRequests.Dequeue();
				}
			}
			this.ReleaseAllRequestsAsync(behavior, array).DoNotWait();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00018B7C File Offset: 0x00016D7C
		private async Task ReleaseAllRequestsAsync(MaxKeysExceededBehvaior behavior, AsyncConcurrentOperationThrottler.ConcurrentOperationRequest[] requests)
		{
			await Task.Yield();
			foreach (AsyncConcurrentOperationThrottler.ConcurrentOperationRequest concurrentOperationRequest in requests)
			{
				if (behavior == MaxKeysExceededBehvaior.FailRequests)
				{
					concurrentOperationRequest.TaskHandle.SetException(new ThrottlerOverflowException("Throttler exceeded max keys."));
				}
				else
				{
					concurrentOperationRequest.TaskHandle.SetResult(new AsyncConcurrentOperationThrottler.NullThrottlingUnitReleaser());
				}
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018BCC File Offset: 0x00016DCC
		private void Release(long numConcurrentRequestsToRelease)
		{
			ExtendedDiagnostics.EnsureOperation(numConcurrentRequestsToRelease <= this.m_maxConcurrentOperations, "numUnitsToRelease <= m_totalThrottlingUnits");
			AsyncConcurrentOperationThrottler.ConcurrentOperationRequest concurrentOperationRequest = null;
			bool flag = false;
			IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> queuedConcurrentRequests = this.m_queuedConcurrentRequests;
			lock (queuedConcurrentRequests)
			{
				this.m_availableConcurrentOperations += numConcurrentRequestsToRelease;
				if (this.m_queuedConcurrentRequests.Count > 0)
				{
					concurrentOperationRequest = this.m_queuedConcurrentRequests.Peek();
					if (concurrentOperationRequest != null && concurrentOperationRequest.NumThrottlingUnitsRequested <= this.m_availableConcurrentOperations)
					{
						flag = true;
						concurrentOperationRequest = this.m_queuedConcurrentRequests.Dequeue();
						this.m_availableConcurrentOperations -= concurrentOperationRequest.NumThrottlingUnitsRequested;
						this.m_numQueuedRequests -= concurrentOperationRequest.NumThrottlingUnitsRequested;
					}
				}
			}
			if (flag)
			{
				this.ReleaseAsync(concurrentOperationRequest).DoNotWait();
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00018C9C File Offset: 0x00016E9C
		private async Task ReleaseAsync(AsyncConcurrentOperationThrottler.ConcurrentOperationRequest toRelease)
		{
			await Task.Yield();
			toRelease.TaskHandle.SetResult(new AsyncConcurrentOperationThrottler.ThrottledOperationReleaser(toRelease.NumThrottlingUnitsRequested, this));
		}

		// Token: 0x04000263 RID: 611
		private readonly ThrottlingBehavior m_throttlingBehavior;

		// Token: 0x04000264 RID: 612
		private readonly IThrottlerQueue<AsyncConcurrentOperationThrottler.ConcurrentOperationRequest> m_queuedConcurrentRequests;

		// Token: 0x04000265 RID: 613
		private readonly long m_maxConcurrentOperations;

		// Token: 0x04000266 RID: 614
		private long m_availableConcurrentOperations;

		// Token: 0x04000267 RID: 615
		private long m_numQueuedRequests;

		// Token: 0x04000268 RID: 616
		private readonly long m_maxQueuedConcurrentRequests;

		// Token: 0x020005E4 RID: 1508
		private sealed class ConcurrentOperationRequest : IPrioritizedObject
		{
			// Token: 0x06002BDA RID: 11226 RVA: 0x0009B2C9 File Offset: 0x000994C9
			internal ConcurrentOperationRequest(long requestedUnits, double priority = 0.0)
			{
				this.NumThrottlingUnitsRequested = requestedUnits;
				this.Priority = priority;
			}

			// Token: 0x17000703 RID: 1795
			// (get) Token: 0x06002BDB RID: 11227 RVA: 0x0009B2EA File Offset: 0x000994EA
			public TaskCompletionSource<IDisposable> TaskHandle
			{
				get
				{
					return this.m_taskHandle;
				}
			}

			// Token: 0x17000704 RID: 1796
			// (get) Token: 0x06002BDC RID: 11228 RVA: 0x0009B2F2 File Offset: 0x000994F2
			// (set) Token: 0x06002BDD RID: 11229 RVA: 0x0009B2FA File Offset: 0x000994FA
			public long NumThrottlingUnitsRequested { get; private set; }

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x06002BDE RID: 11230 RVA: 0x0009B303 File Offset: 0x00099503
			// (set) Token: 0x06002BDF RID: 11231 RVA: 0x0009B30B File Offset: 0x0009950B
			public double Priority { get; private set; }

			// Token: 0x04000FFF RID: 4095
			private readonly TaskCompletionSource<IDisposable> m_taskHandle = new TaskCompletionSource<IDisposable>();
		}

		// Token: 0x020005E5 RID: 1509
		private sealed class ThrottledOperationReleaser : IDisposable
		{
			// Token: 0x06002BE0 RID: 11232 RVA: 0x0009B314 File Offset: 0x00099514
			public ThrottledOperationReleaser(long numOperations, AsyncConcurrentOperationThrottler parentThrottler)
			{
				this.m_numOperations = numOperations;
				this.m_parentThrottler = parentThrottler;
			}

			// Token: 0x06002BE1 RID: 11233 RVA: 0x0009B32A File Offset: 0x0009952A
			public void Dispose()
			{
				this.m_parentThrottler.Release(this.m_numOperations);
			}

			// Token: 0x04001002 RID: 4098
			private readonly long m_numOperations;

			// Token: 0x04001003 RID: 4099
			private readonly AsyncConcurrentOperationThrottler m_parentThrottler;
		}

		// Token: 0x020005E6 RID: 1510
		private sealed class NullThrottlingUnitReleaser : IDisposable
		{
			// Token: 0x06002BE2 RID: 11234 RVA: 0x00009B3B File Offset: 0x00007D3B
			public void Dispose()
			{
			}
		}
	}
}
