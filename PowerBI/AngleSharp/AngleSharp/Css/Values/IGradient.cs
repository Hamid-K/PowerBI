using System;
using System.Collections.Generic;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000118 RID: 280
	public interface IGradient : IImageSource
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600090C RID: 2316
		IEnumerable<GradientStop> Stops { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600090D RID: 2317
		bool IsRepeating { get; }
	}
}
