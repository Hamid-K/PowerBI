using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200032A RID: 810
	internal class NormalValueStore : IValueStore
	{
		// Token: 0x06001D3C RID: 7484 RVA: 0x0005869A File Offset: 0x0005689A
		public long GetValue()
		{
			return this._value;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000586A2 File Offset: 0x000568A2
		public void Add(long count)
		{
			this._value += count;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000586B2 File Offset: 0x000568B2
		public void Increment()
		{
			this._value += 1L;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000586C3 File Offset: 0x000568C3
		public void Decrement()
		{
			this._value -= 1L;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000586D4 File Offset: 0x000568D4
		public void SetValue(long value)
		{
			this._value = value;
		}

		// Token: 0x04001038 RID: 4152
		private long _value;
	}
}
