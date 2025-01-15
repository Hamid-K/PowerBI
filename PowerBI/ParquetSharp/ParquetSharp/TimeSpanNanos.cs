using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200008F RID: 143
	public readonly struct TimeSpanNanos : IEquatable<TimeSpanNanos>
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public TimeSpanNanos(long ticks)
		{
			this.Ticks = ticks;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		public TimeSpanNanos(TimeSpan timeSpan)
		{
			this.Ticks = timeSpan.Ticks * 100L;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		public TimeSpan TimeSpan
		{
			get
			{
				return TimeSpan.FromTicks(this.Ticks / 100L);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public bool Equals(TimeSpanNanos other)
		{
			return this.Ticks == other.Ticks;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		[NullableContext(1)]
		public override string ToString()
		{
			return this.TimeSpan.ToString();
		}

		// Token: 0x04000134 RID: 308
		public readonly long Ticks;
	}
}
