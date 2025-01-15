using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000127 RID: 295
	public sealed class Shadow
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0003EFBC File Offset: 0x0003D1BC
		public Shadow(bool inset, Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
		{
			this._inset = inset;
			this._offsetX = offsetX;
			this._offsetY = offsetY;
			this._blurRadius = blurRadius;
			this._spreadRadius = spreadRadius;
			this._color = color;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0003EFF1 File Offset: 0x0003D1F1
		public Color Color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0003EFF9 File Offset: 0x0003D1F9
		public Length OffsetX
		{
			get
			{
				return this._offsetX;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0003F001 File Offset: 0x0003D201
		public Length OffsetY
		{
			get
			{
				return this._offsetY;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0003F009 File Offset: 0x0003D209
		public Length BlurRadius
		{
			get
			{
				return this._blurRadius;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0003F011 File Offset: 0x0003D211
		public Length SpreadRadius
		{
			get
			{
				return this._spreadRadius;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0003F019 File Offset: 0x0003D219
		public bool IsInset
		{
			get
			{
				return this._inset;
			}
		}

		// Token: 0x040008D1 RID: 2257
		private readonly bool _inset;

		// Token: 0x040008D2 RID: 2258
		private readonly Length _offsetX;

		// Token: 0x040008D3 RID: 2259
		private readonly Length _offsetY;

		// Token: 0x040008D4 RID: 2260
		private readonly Length _blurRadius;

		// Token: 0x040008D5 RID: 2261
		private readonly Length _spreadRadius;

		// Token: 0x040008D6 RID: 2262
		private readonly Color _color;
	}
}
