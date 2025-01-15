using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB5 RID: 2741
	internal class AutomatonQueueManagerStateRecoveryForgetting : StateAsCodeDriver
	{
		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x0600567D RID: 22141 RVA: 0x001622D4 File Offset: 0x001604D4
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001622E7 File Offset: 0x001604E7
		internal AutomatonQueueManagerStateRecoveryForgetting(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x00162308 File Offset: 0x00160508
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
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
								num = 2;
							}
							else if (this.PreConditionRmError())
							{
								this.ActionSetEvent();
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryHeuristic;
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
						else if (this.PreConditionXaForgetReply())
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
							this.ActionXaForgetClosingEvent();
							automatonQueueManagerState = AutomatonQueueManagerState.ServerClosing;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.InFailedState;
							num = 1;
						}
					}
					else
					{
						this.ActionXaForgetDisconnectedEvent();
						automatonQueueManagerState = AutomatonQueueManagerState.DataFlowFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 5;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryForgetting.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryForgetting, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00162488 File Offset: 0x00160688
		public bool PreConditionXaForgetReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaForgetReply;
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x001624B0 File Offset: 0x001606B0
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0016251E File Offset: 0x0016071E
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x0016252E File Offset: 0x0016072E
		public bool PreConditionRmError()
		{
			return this.context.XaReturnCode == XaReturnCode.ResourceManagerError;
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x00162540 File Offset: 0x00160740
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaForgetReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x00162592 File Offset: 0x00160792
		public void ActionXaForgetClosingEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x001625B2 File Offset: 0x001607B2
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x00162592 File Offset: 0x00160792
		public void ActionXaForgetDisconnectedEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043FC RID: 17404
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryForgetting;

		// Token: 0x040043FD RID: 17405
		private AutomatonQueueManager automaton;

		// Token: 0x040043FE RID: 17406
		private AutomatonQueueManagerContext context;

		// Token: 0x040043FF RID: 17407
		private static string[] traceLines = new string[] { "State: RecoveryForgetting, Evt: ServerData, Pre: XaForgetReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryForgetting, Evt: ServerData, Pre: ServerClosing, Act: XaForgetClosingEvent, State: ServerClosing, Evt: InFailedState", "State: RecoveryForgetting, Evt: XaReturnCode, Pre: Ok, Act: SetEvent, State: RecoveryNoTxn, Stop", "State: RecoveryForgetting, Evt: XaReturnCode, Pre: RmError, Act: SetEvent, State: RecoveryHeuristic, Stop", "State: RecoveryForgetting, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryForgetting, Evt: TcpDisconnected, Act: XaForgetDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
