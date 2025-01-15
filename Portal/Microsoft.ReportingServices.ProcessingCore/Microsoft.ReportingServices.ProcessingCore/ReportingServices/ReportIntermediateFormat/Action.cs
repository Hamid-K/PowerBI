using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000457 RID: 1111
	[Serializable]
	internal sealed class Action : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060032D1 RID: 13009 RVA: 0x000E2581 File Offset: 0x000E0781
		internal Action()
		{
			this.m_actionItemList = new List<ActionItem>();
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000E2594 File Offset: 0x000E0794
		internal Action(ActionItem actionItem, bool computed)
		{
			this.m_actionItemList = new List<ActionItem>();
			this.m_actionItemList.Add(actionItem);
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000E25B3 File Offset: 0x000E07B3
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x000E25BB File Offset: 0x000E07BB
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

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000E25C4 File Offset: 0x000E07C4
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x000E25CC File Offset: 0x000E07CC
		internal List<ActionItem> ActionItems
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

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000E25D5 File Offset: 0x000E07D5
		// (set) Token: 0x060032D8 RID: 13016 RVA: 0x000E25DD File Offset: 0x000E07DD
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

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000E25E6 File Offset: 0x000E07E6
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x000E25EE File Offset: 0x000E07EE
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

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000E25F7 File Offset: 0x000E07F7
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x000E25FF File Offset: 0x000E07FF
		internal bool TrackFieldsUsedInValueExpression
		{
			get
			{
				return this.m_trackFieldsUsedInValueExpression;
			}
			set
			{
				this.m_trackFieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000E2608 File Offset: 0x000E0808
		internal void Initialize(InitializationContext context)
		{
			Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder exprHostBuilder = context.ExprHostBuilder;
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

		// Token: 0x060032DE RID: 13022 RVA: 0x000E2670 File Offset: 0x000E0870
		internal void SetExprHost(ActionInfoExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(null != exprHost && null != reportObjectModel)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (exprHost.ActionItemHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_actionItemList != null, "(m_actionItemList != null)");
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

		// Token: 0x060032DF RID: 13023 RVA: 0x000E2714 File Offset: 0x000E0914
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
					objectModel.FieldsImpl.ResetFieldsUsedInExpression();
					objectModel.AggregatesImpl.ResetFieldsUsedInExpression();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000E2795 File Offset: 0x000E0995
		internal void GetSelectedItemsForDrillthroughContext(ObjectModelImpl objectModel, IActionOwner actionOwner)
		{
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000E2798 File Offset: 0x000E0998
		public object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Action action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)base.MemberwiseClone();
			if (this.m_actionItemList != null)
			{
				action.m_actionItemList = new List<ActionItem>(this.m_actionItemList.Count);
				foreach (ActionItem actionItem in this.m_actionItemList)
				{
					action.m_actionItemList.Add((ActionItem)actionItem.PublishClone(context));
				}
			}
			if (this.m_styleClass != null)
			{
				action.m_styleClass = (Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_sharedStyleProperties != null)
			{
				action.m_sharedStyleProperties = (StyleProperties)this.m_sharedStyleProperties.PublishClone(context);
			}
			return action;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000E2864 File Offset: 0x000E0A64
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ActionItemList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionItem),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.TrackFieldsUsedInValueExpression, Token.Boolean)
			});
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000E28C4 File Offset: 0x000E0AC4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Action.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName != MemberName.ActionItemList)
					{
						if (memberName != MemberName.TrackFieldsUsedInValueExpression)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_trackFieldsUsedInValueExpression);
						}
					}
					else
					{
						writer.Write<ActionItem>(this.m_actionItemList);
					}
				}
				else
				{
					writer.Write(this.m_styleClass);
				}
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000E2948 File Offset: 0x000E0B48
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Action.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName != MemberName.ActionItemList)
					{
						if (memberName != MemberName.TrackFieldsUsedInValueExpression)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_trackFieldsUsedInValueExpression = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_actionItemList = reader.ReadGenericListOfRIFObjects<ActionItem>();
					}
				}
				else
				{
					this.m_styleClass = (Style)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000E29D0 File Offset: 0x000E0BD0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000E29D2 File Offset: 0x000E0BD2
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action;
		}

		// Token: 0x04001992 RID: 6546
		private List<ActionItem> m_actionItemList;

		// Token: 0x04001993 RID: 6547
		private Style m_styleClass;

		// Token: 0x04001994 RID: 6548
		private bool m_trackFieldsUsedInValueExpression;

		// Token: 0x04001995 RID: 6549
		[NonSerialized]
		private ActionInfoExprHost m_exprHost;

		// Token: 0x04001996 RID: 6550
		[NonSerialized]
		private StyleProperties m_sharedStyleProperties;

		// Token: 0x04001997 RID: 6551
		[NonSerialized]
		private bool m_noNonSharedStyleProps;

		// Token: 0x04001998 RID: 6552
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Action.GetDeclaration();
	}
}
