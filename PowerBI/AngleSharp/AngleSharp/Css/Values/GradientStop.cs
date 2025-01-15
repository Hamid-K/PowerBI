using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000117 RID: 279
	public struct GradientStop
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x0003DF48 File Offset: 0x0003C148
		public GradientStop(Color color, Length location)
		{
			this._color = color;
			this._location = location;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0003DF58 File Offset: 0x0003C158
		public Color Color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0003DF60 File Offset: 0x0003C160
		public Length Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x040008A1 RID: 2209
		private readonly Color _color;

		// Token: 0x040008A2 RID: 2210
		private readonly Length _location;
	}
}
