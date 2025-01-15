using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000007 RID: 7
	public sealed class Action
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x000061AD File Offset: 0x000043AD
		public Action()
		{
			this.m_members = new ActionProcessing();
			Global.Tracer.Assert(this.IsCustomControl);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000061D0 File Offset: 0x000043D0
		internal Action(ActionItem actionItemDef, ActionItemInstance actionItemInstance, string drillthroughId, RenderingContext renderingContext)
		{
			this.m_members = new ActionRendering();
			Global.Tracer.Assert(!this.IsCustomControl);
			this.Rendering.m_actionDef = actionItemDef;
			this.Rendering.m_actionInstance = actionItemInstance;
			this.Rendering.m_renderingContext = renderingContext;
			this.Rendering.m_drillthroughId = drillthroughId;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00006234 File Offset: 0x00004434
		public ReportUrl HyperLinkURL
		{
			get
			{
				if (this.m_actionType == ActionType.DrillThrough || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				if (this.IsCustomControl)
				{
					return null;
				}
				ReportUrl reportUrl = this.Rendering.m_actionURL;
				if (this.Rendering.m_actionURL == null && this.Rendering.m_actionDef.HyperLinkURL != null)
				{
					string text = null;
					this.m_actionType = ActionType.HyperLink;
					if (this.Rendering.m_actionDef.HyperLinkURL.Type == ExpressionInfo.Types.Constant)
					{
						text = this.Rendering.m_actionDef.HyperLinkURL.Value;
					}
					else if (this.Rendering.m_actionInstance != null)
					{
						text = this.Rendering.m_actionInstance.HyperLinkURL;
					}
					reportUrl = ReportUrl.BuildHyperLinkURL(text, this.Rendering.m_renderingContext);
					if (this.Rendering.m_renderingContext.CacheState)
					{
						this.Rendering.m_actionURL = reportUrl;
					}
				}
				return reportUrl;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000631C File Offset: 0x0000451C
		public ReportUrl DrillthroughReport
		{
			get
			{
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				if (this.IsCustomControl)
				{
					return null;
				}
				ReportUrl reportUrl = this.Rendering.m_actionURL;
				if (this.Rendering.m_actionURL == null && this.Rendering.m_actionDef.DrillthroughReportName != null)
				{
					string drillthroughPath = this.DrillthroughPath;
					this.m_actionType = ActionType.DrillThrough;
					if (drillthroughPath != null)
					{
						try
						{
							reportUrl = new ReportUrl(this.Rendering.m_renderingContext, drillthroughPath, true, null, true);
						}
						catch (ItemNotFoundException)
						{
							return null;
						}
					}
					if (this.Rendering.m_renderingContext.CacheState)
					{
						this.Rendering.m_actionURL = reportUrl;
					}
				}
				return reportUrl;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000063D0 File Offset: 0x000045D0
		internal DrillthroughParameters DrillthroughParameterNameObjectCollection
		{
			get
			{
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				DrillthroughParameters drillthroughParameters = this.m_parameterNameObjectCollection;
				if (!this.IsCustomControl && this.m_parameters == null)
				{
					ParameterValueList drillthroughParameters2 = this.Rendering.m_actionDef.DrillthroughParameters;
					if (drillthroughParameters2 != null && drillthroughParameters2.Count > 0)
					{
						this.m_actionType = ActionType.DrillThrough;
						drillthroughParameters = new DrillthroughParameters();
						for (int i = 0; i < drillthroughParameters2.Count; i++)
						{
							ParameterValue parameterValue = drillthroughParameters2[i];
							if (parameterValue.Omit == null || (this.Rendering.m_actionInstance == null && parameterValue.Omit.Type == ExpressionInfo.Types.Constant && !parameterValue.Omit.BoolValue) || !this.Rendering.m_actionInstance.DrillthroughParametersOmits[i])
							{
								object obj;
								if (parameterValue.Value.Type == ExpressionInfo.Types.Constant)
								{
									obj = parameterValue.Value.Value;
								}
								else if (this.Rendering.m_actionInstance == null)
								{
									obj = null;
								}
								else
								{
									obj = this.Rendering.m_actionInstance.DrillthroughParametersValues[i];
								}
								drillthroughParameters.Add(parameterValue.Name, obj);
							}
						}
						this.m_parameterNameObjectCollection = drillthroughParameters;
					}
				}
				return drillthroughParameters;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006504 File Offset: 0x00004704
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00006724 File Offset: 0x00004924
		public NameValueCollection DrillthroughParameters
		{
			get
			{
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				NameValueCollection nameValueCollection = this.m_parameters;
				if (!this.IsCustomControl && this.m_parameters == null)
				{
					ParameterValueList drillthroughParameters = this.Rendering.m_actionDef.DrillthroughParameters;
					if (drillthroughParameters != null && drillthroughParameters.Count > 0)
					{
						this.m_actionType = ActionType.DrillThrough;
						nameValueCollection = new NameValueCollection();
						bool[] array = new bool[drillthroughParameters.Count];
						for (int i = 0; i < drillthroughParameters.Count; i++)
						{
							ParameterValue parameterValue = drillthroughParameters[i];
							if (parameterValue.Value != null && parameterValue.Value.Type == ExpressionInfo.Types.Token)
							{
								array[i] = true;
							}
							else
							{
								array[i] = false;
							}
							if (parameterValue.Omit == null || (this.Rendering.m_actionInstance == null && parameterValue.Omit.Type == ExpressionInfo.Types.Constant && !parameterValue.Omit.BoolValue) || !this.Rendering.m_actionInstance.DrillthroughParametersOmits[i])
							{
								object obj;
								if (parameterValue.Value.Type == ExpressionInfo.Types.Constant)
								{
									obj = parameterValue.Value.Value;
								}
								else if (this.Rendering.m_actionInstance == null)
								{
									obj = null;
								}
								else
								{
									obj = this.Rendering.m_actionInstance.DrillthroughParametersValues[i];
								}
								if (obj == null)
								{
									nameValueCollection.Add(parameterValue.Name, null);
								}
								else
								{
									object[] array2 = obj as object[];
									if (array2 != null)
									{
										for (int j = 0; j < array2.Length; j++)
										{
											nameValueCollection.Add(parameterValue.Name, array2[j].ToString());
										}
									}
									else
									{
										nameValueCollection.Add(parameterValue.Name, obj.ToString());
									}
								}
							}
						}
						bool flag = false;
						if (this.Rendering.m_renderingContext.StoreServerParameters != null && this.DrillthroughPath != null)
						{
							string drillthroughPath = this.DrillthroughPath;
							ICatalogItemContext subreportContext = this.Rendering.m_renderingContext.TopLevelReportContext.GetSubreportContext(drillthroughPath);
							nameValueCollection = this.Rendering.m_renderingContext.StoreServerParameters(subreportContext, nameValueCollection, array, out flag);
						}
						if (this.Rendering.m_renderingContext.CacheState)
						{
							this.m_parameters = nameValueCollection;
						}
					}
				}
				return nameValueCollection;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_parameters = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00006740 File Offset: 0x00004940
		public string DrillthroughID
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				if (this.DrillthroughReport != null)
				{
					this.m_actionType = ActionType.DrillThrough;
					return this.Rendering.m_drillthroughId;
				}
				return null;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000677C File Offset: 0x0000497C
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00006821 File Offset: 0x00004A21
		public string BookmarkLink
		{
			get
			{
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.DrillThrough)
				{
					return null;
				}
				string text = null;
				if (this.IsCustomControl)
				{
					text = this.Processing.m_action;
				}
				else if (this.Rendering.m_actionDef.BookmarkLink != null)
				{
					this.m_actionType = ActionType.BookmarkLink;
					if (this.Rendering.m_actionDef.BookmarkLink.Type == ExpressionInfo.Types.Constant)
					{
						text = this.Rendering.m_actionDef.BookmarkLink.Value;
					}
					else if (this.Rendering.m_actionInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.Rendering.m_actionInstance.BookmarkLink;
					}
				}
				return text;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_actionType = ActionType.BookmarkLink;
				this.Processing.m_action = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000684C File Offset: 0x00004A4C
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000068D6 File Offset: 0x00004AD6
		public string Label
		{
			get
			{
				string text = null;
				if (this.IsCustomControl)
				{
					text = this.Processing.m_label;
				}
				else if (this.Rendering.m_actionDef.Label != null)
				{
					if (this.Rendering.m_actionDef.Label.Type == ExpressionInfo.Types.Constant)
					{
						text = this.Rendering.m_actionDef.Label.Value;
					}
					else if (this.Rendering.m_actionInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.Rendering.m_actionInstance.Label;
					}
				}
				return text;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.m_label = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000068F7 File Offset: 0x00004AF7
		internal ActionItem ActionDefinition
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.Rendering.m_actionDef;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00006917 File Offset: 0x00004B17
		internal ActionItemInstance ActionInstance
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.Rendering.m_actionInstance;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00006937 File Offset: 0x00004B37
		internal ParameterValueList DrillthroughParameterValueList
		{
			get
			{
				if (this.m_actionType == ActionType.HyperLink || this.m_actionType == ActionType.BookmarkLink)
				{
					return null;
				}
				return this.Rendering.m_actionDef.DrillthroughParameters;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00006960 File Offset: 0x00004B60
		internal string DrillthroughPath
		{
			get
			{
				string text;
				if (this.Rendering.m_actionDef.DrillthroughReportName.Type == ExpressionInfo.Types.Constant)
				{
					text = this.Rendering.m_actionDef.DrillthroughReportName.Value;
				}
				else if (this.Rendering.m_actionInstance == null)
				{
					text = null;
				}
				else
				{
					text = this.Rendering.m_actionInstance.DrillthroughReportName;
				}
				return text;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000069C2 File Offset: 0x00004BC2
		private bool IsCustomControl
		{
			get
			{
				return this.m_members.IsCustomControl;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000069D0 File Offset: 0x00004BD0
		private ActionRendering Rendering
		{
			get
			{
				Global.Tracer.Assert(!this.m_members.IsCustomControl);
				ActionRendering actionRendering = this.m_members as ActionRendering;
				if (actionRendering == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionRendering;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00006A10 File Offset: 0x00004C10
		internal ActionProcessing Processing
		{
			get
			{
				Global.Tracer.Assert(this.m_members.IsCustomControl);
				ActionProcessing actionProcessing = this.m_members as ActionProcessing;
				if (actionProcessing == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return actionProcessing;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00006A4D File Offset: 0x00004C4D
		public void SetHyperlinkAction(string hyperlink)
		{
			this.SetHyperlinkAction(hyperlink, null);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00006A57 File Offset: 0x00004C57
		public void SetHyperlinkAction(string hyperlink, string label)
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			this.m_actionType = ActionType.HyperLink;
			this.Processing.m_action = hyperlink;
			this.Processing.m_label = label;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00006A8B File Offset: 0x00004C8B
		public void SetDrillthroughAction(string reportName)
		{
			this.SetDrillthroughAction(reportName, null, null);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00006A96 File Offset: 0x00004C96
		public void SetDrillthroughAction(string reportName, NameValueCollection parameters)
		{
			this.SetDrillthroughAction(reportName, parameters, null);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public void SetDrillthroughAction(string reportName, NameValueCollection parameters, string label)
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (parameters != null && reportName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "reportName" });
			}
			this.m_actionType = ActionType.DrillThrough;
			this.Processing.m_action = reportName;
			this.Processing.m_label = label;
			this.m_parameters = parameters;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00006B09 File Offset: 0x00004D09
		public void SetBookmarkAction(string bookmark)
		{
			this.SetBookmarkAction(bookmark, null);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00006B13 File Offset: 0x00004D13
		public void SetBookmarkAction(string bookmark, string label)
		{
			this.m_actionType = ActionType.BookmarkLink;
			this.Processing.m_action = bookmark;
			this.Processing.m_label = label;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00006B34 File Offset: 0x00004D34
		internal Microsoft.ReportingServices.ReportRendering.Action DeepClone()
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Global.Tracer.Assert(this.m_members != null && this.m_members is ActionProcessing);
			Microsoft.ReportingServices.ReportRendering.Action action = new Microsoft.ReportingServices.ReportRendering.Action();
			action.m_actionType = this.m_actionType;
			action.m_members = this.Processing.DeepClone();
			if (ActionType.DrillThrough == this.m_actionType && this.m_parameters != null)
			{
				action.m_parameters = new NameValueCollection(this.m_parameters);
			}
			return action;
		}

		// Token: 0x04000013 RID: 19
		internal ActionType m_actionType;

		// Token: 0x04000014 RID: 20
		internal MemberBase m_members;

		// Token: 0x04000015 RID: 21
		internal NameValueCollection m_parameters;

		// Token: 0x04000016 RID: 22
		internal DrillthroughParameters m_parameterNameObjectCollection;
	}
}
