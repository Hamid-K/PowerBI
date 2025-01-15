using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000574 RID: 1396
	internal abstract class OleDbDataSourceInfo : IDataSourceCapabilities
	{
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06002C69 RID: 11369
		public abstract string Provider { get; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06002C6A RID: 11370
		public abstract int? DataSourceType { get; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06002C6B RID: 11371
		public abstract IDictionary<Guid, int> SupportedSchemas { get; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06002C6C RID: 11372
		public abstract IDictionary<OleDbLiteral, DbLiteralInfo> LiteralInfo { get; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06002C6D RID: 11373
		public abstract int? CatalogLocation { get; }

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x00087384 File Offset: 0x00085584
		public virtual bool IsAce
		{
			get
			{
				return this.Provider.StartsWith("Microsoft.Jet.OLEDB", StringComparison.OrdinalIgnoreCase) || this.Provider.StartsWith("Microsoft.ACE.OLEDB", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000873AC File Offset: 0x000855AC
		public virtual bool SupportsOLAP
		{
			get
			{
				int? num = this.DataSourceType;
				int num2 = 2;
				if (!((num.GetValueOrDefault() == num2) & (num != null)))
				{
					num = this.DataSourceType;
					num2 = 3;
					return (num.GetValueOrDefault() == num2) & (num != null);
				}
				return true;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000873F3 File Offset: 0x000855F3
		public bool SupportsCatalogNames
		{
			get
			{
				return this.LiteralInfo.ContainsKey(OleDbLiteral.Catalog_Name);
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x00087401 File Offset: 0x00085601
		public bool SupportsSchemaNames
		{
			get
			{
				return this.LiteralInfo.ContainsKey(OleDbLiteral.Schema_Name);
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x00087410 File Offset: 0x00085610
		public bool SupportsForeignKeys
		{
			get
			{
				return this.SupportsSchema(OleDbSchemaGuid.Foreign_Keys, Array.Empty<int>());
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x00002105 File Offset: 0x00000305
		public bool SupportsStoredProcedures
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00002105 File Offset: 0x00000305
		public bool SupportsStoredFunctions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x00087422 File Offset: 0x00085622
		public string CatalogSeperator
		{
			get
			{
				return this.GetLiteralStringOrDefault(OleDbLiteral.Catalog_Separator);
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x0008742B File Offset: 0x0008562B
		public string SchemaSeperator
		{
			get
			{
				return this.GetLiteralStringOrDefault(OleDbLiteral.Schema_Separator);
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x00087435 File Offset: 0x00085635
		public string QuotePrefix
		{
			get
			{
				return this.GetLiteralStringOrDefault(OleDbLiteral.Quote_Prefix);
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x00087440 File Offset: 0x00085640
		public string QuoteSuffix
		{
			get
			{
				string literalStringOrDefault = this.GetLiteralStringOrDefault(OleDbLiteral.Quote_Suffix);
				if (literalStringOrDefault == null || literalStringOrDefault == this.InvalidLiteral)
				{
					return this.QuotePrefix;
				}
				return literalStringOrDefault;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x0008746F File Offset: 0x0008566F
		private string InvalidLiteral
		{
			get
			{
				return this.GetLiteralStringOrDefault(OleDbLiteral.Invalid);
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00087478 File Offset: 0x00085678
		public bool SupportsSchema(Guid schemaGuid, params int[] restrictionColumns)
		{
			int num;
			if (!this.SupportedSchemas.TryGetValue(schemaGuid, out num))
			{
				return false;
			}
			int num2 = 0;
			foreach (int num3 in restrictionColumns)
			{
				num2 |= 1 << num3;
			}
			return (num & num2) == num2;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000874BE File Offset: 0x000856BE
		public static OleDbDataSourceInfo Load(IOleDbDataSource dataSource)
		{
			return new OleDbDataSourceInfo.OleDbMemoryDataSourceInfo(new OleDbDataSourceInfo.OleDbProviderDataSourceInfo(dataSource));
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000874CC File Offset: 0x000856CC
		private string GetLiteralStringOrDefault(OleDbLiteral literal)
		{
			DbLiteralInfo valueOrDefault = this.LiteralInfo.GetValueOrDefault(literal, null);
			if (valueOrDefault == null)
			{
				return null;
			}
			return valueOrDefault.LiteralValue;
		}

		// Token: 0x04001350 RID: 4944
		private const string JetProvider = "Microsoft.Jet.OLEDB";

		// Token: 0x04001351 RID: 4945
		private const string AceProvider = "Microsoft.ACE.OLEDB";

		// Token: 0x02000575 RID: 1397
		private sealed class OleDbMemoryDataSourceInfo : OleDbDataSourceInfo
		{
			// Token: 0x06002C7E RID: 11390 RVA: 0x000874F4 File Offset: 0x000856F4
			public OleDbMemoryDataSourceInfo(OleDbDataSourceInfo dataSourceInfo)
			{
				this.provider = dataSourceInfo.Provider;
				this.dataSourceType = dataSourceInfo.DataSourceType;
				this.catalogLocation = dataSourceInfo.CatalogLocation;
				this.supportedSchemas = dataSourceInfo.SupportedSchemas;
				this.literalInfo = dataSourceInfo.LiteralInfo;
				this.isAce = dataSourceInfo.IsAce;
				this.supportsOLAP = dataSourceInfo.SupportsOLAP;
			}

			// Token: 0x17001079 RID: 4217
			// (get) Token: 0x06002C7F RID: 11391 RVA: 0x0008755B File Offset: 0x0008575B
			public override string Provider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x1700107A RID: 4218
			// (get) Token: 0x06002C80 RID: 11392 RVA: 0x00087563 File Offset: 0x00085763
			public override int? DataSourceType
			{
				get
				{
					return this.dataSourceType;
				}
			}

			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x06002C81 RID: 11393 RVA: 0x0008756B File Offset: 0x0008576B
			public override int? CatalogLocation
			{
				get
				{
					return this.catalogLocation;
				}
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x06002C82 RID: 11394 RVA: 0x00087573 File Offset: 0x00085773
			public override IDictionary<Guid, int> SupportedSchemas
			{
				get
				{
					return this.supportedSchemas;
				}
			}

			// Token: 0x1700107D RID: 4221
			// (get) Token: 0x06002C83 RID: 11395 RVA: 0x0008757B File Offset: 0x0008577B
			public override IDictionary<OleDbLiteral, DbLiteralInfo> LiteralInfo
			{
				get
				{
					return this.literalInfo;
				}
			}

			// Token: 0x1700107E RID: 4222
			// (get) Token: 0x06002C84 RID: 11396 RVA: 0x00087583 File Offset: 0x00085783
			public override bool IsAce
			{
				get
				{
					return this.isAce;
				}
			}

			// Token: 0x1700107F RID: 4223
			// (get) Token: 0x06002C85 RID: 11397 RVA: 0x0008758B File Offset: 0x0008578B
			public override bool SupportsOLAP
			{
				get
				{
					return this.supportsOLAP;
				}
			}

			// Token: 0x04001352 RID: 4946
			private readonly string provider;

			// Token: 0x04001353 RID: 4947
			private readonly int? dataSourceType;

			// Token: 0x04001354 RID: 4948
			private readonly int? catalogLocation;

			// Token: 0x04001355 RID: 4949
			private readonly IDictionary<Guid, int> supportedSchemas;

			// Token: 0x04001356 RID: 4950
			private readonly IDictionary<OleDbLiteral, DbLiteralInfo> literalInfo;

			// Token: 0x04001357 RID: 4951
			private readonly bool isAce;

			// Token: 0x04001358 RID: 4952
			private readonly bool supportsOLAP;
		}

		// Token: 0x02000576 RID: 1398
		private sealed class OleDbProviderDataSourceInfo : OleDbDataSourceInfo
		{
			// Token: 0x06002C86 RID: 11398 RVA: 0x00087593 File Offset: 0x00085793
			public OleDbProviderDataSourceInfo(IOleDbDataSource dataSource)
			{
				this.dataSource = dataSource;
			}

			// Token: 0x17001080 RID: 4224
			// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000875A2 File Offset: 0x000857A2
			public override string Provider
			{
				get
				{
					return this.dataSource.Provider;
				}
			}

			// Token: 0x17001081 RID: 4225
			// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000875B0 File Offset: 0x000857B0
			public override int? DataSourceType
			{
				get
				{
					object obj;
					if (this.dataSource.TryGetProperty(DBPROPGROUP.DataSourceInfo, DBPROPID.DATASOURCE_TYPE, out obj))
					{
						return new int?((int)obj);
					}
					return null;
				}
			}

			// Token: 0x17001082 RID: 4226
			// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000875EC File Offset: 0x000857EC
			public override IDictionary<Guid, int> SupportedSchemas
			{
				get
				{
					return this.ToDictionaryDistinct<DataRow, Guid, int>(this.dataSource.GetSchemas().Rows.Cast<DataRow>(), (DataRow row) => (Guid)row["Schema"], (DataRow row) => (int)row["RestrictionSupport"]);
				}
			}

			// Token: 0x17001083 RID: 4227
			// (get) Token: 0x06002C8A RID: 11402 RVA: 0x00087654 File Offset: 0x00085854
			public override IDictionary<OleDbLiteral, DbLiteralInfo> LiteralInfo
			{
				get
				{
					return this.ToDictionaryDistinct<DbLiteralInfo, OleDbLiteral>(from DataRow row in this.dataSource.GetLiteralInfo().Rows
						select new DbLiteralInfo((DBLITERAL)((int)row["Literal"]), row["LiteralValue"] as string, row["InvalidChars"] as string, row["InvalidStartingChars"] as string, (int)row["Maxlen"]), (DbLiteralInfo info) => (OleDbLiteral)info.Literal);
				}
			}

			// Token: 0x17001084 RID: 4228
			// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000876C0 File Offset: 0x000858C0
			public override int? CatalogLocation
			{
				get
				{
					object obj;
					if (this.dataSource.TryGetProperty(DBPROPGROUP.DataSourceInfo, DBPROPID.CATALOGLOCATION, out obj))
					{
						return new int?((int)obj);
					}
					return null;
				}
			}

			// Token: 0x06002C8C RID: 11404 RVA: 0x000876F8 File Offset: 0x000858F8
			private Dictionary<TKey, TSource> ToDictionaryDistinct<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			{
				return this.ToDictionaryDistinct<TSource, TKey, TSource>(source, keySelector, (TSource element) => element);
			}

			// Token: 0x06002C8D RID: 11405 RVA: 0x00087724 File Offset: 0x00085924
			private Dictionary<TKey, TElement> ToDictionaryDistinct<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
			{
				Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();
				foreach (TSource tsource in source)
				{
					TKey tkey = keySelector(tsource);
					TElement telement = elementSelector(tsource);
					TElement telement2;
					if (dictionary.TryGetValue(tkey, out telement2))
					{
						if (!EqualityComparer<TElement>.Default.Equals(telement, telement2))
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.OleDbMultipleMetadataValuesError(this.Provider), Value.Null, null);
						}
					}
					else
					{
						dictionary.Add(tkey, telement);
					}
				}
				return dictionary;
			}

			// Token: 0x04001359 RID: 4953
			private readonly IOleDbDataSource dataSource;
		}
	}
}
