using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000512 RID: 1298
	internal sealed class ObjectItemAttributeAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x000D4560 File Offset: 0x000D2760
		private new MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000D456D File Offset: 0x000D276D
		internal ObjectItemAttributeAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData)
			: base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000D4594 File Offset: 0x000D2794
		internal override void OnLevel1SessionProcessing()
		{
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000D45E4 File Offset: 0x000D27E4
		internal override void OnLevel2SessionProcessing()
		{
			foreach (Action action in this._unresolvedNavigationProperties)
			{
				action();
			}
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000D4634 File Offset: 0x000D2834
		internal override void Load()
		{
			base.Load();
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000D463C File Offset: 0x000D283C
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000D465C File Offset: 0x000D285C
		private bool TryGetLoadedType(Type clrType, out EdmType edmType)
		{
			if (base.SessionData.TypesInLoading.TryGetValue(clrType.FullName, out edmType) || this.TryGetCachedEdmType(clrType, out edmType))
			{
				if (edmType.ClrType != clrType)
				{
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NewTypeConflictsWithExistingType(clrType.AssemblyQualifiedName, edmType.ClrType.AssemblyQualifiedName)));
					edmType = null;
					return false;
				}
				return true;
			}
			else
			{
				if (!clrType.IsGenericType())
				{
					edmType = null;
					return false;
				}
				EdmType edmType2;
				if (!this.TryGetLoadedType(clrType.GetGenericArguments()[0], out edmType2))
				{
					return false;
				}
				if (typeof(IEnumerable).IsAssignableFrom(clrType))
				{
					EntityType entityType = edmType2 as EntityType;
					if (entityType == null)
					{
						return false;
					}
					edmType = entityType.GetCollectionType();
				}
				else
				{
					edmType = edmType2;
				}
				return true;
			}
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000D471C File Offset: 0x000D291C
		private bool TryGetCachedEdmType(Type clrType, out EdmType edmType)
		{
			ImmutableAssemblyCacheEntry immutableAssemblyCacheEntry;
			if (base.SessionData.LockedAssemblyCache.TryGetValue(clrType.Assembly(), out immutableAssemblyCacheEntry))
			{
				return immutableAssemblyCacheEntry.TryGetEdmType(clrType.FullName, out edmType);
			}
			edmType = null;
			return false;
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000D4758 File Offset: 0x000D2958
		protected override void LoadTypesFromAssembly()
		{
			this.LoadRelationshipTypes();
			foreach (Type type in base.SourceAssembly.GetAccessibleTypes())
			{
				if (type.GetCustomAttributes(false).Any<EdmTypeAttribute>())
				{
					if (type.IsGenericType())
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.GenericTypeNotSupported(type.FullName)));
					}
					else
					{
						this.LoadType(type);
					}
				}
			}
			if (this._referenceResolutions.Count != 0)
			{
				base.SessionData.RegisterForLevel1PostSessionProcessing(this);
			}
			if (this._unresolvedNavigationProperties.Count != 0)
			{
				base.SessionData.RegisterForLevel2PostSessionProcessing(this);
			}
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000D481C File Offset: 0x000D2A1C
		private void LoadRelationshipTypes()
		{
			foreach (EdmRelationshipAttribute edmRelationshipAttribute in base.SourceAssembly.GetCustomAttributes<EdmRelationshipAttribute>())
			{
				if (!this.TryFindNullParametersInRelationshipAttribute(edmRelationshipAttribute))
				{
					bool flag = false;
					if (edmRelationshipAttribute.Role1Name == edmRelationshipAttribute.Role2Name)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.SameRoleNameOnRelationshipAttribute(edmRelationshipAttribute.RelationshipName, edmRelationshipAttribute.Role2Name)));
						flag = true;
					}
					if (!flag)
					{
						AssociationType associationType = new AssociationType(edmRelationshipAttribute.RelationshipName, edmRelationshipAttribute.RelationshipNamespaceName, edmRelationshipAttribute.IsForeignKey, DataSpace.OSpace);
						base.SessionData.TypesInLoading.Add(associationType.FullName, associationType);
						this.TrackClosure(edmRelationshipAttribute.Role1Type);
						this.TrackClosure(edmRelationshipAttribute.Role2Type);
						string r1Name = edmRelationshipAttribute.Role1Name;
						Type r1Type = edmRelationshipAttribute.Role1Type;
						RelationshipMultiplicity r1Multiplicity = edmRelationshipAttribute.Role1Multiplicity;
						this.AddTypeResolver(delegate
						{
							this.ResolveAssociationEnd(associationType, r1Name, r1Type, r1Multiplicity);
						});
						string r2Name = edmRelationshipAttribute.Role2Name;
						Type r2Type = edmRelationshipAttribute.Role2Type;
						RelationshipMultiplicity r2Multiplicity = edmRelationshipAttribute.Role2Multiplicity;
						this.AddTypeResolver(delegate
						{
							this.ResolveAssociationEnd(associationType, r2Name, r2Type, r2Multiplicity);
						});
						this.CacheEntry.TypesInAssembly.Add(associationType);
					}
				}
			}
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000D49B0 File Offset: 0x000D2BB0
		private void ResolveAssociationEnd(AssociationType associationType, string roleName, Type clrType, RelationshipMultiplicity multiplicity)
		{
			EntityType entityType;
			if (!this.TryGetRelationshipEndEntityType(clrType, out entityType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RoleTypeInEdmRelationshipAttributeIsInvalidType(associationType.Name, roleName, clrType)));
				return;
			}
			associationType.AddKeyMember(new AssociationEndMember(roleName, entityType.GetReferenceType(), multiplicity));
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000D4A00 File Offset: 0x000D2C00
		private void LoadType(Type clrType)
		{
			EdmType edmType = null;
			IEnumerable<EdmTypeAttribute> customAttributes = clrType.GetCustomAttributes(false);
			if (!customAttributes.Any<EdmTypeAttribute>())
			{
				return;
			}
			if (clrType.IsNested)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NestedClassNotSupported(clrType.FullName, clrType.Assembly().FullName)));
				return;
			}
			EdmTypeAttribute edmTypeAttribute = customAttributes.First<EdmTypeAttribute>();
			string text = (string.IsNullOrEmpty(edmTypeAttribute.Name) ? clrType.Name : edmTypeAttribute.Name);
			if (string.IsNullOrEmpty(edmTypeAttribute.NamespaceName) && clrType.Namespace == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_TypeHasNoNamespace));
				return;
			}
			string text2 = (string.IsNullOrEmpty(edmTypeAttribute.NamespaceName) ? clrType.Namespace : edmTypeAttribute.NamespaceName);
			if (edmTypeAttribute.GetType() == typeof(EdmEntityTypeAttribute))
			{
				edmType = new ClrEntityType(clrType, text2, text);
			}
			else if (edmTypeAttribute.GetType() == typeof(EdmComplexTypeAttribute))
			{
				edmType = new ClrComplexType(clrType, text2, text);
			}
			else
			{
				PrimitiveType primitiveType;
				if (!ClrProviderManifest.Instance.TryGetPrimitiveType(clrType.GetEnumUnderlyingType(), out primitiveType))
				{
					base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_UnsupportedEnumUnderlyingType(clrType.GetEnumUnderlyingType().FullName)));
					return;
				}
				edmType = new ClrEnumType(clrType, text2, text);
			}
			this.CacheEntry.TypesInAssembly.Add(edmType);
			base.SessionData.TypesInLoading.Add(clrType.FullName, edmType);
			if (Helper.IsStructuralType(edmType))
			{
				if (Helper.IsEntityType(edmType))
				{
					this.TrackClosure(clrType.BaseType());
					this.AddTypeResolver(delegate
					{
						edmType.BaseType = this.ResolveBaseType(clrType.BaseType());
					});
				}
				this.LoadPropertiesFromType((StructuralType)edmType);
			}
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000D4C3F File Offset: 0x000D2E3F
		private void AddTypeResolver(Action resolver)
		{
			this._referenceResolutions.Add(resolver);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000D4C50 File Offset: 0x000D2E50
		private EdmType ResolveBaseType(Type type)
		{
			EdmType edmType;
			if (type.GetCustomAttributes(false).Any<EdmEntityTypeAttribute>() && this.TryGetLoadedType(type, out edmType))
			{
				return edmType;
			}
			return null;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000D4C7C File Offset: 0x000D2E7C
		private bool TryFindNullParametersInRelationshipAttribute(EdmRelationshipAttribute roleAttribute)
		{
			if (roleAttribute.RelationshipName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullRelationshipNameforEdmRelationshipAttribute(base.SourceAssembly.FullName)));
				return true;
			}
			bool flag = false;
			if (roleAttribute.RelationshipNamespaceName == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("RelationshipNamespaceName", roleAttribute.RelationshipName)));
				flag = true;
			}
			if (roleAttribute.Role1Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Name", roleAttribute.RelationshipName)));
				flag = true;
			}
			if (roleAttribute.Role1Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role1Type", roleAttribute.RelationshipName)));
				flag = true;
			}
			if (roleAttribute.Role2Name == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Name", roleAttribute.RelationshipName)));
				flag = true;
			}
			if (roleAttribute.Role2Type == null)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NullParameterForEdmRelationshipAttribute("Role2Type", roleAttribute.RelationshipName)));
				flag = true;
			}
			return flag;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000D4DB4 File Offset: 0x000D2FB4
		private bool TryGetRelationshipEndEntityType(Type type, out EntityType entityType)
		{
			if (type == null)
			{
				entityType = null;
				return false;
			}
			EdmType edmType;
			if (!this.TryGetLoadedType(type, out edmType) || !Helper.IsEntityType(edmType))
			{
				entityType = null;
				return false;
			}
			entityType = (EntityType)edmType;
			return true;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000D4DF0 File Offset: 0x000D2FF0
		private void LoadPropertiesFromType(StructuralType structuralType)
		{
			foreach (PropertyInfo propertyInfo in from p in structuralType.ClrType.GetDeclaredProperties()
				where !p.IsStatic()
				select p)
			{
				EdmMember edmMember = null;
				bool flag = false;
				if (propertyInfo.GetCustomAttributes(false).Any<EdmRelationshipNavigationPropertyAttribute>())
				{
					PropertyInfo pi = propertyInfo;
					this._unresolvedNavigationProperties.Add(delegate
					{
						this.ResolveNavigationProperty(structuralType, pi);
					});
				}
				else if (propertyInfo.GetCustomAttributes(false).Any<EdmScalarPropertyAttribute>())
				{
					if ((Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType).IsEnum())
					{
						this.TrackClosure(propertyInfo.PropertyType);
						PropertyInfo local2 = propertyInfo;
						this.AddTypeResolver(delegate
						{
							this.ResolveEnumTypeProperty(structuralType, local2);
						});
					}
					else
					{
						edmMember = this.LoadScalarProperty(structuralType.ClrType, propertyInfo, out flag);
					}
				}
				else if (propertyInfo.GetCustomAttributes(false).Any<EdmComplexPropertyAttribute>())
				{
					this.TrackClosure(propertyInfo.PropertyType);
					PropertyInfo local = propertyInfo;
					this.AddTypeResolver(delegate
					{
						this.ResolveComplexTypeProperty(structuralType, local);
					});
				}
				if (edmMember != null)
				{
					structuralType.AddMember(edmMember);
					if (Helper.IsEntityType(structuralType) && flag)
					{
						((EntityType)structuralType).AddKeyMember(edmMember);
					}
				}
			}
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000D4FC0 File Offset: 0x000D31C0
		internal void ResolveNavigationProperty(StructuralType declaringType, PropertyInfo propertyInfo)
		{
			IEnumerable<EdmRelationshipNavigationPropertyAttribute> customAttributes = propertyInfo.GetCustomAttributes(false);
			EdmType edmType;
			if (!this.TryGetLoadedType(propertyInfo.PropertyType, out edmType) || (edmType.BuiltInTypeKind != BuiltInTypeKind.EntityType && edmType.BuiltInTypeKind != BuiltInTypeKind.CollectionType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_InvalidNavPropReturnType(propertyInfo.Name, propertyInfo.DeclaringType.FullName, propertyInfo.PropertyType.FullName)));
				return;
			}
			EdmRelationshipNavigationPropertyAttribute edmRelationshipNavigationPropertyAttribute = customAttributes.First<EdmRelationshipNavigationPropertyAttribute>();
			EdmMember edmMember = null;
			EdmType edmType2;
			if (base.SessionData.TypesInLoading.TryGetValue(edmRelationshipNavigationPropertyAttribute.RelationshipNamespaceName + "." + edmRelationshipNavigationPropertyAttribute.RelationshipName, out edmType2) && Helper.IsAssociationType(edmType2))
			{
				AssociationType associationType = (AssociationType)edmType2;
				if (associationType != null)
				{
					NavigationProperty navigationProperty = new NavigationProperty(propertyInfo.Name, TypeUsage.Create(edmType));
					navigationProperty.RelationshipType = associationType;
					edmMember = navigationProperty;
					if (associationType.Members[0].Name == edmRelationshipNavigationPropertyAttribute.TargetRoleName)
					{
						navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[0];
						navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[1];
					}
					else if (associationType.Members[1].Name == edmRelationshipNavigationPropertyAttribute.TargetRoleName)
					{
						navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[1];
						navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[0];
					}
					else
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.TargetRoleNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.TargetRoleName, edmRelationshipNavigationPropertyAttribute.RelationshipName)));
						edmMember = null;
					}
					if (edmMember != null && ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType != declaringType.ClrType)
					{
						base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.NavigationPropertyRelationshipEndTypeMismatch(declaringType.FullName, navigationProperty.Name, associationType.FullName, navigationProperty.FromEndMember.Name, ((RefType)navigationProperty.FromEndMember.TypeUsage.EdmType).ElementType.ClrType)));
						edmMember = null;
					}
				}
			}
			else
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.RelationshipNameInNavigationPropertyNotValid(propertyInfo.Name, propertyInfo.DeclaringType.FullName, edmRelationshipNavigationPropertyAttribute.RelationshipName)));
			}
			if (edmMember != null)
			{
				declaringType.AddMember(edmMember);
			}
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000D5254 File Offset: 0x000D3454
		private EdmMember LoadScalarProperty(Type clrType, PropertyInfo property, out bool isEntityKeyProperty)
		{
			EdmMember edmMember = null;
			isEntityKeyProperty = false;
			PrimitiveType primitiveType;
			if (!ObjectItemAssemblyLoader.TryGetPrimitiveType(property.PropertyType, out primitiveType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(property.Name, property.DeclaringType.FullName, property.PropertyType.FullName)));
			}
			else
			{
				IEnumerable<EdmScalarPropertyAttribute> customAttributes = property.GetCustomAttributes(false);
				isEntityKeyProperty = customAttributes.First<EdmScalarPropertyAttribute>().EntityKeyProperty;
				bool isNullable = customAttributes.First<EdmScalarPropertyAttribute>().IsNullable;
				edmMember = new EdmProperty(property.Name, TypeUsage.Create(primitiveType, new FacetValues
				{
					Nullable = new bool?(isNullable)
				}), property, clrType);
			}
			return edmMember;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000D52FC File Offset: 0x000D34FC
		private void ResolveEnumTypeProperty(StructuralType declaringType, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || !Helper.IsEnumType(edmType))
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ScalarPropertyNotPrimitive(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName)));
				return;
			}
			EdmScalarPropertyAttribute edmScalarPropertyAttribute = clrProperty.GetCustomAttributes(false).Single<EdmScalarPropertyAttribute>();
			EdmProperty edmProperty = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(edmScalarPropertyAttribute.IsNullable)
			}), clrProperty, declaringType.ClrType);
			declaringType.AddMember(edmProperty);
			if (declaringType.BuiltInTypeKind == BuiltInTypeKind.EntityType && edmScalarPropertyAttribute.EntityKeyProperty)
			{
				((EntityType)declaringType).AddKeyMember(edmProperty);
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000D53C0 File Offset: 0x000D35C0
		private void ResolveComplexTypeProperty(StructuralType type, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.TryGetLoadedType(clrProperty.PropertyType, out edmType) || edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType)
			{
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_ComplexPropertyNotComplex(clrProperty.Name, clrProperty.DeclaringType.FullName, clrProperty.PropertyType.FullName)));
				return;
			}
			EdmProperty edmProperty = new EdmProperty(clrProperty.Name, TypeUsage.Create(edmType, new FacetValues
			{
				Nullable = new bool?(false)
			}), clrProperty, type.ClrType);
			type.AddMember(edmProperty);
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000D5454 File Offset: 0x000D3654
		private void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly() && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly()) && ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(type.Assembly()) && (!type.IsGenericType() || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
			{
				this.CacheEntry.ClosureAssemblies.Add(type.Assembly());
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					this.TrackClosure(type2);
				}
			}
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000D5515 File Offset: 0x000D3715
		internal static bool IsSchemaAttributePresent(Assembly assembly)
		{
			return assembly.GetCustomAttributes<EdmSchemaAttribute>().Any<EdmSchemaAttribute>();
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000D5522 File Offset: 0x000D3722
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
			}
			return new ObjectItemAttributeAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x0400164C RID: 5708
		private readonly List<Action> _unresolvedNavigationProperties = new List<Action>();

		// Token: 0x0400164D RID: 5709
		private readonly List<Action> _referenceResolutions = new List<Action>();
	}
}
