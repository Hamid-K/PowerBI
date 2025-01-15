using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x0200020E RID: 526
	internal class EntityMappingConfiguration
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x0004D394 File Offset: 0x0004B594
		internal EntityMappingConfiguration()
		{
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0004D3C8 File Offset: 0x0004B5C8
		private EntityMappingConfiguration(EntityMappingConfiguration source)
		{
			this._tableName = source._tableName;
			this.MapInheritedProperties = source.MapInheritedProperties;
			if (source._properties != null)
			{
				this._properties = new List<PropertyPath>(source._properties);
			}
			this._valueConditions.AddRange(source._valueConditions.Select((ValueConditionConfiguration c) => c.Clone(this)));
			this._notNullConditions.AddRange(source._notNullConditions.Select((NotNullConditionConfiguration c) => c.Clone(this)));
			source._primitivePropertyConfigurations.Each(delegate(KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> c)
			{
				this._primitivePropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			foreach (KeyValuePair<string, object> keyValuePair in source._annotations)
			{
				this._annotations.Add(keyValuePair);
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0004D4D8 File Offset: 0x0004B6D8
		internal virtual EntityMappingConfiguration Clone()
		{
			return new EntityMappingConfiguration(this);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0004D4E0 File Offset: 0x0004B6E0
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0004D4E8 File Offset: 0x0004B6E8
		public bool MapInheritedProperties { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0004D4F1 File Offset: 0x0004B6F1
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0004D4F9 File Offset: 0x0004B6F9
		public DatabaseName TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0004D502 File Offset: 0x0004B702
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0004D50A File Offset: 0x0004B70A
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0004D52D File Offset: 0x0004B72D
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0004D535 File Offset: 0x0004B735
		internal List<PropertyPath> Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				if (this._properties == null)
				{
					this._properties = new List<PropertyPath>();
				}
				value.Each(new Action<PropertyPath>(this.Property));
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0004D55C File Offset: 0x0004B75C
		internal IDictionary<PropertyPath, PrimitivePropertyConfiguration> PrimitivePropertyConfigurations
		{
			get
			{
				return this._primitivePropertyConfigurations;
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0004D564 File Offset: 0x0004B764
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(PropertyPath propertyPath, Func<TPrimitivePropertyConfiguration> primitivePropertyConfigurationCreator) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration
		{
			if (this._properties == null)
			{
				this._properties = new List<PropertyPath>();
			}
			this.Property(propertyPath);
			PrimitivePropertyConfiguration primitivePropertyConfiguration;
			if (!this._primitivePropertyConfigurations.TryGetValue(propertyPath, out primitivePropertyConfiguration))
			{
				this._primitivePropertyConfigurations.Add(propertyPath, primitivePropertyConfiguration = primitivePropertyConfigurationCreator());
			}
			return (TPrimitivePropertyConfiguration)((object)primitivePropertyConfiguration);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		private void Property(PropertyPath property)
		{
			if (!this._properties.Where((PropertyPath pp) => pp.SequenceEqual(property)).Any<PropertyPath>())
			{
				this._properties.Add(property);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0004D605 File Offset: 0x0004B805
		public List<ValueConditionConfiguration> ValueConditions
		{
			get
			{
				return this._valueConditions;
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0004D610 File Offset: 0x0004B810
		public void AddValueCondition(ValueConditionConfiguration valueCondition)
		{
			ValueConditionConfiguration valueConditionConfiguration = this.ValueConditions.SingleOrDefault((ValueConditionConfiguration vc) => vc.Discriminator.Equals(valueCondition.Discriminator, StringComparison.Ordinal));
			if (valueConditionConfiguration == null)
			{
				this.ValueConditions.Add(valueCondition);
				return;
			}
			valueConditionConfiguration.Value = valueCondition.Value;
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0004D668 File Offset: 0x0004B868
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0004D670 File Offset: 0x0004B870
		public List<NotNullConditionConfiguration> NullabilityConditions
		{
			get
			{
				return this._notNullConditions;
			}
			set
			{
				value.Each(new Action<NotNullConditionConfiguration>(this.AddNullabilityCondition));
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0004D684 File Offset: 0x0004B884
		public void AddNullabilityCondition(NotNullConditionConfiguration notNullConditionConfiguration)
		{
			if (!this.NullabilityConditions.Contains(notNullConditionConfiguration))
			{
				this.NullabilityConditions.Add(notNullConditionConfiguration);
			}
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0004D6A0 File Offset: 0x0004B8A0
		public bool MapsAnyInheritedProperties(EntityType entityType)
		{
			HashSet<EdmPropertyPath> properties = new HashSet<EdmPropertyPath>();
			if (this.Properties != null)
			{
				this.Properties.Each(delegate(PropertyPath p)
				{
					properties.AddRange(EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType));
				});
			}
			return this.MapInheritedProperties || properties.Any((EdmPropertyPath x) => !entityType.KeyProperties().Contains(x.First<EdmProperty>()) && !entityType.DeclaredProperties.Contains(x.First<EdmProperty>()));
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0004D708 File Offset: 0x0004B908
		public void Configure(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, ref EntityTypeMapping entityTypeMapping, bool isMappingAnyInheritedProperty, int configurationIndex, int configurationCount, IDictionary<string, object> commonAnnotations)
		{
			EntityType baseType = (EntityType)entityType.BaseType;
			bool flag = baseType == null && configurationIndex == 0;
			MappingFragment mappingFragment = this.FindOrCreateTypeMappingFragment(databaseMapping, ref entityTypeMapping, configurationIndex, entityType, providerManifest);
			EntityType table = mappingFragment.Table;
			bool flag2;
			EntityType entityType2 = this.FindOrCreateTargetTable(databaseMapping, mappingFragment, entityType, table, out flag2);
			bool flag3 = this.DiscoverIsSharingWithBase(databaseMapping, entityType, entityType2);
			HashSet<EdmPropertyPath> hashSet = this.DiscoverAllMappingsToContain(databaseMapping, entityType, entityType2, flag3);
			List<ColumnMappingBuilder> list = mappingFragment.ColumnMappings.ToList<ColumnMappingBuilder>();
			using (HashSet<EdmPropertyPath>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmPropertyPath propertyPath = enumerator.Current;
					ColumnMappingBuilder columnMappingBuilder = mappingFragment.ColumnMappings.SingleOrDefault((ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
					if (columnMappingBuilder == null)
					{
						throw Error.EntityMappingConfiguration_DuplicateMappedProperty(entityType.Name, propertyPath.ToString());
					}
					list.Remove(columnMappingBuilder);
				}
			}
			if (!flag)
			{
				bool flag4;
				EntityType entityType3 = EntityMappingConfiguration.FindParentTable(databaseMapping, table, entityTypeMapping, entityType2, isMappingAnyInheritedProperty, configurationIndex, configurationCount, out flag4);
				if (entityType3 != null)
				{
					DatabaseOperations.AddTypeConstraint(databaseMapping.Database, entityType, entityType3, entityType2, flag4);
				}
			}
			if (table != entityType2)
			{
				if (this.Properties == null)
				{
					AssociationMappingOperations.MoveAllDeclaredAssociationSetMappings(databaseMapping, entityType, table, entityType2, !flag2);
					ForeignKeyPrimitiveOperations.MoveAllDeclaredForeignKeyConstraintsForPrimaryKeyColumns(entityType, table, entityType2);
				}
				if (isMappingAnyInheritedProperty)
				{
					IEnumerable<EntityType> baseTables = from mf in databaseMapping.GetEntityTypeMappings(baseType).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
						select mf.Table;
					AssociationSetMapping associationSetMapping = databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping asm) => asm.AssociationSetMappings).FirstOrDefault((AssociationSetMapping a) => baseTables.Contains(a.Table) && (baseType == a.AssociationSet.ElementType.SourceEnd.GetEntityType() || baseType == a.AssociationSet.ElementType.TargetEnd.GetEntityType()));
					if (associationSetMapping != null)
					{
						AssociationType elementType = associationSetMapping.AssociationSet.ElementType;
						throw Error.EntityMappingConfiguration_TPCWithIAsOnNonLeafType(elementType.Name, elementType.SourceEnd.GetEntityType().Name, elementType.TargetEnd.GetEntityType().Name);
					}
					ForeignKeyPrimitiveOperations.CopyAllForeignKeyConstraintsForPrimaryKeyColumns(databaseMapping.Database, table, entityType2);
				}
			}
			if (list.Any<ColumnMappingBuilder>())
			{
				EntityType extraTable = null;
				if (configurationIndex < configurationCount - 1)
				{
					ColumnMappingBuilder columnMappingBuilder2 = list.First<ColumnMappingBuilder>();
					extraTable = EntityMappingConfiguration.FindTableForTemporaryExtraPropertyMapping(databaseMapping, entityType, table, entityType2, columnMappingBuilder2);
					MappingFragment mappingFragment2 = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, mappingFragment, databaseMapping.Database.GetEntitySet(extraTable));
					bool flag5 = extraTable != table;
					using (List<ColumnMappingBuilder>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ColumnMappingBuilder columnMappingBuilder3 = enumerator2.Current;
							EntityMappingOperations.MovePropertyMapping(databaseMapping, entitySets, mappingFragment, mappingFragment2, columnMappingBuilder3, flag5, true);
						}
						goto IL_03BA;
					}
				}
				EntityType entityType4 = null;
				Func<MappingFragment, bool> <>9__5;
				foreach (ColumnMappingBuilder columnMappingBuilder4 in list)
				{
					extraTable = EntityMappingConfiguration.FindTableForExtraPropertyMapping(databaseMapping, entityType, table, entityType2, ref entityType4, columnMappingBuilder4);
					IEnumerable<MappingFragment> mappingFragments = entityTypeMapping.MappingFragments;
					Func<MappingFragment, bool> func;
					if ((func = <>9__5) == null)
					{
						func = (<>9__5 = (MappingFragment tmf) => tmf.Table == extraTable);
					}
					MappingFragment mappingFragment3 = mappingFragments.SingleOrDefault(func);
					if (mappingFragment3 == null)
					{
						mappingFragment3 = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, mappingFragment, databaseMapping.Database.GetEntitySet(extraTable));
						mappingFragment3.SetIsUnmappedPropertiesFragment(true);
					}
					if (extraTable == table)
					{
						EntityMappingConfiguration.CopyDefaultDiscriminator(mappingFragment, mappingFragment3);
					}
					bool flag6 = extraTable != table;
					EntityMappingOperations.MovePropertyMapping(databaseMapping, entitySets, mappingFragment, mappingFragment3, columnMappingBuilder4, flag6, true);
				}
			}
			IL_03BA:
			EntityMappingOperations.UpdatePropertyMappings(databaseMapping, entitySets, table, mappingFragment, !flag2);
			this.ConfigureDefaultDiscriminator(entityType, mappingFragment);
			this.ConfigureConditions(databaseMapping, entityType, mappingFragment, providerManifest);
			EntityMappingOperations.UpdateConditions(databaseMapping.Database, table, mappingFragment);
			ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, entityType, table, entityType2, isMappingAnyInheritedProperty);
			EntityMappingConfiguration.CleanupUnmappedArtifacts(databaseMapping, table);
			EntityMappingConfiguration.CleanupUnmappedArtifacts(databaseMapping, entityType2);
			EntityMappingConfiguration.ConfigureAnnotations(entityType2, commonAnnotations);
			EntityMappingConfiguration.ConfigureAnnotations(entityType2, this._annotations);
			entityType2.SetConfiguration(this);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0004DB60 File Offset: 0x0004BD60
		private static void ConfigureAnnotations(EdmType toTable, IDictionary<string, object> annotations)
		{
			using (IEnumerator<KeyValuePair<string, object>> enumerator = annotations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> annotation = enumerator.Current;
					string name = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + annotation.Key;
					MetadataProperty metadataProperty = toTable.Annotations.FirstOrDefault((MetadataProperty a) => a.Name == name && !object.Equals(a.Value, annotation.Value));
					if (metadataProperty != null)
					{
						throw new InvalidOperationException(Strings.ConflictingTypeAnnotation(annotation.Key, annotation.Value, metadataProperty.Value, toTable.Name));
					}
					toTable.AddAnnotation(name, annotation.Value);
				}
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0004DC2C File Offset: 0x0004BE2C
		internal void ConfigurePropertyMappings(IList<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this._primitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				keyValuePair.Value.Configure(propertyMappings.Where((Tuple<ColumnMappingBuilder, EntityType> pm) => propertyPath.Equals(new PropertyPath(from p in pm.Item1.PropertyPath.Skip(pm.Item1.PropertyPath.Count - propertyPath.Count)
					select p.GetClrPropertyInfo())) && object.Equals(this.TableName, pm.Item2.GetTableName())), providerManifest, allowOverride, true);
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
		private void ConfigureDefaultDiscriminator(EntityType entityType, MappingFragment fragment)
		{
			if (this.ValueConditions.Any<ValueConditionConfiguration>() || this.NullabilityConditions.Any<NotNullConditionConfiguration>())
			{
				EdmProperty edmProperty = fragment.RemoveDefaultDiscriminatorCondition();
				if (edmProperty != null && entityType.BaseType != null)
				{
					edmProperty.Nullable = true;
				}
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0004DCF4 File Offset: 0x0004BEF4
		private static void CopyDefaultDiscriminator(MappingFragment fromFragment, MappingFragment toFragment)
		{
			EdmProperty discriminatorColumn = fromFragment.GetDefaultDiscriminator();
			if (discriminatorColumn != null)
			{
				ConditionPropertyMapping conditionPropertyMapping = fromFragment.ColumnConditions.SingleOrDefault((ConditionPropertyMapping cc) => cc.Column == discriminatorColumn);
				if (conditionPropertyMapping != null)
				{
					toFragment.AddDiscriminatorCondition(conditionPropertyMapping.Column, conditionPropertyMapping.Value);
					toFragment.SetDefaultDiscriminator(conditionPropertyMapping.Column);
				}
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0004DD54 File Offset: 0x0004BF54
		private static EntityType FindTableForTemporaryExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, ColumnMappingBuilder pm)
		{
			EntityType entityType2;
			if (fromTable == toTable)
			{
				entityType2 = databaseMapping.Database.AddTable(entityType.Name, fromTable);
			}
			else if (entityType.BaseType == null)
			{
				entityType2 = fromTable;
			}
			else
			{
				entityType2 = EntityMappingConfiguration.FindBaseTableForExtraPropertyMapping(databaseMapping, entityType, pm);
				if (entityType2 == null)
				{
					entityType2 = fromTable;
				}
			}
			return entityType2;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0004DD98 File Offset: 0x0004BF98
		private static EntityType FindTableForExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, ref EntityType unmappedTable, ColumnMappingBuilder pm)
		{
			EntityType entityType2 = EntityMappingConfiguration.FindBaseTableForExtraPropertyMapping(databaseMapping, entityType, pm);
			if (entityType2 == null)
			{
				if (fromTable != toTable && entityType.BaseType == null)
				{
					return fromTable;
				}
				if (unmappedTable == null)
				{
					unmappedTable = databaseMapping.Database.AddTable(fromTable.Name, fromTable);
				}
				entityType2 = unmappedTable;
			}
			return entityType2;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0004DDE0 File Offset: 0x0004BFE0
		private static EntityType FindBaseTableForExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, ColumnMappingBuilder pm)
		{
			EntityType entityType2 = (EntityType)entityType.BaseType;
			MappingFragment mappingFragment = null;
			Func<ColumnMappingBuilder, bool> <>9__1;
			Func<MappingFragment, bool> <>9__0;
			while (entityType2 != null && mappingFragment == null)
			{
				EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType2);
				if (entityTypeMapping != null)
				{
					IEnumerable<MappingFragment> mappingFragments = entityTypeMapping.MappingFragments;
					Func<MappingFragment, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(MappingFragment f)
						{
							IEnumerable<ColumnMappingBuilder> columnMappings = f.ColumnMappings;
							Func<ColumnMappingBuilder, bool> func2;
							if ((func2 = <>9__1) == null)
							{
								func2 = (<>9__1 = (ColumnMappingBuilder bpm) => bpm.PropertyPath.SequenceEqual(pm.PropertyPath));
							}
							return columnMappings.Any(func2);
						});
					}
					mappingFragment = mappingFragments.SingleOrDefault(func);
					if (mappingFragment != null)
					{
						return mappingFragment.Table;
					}
				}
				entityType2 = (EntityType)entityType2.BaseType;
			}
			return null;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0004DE60 File Offset: 0x0004C060
		private bool DiscoverIsSharingWithBase(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable)
		{
			bool flag = false;
			if (entityType.BaseType != null)
			{
				EdmType edmType = entityType.BaseType;
				bool flag2 = false;
				Func<MappingFragment, bool> <>9__1;
				while (edmType != null && !flag)
				{
					IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings((EntityType)edmType);
					if (entityTypeMappings.Any<EntityTypeMapping>())
					{
						IEnumerable<MappingFragment> enumerable = entityTypeMappings.SelectMany((EntityTypeMapping m) => m.MappingFragments);
						Func<MappingFragment, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (MappingFragment tmf) => tmf.Table == toTable);
						}
						flag = enumerable.Any(func);
						flag2 = true;
					}
					edmType = edmType.BaseType;
				}
				if (!flag2)
				{
					flag = this.TableName == null || string.IsNullOrWhiteSpace(this.TableName.Name);
				}
			}
			return flag;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0004DF28 File Offset: 0x0004C128
		private static EntityType FindParentTable(DbDatabaseMapping databaseMapping, EntityType fromTable, EntityTypeMapping entityTypeMapping, EntityType toTable, bool isMappingInheritedProperties, int configurationIndex, int configurationCount, out bool isSplitting)
		{
			EntityType entityType = null;
			isSplitting = false;
			if ((entityTypeMapping.UsesOtherTables(toTable) || configurationCount > 1) && configurationIndex != 0)
			{
				entityType = entityTypeMapping.GetPrimaryTable();
				isSplitting = true;
			}
			if (entityType == null && fromTable != toTable && !isMappingInheritedProperties)
			{
				EdmType edmType = entityTypeMapping.EntityType.BaseType;
				while (edmType != null && entityType == null)
				{
					EntityTypeMapping entityTypeMapping2 = databaseMapping.GetEntityTypeMappings((EntityType)edmType).FirstOrDefault<EntityTypeMapping>();
					if (entityTypeMapping2 != null)
					{
						entityType = entityTypeMapping2.GetPrimaryTable();
					}
					edmType = edmType.BaseType;
				}
			}
			return entityType;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0004DF9C File Offset: 0x0004C19C
		private MappingFragment FindOrCreateTypeMappingFragment(DbDatabaseMapping databaseMapping, ref EntityTypeMapping entityTypeMapping, int configurationIndex, EntityType entityType, DbProviderManifest providerManifest)
		{
			if (entityTypeMapping == null)
			{
				new TableMappingGenerator(providerManifest).Generate(entityType, databaseMapping);
				entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType);
				configurationIndex = 0;
			}
			MappingFragment mappingFragment;
			if (configurationIndex < entityTypeMapping.MappingFragments.Count)
			{
				mappingFragment = entityTypeMapping.MappingFragments[configurationIndex];
			}
			else
			{
				if (this.MapInheritedProperties)
				{
					throw Error.EntityMappingConfiguration_DuplicateMapInheritedProperties(entityType.Name);
				}
				if (this.Properties == null)
				{
					throw Error.EntityMappingConfiguration_DuplicateMappedProperties(entityType.Name);
				}
				Func<EdmPropertyPath, bool> <>9__1;
				this.Properties.Each(delegate(PropertyPath p)
				{
					IEnumerable<EdmPropertyPath> enumerable = EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType);
					Func<EdmPropertyPath, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (EdmPropertyPath pp) => !entityType.KeyProperties().Contains(pp.First<EdmProperty>()));
					}
					if (enumerable.Any(func))
					{
						throw Error.EntityMappingConfiguration_DuplicateMappedProperty(entityType.Name, p.ToString());
					}
				});
				EntityType table = entityTypeMapping.MappingFragments[0].Table;
				EntityType entityType2 = databaseMapping.Database.AddTable(table.Name, table);
				mappingFragment = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, entityTypeMapping.MappingFragments[0], databaseMapping.Database.GetEntitySet(entityType2));
			}
			return mappingFragment;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0004E094 File Offset: 0x0004C294
		private EntityType FindOrCreateTargetTable(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType, EntityType fromTable, out bool isTableSharing)
		{
			isTableSharing = false;
			EntityType entityType2;
			if (this.TableName == null)
			{
				entityType2 = fragment.Table;
			}
			else
			{
				entityType2 = databaseMapping.Database.FindTableByName(this.TableName);
				if (entityType2 == null)
				{
					if (entityType.BaseType == null)
					{
						entityType2 = fragment.Table;
					}
					else
					{
						entityType2 = databaseMapping.Database.AddTable(this.TableName.Name, fromTable);
					}
				}
				isTableSharing = EntityMappingConfiguration.UpdateColumnNamesForTableSharing(databaseMapping, entityType, entityType2, fragment);
				fragment.TableSet = databaseMapping.Database.GetEntitySet(entityType2);
				using (IEnumerator<ColumnMappingBuilder> enumerator = fragment.ColumnMappings.Where((ColumnMappingBuilder cm) => cm.ColumnProperty.IsPrimaryKeyColumn).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ColumnMappingBuilder columnMapping = enumerator.Current;
						EdmProperty edmProperty = entityType2.Properties.SingleOrDefault((EdmProperty c) => string.Equals(c.Name, columnMapping.ColumnProperty.Name, StringComparison.Ordinal));
						columnMapping.ColumnProperty = edmProperty ?? columnMapping.ColumnProperty;
					}
				}
				entityType2.SetTableName(this.TableName);
			}
			return entityType2;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		private HashSet<EdmPropertyPath> DiscoverAllMappingsToContain(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable, bool isSharingTableWithBase)
		{
			HashSet<EdmPropertyPath> mappingsToContain = new HashSet<EdmPropertyPath>();
			entityType.KeyProperties().Each(delegate(EdmProperty p)
			{
				mappingsToContain.AddRange(p.ToPropertyPathList());
			});
			if (this.MapInheritedProperties)
			{
				entityType.Properties.Except(entityType.DeclaredProperties).Each(delegate(EdmProperty p)
				{
					mappingsToContain.AddRange(p.ToPropertyPathList());
				});
			}
			if (isSharingTableWithBase)
			{
				HashSet<EdmPropertyPath> baseMappingsToContain = new HashSet<EdmPropertyPath>();
				EntityType entityType2 = (EntityType)entityType.BaseType;
				EntityTypeMapping entityTypeMapping = null;
				MappingFragment mappingFragment = null;
				Func<MappingFragment, bool> <>9__4;
				while (entityType2 != null && entityTypeMapping == null)
				{
					entityTypeMapping = databaseMapping.GetEntityTypeMapping((EntityType)entityType.BaseType);
					if (entityTypeMapping != null)
					{
						IEnumerable<MappingFragment> mappingFragments = entityTypeMapping.MappingFragments;
						Func<MappingFragment, bool> func;
						if ((func = <>9__4) == null)
						{
							func = (<>9__4 = (MappingFragment tmf) => tmf.Table == toTable);
						}
						mappingFragment = mappingFragments.SingleOrDefault(func);
					}
					if (mappingFragment == null)
					{
						entityType2.DeclaredProperties.Each(delegate(EdmProperty p)
						{
							baseMappingsToContain.AddRange(p.ToPropertyPathList());
						});
					}
					entityType2 = (EntityType)entityType2.BaseType;
				}
				if (mappingFragment != null)
				{
					foreach (ColumnMappingBuilder columnMappingBuilder in mappingFragment.ColumnMappings)
					{
						mappingsToContain.Add(new EdmPropertyPath(columnMappingBuilder.PropertyPath));
					}
				}
				mappingsToContain.AddRange(baseMappingsToContain);
			}
			if (this.Properties == null)
			{
				entityType.DeclaredProperties.Each(delegate(EdmProperty p)
				{
					mappingsToContain.AddRange(p.ToPropertyPathList());
				});
			}
			else
			{
				this.Properties.Each(delegate(PropertyPath p)
				{
					mappingsToContain.AddRange(EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType));
				});
			}
			return mappingsToContain;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0004E394 File Offset: 0x0004C594
		private void ConfigureConditions(DbDatabaseMapping databaseMapping, EntityType entityType, MappingFragment fragment, DbProviderManifest providerManifest)
		{
			if (this.ValueConditions.Any<ValueConditionConfiguration>() || this.NullabilityConditions.Any<NotNullConditionConfiguration>())
			{
				fragment.ClearConditions();
				foreach (ValueConditionConfiguration valueConditionConfiguration in this.ValueConditions)
				{
					valueConditionConfiguration.Configure(databaseMapping, fragment, entityType, providerManifest);
				}
				foreach (NotNullConditionConfiguration notNullConditionConfiguration in this.NullabilityConditions)
				{
					notNullConditionConfiguration.Configure(databaseMapping, fragment, entityType);
				}
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0004E44C File Offset: 0x0004C64C
		internal static void CleanupUnmappedArtifacts(DbDatabaseMapping databaseMapping, EntityType table)
		{
			AssociationSetMapping[] array = (from asm in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings)
				where asm.Table == table
				select asm).ToArray<AssociationSetMapping>();
			MappingFragment[] array2 = (from f in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
				where f.Table == table
				select f).ToArray<MappingFragment>();
			if (!array.Any<AssociationSetMapping>() && !array2.Any<MappingFragment>())
			{
				databaseMapping.Database.RemoveEntityType(table);
				databaseMapping.Database.AssociationTypes.Where((AssociationType t) => t.SourceEnd.GetEntityType() == table || t.TargetEnd.GetEntityType() == table).ToArray<AssociationType>().Each(delegate(AssociationType t)
				{
					databaseMapping.Database.RemoveAssociationType(t);
				});
				return;
			}
			EdmProperty[] array3 = table.Properties.ToArray<EdmProperty>();
			for (int i = 0; i < array3.Length; i++)
			{
				EdmProperty column = array3[i];
				if (array2.SelectMany((MappingFragment f) => f.ColumnMappings).All((ColumnMappingBuilder pm) => pm.ColumnProperty != column))
				{
					if (array2.SelectMany((MappingFragment f) => f.ColumnConditions).All((ConditionPropertyMapping cc) => cc.Column != column))
					{
						if (array.SelectMany((AssociationSetMapping am) => am.SourceEndMapping.PropertyMappings).All((ScalarPropertyMapping pm) => pm.Column != column))
						{
							if (array.SelectMany((AssociationSetMapping am) => am.SourceEndMapping.PropertyMappings).All((ScalarPropertyMapping pm) => pm.Column != column))
							{
								ForeignKeyPrimitiveOperations.RemoveAllForeignKeyConstraintsForColumn(table, column, databaseMapping);
								TablePrimitiveOperations.RemoveColumn(table, column);
							}
						}
					}
				}
			}
			table.ForeignKeyBuilders.Where((ForeignKeyBuilder fk) => fk.PrincipalTable == table && fk.DependentColumns.SequenceEqual(table.KeyProperties)).ToArray<ForeignKeyBuilder>().Each(new Action<ForeignKeyBuilder>(table.RemoveForeignKey));
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0004E728 File Offset: 0x0004C928
		internal static IEnumerable<EdmPropertyPath> PropertyPathToEdmPropertyPath(PropertyPath path, EntityType entityType)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			StructuralType structuralType = entityType;
			int i;
			Func<EdmProperty, bool> <>9__0;
			int j;
			for (i = 0; i < path.Count; i = j + 1)
			{
				IEnumerable<EdmProperty> enumerable = structuralType.Members.OfType<EdmProperty>();
				Func<EdmProperty, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(path[i]));
				}
				EdmProperty edmProperty = enumerable.SingleOrDefault(func);
				if (edmProperty == null)
				{
					throw Error.EntityMappingConfiguration_CannotMapIgnoredProperty(entityType.Name, path.ToString());
				}
				list.Add(edmProperty);
				if (edmProperty.IsComplexType)
				{
					structuralType = edmProperty.ComplexType;
				}
				j = i;
			}
			EdmProperty edmProperty2 = list.Last<EdmProperty>();
			if (edmProperty2.IsUnderlyingPrimitiveType)
			{
				return new EdmPropertyPath[]
				{
					new EdmPropertyPath(list)
				};
			}
			if (edmProperty2.IsComplexType)
			{
				list.Remove(edmProperty2);
				return edmProperty2.ToPropertyPathList(list);
			}
			return new EdmPropertyPath[] { EdmPropertyPath.Empty };
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0004E828 File Offset: 0x0004CA28
		private static List<EntityTypeMapping> FindAllTypeMappingsUsingTable(DbDatabaseMapping databaseMapping, EntityType toTable)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			IList<EntityContainerMapping> entityContainerMappings = databaseMapping.EntityContainerMappings;
			for (int i = 0; i < entityContainerMappings.Count; i++)
			{
				List<EntitySetMapping> list2 = entityContainerMappings[i].EntitySetMappings.ToList<EntitySetMapping>();
				for (int j = 0; j < list2.Count; j++)
				{
					ReadOnlyCollection<EntityTypeMapping> entityTypeMappings = list2[j].EntityTypeMappings;
					for (int k = 0; k < entityTypeMappings.Count; k++)
					{
						EntityTypeMapping entityTypeMapping = entityTypeMappings[k];
						EntityTypeConfiguration entityTypeConfiguration = entityTypeMapping.EntityType.GetConfiguration() as EntityTypeConfiguration;
						for (int l = 0; l < entityTypeMapping.MappingFragments.Count; l++)
						{
							bool flag = entityTypeConfiguration != null && entityTypeConfiguration.IsTableNameConfigured;
							if ((!flag && entityTypeMapping.MappingFragments[l].Table == toTable) || (flag && EntityMappingConfiguration.IsTableNameEqual(toTable, entityTypeConfiguration.GetTableName())))
							{
								list.Add(entityTypeMapping);
								break;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0004E934 File Offset: 0x0004CB34
		private static bool IsTableNameEqual(EntityType table, DatabaseName otherTableName)
		{
			DatabaseName tableName = table.GetTableName();
			if (tableName != null)
			{
				return otherTableName.Equals(tableName);
			}
			return otherTableName.Name.Equals(table.Name, StringComparison.Ordinal) && otherTableName.Schema == null;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0004E974 File Offset: 0x0004CB74
		private static IEnumerable<AssociationType> FindAllOneToOneFKAssociationTypes(EdmModel model, EntityType entityType, EntityType candidateType)
		{
			List<AssociationType> list = new List<AssociationType>();
			foreach (EntityContainer entityContainer in model.Containers)
			{
				ReadOnlyMetadataCollection<AssociationSet> associationSets = entityContainer.AssociationSets;
				for (int i = 0; i < associationSets.Count; i++)
				{
					AssociationSet associationSet = associationSets[i];
					AssociationEndMember sourceEnd = associationSet.ElementType.SourceEnd;
					AssociationEndMember targetEnd = associationSet.ElementType.TargetEnd;
					EntityType entityType2 = sourceEnd.GetEntityType();
					EntityType entityType3 = targetEnd.GetEntityType();
					if (associationSet.ElementType.Constraint != null && sourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.One && targetEnd.RelationshipMultiplicity == RelationshipMultiplicity.One && ((entityType2 == entityType && entityType3 == candidateType) || (entityType3 == entityType && entityType2 == candidateType)))
					{
						list.Add(associationSet.ElementType);
					}
				}
			}
			return list;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0004EA5C File Offset: 0x0004CC5C
		private static bool UpdateColumnNamesForTableSharing(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable, MappingFragment fragment)
		{
			List<EntityTypeMapping> list = EntityMappingConfiguration.FindAllTypeMappingsUsingTable(databaseMapping, toTable);
			Dictionary<EntityType, List<AssociationType>> dictionary = new Dictionary<EntityType, List<AssociationType>>();
			foreach (EntityTypeMapping entityTypeMapping in list)
			{
				EntityType entityType2 = entityTypeMapping.EntityType;
				if (entityType != entityType2)
				{
					IEnumerable<AssociationType> enumerable = EntityMappingConfiguration.FindAllOneToOneFKAssociationTypes(databaseMapping.Model, entityType, entityType2);
					EntityType rootType = entityType2.GetRootType();
					if (!dictionary.ContainsKey(rootType))
					{
						dictionary.Add(rootType, enumerable.ToList<AssociationType>());
					}
					else
					{
						dictionary[rootType].AddRange(enumerable);
					}
				}
			}
			List<EntityType> list2 = new List<EntityType>();
			foreach (KeyValuePair<EntityType, List<AssociationType>> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != entityType.GetRootType() && keyValuePair.Value.Count == 0)
				{
					list2.Add(keyValuePair.Key);
				}
			}
			if (list2.Count > 0 && list2.Count == dictionary.Count)
			{
				DatabaseName tableName = toTable.GetTableName();
				throw Error.EntityMappingConfiguration_InvalidTableSharing(entityType.Name, list2.First<EntityType>().Name, (tableName != null) ? tableName.Name : databaseMapping.Database.GetEntitySet(toTable).Table);
			}
			IEnumerable<AssociationType> enumerable2 = dictionary.Values.SelectMany((List<AssociationType> l) => l);
			if (enumerable2.Any<AssociationType>())
			{
				AssociationType associationType = enumerable2.First<AssociationType>();
				EntityType entityType3 = associationType.Constraint.FromRole.GetEntityType();
				EntityType dependentEntityType = ((entityType == entityType3) ? associationType.Constraint.ToRole.GetEntityType() : entityType);
				MappingFragment mappingFragment = ((entityType == entityType3) ? list.Single((EntityTypeMapping etm) => etm.EntityType == dependentEntityType).Fragments.SingleOrDefault((MappingFragment mf) => mf.Table == toTable) : fragment);
				if (mappingFragment != null)
				{
					List<EdmProperty> list3 = entityType3.KeyProperties().ToList<EdmProperty>();
					List<EdmProperty> list4 = dependentEntityType.KeyProperties().ToList<EdmProperty>();
					for (int i = 0; i < list3.Count; i++)
					{
						EdmProperty dependentKey = list4[i];
						dependentKey.SetStoreGeneratedPattern(StoreGeneratedPattern.None);
						mappingFragment.ColumnMappings.Single((ColumnMappingBuilder pm) => pm.PropertyPath.First<EdmProperty>() == dependentKey).ColumnProperty.Name = list3[i].Name;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000ADA RID: 2778
		private DatabaseName _tableName;

		// Token: 0x04000ADB RID: 2779
		private List<PropertyPath> _properties;

		// Token: 0x04000ADC RID: 2780
		private readonly List<ValueConditionConfiguration> _valueConditions = new List<ValueConditionConfiguration>();

		// Token: 0x04000ADD RID: 2781
		private readonly List<NotNullConditionConfiguration> _notNullConditions = new List<NotNullConditionConfiguration>();

		// Token: 0x04000ADE RID: 2782
		private readonly Dictionary<PropertyPath, PrimitivePropertyConfiguration> _primitivePropertyConfigurations = new Dictionary<PropertyPath, PrimitivePropertyConfiguration>();

		// Token: 0x04000ADF RID: 2783
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}
