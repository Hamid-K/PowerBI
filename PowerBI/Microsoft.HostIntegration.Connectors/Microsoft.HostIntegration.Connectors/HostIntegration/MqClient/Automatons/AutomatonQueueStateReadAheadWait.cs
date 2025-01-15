using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC8 RID: 2760
	internal class AutomatonQueueStateReadAheadWait : StateAsCodeDriver
	{
		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x0600577E RID: 22398 RVA: 0x001673D4 File Offset: 0x001655D4
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x001673E7 File Offset: 0x001655E7
		internal AutomatonQueueStateReadAheadWait(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00167408 File Offset: 0x00165608
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
						automatonQueueState = AutomatonQueueState.ReadAheadWaited;
						num = 5;
						break;
					case AutomatonQueueEvent.ServerData:
						if (this.PreConditionAsyncMessage())
						{
							this.ActionQueueCheckReset();
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
						else
						{
							if (!this.PreConditionCheckMsg())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							num = 2;
						}
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
					case AutomatonQueueEvent.AttachFailed:
					case AutomatonQueueEvent.DataToSend:
					case AutomatonQueueEvent.Close:
						goto IL_00DD;
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
							if (automatonQueueEvent != AutomatonQueueEvent.MessageReceived)
							{
								goto IL_00DD;
							}
							this.ActionSetWaitReceiveEvent();
							automatonQueueState = AutomatonQueueState.ReadAhead;
							num = 4;
						}
						else
						{
							this.ActionMessageFromAsync();
							automatonQueueEvent2 = AutomatonQueueEvent.MessageReceived;
							num = 3;
						}
						break;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReadAheadWait.traceLines[num]);
						continue;
					}
					continue;
					IL_00DD:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReadAheadWait, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x001675C4 File Offset: 0x001657C4
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x001675EC File Offset: 0x001657EC
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

		// Token: 0x06005783 RID: 22403 RVA: 0x00167668 File Offset: 0x00165868
		public void ActionQueueCheckReset()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
			this.context.ReceiveTimer.Change(-1, -1);
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001676E0 File Offset: 0x001658E0
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

		// Token: 0x06005785 RID: 22405 RVA: 0x001679F0 File Offset: 0x00165BF0
		public void ActionSetWaitReceiveEvent()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x00167A03 File Offset: 0x00165C03
		public void ActionSendRequestMsg0()
		{
			this.context.OriginalReceiveOptionsIncluded = true;
			this.automaton.SendRequestMessageToQueueManager(true);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x00167A1D File Offset: 0x00165C1D
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00167A40 File Offset: 0x00165C40
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00167A86 File Offset: 0x00165C86
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x04004448 RID: 17480
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReadAheadWait;

		// Token: 0x04004449 RID: 17481
		private AutomatonQueue automaton;

		// Token: 0x0400444A RID: 17482
		private AutomatonQueueContext context;

		// Token: 0x0400444B RID: 17483
		private static string[] traceLines = new string[] { "State: ReadAheadWait, Evt: ServerData, Pre: AsyncMessage, Act: QueueCheckReset, Post: LastSegment, Evt: MakeMessage", "State: ReadAheadWait, Evt: ServerData, Pre: AsyncMessage, Act: QueueCheckReset, Stop", "State: ReadAheadWait, Evt: ServerData, Pre: CheckMsg, Stop", "State: ReadAheadWait, Evt: MakeMessage, Act: MessageFromAsync, Evt: MessageReceived", "State: ReadAheadWait, Evt: MessageReceived, Act: SetWaitReceiveEvent, State: ReadAhead, Stop", "State: ReadAheadWait, Evt: Timer, Act: SendRequestMsg0, State: ReadAheadWaited, Stop", "State: ReadAheadWait, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: ReadAheadWait, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReadAheadWait, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet" };
	}
}
