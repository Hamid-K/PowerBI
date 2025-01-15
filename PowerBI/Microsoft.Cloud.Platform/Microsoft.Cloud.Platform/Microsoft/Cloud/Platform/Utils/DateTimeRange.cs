using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C2 RID: 706
	[DataContract]
	public sealed class DateTimeRange : IEquatable<DateTimeRange>
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x00041CCB File Offset: 0x0003FECB
		public DateTimeRange(DateTime begin, TimeSpan span)
			: this(begin, begin.Add(span))
		{
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00041CDC File Offset: 0x0003FEDC
		public DateTimeRange(DateTime begin, DateTime end)
		{
			this.Begin = begin;
			this.End = end;
			ExtendedDiagnostics.EnsureArgumentIsNotNegative((long)this.Span.TotalMilliseconds, "end, begin");
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00041D16 File Offset: 0x0003FF16
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00041D1E File Offset: 0x0003FF1E
		[DataMember]
		public DateTime Begin { get; private set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00041D27 File Offset: 0x0003FF27
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00041D2F File Offset: 0x0003FF2F
		[DataMember]
		public DateTime End { get; private set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00041D38 File Offset: 0x0003FF38
		public TimeSpan Span
		{
			get
			{
				return this.End.Subtract(this.Begin);
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00041D5C File Offset: 0x0003FF5C
		public static DateTimeRange Aggregate(DateTimeRange a, DateTimeRange b)
		{
			return new DateTimeRange((a.Begin > b.Begin) ? b.Begin : a.Begin, (a.End > b.End) ? a.End : b.End);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00041DB0 File Offset: 0x0003FFB0
		public static DateTimeRange All
		{
			get
			{
				return new DateTimeRange(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc));
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00041DEC File Offset: 0x0003FFEC
		public bool Equals(DateTimeRange other)
		{
			return other != null && this.Begin.Equals(other.Begin) && this.End.Equals(other.End);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00041E2A File Offset: 0x0004002A
		public override bool Equals(object other)
		{
			return this.Equals(other as DateTimeRange);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00041E38 File Offset: 0x00040038
		public override int GetHashCode()
		{
			return this.Begin.GetHashCode() ^ this.End.GetHashCode();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00041E64 File Offset: 0x00040064
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "<Begin={0}, End={1}>", new object[]
			{
				this.Begin.ToString("R", CultureInfo.InvariantCulture),
				this.End.ToString("R", CultureInfo.InvariantCulture)
			});
		}
	}
}
