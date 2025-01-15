using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AC RID: 1196
	internal static class EdmModelSyntacticValidationRules
	{
		// Token: 0x06003ACD RID: 15053 RVA: 0x000C25D4 File Offset: 0x000C07D4
		private static bool IsEdmTypeUsageValid(TypeUsage typeUsage)
		{
			HashSet<TypeUsage> hashSet = new HashSet<TypeUsage>();
			return EdmModelSyntacticValidationRules.IsEdmTypeUsageValid(typeUsage, hashSet);
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000C25EE File Offset: 0x000C07EE
		private static bool IsEdmTypeUsageValid(TypeUsage typeUsage, HashSet<TypeUsage> visitedValidTypeUsages)
		{
			if (visitedValidTypeUsages.Contains(typeUsage))
			{
				return false;
			}
			visitedValidTypeUsages.Add(typeUsage);
			return true;
		}

		// Token: 0x04001457 RID: 5207
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameMustNotBeEmptyOrWhiteSpace = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (string.IsNullOrWhiteSpace(item.Name))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x04001458 RID: 5208
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameIsTooLong = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (!string.IsNullOrWhiteSpace(item.Name) && item.Name.Length > 480 && !(item is RowType) && !(item is CollectionType))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x04001459 RID: 5209
		internal static readonly EdmModelValidationRule<INamedDataModelItem> EdmModel_NameIsNotAllowed = new EdmModelValidationRule<INamedDataModelItem>(delegate(EdmModelValidationContext context, INamedDataModelItem item)
		{
			if (string.IsNullOrWhiteSpace(item.Name) || item is RowType || item is CollectionType || (!context.IsCSpace && item is EdmProperty))
			{
				return;
			}
			if (item.Name.Contains(".") || (context.IsCSpace && !item.Name.IsValidUndottedName()))
			{
				context.AddError((MetadataItem)item, "Name", Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(item.Name));
			}
		});

		// Token: 0x0400145A RID: 5210
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_AssociationEndMustNotBeNull = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd == null || edmAssociationType.TargetEnd == null)
			{
				context.AddError(edmAssociationType, "End", Strings.EdmModel_Validator_Syntactic_EdmAssociationType_AssociationEndMustNotBeNull);
			}
		});

		// Token: 0x0400145B RID: 5211
		internal static readonly EdmModelValidationRule<ReferentialConstraint> EdmAssociationConstraint_DependentEndMustNotBeNull = new EdmModelValidationRule<ReferentialConstraint>(delegate(EdmModelValidationContext context, ReferentialConstraint edmAssociationConstraint)
		{
			if (edmAssociationConstraint.ToRole == null)
			{
				context.AddError(edmAssociationConstraint, "Dependent", Strings.EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull);
			}
		});

		// Token: 0x0400145C RID: 5212
		internal static readonly EdmModelValidationRule<ReferentialConstraint> EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty = new EdmModelValidationRule<ReferentialConstraint>(delegate(EdmModelValidationContext context, ReferentialConstraint edmAssociationConstraint)
		{
			if (edmAssociationConstraint.ToProperties == null || !edmAssociationConstraint.ToProperties.Any<EdmProperty>())
			{
				context.AddError(edmAssociationConstraint, "Dependent", Strings.EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty);
			}
		});

		// Token: 0x0400145D RID: 5213
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_AssociationMustNotBeNull = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association == null)
			{
				context.AddError(edmNavigationProperty, "Relationship", Strings.EdmModel_Validator_Syntactic_EdmNavigationProperty_AssociationMustNotBeNull);
			}
		});

		// Token: 0x0400145E RID: 5214
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_ResultEndMustNotBeNull = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.ToEndMember == null)
			{
				context.AddError(edmNavigationProperty, "ToRole", Strings.EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull);
			}
		});

		// Token: 0x0400145F RID: 5215
		internal static readonly EdmModelValidationRule<AssociationEndMember> EdmAssociationEnd_EntityTypeMustNotBeNull = new EdmModelValidationRule<AssociationEndMember>(delegate(EdmModelValidationContext context, AssociationEndMember edmAssociationEnd)
		{
			if (edmAssociationEnd.GetEntityType() == null)
			{
				context.AddError(edmAssociationEnd, "Type", Strings.EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull);
			}
		});

		// Token: 0x04001460 RID: 5216
		internal static readonly EdmModelValidationRule<EntitySet> EdmEntitySet_ElementTypeMustNotBeNull = new EdmModelValidationRule<EntitySet>(delegate(EdmModelValidationContext context, EntitySet edmEntitySet)
		{
			if (edmEntitySet.ElementType == null)
			{
				context.AddError(edmEntitySet, "ElementType", Strings.EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull);
			}
		});

		// Token: 0x04001461 RID: 5217
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_ElementTypeMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (edmAssociationSet.ElementType == null)
			{
				context.AddError(edmAssociationSet, "ElementType", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull);
			}
		});

		// Token: 0x04001462 RID: 5218
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_SourceSetMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (context.IsCSpace && edmAssociationSet.SourceSet == null)
			{
				context.AddError(edmAssociationSet, "FromRole", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull);
			}
		});

		// Token: 0x04001463 RID: 5219
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_TargetSetMustNotBeNull = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (context.IsCSpace && edmAssociationSet.TargetSet == null)
			{
				context.AddError(edmAssociationSet, "ToRole", Strings.EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull);
			}
		});

		// Token: 0x04001464 RID: 5220
		internal static readonly EdmModelValidationRule<TypeUsage> EdmTypeReference_TypeNotValid = new EdmModelValidationRule<TypeUsage>(delegate(EdmModelValidationContext context, TypeUsage edmTypeReference)
		{
			if (!EdmModelSyntacticValidationRules.IsEdmTypeUsageValid(edmTypeReference))
			{
				context.AddError(edmTypeReference, null, Strings.EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid);
			}
		});
	}
}
