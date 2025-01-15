using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACA RID: 2762
	internal class AutomatonQueueStateReadAheadReceiving : StateAsCodeDriver
	{
		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x00168200 File Offset: 0x00166400
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x00168213 File Offset: 0x00166413
		internal AutomatonQueueStateReadAheadReceiving(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x00168234 File Offset: 0x00166434
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
							num = 5;
							goto IL_00DD;
						case AutomatonQueueEvent.QmFailed:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingReceive;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailedGet;
							num = 7;
							goto IL_00DD;
						case AutomatonQueueEvent.QuiesceDetach:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingReceive;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
							num = 6;
							goto IL_00DD;
						case AutomatonQueueEvent.MakeMessage:
							this.ActionMessageFromAsync();
							automatonQueueEvent2 = AutomatonQueueEvent.MessageReceived;
							num = 3;
							goto IL_00DD;
						case AutomatonQueueEvent.MessageReceived:
							this.ActionSetWaitReceiveEvent();
							automatonQueueState = AutomatonQueueState.ReadAhead;
							num = 4;
							goto IL_00DD;
						}
						throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
					}
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
					else
					{
						if (!this.PreConditionCheckMsg())
						{
							throw new InvalidOperationException("No Precondition returned true");
						}
						num = 2;
					}
					IL_00DD:
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReadAheadReceiving.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReadAheadReceiving, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x001683D4 File Offset: 0x001665D4
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x001683FC File Offset: 0x001665FC
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

		// Token: 0x0600579D RID: 22429 RVA: 0x00168478 File Offset: 0x00166678
		public void ActionQueueAndCheck()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x001684DC File Offset: 0x001666DC
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
					num2 = receiveMessage.ExtractHeaders(passThroughData.Buffers[0].Data, passThroughData.SegmentHeaderInformation.TotalLengthOfSegment, num7, asyncMessageInformation.TruncatedLength == asyncMessageInformation.OriginalMessageLength);
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

		// Token: 0x0600579F RID: 22431 RVA: 0x001687EC File Offset: 0x001669EC
		public void ActionSetWaitReceiveEvent()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x001687FF File Offset: 0x001669FF
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x00168824 File Offset: 0x00166A24
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x0016886A File Offset: 0x00166A6A
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x04004450 RID: 17488
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReadAheadReceiving;

		// Token: 0x04004451 RID: 17489
		private AutomatonQueue automaton;

		// Token: 0x04004452 RID: 17490
		private AutomatonQueueContext context;

		// Token: 0x04004453 RID: 17491
		private static string[] traceLines = new string[] { "State: ReadAheadReceiving, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Post: LastSegment, Evt: MakeMessage", "State: ReadAheadReceiving, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Stop", "State: ReadAheadReceiving, Evt: ServerData, Pre: CheckMsg, Stop", "State: ReadAheadReceiving, Evt: MakeMessage, Act: MessageFromAsync, Evt: MessageReceived", "State: ReadAheadReceiving, Evt: MessageReceived, Act: SetWaitReceiveEvent, State: ReadAhead, Stop", "State: ReadAheadReceiving, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: ReadAheadReceiving, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReadAheadReceiving, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet" };
	}
}
