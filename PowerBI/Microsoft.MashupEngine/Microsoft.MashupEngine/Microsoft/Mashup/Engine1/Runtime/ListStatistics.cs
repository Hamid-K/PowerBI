using System;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001561 RID: 5473
	public class ListStatistics
	{
		// Token: 0x0600880D RID: 34829 RVA: 0x001CD760 File Offset: 0x001CB960
		public ListStatistics(TypeValue type, Value minimum, Value maximum, long totalCount, long nullCount)
		{
			this.type = type;
			this.minimum = minimum;
			this.maximum = maximum;
			this.totalCount = totalCount;
			this.nullCount = nullCount;
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x0600880E RID: 34830 RVA: 0x001CD78D File Offset: 0x001CB98D
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x0600880F RID: 34831 RVA: 0x001CD795 File Offset: 0x001CB995
		public Value Minimum
		{
			get
			{
				return this.minimum;
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x06008810 RID: 34832 RVA: 0x001CD79D File Offset: 0x001CB99D
		public Value Maximum
		{
			get
			{
				return this.maximum;
			}
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x06008811 RID: 34833 RVA: 0x001CD7A5 File Offset: 0x001CB9A5
		public Value TotalCount
		{
			get
			{
				return NumberValue.New(this.totalCount);
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x06008812 RID: 34834 RVA: 0x001CD7B2 File Offset: 0x001CB9B2
		public Value NullCount
		{
			get
			{
				return NumberValue.New(this.nullCount);
			}
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x06008813 RID: 34835 RVA: 0x001CD7BF File Offset: 0x001CB9BF
		public Value NotNullCount
		{
			get
			{
				return NumberValue.New(this.totalCount - this.nullCount);
			}
		}

		// Token: 0x06008814 RID: 34836 RVA: 0x001CD7D4 File Offset: 0x001CB9D4
		public ListStatistics Combine(ListStatistics other)
		{
			return new ListStatistics(TypeAlgebra.Union(this.type, other.Type), Library.List.Min.Invoke(ListValue.New(new Value[] { this.Minimum, other.Minimum })), Library.List.Max.Invoke(ListValue.New(new Value[] { this.Maximum, other.Maximum })), this.totalCount + other.totalCount, this.nullCount + other.nullCount);
		}

		// Token: 0x04004B65 RID: 19301
		public static readonly ListStatistics Empty = new ListStatistics(TypeValue.None, Value.Null, Value.Null, 0L, 0L);

		// Token: 0x04004B66 RID: 19302
		private readonly TypeValue type;

		// Token: 0x04004B67 RID: 19303
		private readonly Value minimum;

		// Token: 0x04004B68 RID: 19304
		private readonly Value maximum;

		// Token: 0x04004B69 RID: 19305
		private readonly long totalCount;

		// Token: 0x04004B6A RID: 19306
		private readonly long nullCount;
	}
}
