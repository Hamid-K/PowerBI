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
	// Token: 0x0200048C RID: 1164
	[Serializable]
	internal sealed class ChartLegendCustomItemCell : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x000EE4F9 File Offset: 0x000EC6F9
		internal ChartLegendCustomItemCell()
		{
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000EE501 File Offset: 0x000EC701
		internal ChartLegendCustomItemCell(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, int id)
			: base(chart)
		{
			this.m_id = id;
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000EE511 File Offset: 0x000EC711
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000EE519 File Offset: 0x000EC719
		internal string LegendCustomItemCellName
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

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000EE522 File Offset: 0x000EC722
		internal ChartLegendCustomItemCellExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000EE52A File Offset: 0x000EC72A
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000EE532 File Offset: 0x000EC732
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000EE53A File Offset: 0x000EC73A
		// (set) Token: 0x060036A0 RID: 13984 RVA: 0x000EE542 File Offset: 0x000EC742
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

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000EE54B File Offset: 0x000EC74B
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000EE553 File Offset: 0x000EC753
		// (set) Token: 0x060036A3 RID: 13987 RVA: 0x000EE55B File Offset: 0x000EC75B
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

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000EE564 File Offset: 0x000EC764
		// (set) Token: 0x060036A5 RID: 13989 RVA: 0x000EE56C File Offset: 0x000EC76C
		internal ExpressionInfo CellType
		{
			get
			{
				return this.m_cellType;
			}
			set
			{
				this.m_cellType = value;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000EE575 File Offset: 0x000EC775
		// (set) Token: 0x060036A7 RID: 13991 RVA: 0x000EE57D File Offset: 0x000EC77D
		internal ExpressionInfo Text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				this.m_text = value;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000EE586 File Offset: 0x000EC786
		// (set) Token: 0x060036A9 RID: 13993 RVA: 0x000EE58E File Offset: 0x000EC78E
		internal ExpressionInfo CellSpan
		{
			get
			{
				return this.m_cellSpan;
			}
			set
			{
				this.m_cellSpan = value;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000EE597 File Offset: 0x000EC797
		// (set) Token: 0x060036AB RID: 13995 RVA: 0x000EE59F File Offset: 0x000EC79F
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

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x000EE5A8 File Offset: 0x000EC7A8
		// (set) Token: 0x060036AD RID: 13997 RVA: 0x000EE5B0 File Offset: 0x000EC7B0
		internal ExpressionInfo ImageWidth
		{
			get
			{
				return this.m_imageWidth;
			}
			set
			{
				this.m_imageWidth = value;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000EE5B9 File Offset: 0x000EC7B9
		// (set) Token: 0x060036AF RID: 13999 RVA: 0x000EE5C1 File Offset: 0x000EC7C1
		internal ExpressionInfo ImageHeight
		{
			get
			{
				return this.m_imageHeight;
			}
			set
			{
				this.m_imageHeight = value;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060036B0 RID: 14000 RVA: 0x000EE5CA File Offset: 0x000EC7CA
		// (set) Token: 0x060036B1 RID: 14001 RVA: 0x000EE5D2 File Offset: 0x000EC7D2
		internal ExpressionInfo SymbolHeight
		{
			get
			{
				return this.m_symbolHeight;
			}
			set
			{
				this.m_symbolHeight = value;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x000EE5DB File Offset: 0x000EC7DB
		// (set) Token: 0x060036B3 RID: 14003 RVA: 0x000EE5E3 File Offset: 0x000EC7E3
		internal ExpressionInfo SymbolWidth
		{
			get
			{
				return this.m_symbolWidth;
			}
			set
			{
				this.m_symbolWidth = value;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000EE5EC File Offset: 0x000EC7EC
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x000EE5F4 File Offset: 0x000EC7F4
		internal ExpressionInfo Alignment
		{
			get
			{
				return this.m_alignment;
			}
			set
			{
				this.m_alignment = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000EE5FD File Offset: 0x000EC7FD
		// (set) Token: 0x060036B7 RID: 14007 RVA: 0x000EE605 File Offset: 0x000EC805
		internal ExpressionInfo TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000EE60E File Offset: 0x000EC80E
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x000EE616 File Offset: 0x000EC816
		internal ExpressionInfo BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000EE61F File Offset: 0x000EC81F
		// (set) Token: 0x060036BB RID: 14011 RVA: 0x000EE627 File Offset: 0x000EC827
		internal ExpressionInfo LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000EE630 File Offset: 0x000EC830
		// (set) Token: 0x060036BD RID: 14013 RVA: 0x000EE638 File Offset: 0x000EC838
		internal ExpressionInfo RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000EE644 File Offset: 0x000EC844
		internal void SetExprHost(ChartLegendCustomItemCellExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000EE6A8 File Offset: 0x000EC8A8
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.ChartLegendCustomItemCellStart(this.m_name);
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_cellType != null)
			{
				this.m_cellType.Initialize("CellType", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellCellType(this.m_cellType);
			}
			if (this.m_text != null)
			{
				this.m_text.Initialize("Text", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellText(this.m_text);
			}
			if (this.m_cellSpan != null)
			{
				this.m_cellSpan.Initialize("CellSpan", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellCellSpan(this.m_cellSpan);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellToolTip(this.m_toolTip);
			}
			if (this.m_imageWidth != null)
			{
				this.m_imageWidth.Initialize("ImageWidth", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellImageWidth(this.m_imageWidth);
			}
			if (this.m_imageHeight != null)
			{
				this.m_imageHeight.Initialize("ImageHeight", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellImageHeight(this.m_imageHeight);
			}
			if (this.m_symbolHeight != null)
			{
				this.m_symbolHeight.Initialize("SymbolHeight", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellSymbolHeight(this.m_symbolHeight);
			}
			if (this.m_symbolWidth != null)
			{
				this.m_symbolWidth.Initialize("SymbolWidth", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellSymbolWidth(this.m_symbolWidth);
			}
			if (this.m_alignment != null)
			{
				this.m_alignment.Initialize("Alignment", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellAlignment(this.m_alignment);
			}
			if (this.m_topMargin != null)
			{
				this.m_topMargin.Initialize("TopMargin", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellTopMargin(this.m_topMargin);
			}
			if (this.m_bottomMargin != null)
			{
				this.m_bottomMargin.Initialize("BottomMargin", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellBottomMargin(this.m_bottomMargin);
			}
			if (this.m_leftMargin != null)
			{
				this.m_leftMargin.Initialize("LeftMargin", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellLeftMargin(this.m_leftMargin);
			}
			if (this.m_rightMargin != null)
			{
				this.m_rightMargin.Initialize("RightMargin", context);
				context.ExprHostBuilder.ChartLegendCustomItemCellRightMargin(this.m_rightMargin);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartLegendCustomItemCellEnd();
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000EE924 File Offset: 0x000ECB24
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegendCustomItemCell chartLegendCustomItemCell = (ChartLegendCustomItemCell)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartLegendCustomItemCell.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_cellType != null)
			{
				chartLegendCustomItemCell.m_cellType = (ExpressionInfo)this.m_cellType.PublishClone(context);
			}
			if (this.m_text != null)
			{
				chartLegendCustomItemCell.m_text = (ExpressionInfo)this.m_text.PublishClone(context);
			}
			if (this.m_cellSpan != null)
			{
				chartLegendCustomItemCell.m_cellSpan = (ExpressionInfo)this.m_cellSpan.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartLegendCustomItemCell.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_imageWidth != null)
			{
				chartLegendCustomItemCell.m_imageWidth = (ExpressionInfo)this.m_imageWidth.PublishClone(context);
			}
			if (this.m_imageHeight != null)
			{
				chartLegendCustomItemCell.m_imageHeight = (ExpressionInfo)this.m_imageHeight.PublishClone(context);
			}
			if (this.m_symbolHeight != null)
			{
				chartLegendCustomItemCell.m_symbolHeight = (ExpressionInfo)this.m_symbolHeight.PublishClone(context);
			}
			if (this.m_symbolWidth != null)
			{
				chartLegendCustomItemCell.m_symbolWidth = (ExpressionInfo)this.m_symbolWidth.PublishClone(context);
			}
			if (this.m_alignment != null)
			{
				chartLegendCustomItemCell.m_alignment = (ExpressionInfo)this.m_alignment.PublishClone(context);
			}
			if (this.m_topMargin != null)
			{
				chartLegendCustomItemCell.m_topMargin = (ExpressionInfo)this.m_topMargin.PublishClone(context);
			}
			if (this.m_bottomMargin != null)
			{
				chartLegendCustomItemCell.m_bottomMargin = (ExpressionInfo)this.m_bottomMargin.PublishClone(context);
			}
			if (this.m_leftMargin != null)
			{
				chartLegendCustomItemCell.m_leftMargin = (ExpressionInfo)this.m_leftMargin.PublishClone(context);
			}
			if (this.m_rightMargin != null)
			{
				chartLegendCustomItemCell.m_rightMargin = (ExpressionInfo)this.m_rightMargin.PublishClone(context);
			}
			return chartLegendCustomItemCell;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000EEAF4 File Offset: 0x000ECCF4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItemCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.CellType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Text, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CellSpan, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ImageWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ImageHeight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SymbolHeight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SymbolWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Alignment, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TopMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BottomMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LeftMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RightMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000EEC75 File Offset: 0x000ECE75
		internal ChartCellType EvaluateCellType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartCellType(context.ReportRuntime.EvaluateChartLegendCustomItemCellCellTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000EECA6 File Offset: 0x000ECEA6
		internal string EvaluateText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellTextExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000EECCC File Offset: 0x000ECECC
		internal int EvaluateCellSpan(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellCellSpanExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000EECF2 File Offset: 0x000ECEF2
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellToolTipExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000EED18 File Offset: 0x000ECF18
		internal int EvaluateImageWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellImageWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000EED3E File Offset: 0x000ECF3E
		internal int EvaluateImageHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellImageHeightExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000EED64 File Offset: 0x000ECF64
		internal int EvaluateSymbolHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellSymbolHeightExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000EED8A File Offset: 0x000ECF8A
		internal int EvaluateSymbolWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellSymbolWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000EEDB0 File Offset: 0x000ECFB0
		internal ChartCellAlignment EvaluateAlignment(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartCellAlignment(context.ReportRuntime.EvaluateChartLegendCustomItemCellAlignmentExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000EEDE1 File Offset: 0x000ECFE1
		internal int EvaluateTopMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellTopMarginExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000EEE07 File Offset: 0x000ED007
		internal int EvaluateBottomMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellBottomMarginExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000EEE2D File Offset: 0x000ED02D
		internal int EvaluateLeftMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellLeftMarginExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000EEE53 File Offset: 0x000ED053
		internal int EvaluateRightMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendCustomItemCellRightMarginExpression(this, this.m_chart.Name);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000EEE7C File Offset: 0x000ED07C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegendCustomItemCell.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.BottomMargin)
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
					switch (memberName)
					{
					case MemberName.LeftMargin:
						writer.Write(this.m_leftMargin);
						continue;
					case MemberName.RightMargin:
						writer.Write(this.m_rightMargin);
						continue;
					case MemberName.TopMargin:
						writer.Write(this.m_topMargin);
						continue;
					case MemberName.BottomMargin:
						writer.Write(this.m_bottomMargin);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.CellType:
						writer.Write(this.m_cellType);
						continue;
					case MemberName.Text:
						writer.Write(this.m_text);
						continue;
					case MemberName.CellSpan:
						writer.Write(this.m_cellSpan);
						continue;
					case MemberName.ImageWidth:
						writer.Write(this.m_imageWidth);
						continue;
					case MemberName.ImageHeight:
						writer.Write(this.m_imageHeight);
						continue;
					case MemberName.SymbolHeight:
						writer.Write(this.m_symbolHeight);
						continue;
					case MemberName.SymbolWidth:
						writer.Write(this.m_symbolWidth);
						continue;
					case MemberName.Alignment:
						writer.Write(this.m_alignment);
						continue;
					case MemberName.ColumnType:
						break;
					case MemberName.ToolTip:
						writer.Write(this.m_toolTip);
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

		// Token: 0x060036D0 RID: 14032 RVA: 0x000EF068 File Offset: 0x000ED268
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegendCustomItemCell.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.BottomMargin)
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
					switch (memberName)
					{
					case MemberName.LeftMargin:
						this.m_leftMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.RightMargin:
						this.m_rightMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TopMargin:
						this.m_topMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.BottomMargin:
						this.m_bottomMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.CellType:
						this.m_cellType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Text:
						this.m_text = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CellSpan:
						this.m_cellSpan = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ImageWidth:
						this.m_imageWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ImageHeight:
						this.m_imageHeight = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SymbolHeight:
						this.m_symbolHeight = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SymbolWidth:
						this.m_symbolWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Alignment:
						this.m_alignment = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ColumnType:
						break;
					case MemberName.ToolTip:
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
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

		// Token: 0x060036D1 RID: 14033 RVA: 0x000EF29D File Offset: 0x000ED49D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_chart.GenerateActionOwnerID();
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000EF2C0 File Offset: 0x000ED4C0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItemCell;
		}

		// Token: 0x04001AA0 RID: 6816
		private string m_name;

		// Token: 0x04001AA1 RID: 6817
		private int m_exprHostID;

		// Token: 0x04001AA2 RID: 6818
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001AA3 RID: 6819
		private ExpressionInfo m_cellType;

		// Token: 0x04001AA4 RID: 6820
		private ExpressionInfo m_text;

		// Token: 0x04001AA5 RID: 6821
		private ExpressionInfo m_cellSpan;

		// Token: 0x04001AA6 RID: 6822
		private ExpressionInfo m_toolTip;

		// Token: 0x04001AA7 RID: 6823
		private ExpressionInfo m_imageWidth;

		// Token: 0x04001AA8 RID: 6824
		private ExpressionInfo m_imageHeight;

		// Token: 0x04001AA9 RID: 6825
		private ExpressionInfo m_symbolHeight;

		// Token: 0x04001AAA RID: 6826
		private ExpressionInfo m_symbolWidth;

		// Token: 0x04001AAB RID: 6827
		private ExpressionInfo m_alignment;

		// Token: 0x04001AAC RID: 6828
		private ExpressionInfo m_topMargin;

		// Token: 0x04001AAD RID: 6829
		private ExpressionInfo m_bottomMargin;

		// Token: 0x04001AAE RID: 6830
		private ExpressionInfo m_leftMargin;

		// Token: 0x04001AAF RID: 6831
		private ExpressionInfo m_rightMargin;

		// Token: 0x04001AB0 RID: 6832
		private int m_id;

		// Token: 0x04001AB1 RID: 6833
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegendCustomItemCell.GetDeclaration();

		// Token: 0x04001AB2 RID: 6834
		[NonSerialized]
		private ChartLegendCustomItemCellExprHost m_exprHost;

		// Token: 0x04001AB3 RID: 6835
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;
	}
}
