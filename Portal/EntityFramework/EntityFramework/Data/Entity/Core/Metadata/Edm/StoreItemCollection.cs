using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FC RID: 1276
	public class StoreItemCollection : ItemCollection
	{
		// Token: 0x06003EFB RID: 16123 RVA: 0x000D1685 File Offset: 0x000CF885
		internal StoreItemCollection()
			: base(DataSpace.SSpace)
		{
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x000D16A4 File Offset: 0x000CF8A4
		internal StoreItemCollection(DbProviderFactory factory, DbProviderManifest manifest, string providerInvariantName, string providerManifestToken)
			: base(DataSpace.SSpace)
		{
			this._providerFactory = factory;
			this._providerManifest = manifest;
			this._providerInvariantName = providerInvariantName;
			this._providerManifestToken = providerManifestToken;
			this._cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			this.LoadProviderManifest(this._providerManifest);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000D1710 File Offset: 0x000CF910
		private StoreItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, IDbDependencyResolver resolver, out IList<EdmSchemaError> errors)
			: base(DataSpace.SSpace)
		{
			errors = this.Init(xmlReaders, filePaths, false, resolver, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000D1768 File Offset: 0x000CF968
		internal StoreItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths)
			: base(DataSpace.SSpace)
		{
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			this.Init(xmlReaders, filePaths, true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x000D17D4 File Offset: 0x000CF9D4
		public StoreItemCollection(IEnumerable<XmlReader> xmlReaders)
			: base(DataSpace.SSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReader");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x000D1860 File Offset: 0x000CFA60
		public StoreItemCollection(EdmModel model)
			: base(DataSpace.SSpace)
		{
			Check.NotNull<EdmModel>(model, "model");
			this._providerManifest = model.ProviderManifest;
			this._providerInvariantName = model.ProviderInfo.ProviderInvariantName;
			this._providerFactory = DbConfiguration.DependencyResolver.GetService(this._providerInvariantName);
			this._providerManifestToken = model.ProviderInfo.ProviderManifestToken;
			this._cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			this.LoadProviderManifest(this._providerManifest);
			this._schemaVersion = model.SchemaVersion;
			model.Validate();
			foreach (GlobalItem globalItem in model.GlobalItems)
			{
				globalItem.SetReadOnly();
				base.AddInternal(globalItem);
			}
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000D1958 File Offset: 0x000CFB58
		public StoreItemCollection(params string[] filePaths)
			: base(DataSpace.SSpace)
		{
			Check.NotNull<string[]>(filePaths, "filePaths");
			IEnumerable<string> enumerable = filePaths;
			EntityUtil.CheckArgumentEmpty<string>(ref enumerable, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(enumerable, ".ssdl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.SSpace);
				IEnumerable<XmlReader> enumerable2 = list.AsEnumerable<XmlReader>();
				EntityUtil.CheckArgumentEmpty<XmlReader>(ref enumerable2, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "filePaths");
				this.Init(list, metadataArtifactLoader.GetPaths(DataSpace.SSpace), true, null, out this._providerManifest, out this._providerFactory, out this._providerInvariantName, out this._providerManifestToken, out this._cachedCTypeFunction);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000D1A2C File Offset: 0x000CFC2C
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError, IDbDependencyResolver resolver, out DbProviderManifest providerManifest, out DbProviderFactory providerFactory, out string providerInvariantName, out string providerManifestToken, out Memoizer<EdmFunction, EdmFunction> cachedCTypeFunction)
		{
			cachedCTypeFunction = new Memoizer<EdmFunction, EdmFunction>(new Func<EdmFunction, EdmFunction>(StoreItemCollection.ConvertFunctionSignatureToCType), null);
			StoreItemCollection.Loader loader = new StoreItemCollection.Loader(xmlReaders, filePaths, throwOnError, resolver);
			providerFactory = loader.ProviderFactory;
			providerManifest = loader.ProviderManifest;
			providerManifestToken = loader.ProviderManifestToken;
			providerInvariantName = loader.ProviderInvariantName;
			if (!loader.HasNonWarningErrors)
			{
				this.LoadProviderManifest(loader.ProviderManifest);
				List<EdmSchemaError> list = EdmItemCollection.LoadItems(this._providerManifest, loader.Schemas, this);
				foreach (EdmSchemaError edmSchemaError in list)
				{
					loader.Errors.Add(edmSchemaError);
				}
				if (throwOnError && list.Count != 0)
				{
					loader.ThrowOnNonWarningErrors();
				}
			}
			return loader.Errors;
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000D1B04 File Offset: 0x000CFD04
		internal QueryCacheManager QueryCacheManager
		{
			get
			{
				return this._queryCacheManager;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000D1B0C File Offset: 0x000CFD0C
		public virtual DbProviderFactory ProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000D1B14 File Offset: 0x000CFD14
		public virtual DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000D1B1C File Offset: 0x000CFD1C
		public virtual string ProviderManifestToken
		{
			get
			{
				return this._providerManifestToken;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000D1B24 File Offset: 0x000CFD24
		public virtual string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x000D1B2C File Offset: 0x000CFD2C
		// (set) Token: 0x06003F09 RID: 16137 RVA: 0x000D1B34 File Offset: 0x000CFD34
		public double StoreSchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			internal set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000D1B3D File Offset: 0x000CFD3D
		public virtual ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out primitiveType);
			return primitiveType;
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000D1B6C File Offset: 0x000CFD6C
		private void LoadProviderManifest(DbProviderManifest storeManifest)
		{
			foreach (PrimitiveType primitiveType in storeManifest.GetStoreTypes())
			{
				base.AddInternal(primitiveType);
				this._primitiveTypeMaps.Add(primitiveType);
			}
			foreach (EdmFunction edmFunction in storeManifest.GetStoreFunctions())
			{
				base.AddInternal(edmFunction);
			}
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000D1C04 File Offset: 0x000CFE04
		internal ReadOnlyCollection<EdmFunction> GetCTypeFunctions(string functionName, bool ignoreCase)
		{
			ReadOnlyCollection<EdmFunction> readOnlyCollection;
			if (!base.FunctionLookUpTable.TryGetValue(functionName, out readOnlyCollection))
			{
				return Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			readOnlyCollection = this.ConvertToCTypeFunctions(readOnlyCollection);
			if (ignoreCase)
			{
				return readOnlyCollection;
			}
			return ItemCollection.GetCaseSensitiveFunctions(readOnlyCollection, functionName);
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000D1C3C File Offset: 0x000CFE3C
		private ReadOnlyCollection<EdmFunction> ConvertToCTypeFunctions(ReadOnlyCollection<EdmFunction> functionOverloads)
		{
			List<EdmFunction> list = new List<EdmFunction>();
			foreach (EdmFunction edmFunction in functionOverloads)
			{
				list.Add(this.ConvertToCTypeFunction(edmFunction));
			}
			return new ReadOnlyCollection<EdmFunction>(list);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000D1C98 File Offset: 0x000CFE98
		internal EdmFunction ConvertToCTypeFunction(EdmFunction sTypeFunction)
		{
			return this._cachedCTypeFunction.Evaluate(sTypeFunction);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000D1CA8 File Offset: 0x000CFEA8
		internal static EdmFunction ConvertFunctionSignatureToCType(EdmFunction sTypeFunction)
		{
			if (sTypeFunction.IsFromProviderManifest)
			{
				return sTypeFunction;
			}
			FunctionParameter functionParameter = null;
			if (sTypeFunction.ReturnParameter != null)
			{
				TypeUsage typeUsage = MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(sTypeFunction.ReturnParameter.TypeUsage);
				functionParameter = new FunctionParameter(sTypeFunction.ReturnParameter.Name, typeUsage, sTypeFunction.ReturnParameter.GetParameterMode());
			}
			List<FunctionParameter> list = new List<FunctionParameter>();
			if (sTypeFunction.Parameters.Count > 0)
			{
				foreach (FunctionParameter functionParameter2 in sTypeFunction.Parameters)
				{
					TypeUsage typeUsage2 = MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(functionParameter2.TypeUsage);
					FunctionParameter functionParameter3 = new FunctionParameter(functionParameter2.Name, typeUsage2, functionParameter2.GetParameterMode());
					list.Add(functionParameter3);
				}
			}
			FunctionParameter[] array;
			if (functionParameter != null)
			{
				(array = new FunctionParameter[1])[0] = functionParameter;
			}
			else
			{
				array = new FunctionParameter[0];
			}
			FunctionParameter[] array2 = array;
			EdmFunction edmFunction = new EdmFunction(sTypeFunction.Name, sTypeFunction.NamespaceName, DataSpace.CSpace, new EdmFunctionPayload
			{
				Schema = sTypeFunction.Schema,
				StoreFunctionName = sTypeFunction.StoreFunctionNameAttribute,
				CommandText = sTypeFunction.CommandTextAttribute,
				IsAggregate = new bool?(sTypeFunction.AggregateAttribute),
				IsBuiltIn = new bool?(sTypeFunction.BuiltInAttribute),
				IsNiladic = new bool?(sTypeFunction.NiladicFunctionAttribute),
				IsComposable = new bool?(sTypeFunction.IsComposableAttribute),
				IsFromProviderManifest = new bool?(sTypeFunction.IsFromProviderManifest),
				IsCachedStoreFunction = new bool?(true),
				IsFunctionImport = new bool?(sTypeFunction.IsFunctionImport),
				ReturnParameters = array2,
				Parameters = list.ToArray(),
				ParameterTypeSemantics = new ParameterTypeSemantics?(sTypeFunction.ParameterTypeSemanticsAttribute)
			});
			edmFunction.SetReadOnly();
			return edmFunction;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000D1E68 File Offset: 0x000D0068
		public static StoreItemCollection Create(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, IDbDependencyResolver resolver, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentEmpty<XmlReader>(ref xmlReaders, new Func<string, string>(Strings.StoreItemCollectionMustHaveOneArtifact), "xmlReaders");
			StoreItemCollection storeItemCollection = new StoreItemCollection(xmlReaders, filePaths, resolver, out errors);
			if (errors == null || errors.Count <= 0)
			{
				return storeItemCollection;
			}
			return null;
		}

		// Token: 0x04001580 RID: 5504
		private double _schemaVersion;

		// Token: 0x04001581 RID: 5505
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x04001582 RID: 5506
		private readonly Memoizer<EdmFunction, EdmFunction> _cachedCTypeFunction;

		// Token: 0x04001583 RID: 5507
		private readonly DbProviderManifest _providerManifest;

		// Token: 0x04001584 RID: 5508
		private readonly string _providerInvariantName;

		// Token: 0x04001585 RID: 5509
		private readonly string _providerManifestToken;

		// Token: 0x04001586 RID: 5510
		private readonly DbProviderFactory _providerFactory;

		// Token: 0x04001587 RID: 5511
		private readonly QueryCacheManager _queryCacheManager = QueryCacheManager.Create();

		// Token: 0x02000B0E RID: 2830
		private class Loader
		{
			// Token: 0x0600643F RID: 25663 RVA: 0x0015AB98 File Offset: 0x00158D98
			public Loader(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, bool throwOnError, IDbDependencyResolver resolver)
			{
				this._throwOnError = throwOnError;
				IDbDependencyResolver dbDependencyResolver2;
				if (resolver != null)
				{
					IDbDependencyResolver dbDependencyResolver = new CompositeResolver<IDbDependencyResolver, IDbDependencyResolver>(resolver, DbConfiguration.DependencyResolver);
					dbDependencyResolver2 = dbDependencyResolver;
				}
				else
				{
					dbDependencyResolver2 = DbConfiguration.DependencyResolver;
				}
				this._resolver = dbDependencyResolver2;
				this.LoadItems(xmlReaders, sourceFilePaths);
			}

			// Token: 0x170010E2 RID: 4322
			// (get) Token: 0x06006440 RID: 25664 RVA: 0x0015ABD9 File Offset: 0x00158DD9
			public IList<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x170010E3 RID: 4323
			// (get) Token: 0x06006441 RID: 25665 RVA: 0x0015ABE1 File Offset: 0x00158DE1
			public IList<Schema> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x170010E4 RID: 4324
			// (get) Token: 0x06006442 RID: 25666 RVA: 0x0015ABE9 File Offset: 0x00158DE9
			public DbProviderManifest ProviderManifest
			{
				get
				{
					return this._providerManifest;
				}
			}

			// Token: 0x170010E5 RID: 4325
			// (get) Token: 0x06006443 RID: 25667 RVA: 0x0015ABF1 File Offset: 0x00158DF1
			public DbProviderFactory ProviderFactory
			{
				get
				{
					return this._providerFactory;
				}
			}

			// Token: 0x170010E6 RID: 4326
			// (get) Token: 0x06006444 RID: 25668 RVA: 0x0015ABF9 File Offset: 0x00158DF9
			public string ProviderManifestToken
			{
				get
				{
					return this._providerManifestToken;
				}
			}

			// Token: 0x170010E7 RID: 4327
			// (get) Token: 0x06006445 RID: 25669 RVA: 0x0015AC01 File Offset: 0x00158E01
			public string ProviderInvariantName
			{
				get
				{
					return this._provider;
				}
			}

			// Token: 0x170010E8 RID: 4328
			// (get) Token: 0x06006446 RID: 25670 RVA: 0x0015AC09 File Offset: 0x00158E09
			public bool HasNonWarningErrors
			{
				get
				{
					return !MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors);
				}
			}

			// Token: 0x06006447 RID: 25671 RVA: 0x0015AC1C File Offset: 0x00158E1C
			private void LoadItems(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths)
			{
				this._errors = SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, SchemaDataModelOption.ProviderDataModel, new AttributeValueNotification(this.OnProviderNotification), new AttributeValueNotification(this.OnProviderManifestTokenNotification), new ProviderManifestNeeded(this.OnProviderManifestNeeded), out this._schemas);
				if (this._throwOnError)
				{
					this.ThrowOnNonWarningErrors();
				}
			}

			// Token: 0x06006448 RID: 25672 RVA: 0x0015AC6F File Offset: 0x00158E6F
			internal void ThrowOnNonWarningErrors()
			{
				if (!MetadataHelper.CheckIfAllErrorsAreWarnings(this._errors))
				{
					throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(this._errors));
				}
			}

			// Token: 0x06006449 RID: 25673 RVA: 0x0015AC90 File Offset: 0x00158E90
			private void OnProviderNotification(string provider, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				string provider2 = this._provider;
				if (this._provider == null)
				{
					this._provider = provider;
					this.InitializeProviderManifest(addError);
					return;
				}
				if (this._provider == provider)
				{
					return;
				}
				addError(Strings.AllArtifactsMustTargetSameProvider_InvariantName(provider2, this._provider), ErrorCode.InconsistentProvider, EdmSchemaErrorSeverity.Error);
			}

			// Token: 0x0600644A RID: 25674 RVA: 0x0015ACE4 File Offset: 0x00158EE4
			private void InitializeProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null && this._providerManifestToken != null && this._provider != null)
				{
					DbProviderFactory dbProviderFactory = null;
					try
					{
						dbProviderFactory = DbConfiguration.DependencyResolver.GetService(this._provider);
					}
					catch (ArgumentException ex)
					{
						addError(ex.Message, ErrorCode.InvalidProvider, EdmSchemaErrorSeverity.Error);
						return;
					}
					try
					{
						DbProviderServices service = this._resolver.GetService(this._provider);
						this._providerManifest = service.GetProviderManifest(this._providerManifestToken);
						this._providerFactory = dbProviderFactory;
						if (this._providerManifest is EdmProviderManifest)
						{
							if (this._throwOnError)
							{
								throw new NotSupportedException(Strings.OnlyStoreConnectionsSupported);
							}
							addError(Strings.OnlyStoreConnectionsSupported, ErrorCode.InvalidProvider, EdmSchemaErrorSeverity.Error);
						}
					}
					catch (ProviderIncompatibleException ex2)
					{
						if (this._throwOnError)
						{
							throw;
						}
						StoreItemCollection.Loader.AddProviderIncompatibleError(ex2, addError);
					}
				}
			}

			// Token: 0x0600644B RID: 25675 RVA: 0x0015ADCC File Offset: 0x00158FCC
			private void OnProviderManifestTokenNotification(string token, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifestToken == null)
				{
					this._providerManifestToken = token;
					this.InitializeProviderManifest(addError);
					return;
				}
				if (this._providerManifestToken != token)
				{
					addError(Strings.AllArtifactsMustTargetSameProvider_ManifestToken(token, this._providerManifestToken), ErrorCode.ProviderManifestTokenMismatch, EdmSchemaErrorSeverity.Error);
				}
			}

			// Token: 0x0600644C RID: 25676 RVA: 0x0015AE0B File Offset: 0x0015900B
			private DbProviderManifest OnProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (this._providerManifest == null)
				{
					addError(Strings.ProviderManifestTokenNotFound, ErrorCode.ProviderManifestTokenNotFound, EdmSchemaErrorSeverity.Error);
				}
				return this._providerManifest;
			}

			// Token: 0x0600644D RID: 25677 RVA: 0x0015AE2C File Offset: 0x0015902C
			private static void AddProviderIncompatibleError(ProviderIncompatibleException provEx, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				StringBuilder stringBuilder = new StringBuilder(provEx.Message);
				if (provEx.InnerException != null && !string.IsNullOrEmpty(provEx.InnerException.Message))
				{
					stringBuilder.AppendFormat(" {0}", provEx.InnerException.Message);
				}
				addError(stringBuilder.ToString(), ErrorCode.FailedToRetrieveProviderManifest, EdmSchemaErrorSeverity.Error);
			}

			// Token: 0x04002C99 RID: 11417
			private string _provider;

			// Token: 0x04002C9A RID: 11418
			private string _providerManifestToken;

			// Token: 0x04002C9B RID: 11419
			private DbProviderManifest _providerManifest;

			// Token: 0x04002C9C RID: 11420
			private DbProviderFactory _providerFactory;

			// Token: 0x04002C9D RID: 11421
			private IList<EdmSchemaError> _errors;

			// Token: 0x04002C9E RID: 11422
			private IList<Schema> _schemas;

			// Token: 0x04002C9F RID: 11423
			private readonly bool _throwOnError;

			// Token: 0x04002CA0 RID: 11424
			private readonly IDbDependencyResolver _resolver;
		}
	}
}
