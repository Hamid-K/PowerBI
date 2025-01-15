using System;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Media;

namespace AngleSharp.Services
{
	// Token: 0x02000034 RID: 52
	public interface IRenderingService
	{
		// Token: 0x06000137 RID: 311
		bool IsSupportingContext(string contextId);

		// Token: 0x06000138 RID: 312
		IRenderingContext CreateContext(IHtmlCanvasElement host, string contextId);
	}
}
