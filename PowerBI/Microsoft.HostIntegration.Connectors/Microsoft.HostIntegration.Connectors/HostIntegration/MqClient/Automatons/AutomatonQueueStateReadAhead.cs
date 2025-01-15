using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC7 RID: 2759
	internal class AutomatonQueueStateReadAhead : StateAsCodeDriver
	{
		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x00166B20 File Offset: 0x00164D20
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00166B33 File Offset: 0x00164D33
		internal AutomatonQueueStateReadAhead(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00166B54 File Offset: 0x00164D54
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
						if (this.PreConditionAsyncMessage())
						{
							this.ActionQueueAndCheck();
							if (this.PostConditionLastSegment())
							{
								automatonQueueEvent2 = AutomatonQueueEvent.MakeMessage;
								num = 5;
							}
							else
							{
								num = 6;
							}
						}
						else
						{
							if (!this.PreConditionCheckMsg())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							num = 7;
						}
						break;
					case AutomatonQueueEvent.UpdateDeterminant:
					case AutomatonQueueEvent.AttachFailed:
					case AutomatonQueueEvent.DataToSend:
					case AutomatonQueueEvent.QuiesceDetach:
					case AutomatonQueueEvent.AsyncPutDone:
						goto IL_0110;
					case AutomatonQueueEvent.Quiesced:
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 9;
						break;
					case AutomatonQueueEvent.QmFailed:
						this.ActionDetachManager();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
						num = 10;
						break;
					case AutomatonQueueEvent.Close:
						this.ActionSendMqClose();
						automatonQueueState = AutomatonQueueState.DetachingReceive;
						automatonQueueEvent2 = AutomatonQueueEvent.Close;
						num = 11;
						break;
					case AutomatonQueueEvent.Receive:
						if (this.PreConditionMessageAvailable())
						{
							this.ActionDequeueAndReturn();
							num = 0;
						}
						else if (this.PreConditionMoreDataToCome())
						{
							automatonQueueEvent2 = AutomatonQueueEvent.CheckTimer;
							num = 1;
						}
						else
						{
							this.ActionSendRequestMsg();
							automatonQueueEvent2 = AutomatonQueueEvent.CheckTimer;
							num = 2;
						}
						break;
					case AutomatonQueueEvent.CheckTimer:
						if (this.PreConditionWait())
						{
							this.ActionSetTimer();
							automatonQueueState = AutomatonQueueState.ReadAheadWait;
							num = 3;
						}
						else
						{
							automatonQueueState = AutomatonQueueState.ReadAheadReceiving;
							num = 4;
						}
						break;
					case AutomatonQueueEvent.MakeMessage:
						this.ActionCreateAndEnqueue();
						num = 8;
						break;
					default:
						goto IL_0110;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReadAhead.traceLines[num]);
						continue;
					}
					continue;
					IL_0110:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReadAhead, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00166D44 File Offset: 0x00164F44
		public bool PreConditionMessageAvailable()
		{
			return this.context.MessagesReceived.Count != 0;
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00166D59 File Offset: 0x00164F59
		public bool PreConditionMoreDataToCome()
		{
			return this.context.FollowUpRequest && this.context.AsynchronouslyReceivedBytes < this.context.BytesToReceiveAsynchronously;
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x00166D82 File Offset: 0x00164F82
		public bool PreConditionWait()
		{
			return this.context.ReceiveOptions != null && this.context.ReceiveOptions.Wait;
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x00166DA3 File Offset: 0x00164FA3
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x00166DC8 File Offset: 0x00164FC8
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

		// Token: 0x06005774 RID: 22388 RVA: 0x00166E42 File Offset: 0x00165042
		public void ActionDequeueAndReturn()
		{
			this.context.DataForClient = this.context.MessagesReceived.Dequeue();
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00166E70 File Offset: 0x00165070
		public void ActionSendRequestMsg()
		{
			this.context.OriginalReceiveOptionsIncluded = this.context.ReceiveOptions != null;
			this.automaton.SendRequestMessageToQueueManager(false);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00166E98 File Offset: 0x00165098
		public void ActionSetTimer()
		{
			if (this.context.ReceiveTimer == null)
			{
				this.context.ReceiveTimer = new Timer(new TimerCallback(this.ReceiveTimerProcedure), null, this.context.ReceiveOptions.Timeout, -1);
				return;
			}
			this.context.ReceiveTimer.Change(this.context.ReceiveOptions.Timeout, -1);
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x00166F03 File Offset: 0x00165103
		private void ReceiveTimerProcedure(object state)
		{
			this.automaton.ProcessEvent(AutomatonQueueEvent.Timer);
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00166F14 File Offset: 0x00165114
		public void ActionQueueAndCheck()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00166F78 File Offset: 0x00165178
		public void ActionCreateAndEnqueue()
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
			this.context.MessagesReceived.Enqueue(receiveMessage);
			this.context.SegmentsReceived.Clear();
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x00167290 File Offset: 0x00165490
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x001672D8 File Offset: 0x001654D8
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

		// Token: 0x0600577C RID: 22396 RVA: 0x00167348 File Offset: 0x00165548
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x04004444 RID: 17476
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReadAhead;

		// Token: 0x04004445 RID: 17477
		private AutomatonQueue automaton;

		// Token: 0x04004446 RID: 17478
		private AutomatonQueueContext context;

		// Token: 0x04004447 RID: 17479
		private static string[] traceLines = new string[]
		{
			"State: ReadAhead, Evt: Receive, Pre: MessageAvailable, Act: DequeueAndReturn, Stop", "State: ReadAhead, Evt: Receive, Pre: MoreDataToCome, Evt: CheckTimer", "State: ReadAhead, Evt: Receive, Act: SendRequestMsg, Evt: CheckTimer", "State: ReadAhead, Evt: CheckTimer, Pre: Wait, Act: SetTimer, State: ReadAheadWait, Stop", "State: ReadAhead, Evt: CheckTimer, State: ReadAheadReceiving, Stop", "State: ReadAhead, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Post: LastSegment, Evt: MakeMessage", "State: ReadAhead, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Stop", "State: ReadAhead, Evt: ServerData, Pre: CheckMsg, Stop", "State: ReadAhead, Evt: MakeMessage, Act: CreateAndEnqueue, Stop", "State: ReadAhead, Evt: Quiesced, Evt: QmFailed",
			"State: ReadAhead, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReadAhead, Evt: Close, Act: SendMqClose, State: DetachingReceive, Evt: Close"
		};
	}
}
