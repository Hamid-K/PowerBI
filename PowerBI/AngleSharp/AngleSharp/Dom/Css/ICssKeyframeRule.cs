using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000320 RID: 800
	[DomName("CSSKeyframeRule")]
	public interface ICssKeyframeRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001704 RID: 5892
		// (set) Token: 0x06001705 RID: 5893
		[DomName("keyText")]
		string KeyText { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001706 RID: 5894
		[DomName("style")]
		ICssStyleDeclaration Style { get; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001707 RID: 5895
		// (set) Token: 0x06001708 RID: 5896
		IKeyframeSelector Key { get; set; }
	}
}
