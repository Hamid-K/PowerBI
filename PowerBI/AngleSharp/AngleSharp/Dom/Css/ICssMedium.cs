using System;
using System.Collections.Generic;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000324 RID: 804
	public interface ICssMedium : ICssNode, IStyleFormattable
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001712 RID: 5906
		string Type { get; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001713 RID: 5907
		bool IsExclusive { get; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001714 RID: 5908
		bool IsInverse { get; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001715 RID: 5909
		string Constraints { get; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001716 RID: 5910
		IEnumerable<IMediaFeature> Features { get; }
	}
}
