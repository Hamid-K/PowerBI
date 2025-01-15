using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DE RID: 478
	public class EntityMappingConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x00043B39 File Offset: 0x00041D39
		public EntityMappingConfiguration()
			: this(new EntityMappingConfiguration())
		{
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00043B46 File Offset: 0x00041D46
		internal EntityMappingConfiguration(EntityMappingConfiguration entityMappingConfiguration)
		{
			this._entityMappingConfiguration = entityMappingConfiguration;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00043B55 File Offset: 0x00041D55
		internal EntityMappingConfiguration EntityMappingConfigurationInstance
		{
			get
			{
				return this._entityMappingConfiguration;
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00043B5D File Offset: 0x00041D5D
		public void Properties<TObject>(Expression<Func<TEntityType, TObject>> propertiesExpression)
		{
			Check.NotNull<Expression<Func<TEntityType, TObject>>>(propertiesExpression, "propertiesExpression");
			this._entityMappingConfiguration.Properties = propertiesExpression.GetComplexPropertyAccessList().ToList<PropertyPath>();
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00043B81 File Offset: 0x00041D81
		public PropertyMappingConfiguration Property<T>(Expression<Func<TEntityType, T>> propertyExpression) where T : struct
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00043B8F File Offset: 0x00041D8F
		public PropertyMappingConfiguration Property<T>(Expression<Func<TEntityType, T?>> propertyExpression) where T : struct
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00043B9D File Offset: 0x00041D9D
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DbGeometry>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00043BAB File Offset: 0x00041DAB
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DbGeography>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00043BB9 File Offset: 0x00041DB9
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, string>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<StringPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00043BC7 File Offset: 0x00041DC7
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, byte[]>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<BinaryPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00043BD5 File Offset: 0x00041DD5
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, decimal>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00043BE3 File Offset: 0x00041DE3
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, decimal?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00043BF1 File Offset: 0x00041DF1
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTime>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00043BFF File Offset: 0x00041DFF
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTime?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00043C0D File Offset: 0x00041E0D
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTimeOffset>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00043C1B File Offset: 0x00041E1B
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, DateTimeOffset?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00043C29 File Offset: 0x00041E29
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, TimeSpan>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00043C37 File Offset: 0x00041E37
		public PropertyMappingConfiguration Property(Expression<Func<TEntityType, TimeSpan?>> propertyExpression)
		{
			return new PropertyMappingConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00043C45 File Offset: 0x00041E45
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration, new()
		{
			return this._entityMappingConfiguration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetComplexPropertyAccess(), delegate
			{
				TPrimitivePropertyConfiguration tprimitivePropertyConfiguration = new TPrimitivePropertyConfiguration();
				tprimitivePropertyConfiguration.OverridableConfigurationParts = OverridableConfigurationParts.None;
				return tprimitivePropertyConfiguration;
			});
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00043C77 File Offset: 0x00041E77
		public EntityMappingConfiguration<TEntityType> MapInheritedProperties()
		{
			this._entityMappingConfiguration.MapInheritedProperties = true;
			return this;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00043C88 File Offset: 0x00041E88
		public EntityMappingConfiguration<TEntityType> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			DatabaseName databaseName = DatabaseName.Parse(tableName);
			this.ToTable(databaseName.Name, databaseName.Schema);
			return this;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00043CBC File Offset: 0x00041EBC
		public EntityMappingConfiguration<TEntityType> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._entityMappingConfiguration.TableName = new DatabaseName(tableName, schemaName);
			return this;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00043CDD File Offset: 0x00041EDD
		public EntityMappingConfiguration<TEntityType> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._entityMappingConfiguration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00043CF9 File Offset: 0x00041EF9
		public ValueConditionConfiguration Requires(string discriminator)
		{
			Check.NotEmpty(discriminator, "discriminator");
			return new ValueConditionConfiguration(this._entityMappingConfiguration, discriminator);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00043D13 File Offset: 0x00041F13
		public NotNullConditionConfiguration Requires<TProperty>(Expression<Func<TEntityType, TProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(property, "property");
			return new NotNullConditionConfiguration(this._entityMappingConfiguration, property.GetComplexPropertyAccess());
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00043D32 File Offset: 0x00041F32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00043D3A File Offset: 0x00041F3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00043D43 File Offset: 0x00041F43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00043D4B File Offset: 0x00041F4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A74 RID: 2676
		private readonly EntityMappingConfiguration _entityMappingConfiguration;
	}
}
