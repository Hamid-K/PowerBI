using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E56 RID: 3670
	[DataContract]
	internal class TimeParts<T> : IEquatable<TimeParts<T>> where T : IEquatable<T>
	{
		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060062AB RID: 25259 RVA: 0x00152DBB File Offset: 0x00150FBB
		// (set) Token: 0x060062AC RID: 25260 RVA: 0x00152DC3 File Offset: 0x00150FC3
		[DataMember(Name = "years", IsRequired = false)]
		public T Years { get; set; }

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060062AD RID: 25261 RVA: 0x00152DCC File Offset: 0x00150FCC
		// (set) Token: 0x060062AE RID: 25262 RVA: 0x00152DD4 File Offset: 0x00150FD4
		[DataMember(Name = "months", IsRequired = false)]
		public T Months { get; set; }

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060062AF RID: 25263 RVA: 0x00152DDD File Offset: 0x00150FDD
		// (set) Token: 0x060062B0 RID: 25264 RVA: 0x00152DE5 File Offset: 0x00150FE5
		[DataMember(Name = "days", IsRequired = false)]
		public T Days { get; set; }

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00152DEE File Offset: 0x00150FEE
		// (set) Token: 0x060062B2 RID: 25266 RVA: 0x00152DF6 File Offset: 0x00150FF6
		[DataMember(Name = "hours", IsRequired = false)]
		public T Hours { get; set; }

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060062B3 RID: 25267 RVA: 0x00152DFF File Offset: 0x00150FFF
		// (set) Token: 0x060062B4 RID: 25268 RVA: 0x00152E07 File Offset: 0x00151007
		[DataMember(Name = "minutes", IsRequired = false)]
		public T Minutes { get; set; }

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x00152E10 File Offset: 0x00151010
		// (set) Token: 0x060062B6 RID: 25270 RVA: 0x00152E18 File Offset: 0x00151018
		[DataMember(Name = "seconds", IsRequired = false)]
		public T Seconds { get; set; }

		// Token: 0x060062B7 RID: 25271 RVA: 0x00152E24 File Offset: 0x00151024
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Years != null)
			{
				int num2 = num;
				T t = this.Years;
				num = num2 + t.GetHashCode();
			}
			if (this.Months != null)
			{
				int num3 = num;
				T t = this.Months;
				num = num3 + t.GetHashCode();
			}
			if (this.Days != null)
			{
				int num4 = num;
				T t = this.Days;
				num = num4 + t.GetHashCode();
			}
			if (this.Hours != null)
			{
				int num5 = num;
				T t = this.Hours;
				num = num5 + t.GetHashCode();
			}
			if (this.Minutes != null)
			{
				int num6 = num;
				T t = this.Minutes;
				num = num6 + t.GetHashCode();
			}
			if (this.Seconds != null)
			{
				int num7 = num;
				T t = this.Seconds;
				num = num7 + t.GetHashCode();
			}
			return num;
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x00152F0C File Offset: 0x0015110C
		public override bool Equals(object other)
		{
			return this.Equals(other as TimeParts<T>);
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x00152F1C File Offset: 0x0015111C
		public virtual bool Equals(TimeParts<T> other)
		{
			return this.Years.NullableEquals(other.Years) && this.Months.NullableEquals(other.Months) && this.Days.NullableEquals(other.Days) && this.Hours.NullableEquals(other.Hours) && this.Minutes.NullableEquals(other.Minutes) && this.Seconds.NullableEquals(other.Seconds);
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x00152F9B File Offset: 0x0015119B
		public override string ToString()
		{
			return this.ToJson();
		}
	}
}
