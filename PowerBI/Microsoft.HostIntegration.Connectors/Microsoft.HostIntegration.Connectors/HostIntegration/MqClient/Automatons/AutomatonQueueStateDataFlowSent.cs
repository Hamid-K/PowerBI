using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC1 RID: 2753
	internal class AutomatonQueueStateDataFlowSent : StateAsCodeDriver
	{
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x001651C5 File Offset: 0x001633C5
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x001651D8 File Offset: 0x001633D8
		internal AutomatonQueueStateDataFlowSent(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x001651F8 File Offset: 0x001633F8
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
					if (automatonQueueEvent != AutomatonQueueEvent.ServerData)
					{
						switch (automatonQueueEvent)
						{
						case AutomatonQueueEvent.Quiesced:
							this.ActionQuiesceRcSetEvent();
							automatonQueueEvent2 = AutomatonQueueEvent.QuiesceDetach;
							num = 2;
							goto IL_00AB;
						case AutomatonQueueEvent.QmFailed:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingSend;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailedPut;
							num = 4;
							goto IL_00AB;
						case AutomatonQueueEvent.QuiesceDetach:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingSend;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
							num = 3;
							goto IL_00AB;
						}
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					if (this.PreConditionPutReplyOk())
					{
						this.ActionFillOutSetSendEvent();
						automatonQueueState = AutomatonQueueState.DataFlowSend;
						num = 0;
					}
					else
					{
						if (!this.PreConditionPutReply())
						{
							throw new InvalidOperationException("No Precondition returned true");
						}
						this.ActionReturnCodeSetEvent();
						automatonQueueState = AutomatonQueueState.DataFlowSend;
						num = 1;
					}
					IL_00AB:
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDataFlowSent.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowSent, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0016535C File Offset: 0x0016355C
		public bool PreConditionPutReplyOk()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqPutReply && passThroughData.ApiHeaderInformation.ReasonCode == ReturnCode.Ok;
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x001653A1 File Offset: 0x001635A1
		public bool PreConditionPutReply()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqPutReply;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x001653CC File Offset: 0x001635CC
		public void ActionFillOutSetSendEvent()
		{
			SendMessage clientData = this.context.ClientData;
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			int num = 52;
			clientData.ExtractMqmd(passThroughData.Buffers[0].Data, num, passThroughData.SegmentHeaderInformation.LittleEndian, (int)passThroughData.SegmentHeaderInformation.Ccsid);
			int num2 = num + 364;
			clientData.Options.ExtractFromPutReply(passThroughData.Buffers[0].Data, num2, passThroughData.SegmentHeaderInformation.LittleEndian, (int)passThroughData.SegmentHeaderInformation.Ccsid);
			this.context.SentEvent.Set();
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x00165478 File Offset: 0x00163678
		public void ActionReturnCodeSetEvent()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.SendReturnCode = passThroughData.ApiHeaderInformation.ReasonCode;
			this.context.SentEvent.Set();
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x001654C2 File Offset: 0x001636C2
		public void ActionQuiesceRcSetEvent()
		{
			this.context.SendReturnCode = ReturnCode.QSendFailedQmQuiesced;
			this.context.SentEvent.Set();
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001654E8 File Offset: 0x001636E8
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x0400442C RID: 17452
		private AutomatonQueueState stateNumber = AutomatonQueueState.DataFlowSent;

		// Token: 0x0400442D RID: 17453
		private AutomatonQueue automaton;

		// Token: 0x0400442E RID: 17454
		private AutomatonQueueContext context;

		// Token: 0x0400442F RID: 17455
		private static string[] traceLines = new string[] { "State: DataFlowSent, Evt: ServerData, Pre: PutReplyOk, Act: FillOutSetSendEvent, State: DataFlowSend, Stop", "State: DataFlowSent, Evt: ServerData, Pre: PutReply, Act: ReturnCodeSetEvent, State: DataFlowSend, Stop", "State: DataFlowSent, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: DataFlowSent, Evt: QuiesceDetach, Act: DetachManager, State: DetachingSend, Evt: QmFailed", "State: DataFlowSent, Evt: QmFailed, Act: DetachManager, State: DetachingSend, Evt: QmFailedPut" };
	}
}
