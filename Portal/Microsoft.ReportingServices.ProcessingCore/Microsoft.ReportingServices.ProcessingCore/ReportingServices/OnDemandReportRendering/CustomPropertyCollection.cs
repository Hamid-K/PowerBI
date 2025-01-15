using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B3 RID: 691
	public sealed class CustomPropertyCollection : ReportElementCollectionBase<CustomProperty>
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x0006A79F File Offset: 0x0006899F
		internal CustomPropertyCollection()
		{
			this.m_list = new List<CustomProperty>();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0006A7B4 File Offset: 0x000689B4
		internal CustomPropertyCollection(IReportScopeInstance romInstance, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ReportElement reportElementOwner, ICustomPropertiesHolder customPropertiesHolder, ObjectType objectType, string objectName)
		{
			this.m_reportElementOwner = reportElementOwner;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataValueList customProperties = customPropertiesHolder.CustomProperties;
			if (customProperties == null)
			{
				this.m_list = new List<CustomProperty>();
				return;
			}
			bool flag = InstancePathItem.IsValidContext(customPropertiesHolder.InstancePath.InstancePath);
			int count = customProperties.Count;
			this.m_list = new List<CustomProperty>(count);
			for (int i = 0; i < count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = customProperties[i];
				string text = null;
				object obj = null;
				TypeCode typeCode = TypeCode.Empty;
				if (flag)
				{
					dataValue.EvaluateNameAndValue(this.m_reportElementOwner, romInstance, customPropertiesHolder.InstancePath, renderingContext.OdpContext, objectType, objectName, out text, out obj, out typeCode);
				}
				CustomProperty customProperty = new CustomProperty(this.m_reportElementOwner, renderingContext, dataValue.Name, dataValue.Value, text, obj, typeCode);
				this.m_list.Add(customProperty);
				if (flag)
				{
					this.AddPropToLookupTable(text, customProperty);
				}
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0006A88C File Offset: 0x00068A8C
		internal CustomPropertyCollection(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, CustomPropertyCollection collection)
		{
			if (collection == null)
			{
				this.m_list = new List<CustomProperty>();
				return;
			}
			int count = collection.Count;
			this.m_list = new List<CustomProperty>(count);
			for (int i = 0; i < count; i++)
			{
				Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expressionInfo;
				Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expressionInfo2;
				string text;
				object obj;
				collection.GetNameValueExpressions(i, out expressionInfo, out expressionInfo2, out text, out obj);
				CustomProperty customProperty = new CustomProperty(renderingContext, expressionInfo, expressionInfo2, text, obj, TypeCode.Empty);
				this.m_list.Add(customProperty);
				this.AddPropToLookupTable(text, customProperty);
			}
		}

		// Token: 0x17000F0E RID: 3854
		public CustomProperty this[string name]
		{
			get
			{
				if (name != null && this.m_lookupTable != null)
				{
					CustomProperty customProperty = null;
					if (this.m_lookupTable.TryGetValue(name, out customProperty))
					{
						return customProperty;
					}
				}
				return null;
			}
		}

		// Token: 0x17000F0F RID: 3855
		public override CustomProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_list[index];
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0006A98B File Offset: 0x00068B8B
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0006A998 File Offset: 0x00068B98
		internal CustomProperty Add(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo nameExpr, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpr)
		{
			CustomProperty customProperty = new CustomProperty(this.m_reportElementOwner, renderingContext, nameExpr, valueExpr, null, null, TypeCode.Empty);
			Global.Tracer.Assert(customProperty.Instance != null, "prop.Instance != null");
			this.m_list.Add(customProperty);
			return customProperty;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0006A9DC File Offset: 0x00068BDC
		internal void UpdateCustomProperties(CustomPropertyCollection collection)
		{
			int count = this.m_list.Count;
			for (int i = 0; i < count; i++)
			{
				string text = null;
				object obj = null;
				if (collection != null)
				{
					collection.GetNameValue(i, out text, out obj);
				}
				this.m_list[i].Update(text, obj, TypeCode.Empty);
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0006AA28 File Offset: 0x00068C28
		internal void UpdateCustomProperties(IReportScopeInstance romInstance, ICustomPropertiesHolder customPropertiesHolder, OnDemandProcessingContext context, ObjectType objectType, string objectName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataValueList customProperties = customPropertiesHolder.CustomProperties;
			int count = this.m_list.Count;
			bool flag = false;
			if (this.m_lookupTable == null)
			{
				flag = true;
			}
			for (int i = 0; i < count; i++)
			{
				string text = null;
				object obj = null;
				TypeCode typeCode = TypeCode.Empty;
				if (customProperties != null && i < customProperties.Count)
				{
					customProperties[i].EvaluateNameAndValue(this.m_reportElementOwner, romInstance, customPropertiesHolder.InstancePath, context, objectType, objectName, out text, out obj, out typeCode);
				}
				this.m_list[i].Update(text, obj, typeCode);
				if (flag)
				{
					this.AddPropToLookupTable(text, this.m_list[i]);
				}
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0006AAC7 File Offset: 0x00068CC7
		private void AddPropToLookupTable(string name, CustomProperty property)
		{
			if (this.m_lookupTable == null)
			{
				this.m_lookupTable = new Dictionary<string, CustomProperty>(this.m_list.Count);
			}
			if (name != null && !this.m_lookupTable.ContainsKey(name))
			{
				this.m_lookupTable.Add(name, property);
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006AB08 File Offset: 0x00068D08
		internal void ConstructCustomPropertyDefinitions(Microsoft.ReportingServices.ReportIntermediateFormat.DataValueList dataValueDefs)
		{
			Global.Tracer.Assert(dataValueDefs != null && this.m_list.Count == dataValueDefs.Count, "m_list.Count == dataValueDefs.Count");
			for (int i = 0; i < this.m_list.Count; i++)
			{
				CustomProperty customProperty = this.m_list[i];
				customProperty.ConstructCustomPropertyDefinition(dataValueDefs[i]);
				if (customProperty.Instance != null && customProperty.Instance.Name != null)
				{
					this.AddPropToLookupTable(customProperty.Instance.Name, customProperty);
				}
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0006AB94 File Offset: 0x00068D94
		internal void GetDynamicValues(out List<string> customPropertyNames, out List<object> customPropertyValues)
		{
			customPropertyNames = new List<string>(this.m_list.Count);
			customPropertyValues = new List<object>(this.m_list.Count);
			bool flag = false;
			for (int i = 0; i < this.m_list.Count; i++)
			{
				CustomProperty customProperty = this.m_list[i];
				string text = null;
				if (customProperty.Name.IsExpression)
				{
					flag = true;
					text = customProperty.Instance.Name;
				}
				object obj = null;
				if (customProperty.Value.IsExpression)
				{
					flag = true;
					obj = customProperty.Instance.Value;
				}
				customPropertyNames.Add(text);
				customPropertyValues.Add(obj);
			}
			if (!flag)
			{
				customPropertyNames = null;
				customPropertyValues = null;
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0006AC40 File Offset: 0x00068E40
		internal void SetDynamicValues(List<string> customPropertyNames, List<object> customPropertyValues)
		{
			if (customPropertyNames == null && customPropertyValues == null)
			{
				return;
			}
			Global.Tracer.Assert(customPropertyNames != null && customPropertyValues != null && customPropertyNames.Count == customPropertyValues.Count && this.m_list.Count == customPropertyNames.Count, "Chck customPropertyNames and customPropertyValues consistency");
			for (int i = 0; i < this.m_list.Count; i++)
			{
				CustomProperty customProperty = this.m_list[i];
				if (customProperty.Name.IsExpression)
				{
					customProperty.Instance.Name = customPropertyNames[i];
				}
				if (customProperty.Value.IsExpression)
				{
					customProperty.Instance.Value = customPropertyValues[i];
				}
			}
		}

		// Token: 0x04000D2B RID: 3371
		private List<CustomProperty> m_list;

		// Token: 0x04000D2C RID: 3372
		private Dictionary<string, CustomProperty> m_lookupTable;

		// Token: 0x04000D2D RID: 3373
		private ReportElement m_reportElementOwner;
	}
}
