using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005EC RID: 1516
	public abstract class DbXmlEnabledProviderManifest : DbProviderManifest
	{
		// Token: 0x06004A2A RID: 18986 RVA: 0x00106E0C File Offset: 0x0010500C
		protected DbXmlEnabledProviderManifest(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ProviderIncompatibleException(Strings.IncorrectProviderManifest, new ArgumentNullException("reader"));
			}
			this.Load(reader);
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x00106E5F File Offset: 0x0010505F
		public override string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x00106E67 File Offset: 0x00105067
		protected Dictionary<string, PrimitiveType> StoreTypeNameToEdmPrimitiveType
		{
			get
			{
				return this._storeTypeNameToEdmPrimitiveType;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x00106E6F File Offset: 0x0010506F
		protected Dictionary<string, PrimitiveType> StoreTypeNameToStorePrimitiveType
		{
			get
			{
				return this._storeTypeNameToStorePrimitiveType;
			}
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00106E77 File Offset: 0x00105077
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType)
		{
			return DbXmlEnabledProviderManifest.GetReadOnlyCollection<FacetDescription>(edmType as PrimitiveType, this._facetDescriptions, Helper.EmptyFacetDescriptionEnumerable);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00106E8F File Offset: 0x0010508F
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			return this._primitiveTypes;
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x00106E97 File Offset: 0x00105097
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return this._functions;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00106EA0 File Offset: 0x001050A0
		private void Load(XmlReader reader)
		{
			Schema schema;
			IList<EdmSchemaError> list = SchemaManager.LoadProviderManifest(reader, (reader.BaseURI.Length > 0) ? reader.BaseURI : null, true, out schema);
			if (list.Count != 0)
			{
				throw new ProviderIncompatibleException(Strings.IncorrectProviderManifest + Helper.CombineErrorMessage(list));
			}
			this._namespaceName = schema.Namespace;
			List<PrimitiveType> list2 = new List<PrimitiveType>();
			foreach (SchemaType schemaType in schema.SchemaTypes)
			{
				TypeElement typeElement = schemaType as TypeElement;
				if (typeElement != null)
				{
					PrimitiveType primitiveType = typeElement.PrimitiveType;
					primitiveType.ProviderManifest = this;
					primitiveType.DataSpace = DataSpace.SSpace;
					primitiveType.SetReadOnly();
					list2.Add(primitiveType);
					this._storeTypeNameToStorePrimitiveType.Add(primitiveType.Name.ToLowerInvariant(), primitiveType);
					this._storeTypeNameToEdmPrimitiveType.Add(primitiveType.Name.ToLowerInvariant(), EdmProviderManifest.Instance.GetPrimitiveType(primitiveType.PrimitiveTypeKind));
					ReadOnlyCollection<FacetDescription> readOnlyCollection;
					if (DbXmlEnabledProviderManifest.EnumerableToReadOnlyCollection<FacetDescription, FacetDescription>(typeElement.FacetDescriptions, out readOnlyCollection))
					{
						this._facetDescriptions.Add(primitiveType, readOnlyCollection);
					}
				}
			}
			this._primitiveTypes = new ReadOnlyCollection<PrimitiveType>(list2.ToArray());
			ItemCollection itemCollection = new DbXmlEnabledProviderManifest.EmptyItemCollection();
			if (!DbXmlEnabledProviderManifest.EnumerableToReadOnlyCollection<EdmFunction, GlobalItem>(Converter.ConvertSchema(schema, this, itemCollection), out this._functions))
			{
				this._functions = Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			foreach (EdmFunction edmFunction in this._functions)
			{
				edmFunction.SetReadOnly();
			}
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00107050 File Offset: 0x00105250
		private static ReadOnlyCollection<T> GetReadOnlyCollection<T>(PrimitiveType type, Dictionary<PrimitiveType, ReadOnlyCollection<T>> typeDictionary, ReadOnlyCollection<T> useIfEmpty)
		{
			ReadOnlyCollection<T> readOnlyCollection;
			if (typeDictionary.TryGetValue(type, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return useIfEmpty;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0010706C File Offset: 0x0010526C
		private static bool EnumerableToReadOnlyCollection<Target, BaseType>(IEnumerable<BaseType> enumerable, out ReadOnlyCollection<Target> collection) where Target : BaseType
		{
			List<Target> list = new List<Target>();
			foreach (BaseType baseType in enumerable)
			{
				if (typeof(Target) == typeof(BaseType) || baseType is Target)
				{
					list.Add((Target)((object)baseType));
				}
			}
			if (list.Count != 0)
			{
				collection = new ReadOnlyCollection<Target>(list);
				return true;
			}
			collection = null;
			return false;
		}

		// Token: 0x04001A25 RID: 6693
		private string _namespaceName;

		// Token: 0x04001A26 RID: 6694
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04001A27 RID: 6695
		private readonly Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();

		// Token: 0x04001A28 RID: 6696
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04001A29 RID: 6697
		private readonly Dictionary<string, PrimitiveType> _storeTypeNameToEdmPrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x04001A2A RID: 6698
		private readonly Dictionary<string, PrimitiveType> _storeTypeNameToStorePrimitiveType = new Dictionary<string, PrimitiveType>();

		// Token: 0x02000C34 RID: 3124
		private class EmptyItemCollection : ItemCollection
		{
			// Token: 0x060069E1 RID: 27105 RVA: 0x0016A8BC File Offset: 0x00168ABC
			public EmptyItemCollection()
				: base(DataSpace.SSpace)
			{
			}
		}
	}
}
