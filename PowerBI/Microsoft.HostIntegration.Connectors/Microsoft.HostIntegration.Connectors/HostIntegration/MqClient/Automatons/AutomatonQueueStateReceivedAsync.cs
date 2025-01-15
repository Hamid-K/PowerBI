using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACB RID: 2763
	internal class AutomatonQueueStateReceivedAsync : StateAsCodeDriver
	{
		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x001688D4 File Offset: 0x00166AD4
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x001688E7 File Offset: 0x00166AE7
		internal AutomatonQueueStateReceivedAsync(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x00168908 File Offset: 0x00166B08
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
						num = 5;
						break;
					case AutomatonQueueEvent.ServerData:
						if (this.PreConditionCheckMsg())
						{
							this.ActionSetWaitReceiveEvent();
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 0;
						}
						else
						{
							if (!this.PreConditionNotification())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionNotificationSetEvent();
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 1;
						}
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
					case AutomatonQueueEvent.AttachFailed:
					case AutomatonQueueEvent.DataToSend:
					case AutomatonQueueEvent.Close:
						goto IL_00A6;
					case AutomatonQueueEvent.Quiesced:
						this.ActionQuiesceRcSetEvent();
						automatonQueueEvent2 = AutomatonQueueEvent.QuiesceDetach;
						num = 2;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailedGet;
						num = 4;
						break;
					case AutomatonQueueEvent.QuiesceDetach:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 3;
						break;
					default:
						goto IL_00A6;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReceivedAsync.traceLines[num]);
						continue;
					}
					continue;
					IL_00A6:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReceivedAsync, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x00168A80 File Offset: 0x00166C80
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

		// Token: 0x060057A8 RID: 22440 RVA: 0x00168AFA File Offset: 0x00166CFA
		public bool PreConditionNotification()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.Notification;
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x00168B1F File Offset: 0x00166D1F
		public void ActionSetWaitReceiveEvent()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.MessageFromQueueManager = null;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x00168B58 File Offset: 0x00166D58
		public void ActionNotificationSetEvent()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.MessageFromQueueManager = null;
			NotificationType code = this.context.Notification.Code;
			MqReasonCode mqReasonCode;
			if (code != NotificationType.GetInhibited)
			{
				if (code != NotificationType.CheckMessage)
				{
					if (code != NotificationType.StreamingFailure)
					{
						throw new InvalidOperationException("Unknown notification code");
					}
					mqReasonCode = MqReasonCode.StreamingFailure;
				}
				else
				{
					mqReasonCode = MqReasonCode.NoMessageAvailable;
				}
			}
			else
			{
				mqReasonCode = MqReasonCode.GetInhibited;
			}
			this.context.ReceiveReturnCode = (ReturnCode)mqReasonCode;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x00168BEC File Offset: 0x00166DEC
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x00168C10 File Offset: 0x00166E10
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x04004454 RID: 17492
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReceivedAsync;

		// Token: 0x04004455 RID: 17493
		private AutomatonQueue automaton;

		// Token: 0x04004456 RID: 17494
		private AutomatonQueueContext context;

		// Token: 0x04004457 RID: 17495
		private static string[] traceLines = new string[] { "State: ReceivedAsync, Evt: ServerData, Pre: CheckMsg, Act: SetWaitReceiveEvent, State: DataFlowReceive, Stop", "State: ReceivedAsync, Evt: ServerData, Pre: Notification, Act: NotificationSetEvent, State: DataFlowReceive, Stop", "State: ReceivedAsync, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: ReceivedAsync, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReceivedAsync, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet", "State: ReceivedAsync, Evt: Timer, Stop" };
	}
}
