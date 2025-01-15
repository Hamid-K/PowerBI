using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAF RID: 2735
	internal class AutomatonQueueManagerStateTxnPrepared : StateAsCodeDriver
	{
		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06005623 RID: 22051 RVA: 0x00160623 File Offset: 0x0015E823
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x00160636 File Offset: 0x0015E836
		internal AutomatonQueueManagerStateTxnPrepared(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00160658 File Offset: 0x0015E858
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
								num = 3;
								break;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 4;
								}
								else
								{
									this.ActionDetached();
									num = 5;
								}
								break;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 7;
								break;
							case AutomatonQueueManagerEvent.MqCommand:
								if (this.PreConditionXaCommit())
								{
									this.ActionSendXaCommit();
									automatonQueueManagerState = AutomatonQueueManagerState.TxnFinalizing;
									num = 1;
								}
								else
								{
									if (!this.PreConditionXaRollback())
									{
										throw new InvalidOperationException("No Precondition returned true");
									}
									this.ActionSendXaRollback();
									automatonQueueManagerState = AutomatonQueueManagerState.TxnFinalizing;
									num = 2;
								}
								break;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 9;
								break;
							case AutomatonQueueManagerEvent.Enlist:
								this.ActionEnqueueEnlist();
								num = 6;
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
						this.ActionFailAllTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 8;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnPrepared.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnPrepared, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00160824 File Offset: 0x0015EA24
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x00160892 File Offset: 0x0015EA92
		public bool PreConditionXaCommit()
		{
			return this.context.CommandType == MqCommandType.XaCommit;
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x001608A2 File Offset: 0x0015EAA2
		public bool PreConditionXaRollback()
		{
			return this.context.CommandType == MqCommandType.XaRollback;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x001608B4 File Offset: 0x0015EAB4
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x001608F0 File Offset: 0x0015EAF0
		public void ActionSendXaCommit()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaCommit(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_COMMIT");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00160958 File Offset: 0x0015EB58
		public void ActionSendXaRollback()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateXaRollback(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending XA_ROLLBACK");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x001609BF File Offset: 0x0015EBBF
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x001609F2 File Offset: 0x0015EBF2
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x00160A28 File Offset: 0x0015EC28
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

		// Token: 0x0600562F RID: 22063 RVA: 0x00160A8F File Offset: 0x0015EC8F
		public void ActionEnqueueEnlist()
		{
			this.context.EnlistQueued = true;
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x00160A9D File Offset: 0x0015EC9D
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x00160AAC File Offset: 0x0015ECAC
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

		// Token: 0x040043E4 RID: 17380
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnPrepared;

		// Token: 0x040043E5 RID: 17381
		private AutomatonQueueManager automaton;

		// Token: 0x040043E6 RID: 17382
		private AutomatonQueueManagerContext context;

		// Token: 0x040043E7 RID: 17383
		private static string[] traceLines = new string[] { "State: TxnPrepared, Evt: ServerData, Pre: ServerClosing, State: ServerClosing, Evt: InFailedState", "State: TxnPrepared, Evt: MqCommand, Pre: XaCommit, Act: SendXaCommit, State: TxnFinalizing, Stop", "State: TxnPrepared, Evt: MqCommand, Pre: XaRollback, Act: SendXaRollback, State: TxnFinalizing, Stop", "State: TxnPrepared, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnPrepared, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnPrepared, Evt: QDetach, Act: Detached, Stop", "State: TxnPrepared, Evt: Enlist, Act: EnqueueEnlist, Stop", "State: TxnPrepared, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: TxnPrepared, Evt: TcpDisconnected, Act: FailAllTcp, State: DataFlowFailed, Evt: TcpFailed", "State: TxnPrepared, Evt: QueueDisconnected, Stop" };
	}
}
