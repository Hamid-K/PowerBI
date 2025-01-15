using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FD7 RID: 8151
	internal class GroupSchemaElement : SchemaElement
	{
		// Token: 0x06011089 RID: 69769 RVA: 0x003ABE78 File Offset: 0x003AA078
		public GroupSchemaElement(string name, Repetition repetition, ParquetGroupTypeMap typeMap, Keys fieldKeys, SchemaElement[] fields, RepeatedTypeKind repeatedTypeKind = RepeatedTypeKind.Default)
			: base(name, repetition, typeMap.LogicalTypeType, new Func<LogicalType>(typeMap.CreateLogicalType), repeatedTypeKind)
		{
			this.typeMap = typeMap;
			this.fieldKeys = fieldKeys;
			this.fields = fields;
			for (int i = 0; i < fields.Length; i++)
			{
				SchemaElement.SetParent(fields[i], this);
			}
		}

		// Token: 0x17002CEB RID: 11499
		// (get) Token: 0x0601108A RID: 69770 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override NodeType ElementType
		{
			get
			{
				return NodeType.Group;
			}
		}

		// Token: 0x17002CEC RID: 11500
		// (get) Token: 0x0601108B RID: 69771 RVA: 0x003ABED1 File Offset: 0x003AA0D1
		public SchemaElement[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x17002CED RID: 11501
		// (get) Token: 0x0601108C RID: 69772 RVA: 0x003ABEDC File Offset: 0x003AA0DC
		public override IList<PrimitiveSchemaElement> PrimitiveElements
		{
			get
			{
				if (this.primitiveElements == null)
				{
					this.primitiveElements = new List<PrimitiveSchemaElement>();
					foreach (SchemaElement schemaElement in this.Fields)
					{
						this.primitiveElements.AddRange(schemaElement.PrimitiveElements);
					}
				}
				return this.primitiveElements;
			}
		}

		// Token: 0x17002CEE RID: 11502
		// (get) Token: 0x0601108D RID: 69773 RVA: 0x003ABF2C File Offset: 0x003AA12C
		public Keys FieldKeys
		{
			get
			{
				return this.fieldKeys;
			}
		}

		// Token: 0x17002CEF RID: 11503
		// (get) Token: 0x0601108E RID: 69774 RVA: 0x003ABF34 File Offset: 0x003AA134
		public RecordTypeValue RecordTypeValue
		{
			get
			{
				if (this.recordTypeValue == null)
				{
					RecordBuilder recordBuilder = new RecordBuilder(this.Fields.Length);
					for (int i = 0; i < this.Fields.Length; i++)
					{
						int index = i;
						recordBuilder.Add(this.FieldKeys[i], RecordValue.New(RecordTypeValue.RecordFieldKeys, new IValueReference[]
						{
							new DelayedValue(() => this.Fields[index].TypeValue),
							LogicalValue.False
						}), TypeValue.Any);
					}
					this.recordTypeValue = RecordTypeValue.New(recordBuilder.ToRecord());
				}
				return this.recordTypeValue;
			}
		}

		// Token: 0x17002CF0 RID: 11504
		// (get) Token: 0x0601108F RID: 69775 RVA: 0x003ABFE0 File Offset: 0x003AA1E0
		public override TypeValue ItemTypeValue
		{
			get
			{
				if (this.itemTypeValue == null)
				{
					this.itemTypeValue = this.typeMap.ToTypeValue(this.RecordTypeValue);
					this.itemTypeValue = this.itemTypeValue.NewFacets(this.typeMap.Facets);
				}
				return this.itemTypeValue;
			}
		}

		// Token: 0x06011090 RID: 69776 RVA: 0x003AC030 File Offset: 0x003AA230
		public override Node CreateNode()
		{
			Node[] array = new Node[this.Fields.Length];
			Node node;
			try
			{
				for (int i = 0; i < this.Fields.Length; i++)
				{
					array[i] = this.Fields[i].CreateNode();
				}
				LogicalType logicalType = null;
				try
				{
					if (base.LogicalTypeType != LogicalTypeEnum.None)
					{
						logicalType = base.CreateLogicalType();
					}
					node = new GroupNode(base.Name, base.Repetition, array, logicalType);
				}
				finally
				{
					if (logicalType != null)
					{
						logicalType.Dispose();
					}
				}
			}
			finally
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						array[j].Dispose();
					}
				}
			}
			return node;
		}

		// Token: 0x06011091 RID: 69777 RVA: 0x003AC0E0 File Offset: 0x003AA2E0
		public TableSchema CreateTableSchema(out ParquetPrimitiveTypeMap[] typeMaps)
		{
			typeMaps = new ParquetPrimitiveTypeMap[this.Fields.Length];
			TableSchema tableSchema = new TableSchema();
			for (int i = 0; i < typeMaps.Length; i++)
			{
				PrimitiveSchemaElement primitiveSchemaElement = this.Fields[i] as PrimitiveSchemaElement;
				if (primitiveSchemaElement == null)
				{
					throw new NotSupportedException();
				}
				typeMaps[i] = primitiveSchemaElement.TypeMap;
				SchemaColumn schemaColumn = new SchemaColumn(this.FieldKeys[i]);
				schemaColumn.Ordinal = new int?(i);
				schemaColumn.DataType = primitiveSchemaElement.TypeMap.Type;
				schemaColumn.Nullable = primitiveSchemaElement.DefinitionLevel > 0;
				TypeFacets facets = primitiveSchemaElement.TypeMap.Facets;
				if (facets.NativeTypeName != null)
				{
					schemaColumn.DataTypeName = facets.NativeTypeName;
				}
				if (facets.MaxLength != null)
				{
					schemaColumn.ColumnSize = new long?(facets.MaxLength.Value);
				}
				if (facets.NumericPrecisionBase != null)
				{
					schemaColumn.NumericBase = new int?(facets.NumericPrecisionBase.Value);
				}
				if (facets.NumericPrecision != null)
				{
					schemaColumn.NumericPrecision = new int?(facets.NumericPrecision.Value);
				}
				if (facets.NumericScale != null)
				{
					schemaColumn.NumericScale = new int?(facets.NumericScale.Value);
				}
				using (LogicalType logicalType = primitiveSchemaElement.TypeMap.CreateLogicalType())
				{
					IntLogicalType intLogicalType = logicalType as IntLogicalType;
					if (intLogicalType != null)
					{
						schemaColumn.IsUnsigned = new bool?(!intLogicalType.IsSigned);
					}
				}
				tableSchema.AddColumn(schemaColumn);
			}
			return tableSchema;
		}

		// Token: 0x06011092 RID: 69778 RVA: 0x003AC2A4 File Offset: 0x003AA4A4
		public override Value ToValue(object raw)
		{
			if (this.toValue == null)
			{
				Func<object, Value> toValue = this.typeMap.GetToValue(this.ItemTypeValue);
				Func<Value, Value> conformer = GroupSchemaElement.ConformToType(this.ItemTypeValue, toValue(raw).Type);
				this.toValue = (object obj) => conformer(toValue(obj));
			}
			return this.toValue(raw);
		}

		// Token: 0x06011093 RID: 69779 RVA: 0x003AC316 File Offset: 0x003AA516
		public override object FromValue(IAllocator allocator, Value value)
		{
			if (this.fromValue == null)
			{
				this.fromValue = this.typeMap.GetFromValue(this.ItemTypeValue);
			}
			return this.fromValue(allocator, value);
		}

		// Token: 0x06011094 RID: 69780 RVA: 0x003AC344 File Offset: 0x003AA544
		public override bool TrySelectColumns(NestedColumnSelection columnSelection, out SchemaElement schemaElement)
		{
			NestedColumnSelection nestedColumnSelection = columnSelection;
			try
			{
				nestedColumnSelection = this.typeMap.MapColumnSelection(nestedColumnSelection);
			}
			catch (NotSupportedException)
			{
				schemaElement = null;
				return false;
			}
			if (!nestedColumnSelection.IsAll && nestedColumnSelection.ColumnSelection.Keys.Length == 0)
			{
				nestedColumnSelection = new NestedColumnSelection(new ColumnSelection(Keys.New(this.FieldKeys[0])), new NestedColumnSelection[]
				{
					new NestedColumnSelection(new ColumnSelection(Keys.Empty), null)
				});
			}
			SchemaElement[] array = new SchemaElement[nestedColumnSelection.IsAll ? this.Fields.Length : nestedColumnSelection.ColumnSelection.Keys.Length];
			int num = -1;
			KeysBuilder keysBuilder = new KeysBuilder(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				int column = nestedColumnSelection.GetColumn(i);
				string text = this.FieldKeys[column];
				if (!nestedColumnSelection.IsAll && num >= column)
				{
					schemaElement = null;
					return false;
				}
				keysBuilder.Add(text);
				SchemaElement schemaElement2 = this.Fields[column];
				NestedColumnSelection nestedColumnSelection2 = nestedColumnSelection.GetNestedColumnSelection(i);
				if (!schemaElement2.TrySelectColumns(nestedColumnSelection2, out array[i]))
				{
					schemaElement = null;
					return false;
				}
				num = column;
			}
			schemaElement = new GroupSchemaElement(base.Name, base.Repetition, this.typeMap, keysBuilder.ToKeys(), array, this.repeatedTypeKind)
			{
				itemTypeValue = GroupSchemaElement.ApplyColumnSelection(this.ItemTypeValue, columnSelection)
			};
			return true;
		}

		// Token: 0x06011095 RID: 69781 RVA: 0x003AC4C4 File Offset: 0x003AA6C4
		public bool TryGetPrimitiveColumnSelection(SchemaDescriptor schemaDescriptor, out int[] columnSelection, SchemaConfig config = null)
		{
			Dictionary<string, List<ValueTuple<string[], int>>> dictionary = new Dictionary<string, List<ValueTuple<string[], int>>>(this.PrimitiveElements.Count);
			for (int i = 0; i < this.PrimitiveElements.Count; i++)
			{
				PrimitiveSchemaElement primitiveSchemaElement = this.PrimitiveElements[i];
				string[] path = primitiveSchemaElement.Path;
				string pathString = primitiveSchemaElement.PathString;
				List<ValueTuple<string[], int>> list;
				if (!dictionary.TryGetValue(pathString, out list))
				{
					list = new List<ValueTuple<string[], int>>(1);
					dictionary.Add(pathString, list);
				}
				list.Add(new ValueTuple<string[], int>(path, i));
			}
			columnSelection = new int[this.PrimitiveElements.Count];
			int num = 0;
			for (int j = 0; j < schemaDescriptor.NumColumns; j++)
			{
				string text;
				string[] dotVector;
				using (Node schemaNode = schemaDescriptor.Column(j).SchemaNode)
				{
					using (ColumnPath path2 = schemaNode.Path)
					{
						text = path2.ToDotString();
						dotVector = path2.ToDotVector();
					}
				}
				List<ValueTuple<string[], int>> list2;
				if (dictionary.TryGetValue(text, out list2))
				{
					int num2 = list2.FindIndex((ValueTuple<string[], int> pair) => pair.Item1.SequenceEqual(dotVector));
					if (num2 >= 0)
					{
						num++;
						columnSelection[list2[num2].Item2] = j;
						list2.RemoveAt(num2);
						if (num == this.PrimitiveElements.Count)
						{
							break;
						}
					}
				}
			}
			if (num != this.PrimitiveElements.Count)
			{
				columnSelection = null;
				return false;
			}
			return true;
		}

		// Token: 0x06011096 RID: 69782 RVA: 0x003AC648 File Offset: 0x003AA848
		private static TypeValue ApplyColumnSelection(TypeValue type, NestedColumnSelection columnSelection)
		{
			if (columnSelection.IsAll)
			{
				return type;
			}
			RecordTypeValue recordTypeValue;
			if (type.IsRecordType)
			{
				recordTypeValue = type.AsRecordType;
			}
			else
			{
				if (!type.IsTableType)
				{
					return TypeValue.None;
				}
				recordTypeValue = type.AsTableType.ItemType;
			}
			RecordValue[] array = new RecordValue[columnSelection.ColumnSelection.Keys.Length];
			for (int j = 0; j < array.Length; j++)
			{
				int column = columnSelection.GetColumn(j);
				bool flag;
				TypeValue fieldType = recordTypeValue.GetFieldType(column, out flag);
				NestedColumnSelection nestedColumnSelection = columnSelection.GetNestedColumnSelection(j);
				array[j] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new IValueReference[]
				{
					new DelayedValue(() => GroupSchemaElement.ApplyColumnSelection(fieldType, nestedColumnSelection)),
					LogicalValue.New(flag)
				});
			}
			Keys keys = columnSelection.ColumnSelection.Keys;
			Value[] array2 = array;
			RecordTypeValue recordTypeValue2 = RecordTypeValue.New(RecordValue.New(keys, array2));
			if (type.IsTableType)
			{
				IList<TableKey> list = type.AsTableType.TableKeys;
				if (list.Count != 0)
				{
					ColumnSelection.SelectMap selectMap = columnSelection.ColumnSelection.CreateSelectMap(recordTypeValue.FieldKeys);
					List<TableKey> list2 = new List<TableKey>(list.Count);
					Func<int, bool> <>9__1;
					foreach (TableKey tableKey in list)
					{
						int[] array3 = selectMap.MapColumns(tableKey.Columns);
						if (array3 != null)
						{
							IEnumerable<int> enumerable = array3;
							Func<int, bool> func;
							if ((func = <>9__1) == null)
							{
								func = (<>9__1 = (int i) => columnSelection.GetNestedColumnSelection(i).IsAll);
							}
							if (enumerable.All(func))
							{
								list2.Add(tableKey.SelectColumns(array3));
							}
						}
					}
					IList<TableKey> list3;
					if (list2.Count <= 0)
					{
						list3 = TableKeys.None;
					}
					else
					{
						IList<TableKey> list4 = list2.ToArray();
						list3 = list4;
					}
					list = list3;
				}
				return TableTypeValue.New(recordTypeValue2, list);
			}
			return recordTypeValue2;
		}

		// Token: 0x06011097 RID: 69783 RVA: 0x003AC854 File Offset: 0x003AAA54
		private static Func<Value, Value> ConformToType(TypeValue expectedType, TypeValue actualType)
		{
			if (expectedType == actualType)
			{
				return GroupSchemaElement.NopConformer;
			}
			if (expectedType.IsRecordType)
			{
				RecordTypeValue expectedRecordType = expectedType.AsRecordType;
				if (!actualType.IsRecordType)
				{
					return (Value value) => GroupSchemaElement.ConformToType(expectedType, value.Type.AsRecordType)(value);
				}
				RecordTypeValue asRecordType = actualType.AsRecordType;
				Func<Value, Value>[] fieldConformers = new Func<Value, Value>[expectedRecordType.FieldKeys.Length];
				int[] columns = new int[expectedRecordType.FieldKeys.Length];
				bool flag = true;
				for (int i = 0; i < fieldConformers.Length; i++)
				{
					int num = i;
					int num2 = asRecordType.FieldKeys.IndexOfKey(expectedRecordType.FieldKeys[num]);
					columns[num] = num2;
					fieldConformers[num] = GroupSchemaElement.ConformToType(expectedRecordType.Fields[num]["Type"].AsType, asRecordType.Fields[num2]["Type"].AsType);
					flag &= fieldConformers[num] == GroupSchemaElement.NopConformer;
				}
				if (flag && expectedRecordType.FieldKeys.Equals(asRecordType.FieldKeys))
				{
					return GroupSchemaElement.NopConformer;
				}
				if (expectedRecordType.FieldKeys.Length == 0)
				{
					return (Value value) => RecordValue.Empty;
				}
				return delegate(Value value)
				{
					RecordValue record = value.AsRecord;
					IValueReference[] array = new IValueReference[expectedRecordType.FieldKeys.Length];
					for (int j = 0; j < array.Length; j++)
					{
						int source = columns[j];
						Func<Value, Value> conformer2 = fieldConformers[j];
						IValueReference[] array2 = array;
						int num3 = j;
						IValueReference valueReference2;
						if (conformer2 != null)
						{
							IValueReference valueReference = new DelayedValue(() => conformer2(record[source]));
							valueReference2 = valueReference;
						}
						else
						{
							IValueReference valueReference = record[source];
							valueReference2 = valueReference;
						}
						array2[num3] = valueReference2;
					}
					return RecordValue.New(expectedRecordType, array);
				};
			}
			else
			{
				if (!expectedType.IsTableType)
				{
					return GroupSchemaElement.NopConformer;
				}
				TableTypeValue expectedTableType = expectedType.AsTableType;
				RecordTypeValue itemType = expectedTableType.ItemType;
				if (!actualType.IsTableType)
				{
					return (Value value) => GroupSchemaElement.ConformToType(expectedType, value.Type.AsTableType)(value);
				}
				RecordTypeValue itemType2 = actualType.AsTableType.ItemType;
				Func<Value, Value> conformer = GroupSchemaElement.ConformToType(itemType, itemType2);
				if (conformer == GroupSchemaElement.NopConformer)
				{
					return GroupSchemaElement.NopConformer;
				}
				Func<IValueReference, Value> <>9__6;
				return delegate(Value value)
				{
					IEnumerable<IValueReference> asTable = value.AsTable;
					Func<IValueReference, Value> func;
					if ((func = <>9__6) == null)
					{
						func = (<>9__6 = (IValueReference row) => conformer(row.Value));
					}
					return ListValue.New(asTable.Select(func)).ToTable(expectedTableType);
				};
			}
		}

		// Token: 0x040066EE RID: 26350
		private static readonly Func<Value, Value> NopConformer = (Value value) => value;

		// Token: 0x040066EF RID: 26351
		private readonly ParquetGroupTypeMap typeMap;

		// Token: 0x040066F0 RID: 26352
		private readonly Keys fieldKeys;

		// Token: 0x040066F1 RID: 26353
		private readonly SchemaElement[] fields;

		// Token: 0x040066F2 RID: 26354
		private RecordTypeValue recordTypeValue;

		// Token: 0x040066F3 RID: 26355
		private TypeValue itemTypeValue;

		// Token: 0x040066F4 RID: 26356
		private Func<object, Value> toValue;

		// Token: 0x040066F5 RID: 26357
		private Func<IAllocator, Value, object> fromValue;

		// Token: 0x040066F6 RID: 26358
		private List<PrimitiveSchemaElement> primitiveElements;
	}
}
