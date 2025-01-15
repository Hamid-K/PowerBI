using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A98 RID: 2712
	internal class AutomatonQueueManagerStateAuthorization : StateAsCodeDriver
	{
		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x00157E75 File Offset: 0x00156075
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x00157E88 File Offset: 0x00156088
		internal AutomatonQueueManagerStateAuthorization(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x00157EA8 File Offset: 0x001560A8
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.Authorize)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							this.ActionSendConAuth();
							num = 0;
						}
						else
						{
							if (!this.PreConditionIsStatusData())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							automatonQueueManagerState = AutomatonQueueManagerState.ConnectingQm;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SendConnect;
							num = 1;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 2;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateAuthorization.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Authorization, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x00157FCC File Offset: 0x001561CC
		public bool PreConditionIsStatusData()
		{
			return (this.context.DataMessageFromTcp.Contents as PassThroughData).SegmentHeaderInformation.SegmentType == SegmentType.StatusData;
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x00157FF0 File Offset: 0x001561F0
		public void ActionSendConAuth()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateConAuth(buffer);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending CONAUTH_INFO");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x04004388 RID: 17288
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.Authorization;

		// Token: 0x04004389 RID: 17289
		private AutomatonQueueManager automaton;

		// Token: 0x0400438A RID: 17290
		private AutomatonQueueManagerContext context;

		// Token: 0x0400438B RID: 17291
		private static string[] traceLines = new string[] { "State: Authorization, Evt: Authorize, Act: SendConAuth, Stop", "State: Authorization, Evt: ServerData, Pre: IsStatusData, State: ConnectingQm, Evt: SendConnect", "State: Authorization, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed" };
	}
}
