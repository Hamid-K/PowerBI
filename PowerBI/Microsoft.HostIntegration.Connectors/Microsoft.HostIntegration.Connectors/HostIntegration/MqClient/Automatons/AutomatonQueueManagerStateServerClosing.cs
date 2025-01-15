using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA2 RID: 2722
	internal class AutomatonQueueManagerStateServerClosing : StateAsCodeDriver
	{
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x0015B1C7 File Offset: 0x001593C7
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0015B1DA File Offset: 0x001593DA
		internal AutomatonQueueManagerStateServerClosing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0015B1FC File Offset: 0x001593FC
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
							case AutomatonQueueManagerEvent.ClientData:
								this.ActionReturnQuiesced();
								num = 0;
								goto IL_021D;
							case AutomatonQueueManagerEvent.AsyncSendDone:
								this.ActionMakeAsyncSent();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
								num = 7;
								goto IL_021D;
							case AutomatonQueueManagerEvent.MessageToQ:
								this.ActionSendMessageToQ();
								num = 8;
								goto IL_021D;
							case AutomatonQueueManagerEvent.QAttach:
								this.ActionReturnAttachQuiesced();
								num = 10;
								goto IL_021D;
							case AutomatonQueueManagerEvent.QDetach:
								if (!this.PreConditionDoClose())
								{
									this.ActionDetached();
									num = 13;
									goto IL_021D;
								}
								this.ActionEnqueueClose();
								if (this.PostConditionNoCurrentClose())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 11;
									goto IL_021D;
								}
								num = 12;
								goto IL_021D;
							case AutomatonQueueManagerEvent.MqCommand:
								if (this.PreConditionAsyncStatus())
								{
									this.ActionFailMqStatQuiescing();
									num = 14;
									goto IL_021D;
								}
								throw new InvalidOperationException("No Precondition returned true");
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 22;
								goto IL_021D;
							case AutomatonQueueManagerEvent.Enlist:
								this.ActionSetEnlistRcWaitEvent();
								num = 6;
								goto IL_021D;
							case AutomatonQueueManagerEvent.MessageMore:
								this.ActionSendMessageToQ();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 9;
								goto IL_021D;
							case AutomatonQueueManagerEvent.StartOpen:
								this.ActionSetCurrentOpenSendOpen();
								num = 16;
								goto IL_021D;
							case AutomatonQueueManagerEvent.StartClose:
								this.ActionSetCurrentCloseSendClose();
								num = 17;
								goto IL_021D;
							case AutomatonQueueManagerEvent.CheckMore:
								if (this.PostConditionMoreOpens())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
									num = 18;
									goto IL_021D;
								}
								if (this.PostConditionMoreCloses())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 19;
									goto IL_021D;
								}
								num = 20;
								goto IL_021D;
							case AutomatonQueueManagerEvent.InFailedState:
								this.ActionSetInFailedState();
								num = 21;
								goto IL_021D;
							}
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionForClient())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
							num = 1;
						}
						else if (this.PreConditionOpenOrClose())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageMore;
							num = 2;
						}
						else if (this.PreConditionMqStatReply())
						{
							this.ActionSetMqStatWaitEvent();
							num = 3;
						}
						else if (this.PreConditionQuiescedChannel())
						{
							automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.ChannelQuiesced;
							num = 4;
						}
						else
						{
							if (!this.PreConditionQuiescedQm())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							num = 5;
						}
					}
					else
					{
						this.ActionFailAllTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 15;
					}
					IL_021D:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateServerClosing.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: ServerClosing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0015B4DC File Offset: 0x001596DC
		public bool PreConditionForClient()
		{
			SegmentType segmentType = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType;
			if (segmentType > SegmentType.StatusData)
			{
				switch (segmentType)
				{
				case SegmentType.SocketAction:
					return false;
				case SegmentType.AsyncMessage:
				case SegmentType.Notification:
					break;
				case SegmentType.RequestMessages:
					goto IL_009A;
				default:
					switch (segmentType)
					{
					case SegmentType.MqConnectReply:
					case SegmentType.MqDisconnectReply:
						goto IL_008F;
					case SegmentType.MqOpenReply:
					case SegmentType.MqCloseReply:
						return false;
					case SegmentType.MqGetReply:
					case SegmentType.MqPutReply:
						break;
					case (SegmentType)151:
					case (SegmentType)152:
					case (SegmentType)153:
					case (SegmentType)154:
					case (SegmentType)155:
					case (SegmentType)156:
						goto IL_009A;
					case SegmentType.MqStatReply:
						return false;
					default:
						goto IL_009A;
					}
					break;
				}
				return true;
			}
			if (segmentType != SegmentType.InitialData && segmentType != SegmentType.StatusData)
			{
				goto IL_009A;
			}
			IL_008F:
			throw new InvalidOperationException("QueueManager data in DataFlow");
			IL_009A:
			throw new InvalidOperationException("Unknown Segment");
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0015B58D File Offset: 0x0015978D
		public bool PreConditionMqStatReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqStatReply;
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0015B5B8 File Offset: 0x001597B8
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0015B5F4 File Offset: 0x001597F4
		public bool PreConditionOpenOrClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply || passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0015B640 File Offset: 0x00159840
		public bool PreConditionQuiescedChannel()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.QuiescedChannel;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x0015B6B8 File Offset: 0x001598B8
		public bool PreConditionQuiescedQm()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.QuiescedQueueManager;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0015B72F File Offset: 0x0015992F
		public bool PreConditionAsyncStatus()
		{
			return this.context.CommandType == MqCommandType.AsyncStatus;
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0015B740 File Offset: 0x00159940
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

		// Token: 0x0600555C RID: 21852 RVA: 0x0015B888 File Offset: 0x00159A88
		public void ActionMakeAsyncSent()
		{
			AsynchronousConnectionMessage dataMessageFromTcp = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			dataMessageFromTcp.Change(204);
			this.context.MessageToQueue = dataMessageFromTcp;
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0015B8C4 File Offset: 0x00159AC4
		public void ActionSetInFailedState()
		{
			this.context.InFailedState = true;
			if (this.context.DisconnectRequested)
			{
				this.context.DisconnectRequested = false;
				this.context.DisconnectedEvent.Set();
			}
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x0015B8FC File Offset: 0x00159AFC
		public void ActionReturnAttachQuiesced()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Queue failed to open as Queue Manager is Quiescing");
			}
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			this.context.DataMessageFromQueue = null;
			QAttachFailed qattachFailed = new QAttachFailed();
			qattachFailed.QueueDeterminant = passThroughData.QueueDeterminant;
			qattachFailed.ReturnCode = ReturnCode.QOpenFailedQmQuiesced;
			this.automaton.SendToQueue(202, qattachFailed);
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x0015B984 File Offset: 0x00159B84
		public void ActionReturnQuiesced()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			passThroughData.ReturnBuffers();
			dataMessageFromQueue.Change(205);
			this.automaton.SendToQueue(dataMessageFromQueue);
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x0015B9DA File Offset: 0x00159BDA
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x0015BA10 File Offset: 0x00159C10
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

		// Token: 0x06005562 RID: 21858 RVA: 0x0015BA78 File Offset: 0x00159C78
		public void ActionSetCurrentOpenSendOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Opening Queue");
			}
			this.context.CurrentOpen = this.context.QueuedOpens.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentOpen);
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0015BAE4 File Offset: 0x00159CE4
		public void ActionSetCurrentCloseSendClose()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Closing Queue");
			}
			this.context.CurrentClose = this.context.QueuedCloses.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentClose);
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0015BB4D File Offset: 0x00159D4D
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x0015BB68 File Offset: 0x00159D68
		public void ActionFailAllTcp()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Failing all Opens as the Socket has failed");
			}
			foreach (PassThroughData passThroughData in this.context.QueuedOpens)
			{
				QAttachFailed qattachFailed = new QAttachFailed();
				qattachFailed.QueueDeterminant = passThroughData.QueueDeterminant;
				qattachFailed.ReturnCode = ReturnCode.QOpenFailedTcpFailed;
				this.automaton.SendToQueue(202, qattachFailed);
			}
			if (this.context.CurrentOpen != null)
			{
				QAttachFailed qattachFailed2 = new QAttachFailed();
				qattachFailed2.QueueDeterminant = this.context.CurrentOpen.QueueDeterminant;
				qattachFailed2.ReturnCode = ReturnCode.QOpenFailedTcpFailed;
				this.automaton.SendToQueue(202, qattachFailed2);
			}
			foreach (PassThroughData passThroughData2 in this.context.QueuedCloses)
			{
				DeterminantMessage determinantMessage = new DeterminantMessage();
				determinantMessage.QueueDeterminant = passThroughData2.QueueDeterminant;
				this.automaton.SendToQueue(203, determinantMessage);
			}
			if (this.context.CurrentClose != null)
			{
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

		// Token: 0x06005566 RID: 21862 RVA: 0x0015BD28 File Offset: 0x00159F28
		public void ActionFailMqStatQuiescing()
		{
			this.context.CommandReturnCode = ReturnCode.QmAsyncStatusFailedQuiescing;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x0015BD4C File Offset: 0x00159F4C
		public void ActionSetMqStatWaitEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.automaton.ExtractAsyncStatus(passThroughData.Buffers[0]);
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x0015BD97 File Offset: 0x00159F97
		public void ActionSetEnlistRcWaitEvent()
		{
			this.context.EnlistQueued = false;
			this.context.EnlistReturnCode = ReturnCode.EnlistServerClosing;
			this.context.EnlistEvent.Set();
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x0015BDC6 File Offset: 0x00159FC6
		public bool PostConditionNoCurrentClose()
		{
			return this.context.CurrentClose == null;
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x0015BDD6 File Offset: 0x00159FD6
		public bool PostConditionMoreOpens()
		{
			return this.context.QueuedOpens.Count != 0;
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0015BDEB File Offset: 0x00159FEB
		public bool PostConditionMoreCloses()
		{
			return this.context.QueuedCloses.Count != 0;
		}

		// Token: 0x040043B0 RID: 17328
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.ServerClosing;

		// Token: 0x040043B1 RID: 17329
		private AutomatonQueueManager automaton;

		// Token: 0x040043B2 RID: 17330
		private AutomatonQueueManagerContext context;

		// Token: 0x040043B3 RID: 17331
		private static string[] traceLines = new string[]
		{
			"State: ServerClosing, Evt: ClientData, Act: ReturnQuiesced, Stop", "State: ServerClosing, Evt: ServerData, Pre: ForClient, Act: MakeClientMessage, Evt: MessageToQ", "State: ServerClosing, Evt: ServerData, Pre: OpenOrClose, Act: MakeClientMessage, Evt: MessageMore", "State: ServerClosing, Evt: ServerData, Pre: MqStatReply, Act: SetMqStatWaitEvent, Stop", "State: ServerClosing, Evt: ServerData, Pre: QuiescedChannel, State: DataFlowFailed, Evt: ChannelQuiesced", "State: ServerClosing, Evt: ServerData, Pre: QuiescedQm, Stop", "State: ServerClosing, Evt: Enlist, Act: SetEnlistRcWaitEvent, Stop", "State: ServerClosing, Evt: AsyncSendDone, Act: MakeAsyncSent, Evt: MessageToQ", "State: ServerClosing, Evt: MessageToQ, Act: SendMessageToQ, Stop", "State: ServerClosing, Evt: MessageMore, Act: SendMessageToQ, Evt: CheckMore",
			"State: ServerClosing, Evt: QAttach, Act: ReturnAttachQuiesced, Stop", "State: ServerClosing, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Post: NoCurrentClose, Evt: StartClose", "State: ServerClosing, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: ServerClosing, Evt: QDetach, Act: Detached, Stop", "State: ServerClosing, Evt: MqCommand, Pre: AsyncStatus, Act: FailMqStatQuiescing, Stop", "State: ServerClosing, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed", "State: ServerClosing, Evt: StartOpen, Act: SetCurrentOpenSendOpen, Stop", "State: ServerClosing, Evt: StartClose, Act: SetCurrentCloseSendClose, Stop", "State: ServerClosing, Evt: CheckMore, Post: MoreOpens, Evt: StartOpen", "State: ServerClosing, Evt: CheckMore, Post: MoreCloses, Evt: StartClose",
			"State: ServerClosing, Evt: CheckMore, Stop", "State: ServerClosing, Evt: InFailedState, Act: SetInFailedState, Stop", "State: ServerClosing, Evt: QueueDisconnected, Stop"
		};
	}
}
