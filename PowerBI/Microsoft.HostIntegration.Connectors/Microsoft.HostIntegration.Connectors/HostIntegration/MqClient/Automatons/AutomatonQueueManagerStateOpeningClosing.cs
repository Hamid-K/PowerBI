using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA1 RID: 2721
	internal class AutomatonQueueManagerStateOpeningClosing : StateAsCodeDriver
	{
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06005535 RID: 21813 RVA: 0x0015A535 File Offset: 0x00158735
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0015A548 File Offset: 0x00158748
		internal AutomatonQueueManagerStateOpeningClosing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0015A568 File Offset: 0x00158768
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
								this.ActionPassToTcp();
								num = 0;
								goto IL_024A;
							case AutomatonQueueManagerEvent.AsyncSendDone:
								this.ActionMakeAsyncSent();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
								num = 6;
								goto IL_024A;
							case AutomatonQueueManagerEvent.MessageToQ:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									num = 7;
									goto IL_024A;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.QAttach:
								this.ActionEnqueueOpen();
								num = 10;
								goto IL_024A;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 11;
									goto IL_024A;
								}
								this.ActionDetached();
								num = 12;
								goto IL_024A;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 14;
								goto IL_024A;
							case AutomatonQueueManagerEvent.MqCommand:
								if (this.PreConditionAsyncStatus())
								{
									this.ActionSendMqStat();
									num = 13;
									goto IL_024A;
								}
								throw new InvalidOperationException("No Precondition returned true");
							case AutomatonQueueManagerEvent.QueueDisconnected:
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 22;
								goto IL_024A;
							case AutomatonQueueManagerEvent.MessageMore:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
									num = 8;
									goto IL_024A;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.MessageClose:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									num = 9;
									goto IL_024A;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.StartOpen:
								this.ActionSetCurrentOpenSendOpen();
								num = 16;
								goto IL_024A;
							case AutomatonQueueManagerEvent.StartClose:
								this.ActionSetCurrentCloseSendClose();
								num = 17;
								goto IL_024A;
							case AutomatonQueueManagerEvent.CheckMore:
								if (this.PostConditionMoreOpens())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
									num = 18;
									goto IL_024A;
								}
								if (this.PostConditionMoreCloses())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 19;
									goto IL_024A;
								}
								if (this.PostConditionDisconnectQueued())
								{
									automatonQueueManagerState = AutomatonQueueManagerState.DataFlow;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Disconnect;
									num = 20;
									goto IL_024A;
								}
								automatonQueueManagerState = AutomatonQueueManagerState.DataFlow;
								num = 21;
								goto IL_024A;
							}
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionForClient())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageToQ;
							num = 1;
						}
						else if (this.PreConditionOpen())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageMore;
							num = 2;
						}
						else if (this.PreConditionClose())
						{
							this.ActionMakeClientMessage();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.MessageClose;
							num = 3;
						}
						else if (this.PreConditionMqStatReply())
						{
							this.ActionSetMqStatWaitEvent();
							num = 4;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
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
					IL_024A:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateOpeningClosing.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: OpeningClosing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0015A878 File Offset: 0x00158A78
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

		// Token: 0x06005539 RID: 21817 RVA: 0x0015A929 File Offset: 0x00158B29
		public bool PreConditionMqStatReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqStatReply;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0015A954 File Offset: 0x00158B54
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0015A98F File Offset: 0x00158B8F
		public bool PreConditionOpen()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0015A9B7 File Offset: 0x00158BB7
		public bool PreConditionClose()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0015A9E0 File Offset: 0x00158BE0
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0015AA58 File Offset: 0x00158C58
		public bool PreConditionAsyncStatus()
		{
			return this.context.CommandType == MqCommandType.AsyncStatus;
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0015AA68 File Offset: 0x00158C68
		public void ActionPassToTcp()
		{
			AsynchronousConnectionMessage dataMessageFromQueue = this.context.DataMessageFromQueue;
			this.context.DataMessageFromQueue = null;
			dataMessageFromQueue.Change((dataMessageFromQueue.SubType == 350) ? 104 : 102);
			this.automaton.SendToTcp(dataMessageFromQueue);
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0015AAB4 File Offset: 0x00158CB4
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

		// Token: 0x06005541 RID: 21825 RVA: 0x0015ABFC File Offset: 0x00158DFC
		public void ActionMakeAsyncSent()
		{
			AsynchronousConnectionMessage dataMessageFromTcp = this.context.DataMessageFromTcp;
			this.context.DataMessageFromTcp = null;
			dataMessageFromTcp.Change(204);
			this.context.MessageToQueue = dataMessageFromTcp;
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0015AC38 File Offset: 0x00158E38
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x0015AC6C File Offset: 0x00158E6C
		public void ActionSetCurrentOpenSendOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Opening Queue");
			}
			this.context.CurrentOpen = this.context.QueuedOpens.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentOpen);
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0015ACD5 File Offset: 0x00158ED5
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0015AD08 File Offset: 0x00158F08
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

		// Token: 0x06005546 RID: 21830 RVA: 0x0015AD70 File Offset: 0x00158F70
		public void ActionSetCurrentCloseSendClose()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Closing Queue");
			}
			this.context.CurrentClose = this.context.QueuedCloses.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentClose);
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x0015ADD9 File Offset: 0x00158FD9
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0015ADF4 File Offset: 0x00158FF4
		public void ActionSendMqStat()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			AutomatonQueueManager.GenerateMqStat(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending MQ STAT");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0015AE55 File Offset: 0x00159055
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0015AE64 File Offset: 0x00159064
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

		// Token: 0x0600554B RID: 21835 RVA: 0x0015B050 File Offset: 0x00159250
		public void ActionSetMqStatWaitEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			this.automaton.ExtractAsyncStatus(passThroughData.Buffers[0]);
			passThroughData.ReturnBuffers();
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x0015B0A1 File Offset: 0x001592A1
		public bool PostConditionMoreOpens()
		{
			return this.context.QueuedOpens.Count != 0;
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0015B0B6 File Offset: 0x001592B6
		public bool PostConditionMoreCloses()
		{
			return this.context.QueuedCloses.Count != 0;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0015B0CB File Offset: 0x001592CB
		public bool PostConditionDisconnectQueued()
		{
			return this.context.DisconnectRequested;
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0015B0D8 File Offset: 0x001592D8
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x040043AC RID: 17324
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.OpeningClosing;

		// Token: 0x040043AD RID: 17325
		private AutomatonQueueManager automaton;

		// Token: 0x040043AE RID: 17326
		private AutomatonQueueManagerContext context;

		// Token: 0x040043AF RID: 17327
		private static string[] traceLines = new string[]
		{
			"State: OpeningClosing, Evt: ClientData, Act: PassToTcp, Stop", "State: OpeningClosing, Evt: ServerData, Pre: ForClient, Act: MakeClientMessage, Evt: MessageToQ", "State: OpeningClosing, Evt: ServerData, Pre: Open, Act: MakeClientMessage, Evt: MessageMore", "State: OpeningClosing, Evt: ServerData, Pre: Close, Act: MakeClientMessage, Evt: MessageClose", "State: OpeningClosing, Evt: ServerData, Pre: MqStatReply, Act: SetMqStatWaitEvent, Stop", "State: OpeningClosing, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: OpeningClosing, Evt: AsyncSendDone, Act: MakeAsyncSent, Evt: MessageToQ", "State: OpeningClosing, Evt: MessageToQ, Act: SendMessageToQ, Post: QueueFound, Stop", "State: OpeningClosing, Evt: MessageMore, Act: SendMessageToQ, Post: QueueFound, Evt: CheckMore", "State: OpeningClosing, Evt: MessageClose, Act: SendMessageToQ, Post: QueueFound, Stop",
			"State: OpeningClosing, Evt: QAttach, Act: EnqueueOpen, Stop", "State: OpeningClosing, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: OpeningClosing, Evt: QDetach, Act: Detached, Stop", "State: OpeningClosing, Evt: MqCommand, Pre: AsyncStatus, Act: SendMqStat, Stop", "State: OpeningClosing, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: OpeningClosing, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed", "State: OpeningClosing, Evt: StartOpen, Act: SetCurrentOpenSendOpen, Stop", "State: OpeningClosing, Evt: StartClose, Act: SetCurrentCloseSendClose, Stop", "State: OpeningClosing, Evt: CheckMore, Post: MoreOpens, Evt: StartOpen", "State: OpeningClosing, Evt: CheckMore, Post: MoreCloses, Evt: StartClose",
			"State: OpeningClosing, Evt: CheckMore, Post: DisconnectQueued, State: DataFlow, Evt: Disconnect", "State: OpeningClosing, Evt: CheckMore, State: DataFlow, Stop", "State: OpeningClosing, Evt: QueueDisconnected, Evt: CheckMore"
		};
	}
}
