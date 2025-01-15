using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200028A RID: 650
	public class Serializer<T> : ISerializer<T>
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0003CDF2 File Offset: 0x0003AFF2
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0003CDFA File Offset: 0x0003AFFA
		public int QueuesCount { get; private set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0003CE03 File Offset: 0x0003B003
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0003CE0B File Offset: 0x0003B00B
		public int ItemsCount { get; private set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0003CE14 File Offset: 0x0003B014
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x0003CE1C File Offset: 0x0003B01C
		public int PeakQueueSize { get; private set; }

		// Token: 0x06001183 RID: 4483 RVA: 0x0003CE25 File Offset: 0x0003B025
		public Serializer()
		{
			this.m_actions = new Dictionary<T, Queue<AsyncResult<T>>>();
			this.PeakQueueSize = 0;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003CE3F File Offset: 0x0003B03F
		public virtual IAsyncResult BeginAcquireLock(T key, AsyncCallback callback, object state)
		{
			return this.BeginAcquireLock(key, -1, callback, state);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003CE4B File Offset: 0x0003B04B
		public virtual IAsyncResult BeginAcquireLock(T key, int timeoutInMilliseconds, AsyncCallback callback, object state)
		{
			return this.BeginAcquireLockImplementation(key, callback, state, timeoutInMilliseconds).First;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003CE60 File Offset: 0x0003B060
		public IDisposable EndAcquireLock(IAsyncResult ar)
		{
			T t = ((AsyncResult<T>)ar).End();
			return new Serializer<T>.SerializerHandle(this, t);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003CE80 File Offset: 0x0003B080
		public virtual bool TryAcquireLock(T key, out IDisposable lockHandle)
		{
			Pair<IDisposable, Serializer<T>.SerializerCountersData> pair;
			bool flag = this.TryAcquireLockImplementation(key, out pair);
			lockHandle = pair.First;
			return flag;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0003CEA0 File Offset: 0x0003B0A0
		public async Task<IDisposable> AcquireLockAsync(T key)
		{
			return await Task.Factory.FromAsync<T, IDisposable>(new Func<T, AsyncCallback, object, IAsyncResult>(this.BeginAcquireLock), new Func<IAsyncResult, IDisposable>(this.EndAcquireLock), key, null);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		public async Task<IDisposable> AcquireLockAsync(T key, int timeoutInMilliseconds)
		{
			return await Task.Factory.FromAsync<T, int, IDisposable>(new Func<T, int, AsyncCallback, object, IAsyncResult>(this.BeginAcquireLock), new Func<IAsyncResult, IDisposable>(this.EndAcquireLock), key, timeoutInMilliseconds, null);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0003CF45 File Offset: 0x0003B145
		protected Pair<IAsyncResult, Serializer<T>.SerializerCountersData> BeginAcquireLockImplementation(T key, AsyncCallback callback, object state, int timeoutInMillisecond = -1)
		{
			return this.BeginTryAcquireLockImplementation(key, timeoutInMillisecond, true, callback, state);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0003CF54 File Offset: 0x0003B154
		private Pair<IAsyncResult, Serializer<T>.SerializerCountersData> BeginTryAcquireLockImplementation(T key, int timeoutInMillisecond, bool allowEnqueueing, AsyncCallback callback, object state)
		{
			bool flag = false;
			Dictionary<T, Queue<AsyncResult<T>>> actions = this.m_actions;
			AsyncResult<T> asyncResult;
			Serializer<T>.SerializerCountersData serializerCountersData;
			lock (actions)
			{
				Queue<AsyncResult<T>> queue;
				if (this.m_actions.TryGetValue(key, out queue))
				{
					if (allowEnqueueing)
					{
						if (queue == null)
						{
							queue = new Queue<AsyncResult<T>>();
							this.m_actions[key] = queue;
						}
						asyncResult = new AsyncResult<T>(callback, state, key);
						queue.Enqueue(asyncResult);
						int itemsCount = this.ItemsCount;
						this.ItemsCount = itemsCount + 1;
						if (timeoutInMillisecond != -1)
						{
							AsyncResult<T>.TimeoutState timeoutState = new AsyncResult<T>.TimeoutState(asyncResult, key, timeoutInMillisecond);
							Timer timer = new Timer(delegate(object s)
							{
								this.RevokeLockRequestIfTimeout(s as AsyncResult<T>.TimeoutState);
							}, timeoutState, -1, -1);
							timeoutState.SetTimer(timer);
							timer.Change(timeoutInMillisecond, -1);
						}
						if (queue.Count > this.PeakQueueSize)
						{
							this.PeakQueueSize = queue.Count;
						}
					}
					else
					{
						asyncResult = null;
					}
				}
				else
				{
					asyncResult = new AsyncResult<T>(callback, state, key);
					this.m_actions.Add(key, null);
					flag = true;
				}
				this.QueuesCount = this.m_actions.Count;
				serializerCountersData = new Serializer<T>.SerializerCountersData
				{
					ItemsCount = this.ItemsCount,
					QueuesCount = this.QueuesCount,
					PeakQueueSize = this.PeakQueueSize
				};
			}
			if (flag)
			{
				asyncResult.SignalCompletion(true, key);
			}
			return new Pair<IAsyncResult, Serializer<T>.SerializerCountersData>(asyncResult, serializerCountersData);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		private void RevokeLockRequestIfTimeout(AsyncResult<T>.TimeoutState timeoutState)
		{
			AsyncResult<T> asyncResult = timeoutState.AsyncResult;
			T key = timeoutState.Key;
			using (timeoutState.Timer)
			{
				Dictionary<T, Queue<AsyncResult<T>>> actions = this.m_actions;
				lock (actions)
				{
					if (!asyncResult.IsCompleted)
					{
						Queue<AsyncResult<T>> queue;
						this.m_actions.TryGetValue(key, out queue);
						if (queue != null && queue.Contains(asyncResult))
						{
							Queue<AsyncResult<T>> queue2 = new Queue<AsyncResult<T>>();
							while (queue.Count > 0)
							{
								AsyncResult<T> asyncResult2 = queue.Dequeue();
								if (asyncResult2 != asyncResult)
								{
									queue2.Enqueue(asyncResult2);
								}
							}
							while (queue2.Count > 0)
							{
								queue.Enqueue(queue2.Dequeue());
							}
							int itemsCount = this.ItemsCount;
							this.ItemsCount = itemsCount - 1;
							asyncResult.SignalCompletion(false, new AcquireLockTimeoutException(key.ToString(), timeoutState.TimeoutInMillisecond.ToString()));
						}
					}
				}
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		protected bool TryAcquireLockImplementation(T key, out Pair<IDisposable, Serializer<T>.SerializerCountersData> handleAndCounters)
		{
			Pair<IAsyncResult, Serializer<T>.SerializerCountersData> pair = this.BeginTryAcquireLockImplementation(key, -1, false, null, null);
			IAsyncResult first = pair.First;
			Serializer<T>.SerializerCountersData second = pair.Second;
			if (first == null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Lock could not be acquired immediately and the operation was not enqueued.");
				handleAndCounters = new Pair<IDisposable, Serializer<T>.SerializerCountersData>(null, second);
				return false;
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Lock acquired.");
			IDisposable disposable = this.EndAcquireLock(first);
			handleAndCounters = new Pair<IDisposable, Serializer<T>.SerializerCountersData>(disposable, second);
			return true;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003D238 File Offset: 0x0003B438
		protected virtual Serializer<T>.SerializerCountersData ReleaseLock(T key)
		{
			Dictionary<T, Queue<AsyncResult<T>>> actions = this.m_actions;
			Serializer<T>.SerializerCountersData serializerCountersData;
			lock (actions)
			{
				Queue<AsyncResult<T>> queue;
				if (!this.m_actions.TryGetValue(key, out queue))
				{
					serializerCountersData = default(Serializer<T>.SerializerCountersData);
					serializerCountersData.ItemsCount = this.ItemsCount;
					serializerCountersData.QueuesCount = this.QueuesCount;
					serializerCountersData.PeakQueueSize = this.PeakQueueSize;
					serializerCountersData = serializerCountersData;
				}
				else
				{
					if (queue == null || queue.Count == 0)
					{
						this.m_actions.Remove(key);
						this.QueuesCount = this.m_actions.Count;
					}
					else
					{
						AsyncResult<T> ar = queue.Dequeue();
						int itemsCount = this.ItemsCount;
						this.ItemsCount = itemsCount - 1;
						AsyncInvoker.InvokeMethodAsynchronously(delegate
						{
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "NOP trace to workaround .Net presumable Jitter bug.[{0}]", new object[] { DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture) });
							ar.SignalCompletion(false, key);
						}, WaitOrNot.DontWait, "SerializerReleaseLock");
					}
					serializerCountersData = new Serializer<T>.SerializerCountersData
					{
						ItemsCount = this.ItemsCount,
						QueuesCount = this.QueuesCount,
						PeakQueueSize = this.PeakQueueSize
					};
				}
			}
			return serializerCountersData;
		}

		// Token: 0x04000660 RID: 1632
		private Dictionary<T, Queue<AsyncResult<T>>> m_actions;

		// Token: 0x02000738 RID: 1848
		private class SerializerHandle : IDisposable
		{
			// Token: 0x06002FCF RID: 12239 RVA: 0x000A462D File Offset: 0x000A282D
			public SerializerHandle(Serializer<T> serializer, T key)
			{
				this.m_serializer = serializer;
				this.m_key = key;
			}

			// Token: 0x06002FD0 RID: 12240 RVA: 0x000A4643 File Offset: 0x000A2843
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06002FD1 RID: 12241 RVA: 0x000A464C File Offset: 0x000A284C
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.m_serializer.ReleaseLock(this.m_key);
				}
			}

			// Token: 0x06002FD2 RID: 12242 RVA: 0x000A4663 File Offset: 0x000A2863
			private static bool IsNotNull(object value)
			{
				return value != null;
			}

			// Token: 0x04001553 RID: 5459
			private Serializer<T> m_serializer;

			// Token: 0x04001554 RID: 5460
			private T m_key;
		}

		// Token: 0x02000739 RID: 1849
		protected struct SerializerCountersData
		{
			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000A4669 File Offset: 0x000A2869
			// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000A4671 File Offset: 0x000A2871
			public int QueuesCount { get; set; }

			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000A467A File Offset: 0x000A287A
			// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000A4682 File Offset: 0x000A2882
			public int ItemsCount { get; set; }

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000A468B File Offset: 0x000A288B
			// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x000A4693 File Offset: 0x000A2893
			public int PeakQueueSize { get; set; }

			// Token: 0x06002FD9 RID: 12249 RVA: 0x000A469C File Offset: 0x000A289C
			public override bool Equals(object obj)
			{
				if (!(obj is Serializer<T>.SerializerCountersData))
				{
					return false;
				}
				Serializer<T>.SerializerCountersData serializerCountersData = (Serializer<T>.SerializerCountersData)obj;
				return this.ItemsCount.Equals(serializerCountersData.ItemsCount) && this.QueuesCount.Equals(serializerCountersData.QueuesCount) && this.PeakQueueSize.Equals(serializerCountersData.PeakQueueSize);
			}

			// Token: 0x06002FDA RID: 12250 RVA: 0x000A4700 File Offset: 0x000A2900
			public override int GetHashCode()
			{
				return (this.ItemsCount.GetHashCode() ^ this.QueuesCount.GetHashCode() ^ this.PeakQueueSize.GetHashCode()).GetHashCode();
			}

			// Token: 0x06002FDB RID: 12251 RVA: 0x000A4741 File Offset: 0x000A2941
			public static bool operator ==(Serializer<T>.SerializerCountersData scLeft, Serializer<T>.SerializerCountersData scRight)
			{
				return scLeft.Equals(scRight);
			}

			// Token: 0x06002FDC RID: 12252 RVA: 0x000A4756 File Offset: 0x000A2956
			public static bool operator !=(Serializer<T>.SerializerCountersData scLeft, Serializer<T>.SerializerCountersData scRight)
			{
				return !(scLeft == scRight);
			}
		}
	}
}
