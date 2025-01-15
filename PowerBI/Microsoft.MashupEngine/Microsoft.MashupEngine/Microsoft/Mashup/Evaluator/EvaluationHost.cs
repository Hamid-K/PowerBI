using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C9C RID: 7324
	public class EvaluationHost : IExceptionHandler2, IDisposable
	{
		// Token: 0x0600B622 RID: 46626 RVA: 0x0024F898 File Offset: 0x0024DA98
		public EvaluationHost(int containerID, Stream inputStream, Stream outputStream)
		{
			EvaluationHost.isContainer = true;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("EvaluationHost/.ctor", null, TraceEventType.Information, null))
			{
				hostTrace.Add("containerID", containerID, false);
				this.syncRoot = new object();
				this.containerID = containerID;
				this.messenger = new StreamMessenger();
				this.messenger.InputStream = inputStream;
				this.messenger.OutputStream = outputStream;
				this.messenger.AddHandler(new Action<IMessageChannel, EvaluationHost.ExceptionMessage>(this.OnException));
				this.baseChannel = this.messenger.CreateChannel();
				SafeThread2.AddExceptionHandler(this);
				EvaluatorThreadPool.Initialize(4);
			}
		}

		// Token: 0x17002D78 RID: 11640
		// (get) Token: 0x0600B623 RID: 46627 RVA: 0x0024F958 File Offset: 0x0024DB58
		public static bool IsContainer
		{
			get
			{
				return EvaluationHost.isContainer;
			}
		}

		// Token: 0x0600B624 RID: 46628 RVA: 0x0024F960 File Offset: 0x0024DB60
		public void Run()
		{
			for (;;)
			{
				using (IMessenger messenger = new ChannelMessenger(this.messenger.Handlers, this.baseChannel, false))
				{
					object obj = this.syncRoot;
					IRemoteServiceProxy[] array;
					lock (obj)
					{
						this.engineHost = RemoteServiceEnvironment.CreateServiceProxies(messenger, out array);
					}
					bool evaluationComplete = false;
					using (new RemoteDocumentEvaluator.Service(this.engineHost, messenger, delegate
					{
						evaluationComplete = true;
					}))
					{
						using (IMessageChannel messageChannel = messenger.CreateChannel())
						{
							while (!evaluationComplete)
							{
								messageChannel.Messenger.Handlers.Dispatch(messageChannel, messageChannel.Read());
							}
						}
					}
					this.CancelTasks();
					obj = this.syncRoot;
					lock (obj)
					{
						this.engineHost = null;
						RemoteServiceEnvironment.DisposeServiceProxies(messenger, array);
					}
					array = null;
				}
			}
		}

		// Token: 0x0600B625 RID: 46629 RVA: 0x0024FAE0 File Offset: 0x0024DCE0
		public void Terminate()
		{
			this.CancelTasks();
		}

		// Token: 0x0600B626 RID: 46630 RVA: 0x0024FAE8 File Offset: 0x0024DCE8
		private void CancelTasks()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.engineHost != null)
				{
					this.engineHost.QueryService<ICancellationService>().CancelAll();
				}
			}
		}

		// Token: 0x0600B627 RID: 46631 RVA: 0x0024FB3C File Offset: 0x0024DD3C
		public bool TryHandleException(Exception exception)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("EvaluationHost/TryHandleException", null, TraceEventType.Information, null))
			{
				try
				{
					if (!EvaluationHost.TryReportException(hostTrace, this.engineHost, this.baseChannel, exception))
					{
						throw exception.ToCallbackException();
					}
				}
				catch (OutOfMemoryException)
				{
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
				}
			}
			return false;
		}

		// Token: 0x0600B628 RID: 46632 RVA: 0x0024FBBC File Offset: 0x0024DDBC
		public void Dispose()
		{
			if (this.baseChannel != null)
			{
				SafeThread2.RemoveExceptionHandler(this);
				this.baseChannel.Dispose();
				this.baseChannel = null;
			}
			if (this.messenger != null)
			{
				this.messenger.RemoveHandler<EvaluationHost.ExceptionMessage>();
				this.messenger.Dispose();
				this.messenger = null;
			}
		}

		// Token: 0x0600B629 RID: 46633 RVA: 0x0024FC10 File Offset: 0x0024DE10
		public static void ReportExceptions(IHostTrace trace, IEngineHost engineHost, IMessageChannel channel, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!EvaluationHost.TryReportException(trace, engineHost, channel, ex))
				{
					throw;
				}
				if (!SafeExceptions.TraceIsSafeException(trace, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x0600B62A RID: 46634 RVA: 0x0024FC50 File Offset: 0x0024DE50
		public static void ReportExceptions(string entryName, IEngineHost engineHost, IMessageChannel channel, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace(entryName, engineHost, TraceEventType.Information, null))
				{
					if (!EvaluationHost.TryReportException(hostTrace, engineHost, channel, ex))
					{
						throw;
					}
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600B62B RID: 46635 RVA: 0x0024FCB4 File Offset: 0x0024DEB4
		public static void OnException(IEngineHost engineHost, IMessageChannel channel, EvaluationHost.ExceptionMessage message)
		{
			SerializedValueException ex = message.Exception as SerializedValueException;
			if (ex != null && engineHost != null)
			{
				ex.Throw(engineHost);
			}
			throw message.Exception.ToRemoteException();
		}

		// Token: 0x0600B62C RID: 46636 RVA: 0x0024FCE8 File Offset: 0x0024DEE8
		private static bool TryReportException(IHostTrace trace, IEngineHost engineHost, IMessageChannel channel, Exception exception)
		{
			SafeExceptions.IgnoreSafeExceptions(engineHost, trace, delegate
			{
				ValueException2 valueException = exception as ValueException2;
				if (valueException != null && engineHost != null)
				{
					exception = SerializedValueException.New(valueException, engineHost);
				}
			});
			bool sentException = false;
			SafeExceptions.IgnoreSafeExceptions(engineHost, trace, delegate
			{
				channel.Post(new EvaluationHost.ExceptionMessage
				{
					Exception = exception.ToCallbackException()
				});
				sentException = true;
			});
			return sentException;
		}

		// Token: 0x0600B62D RID: 46637 RVA: 0x0024FD4D File Offset: 0x0024DF4D
		private void OnException(IMessageChannel channel, EvaluationHost.ExceptionMessage message)
		{
			EvaluationHost.OnException(this.engineHost, channel, message);
		}

		// Token: 0x04005D08 RID: 23816
		private static bool isContainer;

		// Token: 0x04005D09 RID: 23817
		private readonly object syncRoot;

		// Token: 0x04005D0A RID: 23818
		private readonly int containerID;

		// Token: 0x04005D0B RID: 23819
		private StreamMessenger messenger;

		// Token: 0x04005D0C RID: 23820
		private IMessageChannel baseChannel;

		// Token: 0x04005D0D RID: 23821
		private IEngineHost engineHost;

		// Token: 0x02001C9D RID: 7325
		public sealed class ExceptionMessage : BufferedMessage
		{
			// Token: 0x17002D79 RID: 11641
			// (get) Token: 0x0600B62E RID: 46638 RVA: 0x0024FD5C File Offset: 0x0024DF5C
			// (set) Token: 0x0600B62F RID: 46639 RVA: 0x0024FD64 File Offset: 0x0024DF64
			public Exception Exception { get; set; }

			// Token: 0x0600B630 RID: 46640 RVA: 0x0024FD6D File Offset: 0x0024DF6D
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteException(this.Exception);
			}

			// Token: 0x0600B631 RID: 46641 RVA: 0x0024FD7B File Offset: 0x0024DF7B
			public override void Deserialize(BinaryReader reader)
			{
				this.Exception = reader.ReadException();
			}
		}
	}
}
