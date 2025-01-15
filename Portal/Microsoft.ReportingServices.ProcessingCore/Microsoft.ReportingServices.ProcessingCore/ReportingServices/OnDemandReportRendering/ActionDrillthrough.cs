using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000213 RID: 531
	public sealed class ActionDrillthrough
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x0005261E File Offset: 0x0005081E
		internal ActionDrillthrough(ActionInfo owner, Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItemDef, int index)
		{
			this.m_owner = owner;
			this.m_actionItemDef = actionItemDef;
			this.m_index = index;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00052642 File Offset: 0x00050842
		internal ActionDrillthrough(ActionInfo owner, Microsoft.ReportingServices.ReportRendering.Action renderAction)
		{
			this.m_owner = owner;
			this.m_renderAction = renderAction;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00052660 File Offset: 0x00050860
		public ReportStringProperty ReportName
		{
			get
			{
				if (this.m_reportName == null)
				{
					if (this.IsOldSnapshot)
					{
						this.m_reportName = new ReportStringProperty(this.m_renderAction.ActionDefinition.DrillthroughReportName);
					}
					else
					{
						this.m_reportName = new ReportStringProperty(this.m_actionItemDef.DrillthroughReportName);
					}
				}
				return this.m_reportName;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000526B8 File Offset: 0x000508B8
		public ParameterCollection Parameters
		{
			get
			{
				if (this.m_parameters == null)
				{
					if (this.IsOldSnapshot)
					{
						NameValueCollection drillthroughParameters = this.m_renderAction.DrillthroughParameters;
						if (drillthroughParameters != null)
						{
							this.m_parameters = new ParameterCollection(this, drillthroughParameters, this.m_renderAction.DrillthroughParameterNameObjectCollection, this.m_renderAction.DrillthroughParameterValueList, this.m_renderAction.ActionInstance);
						}
					}
					else if (this.m_actionItemDef.DrillthroughParameters != null)
					{
						this.m_parameters = new ParameterCollection(this, this.m_actionItemDef.DrillthroughParameters);
					}
				}
				return this.m_parameters;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0005273E File Offset: 0x0005093E
		private bool IsOldSnapshot
		{
			get
			{
				return this.m_owner.IsOldSnapshot;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005274C File Offset: 0x0005094C
		public ActionDrillthroughInstance Instance
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
						this.m_instance = new ActionDrillthroughInstance(this.m_renderAction);
					}
					else
					{
						this.m_instance = new ActionDrillthroughInstance(this.m_owner.ReportScope, this, this.m_index);
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x000527CD File Offset: 0x000509CD
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem ActionItemDef
		{
			get
			{
				return this.m_actionItemDef;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x000527D5 File Offset: 0x000509D5
		internal ActionInfo Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x000527DD File Offset: 0x000509DD
		internal ICatalogItemContext PathResolutionContext
		{
			get
			{
				return this.m_owner.RenderingContext.OdpContext.TopLevelContext.ReportContext;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000527FC File Offset: 0x000509FC
		public void RegisterDrillthroughAction()
		{
			string drillthroughID = this.Instance.DrillthroughID;
			if (drillthroughID != null)
			{
				this.m_owner.RenderingContext.AddDrillthroughAction(drillthroughID, this.Instance.ReportName, (this.Parameters != null) ? this.Parameters.ParametersNameObjectCollection : null);
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0005284C File Offset: 0x00050A4C
		public Parameter CreateParameter(string name)
		{
			if (!this.m_owner.IsChartConstruction)
			{
				if (this.m_owner.IsDynamic && this.m_owner.ReportElementOwner.CriGenerationPhase != ReportElement.CriGenerationPhases.Instance)
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackDynamicAction);
				}
				if (!this.m_owner.IsDynamic && this.m_owner.ReportElementOwner.CriGenerationPhase != ReportElement.CriGenerationPhases.Definition)
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackNonDynamicAction);
				}
			}
			if (this.Parameters == null)
			{
				this.m_actionItemDef.DrillthroughParameters = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue>();
				Global.Tracer.Assert(this.Parameters != null, "(Parameters != null)");
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue parameterValue = new Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue();
			parameterValue.Name = name;
			this.m_actionItemDef.DrillthroughParameters.Add(parameterValue);
			if (!this.m_owner.IsChartConstruction && this.m_owner.ReportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance)
			{
				parameterValue.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			return this.Parameters.Add(this, parameterValue);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00052944 File Offset: 0x00050B44
		internal void Update(Microsoft.ReportingServices.ReportRendering.Action newAction)
		{
			if (this.m_instance != null)
			{
				this.m_instance.Update(newAction);
			}
			if (newAction != null)
			{
				this.m_renderAction = newAction;
			}
			if (this.m_parameters != null)
			{
				this.m_parameters.Update(this.m_renderAction.DrillthroughParameters, this.m_renderAction.DrillthroughParameterNameObjectCollection, this.m_renderAction.ActionInstance);
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000529A3 File Offset: 0x00050BA3
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_parameters != null)
			{
				this.m_parameters.SetNewContext();
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000529CC File Offset: 0x00050BCC
		internal void ConstructDrillthoughDefinition()
		{
			ActionDrillthroughInstance instance = this.Instance;
			Global.Tracer.Assert(instance != null, "(instance != null)");
			if (instance.ReportName != null)
			{
				this.m_actionItemDef.DrillthroughReportName = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.ReportName);
			}
			else
			{
				this.m_actionItemDef.DrillthroughReportName = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_reportName = null;
			if (this.m_parameters != null)
			{
				this.m_parameters.ConstructParameterDefinitions();
			}
		}

		// Token: 0x04000988 RID: 2440
		private ReportStringProperty m_reportName;

		// Token: 0x04000989 RID: 2441
		private ParameterCollection m_parameters;

		// Token: 0x0400098A RID: 2442
		private ActionDrillthroughInstance m_instance;

		// Token: 0x0400098B RID: 2443
		private Microsoft.ReportingServices.ReportRendering.Action m_renderAction;

		// Token: 0x0400098C RID: 2444
		private Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem m_actionItemDef;

		// Token: 0x0400098D RID: 2445
		private ActionInfo m_owner;

		// Token: 0x0400098E RID: 2446
		private int m_index = -1;
	}
}
