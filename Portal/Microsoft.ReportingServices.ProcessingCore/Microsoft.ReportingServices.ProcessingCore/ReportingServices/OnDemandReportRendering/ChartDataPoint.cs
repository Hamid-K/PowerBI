using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022A RID: 554
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ChartDataPoint : IReportScope, IDataRegionCell, IROMStyleDefinitionContainer
	{
		// Token: 0x06001506 RID: 5382 RVA: 0x00055378 File Offset: 0x00053578
		internal ChartDataPoint(Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, int rowIndex, int colIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			this.m_dataPointValues = null;
			this.m_actionInfo = null;
			this.m_customProperties = null;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06001507 RID: 5383
		public abstract DataElementOutputTypes DataElementOutput { get; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06001508 RID: 5384
		public abstract string DataElementName { get; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06001509 RID: 5385
		public abstract ChartDataPointValues DataPointValues { get; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600150A RID: 5386
		public abstract ChartItemInLegend ItemInLegend { get; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600150B RID: 5387
		public abstract ActionInfo ActionInfo { get; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600150C RID: 5388
		public abstract CustomPropertyCollection CustomProperties { get; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600150D RID: 5389
		public abstract Microsoft.ReportingServices.OnDemandReportRendering.Style Style { get; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600150E RID: 5390
		public abstract ChartMarker Marker { get; }

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600150F RID: 5391
		public abstract Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel DataLabel { get; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06001510 RID: 5392
		public abstract ReportVariantProperty AxisLabel { get; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06001511 RID: 5393
		public abstract ReportStringProperty ToolTip { get; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06001512 RID: 5394
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint DataPointDef { get; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06001513 RID: 5395
		internal abstract Microsoft.ReportingServices.ReportRendering.ChartDataPoint RenderItem { get; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06001514 RID: 5396
		internal abstract Microsoft.ReportingServices.ReportProcessing.ChartDataPoint RenderDataPointDef { get; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000553AA File Offset: 0x000535AA
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000553B2 File Offset: 0x000535B2
		public ChartDataPointInstance Instance
		{
			get
			{
				if (this.m_owner.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartDataPointInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000553E2 File Offset: 0x000535E2
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x000553EA File Offset: 0x000535EA
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.RIFReportScope;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000553F2 File Offset: 0x000535F2
		internal virtual IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000553F5 File Offset: 0x000535F5
		void IDataRegionCell.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00055400 File Offset: 0x00053600
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			this.m_customPropertiesReady = false;
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_marker != null)
			{
				this.m_marker.SetNewContext();
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.SetNewContext();
			}
			if (this.m_dataPointValues != null)
			{
				this.m_dataPointValues.SetNewContext();
			}
		}

		// Token: 0x040009E4 RID: 2532
		protected Microsoft.ReportingServices.OnDemandReportRendering.Chart m_owner;

		// Token: 0x040009E5 RID: 2533
		protected int m_rowIndex;

		// Token: 0x040009E6 RID: 2534
		protected int m_columnIndex;

		// Token: 0x040009E7 RID: 2535
		protected ChartDataPointValues m_dataPointValues;

		// Token: 0x040009E8 RID: 2536
		protected ActionInfo m_actionInfo;

		// Token: 0x040009E9 RID: 2537
		protected CustomPropertyCollection m_customProperties;

		// Token: 0x040009EA RID: 2538
		protected bool m_customPropertiesReady;

		// Token: 0x040009EB RID: 2539
		protected ChartDataPointInstance m_instance;

		// Token: 0x040009EC RID: 2540
		protected Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x040009ED RID: 2541
		protected ChartMarker m_marker;

		// Token: 0x040009EE RID: 2542
		protected Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel m_dataLabel;
	}
}
