using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009D RID: 157
	internal sealed class DataShapeResult : IDefinitionPath, IReportScope
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x000272D0 File Offset: 0x000254D0
		internal DataShapeResult(Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef, RenderingContext renderingContext)
		{
			this.m_rifReport = reportDef;
			this.m_renderingContext = renderingContext;
			Global.Tracer.Assert(!this.m_renderingContext.InstanceAccessDisallowed, "Instance access must be allowed for data shape rendering.");
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00027303 File Offset: 0x00025503
		public string DefinitionPath
		{
			get
			{
				return "xA";
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0002730A File Offset: 0x0002550A
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002730D File Offset: 0x0002550D
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00027315 File Offset: 0x00025515
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_rifReport;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0002731D File Offset: 0x0002551D
		public DataShapeCollection DataShapes
		{
			get
			{
				if (this.m_dataShapes == null)
				{
					this.m_dataShapes = new DataShapeCollection(this.RifReportDefinition.DataShapes, this, this.m_renderingContext);
				}
				return this.m_dataShapes;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0002734A File Offset: 0x0002554A
		internal DataShapeResultInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new DataShapeResultInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027366 File Offset: 0x00025566
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report RifReportDefinition
		{
			get
			{
				return this.m_rifReport;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002736E File Offset: 0x0002556E
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00027376 File Offset: 0x00025576
		internal void SetNewContext()
		{
			if (this.m_dataShapes != null)
			{
				this.m_dataShapes.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000271 RID: 625
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.Report m_rifReport;

		// Token: 0x04000272 RID: 626
		private readonly RenderingContext m_renderingContext;

		// Token: 0x04000273 RID: 627
		private DataShapeResultInstance m_instance;

		// Token: 0x04000274 RID: 628
		private DataShapeCollection m_dataShapes;
	}
}
