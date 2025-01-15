using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Edm
{
	// Token: 0x020002CA RID: 714
	internal abstract class EdmModelVisitor
	{
		// Token: 0x0600225A RID: 8794 RVA: 0x000611F4 File Offset: 0x0005F3F4
		protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
		{
			if (collection != null)
			{
				foreach (T t in collection)
				{
					visitMethod(t);
				}
			}
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00061240 File Offset: 0x0005F440
		protected internal virtual void VisitEdmModel(EdmModel item)
		{
			if (item != null)
			{
				this.VisitComplexTypes(item.ComplexTypes);
				this.VisitEntityTypes(item.EntityTypes);
				this.VisitEnumTypes(item.EnumTypes);
				this.VisitAssociationTypes(item.AssociationTypes);
				this.VisitFunctions(item.Functions);
				this.VisitEntityContainers(item.Containers);
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00061298 File Offset: 0x0005F498
		protected virtual void VisitAnnotations(MetadataItem item, IEnumerable<MetadataProperty> annotations)
		{
			EdmModelVisitor.VisitCollection<MetadataProperty>(annotations, new Action<MetadataProperty>(this.VisitAnnotation));
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000612AD File Offset: 0x0005F4AD
		protected virtual void VisitAnnotation(MetadataProperty item)
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000612AF File Offset: 0x0005F4AF
		protected internal virtual void VisitMetadataItem(MetadataItem item)
		{
			if (item != null && item.Annotations.Any<MetadataProperty>())
			{
				this.VisitAnnotations(item, item.Annotations);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000612CE File Offset: 0x0005F4CE
		protected virtual void VisitEntityContainers(IEnumerable<EntityContainer> entityContainers)
		{
			EdmModelVisitor.VisitCollection<EntityContainer>(entityContainers, new Action<EntityContainer>(this.VisitEdmEntityContainer));
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000612E4 File Offset: 0x0005F4E4
		protected virtual void VisitEdmEntityContainer(EntityContainer item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.EntitySets.Count > 0)
				{
					this.VisitEntitySets(item, item.EntitySets);
				}
				if (item.AssociationSets.Count > 0)
				{
					this.VisitAssociationSets(item, item.AssociationSets);
				}
				if (item.FunctionImports.Count > 0)
				{
					this.VisitFunctionImports(item, item.FunctionImports);
				}
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0006134C File Offset: 0x0005F54C
		protected internal virtual void VisitEdmFunction(EdmFunction function)
		{
			this.VisitMetadataItem(function);
			if (function != null)
			{
				if (function.Parameters != null)
				{
					this.VisitFunctionParameters(function.Parameters);
				}
				if (function.ReturnParameters != null)
				{
					this.VisitFunctionReturnParameters(function.ReturnParameters);
				}
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00061380 File Offset: 0x0005F580
		protected virtual void VisitEntitySets(EntityContainer container, IEnumerable<EntitySet> entitySets)
		{
			EdmModelVisitor.VisitCollection<EntitySet>(entitySets, new Action<EntitySet>(this.VisitEdmEntitySet));
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00061395 File Offset: 0x0005F595
		protected internal virtual void VisitEdmEntitySet(EntitySet item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0006139E File Offset: 0x0005F59E
		protected virtual void VisitAssociationSets(EntityContainer container, IEnumerable<AssociationSet> associationSets)
		{
			EdmModelVisitor.VisitCollection<AssociationSet>(associationSets, new Action<AssociationSet>(this.VisitEdmAssociationSet));
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000613B3 File Offset: 0x0005F5B3
		protected virtual void VisitEdmAssociationSet(AssociationSet item)
		{
			this.VisitMetadataItem(item);
			if (item.SourceSet != null)
			{
				this.VisitEdmAssociationSetEnd(item.SourceSet);
			}
			if (item.TargetSet != null)
			{
				this.VisitEdmAssociationSetEnd(item.TargetSet);
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000613E4 File Offset: 0x0005F5E4
		protected virtual void VisitEdmAssociationSetEnd(EntitySet item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000613ED File Offset: 0x0005F5ED
		protected internal virtual void VisitFunctionImports(EntityContainer container, IEnumerable<EdmFunction> functionImports)
		{
			EdmModelVisitor.VisitCollection<EdmFunction>(functionImports, new Action<EdmFunction>(this.VisitFunctionImport));
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00061402 File Offset: 0x0005F602
		protected internal virtual void VisitFunctionImport(EdmFunction functionImport)
		{
			this.VisitMetadataItem(functionImport);
			if (functionImport.Parameters != null)
			{
				this.VisitFunctionImportParameters(functionImport.Parameters);
			}
			if (functionImport.ReturnParameters != null)
			{
				this.VisitFunctionImportReturnParameters(functionImport.ReturnParameters);
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00061433 File Offset: 0x0005F633
		protected internal virtual void VisitFunctionImportParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionImportParameter));
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00061448 File Offset: 0x0005F648
		protected internal virtual void VisitFunctionImportParameter(FunctionParameter parameter)
		{
			this.VisitMetadataItem(parameter);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00061451 File Offset: 0x0005F651
		protected internal virtual void VisitFunctionImportReturnParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionImportReturnParameter));
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00061466 File Offset: 0x0005F666
		protected internal virtual void VisitFunctionImportReturnParameter(FunctionParameter parameter)
		{
			this.VisitMetadataItem(parameter);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0006146F File Offset: 0x0005F66F
		protected virtual void VisitComplexTypes(IEnumerable<ComplexType> complexTypes)
		{
			EdmModelVisitor.VisitCollection<ComplexType>(complexTypes, new Action<ComplexType>(this.VisitComplexType));
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00061484 File Offset: 0x0005F684
		protected virtual void VisitComplexType(ComplexType item)
		{
			this.VisitMetadataItem(item);
			if (item.Properties.Count > 0)
			{
				EdmModelVisitor.VisitCollection<EdmProperty>(item.Properties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000614B3 File Offset: 0x0005F6B3
		protected virtual void VisitDeclaredProperties(ComplexType complexType, IEnumerable<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000614C8 File Offset: 0x0005F6C8
		protected virtual void VisitEntityTypes(IEnumerable<EntityType> entityTypes)
		{
			EdmModelVisitor.VisitCollection<EntityType>(entityTypes, new Action<EntityType>(this.VisitEdmEntityType));
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000614DD File Offset: 0x0005F6DD
		protected virtual void VisitEnumTypes(IEnumerable<EnumType> enumTypes)
		{
			EdmModelVisitor.VisitCollection<EnumType>(enumTypes, new Action<EnumType>(this.VisitEdmEnumType));
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000614F2 File Offset: 0x0005F6F2
		protected internal virtual void VisitFunctions(IEnumerable<EdmFunction> functions)
		{
			EdmModelVisitor.VisitCollection<EdmFunction>(functions, new Action<EdmFunction>(this.VisitEdmFunction));
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00061507 File Offset: 0x0005F707
		protected virtual void VisitFunctionParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionParameter));
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0006151C File Offset: 0x0005F71C
		protected internal virtual void VisitFunctionParameter(FunctionParameter functionParameter)
		{
			this.VisitMetadataItem(functionParameter);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00061525 File Offset: 0x0005F725
		protected internal virtual void VisitFunctionReturnParameters(IEnumerable<FunctionParameter> returnParameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(returnParameters, new Action<FunctionParameter>(this.VisitFunctionReturnParameter));
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0006153A File Offset: 0x0005F73A
		protected internal virtual void VisitFunctionReturnParameter(FunctionParameter returnParameter)
		{
			this.VisitMetadataItem(returnParameter);
			this.VisitEdmType(returnParameter.TypeUsage.EdmType);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00061554 File Offset: 0x0005F754
		protected internal virtual void VisitEdmType(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				this.VisitCollectionType((CollectionType)edmType);
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				this.VisitPrimitiveType((PrimitiveType)edmType);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return;
			}
			this.VisitRowType((RowType)edmType);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0006159D File Offset: 0x0005F79D
		protected internal virtual void VisitCollectionType(CollectionType collectionType)
		{
			this.VisitMetadataItem(collectionType);
			this.VisitEdmType(collectionType.TypeUsage.EdmType);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000615B7 File Offset: 0x0005F7B7
		protected internal virtual void VisitRowType(RowType rowType)
		{
			this.VisitMetadataItem(rowType);
			if (rowType.DeclaredProperties.Count > 0)
			{
				EdmModelVisitor.VisitCollection<EdmProperty>(rowType.DeclaredProperties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000615E6 File Offset: 0x0005F7E6
		protected internal virtual void VisitPrimitiveType(PrimitiveType primitiveType)
		{
			this.VisitMetadataItem(primitiveType);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000615EF File Offset: 0x0005F7EF
		protected virtual void VisitEdmEnumType(EnumType item)
		{
			this.VisitMetadataItem(item);
			if (item != null && item.Members.Count > 0)
			{
				this.VisitEnumMembers(item, item.Members);
			}
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00061616 File Offset: 0x0005F816
		protected virtual void VisitEnumMembers(EnumType enumType, IEnumerable<EnumMember> members)
		{
			EdmModelVisitor.VisitCollection<EnumMember>(members, new Action<EnumMember>(this.VisitEdmEnumTypeMember));
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0006162C File Offset: 0x0005F82C
		protected internal virtual void VisitEdmEntityType(EntityType item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.BaseType == null && item.KeyProperties.Count > 0)
				{
					this.VisitKeyProperties(item, item.KeyProperties);
				}
				if (item.DeclaredProperties.Count > 0)
				{
					this.VisitDeclaredProperties(item, item.DeclaredProperties);
				}
				if (item.DeclaredNavigationProperties.Count > 0)
				{
					this.VisitDeclaredNavigationProperties(item, item.DeclaredNavigationProperties);
				}
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0006169C File Offset: 0x0005F89C
		protected virtual void VisitKeyProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000616B1 File Offset: 0x0005F8B1
		protected virtual void VisitDeclaredProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000616C6 File Offset: 0x0005F8C6
		protected virtual void VisitDeclaredNavigationProperties(EntityType entityType, IEnumerable<NavigationProperty> navigationProperties)
		{
			EdmModelVisitor.VisitCollection<NavigationProperty>(navigationProperties, new Action<NavigationProperty>(this.VisitEdmNavigationProperty));
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000616DB File Offset: 0x0005F8DB
		protected virtual void VisitAssociationTypes(IEnumerable<AssociationType> associationTypes)
		{
			EdmModelVisitor.VisitCollection<AssociationType>(associationTypes, new Action<AssociationType>(this.VisitEdmAssociationType));
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000616F0 File Offset: 0x0005F8F0
		protected internal virtual void VisitEdmAssociationType(AssociationType item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.SourceEnd != null)
				{
					this.VisitEdmAssociationEnd(item.SourceEnd);
				}
				if (item.TargetEnd != null)
				{
					this.VisitEdmAssociationEnd(item.TargetEnd);
				}
			}
			if (item.Constraint != null)
			{
				this.VisitEdmAssociationConstraint(item.Constraint);
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00061743 File Offset: 0x0005F943
		protected internal virtual void VisitEdmProperty(EdmProperty item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0006174C File Offset: 0x0005F94C
		protected virtual void VisitEdmEnumTypeMember(EnumMember item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00061755 File Offset: 0x0005F955
		protected virtual void VisitEdmAssociationEnd(RelationshipEndMember item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0006175E File Offset: 0x0005F95E
		protected virtual void VisitEdmAssociationConstraint(ReferentialConstraint item)
		{
			if (item != null)
			{
				this.VisitMetadataItem(item);
				if (item.ToRole != null)
				{
					this.VisitEdmAssociationEnd(item.ToRole);
				}
				EdmModelVisitor.VisitCollection<EdmProperty>(item.ToProperties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00061796 File Offset: 0x0005F996
		protected virtual void VisitEdmNavigationProperty(NavigationProperty item)
		{
			this.VisitMetadataItem(item);
		}
	}
}
