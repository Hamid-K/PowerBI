using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB4 RID: 2740
	internal class AutomatonQueueManagerStateRecoveryFinalizing : StateAsCodeDriver
	{
		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x00161DA2 File Offset: 0x0015FFA2
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00161DB5 File Offset: 0x0015FFB5
		internal AutomatonQueueManagerStateRecoveryFinalizing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x00161DD4 File Offset: 0x0015FFD4
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
								goto IL_0149;
							}
							if (this.PreConditionXaCommitReply())
							{
								this.ActionExtractCommitReturnCode();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.XaCommitRc;
								num = 0;
							}
							else if (this.PreConditionXaRollbackReply())
							{
								this.ActionExtractRollbackReturnCode();
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.XaRollbackRc;
								num = 1;
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
								num = 2;
							}
						}
						else
						{
							this.ActionXaFinalizeDisconnectedEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
							num = 11;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaCommitRc)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaRollbackRc)
						{
							goto IL_0149;
						}
						if (this.PreConditionHeuristic())
						{
							this.ActionSetRollbackEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryHeuristic;
							num = 8;
						}
						else if (this.PreConditionOkRollbackRmError())
						{
							this.ActionSetRollbackEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
							num = 9;
						}
						else
						{
							this.ActionSetRollbackEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.RmError;
							num = 10;
						}
					}
					else if (this.PreConditionRollback())
					{
						this.ActionSetCommitEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
						num = 3;
					}
					else if (this.PreConditionRetry())
					{
						this.ActionSetCommitEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.RecoveryPrepared;
						num = 4;
					}
					else if (this.PreConditionHeuristic())
					{
						this.ActionSetCommitEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.RecoveryHeuristic;
						num = 5;
					}
					else if (this.PreConditionOkRmError())
					{
						this.ActionSetCommitEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
						num = 6;
					}
					else
					{
						this.ActionSetCommitEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.RmError;
						num = 7;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryFinalizing.traceLines[num]);
						continue;
					}
					continue;
					IL_0149:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryFinalizing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x00161FFC File Offset: 0x001601FC
		public bool PreConditionXaCommitReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaCommitReply;
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x00162024 File Offset: 0x00160224
		public bool PreConditionXaRollbackReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaRollbackReply;
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x0016204C File Offset: 0x0016024C
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x001620BA File Offset: 0x001602BA
		public bool PreConditionRollback()
		{
			return this.context.XaReturnCode >= XaReturnCode.Rollback && this.context.XaReturnCode <= XaReturnCode.RollbackTransient;
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x001620DD File Offset: 0x001602DD
		public bool PreConditionRetry()
		{
			return this.context.XaReturnCode == XaReturnCode.Retry;
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x001620ED File Offset: 0x001602ED
		public bool PreConditionHeuristic()
		{
			return this.context.XaReturnCode >= XaReturnCode.HeuristicMix && this.context.XaReturnCode <= XaReturnCode.HeuristicHazard;
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x0016210E File Offset: 0x0016030E
		public bool PreConditionOkRmError()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok || this.context.XaReturnCode == XaReturnCode.ResourceManagerError;
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x00162130 File Offset: 0x00160330
		public bool PreConditionOkRollbackRmError()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok || this.context.XaReturnCode == XaReturnCode.ResourceManagerError || (this.context.XaReturnCode >= XaReturnCode.Rollback && this.context.XaReturnCode <= XaReturnCode.RollbackTransient);
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0016217C File Offset: 0x0016037C
		public void ActionExtractCommitReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaCommitReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x001621D0 File Offset: 0x001603D0
		public void ActionExtractRollbackReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaRollbackReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x00162222 File Offset: 0x00160422
		public void ActionXaFinalizeClosingEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x00162242 File Offset: 0x00160442
		public void ActionSetCommitEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00162242 File Offset: 0x00160442
		public void ActionSetRollbackEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00162222 File Offset: 0x00160422
		public void ActionXaFinalizeDisconnectedEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043F8 RID: 17400
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryFinalizing;

		// Token: 0x040043F9 RID: 17401
		private AutomatonQueueManager automaton;

		// Token: 0x040043FA RID: 17402
		private AutomatonQueueManagerContext context;

		// Token: 0x040043FB RID: 17403
		private static string[] traceLines = new string[]
		{
			"State: RecoveryFinalizing, Evt: ServerData, Pre: XaCommitReply, Act: ExtractCommitReturnCode, Evt: XaCommitRc", "State: RecoveryFinalizing, Evt: ServerData, Pre: XaRollbackReply, Act: ExtractRollbackReturnCode, Evt: XaRollbackRc", "State: RecoveryFinalizing, Evt: ServerData, Pre: ServerClosing, Act: XaFinalizeClosingEvent, State: ServerClosing, Evt: InFailedState", "State: RecoveryFinalizing, Evt: XaCommitRc, Pre: Rollback, Act: SetCommitEvent, State: RecoveryNoTxn, Stop", "State: RecoveryFinalizing, Evt: XaCommitRc, Pre: Retry, Act: SetCommitEvent, State: RecoveryPrepared, Stop", "State: RecoveryFinalizing, Evt: XaCommitRc, Pre: Heuristic, Act: SetCommitEvent, State: RecoveryHeuristic, Stop", "State: RecoveryFinalizing, Evt: XaCommitRc, Pre: OkRmError, Act: SetCommitEvent, State: RecoveryNoTxn, Stop", "State: RecoveryFinalizing, Evt: XaCommitRc, Act: SetCommitEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryFinalizing, Evt: XaRollbackRc, Pre: Heuristic, Act: SetRollbackEvent, State: RecoveryHeuristic, Stop", "State: RecoveryFinalizing, Evt: XaRollbackRc, Pre: OkRollbackRmError, Act: SetRollbackEvent, State: RecoveryNoTxn, Stop",
			"State: RecoveryFinalizing, Evt: XaRollbackRc, Act: SetRollbackEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryFinalizing, Evt: TcpDisconnected, Act: XaFinalizeDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed"
		};
	}
}
