using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AC9 RID: 2761
	internal class AutomatonQueueStateReadAheadWaited : StateAsCodeDriver
	{
		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x0600578B RID: 22411 RVA: 0x00167AF9 File Offset: 0x00165CF9
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x00167B0C File Offset: 0x00165D0C
		internal AutomatonQueueStateReadAheadWaited(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x00167B2C File Offset: 0x00165D2C
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
							goto IL_00EB;
						case AutomatonQueueEvent.QmFailed:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingReceive;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailedGet;
							num = 7;
							goto IL_00EB;
						case AutomatonQueueEvent.QuiesceDetach:
							this.ActionDetachManager();
							automatonQueueState = AutomatonQueueState.DetachingReceive;
							automatonQueueEvent2 = AutomatonQueueEvent.QmFailed;
							num = 6;
							goto IL_00EB;
						case AutomatonQueueEvent.MakeMessage:
							this.ActionMessageFromAsync();
							automatonQueueEvent2 = AutomatonQueueEvent.MessageReceived;
							num = 3;
							goto IL_00EB;
						case AutomatonQueueEvent.MessageReceived:
							this.ActionSetWaitReceiveEvent();
							automatonQueueState = AutomatonQueueState.ReadAhead;
							num = 4;
							goto IL_00EB;
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
						this.ActionSet2033ReceiveEvent();
						automatonQueueState = AutomatonQueueState.ReadAhead;
						num = 2;
					}
					IL_00EB:
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateReadAheadWaited.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ReadAheadWaited, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00167CDC File Offset: 0x00165EDC
		public bool PreConditionAsyncMessage()
		{
			return (this.context.MessageFromQueueManager.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage;
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x00167D04 File Offset: 0x00165F04
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

		// Token: 0x06005790 RID: 22416 RVA: 0x00167D80 File Offset: 0x00165F80
		public void ActionQueueAndCheck()
		{
			PassThroughData passThroughData = this.context.MessageFromQueueManager.Contents as PassThroughData;
			this.context.MessageFromQueueManager = null;
			this.context.SegmentsReceived.Enqueue(passThroughData);
			ControlFlag1 controlFlag = passThroughData.SegmentHeaderInformation.ControlFlag1;
			this.context.CurrentAsyncMessageStatus = (((controlFlag & ControlFlag1.LastSegment) == ControlFlag1.LastSegment) ? AsyncMessageStatus.LastSegment : AsyncMessageStatus.None);
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x00167DE3 File Offset: 0x00165FE3
		public void ActionSet2033ReceiveEvent()
		{
			this.context.ReceiveReturnCode = (ReturnCode)2033;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x00167E08 File Offset: 0x00166008
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

		// Token: 0x06005793 RID: 22419 RVA: 0x00168118 File Offset: 0x00166318
		public void ActionSetWaitReceiveEvent()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x0016812B File Offset: 0x0016632B
		public void ActionQuiesceRcSetEvent()
		{
			this.context.ReceiveReturnCode = ReturnCode.QReceiveFailedQmQuiesced;
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00168150 File Offset: 0x00166350
		public void ActionDetachManager()
		{
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			this.automaton.SendToQueueManager(301, passThroughData);
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x00168196 File Offset: 0x00166396
		public bool PostConditionLastSegment()
		{
			return this.context.CurrentAsyncMessageStatus == AsyncMessageStatus.LastSegment;
		}

		// Token: 0x0400444C RID: 17484
		private AutomatonQueueState stateNumber = AutomatonQueueState.ReadAheadWaited;

		// Token: 0x0400444D RID: 17485
		private AutomatonQueue automaton;

		// Token: 0x0400444E RID: 17486
		private AutomatonQueueContext context;

		// Token: 0x0400444F RID: 17487
		private static string[] traceLines = new string[] { "State: ReadAheadWaited, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Post: LastSegment, Evt: MakeMessage", "State: ReadAheadWaited, Evt: ServerData, Pre: AsyncMessage, Act: QueueAndCheck, Stop", "State: ReadAheadWaited, Evt: ServerData, Pre: CheckMsg, Act: Set2033ReceiveEvent, State: ReadAhead, Stop", "State: ReadAheadWaited, Evt: MakeMessage, Act: MessageFromAsync, Evt: MessageReceived", "State: ReadAheadWaited, Evt: MessageReceived, Act: SetWaitReceiveEvent, State: ReadAhead, Stop", "State: ReadAheadWaited, Evt: Quiesced, Act: QuiesceRcSetEvent, Evt: QuiesceDetach", "State: ReadAheadWaited, Evt: QuiesceDetach, Act: DetachManager, State: DetachingReceive, Evt: QmFailed", "State: ReadAheadWaited, Evt: QmFailed, Act: DetachManager, State: DetachingReceive, Evt: QmFailedGet" };
	}
}
