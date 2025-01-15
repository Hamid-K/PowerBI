using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000128 RID: 296
	public sealed class Shape
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0003F021 File Offset: 0x0003D221
		public Shape(Length top, Length right, Length bottom, Length left)
		{
			this._top = top;
			this._right = right;
			this._bottom = bottom;
			this._left = left;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0003F046 File Offset: 0x0003D246
		public Length Top
		{
			get
			{
				return this._top;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0003F04E File Offset: 0x0003D24E
		public Length Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0003F056 File Offset: 0x0003D256
		public Length Bottom
		{
			get
			{
				return this._bottom;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0003F05E File Offset: 0x0003D25E
		public Length Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x040008D7 RID: 2263
		private readonly Length _top;

		// Token: 0x040008D8 RID: 2264
		private readonly Length _right;

		// Token: 0x040008D9 RID: 2265
		private readonly Length _bottom;

		// Token: 0x040008DA RID: 2266
		private readonly Length _left;
	}
}
