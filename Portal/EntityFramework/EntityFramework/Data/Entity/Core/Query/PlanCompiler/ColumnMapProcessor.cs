using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000335 RID: 821
	internal class ColumnMapProcessor
	{
		// Token: 0x06002717 RID: 10007 RVA: 0x000714A8 File Offset: 0x0006F6A8
		internal ColumnMap ExpandColumnMap()
		{
			if (this.m_varInfo.Kind == VarInfoKind.CollectionVarInfo)
			{
				return new VarRefColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name, ((CollectionVarInfo)this.m_varInfo).NewVar);
			}
			if (this.m_varInfo.Kind == VarInfoKind.PrimitiveTypeVarInfo)
			{
				return new VarRefColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name, ((PrimitiveTypeVarInfo)this.m_varInfo).NewVar);
			}
			return this.CreateColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00071554 File Offset: 0x0006F754
		internal ColumnMapProcessor(VarRefColumnMap columnMap, VarInfo varInfo, StructuredTypeInfo typeInfo)
		{
			this.m_columnMap = columnMap;
			this.m_varInfo = varInfo;
			PlanCompiler.Assert(varInfo.NewVars != null && varInfo.NewVars.Count > 0, "No new Vars specified");
			this.m_varList = varInfo.NewVars.GetEnumerator();
			this.m_typeInfo = typeInfo;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000715B5 File Offset: 0x0006F7B5
		private Var GetNextVar()
		{
			if (this.m_varList.MoveNext())
			{
				return this.m_varList.Current;
			}
			PlanCompiler.Assert(false, "Could not GetNextVar");
			return null;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000715DC File Offset: 0x0006F7DC
		private ColumnMap CreateColumnMap(TypeUsage type, string name)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.CreateSimpleColumnMap(type, name);
			}
			return this.CreateStructuralColumnMap(type, name);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000715F8 File Offset: 0x0006F7F8
		private ComplexTypeColumnMap CreateComplexTypeColumnMap(TypeInfo typeInfo, string name, ComplexTypeColumnMap superTypeColumnMap, Dictionary<object, TypedColumnMap> discriminatorMap, List<TypedColumnMap> allMaps)
		{
			List<ColumnMap> list = new List<ColumnMap>();
			SimpleColumnMap simpleColumnMap = null;
			if (typeInfo.HasNullSentinelProperty)
			{
				simpleColumnMap = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.NullSentinelProperty), "__NullSentinel");
			}
			IEnumerable enumerable;
			if (superTypeColumnMap != null)
			{
				foreach (ColumnMap columnMap in superTypeColumnMap.Properties)
				{
					list.Add(columnMap);
				}
				enumerable = TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type);
			}
			else
			{
				enumerable = TypeHelpers.GetAllStructuralMembers(typeInfo.Type);
			}
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				ColumnMap columnMap2 = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
				list.Add(columnMap2);
			}
			ComplexTypeColumnMap complexTypeColumnMap = new ComplexTypeColumnMap(typeInfo.Type, name, list.ToArray(), simpleColumnMap);
			if (discriminatorMap != null)
			{
				discriminatorMap[typeInfo.TypeId] = complexTypeColumnMap;
			}
			if (allMaps != null)
			{
				allMaps.Add(complexTypeColumnMap);
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.CreateComplexTypeColumnMap(typeInfo2, name, complexTypeColumnMap, discriminatorMap, allMaps);
			}
			return complexTypeColumnMap;
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0007175C File Offset: 0x0006F95C
		private EntityColumnMap CreateEntityColumnMap(TypeInfo typeInfo, string name, EntityColumnMap superTypeColumnMap, Dictionary<object, TypedColumnMap> discriminatorMap, List<TypedColumnMap> allMaps, bool handleRelProperties)
		{
			EntityColumnMap entityColumnMap = null;
			List<ColumnMap> list = new List<ColumnMap>();
			if (superTypeColumnMap != null)
			{
				foreach (ColumnMap columnMap in superTypeColumnMap.Properties)
				{
					list.Add(columnMap);
				}
				foreach (object obj in TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type))
				{
					EdmMember edmMember = (EdmMember)obj;
					ColumnMap columnMap2 = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
					list.Add(columnMap2);
				}
				entityColumnMap = new EntityColumnMap(typeInfo.Type, name, list.ToArray(), superTypeColumnMap.EntityIdentity);
			}
			else
			{
				SimpleColumnMap simpleColumnMap = null;
				if (typeInfo.HasEntitySetIdProperty)
				{
					simpleColumnMap = this.CreateEntitySetIdColumnMap(typeInfo.EntitySetIdProperty);
				}
				List<SimpleColumnMap> list2 = new List<SimpleColumnMap>();
				Dictionary<EdmProperty, ColumnMap> dictionary = new Dictionary<EdmProperty, ColumnMap>();
				foreach (object obj2 in TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type))
				{
					EdmMember edmMember2 = (EdmMember)obj2;
					ColumnMap columnMap3 = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember2), edmMember2.Name);
					list.Add(columnMap3);
					if (TypeSemantics.IsPartOfKey(edmMember2))
					{
						EdmProperty edmProperty = edmMember2 as EdmProperty;
						PlanCompiler.Assert(edmProperty != null, "EntityType key member is not property?");
						dictionary[edmProperty] = columnMap3;
					}
				}
				foreach (EdmMember edmMember3 in TypeHelpers.GetEdmType<EntityType>(typeInfo.Type).KeyMembers)
				{
					EdmProperty edmProperty2 = edmMember3 as EdmProperty;
					PlanCompiler.Assert(edmProperty2 != null, "EntityType key member is not property?");
					SimpleColumnMap simpleColumnMap2 = dictionary[edmProperty2] as SimpleColumnMap;
					PlanCompiler.Assert(simpleColumnMap2 != null, "keyColumnMap is null");
					list2.Add(simpleColumnMap2);
				}
				EntityIdentity entityIdentity = this.CreateEntityIdentity((EntityType)typeInfo.Type.EdmType, simpleColumnMap, list2.ToArray());
				entityColumnMap = new EntityColumnMap(typeInfo.Type, name, list.ToArray(), entityIdentity);
			}
			if (discriminatorMap != null && typeInfo.TypeId != null)
			{
				discriminatorMap[typeInfo.TypeId] = entityColumnMap;
			}
			if (allMaps != null)
			{
				allMaps.Add(entityColumnMap);
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.CreateEntityColumnMap(typeInfo2, name, entityColumnMap, discriminatorMap, allMaps, false);
			}
			if (handleRelProperties)
			{
				this.BuildRelPropertyColumnMaps(typeInfo, true);
			}
			return entityColumnMap;
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00071A20 File Offset: 0x0006FC20
		private void BuildRelPropertyColumnMaps(TypeInfo typeInfo, bool includeSupertypeRelProperties)
		{
			IEnumerable<RelProperty> enumerable;
			if (includeSupertypeRelProperties)
			{
				enumerable = this.m_typeInfo.RelPropertyHelper.GetRelProperties(typeInfo.Type.EdmType as EntityTypeBase);
			}
			else
			{
				enumerable = this.m_typeInfo.RelPropertyHelper.GetDeclaredOnlyRelProperties(typeInfo.Type.EdmType as EntityTypeBase);
			}
			foreach (RelProperty relProperty in enumerable)
			{
				this.CreateColumnMap(relProperty.ToEnd.TypeUsage, relProperty.ToString());
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.BuildRelPropertyColumnMaps(typeInfo2, false);
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00071B08 File Offset: 0x0006FD08
		private SimpleColumnMap CreateEntitySetIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__EntitySetId");
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00071B1C File Offset: 0x0006FD1C
		private SimplePolymorphicColumnMap CreatePolymorphicColumnMap(TypeInfo typeInfo, string name)
		{
			Dictionary<object, TypedColumnMap> dictionary = new Dictionary<object, TypedColumnMap>((typeInfo.RootType.DiscriminatorMap == null) ? null : TrailingSpaceComparer.Instance);
			List<TypedColumnMap> list = new List<TypedColumnMap>();
			TypeInfo rootType = typeInfo.RootType;
			SimpleColumnMap simpleColumnMap = this.CreateTypeIdColumnMap(rootType.TypeIdProperty);
			if (TypeSemantics.IsComplexType(typeInfo.Type))
			{
				this.CreateComplexTypeColumnMap(rootType, name, null, dictionary, list);
			}
			else
			{
				this.CreateEntityColumnMap(rootType, name, null, dictionary, list, true);
			}
			TypedColumnMap typedColumnMap = null;
			foreach (TypedColumnMap typedColumnMap2 in list)
			{
				if (TypeSemantics.IsStructurallyEqual(typedColumnMap2.Type, typeInfo.Type))
				{
					typedColumnMap = typedColumnMap2;
					break;
				}
			}
			PlanCompiler.Assert(typedColumnMap != null, "Didn't find requested type in polymorphic type hierarchy?");
			return new SimplePolymorphicColumnMap(typeInfo.Type, name, typedColumnMap.Properties, simpleColumnMap, dictionary);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00071C04 File Offset: 0x0006FE04
		private RecordColumnMap CreateRecordColumnMap(TypeInfo typeInfo, string name)
		{
			PlanCompiler.Assert(typeInfo.Type.EdmType is RowType, "not RowType");
			SimpleColumnMap simpleColumnMap = null;
			if (typeInfo.HasNullSentinelProperty)
			{
				simpleColumnMap = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.NullSentinelProperty), "__NullSentinel");
			}
			ReadOnlyMetadataCollection<EdmProperty> properties = TypeHelpers.GetProperties(typeInfo.Type);
			ColumnMap[] array = new ColumnMap[properties.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember edmMember = properties[i];
				array[i] = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
			}
			return new RecordColumnMap(typeInfo.Type, name, array, simpleColumnMap);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		private RefColumnMap CreateRefColumnMap(TypeInfo typeInfo, string name)
		{
			SimpleColumnMap simpleColumnMap = null;
			if (typeInfo.HasEntitySetIdProperty)
			{
				simpleColumnMap = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.EntitySetIdProperty), "__EntitySetId");
			}
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(typeInfo.Type).ElementType;
			SimpleColumnMap[] array = new SimpleColumnMap[entityType.KeyMembers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember edmMember = entityType.KeyMembers[i];
				array[i] = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
			}
			EntityIdentity entityIdentity = this.CreateEntityIdentity(entityType, simpleColumnMap, array);
			return new RefColumnMap(typeInfo.Type, name, entityIdentity);
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00071D4C File Offset: 0x0006FF4C
		private SimpleColumnMap CreateSimpleColumnMap(TypeUsage type, string name)
		{
			Var nextVar = this.GetNextVar();
			return new VarRefColumnMap(type, name, nextVar);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00071D68 File Offset: 0x0006FF68
		private SimpleColumnMap CreateTypeIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__TypeId");
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x00071D7C File Offset: 0x0006FF7C
		private ColumnMap CreateStructuralColumnMap(TypeUsage type, string name)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			if (TypeSemantics.IsRowType(type))
			{
				return this.CreateRecordColumnMap(typeInfo, name);
			}
			if (TypeSemantics.IsReferenceType(type))
			{
				return this.CreateRefColumnMap(typeInfo, name);
			}
			if (typeInfo.HasTypeIdProperty)
			{
				return this.CreatePolymorphicColumnMap(typeInfo, name);
			}
			if (TypeSemantics.IsComplexType(type))
			{
				return this.CreateComplexTypeColumnMap(typeInfo, name, null, null, null);
			}
			if (TypeSemantics.IsEntityType(type))
			{
				return this.CreateEntityColumnMap(typeInfo, name, null, null, null, true);
			}
			throw new NotSupportedException(type.Identity);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00071E00 File Offset: 0x00070000
		private EntityIdentity CreateEntityIdentity(EntityType entityType, SimpleColumnMap entitySetIdColumnMap, SimpleColumnMap[] keyColumnMaps)
		{
			if (entitySetIdColumnMap != null)
			{
				return new DiscriminatedEntityIdentity(entitySetIdColumnMap, this.m_typeInfo.EntitySetIdToEntitySetMap, keyColumnMaps);
			}
			EntitySet entitySet = this.m_typeInfo.GetEntitySet(entityType);
			PlanCompiler.Assert(entitySet != null, "Expected non-null entitySet when no entity set ID is required. Entity type = " + ((entityType != null) ? entityType.ToString() : null));
			return new SimpleEntityIdentity(entitySet, keyColumnMaps);
		}

		// Token: 0x04000DA4 RID: 3492
		private readonly IEnumerator<Var> m_varList;

		// Token: 0x04000DA5 RID: 3493
		private readonly VarInfo m_varInfo;

		// Token: 0x04000DA6 RID: 3494
		private readonly VarRefColumnMap m_columnMap;

		// Token: 0x04000DA7 RID: 3495
		private readonly StructuredTypeInfo m_typeInfo;

		// Token: 0x04000DA8 RID: 3496
		private const string c_TypeIdColumnName = "__TypeId";

		// Token: 0x04000DA9 RID: 3497
		private const string c_EntitySetIdColumnName = "__EntitySetId";

		// Token: 0x04000DAA RID: 3498
		private const string c_NullSentinelColumnName = "__NullSentinel";
	}
}
