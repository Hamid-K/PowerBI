using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AAE RID: 2734
	internal class AutomatonQueueManagerStateTxnOpening : StateAsCodeDriver
	{
		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x0016038F File Offset: 0x0015E58F
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x001603A2 File Offset: 0x0015E5A2
		internal AutomatonQueueManagerStateTxnOpening(AutomatonQueueManager driver, AutomatonQueueManagerContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x001603C4 File Offset: 0x0015E5C4
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
							throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
						}
						if (this.PreConditionXaOpenReplyOk())
						{
							this.ActionSetWaitConnectEvent();
							if (this.PostConditionInMsdtc())
							{
								automatonQueueManagerState = AutomatonQueueManagerState.RecoveryNoTxn;
								num = 0;
							}
							else
							{
								automatonQueueManagerState = AutomatonQueueManagerState.TxnOpen;
								num = 1;
							}
						}
						else
						{
							automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
							automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.XaOpenError;
							num = 2;
						}
					}
					else
					{
						automatonQueueManagerState = AutomatonQueueManagerState.HandshakeFailed;
						automatonQueueManagerEvent2 = AutomatonQueueManagerEvent.TcpFailed;
						num = 3;
					}
					automatonQueueManagerEvent = automatonQueueManagerEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueManagerStateTxnOpening.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: TxnOpening, Event: " + automatonQueueManagerEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueManagerEvent;
			return (int)automatonQueueManagerState;
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x001604E8 File Offset: 0x0015E6E8
		public bool PreConditionXaOpenReplyOk()
		{
			PassThroughData passThroughData = this.context.DataMessageFromTcp.Contents as PassThroughData;
			SegmentHeaderInformation segmentHeaderInformation = passThroughData.SegmentHeaderInformation;
			if (segmentHeaderInformation.SegmentType != SegmentType.XaOpenReply)
			{
				return false;
			}
			XaReturnCode xaReturnCode = SegmentHelpers.ExtractXaOpenReturnCode(passThroughData.Buffers[0], segmentHeaderInformation.LengthOfSegmentHeader, segmentHeaderInformation.LittleEndian);
			if (xaReturnCode == XaReturnCode.Ok)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "XA_OPEN Succeeded", Array.Empty<object>()));
				}
				this.context.DataMessageFromTcp = null;
				passThroughData.ReturnBuffers();
				return true;
			}
			if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
			{
				this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "XA_OPEN failed, XA Code: {0}", xaReturnCode.ToString()));
			}
			return false;
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x001605D1 File Offset: 0x0015E7D1
		public void ActionSetWaitConnectEvent()
		{
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001605E4 File Offset: 0x0015E7E4
		public bool PostConditionInMsdtc()
		{
			return this.context.ConnectionParameters.InMsdtc;
		}

		// Token: 0x040043E0 RID: 17376
		private AutomatonQueueManagerState stateNumber = AutomatonQueueManagerState.TxnOpening;

		// Token: 0x040043E1 RID: 17377
		private AutomatonQueueManager automaton;

		// Token: 0x040043E2 RID: 17378
		private AutomatonQueueManagerContext context;

		// Token: 0x040043E3 RID: 17379
		private static string[] traceLines = new string[] { "State: TxnOpening, Evt: ServerData, Pre: XaOpenReplyOk, Act: SetWaitConnectEvent, Post: InMsdtc, State: RecoveryNoTxn, Stop", "State: TxnOpening, Evt: ServerData, Pre: XaOpenReplyOk, Act: SetWaitConnectEvent, State: TxnOpen, Stop", "State: TxnOpening, Evt: ServerData, State: HandshakeFailed, Evt: XaOpenError", "State: TxnOpening, Evt: TcpDisconnected, State: HandshakeFailed, Evt: TcpFailed" };
	}
}
