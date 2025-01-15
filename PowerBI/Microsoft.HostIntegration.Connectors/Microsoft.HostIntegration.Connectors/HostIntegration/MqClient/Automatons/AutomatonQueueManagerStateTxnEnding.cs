using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAB RID: 2731
	internal class AutomatonQueueManagerStateTxnEnding : StateAsCodeDriver
	{
		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x060055E8 RID: 21992 RVA: 0x0015F090 File Offset: 0x0015D290
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x0015F0A3 File Offset: 0x0015D2A3
		internal AutomatonQueueManagerStateTxnEnding(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x0015F0C4 File Offset: 0x0015D2C4
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
								num = 2;
								goto IL_00D7;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 3;
									goto IL_00D7;
								}
								this.ActionDetached();
								num = 4;
								goto IL_00D7;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 5;
								goto IL_00D7;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 7;
								goto IL_00D7;
							}
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionXaEndReplyOk())
						{
							this.ActionSetXaEndWaitEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.TxnEnded;
							num = 0;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionXaEndClosingEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 1;
						}
					}
					else
					{
						this.ActionXaEndDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 6;
					}
					IL_00D7:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnEnding.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnEnding, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0015F260 File Offset: 0x0015D460
		public bool PreConditionXaEndReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaEndReply)
			{
				return false;
			}
			int num = (int)SegmentHelpers.ExtractXaEndReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			return num == 0;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0015F2CC File Offset: 0x0015D4CC
		public bool PreConditionServerClosing()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			return passThroughData.SegmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], passThroughData.SegmentHeaderInformation.LengthOfSegmentHeader, passThroughData.SegmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0015F344 File Offset: 0x0015D544
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0015F37F File Offset: 0x0015D57F
		public void ActionSetXaEndWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0015F392 File Offset: 0x0015D592
		public void ActionXaEndClosingEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaEndClosing;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x0015F3C2 File Offset: 0x0015D5C2
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x0015F3F5 File Offset: 0x0015D5F5
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x0015F428 File Offset: 0x0015D628
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

		// Token: 0x060055F3 RID: 22003 RVA: 0x0015F48F File Offset: 0x0015D68F
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0015F49D File Offset: 0x0015D69D
		public void ActionXaEndDisconnectedEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaEndDisconnecting;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043D4 RID: 17364
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnEnding;

		// Token: 0x040043D5 RID: 17365
		private AutomatonQueueManager automaton;

		// Token: 0x040043D6 RID: 17366
		private AutomatonQueueManagerContext context;

		// Token: 0x040043D7 RID: 17367
		private static string[] traceLines = new string[] { "State: TxnEnding, Evt: ServerData, Pre: XaEndReplyOk, Act: SetXaEndWaitEvent, State: TxnEnded, Stop", "State: TxnEnding, Evt: ServerData, Pre: ServerClosing, Act: XaEndClosingEvent, State: ServerClosing, Evt: InFailedState", "State: TxnEnding, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnEnding, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnEnding, Evt: QDetach, Act: Detached, Stop", "State: TxnEnding, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: TxnEnding, Evt: TcpDisconnected, Act: XaEndDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed", "State: TxnEnding, Evt: QueueDisconnected, Stop" };
	}
}
