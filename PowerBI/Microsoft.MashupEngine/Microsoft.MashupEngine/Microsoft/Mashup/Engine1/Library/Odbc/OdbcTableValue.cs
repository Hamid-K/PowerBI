using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200066C RID: 1644
	internal static class OdbcTableValue
	{
		// Token: 0x060033D2 RID: 13266 RVA: 0x000A5EF8 File Offset: 0x000A40F8
		public static TableValue New(OdbcQueryDomain queryDomain, OdbcTableInfo tableInfo, bool createNavigationProperties)
		{
			return new OdbcTableValue.OdbcTestConnectionTableValue(queryDomain.DataSource, new OdbcTableValue.OdbcTableValueBuilder(queryDomain, createNavigationProperties).CreateTableValue(tableInfo), tableInfo.Identifier.Catalog);
		}

		// Token: 0x0200066D RID: 1645
		private sealed class OdbcTestConnectionTableValue : DelegatingTableValue
		{
			// Token: 0x060033D3 RID: 13267 RVA: 0x000A5F1D File Offset: 0x000A411D
			public OdbcTestConnectionTableValue(OdbcDataSource dataSource, TableValue tableValue, string catalog)
				: base(tableValue)
			{
				this.dataSource = dataSource;
				this.catalog = catalog;
			}

			// Token: 0x060033D4 RID: 13268 RVA: 0x000A5F34 File Offset: 0x000A4134
			public override void TestConnection()
			{
				this.dataSource.TestConnectionAndGetVersion(this.catalog);
			}

			// Token: 0x04001721 RID: 5921
			private readonly OdbcDataSource dataSource;

			// Token: 0x04001722 RID: 5922
			private readonly string catalog;
		}

		// Token: 0x0200066E RID: 1646
		private class OdbcTableValueBuilder
		{
			// Token: 0x060033D5 RID: 13269 RVA: 0x000A5F48 File Offset: 0x000A4148
			public OdbcTableValueBuilder(OdbcQueryDomain queryDomain, bool createNavigationProperties)
			{
				this.queryDomain = queryDomain;
				this.createNavigationProperties = createNavigationProperties;
			}

			// Token: 0x060033D6 RID: 13270 RVA: 0x000A5F60 File Offset: 0x000A4160
			public TableValue CreateTableValue(OdbcTableInfo tableInfo)
			{
				OdbcQuery odbcQuery = this.queryDomain.NewQuery(tableInfo, null);
				if (odbcQuery.Columns.Length == 0)
				{
					return ListValue.New(DeferredEnumerable.From<IValueReference>(delegate
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.TableHasNoVisibleColumns, TextValue.New(tableInfo.Identifier.Name), null);
					})).ToTable(TypeValue.Table);
				}
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(odbcQuery.Columns);
				TableValue tableValue = new QueryTableValue(new OptimizableQuery(odbcQuery));
				if (this.createNavigationProperties)
				{
					List<OdbcLinkInfo> list = new List<OdbcLinkInfo>();
					foreach (OdbcRelationship odbcRelationship in tableInfo.GetReferencedTables())
					{
						OdbcLinkInfo odbcLinkInfo;
						if (OdbcTableValue.OdbcTableValueBuilder.TryCreateLinkInfo(this.queryDomain, tableInfo, odbcRelationship, true, new Func<OdbcTableInfo, TableValue>(this.CreateTableValue), out odbcLinkInfo))
						{
							list.Add(odbcLinkInfo);
						}
					}
					foreach (OdbcRelationship odbcRelationship2 in tableInfo.GetReferencingTables())
					{
						OdbcLinkInfo odbcLinkInfo2;
						if (OdbcTableValue.OdbcTableValueBuilder.TryCreateLinkInfo(this.queryDomain, tableInfo, odbcRelationship2, false, new Func<OdbcTableInfo, TableValue>(this.CreateTableValue), out odbcLinkInfo2))
						{
							list.Add(odbcLinkInfo2);
						}
					}
					OdbcTableValue.OdbcTableValueBuilder.EnsureUniqueNames(odbcQuery.Columns, list);
					list.Sort();
					foreach (OdbcLinkInfo odbcLinkInfo3 in list)
					{
						keysBuilder.Add(odbcLinkInfo3.Name);
						tableValue = tableValue.NestedJoin(odbcLinkInfo3.SourceColumns, odbcLinkInfo3.LinkTable, odbcLinkInfo3.TargetKeys, TableTypeAlgebra.JoinKind.LeftOuter, odbcLinkInfo3.Name, keysBuilder.ToKeys(), null);
						if (odbcLinkInfo3.IsSingleTarget)
						{
							tableValue = tableValue.ExpandListColumn(tableValue.Columns.Length - 1, true);
						}
					}
				}
				return tableValue.ReplaceRelationshipIdentity(tableInfo.UniqueIdentifier);
			}

			// Token: 0x060033D7 RID: 13271 RVA: 0x000A6184 File Offset: 0x000A4384
			private static bool TryCreateLinkInfo(OdbcQueryDomain queryDomain, OdbcTableInfo sourceTable, OdbcRelationship relationship, bool isSingleTarget, Func<OdbcTableInfo, TableValue> tableLoader, out OdbcLinkInfo linkInfo)
			{
				int[] array = new int[relationship.SourceKeys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					OdbcColumnInfo odbcColumnInfo;
					if (!sourceTable.Columns.TryGetColumnInfo(relationship.SourceKeys[i], out odbcColumnInfo))
					{
						linkInfo = null;
						return false;
					}
					array[i] = odbcColumnInfo.Ordinal;
				}
				OdbcTableInfo orCreateTableInfo = queryDomain.DataSource.GetOrCreateTableInfo(relationship.TargetTable, null);
				string name = relationship.TargetTable.Name;
				int[] array2 = array;
				linkInfo = new OdbcLinkInfo(name, relationship.SourceKeys, array2, orCreateTableInfo, relationship.TargetKeys, isSingleTarget, tableLoader);
				return true;
			}

			// Token: 0x060033D8 RID: 13272 RVA: 0x000A6218 File Offset: 0x000A4418
			private static void EnsureUniqueNames(Keys keys, List<OdbcLinkInfo> links)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(keys.Length);
				for (int i = 0; i < keys.Length; i++)
				{
					dictionary[keys[i]] = 1;
				}
				for (int j = 0; j < links.Count; j++)
				{
					OdbcLinkInfo odbcLinkInfo = links[j];
					int num;
					dictionary.TryGetValue(odbcLinkInfo.TargetTable.Identifier.Name, out num);
					dictionary[odbcLinkInfo.TargetTable.Identifier.Name] = num + 1;
				}
				HashSet<string> hashSet = new HashSet<string>();
				foreach (OdbcLinkInfo odbcLinkInfo2 in links)
				{
					string text = odbcLinkInfo2.TargetTable.Identifier.Name;
					if (dictionary[text] > 1)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(text);
						stringBuilder.Append("(");
						for (int k = 0; k < odbcLinkInfo2.SourceKeys.Length; k++)
						{
							if (k > 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append(odbcLinkInfo2.SourceKeys[k]);
						}
						stringBuilder.Append(")");
						text = stringBuilder.ToString();
					}
					string text2 = NavigationPropertiesHelper.EnsureUniqueName(text, keys, hashSet);
					odbcLinkInfo2.Name = text2;
				}
			}

			// Token: 0x04001723 RID: 5923
			private readonly OdbcQueryDomain queryDomain;

			// Token: 0x04001724 RID: 5924
			private readonly bool createNavigationProperties;
		}
	}
}
