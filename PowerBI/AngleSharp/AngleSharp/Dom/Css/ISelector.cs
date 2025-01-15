using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000337 RID: 823
	public interface ISelector : ICssNode, IStyleFormattable
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001917 RID: 6423
		Priority Specifity { get; }

		// Token: 0x06001918 RID: 6424
		bool Match(IElement element);

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001919 RID: 6425
		string Text { get; }
	}
}
