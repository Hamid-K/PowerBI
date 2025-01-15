using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000329 RID: 809
	internal sealed class SafeValueStore : IValueStore
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x00058653 File Offset: 0x00056853
		public long GetValue()
		{
			return Interlocked.Read(ref this._value);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00058660 File Offset: 0x00056860
		public void Add(long count)
		{
			Interlocked.Add(ref this._value, count);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0005866F File Offset: 0x0005686F
		public void Increment()
		{
			Interlocked.Increment(ref this._value);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0005867D File Offset: 0x0005687D
		public void Decrement()
		{
			Interlocked.Decrement(ref this._value);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0005868B File Offset: 0x0005688B
		public void SetValue(long value)
		{
			Interlocked.Exchange(ref this._value, value);
		}

		// Token: 0x04001037 RID: 4151
		private long _value;
	}
}
