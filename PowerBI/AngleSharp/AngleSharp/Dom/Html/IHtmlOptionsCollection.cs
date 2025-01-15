using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D7 RID: 983
	[DomName("HTMLOptionsCollection")]
	public interface IHtmlOptionsCollection : IHtmlCollection<IHtmlOptionElement>, IEnumerable<IHtmlOptionElement>, IEnumerable
	{
		// Token: 0x06001F6C RID: 8044
		[DomAccessor(Accessors.Getter)]
		IHtmlOptionElement GetOptionAt(int index);

		// Token: 0x06001F6D RID: 8045
		[DomAccessor(Accessors.Setter)]
		void SetOptionAt(int index, IHtmlOptionElement option);

		// Token: 0x06001F6E RID: 8046
		[DomName("add")]
		void Add(IHtmlOptionElement element, IHtmlElement before = null);

		// Token: 0x06001F6F RID: 8047
		[DomName("add")]
		void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null);

		// Token: 0x06001F70 RID: 8048
		[DomName("remove")]
		void Remove(int index);

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001F71 RID: 8049
		// (set) Token: 0x06001F72 RID: 8050
		[DomName("selectedIndex")]
		int SelectedIndex { get; set; }
	}
}
