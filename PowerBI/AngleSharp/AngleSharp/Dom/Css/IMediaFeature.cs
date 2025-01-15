using System;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000334 RID: 820
	public interface IMediaFeature : ICssNode, IStyleFormattable
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001906 RID: 6406
		string Name { get; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001907 RID: 6407
		bool IsMinimum { get; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001908 RID: 6408
		bool IsMaximum { get; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001909 RID: 6409
		string Value { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600190A RID: 6410
		bool HasValue { get; }
	}
}
