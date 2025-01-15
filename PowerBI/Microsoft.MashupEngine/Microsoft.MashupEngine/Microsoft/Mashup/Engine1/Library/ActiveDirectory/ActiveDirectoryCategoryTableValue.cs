using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FBF RID: 4031
	internal sealed class ActiveDirectoryCategoryTableValue : TableValue
	{
		// Token: 0x060069DE RID: 27102 RVA: 0x0016C344 File Offset: 0x0016A544
		private ActiveDirectoryCategoryTableValue(string domainName, ActiveDirectoryEnvironment environment, string objectCategory, ActiveDirectoryColumnInfo[] columnInfos, Keys columns, SetFilter filter, SortOption sortOption, TableSortOrder tableSortOrder, RowCount rowCount, Func<RecordValue, bool> localFilter, Dictionary<string, string> localFilterColumns, TableValue unoptimized)
		{
			this.domainName = domainName;
			this.environment = environment;
			this.objectCategory = objectCategory;
			this.columnInfos = columnInfos;
			this.columns = columns;
			this.filter = filter;
			this.sortOption = sortOption;
			this.tableSortOrder = tableSortOrder;
			this.rowCount = rowCount;
			this.localFilter = localFilter;
			this.localFilterColumns = localFilterColumns;
			this.unoptimized = unoptimized;
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x060069DF RID: 27103 RVA: 0x0016C3B4 File Offset: 0x0016A5B4
		public override TypeValue Type
		{
			get
			{
				this.EnsureInitialized();
				return this.type;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x0016C3C2 File Offset: 0x0016A5C2
		public override Keys Columns
		{
			get
			{
				this.EnsureInitialized();
				return this.columns;
			}
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x060069E1 RID: 27105 RVA: 0x0016C3D0 File Offset: 0x0016A5D0
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.tableSortOrder;
			}
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x060069E2 RID: 27106 RVA: 0x0016C3D8 File Offset: 0x0016A5D8
		private TableValue Unoptimized
		{
			get
			{
				return this.unoptimized ?? new QueryTableValue(new TableQuery(this, this.environment.EngineHost));
			}
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0016C3FC File Offset: 0x0016A5FC
		public override TableValue SelectRows(FunctionValue condition)
		{
			this.EnsureInitialized();
			ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue;
			if (this.TrySelectRows(condition, out activeDirectoryCategoryTableValue))
			{
				return activeDirectoryCategoryTableValue;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0016C424 File Offset: 0x0016A624
		private bool TrySelectRows(FunctionValue condition, out ActiveDirectoryCategoryTableValue tableValue)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
			if (queryExpression == null || !this.rowCount.IsInfinite)
			{
				tableValue = null;
				return false;
			}
			tableValue = ActiveDirectoryCategoryTableValue.New(this, this.Unoptimized.SelectRows(condition));
			Filter filter;
			bool flag;
			if (new LdapSearchFilterCompiler(this.environment, this.objectCategory, this.columnInfos).TryBuild(queryExpression, out filter, out flag))
			{
				tableValue.filter = this.filter.AddOperand(filter);
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				tableValue.localFilter = delegate(RecordValue row)
				{
					if (!this.localFilter(row))
					{
						return false;
					}
					Value value = condition.Invoke(row);
					return !value.IsNull && value.AsBoolean;
				};
				ActiveDirectoryCategoryTableValue.ColumnAccessCollector columnAccessCollector = new ActiveDirectoryCategoryTableValue.ColumnAccessCollector();
				columnAccessCollector.Visit(queryExpression);
				foreach (int num in columnAccessCollector.ColumnsAccessed)
				{
					ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columnInfos[num];
					if (activeDirectoryColumnInfo.ColumnType != ColumnType.Attribute)
					{
						tableValue = null;
						return false;
					}
					tableValue.localFilterColumns[this.Columns[num]] = activeDirectoryColumnInfo.AttributeNames[0];
				}
				return true;
			}
			return true;
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x0016C574 File Offset: 0x0016A774
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			this.EnsureInitialized();
			Keys keys = columnSelection.Keys;
			ActiveDirectoryColumnInfo[] array = new ActiveDirectoryColumnInfo[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				array[i] = this.columnInfos[column];
			}
			ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue = ActiveDirectoryCategoryTableValue.New(this, new QueryTableValue(SelectColumnsQuery.New(columnSelection, this.Unoptimized.Query)));
			activeDirectoryCategoryTableValue.columnInfos = array;
			activeDirectoryCategoryTableValue.columns = keys;
			table = activeDirectoryCategoryTableValue;
			return true;
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x0016C5F0 File Offset: 0x0016A7F0
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue;
			if (this.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out activeDirectoryCategoryTableValue))
			{
				return activeDirectoryCategoryTableValue;
			}
			return base.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x0016C618 File Offset: 0x0016A818
		private bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out ActiveDirectoryCategoryTableValue newTable)
		{
			this.EnsureInitialized();
			ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columnInfos[columnToExpand];
			if (activeDirectoryColumnInfo.ColumnType == ColumnType.AttributeGroup)
			{
				ActiveDirectoryColumnInfo[] array = new ActiveDirectoryColumnInfo[this.columnInfos.Length - 1 + fieldsToProject.Length];
				string[] array2 = new string[array.Length];
				for (int i = 0; i < this.columnInfos.Length; i++)
				{
					if (i < columnToExpand)
					{
						array[i] = this.columnInfos[i];
						array2[i] = this.columns[i];
					}
					else if (i == columnToExpand)
					{
						for (int j = 0; j < fieldsToProject.Length; j++)
						{
							string text = newColumns[j];
							string text2 = fieldsToProject[j];
							if (!activeDirectoryColumnInfo.AttributeNames.Contains(text2, StringComparer.Ordinal) || this.localFilterColumns.ContainsKey(text))
							{
								newTable = null;
								return false;
							}
							int num = i + j;
							array[num] = ActiveDirectoryColumnInfo.CreateAttributeColumn(text2, false);
							array2[num] = text;
						}
					}
					else
					{
						int num2 = i + fieldsToProject.Length - 1;
						array[num2] = this.columnInfos[i];
						array2[num2] = this.columns[i];
					}
				}
				newTable = ActiveDirectoryCategoryTableValue.New(this, this.Unoptimized.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
				newTable.columnInfos = array;
				newTable.columns = Keys.New(array2);
				return true;
			}
			newTable = null;
			return false;
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x0016C768 File Offset: 0x0016A968
		public override TableValue Sort(TableSortOrder tableSortOrder)
		{
			this.EnsureInitialized();
			QueryExpression[] array;
			bool[] array2;
			if (this.rowCount.IsInfinite && SortQuery.TryGetSelectors(tableSortOrder, this.Type.AsTableType.ItemType, out array, out array2))
			{
				if (array.Length == 0)
				{
					ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue = ActiveDirectoryCategoryTableValue.New(this, this.Unoptimized.Sort(tableSortOrder));
					activeDirectoryCategoryTableValue.tableSortOrder = tableSortOrder;
					activeDirectoryCategoryTableValue.sortOption = new SortOption();
					return activeDirectoryCategoryTableValue;
				}
				if (array.Length == 1)
				{
					QueryExpression queryExpression = array[0];
					if (array[0].Kind == QueryExpressionKind.ColumnAccess)
					{
						ColumnAccessQueryExpression columnAccessQueryExpression = (ColumnAccessQueryExpression)array[0];
						ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columnInfos[columnAccessQueryExpression.Column];
						if (activeDirectoryColumnInfo.ColumnType == ColumnType.Attribute)
						{
							string text = activeDirectoryColumnInfo.AttributeNames[0];
							if (this.sortOption.PropertyName == null || this.sortOption.PropertyName == text)
							{
								ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue2 = ActiveDirectoryCategoryTableValue.New(this, this.Unoptimized.Sort(tableSortOrder));
								activeDirectoryCategoryTableValue2.tableSortOrder = tableSortOrder;
								activeDirectoryCategoryTableValue2.sortOption = new SortOption
								{
									PropertyName = text,
									Direction = (tableSortOrder.SortOrders[0].Ascending ? SortDirection.Ascending : SortDirection.Descending)
								};
								return activeDirectoryCategoryTableValue2;
							}
						}
					}
				}
			}
			return base.Sort(tableSortOrder);
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x0016C88C File Offset: 0x0016AA8C
		public override TableValue Take(RowCount count)
		{
			if (this.localFilterColumns.Count > 0 || count.IsZero)
			{
				return base.Take(count);
			}
			if (count.IsInfinite || (!this.rowCount.IsInfinite && this.rowCount.Value <= count.Value))
			{
				return this;
			}
			ActiveDirectoryCategoryTableValue activeDirectoryCategoryTableValue = ActiveDirectoryCategoryTableValue.New(this, this.Unoptimized.Take(count));
			activeDirectoryCategoryTableValue.rowCount = count;
			return activeDirectoryCategoryTableValue;
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x0016C8FD File Offset: 0x0016AAFD
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new ActiveDirectoryCategoryTableValue.RecoveringEnumerator(this.environment.Service.Host, this.GetRows().GetEnumerator(), this.unoptimized, this.environment.Service.Resource);
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x0016C935 File Offset: 0x0016AB35
		private IEnumerable<IValueReference> GetRows()
		{
			this.EnsureInitialized();
			ArrayBuilder<string> arrayBuilder = default(ArrayBuilder<string>);
			HashSet<string> baseSearchAttributes = new HashSet<string>();
			string[] array;
			KeyValuePair<string, string>[] array2;
			this.GetAttributesToLoad(out array, out array2);
			foreach (string text in array)
			{
				if (this.environment.TypeCatalog.GetAttribute(text).IsConstructed)
				{
					baseSearchAttributes.Add(text);
				}
				else
				{
					arrayBuilder.Add(text);
				}
			}
			RecordTypeValue rowType = this.Type.AsTableType.ItemType.AsRecordType;
			IValueReference[] filterOnlyFields = new IValueReference[array2.Length];
			string[] filterOnlyAttributes = new string[array2.Length];
			KeysBuilder keysBuilder = new KeysBuilder(rowType.Fields.Keys.Length + filterOnlyFields.Length);
			foreach (string text2 in rowType.Fields.Keys)
			{
				keysBuilder.Add(text2);
			}
			for (int j = 0; j < filterOnlyFields.Length; j++)
			{
				KeyValuePair<string, string> keyValuePair = array2[j];
				filterOnlyFields[j] = TextValue.New(keyValuePair.Key);
				filterOnlyAttributes[j] = keyValuePair.Value;
				keysBuilder.Add(keyValuePair.Key);
			}
			ListValue filterOnlyFieldsListValue = ListValue.New(filterOnlyFields);
			Keys allFieldsKeys = keysBuilder.ToKeys();
			LdapPath searchRoot = LdapPath.GetDomainRoot(this.domainName);
			IEnumerable<ActiveDirectoryServiceSearchResult> enumerable = this.environment.Service.FindAll(searchRoot, this.filter.ToString(), this.sortOption, this.rowCount, arrayBuilder.ToArray());
			long count = 0L;
			foreach (ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult in enumerable)
			{
				RecordValue recordValue = RecordValue.New(allFieldsKeys, new Func<int, Value>(new ActiveDirectoryCategoryTableValue.RowValueProvider(this, searchRoot.Host, filterOnlyAttributes, baseSearchAttributes, activeDirectoryServiceSearchResult).GetValue));
				if (this.localFilter(recordValue))
				{
					if (filterOnlyFields.Length != 0)
					{
						recordValue = Library.Record.RemoveFields.Invoke(recordValue, filterOnlyFieldsListValue).AsRecord;
					}
					yield return recordValue.NewType(rowType);
					long num = count;
					count = num + 1L;
					if (!this.rowCount.IsInfinite && count >= this.rowCount.Value)
					{
						break;
					}
				}
			}
			IEnumerator<ActiveDirectoryServiceSearchResult> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x0016C948 File Offset: 0x0016AB48
		private Value[] CreateRow(string searchHost, ActiveDirectoryServiceSearchResult subtreeSearchResult, HashSet<string> baseSearchAttributes, string[] filterOnlyAttributes)
		{
			Value[] array = new Value[this.columnInfos.Length + filterOnlyAttributes.Length];
			ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult = null;
			for (int i = 0; i < array.Length; i++)
			{
				string text;
				bool flag;
				if (i < this.columnInfos.Length)
				{
					ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columnInfos[i];
					if (activeDirectoryColumnInfo.ColumnType == ColumnType.Attribute)
					{
						text = activeDirectoryColumnInfo.AttributeNames[0];
						flag = true;
					}
					else
					{
						text = null;
						flag = false;
					}
				}
				else
				{
					flag = true;
					text = filterOnlyAttributes[i - this.columnInfos.Length];
				}
				Value value;
				if (flag)
				{
					ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult2;
					if (baseSearchAttributes.Contains(text))
					{
						if (activeDirectoryServiceSearchResult == null)
						{
							activeDirectoryServiceSearchResult = this.environment.Service.GetObject(searchHost, subtreeSearchResult.DistinguishedName, baseSearchAttributes.ToArray<string>());
						}
						activeDirectoryServiceSearchResult2 = activeDirectoryServiceSearchResult;
					}
					else
					{
						activeDirectoryServiceSearchResult2 = subtreeSearchResult;
					}
					value = this.environment.ValueBuilder.CreateValue(this.objectCategory, text, activeDirectoryServiceSearchResult2, searchHost, subtreeSearchResult.DistinguishedName).Value;
				}
				else
				{
					ActiveDirectoryColumnInfo activeDirectoryColumnInfo2 = this.columnInfos[i];
					ActiveDirectoryCategoryTableValue.DelayedRecordValues delayedRecordValues = new ActiveDirectoryCategoryTableValue.DelayedRecordValues(searchHost, this.environment, activeDirectoryColumnInfo2.AttributeNames, subtreeSearchResult.DistinguishedName, this.objectCategory);
					value = RecordValue.New(this.type.AsTableType.ItemType.Fields[i].AsRecord["Type"].AsType.AsRecordType, new Func<int, Value>(delayedRecordValues.GetValue));
				}
				array[i] = value;
			}
			return array;
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x0016CAA0 File Offset: 0x0016ACA0
		private void GetAttributesToLoad(out string[] allAttributes, out KeyValuePair<string, string>[] extraColumns)
		{
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("distinguishedName");
			for (int i = 0; i < this.columnInfos.Length; i++)
			{
				ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.columnInfos[i];
				if (activeDirectoryColumnInfo.ColumnType == ColumnType.Attribute)
				{
					string text = activeDirectoryColumnInfo.AttributeNames[0];
					hashSet.Add(text);
				}
			}
			ArrayBuilder<KeyValuePair<string, string>> arrayBuilder = default(ArrayBuilder<KeyValuePair<string, string>>);
			foreach (KeyValuePair<string, string> keyValuePair in this.localFilterColumns)
			{
				hashSet.Add(keyValuePair.Value);
				if (!this.Columns.Contains(keyValuePair.Key))
				{
					arrayBuilder.Add(keyValuePair);
				}
			}
			allAttributes = hashSet.ToArray<string>();
			extraColumns = arrayBuilder.ToArray();
		}

		// Token: 0x060069EE RID: 27118 RVA: 0x0016CB7C File Offset: 0x0016AD7C
		private void EnsureInitialized()
		{
			if (this.columnInfos == null)
			{
				ActiveDirectoryCategoryTableValue.CreateColumns(this.environment, this.objectCategory, out this.columns, out this.columnInfos);
			}
			if (this.type == null)
			{
				IList<TableKey> list = ActiveDirectoryCategoryTableValue.CreateTableKeys(this.columnInfos);
				TypeValue[] array = ActiveDirectoryCategoryTableValue.CreateColumnTypes(this.environment, this.objectCategory, this.columns, this.columnInfos);
				this.type = ActiveDirectoryCategoryTableValue.CreateTableType(this.environment, this.columns, this.columnInfos, list, array);
			}
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x0016CC00 File Offset: 0x0016AE00
		public static ActiveDirectoryCategoryTableValue New(ActiveDirectoryEnvironment environment, string objectCategoryName, string domainName)
		{
			return new ActiveDirectoryCategoryTableValue(domainName, environment, objectCategoryName, null, null, new SetFilter(BooleanOperator.And, new Filter[]
			{
				new AttributeValueAssertionFilter("objectCategory", RelationalOperator.Equal, AttributeValue.Escaped(objectCategoryName))
			}), new SortOption(), TableSortOrder.None, RowCount.Infinite, (RecordValue r) => true, new Dictionary<string, string>(), null);
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x0016CC6C File Offset: 0x0016AE6C
		private static ActiveDirectoryCategoryTableValue New(ActiveDirectoryCategoryTableValue tableValue, TableValue unoptimized)
		{
			return new ActiveDirectoryCategoryTableValue(tableValue.domainName, tableValue.environment, tableValue.objectCategory, tableValue.columnInfos, tableValue.columns, tableValue.filter, tableValue.sortOption, tableValue.tableSortOrder, tableValue.rowCount, tableValue.localFilter, new Dictionary<string, string>(tableValue.localFilterColumns), unoptimized);
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x0016CCC8 File Offset: 0x0016AEC8
		private static void CreateColumns(ActiveDirectoryEnvironment environment, string objectCategory, out Keys columns, out ActiveDirectoryColumnInfo[] columnInfos)
		{
			string[] objectClassNames = environment.TypeCatalog.GetObjectClassNames(objectCategory);
			int num = objectClassNames.Length + 2;
			columnInfos = new ActiveDirectoryColumnInfo[num];
			string[] array = new string[num];
			string keyColumn = "distinguishedName";
			columnInfos[columnInfos.Length - 1] = ActiveDirectoryColumnInfo.CreateAttributeColumn(keyColumn, true);
			array[array.Length - 1] = keyColumn;
			columnInfos[0] = ActiveDirectoryColumnInfo.CreateAttributeColumn("displayName", false);
			array[0] = "displayName";
			Func<string, bool> <>9__0;
			for (int i = 0; i < objectClassNames.Length; i++)
			{
				string text = objectClassNames[i];
				string[] array2 = environment.TypeCatalog.GetObjectClass(text).AttributeNames;
				IEnumerable<string> enumerable = array2;
				Func<string, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (string s) => s != keyColumn && s != "displayName");
				}
				array2 = enumerable.Where(func).ToArray<string>();
				columnInfos[i + 1] = ActiveDirectoryColumnInfo.CreateAttributeGroup(array2);
				array[i + 1] = text;
			}
			columns = Keys.New(array);
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x0016CDBC File Offset: 0x0016AFBC
		private static TypeValue[] CreateColumnTypes(ActiveDirectoryEnvironment environment, string objectCategory, Keys columns, ActiveDirectoryColumnInfo[] columnInfos)
		{
			TypeValue[] array = new TypeValue[columnInfos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				ActiveDirectoryColumnInfo activeDirectoryColumnInfo = columnInfos[i];
				ColumnType columnType = activeDirectoryColumnInfo.ColumnType;
				TypeValue typeValue;
				if (columnType != ColumnType.AttributeGroup)
				{
					if (columnType != ColumnType.Attribute)
					{
						throw new InvalidOperationException();
					}
					typeValue = environment.ValueBuilder.CreateAttributeTypeValue(activeDirectoryColumnInfo.AttributeNames[0]);
				}
				else
				{
					Keys keys = Keys.New(activeDirectoryColumnInfo.AttributeNames);
					typeValue = environment.ValueBuilder.CreateRecordTypeValue(objectCategory, keys);
				}
				array[i] = typeValue;
			}
			return array;
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x0016CE34 File Offset: 0x0016B034
		private static TableTypeValue CreateTableType(ActiveDirectoryEnvironment environment, Keys columns, ActiveDirectoryColumnInfo[] columnInfos, IList<TableKey> tableKeys, TypeValue[] columnTypes)
		{
			Value[] array = new Value[columnInfos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					columnTypes[i],
					LogicalValue.False
				});
			}
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(columns, array)), tableKeys);
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x0016CE8C File Offset: 0x0016B08C
		private static IList<TableKey> CreateTableKeys(ActiveDirectoryColumnInfo[] columnInfos)
		{
			List<TableKey> list = new List<TableKey>();
			for (int i = 0; i < columnInfos.Length; i++)
			{
				if (columnInfos[i].IsPrimaryKey)
				{
					list.Add(new TableKey(new int[] { i }, true));
				}
			}
			return list.ToArray();
		}

		// Token: 0x04003AAD RID: 15021
		private readonly string domainName;

		// Token: 0x04003AAE RID: 15022
		private readonly ActiveDirectoryEnvironment environment;

		// Token: 0x04003AAF RID: 15023
		private readonly string objectCategory;

		// Token: 0x04003AB0 RID: 15024
		private readonly TableValue unoptimized;

		// Token: 0x04003AB1 RID: 15025
		private SetFilter filter;

		// Token: 0x04003AB2 RID: 15026
		private SortOption sortOption;

		// Token: 0x04003AB3 RID: 15027
		private TableSortOrder tableSortOrder;

		// Token: 0x04003AB4 RID: 15028
		private ActiveDirectoryColumnInfo[] columnInfos;

		// Token: 0x04003AB5 RID: 15029
		private Keys columns;

		// Token: 0x04003AB6 RID: 15030
		private RowCount rowCount;

		// Token: 0x04003AB7 RID: 15031
		private Func<RecordValue, bool> localFilter;

		// Token: 0x04003AB8 RID: 15032
		private Dictionary<string, string> localFilterColumns;

		// Token: 0x04003AB9 RID: 15033
		private TypeValue type;

		// Token: 0x02000FC0 RID: 4032
		private sealed class DelayedRecordValues
		{
			// Token: 0x060069F5 RID: 27125 RVA: 0x0016CED3 File Offset: 0x0016B0D3
			public DelayedRecordValues(string searchHost, ActiveDirectoryEnvironment environment, string[] columns, string distinguishedName, string objectCategory)
			{
				this.searchHost = searchHost;
				this.columns = columns;
				this.environment = environment;
				this.distinguishedName = distinguishedName;
				this.objectCategory = objectCategory;
			}

			// Token: 0x060069F6 RID: 27126 RVA: 0x0016CF00 File Offset: 0x0016B100
			public Value GetValue(int index)
			{
				if (this.result == null)
				{
					this.result = this.environment.Service.GetObject(this.searchHost, this.distinguishedName, this.columns);
				}
				return this.environment.ValueBuilder.CreateValue(this.objectCategory, this.columns[index], this.result, this.searchHost, this.distinguishedName).Value;
			}

			// Token: 0x04003ABA RID: 15034
			private readonly string searchHost;

			// Token: 0x04003ABB RID: 15035
			private readonly string[] columns;

			// Token: 0x04003ABC RID: 15036
			private readonly ActiveDirectoryEnvironment environment;

			// Token: 0x04003ABD RID: 15037
			private readonly string distinguishedName;

			// Token: 0x04003ABE RID: 15038
			private readonly string objectCategory;

			// Token: 0x04003ABF RID: 15039
			private ActiveDirectoryServiceSearchResult result;
		}

		// Token: 0x02000FC1 RID: 4033
		private sealed class ColumnAccessCollector : QueryExpressionVisitor
		{
			// Token: 0x17001E6D RID: 7789
			// (get) Token: 0x060069F7 RID: 27127 RVA: 0x0016CF72 File Offset: 0x0016B172
			// (set) Token: 0x060069F8 RID: 27128 RVA: 0x0016CF7A File Offset: 0x0016B17A
			public HashSet<int> ColumnsAccessed { get; set; }

			// Token: 0x060069F9 RID: 27129 RVA: 0x0016CF83 File Offset: 0x0016B183
			public ColumnAccessCollector()
			{
				this.ColumnsAccessed = new HashSet<int>();
			}

			// Token: 0x060069FA RID: 27130 RVA: 0x0016CF96 File Offset: 0x0016B196
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				this.ColumnsAccessed.Add(columnAccess.Column);
				return base.VisitColumnAccess(columnAccess);
			}
		}

		// Token: 0x02000FC2 RID: 4034
		private sealed class RowValueProvider
		{
			// Token: 0x060069FB RID: 27131 RVA: 0x0016CFB1 File Offset: 0x0016B1B1
			public RowValueProvider(ActiveDirectoryCategoryTableValue tableValue, string searchHost, string[] filterOnlyAttributes, HashSet<string> baseSearchAttributes, ActiveDirectoryServiceSearchResult subtreeSearchResult)
			{
				this.searchHost = searchHost;
				this.tableValue = tableValue;
				this.filterOnlyAttributes = filterOnlyAttributes;
				this.baseSearchAttributes = baseSearchAttributes;
				this.subtreeSearchResult = subtreeSearchResult;
			}

			// Token: 0x060069FC RID: 27132 RVA: 0x0016CFE0 File Offset: 0x0016B1E0
			public Value GetValue(int columnIndex)
			{
				string text;
				bool flag;
				if (columnIndex < this.tableValue.columnInfos.Length)
				{
					ActiveDirectoryColumnInfo activeDirectoryColumnInfo = this.tableValue.columnInfos[columnIndex];
					if (activeDirectoryColumnInfo.ColumnType == ColumnType.Attribute)
					{
						text = activeDirectoryColumnInfo.AttributeNames[0];
						flag = true;
					}
					else
					{
						text = null;
						flag = false;
					}
				}
				else
				{
					flag = true;
					text = this.filterOnlyAttributes[columnIndex - this.tableValue.columnInfos.Length];
				}
				Value value;
				if (flag)
				{
					ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult;
					if (this.baseSearchAttributes.Contains(text))
					{
						if (this.baseSearchResult == null)
						{
							this.baseSearchResult = this.tableValue.environment.Service.GetObject(this.searchHost, this.subtreeSearchResult.DistinguishedName, this.baseSearchAttributes.ToArray<string>());
						}
						activeDirectoryServiceSearchResult = this.baseSearchResult;
					}
					else
					{
						activeDirectoryServiceSearchResult = this.subtreeSearchResult;
					}
					value = this.tableValue.environment.ValueBuilder.CreateValue(this.tableValue.objectCategory, text, activeDirectoryServiceSearchResult, this.searchHost, this.subtreeSearchResult.DistinguishedName).Value;
				}
				else
				{
					ActiveDirectoryColumnInfo activeDirectoryColumnInfo2 = this.tableValue.columnInfos[columnIndex];
					ActiveDirectoryCategoryTableValue.DelayedRecordValues delayedRecordValues = new ActiveDirectoryCategoryTableValue.DelayedRecordValues(this.searchHost, this.tableValue.environment, activeDirectoryColumnInfo2.AttributeNames, this.subtreeSearchResult.DistinguishedName, this.tableValue.objectCategory);
					value = RecordValue.New(this.tableValue.type.AsTableType.ItemType.Fields[columnIndex].AsRecord["Type"].AsType.AsRecordType, new Func<int, Value>(delayedRecordValues.GetValue));
				}
				return value;
			}

			// Token: 0x04003AC1 RID: 15041
			private readonly string searchHost;

			// Token: 0x04003AC2 RID: 15042
			private readonly ActiveDirectoryCategoryTableValue tableValue;

			// Token: 0x04003AC3 RID: 15043
			private readonly string[] filterOnlyAttributes;

			// Token: 0x04003AC4 RID: 15044
			private readonly HashSet<string> baseSearchAttributes;

			// Token: 0x04003AC5 RID: 15045
			private readonly ActiveDirectoryServiceSearchResult subtreeSearchResult;

			// Token: 0x04003AC6 RID: 15046
			private ActiveDirectoryServiceSearchResult baseSearchResult;
		}

		// Token: 0x02000FC3 RID: 4035
		private sealed class RecoveringEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060069FD RID: 27133 RVA: 0x0016D175 File Offset: 0x0016B375
			public RecoveringEnumerator(IEngineHost engineHost, IEnumerator<IValueReference> enumerator, TableValue unoptimized, IResource resource)
			{
				this.engineHost = engineHost;
				this.unoptimized = unoptimized;
				this.enumerator = enumerator;
				this.resource = resource;
			}

			// Token: 0x17001E6E RID: 7790
			// (get) Token: 0x060069FE RID: 27134 RVA: 0x0016D19A File Offset: 0x0016B39A
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x060069FF RID: 27135 RVA: 0x0016D1A7 File Offset: 0x0016B3A7
			public void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
			}

			// Token: 0x17001E6F RID: 7791
			// (get) Token: 0x06006A00 RID: 27136 RVA: 0x0016D1C3 File Offset: 0x0016B3C3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006A01 RID: 27137 RVA: 0x0016D1CC File Offset: 0x0016B3CC
			public bool MoveNext()
			{
				bool flag;
				try
				{
					if (this.enumerator.MoveNext())
					{
						this.rowsReturned++;
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				catch (ActiveDirectoryServiceException ex)
				{
					if (this.unoptimized == null)
					{
						throw ActiveDirectoryExceptions.NewUnexpectedException(this.engineHost, ex, this.resource);
					}
					this.enumerator = this.unoptimized.GetEnumerator();
					while (this.rowsReturned != 0)
					{
						this.enumerator.MoveNext();
						this.rowsReturned--;
					}
					this.unoptimized = null;
					flag = this.MoveNext();
				}
				return flag;
			}

			// Token: 0x06006A02 RID: 27138 RVA: 0x000033E7 File Offset: 0x000015E7
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04003AC7 RID: 15047
			private readonly IEngineHost engineHost;

			// Token: 0x04003AC8 RID: 15048
			private readonly IResource resource;

			// Token: 0x04003AC9 RID: 15049
			private TableValue unoptimized;

			// Token: 0x04003ACA RID: 15050
			private IEnumerator<IValueReference> enumerator;

			// Token: 0x04003ACB RID: 15051
			private int rowsReturned;
		}
	}
}
