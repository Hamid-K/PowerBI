using System;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200011D RID: 285
	public sealed class LinearGradient : IGradient, IImageSource
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0003E694 File Offset: 0x0003C894
		public LinearGradient(Angle angle, GradientStop[] stops, bool repeating = false)
		{
			this._stops = stops;
			this._angle = angle;
			this._repeating = repeating;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0003E6B1 File Offset: 0x0003C8B1
		public Angle Angle
		{
			get
			{
				return this._angle;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0003E6B9 File Offset: 0x0003C8B9
		public IEnumerable<GradientStop> Stops
		{
			get
			{
				return this._stops.AsEnumerable<GradientStop>();
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0003E6C6 File Offset: 0x0003C8C6
		public bool IsRepeating
		{
			get
			{
				return this._repeating;
			}
		}

		// Token: 0x040008AC RID: 2220
		private readonly GradientStop[] _stops;

		// Token: 0x040008AD RID: 2221
		private readonly Angle _angle;

		// Token: 0x040008AE RID: 2222
		private readonly bool _repeating;
	}
}
