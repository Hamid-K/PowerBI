using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABC RID: 2748
	internal class AutomatonQueueManagerStateRecoveryStarting : StateAsCodeDriver
	{
		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060056F7 RID: 22263 RVA: 0x001643E1 File Offset: 0x001625E1
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x001643F4 File Offset: 0x001625F4
		internal AutomatonQueueManagerStateRecoveryStarting(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x00164414 File Offset: 0x00162614
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
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryActive;
								num = 2;
							}
							else if (this.PreConditionRollback())
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryRollbackOnly;
								num = 3;
							}
							else
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
								automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.RmError;
								num = 4;
							}
						}
						else if (this.PreConditionXaStartReply())
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
							this.ActionXaStartClosingEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 1;
						}
					}
					else
					{
						this.ActionXaStartDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 5;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryStarting.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryStarting, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x00164594 File Offset: 0x00162794
		public bool PreConditionXaStartReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaStartReply;
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x001645BC File Offset: 0x001627BC
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x0016462A File Offset: 0x0016282A
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x0016463A File Offset: 0x0016283A
		public bool PreConditionRollback()
		{
			return this.context.XaReturnCode >= XaReturnCode.Rollback && this.context.XaReturnCode <= XaReturnCode.RollbackTransient;
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00164660 File Offset: 0x00162860
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaStartReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x001646B2 File Offset: 0x001628B2
		public void ActionXaStartClosingEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x001646D2 File Offset: 0x001628D2
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x001646B2 File Offset: 0x001628B2
		public void ActionXaStartDisconnectedEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x04004418 RID: 17432
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryStarting;

		// Token: 0x04004419 RID: 17433
		private AutomatonQueueManager automaton;

		// Token: 0x0400441A RID: 17434
		private AutomatonQueueManagerContext context;

		// Token: 0x0400441B RID: 17435
		private static string[] traceLines = new string[] { "State: RecoveryStarting, Evt: ServerData, Pre: XaStartReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryStarting, Evt: ServerData, Pre: ServerClosing, Act: XaStartClosingEvent, State: ServerClosing, Evt: InFailedState", "State: RecoveryStarting, Evt: XaReturnCode, Pre: Ok, Act: SetEvent, State: RecoveryActive, Stop", "State: RecoveryStarting, Evt: XaReturnCode, Pre: Rollback, Act: SetEvent, State: RecoveryRollbackOnly, Stop", "State: RecoveryStarting, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryStarting, Evt: TcpDisconnected, Act: XaStartDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
