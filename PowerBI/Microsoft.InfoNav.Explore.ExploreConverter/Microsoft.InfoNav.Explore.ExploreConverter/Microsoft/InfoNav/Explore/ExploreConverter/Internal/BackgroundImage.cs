using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005E RID: 94
	internal sealed class BackgroundImage
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000B66F File Offset: 0x0000986F
		internal BackgroundImage(ReportImageSource? source, string value, string mimeType, BackgroundImageRepeat repeat, double transparency)
		{
			this._source = new ImageSource(source, value, mimeType);
			this._repeat = repeat;
			this._transparency = transparency;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B695 File Offset: 0x00009895
		public ImageSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000B69D File Offset: 0x0000989D
		public BackgroundImageRepeat Repeat
		{
			get
			{
				return this._repeat;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000B6A5 File Offset: 0x000098A5
		public double Transparency
		{
			get
			{
				return this._transparency;
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly ImageSource _source;

		// Token: 0x0400015E RID: 350
		private readonly BackgroundImageRepeat _repeat;

		// Token: 0x0400015F RID: 351
		private readonly double _transparency;
	}
}
