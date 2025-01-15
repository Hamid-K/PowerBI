using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000517 RID: 1303
	internal abstract class OSpaceTypeFactory
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004018 RID: 16408
		public abstract List<Action> ReferenceResolutions { get; }

		// Token: 0x06004019 RID: 16409
		public abstract void LogLoadMessage(string message, EdmType relatedType);

		// Token: 0x0600401A RID: 16410
		public abstract void LogError(string errorMessage, EdmType relatedType);

		// Token: 0x0600401B RID: 16411
		public abstract void TrackClosure(Type type);

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600401C RID: 16412
		public abstract Dictionary<EdmType, EdmType> CspaceToOspace { get; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600401D RID: 16413
		public abstract Dictionary<string, EdmType> LoadedTypes { get; }

		// Token: 0x0600401E RID: 16414
		public abstract void AddToTypesInAssembly(EdmType type);

		// Token: 0x0600401F RID: 16415 RVA: 0x000D5DE8 File Offset: 0x000D3FE8
		public virtual EdmType TryCreateType(Type type, EdmType cspaceType)
		{
			if (Helper.IsEnumType(cspaceType) ^ type.IsEnum())
			{
				this.LogLoadMessage(Strings.Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(cspaceType.FullName, cspaceType.FullName), cspaceType);
				return null;
			}
			EdmType edmType;
			if (Helper.IsEnumType(cspaceType))
			{
				this.TryCreateEnumType(type, (EnumType)cspaceType, out edmType);
				return edmType;
			}
			this.TryCreateStructuralType(type, (StructuralType)cspaceType, out edmType);
			return edmType;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000D5E4C File Offset: 0x000D404C
		private bool TryCreateEnumType(Type enumType, EnumType cspaceEnumType, out EdmType newOSpaceType)
		{
			newOSpaceType = null;
			if (!this.UnderlyingEnumTypesMatch(enumType, cspaceEnumType) || !this.EnumMembersMatch(enumType, cspaceEnumType))
			{
				return false;
			}
			newOSpaceType = new ClrEnumType(enumType, cspaceEnumType.NamespaceName, cspaceEnumType.Name);
			this.LoadedTypes.Add(enumType.FullName, newOSpaceType);
			return true;
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000D5E9C File Offset: 0x000D409C
		private bool TryCreateStructuralType(Type type, StructuralType cspaceType, out EdmType newOSpaceType)
		{
			List<Action> list = new List<Action>();
			newOSpaceType = null;
			StructuralType ospaceType;
			if (Helper.IsEntityType(cspaceType))
			{
				ospaceType = new ClrEntityType(type, cspaceType.NamespaceName, cspaceType.Name);
			}
			else
			{
				ospaceType = new ClrComplexType(type, cspaceType.NamespaceName, cspaceType.Name);
			}
			if (cspaceType.BaseType != null)
			{
				if (!OSpaceTypeFactory.TypesMatchByConvention(type.BaseType(), cspaceType.BaseType))
				{
					string text = Strings.Validator_OSpace_Convention_BaseTypeIncompatible(type.BaseType().FullName, type.FullName, cspaceType.BaseType.FullName);
					this.LogLoadMessage(text, cspaceType);
					return false;
				}
				this.TrackClosure(type.BaseType());
				list.Add(delegate
				{
					ospaceType.BaseType = this.ResolveBaseType((StructuralType)cspaceType.BaseType, type);
				});
			}
			if (!this.TryCreateMembers(type, cspaceType, ospaceType, list))
			{
				return false;
			}
			this.LoadedTypes.Add(type.FullName, ospaceType);
			foreach (Action action in list)
			{
				this.ReferenceResolutions.Add(action);
			}
			newOSpaceType = ospaceType;
			return true;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000D6048 File Offset: 0x000D4248
		internal static bool TypesMatchByConvention(Type type, EdmType cspaceType)
		{
			return type.Name == cspaceType.Name;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000D605C File Offset: 0x000D425C
		private bool UnderlyingEnumTypesMatch(Type enumType, EnumType cspaceEnumType)
		{
			PrimitiveType primitiveType;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(enumType.GetEnumUnderlyingType(), out primitiveType))
			{
				this.LogLoadMessage(Strings.Validator_UnsupportedEnumUnderlyingType(enumType.GetEnumUnderlyingType().FullName), cspaceEnumType);
				return false;
			}
			if (primitiveType.PrimitiveTypeKind != cspaceEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				this.LogLoadMessage(Strings.Validator_OSpace_Convention_NonMatchingUnderlyingTypes, cspaceEnumType);
				return false;
			}
			return true;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000D60B8 File Offset: 0x000D42B8
		private bool EnumMembersMatch(Type enumType, EnumType cspaceEnumType)
		{
			Type enumUnderlyingType = enumType.GetEnumUnderlyingType();
			IEnumerator<EnumMember> enumerator = cspaceEnumType.Members.OrderBy((EnumMember m) => m.Name).GetEnumerator();
			IEnumerator<string> enumerator2 = (from n in enumType.GetEnumNames()
				orderby n
				select n).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return true;
			}
			while (enumerator2.MoveNext())
			{
				if (enumerator.Current.Name == enumerator2.Current && enumerator.Current.Value.Equals(Convert.ChangeType(Enum.Parse(enumType, enumerator2.Current), enumUnderlyingType, CultureInfo.InvariantCulture)) && !enumerator.MoveNext())
				{
					return true;
				}
			}
			this.LogLoadMessage(Strings.Mapping_Enum_OCMapping_MemberMismatch(enumType.FullName, enumerator.Current.Name, enumerator.Current.Value, cspaceEnumType.FullName), cspaceEnumType);
			return false;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000D61B8 File Offset: 0x000D43B8
		private bool TryCreateMembers(Type type, StructuralType cspaceType, StructuralType ospaceType, List<Action> referenceResolutionListForCurrentType)
		{
			IEnumerable<PropertyInfo> enumerable = ((cspaceType.BaseType == null) ? type.GetRuntimeProperties() : type.GetDeclaredProperties()).Where((PropertyInfo p) => !p.IsStatic());
			return this.TryFindAndCreatePrimitiveProperties(type, cspaceType, ospaceType, enumerable) && this.TryFindAndCreateEnumProperties(type, cspaceType, ospaceType, enumerable, referenceResolutionListForCurrentType) && this.TryFindComplexProperties(type, cspaceType, ospaceType, enumerable, referenceResolutionListForCurrentType) && this.TryFindNavigationProperties(type, cspaceType, ospaceType, enumerable, referenceResolutionListForCurrentType);
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000D6240 File Offset: 0x000D4440
		private bool TryFindComplexProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from m in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
				where Helper.IsComplexType(m.TypeUsage.EdmType)
				select m).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string text = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(text, cspaceType);
						return false;
					}
					list.Add(new KeyValuePair<EdmProperty, PropertyInfo>(cspaceProperty, propertyInfo));
				}
			}
			foreach (KeyValuePair<EdmProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ot = ospaceType;
				EdmProperty cp = keyValuePair.Key;
				PropertyInfo clrp = keyValuePair.Value;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddComplexType(type, ot, cp, clrp);
				});
			}
			return true;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000D63C8 File Offset: 0x000D45C8
		private bool TryFindNavigationProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<NavigationProperty, PropertyInfo>> list = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			using (ReadOnlyMetadataCollection<NavigationProperty>.Enumerator enumerator = cspaceType.GetDeclaredOnlyMembers<NavigationProperty>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavigationProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.NonPrimitiveMemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string text = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(text, cspaceType);
						return false;
					}
					bool flag = cspaceProperty.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many;
					if (propertyInfo.CanRead && (!flag || propertyInfo.CanWriteExtended()))
					{
						list.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(cspaceProperty, propertyInfo));
					}
				}
			}
			foreach (KeyValuePair<NavigationProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ct = cspaceType;
				StructuralType ot = ospaceType;
				NavigationProperty cp = keyValuePair.Key;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddNavigationProperty(ct, ot, cp);
				});
			}
			return true;
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000D6540 File Offset: 0x000D4740
		private EdmType ResolveBaseType(StructuralType baseCSpaceType, Type type)
		{
			EdmType edmType;
			if (!this.CspaceToOspace.TryGetValue(baseCSpaceType, out edmType))
			{
				this.LogError(Strings.Validator_OSpace_Convention_BaseTypeNotLoaded(type, baseCSpaceType), baseCSpaceType);
			}
			return edmType;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000D656C File Offset: 0x000D476C
		private bool TryFindAndCreatePrimitiveProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties)
		{
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
				where Helper.IsPrimitiveType(p.TypeUsage.EdmType)
				select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string text = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(text, cspaceType);
						return false;
					}
					PrimitiveType primitiveType;
					if (!OSpaceTypeFactory.TryGetPrimitiveType(propertyInfo.PropertyType, out primitiveType))
					{
						string text2 = Strings.Validator_OSpace_Convention_NonPrimitiveTypeProperty(propertyInfo.Name, type.FullName, propertyInfo.PropertyType.FullName);
						this.LogLoadMessage(text2, cspaceType);
						return false;
					}
					if (!propertyInfo.CanRead || !propertyInfo.CanWriteExtended())
					{
						string text3 = Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(propertyInfo.Name, type.FullName, type.Assembly().FullName);
						this.LogLoadMessage(text3, cspaceType);
						return false;
					}
					OSpaceTypeFactory.AddScalarMember(type, propertyInfo, ospaceType, cspaceProperty, primitiveType);
				}
			}
			return true;
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000D66BC File Offset: 0x000D48BC
		protected static bool TryGetPrimitiveType(Type type, out PrimitiveType primitiveType)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveType(Nullable.GetUnderlyingType(type) ?? type, out primitiveType);
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000D66D4 File Offset: 0x000D48D4
		private bool TryFindAndCreateEnumProperties(Type type, StructuralType cspaceType, StructuralType ospaceType, IEnumerable<PropertyInfo> clrProperties, List<Action> referenceResolutionListForCurrentType)
		{
			List<KeyValuePair<EdmProperty, PropertyInfo>> list = new List<KeyValuePair<EdmProperty, PropertyInfo>>();
			using (IEnumerator<EdmProperty> enumerator = (from p in cspaceType.GetDeclaredOnlyMembers<EdmProperty>()
				where Helper.IsEnumType(p.TypeUsage.EdmType)
				select p).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty cspaceProperty = enumerator.Current;
					PropertyInfo propertyInfo = clrProperties.FirstOrDefault((PropertyInfo p) => OSpaceTypeFactory.MemberMatchesByConvention(p, cspaceProperty));
					if (!(propertyInfo != null))
					{
						string text = Strings.Validator_OSpace_Convention_MissingRequiredProperty(cspaceProperty.Name, type.FullName);
						this.LogLoadMessage(text, cspaceType);
						return false;
					}
					list.Add(new KeyValuePair<EdmProperty, PropertyInfo>(cspaceProperty, propertyInfo));
				}
			}
			foreach (KeyValuePair<EdmProperty, PropertyInfo> keyValuePair in list)
			{
				this.TrackClosure(keyValuePair.Value.PropertyType);
				StructuralType ot = ospaceType;
				EdmProperty cp = keyValuePair.Key;
				PropertyInfo clrp = keyValuePair.Value;
				referenceResolutionListForCurrentType.Add(delegate
				{
					this.CreateAndAddEnumProperty(type, ot, cp, clrp);
				});
			}
			return true;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000D685C File Offset: 0x000D4A5C
		private static bool MemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000D6870 File Offset: 0x000D4A70
		private void CreateAndAddComplexType(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out edmType))
			{
				EdmProperty edmProperty = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(false)
				}), clrProperty, type);
				ospaceType.AddMember(edmProperty);
				return;
			}
			this.LogError(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000D68F0 File Offset: 0x000D4AF0
		private static bool NonPrimitiveMemberMatchesByConvention(PropertyInfo clrProperty, EdmMember cspaceMember)
		{
			return !clrProperty.PropertyType.IsValueType() && !clrProperty.PropertyType.IsAssignableFrom(typeof(string)) && clrProperty.Name == cspaceMember.Name;
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x000D692C File Offset: 0x000D4B2C
		private void CreateAndAddNavigationProperty(StructuralType cspaceType, StructuralType ospaceType, NavigationProperty cspaceProperty)
		{
			EdmType edmType;
			if (this.CspaceToOspace.TryGetValue(cspaceProperty.RelationshipType, out edmType))
			{
				EdmType edmType2 = null;
				EdmType edmType4;
				if (Helper.IsCollectionType(cspaceProperty.TypeUsage.EdmType))
				{
					EdmType edmType3;
					if (this.CspaceToOspace.TryGetValue(((CollectionType)cspaceProperty.TypeUsage.EdmType).TypeUsage.EdmType, out edmType3))
					{
						edmType2 = edmType3.GetCollectionType();
					}
				}
				else if (this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out edmType4))
				{
					edmType2 = edmType4;
				}
				NavigationProperty navigationProperty = new NavigationProperty(cspaceProperty.Name, TypeUsage.Create(edmType2));
				RelationshipType relationshipType = (RelationshipType)edmType;
				navigationProperty.RelationshipType = relationshipType;
				navigationProperty.ToEndMember = (RelationshipEndMember)relationshipType.Members.First((EdmMember e) => e.Name == cspaceProperty.ToEndMember.Name);
				navigationProperty.FromEndMember = (RelationshipEndMember)relationshipType.Members.First((EdmMember e) => e.Name == cspaceProperty.FromEndMember.Name);
				ospaceType.AddMember(navigationProperty);
				return;
			}
			EntityTypeBase entityTypeBase = cspaceProperty.RelationshipType.RelationshipEndMembers.Select((RelationshipEndMember e) => ((RefType)e.TypeUsage.EdmType).ElementType).First((EntityTypeBase e) => e != cspaceType);
			this.LogError(Strings.Validator_OSpace_Convention_RelationshipNotLoaded(cspaceProperty.RelationshipType.FullName, entityTypeBase.FullName), entityTypeBase);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x000D6AC0 File Offset: 0x000D4CC0
		private void CreateAndAddEnumProperty(Type type, StructuralType ospaceType, EdmProperty cspaceProperty, PropertyInfo clrProperty)
		{
			EdmType edmType;
			if (!this.CspaceToOspace.TryGetValue(cspaceProperty.TypeUsage.EdmType, out edmType))
			{
				this.LogError(Strings.Validator_OSpace_Convention_MissingOSpaceType(cspaceProperty.TypeUsage.EdmType.FullName), cspaceProperty.TypeUsage.EdmType);
				return;
			}
			if (clrProperty.CanRead && clrProperty.CanWriteExtended())
			{
				OSpaceTypeFactory.AddScalarMember(type, clrProperty, ospaceType, cspaceProperty, edmType);
				return;
			}
			this.LogError(Strings.Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(clrProperty.Name, type.FullName, type.Assembly().FullName), cspaceProperty.TypeUsage.EdmType);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000D6B5C File Offset: 0x000D4D5C
		private static void AddScalarMember(Type type, PropertyInfo clrProperty, StructuralType ospaceType, EdmProperty cspaceProperty, EdmType propertyType)
		{
			StructuralType declaringType = cspaceProperty.DeclaringType;
			bool flag = Helper.IsEntityType(declaringType) && ((EntityType)declaringType).KeyMemberNames.Contains(clrProperty.Name);
			bool flag2 = !flag && (!clrProperty.PropertyType.IsValueType() || Nullable.GetUnderlyingType(clrProperty.PropertyType) != null);
			EdmProperty edmProperty = new EdmProperty(cspaceProperty.Name, TypeUsage.Create(propertyType, new FacetValues
			{
				Nullable = new bool?(flag2)
			}), clrProperty, type);
			if (flag)
			{
				((EntityType)ospaceType).AddKeyMember(edmProperty);
				return;
			}
			ospaceType.AddMember(edmProperty);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000D6BFC File Offset: 0x000D4DFC
		public virtual void CreateRelationships(EdmItemCollection edmItemCollection)
		{
			foreach (AssociationType associationType in edmItemCollection.GetItems<AssociationType>())
			{
				if (!this.CspaceToOspace.ContainsKey(associationType))
				{
					EdmType[] array = new EdmType[2];
					if (this.CspaceToOspace.TryGetValue(OSpaceTypeFactory.GetRelationshipEndType(associationType.RelationshipEndMembers[0]), out array[0]) && this.CspaceToOspace.TryGetValue(OSpaceTypeFactory.GetRelationshipEndType(associationType.RelationshipEndMembers[1]), out array[1]))
					{
						AssociationType associationType2 = new AssociationType(associationType.Name, associationType.NamespaceName, associationType.IsForeignKey, DataSpace.OSpace);
						for (int i = 0; i < associationType.RelationshipEndMembers.Count; i++)
						{
							EntityType entityType = (EntityType)array[i];
							RelationshipEndMember relationshipEndMember = associationType.RelationshipEndMembers[i];
							associationType2.AddKeyMember(new AssociationEndMember(relationshipEndMember.Name, entityType.GetReferenceType(), relationshipEndMember.RelationshipMultiplicity));
						}
						this.AddToTypesInAssembly(associationType2);
						this.LoadedTypes.Add(associationType2.FullName, associationType2);
						this.CspaceToOspace.Add(associationType, associationType2);
					}
				}
			}
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000D6D50 File Offset: 0x000D4F50
		private static StructuralType GetRelationshipEndType(RelationshipEndMember relationshipEndMember)
		{
			return ((RefType)relationshipEndMember.TypeUsage.EdmType).ElementType;
		}
	}
}
