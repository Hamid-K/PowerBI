using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000133 RID: 307
	public class EntityTypeConfiguration<TEntityType> : StructuralTypeConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0002AFCA File Offset: 0x000291CA
		internal EntityTypeConfiguration(ODataModelBuilder modelBuilder)
			: this(modelBuilder, new EntityTypeConfiguration(modelBuilder, typeof(TEntityType)))
		{
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002AFE3 File Offset: 0x000291E3
		internal EntityTypeConfiguration(ODataModelBuilder modelBuilder, EntityTypeConfiguration configuration)
			: base(configuration)
		{
			this._modelBuilder = modelBuilder;
			this._configuration = configuration;
			this._collection = new EntityCollectionConfiguration<TEntityType>(configuration);
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002B006 File Offset: 0x00029206
		public EntityTypeConfiguration BaseType
		{
			get
			{
				return this._configuration.BaseType;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002B013 File Offset: 0x00029213
		public IEnumerable<NavigationPropertyConfiguration> NavigationProperties
		{
			get
			{
				return this._configuration.NavigationProperties;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002B020 File Offset: 0x00029220
		public EntityCollectionConfiguration<TEntityType> Collection
		{
			get
			{
				return this._collection;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002B028 File Offset: 0x00029228
		public EntityTypeConfiguration<TEntityType> Abstract()
		{
			this._configuration.IsAbstract = new bool?(true);
			return this;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002B03C File Offset: 0x0002923C
		public EntityTypeConfiguration<TEntityType> MediaType()
		{
			this._configuration.HasStream = true;
			return this;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002B04B File Offset: 0x0002924B
		public EntityTypeConfiguration<TEntityType> DerivesFromNothing()
		{
			this._configuration.DerivesFromNothing();
			return this;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002B05C File Offset: 0x0002925C
		public EntityTypeConfiguration<TEntityType> DerivesFrom<TBaseType>() where TBaseType : class
		{
			EntityTypeConfiguration<TBaseType> entityTypeConfiguration = this._modelBuilder.EntityType<TBaseType>();
			this._configuration.DerivesFrom(entityTypeConfiguration._configuration);
			return this;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002B088 File Offset: 0x00029288
		public EntityTypeConfiguration<TEntityType> HasKey<TKey>(Expression<Func<TEntityType, TKey>> keyDefinitionExpression)
		{
			foreach (PropertyInfo propertyInfo in PropertySelectorVisitor.GetSelectedProperties(keyDefinitionExpression))
			{
				this._configuration.HasKey(propertyInfo);
			}
			return this;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002B0DC File Offset: 0x000292DC
		public ActionConfiguration Action(string name)
		{
			ActionConfiguration actionConfiguration = this._configuration.ModelBuilder.Action(name);
			actionConfiguration.SetBindingParameter("bindingParameter", this._configuration);
			return actionConfiguration;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002B101 File Offset: 0x00029301
		public FunctionConfiguration Function(string name)
		{
			FunctionConfiguration functionConfiguration = this._configuration.ModelBuilder.Function(name);
			functionConfiguration.SetBindingParameter("bindingParameter", this._configuration);
			return functionConfiguration;
		}

		// Token: 0x0400034A RID: 842
		private EntityTypeConfiguration _configuration;

		// Token: 0x0400034B RID: 843
		private EntityCollectionConfiguration<TEntityType> _collection;

		// Token: 0x0400034C RID: 844
		private ODataModelBuilder _modelBuilder;
	}
}
