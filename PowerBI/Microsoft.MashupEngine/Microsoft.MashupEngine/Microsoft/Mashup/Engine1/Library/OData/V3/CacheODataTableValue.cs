using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008AA RID: 2218
	internal class CacheODataTableValue : TableValue
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x000D0FCB File Offset: 0x000CF1CB
		public CacheODataTableValue(ODataEnvironment environment, TableTypeValue type, Uri startPageUri, bool unwrapFoldingExceptions)
		{
			this.environment = environment;
			this.type = type;
			this.startPageUri = startPageUri;
			this.unwrapFoldingExceptions = unwrapFoldingExceptions;
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000D0FF0 File Offset: 0x000CF1F0
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000D0FF8 File Offset: 0x000CF1F8
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			if (this.table == null)
			{
				this.table = new CacheODataTableValue.ODataQueryCache(this.environment, this.unwrapFoldingExceptions).CreateTable(this.environment.Host, this.type, this.startPageUri);
			}
			return this.table.GetEnumerator();
		}

		// Token: 0x0400214C RID: 8524
		private readonly ODataEnvironment environment;

		// Token: 0x0400214D RID: 8525
		private readonly TableTypeValue type;

		// Token: 0x0400214E RID: 8526
		private readonly Uri startPageUri;

		// Token: 0x0400214F RID: 8527
		private readonly bool unwrapFoldingExceptions;

		// Token: 0x04002150 RID: 8528
		private TableValue table;

		// Token: 0x020008AB RID: 2219
		private struct ODataQueryCache
		{
			// Token: 0x06003F6D RID: 16237 RVA: 0x000D104E File Offset: 0x000CF24E
			public ODataQueryCache(ODataEnvironment environment, bool unwrapFoldingExceptions)
			{
				this.environment = environment;
				this.cache = environment.Host.GetPersistentCache();
				this.unwrapFoldingExceptions = unwrapFoldingExceptions;
			}

			// Token: 0x06003F6E RID: 16238 RVA: 0x000D1070 File Offset: 0x000CF270
			public TableValue CreateTable(IEngineHost host, TableTypeValue type, Uri startPageUri)
			{
				Uri serviceUri = this.environment.ServiceUri;
				QueryDescriptorQueryToken queryDescriptorQueryToken;
				if (ODataUri.TryParseODataUri(host, this.environment.Resource, startPageUri, serviceUri, out queryDescriptorQueryToken))
				{
					if (queryDescriptorQueryToken.Select == null)
					{
						IList<string> singleColumnQueries = CacheODataTableValue.ODataQueryCache.GetSingleColumnQueries(queryDescriptorQueryToken, serviceUri, type.ItemType.Fields.Keys);
						Uri uri;
						Keys keys;
						if (!this.TryGetCachedQuery(singleColumnQueries, out uri, out keys))
						{
							this.AddCachedQuery(startPageUri.AbsoluteUri, type.ItemType.Fields.Keys, singleColumnQueries);
						}
					}
					else
					{
						Keys columns = CacheODataTableValue.ODataQueryCache.GetColumns(queryDescriptorQueryToken.Select);
						if (columns != null)
						{
							IList<string> singleColumnQueries2 = CacheODataTableValue.ODataQueryCache.GetSingleColumnQueries(queryDescriptorQueryToken, serviceUri, columns);
							Keys keys2 = null;
							Uri canonicalQuery;
							if (!this.TryGetCachedQuery(singleColumnQueries2, out canonicalQuery, out keys2))
							{
								canonicalQuery = CacheODataTableValue.ODataQueryCache.GetCanonicalQuery(queryDescriptorQueryToken, serviceUri, columns);
								if (ODataUri.TryParseODataUri(host, this.environment.Resource, canonicalQuery, serviceUri, out queryDescriptorQueryToken))
								{
									keys2 = CacheODataTableValue.ODataQueryCache.GetColumns(queryDescriptorQueryToken.Select);
									this.AddCachedQuery(canonicalQuery.AbsoluteUri, keys2, singleColumnQueries2);
								}
							}
							if (keys2 != null)
							{
								TableTypeValue tableTypeValue = CacheODataTableValue.ODataQueryCache.CreateType(keys2, type);
								return new ODataTableValue(this.environment, tableTypeValue, canonicalQuery, this.unwrapFoldingExceptions).SelectColumns(ListValue.New(type.ItemType.Fields.Keys.ToArray<string>()), Value.Null);
							}
						}
					}
				}
				return new ODataTableValue(this.environment, type, startPageUri, this.unwrapFoldingExceptions);
			}

			// Token: 0x06003F6F RID: 16239 RVA: 0x000D11C4 File Offset: 0x000CF3C4
			private static TableTypeValue CreateType(Keys columns, TableTypeValue type)
			{
				RecordBuilder recordBuilder = new RecordBuilder(columns.Length);
				RecordValue fields = type.ItemType.Fields;
				foreach (string text in columns)
				{
					Value value;
					if (!fields.TryGetValue(text, out value))
					{
						value = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Any,
							LogicalValue.False
						});
					}
					recordBuilder.Add(text, value, TypeValue.Any);
				}
				foreach (Microsoft.Mashup.Engine1.Runtime.NamedValue namedValue in type.ItemType.Fields.GetFields())
				{
					if (!columns.Contains(namedValue.Key))
					{
						recordBuilder.Add(namedValue.Key, namedValue.Value, TypeValue.Any);
					}
				}
				return TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()));
			}

			// Token: 0x06003F70 RID: 16240 RVA: 0x000D12E8 File Offset: 0x000CF4E8
			private bool TryGetCachedQuery(IEnumerable<string> singleColumnQueries, out Uri uri, out Keys canonicalColumns)
			{
				uri = null;
				canonicalColumns = null;
				string text = null;
				foreach (string text2 in singleColumnQueries)
				{
					string text3;
					if (!this.TryGetValue(text2, out text3))
					{
						return false;
					}
					if (text3 != text)
					{
						if (text != null)
						{
							return false;
						}
						text = text3;
					}
				}
				if (!this.TryGetCanonicalColumns(text, out canonicalColumns))
				{
					return false;
				}
				uri = new Uri(text);
				return true;
			}

			// Token: 0x06003F71 RID: 16241 RVA: 0x000D136C File Offset: 0x000CF56C
			private bool TryGetValue(string singleColumnQuery, out string canonicalQuery)
			{
				string key = CacheODataTableValue.ODataQueryCache.GetKey(singleColumnQuery);
				Stream stream;
				if (this.cache.TryGetValue(key, out stream))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						canonicalQuery = binaryReader.ReadString();
						return true;
					}
				}
				canonicalQuery = null;
				return false;
			}

			// Token: 0x06003F72 RID: 16242 RVA: 0x000D13C8 File Offset: 0x000CF5C8
			private void AddCachedQuery(string canonicalQuery, Keys canonicalColumns, IEnumerable<string> singleColumnQueries)
			{
				string canonicalKey = CacheODataTableValue.ODataQueryCache.GetCanonicalKey(canonicalQuery);
				Stream stream = this.cache.BeginAdd();
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(canonicalColumns.Length);
				for (int i = 0; i < canonicalColumns.Length; i++)
				{
					binaryWriter.Write(canonicalColumns[i]);
				}
				this.cache.EndAdd(canonicalKey, stream).Close();
				foreach (string text in singleColumnQueries)
				{
					string key = CacheODataTableValue.ODataQueryCache.GetKey(text);
					Stream stream2 = this.cache.BeginAdd();
					new BinaryWriter(stream2).Write(canonicalQuery);
					this.cache.EndAdd(key, stream2).Close();
				}
			}

			// Token: 0x06003F73 RID: 16243 RVA: 0x000D14A4 File Offset: 0x000CF6A4
			private bool TryGetCanonicalColumns(string canonicalQuery, out Keys canonicalColumns)
			{
				string canonicalKey = CacheODataTableValue.ODataQueryCache.GetCanonicalKey(canonicalQuery);
				Stream stream;
				if (!this.cache.TryGetValue(canonicalKey, out stream))
				{
					canonicalColumns = null;
					return false;
				}
				bool flag;
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					string[] array = new string[binaryReader.ReadInt32()];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = binaryReader.ReadString();
					}
					canonicalColumns = Keys.New(array);
					flag = true;
				}
				return flag;
			}

			// Token: 0x06003F74 RID: 16244 RVA: 0x000D152C File Offset: 0x000CF72C
			private static Keys GetColumns(SelectQueryToken selectToken)
			{
				KeysBuilder keysBuilder = new KeysBuilder(selectToken.Properties.Count<QueryToken>());
				foreach (QueryToken queryToken in selectToken.Properties)
				{
					if (queryToken.Kind != QueryTokenKind.PropertyAccess)
					{
						return null;
					}
					PropertyAccessQueryToken propertyAccessQueryToken = (PropertyAccessQueryToken)queryToken;
					if (propertyAccessQueryToken.Parent != null)
					{
						return null;
					}
					keysBuilder.Add(EdmNameEncoder.Decode(propertyAccessQueryToken.Name));
				}
				return keysBuilder.ToKeys();
			}

			// Token: 0x06003F75 RID: 16245 RVA: 0x000D15C4 File Offset: 0x000CF7C4
			private static Uri GetCanonicalQuery(QueryDescriptorQueryToken queryToken, Uri baseUri, Keys columns)
			{
				QueryDescriptorQueryToken queryDescriptorQueryToken = new QueryDescriptorQueryToken(queryToken.Path, queryToken.Filter, queryToken.OrderByTokens, new SelectQueryToken(from x in columns
					orderby x
					select new PropertyAccessQueryToken(EdmNameEncoder.Encode(x), null)), queryToken.Expand, queryToken.Skip, queryToken.Top, queryToken.InlineCount, queryToken.Format, queryToken.QueryOptions);
				return MashupODataUriBuilder.CreateUri(baseUri, queryDescriptorQueryToken);
			}

			// Token: 0x06003F76 RID: 16246 RVA: 0x000D1664 File Offset: 0x000CF864
			private static IList<string> GetSingleColumnQueries(QueryDescriptorQueryToken queryToken, Uri baseUri, Keys columns)
			{
				string[] array = new string[columns.Length];
				for (int i = 0; i < columns.Length; i++)
				{
					QueryDescriptorQueryToken queryDescriptorQueryToken = new QueryDescriptorQueryToken(queryToken.Path, queryToken.Filter, queryToken.OrderByTokens, new SelectQueryToken(new QueryToken[]
					{
						new PropertyAccessQueryToken(EdmNameEncoder.Encode(columns[i]), null)
					}), queryToken.Expand, queryToken.Skip, queryToken.Top, queryToken.InlineCount, queryToken.Format, queryToken.QueryOptions);
					array[i] = MashupODataUriBuilder.CreateUri(baseUri, queryDescriptorQueryToken).AbsoluteUri;
				}
				return array;
			}

			// Token: 0x06003F77 RID: 16247 RVA: 0x000D16FC File Offset: 0x000CF8FC
			private static string GetKey(string singleColumnQuery)
			{
				return PersistentCacheKey.ODataQuery.Qualify("Query", singleColumnQuery);
			}

			// Token: 0x06003F78 RID: 16248 RVA: 0x000D171C File Offset: 0x000CF91C
			private static string GetCanonicalKey(string canonicalUri)
			{
				return PersistentCacheKey.ODataQuery.Qualify("Columns", canonicalUri);
			}

			// Token: 0x04002151 RID: 8529
			private readonly ODataEnvironment environment;

			// Token: 0x04002152 RID: 8530
			private readonly IPersistentCache cache;

			// Token: 0x04002153 RID: 8531
			private readonly bool unwrapFoldingExceptions;
		}
	}
}
