using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010E2 RID: 4322
	internal static class NavigationPropertiesHelper
	{
		// Token: 0x06007129 RID: 28969 RVA: 0x00184B1F File Offset: 0x00182D1F
		public static TableValue AddNavigationPropertiesToDatabase(IDictionary<SchemaItem, Value> database, DbEnvironment environment, NavigationPropertiesHelper.NameGenerator nameGenerator)
		{
			return NavigationPropertiesHelper.AddNavigationPropertiesToDatabase(new NavigationPropertiesHelper.NavigationPropertiesRecord(database, environment, nameGenerator), null);
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x00184B30 File Offset: 0x00182D30
		public static TableValue AddNavigationPropertiesToDatabase(NavigationPropertiesHelper.NavigationPropertiesRecord navigationPropertyRecord, string schemaName = null)
		{
			List<IValueReference> list = new List<IValueReference>();
			TableTypeValue tableTypeValue;
			if (schemaName == null)
			{
				foreach (SchemaItem schemaItem3 in navigationPropertyRecord.Keys)
				{
					list.Add(navigationPropertyRecord.CreateDbCatalogEntryRecord(schemaItem3));
				}
				tableTypeValue = NavigationPropertiesHelper.DbNavigationTableTypeValue;
			}
			else
			{
				IEnumerable<SchemaItem> keys = navigationPropertyRecord.Keys;
				Func<SchemaItem, bool> <>9__0;
				Func<SchemaItem, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (SchemaItem schemaItem) => schemaItem.Schema == schemaName);
				}
				foreach (SchemaItem schemaItem2 in keys.Where(func))
				{
					list.Add(navigationPropertyRecord.CreateSchemaCatalogEntryRecord(schemaItem2));
				}
				tableTypeValue = NavigationPropertiesHelper.SchemaNavigationTableTypeValue;
			}
			return new ConnectionTestedTableValue(ListValue.New(list).ToTable(tableTypeValue));
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x00184C2C File Offset: 0x00182E2C
		public static NavigationPropertiesHelper.NameGenerator CreateNameGenerator(FunctionValue nameGeneratorFunction)
		{
			return new NavigationPropertiesHelper.NameGenerator(new NavigationPropertiesHelper.FunctionNameGenerator(nameGeneratorFunction).NameGenerator);
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x00184C40 File Offset: 0x00182E40
		private static List<NavigationPropertiesHelper.SourceTargetLinkInfo> RetrieveLinks(NavigationPropertiesHelper.NavigationPropertiesRecord navigationPropertyRecord, TableValue table, DbEnvironment environment)
		{
			Value value = table.Type.MetaValue["Sql.Schema"];
			string text = (value.IsNull ? null : value.AsString);
			string asString = table.Type.MetaValue["Sql.Table"].AsString;
			List<NavigationPropertiesHelper.SourceTargetLinkInfo> list = new List<NavigationPropertiesHelper.SourceTargetLinkInfo>();
			foreach (RelationshipInfo relationshipInfo in environment.RetrieveIncomingRelationshipsForTable(text, asString))
			{
				Value value2;
				if (!navigationPropertyRecord.TryGetSchemaValue(relationshipInfo.Foreign, out value2) && !environment.GetNavigationPropertiesRecord(new SchemaItem?(relationshipInfo.Foreign)).TryGetSchemaValue(relationshipInfo.Foreign, out value2))
				{
					value2 = null;
				}
				int[] array;
				int[] array2;
				if (value2 != null && value2.Kind == ValueKind.Table && NavigationPropertiesHelper.TryGetKeyIndexes(table.Columns, relationshipInfo.TargetColumns.Values, out array) && NavigationPropertiesHelper.TryGetKeyIndexes(value2.AsTable.Columns, relationshipInfo.ReferringColumns.Values, out array2))
				{
					list.Add(new NavigationPropertiesHelper.SourceTargetLinkInfo(asString, array, Keys.New(relationshipInfo.TargetColumns.Values.ToArray<string>()), new NavigationPropertiesHelper.TargetLinkInfo(navigationPropertyRecord, relationshipInfo.Foreign, false, Keys.New(relationshipInfo.ReferringColumns.Values.ToArray<string>()))));
				}
			}
			foreach (RelationshipInfo relationshipInfo2 in environment.RetrieveOutgoingRelationshipsForTable(text, asString))
			{
				Value value3;
				if (!navigationPropertyRecord.TryGetSchemaValue(relationshipInfo2.Primary, out value3) && !environment.GetNavigationPropertiesRecord(new SchemaItem?(relationshipInfo2.Primary)).TryGetSchemaValue(relationshipInfo2.Primary, out value3))
				{
					value3 = null;
				}
				int[] array3;
				int[] array4;
				if (value3 != null && value3.Kind == ValueKind.Table && NavigationPropertiesHelper.TryGetKeyIndexes(table.Columns, relationshipInfo2.ReferringColumns.Values, out array3) && NavigationPropertiesHelper.TryGetKeyIndexes(value3.AsTable.Columns, relationshipInfo2.TargetColumns.Values, out array4))
				{
					list.Add(new NavigationPropertiesHelper.SourceTargetLinkInfo(asString, array3, Keys.New(relationshipInfo2.ReferringColumns.Values.ToArray<string>()), new NavigationPropertiesHelper.TargetLinkInfo(navigationPropertyRecord, relationshipInfo2.Primary, true, Keys.New(relationshipInfo2.TargetColumns.Values.ToArray<string>()))));
				}
			}
			return list;
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x00184EC0 File Offset: 0x001830C0
		public static string EnsureUniqueName(string candidateName, Keys keys, HashSet<string> addedKeys)
		{
			int num = 2;
			string text = candidateName;
			while (keys.Contains(text) || addedKeys.Contains(text))
			{
				text = candidateName + " " + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			addedKeys.Add(text);
			return text;
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x00184F0C File Offset: 0x0018310C
		public static string[] DefaultNameGenerator(Keys keys, IList<NavigationPropertiesHelper.LinkInfo> links)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int i = 0; i < keys.Length; i++)
			{
				dictionary[keys[i]] = 1;
			}
			for (int j = 0; j < links.Count; j++)
			{
				NavigationPropertiesHelper.LinkInfo linkInfo = links[j];
				int num;
				dictionary.TryGetValue(linkInfo.TargetTableName.Identifier, out num);
				dictionary[linkInfo.TargetTableName.Identifier] = num + 1;
			}
			HashSet<string> hashSet = new HashSet<string>();
			string[] array = new string[links.Count];
			for (int k = 0; k < array.Length; k++)
			{
				NavigationPropertiesHelper.LinkInfo linkInfo2 = links[k];
				string text = linkInfo2.TargetTableName.Identifier;
				if (dictionary[text] > 1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text);
					stringBuilder.Append("(");
					for (int l = 0; l < linkInfo2.SourceKeys.Length; l++)
					{
						if (l > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(linkInfo2.SourceKeys[l]);
					}
					stringBuilder.Append(")");
					text = stringBuilder.ToString();
				}
				string text2 = NavigationPropertiesHelper.EnsureUniqueName(text, keys, hashSet);
				array[k] = text2;
			}
			return array;
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x0018506C File Offset: 0x0018326C
		private static TableValue AddNavigationPropertiesToTable(NavigationPropertiesHelper.NavigationPropertiesRecord record, DbEnvironment environment, TableValue table, NavigationPropertiesHelper.NameGenerator nameGenerator)
		{
			List<NavigationPropertiesHelper.SourceTargetLinkInfo> list;
			try
			{
				list = NavigationPropertiesHelper.RetrieveLinks(record, table, environment);
			}
			catch (DbException ex)
			{
				RecordValue recordValue = DataSourceException.NewDataSourceErrorRecord(environment.Host, ValueException.DataSourceError, string.Format(CultureInfo.CurrentCulture, "{0} {1}: {2}", Strings.SqlRelationshipQueryFailed, environment.DataSourceNameString, ex.Message), null, environment.GetDbExceptionDetails(ex));
				string text = NavigationPropertiesHelper.EnsureUniqueName("Relationships", table.Columns, new HashSet<string>());
				return table.AddColumns(ListValue.New(new Value[] { TextValue.New(text) }), new NavigationPropertiesHelper.LinkErrorFunctionValue(recordValue, ex), ListValue.New(new Value[] { ErrorRecord._Type }));
			}
			list.Sort(new Comparison<NavigationPropertiesHelper.SourceTargetLinkInfo>(NavigationPropertiesHelper.SourceTargetLinkInfo.Comparison));
			NavigationPropertiesHelper.LinkInfo[] array = new NavigationPropertiesHelper.LinkInfo[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			string[] array2 = nameGenerator(table.Columns, array);
			TableValue tableValue = table;
			KeysBuilder keysBuilder = default(KeysBuilder);
			keysBuilder.Union(table.Columns);
			for (int j = 0; j < list.Count; j++)
			{
				NavigationPropertiesHelper.SourceTargetLinkInfo sourceTargetLinkInfo = list[j];
				keysBuilder.Add(array2[j]);
				tableValue = tableValue.NestedJoin(sourceTargetLinkInfo.SourceColumns, new NavigationPropertiesHelper.LinkTableFunctionValue(sourceTargetLinkInfo.Target), sourceTargetLinkInfo.TargetKeys, TableTypeAlgebra.JoinKind.LeftOuter, array2[j], keysBuilder.ToKeys(), null);
				if (sourceTargetLinkInfo.SingleTarget)
				{
					tableValue = tableValue.ExpandListColumn(tableValue.Columns.Length - 1, sourceTargetLinkInfo.SingleTarget);
				}
			}
			return tableValue.NewType(DbTypeServices.PropagateDocumentation(table.Type, tableValue.Type)).AsTable;
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x00185230 File Offset: 0x00183430
		private static bool TryGetKeyIndexes(Keys tableColumns, IEnumerable<string> keyColumns, out int[] indexes)
		{
			List<string> columns = tableColumns.ToList<string>();
			bool valid = true;
			indexes = keyColumns.Select(delegate(string c)
			{
				int num = columns.IndexOf(c);
				if (num < 0)
				{
					valid = false;
				}
				return num;
			}).ToArray<int>();
			if (!valid)
			{
				indexes = null;
			}
			return valid;
		}

		// Token: 0x04003E4C RID: 15948
		public const string NameKey = "Name";

		// Token: 0x04003E4D RID: 15949
		public const string SchemaKey = "Schema";

		// Token: 0x04003E4E RID: 15950
		public const string ItemKey = "Item";

		// Token: 0x04003E4F RID: 15951
		public const string KindKey = "Kind";

		// Token: 0x04003E50 RID: 15952
		public const string DataKey = "Data";

		// Token: 0x04003E51 RID: 15953
		public const string HiddenKey = "Hidden";

		// Token: 0x04003E52 RID: 15954
		public static readonly Keys ExcelSheetNavigationKeys = Keys.New(new string[] { "Name", "Data", "Item", "Kind", "Hidden" });

		// Token: 0x04003E53 RID: 15955
		public static readonly Keys DbNavigationKeys = Keys.New(new string[] { "Name", "Data", "Schema", "Item", "Kind" });

		// Token: 0x04003E54 RID: 15956
		public static readonly Keys SchemaNavigationKeys = Keys.New("Name", "Data", "Kind");

		// Token: 0x04003E55 RID: 15957
		public static readonly TableTypeValue ExcelSheetTableTypeValue = NavigationTableServices.AddNavigationTableMetadataWithKindHidden(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationPropertiesHelper.ExcelSheetNavigationKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NavigationTableServices.ConvertToLink(TypeValue.Table, "Table", true),
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Logical,
				LogicalValue.False
			})
		}), false), new TableKey[]
		{
			new TableKey(new int[] { 2, 3 }, true)
		}));

		// Token: 0x04003E56 RID: 15958
		public static readonly TableTypeValue DbNavigationTableTypeValue = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationPropertiesHelper.DbNavigationKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NavigationTableServices.AddDataColumnIsLeafMetadata(TypeValue.Any),
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			})
		}), false), new TableKey[]
		{
			new TableKey(new int[] { 2, 3 }, true)
		}));

		// Token: 0x04003E57 RID: 15959
		public static readonly TableTypeValue SchemaNavigationTableTypeValue = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationPropertiesHelper.SchemaNavigationKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NavigationTableServices.AddDataColumnIsLeafMetadata(TypeValue.Any),
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			})
		}), false), new TableKey[]
		{
			new TableKey(new int[1], true)
		}));

		// Token: 0x020010E3 RID: 4323
		// (Invoke) Token: 0x06007133 RID: 28979
		public delegate string[] NameGenerator(Keys keys, IList<NavigationPropertiesHelper.LinkInfo> links);

		// Token: 0x020010E4 RID: 4324
		public abstract class LinkInfo
		{
			// Token: 0x17001FCA RID: 8138
			// (get) Token: 0x06007136 RID: 28982
			public abstract string SourceTableName { get; }

			// Token: 0x17001FCB RID: 8139
			// (get) Token: 0x06007137 RID: 28983
			public abstract Keys SourceKeys { get; }

			// Token: 0x17001FCC RID: 8140
			// (get) Token: 0x06007138 RID: 28984
			public abstract SchemaItem TargetTableName { get; }

			// Token: 0x17001FCD RID: 8141
			// (get) Token: 0x06007139 RID: 28985
			public abstract Keys TargetKeys { get; }

			// Token: 0x17001FCE RID: 8142
			// (get) Token: 0x0600713A RID: 28986
			public abstract bool SingleTarget { get; }
		}

		// Token: 0x020010E5 RID: 4325
		private sealed class TargetLinkInfo
		{
			// Token: 0x0600713C RID: 28988 RVA: 0x001855B0 File Offset: 0x001837B0
			public TargetLinkInfo(NavigationPropertiesHelper.NavigationPropertiesRecord tables, SchemaItem tableName, bool single, Keys keys)
			{
				this.tables = tables;
				this.tableName = tableName;
				this.single = single;
				this.keys = keys;
			}

			// Token: 0x17001FCF RID: 8143
			// (get) Token: 0x0600713D RID: 28989 RVA: 0x001855D5 File Offset: 0x001837D5
			public SchemaItem TableName
			{
				get
				{
					return this.tableName;
				}
			}

			// Token: 0x17001FD0 RID: 8144
			// (get) Token: 0x0600713E RID: 28990 RVA: 0x001855DD File Offset: 0x001837DD
			public bool Single
			{
				get
				{
					return this.single;
				}
			}

			// Token: 0x17001FD1 RID: 8145
			// (get) Token: 0x0600713F RID: 28991 RVA: 0x001855E5 File Offset: 0x001837E5
			public Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x17001FD2 RID: 8146
			// (get) Token: 0x06007140 RID: 28992 RVA: 0x001855ED File Offset: 0x001837ED
			public TableValue Table
			{
				get
				{
					return this.tables[this.tableName].AsTable;
				}
			}

			// Token: 0x04003E58 RID: 15960
			private readonly NavigationPropertiesHelper.NavigationPropertiesRecord tables;

			// Token: 0x04003E59 RID: 15961
			private readonly SchemaItem tableName;

			// Token: 0x04003E5A RID: 15962
			private readonly bool single;

			// Token: 0x04003E5B RID: 15963
			private readonly Keys keys;
		}

		// Token: 0x020010E6 RID: 4326
		private sealed class SourceTargetLinkInfo : NavigationPropertiesHelper.LinkInfo
		{
			// Token: 0x06007141 RID: 28993 RVA: 0x00185605 File Offset: 0x00183805
			public SourceTargetLinkInfo(string sourceTableName, int[] sourceColumns, Keys sourceKeys, NavigationPropertiesHelper.TargetLinkInfo target)
			{
				this.sourceTableName = sourceTableName;
				this.sourceColumns = sourceColumns;
				this.sourceKeys = sourceKeys;
				this.target = target;
			}

			// Token: 0x17001FD3 RID: 8147
			// (get) Token: 0x06007142 RID: 28994 RVA: 0x0018562A File Offset: 0x0018382A
			public override string SourceTableName
			{
				get
				{
					return this.sourceTableName;
				}
			}

			// Token: 0x17001FD4 RID: 8148
			// (get) Token: 0x06007143 RID: 28995 RVA: 0x00185632 File Offset: 0x00183832
			public override Keys SourceKeys
			{
				get
				{
					return this.sourceKeys;
				}
			}

			// Token: 0x17001FD5 RID: 8149
			// (get) Token: 0x06007144 RID: 28996 RVA: 0x0018563A File Offset: 0x0018383A
			public override SchemaItem TargetTableName
			{
				get
				{
					return this.target.TableName;
				}
			}

			// Token: 0x17001FD6 RID: 8150
			// (get) Token: 0x06007145 RID: 28997 RVA: 0x00185647 File Offset: 0x00183847
			public override Keys TargetKeys
			{
				get
				{
					return this.target.Keys;
				}
			}

			// Token: 0x17001FD7 RID: 8151
			// (get) Token: 0x06007146 RID: 28998 RVA: 0x00185654 File Offset: 0x00183854
			public override bool SingleTarget
			{
				get
				{
					return this.target.Single;
				}
			}

			// Token: 0x17001FD8 RID: 8152
			// (get) Token: 0x06007147 RID: 28999 RVA: 0x00185661 File Offset: 0x00183861
			public int[] SourceColumns
			{
				get
				{
					return this.sourceColumns;
				}
			}

			// Token: 0x17001FD9 RID: 8153
			// (get) Token: 0x06007148 RID: 29000 RVA: 0x00185669 File Offset: 0x00183869
			public NavigationPropertiesHelper.TargetLinkInfo Target
			{
				get
				{
					return this.target;
				}
			}

			// Token: 0x06007149 RID: 29001 RVA: 0x00185674 File Offset: 0x00183874
			public static int Comparison(NavigationPropertiesHelper.SourceTargetLinkInfo x, NavigationPropertiesHelper.SourceTargetLinkInfo y)
			{
				int num = x.TargetTableName.CompareTo(y.TargetTableName, StringComparison.Ordinal);
				if (num == 0)
				{
					num = x.SourceKeys.Length.CompareTo(y.SourceKeys.Length);
				}
				if (num == 0)
				{
					int num2 = 0;
					while (num == 0 && num2 < x.SourceKeys.Length)
					{
						num = string.Compare(x.SourceKeys[num2], y.SourceKeys[num2], StringComparison.Ordinal);
						num2++;
					}
					int num3 = 0;
					while (num == 0 && num3 < x.TargetKeys.Length)
					{
						num = string.Compare(x.TargetKeys[num3], y.TargetKeys[num3], StringComparison.Ordinal);
						num3++;
					}
				}
				return num;
			}

			// Token: 0x04003E5C RID: 15964
			private readonly string sourceTableName;

			// Token: 0x04003E5D RID: 15965
			private readonly int[] sourceColumns;

			// Token: 0x04003E5E RID: 15966
			private readonly Keys sourceKeys;

			// Token: 0x04003E5F RID: 15967
			private readonly NavigationPropertiesHelper.TargetLinkInfo target;
		}

		// Token: 0x020010E7 RID: 4327
		private sealed class LinkTableFunctionValue : NativeFunctionValue0<TableValue>
		{
			// Token: 0x0600714A RID: 29002 RVA: 0x00185733 File Offset: 0x00183933
			public LinkTableFunctionValue(NavigationPropertiesHelper.TargetLinkInfo link)
				: base(TypeValue.Table)
			{
				this.link = link;
			}

			// Token: 0x0600714B RID: 29003 RVA: 0x00185747 File Offset: 0x00183947
			public override TableValue TypedInvoke()
			{
				return this.link.Table;
			}

			// Token: 0x04003E60 RID: 15968
			private readonly NavigationPropertiesHelper.TargetLinkInfo link;
		}

		// Token: 0x020010E8 RID: 4328
		private sealed class LinkErrorFunctionValue : NativeFunctionValue1<RecordValue, Value>
		{
			// Token: 0x0600714C RID: 29004 RVA: 0x00185754 File Offset: 0x00183954
			public LinkErrorFunctionValue(RecordValue error, Exception innerException)
				: base(ErrorRecord._Type, 0, "row", TypeValue.Any)
			{
				this.error = error;
				this.innerException = innerException;
			}

			// Token: 0x0600714D RID: 29005 RVA: 0x0018577A File Offset: 0x0018397A
			public override RecordValue TypedInvoke(Value value)
			{
				throw ValueException.New(this.error, this.innerException);
			}

			// Token: 0x04003E61 RID: 15969
			private readonly RecordValue error;

			// Token: 0x04003E62 RID: 15970
			private readonly Exception innerException;
		}

		// Token: 0x020010E9 RID: 4329
		private sealed class FunctionNameGenerator
		{
			// Token: 0x0600714E RID: 29006 RVA: 0x0018578D File Offset: 0x0018398D
			public FunctionNameGenerator(FunctionValue function)
			{
				this.function = function;
			}

			// Token: 0x0600714F RID: 29007 RVA: 0x0018579C File Offset: 0x0018399C
			public string[] NameGenerator(Keys keys, IList<NavigationPropertiesHelper.LinkInfo> links)
			{
				ListValue listValue = NavigationPropertiesHelper.FunctionNameGenerator.CreateTextList(keys);
				Value[] array = new Value[links.Count];
				for (int i = 0; i < array.Length; i++)
				{
					NavigationPropertiesHelper.LinkInfo link = links[i];
					array[i] = RecordValue.New(NavigationPropertiesHelper.FunctionNameGenerator.linkInfoKeys, delegate(int fieldIndex)
					{
						switch (fieldIndex)
						{
						case 0:
							return TextValue.New(link.SourceTableName);
						case 1:
							return NavigationPropertiesHelper.FunctionNameGenerator.CreateTextList(link.SourceKeys);
						case 2:
							return TextValue.New(link.TargetTableName.Identifier);
						case 3:
							return NavigationPropertiesHelper.FunctionNameGenerator.CreateTextList(link.TargetKeys);
						default:
							return LogicalValue.New(link.SingleTarget);
						}
					});
				}
				ListValue asList = this.function.Invoke(listValue, ListValue.New(array)).AsList;
				string[] array2 = new string[asList.Count];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = asList[j].AsString;
				}
				return array2;
			}

			// Token: 0x06007150 RID: 29008 RVA: 0x00185848 File Offset: 0x00183A48
			private static ListValue CreateTextList(Keys keys)
			{
				Value[] array = new Value[keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TextValue.New(keys[i]);
				}
				return ListValue.New(array);
			}

			// Token: 0x04003E63 RID: 15971
			private static readonly Keys linkInfoKeys = Keys.New(new string[] { "SourceTableName", "SourceKeys", "TargetTableName", "TargetKeys", "SingleTarget" });

			// Token: 0x04003E64 RID: 15972
			private readonly FunctionValue function;
		}

		// Token: 0x020010EB RID: 4331
		public sealed class NavigationPropertiesRecord
		{
			// Token: 0x06007154 RID: 29012 RVA: 0x00185941 File Offset: 0x00183B41
			public NavigationPropertiesRecord(IDictionary<SchemaItem, Value> baseValues, DbEnvironment environment, NavigationPropertiesHelper.NameGenerator nameGenerator)
			{
				this.baseValues = baseValues;
				this.environment = environment;
				this.nameGenerator = nameGenerator;
			}

			// Token: 0x17001FDA RID: 8154
			// (get) Token: 0x06007155 RID: 29013 RVA: 0x00185969 File Offset: 0x00183B69
			public IEnumerable<SchemaItem> Keys
			{
				get
				{
					return this.baseValues.Keys;
				}
			}

			// Token: 0x17001FDB RID: 8155
			public Value this[SchemaItem key]
			{
				get
				{
					Value value;
					if (!this.values.TryGetValue(key, out value))
					{
						Value value2 = this.baseValues[key];
						if (value2.Kind == ValueKind.Table)
						{
							if (value2.AsTable.Columns.Length == 0)
							{
								value = TableValue.Empty;
							}
							else
							{
								value = NavigationPropertiesHelper.AddNavigationPropertiesToTable(this, this.environment, value2.AsTable, this.nameGenerator);
							}
						}
						else
						{
							value = value2;
						}
						this.values.Add(key, value);
					}
					return value;
				}
			}

			// Token: 0x06007157 RID: 29015 RVA: 0x001859F1 File Offset: 0x00183BF1
			public bool TryGetSchemaValue(SchemaItem item, out Value value)
			{
				return this.baseValues.TryGetValue(item, out value);
			}

			// Token: 0x06007158 RID: 29016 RVA: 0x00185A00 File Offset: 0x00183C00
			public RecordValue CreateDbCatalogEntryRecord(SchemaItem key)
			{
				return NavigationPropertiesHelper.NavigationPropertiesRecord.AddNavigationKeysIfTable(RecordValue.New(NavigationPropertiesHelper.DbNavigationKeys, delegate(int j)
				{
					switch (j)
					{
					case 0:
						return TextValue.New(key.Identifier);
					case 1:
						return TypeServices.ConvertToLimitedPreview(this[key]);
					case 2:
						return TextValue.New(key.Schema);
					case 3:
						return TextValue.New(key.Item);
					default:
						return TextValue.New(key.Kind);
					}
				}), this.baseValues[key]);
			}

			// Token: 0x06007159 RID: 29017 RVA: 0x00185A50 File Offset: 0x00183C50
			public RecordValue CreateSchemaCatalogEntryRecord(SchemaItem key)
			{
				return NavigationPropertiesHelper.NavigationPropertiesRecord.AddNavigationKeysIfTable(RecordValue.New(NavigationPropertiesHelper.SchemaNavigationKeys, delegate(int j)
				{
					if (j == 0)
					{
						return TextValue.New(key.Item);
					}
					if (j != 1)
					{
						return TextValue.New(key.Kind);
					}
					return TypeServices.ConvertToLimitedPreview(this[key]);
				}), this.baseValues[key]);
			}

			// Token: 0x0600715A RID: 29018 RVA: 0x00185A9D File Offset: 0x00183C9D
			private static RecordValue AddNavigationKeysIfTable(RecordValue record, Value value)
			{
				if (value.IsTable)
				{
					return record.NewMeta(RecordValue.New(NavigationPropertiesHelper.NavigationPropertiesRecord.NavigationTableKeys, new Value[] { NavigationTableServices.DataColumnValue })).AsRecord;
				}
				return record;
			}

			// Token: 0x04003E66 RID: 15974
			private const string NavigationTableMetadata = "NavigationTable";

			// Token: 0x04003E67 RID: 15975
			private static readonly Keys NavigationTableKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("NavigationTable");

			// Token: 0x04003E68 RID: 15976
			private readonly IDictionary<SchemaItem, Value> baseValues;

			// Token: 0x04003E69 RID: 15977
			private readonly DbEnvironment environment;

			// Token: 0x04003E6A RID: 15978
			private readonly IDictionary<SchemaItem, Value> values = new Dictionary<SchemaItem, Value>();

			// Token: 0x04003E6B RID: 15979
			private readonly NavigationPropertiesHelper.NameGenerator nameGenerator;
		}
	}
}
