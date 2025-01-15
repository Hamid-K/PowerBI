using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAA RID: 2730
	internal class AutomatonQueueManagerStateTxnEnded : StateAsCodeDriver
	{
		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x0015EC66 File Offset: 0x0015CE66
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x0015EC79 File Offset: 0x0015CE79
		internal AutomatonQueueManagerStateTxnEnded(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x0015EC98 File Offset: 0x0015CE98
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
								num = 1;
								break;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 2;
								}
								else
								{
									this.ActionDetached();
									num = 3;
								}
								break;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 4;
								break;
							case AutomatonQueueManagerEvent.MqCommand:
								if (!this.PreConditionXaPrepare())
								{
									throw new InvalidOperationException("No Precondition returned true");
								}
								this.ActionSendXaPrepare();
								automatonQueueManagerState = AutomatonQueueManagerState.TxnPreparing;
								num = 5;
								break;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 7;
								break;
							default:
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 0;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 6;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnEnded.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnEnded, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x0015EE34 File Offset: 0x0015D034
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x0015EEA4 File Offset: 0x0015D0A4
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0015EEDF File Offset: 0x0015D0DF
		public bool PreConditionXaPrepare()
		{
			return this.context.CommandType == MqCommandType.XaPrepare;
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0015EEEF File Offset: 0x0015D0EF
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x0015EF22 File Offset: 0x0015D122
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x0015EF58 File Offset: 0x0015D158
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

		// Token: 0x060055E5 RID: 21989 RVA: 0x0015EFBF File Offset: 0x0015D1BF
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x0015EFD0 File Offset: 0x0015D1D0
		public void ActionSendXaPrepare()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaPrepare(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_PREPARE");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x040043D0 RID: 17360
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnEnded;

		// Token: 0x040043D1 RID: 17361
		private AutomatonQueueManager automaton;

		// Token: 0x040043D2 RID: 17362
		private AutomatonQueueManagerContext context;

		// Token: 0x040043D3 RID: 17363
		private static string[] traceLines = new string[] { "State: TxnEnded, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: TxnEnded, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnEnded, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnEnded, Evt: QDetach, Act: Detached, Stop", "State: TxnEnded, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: TxnEnded, Evt: MqCommand, Pre: XaPrepare, Act: SendXaPrepare, State: TxnPreparing, Stop", "State: TxnEnded, Evt: TcpDisconnected, State: DataFlowFailed, Evt: TcpFailed", "State: TxnEnded, Evt: QueueDisconnected, Stop" };
	}
}
