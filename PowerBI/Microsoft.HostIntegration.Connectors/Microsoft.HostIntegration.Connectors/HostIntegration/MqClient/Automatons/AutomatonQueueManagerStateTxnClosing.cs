using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AA9 RID: 2729
	internal class AutomatonQueueManagerStateTxnClosing : StateAsCodeDriver
	{
		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x0015E99C File Offset: 0x0015CB9C
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0015E9AF File Offset: 0x0015CBAF
		internal AutomatonQueueManagerStateTxnClosing(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x0015E9D0 File Offset: 0x0015CBD0
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
							if (automatonQueueManagerEvent != AutomatonQueueManagerEvent.QueueDisconnected)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							num = 2;
						}
						else
						{
							if (!this.PreConditionXaCloseReplyOk())
							{
								throw new InvalidOperationException("No Precondition returned true");
							}
							this.ActionSendMqDisc();
							automatonQueueManagerState = AutomatonQueueManagerState.Disconnecting;
							num = 0;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 1;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnClosing.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnClosing, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0015EAF4 File Offset: 0x0015CCF4
		public bool PreConditionXaCloseReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaCloseReply)
			{
				return false;
			}
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaCloseReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			this.context.DataMessageFromTcp = null;
			passThroughData.ReturnBuffers();
			if (xaReturnCode == XaReturnCode.Ok)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_CLOSE Succeeded", Array.Empty<object>()));
				}
			}
			else if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
			{
				this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "XA_CLOSE failed, XA Code: {0}", xaReturnCode.ToString()));
			}
			return xaReturnCode == XaReturnCode.Ok;
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0015EBE0 File Offset: 0x0015CDE0
		public void ActionSendMqDisc()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			AutomatonQueueManager.GenerateMqDisconnect(buffer, this.context.DeterminantForTcp);
			if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this.context.TracePoint.Trace(TraceFlags.Verbose, "Sending MQ Disconnect");
			}
			this.automaton.SendBufferAsPassthroughClientDataToTcp(buffer);
		}

		// Token: 0x040043CC RID: 17356
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnClosing;

		// Token: 0x040043CD RID: 17357
		private AutomatonQueueManager automaton;

		// Token: 0x040043CE RID: 17358
		private AutomatonQueueManagerContext context;

		// Token: 0x040043CF RID: 17359
		private static string[] traceLines = new string[] { "State: TxnClosing, Evt: ServerData, Pre: XaCloseReplyOk, Act: SendMqDisc, State: Disconnecting, Stop", "State: TxnClosing, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed", "State: TxnClosing, Evt: QueueDisconnected, Stop" };
	}
}
