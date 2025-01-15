using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000329 RID: 809
	[DomName("CSSProperty")]
	[DomNoInterfaceObject]
	public interface ICssProperty : ICssNode, IStyleFormattable
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001726 RID: 5926
		[DomName("name")]
		string Name { get; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001727 RID: 5927
		[DomName("value")]
		string Value { get; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001728 RID: 5928
		[DomName("important")]
		bool IsImportant { get; }
	}
}
