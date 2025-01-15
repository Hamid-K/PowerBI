using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC3 RID: 2755
	internal class AutomatonQueueStateDataFlowWait : StateAsCodeDriver
	{
		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x0016577B File Offset: 0x0016397B
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x0016578E File Offset: 0x0016398E
		internal AutomatonQueueStateDataFlowWait(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x001657B0 File Offset: 0x001639B0
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
						this.ActionSendRequestMsg0();
						automatonQueueState = AutomatonQueueState.ReceivingAsync;
						num = 7;
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
						goto IL_0108;
					case AutomatonQueueEvent.Quiesced:
						this.ActionQuiesceRcSetEvent();
						automatonQueueEvent2 = AutomatonQueueEvent.QuiesceDetach;
						num = 8;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailedGet;
						num = 10;
						break;
					case AutomatonQueueEvent.QuiesceDetach:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 9;
						break;
					default:
						if (automatonQueueEvent != AutomatonQueueEvent.MakeMessage)
						{
							if (automatonQueueEvent != AutomatonQueueEvent.MessageReceived)
							{
								goto IL_0108;
							}
							this.ActionSetWaitReceiveEvent();
							automatonQueueState = AutomatonQueueState.DataFlowReceive;
							num = 6;
						}
						else
						{
							this.ActionMessageFromAsync();
							automatonQueueEvent2 = AutomatonQueueEvent.MessageReceived;
							num = 5;
						}
						break;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateDataFlowWait.traceLines[num]);
						continue;
					}
					continue;
					IL_0108:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataFlowWait, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x00165998 File Offset: 0x00163B98
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x001659C0 File Offset: 0x00163BC0
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

		// Token: 0x06005739 RID: 22329 RVA: 0x00165A3C File Offset: 0x00163C3C
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

		// Token: 0x0600573A RID: 22330 RVA: 0x00165AB6 File Offset: 0x00163CB6
		public bool PreConditionNotification()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.Notification;
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x00165ADC File Offset: 0x00163CDC
		public void ActionQueueAndCheck()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x00165B40 File Offset: 0x00163D40
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

		// Token: 0x0600573D RID: 22333 RVA: 0x00165BD4 File Offset: 0x00163DD4
		public void ActionMessageFromAsync()
		{
			int[] array = new int[this.context.SegmentsReceived.Count];
			int[] array2 = new int[this.context.SegmentsReceived.Count];
			ReceiveMessage receiveMessage = new ReceiveMessage(this.context.OriginalReceiveOptionsIncluded ? this.context.ReceiveOptions : new ReceiveOptions());
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
			if (!this.context.OriginalReceiveOptionsIncluded)
			{
				receiveMessage.Options = null;
			}
			this.context.SegmentsReceived.Clear();
			this.context.DataForClient = receiveMessage;
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x00165EE4 File Offset: 0x001640E4
		public void ActionSetWaitReceiveEvent()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x00165EF7 File Offset: 0x001640F7
		public void ActionSendRequestMsg0()
		{
			this.context.OriginalReceiveOptionsIncluded = true;
			this.automaton.SendRequestMessageToQueueManager(true);
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x00165F11 File Offset: 0x00164111
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x00165F34 File Offset: 0x00164134
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x00165F7A File Offset: 0x0016417A
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x04004434 RID: 17460
		private AutomatonQueueState stateNumber = AutomatonQueueState.DataFlowWait;

		// Token: 0x04004435 RID: 17461
		private AutomatonQueue automaton;

		// Token: 0x04004436 RID: 17462
		private AutomatonQueueContext context;

		// Token: 0x04004437 RID: 17463
		private static string[] traceLines = new string[]
		{
			"State: DataFlowWait, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Post: LastSegment, Evt: MakeMessage", "State: DataFlowWait, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Stop", "State: DataFlowWait, Evt: ServerData, Pre: BrowseFirst, Stop", "State: DataFlowWait, Evt: ServerData, Pre: CheckMsg, Stop", "State: DataFlowWait, Evt: ServerData, Pre: Notification, Act: NotificationSetEvent, State: DataFlowReceive, Stop", "State: DataFlowWait, Evt: MakeMessage, Act: MessageFromAsync, Evt: MessageReceived", "State: DataFlowWait, Evt: MessageReceived, Act: SetWaitReceiveEvent, State: DataFlowReceive, Stop", "State: DataFlowWait, Evt: Timer, Act: SendRequestMsg0, State: ReceivingAsync, Stop", "State: DataFlowWait, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: DataFlowWait, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed",
			"State: DataFlowWait, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet"
		};
	}
}
