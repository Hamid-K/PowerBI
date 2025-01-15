using System;

namespace antlr
{
	// Token: 0x02000024 RID: 36
	internal class TokenBuffer
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00004DEF File Offset: 0x00002FEF
		public TokenBuffer(TokenStream input_)
		{
			this.input = input_;
			this.queue = new TokenQueue(1);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004E0A File Offset: 0x0000300A
		public virtual void reset()
		{
			this.nMarkers = 0;
			this.markerOffset = 0;
			this.numToConsume = 0;
			this.queue.reset();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004E2C File Offset: 0x0000302C
		public virtual void consume()
		{
			this.numToConsume++;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004E3C File Offset: 0x0000303C
		protected virtual void fill(int amount)
		{
			this.syncConsume();
			while (this.queue.nbrEntries < amount + this.markerOffset)
			{
				this.queue.append(this.input.nextToken());
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004E71 File Offset: 0x00003071
		public virtual TokenStream getInput()
		{
			return this.input;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004E79 File Offset: 0x00003079
		public virtual int LA(int i)
		{
			this.fill(i);
			return this.queue.elementAt(this.markerOffset + i - 1).Type;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004E9C File Offset: 0x0000309C
		public virtual IToken LT(int i)
		{
			this.fill(i);
			return this.queue.elementAt(this.markerOffset + i - 1);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004EBA File Offset: 0x000030BA
		public virtual int mark()
		{
			this.syncConsume();
			this.nMarkers++;
			return this.markerOffset;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004ED6 File Offset: 0x000030D6
		public virtual void rewind(int mark)
		{
			this.syncConsume();
			this.markerOffset = mark;
			this.nMarkers--;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004EF3 File Offset: 0x000030F3
		protected virtual void syncConsume()
		{
			while (this.numToConsume > 0)
			{
				if (this.nMarkers > 0)
				{
					this.markerOffset++;
				}
				else
				{
					this.queue.removeFirst();
				}
				this.numToConsume--;
			}
		}

		// Token: 0x04000087 RID: 135
		protected internal TokenStream input;

		// Token: 0x04000088 RID: 136
		protected internal int nMarkers;

		// Token: 0x04000089 RID: 137
		protected internal int markerOffset;

		// Token: 0x0400008A RID: 138
		protected internal int numToConsume;

		// Token: 0x0400008B RID: 139
		internal TokenQueue queue;
	}
}
