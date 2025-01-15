using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000141 RID: 321
	public abstract class StructuralTypeConfiguration<TStructuralType> where TStructuralType : class
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		protected StructuralTypeConfiguration(StructuralTypeConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002D811 File Offset: 0x0002BA11
		public IEnumerable<PropertyConfiguration> Properties
		{
			get
			{
				return this._configuration.Properties;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002D81E File Offset: 0x0002BA1E
		public string FullName
		{
			get
			{
				return this._configuration.FullName;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002D82B File Offset: 0x0002BA2B
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0002D838 File Offset: 0x0002BA38
		public string Namespace
		{
			get
			{
				return this._configuration.Namespace;
			}
			set
			{
				this._configuration.Namespace = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0002D846 File Offset: 0x0002BA46
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x0002D853 File Offset: 0x0002BA53
		public string Name
		{
			get
			{
				return this._configuration.Name;
			}
			set
			{
				this._configuration.Name = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0002D861 File Offset: 0x0002BA61
		public bool IsOpen
		{
			get
			{
				return this._configuration.IsOpen;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0002D86E File Offset: 0x0002BA6E
		internal StructuralTypeConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002D878 File Offset: 0x0002BA78
		public virtual void Ignore<TProperty>(Expression<Func<TStructuralType, TProperty>> propertyExpression)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			this._configuration.RemoveProperty(selectedProperty);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002D898 File Offset: 0x0002BA98
		public LengthPropertyConfiguration Property(Expression<Func<TStructuralType, string>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as LengthPropertyConfiguration;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002D898 File Offset: 0x0002BA98
		public LengthPropertyConfiguration Property(Expression<Func<TStructuralType, byte[]>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as LengthPropertyConfiguration;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002D8A7 File Offset: 0x0002BAA7
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, Stream>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002D8B1 File Offset: 0x0002BAB1
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal?>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as DecimalPropertyConfiguration;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, false) as DecimalPropertyConfiguration;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, TimeOfDay?>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002D8DE File Offset: 0x0002BADE
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, TimeOfDay>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, false) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan?>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002D8DE File Offset: 0x0002BADE
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, false) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset?>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002D8DE File Offset: 0x0002BADE
		public PrecisionPropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset>> propertyExpression)
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, false) as PrecisionPropertyConfiguration;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002D8A7 File Offset: 0x0002BAA7
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, true);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002D8ED File Offset: 0x0002BAED
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
		{
			return this.GetPrimitivePropertyConfiguration(propertyExpression, false);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002D8F7 File Offset: 0x0002BAF7
		public EnumPropertyConfiguration EnumProperty<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
		{
			return this.GetEnumPropertyConfiguration(propertyExpression, true);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002D901 File Offset: 0x0002BB01
		public EnumPropertyConfiguration EnumProperty<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
		{
			return this.GetEnumPropertyConfiguration(propertyExpression, false);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002D90B File Offset: 0x0002BB0B
		public ComplexPropertyConfiguration ComplexProperty<TComplexType>(Expression<Func<TStructuralType, TComplexType>> propertyExpression)
		{
			return this.GetComplexPropertyConfiguration(propertyExpression, false);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002D915 File Offset: 0x0002BB15
		public CollectionPropertyConfiguration CollectionProperty<TElementType>(Expression<Func<TStructuralType, IEnumerable<TElementType>>> propertyExpression)
		{
			return this.GetCollectionPropertyConfiguration(propertyExpression, false);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002D920 File Offset: 0x0002BB20
		public void HasDynamicProperties(Expression<Func<TStructuralType, IDictionary<string, object>>> propertyExpression)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			this._configuration.AddDynamicPropertyDictionary(selectedProperty);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002D940 File Offset: 0x0002BB40
		public NavigationPropertyConfiguration HasMany<TTargetEntity>(Expression<Func<TStructuralType, IEnumerable<TTargetEntity>>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateNavigationProperty(navigationPropertyExpression, EdmMultiplicity.Many);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002D94A File Offset: 0x0002BB4A
		public NavigationPropertyConfiguration HasOptional<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateNavigationProperty(navigationPropertyExpression, EdmMultiplicity.ZeroOrOne);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002D954 File Offset: 0x0002BB54
		public NavigationPropertyConfiguration HasOptional<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.ZeroOrOne, null);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002D960 File Offset: 0x0002BB60
		public NavigationPropertyConfiguration HasOptional<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression, Expression<Func<TTargetEntity, IEnumerable<TStructuralType>>> partnerExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.ZeroOrOne, partnerExpression);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002D96C File Offset: 0x0002BB6C
		public NavigationPropertyConfiguration HasOptional<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression, Expression<Func<TTargetEntity, TStructuralType>> partnerExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.ZeroOrOne, partnerExpression);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002D978 File Offset: 0x0002BB78
		public NavigationPropertyConfiguration HasRequired<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateNavigationProperty(navigationPropertyExpression, EdmMultiplicity.One);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002D982 File Offset: 0x0002BB82
		public NavigationPropertyConfiguration HasRequired<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression, Expression<Func<TTargetEntity, IEnumerable<TStructuralType>>> partnerExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.One, partnerExpression);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002D98E File Offset: 0x0002BB8E
		public NavigationPropertyConfiguration HasRequired<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.One, null);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002D99A File Offset: 0x0002BB9A
		public NavigationPropertyConfiguration HasRequired<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression, Expression<Func<TTargetEntity, TStructuralType>> partnerExpression) where TTargetEntity : class
		{
			return this.HasNavigationProperty<TTargetEntity>(navigationPropertyExpression, referentialConstraintExpression, EdmMultiplicity.One, partnerExpression);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002D9A8 File Offset: 0x0002BBA8
		private NavigationPropertyConfiguration HasNavigationProperty<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression, Expression<Func<TStructuralType, TTargetEntity, bool>> referentialConstraintExpression, EdmMultiplicity multiplicity, Expression partnerProperty) where TTargetEntity : class
		{
			NavigationPropertyConfiguration orCreateNavigationProperty = this.GetOrCreateNavigationProperty(navigationPropertyExpression, multiplicity);
			foreach (KeyValuePair<PropertyInfo, PropertyInfo> keyValuePair in PropertyPairSelectorVisitor.GetSelectedProperty(referentialConstraintExpression))
			{
				orCreateNavigationProperty.HasConstraint(keyValuePair);
			}
			if (partnerProperty != null)
			{
				PropertyInfo partnerPropertyInfo = PropertySelectorVisitor.GetSelectedProperty(partnerProperty);
				if (typeof(IEnumerable).IsAssignableFrom(partnerPropertyInfo.PropertyType))
				{
					this._configuration.ModelBuilder.EntityType<TTargetEntity>().HasMany<TStructuralType>((Expression<Func<TTargetEntity, IEnumerable<TStructuralType>>>)partnerProperty);
				}
				else
				{
					this._configuration.ModelBuilder.EntityType<TTargetEntity>().HasRequired<TStructuralType>((Expression<Func<TTargetEntity, TStructuralType>>)partnerProperty);
				}
				NavigationPropertyConfiguration navigationPropertyConfiguration = this._configuration.ModelBuilder.EntityType<TTargetEntity>().Properties.First((PropertyConfiguration p) => p.Name == partnerPropertyInfo.Name) as NavigationPropertyConfiguration;
				orCreateNavigationProperty.Partner = navigationPropertyConfiguration;
			}
			return orCreateNavigationProperty;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002DAA8 File Offset: 0x0002BCA8
		public NavigationPropertyConfiguration ContainsMany<TTargetEntity>(Expression<Func<TStructuralType, IEnumerable<TTargetEntity>>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateContainedNavigationProperty(navigationPropertyExpression, EdmMultiplicity.Many);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002DAB2 File Offset: 0x0002BCB2
		public NavigationPropertyConfiguration ContainsOptional<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateContainedNavigationProperty(navigationPropertyExpression, EdmMultiplicity.ZeroOrOne);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002DABC File Offset: 0x0002BCBC
		public NavigationPropertyConfiguration ContainsRequired<TTargetEntity>(Expression<Func<TStructuralType, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
		{
			return this.GetOrCreateContainedNavigationProperty(navigationPropertyExpression, EdmMultiplicity.One);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002DAC6 File Offset: 0x0002BCC6
		public StructuralTypeConfiguration<TStructuralType> Count()
		{
			this._configuration.QueryConfiguration.SetCount(true);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002DAE6 File Offset: 0x0002BCE6
		public StructuralTypeConfiguration<TStructuralType> Count(QueryOptionSetting setting)
		{
			this._configuration.QueryConfiguration.SetCount(setting == QueryOptionSetting.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002DB09 File Offset: 0x0002BD09
		public StructuralTypeConfiguration<TStructuralType> OrderBy(QueryOptionSetting setting, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetOrderBy(properties, setting == QueryOptionSetting.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002DB2D File Offset: 0x0002BD2D
		public StructuralTypeConfiguration<TStructuralType> OrderBy(params string[] properties)
		{
			this._configuration.QueryConfiguration.SetOrderBy(properties, true);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002DB4E File Offset: 0x0002BD4E
		public StructuralTypeConfiguration<TStructuralType> OrderBy(QueryOptionSetting setting)
		{
			this._configuration.QueryConfiguration.SetOrderBy(null, setting == QueryOptionSetting.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002DB72 File Offset: 0x0002BD72
		public StructuralTypeConfiguration<TStructuralType> OrderBy()
		{
			this._configuration.QueryConfiguration.SetOrderBy(null, true);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002DB93 File Offset: 0x0002BD93
		public StructuralTypeConfiguration<TStructuralType> Filter(QueryOptionSetting setting, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetFilter(properties, setting == QueryOptionSetting.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002DBB7 File Offset: 0x0002BDB7
		public StructuralTypeConfiguration<TStructuralType> Filter(params string[] properties)
		{
			this._configuration.QueryConfiguration.SetFilter(properties, true);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		public StructuralTypeConfiguration<TStructuralType> Filter(QueryOptionSetting setting)
		{
			this._configuration.QueryConfiguration.SetFilter(null, setting == QueryOptionSetting.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002DBFC File Offset: 0x0002BDFC
		public StructuralTypeConfiguration<TStructuralType> Filter()
		{
			this._configuration.QueryConfiguration.SetFilter(null, true);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002DC1D File Offset: 0x0002BE1D
		public StructuralTypeConfiguration<TStructuralType> Select(SelectExpandType selectType, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetSelect(properties, selectType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002DC3E File Offset: 0x0002BE3E
		public StructuralTypeConfiguration<TStructuralType> Select(params string[] properties)
		{
			this._configuration.QueryConfiguration.SetSelect(properties, SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002DC5F File Offset: 0x0002BE5F
		public StructuralTypeConfiguration<TStructuralType> Select(SelectExpandType selectType)
		{
			this._configuration.QueryConfiguration.SetSelect(null, selectType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002DC80 File Offset: 0x0002BE80
		public StructuralTypeConfiguration<TStructuralType> Select()
		{
			this._configuration.QueryConfiguration.SetSelect(null, SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002DCA1 File Offset: 0x0002BEA1
		public StructuralTypeConfiguration<TStructuralType> Page(int? maxTopValue, int? pageSizeValue)
		{
			this._configuration.QueryConfiguration.SetMaxTop(maxTopValue);
			this._configuration.QueryConfiguration.SetPageSize(pageSizeValue);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002DCD4 File Offset: 0x0002BED4
		public StructuralTypeConfiguration<TStructuralType> Page()
		{
			this._configuration.QueryConfiguration.SetMaxTop(null);
			this._configuration.QueryConfiguration.SetPageSize(null);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002DD20 File Offset: 0x0002BF20
		public StructuralTypeConfiguration<TStructuralType> Expand(int maxDepth, SelectExpandType expandType, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetExpand(properties, new int?(maxDepth), expandType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002DD48 File Offset: 0x0002BF48
		public StructuralTypeConfiguration<TStructuralType> Expand(params string[] properties)
		{
			this._configuration.QueryConfiguration.SetExpand(properties, null, SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002DD7D File Offset: 0x0002BF7D
		public StructuralTypeConfiguration<TStructuralType> Expand(int maxDepth, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetExpand(properties, new int?(maxDepth), SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
		public StructuralTypeConfiguration<TStructuralType> Expand(SelectExpandType expandType, params string[] properties)
		{
			this._configuration.QueryConfiguration.SetExpand(properties, null, expandType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002DDD9 File Offset: 0x0002BFD9
		public StructuralTypeConfiguration<TStructuralType> Expand(SelectExpandType expandType, int maxDepth)
		{
			this._configuration.QueryConfiguration.SetExpand(null, new int?(maxDepth), expandType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002DE00 File Offset: 0x0002C000
		public StructuralTypeConfiguration<TStructuralType> Expand(int maxDepth)
		{
			this._configuration.QueryConfiguration.SetExpand(null, new int?(maxDepth), SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002DE28 File Offset: 0x0002C028
		public StructuralTypeConfiguration<TStructuralType> Expand(SelectExpandType expandType)
		{
			this._configuration.QueryConfiguration.SetExpand(null, null, expandType);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002DE60 File Offset: 0x0002C060
		public StructuralTypeConfiguration<TStructuralType> Expand()
		{
			this._configuration.QueryConfiguration.SetExpand(null, null, SelectExpandType.Allowed);
			this._configuration.AddedExplicitly = true;
			return this;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002DE98 File Offset: 0x0002C098
		internal NavigationPropertyConfiguration GetOrCreateNavigationProperty(Expression navigationPropertyExpression, EdmMultiplicity multiplicity)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(navigationPropertyExpression);
			return this._configuration.AddNavigationProperty(selectedProperty, multiplicity);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002DEBC File Offset: 0x0002C0BC
		internal NavigationPropertyConfiguration GetOrCreateContainedNavigationProperty(Expression navigationPropertyExpression, EdmMultiplicity multiplicity)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(navigationPropertyExpression);
			return this._configuration.AddContainedNavigationProperty(selectedProperty, multiplicity);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
		private PrimitivePropertyConfiguration GetPrimitivePropertyConfiguration(Expression propertyExpression, bool optional)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			PrimitivePropertyConfiguration primitivePropertyConfiguration = this._configuration.AddProperty(selectedProperty);
			if (optional)
			{
				primitivePropertyConfiguration.IsOptional();
			}
			return primitivePropertyConfiguration;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002DF0C File Offset: 0x0002C10C
		private EnumPropertyConfiguration GetEnumPropertyConfiguration(Expression propertyExpression, bool optional)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			EnumPropertyConfiguration enumPropertyConfiguration = this._configuration.AddEnumProperty(selectedProperty);
			if (optional)
			{
				enumPropertyConfiguration.IsOptional();
			}
			return enumPropertyConfiguration;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002DF38 File Offset: 0x0002C138
		private ComplexPropertyConfiguration GetComplexPropertyConfiguration(Expression propertyExpression, bool optional = false)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			ComplexPropertyConfiguration complexPropertyConfiguration = this._configuration.AddComplexProperty(selectedProperty);
			if (optional)
			{
				complexPropertyConfiguration.IsOptional();
			}
			else
			{
				complexPropertyConfiguration.IsRequired();
			}
			return complexPropertyConfiguration;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002DF70 File Offset: 0x0002C170
		private CollectionPropertyConfiguration GetCollectionPropertyConfiguration(Expression propertyExpression, bool optional = false)
		{
			PropertyInfo selectedProperty = PropertySelectorVisitor.GetSelectedProperty(propertyExpression);
			CollectionPropertyConfiguration collectionPropertyConfiguration = this._configuration.AddCollectionProperty(selectedProperty);
			if (optional)
			{
				collectionPropertyConfiguration.IsOptional();
			}
			else
			{
				collectionPropertyConfiguration.IsRequired();
			}
			return collectionPropertyConfiguration;
		}

		// Token: 0x040003A1 RID: 929
		private StructuralTypeConfiguration _configuration;
	}
}
