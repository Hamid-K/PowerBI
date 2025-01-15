using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC0 RID: 2752
	internal class AutomatonQueueStateDataFlowSend : StateAsCodeDriver
	{
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x0600571A RID: 22298 RVA: 0x00164F28 File Offset: 0x00163128
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x00164F3B File Offset: 0x0016313B
		internal AutomatonQueueStateDataFlowSend(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x00164F5C File Offset: 0x0016315C
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
					case AutomatonQueueEvent.DataToSend:
						if (this.PreConditionNormal())
						{
							this.ActionSendToQm();
							automatonQueueState = AutomatonQueueState.DataFlowSent;
							num = 0;
						}
						else
						{
							this.ActionSendToQmAsync();
							automatonQueueState = AutomatonQueueState.DataFlowSentAsync;
							num = 1;
						}
						break;
					case AutomatonQueueEvent.Quiesced:
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 2;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingSend;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 3;
						break;
					case AutomatonQueueEvent.Close:
						this.ActionSendMqClose();
						automatonQueueState = AutomatonQueueState.DetachingSend;
						automatonQueueEvent2 = AutomatonQueueEvent.Close;
						num = 4;
						break;
					default:
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDataFlowSend.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowSend, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x001650A0 File Offset: 0x001632A0
		public bool PreConditionNormal()
		{
			return !this.context.ClientData.Options.AsynchronousResponse;
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x001650BA File Offset: 0x001632BA
		public void ActionSendToQm()
		{
			this.automaton.SendDataToQueueManager(false);
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x001650C8 File Offset: 0x001632C8
		public void ActionSendToQmAsync()
		{
			this.automaton.SendDataToQueueManager(true);
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x001650D8 File Offset: 0x001632D8
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x00165120 File Offset: 0x00163320
		public void ActionSendMqClose()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateMqClose(buffer);
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			passThroughData.Buffers = new List<DynamicDataBuffer> { buffer };
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x04004428 RID: 17448
		private AutomatonQueueState stateNumber = AutomatonQueueState.DataFlowSend;

		// Token: 0x04004429 RID: 17449
		private AutomatonQueue automaton;

		// Token: 0x0400442A RID: 17450
		private AutomatonQueueContext context;

		// Token: 0x0400442B RID: 17451
		private static string[] traceLines = new string[] { "State: DataFlowSend, Evt: DataToSend, Pre: Normal, Act: SendToQm, State: DataFlowSent, Stop", "State: DataFlowSend, Evt: DataToSend, Act: SendToQmAsync, State: DataFlowSentAsync, Stop", "State: DataFlowSend, Evt: Quiesced, Evt: QmFailed", "State: DataFlowSend, Evt: QmFailed, Act: DetachManager, State: DetachingSend, Evt: QmFailed", "State: DataFlowSend, Evt: Close, Act: SendMqClose, State: DetachingSend, Evt: Close" };
	}
}
