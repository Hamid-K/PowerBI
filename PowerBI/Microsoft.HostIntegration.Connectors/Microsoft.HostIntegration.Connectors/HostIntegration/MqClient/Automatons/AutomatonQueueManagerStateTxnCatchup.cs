using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA8 RID: 2728
	internal class AutomatonQueueManagerStateTxnCatchup : StateAsCodeDriver
	{
		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x0015DFD9 File Offset: 0x0015C1D9
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x0015DFEC File Offset: 0x0015C1EC
		internal AutomatonQueueManagerStateTxnCatchup(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x0015E00C File Offset: 0x0015C20C
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
								goto IL_01E2;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 6;
									goto IL_01E2;
								}
								this.ActionDetached();
								num = 7;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 8;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 19;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.Enlist:
								this.ActionEnqueueEnlist();
								num = 9;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.MessageMore:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
									num = 3;
									goto IL_01E2;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.MessageClose:
								this.ActionSendMessageToQ();
								if (this.PostConditionQueueFound())
								{
									num = 4;
									goto IL_01E2;
								}
								throw new InvalidOperationException("No Postcondition returned true");
							case AutomatonQueueManagerEvent.StartOpen:
								this.ActionSetCurrentOpenSendOpen();
								num = 11;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.StartClose:
								this.ActionSetCurrentCloseSendClose();
								num = 12;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.StartDisconnect:
								this.ActionSetDiscSendXaClose();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnClosing;
								num = 13;
								goto IL_01E2;
							case AutomatonQueueManagerEvent.CheckMore:
								if (this.PostConditionMoreCloses())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartClose;
									num = 14;
									goto IL_01E2;
								}
								if (this.PostConditionMoreOpens())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartOpen;
									num = 15;
									goto IL_01E2;
								}
								if (this.PostConditionDisconnectQueued())
								{
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.StartDisconnect;
									num = 16;
									goto IL_01E2;
								}
								if (this.PostConditionEnlistQueued())
								{
									automatonQueueManagerState = AutomatonQueueManagerState.TxnOpen;
									automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.Enlist;
									num = 17;
									goto IL_01E2;
								}
								automatonQueueManagerState = AutomatonQueueManagerState.TxnOpen;
								num = 18;
								goto IL_01E2;
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
						num = 10;
					}
					IL_01E2:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnCatchup.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnCatchup, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0015E2B4 File Offset: 0x0015C4B4
		public bool PreConditionOpen()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqOpenReply;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0015E2DC File Offset: 0x0015C4DC
		public bool PreConditionClose()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.MqCloseReply;
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x0015E304 File Offset: 0x0015C504
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x0015E37C File Offset: 0x0015C57C
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0015E3B8 File Offset: 0x0015C5B8
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

		// Token: 0x060055C6 RID: 21958 RVA: 0x0015E4FF File Offset: 0x0015C6FF
		public void ActionSendMessageToQ()
		{
			this.automaton.SendToQueue(this.context.MessageToQueue);
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0015E517 File Offset: 0x0015C717
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0015E54A File Offset: 0x0015C74A
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0015E580 File Offset: 0x0015C780
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

		// Token: 0x060055CA RID: 21962 RVA: 0x0015E5E7 File Offset: 0x0015C7E7
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x0015E5F5 File Offset: 0x0015C7F5
		public void ActionEnqueueEnlist()
		{
			this.context.EnlistQueued = true;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0015E604 File Offset: 0x0015C804
		public void ActionFailAllTcp()
		{
			foreach (PassThroughData passThroughData in this.context.QueuedCloses)
			{
				passThroughData.ReturnBuffers();
				DeterminantMessage determinantMessage = new DeterminantMessage();
				determinantMessage.QueueDeterminant = passThroughData.QueueDeterminant;
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
			if (this.context.EnlistQueued)
			{
				this.context.EnlistQueued = false;
				this.context.EnlistReturnCode = ReturnCode.EnlistTcpFailed;
				this.context.EnlistEvent.Set();
			}
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0015E72C File Offset: 0x0015C92C
		public void ActionSetCurrentOpenSendOpen()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Opening Queue");
			}
			this.context.CurrentOpen = this.context.QueuedOpens.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentOpen);
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0015E798 File Offset: 0x0015C998
		public void ActionSetCurrentCloseSendClose()
		{
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Closing Queue");
			}
			this.context.CurrentClose = this.context.QueuedCloses.Dequeue();
			this.automaton.SendClientDataToTcp(this.context.CurrentClose);
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0015E804 File Offset: 0x0015CA04
		public void ActionSetDiscSendXaClose()
		{
			this.context.DisconnectRequested = false;
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaClose(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Closing the XA transaction", Array.Empty<object>()));
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0015E886 File Offset: 0x0015CA86
		public bool PostConditionQueueFound()
		{
			return this.context.ConnectionFoundToQueue;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0015E893 File Offset: 0x0015CA93
		public bool PostConditionMoreOpens()
		{
			return this.context.QueuedOpens.Count != 0;
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x0015E8A8 File Offset: 0x0015CAA8
		public bool PostConditionMoreCloses()
		{
			return this.context.QueuedCloses.Count != 0;
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x0015E8BD File Offset: 0x0015CABD
		public bool PostConditionDisconnectQueued()
		{
			return this.context.DisconnectRequested;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x0015E8CA File Offset: 0x0015CACA
		public bool PostConditionEnlistQueued()
		{
			return this.context.EnlistQueued;
		}

		// Token: 0x040043C8 RID: 17352
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnCatchup;

		// Token: 0x040043C9 RID: 17353
		private AutomatonQueueManager automaton;

		// Token: 0x040043CA RID: 17354
		private AutomatonQueueManagerContext context;

		// Token: 0x040043CB RID: 17355
		private static string[] traceLines = new string[]
		{
			"State: TxnCatchup, Evt: ServerData, Pre: Open, Act: MakeClientMessage, Evt: MessageMore", "State: TxnCatchup, Evt: ServerData, Pre: Close, Act: MakeClientMessage, Evt: MessageClose", "State: TxnCatchup, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: TxnCatchup, Evt: MessageMore, Act: SendMessageToQ, Post: QueueFound, Evt: CheckMore", "State: TxnCatchup, Evt: MessageClose, Act: SendMessageToQ, Post: QueueFound, Stop", "State: TxnCatchup, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnCatchup, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnCatchup, Evt: QDetach, Act: Detached, Stop", "State: TxnCatchup, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: TxnCatchup, Evt: Enlist, Act: EnqueueEnlist, Stop",
			"State: TxnCatchup, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed", "State: TxnCatchup, Evt: StartOpen, Act: SetCurrentOpenSendOpen, Stop", "State: TxnCatchup, Evt: StartClose, Act: SetCurrentCloseSendClose, Stop", "State: TxnCatchup, Evt: StartDisconnect, Act: SetDiscSendXaClose, State: TxnClosing, Stop", "State: TxnCatchup, Evt: CheckMore, Post: MoreCloses, Evt: StartClose", "State: TxnCatchup, Evt: CheckMore, Post: MoreOpens, Evt: StartOpen", "State: TxnCatchup, Evt: CheckMore, Post: DisconnectQueued, Evt: StartDisconnect", "State: TxnCatchup, Evt: CheckMore, Post: EnlistQueued, State: TxnOpen, Evt: Enlist", "State: TxnCatchup, Evt: CheckMore, State: TxnOpen, Stop", "State: TxnCatchup, Evt: QueueDisconnected, Evt: CheckMore"
		};
	}
}
