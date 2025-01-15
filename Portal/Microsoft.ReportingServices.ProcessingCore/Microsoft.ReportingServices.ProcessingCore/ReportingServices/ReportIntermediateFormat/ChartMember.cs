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
	// Token: 0x02000481 RID: 1153
	[Serializable]
	internal sealed class ChartMember : ReportHierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600357D RID: 13693 RVA: 0x000EA96E File Offset: 0x000E8B6E
		internal ChartMember()
		{
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000EA97D File Offset: 0x000E8B7D
		internal ChartMember(int id, Microsoft.ReportingServices.ReportIntermediateFormat.Chart crItem)
			: base(id, crItem)
		{
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000EA98E File Offset: 0x000E8B8E
		internal override string RdlElementName
		{
			get
			{
				return "ChartMember";
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x000EA995 File Offset: 0x000E8B95
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_chartMembers;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000EA99D File Offset: 0x000E8B9D
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000EA9A5 File Offset: 0x000E8BA5
		internal ChartMemberList ChartMembers
		{
			get
			{
				return this.m_chartMembers;
			}
			set
			{
				this.m_chartMembers = value;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000EA9AE File Offset: 0x000E8BAE
		// (set) Token: 0x06003584 RID: 13700 RVA: 0x000EA9B6 File Offset: 0x000E8BB6
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

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000EA9BF File Offset: 0x000E8BBF
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000EA9C7 File Offset: 0x000E8BC7
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

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000EA9D0 File Offset: 0x000E8BD0
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000EA9D8 File Offset: 0x000E8BD8
		internal ExpressionInfo Label
		{
			get
			{
				return this.m_labelExpression;
			}
			set
			{
				this.m_labelExpression = value;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000EA9E1 File Offset: 0x000E8BE1
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000EA9E9 File Offset: 0x000E8BE9
		internal bool ChartGroupExpression
		{
			get
			{
				return this.m_chartGroupExpression;
			}
			set
			{
				this.m_chartGroupExpression = value;
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000EA9F2 File Offset: 0x000E8BF2
		internal ChartMemberExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000EA9FC File Offset: 0x000E8BFC
		internal void SetIsCategoryMember(bool value)
		{
			this.m_isColumn = value;
			if (this.m_chartMembers != null)
			{
				foreach (object obj in this.m_chartMembers)
				{
					((ChartMember)obj).SetIsCategoryMember(value);
				}
			}
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000EAA64 File Offset: 0x000E8C64
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Chart, this.m_isColumn);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000EAA73 File Offset: 0x000E8C73
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Chart, this.m_isColumn);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000EAA84 File Offset: 0x000E8C84
		internal override bool InnerInitialize(InitializationContext context, bool restrictive)
		{
			if (this.m_labelExpression != null)
			{
				this.m_labelExpression.Initialize("Label", context);
				context.ExprHostBuilder.ChartMemberLabel(this.m_labelExpression);
			}
			ChartSeries chartSeries = this.GetChartSeries();
			if (chartSeries != null)
			{
				chartSeries.Initialize(context, chartSeries.Name);
			}
			return base.InnerInitialize(context, restrictive);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000EAADB File Offset: 0x000E8CDB
		internal override bool Initialize(InitializationContext context, bool restrictive)
		{
			this.DataRendererInitialize(context);
			return base.Initialize(context, restrictive);
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000EAAEC File Offset: 0x000E8CEC
		internal void DataRendererInitialize(InitializationContext context)
		{
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto)
			{
				if (this.m_grouping != null)
				{
					this.m_dataElementOutput = DataElementOutputTypes.Output;
				}
				else
				{
					this.m_dataElementOutput = DataElementOutputTypes.ContentsOnly;
				}
			}
			string text = string.Empty;
			if (this.m_grouping != null)
			{
				text = this.m_grouping.Name + "_Collection";
			}
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, text, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000EAB68 File Offset: 0x000E8D68
		internal override object PublishClone(AutomaticSubtotalContext context, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion newContainingRegion)
		{
			ChartMember chartMember = (ChartMember)base.PublishClone(context, newContainingRegion);
			if (this.m_chartMembers != null)
			{
				chartMember.m_chartMembers = new ChartMemberList(this.m_chartMembers.Count);
				foreach (object obj in this.m_chartMembers)
				{
					ChartMember chartMember2 = (ChartMember)obj;
					chartMember.m_chartMembers.Add(chartMember2.PublishClone(context, newContainingRegion));
				}
			}
			if (this.m_labelExpression != null)
			{
				chartMember.m_labelExpression = (ExpressionInfo)this.m_labelExpression.PublishClone(context);
			}
			return chartMember;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000EAC1C File Offset: 0x000E8E1C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ChartMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000EAC90 File Offset: 0x000E8E90
		private ChartSeries GetChartSeries()
		{
			if (base.IsColumn || this.ChartMembers != null)
			{
				return null;
			}
			ChartSeriesList chartSeriesCollection = ((Microsoft.ReportingServices.ReportIntermediateFormat.Chart)this.m_dataRegionDef).ChartSeriesCollection;
			if (chartSeriesCollection.Count <= base.MemberCellIndex)
			{
				return null;
			}
			return chartSeriesCollection[base.MemberCellIndex];
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000EACDC File Offset: 0x000E8EDC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartMember.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementName)
				{
					if (memberName == MemberName.Label)
					{
						writer.Write(this.m_labelExpression);
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						writer.Write(this.m_dataElementName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
					if (memberName == MemberName.ChartMembers)
					{
						writer.Write(this.m_chartMembers);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000EAD88 File Offset: 0x000E8F88
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartMember.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementName)
				{
					if (memberName == MemberName.Label)
					{
						this.m_labelExpression = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						this.m_dataElementName = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.ChartMembers)
					{
						this.m_chartMembers = reader.ReadListOfRIFObjects<ChartMemberList>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000EAE3C File Offset: 0x000E903C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000EAE46 File Offset: 0x000E9046
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000EAE50 File Offset: 0x000E9050
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(memberExprHost != null && reportObjectModel != null);
				this.m_exprHost = (ChartMemberExprHost)memberExprHost;
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				base.MemberNodeSetExprHost(this.m_exprHost, reportObjectModel);
			}
			if (this.m_exprHost != null && this.m_exprHost.ChartSeriesHost != null)
			{
				ChartSeries chartSeries = this.GetChartSeries();
				if (chartSeries != null)
				{
					chartSeries.SetExprHost(this.m_exprHost.ChartSeriesHost, reportObjectModel);
				}
			}
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000EAED0 File Offset: 0x000E90D0
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000EAED2 File Offset: 0x000E90D2
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateLabel(ChartMemberInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, instance.ReportScopeInstance);
			return context.ReportRuntime.EvaluateChartDynamicMemberLabelExpression(this, this.m_labelExpression, this.m_dataRegionDef.Name);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000EAF00 File Offset: 0x000E9100
		internal string GetFormattedLabelValue(Microsoft.ReportingServices.RdlExpressions.VariantResult labelObject, OnDemandProcessingContext context)
		{
			string text = null;
			if (labelObject.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (labelObject.Value != null)
			{
				TypeCode typeCode = Type.GetTypeCode(labelObject.Value.GetType());
				if (this.m_formatter == null)
				{
					this.m_formatter = new Formatter(base.DataRegionDef.StyleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, base.DataRegionDef.Name);
				}
				text = this.m_formatter.FormatValue(labelObject.Value, typeCode);
			}
			return text;
		}

		// Token: 0x04001A4F RID: 6735
		private ChartMemberList m_chartMembers;

		// Token: 0x04001A50 RID: 6736
		private string m_dataElementName;

		// Token: 0x04001A51 RID: 6737
		private DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.Auto;

		// Token: 0x04001A52 RID: 6738
		private ExpressionInfo m_labelExpression;

		// Token: 0x04001A53 RID: 6739
		[NonSerialized]
		private bool m_chartGroupExpression;

		// Token: 0x04001A54 RID: 6740
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartMember.GetDeclaration();

		// Token: 0x04001A55 RID: 6741
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x04001A56 RID: 6742
		[NonSerialized]
		private ChartMemberExprHost m_exprHost;
	}
}
