using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABA RID: 2746
	internal class AutomatonQueueManagerStateRecoveryPreparing : StateAsCodeDriver
	{
		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x00163B8A File Offset: 0x00161D8A
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x00163B9D File Offset: 0x00161D9D
		internal AutomatonQueueManagerStateRecoveryPreparing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00163BBC File Offset: 0x00161DBC
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.XaReturnCode)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							if (this.PreConditionOk())
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryPrepared;
								num = 2;
							}
							else if (this.PreConditionRmError())
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryIdle;
								num = 3;
							}
							else if (this.PreConditionReadOnlyRollback())
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
								num = 4;
							}
							else
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.RmError;
								num = 5;
							}
						}
						else if (this.PreConditionXaPrepareReply())
						{
							this.ActionExtractReturnCode();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.XaReturnCode;
							num = 0;
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
							num = 1;
						}
					}
					else
					{
						this.ActionXaPrepareDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 6;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryPreparing.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryPreparing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x00163D60 File Offset: 0x00161F60
		public bool PreConditionXaPrepareReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaPrepareReply;
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x00163D88 File Offset: 0x00161F88
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x00163DF6 File Offset: 0x00161FF6
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x00163E06 File Offset: 0x00162006
		public bool PreConditionRmError()
		{
			return this.context.XaReturnCode == XaReturnCode.ResourceManagerError;
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x00163E17 File Offset: 0x00162017
		public bool PreConditionReadOnlyRollback()
		{
			return this.context.XaReturnCode == XaReturnCode.ReadOnly || (this.context.XaReturnCode >= XaReturnCode.Rollback && this.context.XaReturnCode <= XaReturnCode.RollbackTransient);
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x00163E4C File Offset: 0x0016204C
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaPrepareReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x00163E9E File Offset: 0x0016209E
		public void ActionXaPrepareClosingEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x00163EBE File Offset: 0x001620BE
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x00163E9E File Offset: 0x0016209E
		public void ActionXaPrepareDisconnectedEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x04004410 RID: 17424
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryPreparing;

		// Token: 0x04004411 RID: 17425
		private AutomatonQueueManager automaton;

		// Token: 0x04004412 RID: 17426
		private AutomatonQueueManagerContext context;

		// Token: 0x04004413 RID: 17427
		private static string[] traceLines = new string[] { "State: RecoveryPreparing, Evt: ServerData, Pre: XaPrepareReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryPreparing, Evt: ServerData, Pre: ServerClosing, Act: XaPrepareClosingEvent, State: ServerClosing, Evt: InFailedState", "State: RecoveryPreparing, Evt: XaReturnCode, Pre: Ok, Act: SetEvent, State: RecoveryPrepared, Stop", "State: RecoveryPreparing, Evt: XaReturnCode, Pre: RmError, Act: SetEvent, State: RecoveryIdle, Stop", "State: RecoveryPreparing, Evt: XaReturnCode, Pre: ReadOnlyRollback, Act: SetEvent, State: RecoveryNoTxn, Stop", "State: RecoveryPreparing, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryPreparing, Evt: TcpDisconnected, Act: XaPrepareDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
