using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B2 RID: 178
	internal sealed class Size
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00014163 File Offset: 0x00012363
		internal Size(ReportSize width, ReportSize minWidth, ReportSize maxWidth, ReportSize height, ReportSize minHeight, ReportSize maxHeight)
		{
			this._width = width;
			this._minWidth = minWidth;
			this._maxWidth = maxWidth;
			this._height = height;
			this._minHeight = minHeight;
			this._maxHeight = maxHeight;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00014198 File Offset: 0x00012398
		internal ReportSize Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000141A0 File Offset: 0x000123A0
		internal ReportSize MinWidth
		{
			get
			{
				return this._minWidth;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000141A8 File Offset: 0x000123A8
		internal ReportSize MaxWidth
		{
			get
			{
				return this._maxWidth;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000141B0 File Offset: 0x000123B0
		internal ReportSize Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000141B8 File Offset: 0x000123B8
		internal ReportSize MinHeight
		{
			get
			{
				return this._minHeight;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000141C0 File Offset: 0x000123C0
		internal ReportSize MaxHeight
		{
			get
			{
				return this._maxHeight;
			}
		}

		// Token: 0x04000245 RID: 581
		private readonly ReportSize _width;

		// Token: 0x04000246 RID: 582
		private readonly ReportSize _minWidth;

		// Token: 0x04000247 RID: 583
		private readonly ReportSize _maxWidth;

		// Token: 0x04000248 RID: 584
		private readonly ReportSize _height;

		// Token: 0x04000249 RID: 585
		private readonly ReportSize _minHeight;

		// Token: 0x0400024A RID: 586
		private readonly ReportSize _maxHeight;
	}
}
