using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;

namespace AngleSharp.Dom
{
	// Token: 0x0200019E RID: 414
	[DomName("StyleSheet")]
	public interface IStyleSheet : IStyleFormattable
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000EB7 RID: 3767
		[DomName("type")]
		string Type { get; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000EB8 RID: 3768
		[DomName("href")]
		string Href { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000EB9 RID: 3769
		[DomName("ownerNode")]
		IElement OwnerNode { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000EBA RID: 3770
		[DomName("title")]
		string Title { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000EBB RID: 3771
		[DomName("media")]
		[DomPutForwards("mediaText")]
		IMediaList Media { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000EBC RID: 3772
		// (set) Token: 0x06000EBD RID: 3773
		[DomName("disabled")]
		bool IsDisabled { get; set; }
	}
}
