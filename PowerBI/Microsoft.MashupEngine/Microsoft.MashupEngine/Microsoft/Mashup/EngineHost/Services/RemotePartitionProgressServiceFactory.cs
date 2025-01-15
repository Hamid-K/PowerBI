using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AC3 RID: 6851
	internal class RemotePartitionProgressServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AC43 RID: 44099 RVA: 0x00236CF4 File Offset: 0x00234EF4
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IManyProgressReader manyProgressReader = engineHost.QueryService<IManyProgressReader>();
			IProgressService2Config progressService2Config = engineHost.QueryService<IProgressService>() as IProgressService2Config;
			Guid guid = Guid.NewGuid();
			proxyInitArgs.WriteString(guid.ToString());
			proxyInitArgs.WriteNullable((progressService2Config != null) ? progressService2Config.PartitionKey : null, delegate(BinaryWriter w, IPartitionKey p)
			{
				w.WriteIPartitionKey(p);
			});
			return new RemotePartitionProgressServiceFactory.Stub(manyProgressReader, guid);
		}

		// Token: 0x0600AC44 RID: 44100 RVA: 0x00236D64 File Offset: 0x00234F64
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			Guid guid = new Guid(proxyInitArgs.ReadString());
			IPartitionKey partitionKey = proxyInitArgs.ReadNullable((BinaryReader r) => r.ReadIPartitionKey());
			return new RemotePartitionProgressServiceFactory.Proxy(guid, partitionKey);
		}

		// Token: 0x02001AC4 RID: 6852
		private sealed class Stub : IRemoteServiceStub, IDisposable, IProgressReader
		{
			// Token: 0x0600AC46 RID: 44102 RVA: 0x00236DA8 File Offset: 0x00234FA8
			public Stub(IManyProgressReader manyProgressReader, Guid bufferIdentity)
			{
				this.buffer = new SharedMemoryBuffer(bufferIdentity, true);
				this.manyProgressReader = manyProgressReader;
				this.manyProgressReader.AddReader(this);
			}

			// Token: 0x0600AC47 RID: 44103 RVA: 0x00236DD0 File Offset: 0x00234FD0
			public void Dispose()
			{
				this.manyProgressReader.RemoveReader(this);
				this.buffer.Dispose();
				this.manyProgressReader = null;
				this.buffer = null;
			}

			// Token: 0x0600AC48 RID: 44104 RVA: 0x00236DF8 File Offset: 0x00234FF8
			public IEnumerable<byte[]> ReadAllProgress()
			{
				byte[] array;
				if (this.buffer.TryRead(out array))
				{
					return new byte[][] { array };
				}
				return new byte[0][];
			}

			// Token: 0x0400591F RID: 22815
			private IManyProgressReader manyProgressReader;

			// Token: 0x04005920 RID: 22816
			private SharedMemoryBuffer buffer;
		}

		// Token: 0x02001AC5 RID: 6853
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IRecordProgress
		{
			// Token: 0x0600AC49 RID: 44105 RVA: 0x00236E28 File Offset: 0x00235028
			public Proxy(Guid bufferIdentity, IPartitionKey currentPartitionKey)
			{
				this.syncRoot = new object();
				this.buffer = new SharedMemoryBuffer(bufferIdentity, false);
				this.partitionProgressService = new PartitionProgressService(this);
				if (currentPartitionKey != null)
				{
					this.progressService = this.partitionProgressService.GetPartitionProgressService(currentPartitionKey);
				}
			}

			// Token: 0x0600AC4A RID: 44106 RVA: 0x00236E74 File Offset: 0x00235074
			public void Dispose()
			{
				using (DisposalScope disposalScope = new DisposalScope())
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.OnUpdate(null);
						disposalScope.ClearAndDispose<Timer>(ref this.timer);
						disposalScope.ClearAndDispose<PartitionProgressService>(ref this.partitionProgressService);
						this.manyProgressReader = null;
						this.progressService = null;
					}
				}
				using (DisposalScope disposalScope2 = new DisposalScope())
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						disposalScope2.ClearAndDispose<SharedMemoryBuffer>(ref this.buffer);
					}
				}
			}

			// Token: 0x0600AC4B RID: 44107 RVA: 0x00236F48 File Offset: 0x00235148
			public void RecordProgress(byte[] progressData)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.buffer != null)
					{
						this.buffer.Write(progressData);
					}
				}
			}

			// Token: 0x0600AC4C RID: 44108 RVA: 0x00236F98 File Offset: 0x00235198
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IPartitionProgressService) || typeof(T) == typeof(IGetDataSourceProgress))
				{
					return (T)((object)this.partitionProgressService);
				}
				if (typeof(T) == typeof(IProgressService) || typeof(T) == typeof(IProgressService2))
				{
					return (T)((object)this.progressService);
				}
				if (typeof(T) == typeof(IManyProgressReader))
				{
					return (T)((object)this.CreateManyProgressReader());
				}
				return default(T);
			}

			// Token: 0x0600AC4D RID: 44109 RVA: 0x0023705C File Offset: 0x0023525C
			private IManyProgressReader CreateManyProgressReader()
			{
				object obj = this.syncRoot;
				IManyProgressReader manyProgressReader;
				lock (obj)
				{
					if (this.manyProgressReader == null && this.partitionProgressService != null)
					{
						this.manyProgressReader = new ManyProgressReader();
						this.timer = new Timer(SafeThread2.CreateTimerCallback(new TimerCallback(this.OnUpdate)), null, TimeSpan.Zero, PartitionProgressService.UpdatePeriod);
					}
					manyProgressReader = this.manyProgressReader;
				}
				return manyProgressReader;
			}

			// Token: 0x0600AC4E RID: 44110 RVA: 0x002370E0 File Offset: 0x002352E0
			private void OnUpdate(object state)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.manyProgressReader != null)
					{
						byte[] array = this.manyProgressReader.ReadAllProgress().FirstOrDefault((byte[] b) => b.Length != 0);
						if (array != null)
						{
							this.RecordProgress(array);
						}
					}
				}
			}

			// Token: 0x04005921 RID: 22817
			private readonly object syncRoot;

			// Token: 0x04005922 RID: 22818
			private PartitionProgressService partitionProgressService;

			// Token: 0x04005923 RID: 22819
			private IProgressService2 progressService;

			// Token: 0x04005924 RID: 22820
			private SharedMemoryBuffer buffer;

			// Token: 0x04005925 RID: 22821
			private ManyProgressReader manyProgressReader;

			// Token: 0x04005926 RID: 22822
			private Timer timer;
		}
	}
}
