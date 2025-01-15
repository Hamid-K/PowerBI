using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000CF RID: 207
	internal class EdmModelDiffer
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x00021C3C File Offset: 0x0001FE3C
		public ICollection<MigrationOperation> Diff(XDocument sourceModel, XDocument targetModel, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator = null, MigrationSqlGenerator migrationSqlGenerator = null, string sourceModelVersion = null, string targetModelVersion = null)
		{
			if (sourceModel == targetModel || XNode.DeepEquals(sourceModel, targetModel))
			{
				return new MigrationOperation[0];
			}
			DbProviderInfo dbProviderInfo;
			StorageMappingItemCollection storageMappingItemCollection = sourceModel.GetStorageMappingItemCollection(out dbProviderInfo);
			EdmModelDiffer.ModelMetadata modelMetadata = new EdmModelDiffer.ModelMetadata
			{
				EdmItemCollection = storageMappingItemCollection.EdmItemCollection,
				StoreItemCollection = storageMappingItemCollection.StoreItemCollection,
				StoreEntityContainer = storageMappingItemCollection.StoreItemCollection.GetItems<EntityContainer>().Single<EntityContainer>(),
				EntityContainerMapping = storageMappingItemCollection.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(),
				ProviderManifest = EdmModelDiffer.GetProviderManifest(dbProviderInfo),
				ProviderInfo = dbProviderInfo
			};
			storageMappingItemCollection = targetModel.GetStorageMappingItemCollection(out dbProviderInfo);
			EdmModelDiffer.ModelMetadata modelMetadata2 = new EdmModelDiffer.ModelMetadata
			{
				EdmItemCollection = storageMappingItemCollection.EdmItemCollection,
				StoreItemCollection = storageMappingItemCollection.StoreItemCollection,
				StoreEntityContainer = storageMappingItemCollection.StoreItemCollection.GetItems<EntityContainer>().Single<EntityContainer>(),
				EntityContainerMapping = storageMappingItemCollection.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(),
				ProviderManifest = EdmModelDiffer.GetProviderManifest(dbProviderInfo),
				ProviderInfo = dbProviderInfo
			};
			return this.Diff(modelMetadata, modelMetadata2, modificationCommandTreeGenerator, migrationSqlGenerator, sourceModelVersion, targetModelVersion);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00021D30 File Offset: 0x0001FF30
		private ICollection<MigrationOperation> Diff(EdmModelDiffer.ModelMetadata source, EdmModelDiffer.ModelMetadata target, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string sourceModelVersion = null, string targetModelVersion = null)
		{
			this._source = source;
			this._target = target;
			List<Tuple<EntityType, EntityType>> list = this.FindEntityTypePairs().ToList<Tuple<EntityType, EntityType>>();
			List<Tuple<MappingFragment, MappingFragment>> list2 = this.FindMappingFragmentPairs(list).ToList<Tuple<MappingFragment, MappingFragment>>();
			List<Tuple<AssociationType, AssociationType>> list3 = this.FindAssociationTypePairs(list).ToList<Tuple<AssociationType, AssociationType>>();
			List<Tuple<EntitySet, EntitySet>> list4 = this.FindTablePairs(list2, list3).ToList<Tuple<EntitySet, EntitySet>>();
			list3.AddRange(this.FindStoreOnlyAssociationTypePairs(list3, list4));
			IList<RenameTableOperation> list5 = EdmModelDiffer.FindRenamedTables(list4).ToList<RenameTableOperation>();
			List<RenameColumnOperation> list6 = this.FindRenamedColumns(list2, list3).ToList<RenameColumnOperation>();
			List<AddColumnOperation> list7 = this.FindAddedColumns(list4, list6).ToList<AddColumnOperation>();
			List<DropColumnOperation> list8 = this.FindDroppedColumns(list4, list6).ToList<DropColumnOperation>();
			List<AlterColumnOperation> list9 = this.FindAlteredColumns(list4, list6).ToList<AlterColumnOperation>();
			List<DropColumnOperation> list10 = this.FindOrphanedColumns(list4, list6).ToList<DropColumnOperation>();
			List<MoveTableOperation> list11 = this.FindMovedTables(list4).ToList<MoveTableOperation>();
			List<CreateTableOperation> list12 = this.FindAddedTables(list4).ToList<CreateTableOperation>();
			List<DropTableOperation> list13 = this.FindDroppedTables(list4).ToList<DropTableOperation>();
			List<AlterTableOperation> list14 = this.FindAlteredTables(list4).ToList<AlterTableOperation>();
			List<MigrationOperation> list15 = this.FindAlteredPrimaryKeys(list4, list6, list9).ToList<MigrationOperation>();
			List<AddForeignKeyOperation> list16 = this.FindAddedForeignKeys(list3, list6).Concat(list15.OfType<AddForeignKeyOperation>()).ToList<AddForeignKeyOperation>();
			List<DropForeignKeyOperation> list17 = this.FindDroppedForeignKeys(list3, list6).Concat(list15.OfType<DropForeignKeyOperation>()).ToList<DropForeignKeyOperation>();
			List<CreateProcedureOperation> list18 = this.FindAddedModificationFunctions(modificationCommandTreeGenerator, migrationSqlGenerator).ToList<CreateProcedureOperation>();
			List<AlterProcedureOperation> list19 = this.FindAlteredModificationFunctions(modificationCommandTreeGenerator, migrationSqlGenerator).ToList<AlterProcedureOperation>();
			List<DropProcedureOperation> list20 = this.FindDroppedModificationFunctions().ToList<DropProcedureOperation>();
			List<RenameProcedureOperation> list21 = this.FindRenamedModificationFunctions().ToList<RenameProcedureOperation>();
			List<MoveProcedureOperation> list22 = this.FindMovedModificationFunctions().ToList<MoveProcedureOperation>();
			List<ConsolidatedIndex> list23 = ((string.IsNullOrWhiteSpace(sourceModelVersion) || string.Compare(sourceModelVersion.Substring(0, 3), "6.1", StringComparison.Ordinal) >= 0) ? this.FindSourceIndexes(list4) : EdmModelDiffer.BuildLegacyIndexes(source)).ToList<ConsolidatedIndex>();
			List<ConsolidatedIndex> list24 = ((string.IsNullOrWhiteSpace(targetModelVersion) || string.Compare(targetModelVersion.Substring(0, 3), "6.1", StringComparison.Ordinal) >= 0) ? this.FindTargetIndexes() : EdmModelDiffer.BuildLegacyIndexes(target)).ToList<ConsolidatedIndex>();
			List<CreateIndexOperation> list25 = EdmModelDiffer.FindAddedIndexes(list23, list24, list9, list6).ToList<CreateIndexOperation>();
			List<DropIndexOperation> list26 = EdmModelDiffer.FindDroppedIndexes(list23, list24, list9, list6).ToList<DropIndexOperation>();
			List<RenameIndexOperation> list27 = EdmModelDiffer.FindRenamedIndexes(list25, list26, list9, list6).ToList<RenameIndexOperation>();
			return EdmModelDiffer.HandleTransitiveRenameDependencies(list5).Concat(list11).Concat(list17.Distinct(EdmModelDiffer._foreignKeyEqualityComparer))
				.Concat(list26.Distinct(EdmModelDiffer._indexEqualityComparer))
				.Concat(list10)
				.Concat(EdmModelDiffer.HandleTransitiveRenameDependencies(list6))
				.Concat(EdmModelDiffer.HandleTransitiveRenameDependencies(list27))
				.Concat(list15.OfType<DropPrimaryKeyOperation>())
				.Concat(list12)
				.Concat(list14)
				.Concat(list7)
				.Concat(list9)
				.Concat(list15.OfType<AddPrimaryKeyOperation>())
				.Concat(list25.Distinct(EdmModelDiffer._indexEqualityComparer))
				.Concat(list16.Distinct(EdmModelDiffer._foreignKeyEqualityComparer))
				.Concat(list8)
				.Concat(list13)
				.Concat(list18)
				.Concat(list22)
				.Concat(list21)
				.Concat(list19)
				.Concat(list20)
				.ToList<MigrationOperation>();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002202B File Offset: 0x0002022B
		private static IEnumerable<ConsolidatedIndex> BuildLegacyIndexes(EdmModelDiffer.ModelMetadata modelMetadata)
		{
			using (IEnumerator<AssociationType> enumerator = modelMetadata.StoreItemCollection.GetItems<AssociationType>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AssociationType associationType = enumerator.Current;
					string text = IndexOperation.BuildDefaultName(associationType.Constraint.ToProperties.Select((EdmProperty p) => p.Name));
					string schemaQualifiedName = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationType.Constraint.DependentEnd.GetEntityType()));
					ReadOnlyMetadataCollection<EdmProperty> toProperties = associationType.Constraint.ToProperties;
					ConsolidatedIndex consolidatedIndex;
					if (toProperties.Count > 0)
					{
						consolidatedIndex = new ConsolidatedIndex(schemaQualifiedName, toProperties[0].Name, new IndexAttribute(text, 0));
						for (int i = 1; i < toProperties.Count; i++)
						{
							consolidatedIndex.Add(toProperties[i].Name, new IndexAttribute(text, i));
						}
					}
					else
					{
						consolidatedIndex = new ConsolidatedIndex(schemaQualifiedName, new IndexAttribute(text));
					}
					yield return consolidatedIndex;
				}
			}
			IEnumerator<AssociationType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002203C File Offset: 0x0002023C
		private IEnumerable<Tuple<EntityType, EntityType>> FindEntityTypePairs()
		{
			List<Tuple<EntityType, EntityType>> list = (from et1 in this._source.EdmItemCollection.GetItems<EntityType>()
				from et2 in this._target.EdmItemCollection.GetItems<EntityType>()
				where et1.Name.EqualsOrdinal(et2.Name)
				select Tuple.Create<EntityType, EntityType>(et1, et2)).ToList<Tuple<EntityType, EntityType>>();
			List<EntityType> list2 = list.Select((Tuple<EntityType, EntityType> t) => t.Item1).ToList<EntityType>();
			List<EntityType> list3 = this._source.EdmItemCollection.GetItems<EntityType>().Except(list2).ToList<EntityType>();
			List<EntityType> list4 = list.Select((Tuple<EntityType, EntityType> t) => t.Item2).ToList<EntityType>();
			List<EntityType> targetRemainingEntities = this._target.EdmItemCollection.GetItems<EntityType>().Except(list4).ToList<EntityType>();
			return list.Concat(from et1 in list3
				from et2 in targetRemainingEntities
				where EdmModelDiffer.FuzzyMatchEntities(et1, et2)
				select Tuple.Create<EntityType, EntityType>(et1, et2));
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000221F4 File Offset: 0x000203F4
		private static bool FuzzyMatchEntities(EntityType entityType1, EntityType entityType2)
		{
			if (!entityType1.KeyMembers.SequenceEqual(entityType2.KeyMembers, new DynamicEqualityComparer<EdmMember>((EdmMember m1, EdmMember m2) => m1.EdmEquals(m2))))
			{
				return false;
			}
			if ((entityType1.BaseType != null && entityType2.BaseType == null) || (entityType1.BaseType == null && entityType2.BaseType != null))
			{
				return false;
			}
			return (double)((float)(from m1 in entityType1.DeclaredMembers
				from m2 in entityType2.DeclaredMembers
				where m1.EdmEquals(m2)
				select 1).Count<int>() * 2f / (float)(entityType1.DeclaredMembers.Count + entityType2.DeclaredMembers.Count)) > 0.8;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0002232C File Offset: 0x0002052C
		private static bool SourceAndTargetMatch(EntityType sourceEntityType, EntityTypeMapping sourceEntityTypeMapping, EntityType targetEntityType, EntityTypeMapping targetEntityTypeMapping)
		{
			if (sourceEntityTypeMapping.EntityType != null && targetEntityTypeMapping.EntityType != null)
			{
				if (sourceEntityType == sourceEntityTypeMapping.EntityType && targetEntityType == targetEntityTypeMapping.EntityType)
				{
					return true;
				}
			}
			else
			{
				ReadOnlyCollection<EntityTypeBase> isOfTypes = sourceEntityTypeMapping.IsOfTypes;
				if (isOfTypes.Contains(sourceEntityType))
				{
					ReadOnlyCollection<EntityTypeBase> isOfTypes2 = targetEntityTypeMapping.IsOfTypes;
					if (isOfTypes2.Contains(targetEntityType))
					{
						IEnumerable<string> enumerable = from et in isOfTypes.Except(new EntityType[] { sourceEntityType })
							select et.Name;
						IEnumerable<string> enumerable2 = from et in isOfTypes2.Except(new EntityType[] { targetEntityType })
							select et.Name;
						if (enumerable.SequenceEqual(enumerable2))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000223FC File Offset: 0x000205FC
		private static bool MappingTypesAreIdentical(EntityTypeMapping sourceEntityTypeMapping, EntityTypeMapping targetEntityTypeMapping)
		{
			EdmType edmType = sourceEntityTypeMapping.EntityType ?? sourceEntityTypeMapping.IsOfTypes.First<EntityTypeBase>();
			EntityTypeBase entityTypeBase = targetEntityTypeMapping.EntityType ?? targetEntityTypeMapping.IsOfTypes.First<EntityTypeBase>();
			return edmType.FullName == entityTypeBase.FullName;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00022444 File Offset: 0x00020644
		private IEnumerable<Tuple<MappingFragment, MappingFragment>> FindMappingFragmentPairs(ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			List<EntityTypeMapping> targetEntityTypeMappings = this._target.EntityContainerMapping.EntitySetMappings.SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).ToList<EntityTypeMapping>();
			IEnumerable<EntityTypeMapping> enumerable = this._source.EntityContainerMapping.EntitySetMappings.SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings);
			List<EntityTypeMapping> matchedTargets = new List<EntityTypeMapping>();
			using (IEnumerator<EntityTypeMapping> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityTypeMapping etm1 = enumerator.Current;
					using (List<EntityTypeMapping>.Enumerator enumerator2 = targetEntityTypeMappings.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EntityTypeMapping etm2 = enumerator2.Current;
							if (!matchedTargets.Contains(etm2))
							{
								bool flag = entityTypePairs.Any((Tuple<EntityType, EntityType> t) => EdmModelDiffer.SourceAndTargetMatch(t.Item1, etm1, t.Item2, etm2));
								if (!flag)
								{
									flag = EdmModelDiffer.MappingTypesAreIdentical(etm1, etm2);
								}
								if (flag)
								{
									matchedTargets.Add(etm2);
									foreach (Tuple<MappingFragment, MappingFragment> tuple in etm1.MappingFragments.Zip(etm2.MappingFragments, new Func<MappingFragment, MappingFragment, Tuple<MappingFragment, MappingFragment>>(Tuple.Create<MappingFragment, MappingFragment>)))
									{
										yield return tuple;
									}
									IEnumerator<Tuple<MappingFragment, MappingFragment>> enumerator3 = null;
									break;
								}
							}
						}
					}
					List<EntityTypeMapping>.Enumerator enumerator2 = default(List<EntityTypeMapping>.Enumerator);
				}
			}
			IEnumerator<EntityTypeMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0002245C File Offset: 0x0002065C
		private IEnumerable<Tuple<AssociationType, AssociationType>> FindAssociationTypePairs(ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			List<Tuple<AssociationType, AssociationType>> list = (from ets in entityTypePairs
				from np1 in ets.Item1.NavigationProperties
				from np2 in ets.Item2.NavigationProperties
				where np1.Name.EqualsIgnoreCase(np2.Name)
				from t in this.GetStoreAssociationTypePairs(np1.Association, np2.Association, entityTypePairs)
				select t).Distinct<Tuple<AssociationType, AssociationType>>().ToList<Tuple<AssociationType, AssociationType>>();
			List<AssociationType> list2 = this._source.StoreItemCollection.GetItems<AssociationType>().Except(list.Select((Tuple<AssociationType, AssociationType> t) => t.Item1)).ToList<AssociationType>();
			List<AssociationType> targetRemainingAssociationTypes = this._target.StoreItemCollection.GetItems<AssociationType>().Except(list.Select((Tuple<AssociationType, AssociationType> t) => t.Item2)).ToList<AssociationType>();
			return list.Concat(from <>h__TransparentIdentifier0 in (from at1 in list2
					from at2 in targetRemainingAssociationTypes
					select new { at1, at2 }).Where(delegate(<>h__TransparentIdentifier0)
				{
					if (<>h__TransparentIdentifier0.at1.Name.EqualsIgnoreCase(<>h__TransparentIdentifier0.at2.Name))
					{
						return true;
					}
					if (<>h__TransparentIdentifier0.at1.Constraint != null && <>h__TransparentIdentifier0.at2.Constraint != null && <>h__TransparentIdentifier0.at1.Constraint.PrincipalEnd.GetEntityType().EdmEquals(<>h__TransparentIdentifier0.at2.Constraint.PrincipalEnd.GetEntityType()) && <>h__TransparentIdentifier0.at1.Constraint.DependentEnd.GetEntityType().EdmEquals(<>h__TransparentIdentifier0.at2.Constraint.DependentEnd.GetEntityType()))
					{
						return <>h__TransparentIdentifier0.at1.Constraint.ToProperties.SequenceEqual(<>h__TransparentIdentifier0.at2.Constraint.ToProperties, new DynamicEqualityComparer<EdmMember>((EdmMember p1, EdmMember p2) => p1.EdmEquals(p2)));
					}
					return false;
				})
				select Tuple.Create<AssociationType, AssociationType>(<>h__TransparentIdentifier0.at1, <>h__TransparentIdentifier0.at2));
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002266C File Offset: 0x0002086C
		private IEnumerable<Tuple<AssociationType, AssociationType>> GetStoreAssociationTypePairs(AssociationType conceptualAssociationType1, AssociationType conceptualAssociationType2, ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			AssociationType associationType;
			AssociationType associationType2;
			if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(conceptualAssociationType1.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(conceptualAssociationType2.Name), out associationType2))
			{
				yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
			}
			else
			{
				EdmModelDiffer.<>c__DisplayClass15_0 CS$<>8__locals1 = new EdmModelDiffer.<>c__DisplayClass15_0();
				CS$<>8__locals1.sourceEnd1 = conceptualAssociationType1.SourceEnd;
				Tuple<EntityType, EntityType> tuple = entityTypePairs.Single((Tuple<EntityType, EntityType> t) => t.Item1 == CS$<>8__locals1.sourceEnd1.GetEntityType());
				AssociationEndMember sourceEnd2 = ((conceptualAssociationType2.SourceEnd.GetEntityType() == tuple.Item2) ? conceptualAssociationType2.SourceEnd : conceptualAssociationType2.TargetEnd);
				if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(CS$<>8__locals1.sourceEnd1.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(sourceEnd2.Name), out associationType2))
				{
					yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
				}
				AssociationEndMember otherEnd = conceptualAssociationType1.GetOtherEnd(CS$<>8__locals1.sourceEnd1);
				AssociationEndMember otherEnd2 = conceptualAssociationType2.GetOtherEnd(sourceEnd2);
				if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(otherEnd.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(otherEnd2.Name), out associationType2))
				{
					yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
				}
				CS$<>8__locals1 = null;
				sourceEnd2 = null;
			}
			yield break;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00022694 File Offset: 0x00020894
		private IEnumerable<Tuple<AssociationType, AssociationType>> FindStoreOnlyAssociationTypePairs(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs, ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			List<AssociationType> list = this._source.StoreItemCollection.GetItems<AssociationType>().Except(associationTypePairs.Select((Tuple<AssociationType, AssociationType> t) => t.Item1)).ToList<AssociationType>();
			List<AssociationType> list2 = this._target.StoreItemCollection.GetItems<AssociationType>().Except(associationTypePairs.Select((Tuple<AssociationType, AssociationType> t) => t.Item2)).ToList<AssociationType>();
			List<Tuple<AssociationType, AssociationType>> list3 = new List<Tuple<AssociationType, AssociationType>>();
			while (list.Any<AssociationType>())
			{
				AssociationType associationType1 = list[0];
				for (int i = 0; i < list2.Count; i++)
				{
					AssociationType associationType2 = list2[i];
					if (tablePairs.Any((Tuple<EntitySet, EntitySet> t) => t.Item1.ElementType == associationType1.Constraint.PrincipalEnd.GetEntityType() && t.Item2.ElementType == associationType2.Constraint.PrincipalEnd.GetEntityType()) && tablePairs.Any((Tuple<EntitySet, EntitySet> t) => t.Item1.ElementType == associationType1.Constraint.DependentEnd.GetEntityType() && t.Item2.ElementType == associationType2.Constraint.DependentEnd.GetEntityType()))
					{
						list3.Add(Tuple.Create<AssociationType, AssociationType>(associationType1, associationType2));
						list2.RemoveAt(i);
						break;
					}
				}
				list.RemoveAt(0);
			}
			return list3;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000227D7 File Offset: 0x000209D7
		private static string GetStoreAssociationIdentity(string associationName)
		{
			return "CodeFirstDatabaseSchema." + associationName;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000227E4 File Offset: 0x000209E4
		private IEnumerable<Tuple<EntitySet, EntitySet>> FindTablePairs(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs, ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			HashSet<EntitySet> sourceTables = new HashSet<EntitySet>();
			HashSet<EntitySet> targetTables = new HashSet<EntitySet>();
			foreach (Tuple<MappingFragment, MappingFragment> tuple in mappingFragmentPairs)
			{
				EntitySet tableSet = tuple.Item1.TableSet;
				EntitySet tableSet2 = tuple.Item2.TableSet;
				if (!sourceTables.Contains(tableSet) && !targetTables.Contains(tableSet2))
				{
					sourceTables.Add(tableSet);
					targetTables.Add(tableSet2);
					yield return Tuple.Create<EntitySet, EntitySet>(tableSet, tableSet2);
				}
			}
			IEnumerator<Tuple<MappingFragment, MappingFragment>> enumerator = null;
			using (IEnumerator<Tuple<AssociationType, AssociationType>> enumerator2 = associationTypePairs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Tuple<AssociationType, AssociationType> associationTypePair = enumerator2.Current;
					EntitySet entitySet = this._source.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationTypePair.Item1.Constraint.DependentEnd.GetEntityType());
					EntitySet entitySet2 = this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationTypePair.Item2.Constraint.DependentEnd.GetEntityType());
					if (!sourceTables.Contains(entitySet) && !targetTables.Contains(entitySet2))
					{
						sourceTables.Add(entitySet);
						targetTables.Add(entitySet2);
						yield return Tuple.Create<EntitySet, EntitySet>(entitySet, entitySet2);
					}
				}
			}
			IEnumerator<Tuple<AssociationType, AssociationType>> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00022804 File Offset: 0x00020A04
		private static IEnumerable<RenameTableOperation> HandleTransitiveRenameDependencies(IList<RenameTableOperation> renameTableOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameTableOperation>(renameTableOperations, delegate(RenameTableOperation rt1, RenameTableOperation rt2)
			{
				DatabaseName databaseName = DatabaseName.Parse(rt1.Name);
				DatabaseName databaseName2 = DatabaseName.Parse(rt2.Name);
				return databaseName.Name.EqualsIgnoreCase(rt2.NewName) && databaseName.Schema.EqualsIgnoreCase(databaseName2.Schema);
			}, (string t, RenameTableOperation rt) => new RenameTableOperation(t, rt.NewName, null), delegate(RenameTableOperation rt, string t)
			{
				rt.NewName = t;
			});
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00022874 File Offset: 0x00020A74
		private static IEnumerable<RenameColumnOperation> HandleTransitiveRenameDependencies(IList<RenameColumnOperation> renameColumnOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameColumnOperation>(renameColumnOperations, (RenameColumnOperation rc1, RenameColumnOperation rc2) => rc1.Table.EqualsIgnoreCase(rc2.Table) && rc1.Name.EqualsIgnoreCase(rc2.NewName), (string c, RenameColumnOperation rc) => new RenameColumnOperation(rc.Table, c, rc.NewName, null), delegate(RenameColumnOperation rc, string c)
			{
				rc.NewName = c;
			});
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000228E4 File Offset: 0x00020AE4
		private static IEnumerable<RenameIndexOperation> HandleTransitiveRenameDependencies(IList<RenameIndexOperation> renameIndexOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameIndexOperation>(renameIndexOperations, (RenameIndexOperation ri1, RenameIndexOperation ri2) => ri1.Table.EqualsIgnoreCase(ri2.Table) && ri1.Name.EqualsIgnoreCase(ri2.NewName), (string i, RenameIndexOperation rc) => new RenameIndexOperation(rc.Table, i, rc.NewName, null), delegate(RenameIndexOperation rc, string i)
			{
				rc.NewName = i;
			});
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00022954 File Offset: 0x00020B54
		private static IEnumerable<T> HandleTransitiveRenameDependencies<T>(IList<T> renameOperations, Func<T, T, bool> dependencyFinder, Func<string, T, T> renameCreator, Action<T, string> setNewName) where T : class
		{
			int tempCounter = 0;
			List<T> tempRenames = new List<T>();
			int num;
			for (int i = 0; i < renameOperations.Count; i = num + 1)
			{
				T renameOperation = renameOperations[i];
				if (renameOperations.Skip(i + 1).SingleOrDefault((T rt) => dependencyFinder(renameOperation, rt)) != null)
				{
					string text = "__mig_tmp__";
					num = tempCounter;
					tempCounter = num + 1;
					string text2 = text + num.ToString();
					tempRenames.Add(renameCreator(text2, renameOperation));
					setNewName(renameOperation, text2);
				}
				yield return renameOperation;
				num = i;
			}
			foreach (T t in tempRenames)
			{
				yield return t;
			}
			List<T>.Enumerator enumerator = default(List<T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002297C File Offset: 0x00020B7C
		private IEnumerable<MoveProcedureOperation> FindMovedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
				from mfm1 in esm1.ModificationFunctionMappings
				from esm2 in this._target.EntityContainerMapping.EntitySetMappings
				from mfm2 in esm2.ModificationFunctionMappings
				where mfm1.EntityType.Identity == mfm2.EntityType.Identity
				from o in EdmModelDiffer.DiffModificationFunctionSchemas(mfm1, mfm2)
				select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
				where asm1.ModificationFunctionMapping != null
				from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
				where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
				from o in EdmModelDiffer.DiffModificationFunctionSchemas(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping)
				select o);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00022B86 File Offset: 0x00020D86
		private static IEnumerable<MoveProcedureOperation> DiffModificationFunctionSchemas(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function), targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.UpdateFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.UpdateFunctionMapping.Function), targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function), targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema, null);
			}
			yield break;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00022B9D File Offset: 0x00020D9D
		private static IEnumerable<MoveProcedureOperation> DiffModificationFunctionSchemas(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function), targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function), targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema, null);
			}
			yield break;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00022BB4 File Offset: 0x00020DB4
		private IEnumerable<CreateProcedureOperation> FindAddedModificationFunctions(Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return (from esm1 in this._target.EntityContainerMapping.EntitySetMappings
				from mfm1 in esm1.ModificationFunctionMappings
				where !(from esm2 in this._source.EntityContainerMapping.EntitySetMappings
					from mfm2 in esm2.ModificationFunctionMappings
					where mfm1.EntityType.Identity == mfm2.EntityType.Identity
					select mfm2).Any<EntityTypeModificationFunctionMapping>()
				from o in this.BuildCreateProcedureOperations(mfm1, modificationCommandTreeGenerator, migrationSqlGenerator)
				select o).Concat(from asm1 in this._target.EntityContainerMapping.AssociationSetMappings
				where asm1.ModificationFunctionMapping != null
				where !(from asm2 in this._source.EntityContainerMapping.AssociationSetMappings
					where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
					select asm2.ModificationFunctionMapping).Any<AssociationSetModificationFunctionMapping>()
				from o in this.BuildCreateProcedureOperations(asm1.ModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator)
				select o);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00022CEC File Offset: 0x00020EEC
		private IEnumerable<RenameProcedureOperation> FindRenamedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
				from mfm1 in esm1.ModificationFunctionMappings
				from esm2 in this._target.EntityContainerMapping.EntitySetMappings
				from mfm2 in esm2.ModificationFunctionMappings
				where mfm1.EntityType.Identity == mfm2.EntityType.Identity
				from o in EdmModelDiffer.DiffModificationFunctionNames(mfm1, mfm2)
				select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
				where asm1.ModificationFunctionMapping != null
				from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
				where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
				from o in EdmModelDiffer.DiffModificationFunctionNames(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping)
				select o);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00022EF6 File Offset: 0x000210F6
		private static IEnumerable<RenameProcedureOperation> DiffModificationFunctionNames(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema), targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema), targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, null);
			}
			yield break;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00022F0D File Offset: 0x0002110D
		private static IEnumerable<RenameProcedureOperation> DiffModificationFunctionNames(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema), targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema), targetModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema), targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, null);
			}
			yield break;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00022F24 File Offset: 0x00021124
		private static string GetSchemaQualifiedName(string table, string schema)
		{
			return new DatabaseName(table, schema).ToString();
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00022F34 File Offset: 0x00021134
		private IEnumerable<AlterProcedureOperation> FindAlteredModificationFunctions(Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
				from mfm1 in esm1.ModificationFunctionMappings
				from esm2 in this._target.EntityContainerMapping.EntitySetMappings
				from mfm2 in esm2.ModificationFunctionMappings
				where mfm1.EntityType.Identity == mfm2.EntityType.Identity
				from o in this.DiffModificationFunctions(mfm1, mfm2, modificationCommandTreeGenerator, migrationSqlGenerator)
				select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
				where asm1.ModificationFunctionMapping != null
				from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
				where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
				from o in this.DiffModificationFunctions(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator)
				select o);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00023133 File Offset: 0x00021333
		private IEnumerable<AlterProcedureOperation> DiffModificationFunctions(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.InsertFunctionMapping, targetModificationFunctionMapping.InsertFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.DeleteFunctionMapping, targetModificationFunctionMapping.DeleteFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			yield break;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00023160 File Offset: 0x00021360
		private IEnumerable<AlterProcedureOperation> DiffModificationFunctions(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.InsertFunctionMapping, targetModificationFunctionMapping.InsertFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.UpdateFunctionMapping, targetModificationFunctionMapping.UpdateFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.UpdateFunctionMapping.Function, this.GenerateUpdateFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.DeleteFunctionMapping, targetModificationFunctionMapping.DeleteFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			yield break;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002318D File Offset: 0x0002138D
		private string GenerateInsertFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateInsert(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000231C8 File Offset: 0x000213C8
		private string GenerateInsertFunctionBody(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbInsertCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateAssociationInsert(s), modificationCommandTreeGenerator, migrationSqlGenerator, null);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000231F4 File Offset: 0x000213F4
		private string GenerateUpdateFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateUpdate(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, modificationFunctionMapping.UpdateFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00023244 File Offset: 0x00021444
		private string GenerateDeleteFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateDelete(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, modificationFunctionMapping.DeleteFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00023294 File Offset: 0x00021494
		private string GenerateDeleteFunctionBody(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbDeleteCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateAssociationDelete(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.DeleteFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000232CC File Offset: 0x000214CC
		private string GenerateFunctionBody<TCommandTree>(EntityTypeModificationFunctionMapping modificationFunctionMapping, Func<ModificationCommandTreeGenerator, string, IEnumerable<TCommandTree>> treeGenerator, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string functionName, string rowsAffectedParameterName) where TCommandTree : DbModificationCommandTree
		{
			TCommandTree[] array = new TCommandTree[0];
			if (modificationCommandTreeGenerator != null)
			{
				DynamicToFunctionModificationCommandConverter dynamicToFunctionModificationCommandConverter = new DynamicToFunctionModificationCommandConverter(modificationFunctionMapping, this._target.EntityContainerMapping);
				try
				{
					array = dynamicToFunctionModificationCommandConverter.Convert<TCommandTree>(treeGenerator(modificationCommandTreeGenerator.Value, modificationFunctionMapping.EntityType.Identity)).ToArray<TCommandTree>();
				}
				catch (UpdateException ex)
				{
					throw new InvalidOperationException(Strings.ErrorGeneratingCommandTree(functionName, modificationFunctionMapping.EntityType.Name), ex);
				}
			}
			return this.GenerateFunctionBody<TCommandTree>(migrationSqlGenerator, rowsAffectedParameterName, array);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00023350 File Offset: 0x00021550
		private string GenerateFunctionBody<TCommandTree>(AssociationSetModificationFunctionMapping modificationFunctionMapping, Func<ModificationCommandTreeGenerator, string, IEnumerable<TCommandTree>> treeGenerator, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string rowsAffectedParameterName) where TCommandTree : DbModificationCommandTree
		{
			TCommandTree[] array = new TCommandTree[0];
			if (modificationCommandTreeGenerator != null)
			{
				array = new DynamicToFunctionModificationCommandConverter(modificationFunctionMapping, this._target.EntityContainerMapping).Convert<TCommandTree>(treeGenerator(modificationCommandTreeGenerator.Value, modificationFunctionMapping.AssociationSet.ElementType.Identity)).ToArray<TCommandTree>();
			}
			return this.GenerateFunctionBody<TCommandTree>(migrationSqlGenerator, rowsAffectedParameterName, array);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000233AC File Offset: 0x000215AC
		private string GenerateFunctionBody<TCommandTree>(MigrationSqlGenerator migrationSqlGenerator, string rowsAffectedParameterName, TCommandTree[] commandTrees) where TCommandTree : DbModificationCommandTree
		{
			if (migrationSqlGenerator == null)
			{
				return null;
			}
			string providerManifestToken = this._target.ProviderInfo.ProviderManifestToken;
			return migrationSqlGenerator.GenerateProcedureBody(commandTrees, rowsAffectedParameterName, providerManifestToken);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000233D8 File Offset: 0x000215D8
		private bool DiffModificationFunction(ModificationFunctionMapping functionMapping1, ModificationFunctionMapping functionMapping2)
		{
			if (!functionMapping1.RowsAffectedParameterName.EqualsOrdinal(functionMapping2.RowsAffectedParameterName))
			{
				return false;
			}
			if (!functionMapping1.ParameterBindings.SequenceEqual(functionMapping2.ParameterBindings, new Func<ModificationFunctionParameterBinding, ModificationFunctionParameterBinding, bool>(this.DiffParameterBinding)))
			{
				return false;
			}
			IEnumerable<ModificationFunctionResultBinding> enumerable = Enumerable.Empty<ModificationFunctionResultBinding>();
			IEnumerable<ModificationFunctionResultBinding> enumerable2 = functionMapping1.ResultBindings;
			IEnumerable<ModificationFunctionResultBinding> enumerable3 = enumerable2 ?? enumerable;
			enumerable2 = functionMapping2.ResultBindings;
			return enumerable3.SequenceEqual(enumerable2 ?? enumerable, new Func<ModificationFunctionResultBinding, ModificationFunctionResultBinding, bool>(EdmModelDiffer.DiffResultBinding));
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00023454 File Offset: 0x00021654
		private bool DiffParameterBinding(ModificationFunctionParameterBinding parameterBinding1, ModificationFunctionParameterBinding parameterBinding2)
		{
			FunctionParameter parameter = parameterBinding1.Parameter;
			FunctionParameter parameter2 = parameterBinding2.Parameter;
			if (!parameter.Name.EqualsOrdinal(parameter2.Name))
			{
				return false;
			}
			if (parameter.Mode != parameter2.Mode)
			{
				return false;
			}
			if (parameterBinding1.IsCurrent != parameterBinding2.IsCurrent)
			{
				return false;
			}
			if (!parameterBinding1.MemberPath.Members.SequenceEqual(parameterBinding2.MemberPath.Members, (EdmMember m1, EdmMember m2) => m1.Identity.EqualsOrdinal(m2.Identity)))
			{
				return false;
			}
			if (this._source.ProviderInfo.Equals(this._target.ProviderInfo))
			{
				return parameter.TypeName.EqualsIgnoreCase(parameter2.TypeName) && parameter.TypeUsage.EdmEquals(parameter2.TypeUsage);
			}
			byte? b = parameter.Precision;
			int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			b = parameter2.Precision;
			int? num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				b = parameter.Scale;
				num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				b = parameter2.Scale;
				num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
			}
			return false;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00023614 File Offset: 0x00021814
		private static bool DiffResultBinding(ModificationFunctionResultBinding resultBinding1, ModificationFunctionResultBinding resultBinding2)
		{
			return resultBinding1.ColumnName.EqualsOrdinal(resultBinding2.ColumnName) && resultBinding1.Property.Identity.EqualsOrdinal(resultBinding2.Property.Identity);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002364B File Offset: 0x0002184B
		private IEnumerable<CreateProcedureOperation> BuildCreateProcedureOperations(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.UpdateFunctionMapping.Function, this.GenerateUpdateFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield break;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00023670 File Offset: 0x00021870
		private IEnumerable<CreateProcedureOperation> BuildCreateProcedureOperations(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield break;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00023698 File Offset: 0x00021898
		private CreateProcedureOperation BuildCreateProcedureOperation(EdmFunction function, string bodySql)
		{
			CreateProcedureOperation createProcedureOperation = new CreateProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(function), bodySql, null);
			function.Parameters.Each(delegate(FunctionParameter p)
			{
				createProcedureOperation.Parameters.Add(EdmModelDiffer.BuildParameterModel(p, this._target));
			});
			return createProcedureOperation;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000236E4 File Offset: 0x000218E4
		private AlterProcedureOperation BuildAlterProcedureOperation(EdmFunction function, string bodySql)
		{
			AlterProcedureOperation alterProcedureOperation = new AlterProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(function), bodySql, null);
			function.Parameters.Each(delegate(FunctionParameter p)
			{
				alterProcedureOperation.Parameters.Add(EdmModelDiffer.BuildParameterModel(p, this._target));
			});
			return alterProcedureOperation;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00023730 File Offset: 0x00021930
		private static ParameterModel BuildParameterModel(FunctionParameter functionParameter, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			TypeUsage modelTypeUsage = functionParameter.TypeUsage.ModelTypeUsage;
			string name = modelMetadata.ProviderManifest.GetStoreType(modelTypeUsage).EdmType.Name;
			ParameterModel parameterModel = new ParameterModel(((PrimitiveType)modelTypeUsage.EdmType).PrimitiveTypeKind, modelTypeUsage)
			{
				Name = functionParameter.Name,
				IsOutParameter = (functionParameter.Mode == ParameterMode.Out),
				StoreType = ((!functionParameter.TypeName.EqualsIgnoreCase(name)) ? functionParameter.TypeName : null)
			};
			Facet facet;
			if (modelTypeUsage.Facets.TryGetValue("MaxLength", true, out facet) && facet.Value != null)
			{
				parameterModel.MaxLength = facet.Value as int?;
			}
			if (modelTypeUsage.Facets.TryGetValue("Precision", true, out facet) && facet.Value != null)
			{
				parameterModel.Precision = (byte?)facet.Value;
			}
			if (modelTypeUsage.Facets.TryGetValue("Scale", true, out facet) && facet.Value != null)
			{
				parameterModel.Scale = (byte?)facet.Value;
			}
			if (modelTypeUsage.Facets.TryGetValue("FixedLength", true, out facet) && facet.Value != null && (bool)facet.Value)
			{
				parameterModel.IsFixedLength = new bool?(true);
			}
			if (modelTypeUsage.Facets.TryGetValue("Unicode", true, out facet) && facet.Value != null && !(bool)facet.Value)
			{
				parameterModel.IsUnicode = new bool?(false);
			}
			return parameterModel;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000238AC File Offset: 0x00021AAC
		private IEnumerable<DropProcedureOperation> FindDroppedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
				from mfm1 in esm1.ModificationFunctionMappings
				where !(from esm2 in this._target.EntityContainerMapping.EntitySetMappings
					from mfm2 in esm2.ModificationFunctionMappings
					where mfm1.EntityType.Identity == mfm2.EntityType.Identity
					select mfm2).Any<EntityTypeModificationFunctionMapping>()
				from o in new DropProcedureOperation[]
				{
					new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.InsertFunctionMapping.Function), null),
					new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.UpdateFunctionMapping.Function), null),
					new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.DeleteFunctionMapping.Function), null)
				}
				select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
				where asm1.ModificationFunctionMapping != null
				where !(from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
					where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
					select asm2.ModificationFunctionMapping).Any<AssociationSetModificationFunctionMapping>()
				from o in new DropProcedureOperation[]
				{
					new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(asm1.ModificationFunctionMapping.InsertFunctionMapping.Function), null),
					new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(asm1.ModificationFunctionMapping.DeleteFunctionMapping.Function), null)
				}
				select o);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000239F0 File Offset: 0x00021BF0
		private static IEnumerable<RenameTableOperation> FindRenamedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
				where !p.Item1.Table.EqualsIgnoreCase(p.Item2.Table)
				select new RenameTableOperation(EdmModelDiffer.GetSchemaQualifiedName(p.Item1), p.Item2.Table, null);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00023A48 File Offset: 0x00021C48
		private IEnumerable<CreateTableOperation> FindAddedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._target.StoreEntityContainer.EntitySets.Except(tablePairs.Select((Tuple<EntitySet, EntitySet> p) => p.Item2))
				select EdmModelDiffer.BuildCreateTableOperation(es, this._target);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00023AA0 File Offset: 0x00021CA0
		private IEnumerable<MoveTableOperation> FindMovedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
				where !p.Item1.Schema.EqualsIgnoreCase(p.Item2.Schema)
				select new MoveTableOperation(new DatabaseName(p.Item2.Table, p.Item1.Schema).ToString(), p.Item2.Schema, null)
				{
					CreateTableOperation = EdmModelDiffer.BuildCreateTableOperation(p.Item2, this._target)
				};
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00023AD8 File Offset: 0x00021CD8
		private IEnumerable<DropTableOperation> FindDroppedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._source.StoreEntityContainer.EntitySets.Except(tablePairs.Select((Tuple<EntitySet, EntitySet> p) => p.Item1))
				select new DropTableOperation(EdmModelDiffer.GetSchemaQualifiedName(es), EdmModelDiffer.GetAnnotations(es.ElementType), es.ElementType.Properties.Where((EdmProperty p) => EdmModelDiffer.GetAnnotations(p).Count > 0).ToDictionary((EdmProperty p) => p.Name, (EdmProperty p) => EdmModelDiffer.GetAnnotations(p)), EdmModelDiffer.BuildCreateTableOperation(es, this._source), null);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00023B30 File Offset: 0x00021D30
		private IEnumerable<AlterTableOperation> FindAlteredTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
				where !EdmModelDiffer.GetAnnotations(p.Item1.ElementType).SequenceEqual(EdmModelDiffer.GetAnnotations(p.Item2.ElementType))
				select this.BuildAlterTableAnnotationsOperation(p.Item1, p.Item2);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00023B68 File Offset: 0x00021D68
		private AlterTableOperation BuildAlterTableAnnotationsOperation(EntitySet sourceTable, EntitySet destinationTable)
		{
			AlterTableOperation operation = new AlterTableOperation(EdmModelDiffer.GetSchemaQualifiedName(destinationTable), EdmModelDiffer.BuildAnnotationPairs(EdmModelDiffer.GetAnnotations(sourceTable.ElementType), EdmModelDiffer.GetAnnotations(destinationTable.ElementType)), null);
			destinationTable.ElementType.Properties.Each(delegate(EdmProperty p)
			{
				operation.Columns.Add(EdmModelDiffer.BuildColumnModel(p, this._target, EdmModelDiffer.GetAnnotations(p).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(a.Value, a.Value))));
			});
			return operation;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00023BD4 File Offset: 0x00021DD4
		internal static Dictionary<string, object> GetAnnotations(MetadataItem item)
		{
			return item.Annotations.Where((MetadataProperty a) => a.Name.StartsWith("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:", StringComparison.Ordinal) && !a.Name.EndsWith("Index", StringComparison.Ordinal)).ToDictionary((MetadataProperty a) => a.Name.Substring("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:".Length), (MetadataProperty a) => a.Value);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00023C50 File Offset: 0x00021E50
		internal static IndexAttribute GetPrimaryKeyIndexAttribute(EntityType entityType)
		{
			return (from a in entityType.Annotations
				where a.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index"
				select a.Value).OfType<IndexAnnotation>().SelectMany((IndexAnnotation ia) => ia.Indexes).SingleOrDefault<IndexAttribute>();
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00023CDC File Offset: 0x00021EDC
		private IEnumerable<MigrationOperation> FindAlteredPrimaryKeys(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns, ICollection<AlterColumnOperation> alteredColumns)
		{
			return from ts in tablePairs
				let t2 = EdmModelDiffer.GetSchemaQualifiedName(ts.Item2)
				let pk1 = EdmModelDiffer.GetPrimaryKeyIndexAttribute(ts.Item1.ElementType) ?? new IndexAttribute()
				let pk2 = EdmModelDiffer.GetPrimaryKeyIndexAttribute(ts.Item2.ElementType) ?? new IndexAttribute()
				where !ts.Item1.ElementType.KeyProperties.SequenceEqual(ts.Item2.ElementType.KeyProperties, (EdmProperty p1, EdmProperty p2) => p1.Name.EqualsIgnoreCase(p2.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t2) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(p2.Name))) || ts.Item2.ElementType.KeyProperties.Any((EdmProperty p) => alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(t2) && ac.Column.Name.EqualsIgnoreCase(p.Name))) || pk1.Name != pk2.Name || pk1.IsClusteredConfigured != pk2.IsClusteredConfigured || pk1.IsClustered != pk2.IsClustered
				from o in this.BuildChangePrimaryKeyOperations(ts)
				select o;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00023DB2 File Offset: 0x00021FB2
		private IEnumerable<MigrationOperation> BuildChangePrimaryKeyOperations(Tuple<EntitySet, EntitySet> tablePair)
		{
			List<ReferentialConstraint> list = (from at in this._source.StoreItemCollection.GetItems<AssociationType>()
				select at.Constraint into c
				where c.FromProperties.SequenceEqual(tablePair.Item1.ElementType.KeyProperties)
				select c).ToList<ReferentialConstraint>();
			foreach (ReferentialConstraint referentialConstraint in list)
			{
				yield return EdmModelDiffer.BuildDropForeignKeyOperation(referentialConstraint, this._source);
			}
			List<ReferentialConstraint>.Enumerator enumerator = default(List<ReferentialConstraint>.Enumerator);
			DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
			{
				Table = EdmModelDiffer.GetSchemaQualifiedName(tablePair.Item2)
			};
			tablePair.Item1.ElementType.KeyProperties.Each(delegate(EdmProperty pr)
			{
				dropPrimaryKeyOperation.Columns.Add(pr.Name);
			});
			IndexAttribute primaryKeyIndexAttribute = EdmModelDiffer.GetPrimaryKeyIndexAttribute(tablePair.Item1.ElementType);
			if (primaryKeyIndexAttribute != null)
			{
				dropPrimaryKeyOperation.Name = primaryKeyIndexAttribute.Name;
				if (primaryKeyIndexAttribute.IsClusteredConfigured)
				{
					dropPrimaryKeyOperation.IsClustered = primaryKeyIndexAttribute.IsClustered;
				}
			}
			yield return dropPrimaryKeyOperation;
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
			{
				Table = EdmModelDiffer.GetSchemaQualifiedName(tablePair.Item2)
			};
			tablePair.Item2.ElementType.KeyProperties.Each(delegate(EdmProperty pr)
			{
				addPrimaryKeyOperation.Columns.Add(pr.Name);
			});
			IndexAttribute primaryKeyIndexAttribute2 = EdmModelDiffer.GetPrimaryKeyIndexAttribute(tablePair.Item2.ElementType);
			if (primaryKeyIndexAttribute2 != null)
			{
				addPrimaryKeyOperation.Name = primaryKeyIndexAttribute2.Name;
				if (primaryKeyIndexAttribute2.IsClusteredConfigured)
				{
					addPrimaryKeyOperation.IsClustered = primaryKeyIndexAttribute2.IsClustered;
				}
			}
			yield return addPrimaryKeyOperation;
			List<ReferentialConstraint> list2 = (from at in this._target.StoreItemCollection.GetItems<AssociationType>()
				select at.Constraint into c
				where c.FromProperties.SequenceEqual(tablePair.Item2.ElementType.KeyProperties)
				select c).ToList<ReferentialConstraint>();
			foreach (ReferentialConstraint referentialConstraint2 in list2)
			{
				yield return EdmModelDiffer.BuildAddForeignKeyOperation(referentialConstraint2, this._target);
			}
			enumerator = default(List<ReferentialConstraint>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00023DCC File Offset: 0x00021FCC
		private IEnumerable<AddForeignKeyOperation> FindAddedForeignKeys(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from at in this._target.StoreItemCollection.GetItems<AssociationType>().Except(associationTypePairs.Select((Tuple<AssociationType, AssociationType> p) => p.Item2)).Concat(from at in associationTypePairs
					where !this.DiffAssociations(at.Item1.Constraint, at.Item2.Constraint, renamedColumns)
					select at.Item2)
				select EdmModelDiffer.BuildAddForeignKeyOperation(at.Constraint, this._target);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00023E74 File Offset: 0x00022074
		private IEnumerable<DropForeignKeyOperation> FindDroppedForeignKeys(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from at in this._source.StoreItemCollection.GetItems<AssociationType>().Except(associationTypePairs.Select((Tuple<AssociationType, AssociationType> p) => p.Item1)).Concat(from at in associationTypePairs
					where !this.DiffAssociations(at.Item1.Constraint, at.Item2.Constraint, renamedColumns)
					select at.Item1)
				select EdmModelDiffer.BuildDropForeignKeyOperation(at.Constraint, this._source);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00023F1C File Offset: 0x0002211C
		private bool DiffAssociations(ReferentialConstraint referentialConstraint1, ReferentialConstraint referentialConstraint2, ICollection<RenameColumnOperation> renamedColumns)
		{
			string targetTable = EdmModelDiffer.GetSchemaQualifiedName(this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint2.DependentEnd.GetEntityType()));
			return referentialConstraint1.ToProperties.SequenceEqual(referentialConstraint2.ToProperties, (EdmProperty p1, EdmProperty p2) => p1.Name.EqualsIgnoreCase(p2.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(targetTable) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(p2.Name))) && referentialConstraint1.PrincipalEnd.DeleteBehavior == referentialConstraint2.PrincipalEnd.DeleteBehavior;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00023FAC File Offset: 0x000221AC
		private static AddForeignKeyOperation BuildAddForeignKeyOperation(ReferentialConstraint referentialConstraint, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(null);
			EdmModelDiffer.BuildForeignKeyOperation(referentialConstraint, addForeignKeyOperation, modelMetadata);
			referentialConstraint.FromProperties.Each(delegate(EdmProperty pr)
			{
				addForeignKeyOperation.PrincipalColumns.Add(pr.Name);
			});
			addForeignKeyOperation.CascadeDelete = referentialConstraint.PrincipalEnd.DeleteBehavior == OperationAction.Cascade;
			return addForeignKeyOperation;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00024010 File Offset: 0x00022210
		private static DropForeignKeyOperation BuildDropForeignKeyOperation(ReferentialConstraint referentialConstraint, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(EdmModelDiffer.BuildAddForeignKeyOperation(referentialConstraint, modelMetadata), null);
			EdmModelDiffer.BuildForeignKeyOperation(referentialConstraint, dropForeignKeyOperation, modelMetadata);
			return dropForeignKeyOperation;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00024034 File Offset: 0x00022234
		private static void BuildForeignKeyOperation(ReferentialConstraint referentialConstraint, ForeignKeyOperation foreignKeyOperation, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			foreignKeyOperation.PrincipalTable = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint.PrincipalEnd.GetEntityType()));
			foreignKeyOperation.DependentTable = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint.DependentEnd.GetEntityType()));
			referentialConstraint.ToProperties.Each(delegate(EdmProperty pr)
			{
				foreignKeyOperation.DependentColumns.Add(pr.Name);
			});
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000240CC File Offset: 0x000222CC
		private IEnumerable<AddColumnOperation> FindAddedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
				from c in p.Item2.ElementType.Properties.Except(p.Item1.ElementType.Properties, (EdmProperty c1, EdmProperty c2) => c1.Name.EqualsIgnoreCase(c2.Name))
				where !renamedColumns.Any((RenameColumnOperation cr) => cr.Table.EqualsIgnoreCase(t) && cr.NewName.EqualsIgnoreCase(c.Name))
				select new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._target, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00024178 File Offset: 0x00022378
		private IEnumerable<DropColumnOperation> FindDroppedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
				from c in p.Item1.ElementType.Properties.Except(p.Item2.ElementType.Properties, (EdmProperty c1, EdmProperty c2) => c1.Name.EqualsIgnoreCase(c2.Name))
				where !renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t) && rc.Name.EqualsIgnoreCase(c.Name))
				select new DropColumnOperation(t, c.Name, EdmModelDiffer.GetAnnotations(c), new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._source, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null), null);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00024224 File Offset: 0x00022424
		private IEnumerable<DropColumnOperation> FindOrphanedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
				from rc1 in renamedColumns
				where rc1.Table.EqualsIgnoreCase(t)
				from c in p.Item1.ElementType.Properties
				where c.Name.EqualsIgnoreCase(rc1.NewName) && !renamedColumns.Any((RenameColumnOperation rc2) => rc2 != rc1 && rc2.Table.EqualsIgnoreCase(rc1.Table) && rc2.Name.EqualsIgnoreCase(rc1.NewName))
				select new DropColumnOperation(t, c.Name, EdmModelDiffer.GetAnnotations(c), new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._source, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null), null);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00024324 File Offset: 0x00022524
		private IEnumerable<AlterColumnOperation> FindAlteredColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
				from p1 in p.Item1.ElementType.Properties
				let p2 = p.Item2.ElementType.Properties.SingleOrDefault((EdmProperty c) => (p1.Name.EqualsIgnoreCase(c.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(c.Name))) && !this.DiffColumns(p1, c))
				where p2 != null
				select this.BuildAlterColumnOperation(t, p2, this._target, p1, this._source);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000243F4 File Offset: 0x000225F4
		private IEnumerable<ConsolidatedIndex> FindSourceIndexes(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._source.StoreEntityContainer.EntitySets
				let p = tablePairs.SingleOrDefault((Tuple<EntitySet, EntitySet> p) => p.Item1 == es)
				let t = EdmModelDiffer.GetSchemaQualifiedName((p != null) ? p.Item2 : es)
				from i in ConsolidatedIndex.BuildIndexes(t, es.ElementType.Properties.Select((EdmProperty c) => Tuple.Create<string, EdmProperty>(c.Name, c)))
				select i;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00024498 File Offset: 0x00022698
		private IEnumerable<ConsolidatedIndex> FindTargetIndexes()
		{
			return from es in this._target.StoreEntityContainer.EntitySets
				from i in ConsolidatedIndex.BuildIndexes(EdmModelDiffer.GetSchemaQualifiedName(es), es.ElementType.Properties.Select((EdmProperty p) => Tuple.Create<string, EdmProperty>(p.Name, p)))
				select i;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000244F8 File Offset: 0x000226F8
		private static IEnumerable<CreateIndexOperation> FindAddedIndexes(ICollection<ConsolidatedIndex> sourceIndexes, ICollection<ConsolidatedIndex> targetIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from i in targetIndexes.Except(sourceIndexes, (ConsolidatedIndex i1, ConsolidatedIndex i2) => EdmModelDiffer.IndexesEqual(i1, i2, renamedColumns) && !alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(i2.Table) && i2.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)))
				select i.CreateCreateIndexOperation();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00024550 File Offset: 0x00022750
		private static IEnumerable<DropIndexOperation> FindDroppedIndexes(ICollection<ConsolidatedIndex> sourceIndexes, ICollection<ConsolidatedIndex> targetIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from i in sourceIndexes.Except(targetIndexes, (ConsolidatedIndex i2, ConsolidatedIndex i1) => EdmModelDiffer.IndexesEqual(i1, i2, renamedColumns) && !alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(i2.Table) && i2.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)))
				select i.CreateDropIndexOperation();
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000245A8 File Offset: 0x000227A8
		private static bool IndexesEqual(ConsolidatedIndex consolidatedIndex1, ConsolidatedIndex consolidatedIndex2, ICollection<RenameColumnOperation> renamedColumns)
		{
			return consolidatedIndex1.Table.EqualsIgnoreCase(consolidatedIndex2.Table) && consolidatedIndex1.Index.Equals(consolidatedIndex2.Index) && consolidatedIndex1.Columns.Select((string c) => (from rc in renamedColumns
				where rc.Table.EqualsIgnoreCase(consolidatedIndex1.Table) && rc.Name.EqualsIgnoreCase(c)
				select rc.NewName).SingleOrDefault<string>() ?? c).SequenceEqual(consolidatedIndex2.Columns, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002462C File Offset: 0x0002282C
		private static IEnumerable<RenameIndexOperation> FindRenamedIndexes(ICollection<CreateIndexOperation> addedIndexes, ICollection<DropIndexOperation> droppedIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from ci1 in addedIndexes.ToList<CreateIndexOperation>()
				from di in droppedIndexes.ToList<DropIndexOperation>()
				let ci2 = (CreateIndexOperation)di.Inverse
				where ci1.Table.EqualsIgnoreCase(ci2.Table) && !ci1.Name.EqualsIgnoreCase(ci2.Name) && ci1.Columns.SequenceEqual(ci2.Columns.Select((string c) => (from rc in renamedColumns
					where rc.Table.EqualsIgnoreCase(ci2.Table) && rc.Name.EqualsIgnoreCase(c)
					select rc.NewName).SingleOrDefault<string>() ?? c), StringComparer.OrdinalIgnoreCase) && ci1.IsClustered == ci2.IsClustered && ci1.IsUnique == ci2.IsUnique && (!alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(ci1.Table) && ci1.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)) && addedIndexes.Remove(ci1)) && droppedIndexes.Remove(di)
				select new RenameIndexOperation(ci1.Table, di.Name, ci1.Name, null);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000246F0 File Offset: 0x000228F0
		private bool DiffColumns(EdmProperty column1, EdmProperty column2)
		{
			if (column1.Nullable != column2.Nullable)
			{
				return false;
			}
			if (column1.PrimitiveType.PrimitiveTypeKind != column2.PrimitiveType.PrimitiveTypeKind)
			{
				return false;
			}
			if (column1.StoreGeneratedPattern != column2.StoreGeneratedPattern)
			{
				return false;
			}
			if (!(from a in EdmModelDiffer.GetAnnotations(column1)
				orderby a.Key
				select a).SequenceEqual(from a in EdmModelDiffer.GetAnnotations(column2)
				orderby a.Key
				select a))
			{
				return false;
			}
			if (this._source.ProviderInfo.Equals(this._target.ProviderInfo))
			{
				return column1.TypeName.EqualsIgnoreCase(column2.TypeName) && column1.TypeUsage.EdmEquals(column2.TypeUsage);
			}
			byte? b = column1.Precision;
			int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			b = column2.Precision;
			int? num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				b = column1.Scale;
				num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				b = column2.Scale;
				num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
				{
					bool? flag = column1.IsUnicode;
					bool? flag2 = column2.IsUnicode;
					if ((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null)))
					{
						flag2 = column1.IsFixedLength;
						flag = column2.IsFixedLength;
						return (flag2.GetValueOrDefault() == flag.GetValueOrDefault()) & (flag2 != null == (flag != null));
					}
				}
			}
			return false;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00024928 File Offset: 0x00022B28
		private AlterColumnOperation BuildAlterColumnOperation(string table, EdmProperty targetProperty, EdmModelDiffer.ModelMetadata targetModelMetadata, EdmProperty sourceProperty, EdmModelDiffer.ModelMetadata sourceModelMetadata)
		{
			IDictionary<string, AnnotationValues> dictionary = EdmModelDiffer.BuildAnnotationPairs(EdmModelDiffer.GetAnnotations(sourceProperty), EdmModelDiffer.GetAnnotations(targetProperty));
			Dictionary<string, AnnotationValues> dictionary2 = dictionary.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => new AnnotationValues(a.Value.NewValue, a.Value.OldValue));
			ColumnModel columnModel = EdmModelDiffer.BuildColumnModel(targetProperty, targetModelMetadata, dictionary);
			ColumnModel columnModel2 = EdmModelDiffer.BuildColumnModel(sourceProperty, sourceModelMetadata, dictionary2);
			columnModel2.Name = columnModel.Name;
			return new AlterColumnOperation(table, columnModel, columnModel.IsNarrowerThan(columnModel2, this._target.ProviderManifest), new AlterColumnOperation(table, columnModel2, columnModel2.IsNarrowerThan(columnModel, this._target.ProviderManifest), null), null);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000249E4 File Offset: 0x00022BE4
		private static IDictionary<string, AnnotationValues> BuildAnnotationPairs(IDictionary<string, object> rawSourceAnnotations, IDictionary<string, object> rawTargetAnnotations)
		{
			Dictionary<string, AnnotationValues> dictionary = new Dictionary<string, AnnotationValues>();
			foreach (string text in rawTargetAnnotations.Keys.Concat(rawSourceAnnotations.Keys).Distinct<string>())
			{
				if (!rawSourceAnnotations.ContainsKey(text))
				{
					dictionary[text] = new AnnotationValues(null, rawTargetAnnotations[text]);
				}
				else if (!rawTargetAnnotations.ContainsKey(text))
				{
					dictionary[text] = new AnnotationValues(rawSourceAnnotations[text], null);
				}
				else if (!object.Equals(rawSourceAnnotations[text], rawTargetAnnotations[text]))
				{
					dictionary[text] = new AnnotationValues(rawSourceAnnotations[text], rawTargetAnnotations[text]);
				}
			}
			return dictionary;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00024AB0 File Offset: 0x00022CB0
		private IEnumerable<RenameColumnOperation> FindRenamedColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs, ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			return EdmModelDiffer.FindRenamedMappedColumns(mappingFragmentPairs).Concat(this.FindRenamedForeignKeyColumns(associationTypePairs)).Concat(EdmModelDiffer.FindRenamedDiscriminatorColumns(mappingFragmentPairs))
				.Distinct(new DynamicEqualityComparer<RenameColumnOperation>((RenameColumnOperation c1, RenameColumnOperation c2) => c1.Table.EqualsIgnoreCase(c2.Table) && c1.Name.EqualsIgnoreCase(c2.Name) && c1.NewName.EqualsIgnoreCase(c2.NewName)));
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00024B04 File Offset: 0x00022D04
		private static IEnumerable<RenameColumnOperation> FindRenamedMappedColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs)
		{
			return from mfs in mappingFragmentPairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(mfs.Item2.StoreEntitySet)
				from cr in EdmModelDiffer.FindRenamedMappedColumns(mfs.Item1, mfs.Item2, t)
				select cr;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00024B7C File Offset: 0x00022D7C
		private static IEnumerable<RenameColumnOperation> FindRenamedMappedColumns(MappingFragment mappingFragment1, MappingFragment mappingFragment2, string table)
		{
			return from cmb1 in mappingFragment1.FlattenedProperties
				from cmb2 in mappingFragment2.FlattenedProperties
				where cmb1.PropertyPath.SequenceEqual(cmb2.PropertyPath, new DynamicEqualityComparer<EdmProperty>((EdmProperty p1, EdmProperty p2) => p1.EdmEquals(p2))) && !cmb1.ColumnProperty.Name.EqualsIgnoreCase(cmb2.ColumnProperty.Name)
				select new RenameColumnOperation(table, cmb1.ColumnProperty.Name, cmb2.ColumnProperty.Name, null);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00024C08 File Offset: 0x00022E08
		private IEnumerable<RenameColumnOperation> FindRenamedForeignKeyColumns(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			return from ats in associationTypePairs
				let rc1 = ats.Item1.Constraint
				let rc2 = ats.Item2.Constraint
				from ps in rc1.ToProperties.Zip(rc2.ToProperties)
				where !ps.Key.Name.EqualsIgnoreCase(ps.Value.Name) && (!rc2.DependentEnd.GetEntityType().Properties.Any((EdmProperty p) => p.Name.EqualsIgnoreCase(ps.Key.Name)) || rc1.DependentEnd.GetEntityType().Properties.Any((EdmProperty p) => p.Name.EqualsIgnoreCase(ps.Value.Name)))
				select new RenameColumnOperation(EdmModelDiffer.GetSchemaQualifiedName(this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == rc2.DependentEnd.GetEntityType())), ps.Key.Name, ps.Value.Name, null);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00024CD8 File Offset: 0x00022ED8
		private static IEnumerable<RenameColumnOperation> FindRenamedDiscriminatorColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs)
		{
			return from mfs in mappingFragmentPairs
				let t = EdmModelDiffer.GetSchemaQualifiedName(mfs.Item2.StoreEntitySet)
				from cr in EdmModelDiffer.FindRenamedDiscriminatorColumns(mfs.Item1, mfs.Item2, t)
				select cr;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00024D50 File Offset: 0x00022F50
		private static IEnumerable<RenameColumnOperation> FindRenamedDiscriminatorColumns(MappingFragment mappingFragment1, MappingFragment mappingFragment2, string table)
		{
			return from c1 in mappingFragment1.Conditions
				from c2 in mappingFragment2.Conditions
				where object.Equals(c1.Value, c2.Value)
				where !c1.Column.Name.EqualsIgnoreCase(c2.Column.Name)
				select new RenameColumnOperation(table, c1.Column.Name, c2.Column.Name, null);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00024E00 File Offset: 0x00023000
		private static CreateTableOperation BuildCreateTableOperation(EntitySet entitySet, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			CreateTableOperation createTableOperation = new CreateTableOperation(EdmModelDiffer.GetSchemaQualifiedName(entitySet), EdmModelDiffer.GetAnnotations(entitySet.ElementType), null);
			entitySet.ElementType.Properties.Each(delegate(EdmProperty p)
			{
				createTableOperation.Columns.Add(EdmModelDiffer.BuildColumnModel(p, modelMetadata, EdmModelDiffer.GetAnnotations(p).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))));
			});
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null);
			entitySet.ElementType.KeyProperties.Each(delegate(EdmProperty p)
			{
				addPrimaryKeyOperation.Columns.Add(p.Name);
			});
			IndexAttribute primaryKeyIndexAttribute = EdmModelDiffer.GetPrimaryKeyIndexAttribute(entitySet.ElementType);
			if (primaryKeyIndexAttribute != null)
			{
				addPrimaryKeyOperation.Name = primaryKeyIndexAttribute.Name;
				if (primaryKeyIndexAttribute.IsClusteredConfigured)
				{
					addPrimaryKeyOperation.IsClustered = primaryKeyIndexAttribute.IsClustered;
				}
			}
			createTableOperation.PrimaryKey = addPrimaryKeyOperation;
			return createTableOperation;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00024ECC File Offset: 0x000230CC
		private static ColumnModel BuildColumnModel(EdmProperty property, EdmModelDiffer.ModelMetadata modelMetadata, IDictionary<string, AnnotationValues> annotations)
		{
			TypeUsage edmType = modelMetadata.ProviderManifest.GetEdmType(property.TypeUsage);
			TypeUsage storeType = modelMetadata.ProviderManifest.GetStoreType(edmType);
			return EdmModelDiffer.BuildColumnModel(property, edmType, storeType, annotations);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00024F04 File Offset: 0x00023104
		public static ColumnModel BuildColumnModel(EdmProperty property, TypeUsage conceptualTypeUsage, TypeUsage defaultStoreTypeUsage, IDictionary<string, AnnotationValues> annotations)
		{
			ColumnModel columnModel = new ColumnModel(property.PrimitiveType.PrimitiveTypeKind, conceptualTypeUsage);
			columnModel.Name = property.Name;
			columnModel.IsNullable = ((!property.Nullable) ? new bool?(false) : null);
			columnModel.StoreType = ((!property.TypeName.EqualsIgnoreCase(defaultStoreTypeUsage.EdmType.Name)) ? property.TypeName : null);
			columnModel.IsIdentity = property.IsStoreGeneratedIdentity && EdmModelDiffer._validIdentityTypes.Contains(property.PrimitiveType.PrimitiveTypeKind);
			bool flag;
			if (property.PrimitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary)
			{
				int? maxLength = property.MaxLength;
				int num = 8;
				if ((maxLength.GetValueOrDefault() == num) & (maxLength != null))
				{
					flag = property.IsStoreGeneratedComputed;
					goto IL_00BF;
				}
			}
			flag = false;
			IL_00BF:
			columnModel.IsTimestamp = flag;
			bool? flag2 = property.IsUnicode;
			bool flag3 = false;
			columnModel.IsUnicode = (((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) ? new bool?(false) : null);
			flag2 = property.IsFixedLength;
			flag3 = true;
			columnModel.IsFixedLength = (((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) ? new bool?(true) : null);
			columnModel.Annotations = annotations;
			ColumnModel columnModel2 = columnModel;
			Facet facet;
			if (property.TypeUsage.Facets.TryGetValue("MaxLength", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel2.MaxLength = (int?)facet.Value;
			}
			if (property.TypeUsage.Facets.TryGetValue("Precision", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel2.Precision = (byte?)facet.Value;
			}
			if (property.TypeUsage.Facets.TryGetValue("Scale", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel2.Scale = (byte?)facet.Value;
			}
			return columnModel2;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0002510A File Offset: 0x0002330A
		private static DbProviderManifest GetProviderManifest(DbProviderInfo providerInfo)
		{
			return DbConfiguration.DependencyResolver.GetService(providerInfo.ProviderInvariantName).GetProviderServices().GetProviderManifest(providerInfo.ProviderManifestToken);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0002512C File Offset: 0x0002332C
		private static string GetSchemaQualifiedName(EntitySet entitySet)
		{
			return new DatabaseName(entitySet.Table, entitySet.Schema).ToString();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00025144 File Offset: 0x00023344
		private static string GetSchemaQualifiedName(EdmFunction function)
		{
			return new DatabaseName(function.FunctionName, function.Schema).ToString();
		}

		// Token: 0x040008A0 RID: 2208
		private static readonly PrimitiveTypeKind[] _validIdentityTypes = new PrimitiveTypeKind[]
		{
			PrimitiveTypeKind.Byte,
			PrimitiveTypeKind.Decimal,
			PrimitiveTypeKind.Guid,
			PrimitiveTypeKind.Int16,
			PrimitiveTypeKind.Int32,
			PrimitiveTypeKind.Int64
		};

		// Token: 0x040008A1 RID: 2209
		private static readonly DynamicEqualityComparer<ForeignKeyOperation> _foreignKeyEqualityComparer = new DynamicEqualityComparer<ForeignKeyOperation>((ForeignKeyOperation fk1, ForeignKeyOperation fk2) => fk1.Name.EqualsOrdinal(fk2.Name));

		// Token: 0x040008A2 RID: 2210
		private static readonly DynamicEqualityComparer<IndexOperation> _indexEqualityComparer = new DynamicEqualityComparer<IndexOperation>((IndexOperation i1, IndexOperation i2) => i1.Name.EqualsOrdinal(i2.Name) && i1.Table.EqualsOrdinal(i2.Table));

		// Token: 0x040008A3 RID: 2211
		private EdmModelDiffer.ModelMetadata _source;

		// Token: 0x040008A4 RID: 2212
		private EdmModelDiffer.ModelMetadata _target;

		// Token: 0x0200075F RID: 1887
		private class ModelMetadata
		{
			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x060055F7 RID: 22007 RVA: 0x0013287D File Offset: 0x00130A7D
			// (set) Token: 0x060055F8 RID: 22008 RVA: 0x00132885 File Offset: 0x00130A85
			public EdmItemCollection EdmItemCollection { get; set; }

			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x060055F9 RID: 22009 RVA: 0x0013288E File Offset: 0x00130A8E
			// (set) Token: 0x060055FA RID: 22010 RVA: 0x00132896 File Offset: 0x00130A96
			public StoreItemCollection StoreItemCollection { get; set; }

			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x060055FB RID: 22011 RVA: 0x0013289F File Offset: 0x00130A9F
			// (set) Token: 0x060055FC RID: 22012 RVA: 0x001328A7 File Offset: 0x00130AA7
			public EntityContainerMapping EntityContainerMapping { get; set; }

			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x060055FD RID: 22013 RVA: 0x001328B0 File Offset: 0x00130AB0
			// (set) Token: 0x060055FE RID: 22014 RVA: 0x001328B8 File Offset: 0x00130AB8
			public EntityContainer StoreEntityContainer { get; set; }

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x060055FF RID: 22015 RVA: 0x001328C1 File Offset: 0x00130AC1
			// (set) Token: 0x06005600 RID: 22016 RVA: 0x001328C9 File Offset: 0x00130AC9
			public DbProviderManifest ProviderManifest { get; set; }

			// Token: 0x1700101C RID: 4124
			// (get) Token: 0x06005601 RID: 22017 RVA: 0x001328D2 File Offset: 0x00130AD2
			// (set) Token: 0x06005602 RID: 22018 RVA: 0x001328DA File Offset: 0x00130ADA
			public DbProviderInfo ProviderInfo { get; set; }
		}
	}
}
