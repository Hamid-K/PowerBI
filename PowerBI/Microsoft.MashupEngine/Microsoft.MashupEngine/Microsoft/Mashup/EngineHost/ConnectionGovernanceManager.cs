using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001951 RID: 6481
	public class ConnectionGovernanceManager
	{
		// Token: 0x0600A452 RID: 42066 RVA: 0x002205AC File Offset: 0x0021E7AC
		public ConnectionGovernanceManager()
		{
			this.governorsLock = new object();
			this.detectorLock = new object();
			this.resourceGovernors = new Dictionary<Resource, ConnectionGovernanceManager.ResourceGovernor>();
			this.deadlockDetector = new DeadlockDetector<GlobalThreadId, ConnectionGovernanceManager.ResourceGovernor>();
		}

		// Token: 0x0600A453 RID: 42067 RVA: 0x002205E0 File Offset: 0x0021E7E0
		public void SetMaxActiveConnections(IResource resource, int? count)
		{
			if (count != null && count.Value < 1)
			{
				throw new ArgumentException();
			}
			object obj = this.governorsLock;
			lock (obj)
			{
				this.GetResourceGovernor(ConnectionGovernanceManager.MakeResource(resource)).SetMaxActiveConnections(count);
			}
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x00220648 File Offset: 0x0021E848
		public ITask<IDisposable> BeginGetGovernedHandle(IResource resource, GlobalThreadId threadId)
		{
			object obj = this.governorsLock;
			ConnectionGovernanceManager.ResourceGovernor governor;
			lock (obj)
			{
				governor = this.GetResourceGovernor(ConnectionGovernanceManager.MakeResource(resource));
			}
			Action <>9__1;
			return governor.BeginGetGovernedHandle(threadId).ContinueWith<IDisposable>(delegate(ITask<IDisposable> task)
			{
				IDisposable result = task.Result;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						object obj2 = this.governorsLock;
						lock (obj2)
						{
							if (governor.Inactive)
							{
								this.resourceGovernors.Remove(governor.Resource);
							}
						}
					});
				}
				return new ConnectionGovernanceManager.AfterDispose(result, action);
			});
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x002206C0 File Offset: 0x0021E8C0
		private ConnectionGovernanceManager.ResourceGovernor GetResourceGovernor(Resource resource)
		{
			ConnectionGovernanceManager.ResourceGovernor resourceGovernor;
			if (!this.resourceGovernors.TryGetValue(resource, out resourceGovernor))
			{
				resourceGovernor = new ConnectionGovernanceManager.ResourceGovernor(resource, this);
				this.resourceGovernors.Add(resource, resourceGovernor);
			}
			return resourceGovernor;
		}

		// Token: 0x0600A456 RID: 42070 RVA: 0x002206F4 File Offset: 0x0021E8F4
		private void BreakIfDeadlocked()
		{
			object obj = this.detectorLock;
			DeadlockDetector<GlobalThreadId, ConnectionGovernanceManager.ResourceGovernor> deadlockDetector;
			lock (obj)
			{
				if (!this.deadlockDetector.IsDeadlocked())
				{
					return;
				}
				deadlockDetector = this.deadlockDetector.Clone();
			}
			do
			{
				GlobalThreadId globalThreadId = deadlockDetector.PickVictim();
				foreach (KeyValuePair<ConnectionGovernanceManager.ResourceGovernor, int> keyValuePair in deadlockDetector.GetPending(globalThreadId))
				{
					keyValuePair.Key.Cancel(globalThreadId);
				}
				deadlockDetector.Cancels(globalThreadId);
			}
			while (deadlockDetector.IsDeadlocked());
		}

		// Token: 0x0600A457 RID: 42071 RVA: 0x002207A8 File Offset: 0x0021E9A8
		private static Resource MakeResource(IResource resource)
		{
			if (resource is Resource)
			{
				return (Resource)resource;
			}
			return new Resource(resource);
		}

		// Token: 0x04005594 RID: 21908
		private readonly object governorsLock;

		// Token: 0x04005595 RID: 21909
		private readonly object detectorLock;

		// Token: 0x04005596 RID: 21910
		private readonly Dictionary<Resource, ConnectionGovernanceManager.ResourceGovernor> resourceGovernors;

		// Token: 0x04005597 RID: 21911
		private readonly DeadlockDetector<GlobalThreadId, ConnectionGovernanceManager.ResourceGovernor> deadlockDetector;

		// Token: 0x02001952 RID: 6482
		private class ResourceGovernor
		{
			// Token: 0x0600A458 RID: 42072 RVA: 0x002207BF File Offset: 0x0021E9BF
			public ResourceGovernor(Resource resource, ConnectionGovernanceManager manager)
			{
				this.governorLock = new object();
				this.resource = resource;
				this.manager = manager;
				this.pendingRequests = new Queue<ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask>();
			}

			// Token: 0x170029EE RID: 10734
			// (get) Token: 0x0600A459 RID: 42073 RVA: 0x002207EB File Offset: 0x0021E9EB
			public Resource Resource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x170029EF RID: 10735
			// (get) Token: 0x0600A45A RID: 42074 RVA: 0x002207F4 File Offset: 0x0021E9F4
			public bool Inactive
			{
				get
				{
					object obj = this.governorLock;
					bool flag2;
					lock (obj)
					{
						flag2 = this.maxActiveConnections == null && this.currentActiveConnections == 0 && this.pendingRequests.Count == 0;
					}
					return flag2;
				}
			}

			// Token: 0x0600A45B RID: 42075 RVA: 0x00220858 File Offset: 0x0021EA58
			public void SetMaxActiveConnections(int? count)
			{
				object obj = this.governorLock;
				lock (obj)
				{
					object detectorLock = this.manager.detectorLock;
					lock (detectorLock)
					{
						this.manager.deadlockDetector.SetAvailability(this, count - this.currentActiveConnections);
					}
					this.maxActiveConnections = count;
					this.Dispatch();
				}
			}

			// Token: 0x0600A45C RID: 42076 RVA: 0x00220910 File Offset: 0x0021EB10
			public ITask<IDisposable> BeginGetGovernedHandle(GlobalThreadId id)
			{
				object obj = this.governorLock;
				ITask<IDisposable> task;
				lock (obj)
				{
					object detectorLock = this.manager.detectorLock;
					lock (detectorLock)
					{
						this.manager.deadlockDetector.Requests(id, this, 1);
					}
					if (this.TryAcquire(id))
					{
						ConnectionGovernanceManager.ResourceGovernor.GovernedHandle governedHandle = new ConnectionGovernanceManager.ResourceGovernor.GovernedHandle(id, this);
						SimpleTask<IDisposable> simpleTask = new SimpleTask<IDisposable>();
						simpleTask.SetResult(governedHandle);
						task = simpleTask;
					}
					else
					{
						ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask governedHandleTask = new ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask(id);
						this.pendingRequests.Enqueue(governedHandleTask);
						this.manager.BreakIfDeadlocked();
						task = governedHandleTask;
					}
				}
				return task;
			}

			// Token: 0x0600A45D RID: 42077 RVA: 0x002209D4 File Offset: 0x0021EBD4
			public void Cancel(GlobalThreadId id)
			{
				object obj = this.governorLock;
				lock (obj)
				{
					IEnumerable<ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask> enumerable = this.pendingRequests;
					Func<ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask, bool> <>9__0;
					Func<ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask t) => id.Equals(t.Id));
					}
					foreach (ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask governedHandleTask in enumerable.Where(func))
					{
						if (!governedHandleTask.IsCompleted)
						{
							object detectorLock = this.manager.detectorLock;
							lock (detectorLock)
							{
								this.manager.deadlockDetector.Cancels(id, this, 1);
							}
							governedHandleTask.SetException(new DeadlockException(Strings.ConnectionGovernance_DeadlockAborted));
						}
					}
				}
			}

			// Token: 0x0600A45E RID: 42078 RVA: 0x00220ADC File Offset: 0x0021ECDC
			private void Dispatch()
			{
				while (this.pendingRequests.Count > 0)
				{
					ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask governedHandleTask = this.pendingRequests.Peek();
					if (governedHandleTask.IsCompleted)
					{
						this.pendingRequests.Dequeue();
					}
					else
					{
						if (!this.TryAcquire(governedHandleTask.Id))
						{
							break;
						}
						this.pendingRequests.Dequeue().SetResult(new ConnectionGovernanceManager.ResourceGovernor.GovernedHandle(governedHandleTask.Id, this));
					}
				}
			}

			// Token: 0x0600A45F RID: 42079 RVA: 0x00220B48 File Offset: 0x0021ED48
			private bool TryAcquire(GlobalThreadId id)
			{
				if (this.maxActiveConnections == null || this.currentActiveConnections < this.maxActiveConnections.Value)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ConnectionGovernanceManager/ResourceGovernor/TryAcquire", null, TraceEventType.Information, null))
					{
						this.currentActiveConnections++;
						hostTrace.AddResource(this.resource);
						hostTrace.Add("Current", this.currentActiveConnections, false);
						hostTrace.Add("Max", this.maxActiveConnections, false);
						object detectorLock = this.manager.detectorLock;
						lock (detectorLock)
						{
							this.manager.deadlockDetector.Gets(id, this, 1);
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600A460 RID: 42080 RVA: 0x00220C2C File Offset: 0x0021EE2C
			private void Release(GlobalThreadId id)
			{
				object obj = this.governorLock;
				lock (obj)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ConnectionGovernanceManager/ResourceGovernor/Release", null, TraceEventType.Information, null))
					{
						this.currentActiveConnections--;
						hostTrace.AddResource(this.resource);
						hostTrace.Add("Current", this.currentActiveConnections, false);
						hostTrace.Add("Max", this.maxActiveConnections, false);
						object detectorLock = this.manager.detectorLock;
						lock (detectorLock)
						{
							this.manager.deadlockDetector.Releases(id, this, 1);
						}
						this.Dispatch();
					}
				}
			}

			// Token: 0x04005598 RID: 21912
			private readonly object governorLock;

			// Token: 0x04005599 RID: 21913
			private readonly Resource resource;

			// Token: 0x0400559A RID: 21914
			private readonly Queue<ConnectionGovernanceManager.ResourceGovernor.GovernedHandleTask> pendingRequests;

			// Token: 0x0400559B RID: 21915
			private readonly ConnectionGovernanceManager manager;

			// Token: 0x0400559C RID: 21916
			private int? maxActiveConnections;

			// Token: 0x0400559D RID: 21917
			private int currentActiveConnections;

			// Token: 0x02001953 RID: 6483
			private sealed class GovernedHandleTask : SimpleTask<IDisposable>
			{
				// Token: 0x0600A461 RID: 42081 RVA: 0x00220D1C File Offset: 0x0021EF1C
				public GovernedHandleTask(GlobalThreadId id)
				{
					this.id = id;
				}

				// Token: 0x170029F0 RID: 10736
				// (get) Token: 0x0600A462 RID: 42082 RVA: 0x00220D2B File Offset: 0x0021EF2B
				public GlobalThreadId Id
				{
					get
					{
						return this.id;
					}
				}

				// Token: 0x0400559E RID: 21918
				private readonly GlobalThreadId id;
			}

			// Token: 0x02001954 RID: 6484
			private sealed class GovernedHandle : IDisposable
			{
				// Token: 0x0600A463 RID: 42083 RVA: 0x00220D33 File Offset: 0x0021EF33
				public GovernedHandle(GlobalThreadId id, ConnectionGovernanceManager.ResourceGovernor governor)
				{
					this.id = id;
					this.governor = governor;
					this.disposed = false;
				}

				// Token: 0x0600A464 RID: 42084 RVA: 0x00220D50 File Offset: 0x0021EF50
				public void Dispose()
				{
					if (!this.disposed)
					{
						this.governor.Release(this.id);
						this.disposed = true;
					}
				}

				// Token: 0x0400559F RID: 21919
				private readonly ConnectionGovernanceManager.ResourceGovernor governor;

				// Token: 0x040055A0 RID: 21920
				private readonly GlobalThreadId id;

				// Token: 0x040055A1 RID: 21921
				private bool disposed;
			}
		}

		// Token: 0x02001956 RID: 6486
		private sealed class AfterDispose : IDisposable
		{
			// Token: 0x0600A467 RID: 42087 RVA: 0x00220D85 File Offset: 0x0021EF85
			public AfterDispose(IDisposable inner, Action afterDispose)
			{
				this.inner = inner;
				this.afterDispose = afterDispose;
			}

			// Token: 0x0600A468 RID: 42088 RVA: 0x00220D9B File Offset: 0x0021EF9B
			public void Dispose()
			{
				if (this.inner != null)
				{
					this.inner.Dispose();
					this.afterDispose();
					this.inner = null;
				}
			}

			// Token: 0x040055A4 RID: 21924
			private readonly Action afterDispose;

			// Token: 0x040055A5 RID: 21925
			private IDisposable inner;
		}
	}
}
