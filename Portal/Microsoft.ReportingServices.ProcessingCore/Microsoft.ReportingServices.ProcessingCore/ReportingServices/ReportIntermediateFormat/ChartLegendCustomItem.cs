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
	// Token: 0x0200048B RID: 1163
	[Serializable]
	internal sealed class ChartLegendCustomItem : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06003676 RID: 13942 RVA: 0x000EDCEF File Offset: 0x000EBEEF
		internal ChartLegendCustomItem()
		{
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000EDCF7 File Offset: 0x000EBEF7
		internal ChartLegendCustomItem(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, int id)
			: base(chart)
		{
			this.m_id = id;
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000EDD07 File Offset: 0x000EBF07
		// (set) Token: 0x06003679 RID: 13945 RVA: 0x000EDD0F File Offset: 0x000EBF0F
		internal string LegendCustomItemName
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

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000EDD18 File Offset: 0x000EBF18
		internal ChartLegendCustomItemExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000EDD20 File Offset: 0x000EBF20
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000EDD28 File Offset: 0x000EBF28
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000EDD30 File Offset: 0x000EBF30
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000EDD38 File Offset: 0x000EBF38
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

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000EDD41 File Offset: 0x000EBF41
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000EDD49 File Offset: 0x000EBF49
		// (set) Token: 0x06003681 RID: 13953 RVA: 0x000EDD51 File Offset: 0x000EBF51
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

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000EDD5A File Offset: 0x000EBF5A
		// (set) Token: 0x06003683 RID: 13955 RVA: 0x000EDD62 File Offset: 0x000EBF62
		internal ChartMarker Marker
		{
			get
			{
				return this.m_marker;
			}
			set
			{
				this.m_marker = value;
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000EDD6B File Offset: 0x000EBF6B
		// (set) Token: 0x06003685 RID: 13957 RVA: 0x000EDD73 File Offset: 0x000EBF73
		internal ExpressionInfo Separator
		{
			get
			{
				return this.m_separator;
			}
			set
			{
				this.m_separator = value;
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06003686 RID: 13958 RVA: 0x000EDD7C File Offset: 0x000EBF7C
		// (set) Token: 0x06003687 RID: 13959 RVA: 0x000EDD84 File Offset: 0x000EBF84
		internal ExpressionInfo SeparatorColor
		{
			get
			{
				return this.m_separatorColor;
			}
			set
			{
				this.m_separatorColor = value;
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000EDD8D File Offset: 0x000EBF8D
		// (set) Token: 0x06003689 RID: 13961 RVA: 0x000EDD95 File Offset: 0x000EBF95
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

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x000EDD9E File Offset: 0x000EBF9E
		// (set) Token: 0x0600368B RID: 13963 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
		internal List<ChartLegendCustomItemCell> LegendCustomItemCells
		{
			get
			{
				return this.m_chartLegendCustomItemCells;
			}
			set
			{
				this.m_chartLegendCustomItemCells = value;
			}
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000EDDB0 File Offset: 0x000EBFB0
		internal void SetExprHost(ChartLegendCustomItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_marker != null && this.m_exprHost.ChartMarkerHost != null)
			{
				this.m_marker.SetExprHost(this.m_exprHost.ChartMarkerHost, reportObjectModel);
			}
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
			IList<ChartLegendCustomItemCellExprHost> chartLegendCustomItemCellsHostsRemotable = this.m_exprHost.ChartLegendCustomItemCellsHostsRemotable;
			if (this.m_chartLegendCustomItemCells != null && chartLegendCustomItemCellsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_chartLegendCustomItemCells.Count; i++)
				{
					ChartLegendCustomItemCell chartLegendCustomItemCell = this.m_chartLegendCustomItemCells[i];
					if (chartLegendCustomItemCell != null && chartLegendCustomItemCell.ExpressionHostID > -1)
					{
						chartLegendCustomItemCell.SetExprHost(chartLegendCustomItemCellsHostsRemotable[chartLegendCustomItemCell.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000EDE98 File Offset: 0x000EC098
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendCustomItemStart(this.m_name);
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_marker != null)
			{
				this.m_marker.Initialize(context);
			}
			if (this.m_separator != null)
			{
				this.m_separator.Initialize("Separator", context);
				context.ExprHostBuilder.ChartLegendCustomItemSeparator(this.m_separator);
			}
			if (this.m_separatorColor != null)
			{
				this.m_separatorColor.Initialize("SeparatorColor", context);
				context.ExprHostBuilder.ChartLegendCustomItemSeparatorColor(this.m_separatorColor);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartLegendCustomItemToolTip(this.m_toolTip);
			}
			if (this.m_chartLegendCustomItemCells != null)
			{
				for (int i = 0; i < this.m_chartLegendCustomItemCells.Count; i++)
				{
					this.m_chartLegendCustomItemCells[i].Initialize(context, i);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartLegendCustomItemEnd();
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000EDFAC File Offset: 0x000EC1AC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegendCustomItem chartLegendCustomItem = (ChartLegendCustomItem)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartLegendCustomItem.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_marker != null)
			{
				chartLegendCustomItem.m_marker = (ChartMarker)this.m_marker.PublishClone(context);
			}
			if (this.m_separator != null)
			{
				chartLegendCustomItem.m_separator = (ExpressionInfo)this.m_separator.PublishClone(context);
			}
			if (this.m_separatorColor != null)
			{
				chartLegendCustomItem.m_separatorColor = (ExpressionInfo)this.m_separatorColor.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartLegendCustomItem.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_chartLegendCustomItemCells != null)
			{
				chartLegendCustomItem.m_chartLegendCustomItemCells = new List<ChartLegendCustomItemCell>(this.m_chartLegendCustomItemCells.Count);
				foreach (ChartLegendCustomItemCell chartLegendCustomItemCell in this.m_chartLegendCustomItemCells)
				{
					chartLegendCustomItem.m_chartLegendCustomItemCells.Add((ChartLegendCustomItemCell)chartLegendCustomItemCell.PublishClone(context));
				}
			}
			return chartLegendCustomItem;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000EE0D8 File Offset: 0x000EC2D8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Marker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker),
				new MemberInfo(MemberName.Separator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SeparatorColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartLegendCustomItemCells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItemCell),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000EE1B2 File Offset: 0x000EC3B2
		internal ChartSeparators EvaluateSeparator(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartSeparator(context.ReportRuntime.EvaluateChartLegendCustomItemSeparatorExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000EE1E3 File Offset: 0x000EC3E3
		internal string EvaluateSeparatorColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemSeparatorColorExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000EE209 File Offset: 0x000EC409
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemToolTipExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000EE230 File Offset: 0x000EC430
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegendCustomItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
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
					if (memberName == MemberName.ToolTip)
					{
						writer.Write(this.m_toolTip);
						continue;
					}
				}
				else if (memberName <= MemberName.ChartLegendCustomItemCells)
				{
					switch (memberName)
					{
					case MemberName.Marker:
						writer.Write(this.m_marker);
						continue;
					case MemberName.Separator:
						writer.Write(this.m_separator);
						continue;
					case MemberName.SeparatorColor:
						writer.Write(this.m_separatorColor);
						continue;
					default:
						if (memberName == MemberName.ChartLegendCustomItemCells)
						{
							writer.Write<ChartLegendCustomItemCell>(this.m_chartLegendCustomItemCells);
							continue;
						}
						break;
					}
				}
				else
				{
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000EE36C File Offset: 0x000EC56C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegendCustomItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
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
					if (memberName == MemberName.ToolTip)
					{
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.ChartLegendCustomItemCells)
				{
					switch (memberName)
					{
					case MemberName.Marker:
						this.m_marker = (ChartMarker)reader.ReadRIFObject();
						continue;
					case MemberName.Separator:
						this.m_separator = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SeparatorColor:
						this.m_separatorColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ChartLegendCustomItemCells)
						{
							this.m_chartLegendCustomItemCells = reader.ReadGenericListOfRIFObjects<ChartLegendCustomItemCell>();
							continue;
						}
						break;
					}
				}
				else
				{
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
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000EE4C3 File Offset: 0x000EC6C3
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_chart.GenerateActionOwnerID();
			}
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000EE4E6 File Offset: 0x000EC6E6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItem;
		}

		// Token: 0x04001A94 RID: 6804
		private string m_name;

		// Token: 0x04001A95 RID: 6805
		private int m_exprHostID;

		// Token: 0x04001A96 RID: 6806
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001A97 RID: 6807
		private ChartMarker m_marker;

		// Token: 0x04001A98 RID: 6808
		private ExpressionInfo m_separator;

		// Token: 0x04001A99 RID: 6809
		private ExpressionInfo m_separatorColor;

		// Token: 0x04001A9A RID: 6810
		private ExpressionInfo m_toolTip;

		// Token: 0x04001A9B RID: 6811
		private List<ChartLegendCustomItemCell> m_chartLegendCustomItemCells;

		// Token: 0x04001A9C RID: 6812
		private int m_id;

		// Token: 0x04001A9D RID: 6813
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001A9E RID: 6814
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegendCustomItem.GetDeclaration();

		// Token: 0x04001A9F RID: 6815
		[NonSerialized]
		private ChartLegendCustomItemExprHost m_exprHost;
	}
}
