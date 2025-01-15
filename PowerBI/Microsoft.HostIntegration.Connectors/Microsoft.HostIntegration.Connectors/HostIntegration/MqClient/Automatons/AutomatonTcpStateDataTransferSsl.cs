using System;
using System.Globalization;
using System.IO;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD2 RID: 2770
	internal class AutomatonTcpStateDataTransferSsl : StateAsCodeDriver
	{
		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06005829 RID: 22569 RVA: 0x0016AC65 File Offset: 0x00168E65
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x0016AC78 File Offset: 0x00168E78
		internal AutomatonTcpStateDataTransferSsl(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x0016AC98 File Offset: 0x00168E98
		public override int Process(ref int eventToProcess)
		{
			AutomatonTcpEvent automatonTcpEvent = (AutomatonTcpEvent)eventToProcess;
			AutomatonTcpState automatonTcpState = this.stateNumber;
			try
			{
				while (automatonTcpState == this.stateNumber && automatonTcpEvent != AutomatonTcpEvent.Stop)
				{
					AutomatonTcpEvent automatonTcpEvent2 = AutomatonTcpEvent.Stop;
					int num;
					switch (automatonTcpEvent)
					{
					case AutomatonTcpEvent.DataReceived:
						if (this.PreConditionReceiveSucceeded8())
						{
							this.ActionReadSegmentMarkTime();
							if (this.PostConditionReceiveSucceeded())
							{
								automatonTcpEvent2 = AutomatonTcpEvent.SendToQm;
								num = 2;
							}
							else
							{
								automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
								num = 3;
							}
						}
						else if (this.PreConditionZeroBytes())
						{
							num = 4;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
							num = 5;
						}
						break;
					case AutomatonTcpEvent.Failed:
					case AutomatonTcpEvent.SslFailed:
					case AutomatonTcpEvent.StartHandshake:
						goto IL_0200;
					case AutomatonTcpEvent.StartTransfer:
						this.ActionSetUpReceive8();
						if (this.PostConditionReceiveSucceeded())
						{
							num = 0;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
							num = 1;
						}
						break;
					case AutomatonTcpEvent.DataToSend:
						this.ActionSendDataMarkTime();
						if (this.PostConditionSendSucceeded())
						{
							num = 6;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
							num = 7;
						}
						break;
					case AutomatonTcpEvent.AsyncDataToSend:
						this.ActionSendDataMarkTime();
						if (this.PostConditionSendSucceeded())
						{
							automatonTcpEvent2 = AutomatonTcpEvent.AsyncSendDone;
							num = 8;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
							num = 9;
						}
						break;
					case AutomatonTcpEvent.SendToQm:
						this.ActionSendToQueueManager();
						if (!this.PostConditionQmFound())
						{
							throw new InvalidOperationException("No Postcondition returned true");
						}
						num = 10;
						break;
					case AutomatonTcpEvent.AsyncSendDone:
						this.ActionSendAsyncSendDone();
						num = 11;
						break;
					case AutomatonTcpEvent.InitialDataInfo:
						this.ActionStartTimersNoDelay();
						num = 12;
						break;
					case AutomatonTcpEvent.SendTimer:
						if (this.PreConditionNeedsSendHb())
						{
							this.ActionSendHbMarkTime();
							automatonTcpEvent2 = AutomatonTcpEvent.ResetSendTime;
							num = 13;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.ResetSendTime;
							num = 14;
						}
						break;
					case AutomatonTcpEvent.ResetSendTime:
						this.ActionResetSendTimer();
						num = 15;
						break;
					case AutomatonTcpEvent.ReceiveTimer:
						if (this.PreConditionOneMinuteOk())
						{
							this.ActionResetReceiveTimer();
							num = 16;
						}
						else if (this.PreConditionOneMinute())
						{
							automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
							num = 17;
						}
						else if (this.PreConditionNeedsReceiveHb())
						{
							this.ActionSendHbMarkTime();
							automatonTcpEvent2 = AutomatonTcpEvent.ReceiveTime60;
							num = 18;
						}
						else
						{
							this.ActionResetReceiveTimer();
							num = 19;
						}
						break;
					case AutomatonTcpEvent.ReceiveTime60:
						this.ActionReceiveTimer60();
						num = 20;
						break;
					case AutomatonTcpEvent.QmAttach:
						this.ActionAttached();
						num = 21;
						break;
					case AutomatonTcpEvent.QmDetach:
						this.ActionDetached();
						num = 22;
						break;
					case AutomatonTcpEvent.Disconnect:
						this.ActionCloseClientSetEventStop();
						automatonTcpState = AutomatonTcpState.UnConnected;
						num = 23;
						break;
					case AutomatonTcpEvent.TcpFailed:
						this.ActionCloseClientStop();
						automatonTcpState = AutomatonTcpState.FailedData;
						automatonTcpEvent2 = AutomatonTcpEvent.TcpFailed;
						num = 24;
						break;
					default:
						goto IL_0200;
					}
					automatonTcpEvent = automatonTcpEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateDataTransferSsl.traceLines[num]);
						continue;
					}
					continue;
					IL_0200:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataTransferSsl, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x0016AF78 File Offset: 0x00169178
		public bool PreConditionReceiveSucceeded8()
		{
			this.DoReadCallback();
			return this.context.NumberOfBytesRead == 8 && this.context.TcpReceiveException == null;
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x0016AFA0 File Offset: 0x001691A0
		public bool PreConditionZeroBytes()
		{
			return this.context.NumberOfBytesRead == 8 && this.context.TcpReceiveException == null;
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x0016AFC0 File Offset: 0x001691C0
		public bool PreConditionNeedsSendHb()
		{
			return DateTime.Now - this.context.LastSentTime > this.context.HeartbeatTimespan;
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0016AFE7 File Offset: 0x001691E7
		public bool PreConditionOneMinuteOk()
		{
			return this.context.In60Seconds && DateTime.Now - this.context.LastReceivedTime <= this.context.SixtySeconds;
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x0016B01D File Offset: 0x0016921D
		public bool PreConditionOneMinute()
		{
			return this.context.In60Seconds;
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x0016B02A File Offset: 0x0016922A
		public bool PreConditionNeedsReceiveHb()
		{
			return DateTime.Now - this.context.LastReceivedTime > this.context.HeartbeatTimespan;
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x0016B054 File Offset: 0x00169254
		public void ActionSetUpReceive8()
		{
			try
			{
				this.context.SslStream.BeginRead(this.context.EightBytes, 0, 8, new AsyncCallback(AutomatonTcpStateDataTransferSsl.ReadCallback), this);
			}
			catch (IOException ex)
			{
				this.context.TcpReceiveException = ex;
			}
			catch (ObjectDisposedException)
			{
				this.context.TcpReceiveException = null;
			}
			catch (InvalidOperationException ex2)
			{
				this.context.TcpReceiveException = ex2;
			}
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x0016B0E4 File Offset: 0x001692E4
		public void ActionReadSegmentMarkTime()
		{
			DynamicDataBuffer buffer = base.GetBuffer(32768);
			int num = ConversionHelpers.ExtractIntFromBuffer(this.context.EightBytes, 4, false);
			if (num > 32768)
			{
				throw new InvalidOperationException("Received length larger than our max value");
			}
			int i = num - 8;
			for (int j = 0; j < 8; j++)
			{
				buffer.Data[j] = this.context.EightBytes[j];
			}
			try
			{
				int num2 = 8;
				while (i > 0)
				{
					int num3 = this.context.SslStream.Read(buffer.Data, num2, i);
					if (this.context.TracePoint.IsEnabled(TraceFlags.Data))
					{
						this.context.TracePoint.Trace(TraceFlags.Data, string.Format(CultureInfo.InvariantCulture, "Received {0} bytes:", i));
					}
					num2 += num3;
					i -= num3;
				}
			}
			catch (IOException ex)
			{
				this.context.TcpReceiveException = ex;
				return;
			}
			catch (InvalidOperationException ex2)
			{
				this.context.TcpReceiveException = ex2;
				return;
			}
			buffer.UsedLength = num;
			this.context.TcpReceiveException = null;
			this.context.DataToQmAutomaton = buffer;
			this.context.LastReceivedTime = DateTime.Now;
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x0016B224 File Offset: 0x00169424
		public void ActionSendDataMarkTime()
		{
			PassThroughData passThroughData = this.context.MessageFromQmAutomaton.Contents as PassThroughData;
			if (passThroughData == null)
			{
				throw new InvalidOperationException("Data from QM is not of the right type");
			}
			try
			{
				foreach (DynamicDataBuffer dynamicDataBuffer in passThroughData.Buffers)
				{
					if (this.context.TracePoint.IsEnabled(TraceFlags.Data))
					{
						this.context.TracePoint.Trace(TraceFlags.Data, string.Format(CultureInfo.InvariantCulture, "Sending {0} bytes:", dynamicDataBuffer.UsedLength));
					}
					this.context.SslStream.Write(dynamicDataBuffer.Data, 0, dynamicDataBuffer.UsedLength);
				}
				passThroughData.ReturnBuffers();
				this.context.MessageFromQmAutomaton = null;
				this.context.LastSentTime = DateTime.Now;
			}
			catch (IOException ex)
			{
				this.context.TcpSendException = ex;
			}
			catch (InvalidOperationException ex2)
			{
				this.context.TcpSendException = ex2;
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0016B350 File Offset: 0x00169550
		public void ActionSendAsyncSendDone()
		{
			AsynchronousConnectionMessage messageFromQmAutomaton = this.context.MessageFromQmAutomaton;
			this.context.MessageFromQmAutomaton = null;
			messageFromQmAutomaton.Change(405);
			this.automaton.SendToQueueManager(messageFromQmAutomaton);
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0016B38C File Offset: 0x0016958C
		public void ActionSendToQueueManager()
		{
			this.automaton.SendDataToQueueManager();
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x0016B39C File Offset: 0x0016959C
		public void ActionStartTimersNoDelay()
		{
			this.automaton.StartTimers();
			this.context.TcpClient.NoDelay = this.context.ChannelParameters.NoDelay;
			this.context.TracePoint.CodePageForData = (int)this.context.ChannelParameters.Ccsid;
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0016B3F4 File Offset: 0x001695F4
		public void ActionSendHbMarkTime()
		{
			try
			{
				this.context.SslStream.Write(this.context.HeartbeatBuffer.Data, 0, this.context.HeartbeatBuffer.UsedLength);
			}
			catch (IOException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			this.context.LastSentTime = DateTime.Now;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0016B468 File Offset: 0x00169668
		public void ActionResetSendTimer()
		{
			TimeSpan timeSpan = DateTime.Now - this.context.LastSentTime;
			int num = (this.context.ChannelParameters.HeartBeatInterval + 5) * 1000 - timeSpan.Milliseconds;
			this.context.SendTimer.Change(num, -1);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0016B4C0 File Offset: 0x001696C0
		public void ActionResetReceiveTimer()
		{
			TimeSpan timeSpan = DateTime.Now - this.context.LastReceivedTime;
			int num = (this.context.ChannelParameters.HeartBeatInterval + 5) * 1000 - timeSpan.Milliseconds;
			this.context.ReceiveTimer.Change(num, -1);
			this.context.In60Seconds = false;
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0016B524 File Offset: 0x00169724
		public void ActionReceiveTimer60()
		{
			int num = 60000;
			this.context.ReceiveTimer.Change(num, -1);
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0016B54C File Offset: 0x0016974C
		public void ActionAttached()
		{
			AsynchronousConnectionMessage attach = this.context.Attach;
			this.context.Attach = null;
			attach.Change(400);
			this.automaton.SendToQueueManager(attach);
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x0016B588 File Offset: 0x00169788
		public void ActionCloseClientSetEventStop()
		{
			this.context.TcpClient.Close();
			this.context.TcpClient = null;
			this.context.AsynchronousResult = null;
			this.context.DisconnectedEvent.Set();
			if (this.context.SendTimer != null)
			{
				this.context.SendTimer.Change(-1, -1);
				this.context.SendTimer.Dispose();
				this.context.SendTimer = null;
			}
			if (this.context.ReceiveTimer != null)
			{
				this.context.ReceiveTimer.Change(-1, -1);
				this.context.ReceiveTimer.Dispose();
				this.context.ReceiveTimer = null;
			}
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x0016B648 File Offset: 0x00169848
		public void ActionDetached()
		{
			AsynchronousConnectionMessage detach = this.context.Detach;
			this.context.Detach = null;
			detach.Change(402);
			this.automaton.SendToQueueManager(detach);
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x0016B684 File Offset: 0x00169884
		public void ActionCloseClientStop()
		{
			this.context.TcpClient.Close();
			this.context.TcpClient = null;
			this.context.AsynchronousResult = null;
			if (this.context.SendTimer != null)
			{
				this.context.SendTimer.Change(-1, -1);
				this.context.SendTimer.Dispose();
				this.context.SendTimer = null;
			}
			if (this.context.ReceiveTimer != null)
			{
				this.context.ReceiveTimer.Change(-1, -1);
				this.context.ReceiveTimer.Dispose();
				this.context.ReceiveTimer = null;
			}
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0016B731 File Offset: 0x00169931
		public bool PostConditionReceiveSucceeded()
		{
			return this.context.TcpReceiveException == null;
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0016B743 File Offset: 0x00169943
		public bool PostConditionSendSucceeded()
		{
			return this.context.TcpSendException == null;
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0016B755 File Offset: 0x00169955
		public bool PostConditionQmFound()
		{
			return this.context.ConnectionFoundToQueueManager;
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0016B764 File Offset: 0x00169964
		private static void ReadCallback(IAsyncResult ar)
		{
			AutomatonTcpStateDataTransferSsl automatonTcpStateDataTransferSsl = (AutomatonTcpStateDataTransferSsl)ar.AsyncState;
			AutomatonTcp automatonTcp = automatonTcpStateDataTransferSsl.AutomatonDriver as AutomatonTcp;
			automatonTcp.ProcessAsynchronousRead(ar);
			if ((automatonTcp.Automaton.Context as AutomatonTcpContext).TcpClient != null)
			{
				automatonTcpStateDataTransferSsl.ActionSetUpReceive8();
			}
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x0016B7AC File Offset: 0x001699AC
		public void DoReadCallback()
		{
			try
			{
				int i = this.context.SslStream.EndRead(this.context.AsynchronousResult);
				if (i != 0)
				{
					while (i < 8)
					{
						i += this.context.SslStream.Read(this.context.EightBytes, i, 8 - i);
					}
					this.context.NumberOfBytesRead = i;
				}
				else
				{
					if (this.context.TracePoint.IsEnabled(TraceFlags.Warning))
					{
						this.context.TracePoint.Trace(TraceFlags.Warning, string.Format(CultureInfo.InvariantCulture, "Received 0 bytes", Array.Empty<object>()));
					}
					if (!this.context.TcpClient.Connected)
					{
						throw new ApplicationException("Socket is disconnected");
					}
					this.context.NumberOfBytesRead = 0;
					AutomatonTcpContext automatonTcpContext = this.context;
					int zeroByteReads = automatonTcpContext.ZeroByteReads;
					automatonTcpContext.ZeroByteReads = zeroByteReads + 1;
					if (this.context.ZeroByteReads > 1)
					{
						throw new ApplicationException("Too many zero byte reads");
					}
				}
			}
			catch (IOException ex)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex);
				}
				this.context.TcpReceiveException = ex;
			}
			catch (InvalidOperationException ex2)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				this.context.TcpReceiveException = ex2;
			}
			catch (ApplicationException ex3)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex3);
				}
				this.context.TcpReceiveException = ex3;
			}
			finally
			{
				this.context.AsynchronousResult = null;
			}
		}

		// Token: 0x0400449C RID: 17564
		private AutomatonTcpState stateNumber = AutomatonTcpState.DataTransferSsl;

		// Token: 0x0400449D RID: 17565
		private AutomatonTcp automaton;

		// Token: 0x0400449E RID: 17566
		private AutomatonTcpContext context;

		// Token: 0x0400449F RID: 17567
		private static string[] traceLines = new string[]
		{
			"State: DataTransferSsl, Evt: StartTransfer, Act: SetUpReceive8, Post: ReceiveSucceeded, Stop", "State: DataTransferSsl, Evt: StartTransfer, Act: SetUpReceive8, Evt: TcpFailed", "State: DataTransferSsl, Evt: DataReceived, Pre: ReceiveSucceeded8, Act: ReadSegmentMarkTime, Post: ReceiveSucceeded, Evt: SendToQm", "State: DataTransferSsl, Evt: DataReceived, Pre: ReceiveSucceeded8, Act: ReadSegmentMarkTime, Evt: TcpFailed", "State: DataTransferSsl, Evt: DataReceived, Pre: ZeroBytes, Stop", "State: DataTransferSsl, Evt: DataReceived, Evt: TcpFailed", "State: DataTransferSsl, Evt: DataToSend, Act: SendDataMarkTime, Post: SendSucceeded, Stop", "State: DataTransferSsl, Evt: DataToSend, Act: SendDataMarkTime, Evt: TcpFailed", "State: DataTransferSsl, Evt: AsyncDataToSend, Act: SendDataMarkTime, Post: SendSucceeded, Evt: AsyncSendDone", "State: DataTransferSsl, Evt: AsyncDataToSend, Act: SendDataMarkTime, Evt: TcpFailed",
			"State: DataTransferSsl, Evt: SendToQm, Act: SendToQueueManager, Post: QmFound, Stop", "State: DataTransferSsl, Evt: AsyncSendDone, Act: SendAsyncSendDone, Stop", "State: DataTransferSsl, Evt: InitialDataInfo, Act: StartTimersNoDelay, Stop", "State: DataTransferSsl, Evt: SendTimer, Pre: NeedsSendHb, Act: SendHbMarkTime, Evt: ResetSendTime", "State: DataTransferSsl, Evt: SendTimer, Evt: ResetSendTime", "State: DataTransferSsl, Evt: ResetSendTime, Act: ResetSendTimer, Stop", "State: DataTransferSsl, Evt: ReceiveTimer, Pre: OneMinuteOk, Act: ResetReceiveTimer, Stop", "State: DataTransferSsl, Evt: ReceiveTimer, Pre: OneMinute, Evt: TcpFailed", "State: DataTransferSsl, Evt: ReceiveTimer, Pre: NeedsReceiveHb, Act: SendHbMarkTime, Evt: ReceiveTime60", "State: DataTransferSsl, Evt: ReceiveTimer, Act: ResetReceiveTimer, Stop",
			"State: DataTransferSsl, Evt: ReceiveTime60, Act: ReceiveTimer60, Stop", "State: DataTransferSsl, Evt: QmAttach, Act: Attached, Stop", "State: DataTransferSsl, Evt: QmDetach, Act: Detached, Stop", "State: DataTransferSsl, Evt: Disconnect, Act: CloseClientSetEventStop, State: UnConnected, Stop", "State: DataTransferSsl, Evt: TcpFailed, Act: CloseClientStop, State: FailedData, Evt: TcpFailed"
		};
	}
}
