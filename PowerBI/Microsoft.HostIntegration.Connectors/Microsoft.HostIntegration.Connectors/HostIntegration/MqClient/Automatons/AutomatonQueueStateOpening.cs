using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC6 RID: 2758
	internal class AutomatonQueueStateOpening : StateAsCodeDriver
	{
		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06005760 RID: 22368 RVA: 0x00166740 File Offset: 0x00164940
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x00166753 File Offset: 0x00164953
		internal AutomatonQueueStateOpening(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x00166774 File Offset: 0x00164974
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
					case AutomatonQueueEvent.ServerData:
						if (this.PreConditionOpenReplyOk())
						{
							this.ActionChangeDeterminant();
							automatonQueueEvent2 = AutomatonQueueEvent.UpdateDeterminant;
							num = 0;
						}
						else
						{
							if (!this.PreConditionOpenReply())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionOpenRcEvent();
							automatonQueueState = AutomatonQueueState.Closed;
							num = 1;
						}
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
						this.ActionSetWaitOpenEvent();
						if (this.PostConditionForSend())
						{
							automatonQueueState = AutomatonQueueState.DataFlowSend;
							num = 2;
						}
						else if (this.PostConditionForReadAhead())
						{
							automatonQueueState = AutomatonQueueState.ReadAhead;
							num = 3;
						}
						else
						{
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 4;
						}
						break;
					case AutomatonQueueEvent.AttachFailed:
						this.ActionSetOpenFailEvent();
						automatonQueueState = AutomatonQueueState.Closed;
						num = 5;
						break;
					default:
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateOpening.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Opening, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001668D8 File Offset: 0x00164AD8
		public bool PreConditionOpenReplyOk()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply && passThroughData.ApiHeaderInformation.ReasonCode == ReturnCode.Ok;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x0016691D File Offset: 0x00164B1D
		public bool PreConditionOpenReply()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply;
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x00166948 File Offset: 0x00164B48
		public void ActionChangeDeterminant()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			int objectHandle = passThroughData.ApiHeaderInformation.ObjectHandle;
			this.automaton.DisconnectFromQueueManager(false);
			this.context.DeterminantForQueueManager = objectHandle;
			this.automaton.ConnectToQueueManager();
			this.context.OpenedWithReadAhead = (this.context.OpenOptions & OpenOption.ReadAhead) == OpenOption.ReadAhead;
			if (!this.context.OpenedWithReadAhead)
			{
				this.context.BytesToReceiveAsynchronously = 1;
			}
			this.context.ResolvedQueueName = this.automaton.ExtractQueueFromMqOpenReply(passThroughData);
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x001669F0 File Offset: 0x00164BF0
		public void ActionOpenRcEvent()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.OpenReturnCode = passThroughData.ApiHeaderInformation.ReasonCode;
			passThroughData.ReturnBuffers();
			this.context.OpenedEvent.Set();
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x00166A40 File Offset: 0x00164C40
		public void ActionSetWaitOpenEvent()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.OpenedEvent.Set();
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00166A70 File Offset: 0x00164C70
		public void ActionSetOpenFailEvent()
		{
			QAttachFailed qattachFailed = this.context.MessageFromQueueManager.Contents as QAttachFailed;
			this.context.OpenReturnCode = qattachFailed.ReturnCode;
			this.context.OpenedEvent.Set();
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x00166AB5 File Offset: 0x00164CB5
		public bool PostConditionForSend()
		{
			return (this.context.OpenOptions & (OpenOption)16) == (OpenOption)16;
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x00166AC9 File Offset: 0x00164CC9
		public bool PostConditionForReadAhead()
		{
			return (this.context.OpenOptions & OpenOption.ReadAhead) == OpenOption.ReadAhead;
		}

		// Token: 0x04004440 RID: 17472
		private AutomatonQueueState stateNumber = AutomatonQueueState.Opening;

		// Token: 0x04004441 RID: 17473
		private AutomatonQueue automaton;

		// Token: 0x04004442 RID: 17474
		private AutomatonQueueContext context;

		// Token: 0x04004443 RID: 17475
		private static string[] traceLines = new string[] { "State: Opening, Evt: ServerData, Pre: OpenReplyOk, Act: ChangeDeterminant, Evt: UpdateDeterminant", "State: Opening, Evt: ServerData, Pre: OpenReply, Act: OpenRcEvent, State: Closed, Stop", "State: Opening, Evt: UpdateDeterminant, Act: SetWaitOpenEvent, Post: ForSend, State: DataFlowSend, Stop", "State: Opening, Evt: UpdateDeterminant, Act: SetWaitOpenEvent, Post: ForReadAhead, State: ReadAhead, Stop", "State: Opening, Evt: UpdateDeterminant, Act: SetWaitOpenEvent, State: DataFlowReceive, Stop", "State: Opening, Evt: AttachFailed, Act: SetOpenFailEvent, State: Closed, Stop" };
	}
}
