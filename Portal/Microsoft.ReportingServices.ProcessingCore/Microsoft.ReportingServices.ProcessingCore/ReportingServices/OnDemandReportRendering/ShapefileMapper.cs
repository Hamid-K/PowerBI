using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000160 RID: 352
	internal class ShapefileMapper : SpatialDataMapper
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x0003F0FC File Offset: 0x0003D2FC
		internal ShapefileMapper(VectorLayerMapper vectorLayerMapper, Dictionary<SpatialElementKey, SpatialElementInfoGroup> spatialElementsDictionary, MapControl coreMap, MapMapper mapMapper)
			: base(vectorLayerMapper, spatialElementsDictionary, coreMap, mapMapper)
		{
			this.m_shapefile = (MapShapefile)this.m_mapVectorLayer.MapSpatialData;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003F11F File Offset: 0x0003D31F
		internal void SubscribeToAddedEvent()
		{
			this.m_coreMap.ElementAdded += new ElementEvent(this.CoreMap_ElementAdded);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003F138 File Offset: 0x0003D338
		internal void UnsubscribeToAddedEvent()
		{
			this.m_coreMap.ElementAdded -= new ElementEvent(this.CoreMap_ElementAdded);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003F154 File Offset: 0x0003D354
		private void CoreMap_ElementAdded(object sender, ElementEventArgs e)
		{
			if (e.MapElement is ISpatialElement)
			{
				if (!this.m_shapefileMatchingLayer)
				{
					if (!this.m_vectorLayerMapper.IsValidSpatialElement((ISpatialElement)e.MapElement))
					{
						throw new RenderingObjectModelException(RPResWrapper.rsMapShapefileTypeMismatch(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, this.m_shapefile.Instance.Source));
					}
					this.m_shapefileMatchingLayer = true;
				}
				base.OnSpatialElementAdded(new SpatialElementInfo
				{
					CoreSpatialElement = (ISpatialElement)e.MapElement
				});
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003F1F4 File Offset: 0x0003D3F4
		internal override void Populate()
		{
			this.SubscribeToAddedEvent();
			string[] array;
			string[] array2;
			this.GetFieldNameMapping(out array, out array2);
			Stream stream = this.m_shapefile.Instance.Stream;
			if (stream != null)
			{
				this.m_coreMap.mapCore.MaxSpatialElementCount = this.m_mapMapper.RemainingSpatialElementCount;
				this.m_coreMap.mapCore.MaxSpatialPointCount = this.m_mapMapper.RemainingTotalPointCount;
				SpatialLoadResult spatialLoadResult = this.m_coreMap.mapCore.LoadFromShapeFileStreams(stream, this.m_shapefile.Instance.DBFStream, array, array2, this.m_mapVectorLayer.Name, this.m_mapVectorLayer.Name);
				if (spatialLoadResult == 1)
				{
					this.m_coreMap.Viewport.ErrorMessage = RPResWrapper.rsMapMaximumSpatialElementCountReached(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name);
				}
				else if (spatialLoadResult == 2)
				{
					this.m_coreMap.Viewport.ErrorMessage = RPResWrapper.rsMapMaximumTotalPointCountReached(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name);
				}
				this.UnsubscribeToAddedEvent();
				return;
			}
			throw new RenderingObjectModelException(RPResWrapper.rsMapCannotLoadShapefile(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_shapefile.Instance.Source));
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003F330 File Offset: 0x0003D530
		private void GetFieldNameMapping(out string[] dbfNames, out string[] uniqueNames)
		{
			MapFieldNameCollection mapFieldNames = this.m_shapefile.MapFieldNames;
			if (mapFieldNames == null)
			{
				dbfNames = null;
				uniqueNames = null;
				return;
			}
			dbfNames = new string[mapFieldNames.Count];
			uniqueNames = new string[mapFieldNames.Count];
			for (int i = 0; i < mapFieldNames.Count; i++)
			{
				string fieldName = base.GetFieldName(mapFieldNames[i]);
				dbfNames[i] = fieldName;
				uniqueNames[i] = SpatialDataMapper.GetUniqueFieldName(this.m_mapVectorLayer.Name, fieldName);
			}
		}

		// Token: 0x040006FD RID: 1789
		private MapShapefile m_shapefile;

		// Token: 0x040006FE RID: 1790
		private bool m_shapefileMatchingLayer;
	}
}
