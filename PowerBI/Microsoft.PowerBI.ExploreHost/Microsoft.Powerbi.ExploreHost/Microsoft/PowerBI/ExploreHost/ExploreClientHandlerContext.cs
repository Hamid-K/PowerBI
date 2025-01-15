using System;
using System.Collections.Generic;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Analytics;
using Microsoft.PowerBI.ExploreServiceCommon.Interfaces;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.ReportingServices.Library;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000029 RID: 41
	internal sealed class ExploreClientHandlerContext
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00004340 File Offset: 0x00002540
		internal ExploreClientHandlerContext(IPowerViewHandler powerviewHandler, IDataShapeEngine dataShapeEngine, FeatureSwitches featureSwitches, IQueryCancellationManager cancellationManager)
		{
			this.PowerViewHandler = powerviewHandler;
			this.DataShapeEngine = dataShapeEngine;
			this.FeatureSwitches = featureSwitches;
			this.RunningQueriesCancellationManager = cancellationManager;
			this.FeatureSwitchProvider = DataShapingFeatureSwitchProvider.Create(featureSwitches);
			this.AnalyticsFeatureSwitchProvider = Microsoft.PowerBI.ReportingServicesHost.AnalyticsFeatureSwitchProvider.Create(featureSwitches);
			this.ScriptHandlers = new Dictionary<string, IScriptHandler>();
			this.ScriptEditors = new Dictionary<string, IScriptEditor>();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000439E File Offset: 0x0000259E
		public FeatureSwitches FeatureSwitches { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000043A6 File Offset: 0x000025A6
		public IPowerViewHandler PowerViewHandler { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000043AE File Offset: 0x000025AE
		public IDataShapeEngine DataShapeEngine { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000043B6 File Offset: 0x000025B6
		public Dictionary<string, IScriptHandler> ScriptHandlers { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000043BE File Offset: 0x000025BE
		public Dictionary<string, IScriptEditor> ScriptEditors { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000043C6 File Offset: 0x000025C6
		public IQueryCancellationManager RunningQueriesCancellationManager { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000043CE File Offset: 0x000025CE
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000043D6 File Offset: 0x000025D6
		public IAnalyticsFeatureSwitchProvider AnalyticsFeatureSwitchProvider { get; }
	}
}
