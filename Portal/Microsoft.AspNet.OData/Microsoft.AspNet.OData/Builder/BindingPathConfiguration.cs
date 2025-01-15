using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x020000FF RID: 255
	public class BindingPathConfiguration<TStructuralType> where TStructuralType : class
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x000253C1 File Offset: 0x000235C1
		public BindingPathConfiguration(ODataModelBuilder modelBuilder, StructuralTypeConfiguration<TStructuralType> structuralType, NavigationSourceConfiguration navigationSource)
			: this(modelBuilder, structuralType, navigationSource, new List<MemberInfo>())
		{
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000253D4 File Offset: 0x000235D4
		public BindingPathConfiguration(ODataModelBuilder modelBuilder, StructuralTypeConfiguration<TStructuralType> structuralType, NavigationSourceConfiguration navigationSource, IList<MemberInfo> bindingPath)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (structuralType == null)
			{
				throw Error.ArgumentNull("structuralType");
			}
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			if (bindingPath == null)
			{
				throw Error.ArgumentNull("bindingPath");
			}
			this._modelBuilder = modelBuilder;
			this._navigationSource = navigationSource;
			this._structuralType = structuralType;
			this._bindingPath = bindingPath;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002543D File Offset: 0x0002363D
		public IList<MemberInfo> Path
		{
			get
			{
				return this._bindingPath;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00025445 File Offset: 0x00023645
		public string BindingPath
		{
			get
			{
				return this._bindingPath.ConvertBindingPath();
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00025452 File Offset: 0x00023652
		public BindingPathConfiguration<TTargetType> HasManyPath<TTargetType>(Expression<Func<TStructuralType, IEnumerable<TTargetType>>> pathExpression) where TTargetType : class
		{
			return this.HasManyPath<TTargetType>(pathExpression, false);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002545C File Offset: 0x0002365C
		public BindingPathConfiguration<TTargetType> HasManyPath<TTargetType>(Expression<Func<TStructuralType, IEnumerable<TTargetType>>> pathExpression, bool contained) where TTargetType : class
		{
			if (pathExpression == null)
			{
				throw Error.ArgumentNull("pathExpression");
			}
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(pathExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(selectedProperty);
			StructuralTypeConfiguration<TTargetType> structuralTypeConfiguration;
			if (contained)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TTargetType>();
				this._structuralType.ContainsMany<TTargetType>(pathExpression);
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TTargetType>();
				this._structuralType.CollectionProperty<TTargetType>(pathExpression);
			}
			return new BindingPathConfiguration<TTargetType>(this._modelBuilder, structuralTypeConfiguration, this._navigationSource, list);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000254DB File Offset: 0x000236DB
		public BindingPathConfiguration<TTargetType> HasManyPath<TTargetType, TDerivedType>(Expression<Func<TDerivedType, IEnumerable<TTargetType>>> pathExpression) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			return this.HasManyPath<TTargetType, TDerivedType>(pathExpression, false);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000254E8 File Offset: 0x000236E8
		public BindingPathConfiguration<TTargetType> HasManyPath<TTargetType, TDerivedType>(Expression<Func<TDerivedType, IEnumerable<TTargetType>>> pathExpression, bool contained) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			if (pathExpression == null)
			{
				throw Error.ArgumentNull("pathExpression");
			}
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(pathExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(TypeHelper.AsMemberInfo(typeof(TDerivedType)));
			list.Add(selectedProperty);
			StructuralTypeConfiguration<TDerivedType> structuralTypeConfiguration;
			if (this._structuralType.Configuration.Kind == EdmTypeKind.Entity)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			StructuralTypeConfiguration<TTargetType> structuralTypeConfiguration2;
			if (contained)
			{
				structuralTypeConfiguration2 = this._modelBuilder.EntityType<TTargetType>();
				structuralTypeConfiguration.ContainsMany<TTargetType>(pathExpression);
			}
			else
			{
				structuralTypeConfiguration2 = this._modelBuilder.ComplexType<TTargetType>();
				structuralTypeConfiguration.CollectionProperty<TTargetType>(pathExpression);
			}
			return new BindingPathConfiguration<TTargetType>(this._modelBuilder, structuralTypeConfiguration2, this._navigationSource, list);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000255A9 File Offset: 0x000237A9
		public BindingPathConfiguration<TTargetType> HasSinglePath<TTargetType>(Expression<Func<TStructuralType, TTargetType>> pathExpression) where TTargetType : class
		{
			return this.HasSinglePath<TTargetType>(pathExpression, false, false);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000255B4 File Offset: 0x000237B4
		public BindingPathConfiguration<TTargetType> HasSinglePath<TTargetType>(Expression<Func<TStructuralType, TTargetType>> pathExpression, bool required, bool contained) where TTargetType : class
		{
			if (pathExpression == null)
			{
				throw Error.ArgumentNull("pathExpression");
			}
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(pathExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(selectedProperty);
			StructuralTypeConfiguration<TTargetType> structuralTypeConfiguration;
			if (contained)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TTargetType>();
				if (required)
				{
					this._structuralType.ContainsRequired<TTargetType>(pathExpression);
				}
				else
				{
					this._structuralType.ContainsOptional<TTargetType>(pathExpression);
				}
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TTargetType>();
				this._structuralType.ComplexProperty<TTargetType>(pathExpression).OptionalProperty = !required;
			}
			return new BindingPathConfiguration<TTargetType>(this._modelBuilder, structuralTypeConfiguration, this._navigationSource, list);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002564D File Offset: 0x0002384D
		public BindingPathConfiguration<TTargetType> HasSinglePath<TTargetType, TDerivedType>(Expression<Func<TDerivedType, TTargetType>> pathExpression) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			return this.HasSinglePath<TTargetType, TDerivedType>(pathExpression, false, false);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00025658 File Offset: 0x00023858
		public BindingPathConfiguration<TTargetType> HasSinglePath<TTargetType, TDerivedType>(Expression<Func<TDerivedType, TTargetType>> pathExpression, bool required, bool contained) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			if (pathExpression == null)
			{
				throw Error.ArgumentNull("pathExpression");
			}
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(pathExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(TypeHelper.AsMemberInfo(typeof(TDerivedType)));
			list.Add(selectedProperty);
			StructuralTypeConfiguration<TDerivedType> structuralTypeConfiguration;
			if (this._structuralType.Configuration.Kind == EdmTypeKind.Entity)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			StructuralTypeConfiguration<TTargetType> structuralTypeConfiguration2;
			if (contained)
			{
				structuralTypeConfiguration2 = this._modelBuilder.EntityType<TTargetType>();
				if (required)
				{
					structuralTypeConfiguration.ContainsRequired<TTargetType>(pathExpression);
				}
				else
				{
					structuralTypeConfiguration.ContainsOptional<TTargetType>(pathExpression);
				}
			}
			else
			{
				structuralTypeConfiguration2 = this._modelBuilder.ComplexType<TTargetType>();
				structuralTypeConfiguration.ComplexProperty<TTargetType>(pathExpression).OptionalProperty = !required;
			}
			return new BindingPathConfiguration<TTargetType>(this._modelBuilder, structuralTypeConfiguration2, this._navigationSource, list);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00025730 File Offset: 0x00023930
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType>(Expression<Func<TStructuralType, IEnumerable<TTargetType>>> navigationExpression, string targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._structuralType.HasMany<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000257A4 File Offset: 0x000239A4
		public NavigationPropertyBindingConfiguration HasManyBinding<TTargetType, TDerivedType>(Expression<Func<TDerivedType, IEnumerable<TTargetType>>> navigationExpression, string targetEntitySet) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			StructuralTypeConfiguration<TDerivedType> structuralTypeConfiguration;
			if (this._structuralType.Configuration.Kind == EdmTypeKind.Entity)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = structuralTypeConfiguration.HasMany<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(TypeHelper.AsMemberInfo(typeof(TDerivedType)));
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00025860 File Offset: 0x00023A60
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType>(Expression<Func<TStructuralType, TTargetType>> navigationExpression, string targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._structuralType.HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000258D4 File Offset: 0x00023AD4
		public NavigationPropertyBindingConfiguration HasRequiredBinding<TTargetType, TDerivedType>(Expression<Func<TDerivedType, TTargetType>> navigationExpression, string targetEntitySet) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			StructuralTypeConfiguration<TDerivedType> structuralTypeConfiguration;
			if (this._structuralType.Configuration.Kind == EdmTypeKind.Entity)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = structuralTypeConfiguration.HasRequired<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(TypeHelper.AsMemberInfo(typeof(TDerivedType)));
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00025990 File Offset: 0x00023B90
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType>(Expression<Func<TStructuralType, TTargetType>> navigationExpression, string targetEntitySet) where TTargetType : class
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = this._structuralType.HasOptional<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00025A04 File Offset: 0x00023C04
		public NavigationPropertyBindingConfiguration HasOptionalBinding<TTargetType, TDerivedType>(Expression<Func<TDerivedType, TTargetType>> navigationExpression, string targetEntitySet) where TTargetType : class where TDerivedType : class, TStructuralType
		{
			if (navigationExpression == null)
			{
				throw Error.ArgumentNull("navigationExpression");
			}
			if (string.IsNullOrEmpty(targetEntitySet))
			{
				throw Error.ArgumentNullOrEmpty("targetEntitySet");
			}
			StructuralTypeConfiguration<TDerivedType> structuralTypeConfiguration;
			if (this._structuralType.Configuration.Kind == EdmTypeKind.Entity)
			{
				structuralTypeConfiguration = this._modelBuilder.EntityType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			else
			{
				structuralTypeConfiguration = this._modelBuilder.ComplexType<TDerivedType>().DerivesFrom<TStructuralType>();
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = structuralTypeConfiguration.HasOptional<TTargetType>(navigationExpression);
			IList<MemberInfo> list = new List<MemberInfo>(this._bindingPath);
			list.Add(TypeHelper.AsMemberInfo(typeof(TDerivedType)));
			list.Add(navigationPropertyConfiguration.PropertyInfo);
			NavigationSourceConfiguration configuration = this._modelBuilder.EntitySet<TTargetType>(targetEntitySet).Configuration;
			return this._navigationSource.AddBinding(navigationPropertyConfiguration, configuration, list);
		}

		// Token: 0x040002C9 RID: 713
		private readonly NavigationSourceConfiguration _navigationSource;

		// Token: 0x040002CA RID: 714
		private readonly StructuralTypeConfiguration<TStructuralType> _structuralType;

		// Token: 0x040002CB RID: 715
		private readonly ODataModelBuilder _modelBuilder;

		// Token: 0x040002CC RID: 716
		private readonly IList<MemberInfo> _bindingPath;
	}
}
