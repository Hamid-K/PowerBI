using System;
using System.Globalization;
using System.Threading;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF7 RID: 2807
	public class PooledQueue : IPooledQueue
	{
		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x0016D144 File Offset: 0x0016B344
		// (set) Token: 0x060058D4 RID: 22740 RVA: 0x0016D14C File Offset: 0x0016B34C
		public AutomatonQueue Automaton { get; private set; }

		// Token: 0x060058D5 RID: 22741 RVA: 0x0016D158 File Offset: 0x0016B358
		public PooledQueue(QueueConnectionParameters parameters, WrappedPooledQueueManager wrappedQueueManager)
		{
			if (parameters == null)
			{
				throw new InvalidOperationException("parameters null");
			}
			if (parameters.Name == null)
			{
				throw new InvalidOperationException("parameters.Name null");
			}
			if (wrappedQueueManager == null)
			{
				throw new InvalidOperationException("wrappedQueueManager null");
			}
			this.queueManager = wrappedQueueManager.QueueManager;
			if (this.queueManager == null)
			{
				throw new InvalidOperationException("queueManager null");
			}
			this.traceContainer = new MqTraceContainer();
			ApplicationTracePoint applicationTracePoint = new ApplicationTracePoint(this.traceContainer);
			this.tracePoint = new QueueTracePoint(applicationTracePoint);
			this.tracePoint[TracePointPropertyIdentifiers.QueueName] = parameters.Name;
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Queue Name: {0}", parameters.Name));
			}
			this.Automaton = new AutomatonQueue(this.tracePoint, Pooling.SingletonEventLogContainer);
			AutomatonQueueContext automatonQueueContext = this.Automaton.Automaton.Context as AutomatonQueueContext;
			automatonQueueContext.ConnectionParameters = parameters;
			automatonQueueContext.AutomatonQueueManager = this.queueManager.Automaton;
			AutomatonQueueManagerContext automatonQueueManagerContext = this.queueManager.Automaton.Automaton.Context as AutomatonQueueManagerContext;
			automatonQueueContext.QueueManagerConversationId = automatonQueueManagerContext.DeterminantForTcp;
			automatonQueueContext.UsedMaximumTransmissionSize = automatonQueueManagerContext.UsedMaximumTransmissionSize;
			automatonQueueContext.MaximumMessageSize = automatonQueueManagerContext.MaximumMessageSize;
			automatonQueueContext.DeterminantForQueueManager = Interlocked.Increment(ref PooledQueue.queueCounter);
			this.isTransactional = automatonQueueManagerContext.ConnectionParameters.IsTransactional;
			automatonQueueContext.IsTransactional = this.isTransactional;
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0016D2EC File Offset: 0x0016B4EC
		~PooledQueue()
		{
			this.traceContainer.Release();
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x0016D320 File Offset: 0x0016B520
		public bool Failed
		{
			get
			{
				return (this.Automaton.Automaton.Context as AutomatonQueueContext).InFailedState;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x060058D8 RID: 22744 RVA: 0x0016D33C File Offset: 0x0016B53C
		public int ObjectHandle
		{
			get
			{
				return (this.Automaton.Automaton.Context as AutomatonQueueContext).DeterminantForQueueManager;
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0016D358 File Offset: 0x0016B558
		public ReturnCode Open(bool forOutput, int intOptions, string dynamicQueueNamePrefix, out string resolvedQueueName)
		{
			resolvedQueueName = null;
			object obj = this.lockOpenObject;
			ReturnCode openReturnCode;
			lock (obj)
			{
				AutomatonQueueContext automatonQueueContext = this.Automaton.Automaton.Context as AutomatonQueueContext;
				automatonQueueContext.OpenReturnCode = ReturnCode.Ok;
				automatonQueueContext.OpenOptions = (OpenOption)intOptions;
				automatonQueueContext.DynamicQueueNamePrefix = dynamicQueueNamePrefix;
				automatonQueueContext.InFailedState = false;
				if (forOutput)
				{
					automatonQueueContext.OpenOptions |= (OpenOption)16;
				}
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Opening", Array.Empty<object>()));
				}
				this.Automaton.ProcessEvent(AutomatonQueueEvent.Open);
				automatonQueueContext.OpenedEvent.WaitOne();
				if (automatonQueueContext.OpenReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Open Succeeded", Array.Empty<object>()));
					}
					resolvedQueueName = automatonQueueContext.ResolvedQueueName;
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Open Failed, Return Code: {0}", automatonQueueContext.OpenReturnCode));
				}
				AutomatonQueueManagerContext automatonQueueManagerContext = automatonQueueContext.AutomatonQueueManager.Automaton.Context as AutomatonQueueManagerContext;
				this.maximumMessageSize = automatonQueueManagerContext.MaximumMessageSize;
				openReturnCode = automatonQueueContext.OpenReturnCode;
			}
			return openReturnCode;
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0016D4D4 File Offset: 0x0016B6D4
		public ReturnCode Close()
		{
			object obj = this.lockCloseObject;
			ReturnCode closeReturnCode;
			lock (obj)
			{
				AutomatonQueueContext automatonQueueContext = this.Automaton.Automaton.Context as AutomatonQueueContext;
				automatonQueueContext.CloseReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Closing", Array.Empty<object>()));
				}
				this.Automaton.ProcessEvent(AutomatonQueueEvent.Close);
				automatonQueueContext.ClosedEvent.WaitOne();
				if (automatonQueueContext.CloseReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Close Succeeded", Array.Empty<object>()));
					}
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Close Failed, Return Code: {0}", automatonQueueContext.CloseReturnCode));
				}
				this.Automaton.ClearBuffers();
				closeReturnCode = automatonQueueContext.CloseReturnCode;
			}
			return closeReturnCode;
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0016D5F4 File Offset: 0x0016B7F4
		public ReturnCode Send(object messageObject)
		{
			SendMessage sendMessage = (SendMessage)messageObject;
			if (sendMessage.Data.Length > this.maximumMessageSize)
			{
				return ReturnCode.QSendMaximumMessageSize;
			}
			if (this.isTransactional)
			{
				ReturnCode returnCode = this.queueManager.EnlistIfNeeded();
				if (returnCode != ReturnCode.Ok)
				{
					return returnCode;
				}
			}
			object obj = this.lockSendReceiveObject;
			ReturnCode sendReturnCode;
			lock (obj)
			{
				AutomatonQueueContext automatonQueueContext = this.Automaton.Automaton.Context as AutomatonQueueContext;
				automatonQueueContext.SendReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Sending", Array.Empty<object>()));
				}
				automatonQueueContext.ClientData = sendMessage;
				this.Automaton.ProcessEvent(AutomatonQueueEvent.DataToSend);
				automatonQueueContext.SentEvent.WaitOne();
				if (automatonQueueContext.SendReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Send Succeeded", Array.Empty<object>()));
					}
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Send Failed, Return Code: {0}", automatonQueueContext.SendReturnCode));
				}
				sendReturnCode = automatonQueueContext.SendReturnCode;
			}
			return sendReturnCode;
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0016D750 File Offset: 0x0016B950
		public ReturnCode Receive(object optionsObject, out object messageObject)
		{
			messageObject = null;
			ReceiveOptions receiveOptions = (ReceiveOptions)optionsObject;
			if (this.isTransactional)
			{
				ReturnCode returnCode = this.queueManager.EnlistIfNeeded();
				if (returnCode != ReturnCode.Ok)
				{
					return returnCode;
				}
			}
			object obj = this.lockSendReceiveObject;
			ReturnCode receiveReturnCode;
			lock (obj)
			{
				AutomatonQueueContext automatonQueueContext = this.Automaton.Automaton.Context as AutomatonQueueContext;
				automatonQueueContext.ReceiveReturnCode = ReturnCode.Ok;
				if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Receiving", Array.Empty<object>()));
				}
				automatonQueueContext.ReceiveOptions = receiveOptions;
				this.Automaton.ProcessEvent(AutomatonQueueEvent.Receive);
				automatonQueueContext.ReceivedEvent.WaitOne();
				if (automatonQueueContext.ReceiveReturnCode == ReturnCode.Ok)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.tracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Receive Succeeded", Array.Empty<object>()));
					}
					messageObject = automatonQueueContext.DataForClient;
				}
				else if (this.tracePoint.IsEnabled(TraceFlags.Information))
				{
					this.tracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Receive Failed, Return Code: {0}", automatonQueueContext.ReceiveReturnCode));
				}
				receiveReturnCode = automatonQueueContext.ReceiveReturnCode;
			}
			return receiveReturnCode;
		}

		// Token: 0x040045D1 RID: 17873
		private static int queueCounter = 1000000000;

		// Token: 0x040045D3 RID: 17875
		private QueueTracePoint tracePoint;

		// Token: 0x040045D4 RID: 17876
		private int maximumMessageSize;

		// Token: 0x040045D5 RID: 17877
		private object lockOpenObject = new object();

		// Token: 0x040045D6 RID: 17878
		private object lockCloseObject = new object();

		// Token: 0x040045D7 RID: 17879
		private object lockSendReceiveObject = new object();

		// Token: 0x040045D8 RID: 17880
		private PooledQueueManager queueManager;

		// Token: 0x040045D9 RID: 17881
		private bool isTransactional;

		// Token: 0x040045DA RID: 17882
		private MqTraceContainer traceContainer;
	}
}
