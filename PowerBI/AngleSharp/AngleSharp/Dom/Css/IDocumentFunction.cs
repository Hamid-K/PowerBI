using System;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000331 RID: 817
	public interface IDocumentFunction : ICssNode, IStyleFormattable
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001900 RID: 6400
		string Name { get; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001901 RID: 6401
		string Data { get; }

		// Token: 0x06001902 RID: 6402
		bool Matches(Url url);
	}
}
