using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D9 RID: 1497
	internal class ViewLoader
	{
		// Token: 0x06004854 RID: 18516 RVA: 0x0010171C File Offset: 0x000FF91C
		internal ViewLoader(StorageMappingItemCollection mappingCollection)
		{
			this.m_mappingCollection = mappingCollection;
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0010177D File Offset: 0x000FF97D
		internal ModificationFunctionMappingTranslator GetFunctionMappingTranslator(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, ModificationFunctionMappingTranslator>(extent, workspace, this.m_functionMappingTranslators, extent);
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x0010178E File Offset: 0x000FF98E
		internal Set<EntitySet> GetAffectedTables(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, Set<EntitySet>>(extent, workspace, this.m_affectedTables, extent);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0010179F File Offset: 0x000FF99F
		internal AssociationSetMetadata GetAssociationSetMetadata(AssociationSet associationSet, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<AssociationSet, AssociationSetMetadata>(associationSet, workspace, this.m_associationSetMetadata, associationSet);
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x001017B0 File Offset: 0x000FF9B0
		internal bool IsServerGen(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_serverGenProperties, member);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x001017C1 File Offset: 0x000FF9C1
		internal bool IsNullConditionMember(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_isNullConditionProperties, member);
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x001017D4 File Offset: 0x000FF9D4
		private T_Value SyncGetValue<T_Key, T_Value>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Dictionary<T_Key, T_Value> dictionary, T_Key key)
		{
			return this.SyncInitializeEntitySet<T_Key, T_Value>(entitySetBase, workspace, (T_Key k) => dictionary[k], key);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00101804 File Offset: 0x000FFA04
		private bool SyncContains<T_Element>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Set<T_Element> set, T_Element element)
		{
			return this.SyncInitializeEntitySet<T_Element, bool>(entitySetBase, workspace, new Func<T_Element, bool>(set.Contains), element);
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x0010181C File Offset: 0x000FFA1C
		private TResult SyncInitializeEntitySet<TArg, TResult>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Func<TArg, TResult> evaluate, TArg arg)
		{
			this.m_readerWriterLock.EnterReadLock();
			try
			{
				if (this.m_affectedTables.ContainsKey(entitySetBase))
				{
					return evaluate(arg);
				}
			}
			finally
			{
				this.m_readerWriterLock.ExitReadLock();
			}
			this.m_readerWriterLock.EnterWriteLock();
			TResult tresult;
			try
			{
				if (this.m_affectedTables.ContainsKey(entitySetBase))
				{
					tresult = evaluate(arg);
				}
				else
				{
					this.InitializeEntitySet(entitySetBase, workspace);
					tresult = evaluate(arg);
				}
			}
			finally
			{
				this.m_readerWriterLock.ExitWriteLock();
			}
			return tresult;
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x001018BC File Offset: 0x000FFABC
		private void InitializeEntitySet(EntitySetBase entitySetBase, MetadataWorkspace workspace)
		{
			EntityContainerMapping entityContainerMapping = (EntityContainerMapping)this.m_mappingCollection.GetMap(entitySetBase.EntityContainer);
			if (entityContainerMapping.HasViews)
			{
				this.m_mappingCollection.GetGeneratedView(entitySetBase, workspace);
			}
			Set<EntitySet> set = new Set<EntitySet>();
			if (entityContainerMapping != null)
			{
				Set<EdmMember> set2 = new Set<EdmMember>();
				EntitySetBaseMapping entitySetBaseMapping;
				if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					entitySetBaseMapping = entityContainerMapping.GetEntitySetMapping(entitySetBase.Name);
					this.m_serverGenProperties.Unite(ViewLoader.GetMembersWithResultBinding((EntitySetMapping)entitySetBaseMapping));
				}
				else
				{
					if (entitySetBase.BuiltInTypeKind != BuiltInTypeKind.AssociationSet)
					{
						throw new NotSupportedException();
					}
					entitySetBaseMapping = entityContainerMapping.GetAssociationSetMapping(entitySetBase.Name);
				}
				foreach (MappingFragment mappingFragment in ViewLoader.GetMappingFragments(entitySetBaseMapping))
				{
					set.Add(mappingFragment.TableSet);
					this.m_serverGenProperties.AddRange(ViewLoader.FindServerGenMembers(mappingFragment));
					set2.AddRange(ViewLoader.FindIsNullConditionColumns(mappingFragment));
				}
				if (0 < set2.Count)
				{
					foreach (MappingFragment mappingFragment2 in ViewLoader.GetMappingFragments(entitySetBaseMapping))
					{
						this.m_isNullConditionProperties.AddRange(ViewLoader.FindPropertiesMappedToColumns(set2, mappingFragment2));
					}
				}
			}
			this.m_affectedTables.Add(entitySetBase, set.MakeReadOnly());
			this.InitializeFunctionMappingTranslators(entitySetBase, entityContainerMapping);
			if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				AssociationSet associationSet = (AssociationSet)entitySetBase;
				if (!this.m_associationSetMetadata.ContainsKey(associationSet))
				{
					this.m_associationSetMetadata.Add(associationSet, new AssociationSetMetadata(this.m_affectedTables[associationSet], associationSet, workspace));
				}
			}
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00101A74 File Offset: 0x000FFC74
		private static IEnumerable<EdmMember> GetMembersWithResultBinding(EntitySetMapping entitySetMapping)
		{
			foreach (EntityTypeModificationFunctionMapping typeFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				if (typeFunctionMapping.InsertFunctionMapping != null && typeFunctionMapping.InsertFunctionMapping.ResultBindings != null)
				{
					foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in typeFunctionMapping.InsertFunctionMapping.ResultBindings)
					{
						yield return modificationFunctionResultBinding.Property;
					}
					IEnumerator<ModificationFunctionResultBinding> enumerator2 = null;
				}
				if (typeFunctionMapping.UpdateFunctionMapping != null && typeFunctionMapping.UpdateFunctionMapping.ResultBindings != null)
				{
					foreach (ModificationFunctionResultBinding modificationFunctionResultBinding2 in typeFunctionMapping.UpdateFunctionMapping.ResultBindings)
					{
						yield return modificationFunctionResultBinding2.Property;
					}
					IEnumerator<ModificationFunctionResultBinding> enumerator2 = null;
				}
				typeFunctionMapping = null;
			}
			IEnumerator<EntityTypeModificationFunctionMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x00101A84 File Offset: 0x000FFC84
		private void InitializeFunctionMappingTranslators(EntitySetBase entitySetBase, EntityContainerMapping mapping)
		{
			KeyToListMap<AssociationSet, AssociationEndMember> keyToListMap = new KeyToListMap<AssociationSet, AssociationEndMember>(EqualityComparer<AssociationSet>.Default);
			if (!this.m_functionMappingTranslators.ContainsKey(entitySetBase))
			{
				foreach (EntitySetBaseMapping entitySetBaseMapping in mapping.EntitySetMaps)
				{
					EntitySetMapping entitySetMapping = (EntitySetMapping)entitySetBaseMapping;
					if (0 < entitySetMapping.ModificationFunctionMappings.Count)
					{
						this.m_functionMappingTranslators.Add(entitySetMapping.Set, ModificationFunctionMappingTranslator.CreateEntitySetTranslator(entitySetMapping));
						using (IEnumerator<AssociationSetEnd> enumerator2 = entitySetMapping.ImplicitlyMappedAssociationSetEnds.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AssociationSetEnd associationSetEnd = enumerator2.Current;
								AssociationSet parentAssociationSet = associationSetEnd.ParentAssociationSet;
								if (!this.m_functionMappingTranslators.ContainsKey(parentAssociationSet))
								{
									this.m_functionMappingTranslators.Add(parentAssociationSet, ModificationFunctionMappingTranslator.CreateAssociationSetTranslator(null));
								}
								AssociationSetEnd oppositeEnd = MetadataHelper.GetOppositeEnd(associationSetEnd);
								keyToListMap.Add(parentAssociationSet, oppositeEnd.CorrespondingAssociationEndMember);
							}
							continue;
						}
					}
					this.m_functionMappingTranslators.Add(entitySetMapping.Set, null);
				}
				foreach (EntitySetBaseMapping entitySetBaseMapping2 in mapping.RelationshipSetMaps)
				{
					AssociationSetMapping associationSetMapping = (AssociationSetMapping)entitySetBaseMapping2;
					if (associationSetMapping.ModificationFunctionMapping != null)
					{
						AssociationSet associationSet = (AssociationSet)associationSetMapping.Set;
						this.m_functionMappingTranslators.Add(associationSet, ModificationFunctionMappingTranslator.CreateAssociationSetTranslator(associationSetMapping));
						keyToListMap.AddRange(associationSet, Enumerable.Empty<AssociationEndMember>());
					}
					else if (!this.m_functionMappingTranslators.ContainsKey(associationSetMapping.Set))
					{
						this.m_functionMappingTranslators.Add(associationSetMapping.Set, null);
					}
				}
			}
			foreach (AssociationSet associationSet2 in keyToListMap.Keys)
			{
				this.m_associationSetMetadata.Add(associationSet2, new AssociationSetMetadata(keyToListMap.EnumerateValues(associationSet2)));
			}
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x00101C90 File Offset: 0x000FFE90
		private static IEnumerable<EdmMember> FindServerGenMembers(MappingFragment mappingFragment)
		{
			foreach (ScalarPropertyMapping scalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ScalarPropertyMapping>())
			{
				if (MetadataHelper.GetStoreGeneratedPattern(scalarPropertyMapping.Column) != StoreGeneratedPattern.None)
				{
					yield return scalarPropertyMapping.Property;
				}
			}
			IEnumerator<ScalarPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00101CA0 File Offset: 0x000FFEA0
		private static IEnumerable<EdmMember> FindIsNullConditionColumns(MappingFragment mappingFragment)
		{
			foreach (ConditionPropertyMapping conditionPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ConditionPropertyMapping>())
			{
				if (conditionPropertyMapping.Column != null && conditionPropertyMapping.IsNull != null)
				{
					yield return conditionPropertyMapping.Column;
				}
			}
			IEnumerator<ConditionPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00101CB0 File Offset: 0x000FFEB0
		private static IEnumerable<EdmMember> FindPropertiesMappedToColumns(Set<EdmMember> columns, MappingFragment mappingFragment)
		{
			foreach (ScalarPropertyMapping scalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ScalarPropertyMapping>())
			{
				if (columns.Contains(scalarPropertyMapping.Column))
				{
					yield return scalarPropertyMapping.Property;
				}
			}
			IEnumerator<ScalarPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00101CC7 File Offset: 0x000FFEC7
		private static IEnumerable<MappingFragment> GetMappingFragments(EntitySetBaseMapping setMapping)
		{
			foreach (TypeMapping typeMapping in setMapping.TypeMappings)
			{
				foreach (MappingFragment mappingFragment in typeMapping.MappingFragments)
				{
					yield return mappingFragment;
				}
				IEnumerator<MappingFragment> enumerator2 = null;
			}
			IEnumerator<TypeMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x00101CD7 File Offset: 0x000FFED7
		private static IEnumerable<PropertyMapping> FlattenPropertyMappings(ReadOnlyCollection<PropertyMapping> propertyMappings)
		{
			foreach (PropertyMapping propertyMapping in propertyMappings)
			{
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (complexPropertyMapping != null)
				{
					foreach (ComplexTypeMapping complexTypeMapping in complexPropertyMapping.TypeMappings)
					{
						foreach (PropertyMapping propertyMapping2 in ViewLoader.FlattenPropertyMappings(complexTypeMapping.AllProperties))
						{
							yield return propertyMapping2;
						}
						IEnumerator<PropertyMapping> enumerator3 = null;
					}
					IEnumerator<ComplexTypeMapping> enumerator2 = null;
				}
				else
				{
					yield return propertyMapping;
				}
			}
			IEnumerator<PropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040019AC RID: 6572
		private readonly StorageMappingItemCollection m_mappingCollection;

		// Token: 0x040019AD RID: 6573
		private readonly Dictionary<AssociationSet, AssociationSetMetadata> m_associationSetMetadata = new Dictionary<AssociationSet, AssociationSetMetadata>();

		// Token: 0x040019AE RID: 6574
		private readonly Dictionary<EntitySetBase, Set<EntitySet>> m_affectedTables = new Dictionary<EntitySetBase, Set<EntitySet>>();

		// Token: 0x040019AF RID: 6575
		private readonly Set<EdmMember> m_serverGenProperties = new Set<EdmMember>();

		// Token: 0x040019B0 RID: 6576
		private readonly Set<EdmMember> m_isNullConditionProperties = new Set<EdmMember>();

		// Token: 0x040019B1 RID: 6577
		private readonly Dictionary<EntitySetBase, ModificationFunctionMappingTranslator> m_functionMappingTranslators = new Dictionary<EntitySetBase, ModificationFunctionMappingTranslator>(EqualityComparer<EntitySetBase>.Default);

		// Token: 0x040019B2 RID: 6578
		private readonly ReaderWriterLockSlim m_readerWriterLock = new ReaderWriterLockSlim();
	}
}
