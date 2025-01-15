using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000210 RID: 528
	internal static class ForeignKeyPrimitiveOperations
	{
		// Token: 0x06001C0B RID: 7179 RVA: 0x0004EE30 File Offset: 0x0004D030
		public static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, bool isMappingAnyInheritedProperty)
		{
			if (fromTable != toTable)
			{
				ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, entityType, false);
				if (isMappingAnyInheritedProperty)
				{
					ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, (EntityType)entityType.BaseType, true);
				}
			}
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0004EE58 File Offset: 0x0004D058
		private static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType toTable, EntityType entityType, bool removeFks)
		{
			IEnumerable<AssociationType> associationTypes = databaseMapping.Model.AssociationTypes;
			Func<AssociationType, bool> <>9__0;
			Func<AssociationType, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (AssociationType at) => at.SourceEnd.GetEntityType().Equals(entityType) || at.TargetEnd.GetEntityType().Equals(entityType));
			}
			foreach (AssociationType associationType in associationTypes.Where(func))
			{
				ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, toTable, removeFks, associationType, entityType);
			}
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		private static void UpdatePrincipalTables(DbDatabaseMapping databaseMapping, EntityType toTable, bool removeFks, AssociationType associationType, EntityType et)
		{
			List<AssociationEndMember> list = new List<AssociationEndMember>();
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			if (associationType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2))
			{
				list.Add(associationEndMember);
			}
			else if (associationType.SourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && associationType.TargetEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				list.Add(associationType.SourceEnd);
				list.Add(associationType.TargetEnd);
			}
			else
			{
				list.Add(associationType.SourceEnd);
			}
			Func<EntityType, EntityTypeMapping> <>9__0;
			Func<MappingFragment, bool> <>9__5;
			Func<EntityTypeMapping, IEnumerable<MappingFragment>> <>9__2;
			Func<ColumnMappingBuilder, bool> <>9__8;
			Func<MappingFragment, KeyValuePair<EntityType, IEnumerable<EdmProperty>>> <>9__4;
			Func<AssociationSetMapping, bool> <>9__10;
			foreach (AssociationEndMember associationEndMember3 in list)
			{
				if (associationEndMember3.GetEntityType() == et)
				{
					IEnumerable<KeyValuePair<EntityType, IEnumerable<EdmProperty>>> enumerable3;
					if (associationType.Constraint != null)
					{
						EntityType entityType = associationType.GetOtherEnd(associationEndMember3).GetEntityType();
						IEnumerable<EntityType> selfAndAllDerivedTypes = databaseMapping.Model.GetSelfAndAllDerivedTypes(entityType);
						Func<EntityType, EntityTypeMapping> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (EntityType t) => databaseMapping.GetEntityTypeMapping(t));
						}
						IEnumerable<EntityTypeMapping> enumerable = from dm in selfAndAllDerivedTypes.Select(func)
							where dm != null
							select dm;
						Func<EntityTypeMapping, IEnumerable<MappingFragment>> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = delegate(EntityTypeMapping dm)
							{
								IEnumerable<MappingFragment> mappingFragments = dm.MappingFragments;
								Func<MappingFragment, bool> func6;
								if ((func6 = <>9__5) == null)
								{
									func6 = (<>9__5 = (MappingFragment tmf) => associationType.Constraint.ToProperties.All((EdmProperty p) => tmf.ColumnMappings.Any((ColumnMappingBuilder pm) => pm.PropertyPath.First<EdmProperty>() == p)));
								}
								return mappingFragments.Where(func6);
							});
						}
						IEnumerable<MappingFragment> enumerable2 = enumerable.SelectMany(func2).Distinct((MappingFragment f1, MappingFragment f2) => f1.Table == f2.Table);
						Func<MappingFragment, KeyValuePair<EntityType, IEnumerable<EdmProperty>>> func3;
						if ((func3 = <>9__4) == null)
						{
							func3 = (<>9__4 = delegate(MappingFragment df)
							{
								EntityType table2 = df.Table;
								IEnumerable<ColumnMappingBuilder> columnMappings = df.ColumnMappings;
								Func<ColumnMappingBuilder, bool> func7;
								if ((func7 = <>9__8) == null)
								{
									func7 = (<>9__8 = (ColumnMappingBuilder pm) => associationType.Constraint.ToProperties.Contains(pm.PropertyPath.First<EdmProperty>()));
								}
								return new KeyValuePair<EntityType, IEnumerable<EdmProperty>>(table2, from pm in columnMappings.Where(func7)
									select pm.ColumnProperty);
							});
						}
						enumerable3 = enumerable2.Select(func3);
					}
					else
					{
						IEnumerable<AssociationSetMapping> associationSetMappings = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AssociationSetMappings;
						Func<AssociationSetMapping, bool> func4;
						if ((func4 = <>9__10) == null)
						{
							func4 = (<>9__10 = (AssociationSetMapping asm) => asm.AssociationSet.ElementType == associationType);
						}
						AssociationSetMapping associationSetMapping = associationSetMappings.Single(func4);
						EntityType table = associationSetMapping.Table;
						IEnumerable<EdmProperty> enumerable4 = ((associationSetMapping.SourceEndMapping.AssociationEnd == associationEndMember3) ? associationSetMapping.SourceEndMapping.PropertyMappings : associationSetMapping.TargetEndMapping.PropertyMappings).Select((ScalarPropertyMapping pm) => pm.Column);
						enumerable3 = new KeyValuePair<EntityType, IEnumerable<EdmProperty>>[]
						{
							new KeyValuePair<EntityType, IEnumerable<EdmProperty>>(table, enumerable4)
						};
					}
					using (IEnumerator<KeyValuePair<EntityType, IEnumerable<EdmProperty>>> enumerator2 = enumerable3.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<EntityType, IEnumerable<EdmProperty>> tableInfo = enumerator2.Current;
							IEnumerable<ForeignKeyBuilder> foreignKeyBuilders = tableInfo.Key.ForeignKeyBuilders;
							Func<ForeignKeyBuilder, bool> func5;
							Func<ForeignKeyBuilder, bool> <>9__12;
							if ((func5 = <>9__12) == null)
							{
								func5 = (<>9__12 = (ForeignKeyBuilder fk) => fk.DependentColumns.SequenceEqual(tableInfo.Value));
							}
							foreach (ForeignKeyBuilder foreignKeyBuilder in foreignKeyBuilders.Where(func5).ToArray<ForeignKeyBuilder>())
							{
								if (removeFks)
								{
									tableInfo.Key.RemoveForeignKey(foreignKeyBuilder);
								}
								else if (foreignKeyBuilder.GetAssociationType() == null || foreignKeyBuilder.GetAssociationType() == associationType)
								{
									foreignKeyBuilder.PrincipalTable = toTable;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0004F26C File Offset: 0x0004D46C
		private static void MoveForeignKeyConstraint(EntityType fromTable, EntityType toTable, ForeignKeyBuilder fk)
		{
			fromTable.RemoveForeignKey(fk);
			if (fk.PrincipalTable == toTable)
			{
				if (fk.DependentColumns.All((EdmProperty c) => c.IsPrimaryKeyColumn))
				{
					return;
				}
			}
			IList<EdmProperty> dependentColumns = ForeignKeyPrimitiveOperations.GetDependentColumns(fk.DependentColumns.ToArray<EdmProperty>(), toTable.Properties);
			if (!ForeignKeyPrimitiveOperations.ContainsEquivalentForeignKey(toTable, fk.PrincipalTable, dependentColumns))
			{
				toTable.AddForeignKey(fk);
				fk.DependentColumns = dependentColumns;
			}
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		private static void CopyForeignKeyConstraint(EdmModel database, EntityType toTable, ForeignKeyBuilder fk, Func<EdmProperty, EdmProperty> selector = null)
		{
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(database, database.EntityTypes.SelectMany((EntityType t) => t.ForeignKeyBuilders).UniquifyName(fk.Name))
			{
				PrincipalTable = fk.PrincipalTable,
				DeleteAction = fk.DeleteAction
			};
			foreignKeyBuilder.SetPreferredName(fk.Name);
			IList<EdmProperty> dependentColumns = ForeignKeyPrimitiveOperations.GetDependentColumns((selector != null) ? fk.DependentColumns.Select(selector) : fk.DependentColumns, toTable.Properties);
			if (!ForeignKeyPrimitiveOperations.ContainsEquivalentForeignKey(toTable, foreignKeyBuilder.PrincipalTable, dependentColumns))
			{
				toTable.AddForeignKey(foreignKeyBuilder);
				foreignKeyBuilder.DependentColumns = dependentColumns;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0004F39C File Offset: 0x0004D59C
		private static bool ContainsEquivalentForeignKey(EntityType dependentTable, EntityType principalTable, IEnumerable<EdmProperty> columns)
		{
			return dependentTable.ForeignKeyBuilders.Any((ForeignKeyBuilder fk) => fk.PrincipalTable == principalTable && fk.DependentColumns.SequenceEqual(columns));
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		private static IList<EdmProperty> GetDependentColumns(IEnumerable<EdmProperty> sourceColumns, IEnumerable<EdmProperty> destinationColumns)
		{
			return sourceColumns.Select((EdmProperty sc) => destinationColumns.SingleOrDefault((EdmProperty dc) => string.Equals(dc.Name, sc.Name, StringComparison.Ordinal)) ?? destinationColumns.Single((EdmProperty dc) => string.Equals(dc.GetUnpreferredUniqueName(), sc.Name, StringComparison.Ordinal))).ToList<EdmProperty>();
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0004F408 File Offset: 0x0004D608
		private static IEnumerable<ForeignKeyBuilder> FindAllForeignKeyConstraintsForColumn(EntityType fromTable, EntityType toTable, EdmProperty column)
		{
			Func<EdmProperty, bool> <>9__1;
			return fromTable.ForeignKeyBuilders.Where(delegate(ForeignKeyBuilder fk)
			{
				if (fk.DependentColumns.Contains(column))
				{
					IEnumerable<EdmProperty> dependentColumns = fk.DependentColumns;
					Func<EdmProperty, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (EdmProperty c) => toTable.Properties.Any((EdmProperty nc) => string.Equals(nc.Name, c.Name, StringComparison.Ordinal) || string.Equals(nc.GetUnpreferredUniqueName(), c.Name, StringComparison.Ordinal)));
					}
					return dependentColumns.All(func);
				}
				return false;
			});
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0004F440 File Offset: 0x0004D640
		public static void CopyAllForeignKeyConstraintsForColumn(EdmModel database, EntityType fromTable, EntityType toTable, EdmProperty column, EdmProperty movedColumn)
		{
			Func<EdmProperty, EdmProperty> <>9__1;
			ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				EdmModel database2 = database;
				EntityType toTable2 = toTable;
				Func<EdmProperty, EdmProperty> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(EdmProperty c)
					{
						if (c != column)
						{
							return c;
						}
						return movedColumn;
					});
				}
				ForeignKeyPrimitiveOperations.CopyForeignKeyConstraint(database2, toTable2, fk, func);
			});
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0004F498 File Offset: 0x0004D698
		public static void MoveAllDeclaredForeignKeyConstraintsForPrimaryKeyColumns(EntityType entityType, EntityType fromTable, EntityType toTable)
		{
			Action<ForeignKeyBuilder> <>9__0;
			foreach (EdmProperty edmProperty in fromTable.KeyProperties)
			{
				IEnumerable<ForeignKeyBuilder> enumerable = ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, edmProperty).ToArray<ForeignKeyBuilder>();
				Action<ForeignKeyBuilder> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(ForeignKeyBuilder fk)
					{
						AssociationType associationType = fk.GetAssociationType();
						if (associationType != null && associationType.Constraint.ToRole.GetEntityType() == entityType && !fk.GetIsTypeConstraint())
						{
							ForeignKeyPrimitiveOperations.MoveForeignKeyConstraint(fromTable, toTable, fk);
						}
					});
				}
				enumerable.Each(action);
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0004F540 File Offset: 0x0004D740
		public static void CopyAllForeignKeyConstraintsForPrimaryKeyColumns(EdmModel database, EntityType fromTable, EntityType toTable)
		{
			Action<ForeignKeyBuilder> <>9__0;
			foreach (EdmProperty edmProperty in fromTable.KeyProperties)
			{
				IEnumerable<ForeignKeyBuilder> enumerable = ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, edmProperty).ToArray<ForeignKeyBuilder>();
				Action<ForeignKeyBuilder> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(ForeignKeyBuilder fk)
					{
						if (!fk.GetIsTypeConstraint())
						{
							ForeignKeyPrimitiveOperations.CopyForeignKeyConstraint(database, toTable, fk, null);
						}
					});
				}
				enumerable.Each(action);
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0004F5D8 File Offset: 0x0004D7D8
		public static void MoveAllForeignKeyConstraintsForColumn(EntityType fromTable, EntityType toTable, EdmProperty column)
		{
			ForeignKeyPrimitiveOperations.FindAllForeignKeyConstraintsForColumn(fromTable, toTable, column).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				ForeignKeyPrimitiveOperations.MoveForeignKeyConstraint(fromTable, toTable, fk);
			});
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0004F624 File Offset: 0x0004D824
		public static void RemoveAllForeignKeyConstraintsForColumn(EntityType table, EdmProperty column, DbDatabaseMapping databaseMapping)
		{
			table.ForeignKeyBuilders.Where((ForeignKeyBuilder fk) => fk.DependentColumns.Contains(column)).ToArray<ForeignKeyBuilder>().Each(delegate(ForeignKeyBuilder fk)
			{
				table.RemoveForeignKey(fk);
				ForeignKeyBuilder foreignKeyBuilder = databaseMapping.Database.EntityTypes.SelectMany((EntityType t) => t.ForeignKeyBuilders).SingleOrDefault((ForeignKeyBuilder fk2) => object.Equals(fk2.GetPreferredName(), fk.Name));
				if (foreignKeyBuilder != null)
				{
					foreignKeyBuilder.Name = foreignKeyBuilder.GetPreferredName();
				}
			});
		}
	}
}
