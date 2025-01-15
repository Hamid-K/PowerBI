using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Text;
using System.Xml;
using Microsoft.Data.Common;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000082 RID: 130
	public sealed class EdmItemCollection : ItemCollection
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x00017402 File Offset: 0x00015602
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, ReadOnlyCollection<string> filePaths, out IList<EdmSchemaError> errors)
			: base(DataSpace.CSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			errors = this.Init(xmlReaders, filePaths, false);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001743A File Offset: 0x0001563A
		internal EdmItemCollection(IList<Schema> schemas)
			: base(DataSpace.CSpace)
		{
			this.Init();
			EdmItemCollection.LoadItems(MetadataItem.EdmProviderManifest, schemas, this);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00017461 File Offset: 0x00015661
		internal EdmItemCollection(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths)
			: base(DataSpace.CSpace)
		{
			this.Init(xmlReaders, filePaths, true);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00017480 File Offset: 0x00015680
		public EdmItemCollection(IEnumerable<XmlReader> xmlReaders)
			: base(DataSpace.CSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000174D3 File Offset: 0x000156D3
		internal EdmItemCollection(MetadataCollection<GlobalItem> items)
			: base(DataSpace.CSpace, items)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000174E8 File Offset: 0x000156E8
		private void Init()
		{
			this.LoadEdmPrimitiveTypesAndFunctions();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000174F0 File Offset: 0x000156F0
		private IList<EdmSchemaError> Init(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> filePaths, bool throwOnError)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			this.Init();
			return EdmItemCollection.LoadItems(xmlReaders, filePaths, SchemaDataModelOption.EntityDataModel, MetadataItem.EdmProviderManifest, this, throwOnError);
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00017513 File Offset: 0x00015713
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0001751B File Offset: 0x0001571B
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

		// Token: 0x060009C4 RID: 2500 RVA: 0x00017524 File Offset: 0x00015724
		internal static bool IsSystemNamespace(DbProviderManifest manifest, string namespaceName)
		{
			if (manifest == MetadataItem.EdmProviderManifest)
			{
				return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
			}
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System" || (manifest != null && namespaceName == manifest.NamespaceName);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001759C File Offset: 0x0001579C
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

		// Token: 0x060009C6 RID: 2502 RVA: 0x00017624 File Offset: 0x00015824
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

		// Token: 0x060009C7 RID: 2503 RVA: 0x00017754 File Offset: 0x00015954
		internal static IEnumerable<GlobalItem> LoadSomSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			return Converter.ConvertSchema(somSchemas, providerManifest, itemCollection);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001775E File Offset: 0x0001595E
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001776C File Offset: 0x0001596C
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = null;
			this._primitiveTypeMaps.TryGetType(primitiveTypeKind, null, out primitiveType);
			return primitiveType;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001778C File Offset: 0x0001598C
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

		// Token: 0x0400080E RID: 2062
		private CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x0400080F RID: 2063
		private double _edmVersion;
	}
}
