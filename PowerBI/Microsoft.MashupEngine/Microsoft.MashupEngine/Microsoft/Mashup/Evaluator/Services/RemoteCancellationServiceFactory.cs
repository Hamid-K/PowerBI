using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D87 RID: 7559
	public sealed class RemoteCancellationServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600BBBC RID: 48060 RVA: 0x0025FBAD File Offset: 0x0025DDAD
		private static EventWaitHandle CreateCancelEvent(string prefix)
		{
			return new EventWaitHandle(false, EventResetMode.ManualReset, prefix + "-Cancel");
		}

		// Token: 0x0600BBBD RID: 48061 RVA: 0x0025FBC1 File Offset: 0x0025DDC1
		private static EventWaitHandle CreateNonZeroCancellationsEvent(string prefix)
		{
			return new EventWaitHandle(false, EventResetMode.ManualReset, prefix + "-NonZero");
		}

		// Token: 0x0600BBBE RID: 48062 RVA: 0x0025FBD5 File Offset: 0x0025DDD5
		private static EventWaitHandle CreateZeroCancellationsEvent(string prefix)
		{
			return new EventWaitHandle(true, EventResetMode.ManualReset, prefix + "-Zero");
		}

		// Token: 0x0600BBBF RID: 48063 RVA: 0x0025FBEC File Offset: 0x0025DDEC
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			string text = Guid.NewGuid().ToString();
			proxyInitArgs.Write(text);
			ICancellationService cancellationService = engineHost.QueryService<ICancellationService>();
			RemoteCancellationServiceFactory.Stub stub = new RemoteCancellationServiceFactory.Stub(cancellationService, text, engineHost);
			cancellationService.Register(stub);
			return stub;
		}

		// Token: 0x0600BBC0 RID: 48064 RVA: 0x0025FC2A File Offset: 0x0025DE2A
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteCancellationServiceFactory.Proxy(proxyInitArgs.ReadString(), engineHost);
		}

		// Token: 0x02001D88 RID: 7560
		private sealed class Stub : IRemoteServiceStub, IDisposable, ICancellable
		{
			// Token: 0x0600BBC2 RID: 48066 RVA: 0x0025FC38 File Offset: 0x0025DE38
			public Stub(ICancellationService service, string eventPrefix, IEngineHost engineHost)
			{
				this.syncRoot = new object();
				this.cancellationService = service;
				this.engineHost = engineHost;
				this.disposed = false;
				this.cancelEvent = RemoteCancellationServiceFactory.CreateCancelEvent(eventPrefix);
				this.nonZeroCancellationsEvent = RemoteCancellationServiceFactory.CreateNonZeroCancellationsEvent(eventPrefix);
				this.zeroCancellationsEvent = RemoteCancellationServiceFactory.CreateZeroCancellationsEvent(eventPrefix);
			}

			// Token: 0x0600BBC3 RID: 48067 RVA: 0x0025FC90 File Offset: 0x0025DE90
			public void Dispose()
			{
				this.cancellationService.Unregister(this);
				object obj = this.syncRoot;
				lock (obj)
				{
					this.disposed = true;
					EventWaitHandle eventWaitHandle = this.cancelEvent;
					if (eventWaitHandle != null)
					{
						eventWaitHandle.Close();
					}
					this.cancelEvent = null;
					EventWaitHandle eventWaitHandle2 = this.nonZeroCancellationsEvent;
					if (eventWaitHandle2 != null)
					{
						eventWaitHandle2.Close();
					}
					this.nonZeroCancellationsEvent = null;
					EventWaitHandle eventWaitHandle3 = this.zeroCancellationsEvent;
					if (eventWaitHandle3 != null)
					{
						eventWaitHandle3.Set();
					}
					EventWaitHandle eventWaitHandle4 = this.zeroCancellationsEvent;
					if (eventWaitHandle4 != null)
					{
						eventWaitHandle4.Close();
					}
					this.zeroCancellationsEvent = null;
				}
			}

			// Token: 0x0600BBC4 RID: 48068 RVA: 0x0025FD38 File Offset: 0x0025DF38
			public bool Cancel()
			{
				bool flag2;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteCancellationServiceFactory/Stub/Cancel", this.engineHost, TraceEventType.Information, null))
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.disposed)
						{
							hostTrace.Add("disposed", this.disposed, false);
							return false;
						}
						EventWaitHandle eventWaitHandle = this.cancelEvent;
						if (eventWaitHandle != null)
						{
							eventWaitHandle.Set();
						}
					}
					ArrayBuilder<WaitHandle> arrayBuilder = new ArrayBuilder<WaitHandle>(3);
					arrayBuilder.Add(this.nonZeroCancellationsEvent);
					arrayBuilder.Add(this.zeroCancellationsEvent);
					ISignalEvaluationCanceled signalEvaluationCanceled = this.engineHost.QueryService<ISignalEvaluationCanceled>();
					WaitHandle waitHandle = ((signalEvaluationCanceled != null) ? signalEvaluationCanceled.EvaluationCanceled : null);
					if (waitHandle != null)
					{
						arrayBuilder.Add(waitHandle);
					}
					int num = WaitHandle.WaitAny(arrayBuilder.ToArray());
					obj = this.syncRoot;
					lock (obj)
					{
						hostTrace.Add("disposed", this.disposed, false);
						hostTrace.Add("nonZeroCancelled", num == 0, false);
						hostTrace.Add("evaluationCancelled", num == 2, false);
						flag2 = !this.disposed && num == 0;
					}
				}
				return flag2;
			}

			// Token: 0x04005F80 RID: 24448
			private readonly object syncRoot;

			// Token: 0x04005F81 RID: 24449
			private readonly ICancellationService cancellationService;

			// Token: 0x04005F82 RID: 24450
			private readonly IEngineHost engineHost;

			// Token: 0x04005F83 RID: 24451
			private bool disposed;

			// Token: 0x04005F84 RID: 24452
			private EventWaitHandle cancelEvent;

			// Token: 0x04005F85 RID: 24453
			private EventWaitHandle nonZeroCancellationsEvent;

			// Token: 0x04005F86 RID: 24454
			private EventWaitHandle zeroCancellationsEvent;
		}

		// Token: 0x02001D89 RID: 7561
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ICancellationService, ITrackingService<ICancellable>
		{
			// Token: 0x0600BBC5 RID: 48069 RVA: 0x0025FED4 File Offset: 0x0025E0D4
			public Proxy(string eventPrefix, IEngineHost engineHost)
			{
				this.syncRoot = new object();
				this.serviceDelegate = new CancellationService();
				this.engineHost = engineHost;
				this.disposed = false;
				this.cancelEvent = RemoteCancellationServiceFactory.CreateCancelEvent(eventPrefix);
				this.nonZeroCancellationsEvent = RemoteCancellationServiceFactory.CreateNonZeroCancellationsEvent(eventPrefix);
				this.zeroCancellationsEvent = RemoteCancellationServiceFactory.CreateZeroCancellationsEvent(eventPrefix);
				this.zeroCancellationsEvent.Reset();
				this.waiter = ThreadPool.RegisterWaitForSingleObject(this.cancelEvent, delegate(object _, bool __)
				{
					this.CancelAndSendCount();
				}, null, -1, true);
			}

			// Token: 0x0600BBC6 RID: 48070 RVA: 0x0025FF5C File Offset: 0x0025E15C
			private void CancelAndSendCount()
			{
				int num = this.CancelAll();
				object obj = this.syncRoot;
				lock (obj)
				{
					if (num == 0)
					{
						EventWaitHandle eventWaitHandle = this.zeroCancellationsEvent;
						if (eventWaitHandle != null)
						{
							eventWaitHandle.Set();
						}
					}
					else
					{
						EventWaitHandle eventWaitHandle2 = this.nonZeroCancellationsEvent;
						if (eventWaitHandle2 != null)
						{
							eventWaitHandle2.Set();
						}
					}
				}
			}

			// Token: 0x0600BBC7 RID: 48071 RVA: 0x0025FFC8 File Offset: 0x0025E1C8
			public void Dispose()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.disposed = true;
					if (this.waiter != null)
					{
						this.waiter.Unregister(null);
						this.waiter = null;
					}
					EventWaitHandle eventWaitHandle = this.cancelEvent;
					if (eventWaitHandle != null)
					{
						eventWaitHandle.Close();
					}
					this.cancelEvent = null;
					EventWaitHandle eventWaitHandle2 = this.nonZeroCancellationsEvent;
					if (eventWaitHandle2 != null)
					{
						eventWaitHandle2.Close();
					}
					this.nonZeroCancellationsEvent = null;
					EventWaitHandle eventWaitHandle3 = this.zeroCancellationsEvent;
					if (eventWaitHandle3 != null)
					{
						eventWaitHandle3.Set();
					}
					EventWaitHandle eventWaitHandle4 = this.zeroCancellationsEvent;
					if (eventWaitHandle4 != null)
					{
						eventWaitHandle4.Close();
					}
					this.zeroCancellationsEvent = null;
				}
			}

			// Token: 0x0600BBC8 RID: 48072 RVA: 0x00260080 File Offset: 0x0025E280
			public void Register(ICancellable task)
			{
				this.serviceDelegate.Register(task);
			}

			// Token: 0x0600BBC9 RID: 48073 RVA: 0x0026008E File Offset: 0x0025E28E
			public void Unregister(ICancellable task)
			{
				this.serviceDelegate.Unregister(task);
			}

			// Token: 0x0600BBCA RID: 48074 RVA: 0x0026009C File Offset: 0x0025E29C
			public int CancelAll()
			{
				int num2;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteCancellationServiceFactory/Proxy/CancelAll", this.engineHost, TraceEventType.Information, null))
				{
					int num = this.serviceDelegate.CancelAll();
					hostTrace.Add("cancelCount", num, false);
					hostTrace.Add("disposed", this.disposed, false);
					num2 = num;
				}
				return num2;
			}

			// Token: 0x0600BBCB RID: 48075 RVA: 0x00260110 File Offset: 0x0025E310
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(ICancellationService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x04005F87 RID: 24455
			private readonly object syncRoot;

			// Token: 0x04005F88 RID: 24456
			private readonly ICancellationService serviceDelegate;

			// Token: 0x04005F89 RID: 24457
			private readonly IEngineHost engineHost;

			// Token: 0x04005F8A RID: 24458
			private bool disposed;

			// Token: 0x04005F8B RID: 24459
			private EventWaitHandle cancelEvent;

			// Token: 0x04005F8C RID: 24460
			private EventWaitHandle nonZeroCancellationsEvent;

			// Token: 0x04005F8D RID: 24461
			private EventWaitHandle zeroCancellationsEvent;

			// Token: 0x04005F8E RID: 24462
			private RegisteredWaitHandle waiter;
		}
	}
}
