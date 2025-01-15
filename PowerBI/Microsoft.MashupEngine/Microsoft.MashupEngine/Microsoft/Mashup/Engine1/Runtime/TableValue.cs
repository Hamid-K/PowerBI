using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001650 RID: 5712
	public abstract class TableValue : StructureValue, ITableValue, IValue, IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x0600901E RID: 36894 RVA: 0x001DEB58 File Offset: 0x001DCD58
		public static TableValue FromPartitions(TextValue partitionColumn, ListValue partitions, TypeValue partitionColumnType)
		{
			List<TableValue> list = new List<TableValue>();
			ListValue listValue = ListValue.New(new Value[] { partitionColumn });
			HashSet<Value> hashSet = new HashSet<Value>();
			foreach (IValueReference valueReference in partitions)
			{
				if (!valueReference.Value.IsList)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableFromPartitions_InvalidPartition, valueReference.Value, null);
				}
				ListValue asList = valueReference.Value.AsList;
				if (asList.Count != 2)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableFromPartitions_InvalidPartition, valueReference.Value, null);
				}
				Value item = asList.Item0;
				Value item2 = asList.Item1;
				if (!hashSet.Add(asList.Item0))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableFromPartitions_InvalidPartition, valueReference.Value, null);
				}
				if (!item2.IsTable)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableFromPartitions_InvalidPartition, valueReference.Value, null);
				}
				ListValue listValue2 = ListValue.New(new Value[] { TypeServices.SetDomain(partitionColumnType, ListValue.New(new Value[] { item })) });
				TableValue tableValue = item2.AsTable.AddColumns(listValue, new TableValue.ConstantListFunctionValue(item), listValue2);
				list.Add(tableValue);
			}
			return TableValue.Combine(list.ToArray(), partitionColumn.AsString, null);
		}

		// Token: 0x0600901F RID: 36895 RVA: 0x001DECB0 File Offset: 0x001DCEB0
		public static string GetUniqueName(Keys keys, int i)
		{
			int num = 0;
			string text;
			for (;;)
			{
				text = string.Format(CultureInfo.InvariantCulture, "t{0}_{1}", i, num);
				int num2;
				if (!keys.TryGetKeyIndex(text, out num2))
				{
					break;
				}
				num++;
			}
			return text;
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x001DECED File Offset: 0x001DCEED
		public TableValue GetPartitionTable()
		{
			return this.Query.GetPartitionTable(new int[0]);
		}

		// Token: 0x06009021 RID: 36897 RVA: 0x001DED00 File Offset: 0x001DCF00
		protected static TableValue New(TableValue value, RecordValue meta)
		{
			if (!meta.IsEmpty)
			{
				value = new MetaTableValue(value, meta);
			}
			return value;
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x06009022 RID: 36898
		public abstract override TypeValue Type { get; }

		// Token: 0x06009023 RID: 36899 RVA: 0x001DED14 File Offset: 0x001DCF14
		public override Value NewType(TypeValue type)
		{
			TableTypeValue asTableType = type.AsTableType;
			if (asTableType.ItemType.Fields.Keys.Length != this.Columns.Length)
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			return CastTableValue.New(this, asTableType);
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x06009024 RID: 36900 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06009025 RID: 36901 RVA: 0x001DED59 File Offset: 0x001DCF59
		public override Value NewMeta(RecordValue metaValue)
		{
			return TableValue.New(this, metaValue);
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x06009026 RID: 36902 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsCube
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x06009027 RID: 36903 RVA: 0x001DED62 File Offset: 0x001DCF62
		public virtual CubeValue AsCube
		{
			get
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Cube_ValueNotACube, this, null);
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x06009028 RID: 36904 RVA: 0x001DED70 File Offset: 0x001DCF70
		public override IExpression Expression
		{
			get
			{
				return QueryToExpressionVisitor.ToResourceExpression(this.Query);
			}
		}

		// Token: 0x06009029 RID: 36905 RVA: 0x001898A8 File Offset: 0x00187AA8
		public virtual TableValue Optimize()
		{
			return new QueryTableValue(this.Query).Optimize();
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x0600902A RID: 36906 RVA: 0x001DED7D File Offset: 0x001DCF7D
		public virtual Keys Columns
		{
			get
			{
				return this.Type.AsTableType.ItemType.Fields.Keys;
			}
		}

		// Token: 0x0600902B RID: 36907 RVA: 0x001DED99 File Offset: 0x001DCF99
		public virtual TypeValue GetColumnType(int index)
		{
			return this.Type.AsTableType.ItemType.Fields[index]["Type"].AsType;
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x0600902C RID: 36908 RVA: 0x001DEDC5 File Offset: 0x001DCFC5
		public virtual IList<TableKey> TableKeys
		{
			get
			{
				return this.Type.AsTableType.TableKeys;
			}
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x0600902D RID: 36909 RVA: 0x001DEDD7 File Offset: 0x001DCFD7
		public virtual IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.ComputedColumns.None;
			}
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x0600902E RID: 36910 RVA: 0x001DEDDE File Offset: 0x001DCFDE
		public virtual IList<RelatedTable> RelatedTables
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.RelatedTables.None;
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x0600902F RID: 36911 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual ColumnIdentity[] ColumnIdentities
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x06009030 RID: 36912 RVA: 0x001DEDE5 File Offset: 0x001DCFE5
		public virtual IList<Relationship> Relationships
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.Relationships.None;
			}
		}

		// Token: 0x06009031 RID: 36913 RVA: 0x001DEDEC File Offset: 0x001DCFEC
		public virtual TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return RelatedTablesTableValue.New(this, relatedTables);
		}

		// Token: 0x06009032 RID: 36914 RVA: 0x001DEDF5 File Offset: 0x001DCFF5
		public virtual TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(this, relatedTables, columnIdentities, relationships);
		}

		// Token: 0x06009033 RID: 36915 RVA: 0x001DEE00 File Offset: 0x001DD000
		public virtual TableValue ReplaceRelationshipIdentity(string identity)
		{
			return RelatedTablesTableValue.New(this, identity);
		}

		// Token: 0x06009034 RID: 36916 RVA: 0x001DEE09 File Offset: 0x001DD009
		public virtual TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return RelatedTablesTableValue.New(this, columnIdentities);
		}

		// Token: 0x06009035 RID: 36917 RVA: 0x001DEE12 File Offset: 0x001DD012
		public virtual TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(this, relationships);
		}

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x06009036 RID: 36918 RVA: 0x001DEE1B File Offset: 0x001DD01B
		public virtual Query Query
		{
			get
			{
				return new TableQuery(this, null);
			}
		}

		// Token: 0x06009037 RID: 36919 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public virtual TableValue Unordered()
		{
			return this;
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x06009038 RID: 36920 RVA: 0x001DEE24 File Offset: 0x001DD024
		public virtual IQueryDomain QueryDomain
		{
			get
			{
				return NullQueryDomain.Instance;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x06009039 RID: 36921 RVA: 0x00049E54 File Offset: 0x00048054
		public virtual TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x001DEE2B File Offset: 0x001DD02B
		public virtual IPageReader GetReader()
		{
			return new TableValue.ConformingPageReader(new DataReaderPageReader(new TableDataReader(this.Type.AsTableType, new TableValueDataReader(this, true), null), new DataReaderPageReader.ExceptionPropertyGetter(PageExceptionSerializer.TryGetPropertiesFromException)));
		}

		// Token: 0x170025C2 RID: 9666
		public override Value this[string key]
		{
			get
			{
				TextValue textValue = TextValue.New(key);
				return new ColumnReferenceListValue(this.SelectColumns(textValue, MissingFieldMode.Error), key);
			}
		}

		// Token: 0x170025C3 RID: 9667
		public override Value this[Value key]
		{
			get
			{
				if (key.IsNumber)
				{
					return this[key.AsInteger32];
				}
				if (!key.IsRecord)
				{
					return this[key.AsString];
				}
				Value value;
				if (!this.TryGetValue(key.AsRecord, out value))
				{
					throw ValueException.KeyNotFound(key, this);
				}
				return value;
			}
		}

		// Token: 0x0600903D RID: 36925 RVA: 0x001DEED0 File Offset: 0x001DD0D0
		public override bool TryGetValue(Value key, out Value value)
		{
			if (key.IsNumber)
			{
				int num = key.AsInteger32;
				if (num < 0)
				{
					throw ValueException.StructureIndexCannotBeNegative(num, this);
				}
				foreach (IValueReference valueReference in this)
				{
					if (num == 0)
					{
						value = valueReference.Value;
						return true;
					}
					num--;
				}
				value = Value.Null;
				return false;
			}
			else
			{
				if (key.IsRecord)
				{
					using (IEnumerator<IValueReference> enumerator2 = this.SelectRows(key.AsRecord).Take(RowCount.Two).GetEnumerator())
					{
						if (!enumerator2.MoveNext())
						{
							value = Value.Null;
							return false;
						}
						value = enumerator2.Current.Value;
						if (enumerator2.MoveNext())
						{
							throw ValueException.KeyNotUnique(key, this);
						}
						return true;
					}
				}
				if (!key.IsText)
				{
					return base.TryGetValue(key, out value);
				}
				string asString = key.AsString;
				if (this.Type.AsTableType.ItemType.Fields.Keys.Contains(asString))
				{
					value = this[asString];
					return true;
				}
				value = Value.Null;
				return false;
			}
			bool flag;
			return flag;
		}

		// Token: 0x0600903E RID: 36926 RVA: 0x001DF014 File Offset: 0x001DD214
		public TableValue ReplaceTableKeys(IList<TableKey> tableKeys)
		{
			return this.NewType(this.Type.AsTableType.ReplaceTableKeys(tableKeys)).AsTable;
		}

		// Token: 0x0600903F RID: 36927 RVA: 0x001DF032 File Offset: 0x001DD232
		public TableValue SelectColumns(Value columns, Value missingField)
		{
			return this.SelectColumns(columns, RecordTypeAlgebra.GetMissingFieldMode(missingField));
		}

		// Token: 0x06009040 RID: 36928 RVA: 0x001DF041 File Offset: 0x001DD241
		public TableValue SelectColumns(Value columns, MissingFieldMode missingField)
		{
			return this.AddNullColumns(columns, missingField).SelectColumns(columns, missingField == MissingFieldMode.Ignore);
		}

		// Token: 0x06009041 RID: 36929 RVA: 0x001DF058 File Offset: 0x001DD258
		private TableValue SelectColumns(Value columns, bool ignoreMissing)
		{
			int[] columns2 = TableValue.GetColumnBindings(this.Columns, columns, ignoreMissing ? MissingFieldMode.Ignore : MissingFieldMode.Error).Columns;
			KeysBuilder keysBuilder = new KeysBuilder(columns2.Length);
			for (int i = 0; i < columns2.Length; i++)
			{
				keysBuilder.Add(this.Columns[columns2[i]]);
			}
			Keys keys = keysBuilder.ToKeys();
			return this.SelectColumns(new ColumnSelection(keys, columns2));
		}

		// Token: 0x06009042 RID: 36930 RVA: 0x001DF0C8 File Offset: 0x001DD2C8
		public virtual TableValue SelectColumns(ColumnSelection columnSelection)
		{
			TableValue tableValue;
			if (!this.TrySelectColumns(columnSelection, out tableValue))
			{
				tableValue = new QueryTableValue(this.Query.ProjectColumns(columnSelection));
			}
			return tableValue;
		}

		// Token: 0x06009043 RID: 36931 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			table = null;
			return false;
		}

		// Token: 0x06009044 RID: 36932 RVA: 0x001DF0F4 File Offset: 0x001DD2F4
		public TableValue SelectRows(RecordValue index)
		{
			for (int i = 0; i < index.Keys.Length; i++)
			{
				if (!this.Columns.Contains(index.Keys[i]))
				{
					return this.Take(NumberValue.Zero);
				}
			}
			return this.SelectRows(new TableValue.RowSelectorFunctionValue(index));
		}

		// Token: 0x06009045 RID: 36933 RVA: 0x001DF148 File Offset: 0x001DD348
		public virtual TableValue SelectRows(FunctionValue condition)
		{
			return new QueryTableValue(this).SelectRows(condition);
		}

		// Token: 0x06009046 RID: 36934 RVA: 0x001DF158 File Offset: 0x001DD358
		private TableValue AddNullColumns(Value columns, MissingFieldMode missingField)
		{
			if (missingField == MissingFieldMode.UseNull)
			{
				return this.AddNullColumns(Keys.New(TableValue.GetColumnBindings(this.Columns, columns, missingField).MissingColumns));
			}
			return this;
		}

		// Token: 0x06009047 RID: 36935 RVA: 0x001DF18C File Offset: 0x001DD38C
		private TableValue AddNullColumns(Keys columnNames)
		{
			if (columnNames.Length == 0)
			{
				return this;
			}
			FunctionValue[] array = new FunctionValue[columnNames.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ConstantFunctionValue.EachNull;
			}
			IValueReference[] array2 = new IValueReference[columnNames.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = TypeValue.Null;
			}
			return this.AddColumns(new ColumnsConstructor(columnNames, new TableValue.FunctionsColumnsConstructorFunctionValue(array), array2));
		}

		// Token: 0x06009048 RID: 36936 RVA: 0x001DF1FC File Offset: 0x001DD3FC
		public TableValue AddColumns(ListValue columnNames, FunctionValue columnGenerator, Value columnTypes)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < columnNames.Count; i++)
			{
				keysBuilder.Add(columnNames[i].AsString);
			}
			Keys keys = keysBuilder.ToKeys();
			IValueReference[] array = new IValueReference[keys.Length];
			if (!columnTypes.IsNull)
			{
				ListValue asList = columnTypes.AsList;
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = asList.GetReference(j);
				}
			}
			else
			{
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = TypeValue.Any;
				}
			}
			return this.AddColumns(new ColumnsConstructor(keys, columnGenerator, array));
		}

		// Token: 0x06009049 RID: 36937 RVA: 0x001DF2A4 File Offset: 0x001DD4A4
		public virtual TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			if (columnGenerator.Length == 0)
			{
				return this;
			}
			for (int i = 0; i < columnGenerator.Length; i++)
			{
				int num;
				if (this.Columns.TryGetKeyIndex(columnGenerator.Names[i], out num))
				{
					throw ValueException.DuplicateField(this.Columns[num]);
				}
			}
			return new QueryTableValue(this).AddColumns(columnGenerator);
		}

		// Token: 0x0600904A RID: 36938 RVA: 0x001DF305 File Offset: 0x001DD505
		public TableValue RenameColumns(ListValue renames, Value missingField)
		{
			return this.RenameColumns(renames, RecordTypeAlgebra.GetMissingFieldMode(missingField));
		}

		// Token: 0x0600904B RID: 36939 RVA: 0x001DF314 File Offset: 0x001DD514
		public TableValue RenameColumns(ListValue renames, MissingFieldMode missingField)
		{
			IDictionary<string, string> renames2 = Library.RenameOperations.GetRenames(renames);
			KeysBuilder keysBuilder = new KeysBuilder(this.Columns.Length);
			foreach (string text in this.Columns)
			{
				string text2;
				if (!renames2.TryGetValue(text, out text2))
				{
					text2 = text;
				}
				keysBuilder.Add(text2);
			}
			TableValue tableValue = this.SelectColumns(new ColumnSelection(keysBuilder.ToKeys()));
			foreach (KeyValuePair<string, string> keyValuePair in renames2)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!this.Columns.Contains(key))
				{
					switch (missingField)
					{
					case MissingFieldMode.Error:
						throw ValueException.TableColumnNotFound(key);
					case MissingFieldMode.Ignore:
						break;
					case MissingFieldMode.UseNull:
						tableValue = tableValue.AddNullColumns(Keys.New(value));
						break;
					default:
						throw new InvalidOperationException();
					}
				}
			}
			return tableValue;
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x001DF430 File Offset: 0x001DD630
		public TableValue ReorderColumns(ListValue columnOrder, Value missingField)
		{
			return this.ReorderColumns(columnOrder, RecordTypeAlgebra.GetMissingFieldMode(missingField));
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x001DF43F File Offset: 0x001DD63F
		public TableValue ReorderColumns(ListValue columnOrder, MissingFieldMode missingField)
		{
			return this.AddNullColumns(columnOrder, missingField).ReorderColumns(columnOrder, missingField == MissingFieldMode.Ignore);
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x001DF454 File Offset: 0x001DD654
		private TableValue ReorderColumns(ListValue columnOrder, bool ignoreMissing)
		{
			TableValue.ColumnBindings columnBindings = TableValue.GetColumnBindings(this.Columns, columnOrder, ignoreMissing ? MissingFieldMode.Ignore : MissingFieldMode.Error);
			int[] columns = columnBindings.Columns;
			string[] missingColumns = columnBindings.MissingColumns;
			bool[] array = new bool[this.Columns.Length];
			foreach (int num in columns)
			{
				if (array[num])
				{
					throw ValueException.DuplicateField(this.Columns[num]);
				}
				array[num] = true;
			}
			int[] array2 = new int[this.Columns.Length];
			KeysBuilder keysBuilder = new KeysBuilder(this.Columns.Length);
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j])
				{
					int num4 = columns[num3++];
					array2[num2++] = num4;
					keysBuilder.Add(this.Columns[num4]);
				}
				else
				{
					array2[num2++] = j;
					keysBuilder.Add(this.Columns[j]);
				}
			}
			return this.SelectColumns(new ColumnSelection(keysBuilder.ToKeys(), array2));
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x001DF56B File Offset: 0x001DD76B
		public TableValue RemoveColumns(Value columns, Value missingField)
		{
			return this.RemoveColumns(columns, RecordTypeAlgebra.GetMissingFieldMode(missingField));
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x001DF57C File Offset: 0x001DD77C
		public TableValue RemoveColumns(Value columns, MissingFieldMode missingField)
		{
			int[] columns2 = TableValue.GetColumnBindings(this.Columns, columns, missingField).Columns;
			bool[] array = new bool[this.Columns.Length];
			foreach (int num in columns2)
			{
				if (array[num])
				{
					throw ValueException.DuplicateField(this.Columns[num]);
				}
				array[num] = true;
			}
			int[] array2 = new int[this.Columns.Length - columns2.Length];
			KeysBuilder keysBuilder = new KeysBuilder(array2.Length);
			int num2 = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					keysBuilder.Add(this.Columns[j]);
					array2[num2++] = j;
				}
			}
			return this.SelectColumns(new ColumnSelection(keysBuilder.ToKeys(), array2));
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x001DF653 File Offset: 0x001DD853
		private static TableValue.GroupKind GetGroupKind(Value value)
		{
			if (!value.IsNull)
			{
				return Library.GroupKind.Type.GetValue(value.AsNumber);
			}
			return TableValue.GroupKind.Global;
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x001DF670 File Offset: 0x001DD870
		public TableValue Group(Value key, ListValue aggregatedColumns, Value groupKind, Value comparer)
		{
			int[] columns = TableValue.GetColumns(this.Columns, key);
			ColumnConstructor[] columnConstructors = TableValue.GetColumnConstructors(aggregatedColumns);
			KeysBuilder keysBuilder = new KeysBuilder(columns.Length + columnConstructors.Length);
			for (int i = 0; i < columns.Length; i++)
			{
				keysBuilder.Add(this.Columns[columns[i]]);
			}
			Keys keys = keysBuilder.ToKeys();
			for (int j = 0; j < columnConstructors.Length; j++)
			{
				keysBuilder.Add(columnConstructors[j].Name);
			}
			Keys keys2 = keysBuilder.ToKeys();
			bool flag = !groupKind.IsNull && TableValue.GetGroupKind(groupKind) == TableValue.GroupKind.Local;
			bool isList = key.IsList;
			Grouping grouping = new Grouping(flag, keys2, keys, columns, columnConstructors, isList, comparer.IsNull ? null : comparer.AsFunction, this.Type.AsTableType);
			return this.Group(grouping);
		}

		// Token: 0x06009053 RID: 36947 RVA: 0x001DF74A File Offset: 0x001DD94A
		public virtual TableValue Group(Grouping grouping)
		{
			return new QueryTableValue(this).Group(grouping);
		}

		// Token: 0x06009054 RID: 36948 RVA: 0x001DF758 File Offset: 0x001DD958
		public Value Skip(Value countOrCondition)
		{
			if (countOrCondition.IsNull)
			{
				return this.Skip(RowCount.One);
			}
			if (countOrCondition.IsFunction)
			{
				return this.SkipWhile(countOrCondition.AsFunction);
			}
			return this.Skip(countOrCondition.AsNumber);
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x001DF78F File Offset: 0x001DD98F
		public TableValue Skip(NumberValue count)
		{
			return this.Skip(this.GetRowCount(count));
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x001DF79E File Offset: 0x001DD99E
		public virtual TableValue Skip(RowCount count)
		{
			return new QueryTableValue(this).Skip(count);
		}

		// Token: 0x06009057 RID: 36951 RVA: 0x001DF7AC File Offset: 0x001DD9AC
		public TableValue SkipWhile(FunctionValue condition)
		{
			return new SkipWhileTableValue(this, condition);
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x001DF7B5 File Offset: 0x001DD9B5
		public Value Take(Value countOrCondition)
		{
			if (countOrCondition.IsFunction)
			{
				return this.TakeWhile(countOrCondition.AsFunction);
			}
			return this.Take(countOrCondition.AsNumber);
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x001DF7D8 File Offset: 0x001DD9D8
		public TableValue Take(NumberValue count)
		{
			return this.Take(this.GetRowCount(count));
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x001DF7E7 File Offset: 0x001DD9E7
		public virtual TableValue Take(RowCount count)
		{
			return new QueryTableValue(this).Take(count);
		}

		// Token: 0x0600905B RID: 36955 RVA: 0x001DF7F5 File Offset: 0x001DD9F5
		public TableValue TakeWhile(FunctionValue condition)
		{
			return new TakeWhileTableValue(this, condition);
		}

		// Token: 0x0600905C RID: 36956 RVA: 0x001DF800 File Offset: 0x001DDA00
		public TableValue Sort(Value sortOrderValue)
		{
			TableSortOrder tableSortOrder = TableValue.GetTableSortOrder(this.Columns, sortOrderValue);
			if (tableSortOrder.IsEmpty)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TableSortMustHaveCriterion, this, null);
			}
			return this.ApplySortOrder(tableSortOrder);
		}

		// Token: 0x0600905D RID: 36957 RVA: 0x001DF838 File Offset: 0x001DDA38
		public TableValue SortDescending(Value sortOrderValue)
		{
			TableSortOrder tableSortOrder = TableValue.GetTableSortOrder(this.Columns, sortOrderValue);
			if (tableSortOrder.IsEmpty)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TableSortMustHaveCriterion, this, null);
			}
			return this.ApplySortOrder(tableSortOrder.Invert());
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x001DF873 File Offset: 0x001DDA73
		private TableValue ApplySortOrder(TableSortOrder sortOrder)
		{
			sortOrder = sortOrder.EliminateConstantSelectors(this.Type.AsTableType.ItemType);
			if (!sortOrder.IsEmpty)
			{
				return this.Sort(sortOrder);
			}
			return this.Unordered();
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x001DF8A3 File Offset: 0x001DDAA3
		public virtual TableValue Sort(TableSortOrder sortOrder)
		{
			return new QueryTableValue(this).Sort(sortOrder);
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x001DF8B1 File Offset: 0x001DDAB1
		public TableValue Distinct(Value tableDistinct)
		{
			return this.Distinct(TableValue.GetTableDistinct(this.Columns, tableDistinct));
		}

		// Token: 0x06009061 RID: 36961 RVA: 0x001DF8C5 File Offset: 0x001DDAC5
		public virtual TableValue Distinct(TableDistinct distinctCriteria)
		{
			return new QueryTableValue(this).Distinct(distinctCriteria);
		}

		// Token: 0x06009062 RID: 36962 RVA: 0x001DF8D4 File Offset: 0x001DDAD4
		public TableValue TransformColumns(ListValue transformOperations, Value defaultTransformation, Value missingField)
		{
			ColumnConstructor[] columnConstructors = TableValue.GetColumnConstructors(transformOperations);
			Dictionary<string, ColumnConstructor> dictionary = new Dictionary<string, ColumnConstructor>(columnConstructors.Length);
			for (int i = 0; i < columnConstructors.Length; i++)
			{
				string name = columnConstructors[i].Name;
				if (dictionary.ContainsKey(name))
				{
					throw ValueException.DuplicateField(name);
				}
				dictionary.Add(name, columnConstructors[i]);
			}
			Dictionary<string, ColumnConstructor> dictionary2 = new Dictionary<string, ColumnConstructor>(this.Columns.Length);
			foreach (string text in this.Columns)
			{
				ColumnConstructor columnConstructor;
				if (dictionary.TryGetValue(text, out columnConstructor))
				{
					dictionary2.Add(text, columnConstructor);
				}
				else if (!defaultTransformation.IsNull)
				{
					dictionary2.Add(text, new ColumnConstructor(text, defaultTransformation.AsFunction));
				}
			}
			foreach (KeyValuePair<string, ColumnConstructor> keyValuePair in dictionary)
			{
				if (!dictionary2.ContainsKey(keyValuePair.Key))
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return this.TransformColumns(dictionary2, RecordTypeAlgebra.GetMissingFieldMode(missingField));
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x001DFA18 File Offset: 0x001DDC18
		public TableValue TransformColumnTypes(IEngineHost engineHost, ListValue typeTransformations, ICulture culture)
		{
			TransformTypesHelper transformTypesHelper = new TransformTypesHelper(engineHost, culture);
			ListValue[] array = null;
			if (typeTransformations.Count > 0 && typeTransformations.Item0.IsText)
			{
				typeTransformations = ListValue.New(new IValueReference[] { typeTransformations });
			}
			array = new ListValue[typeTransformations.Count];
			int num = 0;
			foreach (IValueReference valueReference in typeTransformations)
			{
				int num2;
				if (!this.Columns.TryGetKeyIndex(valueReference.Value.AsList.Item0.AsString, out num2))
				{
					throw ValueException.TableColumnNotFound(valueReference.Value.AsList.Item0.AsString);
				}
				FunctionValue functionValueFromType = transformTypesHelper.GetFunctionValueFromType(valueReference.Value.AsList.Item1.AsType, this.GetColumnType(num2), false);
				array[num] = ListValue.New(new Value[]
				{
					valueReference.Value.AsList.Item0,
					functionValueFromType
				});
				num++;
			}
			Value[] array2 = array;
			return this.TransformColumns(ListValue.New(array2), Value.Null, Value.Null);
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x001DFB54 File Offset: 0x001DDD54
		private TableValue TransformColumns(Dictionary<string, ColumnConstructor> transforms, MissingFieldMode missingField)
		{
			TableValue.ColumnBindings columnBindings = TableValue.GetColumnBindings(this.Columns, ListValue.New(transforms.Keys.ToArray<string>()), missingField);
			int[] columns = columnBindings.Columns;
			Dictionary<int, ColumnTransform> dictionary = new Dictionary<int, ColumnTransform>(columns.Length);
			for (int i = 0; i < columns.Length; i++)
			{
				string text = this.Columns[columns[i]];
				ColumnConstructor columnConstructor = transforms[text];
				dictionary.Add(columns[i], new ColumnTransform(columnConstructor.Function, columnConstructor.Type));
			}
			return this.TransformColumns(new ColumnTransforms(dictionary)).AddNullColumns(Keys.New(columnBindings.MissingColumns));
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x001DFBF0 File Offset: 0x001DDDF0
		public virtual TableValue TransformColumns(ColumnTransforms columnTransforms)
		{
			IDictionary<int, ColumnTransform> dictionary = columnTransforms.Dictionary;
			int[] array = dictionary.Keys.ToArray<int>();
			FunctionValue[] array2 = dictionary.Values.Select((ColumnTransform x) => x.Function).ToArray<FunctionValue>();
			IValueReference[] array3 = dictionary.Values.Select((ColumnTransform x) => x.Type).ToArray<IValueReference>();
			string[] array4 = new string[array.Length];
			FunctionValue[] array5 = new FunctionValue[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = this.Columns[array[i]];
				array4[i] = text;
				array5[i] = new TableValue.TransformColumnFunctionValue(text, array[i], array2[i]);
			}
			FunctionValue functionValue = new TableValue.FunctionsColumnsConstructorFunctionValue(array5);
			ColumnsConstructor columnsConstructor = new ColumnsConstructor(Keys.New(array4), functionValue, array3);
			return this.ReplaceColumns(columnsConstructor);
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x001DFCE0 File Offset: 0x001DDEE0
		public TableValue ReplaceColumns(ColumnsConstructor columnConstructor)
		{
			int length = columnConstructor.Names.Length;
			string[] array = new string[length];
			string[] array2 = new string[length];
			Value[] array3 = new Value[length];
			for (int i = 0; i < length; i++)
			{
				string text = columnConstructor.Names[i];
				array[i] = text;
				array2[i] = TableValue.GetUniqueName(this.Columns, i);
				array3[i] = ListValue.New(new string[]
				{
					array2[i],
					text
				});
			}
			string[] array4 = new string[array2.Length + array.Length];
			Array.Copy(array2, array4, array2.Length);
			Array.Copy(array, 0, array4, array2.Length, array.Length);
			columnConstructor = new ColumnsConstructor(Keys.New(array2), columnConstructor.Function, columnConstructor.Types);
			return this.AddColumns(columnConstructor).ReorderColumns(ListValue.New(array4), MissingFieldMode.Error).RemoveColumns(ListValue.New(array), MissingFieldMode.Error)
				.RenameColumns(ListValue.New(array3), MissingFieldMode.Error);
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x001DFDCC File Offset: 0x001DDFCC
		public static TableValue Join(TableValue leftTable, Value leftKey, TableValue rightTable, Value rightKey, Value joinKindValue, JoinAlgorithm joinAlgorithm, Value keyEqualityComparers)
		{
			TableTypeAlgebra.JoinKind joinKind = (joinKindValue.IsNull ? TableTypeAlgebra.JoinKind.Inner : TableTypeAlgebra.GetJoinKind(joinKindValue));
			int[] columns = TableValue.GetColumns(leftTable.Columns, leftKey);
			int[] columns2 = TableValue.GetColumns(rightTable.Columns, rightKey);
			FunctionValue[] keyEqualityComparers2 = TableValue.GetKeyEqualityComparers(columns.Length, keyEqualityComparers);
			if (columns.Length != columns2.Length)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.RelationalAlgebra_JoinMustHaveSameColumnCountAndType, rightKey, null);
			}
			return TableValue.Join(leftTable, columns, rightTable, columns2, joinKind, joinAlgorithm, keyEqualityComparers2);
		}

		// Token: 0x06009068 RID: 36968 RVA: 0x001DFE34 File Offset: 0x001DE034
		public static TableValue Join(TableValue leftTable, int[] leftKeyColumns, TableValue rightTable, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			Keys columns = leftTable.Columns;
			Keys columns2 = rightTable.Columns;
			IList<string> joinOverlap = TableValue.GetJoinOverlap(columns, leftKeyColumns, columns2, rightKeyColumns, joinKind);
			if (joinOverlap != null)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.RelationalAlgebra_JoinMustNotHaveColumnOverlap(joinOverlap[0]), rightTable.Type, null);
			}
			Keys joinKeys = TableValue.GetJoinKeys(columns, columns2);
			JoinColumn[] joinColumns = TableValue.GetJoinColumns(joinKeys, columns, columns2);
			Query query = Query.Join(RowCount.Infinite, leftTable.Query, leftKeyColumns, rightTable.Query, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
			IList<RelatedTable> list = Microsoft.Mashup.Engine1.Runtime.RelatedTables.Join(leftTable.Columns, leftTable.RelatedTables, rightTable.Columns, rightTable.RelatedTables, joinColumns);
			ColumnIdentity[] array = Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.Join(leftTable.ColumnIdentities, rightTable.ColumnIdentities, joinColumns);
			IList<Relationship> list2 = Microsoft.Mashup.Engine1.Runtime.Relationships.Join(leftTable.Columns, leftKeyColumns, leftTable.Relationships, rightTable.Columns, rightTable.ColumnIdentities, rightKeyColumns, rightTable.Relationships, joinColumns);
			return RelatedTablesTableValue.New(new QueryTableValue(query), list, array, list2);
		}

		// Token: 0x06009069 RID: 36969 RVA: 0x001DFF1C File Offset: 0x001DE11C
		public static Keys GetJoinKeys(Keys leftKeys, Keys rightKeys)
		{
			KeysBuilder keysBuilder = new KeysBuilder(leftKeys.Length + rightKeys.Length);
			keysBuilder.Union(leftKeys);
			keysBuilder.Union(rightKeys);
			return keysBuilder.ToKeys();
		}

		// Token: 0x0600906A RID: 36970 RVA: 0x001DFF54 File Offset: 0x001DE154
		public static JoinColumn[] GetJoinColumns(Keys joinKeys, Keys leftKeys, Keys rightKeys)
		{
			JoinColumn[] array = new JoinColumn[joinKeys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				if (!rightKeys.TryGetKeyIndex(joinKeys[i], out num))
				{
					num = -1;
				}
				int num2;
				if (!leftKeys.TryGetKeyIndex(joinKeys[i], out num2))
				{
					num2 = -1;
				}
				array[i] = new JoinColumn(num2, num);
			}
			return array;
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x001DFFB0 File Offset: 0x001DE1B0
		public static IList<string> GetJoinOverlap(Keys leftKeys, int[] leftColumns, Keys rightKeys, int[] rightColumns, TableTypeAlgebra.JoinKind kind)
		{
			List<string> list = new List<string>();
			List<string> list2 = null;
			for (int i = 0; i < leftColumns.Length; i++)
			{
				string text = leftKeys[leftColumns[i]];
				string text2 = rightKeys[rightColumns[i]];
				if (text == text2)
				{
					list.Add(text);
				}
			}
			for (int j = 0; j < leftKeys.Length; j++)
			{
				string text3 = leftKeys[j];
				if (rightKeys.Contains(text3) && (kind != TableTypeAlgebra.JoinKind.Inner || !list.Contains(text3)))
				{
					if (list2 == null)
					{
						list2 = new List<string>();
					}
					list2.Add(text3);
				}
			}
			return list2;
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x001E0044 File Offset: 0x001DE244
		public TableValue NestedJoin(Value leftKey, Value rightTable, Value rightKey, Value joinKindValue, TextValue newColumn, Value keyEqualityComparers)
		{
			TableTypeAlgebra.JoinKind joinKind = (joinKindValue.IsNull ? TableTypeAlgebra.JoinKind.LeftOuter : TableTypeAlgebra.GetJoinKind(joinKindValue));
			int[] columns = TableValue.GetColumns(this.Columns, leftKey);
			ListValue listValue = (rightKey.IsList ? rightKey.AsList : ListValue.New(new Value[] { rightKey }));
			if (!rightTable.IsFunction)
			{
				rightTable = rightTable.AsTable;
				TableValue.GetColumns(rightTable.AsTable.Columns, rightKey);
			}
			if (columns.Length != listValue.Count)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.RelationalAlgebra_JoinMustHaveSameColumnCountAndType, listValue, null);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < listValue.Count; i++)
			{
				keysBuilder.Add(listValue[i].AsString);
			}
			KeysBuilder keysBuilder2 = default(KeysBuilder);
			keysBuilder2.Union(this.Columns);
			if (!keysBuilder2.Union(newColumn.String))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Table_ColumnAlreadyExistsInTable(newColumn.String), null, null);
			}
			FunctionValue[] keyEqualityComparers2 = TableValue.GetKeyEqualityComparers(columns.Length, keyEqualityComparers);
			return this.NestedJoin(columns, rightTable, keysBuilder.ToKeys(), joinKind, newColumn.String, keysBuilder2.ToKeys(), keyEqualityComparers2);
		}

		// Token: 0x0600906D RID: 36973 RVA: 0x001E0162 File Offset: 0x001DE362
		public virtual TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return new QueryTableValue(this).NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x0600906E RID: 36974 RVA: 0x001E017C File Offset: 0x001DE37C
		public TableValue ExpandListColumn(TextValue columnName, Value singleOrDefault)
		{
			TableValue.ColumnBindings columnBindings = TableValue.GetColumnBindings(this.Columns, columnName, MissingFieldMode.Error);
			bool flag = !singleOrDefault.IsNull && singleOrDefault.AsBoolean;
			return this.ExpandListColumn(columnBindings.Columns[0], flag);
		}

		// Token: 0x0600906F RID: 36975 RVA: 0x001E01B9 File Offset: 0x001DE3B9
		public virtual TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return new QueryTableValue(this).ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x06009070 RID: 36976 RVA: 0x001E01C8 File Offset: 0x001DE3C8
		public TableValue ExpandRecordColumn(TextValue column, ListValue fieldNames, Value newColumnNames)
		{
			TableValue.ColumnBindings columnBindings = TableValue.GetColumnBindings(this.Columns, column, MissingFieldMode.Error);
			ListValue listValue = (newColumnNames.IsNull ? null : newColumnNames.AsList);
			if (listValue != null && fieldNames.Count != listValue.Count)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TableExpandRecordColumn_FieldAndNewColumnNamesMustHaveSameCount, newColumnNames, null);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			KeysBuilder keysBuilder2 = default(KeysBuilder);
			for (int i = 0; i < fieldNames.Count; i++)
			{
				keysBuilder.Add(fieldNames[i].AsString);
				keysBuilder2.Add((listValue != null) ? listValue[i].AsString : fieldNames[i].AsString);
			}
			return this.ExpandRecordColumn(columnBindings.Columns[0], keysBuilder.ToKeys(), keysBuilder2.ToKeys());
		}

		// Token: 0x06009071 RID: 36977 RVA: 0x001E028F File Offset: 0x001DE48F
		public virtual TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return new QueryTableValue(this).ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x06009072 RID: 36978 RVA: 0x001E02A0 File Offset: 0x001DE4A0
		public static int[] GetColumns(Keys keys, Value columnNames)
		{
			return TableValue.GetColumnBindings(keys, columnNames, MissingFieldMode.Error).Columns;
		}

		// Token: 0x06009073 RID: 36979 RVA: 0x001E02BD File Offset: 0x001DE4BD
		public static int[] GetColumns(Keys columns, Keys key)
		{
			return TableValue.GetColumns(columns, key, true);
		}

		// Token: 0x06009074 RID: 36980 RVA: 0x001E02C7 File Offset: 0x001DE4C7
		public static int[] GetColumnsOrNull(Keys columns, Keys key)
		{
			return TableValue.GetColumns(columns, key, false);
		}

		// Token: 0x06009075 RID: 36981 RVA: 0x001E02D4 File Offset: 0x001DE4D4
		private static int[] GetColumns(Keys columns, Keys key, bool throwOnError)
		{
			int[] array = new int[key.Length];
			int i = 0;
			while (i < array.Length)
			{
				int num;
				if (!columns.TryGetKeyIndex(key[i], out num))
				{
					if (throwOnError)
					{
						throw ValueException.TableColumnNotFound(key[i]);
					}
					return null;
				}
				else
				{
					array[i] = num;
					i++;
				}
			}
			return array;
		}

		// Token: 0x06009076 RID: 36982 RVA: 0x001E0324 File Offset: 0x001DE524
		private static int GetColumn(Keys columns, string key, bool throwOnError = true)
		{
			int num;
			if (columns.TryGetKeyIndex(key, out num))
			{
				return num;
			}
			if (throwOnError)
			{
				throw ValueException.TableColumnNotFound(key);
			}
			return -1;
		}

		// Token: 0x06009077 RID: 36983 RVA: 0x001E034C File Offset: 0x001DE54C
		private static TableValue.ColumnBindings GetColumnBindings(Keys keys, Value columnNames, MissingFieldMode missingField)
		{
			if (!columnNames.IsList)
			{
				columnNames = ListValue.New(new Value[] { columnNames });
			}
			List<int> list = new List<int>();
			List<string> list2 = new List<string>();
			foreach (IValueReference valueReference in columnNames.AsList)
			{
				string asString = valueReference.Value.AsString;
				int num;
				if (keys.TryGetKeyIndex(asString, out num))
				{
					list.Add(num);
				}
				else
				{
					switch (missingField)
					{
					case MissingFieldMode.Error:
						throw ValueException.TableColumnNotFound(asString);
					case MissingFieldMode.Ignore:
						break;
					case MissingFieldMode.UseNull:
						list2.Add(asString);
						break;
					default:
						throw new InvalidOperationException();
					}
				}
			}
			return new TableValue.ColumnBindings(list.ToArray(), list2.ToArray());
		}

		// Token: 0x06009078 RID: 36984 RVA: 0x001E0414 File Offset: 0x001DE614
		public static ColumnConstructor[] GetColumnConstructors(ListValue value)
		{
			if (value.Count > 1 && value[0].IsText)
			{
				return new ColumnConstructor[] { TableValue.GetColumnConstructor(value) };
			}
			List<ColumnConstructor> list = new List<ColumnConstructor>();
			foreach (IValueReference valueReference in value)
			{
				list.Add(TableValue.GetColumnConstructor(valueReference.Value));
			}
			return list.ToArray();
		}

		// Token: 0x06009079 RID: 36985 RVA: 0x001E049C File Offset: 0x001DE69C
		private static ColumnConstructor GetColumnConstructor(Value value)
		{
			ListValue asList = value.AsList;
			IValueReference valueReference = null;
			if (asList.Count == 3)
			{
				valueReference = asList.GetReference(2);
			}
			return new ColumnConstructor(asList[0].AsString, asList[1].AsFunction, valueReference);
		}

		// Token: 0x0600907A RID: 36986 RVA: 0x001E04E4 File Offset: 0x001DE6E4
		private RowCount GetRowCount(NumberValue count)
		{
			long asInteger = count.AsInteger64;
			if (asInteger < 0L || asInteger > RowCount.MaxValue)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.CountOrCondition_CountOrConditionValueExpectedError, this, null);
			}
			return new RowCount(count.AsInteger64);
		}

		// Token: 0x0600907B RID: 36987 RVA: 0x001E0520 File Offset: 0x001DE720
		public static TableSortOrder GetTableSortOrder(Keys columns, Value sortOrder)
		{
			TableSortOrder tableSortOrder;
			try
			{
				if (sortOrder.IsFunction && sortOrder.Type.AsFunctionType.ParameterCount == 2)
				{
					tableSortOrder = new TableSortOrder(new SortOrder[]
					{
						new SortOrder(null, sortOrder.AsFunction, true)
					});
				}
				else if (sortOrder.IsList)
				{
					tableSortOrder = TableValue.GetTableSortOrder(columns, sortOrder.AsList.ToArray());
				}
				else
				{
					tableSortOrder = new TableSortOrder(new SortOrder[] { TableValue.GetSortOrder(columns, sortOrder) });
				}
			}
			catch (ValueException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TableSortInvalidSortCriteria, sortOrder, ex);
			}
			return tableSortOrder;
		}

		// Token: 0x0600907C RID: 36988 RVA: 0x001E05C4 File Offset: 0x001DE7C4
		private static TableSortOrder GetTableSortOrder(Keys columns, Value[] values)
		{
			if (values.Length == 2 && (values[0].IsText || values[0].IsFunction) && values[1].IsNumber)
			{
				return new TableSortOrder(new SortOrder[] { TableValue.GetSortOrder(columns, ListValue.New(values)) });
			}
			SortOrder[] array = new SortOrder[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TableValue.GetSortOrder(columns, values[i]);
			}
			return new TableSortOrder(array);
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x001E0644 File Offset: 0x001DE844
		private static SortOrder GetSortOrder(Keys columns, Value value)
		{
			FunctionValue functionValue;
			bool flag;
			if (value.IsList)
			{
				Value[] array = value.AsList.ToArray();
				if (array.Length != 2)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableSortInvalidSortCriterion, value, null);
				}
				functionValue = TableValue.GetSelector(columns, array[0]);
				flag = TableValue.GetSortDirection(array[1]);
			}
			else
			{
				functionValue = TableValue.GetSelector(columns, value);
				flag = true;
			}
			return new SortOrder(functionValue, null, flag);
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x001E069F File Offset: 0x001DE89F
		private static bool GetSortDirection(Value value)
		{
			if (value.Equals(Library.Order.Ascending))
			{
				return true;
			}
			if (value.Equals(Library.Order.Descending))
			{
				return false;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.TableSortInvalidSortDirection, value, null);
		}

		// Token: 0x0600907F RID: 36991 RVA: 0x001E06CC File Offset: 0x001DE8CC
		private static TableDistinct GetTableDistinct(Keys columns, Value tableDistinct)
		{
			TableDistinct tableDistinct2;
			try
			{
				if (tableDistinct.IsNull)
				{
					Value[] array = new Value[columns.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = TextValue.New(columns[i]);
					}
					tableDistinct2 = TableValue.GetTableDistinct(columns, array);
				}
				else if (tableDistinct.IsText)
				{
					tableDistinct2 = new TableDistinct(new Distinct[]
					{
						new Distinct(TableValue.GetSelector(columns, tableDistinct), null)
					});
				}
				else if (tableDistinct.IsFunction)
				{
					tableDistinct2 = new TableDistinct(new Distinct[]
					{
						new Distinct(null, TableValue.GetComparer(tableDistinct.AsFunction))
					});
				}
				else
				{
					tableDistinct2 = TableValue.GetTableDistinct(columns, tableDistinct.AsList.ToArray());
				}
			}
			catch (ValueException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TableDistinctInvalidDistinctCriteria, tableDistinct, ex);
			}
			return tableDistinct2;
		}

		// Token: 0x06009080 RID: 36992 RVA: 0x001E07A0 File Offset: 0x001DE9A0
		private static TableDistinct GetTableDistinct(Keys columns, Value[] values)
		{
			if (values.Length != 0 && values[0].IsList)
			{
				Distinct[] array = new Distinct[values.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TableValue.GetSelectorComparer(columns, values[i].AsList.ToArray());
				}
				return new TableDistinct(array);
			}
			if (values.Length == 2 && values[1].IsFunction)
			{
				return new TableDistinct(new Distinct[] { TableValue.GetSelectorComparer(columns, values) });
			}
			Distinct[] array2 = new Distinct[values.Length];
			for (int j = 0; j < values.Length; j++)
			{
				string asString = values[j].AsString;
				int num;
				if (!columns.TryGetKeyIndex(asString, out num))
				{
					throw ValueException.TableColumnNotFound(asString);
				}
				array2[j] = new Distinct(new TableValue.ColumnSelectorFunctionValue(asString, num), null);
			}
			return new TableDistinct(array2);
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x001E086E File Offset: 0x001DEA6E
		private static Distinct GetSelectorComparer(Keys columns, Value[] values)
		{
			if (values.Length != 2)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ColumnSelectorInvalid, ListValue.New(values), null);
			}
			return TableValue.GetSelectorComparer(columns, values[0], values[1].AsFunction);
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x001E0899 File Offset: 0x001DEA99
		private static Distinct GetSelectorComparer(Keys columns, Value selector, FunctionValue function)
		{
			return new Distinct(TableValue.GetSelector(columns, selector), TableValue.GetComparer(function));
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x001E08B0 File Offset: 0x001DEAB0
		private static FunctionValue GetSelector(Keys columns, Value selector)
		{
			if (selector.IsText)
			{
				string asString = selector.AsString;
				int num;
				if (!columns.TryGetKeyIndex(asString, out num))
				{
					throw ValueException.TableColumnNotFound(asString);
				}
				return new TableValue.ColumnSelectorFunctionValue(asString, num);
			}
			else
			{
				if (selector.IsFunction)
				{
					return selector.AsFunction;
				}
				throw ValueException.NewExpressionError<Message0>(Strings.TableSortInvalidColumnSelector, selector, null);
			}
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x001E0904 File Offset: 0x001DEB04
		private static IEqualityComparer<Value> GetComparer(FunctionValue function)
		{
			IEqualityComparer<Value> equalityComparer;
			if (!function.TryGetEqualityComparer(out equalityComparer))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.CustomComparersNotAllowed, function, null);
			}
			return equalityComparer;
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x001E092C File Offset: 0x001DEB2C
		public static FunctionValue[] GetKeyEqualityComparers(int count, Value keyEqualityComparers)
		{
			if (keyEqualityComparers.IsNull)
			{
				return null;
			}
			ListValue asList = keyEqualityComparers.AsList;
			FunctionValue[] array = new FunctionValue[count];
			int num = 0;
			while (num < count && num < asList.Count)
			{
				if (!asList[num].IsNull)
				{
					array[num] = asList[num].AsFunction;
				}
				num++;
			}
			return array;
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x06009086 RID: 36998 RVA: 0x001E0984 File Offset: 0x001DEB84
		public static TableValue Empty
		{
			get
			{
				if (TableValue.empty == null)
				{
					TableValue.empty = ListValue.Empty.ToTable(TableTypeValue.New(RecordTypeValue.New(Keys.Empty)));
				}
				return TableValue.empty;
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06009087 RID: 36999 RVA: 0x001422C0 File Offset: 0x001404C0
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Table;
			}
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x06009088 RID: 37000 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsTable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x06009089 RID: 37001 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TableValue AsTable
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600908A RID: 37002
		public abstract IEnumerator<IValueReference> GetEnumerator();

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x0600908B RID: 37003 RVA: 0x001E09B0 File Offset: 0x001DEBB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Count
		{
			get
			{
				long largeCount = this.LargeCount;
				if (largeCount > 2147483647L)
				{
					throw ValueException.ListCountTooLarge(largeCount);
				}
				return (int)largeCount;
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x0600908C RID: 37004 RVA: 0x001E09D8 File Offset: 0x001DEBD8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual long LargeCount
		{
			get
			{
				long num2;
				using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						num += 1L;
						if (num > ListValue.MaxCount)
						{
							throw ValueException.ListCountTooLarge(num);
						}
					}
					num2 = num;
				}
				return num2;
			}
		}

		// Token: 0x0600908D RID: 37005 RVA: 0x001E0A2C File Offset: 0x001DEC2C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600908E RID: 37006 RVA: 0x001E0A34 File Offset: 0x001DEC34
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			foreach (IValueReference valueReference in this)
			{
				hashBuilder.Add(valueReference.Value.GetHashCode(comparer));
			}
			return hashBuilder.ToHash();
		}

		// Token: 0x0600908F RID: 37007 RVA: 0x001E0A98 File Offset: 0x001DEC98
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			if (!value.IsTable)
			{
				return false;
			}
			bool flag3;
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				using (IEnumerator<IValueReference> enumerator2 = value.AsTable.GetEnumerator())
				{
					for (;;)
					{
						bool flag = enumerator.MoveNext();
						bool flag2 = enumerator2.MoveNext();
						if (!flag && !flag2)
						{
							break;
						}
						if (!flag || !flag2)
						{
							goto IL_003C;
						}
						if (!enumerator.Current.Value.Equals(enumerator2.Current.Value, comparer))
						{
							goto Block_8;
						}
					}
					return true;
					IL_003C:
					return false;
					Block_8:
					flag3 = false;
				}
			}
			return flag3;
		}

		// Token: 0x06009090 RID: 37008 RVA: 0x001E0B3C File Offset: 0x001DED3C
		public sealed override string ToSource()
		{
			return "Table.FromRecords({})";
		}

		// Token: 0x06009091 RID: 37009 RVA: 0x001E0B43 File Offset: 0x001DED43
		public sealed override string ToString()
		{
			return "Table";
		}

		// Token: 0x06009092 RID: 37010 RVA: 0x001E0B4A File Offset: 0x001DED4A
		public sealed override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString("[Table]", this, type);
		}

		// Token: 0x06009093 RID: 37011 RVA: 0x001E0B58 File Offset: 0x001DED58
		public override void TestConnection()
		{
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					enumerator.Current.Value.TestConnection();
				}
			}
		}

		// Token: 0x06009094 RID: 37012 RVA: 0x001E0BA4 File Offset: 0x001DEDA4
		public virtual TableValue Buffer(Library.BufferMode bufferMode = Library.BufferMode.Eager)
		{
			TableValue tableValue;
			if (bufferMode != Library.BufferMode.Eager)
			{
				if (bufferMode != Library.BufferMode.Delayed)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				tableValue = new TableValue.BufferedTableValue(this);
			}
			else
			{
				tableValue = TableValue.BufferedTableValue.BufferTable(this);
			}
			NumberValue eager;
			if (!Library.BufferModeEnum.Type.TryLookupEnum(bufferMode, out eager))
			{
				eager = Library.BufferModeEnum.Eager;
			}
			RecordValue recordValue = RecordValue.New(Keys.New("BufferMode"), new Value[] { eager });
			return TransformedTableValue.New(tableValue, TableModule.Table.Buffer, new Value[] { this, recordValue });
		}

		// Token: 0x06009095 RID: 37013 RVA: 0x001E0C25 File Offset: 0x001DEE25
		public ListValue ToRecords()
		{
			return ListValue.New(this).NewType(ListTypeValue.New(this.Type.AsTableType.ItemType)).AsList;
		}

		// Token: 0x170025CA RID: 9674
		public override Value this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw ValueException.StructureIndexCannotBeNegative(index, this);
				}
				foreach (IValueReference valueReference in this)
				{
					if (index == 0)
					{
						return valueReference.Value;
					}
					index--;
				}
				throw ValueException.InsufficientElements(this);
			}
		}

		// Token: 0x06009097 RID: 37015 RVA: 0x001E0CB4 File Offset: 0x001DEEB4
		public override Value Concatenate(Value value)
		{
			if (value.IsTable)
			{
				return TableValue.Combine(ListValue.New(new Value[] { this, value }), null);
			}
			return base.Concatenate(value);
		}

		// Token: 0x06009098 RID: 37016 RVA: 0x001E0CDF File Offset: 0x001DEEDF
		public static TableValue Combine(ListValue tableList, TableTypeValue tableType = null)
		{
			return TableValue.Combine(tableList.Select((IValueReference x) => x.Value.AsTable).ToArray<TableValue>(), null, tableType);
		}

		// Token: 0x06009099 RID: 37017 RVA: 0x001E0D14 File Offset: 0x001DEF14
		public static TableValue Combine(TableValue[] tables, string disjointColumn, TableTypeValue tableType = null)
		{
			if (tables.Length == 0)
			{
				tables = new TableValue[] { TableValue.Empty };
			}
			if (tables.Length == 1 && disjointColumn == null && tableType == null)
			{
				return tables[0];
			}
			Keys keys;
			if (tableType == null)
			{
				KeysBuilder keysBuilder = default(KeysBuilder);
				for (int i = 0; i < tables.Length; i++)
				{
					keysBuilder.Union(tables[i].Columns);
				}
				keys = keysBuilder.ToKeys();
			}
			else
			{
				keys = tableType.ItemType.Fields.Keys;
			}
			ListValue listValue = ListValue.New(keys.ToArray<string>());
			Query[] array = new Query[tables.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = tables[j].AsTable.SelectColumns(listValue, Library.MissingField.UseNull).Query;
			}
			return new QueryTableValue(Query.Combine(array, null, null, (disjointColumn != null) ? keys.IndexOfKey(disjointColumn) : (-1)), tableType);
		}

		// Token: 0x0600909A RID: 37018 RVA: 0x001E0DEC File Offset: 0x001DEFEC
		public TableValue Unpivot(ListValue toPivot, TextValue attributeColumn, TextValue valueColumn, bool dynamicColumnSelection)
		{
			string[] array = toPivot.Select((IValueReference v) => v.Value.AsString).Distinct<string>().ToArray<string>();
			string @string = attributeColumn.String;
			string string2 = valueColumn.String;
			TableTypeValue asTableType = this.Type.AsTableType;
			RecordValue fields = asTableType.ItemType.Fields;
			if (fields.Keys.Contains(@string))
			{
				throw ValueException.DuplicateField(@string);
			}
			if (fields.Keys.Contains(string2))
			{
				throw ValueException.DuplicateField(string2);
			}
			foreach (string text in array)
			{
				if (!fields.Keys.Contains(text))
				{
					throw ValueException.TableColumnNotFound(text);
				}
			}
			if (dynamicColumnSelection)
			{
				HashSet<string> hashSet = new HashSet<string>(array);
				string[] array3 = new string[fields.Keys.Length - hashSet.Count];
				int num = 0;
				foreach (string text2 in fields.Keys)
				{
					if (!hashSet.Contains(text2))
					{
						array3[num] = text2;
						num++;
					}
				}
				array = array3;
			}
			if (array.Length == 0)
			{
				throw ValueException.InsufficientElements(ListValue.Empty);
			}
			TableTypeValue tableTypeValue = TableValue.ConstructUnpivotTableType(new HashSet<string>(array), @string, string2, asTableType);
			return new QueryTableValue(this.Query.Unpivot(this.Type.AsTableType, tableTypeValue, array, @string, string2));
		}

		// Token: 0x0600909B RID: 37019 RVA: 0x001E0F78 File Offset: 0x001DF178
		public static TableTypeValue ConstructUnpivotTableType(HashSet<string> pivotColumns, string attribute, string value, TableTypeValue inputTableType)
		{
			IList<TableKey> tableKeys = inputTableType.TableKeys;
			RecordValue fields = inputTableType.ItemType.Fields;
			List<NamedValue> list = new List<NamedValue>();
			TypeValue typeValue = TypeValue.None;
			Dictionary<int, int> nonPivotColumnIndices = new Dictionary<int, int>();
			int num = 0;
			int num2 = 0;
			foreach (NamedValue namedValue in fields.GetFields())
			{
				if (pivotColumns.Contains(namedValue.Key))
				{
					TypeValue asType = namedValue.Value["Type"].AsType;
					typeValue = TypeAlgebra.Union(typeValue, asType.NonNullable);
				}
				else
				{
					list.Add(new NamedValue(namedValue.Key, namedValue.Value));
					nonPivotColumnIndices.Add(num, num2);
					num2++;
				}
				num++;
			}
			Value[] array = pivotColumns.Select((string c) => TextValue.New(c)).ToArray<TextValue>();
			ListValue listValue = ListValue.New(array);
			list.Add(new NamedValue(attribute, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeServices.SetDomain(TypeValue.Text, listValue),
				LogicalValue.False
			})));
			list.Add(new NamedValue(value, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				typeValue,
				LogicalValue.False
			})));
			int num3 = list.Count - 2;
			List<TableKey> list2 = new List<TableKey>();
			Func<int, int> <>9__1;
			foreach (TableKey tableKey in tableKeys)
			{
				if (tableKey.Columns.All(new Func<int, bool>(nonPivotColumnIndices.Keys.Contains<int>)))
				{
					int[] columns = tableKey.Columns;
					Func<int, int> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (int c) => nonPivotColumnIndices[c]);
					}
					List<int> list3 = columns.Select(func).ToList<int>();
					list3.Add(num3);
					list2.Add(new TableKey(list3.ToArray(), tableKey.Primary));
				}
			}
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(list.ToArray())), list2);
		}

		// Token: 0x0600909C RID: 37020 RVA: 0x001E11D8 File Offset: 0x001DF3D8
		public TableValue Pivot(ListValue pivotValues, TextValue attributeColumn, TextValue valueColumn, Value aggregationFunction)
		{
			FunctionValue functionValue = (aggregationFunction.IsNull ? Library.List.SingleOrDefault : aggregationFunction.AsFunction);
			string[] array = pivotValues.Select((IValueReference v) => v.Value.AsString).ToArray<string>();
			string @string = attributeColumn.String;
			string string2 = valueColumn.String;
			TableTypeValue asTableType = this.Type.AsTableType;
			RecordValue fields = asTableType.ItemType.Fields;
			if (!fields.Keys.Contains(@string))
			{
				throw ValueException.TableColumnNotFound(@string);
			}
			if (!fields.Keys.Contains(string2))
			{
				throw ValueException.TableColumnNotFound(string2);
			}
			foreach (string text in array)
			{
				if (fields.Keys.Contains(text) && text != @string && text != string2)
				{
					throw ValueException.DuplicateField(text);
				}
			}
			TableTypeValue tableTypeValue = TableValue.ConstructPivotTableType(array, @string, string2, asTableType, functionValue);
			return new QueryTableValue(this.Query.Pivot(this.Type.AsTableType, tableTypeValue, array, @string, string2, functionValue));
		}

		// Token: 0x0600909D RID: 37021 RVA: 0x001E12F4 File Offset: 0x001DF4F4
		public static TableTypeValue ConstructPivotTableType(IEnumerable<string> pivotColumns, string attribute, string value, TableTypeValue inputTableType, FunctionValue aggregator)
		{
			IList<TableKey> tableKeys = inputTableType.TableKeys;
			RecordValue fields = inputTableType.ItemType.Fields;
			TypeValue returnType = aggregator.Type.AsFunctionType.ReturnType;
			TypeValue asType = fields[value]["Type"].AsType;
			TypeValue typeValue = TypeValue.Any;
			if (returnType != TypeValue.Any)
			{
				typeValue = returnType;
			}
			else if (TableValue.sameOutputTypeFunctions.Contains(aggregator))
			{
				typeValue = asType.Nullable;
			}
			List<NamedValue> list = new List<NamedValue>();
			Dictionary<int, int> nonPivotColumnIndices = new Dictionary<int, int>();
			int num = 0;
			int num2 = 0;
			int attributeIndex = -1;
			int num3 = -1;
			foreach (NamedValue namedValue in fields.GetFields())
			{
				if (namedValue.Key == attribute)
				{
					attributeIndex = num;
				}
				else if (namedValue.Key == value)
				{
					num3 = num;
				}
				else
				{
					list.Add(new NamedValue(namedValue.Key, namedValue.Value));
					nonPivotColumnIndices.Add(num, num2);
					num2++;
				}
				num++;
			}
			foreach (string text in pivotColumns)
			{
				list.Add(new NamedValue(text, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					LogicalValue.False
				})));
			}
			List<TableKey> list2 = new List<TableKey>();
			Func<int, bool> <>9__0;
			Func<int, int> <>9__1;
			foreach (TableKey tableKey in tableKeys)
			{
				if (!tableKey.Columns.Contains(num3))
				{
					IEnumerable<int> columns = tableKey.Columns;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int c) => c != attributeIndex);
					}
					IEnumerable<int> enumerable = columns.Where(func);
					Func<int, int> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (int c) => nonPivotColumnIndices[c]);
					}
					List<int> list3 = enumerable.Select(func2).ToList<int>();
					list2.Add(new TableKey(list3.ToArray(), tableKey.Primary));
				}
			}
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(list.ToArray())), list2);
		}

		// Token: 0x0600909E RID: 37022 RVA: 0x001E1578 File Offset: 0x001DF778
		public static TableTypeValue ConstructPivotTableType(IEnumerable<string> pivotColumns, string attribute, string value, TableTypeValue inputTableType)
		{
			FunctionValue singleOrDefault = Library.List.SingleOrDefault;
			return TableValue.ConstructPivotTableType(pivotColumns, attribute, value, inputTableType, singleOrDefault);
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x0600909F RID: 37023 RVA: 0x001E1595 File Offset: 0x001DF795
		IEnumerable<IRelationship> ITableValue.Relationships
		{
			get
			{
				return this.Relationships.Cast<IRelationship>();
			}
		}

		// Token: 0x060090A0 RID: 37024 RVA: 0x001E15A2 File Offset: 0x001DF7A2
		IColumnIdentity ITableValue.ColumnIdentity(int index)
		{
			if (this.ColumnIdentities != null)
			{
				return this.ColumnIdentities[index];
			}
			return null;
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x060090A1 RID: 37025 RVA: 0x001E15B6 File Offset: 0x001DF7B6
		long ITableValue.RowCount
		{
			get
			{
				return this.LargeCount;
			}
		}

		// Token: 0x170025CD RID: 9677
		IValue ITableValue.this[int column]
		{
			get
			{
				return this[column];
			}
		}

		// Token: 0x170025CE RID: 9678
		IListValue ITableValue.this[string column]
		{
			get
			{
				return this[column].AsList;
			}
		}

		// Token: 0x060090A4 RID: 37028 RVA: 0x001E15CC File Offset: 0x001DF7CC
		IEnumerator<IValueReference2> ITableValue.GetEnumerator()
		{
			return new TableValue.ValueReference2Enumerator(this.GetEnumerator());
		}

		// Token: 0x060090A5 RID: 37029 RVA: 0x001E15D9 File Offset: 0x001DF7D9
		public virtual TableValue DeltaSince(Value tag)
		{
			throw ValueException.NewDataSourceError<Message0>(Strings.Delta_SinceNotSupported, this, null);
		}

		// Token: 0x060090A6 RID: 37030 RVA: 0x001E15E7 File Offset: 0x001DF7E7
		public virtual ActionValue InsertRows(Query rowsToInsert)
		{
			return new QueryTableValue(this).InsertRows(rowsToInsert);
		}

		// Token: 0x060090A7 RID: 37031 RVA: 0x001E15F5 File Offset: 0x001DF7F5
		public virtual ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return new QueryTableValue(this).UpdateRows(columnUpdates);
		}

		// Token: 0x060090A8 RID: 37032 RVA: 0x001E1603 File Offset: 0x001DF803
		public virtual ActionValue DeleteRows()
		{
			return new QueryTableValue(this).DeleteRows();
		}

		// Token: 0x060090A9 RID: 37033 RVA: 0x001E1610 File Offset: 0x001DF810
		public ActionValue InsertRows(TableValue rowsToInsert)
		{
			TableValue.GetColumns(this.Columns, rowsToInsert.Columns);
			return this.InsertRows(rowsToInsert.Query);
		}

		// Token: 0x060090AA RID: 37034 RVA: 0x001E1630 File Offset: 0x001DF830
		public ActionValue UpdateRows(ListValue columnUpdates)
		{
			ColumnConstructor[] columnConstructors = TableValue.GetColumnConstructors(columnUpdates);
			Dictionary<int, FunctionValue> dictionary = new Dictionary<int, FunctionValue>(columnConstructors.Length);
			for (int i = 0; i < columnConstructors.Length; i++)
			{
				dictionary.Add(TableValue.GetColumn(this.Columns, columnConstructors[i].Name, true), columnConstructors[i].Function);
			}
			return this.UpdateRows(new ColumnUpdates(dictionary));
		}

		// Token: 0x060090AB RID: 37035 RVA: 0x001E1689 File Offset: 0x001DF889
		public override ActionValue Replace(Value value)
		{
			Func<Value> <>9__1;
			return new SimpleBindingActionValue(delegate(FunctionValue binding)
			{
				if (binding != SimpleActionBinding.ReturnNull)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
				}
				Value[] array = new Value[3];
				array[0] = this.DeleteRows();
				int num = 1;
				Func<Value> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.InsertRows(value.AsTable));
				}
				array[num] = FunctionValue.New(func);
				array[2] = ActionModule.Action.DoNothing;
				return ActionValue.New(ListValue.New(array));
			});
		}

		// Token: 0x04004DA3 RID: 19875
		private const string placeholder = "[Table]";

		// Token: 0x04004DA4 RID: 19876
		public static readonly TextValue Placeholder = TextValue.New("[Table]");

		// Token: 0x04004DA5 RID: 19877
		private static TableValue empty;

		// Token: 0x04004DA6 RID: 19878
		private static readonly FunctionValue[] sameOutputTypeFunctions = new FunctionValue[]
		{
			Library.List.First,
			Library.List.Last,
			Library.List.Single,
			Library.List.SingleOrDefault,
			Library.List.Max,
			Library.List.Min,
			Library.List.Median,
			Library.List.Average,
			Library.List.Mode,
			Library.List.Sum
		};

		// Token: 0x02001651 RID: 5713
		public enum GroupKind
		{
			// Token: 0x04004DA8 RID: 19880
			Local,
			// Token: 0x04004DA9 RID: 19881
			Global
		}

		// Token: 0x02001652 RID: 5714
		private struct ColumnBindings
		{
			// Token: 0x060090AE RID: 37038 RVA: 0x001E1729 File Offset: 0x001DF929
			public ColumnBindings(int[] columns, string[] missingColumns)
			{
				this.columns = columns;
				this.missingColumns = missingColumns;
			}

			// Token: 0x170025CF RID: 9679
			// (get) Token: 0x060090AF RID: 37039 RVA: 0x001E1739 File Offset: 0x001DF939
			public int[] Columns
			{
				get
				{
					return this.columns;
				}
			}

			// Token: 0x170025D0 RID: 9680
			// (get) Token: 0x060090B0 RID: 37040 RVA: 0x001E1741 File Offset: 0x001DF941
			public string[] MissingColumns
			{
				get
				{
					return this.missingColumns;
				}
			}

			// Token: 0x04004DAA RID: 19882
			private readonly int[] columns;

			// Token: 0x04004DAB RID: 19883
			private readonly string[] missingColumns;
		}

		// Token: 0x02001653 RID: 5715
		public class RowSelectorFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060090B1 RID: 37041 RVA: 0x001E1749 File Offset: 0x001DF949
			public RowSelectorFunctionValue(RecordValue key)
			{
				this.key = key;
			}

			// Token: 0x060090B2 RID: 37042 RVA: 0x001E1758 File Offset: 0x001DF958
			public override Value Invoke(Value value)
			{
				RecordValue asRecord = value.AsRecord;
				for (int i = 0; i < this.key.Keys.Length; i++)
				{
					if (!asRecord[this.key.Keys[i]].Equals(this.key[i]))
					{
						return LogicalValue.False;
					}
				}
				return LogicalValue.True;
			}

			// Token: 0x170025D1 RID: 9681
			// (get) Token: 0x060090B3 RID: 37043 RVA: 0x001E17BC File Offset: 0x001DF9BC
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateLookupSelectorExpression(this.key);
					}
					return this.expression;
				}
			}

			// Token: 0x04004DAC RID: 19884
			private readonly RecordValue key;

			// Token: 0x04004DAD RID: 19885
			private IFunctionExpression expression;
		}

		// Token: 0x02001654 RID: 5716
		public sealed class ColumnSelectorFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060090B4 RID: 37044 RVA: 0x001E17DD File Offset: 0x001DF9DD
			public ColumnSelectorFunctionValue(string key, int index)
				: base(Identifier.Underscore.Name)
			{
				this.key = key;
				this.index = index;
			}

			// Token: 0x060090B5 RID: 37045 RVA: 0x001E17FD File Offset: 0x001DF9FD
			public override Value Invoke(Value row)
			{
				return row[this.index];
			}

			// Token: 0x170025D2 RID: 9682
			// (get) Token: 0x060090B6 RID: 37046 RVA: 0x001E180B File Offset: 0x001DFA0B
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(this.key)));
					}
					return this.expression;
				}
			}

			// Token: 0x170025D3 RID: 9683
			// (get) Token: 0x060090B7 RID: 37047 RVA: 0x001E1845 File Offset: 0x001DFA45
			public int Column
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x170025D4 RID: 9684
			// (get) Token: 0x060090B8 RID: 37048 RVA: 0x001E184D File Offset: 0x001DFA4D
			public string FieldName
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x04004DAE RID: 19886
			private readonly string key;

			// Token: 0x04004DAF RID: 19887
			private readonly int index;

			// Token: 0x04004DB0 RID: 19888
			private IExpression expression;
		}

		// Token: 0x02001655 RID: 5717
		public class TransformColumnFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060090B9 RID: 37049 RVA: 0x001E1855 File Offset: 0x001DFA55
			public TransformColumnFunctionValue(string columnName, int column, FunctionValue function)
			{
				this.columnName = columnName;
				this.column = column;
				this.function = function;
			}

			// Token: 0x060090BA RID: 37050 RVA: 0x001E1872 File Offset: 0x001DFA72
			public override Value Invoke(Value record)
			{
				return this.function.Invoke(record[this.column]);
			}

			// Token: 0x170025D5 RID: 9685
			// (get) Token: 0x060090BB RID: 37051 RVA: 0x001E188C File Offset: 0x001DFA8C
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.function), new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), this.columnName)));
					}
					return this.expression;
				}
			}

			// Token: 0x04004DB1 RID: 19889
			private readonly string columnName;

			// Token: 0x04004DB2 RID: 19890
			private readonly int column;

			// Token: 0x04004DB3 RID: 19891
			private readonly FunctionValue function;

			// Token: 0x04004DB4 RID: 19892
			private IExpression expression;
		}

		// Token: 0x02001656 RID: 5718
		public sealed class FunctionsColumnsConstructorFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060090BC RID: 37052 RVA: 0x001E18E1 File Offset: 0x001DFAE1
			public FunctionsColumnsConstructorFunctionValue(params FunctionValue[] columnGenerators)
			{
				this.columnGenerators = columnGenerators;
			}

			// Token: 0x060090BD RID: 37053 RVA: 0x001E18F0 File Offset: 0x001DFAF0
			public override Value Invoke(Value row)
			{
				IValueReference[] array = new IValueReference[this.columnGenerators.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new TransformValueReference(row, this.columnGenerators[i]);
				}
				return ListValue.New(array);
			}

			// Token: 0x170025D6 RID: 9686
			// (get) Token: 0x060090BE RID: 37054 RVA: 0x001E1930 File Offset: 0x001DFB30
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						IExpression[] array = new IExpression[this.columnGenerators.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.columnGenerators[i]), new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore));
						}
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new ListExpressionSyntaxNode(array));
					}
					return this.expression;
				}
			}

			// Token: 0x170025D7 RID: 9687
			// (get) Token: 0x060090BF RID: 37055 RVA: 0x001E199C File Offset: 0x001DFB9C
			public FunctionValue[] ColumnGenerators
			{
				get
				{
					return this.columnGenerators;
				}
			}

			// Token: 0x04004DB5 RID: 19893
			private readonly FunctionValue[] columnGenerators;

			// Token: 0x04004DB6 RID: 19894
			private IExpression expression;
		}

		// Token: 0x02001657 RID: 5719
		private sealed class BufferedTableValue : TableValue
		{
			// Token: 0x060090C0 RID: 37056 RVA: 0x001E19A4 File Offset: 0x001DFBA4
			public BufferedTableValue(TableValue table)
			{
				this.table = table;
			}

			// Token: 0x060090C1 RID: 37057 RVA: 0x001E19B3 File Offset: 0x001DFBB3
			public static TableValue BufferTable(TableValue table)
			{
				return TableModule.Table.FromRecords.Invoke(Library.List.Buffer.Invoke(table.ToRecords()), table.Type).AsTable;
			}

			// Token: 0x170025D8 RID: 9688
			// (get) Token: 0x060090C2 RID: 37058 RVA: 0x001E19DA File Offset: 0x001DFBDA
			public override TypeValue Type
			{
				get
				{
					return this.table.Type;
				}
			}

			// Token: 0x060090C3 RID: 37059 RVA: 0x001E19E7 File Offset: 0x001DFBE7
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				if (this.buffered == null)
				{
					this.buffered = TableValue.BufferedTableValue.BufferTable(this.table);
				}
				return this.buffered.GetEnumerator();
			}

			// Token: 0x04004DB7 RID: 19895
			private readonly TableValue table;

			// Token: 0x04004DB8 RID: 19896
			private TableValue buffered;
		}

		// Token: 0x02001658 RID: 5720
		private class ValueReference2Enumerator : IEnumerator<IValueReference2>, IDisposable, IEnumerator
		{
			// Token: 0x060090C4 RID: 37060 RVA: 0x001E1A0D File Offset: 0x001DFC0D
			public ValueReference2Enumerator(IEnumerator<IValueReference> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x170025D9 RID: 9689
			// (get) Token: 0x060090C5 RID: 37061 RVA: 0x001E1A1C File Offset: 0x001DFC1C
			public IValueReference2 Current
			{
				get
				{
					return new ValueReference2(this.enumerator.Current);
				}
			}

			// Token: 0x060090C6 RID: 37062 RVA: 0x001E1A2E File Offset: 0x001DFC2E
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x170025DA RID: 9690
			// (get) Token: 0x060090C7 RID: 37063 RVA: 0x001E1A3B File Offset: 0x001DFC3B
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x060090C8 RID: 37064 RVA: 0x001E1A48 File Offset: 0x001DFC48
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060090C9 RID: 37065 RVA: 0x001E1A55 File Offset: 0x001DFC55
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04004DB9 RID: 19897
			private readonly IEnumerator<IValueReference> enumerator;
		}

		// Token: 0x02001659 RID: 5721
		private class ConstantListFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060090CA RID: 37066 RVA: 0x001E1A62 File Offset: 0x001DFC62
			public ConstantListFunctionValue(Value value)
			{
				this.list = ListValue.New(new Value[] { value });
			}

			// Token: 0x170025DB RID: 9691
			// (get) Token: 0x060090CB RID: 37067 RVA: 0x001E1A7F File Offset: 0x001DFC7F
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new ListExpressionSyntaxNode(new IExpression[]
						{
							new ConstantExpressionSyntaxNode(this.list.Item0)
						}));
					}
					return this.expression;
				}
			}

			// Token: 0x060090CC RID: 37068 RVA: 0x001E1ABD File Offset: 0x001DFCBD
			public override Value Invoke(Value row)
			{
				return this.list;
			}

			// Token: 0x04004DBA RID: 19898
			private readonly ListValue list;

			// Token: 0x04004DBB RID: 19899
			private IExpression expression;
		}

		// Token: 0x0200165A RID: 5722
		private class ConformingPageReader : DelegatingPageReader, IConformingPageReader, IPageReader, IDisposable
		{
			// Token: 0x060090CD RID: 37069 RVA: 0x001E1AC5 File Offset: 0x001DFCC5
			public ConformingPageReader(IPageReader reader)
				: base(reader)
			{
			}
		}
	}
}
