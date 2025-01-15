using System;

namespace AngleSharp.Services.Media
{
	// Token: 0x02000040 RID: 64
	public interface IImageInfo : IResourceInfo
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000159 RID: 345
		int Width { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015A RID: 346
		int Height { get; }
	}
}
