using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Builder.Conventions;
using Microsoft.AspNet.OData.Builder.Conventions.Attributes;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x020000FB RID: 251
	public class ODataConventionModelBuilder : ODataModelBuilder
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x000238FA File Offset: 0x00021AFA
		public ODataConventionModelBuilder()
			: this(WebApiAssembliesResolver.Default)
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00023907 File Offset: 0x00021B07
		public ODataConventionModelBuilder(HttpConfiguration configuration)
			: this(configuration, false)
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00023914 File Offset: 0x00021B14
		public ODataConventionModelBuilder(HttpConfiguration configuration, bool isQueryCompositionMode)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			IWebApiAssembliesResolver webApiAssembliesResolver = new WebApiAssembliesResolver(ServicesExtensions.GetAssembliesResolver(configuration.Services));
			this.Initialize(webApiAssembliesResolver, isQueryCompositionMode);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002394E File Offset: 0x00021B4E
		internal ODataConventionModelBuilder(IWebApiAssembliesResolver resolver)
			: this(resolver, false)
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00023958 File Offset: 0x00021B58
		internal ODataConventionModelBuilder(IWebApiAssembliesResolver resolver, bool isQueryCompositionMode)
		{
			if (resolver == null)
			{
				throw Error.ArgumentNull("resolver");
			}
			this.Initialize(resolver, isQueryCompositionMode);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00023976 File Offset: 0x00021B76
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0002397E File Offset: 0x00021B7E
		public bool ModelAliasingEnabled { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00023987 File Offset: 0x00021B87
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0002398F File Offset: 0x00021B8F
		public Action<ODataConventionModelBuilder> OnModelCreating { get; set; }

		// Token: 0x060008BE RID: 2238 RVA: 0x00023998 File Offset: 0x00021B98
		internal void Initialize(IWebApiAssembliesResolver assembliesResolver, bool isQueryCompositionMode)
		{
			this._isQueryCompositionMode = isQueryCompositionMode;
			this._configuredNavigationSources = new HashSet<NavigationSourceConfiguration>();
			this._mappedTypes = new HashSet<StructuralTypeConfiguration>();
			this._ignoredTypes = new HashSet<Type>();
			this.ModelAliasingEnabled = true;
			this._allTypesWithDerivedTypeMapping = new Lazy<IDictionary<Type, List<Type>>>(() => ODataConventionModelBuilder.BuildDerivedTypesMapping(assembliesResolver), false);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000239F9 File Offset: 0x00021BF9
		public ODataConventionModelBuilder Ignore<T>()
		{
			this._ignoredTypes.Add(typeof(T));
			return this;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00023A14 File Offset: 0x00021C14
		public ODataConventionModelBuilder Ignore(params Type[] types)
		{
			foreach (Type type in types)
			{
				this._ignoredTypes.Add(type);
			}
			return this;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00023A44 File Offset: 0x00021C44
		public override EntityTypeConfiguration AddEntityType(Type type)
		{
			EntityTypeConfiguration entityTypeConfiguration = base.AddEntityType(type);
			if (this._isModelBeingBuilt)
			{
				this.MapType(entityTypeConfiguration);
			}
			return entityTypeConfiguration;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00023A6C File Offset: 0x00021C6C
		public override ComplexTypeConfiguration AddComplexType(Type type)
		{
			ComplexTypeConfiguration complexTypeConfiguration = base.AddComplexType(type);
			if (this._isModelBeingBuilt)
			{
				this.MapType(complexTypeConfiguration);
			}
			return complexTypeConfiguration;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00023A94 File Offset: 0x00021C94
		public override EntitySetConfiguration AddEntitySet(string name, EntityTypeConfiguration entityType)
		{
			EntitySetConfiguration entitySetConfiguration = base.AddEntitySet(name, entityType);
			if (this._isModelBeingBuilt)
			{
				this.ApplyNavigationSourceConventions(entitySetConfiguration);
			}
			return entitySetConfiguration;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00023ABC File Offset: 0x00021CBC
		public override SingletonConfiguration AddSingleton(string name, EntityTypeConfiguration entityType)
		{
			SingletonConfiguration singletonConfiguration = base.AddSingleton(name, entityType);
			if (this._isModelBeingBuilt)
			{
				this.ApplyNavigationSourceConventions(singletonConfiguration);
			}
			return singletonConfiguration;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00023AE4 File Offset: 0x00021CE4
		public override EnumTypeConfiguration AddEnumType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (!TypeHelper.IsEnum(type))
			{
				throw Error.Argument("type", SRResources.TypeCannotBeEnum, new object[] { type.FullName });
			}
			EnumTypeConfiguration enumTypeConfiguration = this.EnumTypes.SingleOrDefault((EnumTypeConfiguration e) => e.ClrType == type);
			if (enumTypeConfiguration == null)
			{
				enumTypeConfiguration = base.AddEnumType(type);
				using (IEnumerator enumerator = Enum.GetValues(type).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object member = enumerator.Current;
						bool flag = enumTypeConfiguration.Members.Any((EnumMemberConfiguration m) => m.Name.Equals(member.ToString()));
						enumTypeConfiguration.AddMember((Enum)member).AddedExplicitly = flag;
					}
				}
				this.ApplyEnumTypeConventions(enumTypeConfiguration);
			}
			return enumTypeConfiguration;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00023C00 File Offset: 0x00021E00
		public override IEdmModel GetEdmModel()
		{
			if (this._isModelBeingBuilt)
			{
				throw Error.NotSupported(SRResources.GetEdmModelCalledMoreThanOnce, new object[0]);
			}
			this._explicitlyAddedTypes = new List<StructuralTypeConfiguration>(this.StructuralTypes);
			this._isModelBeingBuilt = true;
			this.MapTypes();
			this.DiscoverInheritanceRelationships();
			if (!this._isQueryCompositionMode)
			{
				this.RediscoverComplexTypes();
			}
			this.PruneUnreachableTypes();
			foreach (NavigationSourceConfiguration navigationSourceConfiguration in ((IEnumerable<NavigationSourceConfiguration>)new List<NavigationSourceConfiguration>(this.NavigationSources)))
			{
				this.ApplyNavigationSourceConventions(navigationSourceConfiguration);
			}
			foreach (OperationConfiguration operationConfiguration in this.Operations)
			{
				this.ApplyOperationConventions(operationConfiguration);
			}
			if (this.OnModelCreating != null)
			{
				this.OnModelCreating(this);
			}
			return base.GetEdmModel();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00023CFC File Offset: 0x00021EFC
		internal bool IsIgnoredType(Type type)
		{
			return this._ignoredTypes.Contains(type);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00023D0C File Offset: 0x00021F0C
		internal void DiscoverInheritanceRelationships()
		{
			Dictionary<Type, EntityTypeConfiguration> dictionary = this.StructuralTypes.OfType<EntityTypeConfiguration>().ToDictionary((EntityTypeConfiguration e) => e.ClrType);
			foreach (EntityTypeConfiguration entityTypeConfiguration in from e in this.StructuralTypes.OfType<EntityTypeConfiguration>()
				where !e.BaseTypeConfigured
				select e)
			{
				Type type = TypeHelper.GetBaseType(entityTypeConfiguration.ClrType);
				while (type != null)
				{
					EntityTypeConfiguration entityTypeConfiguration2;
					if (dictionary.TryGetValue(type, out entityTypeConfiguration2))
					{
						this.RemoveBaseTypeProperties(entityTypeConfiguration, entityTypeConfiguration2);
						if (this._isQueryCompositionMode)
						{
							foreach (PrimitivePropertyConfiguration primitivePropertyConfiguration in entityTypeConfiguration.Keys.ToArray<PrimitivePropertyConfiguration>())
							{
								entityTypeConfiguration.RemoveKey(primitivePropertyConfiguration);
							}
							foreach (EnumPropertyConfiguration enumPropertyConfiguration in entityTypeConfiguration.EnumKeys.ToArray<EnumPropertyConfiguration>())
							{
								entityTypeConfiguration.RemoveKey(enumPropertyConfiguration);
							}
						}
						entityTypeConfiguration.DerivesFrom(entityTypeConfiguration2);
						break;
					}
					type = TypeHelper.GetBaseType(type);
				}
			}
			Dictionary<Type, ComplexTypeConfiguration> dictionary2 = this.StructuralTypes.OfType<ComplexTypeConfiguration>().ToDictionary((ComplexTypeConfiguration e) => e.ClrType);
			foreach (ComplexTypeConfiguration complexTypeConfiguration in from e in this.StructuralTypes.OfType<ComplexTypeConfiguration>()
				where !e.BaseTypeConfigured
				select e)
			{
				Type type2 = TypeHelper.GetBaseType(complexTypeConfiguration.ClrType);
				while (type2 != null)
				{
					ComplexTypeConfiguration complexTypeConfiguration2;
					if (dictionary2.TryGetValue(type2, out complexTypeConfiguration2))
					{
						this.RemoveBaseTypeProperties(complexTypeConfiguration, complexTypeConfiguration2);
						complexTypeConfiguration.DerivesFrom(complexTypeConfiguration2);
						break;
					}
					type2 = TypeHelper.GetBaseType(type2);
				}
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00023F38 File Offset: 0x00022138
		internal void RemoveBaseTypeProperties(StructuralTypeConfiguration derivedStructrualType, StructuralTypeConfiguration baseStructuralType)
		{
			IEnumerable<StructuralTypeConfiguration> enumerable = new StructuralTypeConfiguration[] { derivedStructrualType }.Concat(this.DerivedTypes(derivedStructrualType));
			using (IEnumerator<PropertyConfiguration> enumerator = baseStructuralType.Properties.Concat(baseStructuralType.DerivedProperties()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyConfiguration property = enumerator.Current;
					Func<PropertyConfiguration, bool> <>9__0;
					foreach (StructuralTypeConfiguration structuralTypeConfiguration in enumerable)
					{
						IEnumerable<PropertyConfiguration> properties = structuralTypeConfiguration.Properties;
						Func<PropertyConfiguration, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (PropertyConfiguration p) => p.PropertyInfo.Name == property.PropertyInfo.Name);
						}
						PropertyConfiguration propertyConfiguration = properties.SingleOrDefault(func);
						if (propertyConfiguration != null)
						{
							structuralTypeConfiguration.RemoveProperty(propertyConfiguration.PropertyInfo);
						}
					}
				}
			}
			using (IEnumerator<PropertyInfo> enumerator3 = baseStructuralType.IgnoredProperties().GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					PropertyInfo ignoredProperty = enumerator3.Current;
					Func<PropertyConfiguration, bool> <>9__1;
					foreach (StructuralTypeConfiguration structuralTypeConfiguration2 in enumerable)
					{
						IEnumerable<PropertyConfiguration> properties2 = structuralTypeConfiguration2.Properties;
						Func<PropertyConfiguration, bool> func2;
						if ((func2 = <>9__1) == null)
						{
							func2 = (<>9__1 = (PropertyConfiguration p) => p.PropertyInfo.Name == ignoredProperty.Name);
						}
						PropertyConfiguration propertyConfiguration2 = properties2.SingleOrDefault(func2);
						if (propertyConfiguration2 != null)
						{
							structuralTypeConfiguration2.RemoveProperty(propertyConfiguration2.PropertyInfo);
						}
					}
				}
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000240DC File Offset: 0x000222DC
		private void RediscoverComplexTypes()
		{
			EntityTypeConfiguration[] array = (from entity in this.StructuralTypes.Except(this._explicitlyAddedTypes).OfType<EntityTypeConfiguration>()
				where !entity.Keys().Any<PropertyConfiguration>()
				select entity).ToArray<EntityTypeConfiguration>();
			this.ReconfigureEntityTypesAsComplexType(array);
			this.DiscoverInheritanceRelationships();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00024138 File Offset: 0x00022338
		private void ReconfigureEntityTypesAsComplexType(EntityTypeConfiguration[] misconfiguredEntityTypes)
		{
			IList<EntityTypeConfiguration> list = (from entity in this.StructuralTypes.OfType<EntityTypeConfiguration>()
				where entity.Keys().Any<PropertyConfiguration>()
				select entity).Concat(this._explicitlyAddedTypes.OfType<EntityTypeConfiguration>()).Except(misconfiguredEntityTypes).ToList<EntityTypeConfiguration>();
			HashSet<EntityTypeConfiguration> hashSet = new HashSet<EntityTypeConfiguration>();
			for (int i = 0; i < misconfiguredEntityTypes.Length; i++)
			{
				EntityTypeConfiguration entityTypeConfiguration = misconfiguredEntityTypes[i];
				if (!hashSet.Contains(entityTypeConfiguration))
				{
					IEnumerable<EntityTypeConfiguration> basedTypes = entityTypeConfiguration.BaseTypes().OfType<EntityTypeConfiguration>();
					if (list.Any((EntityTypeConfiguration e) => basedTypes.Any((EntityTypeConfiguration a) => a.ClrType == e.ClrType)))
					{
						hashSet.Add(entityTypeConfiguration);
					}
					else
					{
						IList<EntityTypeConfiguration> list2 = this.DerivedTypes(entityTypeConfiguration).Concat(new EntityTypeConfiguration[] { entityTypeConfiguration }).OfType<EntityTypeConfiguration>()
							.ToList<EntityTypeConfiguration>();
						using (IEnumerator<EntityTypeConfiguration> enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								EntityTypeConfiguration subEnityType2 = enumerator.Current;
								if (list.Any((EntityTypeConfiguration e) => e.ClrType == subEnityType2.ClrType))
								{
									throw Error.InvalidOperation(SRResources.CannotReconfigEntityTypeAsComplexType, new object[]
									{
										entityTypeConfiguration.ClrType.FullName,
										subEnityType2.ClrType.FullName
									});
								}
								this.RemoveStructuralType(subEnityType2.ClrType);
							}
						}
						this.AddComplexType(entityTypeConfiguration.ClrType);
						using (IEnumerator<EntityTypeConfiguration> enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								EntityTypeConfiguration subEnityType = enumerator.Current;
								hashSet.Add(subEnityType);
								Func<NavigationPropertyConfiguration, bool> <>9__4;
								foreach (StructuralTypeConfiguration structuralTypeConfiguration in ((IEnumerable<StructuralTypeConfiguration>)this.StructuralTypes.ToList<StructuralTypeConfiguration>()))
								{
									IEnumerable<NavigationPropertyConfiguration> navigationProperties = structuralTypeConfiguration.NavigationProperties;
									Func<NavigationPropertyConfiguration, bool> func;
									if ((func = <>9__4) == null)
									{
										func = (<>9__4 = (NavigationPropertyConfiguration navigationProperty) => navigationProperty.RelatedClrType == subEnityType.ClrType);
									}
									foreach (NavigationPropertyConfiguration navigationPropertyConfiguration in navigationProperties.Where(func).ToArray<NavigationPropertyConfiguration>())
									{
										string name = navigationPropertyConfiguration.Name;
										structuralTypeConfiguration.RemoveProperty(navigationPropertyConfiguration.PropertyInfo);
										PropertyConfiguration propertyConfiguration;
										if (navigationPropertyConfiguration.Multiplicity == EdmMultiplicity.Many)
										{
											propertyConfiguration = structuralTypeConfiguration.AddCollectionProperty(navigationPropertyConfiguration.PropertyInfo);
										}
										else
										{
											propertyConfiguration = structuralTypeConfiguration.AddComplexProperty(navigationPropertyConfiguration.PropertyInfo);
										}
										propertyConfiguration.AddedExplicitly = false;
										this.ReapplyPropertyConvention(propertyConfiguration, structuralTypeConfiguration);
										propertyConfiguration.Name = name;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00024440 File Offset: 0x00022640
		private void MapTypes()
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in this._explicitlyAddedTypes)
			{
				this.MapType(structuralTypeConfiguration);
			}
			this.ApplyForeignKeyConventions();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00024494 File Offset: 0x00022694
		private void ApplyForeignKeyConventions()
		{
			ForeignKeyAttributeConvention foreignKeyAttributeConvention = new ForeignKeyAttributeConvention();
			ForeignKeyDiscoveryConvention foreignKeyDiscoveryConvention = new ForeignKeyDiscoveryConvention();
			ActionOnDeleteAttributeConvention actionOnDeleteAttributeConvention = new ActionOnDeleteAttributeConvention();
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.StructuralTypes.OfType<EntityTypeConfiguration>())
			{
				foreach (PropertyConfiguration propertyConfiguration in entityTypeConfiguration.Properties)
				{
					foreignKeyAttributeConvention.Apply(propertyConfiguration, entityTypeConfiguration, this);
					foreignKeyDiscoveryConvention.Apply(propertyConfiguration, entityTypeConfiguration, this);
					actionOnDeleteAttributeConvention.Apply(propertyConfiguration, entityTypeConfiguration, this);
				}
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00024550 File Offset: 0x00022750
		private void MapType(StructuralTypeConfiguration edmType)
		{
			if (!this._mappedTypes.Contains(edmType))
			{
				this._mappedTypes.Add(edmType);
				this.MapStructuralType(edmType);
				this.ApplyTypeAndPropertyConventions(edmType);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002457C File Offset: 0x0002277C
		private void MapStructuralType(StructuralTypeConfiguration structuralType)
		{
			using (IEnumerator<PropertyInfo> enumerator = ConventionsHelpers.GetProperties(structuralType, this._isQueryCompositionMode).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyInfo property = enumerator.Current;
					bool flag;
					IEdmTypeConfiguration edmTypeConfiguration;
					PropertyKind propertyType = this.GetPropertyType(property, out flag, out edmTypeConfiguration);
					if (propertyType == PropertyKind.Primitive || propertyType == PropertyKind.Complex || propertyType == PropertyKind.Enum)
					{
						this.MapStructuralProperty(structuralType, property, propertyType, flag);
					}
					else if (propertyType == PropertyKind.Dynamic)
					{
						structuralType.AddDynamicPropertyDictionary(property);
					}
					else if (structuralType.NavigationProperties.All((NavigationPropertyConfiguration p) => p.Name != property.Name))
					{
						NavigationPropertyConfiguration navigationPropertyConfiguration;
						if (!flag)
						{
							navigationPropertyConfiguration = structuralType.AddNavigationProperty(property, EdmMultiplicity.ZeroOrOne);
						}
						else
						{
							navigationPropertyConfiguration = structuralType.AddNavigationProperty(property, EdmMultiplicity.Many);
						}
						if (property.GetCustomAttribute<ContainedAttribute>() != null)
						{
							navigationPropertyConfiguration.Contained();
						}
						navigationPropertyConfiguration.AddedExplicitly = false;
					}
				}
			}
			this.MapDerivedTypes(structuralType);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00024680 File Offset: 0x00022880
		internal void MapDerivedTypes(StructuralTypeConfiguration structuralType)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			Queue<StructuralTypeConfiguration> queue = new Queue<StructuralTypeConfiguration>();
			queue.Enqueue(structuralType);
			while (queue.Count != 0)
			{
				StructuralTypeConfiguration structuralTypeConfiguration = queue.Dequeue();
				hashSet.Add(structuralTypeConfiguration.ClrType);
				List<Type> list;
				if (this._allTypesWithDerivedTypeMapping.Value.TryGetValue(structuralTypeConfiguration.ClrType, out list))
				{
					foreach (Type type in list)
					{
						if (!hashSet.Contains(type) && !this.IsIgnoredType(type))
						{
							StructuralTypeConfiguration structuralTypeConfiguration2;
							if (structuralTypeConfiguration.Kind == EdmTypeKind.Entity)
							{
								structuralTypeConfiguration2 = this.AddEntityType(type);
							}
							else
							{
								structuralTypeConfiguration2 = this.AddComplexType(type);
							}
							queue.Enqueue(structuralTypeConfiguration2);
						}
					}
				}
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00024758 File Offset: 0x00022958
		private void MapStructuralProperty(StructuralTypeConfiguration type, PropertyInfo property, PropertyKind propertyKind, bool isCollection)
		{
			bool flag = type.Properties.Any((PropertyConfiguration p) => p.PropertyInfo.Name == property.Name);
			PropertyConfiguration propertyConfiguration;
			if (!isCollection)
			{
				if (propertyKind == PropertyKind.Primitive)
				{
					propertyConfiguration = type.AddProperty(property);
				}
				else if (propertyKind == PropertyKind.Enum)
				{
					this.AddEnumType(TypeHelper.GetUnderlyingTypeOrSelf(property.PropertyType));
					propertyConfiguration = type.AddEnumProperty(property);
				}
				else
				{
					propertyConfiguration = type.AddComplexProperty(property);
				}
			}
			else
			{
				bool isQueryCompositionMode = this._isQueryCompositionMode;
				Type type2;
				if (property.PropertyType.IsGenericType())
				{
					Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(property.PropertyType.GetGenericArguments().First<Type>());
					if (TypeHelper.IsEnum(underlyingTypeOrSelf))
					{
						this.AddEnumType(underlyingTypeOrSelf);
					}
				}
				else if (TypeHelper.IsCollection(property.PropertyType, out type2))
				{
					Type underlyingTypeOrSelf2 = TypeHelper.GetUnderlyingTypeOrSelf(type2);
					if (TypeHelper.IsEnum(underlyingTypeOrSelf2))
					{
						this.AddEnumType(underlyingTypeOrSelf2);
					}
				}
				propertyConfiguration = type.AddCollectionProperty(property);
			}
			propertyConfiguration.AddedExplicitly = flag;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002486C File Offset: 0x00022A6C
		private PropertyKind GetPropertyType(PropertyInfo property, out bool isCollection, out IEdmTypeConfiguration mappedType)
		{
			if (typeof(IDictionary<string, object>).IsAssignableFrom(property.PropertyType))
			{
				mappedType = null;
				isCollection = false;
				return PropertyKind.Dynamic;
			}
			PropertyKind propertyKind;
			if (this.TryGetPropertyTypeKind(property.PropertyType, out mappedType, out propertyKind))
			{
				isCollection = false;
				return propertyKind;
			}
			Type type;
			if (!TypeHelper.IsCollection(property.PropertyType, out type))
			{
				isCollection = false;
				return PropertyKind.Navigation;
			}
			isCollection = true;
			if (this.TryGetPropertyTypeKind(type, out mappedType, out propertyKind))
			{
				return propertyKind;
			}
			return PropertyKind.Navigation;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000248D4 File Offset: 0x00022AD4
		private bool TryGetPropertyTypeKind(Type propertyType, out IEdmTypeConfiguration mappedType, out PropertyKind propertyKind)
		{
			if (EdmLibHelpers.GetEdmPrimitiveTypeOrNull(propertyType) != null)
			{
				mappedType = null;
				propertyKind = PropertyKind.Primitive;
				return true;
			}
			mappedType = this.GetStructuralTypeOrNull(propertyType);
			if (mappedType != null)
			{
				if (mappedType is ComplexTypeConfiguration)
				{
					propertyKind = PropertyKind.Complex;
				}
				else if (mappedType is EnumTypeConfiguration)
				{
					propertyKind = PropertyKind.Enum;
				}
				else
				{
					propertyKind = PropertyKind.Navigation;
				}
				return true;
			}
			Type type = TypeHelper.GetBaseType(propertyType);
			while (type != null && type != typeof(object))
			{
				IEdmTypeConfiguration structuralTypeOrNull = this.GetStructuralTypeOrNull(type);
				if (structuralTypeOrNull != null && structuralTypeOrNull is ComplexTypeConfiguration)
				{
					propertyKind = PropertyKind.Complex;
					return true;
				}
				type = TypeHelper.GetBaseType(type);
			}
			PropertyKind propertyKind2 = PropertyKind.Navigation;
			if (this.InferEdmTypeFromDerivedTypes(propertyType, ref propertyKind2))
			{
				if (propertyKind2 == PropertyKind.Complex)
				{
					this.ReconfigInferedEntityTypeAsComplexType(propertyType);
				}
				propertyKind = propertyKind2;
				return true;
			}
			if (TypeHelper.IsEnum(propertyType))
			{
				propertyKind = PropertyKind.Enum;
				return true;
			}
			propertyKind = PropertyKind.Navigation;
			return false;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00024990 File Offset: 0x00022B90
		internal void ReconfigInferedEntityTypeAsComplexType(Type propertyType)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			Queue<Type> queue = new Queue<Type>();
			queue.Enqueue(propertyType);
			IList<EntityTypeConfiguration> list = new List<EntityTypeConfiguration>();
			while (queue.Count != 0)
			{
				Type type = queue.Dequeue();
				hashSet.Add(type);
				List<Type> list2;
				if (this._allTypesWithDerivedTypeMapping.Value.TryGetValue(type, out list2))
				{
					using (List<Type>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Type derivedType = enumerator.Current;
							if (!hashSet.Contains(derivedType))
							{
								StructuralTypeConfiguration structuralTypeConfiguration = this.StructuralTypes.Except(this._explicitlyAddedTypes).FirstOrDefault((StructuralTypeConfiguration c) => c.ClrType == derivedType);
								if (structuralTypeConfiguration != null && structuralTypeConfiguration.Kind == EdmTypeKind.Entity)
								{
									list.Add((EntityTypeConfiguration)structuralTypeConfiguration);
								}
								queue.Enqueue(derivedType);
							}
						}
					}
				}
			}
			if (list.Any<EntityTypeConfiguration>())
			{
				this.ReconfigureEntityTypesAsComplexType(list.ToArray<EntityTypeConfiguration>());
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00024AA8 File Offset: 0x00022CA8
		internal bool InferEdmTypeFromDerivedTypes(Type propertyType, ref PropertyKind propertyKind)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			Queue<Type> queue = new Queue<Type>();
			queue.Enqueue(propertyType);
			IList<StructuralTypeConfiguration> list = new List<StructuralTypeConfiguration>();
			while (queue.Count != 0)
			{
				Type type = queue.Dequeue();
				hashSet.Add(type);
				List<Type> list2;
				if (this._allTypesWithDerivedTypeMapping.Value.TryGetValue(type, out list2))
				{
					using (List<Type>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Type derivedType = enumerator.Current;
							if (!hashSet.Contains(derivedType))
							{
								StructuralTypeConfiguration structuralTypeConfiguration = this._explicitlyAddedTypes.FirstOrDefault((StructuralTypeConfiguration c) => c.ClrType == derivedType);
								if (structuralTypeConfiguration != null)
								{
									list.Add(structuralTypeConfiguration);
								}
								queue.Enqueue(derivedType);
							}
						}
					}
				}
			}
			if (!list.Any<StructuralTypeConfiguration>())
			{
				return false;
			}
			IEnumerable<EntityTypeConfiguration> enumerable = list.OfType<EntityTypeConfiguration>().ToList<EntityTypeConfiguration>();
			IEnumerable<ComplexTypeConfiguration> enumerable2 = list.OfType<ComplexTypeConfiguration>().ToList<ComplexTypeConfiguration>();
			if (!enumerable.Any<EntityTypeConfiguration>())
			{
				propertyKind = PropertyKind.Complex;
				return true;
			}
			if (!enumerable2.Any<ComplexTypeConfiguration>())
			{
				propertyKind = PropertyKind.Navigation;
				return true;
			}
			string cannotInferEdmType = SRResources.CannotInferEdmType;
			object[] array = new object[3];
			array[0] = propertyType.FullName;
			array[1] = string.Join(",", enumerable.Select((EntityTypeConfiguration e) => e.ClrType.FullName));
			array[2] = string.Join(",", enumerable2.Select((ComplexTypeConfiguration e) => e.ClrType.FullName));
			throw Error.InvalidOperation(cannotInferEdmType, array);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00024C4C File Offset: 0x00022E4C
		private void PruneUnreachableTypes()
		{
			Queue<StructuralTypeConfiguration> queue = new Queue<StructuralTypeConfiguration>(this._explicitlyAddedTypes);
			HashSet<StructuralTypeConfiguration> hashSet = new HashSet<StructuralTypeConfiguration>();
			while (queue.Count != 0)
			{
				StructuralTypeConfiguration structuralTypeConfiguration = queue.Dequeue();
				foreach (PropertyConfiguration propertyConfiguration in structuralTypeConfiguration.Properties.Where((PropertyConfiguration property) => property.Kind > PropertyKind.Primitive))
				{
					if (propertyConfiguration.Kind != PropertyKind.Collection || EdmLibHelpers.GetEdmPrimitiveTypeOrNull((propertyConfiguration as CollectionPropertyConfiguration).ElementType) == null)
					{
						IEdmTypeConfiguration structuralTypeOrNull = this.GetStructuralTypeOrNull(propertyConfiguration.RelatedClrType);
						StructuralTypeConfiguration structuralTypeConfiguration2 = structuralTypeOrNull as StructuralTypeConfiguration;
						if (structuralTypeConfiguration2 != null && !hashSet.Contains(structuralTypeOrNull))
						{
							queue.Enqueue(structuralTypeConfiguration2);
						}
					}
				}
				if (structuralTypeConfiguration.Kind == EdmTypeKind.Entity)
				{
					EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)structuralTypeConfiguration;
					if (entityTypeConfiguration.BaseType != null && !hashSet.Contains(entityTypeConfiguration.BaseType))
					{
						queue.Enqueue(entityTypeConfiguration.BaseType);
					}
					using (IEnumerator<EntityTypeConfiguration> enumerator2 = this.DerivedTypes(entityTypeConfiguration).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EntityTypeConfiguration entityTypeConfiguration2 = enumerator2.Current;
							if (!hashSet.Contains(entityTypeConfiguration2))
							{
								queue.Enqueue(entityTypeConfiguration2);
							}
						}
						goto IL_01AC;
					}
					goto IL_0133;
				}
				goto IL_0133;
				IL_01AC:
				hashSet.Add(structuralTypeConfiguration);
				continue;
				IL_0133:
				if (structuralTypeConfiguration.Kind == EdmTypeKind.Complex)
				{
					ComplexTypeConfiguration complexTypeConfiguration = (ComplexTypeConfiguration)structuralTypeConfiguration;
					if (complexTypeConfiguration.BaseType != null && !hashSet.Contains(complexTypeConfiguration.BaseType))
					{
						queue.Enqueue(complexTypeConfiguration.BaseType);
					}
					foreach (ComplexTypeConfiguration complexTypeConfiguration2 in this.DerivedTypes(complexTypeConfiguration))
					{
						if (!hashSet.Contains(complexTypeConfiguration2))
						{
							queue.Enqueue(complexTypeConfiguration2);
						}
					}
					goto IL_01AC;
				}
				goto IL_01AC;
			}
			foreach (StructuralTypeConfiguration structuralTypeConfiguration3 in this.StructuralTypes.ToArray<StructuralTypeConfiguration>())
			{
				if (!hashSet.Contains(structuralTypeConfiguration3))
				{
					this.RemoveStructuralType(structuralTypeConfiguration3.ClrType);
				}
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00024E80 File Offset: 0x00023080
		private void ApplyTypeAndPropertyConventions(StructuralTypeConfiguration edmTypeConfiguration)
		{
			foreach (IConvention convention in ODataConventionModelBuilder._conventions)
			{
				IEdmTypeConvention edmTypeConvention = convention as IEdmTypeConvention;
				if (edmTypeConvention != null)
				{
					edmTypeConvention.Apply(edmTypeConfiguration, this);
				}
				IEdmPropertyConvention edmPropertyConvention = convention as IEdmPropertyConvention;
				if (edmPropertyConvention != null)
				{
					this.ApplyPropertyConvention(edmPropertyConvention, edmTypeConfiguration);
				}
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00024EF0 File Offset: 0x000230F0
		private void ApplyEnumTypeConventions(EnumTypeConfiguration enumTypeConfiguration)
		{
			new DataContractAttributeEnumTypeConvention().Apply(enumTypeConfiguration, this);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00024F00 File Offset: 0x00023100
		private void ApplyNavigationSourceConventions(NavigationSourceConfiguration navigationSourceConfiguration)
		{
			if (!this._configuredNavigationSources.Contains(navigationSourceConfiguration))
			{
				this._configuredNavigationSources.Add(navigationSourceConfiguration);
				foreach (INavigationSourceConvention navigationSourceConvention in ODataConventionModelBuilder._conventions.OfType<INavigationSourceConvention>())
				{
					if (navigationSourceConvention != null)
					{
						navigationSourceConvention.Apply(navigationSourceConfiguration, this);
					}
				}
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00024F70 File Offset: 0x00023170
		private void ApplyOperationConventions(OperationConfiguration operation)
		{
			foreach (IOperationConvention operationConvention in ODataConventionModelBuilder._conventions.OfType<IOperationConvention>())
			{
				operationConvention.Apply(operation, this);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00024FC0 File Offset: 0x000231C0
		private IEdmTypeConfiguration GetStructuralTypeOrNull(Type clrType)
		{
			IEdmTypeConfiguration edmTypeConfiguration = this.StructuralTypes.SingleOrDefault((StructuralTypeConfiguration edmType) => edmType.ClrType == clrType);
			if (edmTypeConfiguration == null)
			{
				Type type = TypeHelper.GetUnderlyingTypeOrSelf(clrType);
				edmTypeConfiguration = this.EnumTypes.SingleOrDefault((EnumTypeConfiguration edmType) => edmType.ClrType == type);
			}
			return edmTypeConfiguration;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00025028 File Offset: 0x00023228
		private void ApplyPropertyConvention(IEdmPropertyConvention propertyConvention, StructuralTypeConfiguration edmTypeConfiguration)
		{
			foreach (PropertyConfiguration propertyConfiguration in edmTypeConfiguration.Properties.ToArray<PropertyConfiguration>())
			{
				propertyConvention.Apply(propertyConfiguration, edmTypeConfiguration, this);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002505C File Offset: 0x0002325C
		private void ReapplyPropertyConvention(PropertyConfiguration property, StructuralTypeConfiguration edmTypeConfiguration)
		{
			foreach (IEdmPropertyConvention edmPropertyConvention in ODataConventionModelBuilder._conventions.OfType<IEdmPropertyConvention>())
			{
				edmPropertyConvention.Apply(property, edmTypeConfiguration, this);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000250B0 File Offset: 0x000232B0
		private static Dictionary<Type, List<Type>> BuildDerivedTypesMapping(IWebApiAssembliesResolver assemblyResolver)
		{
			IEnumerable<Type> enumerable = from t in TypeHelper.GetLoadedTypes(assemblyResolver)
				where TypeHelper.IsVisible(t) && TypeHelper.IsClass(t) && t != typeof(object)
				select t;
			Dictionary<Type, List<Type>> dictionary = enumerable.Distinct<Type>().ToDictionary((Type k) => k, (Type k) => new List<Type>());
			foreach (Type type in enumerable)
			{
				List<Type> list;
				if (TypeHelper.GetBaseType(type) != null && dictionary.TryGetValue(TypeHelper.GetBaseType(type), out list))
				{
					list.Add(type);
				}
			}
			return dictionary;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002518C File Offset: 0x0002338C
		public override void ValidateModel(IEdmModel model)
		{
			if (!this._isQueryCompositionMode)
			{
				base.ValidateModel(model);
			}
		}

		// Token: 0x040002BC RID: 700
		private static readonly List<IConvention> _conventions = new List<IConvention>
		{
			new AbstractTypeDiscoveryConvention(),
			new DataContractAttributeEdmTypeConvention(),
			new NotMappedAttributeConvention(),
			new DataMemberAttributeEdmPropertyConvention(),
			new RequiredAttributeEdmPropertyConvention(),
			new DefaultValueAttributeEdmPropertyConvention(),
			new ConcurrencyCheckAttributeEdmPropertyConvention(),
			new TimestampAttributeEdmPropertyConvention(),
			new ColumnAttributeEdmPropertyConvention(),
			new KeyAttributeEdmPropertyConvention(),
			new EntityKeyConvention(),
			new ComplexTypeAttributeConvention(),
			new IgnoreDataMemberAttributeEdmPropertyConvention(),
			new NotFilterableAttributeEdmPropertyConvention(),
			new NonFilterableAttributeEdmPropertyConvention(),
			new NotSortableAttributeEdmPropertyConvention(),
			new UnsortableAttributeEdmPropertyConvention(),
			new NotNavigableAttributeEdmPropertyConvention(),
			new NotExpandableAttributeEdmPropertyConvention(),
			new NotCountableAttributeEdmPropertyConvention(),
			new MediaTypeAttributeConvention(),
			new AutoExpandAttributeEdmPropertyConvention(),
			new AutoExpandAttributeEdmTypeConvention(),
			new MaxLengthAttributeEdmPropertyConvention(),
			new PageAttributeEdmPropertyConvention(),
			new PageAttributeEdmTypeConvention(),
			new ExpandAttributeEdmPropertyConvention(),
			new ExpandAttributeEdmTypeConvention(),
			new CountAttributeEdmPropertyConvention(),
			new CountAttributeEdmTypeConvention(),
			new OrderByAttributeEdmTypeConvention(),
			new FilterAttributeEdmTypeConvention(),
			new OrderByAttributeEdmPropertyConvention(),
			new FilterAttributeEdmPropertyConvention(),
			new SelectAttributeEdmTypeConvention(),
			new SelectAttributeEdmPropertyConvention(),
			new SelfLinksGenerationConvention(),
			new NavigationLinksGenerationConvention(),
			new AssociationSetDiscoveryConvention(),
			new ActionLinkGenerationConvention(),
			new FunctionLinkGenerationConvention()
		};

		// Token: 0x040002BD RID: 701
		private HashSet<StructuralTypeConfiguration> _mappedTypes;

		// Token: 0x040002BE RID: 702
		private HashSet<NavigationSourceConfiguration> _configuredNavigationSources;

		// Token: 0x040002BF RID: 703
		private HashSet<Type> _ignoredTypes;

		// Token: 0x040002C0 RID: 704
		private IEnumerable<StructuralTypeConfiguration> _explicitlyAddedTypes;

		// Token: 0x040002C1 RID: 705
		private bool _isModelBeingBuilt;

		// Token: 0x040002C2 RID: 706
		private bool _isQueryCompositionMode;

		// Token: 0x040002C3 RID: 707
		private Lazy<IDictionary<Type, List<Type>>> _allTypesWithDerivedTypeMapping;
	}
}
