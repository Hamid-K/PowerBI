using System;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000123 RID: 291
	public sealed class RadialGradient : IImageSource
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0003EAFD File Offset: 0x0003CCFD
		public RadialGradient(bool circle, Point pt, Length width, Length height, RadialGradient.SizeMode sizeMode, GradientStop[] stops, bool repeating = false)
		{
			this._stops = stops;
			this._pt = pt;
			this._width = width;
			this._height = height;
			this._repeating = repeating;
			this._circle = circle;
			this._sizeMode = sizeMode;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0003EB3A File Offset: 0x0003CD3A
		public bool IsCircle
		{
			get
			{
				return this._circle;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0003EB42 File Offset: 0x0003CD42
		public RadialGradient.SizeMode Mode
		{
			get
			{
				return this._sizeMode;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0003EB4A File Offset: 0x0003CD4A
		public Point Position
		{
			get
			{
				return this._pt;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0003EB52 File Offset: 0x0003CD52
		public Length MajorRadius
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0003EB5A File Offset: 0x0003CD5A
		public Length MinorRadius
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0003EB62 File Offset: 0x0003CD62
		public IEnumerable<GradientStop> Stops
		{
			get
			{
				return this._stops.AsEnumerable<GradientStop>();
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0003EB6F File Offset: 0x0003CD6F
		public bool IsRepeating
		{
			get
			{
				return this._repeating;
			}
		}

		// Token: 0x040008C1 RID: 2241
		private readonly GradientStop[] _stops;

		// Token: 0x040008C2 RID: 2242
		private readonly Point _pt;

		// Token: 0x040008C3 RID: 2243
		private readonly Length _width;

		// Token: 0x040008C4 RID: 2244
		private readonly Length _height;

		// Token: 0x040008C5 RID: 2245
		private readonly bool _repeating;

		// Token: 0x040008C6 RID: 2246
		private readonly bool _circle;

		// Token: 0x040008C7 RID: 2247
		private readonly RadialGradient.SizeMode _sizeMode;

		// Token: 0x020004AF RID: 1199
		public enum SizeMode : byte
		{
			// Token: 0x0400111C RID: 4380
			None,
			// Token: 0x0400111D RID: 4381
			ClosestCorner,
			// Token: 0x0400111E RID: 4382
			ClosestSide,
			// Token: 0x0400111F RID: 4383
			FarthestCorner,
			// Token: 0x04001120 RID: 4384
			FarthestSide
		}
	}
}
