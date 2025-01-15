using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D57 RID: 7511
	internal sealed class SharedWorkingSetContainerFactory : IContainerFactory, IDisposable
	{
		// Token: 0x0600BAD4 RID: 47828 RVA: 0x0025D2DC File Offset: 0x0025B4DC
		public SharedWorkingSetContainerFactory(IProcessContainerFactory containerFactory, int sharedWorkingSetInMB)
		{
			this.syncRoot = new object();
			this.containerFactory = containerFactory;
			this.sharedWorkingSetInMB = sharedWorkingSetInMB;
			this.containers = new HashSet<SharedWorkingSetContainerFactory.Container>();
		}

		// Token: 0x0600BAD5 RID: 47829 RVA: 0x0025D308 File Offset: 0x0025B508
		public IContainer CreateContainer()
		{
			IContainer container2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("SharedWorkingSetContainerFactory/CreateContainer", null, TraceEventType.Information, null))
			{
				SharedWorkingSetContainerFactory.Container container = new SharedWorkingSetContainerFactory.Container(this);
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.containers == null)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					this.containers.Add(container);
					this.AdjustWorkingSet();
				}
				IProcessContainer processContainer = this.containerFactory.CreateProcessContainer();
				obj = this.syncRoot;
				lock (obj)
				{
					container.ProcessContainer = processContainer;
					processContainer.SetProcessWorkingSetSize(this.ProcessWorkingSetInMB);
				}
				hostTrace.Add("containerID", processContainer.ContainerID, false);
				container2 = container;
			}
			return container2;
		}

		// Token: 0x0600BAD6 RID: 47830 RVA: 0x0025D404 File Offset: 0x0025B604
		public void Dispose()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.containers = null;
			}
			this.containerFactory.Dispose();
		}

		// Token: 0x17002E1C RID: 11804
		// (get) Token: 0x0600BAD7 RID: 47831 RVA: 0x0025D450 File Offset: 0x0025B650
		private int ProcessWorkingSetInMB
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.sharedWorkingSetInMB / Math.Max(1, this.containers.Count);
				}
				return num;
			}
		}

		// Token: 0x0600BAD8 RID: 47832 RVA: 0x0025D4A4 File Offset: 0x0025B6A4
		private void AdjustWorkingSet()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				int processWorkingSetInMB = this.ProcessWorkingSetInMB;
				foreach (SharedWorkingSetContainerFactory.Container container in this.containers)
				{
					if (container.ProcessContainer != null)
					{
						container.ProcessContainer.SetProcessWorkingSetSize(processWorkingSetInMB);
					}
				}
			}
		}

		// Token: 0x0600BAD9 RID: 47833 RVA: 0x0025D538 File Offset: 0x0025B738
		private void ContainerDisposed(SharedWorkingSetContainerFactory.Container container)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.containers.Remove(container);
				this.AdjustWorkingSet();
			}
		}

		// Token: 0x04005F15 RID: 24341
		private readonly object syncRoot;

		// Token: 0x04005F16 RID: 24342
		private readonly IProcessContainerFactory containerFactory;

		// Token: 0x04005F17 RID: 24343
		private readonly int sharedWorkingSetInMB;

		// Token: 0x04005F18 RID: 24344
		private HashSet<SharedWorkingSetContainerFactory.Container> containers;

		// Token: 0x02001D58 RID: 7512
		private sealed class Container : IContainer, IDisposable
		{
			// Token: 0x0600BADA RID: 47834 RVA: 0x0025D588 File Offset: 0x0025B788
			public Container(SharedWorkingSetContainerFactory factory)
			{
				this.factory = factory;
			}

			// Token: 0x17002E1D RID: 11805
			// (get) Token: 0x0600BADB RID: 47835 RVA: 0x0025D597 File Offset: 0x0025B797
			// (set) Token: 0x0600BADC RID: 47836 RVA: 0x0025D59F File Offset: 0x0025B79F
			public IProcessContainer ProcessContainer
			{
				get
				{
					return this.container;
				}
				set
				{
					this.container = value;
				}
			}

			// Token: 0x17002E1E RID: 11806
			// (get) Token: 0x0600BADD RID: 47837 RVA: 0x0025D5A8 File Offset: 0x0025B7A8
			public int ContainerID
			{
				get
				{
					IProcessContainer processContainer = this.container;
					if (processContainer == null)
					{
						return -1;
					}
					return processContainer.ContainerID;
				}
			}

			// Token: 0x17002E1F RID: 11807
			// (get) Token: 0x0600BADE RID: 47838 RVA: 0x0025D5BB File Offset: 0x0025B7BB
			public bool IsHealthy
			{
				get
				{
					return this.container.IsHealthy;
				}
			}

			// Token: 0x17002E20 RID: 11808
			// (get) Token: 0x0600BADF RID: 47839 RVA: 0x0025D5C8 File Offset: 0x0025B7C8
			public IFeatureLoggingService Features
			{
				get
				{
					return this.container.Features;
				}
			}

			// Token: 0x17002E21 RID: 11809
			// (get) Token: 0x0600BAE0 RID: 47840 RVA: 0x0025D5D5 File Offset: 0x0025B7D5
			public IMessenger Messenger
			{
				get
				{
					return this.container.Messenger;
				}
			}

			// Token: 0x0600BAE1 RID: 47841 RVA: 0x0025D5E2 File Offset: 0x0025B7E2
			public bool TryGetAs<T>(out T result) where T : class
			{
				return this.container.TryGetAs<T>(out result);
			}

			// Token: 0x0600BAE2 RID: 47842 RVA: 0x0025D5F0 File Offset: 0x0025B7F0
			public void Kill()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("SharedWorkingSetContainerFactory/Container/Kill", null, TraceEventType.Information, null))
				{
					IHostTrace hostTrace2 = hostTrace;
					string text = "containerID";
					IProcessContainer processContainer = this.container;
					hostTrace2.Add(text, (processContainer != null) ? new int?(processContainer.ContainerID) : null, false);
					this.container.Kill();
				}
			}

			// Token: 0x0600BAE3 RID: 47843 RVA: 0x0025D664 File Offset: 0x0025B864
			public void Dispose()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("SharedWorkingSetContainerFactory/Container/Dispose", null, TraceEventType.Information, null))
				{
					IHostTrace hostTrace2 = hostTrace;
					string text = "containerID";
					IProcessContainer processContainer = this.container;
					hostTrace2.Add(text, (processContainer != null) ? new int?(processContainer.ContainerID) : null, false);
					if (this.factory != null)
					{
						this.factory.ContainerDisposed(this);
						this.factory = null;
						this.container.Dispose();
						hostTrace.Add("disposed", true, false);
					}
				}
			}

			// Token: 0x04005F19 RID: 24345
			private SharedWorkingSetContainerFactory factory;

			// Token: 0x04005F1A RID: 24346
			private IProcessContainer container;
		}
	}
}
