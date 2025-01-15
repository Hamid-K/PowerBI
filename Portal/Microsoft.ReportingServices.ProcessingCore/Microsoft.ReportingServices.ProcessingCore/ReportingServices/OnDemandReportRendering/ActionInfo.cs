using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020E RID: 526
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class ActionInfo
	{
		// Token: 0x060013EA RID: 5098 RVA: 0x000517A0 File Offset: 0x0004F9A0
		internal ActionInfo(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, IReportScope reportScope, Microsoft.ReportingServices.ReportIntermediateFormat.Action actionDef, IInstancePath instancePath, ReportElement reportElementOwner, ObjectType objectType, string objectName, IROMActionOwner romActionOwner)
		{
			this.m_renderingContext = renderingContext;
			this.m_reportScope = reportScope;
			this.m_actionDef = actionDef;
			this.m_isOldSnapshot = false;
			this.m_instancePath = instancePath;
			this.m_reportElementOwner = reportElementOwner;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_romActionOwner = romActionOwner;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000517F7 File Offset: 0x0004F9F7
		internal ActionInfo(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ActionInfo renderAction)
		{
			this.m_renderingContext = renderingContext;
			this.m_renderAction = renderAction;
			this.m_isOldSnapshot = true;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00051814 File Offset: 0x0004FA14
		public ActionCollection Actions
		{
			get
			{
				this.InitActions();
				return this.m_collection;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00051822 File Offset: 0x0004FA22
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0005182A File Offset: 0x0004FA2A
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00051832 File Offset: 0x0004FA32
		internal Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0005183A File Offset: 0x0004FA3A
		internal IInstancePath InstancePath
		{
			get
			{
				return this.m_instancePath;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00051842 File Offset: 0x0004FA42
		internal ReportElement ReportElementOwner
		{
			get
			{
				return this.m_reportElementOwner;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0005184A File Offset: 0x0004FA4A
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00051852 File Offset: 0x0004FA52
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0005185A File Offset: 0x0004FA5A
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00051862 File Offset: 0x0004FA62
		internal bool IsDynamic
		{
			get
			{
				return this.m_dynamic;
			}
			set
			{
				this.m_dynamic = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0005186B File Offset: 0x0004FA6B
		internal bool IsChartConstruction
		{
			get
			{
				return this.m_chartConstruction;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00051873 File Offset: 0x0004FA73
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0005187B File Offset: 0x0004FA7B
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action ActionDef
		{
			get
			{
				return this.m_actionDef;
			}
			set
			{
				this.m_actionDef = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00051884 File Offset: 0x0004FA84
		internal IROMActionOwner ROMActionOwner
		{
			get
			{
				return this.m_romActionOwner;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0005188C File Offset: 0x0004FA8C
		public Microsoft.ReportingServices.OnDemandReportRendering.Action CreateHyperlinkAction()
		{
			this.AssertValidCreateActionContext();
			this.InitActions();
			if (this.Actions.Count > 0)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem = new Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem();
			actionItem.HyperLinkURL = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			this.m_actionDef.ActionItems.Add(actionItem);
			return this.Actions.Add(this, actionItem);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000518F0 File Offset: 0x0004FAF0
		public Microsoft.ReportingServices.OnDemandReportRendering.Action CreateBookmarkLinkAction()
		{
			this.AssertValidCreateActionContext();
			this.InitActions();
			if (this.Actions.Count > 0)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem = new Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem();
			actionItem.BookmarkLink = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			this.m_actionDef.ActionItems.Add(actionItem);
			return this.Actions.Add(this, actionItem);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00051954 File Offset: 0x0004FB54
		public Microsoft.ReportingServices.OnDemandReportRendering.Action CreateDrillthroughAction()
		{
			this.AssertValidCreateActionContext();
			this.InitActions();
			if (this.Actions.Count > 0)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem = new Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem();
			actionItem.DrillthroughReportName = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			this.m_actionDef.ActionItems.Add(actionItem);
			return this.Actions.Add(this, actionItem);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x000519B8 File Offset: 0x0004FBB8
		private void AssertValidCreateActionContext()
		{
			if (!this.m_chartConstruction)
			{
				if (this.m_dynamic && this.m_reportElementOwner.CriGenerationPhase != ReportElement.CriGenerationPhases.Instance)
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackDynamicAction);
				}
				if (!this.m_dynamic && this.m_reportElementOwner.CriGenerationPhase != ReportElement.CriGenerationPhases.Definition)
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackDynamicAction);
				}
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00051A0F File Offset: 0x0004FC0F
		internal void Update(ActionInfo newActionInfo)
		{
			this.m_collection.Update(newActionInfo);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00051A1D File Offset: 0x0004FC1D
		internal virtual void SetNewContext()
		{
			if (this.m_collection != null)
			{
				this.m_collection.SetNewContext();
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00051A32 File Offset: 0x0004FC32
		internal bool ConstructActionDefinition()
		{
			if (this.m_collection == null || this.m_collection.Count == 0)
			{
				return false;
			}
			this.m_collection.ConstructActionDefinitions();
			return true;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00051A58 File Offset: 0x0004FC58
		private void InitActions()
		{
			if (this.m_collection == null)
			{
				if (this.IsOldSnapshot)
				{
					this.m_collection = new ActionCollection(this, this.m_renderAction.Actions);
					return;
				}
				this.m_collection = new ActionCollection(this, this.m_actionDef.ActionItems);
			}
		}

		// Token: 0x0400096E RID: 2414
		private ActionCollection m_collection;

		// Token: 0x0400096F RID: 2415
		private ActionInfo m_renderAction;

		// Token: 0x04000970 RID: 2416
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_actionDef;

		// Token: 0x04000971 RID: 2417
		private bool m_isOldSnapshot;

		// Token: 0x04000972 RID: 2418
		private IReportScope m_reportScope;

		// Token: 0x04000973 RID: 2419
		private IInstancePath m_instancePath;

		// Token: 0x04000974 RID: 2420
		private ReportElement m_reportElementOwner;

		// Token: 0x04000975 RID: 2421
		private ObjectType m_objectType;

		// Token: 0x04000976 RID: 2422
		private string m_objectName;

		// Token: 0x04000977 RID: 2423
		private bool m_dynamic;

		// Token: 0x04000978 RID: 2424
		protected bool m_chartConstruction;

		// Token: 0x04000979 RID: 2425
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_renderingContext;

		// Token: 0x0400097A RID: 2426
		private IROMActionOwner m_romActionOwner;
	}
}
