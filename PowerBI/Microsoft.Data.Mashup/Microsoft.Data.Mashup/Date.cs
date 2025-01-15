using System;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000043 RID: 67
	public struct Date : IComparable, IComparable<Date>, IEquatable<Date>
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000D150 File Offset: 0x0000B350
		internal Date(DateTime date)
		{
			this.date = date;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000D159 File Offset: 0x0000B359
		public DateTime DateTime
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D161 File Offset: 0x0000B361
		public static bool operator ==(Date x, Date y)
		{
			return x.DateTime == y.DateTime;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D176 File Offset: 0x0000B376
		public static bool operator !=(Date x, Date y)
		{
			return !(x == y);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D182 File Offset: 0x0000B382
		public override bool Equals(object obj)
		{
			return obj is Date && this.Equals((Date)obj);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000D19A File Offset: 0x0000B39A
		public bool Equals(Date other)
		{
			return this == other;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public override int GetHashCode()
		{
			return this.DateTime.GetHashCode();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Date))
			{
				throw new ArgumentException(ProviderErrorStrings.ObjectNotDate);
			}
			return this.CompareTo((Date)obj);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		public int CompareTo(Date other)
		{
			return this.DateTime.CompareTo(other.DateTime);
		}

		// Token: 0x040001AB RID: 427
		private readonly DateTime date;
	}
}
