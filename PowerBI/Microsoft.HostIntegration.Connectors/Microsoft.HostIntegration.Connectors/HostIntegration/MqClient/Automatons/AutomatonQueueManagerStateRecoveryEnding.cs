using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AB3 RID: 2739
	internal class AutomatonQueueManagerStateRecoveryEnding : StateAsCodeDriver
	{
		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x00161A61 File Offset: 0x0015FC61
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x00161A74 File Offset: 0x0015FC74
		internal AutomatonQueueManagerStateRecoveryEnding(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00161A94 File Offset: 0x0015FC94
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
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryIdle;
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
						else if (this.PreConditionXaEndReply())
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
						num = 5;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateRecoveryEnding.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: RecoveryEnding, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00161C14 File Offset: 0x0015FE14
		public bool PreConditionXaEndReply()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.XaEndReply;
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00161C3C File Offset: 0x0015FE3C
		public bool PreConditionServerClosing()
		{
			SegmentHeaderInformation segmentHeaderInformation = (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation;
			return segmentHeaderInformation.SegmentType == SegmentType.SocketAction && SegmentHelpers.ExtractSocketAction((this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian).SocketActionType == SocketActionType.Quiescing;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x00161CAA File Offset: 0x0015FEAA
		public bool PreConditionOk()
		{
			return this.context.XaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00161CBA File Offset: 0x0015FEBA
		public bool PreConditionRollback()
		{
			return this.context.XaReturnCode >= XaReturnCode.Rollback && this.context.XaReturnCode <= XaReturnCode.RollbackTransient;
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00161CE0 File Offset: 0x0015FEE0
		public void ActionExtractReturnCode()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			this.context.XaReturnCode = SegmentHelpers.ExtractXaEndReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00161D32 File Offset: 0x0015FF32
		public void ActionXaEndClosingEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x00161D52 File Offset: 0x0015FF52
		public void ActionSetEvent()
		{
			this.context.CommandEvent.Set();
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x00161D32 File Offset: 0x0015FF32
		public void ActionXaEndDisconnectedEvent()
		{
			this.context.XaReturnCode = XaReturnCode.ResourceManagerUnavailable;
			this.context.CommandEvent.Set();
		}

		// Token: 0x040043F4 RID: 17396
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.RecoveryEnding;

		// Token: 0x040043F5 RID: 17397
		private AutomatonQueueManager automaton;

		// Token: 0x040043F6 RID: 17398
		private AutomatonQueueManagerContext context;

		// Token: 0x040043F7 RID: 17399
		private static string[] traceLines = new string[] { "State: RecoveryEnding, Evt: ServerData, Pre: XaEndReply, Act: ExtractReturnCode, Evt: XaReturnCode", "State: RecoveryEnding, Evt: ServerData, Pre: ServerClosing, Act: XaEndClosingEvent, State: ServerClosing, Evt: InFailedState", "State: RecoveryEnding, Evt: XaReturnCode, Pre: Ok, Act: SetEvent, State: RecoveryIdle, Stop", "State: RecoveryEnding, Evt: XaReturnCode, Pre: Rollback, Act: SetEvent, State: RecoveryRollbackOnly, Stop", "State: RecoveryEnding, Evt: XaReturnCode, Act: SetEvent, State: DataFlowFailed, Evt: RmError", "State: RecoveryEnding, Evt: TcpDisconnected, Act: XaEndDisconnectedEvent, State: DataFlowFailed, Evt: TcpFailed" };
	}
}
