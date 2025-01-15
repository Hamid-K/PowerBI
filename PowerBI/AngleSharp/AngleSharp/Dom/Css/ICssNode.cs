using System;
using System.Collections.Generic;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000326 RID: 806
	public interface ICssNode : IStyleFormattable
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600171B RID: 5915
		IEnumerable<ICssNode> Children { get; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600171C RID: 5916
		TextView SourceCode { get; }
	}
}
