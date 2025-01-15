using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000559 RID: 1369
	internal class ProcessingRIFObjectCreator : IRIFObjectCreator
	{
		// Token: 0x06004A24 RID: 18980 RVA: 0x001383CE File Offset: 0x001365CE
		internal ProcessingRIFObjectCreator(IDOwner parentIDOwner, ReportItem parentReportItem)
		{
			this.m_parentIDOwner = parentIDOwner;
			this.m_parentReportItem = parentReportItem;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x001383E4 File Offset: 0x001365E4
		public IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context)
		{
			IPersistable persistable = null;
			if (objectType == ObjectType.Null)
			{
				return null;
			}
			IDOwner parentIDOwner = this.m_parentIDOwner;
			ReportItem parentReportItem = this.m_parentReportItem;
			switch (objectType)
			{
			case ObjectType.Report:
				persistable = new Report(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.PageSection:
				persistable = new PageSection(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.Line:
				persistable = new Line(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.Rectangle:
				persistable = new Rectangle(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.Image:
				persistable = new Image(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.TextBox:
				persistable = new TextBox(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.SubReport:
				persistable = new SubReport(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.Grouping:
				persistable = new Grouping(ConstructionPhase.Deserializing);
				goto IL_0FD1;
			case ObjectType.Sorting:
				persistable = new Sorting(ConstructionPhase.Deserializing);
				goto IL_0FD1;
			case ObjectType.ReportItemCollection:
				persistable = new ReportItemCollection();
				goto IL_0FD1;
			case ObjectType.ReportItemIndexer:
				persistable = default(ReportItemIndexer);
				goto IL_0FD1;
			case ObjectType.Style:
				persistable = new Style(ConstructionPhase.Deserializing);
				goto IL_0FD1;
			case ObjectType.AttributeInfo:
				persistable = new AttributeInfo();
				goto IL_0FD1;
			case ObjectType.Visibility:
				persistable = new Visibility();
				goto IL_0FD1;
			case ObjectType.ExpressionInfo:
				persistable = new ExpressionInfo();
				goto IL_0FD1;
			case ObjectType.DataAggregateInfo:
				persistable = new DataAggregateInfo();
				goto IL_0FD1;
			case ObjectType.RunningValueInfo:
				persistable = new RunningValueInfo();
				goto IL_0FD1;
			case ObjectType.Filter:
				persistable = new Filter();
				goto IL_0FD1;
			case ObjectType.DataSource:
				persistable = new DataSource();
				goto IL_0FD1;
			case ObjectType.DataSet:
				persistable = new DataSet();
				goto IL_0FD1;
			case ObjectType.ReportQuery:
				persistable = new ReportQuery();
				goto IL_0FD1;
			case ObjectType.Field:
				persistable = new Field();
				goto IL_0FD1;
			case ObjectType.ParameterValue:
				persistable = new ParameterValue();
				goto IL_0FD1;
			case ObjectType.ReportSnapshot:
				persistable = new ReportSnapshot();
				goto IL_0FD1;
			case ObjectType.DocumentMapNode:
				persistable = new DocumentMapNode();
				goto IL_0FD1;
			case ObjectType.ReportInstance:
				persistable = new ReportInstance();
				goto IL_0FD1;
			case ObjectType.ParameterInfo:
				persistable = new ParameterInfo();
				goto IL_0FD1;
			case ObjectType.ValidValue:
				persistable = new ValidValue();
				goto IL_0FD1;
			case ObjectType.ParameterDataSource:
				persistable = new ParameterDataSource();
				goto IL_0FD1;
			case ObjectType.ParameterDef:
				persistable = new ParameterDef();
				goto IL_0FD1;
			case ObjectType.ProcessingMessage:
				persistable = new ProcessingMessage();
				goto IL_0FD1;
			case ObjectType.CodeClass:
				persistable = default(CodeClass);
				goto IL_0FD1;
			case ObjectType.Action:
				persistable = new Microsoft.ReportingServices.ReportIntermediateFormat.Action();
				goto IL_0FD1;
			case ObjectType.RenderingPagesRanges:
				persistable = default(RenderingPagesRanges);
				goto IL_0FD1;
			case ObjectType.IntermediateFormatVersion:
				persistable = new IntermediateFormatVersion();
				goto IL_0FD1;
			case ObjectType.ImageInfo:
				persistable = new ImageInfo();
				goto IL_0FD1;
			case ObjectType.ActionItem:
				persistable = new ActionItem();
				goto IL_0FD1;
			case ObjectType.DataValue:
				persistable = new DataValue();
				goto IL_0FD1;
			case ObjectType.CustomReportItem:
				persistable = new CustomReportItem(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.SortFilterEventInfo:
				persistable = new SortFilterEventInfo();
				goto IL_0FD1;
			case ObjectType.SortFilterEventInfoMap:
				persistable = new SortFilterEventInfoMap();
				goto IL_0FD1;
			case ObjectType.EndUserSort:
				persistable = new EndUserSort();
				goto IL_0FD1;
			case ObjectType.ScopeLookupTable:
				persistable = new ScopeLookupTable();
				goto IL_0FD1;
			case ObjectType.Tablix:
				persistable = new Tablix(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.TablixHeader:
				persistable = new TablixHeader();
				goto IL_0FD1;
			case ObjectType.TablixMember:
				persistable = new TablixMember();
				goto IL_0FD1;
			case ObjectType.TablixColumn:
				persistable = new TablixColumn();
				goto IL_0FD1;
			case ObjectType.TablixRow:
				persistable = new TablixRow();
				goto IL_0FD1;
			case ObjectType.TablixCornerCell:
				persistable = new TablixCornerCell();
				goto IL_0FD1;
			case ObjectType.TablixCell:
				persistable = new TablixCell();
				goto IL_0FD1;
			case ObjectType.Chart:
				persistable = new Chart(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.ChartMember:
				persistable = new ChartMember();
				goto IL_0FD1;
			case ObjectType.ChartSeries:
				persistable = new ChartSeries();
				goto IL_0FD1;
			case ObjectType.ChartDataPoint:
				persistable = new ChartDataPoint();
				goto IL_0FD1;
			case ObjectType.ChartAxis:
				persistable = new ChartAxis();
				goto IL_0FD1;
			case ObjectType.ThreeDProperties:
				persistable = new ChartThreeDProperties();
				goto IL_0FD1;
			case ObjectType.ChartDataLabel:
				persistable = new ChartDataLabel();
				goto IL_0FD1;
			case ObjectType.ChartDataPointValues:
				persistable = new ChartDataPointValues();
				goto IL_0FD1;
			case ObjectType.ChartArea:
				persistable = new ChartArea();
				goto IL_0FD1;
			case ObjectType.ChartTitle:
				persistable = new ChartTitle();
				goto IL_0FD1;
			case ObjectType.ChartAxisTitle:
				persistable = new ChartAxisTitle();
				goto IL_0FD1;
			case ObjectType.ChartLegendTitle:
				persistable = new ChartLegendTitle();
				goto IL_0FD1;
			case ObjectType.ChartLegend:
				persistable = new ChartLegend();
				goto IL_0FD1;
			case ObjectType.ChartBorderSkin:
				persistable = new ChartBorderSkin();
				goto IL_0FD1;
			case ObjectType.ChartTickMarks:
				persistable = new ChartTickMarks();
				goto IL_0FD1;
			case ObjectType.ChartNoDataMessage:
				persistable = new ChartNoDataMessage();
				goto IL_0FD1;
			case ObjectType.ChartCustomPaletteColor:
				persistable = new ChartCustomPaletteColor();
				goto IL_0FD1;
			case ObjectType.ChartLegendColumn:
				persistable = new ChartLegendColumn();
				goto IL_0FD1;
			case ObjectType.ChartLegendColumnHeader:
				persistable = new ChartLegendColumnHeader();
				goto IL_0FD1;
			case ObjectType.ChartLegendCustomItem:
				persistable = new ChartLegendCustomItem();
				goto IL_0FD1;
			case ObjectType.ChartLegendCustomItemCell:
				persistable = new ChartLegendCustomItemCell();
				goto IL_0FD1;
			case ObjectType.ChartStripLine:
				persistable = new ChartStripLine();
				goto IL_0FD1;
			case ObjectType.ChartAxisScaleBreak:
				persistable = new ChartAxisScaleBreak();
				goto IL_0FD1;
			case ObjectType.ChartDerivedSeries:
				persistable = new ChartDerivedSeries();
				goto IL_0FD1;
			case ObjectType.ChartFormulaParameter:
				persistable = new ChartFormulaParameter();
				goto IL_0FD1;
			case ObjectType.ChartEmptyPoints:
				persistable = new ChartEmptyPoints();
				goto IL_0FD1;
			case ObjectType.ChartItemInLegend:
				persistable = new ChartItemInLegend();
				goto IL_0FD1;
			case ObjectType.ChartSmartLabel:
				persistable = new ChartSmartLabel();
				goto IL_0FD1;
			case ObjectType.ChartNoMoveDirections:
				persistable = new ChartNoMoveDirections();
				goto IL_0FD1;
			case ObjectType.GridLines:
				persistable = new ChartGridLines();
				goto IL_0FD1;
			case ObjectType.DataMember:
				persistable = new DataMember();
				goto IL_0FD1;
			case ObjectType.CustomDataRow:
				persistable = new CustomDataRow();
				goto IL_0FD1;
			case ObjectType.DataCell:
				persistable = new DataCell();
				goto IL_0FD1;
			case ObjectType.Variable:
				persistable = new Variable();
				goto IL_0FD1;
			case ObjectType.ExpressionInfoTypeValuePair:
				persistable = new ExpressionInfoTypeValuePair();
				goto IL_0FD1;
			case ObjectType.Page:
				persistable = new Page();
				goto IL_0FD1;
			case ObjectType.DocumentMapBeginContainer:
				persistable = DocumentMapBeginContainer.Instance;
				goto IL_0FD1;
			case ObjectType.DocumentMapEndContainer:
				persistable = DocumentMapEndContainer.Instance;
				goto IL_0FD1;
			case ObjectType.ChartMarker:
				persistable = new ChartMarker();
				goto IL_0FD1;
			case ObjectType.ChartAlignType:
				persistable = new ChartAlignType();
				goto IL_0FD1;
			case ObjectType.GaugePanel:
				persistable = new GaugePanel(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.GaugeMember:
				persistable = new GaugeMember();
				goto IL_0FD1;
			case ObjectType.GaugeRow:
				persistable = new GaugeRow();
				goto IL_0FD1;
			case ObjectType.GaugeCell:
				persistable = new GaugeCell();
				goto IL_0FD1;
			case ObjectType.FrameBackground:
				persistable = new FrameBackground();
				goto IL_0FD1;
			case ObjectType.IndicatorImage:
				persistable = new IndicatorImage();
				goto IL_0FD1;
			case ObjectType.PointerImage:
				persistable = new PointerImage();
				goto IL_0FD1;
			case ObjectType.CapImage:
				persistable = new CapImage();
				goto IL_0FD1;
			case ObjectType.FrameImage:
				persistable = new FrameImage();
				goto IL_0FD1;
			case ObjectType.CustomLabel:
				persistable = new CustomLabel();
				goto IL_0FD1;
			case ObjectType.RadialGauge:
				persistable = new RadialGauge();
				goto IL_0FD1;
			case ObjectType.LinearGauge:
				persistable = new LinearGauge();
				goto IL_0FD1;
			case ObjectType.GaugeImage:
				persistable = new GaugeImage();
				goto IL_0FD1;
			case ObjectType.GaugeLabel:
				persistable = new GaugeLabel();
				goto IL_0FD1;
			case ObjectType.GaugePanelItem:
				persistable = new GaugePanelItem();
				goto IL_0FD1;
			case ObjectType.RadialPointer:
				persistable = new RadialPointer();
				goto IL_0FD1;
			case ObjectType.LinearPointer:
				persistable = new LinearPointer();
				goto IL_0FD1;
			case ObjectType.RadialScale:
				persistable = new RadialScale();
				goto IL_0FD1;
			case ObjectType.LinearScale:
				persistable = new LinearScale();
				goto IL_0FD1;
			case ObjectType.GaugeTickMarks:
				persistable = new GaugeTickMarks();
				goto IL_0FD1;
			case ObjectType.TickMarkStyle:
				persistable = new TickMarkStyle();
				goto IL_0FD1;
			case ObjectType.ScalePin:
				persistable = new ScalePin();
				goto IL_0FD1;
			case ObjectType.GaugeInputValue:
				persistable = new GaugeInputValue();
				goto IL_0FD1;
			case ObjectType.NumericIndicator:
				persistable = new NumericIndicator();
				goto IL_0FD1;
			case ObjectType.PinLabel:
				persistable = new PinLabel();
				goto IL_0FD1;
			case ObjectType.PointerCap:
				persistable = new PointerCap();
				goto IL_0FD1;
			case ObjectType.ScaleLabels:
				persistable = new ScaleLabels();
				goto IL_0FD1;
			case ObjectType.ScaleRange:
				persistable = new ScaleRange();
				goto IL_0FD1;
			case ObjectType.StateIndicator:
				persistable = new StateIndicator();
				goto IL_0FD1;
			case ObjectType.BackFrame:
				persistable = new BackFrame();
				goto IL_0FD1;
			case ObjectType.TopImage:
				persistable = new TopImage();
				goto IL_0FD1;
			case ObjectType.Thermometer:
				persistable = new Thermometer();
				goto IL_0FD1;
			case ObjectType.ChartElementPosition:
				persistable = new ChartElementPosition();
				goto IL_0FD1;
			case ObjectType.Paragraph:
				persistable = new Paragraph();
				goto IL_0FD1;
			case ObjectType.TextRun:
				persistable = new TextRun();
				goto IL_0FD1;
			case ObjectType.LookupInfo:
				persistable = new LookupInfo();
				goto IL_0FD1;
			case ObjectType.LookupDestinationInfo:
				persistable = new LookupDestinationInfo();
				goto IL_0FD1;
			case ObjectType.ReportSection:
				persistable = new ReportSection();
				goto IL_0FD1;
			case ObjectType.Map:
				persistable = new Map(this.m_parentReportItem);
				this.m_parentReportItem = (ReportItem)persistable;
				goto IL_0FD1;
			case ObjectType.MapDataRegion:
				persistable = new MapDataRegion(this.m_parentReportItem);
				goto IL_0FD1;
			case ObjectType.MapMember:
				persistable = new MapMember();
				goto IL_0FD1;
			case ObjectType.MapRow:
				persistable = new MapRow();
				goto IL_0FD1;
			case ObjectType.MapCell:
				persistable = new MapCell();
				goto IL_0FD1;
			case ObjectType.MapLocation:
				persistable = new MapLocation();
				goto IL_0FD1;
			case ObjectType.MapSize:
				persistable = new MapSize();
				goto IL_0FD1;
			case ObjectType.MapGridLines:
				persistable = new MapGridLines();
				goto IL_0FD1;
			case ObjectType.MapBindingFieldPair:
				persistable = new MapBindingFieldPair();
				goto IL_0FD1;
			case ObjectType.MapViewport:
				persistable = new MapViewport();
				goto IL_0FD1;
			case ObjectType.MapLimits:
				persistable = new MapLimits();
				goto IL_0FD1;
			case ObjectType.MapColorScale:
				persistable = new MapColorScale();
				goto IL_0FD1;
			case ObjectType.MapColorScaleTitle:
				persistable = new MapColorScaleTitle();
				goto IL_0FD1;
			case ObjectType.MapDistanceScale:
				persistable = new MapDistanceScale();
				goto IL_0FD1;
			case ObjectType.MapTitle:
				persistable = new MapTitle();
				goto IL_0FD1;
			case ObjectType.MapLegend:
				persistable = new MapLegend();
				goto IL_0FD1;
			case ObjectType.MapLegendTitle:
				persistable = new MapLegendTitle();
				goto IL_0FD1;
			case ObjectType.MapBucket:
				persistable = new MapBucket();
				goto IL_0FD1;
			case ObjectType.MapColorPaletteRule:
				persistable = new MapColorPaletteRule();
				goto IL_0FD1;
			case ObjectType.MapColorRangeRule:
				persistable = new MapColorRangeRule();
				goto IL_0FD1;
			case ObjectType.MapLineRules:
				persistable = new MapLineRules();
				goto IL_0FD1;
			case ObjectType.MapPolygonRules:
				persistable = new MapPolygonRules();
				goto IL_0FD1;
			case ObjectType.MapSizeRule:
				persistable = new MapSizeRule();
				goto IL_0FD1;
			case ObjectType.MapMarkerImage:
				persistable = new MapMarkerImage();
				goto IL_0FD1;
			case ObjectType.MapMarker:
				persistable = new MapMarker();
				goto IL_0FD1;
			case ObjectType.MapMarkerRule:
				persistable = new MapMarkerRule();
				goto IL_0FD1;
			case ObjectType.MapPointRules:
				persistable = new MapPointRules();
				goto IL_0FD1;
			case ObjectType.MapCustomColor:
				persistable = new MapCustomColor();
				goto IL_0FD1;
			case ObjectType.MapCustomColorRule:
				persistable = new MapCustomColorRule();
				goto IL_0FD1;
			case ObjectType.MapLineTemplate:
				persistable = new MapLineTemplate();
				goto IL_0FD1;
			case ObjectType.MapPolygonTemplate:
				persistable = new MapPolygonTemplate();
				goto IL_0FD1;
			case ObjectType.MapMarkerTemplate:
				persistable = new MapMarkerTemplate();
				goto IL_0FD1;
			case ObjectType.MapField:
				persistable = new MapField();
				goto IL_0FD1;
			case ObjectType.MapLine:
				persistable = new MapLine();
				goto IL_0FD1;
			case ObjectType.MapPolygon:
				persistable = new MapPolygon();
				goto IL_0FD1;
			case ObjectType.MapPoint:
				persistable = new MapPoint();
				goto IL_0FD1;
			case ObjectType.MapFieldDefinition:
				persistable = new MapFieldDefinition();
				goto IL_0FD1;
			case ObjectType.MapFieldName:
				persistable = new MapFieldName();
				goto IL_0FD1;
			case ObjectType.MapLineLayer:
				persistable = new MapLineLayer();
				goto IL_0FD1;
			case ObjectType.MapShapefile:
				persistable = new MapShapefile();
				goto IL_0FD1;
			case ObjectType.MapPolygonLayer:
				persistable = new MapPolygonLayer();
				goto IL_0FD1;
			case ObjectType.MapSpatialDataRegion:
				persistable = new MapSpatialDataRegion();
				goto IL_0FD1;
			case ObjectType.MapSpatialDataSet:
				persistable = new MapSpatialDataSet();
				goto IL_0FD1;
			case ObjectType.MapPointLayer:
				persistable = new MapPointLayer();
				goto IL_0FD1;
			case ObjectType.MapTile:
				persistable = new MapTile();
				goto IL_0FD1;
			case ObjectType.MapTileLayer:
				persistable = new MapTileLayer();
				goto IL_0FD1;
			case ObjectType.MapBorderSkin:
				persistable = new MapBorderSkin();
				goto IL_0FD1;
			case ObjectType.MapCustomView:
				persistable = new MapCustomView();
				goto IL_0FD1;
			case ObjectType.MapDataBoundView:
				persistable = new MapDataBoundView();
				goto IL_0FD1;
			case ObjectType.MapElementView:
				persistable = new MapElementView();
				goto IL_0FD1;
			case ObjectType.PageBreak:
				persistable = new PageBreak();
				goto IL_0FD1;
			case ObjectType.DataScopeInfo:
				persistable = new DataScopeInfo();
				goto IL_0FD1;
			case ObjectType.BucketedDataAggregateInfos:
				persistable = new BucketedDataAggregateInfos();
				goto IL_0FD1;
			case ObjectType.DataAggregateInfoBucket:
				persistable = new DataAggregateInfoBucket();
				goto IL_0FD1;
			case ObjectType.NumericIndicatorRange:
				persistable = new NumericIndicatorRange();
				goto IL_0FD1;
			case ObjectType.IndicatorState:
				persistable = new IndicatorState();
				goto IL_0FD1;
			case ObjectType.SharedDataSetQuery:
				persistable = new SharedDataSetQuery();
				goto IL_0FD1;
			case ObjectType.DataSetCore:
				persistable = new DataSetCore();
				goto IL_0FD1;
			case ObjectType.DataSetParameterValue:
				persistable = new DataSetParameterValue();
				goto IL_0FD1;
			case ObjectType.RIFVariantContainer:
				persistable = new RIFVariantContainer();
				goto IL_0FD1;
			case ObjectType.IdcRelationship:
				persistable = new IdcRelationship();
				goto IL_0FD1;
			case ObjectType.DefaultRelationship:
				persistable = new DefaultRelationship();
				goto IL_0FD1;
			case ObjectType.JoinCondition:
				persistable = new Relationship.JoinCondition();
				goto IL_0FD1;
			case ObjectType.BandLayoutOptions:
				persistable = new BandLayoutOptions();
				goto IL_0FD1;
			case ObjectType.LabelData:
				persistable = new LabelData();
				goto IL_0FD1;
			case ObjectType.Slider:
				persistable = new Slider();
				goto IL_0FD1;
			case ObjectType.Coverflow:
				persistable = new Coverflow();
				goto IL_0FD1;
			case ObjectType.PlayAxis:
				persistable = new PlayAxis();
				goto IL_0FD1;
			case ObjectType.BandNavigationCell:
				persistable = new BandNavigationCell();
				goto IL_0FD1;
			case ObjectType.Tabstrip:
				persistable = new Tabstrip();
				goto IL_0FD1;
			case ObjectType.NavigationItem:
				persistable = new NavigationItem();
				goto IL_0FD1;
			case ObjectType.LinearJoinInfo:
				persistable = new LinearJoinInfo();
				goto IL_0FD1;
			case ObjectType.IntersectJoinInfo:
				persistable = new IntersectJoinInfo();
				goto IL_0FD1;
			case ObjectType.ScopedFieldInfo:
				persistable = new ScopedFieldInfo();
				goto IL_0FD1;
			}
			Global.Tracer.Assert(false, "Unsupported object type: " + objectType.ToString());
			IL_0FD1:
			IDOwner idowner = persistable as IDOwner;
			if (idowner != null)
			{
				idowner.ParentInstancePath = this.m_parentIDOwner;
				this.m_parentIDOwner = idowner;
			}
			persistable.Deserialize(context);
			this.m_parentIDOwner = parentIDOwner;
			this.m_parentReportItem = parentReportItem;
			return persistable;
		}

		// Token: 0x0400284E RID: 10318
		private IDOwner m_parentIDOwner;

		// Token: 0x0400284F RID: 10319
		private ReportItem m_parentReportItem;
	}
}
