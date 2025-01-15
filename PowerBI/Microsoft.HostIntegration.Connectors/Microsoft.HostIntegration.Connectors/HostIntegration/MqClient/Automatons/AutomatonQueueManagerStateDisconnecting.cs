using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A9E RID: 2718
	internal class AutomatonQueueManagerStateDisconnecting : StateAsCodeDriver
	{
		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x001593D1 File Offset: 0x001575D1
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001593E4 File Offset: 0x001575E4
		internal AutomatonQueueManagerStateDisconnecting(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x00159404 File Offset: 0x00157604
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
								goto IL_006B;
							}
							if (!this.PreConditionMqDiscReply())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendSocketAction2();
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.SaSent;
							num = 0;
						}
						else
						{
							this.ActionDetachTcp();
							automatonQueueManagerState = AutomatonQueueManagerState.DetachingTcp;
							num = 2;
						}
					}
					else if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.QueueDisconnected)
					{
						if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.SaSent)
						{
							goto IL_006B;
						}
						this.ActionDetachTcp();
						automatonQueueManagerState = AutomatonQueueManagerState.DetachingTcp;
						num = 1;
					}
					else
					{
						num = 3;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateDisconnecting.traceLines[num]);
						continue;
					}
					continue;
					IL_006B:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Disconnecting, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x00159544 File Offset: 0x00157744
		public bool PreConditionMqDiscReply()
		{
			DynamicDataBuffer dynamicDataBuffer = (this.context.DataMessageFromTcp.Contents as PassThroughData).Buffers[0];
			SegmentHeaderInformation segmentHeaderInformation = SegmentHelpers.ExtractSegmentHeader(dynamicDataBuffer);
			ApiHeaderInformation apiHeaderInformation = SegmentHelpers.ExtractApiHeader(dynamicDataBuffer, segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DisconnectReturnCode = apiHeaderInformation.ReasonCode;
			this.context.DataMessageFromTcp = null;
			dynamicDataBuffer.ReturnToOwner();
			return apiHeaderInformation.CompletionCode == CompletionCode.Ok;
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001595B8 File Offset: 0x001577B8
		public void ActionSendSocketAction2()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateSocketAction(buffer, SocketActionType.QmDisconnected);
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x001595EC File Offset: 0x001577EC
		public void ActionDetachTcp()
		{
			DeterminantMessage determinantMessage = new DeterminantMessage();
			determinantMessage.QueueManagerDeterminant = this.context.DeterminantForTcp;
			this.automaton.SendToTcp(101, determinantMessage);
		}

		// Token: 0x040043A0 RID: 17312
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.Disconnecting;

		// Token: 0x040043A1 RID: 17313
		private AutomatonQueueManager automaton;

		// Token: 0x040043A2 RID: 17314
		private AutomatonQueueManagerContext context;

		// Token: 0x040043A3 RID: 17315
		private static string[] traceLines = new string[] { "State: Disconnecting, Evt: ServerData, Pre: MqDiscReply, Act: SendSocketAction2, Evt: SaSent", "State: Disconnecting, Evt: SaSent, Act: DetachTcp, State: DetachingTcp, Stop", "State: Disconnecting, Evt: TcpDisconnected, Act: DetachTcp, State: DetachingTcp, Stop", "State: Disconnecting, Evt: QueueDisconnected, Stop" };
	}
}
