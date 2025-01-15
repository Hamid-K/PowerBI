using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Html;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D1 RID: 465
	[DomName("RenderingContext")]
	public interface IRenderingContext
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F90 RID: 3984
		string ContextId { get; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F91 RID: 3985
		bool IsFixed { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F92 RID: 3986
		IHtmlCanvasElement Host { get; }

		// Token: 0x06000F93 RID: 3987
		byte[] ToImage(string type);
	}
}
