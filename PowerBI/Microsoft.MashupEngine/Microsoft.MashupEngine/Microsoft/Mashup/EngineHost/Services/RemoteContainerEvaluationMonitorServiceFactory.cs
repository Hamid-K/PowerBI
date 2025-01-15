using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001C3C RID: 7228
	public class RemoteContainerEvaluationMonitorServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600B471 RID: 46193 RVA: 0x00249858 File Offset: 0x00247A58
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IContainerEvaluationMonitorService containerEvaluationMonitorService = engineHost.QueryService<IContainerEvaluationMonitorService>();
			proxyInitArgs.WriteBool(containerEvaluationMonitorService != null);
			if (containerEvaluationMonitorService != null)
			{
				proxyInitArgs.WriteInt32((int)ProcessHelpers.CurrentProcessId);
				return new RemoteContainerEvaluationMonitorServiceFactory.Stub(containerEvaluationMonitorService, engineHost, messenger);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600B472 RID: 46194 RVA: 0x00249894 File Offset: 0x00247A94
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				SafeHandle safeHandle = ProcessHelpers.OpenProcess((uint)proxyInitArgs.ReadInt32());
				return new RemoteContainerEvaluationMonitorServiceFactory.Proxy(engineHost, messenger, safeHandle);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x02001C3D RID: 7229
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IContainerEvaluationMonitorService
		{
			// Token: 0x0600B474 RID: 46196 RVA: 0x002498C3 File Offset: 0x00247AC3
			public Proxy(IEngineHost engineHost, IMessenger messenger, SafeHandle remoteHandle)
			{
				this.messenger = messenger;
				this.remoteHandle = remoteHandle;
			}

			// Token: 0x0600B475 RID: 46197 RVA: 0x002498DC File Offset: 0x00247ADC
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IContainerEvaluationMonitorService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600B476 RID: 46198 RVA: 0x00249914 File Offset: 0x00247B14
			public void Dispose()
			{
				this.remoteHandle.Dispose();
			}

			// Token: 0x0600B477 RID: 46199 RVA: 0x00249924 File Offset: 0x00247B24
			public IDisposable BeginEvaluation(SafeHandle processHandle)
			{
				IDisposable disposable;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					using (RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationMessage beginEvaluationMessage = new RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationMessage(processHandle, this.remoteHandle))
					{
						messageChannel.Post(beginEvaluationMessage);
						messageChannel.WaitFor<RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationInvokedMessage>();
					}
					disposable = new RemoteContainerEvaluationMonitorServiceFactory.DisposableCallback(this.messenger);
				}
				return disposable;
			}

			// Token: 0x04005BBD RID: 23485
			private readonly IMessenger messenger;

			// Token: 0x04005BBE RID: 23486
			private SafeHandle remoteHandle;
		}

		// Token: 0x02001C3E RID: 7230
		private sealed class DisposableCallback : IDisposable
		{
			// Token: 0x0600B478 RID: 46200 RVA: 0x0024999C File Offset: 0x00247B9C
			public DisposableCallback(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600B479 RID: 46201 RVA: 0x002499AC File Offset: 0x00247BAC
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.disposed = true;
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemoteContainerEvaluationMonitorServiceFactory.DisposeMessage());
					}
				}
			}

			// Token: 0x04005BBF RID: 23487
			private readonly IMessenger messenger;

			// Token: 0x04005BC0 RID: 23488
			private bool disposed;
		}

		// Token: 0x02001C3F RID: 7231
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600B47A RID: 46202 RVA: 0x002499FC File Offset: 0x00247BFC
			public Stub(IContainerEvaluationMonitorService service, IEngineHost engineHost, IMessenger messenger)
			{
				this.service = service;
				this.engineHost = engineHost;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationMessage>(this.OnBeginEvaluation));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteContainerEvaluationMonitorServiceFactory.DisposeMessage>(this.OnCallbackDispose));
				this.activeHandlesLock = new object();
			}

			// Token: 0x0600B47B RID: 46203 RVA: 0x00249A60 File Offset: 0x00247C60
			private void OnBeginEvaluation(IMessageChannel channel, RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationMessage message)
			{
				try
				{
					this.callback = this.service.BeginEvaluation(message.ProcessHandle);
					channel.Post(new RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationInvokedMessage());
				}
				finally
				{
					if (message != null)
					{
						((IDisposable)message).Dispose();
					}
				}
			}

			// Token: 0x0600B47C RID: 46204 RVA: 0x00249AB0 File Offset: 0x00247CB0
			private void OnCallbackDispose(IMessageChannel channel, RemoteContainerEvaluationMonitorServiceFactory.DisposeMessage message)
			{
				this.callback.Dispose();
			}

			// Token: 0x0600B47D RID: 46205 RVA: 0x00249ABD File Offset: 0x00247CBD
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteContainerEvaluationMonitorServiceFactory.BeginEvaluationMessage>();
				this.messenger.RemoveHandler<RemoteContainerEvaluationMonitorServiceFactory.DisposeMessage>();
				this.messenger = null;
				this.engineHost = null;
				this.service = null;
			}

			// Token: 0x04005BC1 RID: 23489
			private readonly object activeHandlesLock;

			// Token: 0x04005BC2 RID: 23490
			private IDisposable callback;

			// Token: 0x04005BC3 RID: 23491
			private IContainerEvaluationMonitorService service;

			// Token: 0x04005BC4 RID: 23492
			private IEngineHost engineHost;

			// Token: 0x04005BC5 RID: 23493
			private IMessenger messenger;
		}

		// Token: 0x02001C40 RID: 7232
		private sealed class BeginEvaluationMessage : BufferedMessage, IDisposable
		{
			// Token: 0x0600B47E RID: 46206 RVA: 0x00249AEA File Offset: 0x00247CEA
			public BeginEvaluationMessage(SafeHandle processHandle, SafeHandle remoteHandle)
			{
				this.ProcessHandle = processHandle;
				this.remoteHandle = remoteHandle;
			}

			// Token: 0x17002D24 RID: 11556
			// (get) Token: 0x0600B47F RID: 46207 RVA: 0x00249B00 File Offset: 0x00247D00
			// (set) Token: 0x0600B480 RID: 46208 RVA: 0x00249B08 File Offset: 0x00247D08
			public SafeHandle ProcessHandle { get; private set; }

			// Token: 0x0600B481 RID: 46209 RVA: 0x00249B11 File Offset: 0x00247D11
			public override void Serialize(BinaryWriter writer)
			{
				this.disposableHandle = ProcessHelpers.DuplicateRemoteHandle(ProcessHelpers.CurrentProcess, this.ProcessHandle.DangerousGetHandle(), this.remoteHandle.DangerousGetHandle());
				writer.WriteInt64((long)this.disposableHandle.DangerousGetHandle());
			}

			// Token: 0x0600B482 RID: 46210 RVA: 0x00249B50 File Offset: 0x00247D50
			public override void Deserialize(BinaryReader reader)
			{
				IntPtr intPtr = (IntPtr)reader.ReadInt64();
				this.ProcessHandle = ProcessHelpers.DuplicateRemoteHandle(ProcessHelpers.CurrentProcess, intPtr);
				this.disposableHandle = this.ProcessHandle;
			}

			// Token: 0x0600B483 RID: 46211 RVA: 0x00249B86 File Offset: 0x00247D86
			public void Dispose()
			{
				if (this.disposableHandle != null)
				{
					this.disposableHandle.Dispose();
					this.disposableHandle = null;
				}
			}

			// Token: 0x04005BC6 RID: 23494
			private readonly SafeHandle remoteHandle;

			// Token: 0x04005BC7 RID: 23495
			private SafeHandle disposableHandle;
		}

		// Token: 0x02001C41 RID: 7233
		private sealed class DisposeMessage : BufferedMessage
		{
			// Token: 0x0600B484 RID: 46212 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600B485 RID: 46213 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001C42 RID: 7234
		private sealed class BeginEvaluationInvokedMessage : BufferedMessage
		{
			// Token: 0x0600B487 RID: 46215 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600B488 RID: 46216 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
