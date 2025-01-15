using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A5D RID: 6749
	internal class RemoteConnectionGovernanceServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA6E RID: 43630 RVA: 0x00232ED0 File Offset: 0x002310D0
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IConnectionGovernanceService connectionGovernanceService = engineHost.QueryService<IConnectionGovernanceService>();
			proxyInitArgs.WriteBool(connectionGovernanceService != null);
			if (connectionGovernanceService != null)
			{
				return new RemoteConnectionGovernanceServiceFactory.Stub(connectionGovernanceService, engineHost, messenger);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AA6F RID: 43631 RVA: 0x00232EFF File Offset: 0x002310FF
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new RemoteConnectionGovernanceServiceFactory.Proxy(engineHost, messenger);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x02001A5E RID: 6750
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IConnectionGovernanceService
		{
			// Token: 0x0600AA71 RID: 43633 RVA: 0x00232F18 File Offset: 0x00231118
			public Proxy(IEngineHost engineHost, IMessenger messenger)
			{
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteConnectionGovernanceServiceFactory.AvailableMessage>(this.OnAvailable));
				this.pendingTasksLock = new object();
				this.pendingTasks = new Dictionary<int, RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandleTask>();
				this.nextId = 0;
			}

			// Token: 0x0600AA72 RID: 43634 RVA: 0x00232F68 File Offset: 0x00231168
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IConnectionGovernanceService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AA73 RID: 43635 RVA: 0x00232FA0 File Offset: 0x002311A0
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteConnectionGovernanceServiceFactory.AvailableMessage>();
			}

			// Token: 0x0600AA74 RID: 43636 RVA: 0x00232FB0 File Offset: 0x002311B0
			public ITask<IDisposable> BeginGetGovernedHandle(IResource resource, GlobalThreadId threadId)
			{
				IMessageChannel messageChannel = this.messenger.CreateChannel();
				RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandleTask remoteGovernedHandleTask = new RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandleTask(messageChannel);
				object obj = this.pendingTasksLock;
				int num2;
				lock (obj)
				{
					int num = this.nextId;
					this.nextId = num + 1;
					num2 = num;
					this.pendingTasks.Add(num2, remoteGovernedHandleTask);
				}
				messageChannel.Post(new RemoteConnectionGovernanceServiceFactory.BeginGetGovernedHandleRequestMessage
				{
					RequestId = num2,
					ThreadId = threadId,
					Resource = resource
				});
				return remoteGovernedHandleTask;
			}

			// Token: 0x0600AA75 RID: 43637 RVA: 0x00233044 File Offset: 0x00231244
			public void OnAvailable(IMessageChannel channel, RemoteConnectionGovernanceServiceFactory.AvailableMessage message)
			{
				object obj = this.pendingTasksLock;
				RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandleTask remoteGovernedHandleTask;
				lock (obj)
				{
					remoteGovernedHandleTask = this.pendingTasks[message.Id];
					this.pendingTasks.Remove(message.Id);
				}
				if (message.Exception != null)
				{
					remoteGovernedHandleTask.SetException(message.Exception);
					return;
				}
				remoteGovernedHandleTask.SetResult(new RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandle(this.messenger, message.Id));
			}

			// Token: 0x04005879 RID: 22649
			private readonly IMessenger messenger;

			// Token: 0x0400587A RID: 22650
			private readonly object pendingTasksLock;

			// Token: 0x0400587B RID: 22651
			private readonly Dictionary<int, RemoteConnectionGovernanceServiceFactory.RemoteGovernedHandleTask> pendingTasks;

			// Token: 0x0400587C RID: 22652
			private int nextId;
		}

		// Token: 0x02001A5F RID: 6751
		private sealed class RemoteGovernedHandleTask : SimpleTask<IDisposable>
		{
			// Token: 0x0600AA76 RID: 43638 RVA: 0x002330D0 File Offset: 0x002312D0
			public RemoteGovernedHandleTask(IMessageChannel channel)
			{
				this.channelLock = new object();
				this.channel = channel;
			}

			// Token: 0x0600AA77 RID: 43639 RVA: 0x002330EC File Offset: 0x002312EC
			public override void Wait()
			{
				IMessageChannel messageChannel = this.TakeChannel();
				if (messageChannel != null)
				{
					messageChannel.WaitFor<RemoteConnectionGovernanceServiceFactory.EndGetGovernedHandleResponseMessage>();
					messageChannel.Dispose();
					return;
				}
				base.Wait();
			}

			// Token: 0x0600AA78 RID: 43640 RVA: 0x00233118 File Offset: 0x00231318
			public override void Dispose()
			{
				IMessageChannel messageChannel = this.TakeChannel();
				if (messageChannel != null)
				{
					try
					{
						messageChannel.Dispose();
					}
					catch (InvalidOperationException)
					{
					}
				}
				base.Dispose();
			}

			// Token: 0x0600AA79 RID: 43641 RVA: 0x00233150 File Offset: 0x00231350
			private IMessageChannel TakeChannel()
			{
				object obj = this.channelLock;
				IMessageChannel messageChannel2;
				lock (obj)
				{
					IMessageChannel messageChannel = null;
					if (this.channel != null)
					{
						messageChannel = this.channel;
						this.channel = null;
					}
					messageChannel2 = messageChannel;
				}
				return messageChannel2;
			}

			// Token: 0x0400587D RID: 22653
			private readonly object channelLock;

			// Token: 0x0400587E RID: 22654
			private IMessageChannel channel;
		}

		// Token: 0x02001A60 RID: 6752
		private sealed class RemoteGovernedHandle : IDisposable
		{
			// Token: 0x0600AA7A RID: 43642 RVA: 0x002331A8 File Offset: 0x002313A8
			public RemoteGovernedHandle(IMessenger messenger, int id)
			{
				this.messenger = messenger;
				this.id = id;
				this.disposed = false;
			}

			// Token: 0x0600AA7B RID: 43643 RVA: 0x002331C8 File Offset: 0x002313C8
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.disposed = true;
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemoteConnectionGovernanceServiceFactory.DisposeHandleRequestMessage
						{
							Id = this.id
						});
					}
				}
			}

			// Token: 0x0400587F RID: 22655
			private readonly IMessenger messenger;

			// Token: 0x04005880 RID: 22656
			private readonly int id;

			// Token: 0x04005881 RID: 22657
			private bool disposed;
		}

		// Token: 0x02001A61 RID: 6753
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AA7C RID: 43644 RVA: 0x00233224 File Offset: 0x00231424
			public Stub(IConnectionGovernanceService service, IEngineHost engineHost, IMessenger messenger)
			{
				this.service = service;
				this.engineHost = engineHost;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteConnectionGovernanceServiceFactory.BeginGetGovernedHandleRequestMessage>(this.OnBeginGetGovernedHandle));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteConnectionGovernanceServiceFactory.DisposeHandleRequestMessage>(this.OnDisposeHandle));
				this.activeHandlesLock = new object();
				this.activeHandles = new Dictionary<int, IDisposable>();
			}

			// Token: 0x0600AA7D RID: 43645 RVA: 0x00233290 File Offset: 0x00231490
			private void OnBeginGetGovernedHandle(IMessageChannel channel, RemoteConnectionGovernanceServiceFactory.BeginGetGovernedHandleRequestMessage message)
			{
				channel.TakeOwnership();
				ITask continuation = null;
				continuation = this.service.BeginGetGovernedHandle(message.Resource, message.ThreadId).ContinueWith<object>(delegate(ITask<IDisposable> task)
				{
					object obj2;
					try
					{
						object obj = this.activeHandlesLock;
						lock (obj)
						{
							if (this.activeHandles == null)
							{
								if (!task.IsFaulted)
								{
									task.Result.Dispose();
								}
								obj2 = null;
							}
							else
							{
								using (IMessageChannel messageChannel = this.messenger.CreateChannel())
								{
									if (!task.IsFaulted)
									{
										this.activeHandles.Add(message.RequestId, task.Result);
									}
									try
									{
										messageChannel.Post(new RemoteConnectionGovernanceServiceFactory.AvailableMessage
										{
											Id = message.RequestId,
											Exception = (task.IsFaulted ? task.Exception.ToCallbackException() : null)
										});
										channel.Post(new RemoteConnectionGovernanceServiceFactory.EndGetGovernedHandleResponseMessage());
									}
									catch (Exception ex)
									{
										using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteConnectionGovernanceService/Stub/OnAvailable/ChannelFailure", null, TraceEventType.Information, null))
										{
											if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
											{
												throw;
											}
										}
									}
								}
								obj2 = null;
							}
						}
					}
					finally
					{
						channel.Dispose();
						task.Dispose();
						continuation.Dispose();
					}
					return obj2;
				});
			}

			// Token: 0x0600AA7E RID: 43646 RVA: 0x00233304 File Offset: 0x00231504
			private void OnDisposeHandle(IMessageChannel channel, RemoteConnectionGovernanceServiceFactory.DisposeHandleRequestMessage message)
			{
				IDisposable disposable = null;
				object obj = this.activeHandlesLock;
				lock (obj)
				{
					if (this.activeHandles.TryGetValue(message.Id, out disposable))
					{
						this.activeHandles.Remove(message.Id);
					}
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x0600AA7F RID: 43647 RVA: 0x00233370 File Offset: 0x00231570
			public void Dispose()
			{
				object obj = this.activeHandlesLock;
				Dictionary<int, IDisposable> dictionary;
				lock (obj)
				{
					dictionary = this.activeHandles;
					this.activeHandles = null;
				}
				if (dictionary != null)
				{
					foreach (IDisposable disposable in dictionary.Values)
					{
						disposable.Dispose();
					}
				}
				this.messenger.RemoveHandler<RemoteConnectionGovernanceServiceFactory.BeginGetGovernedHandleRequestMessage>();
				this.messenger.RemoveHandler<RemoteConnectionGovernanceServiceFactory.DisposeHandleRequestMessage>();
				this.messenger = null;
				this.engineHost = null;
				this.service = null;
			}

			// Token: 0x04005882 RID: 22658
			private readonly object activeHandlesLock;

			// Token: 0x04005883 RID: 22659
			private Dictionary<int, IDisposable> activeHandles;

			// Token: 0x04005884 RID: 22660
			private IConnectionGovernanceService service;

			// Token: 0x04005885 RID: 22661
			private IEngineHost engineHost;

			// Token: 0x04005886 RID: 22662
			private IMessenger messenger;
		}

		// Token: 0x02001A63 RID: 6755
		private sealed class BeginGetGovernedHandleRequestMessage : BufferedMessage
		{
			// Token: 0x17002B34 RID: 11060
			// (get) Token: 0x0600AA82 RID: 43650 RVA: 0x002335D8 File Offset: 0x002317D8
			// (set) Token: 0x0600AA83 RID: 43651 RVA: 0x002335E0 File Offset: 0x002317E0
			public int RequestId { get; set; }

			// Token: 0x17002B35 RID: 11061
			// (get) Token: 0x0600AA84 RID: 43652 RVA: 0x002335E9 File Offset: 0x002317E9
			// (set) Token: 0x0600AA85 RID: 43653 RVA: 0x002335F1 File Offset: 0x002317F1
			public GlobalThreadId ThreadId { get; set; }

			// Token: 0x17002B36 RID: 11062
			// (get) Token: 0x0600AA86 RID: 43654 RVA: 0x002335FA File Offset: 0x002317FA
			// (set) Token: 0x0600AA87 RID: 43655 RVA: 0x00233602 File Offset: 0x00231802
			public IResource Resource { get; set; }

			// Token: 0x0600AA88 RID: 43656 RVA: 0x0023360C File Offset: 0x0023180C
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.RequestId);
				writer.WriteInt32(this.ThreadId.ProcessId);
				writer.WriteInt32(this.ThreadId.ManagedThreadId);
				writer.WriteIResource(this.Resource);
			}

			// Token: 0x0600AA89 RID: 43657 RVA: 0x00233659 File Offset: 0x00231859
			public override void Deserialize(BinaryReader reader)
			{
				this.RequestId = reader.ReadInt32();
				this.ThreadId = new GlobalThreadId(reader.ReadInt32(), reader.ReadInt32());
				this.Resource = reader.ReadIResource();
			}
		}

		// Token: 0x02001A64 RID: 6756
		private sealed class DisposeHandleRequestMessage : BufferedMessage
		{
			// Token: 0x17002B37 RID: 11063
			// (get) Token: 0x0600AA8B RID: 43659 RVA: 0x0023368A File Offset: 0x0023188A
			// (set) Token: 0x0600AA8C RID: 43660 RVA: 0x00233692 File Offset: 0x00231892
			public int Id { get; set; }

			// Token: 0x0600AA8D RID: 43661 RVA: 0x0023369B File Offset: 0x0023189B
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.Id);
			}

			// Token: 0x0600AA8E RID: 43662 RVA: 0x002336A9 File Offset: 0x002318A9
			public override void Deserialize(BinaryReader reader)
			{
				this.Id = reader.ReadInt32();
			}
		}

		// Token: 0x02001A65 RID: 6757
		private sealed class AvailableMessage : BufferedMessage
		{
			// Token: 0x17002B38 RID: 11064
			// (get) Token: 0x0600AA90 RID: 43664 RVA: 0x002336B7 File Offset: 0x002318B7
			// (set) Token: 0x0600AA91 RID: 43665 RVA: 0x002336BF File Offset: 0x002318BF
			public int Id { get; set; }

			// Token: 0x17002B39 RID: 11065
			// (get) Token: 0x0600AA92 RID: 43666 RVA: 0x002336C8 File Offset: 0x002318C8
			// (set) Token: 0x0600AA93 RID: 43667 RVA: 0x002336D0 File Offset: 0x002318D0
			public Exception Exception { get; set; }

			// Token: 0x0600AA94 RID: 43668 RVA: 0x002336D9 File Offset: 0x002318D9
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.Id);
				writer.WriteNullable(this.Exception, delegate(BinaryWriter w, Exception e)
				{
					w.WriteException(e);
				});
			}

			// Token: 0x0600AA95 RID: 43669 RVA: 0x00233712 File Offset: 0x00231912
			public override void Deserialize(BinaryReader reader)
			{
				this.Id = reader.ReadInt32();
				this.Exception = reader.ReadNullable((BinaryReader r) => r.ReadException());
			}
		}

		// Token: 0x02001A67 RID: 6759
		private sealed class EndGetGovernedHandleResponseMessage : BufferedMessage
		{
			// Token: 0x0600AA9B RID: 43675 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AA9C RID: 43676 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
