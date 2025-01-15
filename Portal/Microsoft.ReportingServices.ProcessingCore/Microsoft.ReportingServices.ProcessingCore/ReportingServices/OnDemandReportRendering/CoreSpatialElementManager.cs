using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000159 RID: 345
	internal abstract class CoreSpatialElementManager
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x0003E22B File Offset: 0x0003C42B
		internal CoreSpatialElementManager(MapControl coreMap, MapVectorLayer mapVectorLayer)
		{
			this.m_coreMap = coreMap;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003E244 File Offset: 0x0003C444
		internal ISpatialElement AddGeography(SqlGeography geography, string layerName)
		{
			if (geography == null)
			{
				return null;
			}
			if (geography.IsNull)
			{
				return null;
			}
			ISpatialElement spatialElement = this.CreateSpatialElement();
			spatialElement.Layer = layerName;
			spatialElement.Category = layerName;
			this.AddSpatialElement(spatialElement);
			if (!spatialElement.AddGeography(geography))
			{
				this.RemoveSpatialElement(spatialElement);
				return null;
			}
			return spatialElement;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003E290 File Offset: 0x0003C490
		internal ISpatialElement AddGeometry(SqlGeometry geometry, string layerName)
		{
			if (geometry == null)
			{
				return null;
			}
			if (geometry.IsNull)
			{
				return null;
			}
			ISpatialElement spatialElement = this.CreateSpatialElement();
			spatialElement.Layer = layerName;
			spatialElement.Category = layerName;
			if (spatialElement.AddGeometry(geometry))
			{
				this.AddSpatialElement(spatialElement);
				return spatialElement;
			}
			return null;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		internal ISpatialElement AddWKB(string wkb, string layerName)
		{
			ISpatialElement spatialElement = this.CreateSpatialElement();
			spatialElement.Layer = layerName;
			spatialElement.Category = layerName;
			if (spatialElement.AddWKB(Convert.FromBase64String(wkb)))
			{
				this.AddSpatialElement(spatialElement);
				return spatialElement;
			}
			return null;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003E310 File Offset: 0x0003C510
		internal static Type GetFieldType(object value)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				return typeof(bool);
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
				return typeof(int);
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Decimal:
				return typeof(decimal);
			case TypeCode.Single:
			case TypeCode.Double:
				return typeof(double);
			case TypeCode.DateTime:
				return typeof(DateTime);
			}
			return typeof(string);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003E3AC File Offset: 0x0003C5AC
		internal void AddFieldDefinition(string fieldName, Type type)
		{
			Field field = new Field();
			field.Name = fieldName;
			field.Type = type;
			this.FieldDefinitions.Add(field);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		internal void AddFieldValue(ISpatialElement spatialElement, string fieldName, object value)
		{
			try
			{
				spatialElement[fieldName] = value;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				spatialElement[fieldName] = value.ToString();
				this.m_mapVectorLayer.MapDef.RenderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsMapUnsupportedValueFieldType, Severity.Warning, this.m_mapVectorLayer.MapDef.MapDef.ObjectType, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, new string[] { fieldName });
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003E47C File Offset: 0x0003C67C
		internal string AddRuleField(object dataValue)
		{
			string text = this.GenerateUniqueFieldName();
			this.AddFieldDefinition(text, CoreSpatialElementManager.GetFieldType(dataValue));
			return text;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
		private string GenerateUniqueFieldName()
		{
			int num = this.FieldDefinitions.Count;
			string text;
			for (;;)
			{
				text = num.ToString(CultureInfo.InvariantCulture);
				if (this.FieldDefinitions.GetByName(text) == null)
				{
					break;
				}
				num++;
			}
			return text;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003E4DC File Offset: 0x0003C6DC
		internal int GetSpatialElementCount()
		{
			int num = 0;
			using (IEnumerator enumerator = this.SpatialElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(((ISpatialElement)enumerator.Current).Layer != this.m_mapVectorLayer.Name))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003E54C File Offset: 0x0003C74C
		internal int GetDistinctValuesCount(string fieldName)
		{
			CollectionBase spatialElements = this.SpatialElements;
			Dictionary<object, object> dictionary = new Dictionary<object, object>();
			foreach (object obj in spatialElements)
			{
				ISpatialElement spatialElement = (ISpatialElement)obj;
				if (!(spatialElement.Layer != this.m_mapVectorLayer.Name))
				{
					object obj2 = spatialElement[fieldName];
					if (obj2 != null && !dictionary.ContainsKey(obj2))
					{
						dictionary.Add(obj2, null);
					}
				}
			}
			return dictionary.Count;
		}

		// Token: 0x06000E50 RID: 3664
		internal abstract void AddSpatialElement(ISpatialElement spatialElement);

		// Token: 0x06000E51 RID: 3665
		internal abstract void RemoveSpatialElement(ISpatialElement spatialElement);

		// Token: 0x06000E52 RID: 3666
		internal abstract ISpatialElement CreateSpatialElement();

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06000E53 RID: 3667
		internal abstract FieldCollection FieldDefinitions { get; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06000E54 RID: 3668
		protected abstract NamedCollection SpatialElements { get; }

		// Token: 0x040006EF RID: 1775
		protected MapControl m_coreMap;

		// Token: 0x040006F0 RID: 1776
		protected MapVectorLayer m_mapVectorLayer;
	}
}
