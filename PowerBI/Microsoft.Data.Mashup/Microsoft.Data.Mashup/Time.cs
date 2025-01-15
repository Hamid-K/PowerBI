using System;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000045 RID: 69
	public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000D20E File Offset: 0x0000B40E
		internal Time(TimeSpan time)
		{
			this.time = time;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000D217 File Offset: 0x0000B417
		public TimeSpan TimeSpan
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D21F File Offset: 0x0000B41F
		public static bool operator ==(Time x, Time y)
		{
			return x.TimeSpan == y.TimeSpan;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D234 File Offset: 0x0000B434
		public static bool operator !=(Time x, Time y)
		{
			return !(x == y);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D240 File Offset: 0x0000B440
		public override bool Equals(object obj)
		{
			return obj is Time && this.Equals((Time)obj);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D258 File Offset: 0x0000B458
		public bool Equals(Time other)
		{
			return this == other;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D268 File Offset: 0x0000B468
		public override int GetHashCode()
		{
			return this.TimeSpan.GetHashCode();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D289 File Offset: 0x0000B489
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Time))
			{
				throw new ArgumentException(ProviderErrorStrings.ObjectNotTime);
			}
			return this.CompareTo((Time)obj);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		public int CompareTo(Time other)
		{
			return this.TimeSpan.CompareTo(other.TimeSpan);
		}

		// Token: 0x040001AC RID: 428
		private readonly TimeSpan time;
	}
}
