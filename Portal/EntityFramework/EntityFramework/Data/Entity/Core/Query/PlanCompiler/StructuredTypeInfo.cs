using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Globalization;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036A RID: 874
	internal class StructuredTypeInfo
	{
		// Token: 0x06002A58 RID: 10840 RVA: 0x0008A809 File Offset: 0x00088A09
		private StructuredTypeInfo(HashSet<string> typesNeedingNullSentinel)
		{
			this.m_typeInfoMap = new Dictionary<TypeUsage, TypeInfo>(TypeUsageEqualityComparer.Instance);
			this.m_typeInfoMapPopulated = false;
			this.m_typesNeedingNullSentinel = typesNeedingNullSentinel;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0008A82F File Offset: 0x00088A2F
		internal static void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper, HashSet<string> typesNeedingNullSentinel, out StructuredTypeInfo structuredTypeInfo)
		{
			structuredTypeInfo = new StructuredTypeInfo(typesNeedingNullSentinel);
			structuredTypeInfo.Process(itree, referencedTypes, referencedEntitySets, freeFloatingEntityConstructorTypes, discriminatorMaps, relPropertyHelper);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0008A84C File Offset: 0x00088A4C
		private void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper)
		{
			PlanCompiler.Assert(itree != null, "null itree?");
			this.m_stringType = itree.StringType;
			this.m_intType = itree.IntegerType;
			this.m_relPropertyHelper = relPropertyHelper;
			this.ProcessEntitySets(referencedEntitySets, freeFloatingEntityConstructorTypes);
			this.ProcessDiscriminatorMaps(discriminatorMaps);
			this.ProcessTypes(referencedTypes);
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x0008A89F File Offset: 0x00088A9F
		internal EntitySet[] EntitySetIdToEntitySetMap
		{
			get
			{
				return this.m_entitySetIdToEntitySetMap;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x0008A8A7 File Offset: 0x00088AA7
		internal RelPropertyHelper RelPropertyHelper
		{
			get
			{
				return this.m_relPropertyHelper;
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0008A8B0 File Offset: 0x00088AB0
		internal EntitySet GetEntitySet(EntityTypeBase type)
		{
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType(type);
			EntitySet entitySet;
			if (!this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out entitySet))
			{
				return null;
			}
			return entitySet;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0008A8D8 File Offset: 0x00088AD8
		internal int GetEntitySetId(EntitySet e)
		{
			int num = 0;
			if (!this.m_entitySetToEntitySetIdMap.TryGetValue(e, out num))
			{
				PlanCompiler.Assert(false, "no such entity set?");
			}
			return num;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0008A903 File Offset: 0x00088B03
		internal Set<EntitySet> GetEntitySets()
		{
			return new Set<EntitySet>(this.m_entitySetIdToEntitySetMap).MakeReadOnly();
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0008A918 File Offset: 0x00088B18
		internal TypeInfo GetTypeInfo(TypeUsage type)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return null;
			}
			TypeInfo typeInfo = null;
			if (!this.m_typeInfoMap.TryGetValue(type, out typeInfo))
			{
				PlanCompiler.Assert(!TypeUtils.IsStructuredType(type) || !this.m_typeInfoMapPopulated, "cannot find typeInfo for type " + ((type != null) ? type.ToString() : null));
			}
			return typeInfo;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0008A974 File Offset: 0x00088B74
		private void AddEntityTypeToSetEntry(EntityType entityType, EntitySet entitySet)
		{
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType(entityType);
			bool flag = true;
			EntitySet entitySet2;
			if (entitySet == null)
			{
				flag = false;
			}
			else if (this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out entitySet2) && entitySet2 != entitySet)
			{
				flag = false;
			}
			if (flag)
			{
				this.m_entityTypeToEntitySetMap[rootType] = entitySet;
				return;
			}
			this.m_entityTypeToEntitySetMap[rootType] = null;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0008A9C8 File Offset: 0x00088BC8
		private void ProcessEntitySets(HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes)
		{
			this.AssignEntitySetIds(referencedEntitySets);
			this.m_entityTypeToEntitySetMap = new Dictionary<EntityTypeBase, EntitySet>();
			foreach (EntitySet entitySet in referencedEntitySets)
			{
				this.AddEntityTypeToSetEntry(entitySet.ElementType, entitySet);
			}
			foreach (EntityType entityType in freeFloatingEntityConstructorTypes)
			{
				this.AddEntityTypeToSetEntry(entityType, null);
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0008AA6C File Offset: 0x00088C6C
		private void ProcessDiscriminatorMaps(Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps)
		{
			Dictionary<EntitySetBase, ExplicitDiscriminatorMap> dictionary = null;
			if (discriminatorMaps != null)
			{
				dictionary = new Dictionary<EntitySetBase, ExplicitDiscriminatorMap>(discriminatorMaps.Count, discriminatorMaps.Comparer);
				foreach (KeyValuePair<EntitySetBase, DiscriminatorMapInfo> keyValuePair in discriminatorMaps)
				{
					EntitySetBase key = keyValuePair.Key;
					ExplicitDiscriminatorMap discriminatorMap = keyValuePair.Value.DiscriminatorMap;
					if (discriminatorMap != null)
					{
						EntityTypeBase rootType = StructuredTypeInfo.GetRootType(key.ElementType);
						if (this.GetEntitySet(rootType) != null)
						{
							dictionary.Add(key, discriminatorMap);
						}
					}
				}
				if (dictionary.Count == 0)
				{
					dictionary = null;
				}
			}
			this.m_discriminatorMaps = dictionary;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0008AB1C File Offset: 0x00088D1C
		private void AssignEntitySetIds(HashSet<EntitySet> referencedEntitySets)
		{
			this.m_entitySetIdToEntitySetMap = new EntitySet[referencedEntitySets.Count];
			this.m_entitySetToEntitySetIdMap = new Dictionary<EntitySet, int>();
			int num = 0;
			foreach (EntitySet entitySet in referencedEntitySets)
			{
				if (!this.m_entitySetToEntitySetIdMap.ContainsKey(entitySet))
				{
					this.m_entitySetIdToEntitySetMap[num] = entitySet;
					this.m_entitySetToEntitySetIdMap[entitySet] = num;
					num++;
				}
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0008ABAC File Offset: 0x00088DAC
		private void ProcessTypes(HashSet<TypeUsage> referencedTypes)
		{
			this.PopulateTypeInfoMap(referencedTypes);
			this.AssignTypeIds();
			this.ExplodeTypes();
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		private void PopulateTypeInfoMap(HashSet<TypeUsage> referencedTypes)
		{
			foreach (TypeUsage typeUsage in referencedTypes)
			{
				this.CreateTypeInfoForType(typeUsage);
			}
			this.m_typeInfoMapPopulated = true;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x0008AC1C File Offset: 0x00088E1C
		private bool TryGetDiscriminatorMap(EdmType type, out ExplicitDiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			if (this.m_discriminatorMaps == null)
			{
				return false;
			}
			if (type.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return false;
			}
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType((EntityType)type);
			EntitySet entitySet;
			return this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out entitySet) && entitySet != null && this.m_discriminatorMaps.TryGetValue(entitySet, out discriminatorMap);
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x0008AC74 File Offset: 0x00088E74
		private void CreateTypeInfoForType(TypeUsage type)
		{
			while (TypeUtils.IsCollectionType(type))
			{
				type = TypeHelpers.GetEdmType<CollectionType>(type).TypeUsage;
			}
			if (TypeUtils.IsStructuredType(type))
			{
				ExplicitDiscriminatorMap explicitDiscriminatorMap;
				this.TryGetDiscriminatorMap(type.EdmType, out explicitDiscriminatorMap);
				this.CreateTypeInfoForStructuredType(type, explicitDiscriminatorMap);
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0008ACB8 File Offset: 0x00088EB8
		private TypeInfo CreateTypeInfoForStructuredType(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap)
		{
			PlanCompiler.Assert(TypeUtils.IsStructuredType(type), "expected structured type. Found " + ((type != null) ? type.ToString() : null));
			TypeInfo typeInfo = this.GetTypeInfo(type);
			if (typeInfo != null)
			{
				return typeInfo;
			}
			TypeInfo typeInfo2 = null;
			RefType refType;
			if (type.EdmType.BaseType != null)
			{
				typeInfo2 = this.CreateTypeInfoForStructuredType(TypeUsage.Create(type.EdmType.BaseType), discriminatorMap);
			}
			else if (TypeHelpers.TryGetEdmType<RefType>(type, out refType))
			{
				EntityType entityType = refType.ElementType as EntityType;
				if (entityType != null && entityType.BaseType != null)
				{
					TypeUsage typeUsage = TypeHelpers.CreateReferenceTypeUsage(entityType.BaseType as EntityType);
					typeInfo2 = this.CreateTypeInfoForStructuredType(typeUsage, discriminatorMap);
				}
			}
			foreach (object obj in TypeHelpers.GetDeclaredStructuralMembers(type))
			{
				EdmMember edmMember = (EdmMember)obj;
				this.CreateTypeInfoForType(edmMember.TypeUsage);
			}
			EntityTypeBase entityTypeBase;
			if (TypeHelpers.TryGetEdmType<EntityTypeBase>(type, out entityTypeBase))
			{
				foreach (RelProperty relProperty in this.m_relPropertyHelper.GetDeclaredOnlyRelProperties(entityTypeBase))
				{
					this.CreateTypeInfoForType(relProperty.ToEnd.TypeUsage);
				}
			}
			typeInfo = TypeInfo.Create(type, typeInfo2, discriminatorMap);
			this.m_typeInfoMap.Add(type, typeInfo);
			return typeInfo;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0008AE2C File Offset: 0x0008902C
		private void AssignTypeIds()
		{
			int num = 0;
			foreach (KeyValuePair<TypeUsage, TypeInfo> keyValuePair in this.m_typeInfoMap)
			{
				if (keyValuePair.Value.RootType.DiscriminatorMap != null)
				{
					EntityType entityType = (EntityType)keyValuePair.Key.EdmType;
					keyValuePair.Value.TypeId = keyValuePair.Value.RootType.DiscriminatorMap.GetTypeId(entityType);
				}
				else if (keyValuePair.Value.IsRootType && (TypeSemantics.IsEntityType(keyValuePair.Key) || TypeSemantics.IsComplexType(keyValuePair.Key)))
				{
					this.AssignRootTypeId(keyValuePair.Value, string.Format(CultureInfo.InvariantCulture, "{0}X", new object[] { num }));
					num++;
				}
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0008AF24 File Offset: 0x00089124
		private void AssignRootTypeId(TypeInfo typeInfo, string typeId)
		{
			typeInfo.TypeId = typeId;
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0008AF34 File Offset: 0x00089134
		private void AssignTypeIdsToSubTypes(TypeInfo typeInfo)
		{
			int num = 0;
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.AssignTypeId(typeInfo2, num);
				num++;
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0008AF90 File Offset: 0x00089190
		private void AssignTypeId(TypeInfo typeInfo, int subtypeNum)
		{
			typeInfo.TypeId = string.Format(CultureInfo.InvariantCulture, "{0}{1}X", new object[]
			{
				typeInfo.SuperType.TypeId,
				subtypeNum
			});
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0008AFCB File Offset: 0x000891CB
		private static bool NeedsTypeIdProperty(TypeInfo typeInfo)
		{
			return typeInfo.ImmediateSubTypes.Count > 0 && !TypeSemantics.IsReferenceType(typeInfo.Type);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0008AFEB File Offset: 0x000891EB
		private bool NeedsNullSentinelProperty(TypeInfo typeInfo)
		{
			return this.m_typesNeedingNullSentinel.Contains(typeInfo.Type.EdmType.Identity);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0008B008 File Offset: 0x00089208
		private bool NeedsEntitySetIdProperty(TypeInfo typeInfo)
		{
			RefType refType = typeInfo.Type.EdmType as RefType;
			EntityType entityType;
			if (refType != null)
			{
				entityType = refType.ElementType as EntityType;
			}
			else
			{
				entityType = typeInfo.Type.EdmType as EntityType;
			}
			return entityType != null && this.GetEntitySet(entityType) == null;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0008B058 File Offset: 0x00089258
		private void ExplodeTypes()
		{
			foreach (KeyValuePair<TypeUsage, TypeInfo> keyValuePair in this.m_typeInfoMap)
			{
				if (keyValuePair.Value.IsRootType)
				{
					this.ExplodeType(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0008B0C0 File Offset: 0x000892C0
		private TypeInfo ExplodeType(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type))
			{
				TypeInfo typeInfo = this.GetTypeInfo(type);
				this.ExplodeType(typeInfo);
				return typeInfo;
			}
			if (TypeUtils.IsCollectionType(type))
			{
				TypeUsage typeUsage = TypeHelpers.GetEdmType<CollectionType>(type).TypeUsage;
				this.ExplodeType(typeUsage);
				return null;
			}
			return null;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0008B105 File Offset: 0x00089305
		private void ExplodeType(TypeInfo typeInfo)
		{
			this.ExplodeRootStructuredType(typeInfo.RootType);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0008B114 File Offset: 0x00089314
		private void ExplodeRootStructuredType(RootTypeInfo rootType)
		{
			if (rootType.FlattenedType != null)
			{
				return;
			}
			if (StructuredTypeInfo.NeedsTypeIdProperty(rootType))
			{
				rootType.AddPropertyRef(TypeIdPropertyRef.Instance);
				if (rootType.DiscriminatorMap != null)
				{
					rootType.TypeIdKind = TypeIdKind.UserSpecified;
					rootType.TypeIdType = Helper.GetModelTypeUsage(rootType.DiscriminatorMap.DiscriminatorProperty);
				}
				else
				{
					rootType.TypeIdKind = TypeIdKind.Generated;
					rootType.TypeIdType = this.m_stringType;
				}
			}
			if (this.NeedsEntitySetIdProperty(rootType))
			{
				rootType.AddPropertyRef(EntitySetIdPropertyRef.Instance);
			}
			if (this.NeedsNullSentinelProperty(rootType))
			{
				rootType.AddPropertyRef(NullSentinelPropertyRef.Instance);
			}
			this.ExplodeRootStructuredTypeHelper(rootType);
			if (TypeSemantics.IsEntityType(rootType.Type))
			{
				this.AddRelProperties(rootType);
			}
			this.CreateFlattenedRecordType(rootType);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0008B1C4 File Offset: 0x000893C4
		private void ExplodeRootStructuredTypeHelper(TypeInfo typeInfo)
		{
			RootTypeInfo rootType = typeInfo.RootType;
			RefType refType;
			IEnumerable enumerable;
			if (TypeHelpers.TryGetEdmType<RefType>(typeInfo.Type, out refType))
			{
				if (!typeInfo.IsRootType)
				{
					return;
				}
				enumerable = refType.ElementType.KeyMembers;
			}
			else
			{
				enumerable = TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type);
			}
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				TypeInfo typeInfo2 = this.ExplodeType(edmMember.TypeUsage);
				if (typeInfo2 == null)
				{
					rootType.AddPropertyRef(new SimplePropertyRef(edmMember));
				}
				else
				{
					foreach (PropertyRef propertyRef in typeInfo2.PropertyRefList)
					{
						rootType.AddPropertyRef(propertyRef.CreateNestedPropertyRef(edmMember));
					}
				}
			}
			foreach (TypeInfo typeInfo3 in typeInfo.ImmediateSubTypes)
			{
				this.ExplodeRootStructuredTypeHelper(typeInfo3);
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0008B304 File Offset: 0x00089504
		private void AddRelProperties(TypeInfo typeInfo)
		{
			EntityTypeBase entityTypeBase = (EntityTypeBase)typeInfo.Type.EdmType;
			foreach (RelProperty relProperty in this.m_relPropertyHelper.GetDeclaredOnlyRelProperties(entityTypeBase))
			{
				TypeInfo typeInfo2 = this.GetTypeInfo(relProperty.ToEnd.TypeUsage);
				this.ExplodeType(typeInfo2);
				foreach (PropertyRef propertyRef in typeInfo2.PropertyRefList)
				{
					typeInfo.RootType.AddPropertyRef(propertyRef.CreateNestedPropertyRef(relProperty));
				}
			}
			foreach (TypeInfo typeInfo3 in typeInfo.ImmediateSubTypes)
			{
				this.AddRelProperties(typeInfo3);
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0008B410 File Offset: 0x00089610
		private void CreateFlattenedRecordType(RootTypeInfo type)
		{
			bool flag = TypeSemantics.IsEntityType(type.Type) && type.ImmediateSubTypes.Count == 0;
			List<KeyValuePair<string, TypeUsage>> list = new List<KeyValuePair<string, TypeUsage>>();
			HashSet<string> hashSet = new HashSet<string>();
			int num = 0;
			foreach (PropertyRef propertyRef in type.PropertyRefList)
			{
				string text = null;
				if (flag)
				{
					SimplePropertyRef simplePropertyRef = propertyRef as SimplePropertyRef;
					if (simplePropertyRef != null)
					{
						text = simplePropertyRef.Property.Name;
					}
				}
				if (text == null)
				{
					text = "F" + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				while (hashSet.Contains(text))
				{
					text = "F" + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				TypeUsage propertyType = this.GetPropertyType(type, propertyRef);
				list.Add(new KeyValuePair<string, TypeUsage>(text, propertyType));
				hashSet.Add(text);
			}
			type.FlattenedType = TypeHelpers.CreateRowType(list);
			IEnumerator<PropertyRef> enumerator2 = type.PropertyRefList.GetEnumerator();
			foreach (EdmProperty edmProperty in type.FlattenedType.Properties)
			{
				if (!enumerator2.MoveNext())
				{
					PlanCompiler.Assert(false, "property refs count and flattened type member count mismatch?");
				}
				type.AddPropertyMapping(enumerator2.Current, edmProperty);
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0008B59C File Offset: 0x0008979C
		private TypeUsage GetNewType(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type))
			{
				return this.GetTypeInfo(type).FlattenedTypeUsage;
			}
			TypeUsage typeUsage;
			if (TypeHelpers.TryGetCollectionElementType(type, out typeUsage))
			{
				TypeUsage newType = this.GetNewType(typeUsage);
				if (newType.EdmEquals(typeUsage))
				{
					return type;
				}
				return TypeHelpers.CreateCollectionTypeUsage(newType);
			}
			else
			{
				if (TypeUtils.IsEnumerationType(type))
				{
					return TypeHelpers.CreateEnumUnderlyingTypeUsage(type);
				}
				if (TypeSemantics.IsStrongSpatialType(type))
				{
					return TypeHelpers.CreateSpatialUnionTypeUsage(type);
				}
				return type;
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0008B604 File Offset: 0x00089804
		private TypeUsage GetPropertyType(RootTypeInfo typeInfo, PropertyRef p)
		{
			TypeUsage typeUsage = null;
			PropertyRef propertyRef = null;
			while (p is NestedPropertyRef)
			{
				NestedPropertyRef nestedPropertyRef = (NestedPropertyRef)p;
				p = nestedPropertyRef.OuterProperty;
				propertyRef = nestedPropertyRef.InnerProperty;
			}
			if (p is TypeIdPropertyRef)
			{
				SimplePropertyRef simplePropertyRef = (SimplePropertyRef)propertyRef;
				if (simplePropertyRef != null)
				{
					TypeUsage typeUsage2 = simplePropertyRef.Property.TypeUsage;
					typeUsage = this.GetTypeInfo(typeUsage2).RootType.TypeIdType;
				}
				else
				{
					typeUsage = typeInfo.TypeIdType;
				}
			}
			else if (p is EntitySetIdPropertyRef || p is NullSentinelPropertyRef)
			{
				typeUsage = this.m_intType;
			}
			else if (p is RelPropertyRef)
			{
				typeUsage = ((RelPropertyRef)p).Property.ToEnd.TypeUsage;
			}
			else
			{
				SimplePropertyRef simplePropertyRef2 = p as SimplePropertyRef;
				if (simplePropertyRef2 != null)
				{
					typeUsage = Helper.GetModelTypeUsage(simplePropertyRef2.Property);
				}
			}
			typeUsage = this.GetNewType(typeUsage);
			PlanCompiler.Assert(typeUsage != null, "unrecognized property type?");
			return typeUsage;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0008B6D7 File Offset: 0x000898D7
		private static EntityTypeBase GetRootType(EntityTypeBase type)
		{
			while (type.BaseType != null)
			{
				type = (EntityTypeBase)type.BaseType;
			}
			return type;
		}

		// Token: 0x04000E99 RID: 3737
		private TypeUsage m_stringType;

		// Token: 0x04000E9A RID: 3738
		private TypeUsage m_intType;

		// Token: 0x04000E9B RID: 3739
		private readonly Dictionary<TypeUsage, TypeInfo> m_typeInfoMap;

		// Token: 0x04000E9C RID: 3740
		private bool m_typeInfoMapPopulated;

		// Token: 0x04000E9D RID: 3741
		private EntitySet[] m_entitySetIdToEntitySetMap;

		// Token: 0x04000E9E RID: 3742
		private Dictionary<EntitySet, int> m_entitySetToEntitySetIdMap;

		// Token: 0x04000E9F RID: 3743
		private Dictionary<EntityTypeBase, EntitySet> m_entityTypeToEntitySetMap;

		// Token: 0x04000EA0 RID: 3744
		private Dictionary<EntitySetBase, ExplicitDiscriminatorMap> m_discriminatorMaps;

		// Token: 0x04000EA1 RID: 3745
		private RelPropertyHelper m_relPropertyHelper;

		// Token: 0x04000EA2 RID: 3746
		private readonly HashSet<string> m_typesNeedingNullSentinel;
	}
}
