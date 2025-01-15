using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000216 RID: 534
	internal class EntityMappingService
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x0004FD40 File Offset: 0x0004DF40
		public EntityMappingService(DbDatabaseMapping databaseMapping)
		{
			this._databaseMapping = databaseMapping;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0004FD4F File Offset: 0x0004DF4F
		public void Configure()
		{
			this.Analyze();
			this.Transform();
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0004FD60 File Offset: 0x0004DF60
		private void Analyze()
		{
			this._tableMappings = new Dictionary<EntityType, TableMapping>();
			this._entityTypes = new SortedEntityTypeIndex();
			foreach (EntitySetMapping entitySetMapping in this._databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings))
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					this._entityTypes.Add(entitySetMapping.EntitySet, entityTypeMapping.EntityType);
					foreach (MappingFragment mappingFragment in entityTypeMapping.MappingFragments)
					{
						this.FindOrCreateTableMapping(mappingFragment.Table).AddEntityTypeMappingFragment(entitySetMapping.EntitySet, entityTypeMapping.EntityType, mappingFragment);
					}
				}
			}
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0004FE94 File Offset: 0x0004E094
		private void Transform()
		{
			using (IEnumerator<EntitySet> enumerator = this._entityTypes.GetEntitySets().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntitySet entitySet = enumerator.Current;
					Dictionary<TableMapping, Dictionary<EntityType, EntityTypeMapping>> dictionary = new Dictionary<TableMapping, Dictionary<EntityType, EntityTypeMapping>>();
					using (IEnumerator<EntityType> enumerator2 = this._entityTypes.GetEntityTypes(entitySet).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EntityType entityType = enumerator2.Current;
							IEnumerable<TableMapping> values = this._tableMappings.Values;
							Func<TableMapping, bool> func;
							Func<TableMapping, bool> <>9__0;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (TableMapping tm) => tm.EntityTypes.Contains(entitySet, entityType));
							}
							Func<ColumnMappingBuilder, bool> <>9__1;
							Func<ColumnMappingBuilder, bool> <>9__2;
							foreach (TableMapping tableMapping in values.Where(func))
							{
								Dictionary<EntityType, EntityTypeMapping> dictionary2;
								if (!dictionary.TryGetValue(tableMapping, out dictionary2))
								{
									dictionary2 = new Dictionary<EntityType, EntityTypeMapping>();
									dictionary.Add(tableMapping, dictionary2);
								}
								EntityMappingService.RemoveRedundantDefaultDiscriminators(tableMapping);
								bool flag = this.DetermineRequiresIsTypeOf(tableMapping, entitySet, entityType);
								EntityTypeMapping propertiesTypeMapping;
								MappingFragment propertiesTypeMappingFragment;
								if (this.FindPropertyEntityTypeMapping(tableMapping, entitySet, entityType, flag, out propertiesTypeMapping, out propertiesTypeMappingFragment))
								{
									bool flag2 = EntityMappingService.DetermineRequiresSplitEntityTypeMapping(tableMapping, entityType, flag);
									EntityTypeMapping entityTypeMapping = this.FindConditionTypeMapping(entityType, flag2, propertiesTypeMapping);
									MappingFragment mappingFragment = EntityMappingService.FindConditionTypeMappingFragment(this._databaseMapping.Database.GetEntitySet(tableMapping.Table), propertiesTypeMappingFragment, entityTypeMapping);
									if (flag)
									{
										if (!propertiesTypeMapping.IsHierarchyMapping)
										{
											EntityTypeMapping entityTypeMapping2 = this._databaseMapping.GetEntityTypeMappings(entityType).SingleOrDefault((EntityTypeMapping etm) => etm.IsHierarchyMapping);
											if (entityTypeMapping2 == null)
											{
												if (propertiesTypeMapping.MappingFragments.Count > 1)
												{
													EntityTypeMapping entityTypeMapping3 = propertiesTypeMapping.Clone();
													this._databaseMapping.GetEntitySetMappings().Single((EntitySetMapping esm) => esm.EntityTypeMappings.Contains(propertiesTypeMapping)).AddTypeMapping(entityTypeMapping3);
													IEnumerable<MappingFragment> mappingFragments = propertiesTypeMapping.MappingFragments;
													Func<MappingFragment, bool> func2;
													Func<MappingFragment, bool> <>9__5;
													if ((func2 = <>9__5) == null)
													{
														func2 = (<>9__5 = (MappingFragment tmf) => tmf != propertiesTypeMappingFragment);
													}
													foreach (MappingFragment mappingFragment2 in mappingFragments.Where(func2).ToArray<MappingFragment>())
													{
														propertiesTypeMapping.RemoveFragment(mappingFragment2);
														entityTypeMapping3.AddFragment(mappingFragment2);
													}
												}
												propertiesTypeMapping.AddIsOfType(propertiesTypeMapping.EntityType);
											}
											else
											{
												propertiesTypeMapping.RemoveFragment(propertiesTypeMappingFragment);
												if (propertiesTypeMapping.MappingFragments.Count == 0)
												{
													this._databaseMapping.GetEntitySetMapping(entitySet).RemoveTypeMapping(propertiesTypeMapping);
												}
												propertiesTypeMapping = entityTypeMapping2;
												propertiesTypeMapping.AddFragment(propertiesTypeMappingFragment);
											}
										}
										dictionary2.Add(entityType, propertiesTypeMapping);
									}
									EntityMappingService.ConfigureTypeMappings(tableMapping, dictionary2, entityType, propertiesTypeMappingFragment, mappingFragment);
									if (propertiesTypeMappingFragment.IsUnmappedPropertiesFragment())
									{
										IEnumerable<ColumnMappingBuilder> columnMappings = propertiesTypeMappingFragment.ColumnMappings;
										Func<ColumnMappingBuilder, bool> func3;
										if ((func3 = <>9__1) == null)
										{
											func3 = (<>9__1 = (ColumnMappingBuilder pm) => entityType.GetKeyProperties().Contains(pm.PropertyPath.First<EdmProperty>()));
										}
										if (columnMappings.All(func3))
										{
											this.RemoveFragment(entitySet, propertiesTypeMapping, propertiesTypeMappingFragment);
											if (flag2)
											{
												IEnumerable<ColumnMappingBuilder> columnMappings2 = mappingFragment.ColumnMappings;
												Func<ColumnMappingBuilder, bool> func4;
												if ((func4 = <>9__2) == null)
												{
													func4 = (<>9__2 = (ColumnMappingBuilder pm) => entityType.GetKeyProperties().Contains(pm.PropertyPath.First<EdmProperty>()));
												}
												if (columnMappings2.All(func4))
												{
													this.RemoveFragment(entitySet, entityTypeMapping, mappingFragment);
												}
											}
										}
									}
									EntityMappingConfiguration.CleanupUnmappedArtifacts(this._databaseMapping, tableMapping.Table);
									foreach (ForeignKeyBuilder foreignKeyBuilder in tableMapping.Table.ForeignKeyBuilders)
									{
										AssociationType associationType = foreignKeyBuilder.GetAssociationType();
										if (associationType != null && associationType.IsRequiredToNonRequired())
										{
											AssociationEndMember associationEndMember;
											AssociationEndMember associationEndMember2;
											foreignKeyBuilder.GetAssociationType().TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2);
											if (associationEndMember2.GetEntityType() == entityType)
											{
												this.MarkColumnsAsNonNullableIfNoTableSharing(entitySet, tableMapping.Table, entityType, foreignKeyBuilder.DependentColumns);
											}
										}
									}
								}
							}
						}
					}
					this.ConfigureAssociationSetMappingForeignKeys(entitySet);
				}
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000503E8 File Offset: 0x0004E5E8
		private void ConfigureAssociationSetMappingForeignKeys(EntitySet entitySet)
		{
			IEnumerable<AssociationSetMapping> enumerable = this._databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings);
			Func<AssociationSetMapping, bool> <>9__1;
			Func<AssociationSetMapping, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (AssociationSetMapping asm) => (asm.AssociationSet.SourceSet == entitySet || asm.AssociationSet.TargetSet == entitySet) && asm.AssociationSet.ElementType.IsRequiredToNonRequired());
			}
			foreach (AssociationSetMapping associationSetMapping in enumerable.Where(func))
			{
				AssociationEndMember associationEndMember;
				AssociationEndMember associationEndMember2;
				associationSetMapping.AssociationSet.ElementType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2);
				if ((associationEndMember2 == associationSetMapping.AssociationSet.ElementType.SourceEnd && associationSetMapping.AssociationSet.SourceSet == entitySet) || (associationEndMember2 == associationSetMapping.AssociationSet.ElementType.TargetEnd && associationSetMapping.AssociationSet.TargetSet == entitySet))
				{
					EndPropertyMapping endPropertyMapping = ((associationSetMapping.SourceEndMapping.AssociationEnd == associationEndMember2) ? associationSetMapping.TargetEndMapping : associationSetMapping.SourceEndMapping);
					this.MarkColumnsAsNonNullableIfNoTableSharing(entitySet, associationSetMapping.Table, associationEndMember2.GetEntityType(), endPropertyMapping.PropertyMappings.Select((ScalarPropertyMapping pm) => pm.Column));
				}
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00050554 File Offset: 0x0004E754
		private void MarkColumnsAsNonNullableIfNoTableSharing(EntitySet entitySet, EntityType table, EntityType dependentEndEntityType, IEnumerable<EdmProperty> columns)
		{
			IEnumerable<EntityType> enumerable = from et in this._tableMappings[table].EntityTypes.GetEntityTypes(entitySet)
				where et != dependentEndEntityType && (et.IsAncestorOf(dependentEndEntityType) || !dependentEndEntityType.IsAncestorOf(et))
				select et;
			if (enumerable.Count<EntityType>() != 0)
			{
				if (!enumerable.All((EntityType et) => et.Abstract))
				{
					return;
				}
			}
			columns.Each((EdmProperty c) => c.Nullable = false);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000505EC File Offset: 0x0004E7EC
		private static void ConfigureTypeMappings(TableMapping tableMapping, Dictionary<EntityType, EntityTypeMapping> rootMappings, EntityType entityType, MappingFragment propertiesTypeMappingFragment, MappingFragment conditionTypeMappingFragment)
		{
			List<ColumnMappingBuilder> list = new List<ColumnMappingBuilder>(propertiesTypeMappingFragment.ColumnMappings.Where((ColumnMappingBuilder pm) => !pm.ColumnProperty.IsPrimaryKeyColumn));
			List<ConditionPropertyMapping> list2 = new List<ConditionPropertyMapping>(propertiesTypeMappingFragment.ColumnConditions);
			var enumerable = from cm in tableMapping.ColumnMappings
				from pm in cm.PropertyMappings
				select new { cm, pm };
			Func<<>f__AnonymousType47<ColumnMapping, PropertyMappingSpecification>, bool> <>9__3;
			var func;
			if ((func = <>9__3) == null)
			{
				func = (<>9__3 = <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.pm.EntityType == entityType);
			}
			using (var enumerator = (from <>h__TransparentIdentifier0 in enumerable.Where(func)
				select new
				{
					Column = <>h__TransparentIdentifier0.cm.Column,
					Property = <>h__TransparentIdentifier0.pm
				}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>f__AnonymousType48<EdmProperty, PropertyMappingSpecification> columnMapping = enumerator.Current;
					if (columnMapping.Property.PropertyPath != null && !EntityMappingService.IsRootTypeMapping(rootMappings, columnMapping.Property.EntityType, columnMapping.Property.PropertyPath))
					{
						ColumnMappingBuilder columnMappingBuilder = propertiesTypeMappingFragment.ColumnMappings.SingleOrDefault((ColumnMappingBuilder x) => x.PropertyPath == columnMapping.Property.PropertyPath);
						if (columnMappingBuilder != null)
						{
							list.Remove(columnMappingBuilder);
						}
						else
						{
							columnMappingBuilder = new ColumnMappingBuilder(columnMapping.Column, columnMapping.Property.PropertyPath);
							propertiesTypeMappingFragment.AddColumnMapping(columnMappingBuilder);
						}
					}
					if (columnMapping.Property.Conditions != null)
					{
						foreach (ConditionPropertyMapping conditionPropertyMapping in columnMapping.Property.Conditions)
						{
							if (conditionTypeMappingFragment.ColumnConditions.Contains(conditionPropertyMapping))
							{
								list2.Remove(conditionPropertyMapping);
							}
							else if (!entityType.Abstract)
							{
								conditionTypeMappingFragment.AddConditionProperty(conditionPropertyMapping);
							}
						}
					}
				}
			}
			foreach (ColumnMappingBuilder columnMappingBuilder2 in list)
			{
				propertiesTypeMappingFragment.RemoveColumnMapping(columnMappingBuilder2);
			}
			foreach (ConditionPropertyMapping conditionPropertyMapping2 in list2)
			{
				conditionTypeMappingFragment.RemoveConditionProperty(conditionPropertyMapping2);
			}
			if (entityType.Abstract)
			{
				propertiesTypeMappingFragment.ClearConditions();
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000508FC File Offset: 0x0004EAFC
		private static MappingFragment FindConditionTypeMappingFragment(EntitySet tableSet, MappingFragment propertiesTypeMappingFragment, EntityTypeMapping conditionTypeMapping)
		{
			EntityType table = tableSet.ElementType;
			MappingFragment mappingFragment = conditionTypeMapping.MappingFragments.SingleOrDefault((MappingFragment x) => x.Table == table);
			if (mappingFragment == null)
			{
				mappingFragment = EntityMappingOperations.CreateTypeMappingFragment(conditionTypeMapping, propertiesTypeMappingFragment, tableSet);
				mappingFragment.SetIsConditionOnlyFragment(true);
				if (propertiesTypeMappingFragment.GetDefaultDiscriminator() != null)
				{
					mappingFragment.SetDefaultDiscriminator(propertiesTypeMappingFragment.GetDefaultDiscriminator());
					propertiesTypeMappingFragment.RemoveDefaultDiscriminatorAnnotation();
				}
			}
			return mappingFragment;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00050964 File Offset: 0x0004EB64
		private EntityTypeMapping FindConditionTypeMapping(EntityType entityType, bool requiresSplit, EntityTypeMapping propertiesTypeMapping)
		{
			EntityTypeMapping entityTypeMapping = propertiesTypeMapping;
			if (requiresSplit)
			{
				if (!entityType.Abstract)
				{
					entityTypeMapping = propertiesTypeMapping.Clone();
					entityTypeMapping.RemoveIsOfType(entityTypeMapping.EntityType);
					this._databaseMapping.GetEntitySetMappings().Single((EntitySetMapping esm) => esm.EntityTypeMappings.Contains(propertiesTypeMapping)).AddTypeMapping(entityTypeMapping);
				}
				propertiesTypeMapping.MappingFragments.Each(delegate(MappingFragment tmf)
				{
					tmf.ClearConditions();
				});
			}
			return entityTypeMapping;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000509FC File Offset: 0x0004EBFC
		private bool DetermineRequiresIsTypeOf(TableMapping tableMapping, EntitySet entitySet, EntityType entityType)
		{
			Func<ForeignKeyBuilder, bool> <>9__2;
			return entityType.IsRootOfSet(tableMapping.EntityTypes.GetEntityTypes(entitySet)) && ((tableMapping.EntityTypes.GetEntityTypes(entitySet).Count<EntityType>() > 1 && tableMapping.EntityTypes.GetEntityTypes(entitySet).Any((EntityType et) => et != entityType && !et.Abstract)) || this._tableMappings.Values.Any(delegate(TableMapping tm)
			{
				if (tm != tableMapping)
				{
					IEnumerable<ForeignKeyBuilder> foreignKeyBuilders = tm.Table.ForeignKeyBuilders;
					Func<ForeignKeyBuilder, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (ForeignKeyBuilder fk) => fk.GetIsTypeConstraint() && fk.PrincipalTable == tableMapping.Table);
					}
					return foreignKeyBuilders.Any(func);
				}
				return false;
			}));
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00050A98 File Offset: 0x0004EC98
		private static bool DetermineRequiresSplitEntityTypeMapping(TableMapping tableMapping, EntityType entityType, bool requiresIsTypeOf)
		{
			return requiresIsTypeOf && EntityMappingService.HasConditions(tableMapping, entityType);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00050AA8 File Offset: 0x0004ECA8
		private bool FindPropertyEntityTypeMapping(TableMapping tableMapping, EntitySet entitySet, EntityType entityType, bool requiresIsTypeOf, out EntityTypeMapping entityTypeMapping, out MappingFragment fragment)
		{
			entityTypeMapping = null;
			fragment = null;
			var <>f__AnonymousType = (from etm in this._databaseMapping.GetEntityTypeMappings(entityType)
				from tmf in etm.MappingFragments
				where tmf.Table == tableMapping.Table
				select new
				{
					TypeMapping = etm,
					Fragment = tmf
				}).SingleOrDefault();
			if (<>f__AnonymousType == null)
			{
				return false;
			}
			entityTypeMapping = <>f__AnonymousType.TypeMapping;
			fragment = <>f__AnonymousType.Fragment;
			if (!requiresIsTypeOf && entityType.Abstract)
			{
				this.RemoveFragment(entitySet, <>f__AnonymousType.TypeMapping, <>f__AnonymousType.Fragment);
				return false;
			}
			return true;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00050B90 File Offset: 0x0004ED90
		private void RemoveFragment(EntitySet entitySet, EntityTypeMapping entityTypeMapping, MappingFragment fragment)
		{
			EdmProperty defaultDiscriminator = fragment.GetDefaultDiscriminator();
			if (defaultDiscriminator != null && entityTypeMapping.EntityType.BaseType != null && !entityTypeMapping.EntityType.Abstract)
			{
				ColumnMapping columnMapping = this._tableMappings[fragment.Table].ColumnMappings.SingleOrDefault((ColumnMapping cm) => cm.Column == defaultDiscriminator);
				if (columnMapping != null)
				{
					PropertyMappingSpecification propertyMappingSpecification = columnMapping.PropertyMappings.SingleOrDefault((PropertyMappingSpecification pm) => pm.EntityType == entityTypeMapping.EntityType);
					if (propertyMappingSpecification != null)
					{
						columnMapping.PropertyMappings.Remove(propertyMappingSpecification);
					}
				}
				defaultDiscriminator.Nullable = true;
			}
			if (entityTypeMapping.EntityType.Abstract)
			{
				IEnumerable<ColumnMapping> columnMappings = this._tableMappings[fragment.Table].ColumnMappings;
				Func<ColumnMapping, bool> <>9__2;
				Func<ColumnMapping, bool> func;
				if ((func = <>9__2) == null)
				{
					Func<PropertyMappingSpecification, bool> <>9__3;
					func = (<>9__2 = delegate(ColumnMapping cm)
					{
						IEnumerable<PropertyMappingSpecification> propertyMappings = cm.PropertyMappings;
						Func<PropertyMappingSpecification, bool> func2;
						if ((func2 = <>9__3) == null)
						{
							func2 = (<>9__3 = (PropertyMappingSpecification pm) => pm.EntityType == entityTypeMapping.EntityType);
						}
						return propertyMappings.All(func2);
					});
				}
				foreach (ColumnMapping columnMapping2 in columnMappings.Where(func))
				{
					fragment.Table.RemoveMember(columnMapping2.Column);
				}
			}
			entityTypeMapping.RemoveFragment(fragment);
			if (!entityTypeMapping.MappingFragments.Any<MappingFragment>())
			{
				this._databaseMapping.GetEntitySetMapping(entitySet).RemoveTypeMapping(entityTypeMapping);
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00050D10 File Offset: 0x0004EF10
		private static void RemoveRedundantDefaultDiscriminators(TableMapping tableMapping)
		{
			using (IEnumerator<EntitySet> enumerator = tableMapping.EntityTypes.GetEntitySets().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntitySet entitySet = enumerator.Current;
					(from cm in tableMapping.ColumnMappings
						from pm in cm.PropertyMappings
						where cm.PropertyMappings.Where((PropertyMappingSpecification pm1) => tableMapping.EntityTypes.GetEntityTypes(entitySet).Contains(pm1.EntityType)).Count((PropertyMappingSpecification pms) => pms.IsDefaultDiscriminatorCondition) == 1
						select new
						{
							ColumnMapping = cm,
							PropertyMapping = pm
						}).ToArray().Each(delegate(x)
					{
						x.PropertyMapping.Conditions.Clear();
						if (x.PropertyMapping.PropertyPath == null)
						{
							x.ColumnMapping.PropertyMappings.Remove(x.PropertyMapping);
						}
					});
				}
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00050E38 File Offset: 0x0004F038
		private static bool HasConditions(TableMapping tableMapping, EntityType entityType)
		{
			return tableMapping.ColumnMappings.SelectMany((ColumnMapping cm) => cm.PropertyMappings).Any((PropertyMappingSpecification pm) => pm.EntityType == entityType && pm.Conditions.Count > 0);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00050E90 File Offset: 0x0004F090
		private static bool IsRootTypeMapping(Dictionary<EntityType, EntityTypeMapping> rootMappings, EntityType entityType, IList<EdmProperty> propertyPath)
		{
			Func<ColumnMappingBuilder, bool> <>9__1;
			for (EntityType entityType2 = (EntityType)entityType.BaseType; entityType2 != null; entityType2 = (EntityType)entityType2.BaseType)
			{
				EntityTypeMapping entityTypeMapping;
				if (rootMappings.TryGetValue(entityType2, out entityTypeMapping))
				{
					IEnumerable<ColumnMappingBuilder> enumerable = entityTypeMapping.MappingFragments.SelectMany((MappingFragment etmf) => etmf.ColumnMappings);
					Func<ColumnMappingBuilder, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
					}
					return enumerable.Any(func);
				}
			}
			return false;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00050F24 File Offset: 0x0004F124
		private TableMapping FindOrCreateTableMapping(EntityType table)
		{
			TableMapping tableMapping;
			if (!this._tableMappings.TryGetValue(table, out tableMapping))
			{
				tableMapping = new TableMapping(table);
				this._tableMappings.Add(table, tableMapping);
			}
			return tableMapping;
		}

		// Token: 0x04000AE3 RID: 2787
		private readonly DbDatabaseMapping _databaseMapping;

		// Token: 0x04000AE4 RID: 2788
		private Dictionary<EntityType, TableMapping> _tableMappings;

		// Token: 0x04000AE5 RID: 2789
		private SortedEntityTypeIndex _entityTypes;
	}
}
