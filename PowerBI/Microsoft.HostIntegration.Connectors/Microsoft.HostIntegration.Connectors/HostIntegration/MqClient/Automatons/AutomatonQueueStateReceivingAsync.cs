using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACC RID: 2764
	internal class AutomatonQueueStateReceivingAsync : StateAsCodeDriver
	{
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x00168C93 File Offset: 0x00166E93
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x00168CA6 File Offset: 0x00166EA6
		internal AutomatonQueueStateReceivingAsync(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x00168CC8 File Offset: 0x00166EC8
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
						num = 9;
						break;
					case AutomatonQueueEvent.ServerData:
						if (this.PreConditionAsyncMessage())
						{
							this.ActionQueueAndCheck();
							if (this.PostConditionLastSegment())
							{
								automatonQueueEvent2 = AutomatonQueueEvent.MakeMessage;
								num = 0;
							}
							else
							{
								num = 1;
							}
						}
						else if (this.PreConditionBrowseFirst())
						{
							num = 2;
						}
						else if (this.PreConditionCheckMsg())
						{
							this.ActionNoMessageSetEvent();
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 3;
						}
						else
						{
							if (!this.PreConditionNotification())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionNotificationSetEvent();
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 4;
						}
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
					case AutomatonQueueEvent.AttachFailed:
					case AutomatonQueueEvent.DataToSend:
					case AutomatonQueueEvent.Close:
						goto IL_00F2;
					case AutomatonQueueEvent.Quiesced:
						this.ActionQuiesceRcSetEvent();
						automatonQueueEvent2 = AutomatonQueueEvent.QuiesceDetach;
						num = 6;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailedGet;
						num = 8;
						break;
					case AutomatonQueueEvent.QuiesceDetach:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 7;
						break;
					default:
						if (automatonQueueEvent != AutomatonQueueEvent.MakeMessage)
						{
							goto IL_00F2;
						}
						this.ActionMessageFromAsync();
						automatonQueueState = AutomatonQueueState.ReceivedAsync;
						num = 5;
						break;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReceivingAsync.traceLines[num]);
						continue;
					}
					continue;
					IL_00F2:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReceivingAsync, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x00168E98 File Offset: 0x00167098
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x00168EC0 File Offset: 0x001670C0
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

		// Token: 0x060057B3 RID: 22451 RVA: 0x00168F3C File Offset: 0x0016713C
		public bool PreConditionBrowseFirst()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentType != SegmentType.Notification)
			{
				return false;
			}
			this.context.Notification = SegmentHelpers.ExtractNotification(passThroughData.Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian);
			return this.context.Notification.Code == NotificationType.BrowseFirst;
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x00168FB6 File Offset: 0x001671B6
		public bool PreConditionNotification()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.Notification;
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x00168FDC File Offset: 0x001671DC
		public void ActionQueueAndCheck()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x00169040 File Offset: 0x00167240
		public void ActionNoMessageSetEvent()
		{
			(this.context.MessageFromQueueManager.Contents as PassThroughData).ReturnBuffers();
			this.context.MessageFromQueueManager = null;
			this.context.ReceiveReturnCode = (ReturnCode)2033;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x00169094 File Offset: 0x00167294
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

		// Token: 0x060057B8 RID: 22456 RVA: 0x00169128 File Offset: 0x00167328
		public void ActionMessageFromAsync()
		{
			int[] array = new int[this.context.SegmentsReceived.Count];
			int[] array2 = new int[this.context.SegmentsReceived.Count];
			ReceiveMessage receiveMessage = new ReceiveMessage(new ReceiveOptions());
			bool flag = true;
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = 0;
			foreach (PassThroughData passThroughData in this.context.SegmentsReceived)
			{
				AsyncMessageInformation asyncMessageInformation = AutomatonQueue.ExtractAsyncMessage(passThroughData);
				if (flag)
				{
					int lengthOfSegmentHeader = passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader;
					receiveMessage.Options.ExtractFromAsyncMessage(passThroughData.Buffers[0].Data, lengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian, (int)passThroughData.SegmentHeaderInformation.Ccsid, receiveMessage);
					int num5 = passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader + asyncMessageInformation.Length;
					int num6 = receiveMessage.ExtractMqmd(passThroughData.Buffers[0].Data, passThroughData.Buffers[0].UsedLength, num5, passThroughData.SegmentHeaderInformation.LittleEndian, (int)passThroughData.SegmentHeaderInformation.Ccsid, out num3);
					int num7 = num5 + num6;
					num2 = receiveMessage.ExtractHeaders(passThroughData.Buffers[0].Data, passThroughData.SegmentHeaderInformation.TotalLengthOfSegment, num7, asyncMessageInformation.TruncatedLength != asyncMessageInformation.OriginalMessageLength);
					num7 += num2;
					array[num] = num7;
					array2[num] = asyncMessageInformation.SegmentLength - num2;
					this.context.AsynchronouslyReceivedBytes += num6 + num2 + array2[num];
					num4 = asyncMessageInformation.TruncatedLength;
				}
				else
				{
					array[num] = passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader + asyncMessageInformation.Length;
					array2[num] = asyncMessageInformation.SegmentLength;
					this.context.AsynchronouslyReceivedBytes += array2[num];
				}
				flag = false;
				num++;
			}
			receiveMessage.OriginalLength = num3;
			byte[] array3 = new byte[num4 - num2];
			num = 0;
			int num8 = 0;
			foreach (PassThroughData passThroughData2 in this.context.SegmentsReceived)
			{
				Array.Copy(passThroughData2.Buffers[0].Data, array[num], array3, num8, array2[num]);
				num8 += array2[num];
				num++;
				passThroughData2.ReturnBuffers();
			}
			receiveMessage.Data = array3;
			receiveMessage.Options = null;
			this.context.SegmentsReceived.Clear();
			this.context.DataForClient = receiveMessage;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x00169414 File Offset: 0x00167614
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x00169438 File Offset: 0x00167638
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x0016947E File Offset: 0x0016767E
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x04004458 RID: 17496
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReceivingAsync;

		// Token: 0x04004459 RID: 17497
		private AutomatonQueue automaton;

		// Token: 0x0400445A RID: 17498
		private AutomatonQueueContext context;

		// Token: 0x0400445B RID: 17499
		private static string[] traceLines = new string[] { "State: ReceivingAsync, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Post: LastSegment, Evt: MakeMessage", "State: ReceivingAsync, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Stop", "State: ReceivingAsync, Evt: ServerData, Pre: BrowseFirst, Stop", "State: ReceivingAsync, Evt: ServerData, Pre: CheckMsg, Act: NoMessageSetEvent, State: DataFlowReceive, Stop", "State: ReceivingAsync, Evt: ServerData, Pre: Notification, Act: NotificationSetEvent, State: DataFlowReceive, Stop", "State: ReceivingAsync, Evt: MakeMessage, Act: MessageFromAsync, State: ReceivedAsync, Stop", "State: ReceivingAsync, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: ReceivingAsync, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReceivingAsync, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet", "State: ReceivingAsync, Evt: Timer, Stop" };
	}
}
