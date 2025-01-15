using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200012C RID: 300
	internal sealed class TranslateTransform : ITransform
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x0003F34E File Offset: 0x0003D54E
		internal TranslateTransform(Length x, Length y, Length z)
		{
			this._x = x;
			this._y = y;
			this._z = z;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0003F36B File Offset: 0x0003D56B
		public Length Dx
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0003F373 File Offset: 0x0003D573
		public Length Dy
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0003F37B File Offset: 0x0003D57B
		public Length Dz
		{
			get
			{
				return this._z;
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003F384 File Offset: 0x0003D584
		public TransformMatrix ComputeMatrix()
		{
			float num = this._x.ToPixel();
			float num2 = this._y.ToPixel();
			float num3 = this._z.ToPixel();
			return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, num, num2, num3, 0f, 0f, 0f);
		}

		// Token: 0x040008E2 RID: 2274
		private readonly Length _x;

		// Token: 0x040008E3 RID: 2275
		private readonly Length _y;

		// Token: 0x040008E4 RID: 2276
		private readonly Length _z;
	}
}
