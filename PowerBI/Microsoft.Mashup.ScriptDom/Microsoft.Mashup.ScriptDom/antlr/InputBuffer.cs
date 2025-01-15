using System;
using System.Text;

namespace antlr
{
	// Token: 0x02000005 RID: 5
	internal abstract class InputBuffer
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000029FD File Offset: 0x00000BFD
		public InputBuffer()
		{
			this.queue = new CircularBuffer();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A10 File Offset: 0x00000C10
		public virtual void commit()
		{
			this.nMarkers--;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A20 File Offset: 0x00000C20
		public virtual char consume()
		{
			this.numToConsume++;
			return this.LA(1);
		}

		// Token: 0x06000037 RID: 55
		public abstract void fill(int amount);

		// Token: 0x06000038 RID: 56 RVA: 0x00002A38 File Offset: 0x00000C38
		public virtual string getLAChars()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = new char[this.queue.Count - this.markerOffset];
			this.queue.CopyTo(array, this.markerOffset);
			stringBuilder.Append(array);
			return stringBuilder.ToString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A84 File Offset: 0x00000C84
		public virtual string getMarkedChars()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = new char[this.queue.Count - this.markerOffset];
			this.queue.CopyTo(array, this.markerOffset);
			stringBuilder.Append(array);
			return stringBuilder.ToString();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ACF File Offset: 0x00000CCF
		public virtual bool isMarked()
		{
			return this.nMarkers != 0;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002ADD File Offset: 0x00000CDD
		public virtual char LA(int i)
		{
			this.fill(i);
			return this.queue[this.markerOffset + i - 1];
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AFB File Offset: 0x00000CFB
		public virtual int mark()
		{
			this.syncConsume();
			this.nMarkers++;
			return this.markerOffset;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B17 File Offset: 0x00000D17
		public virtual void rewind(int mark)
		{
			this.syncConsume();
			this.markerOffset = mark;
			this.nMarkers--;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B34 File Offset: 0x00000D34
		public virtual void reset()
		{
			this.nMarkers = 0;
			this.markerOffset = 0;
			this.numToConsume = 0;
			this.queue.Clear();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B58 File Offset: 0x00000D58
		protected internal virtual void syncConsume()
		{
			if (this.numToConsume > 0)
			{
				if (this.nMarkers > 0)
				{
					this.markerOffset += this.numToConsume;
				}
				else
				{
					this.queue.RemoveRange(0, this.numToConsume);
				}
				this.numToConsume = 0;
			}
		}

		// Token: 0x04000006 RID: 6
		protected internal int nMarkers;

		// Token: 0x04000007 RID: 7
		protected internal int markerOffset;

		// Token: 0x04000008 RID: 8
		protected internal int numToConsume;

		// Token: 0x04000009 RID: 9
		protected CircularBuffer queue;
	}
}
