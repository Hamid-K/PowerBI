using System;
using System.Threading;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000478 RID: 1144
	public abstract class CircularAsynchronousBuffer
	{
		// Token: 0x060027C6 RID: 10182 RVA: 0x00077F60 File Offset: 0x00076160
		protected CircularAsynchronousBuffer()
		{
			this.bufferA = new CircularAsynchronousBuffer.BufferData(CircularAsynchronousBuffer.BufferType.A, this.numelem);
			this.bufferB = new CircularAsynchronousBuffer.BufferData(CircularAsynchronousBuffer.BufferType.B, this.numelem);
			this.buffersHaveData = new Semaphore(0, 2 * this.numelem);
			this.threadCollector = null;
			this.StartThread();
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00077FB8 File Offset: 0x000761B8
		public void StartThread()
		{
			if (this.threadCollector != null)
			{
				return;
			}
			this.stopThread = false;
			this.threadCollector = new Thread(new ThreadStart(this.CollectEvents));
			this.threadCollector.Name = "CollectEventsThread";
			this.threadCollector.Start();
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00078007 File Offset: 0x00076207
		public void StopThread()
		{
			if (this.threadCollector == null)
			{
				return;
			}
			this.stopThread = true;
			this.threadCollector.Join();
			this.threadCollector = null;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0007802C File Offset: 0x0007622C
		private void ProcessEntryFromBuffer(CircularAsynchronousBuffer.BufferType bufferType)
		{
			CircularAsynchronousBuffer.BufferData bufferFromType = this.GetBufferFromType(bufferType);
			int firstAvailPos = bufferFromType.FirstAvailPos;
			while (!bufferFromType.Entries[firstAvailPos].IsSet)
			{
				Thread.Sleep(1);
			}
			bufferFromType.FirstAvailPos = (bufferFromType.FirstAvailPos + 1) % this.numelem;
			bufferFromType.Entries[firstAvailPos].IsSet = false;
			this.ProcessEntry(bufferType, firstAvailPos);
			this.ResetDropped(bufferFromType, firstAvailPos);
			if (bufferFromType.Entries[firstAvailPos].Notif != null)
			{
				bufferFromType.Entries[firstAvailPos].Notif.Set();
				bufferFromType.Entries[firstAvailPos].Notif = null;
			}
			Interlocked.Add(ref bufferFromType.FreeEntries, 1);
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000780E4 File Offset: 0x000762E4
		protected void CollectEvents()
		{
			int[] array = new int[2];
			CircularAsynchronousBuffer.BufferType bufferType = CircularAsynchronousBuffer.BufferType.A;
			while (!this.stopThread)
			{
				try
				{
					if (this.buffersHaveData.WaitOne(this.MaximumWaitingTimeMillis, false))
					{
						bool flag = this.numelem - this.bufferA.FreeEntries > 0;
						bool flag2 = this.numelem - this.bufferB.FreeEntries > 0;
						if (flag && !flag2)
						{
							bufferType = CircularAsynchronousBuffer.BufferType.A;
						}
						else if (flag2 && !flag)
						{
							bufferType = CircularAsynchronousBuffer.BufferType.B;
						}
						else
						{
							array[0] = this.bufferA.FirstAvailPos;
							array[1] = this.bufferB.FirstAvailPos;
							bufferType = this.PickBufferToProcess(array);
						}
						this.ProcessEntryFromBuffer(bufferType);
					}
				}
				catch (ThreadInterruptedException)
				{
					this.stopThread = true;
				}
				catch (Exception ex)
				{
					this.ProcessException(bufferType, ex);
				}
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000781C4 File Offset: 0x000763C4
		private CircularAsynchronousBuffer.BufferData GetBufferFromType(CircularAsynchronousBuffer.BufferType bufferType)
		{
			if (bufferType != CircularAsynchronousBuffer.BufferType.A)
			{
				return this.bufferB;
			}
			return this.bufferA;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000781D8 File Offset: 0x000763D8
		protected int GetDropped(CircularAsynchronousBuffer.BufferType bufferType, int pos)
		{
			CircularAsynchronousBuffer.BufferData bufferFromType = this.GetBufferFromType(bufferType);
			return bufferFromType.Entries[pos].Dropped;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00078200 File Offset: 0x00076400
		protected void AddDropped(CircularAsynchronousBuffer.BufferData buffer)
		{
			int num = (buffer.FirstAvailPos + this.numelem - 1) % this.numelem;
			Interlocked.Add(ref buffer.Entries[num].Dropped, 1);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0007823C File Offset: 0x0007643C
		protected void ResetDropped(CircularAsynchronousBuffer.BufferData buffer, int pos)
		{
			Interlocked.Exchange(ref buffer.Entries[pos].Dropped, 0);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00078258 File Offset: 0x00076458
		protected bool AddEntry(CircularAsynchronousBuffer.BufferType bufferType, bool waitForCompletion, params object[] parameters)
		{
			CircularAsynchronousBuffer.BufferData bufferFromType = this.GetBufferFromType(bufferType);
			if (Interlocked.Add(ref bufferFromType.FreeEntries, -1) + 1 <= 0)
			{
				this.AddDropped(bufferFromType);
				Interlocked.Add(ref bufferFromType.FreeEntries, 1);
				return false;
			}
			int num = Interlocked.Add(ref bufferFromType.NextPos, 1) - 1;
			if (bufferFromType.NextPos >= this.numelem)
			{
				lock (bufferFromType.NextObj)
				{
					if (bufferFromType.NextPos >= this.numelem)
					{
						Interlocked.Add(ref bufferFromType.NextPos, -this.numelem);
					}
				}
			}
			num %= this.numelem;
			ManualResetEvent manualResetEvent = null;
			if (waitForCompletion)
			{
				manualResetEvent = new ManualResetEvent(false);
				bufferFromType.Entries[num].Notif = manualResetEvent;
			}
			this.FillEntry(bufferType, num, parameters);
			bufferFromType.Entries[num].IsSet = true;
			this.buffersHaveData.Release();
			if (manualResetEvent != null)
			{
				manualResetEvent.WaitOne();
			}
			return true;
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060027D0 RID: 10192
		protected abstract int numelem { get; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060027D1 RID: 10193
		protected abstract int MaximumWaitingTimeMillis { get; }

		// Token: 0x060027D2 RID: 10194
		protected abstract void ProcessEntry(CircularAsynchronousBuffer.BufferType bufferType, int pos);

		// Token: 0x060027D3 RID: 10195
		protected abstract void ProcessException(CircularAsynchronousBuffer.BufferType bufferType, Exception e);

		// Token: 0x060027D4 RID: 10196
		protected abstract void FillEntry(CircularAsynchronousBuffer.BufferType bufferType, int next, params object[] parameters);

		// Token: 0x060027D5 RID: 10197
		protected abstract CircularAsynchronousBuffer.BufferType PickBufferToProcess(int[] indices);

		// Token: 0x04001768 RID: 5992
		private CircularAsynchronousBuffer.BufferData bufferA;

		// Token: 0x04001769 RID: 5993
		private CircularAsynchronousBuffer.BufferData bufferB;

		// Token: 0x0400176A RID: 5994
		private Semaphore buffersHaveData;

		// Token: 0x0400176B RID: 5995
		internal Thread threadCollector;

		// Token: 0x0400176C RID: 5996
		internal bool stopThread;

		// Token: 0x02000479 RID: 1145
		protected struct ControlData
		{
			// Token: 0x0400176D RID: 5997
			public bool IsSet;

			// Token: 0x0400176E RID: 5998
			public int Dropped;

			// Token: 0x0400176F RID: 5999
			public ManualResetEvent Notif;
		}

		// Token: 0x0200047A RID: 1146
		protected enum BufferType
		{
			// Token: 0x04001771 RID: 6001
			A,
			// Token: 0x04001772 RID: 6002
			B
		}

		// Token: 0x0200047B RID: 1147
		protected class BufferData
		{
			// Token: 0x060027D6 RID: 10198 RVA: 0x0007835C File Offset: 0x0007655C
			internal BufferData(CircularAsynchronousBuffer.BufferType bufferType, int numelem)
			{
				this.BufferType = bufferType;
				this.Entries = new CircularAsynchronousBuffer.ControlData[numelem];
				for (int i = 0; i < numelem; i++)
				{
					this.Entries[i].IsSet = false;
					this.Entries[i].Dropped = 0;
					this.Entries[i].Notif = null;
				}
				this.NextPos = 0;
				this.FirstAvailPos = 0;
				this.FreeEntries = numelem;
			}

			// Token: 0x04001773 RID: 6003
			internal CircularAsynchronousBuffer.ControlData[] Entries;

			// Token: 0x04001774 RID: 6004
			internal CircularAsynchronousBuffer.BufferType BufferType;

			// Token: 0x04001775 RID: 6005
			internal int NextPos;

			// Token: 0x04001776 RID: 6006
			internal object NextObj = new object();

			// Token: 0x04001777 RID: 6007
			internal int FirstAvailPos;

			// Token: 0x04001778 RID: 6008
			internal int FreeEntries;
		}
	}
}
