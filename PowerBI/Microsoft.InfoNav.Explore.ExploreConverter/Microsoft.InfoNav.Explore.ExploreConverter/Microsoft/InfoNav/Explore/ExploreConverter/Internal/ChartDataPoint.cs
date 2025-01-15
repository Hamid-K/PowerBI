using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200008F RID: 143
	internal sealed class ChartDataPoint
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000CCD5 File Offset: 0x0000AED5
		internal ChartDataPoint(Expression x, Expression y, Expression size)
		{
			this._x = x;
			this._y = y;
			this._size = size;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		internal Expression X
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		internal Expression Y
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000CD02 File Offset: 0x0000AF02
		internal Expression Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x040001E2 RID: 482
		private readonly Expression _x;

		// Token: 0x040001E3 RID: 483
		private readonly Expression _y;

		// Token: 0x040001E4 RID: 484
		private readonly Expression _size;
	}
}
