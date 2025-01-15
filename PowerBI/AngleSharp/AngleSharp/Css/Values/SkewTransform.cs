using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000129 RID: 297
	internal sealed class SkewTransform : ITransform
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0003F066 File Offset: 0x0003D266
		internal SkewTransform(float alpha, float beta)
		{
			this._alpha = alpha;
			this._beta = beta;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0003F07C File Offset: 0x0003D27C
		public float Alpha
		{
			get
			{
				return this._alpha;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0003F084 File Offset: 0x0003D284
		public float Beta
		{
			get
			{
				return this._beta;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003F08C File Offset: 0x0003D28C
		public TransformMatrix ComputeMatrix()
		{
			float num = (float)Math.Tan((double)this._alpha);
			float num2 = (float)Math.Tan((double)this._beta);
			return new TransformMatrix(1f, num, 0f, num2, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x040008DB RID: 2267
		private readonly float _alpha;

		// Token: 0x040008DC RID: 2268
		private readonly float _beta;
	}
}
