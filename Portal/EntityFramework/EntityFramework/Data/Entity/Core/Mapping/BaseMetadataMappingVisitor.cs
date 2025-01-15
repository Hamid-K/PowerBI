using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000520 RID: 1312
	internal abstract class BaseMetadataMappingVisitor
	{
		// Token: 0x06004094 RID: 16532 RVA: 0x000D9C2F File Offset: 0x000D7E2F
		protected BaseMetadataMappingVisitor(bool sortSequence)
		{
			this._sortSequence = sortSequence;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000D9C40 File Offset: 0x000D7E40
		protected virtual void Visit(EntityContainerMapping entityContainerMapping)
		{
			this.Visit(entityContainerMapping.EdmEntityContainer);
			this.Visit(entityContainerMapping.StorageEntityContainer);
			foreach (EntitySetBaseMapping entitySetBaseMapping in this.GetSequence<EntitySetBaseMapping>(entityContainerMapping.EntitySetMaps, (EntitySetBaseMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(entitySetBaseMapping);
			}
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x000D9CCC File Offset: 0x000D7ECC
		protected virtual void Visit(EntitySetBase entitySetBase)
		{
			BuiltInTypeKind builtInTypeKind = entitySetBase.BuiltInTypeKind;
			if (builtInTypeKind != BuiltInTypeKind.AssociationSet)
			{
				if (builtInTypeKind == BuiltInTypeKind.EntitySet)
				{
					this.Visit((EntitySet)entitySetBase);
					return;
				}
			}
			else
			{
				this.Visit((AssociationSet)entitySetBase);
			}
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000D9D04 File Offset: 0x000D7F04
		protected virtual void Visit(EntitySetBaseMapping setMapping)
		{
			foreach (TypeMapping typeMapping in this.GetSequence<TypeMapping>(setMapping.TypeMappings, (TypeMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(typeMapping);
			}
			this.Visit(setMapping.EntityContainerMapping);
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x000D9D84 File Offset: 0x000D7F84
		protected virtual void Visit(EntityContainer entityContainer)
		{
			foreach (EntitySetBase entitySetBase in this.GetSequence<EntitySetBase>(entityContainer.BaseEntitySets, (EntitySetBase it) => it.Identity))
			{
				this.Visit(entitySetBase);
			}
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000D9DF8 File Offset: 0x000D7FF8
		protected virtual void Visit(EntitySet entitySet)
		{
			this.Visit(entitySet.ElementType);
			this.Visit(entitySet.EntityContainer);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000D9E14 File Offset: 0x000D8014
		protected virtual void Visit(AssociationSet associationSet)
		{
			this.Visit(associationSet.ElementType);
			this.Visit(associationSet.EntityContainer);
			foreach (AssociationSetEnd associationSetEnd in this.GetSequence<AssociationSetEnd>(associationSet.AssociationSetEnds, (AssociationSetEnd it) => it.Identity))
			{
				this.Visit(associationSetEnd);
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000D9EA0 File Offset: 0x000D80A0
		protected virtual void Visit(EntityType entityType)
		{
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(entityType.KeyMembers, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in this.GetSequence<EdmMember>(entityType.GetDeclaredOnlyMembers<EdmMember>(), (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember2);
			}
			foreach (NavigationProperty navigationProperty in this.GetSequence<NavigationProperty>(entityType.NavigationProperties, (NavigationProperty it) => it.Identity))
			{
				this.Visit(navigationProperty);
			}
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(entityType.Properties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000DA040 File Offset: 0x000D8240
		protected virtual void Visit(AssociationType associationType)
		{
			foreach (AssociationEndMember associationEndMember in this.GetSequence<AssociationEndMember>(associationType.AssociationEndMembers, (AssociationEndMember it) => it.Identity))
			{
				this.Visit(associationEndMember);
			}
			this.Visit(associationType.BaseType);
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(associationType.KeyMembers, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmMember edmMember2 in this.GetSequence<EdmMember>(associationType.GetDeclaredOnlyMembers<EdmMember>(), (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember2);
			}
			foreach (ReferentialConstraint referentialConstraint in this.GetSequence<ReferentialConstraint>(associationType.ReferentialConstraints, (ReferentialConstraint it) => it.Identity))
			{
				this.Visit(referentialConstraint);
			}
			foreach (RelationshipEndMember relationshipEndMember in this.GetSequence<RelationshipEndMember>(associationType.RelationshipEndMembers, (RelationshipEndMember it) => it.Identity))
			{
				this.Visit(relationshipEndMember);
			}
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000DA254 File Offset: 0x000D8454
		protected virtual void Visit(AssociationSetEnd associationSetEnd)
		{
			this.Visit(associationSetEnd.CorrespondingAssociationEndMember);
			this.Visit(associationSetEnd.EntitySet);
			this.Visit(associationSetEnd.ParentAssociationSet);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000DA27A File Offset: 0x000D847A
		protected virtual void Visit(EdmProperty edmProperty)
		{
			this.Visit(edmProperty.TypeUsage);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000DA288 File Offset: 0x000D8488
		protected virtual void Visit(NavigationProperty navigationProperty)
		{
			this.Visit(navigationProperty.FromEndMember);
			this.Visit(navigationProperty.RelationshipType);
			this.Visit(navigationProperty.ToEndMember);
			this.Visit(navigationProperty.TypeUsage);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000DA2BA File Offset: 0x000D84BA
		protected virtual void Visit(EdmMember edmMember)
		{
			this.Visit(edmMember.TypeUsage);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000DA2C8 File Offset: 0x000D84C8
		protected virtual void Visit(AssociationEndMember associationEndMember)
		{
			this.Visit(associationEndMember.TypeUsage);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000DA2D8 File Offset: 0x000D84D8
		protected virtual void Visit(ReferentialConstraint referentialConstraint)
		{
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(referentialConstraint.FromProperties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
			this.Visit(referentialConstraint.FromRole);
			foreach (EdmProperty edmProperty2 in this.GetSequence<EdmProperty>(referentialConstraint.ToProperties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty2);
			}
			this.Visit(referentialConstraint.ToRole);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000DA3C4 File Offset: 0x000D85C4
		protected virtual void Visit(RelationshipEndMember relationshipEndMember)
		{
			this.Visit(relationshipEndMember.TypeUsage);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000DA3D4 File Offset: 0x000D85D4
		protected virtual void Visit(TypeUsage typeUsage)
		{
			this.Visit(typeUsage.EdmType);
			foreach (Facet facet in this.GetSequence<Facet>(typeUsage.Facets, (Facet it) => it.Identity))
			{
				this.Visit(facet);
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x000DA454 File Offset: 0x000D8654
		protected virtual void Visit(RelationshipType relationshipType)
		{
			if (relationshipType == null)
			{
				return;
			}
			if (relationshipType.BuiltInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)relationshipType);
			}
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000DA470 File Offset: 0x000D8670
		protected virtual void Visit(EdmType edmType)
		{
			if (edmType == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind > BuiltInTypeKind.ComplexType)
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.EntityType:
					this.Visit((EntityType)edmType);
					return;
				case BuiltInTypeKind.EnumType:
					this.Visit((EnumType)edmType);
					break;
				case BuiltInTypeKind.EnumMember:
				case BuiltInTypeKind.Facet:
					break;
				case BuiltInTypeKind.EdmFunction:
					this.Visit((EdmFunction)edmType);
					return;
				default:
					if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
					{
						this.Visit((PrimitiveType)edmType);
						return;
					}
					if (builtInTypeKind != BuiltInTypeKind.RefType)
					{
						return;
					}
					this.Visit((RefType)edmType);
					return;
				}
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)edmType);
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				this.Visit((CollectionType)edmType);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.ComplexType)
			{
				return;
			}
			this.Visit((ComplexType)edmType);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x000DA528 File Offset: 0x000D8728
		protected virtual void Visit(Facet facet)
		{
			this.Visit(facet.FacetType);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000DA538 File Offset: 0x000D8738
		protected virtual void Visit(EdmFunction edmFunction)
		{
			this.Visit(edmFunction.BaseType);
			foreach (EntitySet entitySet in this.GetSequence<EntitySet>(edmFunction.EntitySets, (EntitySet it) => it.Identity))
			{
				if (entitySet != null)
				{
					this.Visit(entitySet);
				}
			}
			foreach (FunctionParameter functionParameter in this.GetSequence<FunctionParameter>(edmFunction.Parameters, (FunctionParameter it) => it.Identity))
			{
				this.Visit(functionParameter);
			}
			foreach (FunctionParameter functionParameter2 in this.GetSequence<FunctionParameter>(edmFunction.ReturnParameters, (FunctionParameter it) => it.Identity))
			{
				this.Visit(functionParameter2);
			}
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000DA680 File Offset: 0x000D8880
		protected virtual void Visit(PrimitiveType primitiveType)
		{
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000DA684 File Offset: 0x000D8884
		protected virtual void Visit(ComplexType complexType)
		{
			this.Visit(complexType.BaseType);
			foreach (EdmMember edmMember in this.GetSequence<EdmMember>(complexType.Members, (EdmMember it) => it.Identity))
			{
				this.Visit(edmMember);
			}
			foreach (EdmProperty edmProperty in this.GetSequence<EdmProperty>(complexType.Properties, (EdmProperty it) => it.Identity))
			{
				this.Visit(edmProperty);
			}
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000DA764 File Offset: 0x000D8964
		protected virtual void Visit(RefType refType)
		{
			this.Visit(refType.BaseType);
			this.Visit(refType.ElementType);
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x000DA780 File Offset: 0x000D8980
		protected virtual void Visit(EnumType enumType)
		{
			foreach (EnumMember enumMember in this.GetSequence<EnumMember>(enumType.Members, (EnumMember it) => it.Identity))
			{
				this.Visit(enumMember);
			}
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x000DA7F4 File Offset: 0x000D89F4
		protected virtual void Visit(EnumMember enumMember)
		{
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x000DA7F6 File Offset: 0x000D89F6
		protected virtual void Visit(CollectionType collectionType)
		{
			this.Visit(collectionType.BaseType);
			this.Visit(collectionType.TypeUsage);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x000DA810 File Offset: 0x000D8A10
		protected virtual void Visit(EntityTypeBase entityTypeBase)
		{
			if (entityTypeBase == null)
			{
				return;
			}
			BuiltInTypeKind builtInTypeKind = entityTypeBase.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.AssociationType)
			{
				this.Visit((AssociationType)entityTypeBase);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				return;
			}
			this.Visit((EntityType)entityTypeBase);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x000DA84B File Offset: 0x000D8A4B
		protected virtual void Visit(FunctionParameter functionParameter)
		{
			this.Visit(functionParameter.DeclaringFunction);
			this.Visit(functionParameter.TypeUsage);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x000DA865 File Offset: 0x000D8A65
		protected virtual void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x000DA868 File Offset: 0x000D8A68
		protected virtual void Visit(TypeMapping typeMapping)
		{
			foreach (EntityTypeBase entityTypeBase in this.GetSequence<EntityTypeBase>(typeMapping.IsOfTypes, (EntityTypeBase it) => it.Identity))
			{
				this.Visit(entityTypeBase);
			}
			foreach (MappingFragment mappingFragment in this.GetSequence<MappingFragment>(typeMapping.MappingFragments, (MappingFragment it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(mappingFragment);
			}
			this.Visit(typeMapping.SetMapping);
			foreach (EntityTypeBase entityTypeBase2 in this.GetSequence<EntityTypeBase>(typeMapping.Types, (EntityTypeBase it) => it.Identity))
			{
				this.Visit(entityTypeBase2);
			}
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x000DA9AC File Offset: 0x000D8BAC
		protected virtual void Visit(MappingFragment mappingFragment)
		{
			foreach (PropertyMapping propertyMapping in this.GetSequence<PropertyMapping>(mappingFragment.AllProperties, (PropertyMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(propertyMapping);
			}
			this.Visit(mappingFragment.TableSet);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x000DAA2C File Offset: 0x000D8C2C
		protected virtual void Visit(PropertyMapping propertyMapping)
		{
			if (propertyMapping.GetType() == typeof(ComplexPropertyMapping))
			{
				this.Visit((ComplexPropertyMapping)propertyMapping);
				return;
			}
			if (propertyMapping.GetType() == typeof(ConditionPropertyMapping))
			{
				this.Visit((ConditionPropertyMapping)propertyMapping);
				return;
			}
			if (propertyMapping.GetType() == typeof(ScalarPropertyMapping))
			{
				this.Visit((ScalarPropertyMapping)propertyMapping);
			}
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000DAAA4 File Offset: 0x000D8CA4
		protected virtual void Visit(ComplexPropertyMapping complexPropertyMapping)
		{
			this.Visit(complexPropertyMapping.Property);
			foreach (ComplexTypeMapping complexTypeMapping in this.GetSequence<ComplexTypeMapping>(complexPropertyMapping.TypeMappings, (ComplexTypeMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(complexTypeMapping);
			}
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000DAB24 File Offset: 0x000D8D24
		protected virtual void Visit(ConditionPropertyMapping conditionPropertyMapping)
		{
			this.Visit(conditionPropertyMapping.Column);
			this.Visit(conditionPropertyMapping.Property);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x000DAB3E File Offset: 0x000D8D3E
		protected virtual void Visit(ScalarPropertyMapping scalarPropertyMapping)
		{
			this.Visit(scalarPropertyMapping.Column);
			this.Visit(scalarPropertyMapping.Property);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000DAB58 File Offset: 0x000D8D58
		protected virtual void Visit(ComplexTypeMapping complexTypeMapping)
		{
			foreach (PropertyMapping propertyMapping in this.GetSequence<PropertyMapping>(complexTypeMapping.AllProperties, (PropertyMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)))
			{
				this.Visit(propertyMapping);
			}
			foreach (ComplexType complexType in this.GetSequence<ComplexType>(complexTypeMapping.IsOfTypes, (ComplexType it) => it.Identity))
			{
				this.Visit(complexType);
			}
			foreach (ComplexType complexType2 in this.GetSequence<ComplexType>(complexTypeMapping.Types, (ComplexType it) => it.Identity))
			{
				this.Visit(complexType2);
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000DAC90 File Offset: 0x000D8E90
		protected IEnumerable<T> GetSequence<T>(IEnumerable<T> sequence, Func<T, string> keySelector)
		{
			if (!this._sortSequence)
			{
				return sequence;
			}
			return sequence.OrderBy(keySelector, StringComparer.Ordinal);
		}

		// Token: 0x04001677 RID: 5751
		private readonly bool _sortSequence;

		// Token: 0x02000B2C RID: 2860
		internal static class IdentityHelper
		{
			// Token: 0x060064E5 RID: 25829 RVA: 0x0015BD17 File Offset: 0x00159F17
			public static string GetIdentity(EntitySetBaseMapping mapping)
			{
				return mapping.Set.Identity;
			}

			// Token: 0x060064E6 RID: 25830 RVA: 0x0015BD24 File Offset: 0x00159F24
			public static string GetIdentity(TypeMapping mapping)
			{
				EntityTypeMapping entityTypeMapping = mapping as EntityTypeMapping;
				if (entityTypeMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(entityTypeMapping);
				}
				return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity((AssociationTypeMapping)mapping);
			}

			// Token: 0x060064E7 RID: 25831 RVA: 0x0015BD50 File Offset: 0x00159F50
			public static string GetIdentity(EntityTypeMapping mapping)
			{
				IOrderedEnumerable<string> orderedEnumerable = mapping.Types.Select((EntityTypeBase it) => it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> orderedEnumerable2 = mapping.IsOfTypes.Select((EntityTypeBase it) => it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				return string.Join(",", orderedEnumerable.Concat(orderedEnumerable2));
			}

			// Token: 0x060064E8 RID: 25832 RVA: 0x0015BE16 File Offset: 0x0015A016
			public static string GetIdentity(AssociationTypeMapping mapping)
			{
				return mapping.AssociationType.Identity;
			}

			// Token: 0x060064E9 RID: 25833 RVA: 0x0015BE24 File Offset: 0x0015A024
			public static string GetIdentity(ComplexTypeMapping mapping)
			{
				IOrderedEnumerable<string> orderedEnumerable = mapping.AllProperties.Select((PropertyMapping it) => BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(it)).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> orderedEnumerable2 = mapping.Types.Select((ComplexType it) => it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				IOrderedEnumerable<string> orderedEnumerable3 = mapping.IsOfTypes.Select((ComplexType it) => it.Identity).OrderBy((string it) => it, StringComparer.Ordinal);
				return string.Join(",", orderedEnumerable.Concat(orderedEnumerable2).Concat(orderedEnumerable3));
			}

			// Token: 0x060064EA RID: 25834 RVA: 0x0015BF44 File Offset: 0x0015A144
			public static string GetIdentity(MappingFragment mapping)
			{
				return mapping.TableSet.Identity;
			}

			// Token: 0x060064EB RID: 25835 RVA: 0x0015BF54 File Offset: 0x0015A154
			public static string GetIdentity(PropertyMapping mapping)
			{
				ScalarPropertyMapping scalarPropertyMapping = mapping as ScalarPropertyMapping;
				if (scalarPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(scalarPropertyMapping);
				}
				ComplexPropertyMapping complexPropertyMapping = mapping as ComplexPropertyMapping;
				if (complexPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(complexPropertyMapping);
				}
				EndPropertyMapping endPropertyMapping = mapping as EndPropertyMapping;
				if (endPropertyMapping != null)
				{
					return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity(endPropertyMapping);
				}
				return BaseMetadataMappingVisitor.IdentityHelper.GetIdentity((ConditionPropertyMapping)mapping);
			}

			// Token: 0x060064EC RID: 25836 RVA: 0x0015BFA0 File Offset: 0x0015A1A0
			public static string GetIdentity(ScalarPropertyMapping mapping)
			{
				return string.Concat(new string[]
				{
					"ScalarProperty(Identity=",
					mapping.Property.Identity,
					",ColumnIdentity=",
					mapping.Column.Identity,
					")"
				});
			}

			// Token: 0x060064ED RID: 25837 RVA: 0x0015BFEC File Offset: 0x0015A1EC
			public static string GetIdentity(ComplexPropertyMapping mapping)
			{
				return "ComplexProperty(Identity=" + mapping.Property.Identity + ")";
			}

			// Token: 0x060064EE RID: 25838 RVA: 0x0015C008 File Offset: 0x0015A208
			public static string GetIdentity(ConditionPropertyMapping mapping)
			{
				if (mapping.Property == null)
				{
					return "ConditionProperty(ColumnIdentity=" + mapping.Column.Identity + ")";
				}
				return "ConditionProperty(Identity=" + mapping.Property.Identity + ")";
			}

			// Token: 0x060064EF RID: 25839 RVA: 0x0015C047 File Offset: 0x0015A247
			public static string GetIdentity(EndPropertyMapping mapping)
			{
				return "EndProperty(Identity=" + mapping.AssociationEnd.Identity + ")";
			}
		}
	}
}
