using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A5 RID: 1189
	public sealed class EdmItemCollection : ItemCollection
	{
		// Token: 0x06003A6F RID: 14959 RVA: 0x000C0E1A File Offset: 0x000BF01A
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool skipInitialization = false)
			: base(DataSpace.CSpace)
		{
			if (!skipInitialization)
			{
				this.Init(xmlReaders, filePaths, true);
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000C0E48 File Offset: 0x000BF048
		public EdmItemCollection(IEnumerable<XmlReader> xmlReaders)
			: base(DataSpace.CSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000C0EA8 File Offset: 0x000BF0A8
		public EdmItemCollection(EdmModel model)
			: base(DataSpace.CSpace)
		{
			Check.NotNull<EdmModel>(model, "model");
			this.Init();
			this._edmVersion = model.SchemaVersion;
			model.Validate();
			foreach (GlobalItem globalItem in model.GlobalItems)
			{
				globalItem.SetReadOnly();
				base.AddInternal(globalItem);
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000C0F3C File Offset: 0x000BF13C
		public EdmItemCollection(params string[] filePaths)
			: base(DataSpace.CSpace)
		{
			Check.NotNull<string[]>(filePaths, "filePaths");
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, ".csdl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.CSpace);
				this.Init(list, metadataArtifactLoader.GetPaths(DataSpace.CSpace), true);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000C0FB8 File Offset: 0x000BF1B8
		private EdmItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors)
			: base(DataSpace.CSpace)
		{
			errors = this.Init(xmlReaders, filePaths, false);
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000C0FE2 File Offset: 0x000BF1E2
		private void Init()
		{
			this.LoadEdmPrimitiveTypesAndFunctions();
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000C0FEA File Offset: 0x000BF1EA
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError)
		{
			this.Init();
			return EdmItemCollection.LoadItems(xmlReaders, filePaths, SchemaDataModelOption.EntityDataModel, MetadataItem.EdmProviderManifest, this, throwOnError);
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000C1001 File Offset: 0x000BF201
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x000C1009 File Offset: 0x000BF209
		public double EdmVersion
		{
			get
			{
				return this._edmVersion;
			}
			internal set
			{
				this._edmVersion = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000C1012 File Offset: 0x000BF212
		internal OcAssemblyCache ConventionalOcCache
		{
			get
			{
				return this._conventionalOcCache;
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000C101C File Offset: 0x000BF21C
		internal InitializerMetadata GetCanonicalInitializerMetadata(InitializerMetadata metadata)
		{
			if (this._getCanonicalInitializerMetadataMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<InitializerMetadata, InitializerMetadata>>(ref this._getCanonicalInitializerMetadataMemoizer, new Memoizer<InitializerMetadata, InitializerMetadata>((InitializerMetadata m) => m, EqualityComparer<InitializerMetadata>.Default), null);
			}
			return this._getCanonicalInitializerMetadataMemoizer.Evaluate(metadata);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000C1074 File Offset: 0x000BF274
		internal static bool IsSystemNamespace(DbProviderManifest manifest, string namespaceName)
		{
			if (manifest == MetadataItem.EdmProviderManifest)
			{
				return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
			}
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System" || (manifest != null && namespaceName == manifest.NamespaceName);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000C10EC File Offset: 0x000BF2EC
		internal static IList<EdmSchemaError> LoadItems(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModelOption, DbProviderManifest providerManifest, ItemCollection itemCollection, bool throwOnError)
		{
			IList<Schema> list = null;
			IList<EdmSchemaError> list2 = SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModelOption, providerManifest, out list);
			if (MetadataHelper.CheckIfAllErrorsAreWarnings(list2))
			{
				foreach (EdmSchemaError edmSchemaError in EdmItemCollection.LoadItems(providerManifest, list, itemCollection))
				{
					list2.Add(edmSchemaError);
				}
			}
			if (!MetadataHelper.CheckIfAllErrorsAreWarnings(list2) && throwOnError)
			{
				throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list2));
			}
			return list2;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000C1174 File Offset: 0x000BF374
		internal static List<EdmSchemaError> LoadItems(DbProviderManifest manifest, IList<Schema> somSchemas, ItemCollection itemCollection)
		{
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			IEnumerable<GlobalItem> enumerable = EdmItemCollection.LoadSomSchema(somSchemas, manifest, itemCollection);
			List<string> list2 = new List<string>();
			foreach (GlobalItem globalItem in enumerable)
			{
				if (globalItem.BuiltInTypeKind == BuiltInTypeKind.EdmFunction && globalItem.DataSpace == DataSpace.SSpace)
				{
					EdmFunction edmFunction = (EdmFunction)globalItem;
					StringBuilder stringBuilder = new StringBuilder();
					EdmFunction.BuildIdentity<FunctionParameter>(stringBuilder, edmFunction.FullName, edmFunction.Parameters, (FunctionParameter param) => MetadataHelper.ConvertStoreTypeUsageToEdmTypeUsage(param.TypeUsage), (FunctionParameter param) => param.Mode);
					string text = stringBuilder.ToString();
					if (list2.Contains(text))
					{
						list.Add(new EdmSchemaError(Strings.DuplicatedFunctionoverloads(edmFunction.FullName, text.Substring(edmFunction.FullName.Length)).Trim(), 174, EdmSchemaErrorSeverity.Error));
						continue;
					}
					list2.Add(text);
				}
				globalItem.SetReadOnly();
				itemCollection.AddInternal(globalItem);
			}
			return list;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000C12A4 File Offset: 0x000BF4A4
		internal static IEnumerable<GlobalItem> LoadSomSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			return Converter.ConvertSchema(somSchemas, providerManifest, itemCollection);
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000C12AE File Offset: 0x000BF4AE
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000C12BC File Offset: 0x000BF4BC
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(double edmVersion)
		{
			if (edmVersion == 1.0 || edmVersion == 1.1 || edmVersion == 2.0)
			{
				return new ReadOnlyCollection<PrimitiveType>((from type in this._primitiveTypeMaps.GetTypes()
					where !Helper.IsSpatialType(type) && !Helper.IsHierarchyIdType(type)
					select type).ToList<PrimitiveType>());
			}
			if (edmVersion == 3.0)
			{
				return this._primitiveTypeMaps.GetTypes();
			}
			throw new ArgumentException(Strings.InvalidEDMVersion(edmVersion.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000C1358 File Offset: 0x000BF558
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out primitiveType);
			return primitiveType;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000C1378 File Offset: 0x000BF578
		private void LoadEdmPrimitiveTypesAndFunctions()
		{
			EdmProviderManifest instance = EdmProviderManifest.Instance;
			ReadOnlyCollection<PrimitiveType> storeTypes = instance.GetStoreTypes();
			for (int i = 0; i < storeTypes.Count; i++)
			{
				base.AddInternal(storeTypes[i]);
				this._primitiveTypeMaps.Add(storeTypes[i]);
			}
			ReadOnlyCollection<EdmFunction> storeFunctions = instance.GetStoreFunctions();
			for (int j = 0; j < storeFunctions.Count; j++)
			{
				base.AddInternal(storeFunctions[j]);
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000C13EC File Offset: 0x000BF5EC
		internal DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			if (this._getGeneratedFunctionDefinitionsMemoizer == null)
			{
				Interlocked.CompareExchange<Memoizer<EdmFunction, DbLambda>>(ref this._getGeneratedFunctionDefinitionsMemoizer, new Memoizer<EdmFunction, DbLambda>(new Func<EdmFunction, DbLambda>(this.GenerateFunctionDefinition), null), null);
			}
			return this._getGeneratedFunctionDefinitionsMemoizer.Evaluate(function);
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000C1424 File Offset: 0x000BF624
		internal DbLambda GenerateFunctionDefinition(EdmFunction function)
		{
			if (!function.HasUserDefinedBody)
			{
				throw new InvalidOperationException(Strings.Cqt_UDF_FunctionHasNoDefinition(function.Identity));
			}
			DbLambda dbLambda = ExternalCalls.CompileFunctionDefinition(function.CommandTextAttribute, function.Parameters, this);
			if (!TypeSemantics.IsStructurallyEqual(function.ReturnParameter.TypeUsage, dbLambda.Body.ResultType))
			{
				throw new InvalidOperationException(Strings.Cqt_UDF_FunctionDefinitionResultTypeMismatch(function.ReturnParameter.TypeUsage.ToString(), function.FullName, dbLambda.Body.ResultType.ToString()));
			}
			return dbLambda;
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000C14AC File Offset: 0x000BF6AC
		public static EdmItemCollection Create(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			EdmItemCollection edmItemCollection = new EdmItemCollection(xmlReaders, filePaths, out errors);
			if (errors == null || errors.Count <= 0)
			{
				return edmItemCollection;
			}
			return null;
		}

		// Token: 0x0400141C RID: 5148
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x0400141D RID: 5149
		private double _edmVersion;

		// Token: 0x0400141E RID: 5150
		private Memoizer<InitializerMetadata, InitializerMetadata> _getCanonicalInitializerMetadataMemoizer;

		// Token: 0x0400141F RID: 5151
		private Memoizer<EdmFunction, DbLambda> _getGeneratedFunctionDefinitionsMemoizer;

		// Token: 0x04001420 RID: 5152
		private readonly OcAssemblyCache _conventionalOcCache = new OcAssemblyCache();
	}
}
