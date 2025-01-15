using System;
using System.Globalization;
using System.Net.Sockets;
using System.Security;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD1 RID: 2769
	internal class AutomatonTcpStateDataTransfer : StateAsCodeDriver
	{
		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x0600580C RID: 22540 RVA: 0x00169C67 File Offset: 0x00167E67
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x00169C7A File Offset: 0x00167E7A
		internal AutomatonTcpStateDataTransfer(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x00169C98 File Offset: 0x00167E98
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
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateDataTransfer.traceLines[num]);
						continue;
					}
					continue;
					IL_0200:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DataTransfer, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x00169F78 File Offset: 0x00168178
		public bool PreConditionReceiveSucceeded8()
		{
			this.DoReadCallback();
			return this.context.NumberOfBytesRead == 8 && this.context.TcpReceiveException == null;
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x00169FA0 File Offset: 0x001681A0
		public bool PreConditionZeroBytes()
		{
			return this.context.NumberOfBytesRead == 0 && this.context.TcpReceiveException == null;
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x00169FBF File Offset: 0x001681BF
		public bool PreConditionNeedsSendHb()
		{
			return DateTime.Now - this.context.LastSentTime > this.context.HeartbeatTimespan;
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x00169FE6 File Offset: 0x001681E6
		public bool PreConditionOneMinuteOk()
		{
			return this.context.In60Seconds && DateTime.Now - this.context.LastReceivedTime <= this.context.SixtySeconds;
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x0016A01C File Offset: 0x0016821C
		public bool PreConditionOneMinute()
		{
			return this.context.In60Seconds;
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x0016A029 File Offset: 0x00168229
		public bool PreConditionNeedsReceiveHb()
		{
			return DateTime.Now - this.context.LastReceivedTime > this.context.HeartbeatTimespan;
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x0016A050 File Offset: 0x00168250
		public void ActionSetUpReceive8()
		{
			try
			{
				this.context.TcpClient.Client.BeginReceive(this.context.EightBytes, 0, 8, SocketFlags.None, new AsyncCallback(AutomatonTcpStateDataTransfer.ReadCallback), this);
			}
			catch (SocketException ex)
			{
				this.context.TcpReceiveException = ex;
			}
			catch (ObjectDisposedException ex2)
			{
				this.context.TcpReceiveException = ex2;
			}
			catch (NullReferenceException)
			{
				this.context.TcpReceiveException = null;
			}
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x0016A0E8 File Offset: 0x001682E8
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
					int num3 = this.context.TcpClient.Client.Receive(buffer.Data, num2, i, SocketFlags.None);
					if (this.context.TracePoint.IsEnabled(TraceFlags.Data))
					{
						this.context.TracePoint.Trace(TraceFlags.Data, string.Format(CultureInfo.InvariantCulture, "Received {0} bytes:", num3));
						this.context.TracePoint.Trace(TraceFlags.Data, buffer.Data, num2, num3);
					}
					num2 += num3;
					i -= num3;
				}
			}
			catch (SocketException ex)
			{
				this.context.TcpReceiveException = ex;
				return;
			}
			catch (ObjectDisposedException ex2)
			{
				this.context.TcpReceiveException = ex2;
				return;
			}
			catch (SecurityException ex3)
			{
				this.context.TcpReceiveException = ex3;
				return;
			}
			buffer.UsedLength = num;
			this.context.TcpReceiveException = null;
			this.context.DataToQmAutomaton = buffer;
			this.context.LastReceivedTime = DateTime.Now;
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x0016A270 File Offset: 0x00168470
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
						this.context.TracePoint.Trace(TraceFlags.Data, dynamicDataBuffer.Data, 0, dynamicDataBuffer.UsedLength);
					}
					this.context.TcpClient.Client.Send(dynamicDataBuffer.Data, dynamicDataBuffer.UsedLength, SocketFlags.None);
				}
				passThroughData.ReturnBuffers();
				this.context.MessageFromQmAutomaton = null;
				this.context.LastSentTime = DateTime.Now;
			}
			catch (SocketException ex)
			{
				this.context.TcpSendException = ex;
			}
			catch (ObjectDisposedException ex2)
			{
				this.context.TcpSendException = ex2;
			}
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x0016A3C8 File Offset: 0x001685C8
		public void ActionSendAsyncSendDone()
		{
			AsynchronousConnectionMessage messageFromQmAutomaton = this.context.MessageFromQmAutomaton;
			this.context.MessageFromQmAutomaton = null;
			messageFromQmAutomaton.Change(405);
			this.automaton.SendToQueueManager(messageFromQmAutomaton);
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x0016A404 File Offset: 0x00168604
		public void ActionSendToQueueManager()
		{
			this.automaton.SendDataToQueueManager();
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x0016A414 File Offset: 0x00168614
		public void ActionStartTimersNoDelay()
		{
			this.automaton.StartTimers();
			this.context.TcpClient.NoDelay = this.context.ChannelParameters.NoDelay;
			this.context.TracePoint.CodePageForData = (int)this.context.ChannelParameters.Ccsid;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0016A46C File Offset: 0x0016866C
		public void ActionSendHbMarkTime()
		{
			try
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Data))
				{
					this.context.TracePoint.Trace(TraceFlags.Data, string.Format(CultureInfo.InvariantCulture, "Sending {0} bytes:", this.context.HeartbeatBuffer.UsedLength));
					this.context.TracePoint.Trace(TraceFlags.Data, this.context.HeartbeatBuffer.Data, 0, this.context.HeartbeatBuffer.UsedLength);
				}
				this.context.TcpClient.Client.Send(this.context.HeartbeatBuffer.Data, this.context.HeartbeatBuffer.UsedLength, SocketFlags.None);
			}
			catch (SocketException ex)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex);
				}
			}
			catch (ObjectDisposedException ex2)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
			}
			this.context.LastSentTime = DateTime.Now;
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0016A5AC File Offset: 0x001687AC
		public void ActionResetSendTimer()
		{
			TimeSpan timeSpan = DateTime.Now - this.context.LastSentTime;
			int num = (this.context.ChannelParameters.HeartBeatInterval + 5) * 1000 - (int)timeSpan.TotalMilliseconds;
			if (num < 1)
			{
				num = 1;
			}
			this.context.SendTimer.Change(num, -1);
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x0016A60C File Offset: 0x0016880C
		public void ActionResetReceiveTimer()
		{
			TimeSpan timeSpan = DateTime.Now - this.context.LastReceivedTime;
			int num = (this.context.ChannelParameters.HeartBeatInterval + 5) * 1000 - (int)timeSpan.TotalMilliseconds;
			if (num < 1)
			{
				num = 1;
			}
			this.context.ReceiveTimer.Change(num, -1);
			this.context.In60Seconds = false;
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x0016A678 File Offset: 0x00168878
		public void ActionReceiveTimer60()
		{
			int num = 60000;
			this.context.ReceiveTimer.Change(num, -1);
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x0016A6A0 File Offset: 0x001688A0
		public void ActionAttached()
		{
			AsynchronousConnectionMessage attach = this.context.Attach;
			this.context.Attach = null;
			attach.Change(400);
			this.automaton.SendToQueueManager(attach);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x0016A6DC File Offset: 0x001688DC
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

		// Token: 0x06005821 RID: 22561 RVA: 0x0016A79C File Offset: 0x0016899C
		public void ActionDetached()
		{
			AsynchronousConnectionMessage detach = this.context.Detach;
			this.context.Detach = null;
			detach.Change(402);
			this.automaton.SendToQueueManager(detach);
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0016A7D8 File Offset: 0x001689D8
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

		// Token: 0x06005823 RID: 22563 RVA: 0x0016A885 File Offset: 0x00168A85
		public bool PostConditionReceiveSucceeded()
		{
			return this.context.TcpReceiveException == null;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x0016A897 File Offset: 0x00168A97
		public bool PostConditionSendSucceeded()
		{
			return this.context.TcpSendException == null;
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x0016A8A9 File Offset: 0x00168AA9
		public bool PostConditionQmFound()
		{
			return this.context.ConnectionFoundToQueueManager;
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x0016A8B8 File Offset: 0x00168AB8
		private static void ReadCallback(IAsyncResult ar)
		{
			AutomatonTcpStateDataTransfer automatonTcpStateDataTransfer = (AutomatonTcpStateDataTransfer)ar.AsyncState;
			AutomatonTcp automatonTcp = automatonTcpStateDataTransfer.AutomatonDriver as AutomatonTcp;
			automatonTcp.ProcessAsynchronousRead(ar);
			if ((automatonTcp.Automaton.Context as AutomatonTcpContext).TcpClient != null)
			{
				automatonTcpStateDataTransfer.ActionSetUpReceive8();
			}
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x0016A900 File Offset: 0x00168B00
		public void DoReadCallback()
		{
			try
			{
				int i = this.context.TcpClient.Client.EndReceive(this.context.AsynchronousResult);
				if (i != 0)
				{
					while (i < 8)
					{
						i += this.context.TcpClient.Client.Receive(this.context.EightBytes, i, 8 - i, SocketFlags.None);
					}
					if (this.context.TracePoint.IsEnabled(TraceFlags.Data))
					{
						this.context.TracePoint.Trace(TraceFlags.Data, string.Format(CultureInfo.InvariantCulture, "Received {0} bytes:", i));
						this.context.TracePoint.Trace(TraceFlags.Data, this.context.EightBytes, 0, i);
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
			catch (SocketException ex)
			{
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex);
				}
				this.context.TcpReceiveException = ex;
			}
			catch (ObjectDisposedException ex2)
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

		// Token: 0x04004498 RID: 17560
		private AutomatonTcpState stateNumber = AutomatonTcpState.DataTransfer;

		// Token: 0x04004499 RID: 17561
		private AutomatonTcp automaton;

		// Token: 0x0400449A RID: 17562
		private AutomatonTcpContext context;

		// Token: 0x0400449B RID: 17563
		private static string[] traceLines = new string[]
		{
			"State: DataTransfer, Evt: StartTransfer, Act: SetUpReceive8, Post: ReceiveSucceeded, Stop", "State: DataTransfer, Evt: StartTransfer, Act: SetUpReceive8, Evt: TcpFailed", "State: DataTransfer, Evt: DataReceived, Pre: ReceiveSucceeded8, Act: ReadSegmentMarkTime, Post: ReceiveSucceeded, Evt: SendToQm", "State: DataTransfer, Evt: DataReceived, Pre: ReceiveSucceeded8, Act: ReadSegmentMarkTime, Evt: TcpFailed", "State: DataTransfer, Evt: DataReceived, Pre: ZeroBytes, Stop", "State: DataTransfer, Evt: DataReceived, Evt: TcpFailed", "State: DataTransfer, Evt: DataToSend, Act: SendDataMarkTime, Post: SendSucceeded, Stop", "State: DataTransfer, Evt: DataToSend, Act: SendDataMarkTime, Evt: TcpFailed", "State: DataTransfer, Evt: AsyncDataToSend, Act: SendDataMarkTime, Post: SendSucceeded, Evt: AsyncSendDone", "State: DataTransfer, Evt: AsyncDataToSend, Act: SendDataMarkTime, Evt: TcpFailed",
			"State: DataTransfer, Evt: SendToQm, Act: SendToQueueManager, Post: QmFound, Stop", "State: DataTransfer, Evt: AsyncSendDone, Act: SendAsyncSendDone, Stop", "State: DataTransfer, Evt: InitialDataInfo, Act: StartTimersNoDelay, Stop", "State: DataTransfer, Evt: SendTimer, Pre: NeedsSendHb, Act: SendHbMarkTime, Evt: ResetSendTime", "State: DataTransfer, Evt: SendTimer, Evt: ResetSendTime", "State: DataTransfer, Evt: ResetSendTime, Act: ResetSendTimer, Stop", "State: DataTransfer, Evt: ReceiveTimer, Pre: OneMinuteOk, Act: ResetReceiveTimer, Stop", "State: DataTransfer, Evt: ReceiveTimer, Pre: OneMinute, Evt: TcpFailed", "State: DataTransfer, Evt: ReceiveTimer, Pre: NeedsReceiveHb, Act: SendHbMarkTime, Evt: ReceiveTime60", "State: DataTransfer, Evt: ReceiveTimer, Act: ResetReceiveTimer, Stop",
			"State: DataTransfer, Evt: ReceiveTime60, Act: ReceiveTimer60, Stop", "State: DataTransfer, Evt: QmAttach, Act: Attached, Stop", "State: DataTransfer, Evt: QmDetach, Act: Detached, Stop", "State: DataTransfer, Evt: Disconnect, Act: CloseClientSetEventStop, State: UnConnected, Stop", "State: DataTransfer, Evt: TcpFailed, Act: CloseClientStop, State: FailedData, Evt: TcpFailed"
		};
	}
}
