using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Security.Cryptography;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F9 RID: 1529
	internal static class MetadataHelper
	{
		// Token: 0x06004AA6 RID: 19110 RVA: 0x00108930 File Offset: 0x00106B30
		internal static bool TryGetFunctionImportReturnType<T>(EdmFunction functionImport, int resultSetIndex, out T returnType) where T : EdmType
		{
			T t;
			if (MetadataHelper.TryGetWrappedReturnEdmTypeFromFunctionImport<T>(functionImport, resultSetIndex, out t) && ((typeof(EntityType).Equals(typeof(T)) && t is EntityType) || (typeof(ComplexType).Equals(typeof(T)) && t is ComplexType) || (typeof(StructuralType).Equals(typeof(T)) && t is StructuralType) || (typeof(EdmType).Equals(typeof(T)) && t != null)))
			{
				returnType = t;
				return true;
			}
			returnType = default(T);
			return false;
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x001089F8 File Offset: 0x00106BF8
		private static bool TryGetWrappedReturnEdmTypeFromFunctionImport<T>(EdmFunction functionImport, int resultSetIndex, out T resultType) where T : EdmType
		{
			resultType = default(T);
			CollectionType collectionType;
			if (MetadataHelper.TryGetFunctionImportReturnCollectionType(functionImport, resultSetIndex, out collectionType))
			{
				resultType = collectionType.TypeUsage.EdmType as T;
				return true;
			}
			return false;
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00108A38 File Offset: 0x00106C38
		internal static bool TryGetFunctionImportReturnCollectionType(EdmFunction functionImport, int resultSetIndex, out CollectionType collectionType)
		{
			FunctionParameter returnParameter = MetadataHelper.GetReturnParameter(functionImport, resultSetIndex);
			if (returnParameter != null && returnParameter.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				collectionType = (CollectionType)returnParameter.TypeUsage.EdmType;
				return true;
			}
			collectionType = null;
			return false;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00108A7B File Offset: 0x00106C7B
		internal static FunctionParameter GetReturnParameter(EdmFunction functionImport, int resultSetIndex)
		{
			if (functionImport.ReturnParameters.Count <= resultSetIndex)
			{
				return null;
			}
			return functionImport.ReturnParameters[resultSetIndex];
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00108A99 File Offset: 0x00106C99
		internal static EdmFunction GetFunctionImport(string functionName, string defaultContainerName, MetadataWorkspace workspace, out string containerName, out string functionImportName)
		{
			CommandHelper.ParseFunctionImportCommandText(functionName, defaultContainerName, out containerName, out functionImportName);
			return CommandHelper.FindFunctionImport(workspace, containerName, functionImportName);
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00108AB0 File Offset: 0x00106CB0
		internal static EdmType GetAndCheckFunctionImportReturnType<TElement>(EdmFunction functionImport, int resultSetIndex, MetadataWorkspace workspace)
		{
			EdmType edmType;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, resultSetIndex, out edmType))
			{
				throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(functionImport);
			}
			MetadataHelper.CheckFunctionImportReturnType<TElement>(edmType, workspace);
			return edmType;
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00108AD8 File Offset: 0x00106CD8
		internal static void CheckFunctionImportReturnType<TElement>(EdmType expectedEdmType, MetadataWorkspace workspace)
		{
			EdmType edmType = expectedEdmType;
			bool flag;
			if (Helper.IsSpatialType(expectedEdmType, out flag))
			{
				edmType = PrimitiveType.GetEdmPrimitiveType(flag ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
			}
			EdmType edmType2;
			if (!workspace.TryDetermineCSpaceModelType<TElement>(out edmType2) || !edmType2.EdmEquals(edmType))
			{
				throw new InvalidOperationException(Strings.ObjectContext_ExecuteFunctionTypeMismatch(typeof(TElement).FullName, expectedEdmType.FullName));
			}
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00108B33 File Offset: 0x00106D33
		internal static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return ParameterDirection.Input;
			case ParameterMode.Out:
				return ParameterDirection.Output;
			case ParameterMode.InOut:
				return ParameterDirection.InputOutput;
			case ParameterMode.ReturnValue:
				return ParameterDirection.ReturnValue;
			default:
				return (ParameterDirection)0;
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00108B58 File Offset: 0x00106D58
		internal static bool DoesMemberExist(StructuralType type, EdmMember member)
		{
			using (ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = type.Members.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Equals(member))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00108BB4 File Offset: 0x00106DB4
		internal static bool IsNonRefSimpleMember(EdmMember member)
		{
			return member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00108BE0 File Offset: 0x00106DE0
		internal static bool HasDiscreteDomain(EdmType edmType)
		{
			PrimitiveType primitiveType = edmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00108C02 File Offset: 0x00106E02
		internal static EntityType GetEntityTypeForEnd(AssociationEndMember end)
		{
			return (EntityType)((RefType)end.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00108C1E File Offset: 0x00106E1E
		internal static EntitySet GetEntitySetAtEnd(AssociationSet associationSet, AssociationEndMember endMember)
		{
			return associationSet.AssociationSetEnds[endMember.Name].EntitySet;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00108C38 File Offset: 0x00106E38
		internal static AssociationEndMember GetOtherAssociationEnd(AssociationEndMember endMember)
		{
			ReadOnlyMetadataCollection<EdmMember> members = endMember.DeclaringType.Members;
			EdmMember edmMember = members[0];
			if (endMember != edmMember)
			{
				return (AssociationEndMember)edmMember;
			}
			return (AssociationEndMember)members[1];
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00108C70 File Offset: 0x00106E70
		internal static bool IsEveryOtherEndAtLeastOne(AssociationSet associationSet, AssociationEndMember member)
		{
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				if (!correspondingAssociationEndMember.Equals(member) && MetadataHelper.GetLowerBoundOfMultiplicity(correspondingAssociationEndMember.RelationshipMultiplicity) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00108CE0 File Offset: 0x00106EE0
		internal static bool IsAssociationValidForEntityType(AssociationSetEnd toEnd, EntityType type)
		{
			return MetadataHelper.GetEntityTypeForEnd(MetadataHelper.GetOppositeEnd(toEnd).CorrespondingAssociationEndMember).IsAssignableFrom(type);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00108CF8 File Offset: 0x00106EF8
		internal static AssociationSetEnd GetOppositeEnd(AssociationSetEnd end)
		{
			return end.ParentAssociationSet.AssociationSetEnds.Where((AssociationSetEnd e) => !e.EdmEquals(end)).Single<AssociationSetEnd>();
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00108D38 File Offset: 0x00106F38
		internal static bool IsComposable(EdmFunction function)
		{
			MetadataProperty metadataProperty;
			if (function.MetadataProperties.TryGetValue("IsComposableAttribute", false, out metadataProperty))
			{
				return (bool)metadataProperty.Value;
			}
			return !function.IsFunctionImport;
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00108D6F File Offset: 0x00106F6F
		internal static bool IsMemberNullable(EdmMember member)
		{
			return Helper.IsEdmProperty(member) && ((EdmProperty)member).Nullable;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00108D88 File Offset: 0x00106F88
		internal static IEnumerable<EntitySet> GetInfluencingEntitySetsForTable(EntitySet table, MetadataWorkspace workspace)
		{
			ItemCollection itemCollection = null;
			workspace.TryGetItemCollection(DataSpace.CSSpace, out itemCollection);
			Func<MappingFragment, bool> <>9__3;
			Func<TypeMapping, bool> <>9__2;
			return (from m in MappingMetadataHelper.GetEntityContainerMap((StorageMappingItemCollection)itemCollection, table.EntityContainer).EntitySetMaps.Where(delegate(EntitySetBaseMapping map)
				{
					IEnumerable<TypeMapping> typeMappings = map.TypeMappings;
					Func<TypeMapping, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = delegate(TypeMapping typeMap)
						{
							IEnumerable<MappingFragment> mappingFragments = typeMap.MappingFragments;
							Func<MappingFragment, bool> func2;
							if ((func2 = <>9__3) == null)
							{
								func2 = (<>9__3 = (MappingFragment mappingFrag) => mappingFrag.TableSet.EdmEquals(table));
							}
							return mappingFragments.Any(func2);
						});
					}
					return typeMappings.Any(func);
				})
				select m.Set).Cast<EntitySet>().Distinct<EntitySet>();
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00108E08 File Offset: 0x00107008
		internal static IEnumerable<EdmType> GetTypeAndSubtypesOf(EdmType type, MetadataWorkspace workspace, bool includeAbstractTypes)
		{
			return MetadataHelper.GetTypeAndSubtypesOf(type, workspace.GetItemCollection(DataSpace.CSpace), includeAbstractTypes);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00108E18 File Offset: 0x00107018
		internal static IEnumerable<EdmType> GetTypeAndSubtypesOf(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes)
		{
			if (Helper.IsRefType(type))
			{
				type = ((RefType)type).ElementType;
			}
			if (includeAbstractTypes || !type.Abstract)
			{
				yield return type;
			}
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf<EntityType>(type, itemCollection, includeAbstractTypes))
			{
				yield return edmType;
			}
			IEnumerator<EdmType> enumerator = null;
			foreach (EdmType edmType2 in MetadataHelper.GetTypeAndSubtypesOf<ComplexType>(type, itemCollection, includeAbstractTypes))
			{
				yield return edmType2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00108E36 File Offset: 0x00107036
		private static IEnumerable<EdmType> GetTypeAndSubtypesOf<T_EdmType>(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes) where T_EdmType : EdmType
		{
			T_EdmType specificType = type as T_EdmType;
			if (specificType != null)
			{
				IEnumerable<T_EdmType> items = itemCollection.GetItems<T_EdmType>();
				foreach (T_EdmType t_EdmType in items)
				{
					if (!specificType.Equals(t_EdmType) && Helper.IsSubtypeOf(t_EdmType, specificType) && (includeAbstractTypes || !t_EdmType.Abstract))
					{
						yield return t_EdmType;
					}
				}
				IEnumerator<T_EdmType> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00108E54 File Offset: 0x00107054
		internal static IEnumerable<EdmType> GetTypeAndParentTypesOf(EdmType type, bool includeAbstractTypes)
		{
			if (Helper.IsRefType(type))
			{
				type = ((RefType)type).ElementType;
			}
			for (EdmType specificType = type; specificType != null; specificType = specificType.BaseType as EntityType)
			{
				if (includeAbstractTypes || !specificType.Abstract)
				{
					yield return specificType;
				}
			}
			yield break;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00108E6C File Offset: 0x0010706C
		internal static Dictionary<EntityType, Set<EntityType>> BuildUndirectedGraphOfTypes(EdmItemCollection edmItemCollection)
		{
			Dictionary<EntityType, Set<EntityType>> dictionary = new Dictionary<EntityType, Set<EntityType>>();
			foreach (EntityType entityType in ((IEnumerable<EntityType>)edmItemCollection.GetItems<EntityType>()))
			{
				if (entityType.BaseType != null)
				{
					EntityType entityType2 = entityType.BaseType as EntityType;
					MetadataHelper.AddDirectedEdgeBetweenEntityTypes(dictionary, entityType, entityType2);
					MetadataHelper.AddDirectedEdgeBetweenEntityTypes(dictionary, entityType2, entityType);
				}
			}
			return dictionary;
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00108EE0 File Offset: 0x001070E0
		internal static bool IsParentOf(EntityType a, EntityType b)
		{
			for (EntityType entityType = b.BaseType as EntityType; entityType != null; entityType = entityType.BaseType as EntityType)
			{
				if (entityType.EdmEquals(a))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00108F18 File Offset: 0x00107118
		private static void AddDirectedEdgeBetweenEntityTypes(Dictionary<EntityType, Set<EntityType>> graph, EntityType a, EntityType b)
		{
			Set<EntityType> set;
			if (graph.ContainsKey(a))
			{
				set = graph[a];
			}
			else
			{
				set = new Set<EntityType>();
				graph.Add(a, set);
			}
			set.Add(b);
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00108F50 File Offset: 0x00107150
		internal static bool DoesEndKeySubsumeAssociationSetKey(AssociationSet assocSet, AssociationEndMember thisEnd, HashSet<Pair<EdmMember, EntityType>> associationkeys)
		{
			AssociationType elementType = assocSet.ElementType;
			EntityType thisEndsEntityType = (EntityType)((RefType)thisEnd.TypeUsage.EdmType).ElementType;
			HashSet<Pair<EdmMember, EntityType>> hashSet = new HashSet<Pair<EdmMember, EntityType>>(thisEndsEntityType.KeyMembers.Select((EdmMember edmMember) => new Pair<EdmMember, EntityType>(edmMember, thisEndsEntityType)));
			foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
			{
				IEnumerable<EdmMember> enumerable;
				EntityType entityType;
				if (thisEnd.Equals(referentialConstraint.ToRole))
				{
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.FromProperties);
					entityType = (EntityType)((RefType)referentialConstraint.FromRole.TypeUsage.EdmType).ElementType;
				}
				else
				{
					if (!thisEnd.Equals(referentialConstraint.FromRole))
					{
						continue;
					}
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties);
					entityType = (EntityType)((RefType)referentialConstraint.ToRole.TypeUsage.EdmType).ElementType;
				}
				foreach (EdmMember edmMember2 in enumerable)
				{
					associationkeys.Remove(new Pair<EdmMember, EntityType>(edmMember2, entityType));
				}
			}
			return associationkeys.IsSubsetOf(hashSet);
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001090B8 File Offset: 0x001072B8
		internal static bool DoesEndFormKey(AssociationSet associationSet, AssociationEndMember end)
		{
			foreach (EdmMember edmMember in associationSet.ElementType.Members)
			{
				AssociationEndMember associationEndMember = (AssociationEndMember)edmMember;
				if (!associationEndMember.Equals(end) && associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00109128 File Offset: 0x00107328
		internal static bool IsExtentAtSomeRelationshipEnd(AssociationSet relationshipSet, EntitySetBase extent)
		{
			return Helper.IsEntitySet(extent) && MetadataHelper.GetSomeEndForEntitySet(relationshipSet, extent) != null;
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x00109140 File Offset: 0x00107340
		internal static AssociationEndMember GetSomeEndForEntitySet(AssociationSet associationSet, EntitySetBase entitySet)
		{
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				if (associationSetEnd.EntitySet.Equals(entitySet))
				{
					return associationSetEnd.CorrespondingAssociationEndMember;
				}
			}
			return null;
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x001091A8 File Offset: 0x001073A8
		internal static List<AssociationSet> GetAssociationsForEntitySets(EntitySet entitySet1, EntitySet entitySet2)
		{
			List<AssociationSet> list = new List<AssociationSet>();
			foreach (EntitySetBase entitySetBase in entitySet1.EntityContainer.BaseEntitySets)
			{
				if (Helper.IsRelationshipSet(entitySetBase))
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					if (MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet1) && MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet2))
					{
						list.Add(associationSet);
					}
				}
			}
			return list;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00109228 File Offset: 0x00107428
		internal static List<AssociationSet> GetAssociationsForEntitySet(EntitySetBase entitySet)
		{
			List<AssociationSet> list = new List<AssociationSet>();
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				if (Helper.IsRelationshipSet(entitySetBase))
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					if (MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet))
					{
						list.Add(associationSet);
					}
				}
			}
			return list;
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x001092A0 File Offset: 0x001074A0
		internal static bool IsSuperTypeOf(EdmType superType, EdmType subType)
		{
			for (EdmType edmType = subType; edmType != null; edmType = edmType.BaseType)
			{
				if (edmType.Equals(superType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x001092C7 File Offset: 0x001074C7
		internal static bool IsPartOfEntityTypeKey(EdmMember member)
		{
			return Helper.IsEntityType(member.DeclaringType) && Helper.IsEdmProperty(member) && ((EntityType)member.DeclaringType).KeyMembers.Contains(member);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001092F6 File Offset: 0x001074F6
		internal static TypeUsage GetElementType(TypeUsage typeUsage)
		{
			if (BuiltInTypeKind.CollectionType == typeUsage.EdmType.BuiltInTypeKind)
			{
				return MetadataHelper.GetElementType(((CollectionType)typeUsage.EdmType).TypeUsage);
			}
			return typeUsage;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0010931D File Offset: 0x0010751D
		internal static int GetLowerBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.Many || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0010932C File Offset: 0x0010752C
		internal static int? GetUpperBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.One || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return new int?(1);
			}
			return null;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00109350 File Offset: 0x00107550
		internal static Set<EdmMember> GetConcurrencyMembersForTypeHierarchy(EntityTypeBase superType, EdmItemCollection edmItemCollection)
		{
			Set<EdmMember> set = new Set<EdmMember>();
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf(superType, edmItemCollection, true))
			{
				foreach (EdmMember edmMember in ((StructuralType)edmType).Members)
				{
					if (MetadataHelper.GetConcurrencyMode(edmMember) == ConcurrencyMode.Fixed)
					{
						set.Add(edmMember);
					}
				}
			}
			return set;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x001093F0 File Offset: 0x001075F0
		internal static ConcurrencyMode GetConcurrencyMode(EdmMember member)
		{
			return MetadataHelper.GetConcurrencyMode(member.TypeUsage);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00109400 File Offset: 0x00107600
		internal static ConcurrencyMode GetConcurrencyMode(TypeUsage typeUsage)
		{
			Facet facet;
			if (typeUsage.Facets.TryGetValue("ConcurrencyMode", false, out facet) && facet.Value != null)
			{
				return (ConcurrencyMode)facet.Value;
			}
			return ConcurrencyMode.None;
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00109438 File Offset: 0x00107638
		internal static StoreGeneratedPattern GetStoreGeneratedPattern(EdmMember member)
		{
			Facet facet;
			if (member.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null)
			{
				return (StoreGeneratedPattern)facet.Value;
			}
			return StoreGeneratedPattern.None;
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00109474 File Offset: 0x00107674
		internal static bool CheckIfAllErrorsAreWarnings(IList<EdmSchemaError> schemaErrors)
		{
			int count = schemaErrors.Count;
			for (int i = 0; i < count; i++)
			{
				if (schemaErrors[i].Severity != EdmSchemaErrorSeverity.Warning)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x001094A8 File Offset: 0x001076A8
		internal static HashAlgorithm CreateMetadataHashAlgorithm(double schemaVersion)
		{
			HashAlgorithm hashAlgorithm;
			if (schemaVersion < 2.0)
			{
				hashAlgorithm = new MD5CryptoServiceProvider();
			}
			else
			{
				hashAlgorithm = MetadataHelper.CreateSHA256HashAlgorithm();
			}
			return hashAlgorithm;
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001094D0 File Offset: 0x001076D0
		internal static SHA256 CreateSHA256HashAlgorithm()
		{
			SHA256 sha;
			try
			{
				sha = new SHA256CryptoServiceProvider();
			}
			catch (PlatformNotSupportedException)
			{
				sha = new SHA256Managed();
			}
			return sha;
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00109500 File Offset: 0x00107700
		internal static TypeUsage ConvertStoreTypeUsageToEdmTypeUsage(TypeUsage storeTypeUsage)
		{
			return storeTypeUsage.ModelTypeUsage.ShallowCopy(FacetValues.NullFacetValues);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00109512 File Offset: 0x00107712
		internal static byte GetPrecision(this TypeUsage type)
		{
			return type.GetFacetValue("Precision");
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x0010951F File Offset: 0x0010771F
		internal static byte GetScale(this TypeUsage type)
		{
			return type.GetFacetValue("Scale");
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x0010952C File Offset: 0x0010772C
		internal static int GetMaxLength(this TypeUsage type)
		{
			return type.GetFacetValue("MaxLength");
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00109539 File Offset: 0x00107739
		internal static T GetFacetValue<T>(this TypeUsage type, string facetName)
		{
			return (T)((object)type.Facets[facetName].Value);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00109551 File Offset: 0x00107751
		internal static NavigationPropertyAccessor GetNavigationPropertyAccessor(EntityType sourceEntityType, AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			return MetadataHelper.GetNavigationPropertyAccessor(sourceEntityType, sourceMember.DeclaringType.FullName, sourceMember.Name, targetMember.Name);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00109570 File Offset: 0x00107770
		internal static NavigationPropertyAccessor GetNavigationPropertyAccessor(EntityType entityType, string relationshipType, string fromName, string toName)
		{
			NavigationProperty navigationProperty;
			if (entityType.TryGetNavigationProperty(relationshipType, fromName, toName, out navigationProperty))
			{
				return navigationProperty.Accessor;
			}
			return NavigationPropertyAccessor.NoNavigationProperty;
		}
	}
}
