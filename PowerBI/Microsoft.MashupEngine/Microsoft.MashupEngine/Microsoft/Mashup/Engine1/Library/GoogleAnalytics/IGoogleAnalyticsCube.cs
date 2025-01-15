using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF9 RID: 2809
	internal interface IGoogleAnalyticsCube
	{
		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06004DEF RID: 19951
		string Name { get; }

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06004DF0 RID: 19952
		DateTime Created { get; }

		// Token: 0x06004DF1 RID: 19953
		GoogleAnalyticsCubeObject GetObject(string id);

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06004DF2 RID: 19954
		string ViewId { get; }

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06004DF3 RID: 19955
		DateTime FixedNow { get; }

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06004DF4 RID: 19956
		Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> ResultEnumeratorFactory { get; }
	}
}
