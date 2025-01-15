using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DF RID: 1759
	[Serializable]
	internal sealed class Action
	{
		// Token: 0x06005FCF RID: 24527 RVA: 0x00182F66 File Offset: 0x00181166
		internal Action(ActionItem actionItem, bool computed)
		{
			this.m_actionItemList = new ActionItemList();
			this.m_actionItemList.Add(actionItem);
			if (computed)
			{
				this.m_computedActionItemsCount = 1;
			}
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00182F90 File Offset: 0x00181190
		internal Action()
		{
			this.m_actionItemList = new ActionItemList();
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x00182FA3 File Offset: 0x001811A3
		// (set) Token: 0x06005FD2 RID: 24530 RVA: 0x00182FAB File Offset: 0x001811AB
		internal Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x00182FB4 File Offset: 0x001811B4
		// (set) Token: 0x06005FD4 RID: 24532 RVA: 0x00182FBC File Offset: 0x001811BC
		internal ActionItemList ActionItems
		{
			get
			{
				return this.m_actionItemList;
			}
			set
			{
				this.m_actionItemList = value;
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x00182FC5 File Offset: 0x001811C5
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x00182FCD File Offset: 0x001811CD
		internal int ComputedActionItemsCount
		{
			get
			{
				return this.m_computedActionItemsCount;
			}
			set
			{
				this.m_computedActionItemsCount = value;
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x00182FD6 File Offset: 0x001811D6
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x00182FDE File Offset: 0x001811DE
		internal StyleProperties SharedStyleProperties
		{
			get
			{
				return this.m_sharedStyleProperties;
			}
			set
			{
				this.m_sharedStyleProperties = value;
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x00182FE7 File Offset: 0x001811E7
		// (set) Token: 0x06005FDA RID: 24538 RVA: 0x00182FEF File Offset: 0x001811EF
		internal bool NoNonSharedStyleProps
		{
			get
			{
				return this.m_noNonSharedStyleProps;
			}
			set
			{
				this.m_noNonSharedStyleProps = value;
			}
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x00182FF8 File Offset: 0x001811F8
		internal void Initialize(InitializationContext context)
		{
			ExprHostBuilder exprHostBuilder = context.ExprHostBuilder;
			exprHostBuilder.ActionInfoStart();
			if (this.m_actionItemList != null)
			{
				for (int i = 0; i < this.m_actionItemList.Count; i++)
				{
					this.m_actionItemList[i].Initialize(context);
				}
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			exprHostBuilder.ActionInfoEnd();
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x00183060 File Offset: 0x00181260
		internal void SetExprHost(ActionInfoExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (exprHost.ActionItemHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_actionItemList != null);
				for (int i = this.m_actionItemList.Count - 1; i >= 0; i--)
				{
					this.m_actionItemList[i].SetExprHost(exprHost.ActionItemHostsRemotable, reportObjectModel);
				}
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x001830F8 File Offset: 0x001812F8
		internal void SetExprHost(ActionExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			if (exprHost != null)
			{
				Global.Tracer.Assert(this.m_actionItemList != null);
				for (int i = this.m_actionItemList.Count - 1; i >= 0; i--)
				{
					this.m_actionItemList[i].SetExprHost(exprHost, reportObjectModel);
				}
			}
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00183148 File Offset: 0x00181348
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ActionItemList, ObjectType.ActionItemList),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.CoumputedActionsCount, Token.Int32)
			});
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x001831A0 File Offset: 0x001813A0
		internal void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, int uniqueName)
		{
			if (this.m_actionItemList == null || this.m_actionItemList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.m_actionItemList.Count; i++)
			{
				this.m_actionItemList[i].ProcessDrillthroughAction(processingContext, uniqueName, i);
			}
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x001831F0 File Offset: 0x001813F0
		internal bool ResetObjectModelForDrillthroughContext(ObjectModelImpl objectModel, IActionOwner actionOwner)
		{
			if (actionOwner.FieldsUsedInValueExpression == null)
			{
				bool flag = false;
				if (this.m_actionItemList != null)
				{
					for (int i = 0; i < this.m_actionItemList.Count; i++)
					{
						if (this.m_actionItemList[i].DrillthroughParameters != null && 0 < this.m_actionItemList[i].DrillthroughParameters.Count)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					objectModel.FieldsImpl.ResetUsedInExpression();
					objectModel.AggregatesImpl.ResetUsedInExpression();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x00183271 File Offset: 0x00181471
		internal void GetSelectedItemsForDrillthroughContext(ObjectModelImpl objectModel, IActionOwner actionOwner)
		{
			if (actionOwner.FieldsUsedInValueExpression == null)
			{
				actionOwner.FieldsUsedInValueExpression = new List<string>();
				objectModel.FieldsImpl.AddFieldsUsedInExpression(actionOwner.FieldsUsedInValueExpression);
				objectModel.AggregatesImpl.AddFieldsUsedInExpression(actionOwner.FieldsUsedInValueExpression);
			}
		}

		// Token: 0x040030BB RID: 12475
		private ActionItemList m_actionItemList;

		// Token: 0x040030BC RID: 12476
		private Style m_styleClass;

		// Token: 0x040030BD RID: 12477
		private int m_computedActionItemsCount;

		// Token: 0x040030BE RID: 12478
		[NonSerialized]
		private ActionInfoExprHost m_exprHost;

		// Token: 0x040030BF RID: 12479
		[NonSerialized]
		private StyleProperties m_sharedStyleProperties;

		// Token: 0x040030C0 RID: 12480
		[NonSerialized]
		private bool m_noNonSharedStyleProps;
	}
}
