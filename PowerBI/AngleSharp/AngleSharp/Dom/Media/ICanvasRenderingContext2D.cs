using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Html;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CE RID: 462
	[DomName("CanvasRenderingContext2D")]
	public interface ICanvasRenderingContext2D : IRenderingContext
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F59 RID: 3929
		[DomName("canvas")]
		IHtmlCanvasElement Canvas { get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F5A RID: 3930
		// (set) Token: 0x06000F5B RID: 3931
		[DomName("width")]
		int Width { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F5C RID: 3932
		// (set) Token: 0x06000F5D RID: 3933
		[DomName("height")]
		int Height { get; set; }

		// Token: 0x06000F5E RID: 3934
		[DomName("save")]
		void SaveState();

		// Token: 0x06000F5F RID: 3935
		[DomName("restore")]
		void RestoreState();
	}
}
