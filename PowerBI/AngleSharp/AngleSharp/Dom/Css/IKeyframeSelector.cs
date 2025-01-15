using System;
using System.Collections.Generic;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000333 RID: 819
	public interface IKeyframeSelector : ICssNode, IStyleFormattable
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001904 RID: 6404
		IEnumerable<Percent> Stops { get; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001905 RID: 6405
		string Text { get; }
	}
}
