using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA7 RID: 2727
	internal class AutomatonQueueManagerStateOpenCloseTxnOpen : StateAsCodeDriver
	{
		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x0015D63C File Offset: 0x0015B83C
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x0015D64F File Offset: 0x0015B84F
		internal AutomatonQueueManagerStateOpenCloseTxnOpen(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x0015D670 File Offset: 0x0015B870
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
								goto IL_01B0;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 6;
									goto IL_01B0;
								}
								this.ActionDetached();
								num = 7;
								goto IL_01B0;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 8;
								goto IL_01B0;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 16;
								goto IL_01B0;
							case AutomatonQueueManagerEvent.MessageMore:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
									num = 3;
									goto IL_01B0;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.MessageClose:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									num = 4;
									goto IL_01B0;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.StartOpen:
								this.ActionSetCurrentOpenSendOpen();
								num = 10;
								goto IL_01B0;
							case AutomatonQueueManagerEvent.StartClose:
								this.ActionSetCurrentCloseSendClose();
								num = 11;
								goto IL_01B0;
							case AutomatonQueueManagerEvent.CheckMore:
								if (this.PostConditionMoreOpens())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
									num = 12;
									goto IL_01B0;
								}
								if (this.PostConditionMoreCloses())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 13;
									goto IL_01B0;
								}
								if (this.PostConditionDisconnectQueued())
								{
									automatonQueueManagerState = AutomatonQueueManagerState.TxnOpen;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Disconnect;
									num = 14;
									goto IL_01B0;
								}
								automatonQueueManagerState = AutomatonQueueManagerState.TxnOpen;
								num = 15;
								goto IL_01B0;
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
					IL_01B0:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateOpenCloseTxnOpen.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: OpenCloseTxnOpen, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x0015D8E4 File Offset: 0x0015BAE4
		public bool PreConditionOpen()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply;
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0015D90C File Offset: 0x0015BB0C
		public bool PreConditionClose()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0015D934 File Offset: 0x0015BB34
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0015D9AC File Offset: 0x0015BBAC
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0015D9E8 File Offset: 0x0015BBE8
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

		// Token: 0x060055B1 RID: 21937 RVA: 0x0015DB2F File Offset: 0x0015BD2F
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x0015DB47 File Offset: 0x0015BD47
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x0015DB7A File Offset: 0x0015BD7A
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x0015DBB0 File Offset: 0x0015BDB0
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

		// Token: 0x060055B5 RID: 21941 RVA: 0x0015DC17 File Offset: 0x0015BE17
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0015DC28 File Offset: 0x0015BE28
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

		// Token: 0x060055B7 RID: 21943 RVA: 0x0015DE14 File Offset: 0x0015C014
		public void ActionSetCurrentOpenSendOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Opening Queue");
			}
			this.context.CurrentOpen = this.context.QueuedOpens.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentOpen);
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x0015DE80 File Offset: 0x0015C080
		public void ActionSetCurrentCloseSendClose()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Closing Queue");
			}
			this.context.CurrentClose = this.context.QueuedCloses.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentClose);
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x0015DEE9 File Offset: 0x0015C0E9
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x0015DEF6 File Offset: 0x0015C0F6
		public bool PostConditionMoreOpens()
		{
			return this.context.QueuedOpens.Count != 0;
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x0015DF0B File Offset: 0x0015C10B
		public bool PostConditionMoreCloses()
		{
			return this.context.QueuedCloses.Count != 0;
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0015DF20 File Offset: 0x0015C120
		public bool PostConditionDisconnectQueued()
		{
			return this.context.DisconnectRequested;
		}

		// Token: 0x040043C4 RID: 17348
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.OpenCloseTxnOpen;

		// Token: 0x040043C5 RID: 17349
		private AutomatonQueueManager automaton;

		// Token: 0x040043C6 RID: 17350
		private AutomatonQueueManagerContext context;

		// Token: 0x040043C7 RID: 17351
		private static string[] traceLines = new string[]
		{
			"State: OpenCloseTxnOpen, Evt: ServerData, Pre: Open, Act: MakeClientMessage, Evt: MessageMore", "State: OpenCloseTxnOpen, Evt: ServerData, Pre: Close, Act: MakeClientMessage, Evt: MessageClose", "State: OpenCloseTxnOpen, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: OpenCloseTxnOpen, Evt: MessageMore, Act: SendMessageToQ, Post: QueueFound, Evt: CheckMore", "State: OpenCloseTxnOpen, Evt: MessageClose, Act: SendMessageToQ, Post: QueueFound, Stop", "State: OpenCloseTxnOpen, Evt: QAttach, Act: EnqueueOpen, Stop", "State: OpenCloseTxnOpen, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: OpenCloseTxnOpen, Evt: QDetach, Act: Detached, Stop", "State: OpenCloseTxnOpen, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: OpenCloseTxnOpen, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed",
			"State: OpenCloseTxnOpen, Evt: StartOpen, Act: SetCurrentOpenSendOpen, Stop", "State: OpenCloseTxnOpen, Evt: StartClose, Act: SetCurrentCloseSendClose, Stop", "State: OpenCloseTxnOpen, Evt: CheckMore, Post: MoreOpens, Evt: StartOpen", "State: OpenCloseTxnOpen, Evt: CheckMore, Post: MoreCloses, Evt: StartClose", "State: OpenCloseTxnOpen, Evt: CheckMore, Post: DisconnectQueued, State: TxnOpen, Evt: Disconnect", "State: OpenCloseTxnOpen, Evt: CheckMore, State: TxnOpen, Stop", "State: OpenCloseTxnOpen, Evt: QueueDisconnected, Evt: CheckMore"
		};
	}
}
