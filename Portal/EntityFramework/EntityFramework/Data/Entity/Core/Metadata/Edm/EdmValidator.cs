using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B5 RID: 1205
	internal class EdmValidator
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000C3F7E File Offset: 0x000C217E
		// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000C3F86 File Offset: 0x000C2186
		internal bool SkipReadOnlyItems { get; set; }

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C3F90 File Offset: 0x000C2190
		public void Validate<T>(IEnumerable<T> items, List<EdmItemError> ospaceErrors) where T : EdmType
		{
			Check.NotNull<IEnumerable<T>>(items, "items");
			Check.NotNull<IEnumerable<T>>(items, "items");
			HashSet<MetadataItem> hashSet = new HashSet<MetadataItem>();
			foreach (T t in items)
			{
				MetadataItem metadataItem = t;
				this.InternalValidate(metadataItem, ospaceErrors, hashSet);
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000C4000 File Offset: 0x000C2200
		protected virtual void OnValidationError(ValidationErrorEventArgs e)
		{
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000C4004 File Offset: 0x000C2204
		private void AddError(List<EdmItemError> errors, EdmItemError newError)
		{
			ValidationErrorEventArgs validationErrorEventArgs = new ValidationErrorEventArgs(newError);
			this.OnValidationError(validationErrorEventArgs);
			errors.Add(validationErrorEventArgs.ValidationError);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000C402B File Offset: 0x000C222B
		protected virtual IEnumerable<EdmItemError> CustomValidate(MetadataItem item)
		{
			return null;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000C4030 File Offset: 0x000C2230
		private void InternalValidate(MetadataItem item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if ((item.IsReadOnly && this.SkipReadOnlyItems) || validatedItems.Contains(item))
			{
				return;
			}
			validatedItems.Add(item);
			if (string.IsNullOrEmpty(item.Identity))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_EmptyIdentity));
			}
			switch (item.BuiltInTypeKind)
			{
			case BuiltInTypeKind.CollectionType:
				this.ValidateCollectionType((CollectionType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.ComplexType:
				this.ValidateComplexType((ComplexType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.EntityType:
				this.ValidateEntityType((EntityType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.Facet:
				this.ValidateFacet((Facet)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.MetadataProperty:
				this.ValidateMetadataProperty((MetadataProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.NavigationProperty:
				this.ValidateNavigationProperty((NavigationProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.PrimitiveType:
				this.ValidatePrimitiveType((PrimitiveType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.EdmProperty:
				this.ValidateEdmProperty((EdmProperty)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.RefType:
				this.ValidateRefType((RefType)item, errors, validatedItems);
				break;
			case BuiltInTypeKind.TypeUsage:
				this.ValidateTypeUsage((TypeUsage)item, errors, validatedItems);
				break;
			}
			IEnumerable<EdmItemError> enumerable = this.CustomValidate(item);
			if (enumerable != null)
			{
				errors.AddRange(enumerable);
			}
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000C41D0 File Offset: 0x000C23D0
		private void ValidateCollectionType(CollectionType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionTypesCannotHaveBaseType));
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_CollectionHasNoTypeUsage));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000C4227 File Offset: 0x000C2427
		private void ValidateComplexType(ComplexType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000C4234 File Offset: 0x000C2434
		private void ValidateEdmType(EdmType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoName));
			}
			if (item.NamespaceName == null || (item.DataSpace != DataSpace.OSpace && string.Empty == item.NamespaceName))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeHasNoNamespace));
			}
			if (item.BaseType != null)
			{
				this.InternalValidate(item.BaseType, errors, validatedItems);
			}
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000C42B4 File Offset: 0x000C24B4
		private void ValidateEntityType(EntityType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.BaseType == null)
			{
				if (item.KeyMembers.Count < 1)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_NoKeyMembers(item.FullName)));
				}
				else
				{
					foreach (EdmMember edmMember in item.KeyMembers)
					{
						EdmProperty edmProperty = (EdmProperty)edmMember;
						if (edmProperty.Nullable)
						{
							this.AddError(errors, new EdmItemError(Strings.Validator_NullableEntityKeyProperty(edmProperty.Name, item.FullName)));
						}
					}
				}
			}
			this.ValidateStructuralType(item, errors, validatedItems);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000C4368 File Offset: 0x000C2568
		private void ValidateFacet(Facet item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetHasNoName));
			}
			if (item.FacetType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_FacetTypeIsNull));
				return;
			}
			this.InternalValidate(item.FacetType, errors, validatedItems);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000C43C4 File Offset: 0x000C25C4
		private void ValidateItem(MetadataItem item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.RawMetadataProperties != null)
			{
				foreach (MetadataProperty metadataProperty in item.MetadataProperties)
				{
					this.InternalValidate(metadataProperty, errors, validatedItems);
				}
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000C4424 File Offset: 0x000C2624
		private void ValidateEdmMember(EdmMember item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (string.IsNullOrEmpty(item.Name))
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNoName));
			}
			if (item.DeclaringType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullDeclaringType));
			}
			else
			{
				this.InternalValidate(item.DeclaringType, errors, validatedItems);
			}
			if (item.TypeUsage == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_MemberHasNullTypeUsage));
				return;
			}
			this.InternalValidate(item.TypeUsage, errors, validatedItems);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000C44AC File Offset: 0x000C26AC
		private void ValidateMetadataProperty(MetadataProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			if (item.PropertyKind == PropertyKind.Extended)
			{
				this.ValidateItem(item, errors, validatedItems);
				if (string.IsNullOrEmpty(item.Name))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_MetadataPropertyHasNoName));
				}
				if (item.TypeUsage == null)
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_ItemAttributeHasNullTypeUsage));
					return;
				}
				this.InternalValidate(item.TypeUsage, errors, validatedItems);
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000C4511 File Offset: 0x000C2711
		private void ValidateNavigationProperty(NavigationProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000C451C File Offset: 0x000C271C
		private void ValidatePrimitiveType(PrimitiveType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateSimpleType(item, errors, validatedItems);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000C4527 File Offset: 0x000C2727
		private void ValidateEdmProperty(EdmProperty item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmMember(item, errors, validatedItems);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000C4534 File Offset: 0x000C2734
		private void ValidateRefType(RefType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			if (item.BaseType != null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypesCannotHaveBaseType));
			}
			if (item.ElementType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_RefTypeHasNullEntityType));
				return;
			}
			this.InternalValidate(item.ElementType, errors, validatedItems);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000C458B File Offset: 0x000C278B
		private void ValidateSimpleType(SimpleType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000C4598 File Offset: 0x000C2798
		private void ValidateStructuralType(StructuralType item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateEdmType(item, errors, validatedItems);
			Dictionary<string, EdmMember> dictionary = new Dictionary<string, EdmMember>();
			foreach (EdmMember edmMember in item.Members)
			{
				EdmMember edmMember2 = null;
				if (dictionary.TryGetValue(edmMember.Name, out edmMember2))
				{
					this.AddError(errors, new EdmItemError(Strings.Validator_BaseTypeHasMemberOfSameName));
				}
				else
				{
					dictionary.Add(edmMember.Name, edmMember);
				}
				this.InternalValidate(edmMember, errors, validatedItems);
			}
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000C4630 File Offset: 0x000C2830
		private void ValidateTypeUsage(TypeUsage item, List<EdmItemError> errors, HashSet<MetadataItem> validatedItems)
		{
			this.ValidateItem(item, errors, validatedItems);
			if (item.EdmType == null)
			{
				this.AddError(errors, new EdmItemError(Strings.Validator_TypeUsageHasNullEdmType));
			}
			else
			{
				this.InternalValidate(item.EdmType, errors, validatedItems);
			}
			foreach (Facet facet in item.Facets)
			{
				this.InternalValidate(facet, errors, validatedItems);
			}
		}
	}
}
