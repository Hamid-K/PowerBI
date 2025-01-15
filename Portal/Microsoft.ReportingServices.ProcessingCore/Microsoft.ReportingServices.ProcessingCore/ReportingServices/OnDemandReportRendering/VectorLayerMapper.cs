using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000165 RID: 357
	internal abstract class VectorLayerMapper
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x0003F8CC File Offset: 0x0003DACC
		internal VectorLayerMapper(MapVectorLayer mapVectorLayer, MapControl coreMap, MapMapper mapMapper)
		{
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_coreMap = coreMap;
			this.m_mapMapper = mapMapper;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003F8F4 File Offset: 0x0003DAF4
		internal void Render()
		{
			this.PopulateSpatialElements();
			this.CreateRules();
			this.RenderSpatialElements();
			this.RenderRules();
			this.UpdateView();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003F914 File Offset: 0x0003DB14
		private void PopulateSpatialElements()
		{
			this.CreateSpatialDataMapper();
			if (this.m_spatialDataMapper != null)
			{
				this.m_spatialDataMapper.Populate();
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003F930 File Offset: 0x0003DB30
		private void CreateSpatialDataMapper()
		{
			MapSpatialData mapSpatialData = this.m_mapVectorLayer.MapSpatialData;
			if (this.IsEmbeddedLayer)
			{
				this.m_spatialDataMapper = new EmbeddedSpatialDataMapper(this, this.m_spatialElementsDictionary, this.SpatialElementCollection, this.GetSpatialElementManager(), this.m_coreMap, this.m_mapMapper);
				return;
			}
			if (this.m_mapVectorLayer.MapSpatialData is MapSpatialDataSet)
			{
				this.m_spatialDataMapper = new SpatialDataSetMapper(this, this.m_spatialElementsDictionary, this.GetSpatialElementManager(), this.m_coreMap, this.m_mapMapper);
				return;
			}
			if (this.m_mapVectorLayer.MapSpatialData is MapShapefile)
			{
				this.m_spatialDataMapper = new ShapefileMapper(this, this.m_spatialElementsDictionary, this.m_coreMap, this.m_mapMapper);
				return;
			}
			this.m_spatialDataMapper = null;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003F9EC File Offset: 0x0003DBEC
		private void RenderGrouping(MapMember mapMember)
		{
			if (!mapMember.IsStatic)
			{
				MapDynamicMemberInstance mapDynamicMemberInstance = (MapDynamicMemberInstance)mapMember.Instance;
				mapDynamicMemberInstance.ResetContext();
				while (mapDynamicMemberInstance.MoveNext())
				{
					if (mapMember.ChildMapMember != null)
					{
						this.RenderGrouping(mapMember.ChildMapMember);
					}
					else
					{
						this.RenderInnerMostMember();
					}
				}
				return;
			}
			if (mapMember.ChildMapMember != null)
			{
				this.RenderGrouping(mapMember.ChildMapMember);
				return;
			}
			this.RenderInnerMostMember();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003FA58 File Offset: 0x0003DC58
		private void RenderSpatialElements()
		{
			MapDataRegion mapDataRegion = this.m_mapVectorLayer.MapDataRegion;
			if (mapDataRegion != null)
			{
				this.RenderGrouping(mapDataRegion.MapMember);
			}
			this.RenderNonBoundSpatialElements();
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003FA88 File Offset: 0x0003DC88
		private void RenderInnerMostMember()
		{
			SpatialElementInfoGroup spatialElementInfoGroup;
			if (this.m_spatialDataMapper != null)
			{
				spatialElementInfoGroup = this.GetSpatialElementsFromDataRegionKey();
			}
			else
			{
				spatialElementInfoGroup = this.CreateSpatialElementFromDataRegion();
			}
			if (spatialElementInfoGroup != null)
			{
				if (spatialElementInfoGroup.BoundToData)
				{
					throw new RenderingObjectModelException(RPResWrapper.rsMapSpatialElementHasMoreThanOnMatchingGroupInstance(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name));
				}
				this.RenderSpatialElementGroup(spatialElementInfoGroup, true);
				spatialElementInfoGroup.BoundToData = true;
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003FAF4 File Offset: 0x0003DCF4
		private void RenderSpatialElementGroup(SpatialElementInfoGroup group, bool hasScope)
		{
			foreach (SpatialElementInfo spatialElementInfo in group.Elements)
			{
				this.RenderSpatialElement(spatialElementInfo, hasScope);
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003FB48 File Offset: 0x0003DD48
		protected void InitializeSpatialElement(ISpatialElement spatialElement)
		{
			spatialElement.Text = "";
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003FB58 File Offset: 0x0003DD58
		private void RenderNonBoundSpatialElements()
		{
			if (this.m_spatialDataMapper != null)
			{
				MapDataRegion mapDataRegion = this.m_mapVectorLayer.MapDataRegion;
				foreach (KeyValuePair<SpatialElementKey, SpatialElementInfoGroup> keyValuePair in this.m_spatialElementsDictionary)
				{
					if (!keyValuePair.Value.BoundToData)
					{
						this.RenderSpatialElementGroup(keyValuePair.Value, mapDataRegion == null);
					}
				}
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		private SpatialElementInfoGroup GetSpatialElementsFromDataRegionKey()
		{
			MapBindingFieldPairCollection mapBindingFieldPairs = this.m_mapVectorLayer.MapBindingFieldPairs;
			if (mapBindingFieldPairs != null)
			{
				SpatialElementKey spatialElementKey = VectorLayerMapper.CreateDataRegionSpatialElementKey(mapBindingFieldPairs);
				this.ValidateKey(spatialElementKey, mapBindingFieldPairs);
				SpatialElementInfoGroup spatialElementInfoGroup;
				if (this.m_spatialElementsDictionary.TryGetValue(spatialElementKey, out spatialElementInfoGroup))
				{
					return spatialElementInfoGroup;
				}
			}
			return null;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003FC18 File Offset: 0x0003DE18
		internal static SpatialElementKey CreateDataRegionSpatialElementKey(MapBindingFieldPairCollection mapBindingFieldPairs)
		{
			List<object> list = new List<object>();
			for (int i = 0; i < mapBindingFieldPairs.Count; i++)
			{
				list.Add(VectorLayerMapper.EvaluateBindingExpression(mapBindingFieldPairs[i]));
			}
			return new SpatialElementKey(list);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003FC54 File Offset: 0x0003DE54
		internal void ValidateKey(SpatialElementKey spatialElementKey, MapBindingFieldPairCollection mapBindingFieldPairs)
		{
			if (this.m_spatialDataMapper.KeyTypes == null)
			{
				return;
			}
			for (int i = 0; i < spatialElementKey.KeyValues.Count; i++)
			{
				object obj = spatialElementKey.KeyValues[i];
				if (obj != null)
				{
					Type type = obj.GetType();
					Type type2 = this.m_spatialDataMapper.KeyTypes[i];
					if (!(type2 == null) && type != type2)
					{
						object obj2 = VectorLayerMapper.Convert(obj, type, type2);
						if (obj2 == null)
						{
							throw new RenderingObjectModelException(RPResWrapper.rsMapFieldBindingExpressionTypeMismatch(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, SpatialDataMapper.GetBindingFieldName(mapBindingFieldPairs[i])));
						}
						spatialElementKey.KeyValues[i] = obj2;
					}
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003FD20 File Offset: 0x0003DF20
		private static object Convert(object value, Type type, Type conversionType)
		{
			TypeCode typeCode = Type.GetTypeCode(conversionType);
			TypeCode typeCode2 = Type.GetTypeCode(type);
			int num;
			if (typeCode != TypeCode.Int32)
			{
				if (typeCode != TypeCode.Double)
				{
					if (typeCode == TypeCode.Decimal)
					{
						if (typeCode2 == TypeCode.Int32)
						{
							return (int)value;
						}
					}
				}
				else
				{
					if (typeCode2 == TypeCode.Int32)
					{
						return (double)((int)value);
					}
					if (typeCode2 == TypeCode.Single)
					{
						return (double)((float)value);
					}
				}
			}
			else if (typeCode2 == TypeCode.Decimal)
			{
				if (VectorLayerMapper.TryConvertDecimalToInt((decimal)value, out num))
				{
					return num;
				}
			}
			else if (typeCode2 == TypeCode.Double && VectorLayerMapper.TryConvertDoubleToInt((double)value, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003FDC0 File Offset: 0x0003DFC0
		private static bool TryConvertDecimalToInt(decimal value, out int convertedValue)
		{
			if (value > 2147483647m || value < -2147483648m)
			{
				convertedValue = 0;
				return false;
			}
			convertedValue = (int)value;
			return value.Equals(convertedValue);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003FE0C File Offset: 0x0003E00C
		private static bool TryConvertDoubleToInt(double value, out int convertedValue)
		{
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				convertedValue = 0;
				return false;
			}
			convertedValue = (int)value;
			return value.Equals((double)convertedValue);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003FE3C File Offset: 0x0003E03C
		internal static object EvaluateBindingExpression(MapBindingFieldPair mapBindingFieldPair)
		{
			ReportVariantProperty bindingExpression = mapBindingFieldPair.BindingExpression;
			object obj;
			if (!bindingExpression.IsExpression)
			{
				obj = bindingExpression.Value;
			}
			else
			{
				obj = mapBindingFieldPair.Instance.BindingExpression;
			}
			return obj;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003FE70 File Offset: 0x0003E070
		private SpatialElementInfoGroup CreateSpatialElementFromDataRegion()
		{
			if (!this.m_mapMapper.CanAddSpatialElement)
			{
				return null;
			}
			MapSpatialDataRegion mapSpatialDataRegion = (MapSpatialDataRegion)this.m_mapVectorLayer.MapSpatialData;
			if (mapSpatialDataRegion == null)
			{
				return null;
			}
			object vectorData = mapSpatialDataRegion.Instance.VectorData;
			if (vectorData == null)
			{
				return null;
			}
			ISpatialElement spatialElement;
			if (vectorData is SqlGeography)
			{
				spatialElement = this.GetSpatialElementManager().AddGeography((SqlGeography)vectorData, this.m_mapVectorLayer.Name);
			}
			else
			{
				if (!(vectorData is SqlGeometry))
				{
					throw new RenderingObjectModelException(RPResWrapper.rsMapInvalidSpatialFieldType(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name));
				}
				spatialElement = this.GetSpatialElementManager().AddGeometry((SqlGeometry)vectorData, this.m_mapVectorLayer.Name);
			}
			if (spatialElement == null)
			{
				return null;
			}
			SpatialElementInfo spatialElementInfo = new SpatialElementInfo();
			spatialElementInfo.CoreSpatialElement = spatialElement;
			spatialElementInfo.MapSpatialElement = null;
			SpatialElementInfoGroup spatialElementInfoGroup = new SpatialElementInfoGroup();
			spatialElementInfoGroup.Elements.Add(spatialElementInfo);
			this.m_spatialElementsDictionary.Add(new SpatialElementKey(null), spatialElementInfoGroup);
			this.OnSpatialElementAdded(spatialElement);
			this.m_mapMapper.OnSpatialElementAdded(spatialElementInfo);
			return spatialElementInfoGroup;
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003FF83 File Offset: 0x0003E183
		protected bool IsEmbeddedLayer
		{
			get
			{
				return this.SpatialElementCollection != null;
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003FF8E File Offset: 0x0003E18E
		protected void RenderSymbolRuleFields(Symbol symbol)
		{
			if (this.m_pointColorRuleMapper != null)
			{
				this.m_pointColorRuleMapper.SetRuleFieldValue(symbol);
			}
			if (this.m_pointlSizeRuleMapper != null)
			{
				this.m_pointlSizeRuleMapper.SetRuleFieldValue(symbol);
			}
			if (this.m_pointMarkerRuleMapper != null)
			{
				this.m_pointMarkerRuleMapper.SetRuleFieldValue(symbol);
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003FFCC File Offset: 0x0003E1CC
		protected void RenderPoint(MapSpatialElement mapSpatialElement, Symbol symbol, bool hasScope)
		{
			if (hasScope)
			{
				this.RenderSymbolRuleFields(symbol);
			}
			this.RenderSymbolTemplate(mapSpatialElement, symbol, hasScope);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		protected void CreatePointRules(MapPointRules mapPointRules)
		{
			if (mapPointRules.MapColorRule != null)
			{
				this.m_pointColorRuleMapper = new ColorRuleMapper(mapPointRules.MapColorRule, this, this.GetSymbolManager());
				this.m_pointColorRuleMapper.CreateSymbolRule();
			}
			if (mapPointRules.MapSizeRule != null)
			{
				this.m_pointlSizeRuleMapper = new SizeRuleMapper(mapPointRules.MapSizeRule, this, this.GetSymbolManager());
				this.m_pointlSizeRuleMapper.CreateSymbolRule();
			}
			if (mapPointRules.MapMarkerRule != null)
			{
				this.m_pointMarkerRuleMapper = new MarkerRuleMapper(mapPointRules.MapMarkerRule, this, this.GetSymbolManager());
				this.m_pointMarkerRuleMapper.CreateSymbolRule();
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00040078 File Offset: 0x0003E278
		protected void RenderPointRules(MapPointRules mapPointRules)
		{
			int? legendSymbolSize = this.GetLegendSymbolSize();
			Color? legendSymbolColor = this.GetLegendSymbolColor();
			MarkerStyle? legendSymbolMarker = this.GetLegendSymbolMarker();
			if (mapPointRules.MapColorRule != null)
			{
				this.m_pointColorRuleMapper.RenderSymbolRule(this.m_pointTemplateMapper, legendSymbolSize, legendSymbolMarker);
			}
			if (mapPointRules.MapSizeRule != null)
			{
				this.m_pointlSizeRuleMapper.RenderSymbolRule(this.m_pointTemplateMapper, legendSymbolColor, legendSymbolMarker);
			}
			if (mapPointRules.MapMarkerRule != null)
			{
				this.m_pointMarkerRuleMapper.RenderPointRule(this.m_pointTemplateMapper, legendSymbolColor, legendSymbolSize);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000400EB File Offset: 0x0003E2EB
		private Color? GetLegendSymbolColor()
		{
			if (this.m_pointTemplateMapper == null)
			{
				return new Color?(Color.Empty);
			}
			return new Color?(this.m_pointTemplateMapper.GetBackgroundColor(false));
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00040111 File Offset: 0x0003E311
		private int? GetLegendSymbolSize()
		{
			if (this.m_pointTemplateMapper == null)
			{
				return new int?(PointTemplateMapper.GetDefaultSymbolSize(this.m_mapMapper.DpiX));
			}
			return new int?(this.m_pointTemplateMapper.GetSize(this.GetMapPointTemplate(), false));
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00040148 File Offset: 0x0003E348
		private MarkerStyle? GetLegendSymbolMarker()
		{
			if (!(this.m_pointTemplateMapper is SymbolMarkerTemplateMapper))
			{
				return new MarkerStyle?(0);
			}
			return new MarkerStyle?(MapMapper.GetMarkerStyle(MapMapper.GetMarkerStyle(((MapMarkerTemplate)this.GetMapPointTemplate()).MapMarker, false)));
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0004017E File Offset: 0x0003E37E
		protected virtual void RenderSymbolTemplate(MapSpatialElement mapSpatialElement, Symbol coreSymbol, bool hasScope)
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00040180 File Offset: 0x0003E380
		internal virtual MapPointRules GetMapPointRules()
		{
			return null;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00040183 File Offset: 0x0003E383
		internal virtual MapPointTemplate GetMapPointTemplate()
		{
			return null;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00040186 File Offset: 0x0003E386
		internal bool HasPointColorRule(Symbol symbol)
		{
			return this.HasPointColorRule() && this.m_pointColorRuleMapper.HasDataValue(symbol);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000401A0 File Offset: 0x0003E3A0
		protected bool HasPointColorRule()
		{
			MapPointRules mapPointRules = this.GetMapPointRules();
			return mapPointRules != null && mapPointRules.MapColorRule != null;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000401C4 File Offset: 0x0003E3C4
		internal bool HasPointSizeRule(Symbol symbol)
		{
			return this.HasPointSizeRule() && this.m_pointlSizeRuleMapper.HasDataValue(symbol);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000401DC File Offset: 0x0003E3DC
		protected bool HasPointSizeRule()
		{
			MapPointRules mapPointRules = this.GetMapPointRules();
			return mapPointRules != null && mapPointRules.MapSizeRule != null;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00040200 File Offset: 0x0003E400
		internal bool HasMarkerRule(Symbol symbol)
		{
			return this.HasMarkerRule() && this.m_pointMarkerRuleMapper.HasDataValue(symbol);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00040218 File Offset: 0x0003E418
		protected bool HasMarkerRule()
		{
			MapPointRules mapPointRules = this.GetMapPointRules();
			return mapPointRules != null && mapPointRules.MapMarkerRule != null;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004023C File Offset: 0x0003E43C
		protected PointTemplateMapper CreatePointTemplateMapper()
		{
			return new SymbolMarkerTemplateMapper(this.m_mapMapper, this, this.m_mapVectorLayer);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00040250 File Offset: 0x0003E450
		internal static string AddPrefixToFieldNames(string layerName, string expression)
		{
			if (expression == null)
			{
				return null;
			}
			string[] array = expression.Split(new char[] { '#' });
			string text = "";
			if (array.Length == 1)
			{
				return expression;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "")
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", text, "#");
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}{1}_{2}", text, layerName, array[i]);
				}
			}
			return text;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000402CD File Offset: 0x0003E4CD
		protected CoreSymbolManager GetSymbolManager()
		{
			if (this.m_symbolManager == null)
			{
				this.m_symbolManager = new CoreSymbolManager(this.m_coreMap, this.m_mapVectorLayer);
			}
			return this.m_symbolManager;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000402F4 File Offset: 0x0003E4F4
		private void UpdateView()
		{
			this.m_coreMap.mapCore.UpdateCachedBounds();
			MapView mapView = this.m_mapVectorLayer.MapDef.MapViewport.MapView;
			if (mapView is MapDataBoundView)
			{
				this.AddBoundSpatialElementsToView();
				return;
			}
			if (mapView is MapElementView)
			{
				this.AddSpatialElementToView((MapElementView)mapView);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004034C File Offset: 0x0003E54C
		private void AddBoundSpatialElementsToView()
		{
			foreach (KeyValuePair<SpatialElementKey, SpatialElementInfoGroup> keyValuePair in this.m_spatialElementsDictionary)
			{
				if (keyValuePair.Value.BoundToData)
				{
					this.AddSpatialElementGroupToView(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000403B4 File Offset: 0x0003E5B4
		private void AddSpatialElementGroupToView(SpatialElementInfoGroup group)
		{
			foreach (SpatialElementInfo spatialElementInfo in group.Elements)
			{
				this.m_mapMapper.AddSpatialElementToView(spatialElementInfo.CoreSpatialElement);
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00040414 File Offset: 0x0003E614
		private void AddSpatialElementToView(MapElementView mapView)
		{
			if (this.GetElementViewLayerName(mapView) != this.m_mapVectorLayer.Name)
			{
				return;
			}
			List<ISpatialElement> elementViewSpatialElements = this.GetElementViewSpatialElements(mapView);
			if (elementViewSpatialElements != null)
			{
				using (List<ISpatialElement>.Enumerator enumerator = elementViewSpatialElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ISpatialElement spatialElement = enumerator.Current;
						this.m_mapMapper.AddSpatialElementToView(spatialElement);
					}
					return;
				}
			}
			foreach (KeyValuePair<SpatialElementKey, SpatialElementInfoGroup> keyValuePair in this.m_spatialElementsDictionary)
			{
				this.AddSpatialElementGroupToView(keyValuePair.Value);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000404D8 File Offset: 0x0003E6D8
		private List<ISpatialElement> GetElementViewSpatialElements(MapElementView mapView)
		{
			MapBindingFieldPairCollection mapBindingFieldPairs = mapView.MapBindingFieldPairs;
			if (mapBindingFieldPairs == null)
			{
				return null;
			}
			SpatialElementKey spatialElementKey = VectorLayerMapper.CreateDataRegionSpatialElementKey(mapBindingFieldPairs);
			List<ISpatialElement> list = null;
			foreach (KeyValuePair<SpatialElementKey, SpatialElementInfoGroup> keyValuePair in this.m_spatialElementsDictionary)
			{
				foreach (SpatialElementInfo spatialElementInfo in keyValuePair.Value.Elements)
				{
					if (SpatialDataMapper.CreateCoreSpatialElementKey(spatialElementInfo.CoreSpatialElement, mapView.MapBindingFieldPairs, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name).Equals(spatialElementKey))
					{
						if (list == null)
						{
							list = new List<ISpatialElement>();
						}
						list.Add(spatialElementInfo.CoreSpatialElement);
					}
				}
			}
			return list;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000405D0 File Offset: 0x0003E7D0
		private string GetElementViewLayerName(MapElementView mapView)
		{
			ReportStringProperty layerName = mapView.LayerName;
			if (!layerName.IsExpression)
			{
				return layerName.Value;
			}
			return mapView.Instance.LayerName;
		}

		// Token: 0x06000EC8 RID: 3784
		protected abstract CoreSpatialElementManager GetSpatialElementManager();

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06000EC9 RID: 3785
		protected abstract ISpatialElementCollection SpatialElementCollection { get; }

		// Token: 0x06000ECA RID: 3786
		protected abstract void CreateRules();

		// Token: 0x06000ECB RID: 3787
		protected abstract void RenderRules();

		// Token: 0x06000ECC RID: 3788
		protected abstract void RenderSpatialElement(SpatialElementInfo spatialElementInfo, bool hasScope);

		// Token: 0x06000ECD RID: 3789
		internal abstract bool IsValidSpatialElement(ISpatialElement spatialElement);

		// Token: 0x06000ECE RID: 3790
		internal abstract void OnSpatialElementAdded(ISpatialElement spatialElement);

		// Token: 0x04000708 RID: 1800
		internal MapVectorLayer m_mapVectorLayer;

		// Token: 0x04000709 RID: 1801
		internal MapControl m_coreMap;

		// Token: 0x0400070A RID: 1802
		internal MapMapper m_mapMapper;

		// Token: 0x0400070B RID: 1803
		protected SpatialDataMapper m_spatialDataMapper;

		// Token: 0x0400070C RID: 1804
		protected PointTemplateMapper m_pointTemplateMapper;

		// Token: 0x0400070D RID: 1805
		private ColorRuleMapper m_pointColorRuleMapper;

		// Token: 0x0400070E RID: 1806
		private SizeRuleMapper m_pointlSizeRuleMapper;

		// Token: 0x0400070F RID: 1807
		private MarkerRuleMapper m_pointMarkerRuleMapper;

		// Token: 0x04000710 RID: 1808
		private CoreSymbolManager m_symbolManager;

		// Token: 0x04000711 RID: 1809
		private Dictionary<SpatialElementKey, SpatialElementInfoGroup> m_spatialElementsDictionary = new Dictionary<SpatialElementKey, SpatialElementInfoGroup>();
	}
}
