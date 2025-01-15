using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D0 RID: 464
	[DomName("MediaError")]
	public interface IMediaError
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F8F RID: 3983
		[DomName("code")]
		MediaErrorCode Code { get; }
	}
}
