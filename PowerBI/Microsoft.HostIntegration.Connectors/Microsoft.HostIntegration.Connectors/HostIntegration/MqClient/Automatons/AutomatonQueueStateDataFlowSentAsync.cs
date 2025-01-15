using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC2 RID: 2754
	internal class AutomatonQueueStateDataFlowSentAsync : StateAsCodeDriver
	{
		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x0600572D RID: 22317 RVA: 0x00165563 File Offset: 0x00163763
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x00165576 File Offset: 0x00163776
		internal AutomatonQueueStateDataFlowSentAsync(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x00165594 File Offset: 0x00163794
		public override int Process(ref int eventToProcess)
		{
			AutomatonQueueEvent automatonQueueEvent = (AutomatonQueueEvent)eventToProcess;
			AutomatonQueueState automatonQueueState = this.stateNumber;
			try
			{
				while (automatonQueueState == this.stateNumber && automatonQueueEvent != AutomatonQueueEvent.Stop)
				{
					AutomatonQueueEvent automatonQueueEvent2 = AutomatonQueueEvent.Stop;
					int num;
					switch (automatonQueueEvent)
					{
					case AutomatonQueueEvent.Quiesced:
						this.ActionQuiesceRcSetEvent();
						automatonQueueEvent2 = AutomatonQueueEvent.QuiesceDetach;
						num = 1;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingSend;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailedPut;
						num = 3;
						break;
					case AutomatonQueueEvent.Close:
						goto IL_0069;
					case AutomatonQueueEvent.QuiesceDetach:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingSend;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 2;
						break;
					case AutomatonQueueEvent.AsyncPutDone:
						this.ActionSetWaitSendEvent();
						automatonQueueState = AutomatonQueueState.DataFlowSend;
						num = 0;
						break;
					default:
						goto IL_0069;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDataFlowSentAsync.traceLines[num]);
						continue;
					}
					continue;
					IL_0069:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowSentAsync, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001656D0 File Offset: 0x001638D0
		public void ActionSetWaitSendEvent()
		{
			this.context.SentEvent.Set();
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001656E3 File Offset: 0x001638E3
		public void ActionQuiesceRcSetEvent()
		{
			this.context.SendReturnCode = ReturnCode.QSendFailedQmQuiesced;
			this.context.SentEvent.Set();
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x00165708 File Offset: 0x00163908
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x04004430 RID: 17456
		private AutomatonQueueState stateNumber = AutomatonQueueState.DataFlowSentAsync;

		// Token: 0x04004431 RID: 17457
		private AutomatonQueue automaton;

		// Token: 0x04004432 RID: 17458
		private AutomatonQueueContext context;

		// Token: 0x04004433 RID: 17459
		private static string[] traceLines = new string[] { "State: DataFlowSentAsync, Evt: AsyncPutDone, Act: SetWaitSendEvent, State: DataFlowSend, Stop", "State: DataFlowSentAsync, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: DataFlowSentAsync, Evt: QuiesceDetach, Act: DetachManager, State: DetachingSend, Evt: QmFailed", "State: DataFlowSentAsync, Evt: QmFailed, Act: DetachManager, State: DetachingSend, Evt: QmFailedPut" };
	}
}
