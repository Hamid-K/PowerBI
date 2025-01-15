using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C5A RID: 7258
	internal sealed class ContainerPoolContainerFactory : IPooledContainerFactory, IContainerFactory, IDisposable
	{
		// Token: 0x0600B4E5 RID: 46309 RVA: 0x0024AC89 File Offset: 0x00248E89
		public ContainerPoolContainerFactory(IContainerFactory factory, string identity, int minSize, TimeSpan? containerTimeToLive)
			: this(factory, identity, minSize, containerTimeToLive, new ContainerPoolContainerFactory.TimerFactory(ContainerPoolContainerFactory.CreateTimer))
		{
		}

		// Token: 0x0600B4E6 RID: 46310 RVA: 0x0024ACA4 File Offset: 0x00248EA4
		public ContainerPoolContainerFactory(IContainerFactory factory, string identity, int minSize, TimeSpan? containerTimeToLive, ContainerPoolContainerFactory.TimerFactory timerFactory)
		{
			this.syncRoot = new object();
			this.identity = identity;
			this.minPoolSize = minSize;
			this.factory = factory;
			this.pool = new Dictionary<int, ContainerPoolContainerFactory.PooledContainer>();
			this.free = new OrderedDictionary();
			this.containerTimeToLive = containerTimeToLive;
			this.timerFactory = timerFactory;
			this.preferredCacheStats = this.NewCacheStats("Preferred");
			this.EnsureMinPool();
		}

		// Token: 0x17002D38 RID: 11576
		// (get) Token: 0x0600B4E7 RID: 46311 RVA: 0x0024AD14 File Offset: 0x00248F14
		public int ActiveContainerCount
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					if (this.pool != null)
					{
						num = this.pool.Count - this.free.Count;
					}
					else
					{
						num = 0;
					}
				}
				return num;
			}
		}

		// Token: 0x0600B4E8 RID: 46312 RVA: 0x0024AD74 File Offset: 0x00248F74
		public IContainer CreateContainer(ConcurrentSet<int> preferredContainerIDs)
		{
			IContainer container2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/CreateContainer", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				int[] array = preferredContainerIDs.ToArray();
				hostTrace.Add("preferredContainerCount", array.Length, false);
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.pool != null)
					{
						preferredContainerIDs.RemoveRange(array.Where((int i) => !this.pool.ContainsKey(i)));
					}
				}
				array = preferredContainerIDs.ToArray();
				ContainerPoolContainerFactory.PooledContainer pooledContainer = null;
				int[] array2 = array;
				int j = 0;
				while (j < array2.Length)
				{
					int num = array2[j];
					obj = this.syncRoot;
					lock (obj)
					{
						if (this.free == null)
						{
							pooledContainer = null;
							break;
						}
						object obj2 = num;
						pooledContainer = (ContainerPoolContainerFactory.PooledContainer)this.free[obj2];
						if (pooledContainer == null)
						{
							goto IL_010C;
						}
						this.free.Remove(obj2);
					}
					goto IL_00F4;
					IL_010C:
					j++;
					continue;
					IL_00F4:
					if (!pooledContainer.IsUsable)
					{
						this.KillContainer(pooledContainer);
						this.FreeContainer(pooledContainer);
						pooledContainer = null;
						goto IL_010C;
					}
					break;
				}
				IContainer container;
				if (pooledContainer == null)
				{
					container = this.CreateContainer();
				}
				else
				{
					container = new ContainerPoolContainerFactory.Container(this, pooledContainer);
				}
				hostTrace.Add("containerID", container.ContainerID, false);
				hostTrace.Add("wasPreferredContainer", pooledContainer != null, false);
				this.preferredCacheStats.Access(pooledContainer != null);
				this.TracePoolSizes(hostTrace);
				preferredContainerIDs.Add(container.ContainerID);
				container2 = container;
			}
			return container2;
		}

		// Token: 0x0600B4E9 RID: 46313 RVA: 0x0024AF60 File Offset: 0x00249160
		public IContainer CreateContainer()
		{
			IContainer container;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/CreateContainer", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				int num = -1;
				int num2 = -1;
				ContainerPoolContainerFactory.PooledContainer pooledContainer;
				for (;;)
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.pool != null)
						{
							num2 = this.pool.Count;
						}
						if (this.free == null || this.free.Count == 0)
						{
							pooledContainer = null;
							num = 0;
						}
						else
						{
							pooledContainer = (ContainerPoolContainerFactory.PooledContainer)this.free.Pop();
							num = this.free.Count;
						}
					}
					if (pooledContainer == null)
					{
						break;
					}
					if (pooledContainer.IsUsable)
					{
						goto IL_00BE;
					}
					this.KillContainer(pooledContainer);
					this.FreeContainer(pooledContainer);
				}
				pooledContainer = this.CreatePooledContainer(TimeSpan.Zero);
				IL_00BE:
				hostTrace.Add("containerID", pooledContainer.Instance.ContainerID, false);
				hostTrace.Add("freeCount", num, false);
				hostTrace.Add("poolCount", num2, false);
				container = new ContainerPoolContainerFactory.Container(this, pooledContainer);
			}
			return container;
		}

		// Token: 0x0600B4EA RID: 46314 RVA: 0x0024B0A0 File Offset: 0x002492A0
		public void Dispose()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.factory != null)
				{
					foreach (ContainerPoolContainerFactory.PooledContainer pooledContainer in this.pool.Values)
					{
						IContainer instance = pooledContainer.Instance;
						if (instance != null)
						{
							instance.Dispose();
						}
					}
					this.factory.Dispose();
					this.factory = null;
					this.pool = null;
					this.free = null;
				}
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
			}
		}

		// Token: 0x0600B4EB RID: 46315 RVA: 0x0024B16C File Offset: 0x0024936C
		private ContainerPoolContainerFactory.PooledContainer CreatePooledContainer(TimeSpan ttlAdjustment)
		{
			ContainerPoolContainerFactory.PooledContainer pooledContainer2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/CreatePooledContainer", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				object obj = this.syncRoot;
				IContainerFactory containerFactory;
				lock (obj)
				{
					containerFactory = this.factory;
					if (containerFactory == null)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
				}
				TimeSpan? timeSpan = this.containerTimeToLive + ttlAdjustment;
				ContainerPoolContainerFactory.PooledContainer pooledContainer = new ContainerPoolContainerFactory.PooledContainer(containerFactory.CreateContainer(), timeSpan);
				hostTrace.Add("containerID", pooledContainer.Instance.ContainerID, false);
				hostTrace.Add("effectiveTTL", timeSpan, false);
				obj = this.syncRoot;
				lock (obj)
				{
					if (this.pool != null)
					{
						this.pool.Add(pooledContainer.Instance.ContainerID, pooledContainer);
					}
				}
				this.TracePoolSizes(hostTrace);
				pooledContainer2 = pooledContainer;
			}
			return pooledContainer2;
		}

		// Token: 0x0600B4EC RID: 46316 RVA: 0x0024B2EC File Offset: 0x002494EC
		private void FreeContainer(ContainerPoolContainerFactory.PooledContainer container)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/FreeContainer", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				hostTrace.Add("containerID", container.Instance.ContainerID, false);
				ContainerPoolContainerFactory.PooledContainer pooledContainer = null;
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.pool != null && this.pool.ContainsKey(container.Instance.ContainerID) && container.IsUsable)
					{
						this.free.Add(container.Instance.ContainerID, container);
						this.EnsureCleanupTimer();
					}
					else
					{
						pooledContainer = container;
					}
				}
				if (pooledContainer != null)
				{
					this.KillContainer(pooledContainer);
					hostTrace.Add("containerIDToDispose", pooledContainer.Instance.ContainerID, false);
					pooledContainer.Instance.Dispose();
				}
				this.TracePoolSizes(hostTrace);
			}
		}

		// Token: 0x0600B4ED RID: 46317 RVA: 0x0024B404 File Offset: 0x00249604
		private void KillContainer(ContainerPoolContainerFactory.PooledContainer container)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/KillContainer", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				hostTrace.Add("containerID", container.Instance.ContainerID, false);
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.pool != null)
					{
						this.free.Remove(container.Instance.ContainerID);
						this.pool.Remove(container.Instance.ContainerID);
					}
				}
				container.Instance.Kill();
				this.TracePoolSizes(hostTrace);
			}
			this.EnsureMinPool();
		}

		// Token: 0x0600B4EE RID: 46318 RVA: 0x0024B4E4 File Offset: 0x002496E4
		private void TracePoolSizes(IHostTrace trace)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.pool != null)
				{
					trace.Add("freeCount", this.free.Count, false);
					trace.Add("poolCount", this.pool.Count, false);
				}
			}
		}

		// Token: 0x0600B4EF RID: 46319 RVA: 0x0024B560 File Offset: 0x00249760
		private CacheStats NewCacheStats(string identity)
		{
			return new CacheStats("ContainerPoolContainerFactory(" + this.identity + ")/" + identity, null, 1, 100);
		}

		// Token: 0x0600B4F0 RID: 46320 RVA: 0x0024B584 File Offset: 0x00249784
		private void EnsureCleanupTimer()
		{
			if (this.containerTimeToLive != null && this.timer == null && this.free.Count > 0)
			{
				double num = Math.Min((from ContainerPoolContainerFactory.PooledContainer c in this.free.Values
					select c.RemainingTimeToLive).Min<TimeSpan>().TotalMilliseconds, 2147483647.0);
				num = Math.Max(num, ContainerPoolContainerFactory.minCleanupInterval.TotalMilliseconds);
				this.timer = this.timerFactory(SafeThread2.CreateTimerCallback(new TimerCallback(this.CleanupTask)), null, (long)num, -1L);
			}
		}

		// Token: 0x0600B4F1 RID: 46321 RVA: 0x0024B648 File Offset: 0x00249848
		private void CleanupTask(object state)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/Cleanup", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				try
				{
					List<ContainerPoolContainerFactory.PooledContainer> list = null;
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.free == null)
						{
							return;
						}
						Stack<ContainerPoolContainerFactory.PooledContainer> stack = null;
						while (this.free.Count > 0)
						{
							ContainerPoolContainerFactory.PooledContainer pooledContainer = (ContainerPoolContainerFactory.PooledContainer)this.free.Pop();
							if (pooledContainer.IsUsable)
							{
								if (stack == null)
								{
									stack = new Stack<ContainerPoolContainerFactory.PooledContainer>(this.free.Count);
								}
								stack.Push(pooledContainer);
							}
							else
							{
								if (list == null)
								{
									list = new List<ContainerPoolContainerFactory.PooledContainer>(this.free.Count);
								}
								list.Add(pooledContainer);
							}
						}
						if (stack != null)
						{
							foreach (ContainerPoolContainerFactory.PooledContainer pooledContainer2 in stack)
							{
								this.free.Add(pooledContainer2.Instance.ContainerID, pooledContainer2);
							}
						}
					}
					if (list != null)
					{
						foreach (ContainerPoolContainerFactory.PooledContainer pooledContainer3 in list)
						{
							this.KillContainer(pooledContainer3);
							this.FreeContainer(pooledContainer3);
						}
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
				}
				finally
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.timer.Dispose();
						this.timer = null;
						this.EnsureCleanupTimer();
					}
				}
			}
		}

		// Token: 0x0600B4F2 RID: 46322 RVA: 0x0024B898 File Offset: 0x00249A98
		private void EnsureMinPool()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!this.startingContainers && this.pool != null && this.minPoolSize > this.pool.Count)
				{
					EvaluatorThreadPool.Start(new ThreadStart(this.StartContainersThread));
					this.startingContainers = true;
				}
			}
		}

		// Token: 0x0600B4F3 RID: 46323 RVA: 0x0024B910 File Offset: 0x00249B10
		private void StartContainersThread()
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerPoolContainerFactory/StartContainersThread", null, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				try
				{
					for (;;)
					{
						object obj = this.syncRoot;
						TimeSpan timeSpan;
						lock (obj)
						{
							if (this.pool == null || this.pool.Count >= this.minPoolSize)
							{
								break;
							}
							int num = this.minPoolSize - this.pool.Count;
							if (this.containerTimeToLive != null)
							{
								double num2 = this.containerTimeToLive.Value.TotalMilliseconds / (double)this.minPoolSize;
								timeSpan = -TimeSpan.FromMilliseconds((double)(num - 1) * num2);
							}
							else
							{
								timeSpan = TimeSpan.Zero;
							}
						}
						ContainerPoolContainerFactory.PooledContainer pooledContainer = this.CreatePooledContainer(timeSpan);
						new ContainerPoolContainerFactory.Container(this, pooledContainer).Dispose();
					}
				}
				catch (Exception ex) when (SafeExceptions.TraceIsSafeException(hostTrace, ex))
				{
				}
				finally
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.startingContainers = false;
					}
				}
			}
		}

		// Token: 0x0600B4F4 RID: 46324 RVA: 0x0024BABC File Offset: 0x00249CBC
		private static Timer CreateTimer(TimerCallback callback, object state, long dueTime, long period)
		{
			return new Timer(callback, state, TimeSpan.FromMilliseconds((double)dueTime), TimeSpan.FromMilliseconds((double)period));
		}

		// Token: 0x04005C23 RID: 23587
		private const int traceFrequency = 1;

		// Token: 0x04005C24 RID: 23588
		private const int resetFrequency = 100;

		// Token: 0x04005C25 RID: 23589
		private static readonly TimeSpan minCleanupInterval = TimeSpan.FromSeconds(60.0);

		// Token: 0x04005C26 RID: 23590
		private readonly object syncRoot;

		// Token: 0x04005C27 RID: 23591
		private readonly string identity;

		// Token: 0x04005C28 RID: 23592
		private readonly int minPoolSize;

		// Token: 0x04005C29 RID: 23593
		private readonly TimeSpan? containerTimeToLive;

		// Token: 0x04005C2A RID: 23594
		private readonly ContainerPoolContainerFactory.TimerFactory timerFactory;

		// Token: 0x04005C2B RID: 23595
		private readonly CacheStats preferredCacheStats;

		// Token: 0x04005C2C RID: 23596
		private IContainerFactory factory;

		// Token: 0x04005C2D RID: 23597
		private Dictionary<int, ContainerPoolContainerFactory.PooledContainer> pool;

		// Token: 0x04005C2E RID: 23598
		private OrderedDictionary free;

		// Token: 0x04005C2F RID: 23599
		private Timer timer;

		// Token: 0x04005C30 RID: 23600
		private bool startingContainers;

		// Token: 0x02001C5B RID: 7259
		// (Invoke) Token: 0x0600B4F8 RID: 46328
		public delegate Timer TimerFactory(TimerCallback callback, object state, long dueTime, long period);

		// Token: 0x02001C5C RID: 7260
		private class PooledContainer
		{
			// Token: 0x0600B4FB RID: 46331 RVA: 0x0024BAF9 File Offset: 0x00249CF9
			public PooledContainer(IContainer instance, TimeSpan? timeToLive)
			{
				this.instance = instance;
				this.timeToLive = timeToLive;
				this.createdTime = DateTime.UtcNow;
			}

			// Token: 0x17002D39 RID: 11577
			// (get) Token: 0x0600B4FC RID: 46332 RVA: 0x0024BB1A File Offset: 0x00249D1A
			public IContainer Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x17002D3A RID: 11578
			// (get) Token: 0x0600B4FD RID: 46333 RVA: 0x0024BB24 File Offset: 0x00249D24
			public TimeSpan RemainingTimeToLive
			{
				get
				{
					if (this.timeToLive == null)
					{
						return TimeSpan.MaxValue;
					}
					TimeSpan timeSpan = DateTime.UtcNow - this.createdTime;
					TimeSpan timeSpan2 = this.timeToLive.Value - timeSpan;
					if (timeSpan2 < TimeSpan.Zero)
					{
						return TimeSpan.Zero;
					}
					return timeSpan2;
				}
			}

			// Token: 0x17002D3B RID: 11579
			// (get) Token: 0x0600B4FE RID: 46334 RVA: 0x0024BB7B File Offset: 0x00249D7B
			public bool IsUsable
			{
				get
				{
					return this.RemainingTimeToLive > TimeSpan.Zero && this.instance.IsHealthy;
				}
			}

			// Token: 0x04005C31 RID: 23601
			private readonly IContainer instance;

			// Token: 0x04005C32 RID: 23602
			private readonly TimeSpan? timeToLive;

			// Token: 0x04005C33 RID: 23603
			private readonly DateTime createdTime;
		}

		// Token: 0x02001C5D RID: 7261
		private sealed class Container : DelegatingContainer
		{
			// Token: 0x0600B4FF RID: 46335 RVA: 0x0024BB9C File Offset: 0x00249D9C
			public Container(ContainerPoolContainerFactory factory, ContainerPoolContainerFactory.PooledContainer pooledContainer)
				: base(pooledContainer.Instance)
			{
				this.factory = factory;
				this.pooledContainer = pooledContainer;
			}

			// Token: 0x17002D3C RID: 11580
			// (get) Token: 0x0600B500 RID: 46336 RVA: 0x0024BBB8 File Offset: 0x00249DB8
			public override bool IsHealthy
			{
				get
				{
					ContainerPoolContainerFactory.PooledContainer pooledContainer = this.pooledContainer;
					bool flag2;
					lock (pooledContainer)
					{
						flag2 = this.killed;
					}
					return !flag2 && base.IsHealthy;
				}
			}

			// Token: 0x0600B501 RID: 46337 RVA: 0x0024BC04 File Offset: 0x00249E04
			public override void Kill()
			{
				ContainerPoolContainerFactory.PooledContainer pooledContainer = null;
				ContainerPoolContainerFactory.PooledContainer pooledContainer2 = this.pooledContainer;
				lock (pooledContainer2)
				{
					if (!this.killed && !this.disposed)
					{
						pooledContainer = this.pooledContainer;
						this.killed = true;
					}
				}
				if (pooledContainer != null)
				{
					this.factory.KillContainer(pooledContainer);
				}
			}

			// Token: 0x0600B502 RID: 46338 RVA: 0x0024BC70 File Offset: 0x00249E70
			public override void Dispose()
			{
				ContainerPoolContainerFactory.PooledContainer pooledContainer = null;
				ContainerPoolContainerFactory.PooledContainer pooledContainer2 = this.pooledContainer;
				lock (pooledContainer2)
				{
					if (!this.disposed)
					{
						pooledContainer = this.pooledContainer;
						this.disposed = true;
					}
				}
				if (pooledContainer != null)
				{
					this.factory.FreeContainer(pooledContainer);
				}
			}

			// Token: 0x04005C34 RID: 23604
			private readonly ContainerPoolContainerFactory factory;

			// Token: 0x04005C35 RID: 23605
			private readonly ContainerPoolContainerFactory.PooledContainer pooledContainer;

			// Token: 0x04005C36 RID: 23606
			private bool killed;

			// Token: 0x04005C37 RID: 23607
			private bool disposed;
		}
	}
}
