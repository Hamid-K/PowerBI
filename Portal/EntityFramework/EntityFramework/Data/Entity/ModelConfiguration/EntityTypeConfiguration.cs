using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x02000156 RID: 342
	public class EntityTypeConfiguration<TEntityType> : StructuralTypeConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x00038937 File Offset: 0x00036B37
		public EntityTypeConfiguration()
			: this(new EntityTypeConfiguration(typeof(TEntityType)))
		{
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0003894E File Offset: 0x00036B4E
		internal EntityTypeConfiguration(EntityTypeConfiguration entityTypeConfiguration)
		{
			this._entityTypeConfiguration = entityTypeConfiguration;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0003895D File Offset: 0x00036B5D
		internal override StructuralTypeConfiguration Configuration
		{
			get
			{
				return this._entityTypeConfiguration;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00038965 File Offset: 0x00036B65
		internal override TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression)
		{
			return this.Configuration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetComplexPropertyAccess(), delegate
			{
				TPrimitivePropertyConfiguration tprimitivePropertyConfiguration = new TPrimitivePropertyConfiguration();
				tprimitivePropertyConfiguration.OverridableConfigurationParts = OverridableConfigurationParts.None;
				return tprimitivePropertyConfiguration;
			});
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00038998 File Offset: 0x00036B98
		public EntityTypeConfiguration<TEntityType> HasKey<TKey>(Expression<Func<TEntityType, TKey>> keyExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TKey>>>(keyExpression, "keyExpression");
			this._entityTypeConfiguration.Key(from p in keyExpression.GetSimplePropertyAccessList()
				select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000389E8 File Offset: 0x00036BE8
		public EntityTypeConfiguration<TEntityType> HasKey<TKey>(Expression<Func<TEntityType, TKey>> keyExpression, Action<PrimaryKeyIndexConfiguration> buildAction)
		{
			Check.NotNull<Expression<Func<TEntityType, TKey>>>(keyExpression, "keyExpression");
			Check.NotNull<Action<PrimaryKeyIndexConfiguration>>(buildAction, "buildAction");
			this._entityTypeConfiguration.Key(from p in keyExpression.GetSimplePropertyAccessList()
				select p.Single<PropertyInfo>());
			buildAction(new PrimaryKeyIndexConfiguration(this._entityTypeConfiguration.ConfigureKey()));
			return this;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00038A5C File Offset: 0x00036C5C
		public IndexConfiguration HasIndex<TIndex>(Expression<Func<TEntityType, TIndex>> indexExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TIndex>>>(indexExpression, "indexExpression");
			IEnumerable<PropertyInfo> enumerable = from p in indexExpression.GetSimplePropertyAccessList()
				select p.Single<PropertyInfo>();
			return new IndexConfiguration(this._entityTypeConfiguration.Index(new PropertyPath(enumerable)));
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00038AB6 File Offset: 0x00036CB6
		public EntityTypeConfiguration<TEntityType> HasEntitySetName(string entitySetName)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this._entityTypeConfiguration.EntitySetName = entitySetName;
			return this;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00038AD1 File Offset: 0x00036CD1
		public EntityTypeConfiguration<TEntityType> Ignore<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			this.Configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00038AF8 File Offset: 0x00036CF8
		public EntityTypeConfiguration<TEntityType> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			DatabaseName databaseName = DatabaseName.Parse(tableName);
			this._entityTypeConfiguration.ToTable(databaseName.Name, databaseName.Schema);
			return this;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00038B30 File Offset: 0x00036D30
		public EntityTypeConfiguration<TEntityType> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._entityTypeConfiguration.ToTable(tableName, schemaName);
			return this;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00038B4C File Offset: 0x00036D4C
		public EntityTypeConfiguration<TEntityType> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._entityTypeConfiguration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00038B68 File Offset: 0x00036D68
		public EntityTypeConfiguration<TEntityType> MapToStoredProcedures()
		{
			this._entityTypeConfiguration.MapToStoredProcedures();
			return this;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00038B78 File Offset: 0x00036D78
		public EntityTypeConfiguration<TEntityType> MapToStoredProcedures(Action<ModificationStoredProceduresConfiguration<TEntityType>> modificationStoredProcedureMappingConfigurationAction)
		{
			Check.NotNull<Action<ModificationStoredProceduresConfiguration<TEntityType>>>(modificationStoredProcedureMappingConfigurationAction, "modificationStoredProcedureMappingConfigurationAction");
			ModificationStoredProceduresConfiguration<TEntityType> modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration<TEntityType>();
			modificationStoredProcedureMappingConfigurationAction(modificationStoredProceduresConfiguration);
			this._entityTypeConfiguration.MapToStoredProcedures(modificationStoredProceduresConfiguration.Configuration, true);
			return this;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00038BB4 File Offset: 0x00036DB4
		public EntityTypeConfiguration<TEntityType> Map(Action<EntityMappingConfiguration<TEntityType>> entityMappingConfigurationAction)
		{
			Check.NotNull<Action<EntityMappingConfiguration<TEntityType>>>(entityMappingConfigurationAction, "entityMappingConfigurationAction");
			EntityMappingConfiguration<TEntityType> entityMappingConfiguration = new EntityMappingConfiguration<TEntityType>();
			entityMappingConfigurationAction(entityMappingConfiguration);
			this._entityTypeConfiguration.AddMappingConfiguration(entityMappingConfiguration.EntityMappingConfigurationInstance, true);
			return this;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00038BF0 File Offset: 0x00036DF0
		public EntityTypeConfiguration<TEntityType> Map<TDerived>(Action<EntityMappingConfiguration<TDerived>> derivedTypeMapConfigurationAction) where TDerived : class, TEntityType
		{
			Check.NotNull<Action<EntityMappingConfiguration<TDerived>>>(derivedTypeMapConfigurationAction, "derivedTypeMapConfigurationAction");
			EntityMappingConfiguration<TDerived> entityMappingConfiguration = new EntityMappingConfiguration<TDerived>();
			DatabaseName tableName = this._entityTypeConfiguration.GetTableName();
			if (tableName != null)
			{
				entityMappingConfiguration.EntityMappingConfigurationInstance.TableName = tableName;
			}
			derivedTypeMapConfigurationAction(entityMappingConfiguration);
			if (typeof(TDerived) == typeof(TEntityType))
			{
				this._entityTypeConfiguration.AddMappingConfiguration(entityMappingConfiguration.EntityMappingConfigurationInstance, true);
			}
			else
			{
				this._entityTypeConfiguration.AddSubTypeMappingConfiguration(typeof(TDerived), entityMappingConfiguration.EntityMappingConfigurationInstance);
			}
			return this;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00038C7C File Offset: 0x00036E7C
		public OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasOptional<TTargetEntity>(Expression<Func<TEntityType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, TTargetEntity>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00038CA5 File Offset: 0x00036EA5
		public RequiredNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasRequired<TTargetEntity>(Expression<Func<TEntityType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, TTargetEntity>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new RequiredNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00038CCE File Offset: 0x00036ECE
		public ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasMany<TTargetEntity>(Expression<Func<TEntityType, ICollection<TTargetEntity>>> navigationPropertyExpression) where TTargetEntity : class
		{
			Check.NotNull<Expression<Func<TEntityType, ICollection<TTargetEntity>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			return new ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity>(this._entityTypeConfiguration.Navigation(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>()));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00038CF7 File Offset: 0x00036EF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00038CFF File Offset: 0x00036EFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00038D08 File Offset: 0x00036F08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00038D10 File Offset: 0x00036F10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040009F4 RID: 2548
		private readonly EntityTypeConfiguration _entityTypeConfiguration;
	}
}
