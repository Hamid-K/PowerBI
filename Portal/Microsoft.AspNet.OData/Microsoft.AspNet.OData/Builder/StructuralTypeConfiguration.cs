using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000140 RID: 320
	public abstract class StructuralTypeConfiguration : IEdmTypeConfiguration
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0002CB39 File Offset: 0x0002AD39
		protected StructuralTypeConfiguration()
		{
			this.ExplicitProperties = new Dictionary<PropertyInfo, PropertyConfiguration>();
			this.RemovedProperties = new List<PropertyInfo>();
			this.QueryConfiguration = new QueryConfiguration();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002CB64 File Offset: 0x0002AD64
		protected StructuralTypeConfiguration(ODataModelBuilder modelBuilder, Type clrType)
			: this()
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			this.ClrType = clrType;
			this.ModelBuilder = modelBuilder;
			this._name = clrType.EdmName();
			this._namespace = (modelBuilder.HasAssignedNamespace ? modelBuilder.Namespace : (clrType.Namespace ?? modelBuilder.Namespace));
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B83 RID: 2947
		public abstract EdmTypeKind Kind { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002CBD9 File Offset: 0x0002ADD9
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0002CBE1 File Offset: 0x0002ADE1
		public virtual Type ClrType { get; private set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002CBEA File Offset: 0x0002ADEA
		public virtual string FullName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002CC02 File Offset: 0x0002AE02
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0002CC0A File Offset: 0x0002AE0A
		public virtual string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._namespace = value;
				this.AddedExplicitly = true;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002CC23 File Offset: 0x0002AE23
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0002CC2B File Offset: 0x0002AE2B
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._name = value;
				this.AddedExplicitly = true;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002CC44 File Offset: 0x0002AE44
		public bool IsOpen
		{
			get
			{
				return this._dynamicPropertyDictionary != null;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002CC52 File Offset: 0x0002AE52
		public PropertyInfo DynamicPropertyDictionary
		{
			get
			{
				return this._dynamicPropertyDictionary;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002CC5A File Offset: 0x0002AE5A
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0002CC62 File Offset: 0x0002AE62
		public virtual bool? IsAbstract { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002CC6B File Offset: 0x0002AE6B
		public virtual bool BaseTypeConfigured
		{
			get
			{
				return this._baseTypeConfigured;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002CC73 File Offset: 0x0002AE73
		public IEnumerable<PropertyConfiguration> Properties
		{
			get
			{
				return this.ExplicitProperties.Values;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002CC80 File Offset: 0x0002AE80
		public ReadOnlyCollection<PropertyInfo> IgnoredProperties
		{
			get
			{
				return new ReadOnlyCollection<PropertyInfo>(this.RemovedProperties);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002CC8D File Offset: 0x0002AE8D
		public virtual IEnumerable<NavigationPropertyConfiguration> NavigationProperties
		{
			get
			{
				return this.ExplicitProperties.Values.OfType<NavigationPropertyConfiguration>();
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002CC9F File Offset: 0x0002AE9F
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0002CCA7 File Offset: 0x0002AEA7
		public QueryConfiguration QueryConfiguration { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002CCB0 File Offset: 0x0002AEB0
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		public bool AddedExplicitly { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002CCC1 File Offset: 0x0002AEC1
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0002CCC9 File Offset: 0x0002AEC9
		public virtual ODataModelBuilder ModelBuilder { get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002CCD2 File Offset: 0x0002AED2
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0002CCDA File Offset: 0x0002AEDA
		protected internal IList<PropertyInfo> RemovedProperties { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002CCE3 File Offset: 0x0002AEE3
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0002CCEB File Offset: 0x0002AEEB
		protected internal IDictionary<PropertyInfo, PropertyConfiguration> ExplicitProperties { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002CCF4 File Offset: 0x0002AEF4
		protected internal virtual StructuralTypeConfiguration BaseTypeInternal
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		internal virtual void AbstractImpl()
		{
			this.IsAbstract = new bool?(true);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002CD0A File Offset: 0x0002AF0A
		internal virtual void DerivesFromNothingImpl()
		{
			this._baseType = null;
			this._baseTypeConfigured = true;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002CD1C File Offset: 0x0002AF1C
		internal virtual void DerivesFromImpl(StructuralTypeConfiguration baseType)
		{
			if (baseType == null)
			{
				throw Error.ArgumentNull("baseType");
			}
			this._baseType = baseType;
			this._baseTypeConfigured = true;
			if (!baseType.ClrType.IsAssignableFrom(this.ClrType) || baseType.ClrType == this.ClrType)
			{
				throw Error.Argument("baseType", SRResources.TypeDoesNotInheritFromBaseType, new object[]
				{
					this.ClrType.FullName,
					baseType.ClrType.FullName
				});
			}
			foreach (PropertyConfiguration propertyConfiguration in this.Properties)
			{
				this.ValidatePropertyNotAlreadyDefinedInBaseTypes(propertyConfiguration.PropertyInfo);
			}
			foreach (PropertyConfiguration propertyConfiguration2 in this.DerivedProperties())
			{
				this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(propertyConfiguration2.PropertyInfo);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002CE24 File Offset: 0x0002B024
		public virtual PrimitivePropertyConfiguration AddProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!TypeHelper.GetReflectedType(propertyInfo).IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					propertyInfo.Name,
					this.ClrType.FullName
				});
			}
			this.ValidatePropertyNotAlreadyDefinedInBaseTypes(propertyInfo);
			this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(propertyInfo);
			if (this.RemovedProperties.Any((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)))
			{
				this.RemovedProperties.Remove(this.RemovedProperties.First((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)));
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = this.ValidatePropertyNotAlreadyDefinedOtherTypes<PrimitivePropertyConfiguration>(propertyInfo, SRResources.MustBePrimitiveProperty);
			if (primitivePropertyConfiguration == null)
			{
				primitivePropertyConfiguration = new PrimitivePropertyConfiguration(propertyInfo, this);
				IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(propertyInfo.PropertyType);
				if (edmPrimitiveTypeOrNull != null)
				{
					if (edmPrimitiveTypeOrNull.PrimitiveKind == EdmPrimitiveTypeKind.Decimal)
					{
						primitivePropertyConfiguration = new DecimalPropertyConfiguration(propertyInfo, this);
					}
					else if (EdmLibHelpers.HasLength(edmPrimitiveTypeOrNull.PrimitiveKind))
					{
						primitivePropertyConfiguration = new LengthPropertyConfiguration(propertyInfo, this);
					}
					else if (EdmLibHelpers.HasPrecision(edmPrimitiveTypeOrNull.PrimitiveKind))
					{
						primitivePropertyConfiguration = new PrecisionPropertyConfiguration(propertyInfo, this);
					}
				}
				this.ExplicitProperties[propertyInfo] = primitivePropertyConfiguration;
			}
			return primitivePropertyConfiguration;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002CF90 File Offset: 0x0002B190
		public virtual EnumPropertyConfiguration AddEnumProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!TypeHelper.GetReflectedType(propertyInfo).IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					propertyInfo.Name,
					this.ClrType.FullName
				});
			}
			if (!TypeHelper.IsEnum(propertyInfo.PropertyType))
			{
				throw Error.Argument("propertyInfo", SRResources.MustBeEnumProperty, new object[]
				{
					propertyInfo.Name,
					this.ClrType.FullName
				});
			}
			this.ValidatePropertyNotAlreadyDefinedInBaseTypes(propertyInfo);
			this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(propertyInfo);
			if (this.RemovedProperties.Any((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)))
			{
				this.RemovedProperties.Remove(this.RemovedProperties.First((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)));
			}
			EnumPropertyConfiguration enumPropertyConfiguration = this.ValidatePropertyNotAlreadyDefinedOtherTypes<EnumPropertyConfiguration>(propertyInfo, SRResources.MustBeEnumProperty);
			if (enumPropertyConfiguration == null)
			{
				enumPropertyConfiguration = new EnumPropertyConfiguration(propertyInfo, this);
				this.ExplicitProperties[propertyInfo] = enumPropertyConfiguration;
			}
			return enumPropertyConfiguration;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		public virtual ComplexPropertyConfiguration AddComplexProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!TypeHelper.GetReflectedType(propertyInfo).IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					propertyInfo.Name,
					this.ClrType.FullName
				});
			}
			this.ValidatePropertyNotAlreadyDefinedInBaseTypes(propertyInfo);
			this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(propertyInfo);
			if (this.RemovedProperties.Any((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)))
			{
				this.RemovedProperties.Remove(this.RemovedProperties.First((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)));
			}
			ComplexPropertyConfiguration complexPropertyConfiguration = this.ValidatePropertyNotAlreadyDefinedOtherTypes<ComplexPropertyConfiguration>(propertyInfo, SRResources.MustBeComplexProperty);
			if (complexPropertyConfiguration == null)
			{
				complexPropertyConfiguration = new ComplexPropertyConfiguration(propertyInfo, this);
				this.ExplicitProperties[propertyInfo] = complexPropertyConfiguration;
				this.ModelBuilder.AddComplexType(propertyInfo.PropertyType);
			}
			return complexPropertyConfiguration;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002D1FC File Offset: 0x0002B3FC
		public virtual CollectionPropertyConfiguration AddCollectionProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!propertyInfo.DeclaringType.IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[0]);
			}
			this.ValidatePropertyNotAlreadyDefinedInBaseTypes(propertyInfo);
			this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(propertyInfo);
			if (this.RemovedProperties.Any((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)))
			{
				this.RemovedProperties.Remove(this.RemovedProperties.First((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)));
			}
			CollectionPropertyConfiguration collectionPropertyConfiguration = this.ValidatePropertyNotAlreadyDefinedOtherTypes<CollectionPropertyConfiguration>(propertyInfo, SRResources.MustBeCollectionProperty);
			if (collectionPropertyConfiguration == null)
			{
				collectionPropertyConfiguration = new CollectionPropertyConfiguration(propertyInfo, this);
				this.ExplicitProperties[propertyInfo] = collectionPropertyConfiguration;
				if (EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(collectionPropertyConfiguration.ElementType) == null && !TypeHelper.IsEnum(collectionPropertyConfiguration.ElementType))
				{
					this.ModelBuilder.AddComplexType(collectionPropertyConfiguration.ElementType);
				}
			}
			return collectionPropertyConfiguration;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002D314 File Offset: 0x0002B514
		public virtual void AddDynamicPropertyDictionary(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!typeof(IDictionary<string, object>).IsAssignableFrom(propertyInfo.PropertyType))
			{
				throw Error.Argument("propertyInfo", SRResources.ArgumentMustBeOfType, new object[] { "IDictionary<string, object>" });
			}
			if (!propertyInfo.DeclaringType.IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[0]);
			}
			if (this.IgnoredProperties.Contains(propertyInfo))
			{
				this.RemovedProperties.Remove(propertyInfo);
			}
			if (this._dynamicPropertyDictionary != null)
			{
				throw Error.Argument("propertyInfo", SRResources.MoreThanOneDynamicPropertyContainerFound, new object[] { this.ClrType.Name });
			}
			this._dynamicPropertyDictionary = propertyInfo;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002D3E8 File Offset: 0x0002B5E8
		public virtual void RemoveProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!TypeHelper.GetReflectedType(propertyInfo).IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("propertyInfo", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					propertyInfo.Name,
					this.ClrType.FullName
				});
			}
			if (this.ExplicitProperties.Keys.Any((PropertyInfo key) => key.Name.Equals(propertyInfo.Name)))
			{
				this.ExplicitProperties.Remove(this.ExplicitProperties.Keys.First((PropertyInfo key) => key.Name.Equals(propertyInfo.Name)));
			}
			if (!this.RemovedProperties.Any((PropertyInfo prop) => prop.Name.Equals(propertyInfo.Name)))
			{
				this.RemovedProperties.Add(propertyInfo);
			}
			if (this._dynamicPropertyDictionary == propertyInfo)
			{
				this._dynamicPropertyDictionary = null;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002D4EF File Offset: 0x0002B6EF
		public virtual NavigationPropertyConfiguration AddNavigationProperty(PropertyInfo navigationProperty, EdmMultiplicity multiplicity)
		{
			return this.AddNavigationProperty(navigationProperty, multiplicity, false);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002D4FA File Offset: 0x0002B6FA
		public virtual NavigationPropertyConfiguration AddContainedNavigationProperty(PropertyInfo navigationProperty, EdmMultiplicity multiplicity)
		{
			return this.AddNavigationProperty(navigationProperty, multiplicity, true);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002D508 File Offset: 0x0002B708
		private NavigationPropertyConfiguration AddNavigationProperty(PropertyInfo navigationProperty, EdmMultiplicity multiplicity, bool containsTarget)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			if (!TypeHelper.GetReflectedType(navigationProperty).IsAssignableFrom(this.ClrType))
			{
				throw Error.Argument("navigationProperty", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					navigationProperty.Name,
					this.ClrType.FullName
				});
			}
			this.ValidatePropertyNotAlreadyDefinedInBaseTypes(navigationProperty);
			this.ValidatePropertyNotAlreadyDefinedInDerivedTypes(navigationProperty);
			NavigationPropertyConfiguration navigationPropertyConfiguration;
			if (this.ExplicitProperties.ContainsKey(navigationProperty))
			{
				PropertyConfiguration propertyConfiguration = this.ExplicitProperties[navigationProperty];
				if (propertyConfiguration.Kind != PropertyKind.Navigation)
				{
					throw Error.Argument("navigationProperty", SRResources.MustBeNavigationProperty, new object[]
					{
						navigationProperty.Name,
						this.ClrType.FullName
					});
				}
				navigationPropertyConfiguration = propertyConfiguration as NavigationPropertyConfiguration;
				if (navigationPropertyConfiguration.Multiplicity != multiplicity)
				{
					throw Error.Argument("navigationProperty", SRResources.MustHaveMatchingMultiplicity, new object[] { navigationProperty.Name, multiplicity });
				}
			}
			else
			{
				navigationPropertyConfiguration = new NavigationPropertyConfiguration(navigationProperty, multiplicity, this);
				if (containsTarget)
				{
					navigationPropertyConfiguration = navigationPropertyConfiguration.Contained();
				}
				this.ExplicitProperties[navigationProperty] = navigationPropertyConfiguration;
				this.ModelBuilder.AddEntityType(navigationPropertyConfiguration.RelatedClrType);
			}
			return navigationPropertyConfiguration;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002D634 File Offset: 0x0002B834
		internal T ValidatePropertyNotAlreadyDefinedOtherTypes<T>(PropertyInfo propertyInfo, string typeErrorMessage) where T : class
		{
			T t = default(T);
			PropertyInfo propertyInfo2 = this.ExplicitProperties.Keys.FirstOrDefault((PropertyInfo key) => key.Name.Equals(propertyInfo.Name));
			if (propertyInfo2 != null)
			{
				t = this.ExplicitProperties[propertyInfo2] as T;
				if (t == null)
				{
					throw Error.Argument("propertyInfo", typeErrorMessage, new object[]
					{
						propertyInfo.Name,
						this.ClrType.FullName
					});
				}
			}
			return t;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002D6CC File Offset: 0x0002B8CC
		internal void ValidatePropertyNotAlreadyDefinedInBaseTypes(PropertyInfo propertyInfo)
		{
			PropertyConfiguration propertyConfiguration = this.DerivedProperties().FirstOrDefault((PropertyConfiguration p) => p.Name == propertyInfo.Name);
			if (propertyConfiguration != null)
			{
				throw Error.Argument("propertyInfo", SRResources.CannotRedefineBaseTypeProperty, new object[]
				{
					propertyInfo.Name,
					TypeHelper.GetReflectedType(propertyConfiguration.PropertyInfo).FullName
				});
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002D738 File Offset: 0x0002B938
		internal void ValidatePropertyNotAlreadyDefinedInDerivedTypes(PropertyInfo propertyInfo)
		{
			Func<PropertyConfiguration, bool> <>9__0;
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in this.ModelBuilder.DerivedTypes(this))
			{
				IEnumerable<PropertyConfiguration> properties = structuralTypeConfiguration.Properties;
				Func<PropertyConfiguration, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (PropertyConfiguration p) => p.Name == propertyInfo.Name);
				}
				if (properties.FirstOrDefault(func) != null)
				{
					throw Error.Argument("propertyInfo", SRResources.PropertyAlreadyDefinedInDerivedType, new object[] { propertyInfo.Name, this.FullName, structuralTypeConfiguration.FullName });
				}
			}
		}

		// Token: 0x04000395 RID: 917
		private string _namespace;

		// Token: 0x04000396 RID: 918
		private string _name;

		// Token: 0x04000397 RID: 919
		private PropertyInfo _dynamicPropertyDictionary;

		// Token: 0x04000398 RID: 920
		private StructuralTypeConfiguration _baseType;

		// Token: 0x04000399 RID: 921
		private bool _baseTypeConfigured;
	}
}
