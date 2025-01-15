using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009C RID: 156
	internal class ReportItemRect
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000D017 File Offset: 0x0000B217
		internal ReportItemRect(ReportSize left, ReportSize top, Size size)
		{
			this._size = size;
			this._top = top;
			this._left = left;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000D034 File Offset: 0x0000B234
		public Size Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000D03C File Offset: 0x0000B23C
		public ReportSize Top
		{
			get
			{
				return this._top;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000D044 File Offset: 0x0000B244
		public ReportSize Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x04000204 RID: 516
		private readonly Size _size;

		// Token: 0x04000205 RID: 517
		private readonly ReportSize _top;

		// Token: 0x04000206 RID: 518
		private readonly ReportSize _left;
	}
}
