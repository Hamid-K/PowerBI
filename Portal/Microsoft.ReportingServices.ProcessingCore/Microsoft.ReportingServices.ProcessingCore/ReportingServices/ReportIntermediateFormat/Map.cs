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
	// Token: 0x02000416 RID: 1046
	[Serializable]
	internal sealed class Map : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, IActionOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IPageBreakOwner
	{
		// Token: 0x06002D6F RID: 11631 RVA: 0x000CFE6A File Offset: 0x000CE06A
		internal Map(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000CFE89 File Offset: 0x000CE089
		internal Map(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000CFEA9 File Offset: 0x000CE0A9
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Map;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000CFEAD File Offset: 0x000CE0AD
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000CFEB5 File Offset: 0x000CE0B5
		internal MapViewport MapViewport
		{
			get
			{
				return this.m_mapViewport;
			}
			set
			{
				this.m_mapViewport = value;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000CFEBE File Offset: 0x000CE0BE
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000CFEC6 File Offset: 0x000CE0C6
		internal List<MapLayer> MapLayers
		{
			get
			{
				return this.m_mapLayers;
			}
			set
			{
				this.m_mapLayers = value;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000CFECF File Offset: 0x000CE0CF
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x000CFED7 File Offset: 0x000CE0D7
		internal List<MapLegend> MapLegends
		{
			get
			{
				return this.m_mapLegends;
			}
			set
			{
				this.m_mapLegends = value;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000CFEE0 File Offset: 0x000CE0E0
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000CFEE8 File Offset: 0x000CE0E8
		internal List<MapTitle> MapTitles
		{
			get
			{
				return this.m_mapTitles;
			}
			set
			{
				this.m_mapTitles = value;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000CFEF1 File Offset: 0x000CE0F1
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x000CFEF9 File Offset: 0x000CE0F9
		internal MapDistanceScale MapDistanceScale
		{
			get
			{
				return this.m_mapDistanceScale;
			}
			set
			{
				this.m_mapDistanceScale = value;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000CFF02 File Offset: 0x000CE102
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x000CFF0A File Offset: 0x000CE10A
		internal MapColorScale MapColorScale
		{
			get
			{
				return this.m_mapColorScale;
			}
			set
			{
				this.m_mapColorScale = value;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000CFF13 File Offset: 0x000CE113
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000CFF1B File Offset: 0x000CE11B
		internal MapBorderSkin MapBorderSkin
		{
			get
			{
				return this.m_mapBorderSkin;
			}
			set
			{
				this.m_mapBorderSkin = value;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000CFF24 File Offset: 0x000CE124
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x000CFF2C File Offset: 0x000CE12C
		internal ExpressionInfo AntiAliasing
		{
			get
			{
				return this.m_antiAliasing;
			}
			set
			{
				this.m_antiAliasing = value;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000CFF35 File Offset: 0x000CE135
		// (set) Token: 0x06002D83 RID: 11651 RVA: 0x000CFF3D File Offset: 0x000CE13D
		internal ExpressionInfo TextAntiAliasingQuality
		{
			get
			{
				return this.m_textAntiAliasingQuality;
			}
			set
			{
				this.m_textAntiAliasingQuality = value;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000CFF46 File Offset: 0x000CE146
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x000CFF4E File Offset: 0x000CE14E
		internal ExpressionInfo ShadowIntensity
		{
			get
			{
				return this.m_shadowIntensity;
			}
			set
			{
				this.m_shadowIntensity = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000CFF57 File Offset: 0x000CE157
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000CFF5F File Offset: 0x000CE15F
		internal ExpressionInfo TileLanguage
		{
			get
			{
				return this.m_tileLanguage;
			}
			set
			{
				this.m_tileLanguage = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000CFF68 File Offset: 0x000CE168
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000CFF70 File Offset: 0x000CE170
		internal int MaximumSpatialElementCount
		{
			get
			{
				return this.m_maximumSpatialElementCount;
			}
			set
			{
				this.m_maximumSpatialElementCount = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000CFF79 File Offset: 0x000CE179
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x000CFF81 File Offset: 0x000CE181
		internal int MaximumTotalPointCount
		{
			get
			{
				return this.m_maximumTotalPointCount;
			}
			set
			{
				this.m_maximumTotalPointCount = value;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000CFF8A File Offset: 0x000CE18A
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x000CFF92 File Offset: 0x000CE192
		internal List<MapDataRegion> MapDataRegions
		{
			get
			{
				return this.m_mapDataRegions;
			}
			set
			{
				this.m_mapDataRegions = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000CFF9B File Offset: 0x000CE19B
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x000CFFA3 File Offset: 0x000CE1A3
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

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000CFFAC File Offset: 0x000CE1AC
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000CFFB4 File Offset: 0x000CE1B4
		// (set) Token: 0x06002D92 RID: 11666 RVA: 0x000CFFBC File Offset: 0x000CE1BC
		internal ExpressionInfo PageName
		{
			get
			{
				return this.m_pageName;
			}
			set
			{
				this.m_pageName = value;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000CFFC5 File Offset: 0x000CE1C5
		// (set) Token: 0x06002D94 RID: 11668 RVA: 0x000CFFCD File Offset: 0x000CE1CD
		internal PageBreak PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000CFFD6 File Offset: 0x000CE1D6
		// (set) Token: 0x06002D96 RID: 11670 RVA: 0x000CFFDE File Offset: 0x000CE1DE
		PageBreak IPageBreakOwner.PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000CFFE7 File Offset: 0x000CE1E7
		Microsoft.ReportingServices.ReportProcessing.ObjectType IPageBreakOwner.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Map;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000CFFEB File Offset: 0x000CE1EB
		string IPageBreakOwner.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000CFFF3 File Offset: 0x000CE1F3
		IInstancePath IPageBreakOwner.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000CFFF6 File Offset: 0x000CE1F6
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x000CFFF9 File Offset: 0x000CE1F9
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000CFFFB File Offset: 0x000CE1FB
		internal MapExprHost MapExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000D0004 File Offset: 0x000CE204
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.MapStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			context.IsTopLevelCellContents = false;
			if (this.m_mapViewport != null)
			{
				this.m_mapViewport.Initialize(context);
			}
			if (this.m_mapLayers != null)
			{
				for (int i = 0; i < this.m_mapLayers.Count; i++)
				{
					this.m_mapLayers[i].Initialize(context);
				}
			}
			if (this.m_mapLegends != null)
			{
				for (int j = 0; j < this.m_mapLegends.Count; j++)
				{
					this.m_mapLegends[j].Initialize(context);
				}
			}
			if (this.m_mapTitles != null)
			{
				for (int k = 0; k < this.m_mapTitles.Count; k++)
				{
					this.m_mapTitles[k].Initialize(context);
				}
			}
			if (this.m_mapDistanceScale != null)
			{
				this.m_mapDistanceScale.Initialize(context);
			}
			if (this.m_mapColorScale != null)
			{
				this.m_mapColorScale.Initialize(context);
			}
			if (this.m_mapBorderSkin != null)
			{
				this.m_mapBorderSkin.Initialize(context);
			}
			if (this.m_antiAliasing != null)
			{
				this.m_antiAliasing.Initialize("AntiAliasing", context);
				context.ExprHostBuilder.MapAntiAliasing(this.m_antiAliasing);
			}
			if (this.m_textAntiAliasingQuality != null)
			{
				this.m_textAntiAliasingQuality.Initialize("TextAntiAliasingQuality", context);
				context.ExprHostBuilder.MapTextAntiAliasingQuality(this.m_textAntiAliasingQuality);
			}
			if (this.m_shadowIntensity != null)
			{
				this.m_shadowIntensity.Initialize("ShadowIntensity", context);
				context.ExprHostBuilder.MapShadowIntensity(this.m_shadowIntensity);
			}
			if (this.m_tileLanguage != null)
			{
				this.m_tileLanguage.Initialize("TileLanguage", context);
				context.ExprHostBuilder.MapTileLanguage(this.m_tileLanguage);
			}
			if (this.m_mapDataRegions != null)
			{
				for (int l = 0; l < this.m_mapDataRegions.Count; l++)
				{
					this.m_mapDataRegions[l].Initialize(context);
				}
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.Initialize(context);
			}
			if (this.m_pageName != null)
			{
				this.m_pageName.Initialize("PageName", context);
				context.ExprHostBuilder.PageName(this.m_pageName);
			}
			base.ExprHostID = context.ExprHostBuilder.MapEnd();
			return false;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000D0284 File Offset: 0x000CE484
		internal override void TraverseScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_mapDataRegions != null)
			{
				for (int i = 0; i < this.m_mapDataRegions.Count; i++)
				{
					this.m_mapDataRegions[i].TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000D02C1 File Offset: 0x000CE4C1
		internal bool ContainsMapDataRegion()
		{
			return this.m_mapDataRegions != null && this.m_mapDataRegions.Count != 0;
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000D02DC File Offset: 0x000CE4DC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Map map = (Map)base.PublishClone(context);
			context.CurrentMapClone = map;
			if (this.m_mapDataRegions != null)
			{
				map.m_mapDataRegions = new List<MapDataRegion>(this.m_mapDataRegions.Count);
				foreach (MapDataRegion mapDataRegion in this.m_mapDataRegions)
				{
					map.m_mapDataRegions.Add((MapDataRegion)mapDataRegion.PublishClone(context));
				}
			}
			if (this.m_mapLayers != null)
			{
				map.m_mapLayers = new List<MapLayer>(this.m_mapLayers.Count);
				foreach (MapLayer mapLayer in this.m_mapLayers)
				{
					map.m_mapLayers.Add((MapLayer)mapLayer.PublishClone(context));
				}
			}
			if (this.m_mapViewport != null)
			{
				map.m_mapViewport = (MapViewport)this.m_mapViewport.PublishClone(context);
			}
			if (this.m_mapLegends != null)
			{
				map.m_mapLegends = new List<MapLegend>(this.m_mapLegends.Count);
				foreach (MapLegend mapLegend in this.m_mapLegends)
				{
					map.m_mapLegends.Add((MapLegend)mapLegend.PublishClone(context));
				}
			}
			if (this.m_mapTitles != null)
			{
				map.m_mapTitles = new List<MapTitle>(this.m_mapTitles.Count);
				foreach (MapTitle mapTitle in this.m_mapTitles)
				{
					map.m_mapTitles.Add((MapTitle)mapTitle.PublishClone(context));
				}
			}
			if (this.m_mapDistanceScale != null)
			{
				map.m_mapDistanceScale = (MapDistanceScale)this.m_mapDistanceScale.PublishClone(context);
			}
			if (this.m_mapColorScale != null)
			{
				map.m_mapColorScale = (MapColorScale)this.m_mapColorScale.PublishClone(context);
			}
			if (this.m_mapBorderSkin != null)
			{
				map.m_mapBorderSkin = (MapBorderSkin)this.m_mapBorderSkin.PublishClone(context);
			}
			if (this.m_antiAliasing != null)
			{
				map.m_antiAliasing = (ExpressionInfo)this.m_antiAliasing.PublishClone(context);
			}
			if (this.m_textAntiAliasingQuality != null)
			{
				map.m_textAntiAliasingQuality = (ExpressionInfo)this.m_textAntiAliasingQuality.PublishClone(context);
			}
			if (this.m_shadowIntensity != null)
			{
				map.m_shadowIntensity = (ExpressionInfo)this.m_shadowIntensity.PublishClone(context);
			}
			if (this.m_tileLanguage != null)
			{
				map.m_tileLanguage = (ExpressionInfo)this.m_tileLanguage.PublishClone(context);
			}
			if (this.m_action != null)
			{
				map.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_pageBreak != null)
			{
				map.m_pageBreak = (PageBreak)this.m_pageBreak.PublishClone(context);
			}
			if (this.m_pageName != null)
			{
				map.m_pageName = (ExpressionInfo)this.m_pageName.PublishClone(context);
			}
			return map;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000D0620 File Offset: 0x000CE820
		internal MapAntiAliasing EvaluateAntiAliasing(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateMapAntiAliasing(context.ReportRuntime.EvaluateMapAntiAliasingExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000D0647 File Offset: 0x000CE847
		internal MapTextAntiAliasingQuality EvaluateTextAntiAliasingQuality(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateMapTextAntiAliasingQuality(context.ReportRuntime.EvaluateMapTextAntiAliasingQualityExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000D066E File Offset: 0x000CE86E
		internal double EvaluateShadowIntensity(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapShadowIntensityExpression(this, base.Name);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000D068A File Offset: 0x000CE88A
		internal string EvaluateTileLanguage(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapTileLanguageExpression(this, base.Name);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000D06A6 File Offset: 0x000CE8A6
		internal string EvaluatePageName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPageNameExpression(this, this.m_pageName, this.m_name);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000D06C8 File Offset: 0x000CE8C8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapDataRegions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataRegion),
				new MemberInfo(MemberName.MapViewport, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapViewport),
				new MemberInfo(MemberName.MapLayers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLayer),
				new MemberInfo(MemberName.MapLegends, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegend),
				new MemberInfo(MemberName.MapTitles, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTitle),
				new MemberInfo(MemberName.MapDistanceScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDistanceScale),
				new MemberInfo(MemberName.MapColorScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScale),
				new MemberInfo(MemberName.MapBorderSkin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBorderSkin),
				new ReadOnlyMemberInfo(MemberName.PageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.AntiAliasing, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextAntiAliasingQuality, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ShadowIntensity, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumSpatialElementCount, Token.Int32),
				new MemberInfo(MemberName.MaximumTotalPointCount, Token.Int32),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.TileLanguage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak),
				new MemberInfo(MemberName.PageName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000D0868 File Offset: 0x000CEA68
		internal int GenerateActionOwnerID()
		{
			int num = this.m_actionOwnerCounter + 1;
			this.m_actionOwnerCounter = num;
			return num;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000D0888 File Offset: 0x000CEA88
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Map.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.MapDataRegions)
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					switch (memberName)
					{
					case MemberName.AntiAliasing:
						writer.Write(this.m_antiAliasing);
						continue;
					case MemberName.AutoLayout:
						break;
					case MemberName.ShadowIntensity:
						writer.Write(this.m_shadowIntensity);
						continue;
					case MemberName.TextAntiAliasingQuality:
						writer.Write(this.m_textAntiAliasingQuality);
						continue;
					default:
						if (memberName == MemberName.MapDataRegions)
						{
							writer.Write<MapDataRegion>(this.m_mapDataRegions);
							continue;
						}
						break;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.MapViewport:
						writer.Write(this.m_mapViewport);
						continue;
					case MemberName.MapLayers:
						writer.Write<MapLayer>(this.m_mapLayers);
						continue;
					case MemberName.MapLegends:
						writer.Write<MapLegend>(this.m_mapLegends);
						continue;
					case MemberName.MapTitles:
						writer.Write<MapTitle>(this.m_mapTitles);
						continue;
					case MemberName.MapDistanceScale:
						writer.Write(this.m_mapDistanceScale);
						continue;
					case MemberName.MapColorScale:
						writer.Write(this.m_mapColorScale);
						continue;
					case MemberName.MapBorderSkin:
						writer.Write(this.m_mapBorderSkin);
						continue;
					case MemberName.MaximumSpatialElementCount:
						writer.Write(this.m_maximumSpatialElementCount);
						continue;
					case MemberName.MaximumTotalPointCount:
						writer.Write(this.m_maximumTotalPointCount);
						continue;
					case MemberName.MapBorderSkinType:
					case MemberName.ExprHostMapMemberID:
					case MemberName.CenterX:
					case MemberName.CenterY:
					case MemberName.Zoom:
					case MemberName.MapView:
					case MemberName.MapVectorLayer:
					case MemberName.CachedShapefiles:
					case MemberName.DataElementLabel:
						break;
					case MemberName.TileLanguage:
						writer.Write(this.m_tileLanguage);
						continue;
					default:
						if (memberName == MemberName.PageBreak)
						{
							writer.Write(this.m_pageBreak);
							continue;
						}
						if (memberName == MemberName.PageName)
						{
							writer.Write(this.m_pageName);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000D0A9C File Offset: 0x000CEC9C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Map.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.TextAntiAliasingQuality)
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.PageBreakLocation)
					{
						this.m_pageBreak = new PageBreak();
						this.m_pageBreak.BreakLocation = (PageBreakLocation)reader.ReadEnum();
						continue;
					}
					switch (memberName)
					{
					case MemberName.AntiAliasing:
						this.m_antiAliasing = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ShadowIntensity:
						this.m_shadowIntensity = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TextAntiAliasingQuality:
						this.m_textAntiAliasingQuality = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.TileLanguage)
				{
					if (memberName == MemberName.MapDataRegions)
					{
						this.m_mapDataRegions = reader.ReadGenericListOfRIFObjects<MapDataRegion>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.MapViewport:
						this.m_mapViewport = (MapViewport)reader.ReadRIFObject();
						continue;
					case MemberName.MapLayers:
						this.m_mapLayers = reader.ReadGenericListOfRIFObjects<MapLayer>();
						continue;
					case MemberName.MapLegends:
						this.m_mapLegends = reader.ReadGenericListOfRIFObjects<MapLegend>();
						continue;
					case MemberName.MapTitles:
						this.m_mapTitles = reader.ReadGenericListOfRIFObjects<MapTitle>();
						continue;
					case MemberName.MapDistanceScale:
						this.m_mapDistanceScale = (MapDistanceScale)reader.ReadRIFObject();
						continue;
					case MemberName.MapColorScale:
						this.m_mapColorScale = (MapColorScale)reader.ReadRIFObject();
						continue;
					case MemberName.MapBorderSkin:
						this.m_mapBorderSkin = (MapBorderSkin)reader.ReadRIFObject();
						continue;
					case MemberName.MaximumSpatialElementCount:
						this.m_maximumSpatialElementCount = reader.ReadInt32();
						continue;
					case MemberName.MaximumTotalPointCount:
						this.m_maximumTotalPointCount = reader.ReadInt32();
						continue;
					case MemberName.TileLanguage:
						this.m_tileLanguage = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PageBreak)
					{
						this.m_pageBreak = (PageBreak)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						this.m_pageName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000D0D22 File Offset: 0x000CEF22
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000D0D2C File Offset: 0x000CEF2C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000D0D34 File Offset: 0x000CEF34
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.MapHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_mapViewport != null && this.m_exprHost.MapViewportHost != null)
				{
					this.m_mapViewport.SetExprHost(this.m_exprHost.MapViewportHost, reportObjectModel);
				}
				IList<MapPolygonLayerExprHost> mapPolygonLayersHostsRemotable = this.m_exprHost.MapPolygonLayersHostsRemotable;
				IList<MapPointLayerExprHost> mapPointLayersHostsRemotable = this.m_exprHost.MapPointLayersHostsRemotable;
				IList<MapLineLayerExprHost> mapLineLayersHostsRemotable = this.m_exprHost.MapLineLayersHostsRemotable;
				IList<MapTileLayerExprHost> mapTileLayersHostsRemotable = this.m_exprHost.MapTileLayersHostsRemotable;
				if (this.m_mapLayers != null)
				{
					for (int i = 0; i < this.m_mapLayers.Count; i++)
					{
						MapLayer mapLayer = this.m_mapLayers[i];
						if (mapLayer != null && mapLayer.ExpressionHostID > -1)
						{
							if (mapLayer is MapPolygonLayer)
							{
								if (mapPolygonLayersHostsRemotable != null)
								{
									mapLayer.SetExprHost(mapPolygonLayersHostsRemotable[mapLayer.ExpressionHostID], reportObjectModel);
								}
							}
							else if (mapLayer is MapPointLayer)
							{
								if (mapPointLayersHostsRemotable != null)
								{
									mapLayer.SetExprHost(mapPointLayersHostsRemotable[mapLayer.ExpressionHostID], reportObjectModel);
								}
							}
							else if (mapLayer is MapLineLayer)
							{
								if (mapLineLayersHostsRemotable != null)
								{
									mapLayer.SetExprHost(mapLineLayersHostsRemotable[mapLayer.ExpressionHostID], reportObjectModel);
								}
							}
							else if (mapLayer is MapTileLayer && mapTileLayersHostsRemotable != null)
							{
								mapLayer.SetExprHost(mapTileLayersHostsRemotable[mapLayer.ExpressionHostID], reportObjectModel);
							}
						}
					}
				}
				IList<MapLegendExprHost> mapLegendsHostsRemotable = this.m_exprHost.MapLegendsHostsRemotable;
				if (this.m_mapLegends != null && mapLegendsHostsRemotable != null)
				{
					for (int j = 0; j < this.m_mapLegends.Count; j++)
					{
						MapLegend mapLegend = this.m_mapLegends[j];
						if (mapLegend != null && mapLegend.ExpressionHostID > -1)
						{
							mapLegend.SetExprHost(mapLegendsHostsRemotable[mapLegend.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<MapTitleExprHost> mapTitlesHostsRemotable = this.m_exprHost.MapTitlesHostsRemotable;
				if (this.m_mapTitles != null && mapTitlesHostsRemotable != null)
				{
					for (int k = 0; k < this.m_mapTitles.Count; k++)
					{
						MapTitle mapTitle = this.m_mapTitles[k];
						if (mapTitle != null && mapTitle.ExpressionHostID > -1)
						{
							mapTitle.SetExprHost(mapTitlesHostsRemotable[mapTitle.ExpressionHostID], reportObjectModel);
						}
					}
				}
				if (this.m_mapDistanceScale != null && this.m_exprHost.MapDistanceScaleHost != null)
				{
					this.m_mapDistanceScale.SetExprHost(this.m_exprHost.MapDistanceScaleHost, reportObjectModel);
				}
				if (this.m_mapColorScale != null && this.m_exprHost.MapColorScaleHost != null)
				{
					this.m_mapColorScale.SetExprHost(this.m_exprHost.MapColorScaleHost, reportObjectModel);
				}
				if (this.m_mapBorderSkin != null && this.m_exprHost.MapBorderSkinHost != null)
				{
					this.m_mapBorderSkin.SetExprHost(this.m_exprHost.MapBorderSkinHost, reportObjectModel);
				}
				if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
				}
				if (this.m_pageBreak != null && this.m_exprHost.PageBreakExprHost != null)
				{
					this.m_pageBreak.SetExprHost(this.m_exprHost.PageBreakExprHost, reportObjectModel);
				}
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000D1064 File Offset: 0x000CF264
		internal string GetFormattedStringFromValue(ref Microsoft.ReportingServices.RdlExpressions.VariantResult result, OnDemandProcessingContext context)
		{
			string text = null;
			if (result.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (result.Value != null)
			{
				text = Formatter.Format(result.Value, ref this.m_formatter, base.StyleClass, null, context, this.ObjectType, base.Name);
			}
			return text;
		}

		// Token: 0x04001836 RID: 6198
		private List<MapDataRegion> m_mapDataRegions;

		// Token: 0x04001837 RID: 6199
		private MapViewport m_mapViewport;

		// Token: 0x04001838 RID: 6200
		private List<MapLayer> m_mapLayers;

		// Token: 0x04001839 RID: 6201
		private List<MapLegend> m_mapLegends;

		// Token: 0x0400183A RID: 6202
		private List<MapTitle> m_mapTitles;

		// Token: 0x0400183B RID: 6203
		private MapDistanceScale m_mapDistanceScale;

		// Token: 0x0400183C RID: 6204
		private MapColorScale m_mapColorScale;

		// Token: 0x0400183D RID: 6205
		private MapBorderSkin m_mapBorderSkin;

		// Token: 0x0400183E RID: 6206
		private ExpressionInfo m_antiAliasing;

		// Token: 0x0400183F RID: 6207
		private ExpressionInfo m_textAntiAliasingQuality;

		// Token: 0x04001840 RID: 6208
		private ExpressionInfo m_shadowIntensity;

		// Token: 0x04001841 RID: 6209
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001842 RID: 6210
		private int m_maximumSpatialElementCount = 20000;

		// Token: 0x04001843 RID: 6211
		private int m_maximumTotalPointCount = 1000000;

		// Token: 0x04001844 RID: 6212
		private ExpressionInfo m_tileLanguage;

		// Token: 0x04001845 RID: 6213
		private PageBreak m_pageBreak;

		// Token: 0x04001846 RID: 6214
		private ExpressionInfo m_pageName;

		// Token: 0x04001847 RID: 6215
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Map.GetDeclaration();

		// Token: 0x04001848 RID: 6216
		[NonSerialized]
		private MapExprHost m_exprHost;

		// Token: 0x04001849 RID: 6217
		[NonSerialized]
		private int m_actionOwnerCounter;

		// Token: 0x0400184A RID: 6218
		[NonSerialized]
		private Formatter m_formatter;
	}
}
