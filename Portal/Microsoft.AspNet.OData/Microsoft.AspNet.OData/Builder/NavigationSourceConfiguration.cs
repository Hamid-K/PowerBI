using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200010F RID: 271
	public abstract class NavigationSourceConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00026A24 File Offset: 0x00024C24
		internal NavigationSourceConfiguration(ODataModelBuilder modelBuilder, NavigationSourceConfiguration configuration)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
			this._modelBuilder = modelBuilder;
			this._entityType = new EntityTypeConfiguration<TEntityType>(modelBuilder, this._configuration.EntityType);
			this._binding = new BindingPathConfiguration<TEntityType>(modelBuilder, this._entityType, this._configuration);
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026A90 File Offset: 0x00024C90
		public EntityTypeConfiguration<TEntityType> EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00026A98 File Offset: 0x00024C98
		internal NavigationSourceConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00026AA0 File Offset: 0x00024CA0
		public BindingPathConfiguration<TEntityType> Binding
		{
			get
			{
				return this._binding;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00026AA8 File Offset: 0x00024CA8
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, IEnumerable<TTargetType>>> navigationExpression, string entitySetName) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasMany<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, this._modelBuilder.EntitySet<TTargetType>(entitySetName)._configuration, list);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00026B34 File Offset: 0x00024D34
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType>(Expression<Func<TEntityType, IEnumerable<TTargetType>>> navigationExpression, string entitySetName) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			return this.Configuration.AddBinding(this.EntityType.HasMany<TTargetType>(navigationExpression), this._modelBuilder.EntitySet<TTargetType>(entitySetName)._configuration);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00026B8A File Offset: 0x00024D8A
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType>(Expression<Func<TEntityType, IEnumerable<TTargetType>>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			return this.Configuration.AddBinding(this.EntityType.HasMany<TTargetType>(navigationExpression), targetEntitySet.Configuration);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00026BC8 File Offset: 0x00024DC8
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, IEnumerable<TTargetType>>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasMany<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, targetEntitySet.Configuration, list);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00026C44 File Offset: 0x00024E44
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, string entitySetName) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			return this.Configuration.AddBinding(this.EntityType.HasRequired<TTargetType>(navigationExpression), this._modelBuilder.EntitySet<TTargetType>(entitySetName).Configuration);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00026C9C File Offset: 0x00024E9C
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, string entitySetName) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, this._modelBuilder.EntitySet<TTargetType>(entitySetName).Configuration, list);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00026D26 File Offset: 0x00024F26
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			return this.Configuration.AddBinding(this.EntityType.HasRequired<TTargetType>(navigationExpression), targetEntitySet.Configuration);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00026D64 File Offset: 0x00024F64
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, targetEntitySet.Configuration, list);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, string entitySetName) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			return this.Configuration.AddBinding(this.EntityType.HasOptional<TTargetType>(navigationExpression), this._modelBuilder.EntitySet<TTargetType>(entitySetName).Configuration);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00026E38 File Offset: 0x00025038
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, string entitySetName) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasOptional<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, this._modelBuilder.EntitySet<TTargetType>(entitySetName).Configuration, list);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00026EC2 File Offset: 0x000250C2
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			return this.Configuration.AddBinding(this.EntityType.HasOptional<TTargetType>(navigationExpression), targetEntitySet.Configuration);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00026F00 File Offset: 0x00025100
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetEntitySet) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetEntitySet == null)
			{
				throw Error.ArgumentNull("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasOptional<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, targetEntitySet.Configuration, list);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00026F7C File Offset: 0x0002517C
		public NavigationPropertyBindingConfiguration HasSingletonBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, string singletonName) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(singletonName))
			{
				throw Error.ArgumentNullOrEmpty("singletonName");
			}
			return this.Configuration.AddBinding(this.EntityType.HasRequired<TTargetType>(navigationExpression), this._modelBuilder.Singleton<TTargetType>(singletonName).Configuration);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00026FD4 File Offset: 0x000251D4
		public NavigationPropertyBindingConfiguration HasSingletonBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, string singletonName) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(singletonName))
			{
				throw Error.ArgumentNullOrEmpty("singletonName");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, this._modelBuilder.Singleton<TTargetType>(singletonName).Configuration, list);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002705E File Offset: 0x0002525E
		public NavigationPropertyBindingConfiguration HasSingletonBinding<TTargetType>(Expression<Func<TEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetSingleton) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetSingleton == null)
			{
				throw Error.ArgumentNull("targetSingleton");
			}
			return this.Configuration.AddBinding(this.EntityType.HasRequired<TTargetType>(navigationExpression), targetSingleton.Configuration);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002709C File Offset: 0x0002529C
		public NavigationPropertyBindingConfiguration HasSingletonBinding<TTargetType, TDerivedEntityType>(Expression<Func<TDerivedEntityType, TTargetType>> navigationExpression, NavigationSourceConfiguration<TTargetType> targetSingleton) where TTargetType : class where TDerivedEntityType : class, TEntityType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (targetSingleton == null)
			{
				throw Error.ArgumentNull("targetSingleton");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._modelBuilder.EntityType<TDerivedEntityType>().DerivesFrom<TEntityType>().HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>
			{
				TypeHelper.AsMemberInfo(typeof(TDerivedEntityType)),
				navigationPropertyConfiguration.PropertyInfo
			};
			return this.Configuration.AddBinding(navigationPropertyConfiguration, targetSingleton.Configuration, list);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00027118 File Offset: 0x00025318
		public void HasEditLink(Func<ResourceContext<TEntityType>, Uri> editLinkFactory, bool followsConventions)
		{
			if (editLinkFactory == null)
			{
				throw Error.ArgumentNull("editLinkFactory");
			}
			this._configuration.HasEditLink(new SelfLinkBuilder<Uri>((ResourceContext context) => editLinkFactory(NavigationSourceConfiguration<TEntityType>.UpCastEntityContext(context)), followsConventions));
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00027164 File Offset: 0x00025364
		public void HasReadLink(Func<ResourceContext<TEntityType>, Uri> readLinkFactory, bool followsConventions)
		{
			if (readLinkFactory == null)
			{
				throw Error.ArgumentNull("readLinkFactory");
			}
			this._configuration.HasReadLink(new SelfLinkBuilder<Uri>((ResourceContext context) => readLinkFactory(NavigationSourceConfiguration<TEntityType>.UpCastEntityContext(context)), followsConventions));
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000271B0 File Offset: 0x000253B0
		public void HasIdLink(Func<ResourceContext<TEntityType>, Uri> idLinkFactory, bool followsConventions)
		{
			if (idLinkFactory == null)
			{
				throw Error.ArgumentNull("idLinkFactory");
			}
			this._configuration.HasIdLink(new SelfLinkBuilder<Uri>((ResourceContext context) => idLinkFactory(NavigationSourceConfiguration<TEntityType>.UpCastEntityContext(context)), followsConventions));
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000271FC File Offset: 0x000253FC
		public void HasNavigationPropertyLink(NavigationPropertyConfiguration navigationProperty, Func<ResourceContext<TEntityType>, IEdmNavigationProperty, Uri> navigationLinkFactory, bool followsConventions)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			if (navigationLinkFactory == null)
			{
				throw Error.ArgumentNull("navigationLinkFactory");
			}
			this._configuration.HasNavigationPropertyLink(navigationProperty, new NavigationLinkBuilder((ResourceContext context, IEdmNavigationProperty property) => navigationLinkFactory(NavigationSourceConfiguration<TEntityType>.UpCastEntityContext(context), property), followsConventions));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00027258 File Offset: 0x00025458
		public void HasNavigationPropertiesLink(IEnumerable<NavigationPropertyConfiguration> navigationProperties, Func<ResourceContext<TEntityType>, IEdmNavigationProperty, Uri> navigationLinkFactory, bool followsConventions)
		{
			if (navigationProperties == null)
			{
				throw Error.ArgumentNull("navigationProperties");
			}
			if (navigationLinkFactory == null)
			{
				throw Error.ArgumentNull("navigationLinkFactory");
			}
			this._configuration.HasNavigationPropertiesLink(navigationProperties, new NavigationLinkBuilder((ResourceContext entity, IEdmNavigationProperty property) => navigationLinkFactory(NavigationSourceConfiguration<TEntityType>.UpCastEntityContext(entity), property), followsConventions));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000272B2 File Offset: 0x000254B2
		public IEnumerable<NavigationPropertyBindingConfiguration> FindBindings(string propertyName)
		{
			return this._configuration.FindBindings(propertyName);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000272C0 File Offset: 0x000254C0
		public IEnumerable<NavigationPropertyBindingConfiguration> FindBinding(NavigationPropertyConfiguration navigationConfiguration)
		{
			return this._configuration.FindBinding(navigationConfiguration);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000272CE File Offset: 0x000254CE
		public NavigationPropertyBindingConfiguration FindBinding(NavigationPropertyConfiguration navigationConfiguration, IList<MemberInfo> bindingPath)
		{
			return this._configuration.FindBinding(navigationConfiguration, bindingPath);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000272DD File Offset: 0x000254DD
		private static ResourceContext<TEntityType> UpCastEntityContext(ResourceContext context)
		{
			return new ResourceContext<TEntityType>
			{
				SerializerContext = context.SerializerContext,
				EdmObject = context.EdmObject,
				StructuredType = context.StructuredType
			};
		}

		// Token: 0x040002F8 RID: 760
		private readonly NavigationSourceConfiguration _configuration;

		// Token: 0x040002F9 RID: 761
		private readonly EntityTypeConfiguration<TEntityType> _entityType;

		// Token: 0x040002FA RID: 762
		private readonly ODataModelBuilder _modelBuilder;

		// Token: 0x040002FB RID: 763
		private readonly BindingPathConfiguration<TEntityType> _binding;
	}
}
