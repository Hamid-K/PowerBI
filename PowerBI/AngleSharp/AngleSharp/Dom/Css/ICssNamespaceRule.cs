using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000325 RID: 805
	[DomName("CSSNamespaceRule")]
	public interface ICssNamespaceRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001717 RID: 5911
		// (set) Token: 0x06001718 RID: 5912
		[DomName("namespaceURI")]
		string NamespaceUri { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001719 RID: 5913
		// (set) Token: 0x0600171A RID: 5914
		[DomName("prefix")]
		string Prefix { get; set; }
	}
}
