using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200132E RID: 4910
	internal struct HashBuilder
	{
		// Token: 0x060081C0 RID: 33216 RVA: 0x001B8F20 File Offset: 0x001B7120
		public void Add(int value)
		{
			this.hash += value;
			this.hash += this.hash << 10;
			this.hash ^= this.hash >> 6;
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x001B8F5C File Offset: 0x001B715C
		public int ToHash()
		{
			this.hash += this.hash << 3;
			this.hash ^= this.hash >> 11;
			this.hash += this.hash << 15;
			return this.hash;
		}

		// Token: 0x040046A1 RID: 18081
		private int hash;
	}
}
