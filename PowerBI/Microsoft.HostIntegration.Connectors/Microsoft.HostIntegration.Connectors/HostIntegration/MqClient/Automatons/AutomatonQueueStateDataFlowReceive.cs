using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABF RID: 2751
	internal class AutomatonQueueStateDataFlowReceive : StateAsCodeDriver
	{
		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x0600570F RID: 22287 RVA: 0x00164B0D File Offset: 0x00162D0D
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x00164B20 File Offset: 0x00162D20
		internal AutomatonQueueStateDataFlowReceive(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x00164B40 File Offset: 0x00162D40
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
					case AutomatonQueueEvent.Timer:
						num = 7;
						break;
					case AutomatonQueueEvent.ServerData:
						if (!this.PreConditionCheckMsg())
						{
							throw new InvalidOperationException("No Precondition returned true");
						}
						num = 3;
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
					case AutomatonQueueEvent.AttachFailed:
					case AutomatonQueueEvent.DataToSend:
					case AutomatonQueueEvent.QuiesceDetach:
					case AutomatonQueueEvent.AsyncPutDone:
						goto IL_00B5;
					case AutomatonQueueEvent.Quiesced:
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 4;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 5;
						break;
					case AutomatonQueueEvent.Close:
						this.ActionSendMqClose();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.Close;
						num = 6;
						break;
					case AutomatonQueueEvent.Receive:
						this.ActionSendRequestMsg();
						automatonQueueEvent2 = AutomatonQueueEvent.CheckTimer;
						num = 0;
						break;
					case AutomatonQueueEvent.CheckTimer:
						if (this.PreConditionWait())
						{
							this.ActionSetTimer();
							automatonQueueState = AutomatonQueueState.DataFlowWait;
							num = 1;
						}
						else
						{
							automatonQueueState = AutomatonQueueState.ReceivingAsync;
							num = 2;
						}
						break;
					default:
						goto IL_00B5;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDataFlowReceive.traceLines[num]);
						continue;
					}
					continue;
					IL_00B5:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowReceive, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x00164CD4 File Offset: 0x00162ED4
		public bool PreConditionWait()
		{
			return this.context.ReceiveOptions != null && this.context.ReceiveOptions.Wait;
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x00164CF8 File Offset: 0x00162EF8
		public bool PreConditionCheckMsg()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentType != SegmentType.Notification)
			{
				return false;
			}
			this.context.Notification = SegmentHelpers.ExtractNotification(passThroughData.Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian);
			return this.context.Notification.Code == NotificationType.CheckMessage;
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x00164D72 File Offset: 0x00162F72
		public void ActionSendRequestMsg()
		{
			this.context.OriginalReceiveOptionsIncluded = this.context.ReceiveOptions != null;
			this.automaton.SendRequestMessageToQueueManager(false);
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x00164D9C File Offset: 0x00162F9C
		public void ActionSetTimer()
		{
			if (this.context.ReceiveTimer == null)
			{
				this.context.ReceiveTimer = new Timer(new TimerCallback(this.ReceiveTimerProcedure), null, this.context.ReceiveOptions.Timeout, -1);
				return;
			}
			this.context.ReceiveTimer.Change(this.context.ReceiveOptions.Timeout, -1);
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00164E07 File Offset: 0x00163007
		private void ReceiveTimerProcedure(object state)
		{
			this.automaton.ProcessEvent(AutomatonQueueEvent.Timer);
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x00164E18 File Offset: 0x00163018
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00164E60 File Offset: 0x00163060
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

		// Token: 0x04004424 RID: 17444
		private AutomatonQueueState stateNumber = AutomatonQueueState.DataFlowReceive;

		// Token: 0x04004425 RID: 17445
		private AutomatonQueue automaton;

		// Token: 0x04004426 RID: 17446
		private AutomatonQueueContext context;

		// Token: 0x04004427 RID: 17447
		private static string[] traceLines = new string[] { "State: DataFlowReceive, Evt: Receive, Act: SendRequestMsg, Evt: CheckTimer", "State: DataFlowReceive, Evt: CheckTimer, Pre: Wait, Act: SetTimer, State: DataFlowWait, Stop", "State: DataFlowReceive, Evt: CheckTimer, State: ReceivingAsync, Stop", "State: DataFlowReceive, Evt: ServerData, Pre: CheckMsg, Stop", "State: DataFlowReceive, Evt: Quiesced, Evt: QmFailed", "State: DataFlowReceive, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: DataFlowReceive, Evt: Close, Act: SendMqClose, State: DetachingReceive, Evt: Close", "State: DataFlowReceive, Evt: Timer, Stop" };
	}
}
