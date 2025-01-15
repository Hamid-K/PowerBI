using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E4 RID: 484
	internal class ModelConfiguration : ConfigurationBase
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x000446D6 File Offset: 0x000428D6
		internal ModelConfiguration()
		{
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00044700 File Offset: 0x00042900
		private ModelConfiguration(ModelConfiguration source)
		{
			source._entityConfigurations.Each(delegate(KeyValuePair<Type, EntityTypeConfiguration> c)
			{
				this._entityConfigurations.Add(c.Key, c.Value.Clone());
			});
			source._complexTypeConfigurations.Each(delegate(KeyValuePair<Type, ComplexTypeConfiguration> c)
			{
				this._complexTypeConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._ignoredTypes.AddRange(source._ignoredTypes);
			this.DefaultSchema = source.DefaultSchema;
			this.ModelNamespace = source.ModelNamespace;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0004478B File Offset: 0x0004298B
		internal virtual ModelConfiguration Clone()
		{
			return new ModelConfiguration(this);
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00044793 File Offset: 0x00042993
		public virtual IEnumerable<Type> ConfiguredTypes
		{
			get
			{
				return this._entityConfigurations.Keys.Union(this._complexTypeConfigurations.Keys).Union(this._ignoredTypes);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x000447BB File Offset: 0x000429BB
		internal virtual IEnumerable<Type> Entities
		{
			get
			{
				return this._entityConfigurations.Keys.Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000447D8 File Offset: 0x000429D8
		internal virtual IEnumerable<Type> ComplexTypes
		{
			get
			{
				return this._complexTypeConfigurations.Keys.Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x000447F5 File Offset: 0x000429F5
		internal virtual IEnumerable<Type> StructuralTypes
		{
			get
			{
				return this._entityConfigurations.Keys.Union(this._complexTypeConfigurations.Keys).Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00044822 File Offset: 0x00042A22
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x0004482A File Offset: 0x00042A2A
		public string DefaultSchema { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x00044833 File Offset: 0x00042A33
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x0004483B File Offset: 0x00042A3B
		public string ModelNamespace { get; set; }

		// Token: 0x0600197B RID: 6523 RVA: 0x00044844 File Offset: 0x00042A44
		internal virtual void Add(EntityTypeConfiguration entityTypeConfiguration)
		{
			EntityTypeConfiguration entityTypeConfiguration2;
			if ((this._entityConfigurations.TryGetValue(entityTypeConfiguration.ClrType, out entityTypeConfiguration2) && !entityTypeConfiguration2.IsReplaceable) || this._complexTypeConfigurations.ContainsKey(entityTypeConfiguration.ClrType))
			{
				throw Error.DuplicateStructuralTypeConfiguration(entityTypeConfiguration.ClrType);
			}
			if (entityTypeConfiguration2 != null && entityTypeConfiguration2.IsReplaceable)
			{
				this._entityConfigurations.Remove(entityTypeConfiguration2.ClrType);
				entityTypeConfiguration.ReplaceFrom(entityTypeConfiguration2);
			}
			else
			{
				entityTypeConfiguration.IsReplaceable = false;
			}
			this._entityConfigurations.Add(entityTypeConfiguration.ClrType, entityTypeConfiguration);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000448CC File Offset: 0x00042ACC
		internal virtual void Add(ComplexTypeConfiguration complexTypeConfiguration)
		{
			if (this._entityConfigurations.ContainsKey(complexTypeConfiguration.ClrType) || this._complexTypeConfigurations.ContainsKey(complexTypeConfiguration.ClrType))
			{
				throw Error.DuplicateStructuralTypeConfiguration(complexTypeConfiguration.ClrType);
			}
			this._complexTypeConfigurations.Add(complexTypeConfiguration.ClrType, complexTypeConfiguration);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0004491D File Offset: 0x00042B1D
		public virtual EntityTypeConfiguration Entity(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			return this.Entity(entityType, false);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00044934 File Offset: 0x00042B34
		internal virtual EntityTypeConfiguration Entity(Type entityType, bool explicitEntity)
		{
			if (this._complexTypeConfigurations.ContainsKey(entityType))
			{
				throw Error.EntityTypeConfigurationMismatch(entityType.Name);
			}
			EntityTypeConfiguration entityTypeConfiguration;
			if (!this._entityConfigurations.TryGetValue(entityType, out entityTypeConfiguration))
			{
				Dictionary<Type, EntityTypeConfiguration> entityConfigurations = this._entityConfigurations;
				EntityTypeConfiguration entityTypeConfiguration2 = new EntityTypeConfiguration(entityType);
				entityTypeConfiguration2.IsExplicitEntity = explicitEntity;
				entityTypeConfiguration = entityTypeConfiguration2;
				entityConfigurations.Add(entityType, entityTypeConfiguration2);
			}
			return entityTypeConfiguration;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00044988 File Offset: 0x00042B88
		public virtual ComplexTypeConfiguration ComplexType(Type complexType)
		{
			Check.NotNull<Type>(complexType, "complexType");
			if (this._entityConfigurations.ContainsKey(complexType))
			{
				throw Error.ComplexTypeConfigurationMismatch(complexType.Name);
			}
			ComplexTypeConfiguration complexTypeConfiguration;
			if (!this._complexTypeConfigurations.TryGetValue(complexType, out complexTypeConfiguration))
			{
				this._complexTypeConfigurations.Add(complexType, complexTypeConfiguration = new ComplexTypeConfiguration(complexType));
			}
			return complexTypeConfiguration;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000449E0 File Offset: 0x00042BE0
		public virtual void Ignore(Type type)
		{
			Check.NotNull<Type>(type, "type");
			this._ignoredTypes.Add(type);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000449FC File Offset: 0x00042BFC
		internal virtual StructuralTypeConfiguration GetStructuralTypeConfiguration(Type type)
		{
			EntityTypeConfiguration entityTypeConfiguration;
			if (this._entityConfigurations.TryGetValue(type, out entityTypeConfiguration))
			{
				return entityTypeConfiguration;
			}
			ComplexTypeConfiguration complexTypeConfiguration;
			if (this._complexTypeConfigurations.TryGetValue(type, out complexTypeConfiguration))
			{
				return complexTypeConfiguration;
			}
			return null;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00044A2E File Offset: 0x00042C2E
		public virtual bool IsComplexType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			return this._complexTypeConfigurations.ContainsKey(type);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00044A48 File Offset: 0x00042C48
		public virtual bool IsIgnoredType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			return this._ignoredTypes.Contains(type);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00044A64 File Offset: 0x00042C64
		public virtual IEnumerable<PropertyInfo> GetConfiguredProperties(Type type)
		{
			Check.NotNull<Type>(type, "type");
			StructuralTypeConfiguration structuralTypeConfiguration = this.GetStructuralTypeConfiguration(type);
			if (structuralTypeConfiguration == null)
			{
				return Enumerable.Empty<PropertyInfo>();
			}
			return structuralTypeConfiguration.ConfiguredProperties;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00044A94 File Offset: 0x00042C94
		public virtual bool IsIgnoredProperty(Type type, PropertyInfo propertyInfo)
		{
			Check.NotNull<Type>(type, "type");
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			Func<PropertyInfo, bool> <>9__0;
			while (type != null)
			{
				StructuralTypeConfiguration structuralTypeConfiguration = this.GetStructuralTypeConfiguration(type);
				if (structuralTypeConfiguration != null)
				{
					IEnumerable<PropertyInfo> ignoredProperties = structuralTypeConfiguration.IgnoredProperties;
					Func<PropertyInfo, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (PropertyInfo p) => p.IsSameAs(propertyInfo));
					}
					if (ignoredProperties.Any(func))
					{
						return true;
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00044B18 File Offset: 0x00042D18
		internal void Configure(EdmModel model)
		{
			this.ConfigureEntities(model);
			this.ConfigureComplexTypes(model);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00044B28 File Offset: 0x00042D28
		private void ConfigureEntities(EdmModel model)
		{
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				this.ConfigureFunctionMappings(model, entityTypeConfiguration, model.GetEntityType(entityTypeConfiguration.ClrType));
			}
			foreach (EntityTypeConfiguration entityTypeConfiguration2 in this.ActiveEntityConfigurations)
			{
				entityTypeConfiguration2.Configure(model.GetEntityType(entityTypeConfiguration2.ClrType), model);
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00044BCC File Offset: 0x00042DCC
		private void ConfigureFunctionMappings(EdmModel model, EntityTypeConfiguration entityTypeConfiguration, EntityType entityType)
		{
			if (entityTypeConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				return;
			}
			while (entityType.BaseType != null)
			{
				Type clrType = ((EntityType)entityType.BaseType).GetClrType();
				EntityTypeConfiguration entityTypeConfiguration2;
				if (!entityType.BaseType.Abstract && (!this._entityConfigurations.TryGetValue(clrType, out entityTypeConfiguration2) || entityTypeConfiguration2.ModificationStoredProceduresConfiguration == null))
				{
					throw Error.BaseTypeNotMappedToFunctions(clrType.Name, entityTypeConfiguration.ClrType.Name);
				}
				entityType = (EntityType)entityType.BaseType;
			}
			model.GetSelfAndAllDerivedTypes(entityType).Each(delegate(EntityType e)
			{
				EntityTypeConfiguration entityTypeConfiguration3 = this.Entity(e.GetClrType());
				if (entityTypeConfiguration3.ModificationStoredProceduresConfiguration == null)
				{
					entityTypeConfiguration3.MapToStoredProcedures();
				}
			});
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00044C5C File Offset: 0x00042E5C
		private void ConfigureComplexTypes(EdmModel model)
		{
			foreach (ComplexTypeConfiguration complexTypeConfiguration in this.ActiveComplexTypeConfigurations)
			{
				ComplexType complexType = model.GetComplexType(complexTypeConfiguration.ClrType);
				complexTypeConfiguration.Configure(complexType);
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00044CB8 File Offset: 0x00042EB8
		internal void Configure(DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in from StructuralTypeConfiguration c in databaseMapping.Model.ComplexTypes.Select((ComplexType ct) => ct.GetConfiguration())
				where c != null
				select c)
			{
				structuralTypeConfiguration.ConfigurePropertyMappings(databaseMapping.GetComplexPropertyMappings(structuralTypeConfiguration.ClrType).ToList<Tuple<ColumnMappingBuilder, EntityType>>(), providerManifest, false);
			}
			this.ConfigureEntityTypes(databaseMapping, databaseMapping.Model.Container.EntitySets, providerManifest);
			ModelConfiguration.RemoveRedundantColumnConditions(databaseMapping);
			ModelConfiguration.RemoveRedundantTables(databaseMapping);
			ModelConfiguration.ConfigureTables(databaseMapping.Database);
			this.ConfigureDefaultSchema(databaseMapping);
			ModelConfiguration.UniquifyFunctionNames(databaseMapping);
			ModelConfiguration.ConfigureFunctionParameters(databaseMapping);
			ModelConfiguration.RemoveDuplicateTphColumns(databaseMapping);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00044DB4 File Offset: 0x00042FB4
		private static void ConfigureFunctionParameters(DbDatabaseMapping databaseMapping)
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in from StructuralTypeConfiguration c in databaseMapping.Model.ComplexTypes.Select((ComplexType ct) => ct.GetConfiguration())
				where c != null
				select c)
			{
				structuralTypeConfiguration.ConfigureFunctionParameters(databaseMapping.GetComplexParameterBindings(structuralTypeConfiguration.ClrType).ToList<ModificationFunctionParameterBinding>());
			}
			foreach (EntityType entityType in databaseMapping.Model.EntityTypes.Where((EntityType e) => e.GetConfiguration() != null))
			{
				((EntityTypeConfiguration)entityType.GetConfiguration()).ConfigureFunctionParameters(databaseMapping, entityType);
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00044ED4 File Offset: 0x000430D4
		private static void UniquifyFunctionNames(DbDatabaseMapping databaseMapping)
		{
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.ModificationFunctionMappings))
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)entityTypeModificationFunctionMapping.EntityType.GetConfiguration();
				if (entityTypeConfiguration.ModificationStoredProceduresConfiguration != null)
				{
					ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration = entityTypeConfiguration.ModificationStoredProceduresConfiguration;
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.InsertFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.UpdateFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.DeleteFunctionMapping);
				}
			}
			foreach (AssociationSetModificationFunctionMapping associationSetModificationFunctionMapping in from asm in databaseMapping.GetAssociationSetMappings()
				select asm.ModificationFunctionMapping into asm
				where asm != null
				select asm)
			{
				NavigationPropertyConfiguration navigationPropertyConfiguration = (NavigationPropertyConfiguration)associationSetModificationFunctionMapping.AssociationSet.ElementType.GetConfiguration();
				if (navigationPropertyConfiguration.ModificationStoredProceduresConfiguration != null)
				{
					ModelConfiguration.UniquifyFunctionName(databaseMapping, navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, associationSetModificationFunctionMapping.InsertFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, associationSetModificationFunctionMapping.DeleteFunctionMapping);
				}
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004506C File Offset: 0x0004326C
		private static void UniquifyFunctionName(DbDatabaseMapping databaseMapping, ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration, ModificationFunctionMapping functionMapping)
		{
			if (modificationStoredProcedureConfiguration == null || string.IsNullOrWhiteSpace(modificationStoredProcedureConfiguration.Name))
			{
				functionMapping.Function.StoreFunctionNameAttribute = (from f in databaseMapping.Database.Functions.Except(new EdmFunction[] { functionMapping.Function })
					select f.FunctionName).Uniquify(functionMapping.Function.FunctionName);
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000450E8 File Offset: 0x000432E8
		private void ConfigureDefaultSchema(DbDatabaseMapping databaseMapping)
		{
			(from es in databaseMapping.Database.GetEntitySets()
				where string.IsNullOrWhiteSpace(es.Schema)
				select es).Each((EntitySet es) => es.Schema = this.DefaultSchema ?? "dbo");
			databaseMapping.Database.Functions.Where((EdmFunction f) => string.IsNullOrWhiteSpace(f.Schema)).Each((EdmFunction f) => f.Schema = this.DefaultSchema ?? "dbo");
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00045178 File Offset: 0x00043378
		private void ConfigureEntityTypes(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest)
		{
			IList<EntityTypeConfiguration> list = this.SortEntityConfigurationsByInheritance(databaseMapping);
			foreach (EntityTypeConfiguration entityTypeConfiguration in list)
			{
				EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityTypeConfiguration.ClrType);
				entityTypeConfiguration.ConfigureTablesAndConditions(entityTypeMapping, databaseMapping, entitySets, providerManifest);
				ModelConfiguration.ConfigureUnconfiguredDerivedTypes(databaseMapping, entitySets, providerManifest, databaseMapping.Model.GetEntityType(entityTypeConfiguration.ClrType), list);
			}
			new EntityMappingService(databaseMapping).Configure();
			foreach (EntityType entityType in databaseMapping.Model.EntityTypes.Where((EntityType e) => e.GetConfiguration() != null))
			{
				((EntityTypeConfiguration)entityType.GetConfiguration()).Configure(entityType, databaseMapping, providerManifest);
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00045278 File Offset: 0x00043478
		private static void ConfigureUnconfiguredDerivedTypes(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, IList<EntityTypeConfiguration> sortedEntityConfigurations)
		{
			List<EntityType> list = databaseMapping.Model.GetDerivedTypes(entityType).ToList<EntityType>();
			while (list.Count > 0)
			{
				EntityType currentType = list[0];
				list.RemoveAt(0);
				if (!currentType.Abstract && sortedEntityConfigurations.All((EntityTypeConfiguration etc) => etc.ClrType != currentType.GetClrType()))
				{
					EntityTypeConfiguration.ConfigureUnconfiguredType(databaseMapping, entitySets, providerManifest, currentType, new Dictionary<string, object>());
					list.AddRange(databaseMapping.Model.GetDerivedTypes(currentType));
				}
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00045308 File Offset: 0x00043508
		private static void ConfigureTables(EdmModel database)
		{
			foreach (EntityType entityType in database.EntityTypes.ToList<EntityType>())
			{
				ModelConfiguration.ConfigureTable(database, entityType);
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00045360 File Offset: 0x00043560
		private static void ConfigureTable(EdmModel database, EntityType table)
		{
			DatabaseName tableName = table.GetTableName();
			if (tableName == null)
			{
				return;
			}
			EntitySet entitySet = database.GetEntitySet(table);
			if (!string.IsNullOrWhiteSpace(tableName.Schema))
			{
				entitySet.Schema = tableName.Schema;
			}
			entitySet.Table = tableName.Name;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000453A8 File Offset: 0x000435A8
		private IList<EntityTypeConfiguration> SortEntityConfigurationsByInheritance(DbDatabaseMapping databaseMapping)
		{
			List<EntityTypeConfiguration> list = new List<EntityTypeConfiguration>();
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				EntityType entityType = databaseMapping.Model.GetEntityType(entityTypeConfiguration.ClrType);
				if (entityType != null)
				{
					if (entityType.BaseType == null)
					{
						if (!list.Contains(entityTypeConfiguration))
						{
							list.Add(entityTypeConfiguration);
						}
					}
					else
					{
						Stack<EntityType> stack = new Stack<EntityType>();
						while (entityType != null)
						{
							stack.Push(entityType);
							entityType = (EntityType)entityType.BaseType;
						}
						Func<EntityTypeConfiguration, bool> <>9__0;
						while (stack.Count > 0)
						{
							entityType = stack.Pop();
							IEnumerable<EntityTypeConfiguration> activeEntityConfigurations = this.ActiveEntityConfigurations;
							Func<EntityTypeConfiguration, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (EntityTypeConfiguration ec) => ec.ClrType == entityType.GetClrType());
							}
							EntityTypeConfiguration entityTypeConfiguration2 = activeEntityConfigurations.SingleOrDefault(func);
							if (entityTypeConfiguration2 != null && !list.Contains(entityTypeConfiguration2))
							{
								list.Add(entityTypeConfiguration2);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000454DC File Offset: 0x000436DC
		internal void NormalizeConfigurations()
		{
			this.DiscoverIndirectlyConfiguredComplexTypes();
			this.ReassignSubtypeMappings();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000454EA File Offset: 0x000436EA
		private void DiscoverIndirectlyConfiguredComplexTypes()
		{
			this.ActiveEntityConfigurations.SelectMany((EntityTypeConfiguration ec) => ec.ConfiguredComplexTypes).Each((Type t) => this.ComplexType(t));
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00045528 File Offset: 0x00043728
		private void ReassignSubtypeMappings()
		{
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				foreach (KeyValuePair<Type, EntityMappingConfiguration> keyValuePair in entityTypeConfiguration.SubTypeMappingConfigurations)
				{
					Type subTypeClrType = keyValuePair.Key;
					EntityTypeConfiguration entityTypeConfiguration2 = this.ActiveEntityConfigurations.SingleOrDefault((EntityTypeConfiguration ec) => ec.ClrType == subTypeClrType);
					if (entityTypeConfiguration2 == null)
					{
						entityTypeConfiguration2 = new EntityTypeConfiguration(subTypeClrType);
						this._entityConfigurations.Add(subTypeClrType, entityTypeConfiguration2);
					}
					entityTypeConfiguration2.AddMappingConfiguration(keyValuePair.Value, false);
				}
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0004560C File Offset: 0x0004380C
		private static void RemoveDuplicateTphColumns(DbDatabaseMapping databaseMapping)
		{
			foreach (EntityType entityType in databaseMapping.Database.EntityTypes)
			{
				EntityType currentTable = entityType;
				new TphColumnFixer((from f in databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping e) => e.EntityTypeMappings).SelectMany((EntityTypeMapping e) => e.MappingFragments)
					where f.Table == currentTable
					select f).SelectMany((MappingFragment f) => f.ColumnMappings), currentTable, databaseMapping.Database).RemoveDuplicateTphColumns();
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00045704 File Offset: 0x00043904
		private static void RemoveRedundantColumnConditions(DbDatabaseMapping databaseMapping)
		{
			(from esm in databaseMapping.GetEntitySetMappings()
				select new
				{
					Set = esm,
					Fragments = from etm in esm.EntityTypeMappings
						from etmf in etm.MappingFragments
						group etmf by etmf.Table into g
						where g.Count((MappingFragment x) => x.GetDefaultDiscriminator() != null) == 1
						select g.Single((MappingFragment x) => x.GetDefaultDiscriminator() != null)
				}).Each(delegate(x)
			{
				x.Fragments.Each(delegate(MappingFragment f)
				{
					f.RemoveDefaultDiscriminator(x.Set);
				});
			});
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00045760 File Offset: 0x00043960
		private static void RemoveRedundantTables(DbDatabaseMapping databaseMapping)
		{
			Action<AssociationType> <>9__7;
			databaseMapping.Database.EntityTypes.Where((EntityType t) => databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
				.All((MappingFragment etmf) => etmf.Table != t) && databaseMapping.GetAssociationSetMappings().All((AssociationSetMapping asm) => asm.Table != t)).ToList<EntityType>().Each(delegate(EntityType t)
			{
				DatabaseName tableName = t.GetTableName();
				if (tableName != null)
				{
					throw Error.OrphanedConfiguredTableDetected(tableName);
				}
				databaseMapping.Database.RemoveEntityType(t);
				IEnumerable<AssociationType> enumerable = databaseMapping.Database.AssociationTypes.Where((AssociationType at) => at.SourceEnd.GetEntityType() == t || at.TargetEnd.GetEntityType() == t).ToList<AssociationType>();
				Action<AssociationType> action;
				if ((action = <>9__7) == null)
				{
					action = (<>9__7 = delegate(AssociationType at)
					{
						databaseMapping.Database.RemoveAssociationType(at);
					});
				}
				enumerable.Each(action);
			});
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x000457B4 File Offset: 0x000439B4
		private IEnumerable<EntityTypeConfiguration> ActiveEntityConfigurations
		{
			get
			{
				return this._entityConfigurations.Where(delegate(KeyValuePair<Type, EntityTypeConfiguration> keyValuePair)
				{
					HashSet<Type> ignoredTypes = this._ignoredTypes;
					KeyValuePair<Type, EntityTypeConfiguration> keyValuePair2 = keyValuePair;
					return !ignoredTypes.Contains(keyValuePair2.Key);
				}).Select(delegate(KeyValuePair<Type, EntityTypeConfiguration> keyValuePair)
				{
					KeyValuePair<Type, EntityTypeConfiguration> keyValuePair3 = keyValuePair;
					return keyValuePair3.Value;
				}).ToList<EntityTypeConfiguration>();
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00045804 File Offset: 0x00043A04
		private IEnumerable<ComplexTypeConfiguration> ActiveComplexTypeConfigurations
		{
			get
			{
				return this._complexTypeConfigurations.Where(delegate(KeyValuePair<Type, ComplexTypeConfiguration> keyValuePair)
				{
					HashSet<Type> ignoredTypes = this._ignoredTypes;
					KeyValuePair<Type, ComplexTypeConfiguration> keyValuePair2 = keyValuePair;
					return !ignoredTypes.Contains(keyValuePair2.Key);
				}).Select(delegate(KeyValuePair<Type, ComplexTypeConfiguration> keyValuePair)
				{
					KeyValuePair<Type, ComplexTypeConfiguration> keyValuePair3 = keyValuePair;
					return keyValuePair3.Value;
				}).ToList<ComplexTypeConfiguration>();
			}
		}

		// Token: 0x04000A7C RID: 2684
		private readonly Dictionary<Type, EntityTypeConfiguration> _entityConfigurations = new Dictionary<Type, EntityTypeConfiguration>();

		// Token: 0x04000A7D RID: 2685
		private readonly Dictionary<Type, ComplexTypeConfiguration> _complexTypeConfigurations = new Dictionary<Type, ComplexTypeConfiguration>();

		// Token: 0x04000A7E RID: 2686
		private readonly HashSet<Type> _ignoredTypes = new HashSet<Type>();
	}
}
