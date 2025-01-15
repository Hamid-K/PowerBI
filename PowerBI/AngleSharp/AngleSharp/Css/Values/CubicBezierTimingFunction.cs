using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000115 RID: 277
	public sealed class CubicBezierTimingFunction : ITimingFunction
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0003DCE3 File Offset: 0x0003BEE3
		public CubicBezierTimingFunction(float x1, float y1, float x2, float y2)
		{
			this._x1 = x1;
			this._y1 = y1;
			this._x2 = x2;
			this._y2 = y2;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0003DD08 File Offset: 0x0003BF08
		public float X1
		{
			get
			{
				return this._x1;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0003DD10 File Offset: 0x0003BF10
		public float Y1
		{
			get
			{
				return this._y1;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0003DD18 File Offset: 0x0003BF18
		public float X2
		{
			get
			{
				return this._x2;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0003DD20 File Offset: 0x0003BF20
		public float Y2
		{
			get
			{
				return this._y2;
			}
		}

		// Token: 0x0400089B RID: 2203
		private readonly float _x1;

		// Token: 0x0400089C RID: 2204
		private readonly float _y1;

		// Token: 0x0400089D RID: 2205
		private readonly float _x2;

		// Token: 0x0400089E RID: 2206
		private readonly float _y2;
	}
}
