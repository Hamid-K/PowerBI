using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000E RID: 14
	public sealed class ActionCollection
	{
		// Token: 0x06000313 RID: 787 RVA: 0x00007626 File Offset: 0x00005826
		public ActionCollection()
		{
			this.m_members = new ActionCollectionProcessing();
			Global.Tracer.Assert(this.IsCustomControl);
			this.Processing.m_actions = new ArrayList();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000765C File Offset: 0x0000585C
		internal ActionCollection(ActionItemList actionItemList, ActionItemInstanceList actionItemInstanceList, string ownerUniqueName, Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext)
		{
			this.m_members = new ActionCollectionRendering();
			Global.Tracer.Assert(!this.IsCustomControl);
			this.Rendering.m_actionList = actionItemList;
			this.Rendering.m_actionInstanceList = actionItemInstanceList;
			this.Rendering.m_renderingContext = renderingContext;
			this.Rendering.m_ownerUniqueName = ownerUniqueName;
		}

		// Token: 0x170002EC RID: 748
		public Microsoft.ReportingServices.ReportRendering.Action this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				Microsoft.ReportingServices.ReportRendering.Action action = null;
				if (this.IsCustomControl)
				{
					action = this.Processing.m_actions[index] as Microsoft.ReportingServices.ReportRendering.Action;
				}
				else
				{
					if (this.Rendering.m_actions != null)
					{
						action = this.Rendering.m_actions[index];
					}
					if (action == null)
					{
						ActionItem actionItem = this.Rendering.m_actionList[index];
						ActionItemInstance actionItemInstance = null;
						if (this.Rendering.m_actionInstanceList != null && actionItem.ComputedIndex >= 0)
						{
							actionItemInstance = this.Rendering.m_actionInstanceList[actionItem.ComputedIndex];
						}
						string text = this.Rendering.m_ownerUniqueName + ":" + index.ToString(CultureInfo.InvariantCulture);
						action = new Microsoft.ReportingServices.ReportRendering.Action(actionItem, actionItemInstance, text, this.Rendering.m_renderingContext);
						if (this.Rendering.m_renderingContext.CacheState)
						{
							if (this.Rendering.m_actions == null)
							{
								this.Rendering.m_actions = new Microsoft.ReportingServices.ReportRendering.Action[this.Count];
							}
							this.Rendering.m_actions[index] = action;
						}
					}
				}
				return action;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000780D File Offset: 0x00005A0D
		public int Count
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.m_actions.Count;
				}
				return this.Rendering.m_actionList.Count;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00007838 File Offset: 0x00005A38
		public void Add(Microsoft.ReportingServices.ReportRendering.Action action)
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (action == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "action" });
			}
			int count = this.Processing.m_actions.Count;
			if (2 <= count)
			{
				if (action.Label == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidActionLabel);
				}
			}
			else if (1 == count)
			{
				if (action.Label == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidActionLabel);
				}
				if (((Microsoft.ReportingServices.ReportRendering.Action)this.Processing.m_actions[0]).Label == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidActionLabel);
				}
			}
			this.Processing.m_actions.Add(action);
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000078ED File Offset: 0x00005AED
		private bool IsCustomControl
		{
			get
			{
				return this.m_members.IsCustomControl;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000078FC File Offset: 0x00005AFC
		private ActionCollectionRendering Rendering
		{
			get
			{
				Global.Tracer.Assert(!this.m_members.IsCustomControl);
				ActionCollectionRendering actionCollectionRendering = this.m_members as ActionCollectionRendering;
				if (actionCollectionRendering == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionCollectionRendering;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000793C File Offset: 0x00005B3C
		private ActionCollectionProcessing Processing
		{
			get
			{
				Global.Tracer.Assert(this.m_members.IsCustomControl);
				ActionCollectionProcessing actionCollectionProcessing = this.m_members as ActionCollectionProcessing;
				if (actionCollectionProcessing == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionCollectionProcessing;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000797C File Offset: 0x00005B7C
		internal ActionCollection DeepClone()
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			ActionCollection actionCollection = new ActionCollection();
			Global.Tracer.Assert(this.m_members != null && this.m_members is ActionCollectionProcessing);
			actionCollection.m_members = this.Processing.DeepClone();
			return actionCollection;
		}

		// Token: 0x0400002A RID: 42
		private MemberBase m_members;
	}
}
