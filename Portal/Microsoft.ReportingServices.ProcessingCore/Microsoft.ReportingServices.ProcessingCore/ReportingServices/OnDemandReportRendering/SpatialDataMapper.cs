using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015D RID: 349
	internal abstract class SpatialDataMapper
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x0003E7DF File Offset: 0x0003C9DF
		internal SpatialDataMapper(VectorLayerMapper vectorLayerMapper, Dictionary<SpatialElementKey, SpatialElementInfoGroup> spatialElementsDictionary, MapControl coreMap, MapMapper mapMapper)
		{
			this.m_vectorLayerMapper = vectorLayerMapper;
			this.m_mapVectorLayer = this.m_vectorLayerMapper.m_mapVectorLayer;
			this.m_spatialElementsDictionary = spatialElementsDictionary;
			this.m_coreMap = coreMap;
			this.m_mapMapper = mapMapper;
		}

		// Token: 0x06000E68 RID: 3688
		internal abstract void Populate();

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003E818 File Offset: 0x0003CA18
		protected void OnSpatialElementAdded(SpatialElementInfo spatialElementInfo)
		{
			this.m_vectorLayerMapper.OnSpatialElementAdded(spatialElementInfo.CoreSpatialElement);
			this.m_mapMapper.OnSpatialElementAdded(spatialElementInfo);
			SpatialElementKey spatialElementKey = this.CreateCoreSpatialElementKey(spatialElementInfo.CoreSpatialElement);
			if (this.m_mapVectorLayer.MapDataRegion != null && this.m_keyTypes == null && spatialElementKey.KeyValues != null)
			{
				this.RegisterKeyTypes(spatialElementKey);
			}
			SpatialElementInfoGroup spatialElementInfoGroup;
			if (!this.m_spatialElementsDictionary.ContainsKey(spatialElementKey))
			{
				spatialElementInfoGroup = new SpatialElementInfoGroup();
				this.m_spatialElementsDictionary.Add(spatialElementKey, spatialElementInfoGroup);
			}
			else
			{
				spatialElementInfoGroup = this.m_spatialElementsDictionary[spatialElementKey];
			}
			spatialElementInfoGroup.Elements.Add(spatialElementInfo);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
		private void RegisterKeyTypes(SpatialElementKey key)
		{
			this.m_keyTypes = new List<Type>();
			foreach (object obj in key.KeyValues)
			{
				if (obj == null)
				{
					this.m_keyTypes.Add(null);
				}
				else
				{
					this.m_keyTypes.Add(obj.GetType());
				}
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003E92C File Offset: 0x0003CB2C
		private SpatialElementKey CreateCoreSpatialElementKey(ISpatialElement coreSpatialElement)
		{
			return SpatialDataMapper.CreateCoreSpatialElementKey(coreSpatialElement, this.m_mapVectorLayer.MapBindingFieldPairs, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003E95C File Offset: 0x0003CB5C
		internal static SpatialElementKey CreateCoreSpatialElementKey(ISpatialElement coreSpatialElement, MapBindingFieldPairCollection mapBindingFieldPairs, string mapName, string layerName)
		{
			if (mapBindingFieldPairs == null)
			{
				return new SpatialElementKey(null);
			}
			List<object> list = new List<object>();
			for (int i = 0; i < mapBindingFieldPairs.Count; i++)
			{
				list.Add(SpatialDataMapper.GetBindingFieldValue(coreSpatialElement, mapBindingFieldPairs[i], mapName, layerName));
			}
			return new SpatialElementKey(list);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		private static object GetBindingFieldValue(ISpatialElement coreSpatialElement, MapBindingFieldPair bindingFieldPair, string mapName, string layerName)
		{
			string bindingFieldName = SpatialDataMapper.GetBindingFieldName(bindingFieldPair);
			if (bindingFieldName == null)
			{
				return null;
			}
			return coreSpatialElement[SpatialDataMapper.GetUniqueFieldName(layerName, bindingFieldName)];
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E9CE File Offset: 0x0003CBCE
		protected string GetUniqueFieldName(string fieldName)
		{
			return SpatialDataMapper.GetUniqueFieldName(this.m_mapVectorLayer.Name, fieldName);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		internal static string GetBindingFieldName(MapBindingFieldPair bindingFieldPair)
		{
			ReportStringProperty fieldName = bindingFieldPair.FieldName;
			if (!fieldName.IsExpression)
			{
				return fieldName.Value;
			}
			return bindingFieldPair.Instance.FieldName;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003EA12 File Offset: 0x0003CC12
		internal static string GetUniqueFieldName(string layerName, string fieldName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", layerName, fieldName);
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0003EA25 File Offset: 0x0003CC25
		internal List<Type> KeyTypes
		{
			get
			{
				return this.m_keyTypes;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003EA2D File Offset: 0x0003CC2D
		protected string GetFieldName(MapFieldName fieldName)
		{
			ReportStringProperty name = fieldName.Name;
			if (!fieldName.Name.IsExpression)
			{
				return fieldName.Name.Value;
			}
			return fieldName.Instance.Name;
		}

		// Token: 0x040006F1 RID: 1777
		protected MapMapper m_mapMapper;

		// Token: 0x040006F2 RID: 1778
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x040006F3 RID: 1779
		protected VectorLayerMapper m_vectorLayerMapper;

		// Token: 0x040006F4 RID: 1780
		protected MapControl m_coreMap;

		// Token: 0x040006F5 RID: 1781
		private List<Type> m_keyTypes;

		// Token: 0x040006F6 RID: 1782
		private Dictionary<SpatialElementKey, SpatialElementInfoGroup> m_spatialElementsDictionary;
	}
}
