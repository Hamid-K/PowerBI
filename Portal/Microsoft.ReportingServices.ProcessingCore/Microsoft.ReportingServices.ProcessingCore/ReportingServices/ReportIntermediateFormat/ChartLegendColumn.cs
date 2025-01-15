using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000489 RID: 1161
	[Serializable]
	internal sealed class ChartLegendColumn : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x0600363B RID: 13883 RVA: 0x000ED138 File Offset: 0x000EB338
		internal ChartLegendColumn()
		{
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000ED140 File Offset: 0x000EB340
		internal ChartLegendColumn(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, int id)
			: base(chart)
		{
			this.m_id = id;
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000ED150 File Offset: 0x000EB350
		// (set) Token: 0x0600363E RID: 13886 RVA: 0x000ED158 File Offset: 0x000EB358
		internal string LegendColumnName
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000ED161 File Offset: 0x000EB361
		internal ChartLegendColumnExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000ED169 File Offset: 0x000EB369
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000ED171 File Offset: 0x000EB371
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000ED179 File Offset: 0x000EB379
		// (set) Token: 0x06003643 RID: 13891 RVA: 0x000ED181 File Offset: 0x000EB381
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

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000ED18A File Offset: 0x000EB38A
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000ED192 File Offset: 0x000EB392
		// (set) Token: 0x06003646 RID: 13894 RVA: 0x000ED19A File Offset: 0x000EB39A
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

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000ED1A3 File Offset: 0x000EB3A3
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000ED1AB File Offset: 0x000EB3AB
		internal ExpressionInfo ColumnType
		{
			get
			{
				return this.m_columnType;
			}
			set
			{
				this.m_columnType = value;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000ED1B4 File Offset: 0x000EB3B4
		// (set) Token: 0x0600364A RID: 13898 RVA: 0x000ED1BC File Offset: 0x000EB3BC
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000ED1C5 File Offset: 0x000EB3C5
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x000ED1CD File Offset: 0x000EB3CD
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

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000ED1D6 File Offset: 0x000EB3D6
		// (set) Token: 0x0600364E RID: 13902 RVA: 0x000ED1DE File Offset: 0x000EB3DE
		internal ExpressionInfo MinimumWidth
		{
			get
			{
				return this.m_minimumWidth;
			}
			set
			{
				this.m_minimumWidth = value;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000ED1E7 File Offset: 0x000EB3E7
		// (set) Token: 0x06003650 RID: 13904 RVA: 0x000ED1EF File Offset: 0x000EB3EF
		internal ExpressionInfo MaximumWidth
		{
			get
			{
				return this.m_maximumWidth;
			}
			set
			{
				this.m_maximumWidth = value;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000ED1F8 File Offset: 0x000EB3F8
		// (set) Token: 0x06003652 RID: 13906 RVA: 0x000ED200 File Offset: 0x000EB400
		internal ExpressionInfo SeriesSymbolWidth
		{
			get
			{
				return this.m_seriesSymbolWidth;
			}
			set
			{
				this.m_seriesSymbolWidth = value;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000ED209 File Offset: 0x000EB409
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x000ED211 File Offset: 0x000EB411
		internal ExpressionInfo SeriesSymbolHeight
		{
			get
			{
				return this.m_seriesSymbolHeight;
			}
			set
			{
				this.m_seriesSymbolHeight = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000ED21A File Offset: 0x000EB41A
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x000ED222 File Offset: 0x000EB422
		internal ChartLegendColumnHeader Header
		{
			get
			{
				return this.m_header;
			}
			set
			{
				this.m_header = value;
			}
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000ED22C File Offset: 0x000EB42C
		internal void SetExprHost(ChartLegendColumnExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_header != null && this.m_exprHost.HeaderHost != null)
			{
				this.m_header.SetExprHost(this.m_exprHost.HeaderHost, reportObjectModel);
			}
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000ED2BC File Offset: 0x000EB4BC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendColumnStart(this.m_name);
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_columnType != null)
			{
				this.m_columnType.Initialize("ColumnType", context);
				context.ExprHostBuilder.ChartLegendColumnColumnType(this.m_columnType);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.ChartLegendColumnValue(this.m_value);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartLegendColumnToolTip(this.m_toolTip);
			}
			if (this.m_minimumWidth != null)
			{
				this.m_minimumWidth.Initialize("MinimumWidth", context);
				context.ExprHostBuilder.ChartLegendColumnMinimumWidth(this.m_minimumWidth);
			}
			if (this.m_maximumWidth != null)
			{
				this.m_maximumWidth.Initialize("MaximumWidth", context);
				context.ExprHostBuilder.ChartLegendColumnMaximumWidth(this.m_maximumWidth);
			}
			if (this.m_seriesSymbolWidth != null)
			{
				this.m_seriesSymbolWidth.Initialize("SeriesSymbolWidth", context);
				context.ExprHostBuilder.ChartLegendColumnSeriesSymbolWidth(this.m_seriesSymbolWidth);
			}
			if (this.m_seriesSymbolHeight != null)
			{
				this.m_seriesSymbolHeight.Initialize("SeriesSymbolHeight", context);
				context.ExprHostBuilder.ChartLegendColumnSeriesSymbolHeight(this.m_seriesSymbolHeight);
			}
			if (this.m_header != null)
			{
				this.m_header.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartLegendColumnEnd();
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000ED44C File Offset: 0x000EB64C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegendColumn chartLegendColumn = (ChartLegendColumn)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartLegendColumn.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_columnType != null)
			{
				chartLegendColumn.m_columnType = (ExpressionInfo)this.m_columnType.PublishClone(context);
			}
			if (this.m_value != null)
			{
				chartLegendColumn.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartLegendColumn.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_minimumWidth != null)
			{
				chartLegendColumn.m_minimumWidth = (ExpressionInfo)this.m_minimumWidth.PublishClone(context);
			}
			if (this.m_maximumWidth != null)
			{
				chartLegendColumn.m_maximumWidth = (ExpressionInfo)this.m_maximumWidth.PublishClone(context);
			}
			if (this.m_seriesSymbolWidth != null)
			{
				chartLegendColumn.m_seriesSymbolWidth = (ExpressionInfo)this.m_seriesSymbolWidth.PublishClone(context);
			}
			if (this.m_seriesSymbolHeight != null)
			{
				chartLegendColumn.m_seriesSymbolHeight = (ExpressionInfo)this.m_seriesSymbolHeight.PublishClone(context);
			}
			if (this.m_header != null)
			{
				chartLegendColumn.m_header = (ChartLegendColumnHeader)this.m_header.PublishClone(context);
			}
			return chartLegendColumn;
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000ED580 File Offset: 0x000EB780
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumn, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.ColumnType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinimumWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SeriesSymbolWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SeriesSymbolHeight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Header, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumnHeader),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000ED695 File Offset: 0x000EB895
		internal ChartColumnType EvaluateColumnType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartColumnType(context.ReportRuntime.EvaluateChartLegendColumnColumnTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000ED6C6 File Offset: 0x000EB8C6
		internal string EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnValueExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000ED6EC File Offset: 0x000EB8EC
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnToolTipExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000ED712 File Offset: 0x000EB912
		internal string EvaluateMinimumWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnMinimumWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000ED738 File Offset: 0x000EB938
		internal string EvaluateMaximumWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnMaximumWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000ED75E File Offset: 0x000EB95E
		internal int EvaluateSeriesSymbolWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnSeriesSymbolWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000ED784 File Offset: 0x000EB984
		internal int EvaluateSeriesSymbolHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnSeriesSymbolHeightExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000ED7AC File Offset: 0x000EB9AC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegendColumn.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Value)
				{
					if (memberName == MemberName.ID)
					{
						writer.Write(this.m_id);
						continue;
					}
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_value);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.ColumnType:
						writer.Write(this.m_columnType);
						continue;
					case MemberName.ToolTip:
						writer.Write(this.m_toolTip);
						continue;
					case MemberName.MinimumWidth:
						writer.Write(this.m_minimumWidth);
						continue;
					case MemberName.MaximumWidth:
						writer.Write(this.m_maximumWidth);
						continue;
					case MemberName.SeriesSymbolWidth:
						writer.Write(this.m_seriesSymbolWidth);
						continue;
					case MemberName.SeriesSymbolHeight:
						writer.Write(this.m_seriesSymbolHeight);
						continue;
					case MemberName.Header:
						writer.Write(this.m_header);
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.Action)
						{
							writer.Write(this.m_action);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000ED910 File Offset: 0x000EBB10
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegendColumn.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Value)
				{
					if (memberName == MemberName.ID)
					{
						this.m_id = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.Value)
					{
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.ColumnType:
						this.m_columnType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ToolTip:
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MinimumWidth:
						this.m_minimumWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaximumWidth:
						this.m_maximumWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SeriesSymbolWidth:
						this.m_seriesSymbolWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SeriesSymbolHeight:
						this.m_seriesSymbolHeight = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Header:
						this.m_header = (ChartLegendColumnHeader)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.Action)
						{
							this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000EDAA6 File Offset: 0x000EBCA6
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_chart.GenerateActionOwnerID();
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000EDAC9 File Offset: 0x000EBCC9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumn;
		}

		// Token: 0x04001A82 RID: 6786
		private string m_name;

		// Token: 0x04001A83 RID: 6787
		private int m_exprHostID;

		// Token: 0x04001A84 RID: 6788
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001A85 RID: 6789
		private ExpressionInfo m_columnType;

		// Token: 0x04001A86 RID: 6790
		private ExpressionInfo m_value;

		// Token: 0x04001A87 RID: 6791
		private ExpressionInfo m_toolTip;

		// Token: 0x04001A88 RID: 6792
		private ExpressionInfo m_minimumWidth;

		// Token: 0x04001A89 RID: 6793
		private ExpressionInfo m_maximumWidth;

		// Token: 0x04001A8A RID: 6794
		private ExpressionInfo m_seriesSymbolWidth;

		// Token: 0x04001A8B RID: 6795
		private ExpressionInfo m_seriesSymbolHeight;

		// Token: 0x04001A8C RID: 6796
		private ChartLegendColumnHeader m_header;

		// Token: 0x04001A8D RID: 6797
		private int m_id;

		// Token: 0x04001A8E RID: 6798
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegendColumn.GetDeclaration();

		// Token: 0x04001A8F RID: 6799
		[NonSerialized]
		private ChartLegendColumnExprHost m_exprHost;

		// Token: 0x04001A90 RID: 6800
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;
	}
}
