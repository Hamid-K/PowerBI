using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200066E RID: 1646
	public class LiveTraceWriter : ILiveTraceWriter
	{
		// Token: 0x0600371E RID: 14110 RVA: 0x000B9B04 File Offset: 0x000B7D04
		public LiveTraceWriter(int liveTracingBufferSize)
		{
			this.allowedLines = liveTracingBufferSize;
			this.lineBuffer = new string[this.allowedLines + 100];
			this.bufferWriteIndex = 0;
			this.bufferSentIndex = this.lineBuffer.Length - 1;
			this.linesLost = 0;
			this.sendInProgress = false;
			this.tryConnect = true;
			ThreadPool.QueueUserWorkItem(new WaitCallback(LiveTraceWriter.ThreadProcToConnect), this);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000B9B74 File Offset: 0x000B7D74
		public void WriteLine(string line)
		{
			if (!this.connected)
			{
				return;
			}
			string[] array = this.lineBuffer;
			lock (array)
			{
				this.lineBuffer[this.bufferWriteIndex] = line;
				this.bufferWriteIndex++;
				if (this.bufferWriteIndex == this.lineBuffer.Length)
				{
					this.bufferWriteIndex = 0;
				}
				int num;
				if (this.bufferSentIndex < this.bufferWriteIndex)
				{
					num = this.bufferWriteIndex - this.bufferSentIndex - 1;
				}
				else
				{
					num = this.lineBuffer.Length - 1 - this.bufferSentIndex + this.bufferWriteIndex;
				}
				if (num > this.allowedLines)
				{
					this.linesLost++;
					this.bufferSentIndex++;
					if (this.bufferSentIndex == this.lineBuffer.Length)
					{
						this.bufferSentIndex = 0;
					}
				}
				if (!this.sendInProgress)
				{
					this.sendInProgress = true;
					this.CopyLinesToSendBuffer(num);
					ThreadPool.QueueUserWorkItem(new WaitCallback(LiveTraceWriter.ThreadProcToWrite), this);
				}
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000B9C8C File Offset: 0x000B7E8C
		private void CopyLinesToSendBuffer(int numberToSend)
		{
			this.traceLines = new string[numberToSend];
			int num = 0;
			if (this.linesLost != 0)
			{
				this.traceLines[0] = "Truncated, Lost Some Lines";
				num++;
				this.linesLost = 0;
			}
			if (this.bufferSentIndex < this.bufferWriteIndex)
			{
				Array.Copy(this.lineBuffer, this.bufferSentIndex + 1, this.traceLines, num, numberToSend);
			}
			else
			{
				if (this.bufferSentIndex != this.lineBuffer.Length - 1)
				{
					Array.Copy(this.lineBuffer, this.bufferSentIndex + 1, this.traceLines, num, this.lineBuffer.Length - 1 - this.bufferSentIndex);
				}
				if (this.bufferWriteIndex != 0)
				{
					Array.Copy(this.lineBuffer, 0, this.traceLines, num + (this.lineBuffer.Length - 1 - this.bufferSentIndex), this.bufferWriteIndex);
				}
			}
			this.bufferSentIndex = this.bufferWriteIndex - 1;
			if (this.bufferSentIndex < 0)
			{
				this.bufferSentIndex = this.lineBuffer.Length - 1;
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000B9D8C File Offset: 0x000B7F8C
		public void ConnectToTraceViewer()
		{
			Process.GetCurrentProcess();
			while (!this.connected)
			{
				try
				{
					this.liveTracing = ChannelFactory<ILiveTracing>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/LiveTracing"));
					this.writerNumber = this.liveTracing.ContactLiveTracing();
					this.connected = true;
					break;
				}
				catch
				{
				}
				if (this.tryConnect)
				{
					Thread.Sleep(5000);
				}
				if (!this.tryConnect)
				{
					break;
				}
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000B9E0C File Offset: 0x000B800C
		public void WriteNeededLines()
		{
			string[] array;
			try
			{
				bool flag = true;
				while (flag)
				{
					this.liveTracing.SendTraceLines(this.writerNumber, this.traceLines);
					array = this.lineBuffer;
					lock (array)
					{
						int num;
						if (this.bufferSentIndex < this.bufferWriteIndex)
						{
							num = this.bufferWriteIndex - this.bufferSentIndex - 1;
						}
						else
						{
							num = this.lineBuffer.Length - 1 - this.bufferSentIndex + this.bufferWriteIndex;
						}
						if (num == 0)
						{
							flag = false;
						}
						else
						{
							this.CopyLinesToSendBuffer(num);
						}
					}
				}
			}
			catch
			{
				this.connected = false;
				if (this.tryConnect)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(LiveTraceWriter.ThreadProcToConnect), this);
				}
			}
			array = this.lineBuffer;
			lock (array)
			{
				this.sendInProgress = false;
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000B9F10 File Offset: 0x000B8110
		private static void ThreadProcToWrite(object ourselves)
		{
			(ourselves as LiveTraceWriter).WriteNeededLines();
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000B9F1D File Offset: 0x000B811D
		private static void ThreadProcToConnect(object ourselves)
		{
			(ourselves as LiveTraceWriter).ConnectToTraceViewer();
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000B9F2A File Offset: 0x000B812A
		public void Close()
		{
			this.tryConnect = false;
			this.connected = false;
		}

		// Token: 0x04001F8D RID: 8077
		private ILiveTracing liveTracing;

		// Token: 0x04001F8E RID: 8078
		private bool tryConnect;

		// Token: 0x04001F8F RID: 8079
		private bool connected;

		// Token: 0x04001F90 RID: 8080
		private string[] lineBuffer;

		// Token: 0x04001F91 RID: 8081
		private int allowedLines;

		// Token: 0x04001F92 RID: 8082
		private int bufferWriteIndex;

		// Token: 0x04001F93 RID: 8083
		private int bufferSentIndex;

		// Token: 0x04001F94 RID: 8084
		private int writerNumber;

		// Token: 0x04001F95 RID: 8085
		private int linesLost;

		// Token: 0x04001F96 RID: 8086
		private bool sendInProgress;

		// Token: 0x04001F97 RID: 8087
		private string[] traceLines;
	}
}
