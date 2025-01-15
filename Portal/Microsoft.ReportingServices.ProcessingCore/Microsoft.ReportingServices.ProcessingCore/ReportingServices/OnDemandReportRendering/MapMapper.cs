using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000158 RID: 344
	internal class MapMapper : MapperBase, IMapMapper, IDVMappingLayer, IDisposable
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003B097 File Offset: 0x00039297
		public MapMapper(Map map, string defaultFontFamily)
			: base(defaultFontFamily)
		{
			this.m_map = map;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003B0C8 File Offset: 0x000392C8
		public void RenderMap()
		{
			try
			{
				if (this.m_map != null)
				{
					this.InitializeMap();
					this.SetMapProperties();
					this.RenderLayers();
					this.RenderViewport();
					this.RenderLegends();
					this.RenderTitles();
					this.RenderDistanceScale();
					this.RenderColorScale();
					this.RenderBorderSkin();
					this.RenderMapStyle();
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003B14C File Offset: 0x0003934C
		public Stream GetCoreXml()
		{
			Stream stream;
			try
			{
				this.m_coreMap.Serializer.Content = 2;
				this.m_coreMap.Serializer.NonSerializableContent = "";
				MemoryStream memoryStream = new MemoryStream();
				this.m_coreMap.Serializer.Save(memoryStream);
				memoryStream.Position = 0L;
				stream = memoryStream;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				stream = null;
			}
			return stream;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003B1D4 File Offset: 0x000393D4
		public Stream GetImage(DynamicImageInstance.ImageType imageType)
		{
			Stream stream;
			try
			{
				if (this.m_coreMap == null)
				{
					stream = null;
				}
				else
				{
					int num = 300;
					if (base.WidthOverrideInPixels != null)
					{
						num = base.WidthOverrideInPixels.Value;
					}
					else if (this.m_map.Width != null)
					{
						num = MappingHelper.ToIntPixels(this.m_map.Width, base.DpiX);
					}
					this.m_coreMap.Width = num;
					int num2 = 300;
					if (base.HeightOverrideInPixels != null)
					{
						num2 = base.HeightOverrideInPixels.Value;
					}
					else if (this.m_map.Height != null)
					{
						num2 = MappingHelper.ToIntPixels(this.m_map.Height, base.DpiY);
					}
					this.m_coreMap.Height = num2;
					Stream stream2 = null;
					if (imageType != DynamicImageInstance.ImageType.PNG)
					{
						if (imageType == DynamicImageInstance.ImageType.EMF)
						{
							this.GetEmfImage(out stream2, num, num2);
						}
					}
					else
					{
						this.GetPngImage(out stream2, num, num2);
					}
					stream2.Position = 0L;
					stream = stream2;
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
			return stream;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003B300 File Offset: 0x00039500
		public ActionInfoWithDynamicImageMapCollection GetImageMaps()
		{
			ActionInfoWithDynamicImageMapCollection actionInfoWithDynamicImageMapCollection = MappingHelper.GetImageMaps(this.GetMapAreaInfoList(), this.m_actions, this.m_map);
			ActionInfoWithDynamicImageMap mapImageMap = this.GetMapImageMap();
			if (mapImageMap != null)
			{
				if (actionInfoWithDynamicImageMapCollection == null)
				{
					actionInfoWithDynamicImageMapCollection = new ActionInfoWithDynamicImageMapCollection();
				}
				actionInfoWithDynamicImageMapCollection.InternalList.Add(mapImageMap);
			}
			return actionInfoWithDynamicImageMapCollection;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003B348 File Offset: 0x00039548
		private ActionInfoWithDynamicImageMap GetMapImageMap()
		{
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_map, this.m_map.ActionInfo, string.Empty, out text, true);
			if (actionInfoWithDynamicImageMap != null)
			{
				actionInfoWithDynamicImageMap.CreateImageMapAreaInstance(ImageMapArea.ImageMapAreaShape.Rectangle, new float[] { 0f, 0f, 100f, 100f }, string.Empty);
			}
			return actionInfoWithDynamicImageMap;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003B39B File Offset: 0x0003959B
		internal IEnumerable<MappingHelper.MapAreaInfo> GetMapAreaInfoList()
		{
			this.m_coreMap.mapCore.PopulateImageMaps();
			float width = (float)this.m_coreMap.Width;
			float height = (float)this.m_coreMap.Height;
			foreach (object obj in this.m_coreMap.MapAreas)
			{
				MapArea mapArea = (MapArea)obj;
				yield return new MappingHelper.MapAreaInfo(mapArea.ToolTip, mapArea.Tag, this.GetMapAreaShape(mapArea.Shape), MappingHelper.ConvertCoordinatesToRelative(mapArea.Coordinates, width, height));
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003B3AC File Offset: 0x000395AC
		private void InitializeMap()
		{
			this.m_coreMap = new MapControl();
			this.m_coreMap.mapCore.UppercaseFieldKeywords = false;
			this.m_coreMap.mapCore.SetUserLocales(new string[]
			{
				Localization.ClientBrowserCultureName,
				Localization.ClientCurrentCultureName,
				Localization.ClientPrimaryCulture.Name
			});
			this.m_coreMap.ShapeFields.Clear();
			this.m_coreMap.ShapeRules.Clear();
			this.m_coreMap.Shapes.Clear();
			this.m_coreMap.SymbolFields.Clear();
			this.m_coreMap.SymbolRules.Clear();
			this.m_coreMap.Symbols.Clear();
			this.m_coreMap.PathFields.Clear();
			this.m_coreMap.PathRules.Clear();
			this.m_coreMap.Paths.Clear();
			MapControl coreMap = this.m_coreMap;
			coreMap.FormatNumberHandler = (FormatNumberHandler)Delegate.Combine(coreMap.FormatNumberHandler, new FormatNumberHandler(this.FormatNumber));
			bool traceVerbose = RSTrace.ProcessingTracer.TraceVerbose;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003B4CC File Offset: 0x000396CC
		private void RenderViewport()
		{
			this.RenderSubItem(this.m_map.MapViewport, this.m_coreMap.Viewport);
			this.SetViewportProperties();
			this.RenderMapLimits();
			this.RenderMapView();
			this.RenderGridLines(this.m_map.MapViewport.MapMeridians, this.m_coreMap.Meridians);
			this.RenderGridLines(this.m_map.MapViewport.MapParallels, this.m_coreMap.Parallels);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003B549 File Offset: 0x00039749
		private void RenderGridLines(MapGridLines mapGridLines, GridAttributes coreGridLines)
		{
			if (mapGridLines == null)
			{
				return;
			}
			this.SetGridLinesProperties(mapGridLines, coreGridLines);
			this.RenderGridLinesStyle(mapGridLines, coreGridLines);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003B55F File Offset: 0x0003975F
		private void RenderSubItem(MapSubItem mapSubItem, Panel coreSubItem)
		{
			this.SetSubItemProperties(mapSubItem, coreSubItem);
			this.RenderSubItemStyle(mapSubItem, coreSubItem);
			if (mapSubItem != null)
			{
				this.RenderLocation(mapSubItem.MapLocation, coreSubItem);
			}
			if (mapSubItem != null)
			{
				this.RenderSize(mapSubItem.MapSize, coreSubItem);
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003B591 File Offset: 0x00039791
		private void RenderDockableSubItem(MapDockableSubItem mapDockableSubItem, DockablePanel coreSubItem)
		{
			this.RenderSubItem(mapDockableSubItem, coreSubItem);
			this.SetDockableSubItemProperties(mapDockableSubItem, coreSubItem);
			this.RenderActionInfo(mapDockableSubItem.ActionInfo, coreSubItem.ToolTip, coreSubItem, null, true);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003B5B8 File Offset: 0x000397B8
		private void RenderLegends()
		{
			if (this.m_map.MapLegends == null)
			{
				return;
			}
			foreach (MapLegend mapLegend in this.m_map.MapLegends)
			{
				this.RenderLegend(mapLegend);
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003B618 File Offset: 0x00039818
		private void RenderLayers()
		{
			if (this.m_map.MapLayers == null)
			{
				return;
			}
			foreach (MapLayer mapLayer in this.m_map.MapLayers)
			{
				this.RenderLayer(mapLayer);
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003B678 File Offset: 0x00039878
		private void RenderTitles()
		{
			if (this.m_map.MapTitles == null)
			{
				return;
			}
			foreach (MapTitle mapTitle in this.m_map.MapTitles)
			{
				this.RenderTitle(mapTitle);
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003B6D8 File Offset: 0x000398D8
		private void RenderLegend(MapLegend mapLegend)
		{
			Legend legend = new Legend();
			this.RenderDockableSubItem(mapLegend, legend);
			this.SetLegendProperties(mapLegend, legend);
			this.RenderLegendTitle(mapLegend.MapLegendTitle, legend);
			this.m_coreMap.Legends.Add(legend);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003B71A File Offset: 0x0003991A
		private void RenderLegendTitle(MapLegendTitle mapLegendTitle, Legend coreLegend)
		{
			if (mapLegendTitle == null)
			{
				return;
			}
			this.SetLegendTitleProperties(mapLegendTitle, coreLegend);
			this.RenderLegendTitleStyle(mapLegendTitle, coreLegend);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003B730 File Offset: 0x00039930
		private void RenderTitle(MapTitle mapTitle)
		{
			MapLabel mapLabel = new MapLabel();
			this.RenderDockableSubItem(mapTitle, mapLabel);
			this.SetTitleProperties(mapTitle, mapLabel);
			this.m_coreMap.Labels.Add(mapLabel);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003B768 File Offset: 0x00039968
		private void RenderLayer(MapLayer mapLayer)
		{
			Layer layer = new Layer();
			this.SetLayerProperties(mapLayer, layer);
			this.m_coreMap.Layers.Add(layer);
			if (mapLayer is MapTileLayer)
			{
				this.RenderTileLayer((MapTileLayer)mapLayer);
				return;
			}
			if (mapLayer is MapVectorLayer)
			{
				this.RenderVectorLayer((MapVectorLayer)mapLayer);
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003B7C0 File Offset: 0x000399C0
		private void SetLayerProperties(MapLayer mapLayer, Layer coreLayer)
		{
			coreLayer.Name = mapLayer.Name;
			ReportDoubleProperty transparency = mapLayer.Transparency;
			if (transparency != null)
			{
				if (!transparency.IsExpression)
				{
					coreLayer.Transparency = (float)transparency.Value;
				}
				else
				{
					coreLayer.Transparency = (float)mapLayer.Instance.Transparency;
				}
			}
			else
			{
				coreLayer.Transparency = 0f;
			}
			ReportDoubleProperty maximumZoom = mapLayer.MaximumZoom;
			if (maximumZoom != null)
			{
				if (!maximumZoom.IsExpression)
				{
					coreLayer.VisibleToZoom = (float)maximumZoom.Value;
				}
				else
				{
					coreLayer.VisibleToZoom = (float)mapLayer.Instance.MaximumZoom;
				}
			}
			else
			{
				coreLayer.VisibleToZoom = 200f;
			}
			ReportDoubleProperty minimumZoom = mapLayer.MinimumZoom;
			if (minimumZoom != null)
			{
				if (!minimumZoom.IsExpression)
				{
					coreLayer.VisibleFromZoom = (float)minimumZoom.Value;
				}
				else
				{
					coreLayer.VisibleFromZoom = (float)mapLayer.Instance.MinimumZoom;
				}
			}
			else
			{
				coreLayer.VisibleFromZoom = 50f;
			}
			ReportEnumProperty<MapVisibilityMode> visibilityMode = mapLayer.VisibilityMode;
			if (visibilityMode == null)
			{
				coreLayer.Visibility = 0;
				return;
			}
			if (!visibilityMode.IsExpression)
			{
				coreLayer.Visibility = MapMapper.GetLayerVisibility(visibilityMode.Value);
				return;
			}
			coreLayer.Visibility = MapMapper.GetLayerVisibility(mapLayer.Instance.VisibilityMode);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003B8DC File Offset: 0x00039ADC
		private void RenderVectorLayer(MapVectorLayer mapVectorLayer)
		{
			if (mapVectorLayer is MapPolygonLayer)
			{
				new PolygonLayerMapper((MapPolygonLayer)mapVectorLayer, this.m_coreMap, this).Render();
				return;
			}
			if (mapVectorLayer is MapPointLayer)
			{
				new PointLayerMapper((MapPointLayer)mapVectorLayer, this.m_coreMap, this).Render();
				return;
			}
			if (mapVectorLayer is MapLineLayer)
			{
				new LineLayerMapper((MapLineLayer)mapVectorLayer, this.m_coreMap, this).Render();
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003B948 File Offset: 0x00039B48
		private void RenderTileLayer(MapTileLayer mapTileLayer)
		{
			if (this.m_tileLayerMapper == null)
			{
				this.m_tileLayerMapper = new TileLayerMapper(this.m_map, this.m_coreMap);
			}
			this.m_tileLayerMapper.AddLayer(mapTileLayer);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003B975 File Offset: 0x00039B75
		private void RenderDistanceScale()
		{
			if (this.m_map.MapDistanceScale == null)
			{
				return;
			}
			this.RenderDockableSubItem(this.m_map.MapDistanceScale, this.m_coreMap.DistanceScalePanel);
			this.SetDistanceScaleProperties();
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003B9A7 File Offset: 0x00039BA7
		private void RenderColorScale()
		{
			if (this.m_map.MapColorScale == null)
			{
				return;
			}
			this.RenderDockableSubItem(this.m_map.MapColorScale, this.m_coreMap.ColorSwatchPanel);
			this.SetColorScaleProperties();
			this.RenderColorScaleTitle();
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003B9DF File Offset: 0x00039BDF
		private void RenderColorScaleTitle()
		{
			this.SetColorScaleTitleProperties();
			this.RenderColorScaleTitleStyle();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003B9ED File Offset: 0x00039BED
		private void RenderBorderSkin()
		{
			if (this.m_map.MapBorderSkin == null)
			{
				return;
			}
			this.SetBorderSkinProperties();
			this.RenderBorderSkinStyle();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003BA0C File Offset: 0x00039C0C
		private void SetSubItemProperties(MapSubItem mapSubItem, Panel coreSubItem)
		{
			ReportSizeProperty reportSizeProperty = mapSubItem.LeftMargin;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					coreSubItem.Margins.Left = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiX);
				}
				else
				{
					coreSubItem.Margins.Left = MappingHelper.ToIntPixels(mapSubItem.Instance.LeftMargin, base.DpiX);
				}
			}
			reportSizeProperty = mapSubItem.TopMargin;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					coreSubItem.Margins.Top = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiY);
				}
				else
				{
					coreSubItem.Margins.Top = MappingHelper.ToIntPixels(mapSubItem.Instance.TopMargin, base.DpiY);
				}
			}
			reportSizeProperty = mapSubItem.RightMargin;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					coreSubItem.Margins.Right = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiX);
				}
				else
				{
					coreSubItem.Margins.Right = MappingHelper.ToIntPixels(mapSubItem.Instance.RightMargin, base.DpiX);
				}
			}
			reportSizeProperty = mapSubItem.BottomMargin;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					coreSubItem.Margins.Bottom = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiX);
				}
				else
				{
					coreSubItem.Margins.Bottom = MappingHelper.ToIntPixels(mapSubItem.Instance.BottomMargin, base.DpiX);
				}
			}
			ReportIntProperty zindex = mapSubItem.ZIndex;
			if (zindex != null)
			{
				if (!zindex.IsExpression)
				{
					coreSubItem.ZOrder = zindex.Value;
					return;
				}
				coreSubItem.ZOrder = mapSubItem.Instance.ZIndex;
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003BB90 File Offset: 0x00039D90
		private void RenderLocation(MapLocation mapLocation, Panel coreSubItem)
		{
			if (mapLocation == null)
			{
				return;
			}
			ReportEnumProperty<Unit> unit = mapLocation.Unit;
			Unit unit2 = Unit.Percentage;
			if (unit != null)
			{
				if (!unit.IsExpression)
				{
					unit2 = unit.Value;
				}
				else
				{
					unit2 = mapLocation.Instance.Unit;
				}
			}
			coreSubItem.LocationUnit = ((unit2 == Unit.Percentage) ? 1 : 0);
			ReportDoubleProperty reportDoubleProperty = mapLocation.Left;
			if (reportDoubleProperty != null)
			{
				double num;
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else
				{
					num = mapLocation.Instance.Left;
				}
				if (unit2 != Unit.Percentage)
				{
					num = MappingHelper.ToPixels(num, unit2, base.DpiX);
				}
				coreSubItem.Location.X = (float)num;
			}
			reportDoubleProperty = mapLocation.Top;
			if (reportDoubleProperty != null)
			{
				double num;
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else
				{
					num = mapLocation.Instance.Top;
				}
				if (unit2 != Unit.Percentage)
				{
					num = MappingHelper.ToPixels(num, unit2, base.DpiY);
				}
				coreSubItem.Location.Y = (float)num;
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003BC64 File Offset: 0x00039E64
		private void RenderSize(MapSize mapSize, Panel coreSubItem)
		{
			if (mapSize == null)
			{
				return;
			}
			ReportEnumProperty<Unit> unit = mapSize.Unit;
			Unit unit2 = Unit.Percentage;
			if (unit != null)
			{
				if (!unit.IsExpression)
				{
					unit2 = unit.Value;
				}
				else
				{
					unit2 = mapSize.Instance.Unit;
				}
			}
			coreSubItem.SizeUnit = ((unit2 == Unit.Percentage) ? 1 : 0);
			ReportDoubleProperty reportDoubleProperty = mapSize.Width;
			if (reportDoubleProperty != null)
			{
				double num;
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else
				{
					num = mapSize.Instance.Width;
				}
				if (unit2 != Unit.Percentage)
				{
					num = MappingHelper.ToPixels(num, unit2, base.DpiX);
				}
				coreSubItem.Size.Width = (float)num;
			}
			reportDoubleProperty = mapSize.Height;
			if (reportDoubleProperty != null)
			{
				double num;
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else
				{
					num = mapSize.Instance.Height;
				}
				if (unit2 != Unit.Percentage)
				{
					num = MappingHelper.ToPixels(num, unit2, base.DpiY);
				}
				coreSubItem.Size.Height = (float)num;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003BD38 File Offset: 0x00039F38
		private void RenderMapLimits()
		{
			MapLimits mapLimits = this.m_map.MapViewport.MapLimits;
			if (mapLimits == null)
			{
				return;
			}
			ReportDoubleProperty reportDoubleProperty = mapLimits.MinimumX;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.MapLimits.MinimumX = reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.MapLimits.MinimumX = mapLimits.Instance.MinimumX;
				}
			}
			reportDoubleProperty = mapLimits.MinimumY;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.MapLimits.MinimumY = reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.MapLimits.MinimumY = mapLimits.Instance.MinimumY;
				}
			}
			reportDoubleProperty = mapLimits.MaximumX;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.MapLimits.MaximumX = reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.MapLimits.MaximumX = mapLimits.Instance.MaximumX;
				}
			}
			reportDoubleProperty = mapLimits.MaximumY;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.MapLimits.MaximumY = reportDoubleProperty.Value;
					return;
				}
				this.m_coreMap.MapLimits.MaximumY = mapLimits.Instance.MaximumY;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003BE70 File Offset: 0x0003A070
		private void RenderMapView()
		{
			MapView mapView = this.m_map.MapViewport.MapView;
			if (mapView == null)
			{
				return;
			}
			ReportDoubleProperty zoom = mapView.Zoom;
			double num = 0.0;
			if (zoom != null)
			{
				if (!zoom.IsExpression)
				{
					num = (double)((float)zoom.Value);
				}
				else
				{
					num = (double)((float)mapView.Instance.Zoom);
				}
			}
			if (num != 0.0)
			{
				this.m_coreMap.Viewport.Zoom = (float)num;
			}
			if (mapView is MapCustomView)
			{
				this.RenderCustomView((MapCustomView)mapView);
				return;
			}
			if (this.m_boundRectCalculator != null)
			{
				this.CenterView(num == 0.0);
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003BF14 File Offset: 0x0003A114
		private void CenterView(bool zoomToFit)
		{
			if (zoomToFit)
			{
				this.m_coreMap.MapLimits.MinimumX = this.m_boundRectCalculator.Min.X;
				this.m_coreMap.MapLimits.MinimumY = this.m_boundRectCalculator.Min.Y;
				this.m_coreMap.MapLimits.MaximumX = this.m_boundRectCalculator.Max.X;
				this.m_coreMap.MapLimits.MaximumY = this.m_boundRectCalculator.Max.Y;
				this.m_coreMap.Viewport.Zoom = 100f;
				return;
			}
			this.m_coreMap.CenterView(this.m_boundRectCalculator.Center);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003BFD3 File Offset: 0x0003A1D3
		internal void AddSpatialElementToView(ISpatialElement spatialElement)
		{
			if (this.m_boundRectCalculator == null)
			{
				this.m_boundRectCalculator = new MapMapper.BoundsRectCalculator();
			}
			this.m_boundRectCalculator.AddSpatialElement(spatialElement);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003BFF4 File Offset: 0x0003A1F4
		private void RenderCustomView(MapCustomView mapView)
		{
			ReportDoubleProperty reportDoubleProperty = mapView.CenterX;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.Viewport.ViewCenter.X = (float)reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.Viewport.ViewCenter.X = (float)mapView.Instance.CenterX;
				}
			}
			reportDoubleProperty = mapView.CenterY;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.Viewport.ViewCenter.Y = (float)reportDoubleProperty.Value;
					return;
				}
				this.m_coreMap.Viewport.ViewCenter.Y = (float)mapView.Instance.CenterY;
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
		private void SetDockableSubItemProperties(MapDockableSubItem mapDockableSubItem, DockablePanel coreDockableSubItem)
		{
			ReportEnumProperty<MapPosition> position = mapDockableSubItem.Position;
			MapPosition mapPosition = MapPosition.TopCenter;
			if (mapDockableSubItem.Position != null)
			{
				if (!mapDockableSubItem.Position.IsExpression)
				{
					mapPosition = mapDockableSubItem.Position.Value;
				}
				else
				{
					mapPosition = mapDockableSubItem.Instance.Position;
				}
			}
			coreDockableSubItem.DockAlignment = this.GetDockablePanelAlignment(mapPosition);
			coreDockableSubItem.Dock = ((mapDockableSubItem.MapLocation == null) ? this.GetDockablePanelDocking(mapPosition) : 0);
			ReportBoolProperty dockOutsideViewport = mapDockableSubItem.DockOutsideViewport;
			if (dockOutsideViewport != null)
			{
				if (!dockOutsideViewport.IsExpression)
				{
					coreDockableSubItem.DockedInsideViewport = !dockOutsideViewport.Value;
				}
				else
				{
					coreDockableSubItem.DockedInsideViewport = !mapDockableSubItem.Instance.DockOutsideViewport;
				}
			}
			else
			{
				coreDockableSubItem.DockedInsideViewport = true;
			}
			ReportBoolProperty hidden = mapDockableSubItem.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					coreDockableSubItem.Visible = !hidden.Value;
				}
				else
				{
					coreDockableSubItem.Visible = !mapDockableSubItem.Instance.Hidden;
				}
			}
			else
			{
				coreDockableSubItem.Visible = true;
			}
			ReportStringProperty toolTip = mapDockableSubItem.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					coreDockableSubItem.ToolTip = toolTip.Value;
					return;
				}
				coreDockableSubItem.ToolTip = mapDockableSubItem.Instance.ToolTip;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003C1BE File Offset: 0x0003A3BE
		private DockAlignment GetDockablePanelAlignment(MapPosition position)
		{
			switch (position)
			{
			case MapPosition.TopCenter:
			case MapPosition.LeftCenter:
			case MapPosition.RightCenter:
			case MapPosition.BottomCenter:
				return 1;
			case MapPosition.TopRight:
			case MapPosition.LeftBottom:
			case MapPosition.RightBottom:
			case MapPosition.BottomRight:
				return 2;
			}
			return 0;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003C1FC File Offset: 0x0003A3FC
		private PanelDockStyle GetDockablePanelDocking(MapPosition position)
		{
			switch (position)
			{
			case MapPosition.TopCenter:
			case MapPosition.TopLeft:
			case MapPosition.TopRight:
				return 1;
			case MapPosition.LeftTop:
			case MapPosition.LeftCenter:
			case MapPosition.LeftBottom:
				return 2;
			case MapPosition.BottomRight:
			case MapPosition.BottomCenter:
			case MapPosition.BottomLeft:
				return 4;
			}
			return 3;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003C248 File Offset: 0x0003A448
		private void SetMapProperties()
		{
			if (this.m_map.AntiAliasing != null)
			{
				if (!this.m_map.AntiAliasing.IsExpression)
				{
					this.m_coreMap.AntiAliasing = this.GetAntiAliasing(this.m_map.AntiAliasing.Value);
				}
				else
				{
					this.m_coreMap.AntiAliasing = this.GetAntiAliasing(this.m_map.Instance.AntiAliasing);
				}
			}
			if (this.m_map.ShadowIntensity != null)
			{
				if (!this.m_map.ShadowIntensity.IsExpression)
				{
					this.m_coreMap.ShadowIntensity = (float)this.m_map.ShadowIntensity.Value;
				}
				else
				{
					this.m_coreMap.ShadowIntensity = (float)this.m_map.Instance.ShadowIntensity;
				}
			}
			if (this.m_map.TextAntiAliasingQuality != null)
			{
				if (!this.m_map.TextAntiAliasingQuality.IsExpression)
				{
					this.m_coreMap.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_map.TextAntiAliasingQuality.Value);
				}
				else
				{
					this.m_coreMap.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_map.Instance.TextAntiAliasingQuality);
				}
			}
			this.m_remainingSpatialElementCount = this.m_map.MaximumSpatialElementCount;
			this.m_remainingTotalPointCount = this.m_map.MaximumTotalPointCount;
			this.SetTileServerConfiguration();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003C39C File Offset: 0x0003A59C
		private void SetTileServerConfiguration()
		{
			IConfiguration configuration = this.m_map.RenderingContext.OdpContext.Configuration;
			IMapTileServerConfiguration mapTileServerConfiguration = null;
			if (configuration != null)
			{
				mapTileServerConfiguration = configuration.MapTileServerConfiguration;
			}
			if (mapTileServerConfiguration != null)
			{
				this.m_coreMap.TileServerMaxConnections = mapTileServerConfiguration.MaxConnections;
				this.m_coreMap.TileServerTimeout = mapTileServerConfiguration.Timeout * 1000;
				this.m_coreMap.TileServerAppId = mapTileServerConfiguration.AppID;
				this.m_coreMap.TileCacheLevel = MapTileServerConsts.ConvertFromMapTileCacheLevel(mapTileServerConfiguration.CacheLevel);
				this.m_coreMap.TileCulture = this.GetTileLanguage();
				this.m_coreMap.TileServerEnabled = mapTileServerConfiguration.Enabled;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003C440 File Offset: 0x0003A640
		private CultureInfo GetTileLanguage()
		{
			return new Formatter(this.m_map.MapDef.StyleClass, this.m_map.RenderingContext.OdpContext, this.m_map.MapDef.ObjectType, this.m_map.Name).GetCulture(this.EvaluateLanguage());
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003C498 File Offset: 0x0003A698
		private string EvaluateLanguage()
		{
			ReportStringProperty reportStringProperty = this.m_map.TileLanguage;
			if (reportStringProperty == null)
			{
				if (this.m_map.Style != null)
				{
					reportStringProperty = this.m_map.Style.Language;
					if (reportStringProperty != null)
					{
						if (!reportStringProperty.IsExpression)
						{
							return reportStringProperty.Value;
						}
						return this.m_map.Instance.Style.Language;
					}
				}
				return null;
			}
			if (!reportStringProperty.IsExpression)
			{
				return reportStringProperty.Value;
			}
			return this.m_map.Instance.TileLanguage;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003C51B File Offset: 0x0003A71B
		private AntiAliasing GetAntiAliasing(MapAntiAliasing mapAntiAliasing)
		{
			switch (mapAntiAliasing)
			{
			case MapAntiAliasing.None:
				return 0;
			case MapAntiAliasing.Text:
				return 1;
			case MapAntiAliasing.Graphics:
				return 2;
			default:
				return 3;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003C53A File Offset: 0x0003A73A
		private TextAntiAliasingQuality GetTextAntiAliasingQuality(MapTextAntiAliasingQuality textAntiAliasingQuality)
		{
			if (textAntiAliasingQuality == MapTextAntiAliasingQuality.Normal)
			{
				return 0;
			}
			if (textAntiAliasingQuality != MapTextAntiAliasingQuality.SystemDefault)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003C54C File Offset: 0x0003A74C
		private void SetViewportProperties()
		{
			MapViewport mapViewport = this.m_map.MapViewport;
			this.m_coreMap.Viewport.AutoSize = mapViewport.MapSize == null && mapViewport.MapLocation == null;
			ReportEnumProperty<MapCoordinateSystem> mapCoordinateSystem = mapViewport.MapCoordinateSystem;
			MapCoordinateSystem mapCoordinateSystem2;
			if (mapCoordinateSystem != null)
			{
				if (!mapCoordinateSystem.IsExpression)
				{
					mapCoordinateSystem2 = mapCoordinateSystem.Value;
				}
				else
				{
					mapCoordinateSystem2 = mapViewport.Instance.MapCoordinateSystem;
				}
			}
			else
			{
				mapCoordinateSystem2 = MapCoordinateSystem.Planar;
			}
			this.m_coreMap.GeographyMode = mapCoordinateSystem2 == MapCoordinateSystem.Geographic;
			ReportEnumProperty<MapProjection> mapProjection = mapViewport.MapProjection;
			MapProjection mapProjection2;
			if (mapProjection != null)
			{
				if (!mapProjection.IsExpression)
				{
					mapProjection2 = mapProjection.Value;
				}
				else
				{
					mapProjection2 = mapViewport.Instance.MapProjection;
				}
			}
			else
			{
				mapProjection2 = MapProjection.Equirectangular;
			}
			this.m_coreMap.Projection = this.GetProjection(mapProjection2);
			ReportDoubleProperty reportDoubleProperty = mapViewport.ProjectionCenterX;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.ProjectionCenter.X = reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.ProjectionCenter.X = mapViewport.Instance.ProjectionCenterX;
				}
			}
			reportDoubleProperty = mapViewport.ProjectionCenterY;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					this.m_coreMap.ProjectionCenter.Y = reportDoubleProperty.Value;
				}
				else
				{
					this.m_coreMap.ProjectionCenter.Y = mapViewport.Instance.ProjectionCenterY;
				}
			}
			ReportDoubleProperty reportDoubleProperty2 = mapViewport.MaximumZoom;
			if (reportDoubleProperty2 != null)
			{
				if (!reportDoubleProperty2.IsExpression)
				{
					this.m_coreMap.Viewport.MaximumZoom = (int)Math.Round(reportDoubleProperty2.Value);
				}
				else
				{
					this.m_coreMap.Viewport.MaximumZoom = (int)Math.Round(mapViewport.Instance.MaximumZoom);
				}
			}
			reportDoubleProperty2 = mapViewport.MinimumZoom;
			if (reportDoubleProperty2 != null)
			{
				if (!reportDoubleProperty2.IsExpression)
				{
					this.m_coreMap.Viewport.MinimumZoom = (int)Math.Round(reportDoubleProperty2.Value);
				}
				else
				{
					this.m_coreMap.Viewport.MinimumZoom = (int)Math.Round(mapViewport.Instance.MinimumZoom);
				}
			}
			ReportSizeProperty contentMargin = mapViewport.ContentMargin;
			ReportSize reportSize;
			if (contentMargin != null)
			{
				if (!contentMargin.IsExpression)
				{
					reportSize = mapViewport.ContentMargin.Value;
				}
				else
				{
					reportSize = mapViewport.Instance.ContentMargin;
				}
			}
			else
			{
				reportSize = MapMapper.m_defaultContentMargin;
			}
			this.m_coreMap.Viewport.ContentAutoFitMargin = MappingHelper.ToIntPixels(reportSize, base.DpiX);
			ReportBoolProperty gridUnderContent = mapViewport.GridUnderContent;
			if (gridUnderContent == null)
			{
				this.m_coreMap.GridUnderContent = false;
				return;
			}
			if (!gridUnderContent.IsExpression)
			{
				this.m_coreMap.GridUnderContent = gridUnderContent.Value;
				return;
			}
			this.m_coreMap.GridUnderContent = gridUnderContent.Value;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003C7DE File Offset: 0x0003A9DE
		private Projection GetProjection(MapProjection projection)
		{
			switch (projection)
			{
			case MapProjection.Mercator:
				return 1;
			case MapProjection.Robinson:
				return 2;
			case MapProjection.Fahey:
				return 3;
			case MapProjection.Eckert1:
				return 4;
			case MapProjection.Eckert3:
				return 5;
			case MapProjection.HammerAitoff:
				return 6;
			case MapProjection.Wagner3:
				return 7;
			case MapProjection.Bonne:
				return 9;
			default:
				return 0;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003C81C File Offset: 0x0003AA1C
		private void SetGridLinesProperties(MapGridLines mapGridLines, GridAttributes coreGridLines)
		{
			if (mapGridLines == null)
			{
				return;
			}
			ReportBoolProperty hidden = mapGridLines.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					coreGridLines.Visible = !hidden.Value;
				}
				else
				{
					coreGridLines.Visible = !mapGridLines.Instance.Hidden;
				}
			}
			ReportDoubleProperty interval = mapGridLines.Interval;
			if (interval != null)
			{
				if (!interval.IsExpression)
				{
					coreGridLines.Interval = interval.Value;
				}
				else
				{
					coreGridLines.Interval = mapGridLines.Instance.Interval;
				}
			}
			ReportBoolProperty showLabels = mapGridLines.ShowLabels;
			if (showLabels != null)
			{
				if (!showLabels.IsExpression)
				{
					coreGridLines.ShowLabels = showLabels.Value;
				}
				else
				{
					coreGridLines.ShowLabels = mapGridLines.Instance.ShowLabels;
				}
			}
			ReportEnumProperty<MapLabelPosition> labelPosition = mapGridLines.LabelPosition;
			MapLabelPosition mapLabelPosition = MapLabelPosition.Near;
			if (labelPosition != null)
			{
				if (!labelPosition.IsExpression)
				{
					mapLabelPosition = labelPosition.Value;
				}
				else
				{
					mapLabelPosition = mapGridLines.Instance.LabelPosition;
				}
			}
			coreGridLines.LabelPosition = this.GetLabelPosition(mapLabelPosition);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003C900 File Offset: 0x0003AB00
		private LabelPosition GetLabelPosition(MapLabelPosition labelPosition)
		{
			switch (labelPosition)
			{
			case MapLabelPosition.OneQuarter:
				return 1;
			case MapLabelPosition.Center:
				return 2;
			case MapLabelPosition.ThreeQuarters:
				return 3;
			case MapLabelPosition.Far:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003C928 File Offset: 0x0003AB28
		private void SetLegendProperties(MapLegend mapLegend, Legend legend)
		{
			legend.MaxAutoSize = 50f;
			Style style = mapLegend.Style;
			if (style == null)
			{
				legend.Font = base.GetDefaultFontFromCache(0);
			}
			else
			{
				StyleInstance style2 = mapLegend.Instance.Style;
				legend.Font = base.GetFontFromCache(0, style, style2);
				legend.TextColor = MappingHelper.GetStyleColor(style, style2);
			}
			legend.AutoSize = mapLegend.MapSize == null;
			if (mapLegend.Hidden != null)
			{
				if (!mapLegend.Hidden.IsExpression)
				{
					legend.Visible = !mapLegend.Hidden.Value;
				}
				else
				{
					legend.Visible = !mapLegend.Instance.Hidden;
				}
			}
			if (mapLegend.Layout != null)
			{
				if (!mapLegend.Layout.IsExpression)
				{
					this.SetLegendLayout(mapLegend.Layout.Value, legend);
				}
				else
				{
					this.SetLegendLayout(mapLegend.Instance.Layout, legend);
				}
			}
			if (mapLegend.AutoFitTextDisabled != null)
			{
				if (!mapLegend.AutoFitTextDisabled.IsExpression)
				{
					legend.AutoFitText = !mapLegend.AutoFitTextDisabled.Value;
				}
				else
				{
					legend.AutoFitText = !mapLegend.Instance.AutoFitTextDisabled;
				}
			}
			else
			{
				legend.AutoFitText = true;
			}
			if (mapLegend.EquallySpacedItems != null)
			{
				if (!mapLegend.EquallySpacedItems.IsExpression)
				{
					legend.EquallySpacedItems = mapLegend.EquallySpacedItems.Value;
				}
				else
				{
					legend.EquallySpacedItems = mapLegend.Instance.EquallySpacedItems;
				}
			}
			if (mapLegend.InterlacedRows != null)
			{
				if (!mapLegend.InterlacedRows.IsExpression)
				{
					legend.InterlacedRows = mapLegend.InterlacedRows.Value;
				}
				else
				{
					legend.InterlacedRows = mapLegend.Instance.InterlacedRows;
				}
			}
			if (mapLegend.InterlacedRowsColor != null)
			{
				Color empty = Color.Empty;
				if (MappingHelper.GetColorFromReportColorProperty(mapLegend.InterlacedRowsColor, ref empty))
				{
					legend.InterlacedRowsColor = empty;
				}
				else if (mapLegend.Instance.InterlacedRowsColor != null)
				{
					legend.InterlacedRowsColor = mapLegend.Instance.InterlacedRowsColor.ToColor();
				}
			}
			if (mapLegend.MinFontSize != null)
			{
				if (!mapLegend.MinFontSize.IsExpression)
				{
					legend.AutoFitMinFontSize = (int)Math.Round(mapLegend.MinFontSize.Value.ToPoints());
				}
				else
				{
					legend.AutoFitMinFontSize = (int)Math.Round(mapLegend.Instance.MinFontSize.ToPoints());
				}
			}
			if (mapLegend.TextWrapThreshold != null)
			{
				if (!mapLegend.TextWrapThreshold.IsExpression)
				{
					legend.TextWrapThreshold = mapLegend.TextWrapThreshold.Value;
					return;
				}
				legend.TextWrapThreshold = mapLegend.Instance.TextWrapThreshold;
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003CB94 File Offset: 0x0003AD94
		private void SetLegendLayout(MapLegendLayout layout, Legend legend)
		{
			switch (layout)
			{
			case MapLegendLayout.AutoTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 0;
				return;
			case MapLegendLayout.Column:
				legend.LegendStyle = 0;
				return;
			case MapLegendLayout.Row:
				legend.LegendStyle = 1;
				return;
			case MapLegendLayout.WideTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 1;
				return;
			case MapLegendLayout.TallTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 2;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003CBF8 File Offset: 0x0003ADF8
		private void SetLegendTitleProperties(MapLegendTitle mapLegendTitle, Legend legend)
		{
			if (mapLegendTitle.Caption != null)
			{
				if (!mapLegendTitle.Caption.IsExpression)
				{
					if (mapLegendTitle.Caption.Value != null)
					{
						legend.Title = mapLegendTitle.Caption.Value;
					}
				}
				else if (mapLegendTitle.Instance.Caption != null)
				{
					legend.Title = mapLegendTitle.Instance.Caption;
				}
			}
			if (mapLegendTitle.TitleSeparator != null)
			{
				if (!mapLegendTitle.TitleSeparator.IsExpression)
				{
					legend.TitleSeparator = this.GetLegendSeparatorStyle(mapLegendTitle.TitleSeparator.Value);
					return;
				}
				legend.TitleSeparator = this.GetLegendSeparatorStyle(mapLegendTitle.Instance.TitleSeparator);
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		private LegendSeparatorType GetLegendSeparatorStyle(MapLegendTitleSeparator legendTitleSeparator)
		{
			switch (legendTitleSeparator)
			{
			case MapLegendTitleSeparator.Line:
				return 1;
			case MapLegendTitleSeparator.ThickLine:
				return 2;
			case MapLegendTitleSeparator.DoubleLine:
				return 3;
			case MapLegendTitleSeparator.DashLine:
				return 4;
			case MapLegendTitleSeparator.DotLine:
				return 5;
			case MapLegendTitleSeparator.GradientLine:
				return 6;
			case MapLegendTitleSeparator.ThickGradientLine:
				return 7;
			default:
				return 0;
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003CCD4 File Offset: 0x0003AED4
		private void SetTitleProperties(MapTitle mapTitle, MapLabel coreTitle)
		{
			Style style = mapTitle.Style;
			if (style == null)
			{
				coreTitle.Font = base.GetDefaultFontFromCache(0);
			}
			else
			{
				StyleInstance style2 = mapTitle.Instance.Style;
				coreTitle.Font = base.GetFontFromCache(0, style, style2);
				coreTitle.TextColor = MappingHelper.GetStyleColor(style, style2);
				coreTitle.TextAlignment = MappingHelper.GetStyleContentAlignment(style, style2);
			}
			coreTitle.AutoSize = mapTitle.MapSize == null;
			coreTitle.Name = mapTitle.Name;
			ReportDoubleProperty angle = mapTitle.Angle;
			if (angle != null)
			{
				if (!angle.IsExpression)
				{
					coreTitle.Angle = (float)angle.Value;
				}
				else
				{
					coreTitle.Angle = (float)mapTitle.Instance.Angle;
				}
			}
			ReportStringProperty text = mapTitle.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						coreTitle.Text = text.Value;
					}
				}
				else
				{
					string text2 = mapTitle.Instance.Text;
					if (text2 != null)
					{
						coreTitle.Text = text2;
					}
				}
			}
			ReportSizeProperty textShadowOffset = mapTitle.TextShadowOffset;
			if (textShadowOffset != null)
			{
				int num;
				if (!textShadowOffset.IsExpression)
				{
					num = MappingHelper.ToIntPixels(textShadowOffset.Value, base.DpiX);
				}
				else
				{
					num = MappingHelper.ToIntPixels(mapTitle.Instance.TextShadowOffset, base.DpiX);
				}
				coreTitle.TextShadowOffset = MapMapper.GetValidShadowOffset(num);
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003CE10 File Offset: 0x0003B010
		private void SetDistanceScaleProperties()
		{
			MapDistanceScale mapDistanceScale = this.m_map.MapDistanceScale;
			Style style = mapDistanceScale.Style;
			if (style == null)
			{
				this.m_coreMap.DistanceScalePanel.Font = base.GetDefaultFontFromCache(0);
			}
			else
			{
				StyleInstance style2 = mapDistanceScale.Instance.Style;
				this.m_coreMap.DistanceScalePanel.Font = base.GetFontFromCache(0, style, style2);
				this.m_coreMap.DistanceScalePanel.LabelColor = MappingHelper.GetStyleColor(style, style2);
			}
			ReportColorProperty reportColorProperty = mapDistanceScale.ScaleColor;
			Color empty = Color.Empty;
			if (reportColorProperty != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(reportColorProperty, ref empty))
				{
					this.m_coreMap.DistanceScalePanel.ScaleForeColor = empty;
				}
				else
				{
					ReportColor scaleColor = mapDistanceScale.Instance.ScaleColor;
					if (scaleColor != null)
					{
						this.m_coreMap.DistanceScalePanel.ScaleForeColor = scaleColor.ToColor();
					}
				}
			}
			else
			{
				this.m_coreMap.DistanceScalePanel.ScaleForeColor = Color.White;
			}
			reportColorProperty = mapDistanceScale.ScaleBorderColor;
			if (reportColorProperty != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(reportColorProperty, ref empty))
				{
					this.m_coreMap.DistanceScalePanel.ScaleBorderColor = empty;
				}
				else
				{
					ReportColor scaleBorderColor = mapDistanceScale.Instance.ScaleBorderColor;
					if (scaleBorderColor != null)
					{
						this.m_coreMap.DistanceScalePanel.ScaleBorderColor = scaleBorderColor.ToColor();
					}
				}
			}
			else
			{
				this.m_coreMap.DistanceScalePanel.ScaleBorderColor = Color.DarkGray;
			}
			if (!this.m_coreMap.GeographyMode)
			{
				this.m_coreMap.DistanceScalePanel.Visible = false;
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003CF7C File Offset: 0x0003B17C
		private void SetColorScaleProperties()
		{
			MapColorScale mapColorScale = this.m_map.MapColorScale;
			Style style = mapColorScale.Style;
			if (style == null)
			{
				this.m_coreMap.ColorSwatchPanel.Font = base.GetDefaultFontFromCache(0);
			}
			else
			{
				StyleInstance style2 = mapColorScale.Instance.Style;
				this.m_coreMap.ColorSwatchPanel.Font = base.GetFontFromCache(0, style, style2);
				this.m_coreMap.ColorSwatchPanel.LabelColor = MappingHelper.GetStyleColor(style, style2);
			}
			this.m_coreMap.ColorSwatchPanel.AutoSize = mapColorScale.MapSize == null;
			ReportSizeProperty tickMarkLength = mapColorScale.TickMarkLength;
			ReportSize reportSize;
			if (tickMarkLength != null)
			{
				if (!tickMarkLength.IsExpression)
				{
					reportSize = tickMarkLength.Value;
				}
				else
				{
					reportSize = mapColorScale.Instance.TickMarkLength;
				}
			}
			else
			{
				reportSize = MapMapper.m_defaultTickMarkLength;
			}
			this.m_coreMap.ColorSwatchPanel.TickMarkLength = MappingHelper.ToIntPixels(reportSize, base.DpiX);
			ReportColorProperty reportColorProperty = mapColorScale.ColorBarBorderColor;
			Color empty = Color.Empty;
			if (reportColorProperty != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(reportColorProperty, ref empty))
				{
					this.m_coreMap.ColorSwatchPanel.OutlineColor = empty;
				}
				else if (mapColorScale.Instance.ColorBarBorderColor != null)
				{
					this.m_coreMap.ColorSwatchPanel.OutlineColor = mapColorScale.Instance.ColorBarBorderColor.ToColor();
				}
			}
			else
			{
				this.m_coreMap.ColorSwatchPanel.OutlineColor = Color.Black;
			}
			reportColorProperty = mapColorScale.RangeGapColor;
			if (reportColorProperty != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(reportColorProperty, ref empty))
				{
					this.m_coreMap.ColorSwatchPanel.RangeGapColor = empty;
				}
				else if (mapColorScale.Instance.RangeGapColor != null)
				{
					this.m_coreMap.ColorSwatchPanel.RangeGapColor = mapColorScale.Instance.RangeGapColor.ToColor();
				}
			}
			else
			{
				this.m_coreMap.ColorSwatchPanel.RangeGapColor = Color.White;
			}
			ReportIntProperty labelInterval = mapColorScale.LabelInterval;
			if (labelInterval != null)
			{
				if (!labelInterval.IsExpression)
				{
					this.m_coreMap.ColorSwatchPanel.LabelInterval = labelInterval.Value;
				}
				else
				{
					this.m_coreMap.ColorSwatchPanel.LabelInterval = mapColorScale.Instance.LabelInterval;
				}
			}
			ReportStringProperty labelFormat = mapColorScale.LabelFormat;
			string text = null;
			if (labelFormat != null)
			{
				if (!labelFormat.IsExpression)
				{
					text = labelFormat.Value;
				}
				else
				{
					text = mapColorScale.Instance.LabelFormat;
				}
			}
			if (text != null)
			{
				this.m_coreMap.ColorSwatchPanel.NumericLabelFormat = text;
			}
			ReportEnumProperty<MapLabelPlacement> labelPlacement = mapColorScale.LabelPlacement;
			if (labelPlacement != null)
			{
				if (!labelPlacement.IsExpression)
				{
					this.m_coreMap.ColorSwatchPanel.LabelAlignment = this.GetLabelAlignment(labelPlacement.Value);
				}
				else
				{
					this.m_coreMap.ColorSwatchPanel.LabelAlignment = this.GetLabelAlignment(mapColorScale.Instance.LabelPlacement);
				}
			}
			ReportEnumProperty<MapLabelBehavior> labelBehavior = mapColorScale.LabelBehavior;
			if (labelBehavior != null)
			{
				if (!labelBehavior.IsExpression)
				{
					this.m_coreMap.ColorSwatchPanel.LabelType = this.GetSwatchLabelType(labelBehavior.Value);
				}
				else
				{
					this.m_coreMap.ColorSwatchPanel.LabelType = this.GetSwatchLabelType(mapColorScale.Instance.LabelBehavior);
				}
			}
			ReportBoolProperty hideEndLabels = mapColorScale.HideEndLabels;
			if (hideEndLabels != null)
			{
				if (!hideEndLabels.IsExpression)
				{
					this.m_coreMap.ColorSwatchPanel.ShowEndLabels = !hideEndLabels.Value;
				}
				else
				{
					this.m_coreMap.ColorSwatchPanel.ShowEndLabels = !mapColorScale.Instance.HideEndLabels;
				}
			}
			ReportStringProperty noDataText = mapColorScale.NoDataText;
			string text2 = null;
			if (noDataText != null)
			{
				if (!noDataText.IsExpression)
				{
					text2 = noDataText.Value;
				}
				else
				{
					text2 = mapColorScale.Instance.NoDataText;
				}
			}
			this.m_coreMap.ColorSwatchPanel.NoDataText = ((text2 != null) ? text2 : "");
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003D318 File Offset: 0x0003B518
		private void SetColorScaleTitleProperties()
		{
			MapColorScaleTitle mapColorScaleTitle = this.m_map.MapColorScale.MapColorScaleTitle;
			ReportStringProperty caption = mapColorScaleTitle.Caption;
			string text = null;
			if (caption != null)
			{
				if (!caption.IsExpression)
				{
					text = caption.Value;
				}
				else
				{
					text = mapColorScaleTitle.Instance.Caption;
				}
			}
			this.m_coreMap.ColorSwatchPanel.Title = ((text != null) ? text : "");
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003D37A File Offset: 0x0003B57A
		private LabelAlignment GetLabelAlignment(MapLabelPlacement labelPlacement)
		{
			if (labelPlacement == MapLabelPlacement.Top)
			{
				return 0;
			}
			if (labelPlacement == MapLabelPlacement.Bottom)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003D389 File Offset: 0x0003B589
		private SwatchLabelType GetSwatchLabelType(MapLabelBehavior labelBehavior)
		{
			if (labelBehavior == MapLabelBehavior.ShowMiddleValue)
			{
				return 1;
			}
			if (labelBehavior == MapLabelBehavior.ShowBorderValue)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003D398 File Offset: 0x0003B598
		private void SetBorderSkinProperties()
		{
			ReportEnumProperty<MapBorderSkinType> mapBorderSkinType = this.m_map.MapBorderSkin.MapBorderSkinType;
			if (mapBorderSkinType != null)
			{
				MapBorderSkinType mapBorderSkinType2;
				if (!mapBorderSkinType.IsExpression)
				{
					mapBorderSkinType2 = mapBorderSkinType.Value;
				}
				else
				{
					mapBorderSkinType2 = this.m_map.MapBorderSkin.Instance.MapBorderSkinType;
				}
				this.m_coreMap.Frame.FrameStyle = MapMapper.GetFrameStyle(mapBorderSkinType2);
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003D3F8 File Offset: 0x0003B5F8
		private static FrameStyle GetFrameStyle(MapBorderSkinType type)
		{
			FrameStyle frameStyle = 0;
			switch (type)
			{
			case MapBorderSkinType.None:
				frameStyle = 0;
				break;
			case MapBorderSkinType.Emboss:
				frameStyle = 1;
				break;
			case MapBorderSkinType.Raised:
				frameStyle = 2;
				break;
			case MapBorderSkinType.Sunken:
				frameStyle = 3;
				break;
			case MapBorderSkinType.FrameThin1:
				frameStyle = 4;
				break;
			case MapBorderSkinType.FrameThin2:
				frameStyle = 5;
				break;
			case MapBorderSkinType.FrameThin3:
				frameStyle = 6;
				break;
			case MapBorderSkinType.FrameThin4:
				frameStyle = 7;
				break;
			case MapBorderSkinType.FrameThin5:
				frameStyle = 8;
				break;
			case MapBorderSkinType.FrameThin6:
				frameStyle = 9;
				break;
			case MapBorderSkinType.FrameTitle1:
				frameStyle = 10;
				break;
			case MapBorderSkinType.FrameTitle2:
				frameStyle = 11;
				break;
			case MapBorderSkinType.FrameTitle3:
				frameStyle = 12;
				break;
			case MapBorderSkinType.FrameTitle4:
				frameStyle = 13;
				break;
			case MapBorderSkinType.FrameTitle5:
				frameStyle = 14;
				break;
			case MapBorderSkinType.FrameTitle6:
				frameStyle = 15;
				break;
			case MapBorderSkinType.FrameTitle7:
				frameStyle = 16;
				break;
			case MapBorderSkinType.FrameTitle8:
				frameStyle = 17;
				break;
			}
			return frameStyle;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
		private void RenderMapStyle()
		{
			Border border = null;
			this.m_coreMap.BackColor = Color.Empty;
			Style style = this.m_map.Style;
			if (style != null)
			{
				StyleInstance style2 = this.m_map.Instance.Style;
				this.m_coreMap.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				this.m_coreMap.BackSecondaryColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				this.m_coreMap.BackGradientType = MapMapper.GetGradientType(style, style2);
				this.m_coreMap.BackHatchStyle = MapMapper.GetHatchStyle(style, style2);
				border = this.m_map.Style.Border;
			}
			if (this.m_coreMap.BackColor.A != 255)
			{
				this.m_coreMap.AntiAliasing = 0;
			}
			if (this.m_map.SpecialBorderHandling)
			{
				this.RenderMapBorder(border);
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003D57C File Offset: 0x0003B77C
		private void RenderMapBorder(Border border)
		{
			if (border != null)
			{
				this.m_coreMap.BorderLineColor = MappingHelper.GetStyleBorderColor(border);
				this.m_coreMap.BorderLineStyle = MapMapper.GetDashStyle(MappingHelper.GetStyleBorderStyle(border), false);
				this.m_coreMap.BorderLineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003D5CC File Offset: 0x0003B7CC
		private void RenderSubItemStyle(MapSubItem mapSubItem, Panel coreSubItem)
		{
			Style style = mapSubItem.Style;
			if (style == null)
			{
				coreSubItem.BackColor = Color.Empty;
				return;
			}
			StyleInstance style2 = mapSubItem.Instance.Style;
			coreSubItem.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			coreSubItem.BackSecondaryColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			coreSubItem.BackGradientType = MapMapper.GetGradientType(style, style2);
			coreSubItem.BackHatchStyle = MapMapper.GetHatchStyle(style, style2);
			coreSubItem.BackShadowOffset = MapMapper.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				coreSubItem.BorderColor = MappingHelper.GetStyleBorderColor(border);
				coreSubItem.BorderStyle = MapMapper.GetDashStyle(MappingHelper.GetStyleBorderStyle(border), false);
				coreSubItem.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003D684 File Offset: 0x0003B884
		private void RenderGridLinesStyle(MapGridLines mapGridLines, GridAttributes coreGridLines)
		{
			Style style = mapGridLines.Style;
			Border border = null;
			if (style != null)
			{
				border = style.Border;
			}
			if (style == null)
			{
				coreGridLines.Font = base.GetDefaultFontFromCache(0);
				coreGridLines.LabelColor = Color.Black;
				coreGridLines.LabelFormatString = "";
			}
			else
			{
				StyleInstance style2 = mapGridLines.Instance.Style;
				coreGridLines.Font = base.GetFontFromCache(0, style, style2);
				coreGridLines.LabelColor = MappingHelper.GetStyleColor(style, style2);
				coreGridLines.LabelFormatString = MappingHelper.GetStyleFormat(style, style2);
			}
			if (border == null)
			{
				coreGridLines.LineWidth = MappingHelper.GetDefaultBorderWidth(base.DpiX);
				coreGridLines.LineColor = Color.Black;
				coreGridLines.LineStyle = 5;
				return;
			}
			coreGridLines.LineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
			coreGridLines.LineColor = MappingHelper.GetStyleBorderColor(border);
			coreGridLines.LineStyle = MapMapper.GetDashStyle(MappingHelper.GetStyleBorderStyle(border), true);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003D758 File Offset: 0x0003B958
		private void RenderLegendTitleStyle(MapLegendTitle mapLegendTitle, Legend legend)
		{
			Style style = mapLegendTitle.Style;
			if (style != null)
			{
				StyleInstance style2 = mapLegendTitle.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(style.Color))
				{
					legend.TitleColor = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundColor))
				{
					legend.TitleBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				this.RenderLegendTitleBorder(style.Border, legend);
				legend.TitleAlignment = this.GetLegendTitleAlign(MappingHelper.GetStyleTextAlign(style, style2));
			}
			this.RenderLegendTitleFont(mapLegendTitle, legend);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		private void RenderLegendTitleFont(MapLegendTitle mapLegendTitle, Legend legend)
		{
			Style style = mapLegendTitle.Style;
			if (style == null)
			{
				legend.TitleFont = base.GetDefaultFont();
				return;
			}
			legend.TitleFont = base.GetFont(style, mapLegendTitle.Instance.Style);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003D814 File Offset: 0x0003BA14
		private StringAlignment GetLegendTitleAlign(TextAlignments textAlignment)
		{
			if (textAlignment == TextAlignments.Left)
			{
				return StringAlignment.Near;
			}
			if (textAlignment != TextAlignments.Right)
			{
				return StringAlignment.Center;
			}
			return StringAlignment.Far;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003D825 File Offset: 0x0003BA25
		private void RenderLegendTitleBorder(Border border, Legend legend)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				legend.TitleSeparatorColor = MappingHelper.GetStyleBorderColor(border);
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003D844 File Offset: 0x0003BA44
		private void RenderColorScaleTitleStyle()
		{
			Style style = this.m_map.MapColorScale.MapColorScaleTitle.Style;
			if (style == null)
			{
				this.m_coreMap.ColorSwatchPanel.TitleFont = base.GetDefaultFontFromCache(0);
				return;
			}
			StyleInstance style2 = this.m_map.MapColorScale.MapColorScaleTitle.Instance.Style;
			this.m_coreMap.ColorSwatchPanel.TitleColor = MappingHelper.GetStyleColor(style, style2);
			this.m_coreMap.ColorSwatchPanel.TitleFont = base.GetFontFromCache(0, style, style2);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		private void RenderBorderSkinStyle()
		{
			Style style = this.m_map.MapBorderSkin.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = this.m_map.MapBorderSkin.Instance.Style;
			Frame frame = this.m_coreMap.Frame;
			frame.PageColor = MappingHelper.GetStyleColor(style, style2);
			frame.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			frame.BackSecondaryColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			frame.BackGradientType = MapMapper.GetGradientType(style, style2);
			frame.BackHatchStyle = MapMapper.GetHatchStyle(style, style2);
			this.RenderBorderSkinBorder(style.Border, frame);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003D964 File Offset: 0x0003BB64
		private void RenderBorderSkinBorder(Border border, Frame borderSkin)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				borderSkin.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				borderSkin.BorderStyle = MapMapper.GetDashStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			borderSkin.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		private void GetPngImage(out Stream imageStream, int width, int height)
		{
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				bitmap.SetResolution(base.DpiX, base.DpiY);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					this.GetImage(graphics);
					imageStream = new MemoryStream();
					bitmap.Save(imageStream, ImageFormat.Png);
				}
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		private void GetEmfImage(out Stream imageStream, int width, int height)
		{
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					IntPtr hdc = graphics.GetHdc();
					imageStream = this.m_map.RenderingContext.OdpContext.CreateStreamCallback(this.m_map.Name, "emf", null, "image/emf", true, StreamOper.CreateOnly);
					using (Metafile metafile = new Metafile(imageStream, hdc, new global::System.Drawing.Rectangle(0, 0, width, height), MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly))
					{
						using (Graphics graphics2 = Graphics.FromImage(metafile))
						{
							this.GetImage(graphics2);
						}
					}
					graphics.ReleaseHdc(hdc);
				}
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003DB20 File Offset: 0x0003BD20
		private void GetImage(Graphics graphics)
		{
			this.m_coreMap.mapCore.Paint(graphics);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003DB33 File Offset: 0x0003BD33
		private static LayerVisibility GetLayerVisibility(MapVisibilityMode visibility)
		{
			if (visibility == MapVisibilityMode.Hidden)
			{
				return 1;
			}
			if (visibility != MapVisibilityMode.ZoomBased)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003DB44 File Offset: 0x0003BD44
		internal static MarkerStyle GetMarkerStyle(MapMarkerStyle mapMarkerStyle)
		{
			switch (mapMarkerStyle)
			{
			case MapMarkerStyle.Rectangle:
				return 1;
			case MapMarkerStyle.Circle:
				return 3;
			case MapMarkerStyle.Diamond:
				return 4;
			case MapMarkerStyle.Triangle:
				return 2;
			case MapMarkerStyle.Trapezoid:
				return 5;
			case MapMarkerStyle.Star:
				return 6;
			case MapMarkerStyle.Wedge:
				return 7;
			case MapMarkerStyle.Pentagon:
				return 8;
			case MapMarkerStyle.PushPin:
				return 9;
			default:
				return 0;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003DB94 File Offset: 0x0003BD94
		internal static MapMarkerStyle GetMarkerStyle(MapMarker mapMarker, bool hasScope)
		{
			if (mapMarker != null)
			{
				ReportEnumProperty<MapMarkerStyle> mapMarkerStyle = mapMarker.MapMarkerStyle;
				if (mapMarkerStyle != null)
				{
					if (!mapMarkerStyle.IsExpression)
					{
						return mapMarkerStyle.Value;
					}
					if (hasScope)
					{
						return mapMarker.Instance.MapMarkerStyle;
					}
				}
			}
			return MapMarkerStyle.None;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		internal static GradientType GetGradientType(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackGradientType(style, styleInstance))
			{
			case BackgroundGradients.LeftRight:
				return 1;
			case BackgroundGradients.TopBottom:
				return 3;
			case BackgroundGradients.Center:
				return 5;
			case BackgroundGradients.DiagonalLeft:
				return 7;
			case BackgroundGradients.DiagonalRight:
				return 9;
			case BackgroundGradients.HorizontalCenter:
				return 11;
			case BackgroundGradients.VerticalCenter:
				return 13;
			default:
				return 0;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003DC20 File Offset: 0x0003BE20
		internal static MapHatchStyle GetHatchStyle(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackgroundHatchType(style, styleInstance))
			{
			case BackgroundHatchTypes.BackwardDiagonal:
				return 1;
			case BackgroundHatchTypes.Cross:
				return 2;
			case BackgroundHatchTypes.DarkDownwardDiagonal:
				return 3;
			case BackgroundHatchTypes.DarkHorizontal:
				return 4;
			case BackgroundHatchTypes.DarkUpwardDiagonal:
				return 5;
			case BackgroundHatchTypes.DarkVertical:
				return 6;
			case BackgroundHatchTypes.DashedDownwardDiagonal:
				return 7;
			case BackgroundHatchTypes.DashedHorizontal:
				return 8;
			case BackgroundHatchTypes.DashedUpwardDiagonal:
				return 9;
			case BackgroundHatchTypes.DashedVertical:
				return 10;
			case BackgroundHatchTypes.DiagonalBrick:
				return 11;
			case BackgroundHatchTypes.DiagonalCross:
				return 12;
			case BackgroundHatchTypes.Divot:
				return 13;
			case BackgroundHatchTypes.DottedDiamond:
				return 14;
			case BackgroundHatchTypes.DottedGrid:
				return 15;
			case BackgroundHatchTypes.ForwardDiagonal:
				return 16;
			case BackgroundHatchTypes.Horizontal:
				return 17;
			case BackgroundHatchTypes.HorizontalBrick:
				return 18;
			case BackgroundHatchTypes.LargeCheckerBoard:
				return 19;
			case BackgroundHatchTypes.LargeConfetti:
				return 20;
			case BackgroundHatchTypes.LargeGrid:
				return 21;
			case BackgroundHatchTypes.LightDownwardDiagonal:
				return 22;
			case BackgroundHatchTypes.LightHorizontal:
				return 23;
			case BackgroundHatchTypes.LightUpwardDiagonal:
				return 24;
			case BackgroundHatchTypes.LightVertical:
				return 25;
			case BackgroundHatchTypes.NarrowHorizontal:
				return 26;
			case BackgroundHatchTypes.NarrowVertical:
				return 27;
			case BackgroundHatchTypes.OutlinedDiamond:
				return 28;
			case BackgroundHatchTypes.Percent05:
				return 29;
			case BackgroundHatchTypes.Percent10:
				return 30;
			case BackgroundHatchTypes.Percent20:
				return 31;
			case BackgroundHatchTypes.Percent25:
				return 32;
			case BackgroundHatchTypes.Percent30:
				return 33;
			case BackgroundHatchTypes.Percent40:
				return 34;
			case BackgroundHatchTypes.Percent50:
				return 35;
			case BackgroundHatchTypes.Percent60:
				return 36;
			case BackgroundHatchTypes.Percent70:
				return 37;
			case BackgroundHatchTypes.Percent75:
				return 38;
			case BackgroundHatchTypes.Percent80:
				return 39;
			case BackgroundHatchTypes.Percent90:
				return 40;
			case BackgroundHatchTypes.Plaid:
				return 41;
			case BackgroundHatchTypes.Shingle:
				return 42;
			case BackgroundHatchTypes.SmallCheckerBoard:
				return 43;
			case BackgroundHatchTypes.SmallConfetti:
				return 44;
			case BackgroundHatchTypes.SmallGrid:
				return 45;
			case BackgroundHatchTypes.SolidDiamond:
				return 46;
			case BackgroundHatchTypes.Sphere:
				return 47;
			case BackgroundHatchTypes.Trellis:
				return 48;
			case BackgroundHatchTypes.Vertical:
				return 49;
			case BackgroundHatchTypes.Wave:
				return 50;
			case BackgroundHatchTypes.Weave:
				return 51;
			case BackgroundHatchTypes.WideDownwardDiagonal:
				return 52;
			case BackgroundHatchTypes.WideUpwardDiagonal:
				return 53;
			case BackgroundHatchTypes.ZigZag:
				return 54;
			default:
				return 0;
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003DDB5 File Offset: 0x0003BFB5
		internal static int GetValidShadowOffset(int shadowOffset)
		{
			return Math.Min(shadowOffset, 100);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003DDC0 File Offset: 0x0003BFC0
		internal static MapDashStyle GetDashStyle(Border border, bool hasScope, bool isLine)
		{
			BorderStyles borderStyles;
			if (!MappingHelper.IsPropertyExpression(border.Style) || hasScope)
			{
				borderStyles = MappingHelper.GetStyleBorderStyle(border);
			}
			else
			{
				borderStyles = BorderStyles.Default;
			}
			return MapMapper.GetDashStyle(borderStyles, isLine);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003DDF1 File Offset: 0x0003BFF1
		private static MapDashStyle GetDashStyle(BorderStyles borderStyle, bool isLine)
		{
			switch (borderStyle)
			{
			case BorderStyles.None:
				return 0;
			case BorderStyles.Dotted:
				return 4;
			case BorderStyles.Dashed:
				return 1;
			case BorderStyles.Solid:
			case BorderStyles.Double:
				return 5;
			case BorderStyles.DashDot:
				return 2;
			case BorderStyles.DashDotDot:
				return 3;
			default:
				if (isLine)
				{
					return 5;
				}
				return 0;
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003DE2C File Offset: 0x0003C02C
		internal string AddImage(MapMarkerImage mapMarkerImage)
		{
			byte[] imageData = mapMarkerImage.Instance.ImageData;
			if (imageData == null)
			{
				return "";
			}
			global::System.Drawing.Image image = global::System.Drawing.Image.FromStream(new MemoryStream(imageData, false));
			if (image == null)
			{
				return "";
			}
			string text = this.m_coreMap.NamedImages.Count.ToString(CultureInfo.InvariantCulture);
			this.m_coreMap.NamedImages.Add(new NamedImage(text, image));
			return text;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003DE9C File Offset: 0x0003C09C
		internal ResizeMode GetImageResizeMode(MapMarkerImage mapMarkerImage)
		{
			ReportEnumProperty<MapResizeMode> resizeMode = mapMarkerImage.ResizeMode;
			if (resizeMode == null)
			{
				return 1;
			}
			if (!resizeMode.IsExpression)
			{
				return this.GetResizeMode(resizeMode.Value);
			}
			return this.GetResizeMode(mapMarkerImage.Instance.ResizeMode);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003DEDB File Offset: 0x0003C0DB
		private ResizeMode GetResizeMode(MapResizeMode resizeMode)
		{
			if (resizeMode == MapResizeMode.None)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003DEE4 File Offset: 0x0003C0E4
		internal Color GetImageTransColor(MapMarkerImage image)
		{
			ReportColorProperty transparentColor = image.TransparentColor;
			Color color = Color.Empty;
			if (transparentColor != null && !MappingHelper.GetColorFromReportColorProperty(transparentColor, ref color))
			{
				ReportColor transparentColor2 = image.Instance.TransparentColor;
				if (transparentColor2 != null)
				{
					color = transparentColor2.ToColor();
				}
			}
			return color;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003DF24 File Offset: 0x0003C124
		internal void RenderActionInfo(ActionInfo actionInfo, string toolTip, IImageMapProvider imageMapProvider, string layerName, bool hasScope)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_map, actionInfo, toolTip, out text, hasScope);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					if (layerName != null)
					{
						text = VectorLayerMapper.AddPrefixToFieldNames(layerName, text);
					}
					imageMapProvider.Href = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				imageMapProvider.Tag = count;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003DF92 File Offset: 0x0003C192
		internal void OnSpatialElementAdded(SpatialElementInfo spatialElementInfo)
		{
			this.DecrementRemainingSpatialElementCount();
			if (spatialElementInfo.CoreSpatialElement.Points != null)
			{
				this.DecrementRemainingTotalCount(spatialElementInfo.CoreSpatialElement.Points.Length);
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003DFBA File Offset: 0x0003C1BA
		private void DecrementRemainingSpatialElementCount()
		{
			this.m_remainingSpatialElementCount--;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003DFCA File Offset: 0x0003C1CA
		private void DecrementRemainingTotalCount(int count)
		{
			this.m_remainingTotalPointCount -= count;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0003DFDA File Offset: 0x0003C1DA
		internal int RemainingSpatialElementCount
		{
			get
			{
				return this.m_remainingSpatialElementCount;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0003DFE2 File Offset: 0x0003C1E2
		internal int RemainingTotalPointCount
		{
			get
			{
				return this.m_remainingTotalPointCount;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0003DFEC File Offset: 0x0003C1EC
		internal bool CanAddSpatialElement
		{
			get
			{
				if (this.m_remainingSpatialElementCount > 0 && this.m_remainingTotalPointCount > 0)
				{
					return true;
				}
				if (this.m_remainingSpatialElementCount < 1)
				{
					this.m_coreMap.Viewport.ErrorMessage = RPResWrapper.rsMapMaximumSpatialElementCountReached(RPRes.rsObjectTypeMap, this.m_map.Name);
				}
				else if (this.m_remainingTotalPointCount < 1)
				{
					this.m_coreMap.Viewport.ErrorMessage = RPResWrapper.rsMapMaximumTotalPointCountReached(RPRes.rsObjectTypeMap, this.m_map.Name);
				}
				return false;
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003E06C File Offset: 0x0003C26C
		private ImageMapArea.ImageMapAreaShape GetMapAreaShape(MapAreaShape shape)
		{
			if (shape == null)
			{
				return ImageMapArea.ImageMapAreaShape.Rectangle;
			}
			if (1 == shape)
			{
				return ImageMapArea.ImageMapAreaShape.Circle;
			}
			return ImageMapArea.ImageMapAreaShape.Polygon;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003E07C File Offset: 0x0003C27C
		private string FormatNumber(object sender, object value, string format)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_map.MapDef.StyleClass, this.m_map.RenderingContext.OdpContext, this.m_map.MapDef.ObjectType, this.m_map.Name);
			}
			return this.m_formatter.FormatValue(value, format, Type.GetTypeCode(value.GetType()));
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0003E0F0 File Offset: 0x0003C2F0
		private MapSimplifier Simplifier
		{
			get
			{
				if (this.m_simpificationResolution == null)
				{
					this.m_simpificationResolution = new double?(this.EvaluateSimplificationResolution());
					if (this.m_simpificationResolution.Value != 0.0)
					{
						this.m_mapSimplifier = new MapSimplifier();
					}
				}
				return this.m_mapSimplifier;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003E144 File Offset: 0x0003C344
		private double EvaluateSimplificationResolution()
		{
			ReportDoubleProperty simplificationResolution = this.m_map.MapViewport.SimplificationResolution;
			if (simplificationResolution == null)
			{
				return 0.0;
			}
			if (!simplificationResolution.IsExpression)
			{
				return simplificationResolution.Value;
			}
			return this.m_map.MapViewport.Instance.SimplificationResolution;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003E193 File Offset: 0x0003C393
		internal void Simplify(Shape shape)
		{
			if (this.Simplifier != null)
			{
				this.Simplifier.Simplify(shape, this.m_simpificationResolution.Value);
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
		internal void Simplify(Path path)
		{
			if (this.Simplifier != null)
			{
				this.Simplifier.Simplify(path, this.m_simpificationResolution.Value);
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003E1D5 File Offset: 0x0003C3D5
		public override void Dispose()
		{
			if (this.m_coreMap != null)
			{
				this.m_coreMap.Dispose();
			}
			this.m_coreMap = null;
			base.Dispose();
		}

		// Token: 0x040006E1 RID: 1761
		private Map m_map;

		// Token: 0x040006E2 RID: 1762
		private int m_remainingSpatialElementCount = 20000;

		// Token: 0x040006E3 RID: 1763
		private int m_remainingTotalPointCount = 1000000;

		// Token: 0x040006E4 RID: 1764
		private MapControl m_coreMap;

		// Token: 0x040006E5 RID: 1765
		private ActionInfoWithDynamicImageMapCollection m_actions = new ActionInfoWithDynamicImageMapCollection();

		// Token: 0x040006E6 RID: 1766
		private static string m_defaultContentMarginString = "10pt";

		// Token: 0x040006E7 RID: 1767
		private static ReportSize m_defaultContentMargin = new ReportSize(MapMapper.m_defaultContentMarginString);

		// Token: 0x040006E8 RID: 1768
		private static string m_defaultTickMarkLengthString = "2.25pt";

		// Token: 0x040006E9 RID: 1769
		private static ReportSize m_defaultTickMarkLength = new ReportSize(MapMapper.m_defaultTickMarkLengthString);

		// Token: 0x040006EA RID: 1770
		private MapMapper.BoundsRectCalculator m_boundRectCalculator;

		// Token: 0x040006EB RID: 1771
		private MapSimplifier m_mapSimplifier;

		// Token: 0x040006EC RID: 1772
		private double? m_simpificationResolution;

		// Token: 0x040006ED RID: 1773
		private TileLayerMapper m_tileLayerMapper;

		// Token: 0x040006EE RID: 1774
		private Formatter m_formatter;

		// Token: 0x02000932 RID: 2354
		private class BoundsRectCalculator
		{
			// Token: 0x06007F72 RID: 32626 RVA: 0x0020D7B0 File Offset: 0x0020B9B0
			internal void AddSpatialElement(ISpatialElement spatialElement)
			{
				if (!this.hasValue)
				{
					this.Min = new MapPoint(spatialElement.MinimumExtent.X + spatialElement.Offset.X, spatialElement.MinimumExtent.Y + spatialElement.Offset.Y);
					this.Max = new MapPoint(spatialElement.MaximumExtent.X + spatialElement.Offset.X, spatialElement.MaximumExtent.Y + spatialElement.Offset.Y);
					this.hasValue = true;
					return;
				}
				this.Min.X = Math.Min(this.Min.X, spatialElement.MinimumExtent.X + spatialElement.Offset.X);
				this.Min.Y = Math.Min(this.Min.Y, spatialElement.MinimumExtent.Y + spatialElement.Offset.Y);
				this.Max.X = Math.Max(this.Max.X, spatialElement.MaximumExtent.X + spatialElement.Offset.X);
				this.Max.Y = Math.Max(this.Max.Y, spatialElement.MaximumExtent.Y + spatialElement.Offset.Y);
			}

			// Token: 0x17002963 RID: 10595
			// (get) Token: 0x06007F73 RID: 32627 RVA: 0x0020D924 File Offset: 0x0020BB24
			internal MapPoint Center
			{
				get
				{
					return new MapPoint((this.Min.X + this.Max.X) / 2.0, (this.Min.Y + this.Max.Y) / 2.0);
				}
			}

			// Token: 0x04003FA7 RID: 16295
			private bool hasValue;

			// Token: 0x04003FA8 RID: 16296
			internal MapPoint Min;

			// Token: 0x04003FA9 RID: 16297
			internal MapPoint Max;
		}
	}
}
