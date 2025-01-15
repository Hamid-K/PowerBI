using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000A RID: 10
	public sealed class ActionInfo
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00006C16 File Offset: 0x00004E16
		public ActionInfo()
		{
			this.m_members = new ActionInfoProcessing();
			Global.Tracer.Assert(this.m_members.IsCustomControl);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006C40 File Offset: 0x00004E40
		internal ActionInfo(Microsoft.ReportingServices.ReportProcessing.Action actionDef, ActionInstance actionInstance, string ownerUniqueName, RenderingContext renderingContext)
		{
			this.m_members = new ActionInfoRendering();
			Global.Tracer.Assert(!this.m_members.IsCustomControl);
			this.Rendering.m_actionInfoDef = actionDef;
			this.Rendering.m_actionInfoInstance = actionInstance;
			this.Rendering.m_renderingContext = renderingContext;
			this.Rendering.m_ownerUniqueName = ownerUniqueName;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00006CA8 File Offset: 0x00004EA8
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00006D4E File Offset: 0x00004F4E
		public ActionCollection Actions
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.m_actionCollection;
				}
				ActionCollection actionCollection = this.Rendering.m_actionCollection;
				if (this.Rendering.m_actionCollection == null)
				{
					ActionItemInstanceList actionItemInstanceList = null;
					if (this.Rendering.m_actionInfoInstance != null)
					{
						actionItemInstanceList = this.Rendering.m_actionInfoInstance.ActionItemsValues;
					}
					actionCollection = new ActionCollection(this.Rendering.m_actionInfoDef.ActionItems, actionItemInstanceList, this.Rendering.m_ownerUniqueName, this.Rendering.m_renderingContext);
					if (this.Rendering.m_renderingContext.CacheState)
					{
						this.Rendering.m_actionCollection = actionCollection;
					}
				}
				return actionCollection;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.m_actionCollection = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00006D70 File Offset: 0x00004F70
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00006DEF File Offset: 0x00004FEF
		public ActionStyle Style
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.m_style;
				}
				if (this.Rendering.m_actionInfoDef.StyleClass == null)
				{
					return null;
				}
				ActionStyle actionStyle = this.Rendering.m_style;
				if (this.Rendering.m_style == null)
				{
					actionStyle = new ActionStyle(this, this.Rendering.m_renderingContext);
					if (this.Rendering.m_renderingContext.CacheState)
					{
						this.Rendering.m_style = actionStyle;
					}
				}
				return actionStyle;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.m_style = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00006E10 File Offset: 0x00005010
		internal Microsoft.ReportingServices.ReportProcessing.Action ActionInfoDef
		{
			get
			{
				return this.Rendering.m_actionInfoDef;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00006E1D File Offset: 0x0000501D
		internal ActionInstance ActionInfoInstance
		{
			get
			{
				return this.Rendering.m_actionInfoInstance;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006E2A File Offset: 0x0000502A
		private bool IsCustomControl
		{
			get
			{
				return this.m_members.IsCustomControl;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00006E38 File Offset: 0x00005038
		private ActionInfoRendering Rendering
		{
			get
			{
				Global.Tracer.Assert(!this.m_members.IsCustomControl);
				ActionInfoRendering actionInfoRendering = this.m_members as ActionInfoRendering;
				if (actionInfoRendering == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionInfoRendering;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00006E78 File Offset: 0x00005078
		private ActionInfoProcessing Processing
		{
			get
			{
				Global.Tracer.Assert(this.m_members.IsCustomControl);
				ActionInfoProcessing actionInfoProcessing = this.m_members as ActionInfoProcessing;
				if (actionInfoProcessing == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionInfoProcessing;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00006EB8 File Offset: 0x000050B8
		internal ActionInfo DeepClone()
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			ActionInfo actionInfo = new ActionInfo();
			Global.Tracer.Assert(this.m_members != null && this.m_members is ActionInfoProcessing);
			actionInfo.m_members = this.Processing.DeepClone();
			return actionInfo;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006F14 File Offset: 0x00005114
		internal void Deconstruct(int uniqueName, ref Microsoft.ReportingServices.ReportProcessing.Action action, out ActionInstance actionInstance, CustomReportItem context)
		{
			Global.Tracer.Assert(this.IsCustomControl && context != null);
			actionInstance = null;
			if (this.Processing.m_actionCollection == null || this.Processing.m_actionCollection.Count == 0)
			{
				if (action != null)
				{
					Global.Tracer.Assert(action.ActionItems != null && 0 < action.ActionItems.Count);
					int count = action.ActionItems.Count;
					actionInstance = new ActionInstance();
					actionInstance.UniqueName = uniqueName;
					actionInstance.ActionItemsValues = new ActionItemInstanceList(count);
					for (int i = 0; i < count; i++)
					{
						ActionItemInstance actionItemInstance = new ActionItemInstance();
						if (action.ActionItems[i].DrillthroughParameters != null)
						{
							int count2 = action.ActionItems[i].DrillthroughParameters.Count;
							actionItemInstance.DrillthroughParametersValues = new object[count2];
							actionItemInstance.DrillthroughParametersOmits = new BoolList(count2);
						}
						actionInstance.ActionItemsValues.Add(actionItemInstance);
					}
				}
				return;
			}
			bool flag = action == null;
			int count3 = this.Processing.m_actionCollection.Count;
			Global.Tracer.Assert(1 <= count3);
			if (flag)
			{
				action = new Microsoft.ReportingServices.ReportProcessing.Action();
				action.ActionItems = new ActionItemList(count3);
				action.ComputedActionItemsCount = count3;
			}
			else if (count3 != action.ComputedActionItemsCount)
			{
				context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[]
				{
					context.Name,
					"Actions",
					action.ComputedActionItemsCount.ToString(CultureInfo.InvariantCulture),
					count3.ToString(CultureInfo.InvariantCulture)
				});
				throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
			}
			actionInstance = new ActionInstance();
			actionInstance.UniqueName = uniqueName;
			actionInstance.ActionItemsValues = new ActionItemInstanceList(count3);
			for (int j = 0; j < count3; j++)
			{
				Microsoft.ReportingServices.ReportRendering.Action action2 = this.Processing.m_actionCollection[j];
				ActionItem actionItem;
				if (flag)
				{
					actionItem = new ActionItem();
					actionItem.ComputedIndex = j;
					actionItem.Label = new ExpressionInfo(ExpressionInfo.Types.Expression);
					switch (action2.m_actionType)
					{
					case ActionType.HyperLink:
						actionItem.HyperLinkURL = new ExpressionInfo(ExpressionInfo.Types.Expression);
						break;
					case ActionType.DrillThrough:
						actionItem.DrillthroughReportName = new ExpressionInfo(ExpressionInfo.Types.Expression);
						if (action2.m_parameters != null && 0 < action2.m_parameters.Count)
						{
							int count4 = action2.m_parameters.Count;
							actionItem.DrillthroughParameters = new ParameterValueList(count4);
							for (int k = 0; k < count4; k++)
							{
								ParameterValue parameterValue = new ParameterValue();
								parameterValue.Name = action2.m_parameters.GetKey(k);
								parameterValue.Omit = new ExpressionInfo(ExpressionInfo.Types.Constant);
								parameterValue.Omit.BoolValue = false;
								parameterValue.Value = new ExpressionInfo(ExpressionInfo.Types.Expression);
								actionItem.DrillthroughParameters.Add(parameterValue);
							}
						}
						break;
					case ActionType.BookmarkLink:
						actionItem.BookmarkLink = new ExpressionInfo(ExpressionInfo.Types.Expression);
						break;
					}
					action.ActionItems.Add(actionItem);
				}
				else
				{
					actionItem = action.ActionItems[j];
				}
				Global.Tracer.Assert(actionItem != null);
				ActionItemInstance actionItemInstance2 = new ActionItemInstance();
				actionItemInstance2.Label = action2.Processing.m_label;
				switch (action2.m_actionType)
				{
				case ActionType.HyperLink:
					actionItemInstance2.HyperLinkURL = action2.Processing.m_action;
					break;
				case ActionType.DrillThrough:
					actionItemInstance2.DrillthroughReportName = action2.Processing.m_action;
					if (action2.m_parameters != null)
					{
						int count5 = action2.m_parameters.Count;
						if (actionItem.DrillthroughParameters == null && 0 < count5)
						{
							context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[]
							{
								context.Name,
								"Action.DrillthroughParameters",
								"0",
								count5.ToString(CultureInfo.InvariantCulture)
							});
							throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
						}
						if (count5 != actionItem.DrillthroughParameters.Count)
						{
							context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[]
							{
								context.Name,
								"Action.DrillthroughParameters",
								actionItem.DrillthroughParameters.Count.ToString(CultureInfo.InvariantCulture),
								count5.ToString(CultureInfo.InvariantCulture)
							});
							throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
						}
						Global.Tracer.Assert(0 < count5);
						actionItemInstance2.DrillthroughParametersValues = new object[count5];
						actionItemInstance2.DrillthroughParametersOmits = new BoolList(count5);
						DrillthroughParameters drillthroughParameters = new DrillthroughParameters(count5);
						for (int l = 0; l < count5; l++)
						{
							actionItemInstance2.DrillthroughParametersValues[l] = action2.m_parameters.GetValues(l);
							actionItemInstance2.DrillthroughParametersOmits.Add(false);
							drillthroughParameters.Add(actionItem.DrillthroughParameters[l].Name, actionItemInstance2.DrillthroughParametersValues[l]);
						}
						DrillthroughInformation drillthroughInformation = new DrillthroughInformation(actionItemInstance2.DrillthroughReportName, drillthroughParameters, null);
						string text = uniqueName.ToString(CultureInfo.InvariantCulture) + ":" + j.ToString(CultureInfo.InvariantCulture);
						context.ProcessingContext.DrillthroughInfo.AddDrillthrough(text, drillthroughInformation);
					}
					break;
				case ActionType.BookmarkLink:
					actionItemInstance2.BookmarkLink = action2.Processing.m_action;
					break;
				}
				actionInstance.ActionItemsValues.Add(actionItemInstance2);
			}
			Global.Tracer.Assert(action != null && actionInstance != null && this.Processing.m_actionCollection != null);
			Style styleClass = action.StyleClass;
			object[] array = null;
			CustomReportItem.DeconstructRenderStyle(flag, this.Processing.m_sharedStyles, this.Processing.m_nonSharedStyles, ref styleClass, out array, context);
			action.StyleClass = styleClass;
			actionInstance.StyleAttributeValues = array;
		}

		// Token: 0x0400001E RID: 30
		private MemberBase m_members;
	}
}
