using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E3 RID: 483
	[DebuggerDisplay("{Discriminator}")]
	public class ValueConditionConfiguration
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x000440DA File Offset: 0x000422DA
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x000440E2 File Offset: 0x000422E2
		internal string Discriminator { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x000440EB File Offset: 0x000422EB
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x000440F3 File Offset: 0x000422F3
		internal object Value { get; set; }

		// Token: 0x0600195F RID: 6495 RVA: 0x000440FC File Offset: 0x000422FC
		internal ValueConditionConfiguration(EntityMappingConfiguration entityMapConfiguration, string discriminator)
		{
			this._entityMappingConfiguration = entityMapConfiguration;
			this.Discriminator = discriminator;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00044114 File Offset: 0x00042314
		private ValueConditionConfiguration(EntityMappingConfiguration owner, ValueConditionConfiguration source)
		{
			this._entityMappingConfiguration = owner;
			this.Discriminator = source.Discriminator;
			this.Value = source.Value;
			this._configuration = ((source._configuration == null) ? null : source._configuration.Clone());
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00044162 File Offset: 0x00042362
		internal virtual ValueConditionConfiguration Clone(EntityMappingConfiguration owner)
		{
			return new ValueConditionConfiguration(owner, this);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004416C File Offset: 0x0004236C
		private T GetOrCreateConfiguration<T>() where T : PrimitivePropertyConfiguration, new()
		{
			if (this._configuration == null)
			{
				this._configuration = new T();
			}
			else if (!(this._configuration is T))
			{
				T t = new T();
				t.CopyFrom(this._configuration);
				this._configuration = t;
			}
			this._configuration.OverridableConfigurationParts = OverridableConfigurationParts.None;
			return (T)((object)this._configuration);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000441DA File Offset: 0x000423DA
		public PrimitiveColumnConfiguration HasValue<T>(T value) where T : struct
		{
			ValueConditionConfiguration.ValidateValueType(value);
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new PrimitiveColumnConfiguration(this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>());
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004420A File Offset: 0x0004240A
		public PrimitiveColumnConfiguration HasValue<T>(T? value) where T : struct
		{
			ValueConditionConfiguration.ValidateValueType(value);
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new PrimitiveColumnConfiguration(this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>());
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0004423A File Offset: 0x0004243A
		public StringColumnConfiguration HasValue(string value)
		{
			this.Value = value;
			this._entityMappingConfiguration.AddValueCondition(this);
			return new StringColumnConfiguration(this.GetOrCreateConfiguration<StringPropertyConfiguration>());
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0004425C File Offset: 0x0004245C
		private static void ValidateValueType(object value)
		{
			PrimitiveType primitiveType;
			if (value != null && !value.GetType().IsPrimitiveType(out primitiveType))
			{
				throw Error.InvalidDiscriminatorType(value.GetType().Name);
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0004428C File Offset: 0x0004248C
		internal static IEnumerable<MappingFragment> GetMappingFragmentsWithColumnAsDefaultDiscriminator(DbDatabaseMapping databaseMapping, EntityType table, EdmProperty column)
		{
			return from tmf in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
				where tmf.Table == table && tmf.GetDefaultDiscriminator() == column
				select tmf;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00044330 File Offset: 0x00042530
		internal static bool AnyBaseTypeToTableWithoutColumnCondition(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType table, EdmProperty column)
		{
			Func<MappingFragment, bool> <>9__1;
			Func<ConditionPropertyMapping, bool> <>9__3;
			for (EdmType edmType = entityType.BaseType; edmType != null; edmType = edmType.BaseType)
			{
				if (!edmType.Abstract)
				{
					IEnumerable<MappingFragment> enumerable = databaseMapping.GetEntityTypeMappings((EntityType)edmType).SelectMany((EntityTypeMapping etm) => etm.MappingFragments);
					Func<MappingFragment, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (MappingFragment tmf) => tmf.Table == table);
					}
					List<MappingFragment> list = enumerable.Where(func).ToList<MappingFragment>();
					if (list.Any<MappingFragment>())
					{
						IEnumerable<ConditionPropertyMapping> enumerable2 = list.SelectMany((MappingFragment etmf) => etmf.ColumnConditions);
						Func<ConditionPropertyMapping, bool> func2;
						if ((func2 = <>9__3) == null)
						{
							func2 = (<>9__3 = (ConditionPropertyMapping cc) => cc.Column != column);
						}
						if (enumerable2.All(func2))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00044428 File Offset: 0x00042628
		internal void Configure(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType, DbProviderManifest providerManifest)
		{
			EdmProperty edmProperty = fragment.Table.Properties.SingleOrDefault((EdmProperty c) => string.Equals(c.Name, this.Discriminator, StringComparison.Ordinal));
			if (edmProperty != null && ValueConditionConfiguration.GetMappingFragmentsWithColumnAsDefaultDiscriminator(databaseMapping, fragment.Table, edmProperty).Any<MappingFragment>())
			{
				edmProperty.Name = fragment.Table.Properties.Select((EdmProperty p) => p.Name).Uniquify(edmProperty.Name);
				edmProperty = null;
			}
			if (edmProperty == null)
			{
				TypeUsage storeType = providerManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
				edmProperty = new EdmProperty(this.Discriminator, storeType)
				{
					Nullable = false
				};
				TablePrimitiveOperations.AddColumn(fragment.Table, edmProperty);
			}
			if (ValueConditionConfiguration.AnyBaseTypeToTableWithoutColumnCondition(databaseMapping, entityType, fragment.Table, edmProperty))
			{
				edmProperty.Nullable = true;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty.GetConfiguration() as PrimitivePropertyConfiguration;
			if (this.Value != null)
			{
				this.ConfigureColumnType(providerManifest, primitivePropertyConfiguration, edmProperty);
				fragment.AddDiscriminatorCondition(edmProperty, this.Value);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(edmProperty.TypeName))
				{
					TypeUsage storeType2 = providerManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
					edmProperty.PrimitiveType = (PrimitiveType)storeType2.EdmType;
					edmProperty.MaxLength = new int?(128);
					edmProperty.Nullable = false;
				}
				this.GetOrCreateConfiguration<PrimitivePropertyConfiguration>().IsNullable = new bool?(true);
				fragment.AddNullabilityCondition(edmProperty, true);
			}
			if (this._configuration == null)
			{
				return;
			}
			string text;
			if (primitivePropertyConfiguration != null && (primitivePropertyConfiguration.OverridableConfigurationParts & OverridableConfigurationParts.OverridableInCSpace) != OverridableConfigurationParts.OverridableInCSpace && !primitivePropertyConfiguration.IsCompatible(this._configuration, true, out text))
			{
				throw Error.ConflictingColumnConfiguration(edmProperty, fragment.Table, text);
			}
			if (this._configuration.IsNullable != null)
			{
				edmProperty.Nullable = this._configuration.IsNullable.Value;
			}
			this._configuration.Configure(edmProperty, fragment.Table, providerManifest, false, false);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000445F8 File Offset: 0x000427F8
		private void ConfigureColumnType(DbProviderManifest providerManifest, PrimitivePropertyConfiguration existingConfiguration, EdmProperty discriminatorColumn)
		{
			if ((existingConfiguration != null && existingConfiguration.ColumnType != null) || (this._configuration != null && this._configuration.ColumnType != null))
			{
				return;
			}
			PrimitiveType primitiveType;
			this.Value.GetType().IsPrimitiveType(out primitiveType);
			PrimitiveType primitiveType2 = (PrimitiveType)providerManifest.GetStoreType((primitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String)) ? DatabaseMappingGenerator.DiscriminatorTypeUsage : TypeUsage.Create(PrimitiveType.GetEdmPrimitiveType(primitiveType.PrimitiveTypeKind))).EdmType;
			if (existingConfiguration != null && !discriminatorColumn.TypeName.Equals(primitiveType2.Name, StringComparison.OrdinalIgnoreCase))
			{
				throw Error.ConflictingInferredColumnType(discriminatorColumn.Name, discriminatorColumn.TypeName, primitiveType2.Name);
			}
			discriminatorColumn.PrimitiveType = primitiveType2;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000446A1 File Offset: 0x000428A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000446A9 File Offset: 0x000428A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000446B2 File Offset: 0x000428B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000446BA File Offset: 0x000428BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A78 RID: 2680
		private readonly EntityMappingConfiguration _entityMappingConfiguration;

		// Token: 0x04000A7B RID: 2683
		private PrimitivePropertyConfiguration _configuration;
	}
}
