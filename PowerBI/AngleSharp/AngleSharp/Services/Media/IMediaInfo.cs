using System;
using AngleSharp.Dom.Media;

namespace AngleSharp.Services.Media
{
	// Token: 0x02000041 RID: 65
	public interface IMediaInfo : IResourceInfo
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015B RID: 347
		IMediaController Controller { get; }
	}
}
