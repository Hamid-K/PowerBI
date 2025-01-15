using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Index;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020001FE RID: 510
	internal class EntityTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004880C File Offset: 0x00046A0C
		internal EntityTypeConfiguration(Type structuralType)
			: base(structuralType)
		{
			this.IsReplaceable = false;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00048898 File Offset: 0x00046A98
		private EntityTypeConfiguration(EntityTypeConfiguration source)
			: base(source)
		{
			this._keyProperties.AddRange(source._keyProperties);
			this._keyConfiguration = source._keyConfiguration;
			source._indexConfigurations.Each(delegate(KeyValuePair<PropertyPath, IndexConfiguration> c)
			{
				this._indexConfigurations.Add(c.Key, c.Value.Clone());
			});
			source._navigationPropertyConfigurations.Each(delegate(KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> c)
			{
				this._navigationPropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			source._entitySubTypesMappingConfigurations.Each(delegate(KeyValuePair<Type, EntityMappingConfiguration> c)
			{
				this._entitySubTypesMappingConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._entityMappingConfigurations.AddRange(from e in source._entityMappingConfigurations.Except(source._nonCloneableMappings)
				select e.Clone());
			this._entitySetName = source._entitySetName;
			if (source._modificationStoredProceduresConfiguration != null)
			{
				this._modificationStoredProceduresConfiguration = source._modificationStoredProceduresConfiguration.Clone();
			}
			this.IsReplaceable = source.IsReplaceable;
			this.IsTableNameConfigured = source.IsTableNameConfigured;
			this.IsExplicitEntity = source.IsExplicitEntity;
			foreach (KeyValuePair<string, object> keyValuePair in source._annotations)
			{
				this._annotations.Add(keyValuePair);
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00048A50 File Offset: 0x00046C50
		internal virtual EntityTypeConfiguration Clone()
		{
			return new EntityTypeConfiguration(this);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00048A58 File Offset: 0x00046C58
		internal IEnumerable<Type> ConfiguredComplexTypes
		{
			get
			{
				return from pi in (from c in base.PrimitivePropertyConfigurations
						where c.Key.Count > 1
						select c.Key.Reverse<PropertyInfo>().Skip(1)).SelectMany((IEnumerable<PropertyInfo> p) => p)
					select pi.PropertyType;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00048AFC File Offset: 0x00046CFC
		internal bool IsStructuralConfigurationOnly
		{
			get
			{
				return !this._keyProperties.Any<PropertyInfo>() && !this._navigationPropertyConfigurations.Any<KeyValuePair<PropertyInfo, NavigationPropertyConfiguration>>() && !this._entityMappingConfigurations.Any<EntityMappingConfiguration>() && !this._entitySubTypesMappingConfigurations.Any<KeyValuePair<Type, EntityMappingConfiguration>>() && this._entitySetName == null;
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00048B48 File Offset: 0x00046D48
		internal override void RemoveProperty(PropertyPath propertyPath)
		{
			base.RemoveProperty(propertyPath);
			this._navigationPropertyConfigurations.Remove(propertyPath.Single<PropertyInfo>());
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00048B63 File Offset: 0x00046D63
		internal bool IsKeyConfigured
		{
			get
			{
				return this._keyConfiguration != null;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00048B6E File Offset: 0x00046D6E
		internal IEnumerable<PropertyInfo> KeyProperties
		{
			get
			{
				return this._keyProperties;
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00048B78 File Offset: 0x00046D78
		internal virtual void Key(IEnumerable<PropertyInfo> keyProperties)
		{
			this.ClearKey();
			foreach (PropertyInfo propertyInfo in keyProperties)
			{
				this.Key(propertyInfo, new OverridableConfigurationParts?(OverridableConfigurationParts.None));
			}
			if (this._keyConfiguration == null)
			{
				this._keyConfiguration = new IndexConfiguration();
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00048BE0 File Offset: 0x00046DE0
		public void Key(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.Key(propertyInfo, null);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00048C0C File Offset: 0x00046E0C
		internal virtual void Key(PropertyInfo propertyInfo, OverridableConfigurationParts? overridableConfigurationParts)
		{
			if (!propertyInfo.IsValidEdmScalarProperty())
			{
				throw Error.ModelBuilder_KeyPropertiesMustBePrimitive(propertyInfo.Name, base.ClrType);
			}
			if (this._keyConfiguration == null && !this._keyProperties.ContainsSame(propertyInfo))
			{
				this._keyProperties.Add(propertyInfo);
				base.Property(new PropertyPath(propertyInfo), overridableConfigurationParts);
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00048C63 File Offset: 0x00046E63
		internal virtual IndexConfiguration ConfigureKey()
		{
			if (this._keyConfiguration == null)
			{
				this._keyConfiguration = new IndexConfiguration();
			}
			return this._keyConfiguration;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00048C7E File Offset: 0x00046E7E
		internal IEnumerable<PropertyPath> PropertyIndexes
		{
			get
			{
				return this._indexConfigurations.Keys;
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00048C8C File Offset: 0x00046E8C
		internal virtual IndexConfiguration Index(PropertyPath indexProperties)
		{
			IndexConfiguration indexConfiguration;
			if (!this._indexConfigurations.TryGetValue(indexProperties, out indexConfiguration))
			{
				this._indexConfigurations.Add(indexProperties, indexConfiguration = new IndexConfiguration());
			}
			return indexConfiguration;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00048CBD File Offset: 0x00046EBD
		internal void ClearKey()
		{
			this._keyProperties.Clear();
			this._keyConfiguration = null;
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00048CD1 File Offset: 0x00046ED1
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00048CD9 File Offset: 0x00046ED9
		public bool IsTableNameConfigured { get; private set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00048CE2 File Offset: 0x00046EE2
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x00048CEA File Offset: 0x00046EEA
		internal bool IsReplaceable { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00048CF3 File Offset: 0x00046EF3
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x00048CFB File Offset: 0x00046EFB
		internal bool IsExplicitEntity { get; set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00048D04 File Offset: 0x00046F04
		internal ModificationStoredProceduresConfiguration ModificationStoredProceduresConfiguration
		{
			get
			{
				return this._modificationStoredProceduresConfiguration;
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00048D0C File Offset: 0x00046F0C
		internal virtual void MapToStoredProcedures()
		{
			if (this._modificationStoredProceduresConfiguration == null)
			{
				this._modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration();
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00048D21 File Offset: 0x00046F21
		internal virtual void MapToStoredProcedures(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration, bool allowOverride)
		{
			if (this._modificationStoredProceduresConfiguration == null)
			{
				this._modificationStoredProceduresConfiguration = modificationStoredProceduresConfiguration;
				return;
			}
			this._modificationStoredProceduresConfiguration.Merge(modificationStoredProceduresConfiguration, allowOverride);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00048D40 File Offset: 0x00046F40
		internal void ReplaceFrom(EntityTypeConfiguration existing)
		{
			if (this.EntitySetName == null)
			{
				this.EntitySetName = existing.EntitySetName;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00048D56 File Offset: 0x00046F56
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00048D5E File Offset: 0x00046F5E
		public virtual string EntitySetName
		{
			get
			{
				return this._entitySetName;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._entitySetName = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00048D73 File Offset: 0x00046F73
		internal override IEnumerable<PropertyInfo> ConfiguredProperties
		{
			get
			{
				return base.ConfiguredProperties.Union(this._navigationPropertyConfigurations.Keys);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00048D8B File Offset: 0x00046F8B
		public string TableName
		{
			get
			{
				if (!this.IsTableNameConfigured)
				{
					return null;
				}
				return this.GetTableName().Name;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00048DA2 File Offset: 0x00046FA2
		public string SchemaName
		{
			get
			{
				if (!this.IsTableNameConfigured)
				{
					return null;
				}
				return this.GetTableName().Schema;
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00048DB9 File Offset: 0x00046FB9
		internal DatabaseName GetTableName()
		{
			if (!this.IsTableNameConfigured)
			{
				return null;
			}
			return this._entityMappingConfigurations.First<EntityMappingConfiguration>().TableName;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00048DD5 File Offset: 0x00046FD5
		public void ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ToTable(tableName, null);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00048DEC File Offset: 0x00046FEC
		public void ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.IsTableNameConfigured = true;
			if (!this._entityMappingConfigurations.Any<EntityMappingConfiguration>())
			{
				this._entityMappingConfigurations.Add(new EntityMappingConfiguration());
			}
			this._entityMappingConfigurations.First<EntityMappingConfiguration>().TableName = (string.IsNullOrWhiteSpace(schemaName) ? new DatabaseName(tableName) : new DatabaseName(tableName, schemaName));
			this.UpdateTableNameForSubTypes();
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00048E56 File Offset: 0x00047056
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00048E5E File Offset: 0x0004705E
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00048E84 File Offset: 0x00047084
		private void UpdateTableNameForSubTypes()
		{
			(from stmc in this._entitySubTypesMappingConfigurations
				where stmc.Value.TableName == null
				select stmc into tphs
				select tphs.Value).Each((EntityMappingConfiguration tphmc) => tphmc.TableName = this.GetTableName());
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00048EF0 File Offset: 0x000470F0
		internal void AddMappingConfiguration(EntityMappingConfiguration mappingConfiguration, bool cloneable = true)
		{
			if (this._entityMappingConfigurations.Contains(mappingConfiguration))
			{
				return;
			}
			DatabaseName tableName = mappingConfiguration.TableName;
			if (tableName != null && this._entityMappingConfigurations.SingleOrDefault((EntityMappingConfiguration mf) => tableName.Equals(mf.TableName)) != null)
			{
				throw Error.InvalidTableMapping(base.ClrType.Name, tableName);
			}
			this._entityMappingConfigurations.Add(mappingConfiguration);
			if (this._entityMappingConfigurations.Count > 1)
			{
				if (this._entityMappingConfigurations.Any((EntityMappingConfiguration mc) => mc.TableName == null))
				{
					throw Error.InvalidTableMapping_NoTableName(base.ClrType.Name);
				}
			}
			this.IsTableNameConfigured |= tableName != null;
			if (!cloneable)
			{
				this._nonCloneableMappings.Add(mappingConfiguration);
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00048FD4 File Offset: 0x000471D4
		internal void AddSubTypeMappingConfiguration(Type subType, EntityMappingConfiguration mappingConfiguration)
		{
			EntityMappingConfiguration entityMappingConfiguration;
			if (this._entitySubTypesMappingConfigurations.TryGetValue(subType, out entityMappingConfiguration))
			{
				throw Error.InvalidChainedMappingSyntax(subType.Name);
			}
			this._entitySubTypesMappingConfigurations.Add(subType, mappingConfiguration);
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0004900A File Offset: 0x0004720A
		internal Dictionary<Type, EntityMappingConfiguration> SubTypeMappingConfigurations
		{
			get
			{
				return this._entitySubTypesMappingConfigurations;
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00049014 File Offset: 0x00047214
		internal NavigationPropertyConfiguration Navigation(PropertyInfo propertyInfo)
		{
			NavigationPropertyConfiguration navigationPropertyConfiguration;
			if (!this._navigationPropertyConfigurations.TryGetValue(propertyInfo, out navigationPropertyConfiguration))
			{
				this._navigationPropertyConfigurations.Add(propertyInfo, navigationPropertyConfiguration = new NavigationPropertyConfiguration(propertyInfo));
			}
			return navigationPropertyConfiguration;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00049046 File Offset: 0x00047246
		internal virtual void Configure(EntityType entityType, EdmModel model)
		{
			this.ConfigureKey(entityType);
			base.Configure(entityType.Name, entityType.Properties, entityType.GetMetadataProperties());
			this.ConfigureAssociations(entityType, model);
			this.ConfigureEntitySetName(entityType, model);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00049078 File Offset: 0x00047278
		private void ConfigureEntitySetName(EntityType entityType, EdmModel model)
		{
			if (this.EntitySetName == null || entityType.BaseType != null)
			{
				return;
			}
			EntitySet entitySet = model.GetEntitySet(entityType);
			entitySet.Name = model.GetEntitySets().Except(new EntitySet[] { entitySet }).UniquifyName(this.EntitySetName);
			entitySet.SetConfiguration(this);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000490CC File Offset: 0x000472CC
		private void ConfigureKey(EntityType entityType)
		{
			if (!this._keyProperties.Any<PropertyInfo>())
			{
				return;
			}
			if (entityType.BaseType != null)
			{
				throw Error.KeyRegisteredOnDerivedType(base.ClrType, entityType.GetRootType().GetClrType());
			}
			IEnumerable<PropertyInfo> enumerable = this._keyProperties.AsEnumerable<PropertyInfo>();
			if (this._keyConfiguration == null)
			{
				var enumerable2 = this._keyProperties.Select((PropertyInfo p) => new
				{
					PropertyInfo = p,
					ColumnOrder = base.Property(new PropertyPath(p), null).ColumnOrder
				});
				if (this._keyProperties.Count > 1)
				{
					if (enumerable2.Any(p => p.ColumnOrder == null))
					{
						throw Error.ModelGeneration_UnableToDetermineKeyOrder(base.ClrType);
					}
				}
				enumerable = from p in enumerable2
					orderby p.ColumnOrder
					select p.PropertyInfo;
			}
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				EdmProperty declaredPrimitiveProperty = entityType.GetDeclaredPrimitiveProperty(propertyInfo);
				if (declaredPrimitiveProperty == null)
				{
					throw Error.KeyPropertyNotFound(propertyInfo.Name, entityType.Name);
				}
				declaredPrimitiveProperty.Nullable = false;
				entityType.AddKeyMember(declaredPrimitiveProperty);
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00049224 File Offset: 0x00047424
		private void ConfigureIndexes(DbDatabaseMapping mapping, EntityType entityType)
		{
			IList<EntityTypeMapping> entityTypeMappings = mapping.GetEntityTypeMappings(entityType);
			if (this._keyConfiguration != null)
			{
				entityTypeMappings.SelectMany((EntityTypeMapping etm) => etm.Fragments).Each(delegate(MappingFragment f)
				{
					this._keyConfiguration.Configure(f.Table);
				});
			}
			foreach (KeyValuePair<PropertyPath, IndexConfiguration> keyValuePair in this._indexConfigurations)
			{
				using (IEnumerator<EntityTypeMapping> enumerator2 = entityTypeMappings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EntityTypeMapping entityTypeMapping = enumerator2.Current;
						Dictionary<PropertyInfo, ColumnMappingBuilder> propertyMappings = keyValuePair.Key.ToDictionary((PropertyInfo icp) => icp, (PropertyInfo icp) => entityTypeMapping.GetPropertyMapping(new EdmProperty[] { entityType.GetDeclaredPrimitiveProperty(icp) }));
						if (keyValuePair.Key.Count > 1 && string.IsNullOrEmpty(keyValuePair.Value.Name))
						{
							keyValuePair.Value.Name = IndexOperation.BuildDefaultName(keyValuePair.Key.Select((PropertyInfo icp) => propertyMappings[icp].ColumnProperty.Name));
						}
						int num = 0;
						foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>)keyValuePair.Key))
						{
							ColumnMappingBuilder columnMappingBuilder = propertyMappings[propertyInfo];
							keyValuePair.Value.Configure(columnMappingBuilder.ColumnProperty, (keyValuePair.Key.Count != 1) ? num : (-1));
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00049458 File Offset: 0x00047658
		private void ConfigureAssociations(EntityType entityType, EdmModel model)
		{
			foreach (KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> keyValuePair in this._navigationPropertyConfigurations)
			{
				PropertyInfo propertyInfo = keyValuePair.Key;
				NavigationPropertyConfiguration value = keyValuePair.Value;
				NavigationProperty navigationProperty = entityType.GetNavigationProperty(propertyInfo);
				if (navigationProperty == null)
				{
					EdmProperty edmProperty = entityType.Properties.SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo() == propertyInfo);
					if (edmProperty != null && edmProperty.ComplexType != null)
					{
						throw new InvalidOperationException(Strings.InvalidNavigationPropertyComplexType(propertyInfo.Name, entityType.Name, edmProperty.ComplexType.Name));
					}
					throw Error.NavigationPropertyNotFound(propertyInfo.Name, entityType.Name);
				}
				else if (entityType.DeclaredNavigationProperties.Any((NavigationProperty np) => np.GetClrPropertyInfo().IsSameAs(propertyInfo)))
				{
					value.Configure(navigationProperty, model, this);
				}
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00049560 File Offset: 0x00047760
		internal void ConfigureTablesAndConditions(EntityTypeMapping entityTypeMapping, DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest)
		{
			EntityType entityType = ((entityTypeMapping != null) ? entityTypeMapping.EntityType : databaseMapping.Model.GetEntityType(base.ClrType));
			if (this._entityMappingConfigurations.Any<EntityMappingConfiguration>())
			{
				for (int i = 0; i < this._entityMappingConfigurations.Count; i++)
				{
					this._entityMappingConfigurations[i].Configure(databaseMapping, entitySets, providerManifest, entityType, ref entityTypeMapping, this.IsMappingAnyInheritedProperty(entityType), i, this._entityMappingConfigurations.Count, this._annotations);
				}
				return;
			}
			EntityTypeConfiguration.ConfigureUnconfiguredType(databaseMapping, entitySets, providerManifest, entityType, this._annotations);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000495F0 File Offset: 0x000477F0
		internal bool IsMappingAnyInheritedProperty(EntityType entityType)
		{
			return this._entityMappingConfigurations.Any((EntityMappingConfiguration emc) => emc.MapsAnyInheritedProperties(entityType));
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00049621 File Offset: 0x00047821
		internal bool IsNavigationPropertyConfigured(PropertyInfo propertyInfo)
		{
			return this._navigationPropertyConfigurations.ContainsKey(propertyInfo);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00049630 File Offset: 0x00047830
		internal static void ConfigureUnconfiguredType(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, IDictionary<string, object> commonAnnotations)
		{
			EntityMappingConfiguration entityMappingConfiguration = new EntityMappingConfiguration();
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType.GetClrType());
			entityMappingConfiguration.Configure(databaseMapping, entitySets, providerManifest, entityType, ref entityTypeMapping, false, 0, 1, commonAnnotations);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00049660 File Offset: 0x00047860
		internal void Configure(EntityType entityType, DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType.GetClrType());
			if (entityTypeMapping != null)
			{
				EntityTypeConfiguration.VerifyAllCSpacePropertiesAreMapped(databaseMapping.GetEntityTypeMappings(entityType).ToList<EntityTypeMapping>(), entityTypeMapping.EntityType.DeclaredProperties, new List<EdmProperty>());
			}
			this.ConfigurePropertyMappings(databaseMapping, entityType, providerManifest, false);
			this.ConfigureIndexes(databaseMapping, entityType);
			this.ConfigureAssociationMappings(databaseMapping, entityType, providerManifest);
			EntityTypeConfiguration.ConfigureDependentKeys(databaseMapping, providerManifest);
			this.ConfigureModificationStoredProcedures(databaseMapping, entityType, providerManifest);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000496CC File Offset: 0x000478CC
		internal void ConfigureFunctionParameters(DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			List<ModificationFunctionParameterBinding> list = (from esm in databaseMapping.GetEntitySetMappings()
				from mfm in esm.ModificationFunctionMappings
				where mfm.EntityType == entityType
				from pb in mfm.PrimaryParameterBindings
				select pb).ToList<ModificationFunctionParameterBinding>();
			base.ConfigureFunctionParameters(list);
			IEnumerable<EntityType> entityTypes = databaseMapping.Model.EntityTypes;
			Func<EntityType, bool> <>9__5;
			Func<EntityType, bool> func;
			if ((func = <>9__5) == null)
			{
				func = (<>9__5 = (EntityType et) => et.BaseType == entityType);
			}
			foreach (EntityType entityType2 in entityTypes.Where(func))
			{
				this.ConfigureFunctionParameters(databaseMapping, entityType2);
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000497FC File Offset: 0x000479FC
		private void ConfigureModificationStoredProcedures(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest)
		{
			if (this._modificationStoredProceduresConfiguration != null)
			{
				new ModificationFunctionMappingGenerator(providerManifest).Generate(entityType, databaseMapping);
				EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping = databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.ModificationFunctionMappings).SingleOrDefault((EntityTypeModificationFunctionMapping mfm) => mfm.EntityType == entityType);
				if (entityTypeModificationFunctionMapping != null)
				{
					this._modificationStoredProceduresConfiguration.Configure(entityTypeModificationFunctionMapping, providerManifest);
				}
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0004987C File Offset: 0x00047A7C
		private void ConfigurePropertyMappings(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings(entityType);
			List<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings = (from etm in entityTypeMappings
				from etmf in etm.MappingFragments
				from pm in etmf.ColumnMappings
				select Tuple.Create<ColumnMappingBuilder, EntityType>(pm, etmf.Table)).ToList<Tuple<ColumnMappingBuilder, EntityType>>();
			base.ConfigurePropertyMappings(propertyMappings, providerManifest, allowOverride);
			this._entityMappingConfigurations.Each(delegate(EntityMappingConfiguration c)
			{
				c.ConfigurePropertyMappings(propertyMappings, providerManifest, allowOverride);
			});
			List<Tuple<ColumnMappingBuilder, EntityType>> inheritedPropertyMappings = (from esm in databaseMapping.GetEntitySetMappings()
				from etm in esm.EntityTypeMappings
				where etm.IsHierarchyMapping && etm.EntityType.IsAncestorOf(entityType)
				from etmf in etm.MappingFragments
				from pm1 in etmf.ColumnMappings
				where !propertyMappings.Any((Tuple<ColumnMappingBuilder, EntityType> pm2) => pm2.Item1.PropertyPath.SequenceEqual(pm1.PropertyPath))
				select Tuple.Create<ColumnMappingBuilder, EntityType>(pm1, etmf.Table)).ToList<Tuple<ColumnMappingBuilder, EntityType>>();
			base.ConfigurePropertyMappings(inheritedPropertyMappings, providerManifest, false);
			this._entityMappingConfigurations.Each(delegate(EntityMappingConfiguration c)
			{
				c.ConfigurePropertyMappings(inheritedPropertyMappings, providerManifest, false);
			});
			IEnumerable<EntityType> entityTypes = databaseMapping.Model.EntityTypes;
			Func<EntityType, bool> <>9__16;
			Func<EntityType, bool> func;
			if ((func = <>9__16) == null)
			{
				func = (<>9__16 = (EntityType et) => et.BaseType == entityType);
			}
			foreach (EntityType entityType2 in entityTypes.Where(func))
			{
				this.ConfigurePropertyMappings(databaseMapping, entityType2, providerManifest, true);
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00049B30 File Offset: 0x00047D30
		private void ConfigureAssociationMappings(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest)
		{
			foreach (KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> keyValuePair in this._navigationPropertyConfigurations)
			{
				PropertyInfo key = keyValuePair.Key;
				NavigationPropertyConfiguration value = keyValuePair.Value;
				NavigationProperty navigationProperty = entityType.GetNavigationProperty(key);
				if (navigationProperty == null)
				{
					throw Error.NavigationPropertyNotFound(key.Name, entityType.Name);
				}
				AssociationSetMapping associationSetMapping = databaseMapping.GetAssociationSetMappings().SingleOrDefault((AssociationSetMapping asm) => asm.AssociationSet.ElementType == navigationProperty.Association);
				if (associationSetMapping != null)
				{
					value.Configure(associationSetMapping, databaseMapping, providerManifest);
				}
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00049BE4 File Offset: 0x00047DE4
		private static void ConfigureDependentKeys(DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			IList<EntityType> list = (databaseMapping.Database.EntityTypes as IList<EntityType>) ?? databaseMapping.Database.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				IList<ForeignKeyBuilder> list2 = (entityType.ForeignKeyBuilders as IList<ForeignKeyBuilder>) ?? entityType.ForeignKeyBuilders.ToList<ForeignKeyBuilder>();
				for (int j = 0; j < list2.Count; j++)
				{
					ForeignKeyBuilder foreignKeyBuilder = list2[j];
					IEnumerable<EdmProperty> dependentColumns = foreignKeyBuilder.DependentColumns;
					IList<EdmProperty> list3 = (dependentColumns as IList<EdmProperty>) ?? dependentColumns.ToList<EdmProperty>();
					for (int k = 0; k < list3.Count; k++)
					{
						EdmProperty edmProperty = list3[k];
						PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty.GetConfiguration() as PrimitivePropertyConfiguration;
						if (primitivePropertyConfiguration == null || primitivePropertyConfiguration.ColumnType == null)
						{
							EdmProperty edmProperty2 = foreignKeyBuilder.PrincipalTable.KeyProperties.ElementAt(k);
							edmProperty.PrimitiveType = providerManifest.GetStoreTypeFromName(edmProperty2.TypeName);
							edmProperty.CopyFrom(edmProperty2);
						}
					}
				}
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00049D00 File Offset: 0x00047F00
		private static void VerifyAllCSpacePropertiesAreMapped(ICollection<EntityTypeMapping> entityTypeMappings, IEnumerable<EdmProperty> properties, IList<EdmProperty> propertyPath)
		{
			EntityType entityType = entityTypeMappings.First<EntityTypeMapping>().EntityType;
			Func<ColumnMappingBuilder, bool> <>9__2;
			foreach (EdmProperty edmProperty in properties)
			{
				propertyPath.Add(edmProperty);
				if (edmProperty.IsComplexType)
				{
					EntityTypeConfiguration.VerifyAllCSpacePropertiesAreMapped(entityTypeMappings, edmProperty.ComplexType.Properties, propertyPath);
				}
				else
				{
					IEnumerable<ColumnMappingBuilder> enumerable = entityTypeMappings.SelectMany((EntityTypeMapping etm) => etm.MappingFragments).SelectMany((MappingFragment mf) => mf.ColumnMappings);
					Func<ColumnMappingBuilder, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
					}
					if (!enumerable.Any(func) && !entityType.Abstract)
					{
						throw Error.InvalidEntitySplittingProperties(entityType.Name);
					}
				}
				propertyPath.Remove(edmProperty);
			}
		}

		// Token: 0x04000AA3 RID: 2723
		private readonly List<PropertyInfo> _keyProperties = new List<PropertyInfo>();

		// Token: 0x04000AA4 RID: 2724
		private IndexConfiguration _keyConfiguration;

		// Token: 0x04000AA5 RID: 2725
		private readonly Dictionary<PropertyPath, IndexConfiguration> _indexConfigurations = new Dictionary<PropertyPath, IndexConfiguration>();

		// Token: 0x04000AA6 RID: 2726
		private readonly Dictionary<PropertyInfo, NavigationPropertyConfiguration> _navigationPropertyConfigurations = new Dictionary<PropertyInfo, NavigationPropertyConfiguration>(new DynamicEqualityComparer<PropertyInfo>((PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2)));

		// Token: 0x04000AA7 RID: 2727
		private readonly List<EntityMappingConfiguration> _entityMappingConfigurations = new List<EntityMappingConfiguration>();

		// Token: 0x04000AA8 RID: 2728
		private readonly Dictionary<Type, EntityMappingConfiguration> _entitySubTypesMappingConfigurations = new Dictionary<Type, EntityMappingConfiguration>();

		// Token: 0x04000AA9 RID: 2729
		private readonly List<EntityMappingConfiguration> _nonCloneableMappings = new List<EntityMappingConfiguration>();

		// Token: 0x04000AAA RID: 2730
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();

		// Token: 0x04000AAB RID: 2731
		private string _entitySetName;

		// Token: 0x04000AAC RID: 2732
		private ModificationStoredProceduresConfiguration _modificationStoredProceduresConfiguration;
	}
}
