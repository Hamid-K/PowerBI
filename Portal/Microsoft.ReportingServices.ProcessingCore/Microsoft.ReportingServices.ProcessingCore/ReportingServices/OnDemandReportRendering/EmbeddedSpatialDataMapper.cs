using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015F RID: 351
	internal class EmbeddedSpatialDataMapper : SpatialDataMapper
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x0003EDCC File Offset: 0x0003CFCC
		internal EmbeddedSpatialDataMapper(VectorLayerMapper vectorLayerMapper, Dictionary<SpatialElementKey, SpatialElementInfoGroup> spatialElementsDictionary, ISpatialElementCollection embeddedCollection, CoreSpatialElementManager spatialElementManager, MapControl coreMap, MapMapper mapMapper)
			: base(vectorLayerMapper, spatialElementsDictionary, coreMap, mapMapper)
		{
			this.m_spatialElementManager = spatialElementManager;
			this.m_embeddedCollection = embeddedCollection;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003EDE9 File Offset: 0x0003CFE9
		internal override void Populate()
		{
			this.AddFieldDefinitions();
			this.AddSpatialElements();
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
		private void AddFieldDefinitions()
		{
			MapFieldDefinitionCollection mapFieldDefinitions = this.m_mapVectorLayer.MapFieldDefinitions;
			if (mapFieldDefinitions == null)
			{
				return;
			}
			foreach (MapFieldDefinition mapFieldDefinition in mapFieldDefinitions)
			{
				this.m_spatialElementManager.AddFieldDefinition(base.GetUniqueFieldName(mapFieldDefinition.Name), this.GetFieldType(mapFieldDefinition.DataType));
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003EE6C File Offset: 0x0003D06C
		private void AddSpatialElements()
		{
			for (int i = 0; i < this.m_embeddedCollection.Count; i++)
			{
				this.AddSpatialElement(this.m_embeddedCollection.GetItem(i));
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		private void AddSpatialElement(MapSpatialElement embeddedElement)
		{
			if (!this.m_mapMapper.CanAddSpatialElement)
			{
				return;
			}
			ISpatialElement spatialElement = this.m_spatialElementManager.AddWKB(embeddedElement.VectorData, this.m_mapVectorLayer.Name);
			if (spatialElement != null)
			{
				this.ProcessNonSpatialFields(embeddedElement, spatialElement);
				base.OnSpatialElementAdded(new SpatialElementInfo
				{
					CoreSpatialElement = spatialElement,
					MapSpatialElement = embeddedElement
				});
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003EF04 File Offset: 0x0003D104
		private void ProcessNonSpatialFields(MapSpatialElement embeddedElement, ISpatialElement spatialElement)
		{
			MapFieldCollection mapFields = embeddedElement.MapFields;
			if (mapFields == null)
			{
				return;
			}
			MapFieldDefinitionCollection mapFieldDefinitions = this.m_mapVectorLayer.MapFieldDefinitions;
			if (mapFieldDefinitions == null)
			{
				return;
			}
			foreach (MapField mapField in mapFields)
			{
				MapFieldDefinition fieldDefinition = mapFieldDefinitions.GetFieldDefinition(mapField.Name);
				if (fieldDefinition == null)
				{
					throw new RenderingObjectModelException(RPResWrapper.rsMapInvalidFieldName(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, mapField.Name));
				}
				this.m_spatialElementManager.AddFieldValue(spatialElement, base.GetUniqueFieldName(mapField.Name), this.GetFieldValue(mapField.Value, fieldDefinition.DataType));
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		private Type GetFieldType(MapDataType dataType)
		{
			switch (dataType)
			{
			case MapDataType.Boolean:
				return typeof(bool);
			case MapDataType.DateTime:
				return typeof(DateTime);
			case MapDataType.Integer:
				return typeof(int);
			case MapDataType.Float:
				return typeof(double);
			case MapDataType.Decimal:
				return typeof(decimal);
			default:
				return typeof(string);
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003F03C File Offset: 0x0003D23C
		private object GetFieldValue(string stringValue, MapDataType dataType)
		{
			object obj;
			try
			{
				switch (dataType)
				{
				case MapDataType.Boolean:
					obj = Convert.ToBoolean(stringValue, CultureInfo.InvariantCulture);
					break;
				case MapDataType.DateTime:
					obj = Convert.ToDateTime(stringValue, CultureInfo.InvariantCulture);
					break;
				case MapDataType.Integer:
					obj = Convert.ToInt32(stringValue, CultureInfo.InvariantCulture);
					break;
				case MapDataType.Float:
					obj = Convert.ToDouble(stringValue, CultureInfo.InvariantCulture);
					break;
				case MapDataType.Decimal:
					obj = Convert.ToDecimal(stringValue, CultureInfo.InvariantCulture);
					break;
				default:
					obj = stringValue;
					break;
				}
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				obj = null;
			}
			return obj;
		}

		// Token: 0x040006FB RID: 1787
		private ISpatialElementCollection m_embeddedCollection;

		// Token: 0x040006FC RID: 1788
		private CoreSpatialElementManager m_spatialElementManager;
	}
}
