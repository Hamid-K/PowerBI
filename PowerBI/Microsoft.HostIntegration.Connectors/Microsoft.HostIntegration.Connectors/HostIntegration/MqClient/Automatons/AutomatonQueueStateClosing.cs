using System;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABE RID: 2750
	internal class AutomatonQueueStateClosing : StateAsCodeDriver
	{
		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06005708 RID: 22280 RVA: 0x00164907 File Offset: 0x00162B07
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0016491A File Offset: 0x00162B1A
		internal AutomatonQueueStateClosing(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0016493C File Offset: 0x00162B3C
		public override int Process(ref int eventToProcess)
		{
			AutomatonQueueEvent automatonQueueEvent = (AutomatonQueueEvent)eventToProcess;
			AutomatonQueueState automatonQueueState = this.stateNumber;
			try
			{
				while (automatonQueueState == this.stateNumber && automatonQueueEvent != AutomatonQueueEvent.Stop)
				{
					AutomatonQueueEvent automatonQueueEvent2 = AutomatonQueueEvent.Stop;
					int num;
					if (automatonQueueEvent <= AutomatonQueueEvent.DataToSend)
					{
						if (automatonQueueEvent != AutomatonQueueEvent.Timer)
						{
							if (automatonQueueEvent != AutomatonQueueEvent.DataToSend)
							{
								goto IL_0050;
							}
							this.ActionSetReturnCodeEventS();
							num = 1;
						}
						else
						{
							num = 3;
						}
					}
					else if (automatonQueueEvent != AutomatonQueueEvent.Close)
					{
						if (automatonQueueEvent != AutomatonQueueEvent.Receive)
						{
							goto IL_0050;
						}
						this.ActionSetReturnCodeEventR();
						num = 2;
					}
					else
					{
						this.ActionSetWaitCloseEvent();
						automatonQueueState = AutomatonQueueState.Closed;
						num = 0;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateClosing.traceLines[num]);
						continue;
					}
					continue;
					IL_0050:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Closing, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x00164A60 File Offset: 0x00162C60
		public void ActionSetWaitCloseEvent()
		{
			this.context.ClosedEvent.Set();
			if (this.context.ReceiveTimer != null)
			{
				this.context.ReceiveTimer.Change(-1, -1);
				this.context.ReceiveTimer.Dispose();
				this.context.ReceiveTimer = null;
			}
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x00164ABA File Offset: 0x00162CBA
		public void ActionSetReturnCodeEventS()
		{
			this.context.SentEvent.Set();
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x00164ACD File Offset: 0x00162CCD
		public void ActionSetReturnCodeEventR()
		{
			this.context.ReceivedEvent.Set();
		}

		// Token: 0x04004420 RID: 17440
		private AutomatonQueueState stateNumber = AutomatonQueueState.Closing;

		// Token: 0x04004421 RID: 17441
		private AutomatonQueue automaton;

		// Token: 0x04004422 RID: 17442
		private AutomatonQueueContext context;

		// Token: 0x04004423 RID: 17443
		private static string[] traceLines = new string[] { "State: Closing, Evt: Close, Act: SetWaitCloseEvent, State: Closed, Stop", "State: Closing, Evt: DataToSend, Act: SetReturnCodeEventS, Stop", "State: Closing, Evt: Receive, Act: SetReturnCodeEventR, Stop", "State: Closing, Evt: Timer, Stop" };
	}
}
