using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000122 RID: 290
	public struct Point
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x0003EA6C File Offset: 0x0003CC6C
		public Point(Length x, Length y)
		{
			this._x = x;
			this._y = y;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0003EA7C File Offset: 0x0003CC7C
		public Length X
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0003EA84 File Offset: 0x0003CC84
		public Length Y
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x040008BA RID: 2234
		public static readonly Point Center = new Point(Length.Half, Length.Half);

		// Token: 0x040008BB RID: 2235
		public static readonly Point LeftTop = new Point(Length.Zero, Length.Zero);

		// Token: 0x040008BC RID: 2236
		public static readonly Point RightTop = new Point(Length.Full, Length.Zero);

		// Token: 0x040008BD RID: 2237
		public static readonly Point RightBottom = new Point(Length.Full, Length.Full);

		// Token: 0x040008BE RID: 2238
		public static readonly Point LeftBottom = new Point(Length.Zero, Length.Full);

		// Token: 0x040008BF RID: 2239
		private readonly Length _x;

		// Token: 0x040008C0 RID: 2240
		private readonly Length _y;
	}
}
