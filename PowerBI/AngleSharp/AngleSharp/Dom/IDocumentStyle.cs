using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000182 RID: 386
	[DomName("DocumentStyle")]
	[DomNoInterfaceObject]
	public interface IDocumentStyle
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E05 RID: 3589
		[DomName("styleSheets")]
		IStyleSheetList StyleSheets { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E06 RID: 3590
		// (set) Token: 0x06000E07 RID: 3591
		[DomName("selectedStyleSheetSet")]
		string SelectedStyleSheetSet { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E08 RID: 3592
		[DomName("lastStyleSheetSet")]
		string LastStyleSheetSet { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E09 RID: 3593
		[DomName("preferredStyleSheetSet")]
		string PreferredStyleSheetSet { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E0A RID: 3594
		[DomName("styleSheetSets")]
		IStringList StyleSheetSets { get; }

		// Token: 0x06000E0B RID: 3595
		[DomName("enableStyleSheetsForSet")]
		void EnableStyleSheetsForSet(string name);
	}
}
