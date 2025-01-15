using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB0 RID: 2736
	internal class AutomatonQueueManagerStateTxnPreparing : StateAsCodeDriver
	{
		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06005633 RID: 22067 RVA: 0x00160C3E File Offset: 0x0015EE3E
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x00160C51 File Offset: 0x0015EE51
		internal AutomatonQueueManagerStateTxnPreparing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00160C70 File Offset: 0x0015EE70
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
								num = 4;
								goto IL_010E;
							case AutomatonQueueManagerEvent.QDetach:
								if (this.PreConditionDoClose())
								{
									this.ActionEnqueueClose();
									num = 5;
									goto IL_010E;
								}
								this.ActionDetached();
								num = 6;
								goto IL_010E;
							case AutomatonQueueManagerEvent.Disconnect:
								this.ActionEnqueueDisconnect();
								num = 7;
								goto IL_010E;
							case AutomatonQueueManagerEvent.QueueDisconnected:
								num = 9;
								goto IL_010E;
							}
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionXaPrepareReplyOk())
						{
							this.ActionSetXaPrepareWaitEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.TxnPrepared;
							num = 0;
						}
						else if (this.PreConditionXaPrepareReplyDone())
						{
							this.ActionXaPrepareOkEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.TxnCatchup;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.CheckMore;
							num = 1;
						}
						else if (this.PreConditionXaPrepareReply())
						{
							this.ActionXaPrepareRcEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
							num = 2;
						}
						else
						{
							if (!this.PreConditionServerClosing())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionXaPrepareClosingEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 3;
						}
					}
					else
					{
						this.ActionXaPrepareDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 8;
					}
					IL_010E:
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnPreparing.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnPreparing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00160E44 File Offset: 0x0015F044
		public bool PreConditionXaPrepareReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaPrepareReply)
			{
				return false;
			}
			if (SegmentHelpers.ExtractXaPrepareReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian) == XaReturnCode.Ok)
			{
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			return false;
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00160EB4 File Offset: 0x0015F0B4
		public bool PreConditionXaPrepareReplyDone()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaPrepareReply)
			{
				return false;
			}
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaPrepareReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			return xaReturnCode == XaReturnCode.ReadOnly || xaReturnCode - XaReturnCode.Rollback <= 7;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x00160F19 File Offset: 0x0015F119
		public bool PreConditionXaPrepareReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaPrepareReply;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00160F44 File Offset: 0x0015F144
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00160FB4 File Offset: 0x0015F1B4
		public bool PreConditionDoClose()
		{
			PassThroughData passThroughData = this.context.DataMessageFromQueue.Contents as PassThroughData;
			return passThroughData.Buffers != null && passThroughData.Buffers.Count != 0;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x00160FEF File Offset: 0x0015F1EF
		public void ActionSetXaPrepareWaitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x00161004 File Offset: 0x0015F204
		public void ActionXaPrepareOkEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaPrepareReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			this.context.CommandReturnCode = ReturnCode.Ok;
			this.context.XaReturnCode = xaReturnCode;
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x00161088 File Offset: 0x0015F288
		public void ActionXaPrepareRcEvent()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaPrepareReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			this.context.CommandReturnCode = ExceptionHelpers.XaReturnCodeToReturnCode(xaReturnCode);
			this.context.XaReturnCode = xaReturnCode;
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x0016110E File Offset: 0x0015F30E
		public void ActionXaPrepareClosingEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaPrepareClosing;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x0016113E File Offset: 0x0015F33E
		public void ActionEnqueueOpen()
		{
			this.context.QueuedOpens.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x00161171 File Offset: 0x0015F371
		public void ActionEnqueueClose()
		{
			this.context.QueuedCloses.Enqueue(this.context.DataMessageFromQueue.Contents as PassThroughData);
			this.context.DataMessageFromQueue = null;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x001611A4 File Offset: 0x0015F3A4
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

		// Token: 0x06005642 RID: 22082 RVA: 0x0016120B File Offset: 0x0015F40B
		public void ActionEnqueueDisconnect()
		{
			this.context.DisconnectRequested = true;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x00161219 File Offset: 0x0015F419
		public void ActionXaPrepareDisconnectedEvent()
		{
			this.context.CommandReturnCode = ReturnCode.XaPrepareDisconnecting;
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043E8 RID: 17384
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnPreparing;

		// Token: 0x040043E9 RID: 17385
		private AutomatonQueueManager automaton;

		// Token: 0x040043EA RID: 17386
		private AutomatonQueueManagerContext context;

		// Token: 0x040043EB RID: 17387
		private static string[] traceLines = new string[] { "State: TxnPreparing, Evt: ServerData, Pre: XaPrepareReplyOk, Act: SetXaPrepareWaitEvent, State: TxnPrepared, Stop", "State: TxnPreparing, Evt: ServerData, Pre: XaPrepareReplyDone, Act: XaPrepareOkEvent, State: TxnCatchup, Evt: CheckMore", "State: TxnPreparing, Evt: ServerData, Pre: XaPrepareReply, Act: XaPrepareRcEvent, State: DataFlowFailed, Stop", "State: TxnPreparing, Evt: ServerData, Pre: ServerClosing, Act: XaPrepareClosingEvent, State: ServerClosing, Evt: InFailedState", "State: TxnPreparing, Evt: QAttach, Act: EnqueueOpen, Stop", "State: TxnPreparing, Evt: QDetach, Pre: DoClose, Act: EnqueueClose, Stop", "State: TxnPreparing, Evt: QDetach, Act: Detached, Stop", "State: TxnPreparing, Evt: Disconnect, Act: EnqueueDisconnect, Stop", "State: TxnPreparing, Evt: TcpDisconnected, Act: XaPrepareDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed", "State: TxnPreparing, Evt: QueueDisconnected, Stop" };
	}
}
