using System;
using Microsoft.ReportingServices.DataShapeDefinition;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000652 RID: 1618
	internal sealed class DataShapePublishingContext : PublishingContextBase
	{
		// Token: 0x060057C6 RID: 22470 RVA: 0x0016FF24 File Offset: 0x0016E124
		internal DataShapePublishingContext(DataShapeDefinition dataShapeDefinition, ICatalogItemContext catalogContext, IConfiguration configuration, DataShapeProcessingDataSourceContext dataSourceContext)
			: base(PublishingContextKind.DataShape, catalogContext, null, null, false, ReportProcessingFlags.OnDemandEngine, null, null, null, null, null, null, configuration, null, false, false, true, false)
		{
			this.m_dataShapeDefinition = dataShapeDefinition;
			this.m_dataSourceContext = dataSourceContext;
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x0016FF58 File Offset: 0x0016E158
		internal DataShapeDefinition DataShapeDefinition
		{
			get
			{
				return this.m_dataShapeDefinition;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x060057C8 RID: 22472 RVA: 0x0016FF60 File Offset: 0x0016E160
		internal DataShapeProcessingDataSourceContext DataSourceContext
		{
			get
			{
				return this.m_dataSourceContext;
			}
		}

		// Token: 0x04002E74 RID: 11892
		private readonly DataShapeDefinition m_dataShapeDefinition;

		// Token: 0x04002E75 RID: 11893
		private readonly DataShapeProcessingDataSourceContext m_dataSourceContext;
	}
}
