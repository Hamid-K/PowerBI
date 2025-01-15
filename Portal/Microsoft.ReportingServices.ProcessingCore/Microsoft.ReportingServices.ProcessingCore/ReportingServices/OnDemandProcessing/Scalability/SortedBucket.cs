using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A2 RID: 2210
	internal sealed class SortedBucket
	{
		// Token: 0x06007911 RID: 30993 RVA: 0x001F2F30 File Offset: 0x001F1130
		internal SortedBucket(int maxSpacesPerBucket)
		{
			int num = 500;
			if (num > maxSpacesPerBucket)
			{
				num = maxSpacesPerBucket;
			}
			this.m_spaces = new Heap<long, Space>(num, maxSpacesPerBucket);
			this.Minimum = int.MaxValue;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x001F2F68 File Offset: 0x001F1168
		internal SortedBucket Split(int maxSpacesPerBucket)
		{
			SortedBucket sortedBucket = new SortedBucket(maxSpacesPerBucket);
			int num = this.Count / 2;
			for (int i = 0; i < num; i++)
			{
				sortedBucket.Insert(this.ExtractMax());
			}
			sortedBucket.Limit = sortedBucket.Minimum;
			return sortedBucket;
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x001F2FAA File Offset: 0x001F11AA
		internal void Insert(Space space)
		{
			if (space.Size < (long)this.Minimum)
			{
				this.Minimum = (int)space.Size;
			}
			this.m_spaces.Insert(space.Size, space);
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x001F2FDA File Offset: 0x001F11DA
		internal Space Peek()
		{
			return this.m_spaces.Peek();
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x001F2FE7 File Offset: 0x001F11E7
		internal Space ExtractMax()
		{
			Space space = this.m_spaces.ExtractMax();
			if (this.m_spaces.Count == 0)
			{
				this.Minimum = int.MaxValue;
			}
			return space;
		}

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x001F300C File Offset: 0x001F120C
		internal int Count
		{
			get
			{
				return this.m_spaces.Count;
			}
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x06007917 RID: 30999 RVA: 0x001F3019 File Offset: 0x001F1219
		internal int Maximum
		{
			get
			{
				return (int)this.Peek().Size;
			}
		}

		// Token: 0x04003CCF RID: 15567
		internal int Limit;

		// Token: 0x04003CD0 RID: 15568
		internal int Minimum;

		// Token: 0x04003CD1 RID: 15569
		internal Heap<long, Space> m_spaces;
	}
}
