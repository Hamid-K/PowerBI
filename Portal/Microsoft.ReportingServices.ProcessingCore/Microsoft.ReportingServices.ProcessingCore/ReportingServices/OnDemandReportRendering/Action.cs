using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000212 RID: 530
	public sealed class Action
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x0005216C File Offset: 0x0005036C
		internal Action(ActionInfo owner, Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItemDef, int index)
		{
			this.m_owner = owner;
			this.m_actionItemDef = actionItemDef;
			this.m_index = index;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00052190 File Offset: 0x00050390
		internal Action(ActionInfo owner, Microsoft.ReportingServices.ReportRendering.Action renderAction)
		{
			this.m_owner = owner;
			this.m_renderAction = renderAction;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000521B0 File Offset: 0x000503B0
		public ReportStringProperty Label
		{
			get
			{
				if (this.m_label == null)
				{
					if (this.IsOldSnapshot)
					{
						if (this.m_renderAction.ActionDefinition.Label != null)
						{
							this.m_label = new ReportStringProperty(this.m_renderAction.ActionDefinition.Label);
						}
					}
					else if (this.m_actionItemDef.Label != null)
					{
						this.m_label = new ReportStringProperty(this.m_actionItemDef.Label);
					}
				}
				return this.m_label;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00052228 File Offset: 0x00050428
		public ReportStringProperty BookmarkLink
		{
			get
			{
				if (this.m_bookmark == null)
				{
					if (this.IsOldSnapshot)
					{
						if (this.m_renderAction.ActionDefinition.BookmarkLink != null)
						{
							this.m_bookmark = new ReportStringProperty(this.m_renderAction.ActionDefinition.BookmarkLink);
						}
					}
					else if (this.m_actionItemDef.BookmarkLink != null)
					{
						this.m_bookmark = new ReportStringProperty(this.m_actionItemDef.BookmarkLink);
					}
				}
				return this.m_bookmark;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000522A0 File Offset: 0x000504A0
		public ReportUrlProperty Hyperlink
		{
			get
			{
				if (this.m_hyperlink == null)
				{
					if (this.IsOldSnapshot)
					{
						if (this.m_renderAction.ActionDefinition.HyperLinkURL != null)
						{
							this.m_hyperlink = new ReportUrlProperty(this.m_renderAction.ActionDefinition.HyperLinkURL.IsExpression, this.m_renderAction.ActionDefinition.HyperLinkURL.OriginalText, this.m_renderAction.ActionDefinition.HyperLinkURL.IsExpression ? null : new ReportUrl(this.m_renderAction.HyperLinkURL));
						}
					}
					else if (this.m_actionItemDef.HyperLinkURL != null)
					{
						ReportUrl reportUrl = null;
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo hyperLinkURL = this.m_actionItemDef.HyperLinkURL;
						if (!hyperLinkURL.IsExpression)
						{
							reportUrl = ReportUrl.BuildHyperlinkUrl(this.m_owner.RenderingContext, this.m_owner.ObjectType, this.m_owner.ObjectName, "Hyperlink", this.m_owner.RenderingContext.OdpContext.ReportContext, hyperLinkURL.StringValue);
						}
						this.m_hyperlink = new ReportUrlProperty(hyperLinkURL.IsExpression, hyperLinkURL.OriginalText, reportUrl);
					}
				}
				return this.m_hyperlink;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000523C4 File Offset: 0x000505C4
		public ActionDrillthrough Drillthrough
		{
			get
			{
				if (this.m_drillthrough == null)
				{
					if (this.IsOldSnapshot)
					{
						if (this.m_renderAction.ActionDefinition.DrillthroughReportName != null)
						{
							this.m_drillthrough = new ActionDrillthrough(this.m_owner, this.m_renderAction);
						}
					}
					else if (this.m_actionItemDef.DrillthroughReportName != null)
					{
						this.m_drillthrough = new ActionDrillthrough(this.m_owner, this.m_actionItemDef, this.m_index);
					}
				}
				return this.m_drillthrough;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0005243C File Offset: 0x0005063C
		private bool IsOldSnapshot
		{
			get
			{
				return this.m_owner.IsOldSnapshot;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0005244C File Offset: 0x0005064C
		public ActionInstance Instance
		{
			get
			{
				if (this.m_owner.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsOldSnapshot)
					{
						this.m_instance = new ActionInstance(this.m_renderAction);
					}
					else
					{
						this.m_instance = new ActionInstance(this.m_owner.ReportScope, this);
					}
				}
				Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = this.m_owner.ReportElementOwner as Microsoft.ReportingServices.OnDemandReportRendering.ReportItem;
				if (reportItem != null)
				{
					reportItem.CriEvaluateInstance();
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000524C7 File Offset: 0x000506C7
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem ActionItemDef
		{
			get
			{
				return this.m_actionItemDef;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000524CF File Offset: 0x000506CF
		internal ActionInfo Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000524D7 File Offset: 0x000506D7
		internal void Update(Microsoft.ReportingServices.ReportRendering.Action newAction)
		{
			if (this.m_instance != null)
			{
				this.m_instance.Update(newAction);
			}
			if (this.m_drillthrough != null)
			{
				this.m_drillthrough.Update(newAction);
			}
			if (newAction != null)
			{
				this.m_renderAction = newAction;
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0005250B File Offset: 0x0005070B
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_drillthrough != null)
			{
				this.m_drillthrough.SetNewContext();
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00052534 File Offset: 0x00050734
		internal void ConstructActionDefinition()
		{
			ActionInstance instance = this.Instance;
			Global.Tracer.Assert(instance != null);
			if (instance.Label != null)
			{
				this.m_actionItemDef.Label = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.Label);
			}
			else
			{
				this.m_actionItemDef.Label = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_label = null;
			if (this.BookmarkLink != null)
			{
				if (instance.BookmarkLink != null)
				{
					this.m_actionItemDef.BookmarkLink = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.BookmarkLink);
				}
				else
				{
					this.m_actionItemDef.BookmarkLink = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
				}
				this.m_bookmark = null;
			}
			if (this.Hyperlink != null)
			{
				if (instance.HyperlinkText != null)
				{
					this.m_actionItemDef.HyperLinkURL = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.HyperlinkText);
				}
				else
				{
					this.m_actionItemDef.HyperLinkURL = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
				}
				this.m_hyperlink = null;
			}
			if (this.Drillthrough != null)
			{
				this.Drillthrough.ConstructDrillthoughDefinition();
			}
		}

		// Token: 0x0400097F RID: 2431
		private ReportStringProperty m_label;

		// Token: 0x04000980 RID: 2432
		private ReportStringProperty m_bookmark;

		// Token: 0x04000981 RID: 2433
		private ReportUrlProperty m_hyperlink;

		// Token: 0x04000982 RID: 2434
		private ActionInstance m_instance;

		// Token: 0x04000983 RID: 2435
		private ActionDrillthrough m_drillthrough;

		// Token: 0x04000984 RID: 2436
		private Microsoft.ReportingServices.ReportRendering.Action m_renderAction;

		// Token: 0x04000985 RID: 2437
		private ActionInfo m_owner;

		// Token: 0x04000986 RID: 2438
		private Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem m_actionItemDef;

		// Token: 0x04000987 RID: 2439
		private int m_index = -1;
	}
}
