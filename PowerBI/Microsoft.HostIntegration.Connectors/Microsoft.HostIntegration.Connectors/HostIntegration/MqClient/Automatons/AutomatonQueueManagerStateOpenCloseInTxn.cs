using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA6 RID: 2726
	internal class AutomatonQueueManagerStateOpenCloseInTxn : StateAsCodeDriver
	{
		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x0015CC03 File Offset: 0x0015AE03
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0015CC16 File Offset: 0x0015AE16
		internal AutomatonQueueManagerStateOpenCloseInTxn(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0015CC38 File Offset: 0x0015AE38
		public override int Process(ref int eventToProcess)
		{
			AutomatonQueueManagerEvent automatonQueueManagerEvent = (AutomatonQueueManagerEvent)eventToProcess;
			AutomatonQueueManagerState automatonQueueManagerState = this.stateNumber;
			try
			{
				while (automatonQueueManagerState == this.stateNumber && automatonQueueManagerEvent != AutomatonQueueManagerEvent.Stop)
				{
					AutomatonQueueManagerEvent automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Stop;
					int num;
					if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.ServerData)
						{
							switch (automatonQueueManagerEvent)
							{
							case AutomatonQueueManagerEvent.QAttach:
								this.ActionEnqueueOpen();
								num = 5;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 6;
									goto IL_01F5;
								}
								this.ActionDetached();
								num = 7;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 8;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.MqCommand:
								this.ActionMqCommandReceived();
								num = 18;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 19;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.MessageMore:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
									num = 3;
									goto IL_01F5;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.MessageClose:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									num = 4;
									goto IL_01F5;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.StartOpen:
								this.ActionSetCurrentOpenSendOpen();
								num = 12;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.StartClose:
								this.ActionSetCurrentCloseSendClose();
								num = 13;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.CheckMore:
								if (this.PostConditionMoreCloses())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 14;
									goto IL_01F5;
								}
								if (this.PostConditionMoreOpens())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
									num = 15;
									goto IL_01F5;
								}
								if (this.PostConditionEndOrRollback())
								{
									automatonQueueManagerState = AutomatonQueueManagerState.InTxn;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MqCommand;
									num = 16;
									goto IL_01F5;
								}
								automatonQueueManagerState = AutomatonQueueManagerState.InTxn;
								num = 17;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.FirstOpen:
								this.ActionNoMqCommandReceived();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
								num = 10;
								goto IL_01F5;
							case AutomatonQueueManagerEvent.FirstClose:
								this.ActionNoMqCommandReceived();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
								num = 11;
								goto IL_01F5;
							}
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionOpen())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageMore;
							num = 0;
						}
						else if (this.PreConditionClose())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageClose;
							num = 1;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 2;
						}
					}
					else
					{
						this.ActionFailAllTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 9;
					}
					IL_01F5:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateOpenCloseInTxn.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: OpenCloseInTxn, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0015CEF0 File Offset: 0x0015B0F0
		public bool PreConditionOpen()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply;
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0015CF18 File Offset: 0x0015B118
		public bool PreConditionClose()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0015CF40 File Offset: 0x0015B140
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x0015CFB8 File Offset: 0x0015B1B8
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x0015CFF4 File Offset: 0x0015B1F4
		public void ActionMakeClientMessage()
		{
			this.context.MessageToQueue = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			PassThroughData passThroughData = this.context.MessageToQueue.Contents as PassThroughData;
			if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqGetReply)
			{
				if ((passThroughData.SegmentHeaderInformation.ControlFlag1 & ControlFlag1.FirstSegment) == ControlFlag1.FirstSegment)
				{
					this.context.LastGetReplyObjectHandle = passThroughData.QueueDeterminant;
				}
				else
				{
					passThroughData.QueueDeterminant = this.context.LastGetReplyObjectHandle;
				}
			}
			else if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.AsyncMessage)
			{
				this.context.GlobalMessageIndex = ConversionHelpers.ExtractIntFromBuffer(passThroughData.Buffers[0].Data, passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader + 12, passThroughData.SegmentHeaderInformation.LittleEndian);
			}
			else if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply)
			{
				passThroughData.QueueDeterminant = this.context.CurrentOpen.QueueDeterminant;
			}
			else if (passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply)
			{
				passThroughData.QueueDeterminant = this.context.CurrentClose.QueueDeterminant;
			}
			this.context.MessageToQueue.Change(200);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x0015D13B File Offset: 0x0015B33B
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x0015D153 File Offset: 0x0015B353
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x0015D186 File Offset: 0x0015B386
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0015D1BC File Offset: 0x0015B3BC
		public void ActionDetached()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Queue Closed");
			}
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			dataMessageFromQueue.Change(203);
			this.automaton.SendToQueue(dataMessageFromQueue);
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0015D223 File Offset: 0x0015B423
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0015D234 File Offset: 0x0015B434
		public void ActionFailAllTcp()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Failing all Opens as the Socket has failed");
			}
			foreach (PassThroughData passThroughData in this.context.QueuedOpens)
			{
				passThroughData.ReturnBuffers();
				QAttachFailed qattachFailed = new QAttachFailed();
				qattachFailed.QueueDeterminant = passThroughData.QueueDeterminant;
				qattachFailed.ReturnCode = ReturnCode.QOpenFailedTcpFailed;
				this.automaton.SendToQueue(202, qattachFailed);
			}
			if (this.context.CurrentOpen != null)
			{
				this.context.CurrentOpen.ReturnBuffers();
				QAttachFailed qattachFailed2 = new QAttachFailed();
				qattachFailed2.QueueDeterminant = this.context.CurrentOpen.QueueDeterminant;
				qattachFailed2.ReturnCode = ReturnCode.QOpenFailedTcpFailed;
				this.automaton.SendToQueue(202, qattachFailed2);
			}
			foreach (PassThroughData passThroughData2 in this.context.QueuedCloses)
			{
				passThroughData2.ReturnBuffers();
				DeterminantMessage determinantMessage = new DeterminantMessage();
				determinantMessage.QueueDeterminant = passThroughData2.QueueDeterminant;
				this.automaton.SendToQueue(203, determinantMessage);
			}
			if (this.context.CurrentClose != null)
			{
				this.context.CurrentClose.ReturnBuffers();
				DeterminantMessage determinantMessage2 = new DeterminantMessage();
				determinantMessage2.QueueDeterminant = this.context.CurrentClose.QueueDeterminant;
				this.automaton.SendToQueue(203, determinantMessage2);
			}
			if (this.context.DisconnectRequested)
			{
				this.context.DisconnectRequested = false;
				this.context.DisconnectedEvent.Set();
			}
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x0015D420 File Offset: 0x0015B620
		public void ActionSetCurrentOpenSendOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Opening Queue");
			}
			this.context.CurrentOpen = this.context.QueuedOpens.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentOpen);
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x0015D48C File Offset: 0x0015B68C
		public void ActionSetCurrentCloseSendClose()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Closing Queue");
			}
			this.context.CurrentClose = this.context.QueuedCloses.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentClose);
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x0015D4F5 File Offset: 0x0015B6F5
		public void ActionNoMqCommandReceived()
		{
			this.context.MqCommandProcessed = false;
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x0015D503 File Offset: 0x0015B703
		public void ActionMqCommandReceived()
		{
			this.context.MqCommandProcessed = true;
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x0015D511 File Offset: 0x0015B711
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0015D51E File Offset: 0x0015B71E
		public bool PostConditionMoreOpens()
		{
			return this.context.QueuedOpens.Count != 0;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0015D533 File Offset: 0x0015B733
		public bool PostConditionMoreCloses()
		{
			return this.context.QueuedCloses.Count != 0;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x0015D548 File Offset: 0x0015B748
		public bool PostConditionEndOrRollback()
		{
			return this.context.MqCommandProcessed && (this.context.CommandType == MqCommandType.XaEnd || this.context.CommandType == MqCommandType.XaRollback);
		}

		// Token: 0x040043C0 RID: 17344
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.OpenCloseInTxn;

		// Token: 0x040043C1 RID: 17345
		private AutomatonQueueManager automaton;

		// Token: 0x040043C2 RID: 17346
		private AutomatonQueueManagerContext context;

		// Token: 0x040043C3 RID: 17347
		private static string[] traceLines = new string[]
		{
			"State: OpenCloseInTxn, Evt: ServerData, Pre: Open, Act: MakeClientMessage, Evt: MessageMore", "State: OpenCloseInTxn, Evt: ServerData, Pre: Close, Act: MakeClientMessage, Evt: MessageClose", "State: OpenCloseInTxn, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: OpenCloseInTxn, Evt: MessageMore, Act: SendMessageToQ, Post: QueueFound, Evt: CheckMore", "State: OpenCloseInTxn, Evt: MessageClose, Act: SendMessageToQ, Post: QueueFound, Stop", "State: OpenCloseInTxn, Evt: QAttach, Act: EnqueueOpen, Stop", "State: OpenCloseInTxn, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: OpenCloseInTxn, Evt: QDetach, Act: Detached, Stop", "State: OpenCloseInTxn, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: OpenCloseInTxn, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed",
			"State: OpenCloseInTxn, Evt: FirstOpen, Act: NoMqCommandReceived, Evt: StartOpen", "State: OpenCloseInTxn, Evt: FirstClose, Act: NoMqCommandReceived, Evt: StartClose", "State: OpenCloseInTxn, Evt: StartOpen, Act: SetCurrentOpenSendOpen, Stop", "State: OpenCloseInTxn, Evt: StartClose, Act: SetCurrentCloseSendClose, Stop", "State: OpenCloseInTxn, Evt: CheckMore, Post: MoreCloses, Evt: StartClose", "State: OpenCloseInTxn, Evt: CheckMore, Post: MoreOpens, Evt: StartOpen", "State: OpenCloseInTxn, Evt: CheckMore, Post: EndOrRollback, State: InTxn, Evt: MqCommand", "State: OpenCloseInTxn, Evt: CheckMore, State: InTxn, Stop", "State: OpenCloseInTxn, Evt: MqCommand, Act: MqCommandReceived, Stop", "State: OpenCloseInTxn, Evt: QueueDisconnected, Evt: CheckMore"
		};
	}
}
