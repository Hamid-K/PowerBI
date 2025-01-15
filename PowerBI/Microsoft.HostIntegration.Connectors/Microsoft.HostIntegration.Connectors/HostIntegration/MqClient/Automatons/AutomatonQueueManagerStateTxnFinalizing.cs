using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAC RID: 2732
	internal class AutomatonQueueManagerStateTxnFinalizing : StateAsCodeDriver
	{
		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x0015F528 File Offset: 0x0015D728
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x0015F53B File Offset: 0x0015D73B
		internal AutomatonQueueManagerStateTxnFinalizing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x0015F55C File Offset: 0x0015D75C
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
					if (automatonQueueManagerEvent <= AutomatonQueueManagerEvent.ServerData)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpDisconnected)
						{
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.ServerData)
							{
								goto IL_0146;
							}
							if (this.PreConditionXaCommitReplyOk())
							{
								this.ActionSetXaFinalizeWaitEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnCatchup;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 0;
							}
							else if (this.PreConditionXaCommitReply())
							{
								this.ActionXaCommitRcEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnCatchup;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 1;
							}
							else if (this.PreConditionXaRollbackReplyOk())
							{
								this.ActionSetXaFinalizeWaitEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnCatchup;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 2;
							}
							else if (this.PreConditionXaRollbackReply())
							{
								this.ActionXaRollbackRcEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnCatchup;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
								num = 3;
							}
							else
							{
								if (!this.PreConditionServerClosing())
								{
									throw new InvalidOperationException("No Precondition returned true");
								}
								this.ActionXaFinalizeClosingEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
								num = 4;
							}
						}
						else
						{
							this.ActionXaFinalizeDisconnectedEvent();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
							num = 10;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.TcpFailed)
					{
						switch (automatonQueueManagerEvent)
						{
						case AutomatonQueueManagerEvent.QAttach:
							this.ActionEnqueueOpen();
							num = 5;
							break;
						case AutomatonQueueManagerEvent.QDetach:
							if (this.PreConditionDoClose())
							{
								this.ActionEnqueueClose();
								num = 6;
							}
							else
							{
								this.ActionDetached();
								num = 7;
							}
							break;
						case AutomatonQueueManagerEvent.Disconnect:
							this.ActionEnqueueDisconnect();
							num = 9;
							break;
						case AutomatonQueueManagerEvent.MqCommand:
							goto IL_0146;
						case AutomatonQueueManagerEvent.QueueDisconnected:
							num = 12;
							break;
						case AutomatonQueueManagerEvent.Enlist:
							this.ActionEnqueueEnlist();
							num = 8;
							break;
						default:
							goto IL_0146;
						}
					}
					else
					{
						this.ActionFailAllTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 11;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnFinalizing.traceLines[num]);
						continue;
					}
					continue;
					IL_0146:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnFinalizing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x0015F780 File Offset: 0x0015D980
		public bool PreConditionXaCommitReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaCommitReply)
			{
				return false;
			}
			if (SegmentHelpers.ExtractXaCommitReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian) == XaReturnCode.Ok)
			{
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			return false;
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x0015F7ED File Offset: 0x0015D9ED
		public bool PreConditionXaCommitReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaCommitReply;
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x0015F818 File Offset: 0x0015DA18
		public bool PreConditionXaRollbackReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaRollbackReply)
			{
				return false;
			}
			if (SegmentHelpers.ExtractXaCommitReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian) == XaReturnCode.Ok)
			{
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			return false;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x0015F885 File Offset: 0x0015DA85
		public bool PreConditionXaRollbackReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRollbackReply;
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x0015F8B0 File Offset: 0x0015DAB0
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0015F928 File Offset: 0x0015DB28
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0015F963 File Offset: 0x0015DB63
		public void ActionSetXaFinalizeWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0015F978 File Offset: 0x0015DB78
		public void ActionXaCommitRcEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaCommitReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			this.context.CommandReturnCode = ExceptionHelpers.XaReturnCodeToReturnCode(xaReturnCode);
			this.context.XaReturnCode = xaReturnCode;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x0015FA00 File Offset: 0x0015DC00
		public void ActionXaRollbackRcEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaRollbackReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			this.context.CommandReturnCode = ExceptionHelpers.XaReturnCodeToReturnCode(xaReturnCode);
			this.context.XaReturnCode = xaReturnCode;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x0015FA86 File Offset: 0x0015DC86
		public void ActionXaFinalizeClosingEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaFinalizeClosing;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x0015FAB6 File Offset: 0x0015DCB6
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x0015FAE9 File Offset: 0x0015DCE9
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x0015FB1C File Offset: 0x0015DD1C
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

		// Token: 0x06005606 RID: 22022 RVA: 0x0015FB83 File Offset: 0x0015DD83
		public void ActionEnqueueEnlist()
		{
			this.context.EnlistQueued = true;
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x0015FB91 File Offset: 0x0015DD91
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x0015FB9F File Offset: 0x0015DD9F
		public void ActionXaFinalizeDisconnectedEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaFinalizeDisconnecting;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x0015FBD0 File Offset: 0x0015DDD0
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

		// Token: 0x040043D8 RID: 17368
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnFinalizing;

		// Token: 0x040043D9 RID: 17369
		private AutomatonQueueManager automaton;

		// Token: 0x040043DA RID: 17370
		private AutomatonQueueManagerContext context;

		// Token: 0x040043DB RID: 17371
		private static string[] traceLines = new string[]
		{
			"State: TxnFinalizing, Evt: ServerData, Pre: XaCommitReplyOk, Act: SetXaFinalizeWaitEvent, State: TxnCatchup, Evt: CheckMore", "State: TxnFinalizing, Evt: ServerData, Pre: XaCommitReply, Act: XaCommitRcEvent, State: TxnCatchup, Evt: CheckMore", "State: TxnFinalizing, Evt: ServerData, Pre: XaRollbackReplyOk, Act: SetXaFinalizeWaitEvent, State: TxnCatchup, Evt: CheckMore", "State: TxnFinalizing, Evt: ServerData, Pre: XaRollbackReply, Act: XaRollbackRcEvent, State: TxnCatchup, Evt: CheckMore", "State: TxnFinalizing, Evt: ServerData, Pre: ServerClosing, Act: XaFinalizeClosingEvent, State: ServerClosing, Evt: InFailedState", "State: TxnFinalizing, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnFinalizing, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnFinalizing, Evt: QDetach, Act: Detached, Stop", "State: TxnFinalizing, Evt: Enlist, Act: EnqueueEnlist, Stop", "State: TxnFinalizing, Evt: Disconnect, Act: EnqueueDisconnect, Stop",
			"State: TxnFinalizing, Evt: TcpDisconnected, Act: XaFinalizeDisconnectedEvent, Evt: TcpFailed", "State: TxnFinalizing, Evt: TcpFailed, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed", "State: TxnFinalizing, Evt: QueueDisconnected, Stop"
		};
	}
}
