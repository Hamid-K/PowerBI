using System;
using System.Collections.Generic;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015E RID: 350
	internal class SpatialDataSetMapper : SpatialDataMapper
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x0003EA5C File Offset: 0x0003CC5C
		internal SpatialDataSetMapper(VectorLayerMapper vectorLayerMapper, Dictionary<SpatialElementKey, SpatialElementInfoGroup> spatialElementsDictionary, CoreSpatialElementManager spatialElementManager, MapControl coreMap, MapMapper mapMapper)
			: base(vectorLayerMapper, spatialElementsDictionary, coreMap, mapMapper)
		{
			this.m_spatialElementManager = spatialElementManager;
			this.m_spatialDataSet = (MapSpatialDataSet)this.m_mapVectorLayer.MapSpatialData;
			this.m_dataSet = this.m_spatialDataSet.DataSet;
			this.m_dataSetInstance = this.m_dataSet.Instance;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		internal override void Populate()
		{
			int spatialFieldIndex = this.GetSpatialFieldIndex();
			SpatialDataSetMapper.FieldInfo[] nonSpatialFieldInfos = this.GetNonSpatialFieldInfos();
			this.m_dataSetInstance.ResetContext();
			while (this.m_dataSetInstance.MoveNext())
			{
				this.ProcessRow(spatialFieldIndex, nonSpatialFieldInfos);
			}
			this.EnsureFieldDefinitionsCreated(nonSpatialFieldInfos);
			this.m_dataSetInstance.Close();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003EB04 File Offset: 0x0003CD04
		private void EnsureFieldDefinitionsCreated(SpatialDataSetMapper.FieldInfo[] nonSpatialFieldInfos)
		{
			if (nonSpatialFieldInfos != null)
			{
				foreach (SpatialDataSetMapper.FieldInfo fieldInfo in nonSpatialFieldInfos)
				{
					if (!fieldInfo.DefinitionAdded)
					{
						this.m_spatialElementManager.AddFieldDefinition(fieldInfo.UniqueName, typeof(string));
					}
				}
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003EB4C File Offset: 0x0003CD4C
		private void ProcessRow(int spatialFieldIndex, SpatialDataSetMapper.FieldInfo[] nonSpatialFieldInfos)
		{
			if (!this.m_mapMapper.CanAddSpatialElement)
			{
				return;
			}
			object value = this.m_dataSetInstance.Row[spatialFieldIndex].Value;
			ISpatialElement spatialElement;
			if (value is SqlGeography)
			{
				spatialElement = this.m_spatialElementManager.AddGeography((SqlGeography)value, this.m_mapVectorLayer.Name);
			}
			else
			{
				if (!(value is SqlGeometry))
				{
					throw new RenderingObjectModelException(RPResWrapper.rsMapInvalidSpatialFieldType(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name));
				}
				spatialElement = this.m_spatialElementManager.AddGeometry((SqlGeometry)value, this.m_mapVectorLayer.Name);
			}
			if (spatialElement != null)
			{
				this.ProcessNonSpatialFields(spatialElement, nonSpatialFieldInfos);
				base.OnSpatialElementAdded(new SpatialElementInfo
				{
					CoreSpatialElement = spatialElement
				});
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003EC18 File Offset: 0x0003CE18
		private void ProcessNonSpatialFields(ISpatialElement spatialElement, SpatialDataSetMapper.FieldInfo[] nonSpatialFieldInfos)
		{
			if (nonSpatialFieldInfos != null)
			{
				foreach (SpatialDataSetMapper.FieldInfo fieldInfo in nonSpatialFieldInfos)
				{
					if (!fieldInfo.DefinitionAdded)
					{
						fieldInfo.DefinitionAdded = this.AddFieldDefinition(fieldInfo.UniqueName, this.m_dataSetInstance.Row[fieldInfo.Index].Value);
					}
					this.m_spatialElementManager.AddFieldValue(spatialElement, fieldInfo.UniqueName, this.m_dataSetInstance.Row[fieldInfo.Index].Value);
				}
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003EC9C File Offset: 0x0003CE9C
		private bool AddFieldDefinition(string fieldUniqueName, object value)
		{
			if (value != null)
			{
				this.m_spatialElementManager.AddFieldDefinition(fieldUniqueName, CoreSpatialElementManager.GetFieldType(value));
				return true;
			}
			return false;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
		private SpatialDataSetMapper.FieldInfo[] GetNonSpatialFieldInfos()
		{
			MapFieldNameCollection mapFieldNames = this.m_spatialDataSet.MapFieldNames;
			if (mapFieldNames == null)
			{
				return null;
			}
			SpatialDataSetMapper.FieldInfo[] array = new SpatialDataSetMapper.FieldInfo[mapFieldNames.Count];
			for (int i = 0; i < mapFieldNames.Count; i++)
			{
				SpatialDataSetMapper.FieldInfo fieldInfo = new SpatialDataSetMapper.FieldInfo();
				string fieldName = base.GetFieldName(mapFieldNames[i]);
				fieldInfo.UniqueName = base.GetUniqueFieldName(fieldName);
				fieldInfo.Index = this.GetFieldIndex(fieldName);
				fieldInfo.DefinitionAdded = false;
				array[i] = fieldInfo;
			}
			return array;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003ED33 File Offset: 0x0003CF33
		private int GetSpatialFieldIndex()
		{
			return this.GetFieldIndex(this.GetSpatialFieldName());
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003ED44 File Offset: 0x0003CF44
		private int GetFieldIndex(string fieldName)
		{
			int fieldIndex = this.m_dataSet.NonCalculatedFields.GetFieldIndex(fieldName);
			if (fieldIndex == -1)
			{
				throw new RenderingObjectModelException(RPResWrapper.rsMapInvalidFieldName(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, fieldName));
			}
			return fieldIndex;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003ED94 File Offset: 0x0003CF94
		private string GetSpatialFieldName()
		{
			ReportStringProperty spatialField = this.m_spatialDataSet.SpatialField;
			if (!spatialField.IsExpression)
			{
				return spatialField.Value;
			}
			return this.m_spatialDataSet.Instance.SpatialField;
		}

		// Token: 0x040006F7 RID: 1783
		private CoreSpatialElementManager m_spatialElementManager;

		// Token: 0x040006F8 RID: 1784
		private MapSpatialDataSet m_spatialDataSet;

		// Token: 0x040006F9 RID: 1785
		private DataSet m_dataSet;

		// Token: 0x040006FA RID: 1786
		private DataSetInstance m_dataSetInstance;

		// Token: 0x02000934 RID: 2356
		private class FieldInfo
		{
			// Token: 0x04003FB1 RID: 16305
			public string UniqueName;

			// Token: 0x04003FB2 RID: 16306
			public int Index;

			// Token: 0x04003FB3 RID: 16307
			public bool DefinitionAdded;
		}
	}
}
