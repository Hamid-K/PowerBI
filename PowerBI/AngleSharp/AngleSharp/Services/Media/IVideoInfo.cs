using System;

namespace AngleSharp.Services.Media
{
	// Token: 0x02000044 RID: 68
	public interface IVideoInfo : IMediaInfo, IResourceInfo
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000160 RID: 352
		int Width { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000161 RID: 353
		int Height { get; }
	}
}
