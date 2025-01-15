using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000211 RID: 529
	public sealed class ActionCollection : ReportElementCollectionBase<Microsoft.ReportingServices.OnDemandReportRendering.Action>
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x00051F64 File Offset: 0x00050164
		internal ActionCollection(ActionInfo actionInfo, List<Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem> actions)
		{
			int count = actions.Count;
			this.m_list = new List<Microsoft.ReportingServices.OnDemandReportRendering.Action>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Microsoft.ReportingServices.OnDemandReportRendering.Action(actionInfo, actions[i], i));
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00051FB0 File Offset: 0x000501B0
		internal ActionCollection(ActionInfo actionInfo, ActionCollection actions)
		{
			int count = actions.Count;
			this.m_list = new List<Microsoft.ReportingServices.OnDemandReportRendering.Action>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Microsoft.ReportingServices.OnDemandReportRendering.Action(actionInfo, actions[i]));
			}
		}

		// Token: 0x17000AB5 RID: 2741
		public override Microsoft.ReportingServices.OnDemandReportRendering.Action this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_list[index];
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00052053 File Offset: 0x00050253
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00052060 File Offset: 0x00050260
		internal Microsoft.ReportingServices.OnDemandReportRendering.Action Add(ActionInfo owner, Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem)
		{
			Microsoft.ReportingServices.OnDemandReportRendering.Action action = new Microsoft.ReportingServices.OnDemandReportRendering.Action(owner, actionItem, this.m_list.Count);
			this.m_list.Add(action);
			return action;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00052090 File Offset: 0x00050290
		internal void Update(ActionInfo newCollection)
		{
			int count = this.m_list.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_list[i].Update((newCollection != null && newCollection.Actions != null) ? newCollection.Actions[i] : null);
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000520E0 File Offset: 0x000502E0
		internal void SetNewContext()
		{
			if (this.m_list != null)
			{
				for (int i = 0; i < this.m_list.Count; i++)
				{
					this.m_list[i].SetNewContext();
				}
			}
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0005211C File Offset: 0x0005031C
		internal void ConstructActionDefinitions()
		{
			foreach (Microsoft.ReportingServices.OnDemandReportRendering.Action action in this.m_list)
			{
				action.ConstructActionDefinition();
			}
		}

		// Token: 0x0400097E RID: 2430
		private List<Microsoft.ReportingServices.OnDemandReportRendering.Action> m_list;
	}
}
