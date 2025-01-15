using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	internal sealed class ActionItem : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000E29E5 File Offset: 0x000E0BE5
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x000E29ED File Offset: 0x000E0BED
		internal ExpressionInfo HyperLinkURL
		{
			get
			{
				return this.m_hyperLinkURL;
			}
			set
			{
				this.m_hyperLinkURL = value;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000E29F6 File Offset: 0x000E0BF6
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x000E29FE File Offset: 0x000E0BFE
		internal ExpressionInfo DrillthroughReportName
		{
			get
			{
				return this.m_drillthroughReportName;
			}
			set
			{
				this.m_drillthroughReportName = value;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000E2A07 File Offset: 0x000E0C07
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x000E2A0F File Offset: 0x000E0C0F
		internal List<ParameterValue> DrillthroughParameters
		{
			get
			{
				return this.m_drillthroughParameters;
			}
			set
			{
				this.m_drillthroughParameters = value;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x000E2A18 File Offset: 0x000E0C18
		// (set) Token: 0x060032EF RID: 13039 RVA: 0x000E2A20 File Offset: 0x000E0C20
		internal ExpressionInfo DrillthroughBookmarkLink
		{
			get
			{
				return this.m_drillthroughBookmarkLink;
			}
			set
			{
				this.m_drillthroughBookmarkLink = value;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000E2A29 File Offset: 0x000E0C29
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x000E2A31 File Offset: 0x000E0C31
		internal ExpressionInfo BookmarkLink
		{
			get
			{
				return this.m_bookmarkLink;
			}
			set
			{
				this.m_bookmarkLink = value;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000E2A3A File Offset: 0x000E0C3A
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x000E2A42 File Offset: 0x000E0C42
		internal ExpressionInfo Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000E2A4B File Offset: 0x000E0C4B
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x000E2A53 File Offset: 0x000E0C53
		internal int ComputedIndex
		{
			get
			{
				return this.m_computedIndex;
			}
			set
			{
				this.m_computedIndex = value;
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000E2A5C File Offset: 0x000E0C5C
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x000E2A64 File Offset: 0x000E0C64
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000E2A6D File Offset: 0x000E0C6D
		internal ActionExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000E2A78 File Offset: 0x000E0C78
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ActionStart();
			if (this.m_hyperLinkURL != null)
			{
				this.m_hyperLinkURL.Initialize("Hyperlink", context);
				context.ExprHostBuilder.ActionHyperlink(this.m_hyperLinkURL);
			}
			if (this.m_drillthroughReportName != null)
			{
				this.m_drillthroughReportName.Initialize("DrillthroughReportName", context);
				context.ExprHostBuilder.ActionDrillThroughReportName(this.m_drillthroughReportName);
			}
			if (this.m_drillthroughParameters != null)
			{
				for (int i = 0; i < this.m_drillthroughParameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_drillthroughParameters[i];
					context.ExprHostBuilder.ActionDrillThroughParameterStart();
					parameterValue.Initialize("DrillthroughParameters", context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.ActionDrillThroughParameterEnd();
				}
			}
			if (this.m_drillthroughBookmarkLink != null)
			{
				this.m_drillthroughBookmarkLink.Initialize("BookmarkLink", context);
				context.ExprHostBuilder.ActionDrillThroughBookmarkLink(this.m_drillthroughBookmarkLink);
			}
			if (this.m_bookmarkLink != null)
			{
				this.m_bookmarkLink.Initialize("BookmarkLink", context);
				context.ExprHostBuilder.ActionBookmarkLink(this.m_bookmarkLink);
			}
			if (this.m_label != null)
			{
				this.m_label.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_label);
			}
			this.m_exprHostID = context.ExprHostBuilder.ActionEnd();
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
		internal void SetExprHost(IList<ActionExprHost> actionItemExprHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(actionItemExprHosts != null && reportObjectModel != null, "(actionItemExprHosts != null && reportObjectModel != null)");
				this.m_exprHost = actionItemExprHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.DrillThroughParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_drillthroughParameters != null, "(m_drillthroughParameters != null)");
					for (int i = this.m_drillthroughParameters.Count - 1; i >= 0; i--)
					{
						this.m_drillthroughParameters[i].SetExprHost(this.m_exprHost.DrillThroughParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000E2C7A File Offset: 0x000E0E7A
		internal string EvaluateHyperLinkURL(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			return context.ReportRuntime.EvaluateReportItemHyperlinkURLExpression(this, this.m_hyperLinkURL, objectType, objectName);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000E2C9A File Offset: 0x000E0E9A
		internal string EvaluateDrillthroughReportName(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			return context.ReportRuntime.EvaluateReportItemDrillthroughReportName(this, this.m_drillthroughReportName, objectType, objectName);
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000E2CBA File Offset: 0x000E0EBA
		internal string EvaluateBookmarkLink(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			return context.ReportRuntime.EvaluateReportItemBookmarkLinkExpression(this, this.m_bookmarkLink, objectType, objectName);
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000E2CDA File Offset: 0x000E0EDA
		internal string EvaluateLabel(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			return context.ReportRuntime.EvaluateActionLabelExpression(this, this.m_label, objectType, objectName);
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000E2CFC File Offset: 0x000E0EFC
		internal object EvaluateDrillthroughParamValue(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, List<string> fieldsUsedInOwnerValue, ParameterValue paramValue, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			Microsoft.ReportingServices.RdlExpressions.ReportRuntime reportRuntime = context.ReportRuntime;
			reportRuntime.FieldsUsedInCurrentActionOwnerValue = fieldsUsedInOwnerValue;
			ref Microsoft.ReportingServices.RdlExpressions.ParameterValueResult ptr = reportRuntime.EvaluateParameterValueExpression(paramValue, objectType, objectName, "DrillthroughParameterValue");
			reportRuntime.FieldsUsedInCurrentActionOwnerValue = null;
			return ptr.Value;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000E2D3D File Offset: 0x000E0F3D
		internal bool EvaluateDrillthroughParamOmit(IReportScopeInstance romInstance, OnDemandProcessingContext context, IInstancePath ownerItem, ParameterValue paramValue, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			context.SetupContext(ownerItem, romInstance);
			return context.ReportRuntime.EvaluateParamValueOmitExpression(paramValue, objectType, objectName);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000E2D58 File Offset: 0x000E0F58
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HyperLinkURL, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DrillthroughReportName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DrillthroughParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue),
				new MemberInfo(MemberName.DrillthroughBookmarkLink, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BookmarkLink, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Index, Token.Int32)
			});
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000E2E20 File Offset: 0x000E1020
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ActionItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Index)
				{
					if (memberName == MemberName.Label)
					{
						writer.Write(this.m_label);
						continue;
					}
					if (memberName == MemberName.HyperLinkURL)
					{
						writer.Write(this.m_hyperLinkURL);
						continue;
					}
					if (memberName == MemberName.Index)
					{
						writer.Write(this.m_computedIndex);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.DrillthroughReportName:
						writer.Write(this.m_drillthroughReportName);
						continue;
					case MemberName.DrillthroughParameters:
						writer.Write<ParameterValue>(this.m_drillthroughParameters);
						continue;
					case MemberName.BookmarkLink:
						writer.Write(this.m_bookmarkLink);
						continue;
					default:
						if (memberName == MemberName.DrillthroughBookmarkLink)
						{
							writer.Write(this.m_drillthroughBookmarkLink);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000E2F30 File Offset: 0x000E1130
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ActionItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Index)
				{
					if (memberName == MemberName.Label)
					{
						this.m_label = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.HyperLinkURL)
					{
						this.m_hyperLinkURL = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Index)
					{
						this.m_computedIndex = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.DrillthroughReportName:
						this.m_drillthroughReportName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DrillthroughParameters:
						this.m_drillthroughParameters = reader.ReadGenericListOfRIFObjects<ParameterValue>();
						continue;
					case MemberName.BookmarkLink:
						this.m_bookmarkLink = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.DrillthroughBookmarkLink)
						{
							this.m_drillthroughBookmarkLink = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000E305E File Offset: 0x000E125E
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000E306B File Offset: 0x000E126B
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionItem;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000E3074 File Offset: 0x000E1274
		public object PublishClone(AutomaticSubtotalContext context)
		{
			ActionItem actionItem = (ActionItem)base.MemberwiseClone();
			if (this.m_hyperLinkURL != null)
			{
				actionItem.m_hyperLinkURL = (ExpressionInfo)this.m_hyperLinkURL.PublishClone(context);
			}
			if (this.m_drillthroughReportName != null)
			{
				actionItem.m_drillthroughReportName = (ExpressionInfo)this.m_drillthroughReportName.PublishClone(context);
			}
			if (this.m_drillthroughParameters != null)
			{
				actionItem.m_drillthroughParameters = new List<ParameterValue>(this.m_drillthroughParameters.Count);
				foreach (ParameterValue parameterValue in this.m_drillthroughParameters)
				{
					actionItem.m_drillthroughParameters.Add((ParameterValue)parameterValue.PublishClone(context));
				}
			}
			if (this.m_drillthroughBookmarkLink != null)
			{
				actionItem.m_drillthroughBookmarkLink = (ExpressionInfo)this.m_drillthroughBookmarkLink.PublishClone(context);
			}
			if (this.m_bookmarkLink != null)
			{
				actionItem.m_bookmarkLink = (ExpressionInfo)this.m_bookmarkLink.PublishClone(context);
			}
			if (this.m_label != null)
			{
				actionItem.m_label = (ExpressionInfo)this.m_label.PublishClone(context);
			}
			return actionItem;
		}

		// Token: 0x04001999 RID: 6553
		private ExpressionInfo m_hyperLinkURL;

		// Token: 0x0400199A RID: 6554
		private ExpressionInfo m_drillthroughReportName;

		// Token: 0x0400199B RID: 6555
		private List<ParameterValue> m_drillthroughParameters;

		// Token: 0x0400199C RID: 6556
		private ExpressionInfo m_drillthroughBookmarkLink;

		// Token: 0x0400199D RID: 6557
		private ExpressionInfo m_bookmarkLink;

		// Token: 0x0400199E RID: 6558
		private ExpressionInfo m_label;

		// Token: 0x0400199F RID: 6559
		private int m_exprHostID = -1;

		// Token: 0x040019A0 RID: 6560
		private int m_computedIndex = -1;

		// Token: 0x040019A1 RID: 6561
		[NonSerialized]
		private ActionExprHost m_exprHost;

		// Token: 0x040019A2 RID: 6562
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ActionItem.GetDeclaration();
	}
}
