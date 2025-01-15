using System;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000215 RID: 533
	public sealed class ActionDrillthroughInstance : BaseInstance
	{
		// Token: 0x0600144E RID: 5198 RVA: 0x000533E5 File Offset: 0x000515E5
		internal ActionDrillthroughInstance(IReportScope reportScope, ActionDrillthrough actionDef, int index)
			: base(reportScope)
		{
			this.m_isOldSnapshot = false;
			this.m_actionDef = actionDef;
			this.m_index = index;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005340A File Offset: 0x0005160A
		internal ActionDrillthroughInstance(Microsoft.ReportingServices.ReportRendering.Action renderAction)
			: base(null)
		{
			this.m_isOldSnapshot = true;
			this.m_renderAction = renderAction;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00053428 File Offset: 0x00051628
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00053524 File Offset: 0x00051724
		public string ReportName
		{
			get
			{
				if (this.m_reportName == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderAction != null)
						{
							this.m_reportName = this.m_renderAction.DrillthroughPath;
						}
					}
					else
					{
						Global.Tracer.Assert(this.m_actionDef.ReportName != null, "(m_actionDef.ReportName != null)");
						if (!this.m_actionDef.ReportName.IsExpression)
						{
							this.m_reportName = this.m_actionDef.ReportName.Value;
						}
						else if (this.m_actionDef.Owner.ReportElementOwner == null || this.m_actionDef.Owner.ReportElementOwner.CriOwner == null)
						{
							ActionInfo owner = this.m_actionDef.Owner;
							this.m_reportName = this.m_actionDef.ActionItemDef.EvaluateDrillthroughReportName(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, owner.ObjectType, owner.ObjectName);
						}
					}
				}
				return this.m_reportName;
			}
			set
			{
				ReportElement reportElementOwner = this.m_actionDef.Owner.ReportElementOwner;
				Global.Tracer.Assert(this.m_actionDef.ReportName != null, "(m_actionDef.ReportName != null)");
				if (!this.m_actionDef.Owner.IsChartConstruction && (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_actionDef.ReportName.IsExpression)))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_reportName = value;
				this.m_drillthroughUrl = null;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000535B0 File Offset: 0x000517B0
		public string DrillthroughID
		{
			get
			{
				if (this.m_drillthroughID == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderAction != null)
						{
							this.m_drillthroughID = this.m_renderAction.DrillthroughID;
						}
					}
					else if (this.ReportName != null)
					{
						this.m_drillthroughID = ((this.m_actionDef.Owner.ROMActionOwner != null) ? this.m_actionDef.Owner.ROMActionOwner.UniqueName : this.m_actionDef.Owner.InstancePath.UniqueName);
						this.m_drillthroughID = this.m_drillthroughID + ":" + this.m_index.ToString(CultureInfo.InvariantCulture);
					}
				}
				return this.m_drillthroughID;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00053668 File Offset: 0x00051868
		public string DrillthroughUrl
		{
			get
			{
				if (!this.m_drillthroughUrlEvaluated)
				{
					this.m_drillthroughUrlEvaluated = true;
					if (this.ReportName != null)
					{
						if (this.m_isOldSnapshot)
						{
							if (this.m_renderAction == null)
							{
								goto IL_00BC;
							}
							try
							{
								ReportUrlBuilder urlBuilder = this.m_renderAction.DrillthroughReport.GetUrlBuilder(null, true);
								urlBuilder.AddParameters(this.m_renderAction.DrillthroughParameters, UrlParameterType.ReportParameter);
								this.m_drillthroughUrl = urlBuilder.ToUri().AbsoluteUri;
								goto IL_00BC;
							}
							catch (ItemNotFoundException)
							{
								this.m_drillthroughUrl = null;
								goto IL_00BC;
							}
						}
						try
						{
							NameValueCollection nameValueCollection = null;
							if (this.m_actionDef.Parameters != null)
							{
								nameValueCollection = this.m_actionDef.Parameters.ToNameValueCollection;
							}
							this.m_drillthroughUrl = ReportUrl.BuildDrillthroughUrl(this.m_actionDef.PathResolutionContext, this.ReportName, nameValueCollection);
						}
						catch (ItemNotFoundException)
						{
							this.m_drillthroughUrl = null;
						}
					}
				}
				IL_00BC:
				return this.m_drillthroughUrl;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00053754 File Offset: 0x00051954
		internal void Update(Microsoft.ReportingServices.ReportRendering.Action newAction)
		{
			this.m_renderAction = newAction;
			this.ResetInstanceCache();
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00053763 File Offset: 0x00051963
		protected override void ResetInstanceCache()
		{
			this.m_reportName = null;
			this.m_drillthroughID = null;
			this.m_drillthroughUrl = null;
			this.m_drillthroughUrlEvaluated = false;
		}

		// Token: 0x04000997 RID: 2455
		private bool m_isOldSnapshot;

		// Token: 0x04000998 RID: 2456
		private Microsoft.ReportingServices.ReportRendering.Action m_renderAction;

		// Token: 0x04000999 RID: 2457
		private string m_reportName;

		// Token: 0x0400099A RID: 2458
		private string m_drillthroughID;

		// Token: 0x0400099B RID: 2459
		private string m_drillthroughUrl;

		// Token: 0x0400099C RID: 2460
		private bool m_drillthroughUrlEvaluated;

		// Token: 0x0400099D RID: 2461
		private ActionDrillthrough m_actionDef;

		// Token: 0x0400099E RID: 2462
		private int m_index = -1;
	}
}
