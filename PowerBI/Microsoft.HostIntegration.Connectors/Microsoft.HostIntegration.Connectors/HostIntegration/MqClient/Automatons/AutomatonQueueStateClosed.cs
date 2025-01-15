using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ABD RID: 2749
	internal class AutomatonQueueStateClosed : StateAsCodeDriver
	{
		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x00164722 File Offset: 0x00162922
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x00164735 File Offset: 0x00162935
		internal AutomatonQueueStateClosed(AutomatonQueue driver, AutomatonQueueContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0016474C File Offset: 0x0016294C
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
					if (automatonQueueEvent != AutomatonQueueEvent.Open)
					{
						if (automatonQueueEvent != AutomatonQueueEvent.Timer)
						{
							if (automatonQueueEvent != AutomatonQueueEvent.QmFailed)
							{
								throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
							}
							num = 2;
						}
						else
						{
							num = 1;
						}
					}
					else
					{
						this.ActionAttachManager();
						automatonQueueState = AutomatonQueueState.Opening;
						num = 0;
					}
					automatonQueueEvent = automatonQueueEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonQueueStateClosed.traceLines[num]);
					}
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: Closed, Event: " + automatonQueueEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonQueueEvent;
			return (int)automatonQueueState;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x0016484C File Offset: 0x00162A4C
		public void ActionAttachManager()
		{
			this.automaton.ConnectToQueueManager();
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			this.automaton.GenerateMqOpen(buffer, this.context.ConnectionParameters.Name, this.context.OpenOptions);
			PassThroughData passThroughData = new PassThroughData();
			passThroughData.QueueDeterminant = this.context.DeterminantForQueueManager;
			passThroughData.QueueManagerDeterminant = this.context.QueueManagerConversationId;
			passThroughData.Buffers = new List<DynamicDataBuffer> { buffer };
			this.automaton.SendToQueueManager(300, passThroughData);
		}

		// Token: 0x0400441C RID: 17436
		private AutomatonQueueState stateNumber;

		// Token: 0x0400441D RID: 17437
		private AutomatonQueue automaton;

		// Token: 0x0400441E RID: 17438
		private AutomatonQueueContext context;

		// Token: 0x0400441F RID: 17439
		private static string[] traceLines = new string[] { "State: Closed, Evt: Open, Act: AttachManager, State: Opening, Stop", "State: Closed, Evt: Timer, Stop", "State: Closed, Evt: QmFailed, Stop" };
	}
}
