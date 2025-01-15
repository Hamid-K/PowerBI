using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B34 RID: 2868
	internal interface IGoogleAnalyticsService
	{
		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06004F83 RID: 20355
		IEngineHost Host { get; }

		// Token: 0x06004F84 RID: 20356
		void DownloadMetadata(GoogleAnalyticsProperty property, out IList<GoogleAnalyticsCubeObject> measures, out IList<GoogleAnalyticsCubeObject> dimensions);

		// Token: 0x06004F85 RID: 20357
		List<Value> DownloadList(UriBuilder uriBuilder);

		// Token: 0x06004F86 RID: 20358
		DateTime GetFixedNow();

		// Token: 0x06004F87 RID: 20359
		Value GetReport(string viewId, GoogleAnalyticsQueryExpression compiledExpression);

		// Token: 0x06004F88 RID: 20360
		IList<GoogleAnalyticsAccount> GetAccounts();
	}
}
