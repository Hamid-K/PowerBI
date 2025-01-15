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
	// Token: 0x02000448 RID: 1096
	[Serializable]
	internal class MapSpatialElementTemplate : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x000DE542 File Offset: 0x000DC742
		internal MapSpatialElementTemplate()
		{
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000DE54A File Offset: 0x000DC74A
		internal MapSpatialElementTemplate(MapVectorLayer mapVectorLayer, Map map, int id)
			: base(map)
		{
			this.m_id = id;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000DE561 File Offset: 0x000DC761
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x000DE569 File Offset: 0x000DC769
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000DE572 File Offset: 0x000DC772
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000DE57A File Offset: 0x000DC77A
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000DE582 File Offset: 0x000DC782
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000DE58B File Offset: 0x000DC78B
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000DE593 File Offset: 0x000DC793
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000DE59B File Offset: 0x000DC79B
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000DE5A4 File Offset: 0x000DC7A4
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000DE5AC File Offset: 0x000DC7AC
		internal ExpressionInfo OffsetX
		{
			get
			{
				return this.m_offsetX;
			}
			set
			{
				this.m_offsetX = value;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000DE5B5 File Offset: 0x000DC7B5
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x000DE5BD File Offset: 0x000DC7BD
		internal ExpressionInfo OffsetY
		{
			get
			{
				return this.m_offsetY;
			}
			set
			{
				this.m_offsetY = value;
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000DE5C6 File Offset: 0x000DC7C6
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x000DE5CE File Offset: 0x000DC7CE
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

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000DE5D7 File Offset: 0x000DC7D7
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x000DE5DF File Offset: 0x000DC7DF
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000DE5E8 File Offset: 0x000DC7E8
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x000DE5F0 File Offset: 0x000DC7F0
		internal ExpressionInfo DataElementLabel
		{
			get
			{
				return this.m_dataElementLabel;
			}
			set
			{
				this.m_dataElementLabel = value;
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000DE5F9 File Offset: 0x000DC7F9
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x000DE601 File Offset: 0x000DC801
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000DE60A File Offset: 0x000DC80A
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x000DE612 File Offset: 0x000DC812
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000DE61B File Offset: 0x000DC81B
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000DE628 File Offset: 0x000DC828
		internal MapSpatialElementTemplateExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000DE630 File Offset: 0x000DC830
		protected IInstancePath InstancePath
		{
			get
			{
				return this.m_mapVectorLayer.InstancePath;
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000DE640 File Offset: 0x000DC840
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.MapSpatialElementTemplateHidden(this.m_hidden);
			}
			if (this.m_offsetX != null)
			{
				this.m_offsetX.Initialize("OffsetX", context);
				context.ExprHostBuilder.MapSpatialElementTemplateOffsetX(this.m_offsetX);
			}
			if (this.m_offsetY != null)
			{
				this.m_offsetY.Initialize("OffsetY", context);
				context.ExprHostBuilder.MapSpatialElementTemplateOffsetY(this.m_offsetY);
			}
			if (this.m_label != null)
			{
				this.m_label.Initialize("Label", context);
				context.ExprHostBuilder.MapSpatialElementTemplateLabel(this.m_label);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.MapSpatialElementTemplateToolTip(this.m_toolTip);
			}
			if (this.m_dataElementLabel != null)
			{
				this.m_dataElementLabel.Initialize("DataElementLabel", context);
				context.ExprHostBuilder.MapSpatialElementTemplateDataElementLabel(this.m_dataElementLabel);
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000DE76C File Offset: 0x000DC96C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapSpatialElementTemplate mapSpatialElementTemplate = (MapSpatialElementTemplate)base.PublishClone(context);
			mapSpatialElementTemplate.m_mapVectorLayer = context.CurrentMapVectorLayerClone;
			if (this.m_action != null)
			{
				mapSpatialElementTemplate.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				mapSpatialElementTemplate.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_offsetX != null)
			{
				mapSpatialElementTemplate.m_offsetX = (ExpressionInfo)this.m_offsetX.PublishClone(context);
			}
			if (this.m_offsetY != null)
			{
				mapSpatialElementTemplate.m_offsetY = (ExpressionInfo)this.m_offsetY.PublishClone(context);
			}
			if (this.m_label != null)
			{
				mapSpatialElementTemplate.m_label = (ExpressionInfo)this.m_label.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				mapSpatialElementTemplate.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_dataElementLabel != null)
			{
				mapSpatialElementTemplate.m_dataElementLabel = (ExpressionInfo)this.m_dataElementLabel.PublishClone(context);
			}
			return mapSpatialElementTemplate;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000DE870 File Offset: 0x000DCA70
		internal void SetExprHost(MapSpatialElementTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_action != null && this.ExprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.ExprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000DE8D4 File Offset: 0x000DCAD4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.ID, Token.Int32),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataElementLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Token.Reference)
			});
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000DE9D8 File Offset: 0x000DCBD8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapSpatialElementTemplate.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Action)
				{
					if (memberName <= MemberName.Label)
					{
						if (memberName == MemberName.ID)
						{
							writer.Write(this.m_id);
							continue;
						}
						if (memberName == MemberName.Label)
						{
							writer.Write(this.m_label);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
						if (memberName == MemberName.Action)
						{
							writer.Write(this.m_action);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.OffsetX)
				{
					if (memberName == MemberName.DataElementName)
					{
						writer.Write(this.m_dataElementName);
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
					if (memberName == MemberName.OffsetX)
					{
						writer.Write(this.m_offsetX);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.OffsetY)
					{
						writer.Write(this.m_offsetY);
						continue;
					}
					if (memberName == MemberName.MapVectorLayer)
					{
						writer.WriteReference(this.m_mapVectorLayer);
						continue;
					}
					if (memberName == MemberName.DataElementLabel)
					{
						writer.Write(this.m_dataElementLabel);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000DEB64 File Offset: 0x000DCD64
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapSpatialElementTemplate.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Action)
				{
					if (memberName <= MemberName.Label)
					{
						if (memberName == MemberName.ID)
						{
							this.m_id = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.Label)
						{
							this.m_label = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Action)
						{
							this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.OffsetX)
				{
					if (memberName == MemberName.DataElementName)
					{
						this.m_dataElementName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.OffsetX)
					{
						this.m_offsetX = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.OffsetY)
					{
						this.m_offsetY = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapVectorLayer)
					{
						this.m_mapVectorLayer = reader.ReadReference<MapVectorLayer>(this);
						continue;
					}
					if (memberName == MemberName.DataElementLabel)
					{
						this.m_dataElementLabel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000DED1C File Offset: 0x000DCF1C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapSpatialElementTemplate.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.MapVectorLayer)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_mapVectorLayer = (MapVectorLayer)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000DEDC8 File Offset: 0x000DCFC8
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000DEDCF File Offset: 0x000DCFCF
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialElementTemplateHiddenExpression(this, this.m_map.Name);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000DEDF5 File Offset: 0x000DCFF5
		internal double EvaluateOffsetX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialElementTemplateOffsetXExpression(this, this.m_map.Name);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000DEE1B File Offset: 0x000DD01B
		internal double EvaluateOffsetY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialElementTemplateOffsetYExpression(this, this.m_map.Name);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000DEE44 File Offset: 0x000DD044
		internal string EvaluateLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapSpatialElementTemplateLabelExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000DEE84 File Offset: 0x000DD084
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapSpatialElementTemplateToolTipExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000DEEC4 File Offset: 0x000DD0C4
		internal string EvaluateDataElementLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapSpatialElementTemplateDataElementLabelExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x04001942 RID: 6466
		[NonSerialized]
		protected MapSpatialElementTemplateExprHost m_exprHost;

		// Token: 0x04001943 RID: 6467
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSpatialElementTemplate.GetDeclaration();

		// Token: 0x04001944 RID: 6468
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001945 RID: 6469
		private int m_id;

		// Token: 0x04001946 RID: 6470
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001947 RID: 6471
		[Reference]
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x04001948 RID: 6472
		private ExpressionInfo m_hidden;

		// Token: 0x04001949 RID: 6473
		private ExpressionInfo m_offsetX;

		// Token: 0x0400194A RID: 6474
		private ExpressionInfo m_offsetY;

		// Token: 0x0400194B RID: 6475
		private ExpressionInfo m_label;

		// Token: 0x0400194C RID: 6476
		private ExpressionInfo m_toolTip;

		// Token: 0x0400194D RID: 6477
		private ExpressionInfo m_dataElementLabel;

		// Token: 0x0400194E RID: 6478
		private string m_dataElementName;

		// Token: 0x0400194F RID: 6479
		private DataElementOutputTypes m_dataElementOutput;
	}
}
