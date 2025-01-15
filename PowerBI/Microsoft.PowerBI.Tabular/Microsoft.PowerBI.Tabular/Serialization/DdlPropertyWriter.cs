using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016D RID: 365
	internal class DdlPropertyWriter : IPropertyWriter
	{
		// Token: 0x0600174C RID: 5964 RVA: 0x000A1BF8 File Offset: 0x0009FDF8
		public DdlPropertyWriter()
		{
			this.writtenProperties = new Dictionary<string, string>();
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000A1C0B File Offset: 0x0009FE0B
		public void SetParentElement(XElement parentElement)
		{
			this.parentElement = parentElement;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000A1C14 File Offset: 0x0009FE14
		public void SetSchema(IObjectRowsetSchema schema)
		{
			this.schema = schema;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000A1C1D File Offset: 0x0009FE1D
		public void BeginWrite()
		{
			this.writtenProperties.Clear();
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000A1C2C File Offset: 0x0009FE2C
		public void EndWrite()
		{
			foreach (string text in this.schema.OrderedPropertyList)
			{
				string text2;
				if (this.writtenProperties.TryGetValue(text, out text2))
				{
					this.parentElement.Add(new XElement(XmlaConstants.XNS.rst + text, text2));
					this.writtenProperties.Remove(text);
				}
			}
			this.writtenProperties.Clear();
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000A1CBC File Offset: 0x0009FEBC
		void IPropertyWriter.WriteProperty(WriteOptions options, string name, Type type, object value)
		{
			if (type == typeof(MetadataObject) || type.IsSubclassOf(typeof(MetadataObject)))
			{
				MetadataObject.WriteObjectId(this, options, name, (MetadataObject)value);
				return;
			}
			if (type == typeof(ObjectId))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertIDToXml((ObjectId)value);
				return;
			}
			if (type == typeof(bool))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertBooleanToXml((bool)value);
				return;
			}
			if (type == typeof(string))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertStringToXml((value != null) ? value.ToString() : null);
				return;
			}
			if (type == typeof(int))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertInt32ToXml((int)value);
				return;
			}
			if (type == typeof(long))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertInt64ToXml((long)value);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDateTimeToXml((DateTime)value);
				return;
			}
			if (type == typeof(ObjectState))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertObjectStateToXml((ObjectState)value);
				return;
			}
			if (type == typeof(ImpersonationMode))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertImpersonationModeToXml((ImpersonationMode)value);
				return;
			}
			if (type == typeof(Alignment))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertAlignmentToXml((Alignment)value);
				return;
			}
			if (type == typeof(AggregateFunction))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertAggregateFunctionToXml((AggregateFunction)value);
				return;
			}
			if (type == typeof(DatasourceIsolation))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDatasourceIsolationToXml((DatasourceIsolation)value);
				return;
			}
			if (type == typeof(ColumnType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertColumnTypeToXml((ColumnType)value);
				return;
			}
			if (type == typeof(PartitionSourceType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertPartitionSourceTypeToXml((PartitionSourceType)value);
				return;
			}
			if (type == typeof(EvaluationBehavior))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertEvaluationBehaviorToXml((EvaluationBehavior)value);
				return;
			}
			if (type == typeof(DataType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDataTypeToXml((DataType)value);
				return;
			}
			if (type == typeof(RefreshType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRefreshTypeToXml((RefreshType)value);
				return;
			}
			if (type == typeof(RelationshipEndCardinality))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRelationshipEndCardinalityToXml((RelationshipEndCardinality)value);
				return;
			}
			if (type == typeof(CrossFilteringBehavior))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertCrossFilteringBehaviorToXml((CrossFilteringBehavior)value);
				return;
			}
			if (type == typeof(SecurityFilteringBehavior))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertSecurityFilteringBehaviorToXml((SecurityFilteringBehavior)value);
				return;
			}
			if (type == typeof(TranslatedProperty))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertTranslatedPropertyToXml((TranslatedProperty)value);
				return;
			}
			if (type == typeof(DataSourceType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDataSourceTypeToXml((DataSourceType)value);
				return;
			}
			if (type == typeof(RelationshipType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRelationshipTypeToXml((RelationshipType)value);
				return;
			}
			if (type == typeof(DateTimeRelationshipBehavior))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDateTimeRelationshipBehaviorToXml((DateTimeRelationshipBehavior)value);
				return;
			}
			if (type == typeof(ModeType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertModeTypeToXml((ModeType)value);
				return;
			}
			if (type == typeof(DataViewType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDataViewTypeToXml((DataViewType)value);
				return;
			}
			if (type == typeof(DirectLakeBehavior))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDirectLakeBehaviorToXml((DirectLakeBehavior)value);
				return;
			}
			if (type == typeof(ModelPermission))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertModelPermissionToXml((ModelPermission)value);
				return;
			}
			if (type == typeof(RoleMemberType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRoleMemberTypeToXml((RoleMemberType)value);
				return;
			}
			if (type == typeof(ExtendedPropertyType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertExtendedPropertyTypeToXml((ExtendedPropertyType)value);
				return;
			}
			if (type == typeof(HierarchyHideMembersType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertHierarchyHideMembersTypeToXml((HierarchyHideMembersType)value);
				return;
			}
			if (type == typeof(ExpressionKind))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertExpressionKindToXml((ExpressionKind)value);
				return;
			}
			if (type == typeof(MetadataPermission))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertMetadataPermissionToXml((MetadataPermission)value);
				return;
			}
			if (type == typeof(EncodingHintType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertEncodingHintTypeToXml((EncodingHintType)value);
				return;
			}
			if (type == typeof(SummarizationType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertSummarizationTypeToXml((SummarizationType)value);
				return;
			}
			if (type == typeof(RefreshGranularityType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRefreshGranularityTypeToXml((RefreshGranularityType)value);
				return;
			}
			if (type == typeof(RefreshPolicyType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRefreshPolicyTypeToXml((RefreshPolicyType)value);
				return;
			}
			if (type == typeof(PowerBIDataSourceVersion))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertPowerBIDataSourceVersionToXml((PowerBIDataSourceVersion)value);
				return;
			}
			if (type == typeof(ContentType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertContentTypeToXml((ContentType)value);
				return;
			}
			if (type == typeof(DataSourceVariablesOverrideBehaviorType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertDataSourceVariablesOverrideBehaviorTypeToXml((DataSourceVariablesOverrideBehaviorType)value);
				return;
			}
			if (type == typeof(RefreshPolicyMode))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertRefreshPolicyModeToXml((RefreshPolicyMode)value);
				return;
			}
			if (type == typeof(TimeUnit))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertTimeUnitToXml((TimeUnit)value);
				return;
			}
			if (type == typeof(CalculationGroupSelectionMode))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertCalculationGroupSelectionModeToXml((CalculationGroupSelectionMode)value);
				return;
			}
			if (type == typeof(ValueFilterBehaviorType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertValueFilterBehaviorTypeToXml((ValueFilterBehaviorType)value);
				return;
			}
			if (type == typeof(BindingInfoType))
			{
				this.writtenProperties[name] = PropertyHelper.ConvertBindingInfoTypeToXml((BindingInfoType)value);
				return;
			}
			throw TomInternalException.Create("Unable to convert object of type '{0}' to string", new object[] { type.Name });
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000A2481 File Offset: 0x000A0681
		void IPropertyWriter.WriteProperty<T>(WriteOptions options, string name, T value)
		{
			((IPropertyWriter)this).WriteProperty(options, name, typeof(T), value);
		}

		// Token: 0x04000446 RID: 1094
		private XElement parentElement;

		// Token: 0x04000447 RID: 1095
		private IObjectRowsetSchema schema;

		// Token: 0x04000448 RID: 1096
		private Dictionary<string, string> writtenProperties;
	}
}
