using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001FA RID: 506
	public class ConventionTypeConfiguration
	{
		// Token: 0x06001A83 RID: 6787 RVA: 0x00047C5F File Offset: 0x00045E5F
		internal ConventionTypeConfiguration(Type type, ModelConfiguration modelConfiguration)
			: this(type, null, null, modelConfiguration)
		{
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00047C6B File Offset: 0x00045E6B
		internal ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, ModelConfiguration modelConfiguration)
			: this(type, entityTypeConfiguration, null, modelConfiguration)
		{
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00047C77 File Offset: 0x00045E77
		internal ConventionTypeConfiguration(Type type, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration)
			: this(type, null, complexTypeConfiguration, modelConfiguration)
		{
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00047C83 File Offset: 0x00045E83
		private ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._type = type;
			this._entityTypeConfiguration = entityTypeConfiguration;
			this._complexTypeConfiguration = complexTypeConfiguration;
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00047CA8 File Offset: 0x00045EA8
		public Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00047CB0 File Offset: 0x00045EB0
		public ConventionTypeConfiguration HasEntitySetName(string entitySetName)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName);
			if (this._entityTypeConfiguration != null && this._entityTypeConfiguration().EntitySetName == null)
			{
				this._entityTypeConfiguration().EntitySetName = entitySetName;
			}
			return this;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00047CFC File Offset: 0x00045EFC
		public ConventionTypeConfiguration Ignore()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.IgnoreType);
			if (this._entityTypeConfiguration == null && this._complexTypeConfiguration == null)
			{
				this._modelConfiguration.Ignore(this._type);
			}
			return this;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00047D27 File Offset: 0x00045F27
		public ConventionTypeConfiguration IsComplexType()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.IsComplexType);
			if (this._entityTypeConfiguration == null && this._complexTypeConfiguration == null)
			{
				this._modelConfiguration.ComplexType(this._type);
			}
			return this;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00047D54 File Offset: 0x00045F54
		public ConventionTypeConfiguration Ignore(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			this.Ignore(instanceProperty);
			return this;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00047DA4 File Offset: 0x00045FA4
		public ConventionTypeConfiguration Ignore(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.Ignore);
			if (propertyInfo != null)
			{
				if (this._entityTypeConfiguration != null)
				{
					this._entityTypeConfiguration().Ignore(propertyInfo);
				}
				if (this._complexTypeConfiguration != null)
				{
					this._complexTypeConfiguration().Ignore(propertyInfo);
				}
			}
			return this;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00047E00 File Offset: 0x00046000
		public ConventionPrimitivePropertyConfiguration Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.Property(instanceProperty);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00047E4D File Offset: 0x0004604D
		public ConventionPrimitivePropertyConfiguration Property(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			return this.Property(new PropertyPath(propertyInfo));
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00047E68 File Offset: 0x00046068
		internal ConventionPrimitivePropertyConfiguration Property(PropertyPath propertyPath)
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.Property);
			PropertyInfo propertyInfo = propertyPath.Last<PropertyInfo>();
			if (!propertyInfo.IsValidEdmScalarProperty())
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_NonScalarProperty(propertyPath));
			}
			PrimitivePropertyConfiguration propertyConfiguration = ((this._entityTypeConfiguration != null) ? this._entityTypeConfiguration().Property(propertyPath, null) : ((this._complexTypeConfiguration != null) ? this._complexTypeConfiguration().Property(propertyPath, null) : null));
			return new ConventionPrimitivePropertyConfiguration(propertyInfo, () => propertyConfiguration);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00047F00 File Offset: 0x00046100
		internal ConventionNavigationPropertyConfiguration NavigationProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.NavigationProperty(instanceProperty);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00047F4D File Offset: 0x0004614D
		internal ConventionNavigationPropertyConfiguration NavigationProperty(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			return this.NavigationProperty(new PropertyPath(propertyInfo));
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00047F68 File Offset: 0x00046168
		internal ConventionNavigationPropertyConfiguration NavigationProperty(PropertyPath propertyPath)
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty);
			PropertyInfo propertyInfo = propertyPath.Last<PropertyInfo>();
			if (!propertyInfo.IsValidEdmNavigationProperty())
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidNavigationProperty(propertyPath));
			}
			return new ConventionNavigationPropertyConfiguration((this._entityTypeConfiguration != null) ? this._entityTypeConfiguration().Navigation(propertyInfo) : null, this._modelConfiguration);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00047FC4 File Offset: 0x000461C4
		public ConventionTypeConfiguration HasKey(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			PropertyInfo instanceProperty = this._type.GetInstanceProperty(propertyName);
			if (instanceProperty == null)
			{
				throw new InvalidOperationException(Strings.NoSuchProperty(propertyName, this._type.Name));
			}
			return this.HasKey(instanceProperty);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00048014 File Offset: 0x00046214
		public ConventionTypeConfiguration HasKey(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasKey);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsKeyConfigured)
			{
				this._entityTypeConfiguration().Key(propertyInfo);
			}
			return this;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00048060 File Offset: 0x00046260
		public ConventionTypeConfiguration HasKey(IEnumerable<string> propertyNames)
		{
			Check.NotNull<IEnumerable<string>>(propertyNames, "propertyNames");
			PropertyInfo[] array = propertyNames.Select(delegate(string n)
			{
				PropertyInfo instanceProperty = this._type.GetInstanceProperty(n);
				if (instanceProperty == null)
				{
					throw new InvalidOperationException(Strings.NoSuchProperty(n, this._type.Name));
				}
				return instanceProperty;
			}).ToArray<PropertyInfo>();
			return this.HasKey(array);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00048098 File Offset: 0x00046298
		public ConventionTypeConfiguration HasKey(IEnumerable<PropertyInfo> keyProperties)
		{
			Check.NotNull<IEnumerable<PropertyInfo>>(keyProperties, "keyProperties");
			EntityUtil.CheckArgumentContainsNull<PropertyInfo>(ref keyProperties, "keyProperties");
			EntityUtil.CheckArgumentEmpty<PropertyInfo>(ref keyProperties, (string p) => Strings.CollectionEmpty(p, "HasKey"), "keyProperties");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasKey);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsKeyConfigured)
			{
				this._entityTypeConfiguration().Key(keyProperties);
			}
			return this;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00048120 File Offset: 0x00046320
		public ConventionTypeConfiguration ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.ToTable);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsTableNameConfigured)
			{
				DatabaseName databaseName = DatabaseName.Parse(tableName);
				this._entityTypeConfiguration().ToTable(databaseName.Name, databaseName.Schema);
			}
			return this;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00048184 File Offset: 0x00046384
		public ConventionTypeConfiguration ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.ToTable);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().IsTableNameConfigured)
			{
				this._entityTypeConfiguration().ToTable(tableName, schemaName);
			}
			return this;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000481D8 File Offset: 0x000463D8
		public ConventionTypeConfiguration HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation);
			if (this._entityTypeConfiguration != null && !this._entityTypeConfiguration().Annotations.ContainsKey(name))
			{
				this._entityTypeConfiguration().SetAnnotation(name, value);
			}
			return this;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0004822F File Offset: 0x0004642F
		public ConventionTypeConfiguration MapToStoredProcedures()
		{
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures);
			if (this._entityTypeConfiguration != null)
			{
				this._entityTypeConfiguration().MapToStoredProcedures();
			}
			return this;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00048254 File Offset: 0x00046454
		public ConventionTypeConfiguration MapToStoredProcedures(Action<ConventionModificationStoredProceduresConfiguration> modificationStoredProceduresConfigurationAction)
		{
			Check.NotNull<Action<ConventionModificationStoredProceduresConfiguration>>(modificationStoredProceduresConfigurationAction, "modificationStoredProceduresConfigurationAction");
			this.ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures);
			ConventionModificationStoredProceduresConfiguration conventionModificationStoredProceduresConfiguration = new ConventionModificationStoredProceduresConfiguration(this._type);
			modificationStoredProceduresConfigurationAction(conventionModificationStoredProceduresConfiguration);
			this.MapToStoredProcedures(conventionModificationStoredProceduresConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00048295 File Offset: 0x00046495
		internal void MapToStoredProcedures(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration)
		{
			if (this._entityTypeConfiguration != null)
			{
				this._entityTypeConfiguration().MapToStoredProcedures(modificationStoredProceduresConfiguration, false);
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000482B4 File Offset: 0x000464B4
		private void ValidateConfiguration(ConventionTypeConfiguration.ConfigurationAspect aspect)
		{
			this._currentConfigurationAspect |= aspect;
			if (this._currentConfigurationAspect.HasFlag(ConventionTypeConfiguration.ConfigurationAspect.IgnoreType) && ConventionTypeConfiguration.ConfigurationAspectsConflictingWithIgnoreType.Any((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)))
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_ConfigurationConflict_IgnoreType(ConventionTypeConfiguration.ConfigurationAspectsConflictingWithIgnoreType.First((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)), this._type.Name));
			}
			if (this._currentConfigurationAspect.HasFlag(ConventionTypeConfiguration.ConfigurationAspect.IsComplexType) && ConventionTypeConfiguration.ConfigurationAspectsConflictingWithComplexType.Any((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)))
			{
				throw new InvalidOperationException(Strings.LightweightEntityConfiguration_ConfigurationConflict_ComplexType(ConventionTypeConfiguration.ConfigurationAspectsConflictingWithComplexType.First((ConventionTypeConfiguration.ConfigurationAspect ca) => this._currentConfigurationAspect.HasFlag(ca)), this._type.Name));
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00048392 File Offset: 0x00046592
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0004839A File Offset: 0x0004659A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000483A3 File Offset: 0x000465A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000483AB File Offset: 0x000465AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A9B RID: 2715
		private readonly Type _type;

		// Token: 0x04000A9C RID: 2716
		private readonly Func<EntityTypeConfiguration> _entityTypeConfiguration;

		// Token: 0x04000A9D RID: 2717
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x04000A9E RID: 2718
		private readonly Func<ComplexTypeConfiguration> _complexTypeConfiguration;

		// Token: 0x04000A9F RID: 2719
		private ConventionTypeConfiguration.ConfigurationAspect _currentConfigurationAspect;

		// Token: 0x04000AA0 RID: 2720
		private static readonly List<ConventionTypeConfiguration.ConfigurationAspect> ConfigurationAspectsConflictingWithIgnoreType = new List<ConventionTypeConfiguration.ConfigurationAspect>
		{
			ConventionTypeConfiguration.ConfigurationAspect.IsComplexType,
			ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName,
			ConventionTypeConfiguration.ConfigurationAspect.Ignore,
			ConventionTypeConfiguration.ConfigurationAspect.HasKey,
			ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures,
			ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty,
			ConventionTypeConfiguration.ConfigurationAspect.Property,
			ConventionTypeConfiguration.ConfigurationAspect.ToTable,
			ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation
		};

		// Token: 0x04000AA1 RID: 2721
		private static readonly List<ConventionTypeConfiguration.ConfigurationAspect> ConfigurationAspectsConflictingWithComplexType = new List<ConventionTypeConfiguration.ConfigurationAspect>
		{
			ConventionTypeConfiguration.ConfigurationAspect.HasEntitySetName,
			ConventionTypeConfiguration.ConfigurationAspect.HasKey,
			ConventionTypeConfiguration.ConfigurationAspect.MapToStoredProcedures,
			ConventionTypeConfiguration.ConfigurationAspect.NavigationProperty,
			ConventionTypeConfiguration.ConfigurationAspect.ToTable,
			ConventionTypeConfiguration.ConfigurationAspect.HasTableAnnotation
		};

		// Token: 0x020008C9 RID: 2249
		[Flags]
		private enum ConfigurationAspect : uint
		{
			// Token: 0x04002448 RID: 9288
			None = 0U,
			// Token: 0x04002449 RID: 9289
			HasEntitySetName = 1U,
			// Token: 0x0400244A RID: 9290
			HasKey = 2U,
			// Token: 0x0400244B RID: 9291
			IgnoreType = 4U,
			// Token: 0x0400244C RID: 9292
			Ignore = 8U,
			// Token: 0x0400244D RID: 9293
			IsComplexType = 16U,
			// Token: 0x0400244E RID: 9294
			MapToStoredProcedures = 32U,
			// Token: 0x0400244F RID: 9295
			Property = 64U,
			// Token: 0x04002450 RID: 9296
			NavigationProperty = 128U,
			// Token: 0x04002451 RID: 9297
			ToTable = 256U,
			// Token: 0x04002452 RID: 9298
			HasTableAnnotation = 512U
		}
	}
}
