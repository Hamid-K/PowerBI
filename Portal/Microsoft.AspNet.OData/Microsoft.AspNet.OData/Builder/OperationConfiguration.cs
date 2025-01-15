using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200013B RID: 315
	public abstract class OperationConfiguration
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x0002C17F File Offset: 0x0002A37F
		internal OperationConfiguration(ODataModelBuilder builder, string name)
		{
			this.Name = name;
			this.ModelBuilder = builder;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
		protected OperationLinkBuilder OperationLinkBuilder { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0002C1B1 File Offset: 0x0002A3B1
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0002C1B9 File Offset: 0x0002A3B9
		public bool FollowsConventions { get; protected set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002C1C2 File Offset: 0x0002A3C2
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0002C1CA File Offset: 0x0002A3CA
		public string Name { get; protected set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002C1D3 File Offset: 0x0002A3D3
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0002C1DB File Offset: 0x0002A3DB
		public string Title { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B14 RID: 2836
		public abstract OperationKind Kind { get; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		public virtual bool IsComposable { get; internal set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B17 RID: 2839
		public abstract bool IsSideEffecting { get; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002C1F5 File Offset: 0x0002A3F5
		public string FullyQualifiedName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002C20D File Offset: 0x0002A40D
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0002C224 File Offset: 0x0002A424
		public string Namespace
		{
			get
			{
				return this._namespace ?? this.ModelBuilder.Namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002C22D File Offset: 0x0002A42D
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0002C235 File Offset: 0x0002A435
		public IEdmTypeConfiguration ReturnType { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002C23E File Offset: 0x0002A43E
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0002C246 File Offset: 0x0002A446
		public bool ReturnNullable { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002C24F File Offset: 0x0002A44F
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0002C257 File Offset: 0x0002A457
		public NavigationSourceConfiguration NavigationSource { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002C260 File Offset: 0x0002A460
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0002C268 File Offset: 0x0002A468
		public IEnumerable<string> EntitySetPath { get; internal set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002C271 File Offset: 0x0002A471
		public virtual BindingParameterConfiguration BindingParameter
		{
			get
			{
				return this._bindingParameter;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002C279 File Offset: 0x0002A479
		public virtual IEnumerable<ParameterConfiguration> Parameters
		{
			get
			{
				if (this._bindingParameter != null)
				{
					yield return this._bindingParameter;
				}
				foreach (ParameterConfiguration parameterConfiguration in this._parameters)
				{
					yield return parameterConfiguration;
				}
				List<ParameterConfiguration>.Enumerator enumerator = default(List<ParameterConfiguration>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002C289 File Offset: 0x0002A489
		public virtual bool IsBindable
		{
			get
			{
				return this._bindingParameter != null;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002C294 File Offset: 0x0002A494
		internal void ReturnsFromEntitySetImplementation<TEntityType>(string entitySetName) where TEntityType : class
		{
			this.ModelBuilder.EntitySet<TEntityType>(entitySetName);
			this.NavigationSource = this.ModelBuilder.EntitySets.Single((EntitySetConfiguration s) => s.Name == entitySetName);
			this.ReturnType = this.ModelBuilder.GetTypeConfigurationOrNull(typeof(TEntityType));
			this.ReturnNullable = true;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002C304 File Offset: 0x0002A504
		internal void ReturnsCollectionFromEntitySetImplementation<TElementEntityType>(string entitySetName) where TElementEntityType : class
		{
			Type typeFromHandle = typeof(IEnumerable<TElementEntityType>);
			this.ModelBuilder.EntitySet<TElementEntityType>(entitySetName);
			this.NavigationSource = this.ModelBuilder.EntitySets.Single((EntitySetConfiguration s) => s.Name == entitySetName);
			IEdmTypeConfiguration typeConfigurationOrNull = this.ModelBuilder.GetTypeConfigurationOrNull(typeof(TElementEntityType));
			this.ReturnType = new CollectionTypeConfiguration(typeConfigurationOrNull, typeFromHandle);
			this.ReturnNullable = true;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002C387 File Offset: 0x0002A587
		internal void ReturnsEntityViaEntitySetPathImplementation<TEntityType>(IEnumerable<string> entitySetPath) where TEntityType : class
		{
			this.ReturnType = this.ModelBuilder.GetTypeConfigurationOrNull(typeof(TEntityType));
			this.EntitySetPath = entitySetPath;
			this.ReturnNullable = true;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		internal void ReturnsCollectionViaEntitySetPathImplementation<TElementEntityType>(IEnumerable<string> entitySetPath) where TElementEntityType : class
		{
			Type typeFromHandle = typeof(IEnumerable<TElementEntityType>);
			IEdmTypeConfiguration typeConfigurationOrNull = this.ModelBuilder.GetTypeConfigurationOrNull(typeof(TElementEntityType));
			this.ReturnType = new CollectionTypeConfiguration(typeConfigurationOrNull, typeFromHandle);
			this.EntitySetPath = entitySetPath;
			this.ReturnNullable = true;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002C400 File Offset: 0x0002A600
		internal void ReturnsImplementation(Type clrReturnType)
		{
			IEdmTypeConfiguration operationTypeConfiguration = this.GetOperationTypeConfiguration(clrReturnType);
			this.ReturnType = operationTypeConfiguration;
			this.ReturnNullable = EdmLibHelpers.IsNullable(clrReturnType);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002C428 File Offset: 0x0002A628
		internal void ReturnsCollectionImplementation<TReturnElementType>()
		{
			Type typeFromHandle = typeof(IEnumerable<TReturnElementType>);
			Type typeFromHandle2 = typeof(TReturnElementType);
			IEdmTypeConfiguration operationTypeConfiguration = this.GetOperationTypeConfiguration(typeFromHandle2);
			this.ReturnType = new CollectionTypeConfiguration(operationTypeConfiguration, typeFromHandle);
			this.ReturnNullable = EdmLibHelpers.IsNullable(typeFromHandle2);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002C46C File Offset: 0x0002A66C
		internal void SetBindingParameterImplementation(string name, IEdmTypeConfiguration bindingParameterType)
		{
			this._bindingParameter = new BindingParameterConfiguration(name, bindingParameterType);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002C47C File Offset: 0x0002A67C
		public ParameterConfiguration AddParameter(string name, IEdmTypeConfiguration parameterType)
		{
			ParameterConfiguration parameterConfiguration = new NonbindingParameterConfiguration(name, parameterType);
			this._parameters.Add(parameterConfiguration);
			return parameterConfiguration;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
		public ParameterConfiguration Parameter(Type clrParameterType, string name)
		{
			if (clrParameterType == null)
			{
				throw Error.ArgumentNull("clrParameterType");
			}
			IEdmTypeConfiguration operationTypeConfiguration = this.GetOperationTypeConfiguration(clrParameterType);
			return this.AddParameter(name, operationTypeConfiguration);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002C4D1 File Offset: 0x0002A6D1
		public ParameterConfiguration Parameter<TParameter>(string name)
		{
			return this.Parameter(typeof(TParameter), name);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		public ParameterConfiguration CollectionParameter<TElementType>(string name)
		{
			Type typeFromHandle = typeof(TElementType);
			CollectionTypeConfiguration collectionTypeConfiguration = new CollectionTypeConfiguration(this.GetOperationTypeConfiguration(typeof(TElementType)), typeof(IEnumerable<>).MakeGenericType(new Type[] { typeFromHandle }));
			return this.AddParameter(name, collectionTypeConfiguration);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002C534 File Offset: 0x0002A734
		public ParameterConfiguration EntityParameter<TEntityType>(string name) where TEntityType : class
		{
			Type entityType = typeof(TEntityType);
			IEdmTypeConfiguration edmTypeConfiguration = this.ModelBuilder.StructuralTypes.FirstOrDefault((StructuralTypeConfiguration t) => t.ClrType == entityType) ?? this.ModelBuilder.AddEntityType(entityType);
			return this.AddParameter(name, edmTypeConfiguration);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002C594 File Offset: 0x0002A794
		public ParameterConfiguration CollectionEntityParameter<TElementEntityType>(string name) where TElementEntityType : class
		{
			Type elementType = typeof(TElementEntityType);
			CollectionTypeConfiguration collectionTypeConfiguration = new CollectionTypeConfiguration(this.ModelBuilder.StructuralTypes.FirstOrDefault((StructuralTypeConfiguration t) => t.ClrType == elementType) ?? this.ModelBuilder.AddEntityType(elementType), typeof(IEnumerable<>).MakeGenericType(new Type[] { elementType }));
			return this.AddParameter(name, collectionTypeConfiguration);
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002C614 File Offset: 0x0002A814
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0002C61C File Offset: 0x0002A81C
		protected ODataModelBuilder ModelBuilder { get; set; }

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002C628 File Offset: 0x0002A828
		private IEdmTypeConfiguration GetOperationTypeConfiguration(Type clrType)
		{
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(clrType);
			IEdmTypeConfiguration edmTypeConfiguration;
			if (TypeHelper.IsEnum(underlyingTypeOrSelf))
			{
				edmTypeConfiguration = this.ModelBuilder.GetTypeConfigurationOrNull(underlyingTypeOrSelf);
				if (edmTypeConfiguration != null && EdmLibHelpers.IsNullable(clrType))
				{
					edmTypeConfiguration = ((EnumTypeConfiguration)edmTypeConfiguration).GetNullableEnumTypeConfiguration();
				}
			}
			else
			{
				edmTypeConfiguration = this.ModelBuilder.GetTypeConfigurationOrNull(clrType);
			}
			if (edmTypeConfiguration == null)
			{
				if (TypeHelper.IsEnum(underlyingTypeOrSelf))
				{
					EnumTypeConfiguration enumTypeConfiguration = this.ModelBuilder.AddEnumType(underlyingTypeOrSelf);
					if (EdmLibHelpers.IsNullable(clrType))
					{
						edmTypeConfiguration = enumTypeConfiguration.GetNullableEnumTypeConfiguration();
					}
					else
					{
						edmTypeConfiguration = enumTypeConfiguration;
					}
				}
				else
				{
					edmTypeConfiguration = this.ModelBuilder.AddComplexType(clrType);
				}
			}
			return edmTypeConfiguration;
		}

		// Token: 0x0400036E RID: 878
		private List<ParameterConfiguration> _parameters = new List<ParameterConfiguration>();

		// Token: 0x0400036F RID: 879
		private BindingParameterConfiguration _bindingParameter;

		// Token: 0x04000370 RID: 880
		private string _namespace;
	}
}
