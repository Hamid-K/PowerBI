using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity
{
	// Token: 0x0200005F RID: 95
	public class DbModelBuilder
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000B448 File Offset: 0x00009648
		public DbModelBuilder()
			: this(new ModelConfiguration(), DbModelBuilderVersion.Latest)
		{
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B456 File Offset: 0x00009656
		public DbModelBuilder(DbModelBuilderVersion modelBuilderVersion)
			: this(new ModelConfiguration(), modelBuilderVersion)
		{
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), modelBuilderVersion))
			{
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B486 File Offset: 0x00009686
		internal DbModelBuilder(ModelConfiguration modelConfiguration, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest)
			: this(modelConfiguration, new ConventionsConfiguration(DbModelBuilder.SelectConventionSet(modelBuilderVersion)), modelBuilderVersion)
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B49B File Offset: 0x0000969B
		private static ConventionSet SelectConventionSet(DbModelBuilderVersion modelBuilderVersion)
		{
			switch (modelBuilderVersion)
			{
			case DbModelBuilderVersion.Latest:
			case DbModelBuilderVersion.V5_0_Net4:
			case DbModelBuilderVersion.V5_0:
			case DbModelBuilderVersion.V6_0:
				return V2ConventionSet.Conventions;
			case DbModelBuilderVersion.V4_1:
				return V1ConventionSet.Conventions;
			default:
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B4D0 File Offset: 0x000096D0
		private DbModelBuilder(ModelConfiguration modelConfiguration, ConventionsConfiguration conventionsConfiguration, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest)
		{
			this._lock = new object();
			base..ctor();
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), modelBuilderVersion))
			{
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
			this._modelConfiguration = modelConfiguration;
			this._conventionsConfiguration = conventionsConfiguration;
			this._modelBuilderVersion = modelBuilderVersion;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B528 File Offset: 0x00009728
		private DbModelBuilder(DbModelBuilder source)
		{
			this._lock = new object();
			base..ctor();
			this._modelConfiguration = source._modelConfiguration.Clone();
			this._conventionsConfiguration = source._conventionsConfiguration.Clone();
			this._modelBuilderVersion = source._modelBuilderVersion;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B574 File Offset: 0x00009774
		internal virtual DbModelBuilder Clone()
		{
			object @lock = this._lock;
			DbModelBuilder dbModelBuilder;
			lock (@lock)
			{
				dbModelBuilder = new DbModelBuilder(this);
			}
			return dbModelBuilder;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B5B8 File Offset: 0x000097B8
		internal DbModel BuildDynamicUpdateModel(DbProviderInfo providerInfo)
		{
			DbModel dbModel = this.Build(providerInfo);
			EntityContainerMapping entityContainerMapping = dbModel.DatabaseMapping.EntityContainerMappings.Single<EntityContainerMapping>();
			entityContainerMapping.EntitySetMappings.Each(delegate(EntitySetMapping esm)
			{
				esm.ClearModificationFunctionMappings();
			});
			entityContainerMapping.AssociationSetMappings.Each((AssociationSetMapping asm) => asm.ModificationFunctionMapping = null);
			return dbModel;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B62F File Offset: 0x0000982F
		public virtual DbModelBuilder Ignore<T>() where T : class
		{
			this._modelConfiguration.Ignore(typeof(T));
			return this;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B647 File Offset: 0x00009847
		public virtual DbModelBuilder HasDefaultSchema(string schema)
		{
			this._modelConfiguration.DefaultSchema = schema;
			return this;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B658 File Offset: 0x00009858
		public virtual DbModelBuilder Ignore(IEnumerable<Type> types)
		{
			Check.NotNull<IEnumerable<Type>>(types, "types");
			foreach (Type type in types)
			{
				this._modelConfiguration.Ignore(type);
			}
			return this;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public virtual EntityTypeConfiguration<TEntityType> Entity<TEntityType>() where TEntityType : class
		{
			return new EntityTypeConfiguration<TEntityType>(this._modelConfiguration.Entity(typeof(TEntityType), true));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B6D1 File Offset: 0x000098D1
		public virtual void RegisterEntityType(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			this.Entity(entityType);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B6E7 File Offset: 0x000098E7
		internal virtual EntityTypeConfiguration Entity(Type entityType)
		{
			EntityTypeConfiguration entityTypeConfiguration = this._modelConfiguration.Entity(entityType);
			entityTypeConfiguration.IsReplaceable = true;
			return entityTypeConfiguration;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B6FC File Offset: 0x000098FC
		public virtual ComplexTypeConfiguration<TComplexType> ComplexType<TComplexType>() where TComplexType : class
		{
			return new ComplexTypeConfiguration<TComplexType>(this._modelConfiguration.ComplexType(typeof(TComplexType)));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B718 File Offset: 0x00009918
		public TypeConventionConfiguration Types()
		{
			return new TypeConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B725 File Offset: 0x00009925
		public TypeConventionConfiguration<T> Types<T>() where T : class
		{
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B732 File Offset: 0x00009932
		public PropertyConventionConfiguration Properties()
		{
			return new PropertyConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B740 File Offset: 0x00009940
		public PropertyConventionConfiguration Properties<T>()
		{
			if (!typeof(T).IsValidEdmScalarType())
			{
				throw Error.ModelBuilder_PropertyFilterTypeMustBePrimitive(typeof(T));
			}
			return new PropertyConventionConfiguration(this._conventionsConfiguration).Where(delegate(PropertyInfo p)
			{
				Type type;
				p.PropertyType.TryUnwrapNullableType(out type);
				return type == typeof(T);
			});
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B79D File Offset: 0x0000999D
		public virtual ConventionsConfiguration Conventions
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B7A5 File Offset: 0x000099A5
		public virtual ConfigurationRegistrar Configurations
		{
			get
			{
				return new ConfigurationRegistrar(this._modelConfiguration);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public virtual DbModel Build(DbConnection providerConnection)
		{
			Check.NotNull<DbConnection>(providerConnection, "providerConnection");
			DbProviderManifest dbProviderManifest;
			DbProviderInfo providerInfo = providerConnection.GetProviderInfo(out dbProviderManifest);
			return this.Build(dbProviderManifest, providerInfo);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public virtual DbModel Build(DbProviderInfo providerInfo)
		{
			Check.NotNull<DbProviderInfo>(providerInfo, "providerInfo");
			DbProviderManifest providerManifest = DbModelBuilder.GetProviderManifest(providerInfo);
			return this.Build(providerManifest, providerInfo);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B808 File Offset: 0x00009A08
		internal DbModelBuilderVersion Version
		{
			get
			{
				return this._modelBuilderVersion;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B810 File Offset: 0x00009A10
		private DbModel Build(DbProviderManifest providerManifest, DbProviderInfo providerInfo)
		{
			double edmVersion = this._modelBuilderVersion.GetEdmVersion();
			DbModelBuilder dbModelBuilder = this.Clone();
			DbModel dbModel = new DbModel(new DbDatabaseMapping
			{
				Model = EdmModel.CreateConceptualModel(edmVersion),
				Database = EdmModel.CreateStoreModel(providerInfo, providerManifest, edmVersion)
			}, dbModelBuilder);
			dbModel.ConceptualModel.Container.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes", "true");
			this._conventionsConfiguration.ApplyModelConfiguration(this._modelConfiguration);
			this._modelConfiguration.NormalizeConfigurations();
			this.MapTypes(dbModel.ConceptualModel);
			this._modelConfiguration.Configure(dbModel.ConceptualModel);
			this._conventionsConfiguration.ApplyConceptualModel(dbModel);
			dbModel.ConceptualModel.Validate();
			dbModel = new DbModel(dbModel.ConceptualModel.GenerateDatabaseMapping(providerInfo, providerManifest), dbModelBuilder);
			this._conventionsConfiguration.ApplyPluralizingTableNameConvention(dbModel);
			this._modelConfiguration.Configure(dbModel.DatabaseMapping, providerManifest);
			this._conventionsConfiguration.ApplyStoreModel(dbModel);
			this._conventionsConfiguration.ApplyMapping(dbModel.DatabaseMapping);
			dbModel.StoreModel.Validate();
			return dbModel;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B91B File Offset: 0x00009B1B
		private static DbProviderManifest GetProviderManifest(DbProviderInfo providerInfo)
		{
			return DbConfiguration.DependencyResolver.GetService(providerInfo.ProviderInvariantName).GetProviderServices().GetProviderManifest(providerInfo.ProviderManifestToken);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B940 File Offset: 0x00009B40
		private void MapTypes(EdmModel model)
		{
			TypeMapper typeMapper = new TypeMapper(new MappingContext(this._modelConfiguration, this._conventionsConfiguration, model, this._modelBuilderVersion, DbConfiguration.DependencyResolver.GetService<AttributeProvider>()));
			IList<Type> list = (this._modelConfiguration.Entities as IList<Type>) ?? this._modelConfiguration.Entities.ToList<Type>();
			for (int i = 0; i < list.Count; i++)
			{
				Type type = list[i];
				if (typeMapper.MapEntityType(type) == null)
				{
					throw Error.InvalidEntityType(type);
				}
			}
			IList<Type> list2 = (this._modelConfiguration.ComplexTypes as IList<Type>) ?? this._modelConfiguration.ComplexTypes.ToList<Type>();
			for (int j = 0; j < list2.Count; j++)
			{
				Type type2 = list2[j];
				if (typeMapper.MapComplexType(type2, false) == null)
				{
					throw Error.CodeFirstInvalidComplexType(type2);
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000BA1D File Offset: 0x00009C1D
		internal ModelConfiguration ModelConfiguration
		{
			get
			{
				return this._modelConfiguration;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BA25 File Offset: 0x00009C25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BA2D File Offset: 0x00009C2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BA36 File Offset: 0x00009C36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BA3E File Offset: 0x00009C3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040000B8 RID: 184
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x040000B9 RID: 185
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040000BA RID: 186
		private readonly DbModelBuilderVersion _modelBuilderVersion;

		// Token: 0x040000BB RID: 187
		private readonly object _lock;
	}
}
