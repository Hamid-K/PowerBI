using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E3 RID: 1251
	internal class MslXmlSchemaWriter : XmlSchemaWriter
	{
		// Token: 0x06003E3B RID: 15931 RVA: 0x000CEB90 File Offset: 0x000CCD90
		internal MslXmlSchemaWriter(XmlWriter xmlWriter, double version)
		{
			this._xmlWriter = xmlWriter;
			this._version = version;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000CEBA6 File Offset: 0x000CCDA6
		internal void WriteSchema(DbDatabaseMapping databaseMapping)
		{
			this.WriteSchemaElementHeader();
			this.WriteDbModelElement(databaseMapping);
			this.WriteEndElement();
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000CEBBC File Offset: 0x000CCDBC
		private void WriteSchemaElementHeader()
		{
			string mslNamespace = MslConstructs.GetMslNamespace(this._version);
			this._xmlWriter.WriteStartElement("Mapping", mslNamespace);
			this._xmlWriter.WriteAttributeString("Space", "C-S");
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000CEBFC File Offset: 0x000CCDFC
		private void WriteDbModelElement(DbDatabaseMapping databaseMapping)
		{
			this._entityTypeNamespace = databaseMapping.Model.NamespaceNames.SingleOrDefault<string>();
			this._dbSchemaName = databaseMapping.Database.Containers.Single<EntityContainer>().Name;
			this.WriteEntityContainerMappingElement(databaseMapping.EntityContainerMappings.First<EntityContainerMapping>());
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000CEC4C File Offset: 0x000CCE4C
		internal void WriteEntityContainerMappingElement(EntityContainerMapping containerMapping)
		{
			this._xmlWriter.WriteStartElement("EntityContainerMapping");
			this._xmlWriter.WriteAttributeString("StorageEntityContainer", this._dbSchemaName);
			this._xmlWriter.WriteAttributeString("CdmEntityContainer", containerMapping.EdmEntityContainer.Name);
			foreach (EntitySetMapping entitySetMapping in containerMapping.EntitySetMappings)
			{
				this.WriteEntitySetMappingElement(entitySetMapping);
			}
			foreach (AssociationSetMapping associationSetMapping in containerMapping.AssociationSetMappings)
			{
				this.WriteAssociationSetMappingElement(associationSetMapping);
			}
			foreach (FunctionImportMappingComposable functionImportMappingComposable in containerMapping.FunctionImportMappings.OfType<FunctionImportMappingComposable>())
			{
				this.WriteFunctionImportMappingElement(functionImportMappingComposable);
			}
			foreach (FunctionImportMappingNonComposable functionImportMappingNonComposable in containerMapping.FunctionImportMappings.OfType<FunctionImportMappingNonComposable>())
			{
				this.WriteFunctionImportMappingElement(functionImportMappingNonComposable);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000CEDB4 File Offset: 0x000CCFB4
		public void WriteEntitySetMappingElement(EntitySetMapping entitySetMapping)
		{
			this._xmlWriter.WriteStartElement("EntitySetMapping");
			this._xmlWriter.WriteAttributeString("Name", entitySetMapping.EntitySet.Name);
			foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
			{
				this.WriteEntityTypeMappingElement(entityTypeMapping);
			}
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				this._xmlWriter.WriteStartElement("EntityTypeMapping");
				this._xmlWriter.WriteAttributeString("TypeName", MslXmlSchemaWriter.GetEntityTypeName(this._entityTypeNamespace + "." + entityTypeModificationFunctionMapping.EntityType.Name, false));
				this.WriteModificationFunctionMapping(entityTypeModificationFunctionMapping);
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000CEEC0 File Offset: 0x000CD0C0
		public void WriteAssociationSetMappingElement(AssociationSetMapping associationSetMapping)
		{
			this._xmlWriter.WriteStartElement("AssociationSetMapping");
			this._xmlWriter.WriteAttributeString("Name", associationSetMapping.AssociationSet.Name);
			this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + associationSetMapping.AssociationSet.ElementType.Name);
			this._xmlWriter.WriteAttributeString("StoreEntitySet", associationSetMapping.Table.Name);
			this.WriteAssociationEndMappingElement(associationSetMapping.SourceEndMapping);
			this.WriteAssociationEndMappingElement(associationSetMapping.TargetEndMapping);
			if (associationSetMapping.ModificationFunctionMapping != null)
			{
				this.WriteModificationFunctionMapping(associationSetMapping.ModificationFunctionMapping);
			}
			foreach (ConditionPropertyMapping conditionPropertyMapping in associationSetMapping.Conditions)
			{
				this.WriteConditionElement(conditionPropertyMapping);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000CEFBC File Offset: 0x000CD1BC
		private void WriteAssociationEndMappingElement(EndPropertyMapping endMapping)
		{
			this._xmlWriter.WriteStartElement("EndProperty");
			this._xmlWriter.WriteAttributeString("Name", endMapping.AssociationEnd.Name);
			foreach (ScalarPropertyMapping scalarPropertyMapping in endMapping.PropertyMappings)
			{
				this.WriteScalarPropertyElement(scalarPropertyMapping.Property.Name, scalarPropertyMapping.Column.Name);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000CF054 File Offset: 0x000CD254
		private void WriteEntityTypeMappingElement(EntityTypeMapping entityTypeMapping)
		{
			this._xmlWriter.WriteStartElement("EntityTypeMapping");
			this._xmlWriter.WriteAttributeString("TypeName", MslXmlSchemaWriter.GetEntityTypeName(this._entityTypeNamespace + "." + entityTypeMapping.EntityType.Name, entityTypeMapping.IsHierarchyMapping));
			foreach (MappingFragment mappingFragment in entityTypeMapping.MappingFragments)
			{
				this.WriteMappingFragmentElement(mappingFragment);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000CF0F4 File Offset: 0x000CD2F4
		internal void WriteMappingFragmentElement(MappingFragment mappingFragment)
		{
			this._xmlWriter.WriteStartElement("MappingFragment");
			this._xmlWriter.WriteAttributeString("StoreEntitySet", mappingFragment.TableSet.Name);
			foreach (PropertyMapping propertyMapping in mappingFragment.PropertyMappings)
			{
				this.WritePropertyMapping(propertyMapping);
			}
			foreach (ConditionPropertyMapping conditionPropertyMapping in mappingFragment.ColumnConditions)
			{
				this.WriteConditionElement(conditionPropertyMapping);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000CF1B4 File Offset: 0x000CD3B4
		public void WriteFunctionImportMappingElement(FunctionImportMappingComposable functionImportMapping)
		{
			this.WriteFunctionImportMappingStartElement(functionImportMapping);
			if (functionImportMapping.StructuralTypeMappings != null)
			{
				this._xmlWriter.WriteStartElement("ResultMapping");
				Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple = functionImportMapping.StructuralTypeMappings.Single<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>>();
				if (tuple.Item1.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
				{
					this._xmlWriter.WriteStartElement("ComplexTypeMapping");
					this._xmlWriter.WriteAttributeString("TypeName", tuple.Item1.FullName);
				}
				else
				{
					this._xmlWriter.WriteStartElement("EntityTypeMapping");
					this._xmlWriter.WriteAttributeString("TypeName", tuple.Item1.FullName);
					foreach (ConditionPropertyMapping conditionPropertyMapping in tuple.Item2)
					{
						this.WriteConditionElement(conditionPropertyMapping);
					}
				}
				foreach (PropertyMapping propertyMapping in tuple.Item3)
				{
					this.WritePropertyMapping(propertyMapping);
				}
				this._xmlWriter.WriteEndElement();
				this._xmlWriter.WriteEndElement();
			}
			this.WriteFunctionImportEndElement();
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000CF2FC File Offset: 0x000CD4FC
		public void WriteFunctionImportMappingElement(FunctionImportMappingNonComposable functionImportMapping)
		{
			this.WriteFunctionImportMappingStartElement(functionImportMapping);
			foreach (FunctionImportResultMapping functionImportResultMapping in functionImportMapping.ResultMappings)
			{
				this.WriteFunctionImportResultMappingElement(functionImportResultMapping);
			}
			this.WriteFunctionImportEndElement();
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000CF358 File Offset: 0x000CD558
		private void WriteFunctionImportMappingStartElement(FunctionImportMapping functionImportMapping)
		{
			this._xmlWriter.WriteStartElement("FunctionImportMapping");
			this._xmlWriter.WriteAttributeString("FunctionName", functionImportMapping.TargetFunction.FullName);
			this._xmlWriter.WriteAttributeString("FunctionImportName", functionImportMapping.FunctionImport.Name);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000CF3AC File Offset: 0x000CD5AC
		private void WriteFunctionImportResultMappingElement(FunctionImportResultMapping resultMapping)
		{
			this._xmlWriter.WriteStartElement("ResultMapping");
			foreach (FunctionImportStructuralTypeMapping functionImportStructuralTypeMapping in resultMapping.TypeMappings)
			{
				FunctionImportEntityTypeMapping functionImportEntityTypeMapping = functionImportStructuralTypeMapping as FunctionImportEntityTypeMapping;
				if (functionImportEntityTypeMapping != null)
				{
					this.WriteFunctionImportEntityTypeMappingElement(functionImportEntityTypeMapping);
				}
				else
				{
					this.WriteFunctionImportComplexTypeMappingElement((FunctionImportComplexTypeMapping)functionImportStructuralTypeMapping);
				}
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000CF42C File Offset: 0x000CD62C
		private void WriteFunctionImportEntityTypeMappingElement(FunctionImportEntityTypeMapping entityTypeMapping)
		{
			this._xmlWriter.WriteStartElement("EntityTypeMapping");
			string text = MslXmlSchemaWriter.CreateFunctionImportEntityTypeMappingTypeName(entityTypeMapping);
			this._xmlWriter.WriteAttributeString("TypeName", text);
			this.WriteFunctionImportPropertyMappingElements(entityTypeMapping.PropertyMappings.Cast<FunctionImportReturnTypeScalarPropertyMapping>());
			foreach (FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition in entityTypeMapping.Conditions)
			{
				this.WriteFunctionImportConditionElement(functionImportEntityTypeMappingCondition);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x000CF4C0 File Offset: 0x000CD6C0
		internal static string CreateFunctionImportEntityTypeMappingTypeName(FunctionImportEntityTypeMapping entityTypeMapping)
		{
			return string.Join(";", entityTypeMapping.EntityTypes.Select((EntityType e) => MslXmlSchemaWriter.GetEntityTypeName(e.FullName, false)).Concat(entityTypeMapping.IsOfTypeEntityTypes.Select((EntityType e) => MslXmlSchemaWriter.GetEntityTypeName(e.FullName, true))));
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x000CF530 File Offset: 0x000CD730
		private void WriteFunctionImportComplexTypeMappingElement(FunctionImportComplexTypeMapping complexTypeMapping)
		{
			this._xmlWriter.WriteStartElement("ComplexTypeMapping");
			this._xmlWriter.WriteAttributeString("TypeName", complexTypeMapping.ReturnType.FullName);
			this.WriteFunctionImportPropertyMappingElements(complexTypeMapping.PropertyMappings.Cast<FunctionImportReturnTypeScalarPropertyMapping>());
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x000CF584 File Offset: 0x000CD784
		private void WriteFunctionImportPropertyMappingElements(IEnumerable<FunctionImportReturnTypeScalarPropertyMapping> propertyMappings)
		{
			foreach (FunctionImportReturnTypeScalarPropertyMapping functionImportReturnTypeScalarPropertyMapping in propertyMappings)
			{
				this.WriteScalarPropertyElement(functionImportReturnTypeScalarPropertyMapping.PropertyName, functionImportReturnTypeScalarPropertyMapping.ColumnName);
			}
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x000CF5D8 File Offset: 0x000CD7D8
		private void WriteFunctionImportConditionElement(FunctionImportEntityTypeMappingCondition condition)
		{
			this._xmlWriter.WriteStartElement("Condition");
			this._xmlWriter.WriteAttributeString("ColumnName", condition.ColumnName);
			FunctionImportEntityTypeMappingConditionIsNull functionImportEntityTypeMappingConditionIsNull = condition as FunctionImportEntityTypeMappingConditionIsNull;
			if (functionImportEntityTypeMappingConditionIsNull != null)
			{
				this.WriteIsNullConditionAttribute(functionImportEntityTypeMappingConditionIsNull.IsNull);
			}
			else
			{
				this.WriteConditionValue(((FunctionImportEntityTypeMappingConditionValue)condition).Value);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000CF63F File Offset: 0x000CD83F
		private void WriteFunctionImportEndElement()
		{
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000CF64C File Offset: 0x000CD84C
		private void WriteModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			this._xmlWriter.WriteStartElement("ModificationFunctionMapping");
			this.WriteFunctionMapping("InsertFunction", modificationFunctionMapping.InsertFunctionMapping, false);
			this.WriteFunctionMapping("UpdateFunction", modificationFunctionMapping.UpdateFunctionMapping, false);
			this.WriteFunctionMapping("DeleteFunction", modificationFunctionMapping.DeleteFunctionMapping, false);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000CF6AC File Offset: 0x000CD8AC
		private void WriteModificationFunctionMapping(AssociationSetModificationFunctionMapping modificationFunctionMapping)
		{
			this._xmlWriter.WriteStartElement("ModificationFunctionMapping");
			this.WriteFunctionMapping("InsertFunction", modificationFunctionMapping.InsertFunctionMapping, true);
			this.WriteFunctionMapping("DeleteFunction", modificationFunctionMapping.DeleteFunctionMapping, true);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000CF6F8 File Offset: 0x000CD8F8
		public void WriteFunctionMapping(string functionElement, ModificationFunctionMapping functionMapping, bool associationSetMapping = false)
		{
			this._xmlWriter.WriteStartElement(functionElement);
			this._xmlWriter.WriteAttributeString("FunctionName", functionMapping.Function.FullName);
			if (functionMapping.RowsAffectedParameter != null)
			{
				this._xmlWriter.WriteAttributeString("RowsAffectedParameter", functionMapping.RowsAffectedParameter.Name);
			}
			if (!associationSetMapping)
			{
				this.WritePropertyParameterBindings(functionMapping.ParameterBindings, 0);
				this.WriteAssociationParameterBindings(functionMapping.ParameterBindings);
				if (functionMapping.ResultBindings != null)
				{
					this.WriteResultBindings(functionMapping.ResultBindings);
				}
			}
			else
			{
				this.WriteAssociationSetMappingParameterBindings(functionMapping.ParameterBindings);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000CF798 File Offset: 0x000CD998
		private void WriteAssociationSetMappingParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			foreach (IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding> grouping in from pm in parameterBindings
				where pm.MemberPath.AssociationSetEnd != null
				group pm by pm.MemberPath.AssociationSetEnd)
			{
				this._xmlWriter.WriteStartElement("EndProperty");
				this._xmlWriter.WriteAttributeString("Name", grouping.Key.Name);
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in grouping)
				{
					this.WriteScalarParameterElement(modificationFunctionParameterBinding.MemberPath.Members.First<EdmMember>(), modificationFunctionParameterBinding);
				}
				this._xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x000CF8A0 File Offset: 0x000CDAA0
		private void WritePropertyParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings, int level = 0)
		{
			foreach (IGrouping<EdmMember, ModificationFunctionParameterBinding> grouping in from pm in parameterBindings
				where pm.MemberPath.AssociationSetEnd == null && pm.MemberPath.Members.Count<EdmMember>() > level
				group pm by pm.MemberPath.Members.ElementAt(level))
			{
				EdmProperty edmProperty = (EdmProperty)grouping.Key;
				if (edmProperty.IsComplexType)
				{
					this._xmlWriter.WriteStartElement("ComplexProperty");
					this._xmlWriter.WriteAttributeString("Name", edmProperty.Name);
					this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + edmProperty.ComplexType.Name);
					this.WritePropertyParameterBindings(grouping, level + 1);
					this._xmlWriter.WriteEndElement();
				}
				else
				{
					foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in grouping)
					{
						this.WriteScalarParameterElement(edmProperty, modificationFunctionParameterBinding);
					}
				}
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000CF9D8 File Offset: 0x000CDBD8
		private void WriteAssociationParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			using (IEnumerator<IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding>> enumerator = (from pm in parameterBindings
				where pm.MemberPath.AssociationSetEnd != null
				group pm by pm.MemberPath.AssociationSetEnd).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding> group = enumerator.Current;
					this._xmlWriter.WriteStartElement("AssociationEnd");
					AssociationSet parentAssociationSet = group.Key.ParentAssociationSet;
					this._xmlWriter.WriteAttributeString("AssociationSet", parentAssociationSet.Name);
					this._xmlWriter.WriteAttributeString("From", group.Key.Name);
					this._xmlWriter.WriteAttributeString("To", parentAssociationSet.AssociationSetEnds.Single((AssociationSetEnd ae) => ae != group.Key).Name);
					foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in group)
					{
						this.WriteScalarParameterElement(modificationFunctionParameterBinding.MemberPath.Members.First<EdmMember>(), modificationFunctionParameterBinding);
					}
					this._xmlWriter.WriteEndElement();
				}
			}
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000CFB50 File Offset: 0x000CDD50
		private void WriteResultBindings(IEnumerable<ModificationFunctionResultBinding> resultBindings)
		{
			foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in resultBindings)
			{
				this._xmlWriter.WriteStartElement("ResultBinding");
				this._xmlWriter.WriteAttributeString("Name", modificationFunctionResultBinding.Property.Name);
				this._xmlWriter.WriteAttributeString("ColumnName", modificationFunctionResultBinding.ColumnName);
				this._xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000CFBE0 File Offset: 0x000CDDE0
		private void WriteScalarParameterElement(EdmMember member, ModificationFunctionParameterBinding parameterBinding)
		{
			this._xmlWriter.WriteStartElement("ScalarProperty");
			this._xmlWriter.WriteAttributeString("Name", member.Name);
			this._xmlWriter.WriteAttributeString("ParameterName", parameterBinding.Parameter.Name);
			this._xmlWriter.WriteAttributeString("Version", parameterBinding.IsCurrent ? "Current" : "Original");
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000CFC60 File Offset: 0x000CDE60
		private void WritePropertyMapping(PropertyMapping propertyMapping)
		{
			ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
			if (scalarPropertyMapping != null)
			{
				this.WritePropertyMapping(scalarPropertyMapping);
				return;
			}
			ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
			if (complexPropertyMapping != null)
			{
				this.WritePropertyMapping(complexPropertyMapping);
			}
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000CFC90 File Offset: 0x000CDE90
		private void WritePropertyMapping(ScalarPropertyMapping scalarPropertyMapping)
		{
			this.WriteScalarPropertyElement(scalarPropertyMapping.Property.Name, scalarPropertyMapping.Column.Name);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000CFCB0 File Offset: 0x000CDEB0
		private void WritePropertyMapping(ComplexPropertyMapping complexPropertyMapping)
		{
			this._xmlWriter.WriteStartElement("ComplexProperty");
			this._xmlWriter.WriteAttributeString("Name", complexPropertyMapping.Property.Name);
			this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + complexPropertyMapping.Property.ComplexType.Name);
			foreach (PropertyMapping propertyMapping in complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>().PropertyMappings)
			{
				this.WritePropertyMapping(propertyMapping);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000CFD70 File Offset: 0x000CDF70
		private static string GetEntityTypeName(string fullyQualifiedEntityTypeName, bool isHierarchyMapping)
		{
			if (isHierarchyMapping)
			{
				return "IsTypeOf(" + fullyQualifiedEntityTypeName + ")";
			}
			return fullyQualifiedEntityTypeName;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000CFD88 File Offset: 0x000CDF88
		private void WriteConditionElement(ConditionPropertyMapping condition)
		{
			this._xmlWriter.WriteStartElement("Condition");
			if (condition.IsNull != null)
			{
				this.WriteIsNullConditionAttribute(condition.IsNull.Value);
			}
			else
			{
				this.WriteConditionValue(condition.Value);
			}
			this._xmlWriter.WriteAttributeString("ColumnName", condition.Column.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000CFDFD File Offset: 0x000CDFFD
		private void WriteIsNullConditionAttribute(bool isNullValue)
		{
			this._xmlWriter.WriteAttributeString("IsNull", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(isNullValue));
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000CFE18 File Offset: 0x000CE018
		private void WriteConditionValue(object conditionValue)
		{
			if (conditionValue is bool)
			{
				this._xmlWriter.WriteAttributeString("Value", ((bool)conditionValue) ? "1" : "0");
				return;
			}
			this._xmlWriter.WriteAttributeString("Value", conditionValue.ToString());
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000CFE68 File Offset: 0x000CE068
		private void WriteScalarPropertyElement(string propertyName, string columnName)
		{
			this._xmlWriter.WriteStartElement("ScalarProperty");
			this._xmlWriter.WriteAttributeString("Name", propertyName);
			this._xmlWriter.WriteAttributeString("ColumnName", columnName);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x04001523 RID: 5411
		private string _entityTypeNamespace;

		// Token: 0x04001524 RID: 5412
		private string _dbSchemaName;
	}
}
