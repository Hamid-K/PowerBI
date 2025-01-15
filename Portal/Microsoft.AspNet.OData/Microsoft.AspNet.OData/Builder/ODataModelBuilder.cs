using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000137 RID: 311
	public class ODataModelBuilder
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002B678 File Offset: 0x00029878
		public ODataModelBuilder()
		{
			this._namespace = "Default";
			this.ContainerName = "Container";
			this.DataServiceVersion = ODataModelBuilder._defaultDataServiceVersion;
			this.MaxDataServiceVersion = ODataModelBuilder._defaultMaxDataServiceVersion;
			this.BindingOptions = NavigationPropertyBindingOption.None;
			this.HasAssignedNamespace = false;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002B6FC File Offset: 0x000298FC
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0002B704 File Offset: 0x00029904
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.HasAssignedNamespace = false;
					this._namespace = "Default";
					return;
				}
				this.HasAssignedNamespace = true;
				this._namespace = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002B72F File Offset: 0x0002992F
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002B737 File Offset: 0x00029937
		public string ContainerName { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B740 File Offset: 0x00029940
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B748 File Offset: 0x00029948
		public Version DataServiceVersion
		{
			get
			{
				return this._dataServiceVersion;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._dataServiceVersion = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002B760 File Offset: 0x00029960
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B768 File Offset: 0x00029968
		public Version MaxDataServiceVersion
		{
			get
			{
				return this._maxDataServiceVersion;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._maxDataServiceVersion = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002B780 File Offset: 0x00029980
		public virtual IEnumerable<EntitySetConfiguration> EntitySets
		{
			get
			{
				return this._navigationSources.Values.OfType<EntitySetConfiguration>();
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002B792 File Offset: 0x00029992
		public virtual IEnumerable<StructuralTypeConfiguration> StructuralTypes
		{
			get
			{
				return this._structuralTypes.Values;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002B79F File Offset: 0x0002999F
		public virtual IEnumerable<EnumTypeConfiguration> EnumTypes
		{
			get
			{
				return this._enumTypes.Values;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002B7AC File Offset: 0x000299AC
		public virtual IEnumerable<SingletonConfiguration> Singletons
		{
			get
			{
				return this._navigationSources.Values.OfType<SingletonConfiguration>();
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002B7BE File Offset: 0x000299BE
		public virtual IEnumerable<NavigationSourceConfiguration> NavigationSources
		{
			get
			{
				return this._navigationSources.Values;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002B7CB File Offset: 0x000299CB
		public virtual IEnumerable<OperationConfiguration> Operations
		{
			get
			{
				return this._operations;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002B7D3 File Offset: 0x000299D3
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0002B7DB File Offset: 0x000299DB
		public NavigationPropertyBindingOption BindingOptions { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002B7E4 File Offset: 0x000299E4
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0002B7EC File Offset: 0x000299EC
		internal bool HasAssignedNamespace { get; private set; }

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002B7F5 File Offset: 0x000299F5
		public EntityTypeConfiguration<TEntityType> EntityType<TEntityType>() where TEntityType : class
		{
			return new EntityTypeConfiguration<TEntityType>(this, this.AddEntityType(typeof(TEntityType)));
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002B80D File Offset: 0x00029A0D
		public ComplexTypeConfiguration<TComplexType> ComplexType<TComplexType>() where TComplexType : class
		{
			return new ComplexTypeConfiguration<TComplexType>(this, this.AddComplexType(typeof(TComplexType)));
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002B828 File Offset: 0x00029A28
		public EntitySetConfiguration<TEntityType> EntitySet<TEntityType>(string name) where TEntityType : class
		{
			EntityTypeConfiguration entityTypeConfiguration = this.AddEntityType(typeof(TEntityType));
			return new EntitySetConfiguration<TEntityType>(this, this.AddEntitySet(name, entityTypeConfiguration));
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002B854 File Offset: 0x00029A54
		public EnumTypeConfiguration<TEnumType> EnumType<TEnumType>()
		{
			return new EnumTypeConfiguration<TEnumType>(this.AddEnumType(typeof(TEnumType)));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002B86C File Offset: 0x00029A6C
		public SingletonConfiguration<TEntityType> Singleton<TEntityType>(string name) where TEntityType : class
		{
			EntityTypeConfiguration entityTypeConfiguration = this.AddEntityType(typeof(TEntityType));
			return new SingletonConfiguration<TEntityType>(this, this.AddSingleton(name, entityTypeConfiguration));
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002B898 File Offset: 0x00029A98
		public virtual ActionConfiguration Action(string name)
		{
			ActionConfiguration actionConfiguration = new ActionConfiguration(this, name);
			this._operations.Add(actionConfiguration);
			return actionConfiguration;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual FunctionConfiguration Function(string name)
		{
			FunctionConfiguration functionConfiguration = new FunctionConfiguration(this, name);
			this._operations.Add(functionConfiguration);
			return functionConfiguration;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		public virtual EntityTypeConfiguration AddEntityType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (!this._structuralTypes.ContainsKey(type))
			{
				EntityTypeConfiguration entityTypeConfiguration = new EntityTypeConfiguration(this, type);
				this._structuralTypes.Add(type, entityTypeConfiguration);
				return entityTypeConfiguration;
			}
			EntityTypeConfiguration entityTypeConfiguration2 = this._structuralTypes[type] as EntityTypeConfiguration;
			if (entityTypeConfiguration2 == null || entityTypeConfiguration2.ClrType != type)
			{
				throw Error.Argument("type", SRResources.TypeCannotBeEntityWasComplex, new object[] { type.FullName });
			}
			return entityTypeConfiguration2;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002B96C File Offset: 0x00029B6C
		public virtual ComplexTypeConfiguration AddComplexType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (!this._structuralTypes.ContainsKey(type))
			{
				ComplexTypeConfiguration complexTypeConfiguration = new ComplexTypeConfiguration(this, type);
				this._structuralTypes.Add(type, complexTypeConfiguration);
				return complexTypeConfiguration;
			}
			ComplexTypeConfiguration complexTypeConfiguration2 = this._structuralTypes[type] as ComplexTypeConfiguration;
			if (complexTypeConfiguration2 == null || complexTypeConfiguration2.ClrType != type)
			{
				throw Error.Argument("type", SRResources.TypeCannotBeComplexWasEntity, new object[] { type.FullName });
			}
			return complexTypeConfiguration2;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		public virtual EnumTypeConfiguration AddEnumType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (!TypeHelper.IsEnum(type))
			{
				throw Error.Argument("type", SRResources.TypeCannotBeEnum, new object[] { type.FullName });
			}
			if (!this._enumTypes.ContainsKey(type))
			{
				EnumTypeConfiguration enumTypeConfiguration = new EnumTypeConfiguration(this, type);
				this._enumTypes.Add(type, enumTypeConfiguration);
				return enumTypeConfiguration;
			}
			EnumTypeConfiguration enumTypeConfiguration2 = this._enumTypes[type];
			if (enumTypeConfiguration2.ClrType != type)
			{
				throw Error.Argument("type", SRResources.TypeCannotBeEnum, new object[] { type.FullName });
			}
			return enumTypeConfiguration2;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		public virtual void AddOperation(OperationConfiguration operation)
		{
			this._operations.Add(operation);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002BAB0 File Offset: 0x00029CB0
		public virtual EntitySetConfiguration AddEntitySet(string name, EntityTypeConfiguration entityType)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw Error.ArgumentNullOrEmpty("name");
			}
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			if (name.Contains("."))
			{
				throw Error.NotSupported(SRResources.InvalidEntitySetName, new object[] { name });
			}
			EntitySetConfiguration entitySetConfiguration;
			if (this._navigationSources.ContainsKey(name))
			{
				entitySetConfiguration = this._navigationSources[name] as EntitySetConfiguration;
				if (entitySetConfiguration == null)
				{
					throw Error.Argument("name", SRResources.EntitySetNameAlreadyConfiguredAsSingleton, new object[] { name });
				}
				if (entitySetConfiguration.EntityType != entityType)
				{
					throw Error.Argument("entityType", SRResources.EntitySetAlreadyConfiguredDifferentEntityType, new object[]
					{
						entitySetConfiguration.Name,
						entitySetConfiguration.EntityType.Name
					});
				}
			}
			else
			{
				entitySetConfiguration = new EntitySetConfiguration(this, entityType, name);
				this._navigationSources[name] = entitySetConfiguration;
			}
			return entitySetConfiguration;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002BB8C File Offset: 0x00029D8C
		public virtual SingletonConfiguration AddSingleton(string name, EntityTypeConfiguration entityType)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw Error.ArgumentNullOrEmpty("name");
			}
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			if (name.Contains("."))
			{
				throw Error.NotSupported(SRResources.InvalidSingletonName, new object[] { name });
			}
			SingletonConfiguration singletonConfiguration;
			if (this._navigationSources.ContainsKey(name))
			{
				singletonConfiguration = this._navigationSources[name] as SingletonConfiguration;
				if (singletonConfiguration == null)
				{
					throw Error.Argument("name", SRResources.SingletonNameAlreadyConfiguredAsEntitySet, new object[] { name });
				}
				if (singletonConfiguration.EntityType != entityType)
				{
					throw Error.Argument("entityType", SRResources.SingletonAlreadyConfiguredDifferentEntityType, new object[]
					{
						singletonConfiguration.Name,
						singletonConfiguration.EntityType.Name
					});
				}
			}
			else
			{
				singletonConfiguration = new SingletonConfiguration(this, entityType, name);
				this._navigationSources[name] = singletonConfiguration;
			}
			return singletonConfiguration;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002BC68 File Offset: 0x00029E68
		public virtual bool RemoveStructuralType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return this._structuralTypes.Remove(type);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002BC8A File Offset: 0x00029E8A
		public virtual bool RemoveEnumType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return this._enumTypes.Remove(type);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002BCAC File Offset: 0x00029EAC
		public virtual bool RemoveEntitySet(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			return this._navigationSources.ContainsKey(name) && this._navigationSources[name] is EntitySetConfiguration && this._navigationSources.Remove(name);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002BCEB File Offset: 0x00029EEB
		public virtual bool RemoveSingleton(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			return this._navigationSources.ContainsKey(name) && this._navigationSources[name] is SingletonConfiguration && this._navigationSources.Remove(name);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002BD2C File Offset: 0x00029F2C
		public virtual bool RemoveOperation(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			OperationConfiguration[] array = this._operations.Where((OperationConfiguration p) => p.Name == name).ToArray<OperationConfiguration>();
			int num = array.Count<OperationConfiguration>();
			if (num == 1)
			{
				return this.RemoveOperation(array[0]);
			}
			if (num == 0)
			{
				return false;
			}
			throw Error.InvalidOperation(SRResources.MoreThanOneOperationFound, new object[] { name });
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002BDA9 File Offset: 0x00029FA9
		public virtual bool RemoveOperation(OperationConfiguration operation)
		{
			if (operation == null)
			{
				throw Error.ArgumentNull("operation");
			}
			return this._operations.Remove(operation);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002BDC8 File Offset: 0x00029FC8
		public IEdmTypeConfiguration GetTypeConfigurationOrNull(Type type)
		{
			if (this._primitiveTypes.ContainsKey(type))
			{
				return this._primitiveTypes[type];
			}
			IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(type);
			if (edmPrimitiveTypeOrNull != null)
			{
				PrimitiveTypeConfiguration primitiveTypeConfiguration = new PrimitiveTypeConfiguration(this, edmPrimitiveTypeOrNull, type);
				this._primitiveTypes[type] = primitiveTypeConfiguration;
				return primitiveTypeConfiguration;
			}
			if (this._structuralTypes.ContainsKey(type))
			{
				return this._structuralTypes[type];
			}
			if (this._enumTypes.ContainsKey(type))
			{
				return this._enumTypes[type];
			}
			return null;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002BE4C File Offset: 0x0002A04C
		public virtual IEdmModel GetEdmModel()
		{
			IEdmModel edmModel = EdmModelHelperMethods.BuildEdmModel(this);
			this.ValidateModel(edmModel);
			return edmModel;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002BE68 File Offset: 0x0002A068
		public virtual void ValidateModel(IEdmModel model)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			foreach (IEdmEntitySet edmEntitySet in model.EntityContainer.Elements.OfType<IEdmEntitySet>())
			{
				if (!edmEntitySet.EntityType().Key().Any<IEdmStructuralProperty>())
				{
					throw Error.InvalidOperation(SRResources.EntitySetTypeHasNoKeys, new object[]
					{
						edmEntitySet.Name,
						edmEntitySet.EntityType().FullName()
					});
				}
			}
			foreach (IEdmStructuredType edmStructuredType in model.SchemaElementsAcrossModels().OfType<IEdmStructuredType>())
			{
				foreach (IEdmNavigationProperty edmNavigationProperty in edmStructuredType.DeclaredNavigationProperties())
				{
					if (edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many)
					{
						IEdmEntityType edmEntityType = edmNavigationProperty.ToEntityType();
						if (!edmEntityType.Key().Any<IEdmStructuralProperty>())
						{
							throw Error.InvalidOperation(SRResources.CollectionNavigationPropertyEntityTypeDoesntHaveKeyDefined, new object[]
							{
								edmEntityType.FullTypeName(),
								edmNavigationProperty.Name,
								edmStructuredType.FullTypeName()
							});
						}
					}
				}
			}
		}

		// Token: 0x04000356 RID: 854
		private const string DefaultNamespace = "Default";

		// Token: 0x04000357 RID: 855
		private static readonly Version _defaultDataServiceVersion = EdmConstants.EdmVersion4;

		// Token: 0x04000358 RID: 856
		private static readonly Version _defaultMaxDataServiceVersion = EdmConstants.EdmVersion4;

		// Token: 0x04000359 RID: 857
		private Dictionary<Type, EnumTypeConfiguration> _enumTypes = new Dictionary<Type, EnumTypeConfiguration>();

		// Token: 0x0400035A RID: 858
		private Dictionary<Type, StructuralTypeConfiguration> _structuralTypes = new Dictionary<Type, StructuralTypeConfiguration>();

		// Token: 0x0400035B RID: 859
		private Dictionary<string, NavigationSourceConfiguration> _navigationSources = new Dictionary<string, NavigationSourceConfiguration>();

		// Token: 0x0400035C RID: 860
		private Dictionary<Type, PrimitiveTypeConfiguration> _primitiveTypes = new Dictionary<Type, PrimitiveTypeConfiguration>();

		// Token: 0x0400035D RID: 861
		private List<OperationConfiguration> _operations = new List<OperationConfiguration>();

		// Token: 0x0400035E RID: 862
		private Version _dataServiceVersion;

		// Token: 0x0400035F RID: 863
		private Version _maxDataServiceVersion;

		// Token: 0x04000360 RID: 864
		private string _namespace;
	}
}
