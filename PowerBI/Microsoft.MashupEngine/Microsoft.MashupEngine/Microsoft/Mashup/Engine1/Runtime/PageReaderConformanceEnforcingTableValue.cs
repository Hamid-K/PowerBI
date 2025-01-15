using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Excel;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015C4 RID: 5572
	internal class PageReaderConformanceEnforcingTableValue : DelegatingTableValue
	{
		// Token: 0x06008B8E RID: 35726 RVA: 0x001D54D8 File Offset: 0x001D36D8
		public PageReaderConformanceEnforcingTableValue(TableValue table)
			: base(table)
		{
			this.shapes = new PageReaderConformanceEnforcingTableValue.ColumnShape[table.Columns.Length];
			for (int i = 0; i < table.Columns.Length; i++)
			{
				this.shapes[i] = new PageReaderConformanceEnforcingTableValue.ColumnShape(table.GetColumnType(i));
			}
		}

		// Token: 0x06008B8F RID: 35727 RVA: 0x001D552C File Offset: 0x001D372C
		private PageReaderConformanceEnforcingTableValue(TableValue table, PageReaderConformanceEnforcingTableValue.ColumnShape[] shapes)
			: base(table)
		{
			this.shapes = shapes;
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x06008B90 RID: 35728 RVA: 0x001D553C File Offset: 0x001D373C
		public int MaxDepth
		{
			get
			{
				if (this.shapes.Length == 0)
				{
					return 0;
				}
				return Math.Min(100, this.shapes.Max((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.MaxDepth));
			}
		}

		// Token: 0x06008B91 RID: 35729 RVA: 0x001D557A File Offset: 0x001D377A
		public override TableValue SelectColumns(ColumnSelection columnSelection)
		{
			return new PageReaderConformanceEnforcingTableValue(base.SelectColumns(columnSelection), this.SelectColumnShapes(columnSelection));
		}

		// Token: 0x06008B92 RID: 35730 RVA: 0x001D558F File Offset: 0x001D378F
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			if (base.TrySelectColumns(columnSelection, out table))
			{
				table = new PageReaderConformanceEnforcingTableValue(table, this.SelectColumnShapes(columnSelection));
				return true;
			}
			return false;
		}

		// Token: 0x06008B93 RID: 35731 RVA: 0x001D55AE File Offset: 0x001D37AE
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.New(base.SelectRows(condition));
		}

		// Token: 0x06008B94 RID: 35732 RVA: 0x001D55C0 File Offset: 0x001D37C0
		public override TableValue TransformColumns(ColumnTransforms columnTransforms)
		{
			PageReaderConformanceEnforcingTableValue.ColumnShape[] array = this.shapes.ShallowCopy<PageReaderConformanceEnforcingTableValue.ColumnShape>();
			foreach (KeyValuePair<int, ColumnTransform> keyValuePair in columnTransforms.Dictionary)
			{
				IExpression functionExpression = PageReaderConformanceEnforcingTableValue.GetFunctionExpression(keyValuePair.Value.Function);
				PageReaderConformanceEnforcingTableValue.ColumnShape columnShape;
				if (!array[keyValuePair.Key].TryApplyPatterns(functionExpression, out columnShape))
				{
					columnShape = new PageReaderConformanceEnforcingTableValue.ColumnShape(keyValuePair.Value.Type.Value.AsType);
				}
				array[keyValuePair.Key] = columnShape;
			}
			return new PageReaderConformanceEnforcingTableValue(base.TransformColumns(columnTransforms), array);
		}

		// Token: 0x06008B95 RID: 35733 RVA: 0x001D5674 File Offset: 0x001D3874
		public override TableValue Skip(RowCount count)
		{
			return this.New(base.Skip(count));
		}

		// Token: 0x06008B96 RID: 35734 RVA: 0x001D5683 File Offset: 0x001D3883
		public override TableValue Take(RowCount count)
		{
			return this.New(base.Take(count));
		}

		// Token: 0x06008B97 RID: 35735 RVA: 0x001D5692 File Offset: 0x001D3892
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.New(base.Sort(sortOrder));
		}

		// Token: 0x06008B98 RID: 35736 RVA: 0x001D56A1 File Offset: 0x001D38A1
		public override TableValue Unordered()
		{
			return this.New(base.Unordered());
		}

		// Token: 0x06008B99 RID: 35737 RVA: 0x001D56AF File Offset: 0x001D38AF
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables));
		}

		// Token: 0x06008B9A RID: 35738 RVA: 0x001D56C3 File Offset: 0x001D38C3
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships));
		}

		// Token: 0x06008B9B RID: 35739 RVA: 0x001D56D9 File Offset: 0x001D38D9
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			return this.New(base.Table.ReplaceRelationshipIdentity(identity));
		}

		// Token: 0x06008B9C RID: 35740 RVA: 0x001D56ED File Offset: 0x001D38ED
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return this.New(base.Table.ReplaceColumnIdentities(columnIdentities));
		}

		// Token: 0x06008B9D RID: 35741 RVA: 0x001D5701 File Offset: 0x001D3901
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelationships(relationships));
		}

		// Token: 0x06008B9E RID: 35742 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.NotSupported();
		}

		// Token: 0x06008B9F RID: 35743 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue Group(Grouping grouping)
		{
			return this.NotSupported();
		}

		// Token: 0x06008BA0 RID: 35744 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.NotSupported();
		}

		// Token: 0x06008BA1 RID: 35745 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.NotSupported();
		}

		// Token: 0x06008BA2 RID: 35746 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.NotSupported();
		}

		// Token: 0x06008BA3 RID: 35747 RVA: 0x001D5715 File Offset: 0x001D3915
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.NotSupported();
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x001D571D File Offset: 0x001D391D
		private TableValue NotSupported()
		{
			throw ValueException.NewExpressionError<Message0>(Strings.UnreachableCodePath, null, null);
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x001D572B File Offset: 0x001D392B
		private PageReaderConformanceEnforcingTableValue New(TableValue table)
		{
			return new PageReaderConformanceEnforcingTableValue(table, this.shapes);
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x001D573C File Offset: 0x001D393C
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			FunctionValue functionValue;
			if (ExcelModule.IsShapingFunction(function, out functionValue))
			{
				TableValue asTable = functionValue.Invoke(arguments).AsTable;
				PageReaderConformanceEnforcingTableValue.ColumnShape[] array = new PageReaderConformanceEnforcingTableValue.ColumnShape[asTable.Columns.Length];
				for (int i = 0; i < asTable.Columns.Length; i++)
				{
					array[i] = new PageReaderConformanceEnforcingTableValue.ColumnShape(asTable.GetColumnType(i)).ReshapeForExcel();
				}
				result = new PageReaderConformanceEnforcingTableValue(asTable, array);
				return true;
			}
			return base.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06008BA7 RID: 35751 RVA: 0x001D57B4 File Offset: 0x001D39B4
		public override IPageReader GetReader()
		{
			IPageReader reader = base.GetReader();
			if (!(reader is IConformingPageReader))
			{
				return reader;
			}
			if (this.MaxDepth >= 100)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.PageReader_UnsupportedShape, TextValue.New("Depth"), null);
			}
			ValueKind invalidKind = ValueKind.None;
			if (this.shapes.Any((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.MightHaveType(PageReaderConformanceEnforcingTableValue.invalidTypes, out invalidKind)) || this.shapes.Any((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.MightHaveMetadataType(PageReaderConformanceEnforcingTableValue.invalidMetadataTypes, out invalidKind)))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.PageReader_UnsupportedShape, TextValue.New(invalidKind.ToString()), null);
			}
			if (this.shapes.Any((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.HasTypeMetadata))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.PageReader_UnsupportedShape, TextValue.New("TypeMetadata"), null);
			}
			bool[] array = this.shapes.Select((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.HasMetadata).ToArray<bool>();
			IPageReader pageReader;
			try
			{
				pageReader = new DataReaderPageReader(new TableDataReader(this.Type.AsTableType, new TableValueDataReader(this, false), new PageReaderConformanceEnforcingTableValue.ConformingDataMapper(array)), new DataReaderPageReader.ExceptionPropertyGetter(PageExceptionSerializer.TryGetPropertiesFromException));
			}
			catch (ValueException ex)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.PageReader_UnsupportedShape, Value.Null, ex);
			}
			return pageReader;
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x001D591C File Offset: 0x001D3B1C
		private static IExpression GetFunctionExpression(FunctionValue function)
		{
			IExpression expression = function.Expression;
			if (expression == null)
			{
				return ConstantExpressionSyntaxNode.Null;
			}
			if (expression.Kind == ExpressionKind.Function)
			{
				return NormalizationVisitor.Normalize(expression, false);
			}
			return expression;
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x001D594C File Offset: 0x001D3B4C
		private PageReaderConformanceEnforcingTableValue.ColumnShape[] SelectColumnShapes(ColumnSelection columnSelection)
		{
			PageReaderConformanceEnforcingTableValue.ColumnShape[] array = new PageReaderConformanceEnforcingTableValue.ColumnShape[columnSelection.Keys.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				array[i] = this.shapes[columnSelection.GetColumn(i)];
			}
			return array;
		}

		// Token: 0x06008BAA RID: 35754 RVA: 0x001D5992 File Offset: 0x001D3B92
		internal Value GetShape()
		{
			return RecordValue.New(this.Columns, this.shapes.Select((PageReaderConformanceEnforcingTableValue.ColumnShape s) => s.GetDescription()).ToArray<IValueReference>());
		}

		// Token: 0x04004C77 RID: 19575
		public const int UnboundedDepth = 100;

		// Token: 0x04004C78 RID: 19576
		private static readonly PageReaderConformanceEnforcingTableValue.TypeSet invalidTypes = new PageReaderConformanceEnforcingTableValue.TypeSet(new ValueKind[]
		{
			ValueKind.Action,
			ValueKind.Function,
			ValueKind.List,
			ValueKind.Table,
			ValueKind.Type
		});

		// Token: 0x04004C79 RID: 19577
		private static readonly PageReaderConformanceEnforcingTableValue.TypeSet invalidMetadataTypes = new PageReaderConformanceEnforcingTableValue.TypeSet(new ValueKind[]
		{
			ValueKind.Action,
			ValueKind.Function,
			ValueKind.List,
			ValueKind.Table,
			ValueKind.Record,
			ValueKind.Type
		});

		// Token: 0x04004C7A RID: 19578
		private readonly PageReaderConformanceEnforcingTableValue.ColumnShape[] shapes;

		// Token: 0x020015C5 RID: 5573
		private class ColumnShape
		{
			// Token: 0x06008BAC RID: 35756 RVA: 0x001D5A08 File Offset: 0x001D3C08
			static ColumnShape()
			{
				PageReaderConformanceEnforcingTableValue.ColumnShape.Any.metadata = PageReaderConformanceEnforcingTableValue.ColumnShape.AnyMetadata;
				PageReaderConformanceEnforcingTableValue.ColumnShape.Any.recordField = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
				PageReaderConformanceEnforcingTableValue.ColumnShape.Any.listItem = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
				PageReaderConformanceEnforcingTableValue.ColumnShape.Any.tableColumn = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
			}

			// Token: 0x06008BAD RID: 35757 RVA: 0x001D5D04 File Offset: 0x001D3F04
			public ColumnShape(TypeValue type)
				: this()
			{
				this.type = type;
				this.types = new PageReaderConformanceEnforcingTableValue.TypeSet(type);
				if (type.IsRecordType && !type.AsRecordType.Open)
				{
					this.recordField = new PageReaderConformanceEnforcingTableValue.ColumnShape(type.AsRecordType);
					return;
				}
				if (type.IsListType)
				{
					this.listItem = new PageReaderConformanceEnforcingTableValue.ColumnShape(type.AsListType.ItemType);
					return;
				}
				if (type.IsTableType)
				{
					this.tableColumn = new PageReaderConformanceEnforcingTableValue.ColumnShape(type.AsTableType.ItemType);
				}
			}

			// Token: 0x06008BAE RID: 35758 RVA: 0x001D5D8E File Offset: 0x001D3F8E
			private ColumnShape()
			{
				this.metadata = PageReaderConformanceEnforcingTableValue.ColumnShape.AnyMetadata;
				this.hasTypeMetadata = true;
				this.recordField = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
				this.listItem = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
				this.tableColumn = PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
			}

			// Token: 0x06008BAF RID: 35759 RVA: 0x001D5DCC File Offset: 0x001D3FCC
			private ColumnShape(RecordTypeValue recordType)
				: this()
			{
				this.types = new PageReaderConformanceEnforcingTableValue.TypeSet(TypeValue.None);
				for (int i = 0; i < recordType.Fields.Count; i++)
				{
					bool flag;
					TypeValue fieldType = recordType.GetFieldType(i, out flag);
					if (fieldType.TypeKind == ValueKind.Any)
					{
						this.types = new PageReaderConformanceEnforcingTableValue.TypeSet(TypeValue.Any);
						return;
					}
					this.types.AddType(fieldType);
				}
			}

			// Token: 0x06008BB0 RID: 35760 RVA: 0x001D5E35 File Offset: 0x001D4035
			private ColumnShape(PageReaderConformanceEnforcingTableValue.ColumnShape shape, KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] metadata, bool hasTypeMetadata)
				: this(shape.type, shape.types, hasTypeMetadata, metadata, shape.recordField, shape.listItem, shape.tableColumn)
			{
			}

			// Token: 0x06008BB1 RID: 35761 RVA: 0x001D5E5D File Offset: 0x001D405D
			private ColumnShape(TypeValue type, PageReaderConformanceEnforcingTableValue.TypeSet types, bool hasTypeMetadata, KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] metadata, PageReaderConformanceEnforcingTableValue.ColumnShape recordField, PageReaderConformanceEnforcingTableValue.ColumnShape listItem, PageReaderConformanceEnforcingTableValue.ColumnShape tableColumn)
			{
				this.type = type;
				this.types = types;
				this.hasTypeMetadata = hasTypeMetadata;
				this.metadata = metadata;
				this.recordField = recordField;
				this.listItem = listItem;
				this.tableColumn = tableColumn;
			}

			// Token: 0x170024BA RID: 9402
			// (get) Token: 0x06008BB2 RID: 35762 RVA: 0x001D5E9C File Offset: 0x001D409C
			public int MaxDepth
			{
				get
				{
					if (this == PageReaderConformanceEnforcingTableValue.ColumnShape.Any)
					{
						return 100;
					}
					int num = (this.types.HasType(ValueKind.Record) ? this.recordField.MaxDepth : (-1));
					int num2 = (this.types.HasType(ValueKind.List) ? this.listItem.MaxDepth : (-1));
					int num3 = (this.types.HasType(ValueKind.Table) ? this.tableColumn.MaxDepth : (-1));
					return Math.Max(Math.Max(num, num2), num3) + 1;
				}
			}

			// Token: 0x170024BB RID: 9403
			// (get) Token: 0x06008BB3 RID: 35763 RVA: 0x001D5F24 File Offset: 0x001D4124
			public bool HasTypeMetadata
			{
				get
				{
					return this.hasTypeMetadata;
				}
			}

			// Token: 0x170024BC RID: 9404
			// (get) Token: 0x06008BB4 RID: 35764 RVA: 0x001D5F2C File Offset: 0x001D412C
			public bool HasMetadata
			{
				get
				{
					return this.metadata.Length != 0;
				}
			}

			// Token: 0x170024BD RID: 9405
			// (get) Token: 0x06008BB5 RID: 35765 RVA: 0x001D5F38 File Offset: 0x001D4138
			public TypeValue BoundingType
			{
				get
				{
					return this.type ?? this.types.BoundingType;
				}
			}

			// Token: 0x06008BB6 RID: 35766 RVA: 0x001D5F60 File Offset: 0x001D4160
			public bool TryApplyPatterns(IExpression function, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				Value value;
				if (function.TryGetConstant(out value) && value.IsFunction)
				{
					function = PageReaderConformanceEnforcingTableValue.GetFunctionExpression(value.AsFunction);
				}
				foreach (PageReaderConformanceEnforcingTableValue.ShapePattern shapePattern in PageReaderConformanceEnforcingTableValue.ColumnShape.knownPatterns)
				{
					if (shapePattern.TryApply(function, this, out newShape))
					{
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BB7 RID: 35767 RVA: 0x001D5FBC File Offset: 0x001D41BC
			public bool MightHaveType(PageReaderConformanceEnforcingTableValue.TypeSet types, out ValueKind invalidKind)
			{
				invalidKind = this.types.FirstMatch(types);
				if (invalidKind != ValueKind.None)
				{
					return true;
				}
				if (this == PageReaderConformanceEnforcingTableValue.ColumnShape.Any)
				{
					invalidKind = types.FirstMatch(types);
					return true;
				}
				return (this.types.HasType(ValueKind.Record) && this.recordField.MightHaveType(types, out invalidKind)) || (this.types.HasType(ValueKind.List) && this.listItem.MightHaveType(types, out invalidKind)) || (this.types.HasType(ValueKind.Table) && this.tableColumn.MightHaveType(types, out invalidKind));
			}

			// Token: 0x06008BB8 RID: 35768 RVA: 0x001D6060 File Offset: 0x001D4260
			public bool MightHaveMetadataType(PageReaderConformanceEnforcingTableValue.TypeSet types, out ValueKind invalidKind)
			{
				foreach (KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> keyValuePair in this.metadata)
				{
					if (keyValuePair.Value.MightHaveType(types, out invalidKind))
					{
						return true;
					}
				}
				invalidKind = ValueKind.None;
				return false;
			}

			// Token: 0x06008BB9 RID: 35769 RVA: 0x001D60A4 File Offset: 0x001D42A4
			public PageReaderConformanceEnforcingTableValue.ColumnShape ReshapeForExcel()
			{
				PageReaderConformanceEnforcingTableValue.TypeSet typeSet = ((this.type == null || this.type.TypeKind == ValueKind.Any) ? PageReaderConformanceEnforcingTableValue.ColumnShape.excelTypes : this.types);
				bool flag = typeSet.HasType(ValueKind.Record);
				return new PageReaderConformanceEnforcingTableValue.ColumnShape(this.type, typeSet, false, flag ? PageReaderConformanceEnforcingTableValue.ColumnShape.excelDisplayName : EmptyArray<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>>.Instance, flag ? this.recordField.ReshapeForExcel() : this.recordField, this.listItem, this.tableColumn);
			}

			// Token: 0x06008BBA RID: 35770 RVA: 0x001D611D File Offset: 0x001D431D
			private PageReaderConformanceEnforcingTableValue.ColumnShape RemoveMetadata()
			{
				return new PageReaderConformanceEnforcingTableValue.ColumnShape(this, EmptyArray<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>>.Instance, this.hasTypeMetadata);
			}

			// Token: 0x06008BBB RID: 35771 RVA: 0x001D6130 File Offset: 0x001D4330
			private bool TryNarrowMetadata(IDictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				Value value;
				Value value2;
				int num;
				if (captures.TryGetConstant("fields", out value) && value.IsList && captures.TryGetConstant("missingField", out value2) && value2.IsNumber && value2.AsNumber.TryGetInt32(out num) && num == 2)
				{
					string[] array = new string[value.AsList.Count];
					int num2 = 0;
					foreach (IValueReference valueReference in value.AsList)
					{
						if (valueReference.Value.IsText)
						{
							array[num2++] = valueReference.Value.AsString;
						}
					}
					if (num2 == array.Length)
					{
						newShape = this.SelectColumnMetadata(array);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BBC RID: 35772 RVA: 0x001D6218 File Offset: 0x001D4418
			private PageReaderConformanceEnforcingTableValue.ColumnShape RemoveTypeMetadata()
			{
				return new PageReaderConformanceEnforcingTableValue.ColumnShape(this, this.metadata, false);
			}

			// Token: 0x06008BBD RID: 35773 RVA: 0x001D6228 File Offset: 0x001D4428
			private PageReaderConformanceEnforcingTableValue.ColumnShape SelectColumnMetadata(string[] keyList)
			{
				PageReaderConformanceEnforcingTableValue.ColumnShape columnShape = this.metadata.FirstOrDefault((KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> s) => s.Key == PageReaderConformanceEnforcingTableValue.ColumnShape.AnyKey).Value ?? PageReaderConformanceEnforcingTableValue.ColumnShape.Any;
				KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] array = new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[keyList.Length];
				int i;
				Func<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>, bool> <>9__1;
				int j;
				for (i = 0; i < array.Length; i = j + 1)
				{
					IEnumerable<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>> enumerable = this.metadata;
					Func<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> s) => s.Key != PageReaderConformanceEnforcingTableValue.ColumnShape.AnyKey && string.Equals(s.Key, keyList[i], StringComparison.Ordinal));
					}
					KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> keyValuePair = enumerable.FirstOrDefault(func);
					array[i] = new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>(keyList[i], keyValuePair.Value ?? columnShape);
					j = i;
				}
				return new PageReaderConformanceEnforcingTableValue.ColumnShape(this, array, this.hasTypeMetadata);
			}

			// Token: 0x06008BBE RID: 35774 RVA: 0x001D6318 File Offset: 0x001D4518
			private bool TryReplaceValuesOfType(IDictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				List<TypeValue> list = (from pair in captures
					where pair.Key.StartsWith("t", StringComparison.Ordinal)
					select PageReaderConformanceEnforcingTableValue.ColumnShape.GetTypeConstant(pair.Value)).ToList<TypeValue>();
				IExpression expression;
				Value value;
				if (captures.TryGetValue("substitute", out expression) && (expression.TryGetConstant(out value) || expression.Kind == ExpressionKind.Throw))
				{
					if (list.All((TypeValue t) => t != null))
					{
						Value[] array;
						if (expression.Kind != ExpressionKind.Throw)
						{
							(array = new Value[1])[0] = value;
						}
						else
						{
							array = EmptyArray<Value>.Instance;
						}
						IList<Value> list2 = array;
						newShape = this.ReplaceTypes(list, list2);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BBF RID: 35775 RVA: 0x001D63EC File Offset: 0x001D45EC
			private bool TryReplaceValuesOfDifferentTypes(IDictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				List<TypeValue> list = (from pair in captures
					where pair.Key.StartsWith("t", StringComparison.Ordinal)
					select PageReaderConformanceEnforcingTableValue.ColumnShape.GetTypeConstant(pair.Value)).ToList<TypeValue>();
				List<Value> list2 = (from pair in captures
					where pair.Key.StartsWith("s", StringComparison.Ordinal)
					select PageReaderConformanceEnforcingTableValue.ColumnShape.GetConstant(pair.Value)).ToList<Value>();
				if (list.All((TypeValue t) => t != null))
				{
					if (list2.All((Value t) => t != null))
					{
						newShape = this.ReplaceTypes(list, list2);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BC0 RID: 35776 RVA: 0x001D64F8 File Offset: 0x001D46F8
			private PageReaderConformanceEnforcingTableValue.ColumnShape ReplaceTypes(List<TypeValue> types, IList<Value> substitutes)
			{
				PageReaderConformanceEnforcingTableValue.TypeSet typeSet = this.types;
				foreach (TypeValue typeValue in types)
				{
					typeSet.RemoveType(typeValue);
				}
				KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] anyMetadata = this.metadata;
				bool flag = this.hasTypeMetadata;
				foreach (Value value in substitutes)
				{
					if (!value.MetaValue.IsEmpty)
					{
						anyMetadata = PageReaderConformanceEnforcingTableValue.ColumnShape.AnyMetadata;
					}
					if (!value.Type.MetaValue.IsEmpty)
					{
						flag = true;
					}
					typeSet.AddType(value.Type);
				}
				return new PageReaderConformanceEnforcingTableValue.ColumnShape(null, typeSet, flag, anyMetadata, this.recordField, this.listItem, this.tableColumn);
			}

			// Token: 0x06008BC1 RID: 35777 RVA: 0x001D65E8 File Offset: 0x001D47E8
			private bool TryApplyRecordField(TypeValue guardType, IExpression each, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				if (guardType != null && guardType.IsRecordType)
				{
					if (!this.types.HasType(ValueKind.Record))
					{
						newShape = this;
						return true;
					}
					if (this.recordField.TryApplyPatterns(each, out newShape))
					{
						newShape = new PageReaderConformanceEnforcingTableValue.ColumnShape(this.type, this.types, this.hasTypeMetadata, this.metadata, newShape, this.listItem, this.tableColumn);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BC2 RID: 35778 RVA: 0x001D665C File Offset: 0x001D485C
			private bool TryApplyListItem(TypeValue guardType, IExpression each, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				if (guardType != null && guardType.IsListType)
				{
					if (!this.types.HasType(ValueKind.List))
					{
						newShape = this;
						return true;
					}
					if (this.listItem.TryApplyPatterns(each, out newShape))
					{
						newShape = new PageReaderConformanceEnforcingTableValue.ColumnShape(this.type, this.types, this.hasTypeMetadata, this.metadata, this.recordField, newShape, this.tableColumn);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BC3 RID: 35779 RVA: 0x001D66D0 File Offset: 0x001D48D0
			private bool TryApplyTableColumn(TypeValue guardType, IExpression each, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				if (guardType != null && guardType.IsTableType)
				{
					if (!this.types.HasType(ValueKind.Table))
					{
						newShape = this;
						return true;
					}
					if (this.tableColumn.TryApplyPatterns(each, out newShape))
					{
						newShape = new PageReaderConformanceEnforcingTableValue.ColumnShape(this.type, this.types, this.hasTypeMetadata, this.metadata, this.recordField, this.listItem, newShape);
						return true;
					}
				}
				newShape = null;
				return false;
			}

			// Token: 0x06008BC4 RID: 35780 RVA: 0x001D6744 File Offset: 0x001D4944
			private bool TryApplyReplaceMetadata(IExpression each, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				if (this.metadata.Length == 0)
				{
					newShape = this;
					return true;
				}
				KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] array = null;
				for (int i = 0; i < this.metadata.Length; i++)
				{
					if (!this.metadata[i].Value.TryApplyPatterns(each, out newShape))
					{
						return false;
					}
					if (array == null)
					{
						array = this.metadata.ShallowCopy<KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>>();
					}
					array[i] = new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>(this.metadata[i].Key, newShape);
				}
				newShape = new PageReaderConformanceEnforcingTableValue.ColumnShape(this, array, this.hasTypeMetadata);
				return true;
			}

			// Token: 0x06008BC5 RID: 35781 RVA: 0x001D67D0 File Offset: 0x001D49D0
			private static TypeValue GetTypeConstant(IExpression expression)
			{
				Value constant = PageReaderConformanceEnforcingTableValue.ColumnShape.GetConstant(expression);
				if (constant != null && constant.IsType)
				{
					return constant.AsType;
				}
				return null;
			}

			// Token: 0x06008BC6 RID: 35782 RVA: 0x001D67F7 File Offset: 0x001D49F7
			private static Value GetConstant(IExpression expression)
			{
				if (expression.Kind != ExpressionKind.Constant)
				{
					return null;
				}
				return ((IConstantExpression)expression).Value;
			}

			// Token: 0x06008BC7 RID: 35783 RVA: 0x001D6810 File Offset: 0x001D4A10
			internal Value GetDescription()
			{
				if (this == PageReaderConformanceEnforcingTableValue.ColumnShape.Any)
				{
					return TextValue.New("Any");
				}
				Keys keys = Keys.New(new string[] { "Kinds", "Metadata", "TypeMetadata", "RecordField", "ListItem", "TableColumn" });
				Value[] array = new Value[6];
				array[0] = this.types.GetDescription();
				array[1] = ListValue.New(this.metadata.Select((KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> m) => PageReaderConformanceEnforcingTableValue.ColumnShape.GetMetadataDescription(m)));
				array[2] = LogicalValue.New(this.hasTypeMetadata);
				array[3] = ((!this.types.HasType(ValueKind.Record)) ? Value.Null : this.recordField.GetDescription());
				array[4] = ((!this.types.HasType(ValueKind.List)) ? Value.Null : this.listItem.GetDescription());
				array[5] = ((!this.types.HasType(ValueKind.Table)) ? Value.Null : this.tableColumn.GetDescription());
				return RecordValue.New(keys, array);
			}

			// Token: 0x06008BC8 RID: 35784 RVA: 0x001D693C File Offset: 0x001D4B3C
			private static IValueReference GetMetadataDescription(KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape> item)
			{
				return ListValue.New(new Value[]
				{
					TextValue.NewOrNull(item.Key),
					item.Value.GetDescription()
				});
			}

			// Token: 0x04004C7B RID: 19579
			public static readonly PageReaderConformanceEnforcingTableValue.ColumnShape Any = new PageReaderConformanceEnforcingTableValue.ColumnShape(TypeValue.Any);

			// Token: 0x04004C7C RID: 19580
			public static readonly string AnyKey = null;

			// Token: 0x04004C7D RID: 19581
			public static readonly KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] AnyMetadata = new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[]
			{
				new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>(PageReaderConformanceEnforcingTableValue.ColumnShape.AnyKey, PageReaderConformanceEnforcingTableValue.ColumnShape.Any)
			};

			// Token: 0x04004C7E RID: 19582
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern removeMetadata = new PageReaderConformanceEnforcingTableValue.ShapePattern(new string[] { "(__x) => Value.RemoveMetadata(__x)", "Value.RemoveMetadata" }, delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				newShape = shape.RemoveMetadata();
				return true;
			});

			// Token: 0x04004C7F RID: 19583
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern narrowMetadata = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => Value.ReplaceMetadata(__x, Record.SelectFields(Value.Metadata(__x), __fields, __missingField))", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryNarrowMetadata(captures, out newShape);
			});

			// Token: 0x04004C80 RID: 19584
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern removeTypeMetadata = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => Value.ReplaceType(__x, Value.RemoveMetadata(Value.Type(__x)))", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				newShape = shape.RemoveTypeMetadata();
				return true;
			});

			// Token: 0x04004C81 RID: 19585
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern removeValueAndTypeMetadata = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => Value.RemoveMetadata(Value.ReplaceType(__x, Value.RemoveMetadata(Value.Type(__x))))", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				newShape = shape.RemoveMetadata().RemoveTypeMetadata();
				return true;
			});

			// Token: 0x04004C82 RID: 19586
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern replaceValuesOfType = new PageReaderConformanceEnforcingTableValue.ShapePattern(new string[] { "(__x) => if Value.Is(__x, __t1) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) or Value.Is(__x, __t3) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) or Value.Is(__x, __t3) or Value.Is(__x, __t4) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) or Value.Is(__x, __t3) or Value.Is(__x, __t4) or Value.Is(__x, __t5) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) or Value.Is(__x, __t3) or Value.Is(__x, __t4) or Value.Is(__x, __t5) or Value.Is(__x, __t6) then __substitute else __x", "(__x) => if Value.Is(__x, __t1) or Value.Is(__x, __t2) or Value.Is(__x, __t3) or Value.Is(__x, __t4) or Value.Is(__x, __t5) or Value.Is(__x, __t6) or Value.Is(__x, __t7) then __substitute else __x" }, delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryReplaceValuesOfType(captures, out newShape);
			});

			// Token: 0x04004C83 RID: 19587
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern replaceValuesOfDifferentTypes = new PageReaderConformanceEnforcingTableValue.ShapePattern(new string[] { "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else __x", "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else if Value.Is(__x, __t3) then __substitute3 else __x", "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else if Value.Is(__x, __t3) then __substitute3 else if Value.Is(__x, __t4) then __substitute4 else __x", "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else if Value.Is(__x, __t3) then __substitute3 else if Value.Is(__x, __t4) then __substitute4 else if Value.Is(__x, __t5) then __substitute5 else __x", "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else if Value.Is(__x, __t3) then __substitute3 else if Value.Is(__x, __t4) then __substitute4 else if Value.Is(__x, __t5) then __substitute5 else if Value.Is(__x, __t6) then __substitute6 else __x", "(__x) => if Value.Is(__x, __t1) then __substitute1 else if Value.Is(__x, __t2) then __substitute2 else if Value.Is(__x, __t3) then __substitute3 else if Value.Is(__x, __t4) then __substitute4 else if Value.Is(__x, __t5) then __substitute5 else if Value.Is(__x, __t6) then __substitute6 else if Value.Is(__x, __t7) then __substitute7 else __x" }, delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryReplaceValuesOfDifferentTypes(captures, out newShape);
			});

			// Token: 0x04004C84 RID: 19588
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern transformRecordFields = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => if Value.Is(__x, __t) then Record.TransformFields(__x, List.Transform(Record.FieldNames(__x), (f) => {f, __each})) meta Value.Metadata(__x) else __x", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryApplyRecordField(PageReaderConformanceEnforcingTableValue.ColumnShape.GetTypeConstant(captures["t"]), captures["each"], out newShape);
			});

			// Token: 0x04004C85 RID: 19589
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern transformListItems = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => if Value.Is(__x, __t) then List.Transform(__x, __each) else __x", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryApplyListItem(PageReaderConformanceEnforcingTableValue.ColumnShape.GetTypeConstant(captures["t"]), captures["each"], out newShape);
			});

			// Token: 0x04004C86 RID: 19590
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern transformTableColumns = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => if Value.Is(__x, __t) then Table.TransformColumns(__x, {}, __each) else __x", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryApplyTableColumn(PageReaderConformanceEnforcingTableValue.ColumnShape.GetTypeConstant(captures["t"]), captures["each"], out newShape);
			});

			// Token: 0x04004C87 RID: 19591
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern replaceMetadataFields = new PageReaderConformanceEnforcingTableValue.ShapePattern("(__x) => Value.ReplaceMetadata(__x, Record.TransformFields(Value.Metadata(__x), List.Transform(Record.FieldNames(Value.Metadata(__x)), (__y) => { __y, __each})))", delegate(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> captures, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				return shape.TryApplyReplaceMetadata(captures["each"], out newShape);
			});

			// Token: 0x04004C88 RID: 19592
			private static readonly PageReaderConformanceEnforcingTableValue.ShapePattern[] knownPatterns = new PageReaderConformanceEnforcingTableValue.ShapePattern[]
			{
				PageReaderConformanceEnforcingTableValue.ColumnShape.removeMetadata,
				PageReaderConformanceEnforcingTableValue.ColumnShape.narrowMetadata,
				PageReaderConformanceEnforcingTableValue.ColumnShape.removeTypeMetadata,
				PageReaderConformanceEnforcingTableValue.ColumnShape.removeValueAndTypeMetadata,
				PageReaderConformanceEnforcingTableValue.ColumnShape.replaceValuesOfType,
				PageReaderConformanceEnforcingTableValue.ColumnShape.replaceValuesOfDifferentTypes,
				PageReaderConformanceEnforcingTableValue.ColumnShape.transformRecordFields,
				PageReaderConformanceEnforcingTableValue.ColumnShape.transformListItems,
				PageReaderConformanceEnforcingTableValue.ColumnShape.transformTableColumns,
				PageReaderConformanceEnforcingTableValue.ColumnShape.replaceMetadataFields
			};

			// Token: 0x04004C89 RID: 19593
			private readonly TypeValue type;

			// Token: 0x04004C8A RID: 19594
			private readonly PageReaderConformanceEnforcingTableValue.TypeSet types;

			// Token: 0x04004C8B RID: 19595
			private readonly bool hasTypeMetadata;

			// Token: 0x04004C8C RID: 19596
			private KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] metadata;

			// Token: 0x04004C8D RID: 19597
			private PageReaderConformanceEnforcingTableValue.ColumnShape recordField;

			// Token: 0x04004C8E RID: 19598
			private PageReaderConformanceEnforcingTableValue.ColumnShape listItem;

			// Token: 0x04004C8F RID: 19599
			private PageReaderConformanceEnforcingTableValue.ColumnShape tableColumn;

			// Token: 0x04004C90 RID: 19600
			private static readonly PageReaderConformanceEnforcingTableValue.TypeSet excelTypes = new PageReaderConformanceEnforcingTableValue.TypeSet(new ValueKind[]
			{
				ValueKind.Date,
				ValueKind.DateTime,
				ValueKind.DateTimeZone,
				ValueKind.Duration,
				ValueKind.Logical,
				ValueKind.Null,
				ValueKind.Number,
				ValueKind.Text,
				ValueKind.Time
			});

			// Token: 0x04004C91 RID: 19601
			private static readonly KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[] excelDisplayName = new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>[]
			{
				new KeyValuePair<string, PageReaderConformanceEnforcingTableValue.ColumnShape>("Documentation.DisplayName", new PageReaderConformanceEnforcingTableValue.ColumnShape(NullableTypeValue.Text).ReshapeForExcel())
			};
		}

		// Token: 0x020015C8 RID: 5576
		// (Invoke) Token: 0x06008BE3 RID: 35811
		private delegate bool PatternFunc(PageReaderConformanceEnforcingTableValue.ColumnShape shape, Dictionary<string, IExpression> matches, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape);

		// Token: 0x020015C9 RID: 5577
		private struct ShapePattern
		{
			// Token: 0x06008BE6 RID: 35814 RVA: 0x001D6AC7 File Offset: 0x001D4CC7
			public ShapePattern(string pattern, PageReaderConformanceEnforcingTableValue.PatternFunc apply)
			{
				this.pattern = new ExpressionPattern(new string[] { pattern });
				this.apply = apply;
			}

			// Token: 0x06008BE7 RID: 35815 RVA: 0x001D6AE5 File Offset: 0x001D4CE5
			public ShapePattern(string[] patterns, PageReaderConformanceEnforcingTableValue.PatternFunc apply)
			{
				this.pattern = new ExpressionPattern(patterns);
				this.apply = apply;
			}

			// Token: 0x06008BE8 RID: 35816 RVA: 0x001D6AFC File Offset: 0x001D4CFC
			public bool TryApply(IExpression expression, PageReaderConformanceEnforcingTableValue.ColumnShape shape, out PageReaderConformanceEnforcingTableValue.ColumnShape newShape)
			{
				newShape = null;
				Dictionary<string, IExpression> dictionary;
				return this.pattern.TryMatch(expression, out dictionary) && this.apply(shape, dictionary, out newShape);
			}

			// Token: 0x04004CA1 RID: 19617
			private readonly ExpressionPattern pattern;

			// Token: 0x04004CA2 RID: 19618
			private readonly PageReaderConformanceEnforcingTableValue.PatternFunc apply;
		}

		// Token: 0x020015CA RID: 5578
		private struct TypeSet
		{
			// Token: 0x06008BE9 RID: 35817 RVA: 0x001D6B2C File Offset: 0x001D4D2C
			public TypeSet(TypeValue type)
			{
				if (type.TypeKind == ValueKind.Any)
				{
					this.typeKinds = PageReaderConformanceEnforcingTableValue.TypeSet.All.typeKinds;
					return;
				}
				if (type.TypeKind == ValueKind.None)
				{
					this.typeKinds = new BitArray(16);
					return;
				}
				int typeKind = (int)type.TypeKind;
				this.typeKinds = new BitArray(16);
				this.typeKinds[typeKind] = true;
				if (type.IsNullable)
				{
					this.typeKinds[0] = true;
				}
			}

			// Token: 0x06008BEA RID: 35818 RVA: 0x001D6BA4 File Offset: 0x001D4DA4
			public TypeSet(params ValueKind[] types)
			{
				this.typeKinds = new BitArray(16);
				foreach (int num in types)
				{
					this.typeKinds[num] = true;
				}
			}

			// Token: 0x06008BEB RID: 35819 RVA: 0x001D6BE0 File Offset: 0x001D4DE0
			private TypeSet(bool all)
			{
				this.typeKinds = new BitArray(16);
				for (int i = 0; i < 16; i++)
				{
					this.typeKinds[i] = all;
				}
			}

			// Token: 0x170024BE RID: 9406
			// (get) Token: 0x06008BEC RID: 35820 RVA: 0x001D6C14 File Offset: 0x001D4E14
			public TypeValue BoundingType
			{
				get
				{
					bool flag = this.HasType(ValueKind.Null);
					TypeValue typeValue = TypeValue.None;
					for (int i = 1; i < 16; i++)
					{
						if (this.typeKinds[i])
						{
							if (typeValue.TypeKind != ValueKind.None)
							{
								typeValue = TypeValue.Any.NonNullable;
								break;
							}
							typeValue = PageReaderConformanceEnforcingTableValue.TypeSet.typeMap[i];
						}
					}
					if (!flag)
					{
						return typeValue;
					}
					return typeValue.Nullable;
				}
			}

			// Token: 0x06008BED RID: 35821 RVA: 0x001D6C78 File Offset: 0x001D4E78
			public void AddType(TypeValue type)
			{
				int typeKind = (int)type.TypeKind;
				this.typeKinds[typeKind] = true;
				if (type.IsNullable)
				{
					this.typeKinds[0] = true;
				}
			}

			// Token: 0x06008BEE RID: 35822 RVA: 0x001D6CB0 File Offset: 0x001D4EB0
			public void RemoveType(TypeValue type)
			{
				int typeKind = (int)type.TypeKind;
				this.typeKinds[typeKind] = false;
			}

			// Token: 0x06008BEF RID: 35823 RVA: 0x001D6CD4 File Offset: 0x001D4ED4
			public bool HasType(ValueKind kind)
			{
				return this.typeKinds[(int)kind];
			}

			// Token: 0x06008BF0 RID: 35824 RVA: 0x001D6CF0 File Offset: 0x001D4EF0
			public ValueKind FirstMatch(PageReaderConformanceEnforcingTableValue.TypeSet types)
			{
				if (this.typeKinds.And(types.typeKinds).Empty)
				{
					return ValueKind.None;
				}
				for (int i = 0; i < 16; i++)
				{
					if (this.typeKinds[i])
					{
						return (ValueKind)i;
					}
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06008BF1 RID: 35825 RVA: 0x001D6D40 File Offset: 0x001D4F40
			internal Value GetDescription()
			{
				int num = 0;
				for (int i = 0; i < 16; i++)
				{
					if (this.typeKinds[i])
					{
						num |= 1 << i;
					}
				}
				return NumberValue.New(num);
			}

			// Token: 0x04004CA3 RID: 19619
			private const int size = 16;

			// Token: 0x04004CA4 RID: 19620
			public static readonly PageReaderConformanceEnforcingTableValue.TypeSet All = new PageReaderConformanceEnforcingTableValue.TypeSet(true);

			// Token: 0x04004CA5 RID: 19621
			private static readonly TypeValue[] typeMap = new TypeValue[]
			{
				TypeValue.Null,
				TypeValue.Time,
				TypeValue.Date,
				TypeValue.DateTime,
				TypeValue.DateTimeZone,
				TypeValue.Duration,
				TypeValue.Number,
				TypeValue.Logical,
				TypeValue.Text,
				TypeValue.Binary,
				TypeValue.List,
				TypeValue.Record,
				TypeValue.Table,
				TypeValue.Function,
				TypeValue._Type,
				TypeValue.Action
			};

			// Token: 0x04004CA6 RID: 19622
			private BitArray typeKinds;
		}

		// Token: 0x020015CB RID: 5579
		private class ConformingDataMapper : TableDataMapper
		{
			// Token: 0x06008BF3 RID: 35827 RVA: 0x001D6E27 File Offset: 0x001D5027
			public ConformingDataMapper(bool[] hasMetadata)
			{
				this.hasMetadata = hasMetadata;
				this.underlyingTypeColumns = new Dictionary<SchemaColumn, SchemaColumn>();
			}

			// Token: 0x06008BF4 RID: 35828 RVA: 0x001D6E44 File Offset: 0x001D5044
			public override SchemaColumn MapType(int position, string columnName, TypeValue fieldType)
			{
				bool flag = this.nestedHasMetadata ?? this.hasMetadata[position];
				SchemaColumn schemaColumn;
				if (fieldType.IsRecordType)
				{
					bool? flag2 = this.nestedHasMetadata;
					this.nestedHasMetadata = new bool?(flag);
					schemaColumn = new SchemaColumn(columnName);
					schemaColumn.DataType = typeof(IDataRecord);
					schemaColumn.Nullable = fieldType.IsNullable;
					schemaColumn.ProviderType = new int?(13);
					schemaColumn.DataTypeName = "record";
					schemaColumn.ColumnSize = new long?(-1L);
					RecordTypeValue asRecordType = fieldType.AsRecordType;
					if (!asRecordType.Open && !PageReaderConformanceEnforcingTableValue.ConformingDataMapper.AnyAreOptional(asRecordType.Fields))
					{
						schemaColumn.ColumnSchema = base.MapRecordType(asRecordType, asRecordType.MetaValue);
					}
					this.nestedHasMetadata = flag2;
				}
				else
				{
					schemaColumn = base.MapType(position, columnName, fieldType);
				}
				if (flag)
				{
					SchemaColumn schemaColumn2 = schemaColumn.Clone(null);
					schemaColumn.DataType = ValueWithMetadata.AddMetadata(schemaColumn.DataType, true);
					this.underlyingTypeColumns[schemaColumn] = schemaColumn2;
				}
				return schemaColumn;
			}

			// Token: 0x06008BF5 RID: 35829 RVA: 0x001D6F50 File Offset: 0x001D5150
			public override object ConvertValue(Value value, SchemaColumn column)
			{
				return this.ConvertValue(value, column, this.hasMetadata[column.Ordinal.Value]);
			}

			// Token: 0x06008BF6 RID: 35830 RVA: 0x001D6F7C File Offset: 0x001D517C
			private object ConvertValue(Value value, SchemaColumn column, bool hasMetadata)
			{
				object obj;
				switch (value.Kind)
				{
				case ValueKind.List:
				case ValueKind.Table:
				case ValueKind.Function:
				case ValueKind.Type:
				case ValueKind.Action:
					throw ValueException.NewDataFormatError<Message1>(Strings.ValueException_CastTypeMismatch_OleDb(TypeValue.GetTypeName(value.Type)), value, null);
				case ValueKind.Record:
					obj = ((column.ColumnSchema != null) ? this.TypedRecord(column.ColumnSchema, value.AsRecord, hasMetadata) : this.UntypedRecord(value.AsRecord, hasMetadata));
					break;
				default:
				{
					SchemaColumn schemaColumn;
					if (!hasMetadata || !this.underlyingTypeColumns.TryGetValue(column, out schemaColumn))
					{
						schemaColumn = column;
					}
					obj = base.ConvertValue(value, schemaColumn);
					break;
				}
				}
				if (hasMetadata)
				{
					obj = new ValueWithMetadata
					{
						Value = obj,
						Metadata = this.UntypedRecord(value.MetaValue, false)
					};
				}
				return obj;
			}

			// Token: 0x06008BF7 RID: 35831 RVA: 0x001D703C File Offset: 0x001D523C
			private DataRecord TypedRecord(TableSchema schema, RecordValue record, bool hasMetadata)
			{
				object[] array = new object[schema.ColumnCount];
				for (int i = 0; i < array.Length; i++)
				{
					SchemaColumn column = schema.GetColumn(i);
					Value @null;
					if (!record.TryGetValue(column.Name, out @null))
					{
						@null = Value.Null;
					}
					array[i] = this.ConvertValue(@null, column, hasMetadata);
				}
				return new DataRecord(schema, array);
			}

			// Token: 0x06008BF8 RID: 35832 RVA: 0x001D7094 File Offset: 0x001D5294
			private DataRecord UntypedRecord(RecordValue record, bool hasMetadata)
			{
				int count = record.Count;
				string[] array = new string[count];
				object[] array2 = new object[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = record.Keys[i];
					array2[i] = this.ConvertValue(record[i], PageReaderConformanceEnforcingTableValue.ConformingDataMapper.objectType, hasMetadata);
				}
				return new DataRecord(array, array2);
			}

			// Token: 0x06008BF9 RID: 35833 RVA: 0x001D70F0 File Offset: 0x001D52F0
			private static bool AnyAreOptional(RecordValue fields)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					if (fields[i].AsRecord["Optional"].AsBoolean)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04004CA7 RID: 19623
			private static readonly SchemaColumn objectType = new SchemaColumn(string.Empty)
			{
				DataType = typeof(object)
			};

			// Token: 0x04004CA8 RID: 19624
			private readonly bool[] hasMetadata;

			// Token: 0x04004CA9 RID: 19625
			private readonly Dictionary<SchemaColumn, SchemaColumn> underlyingTypeColumns;

			// Token: 0x04004CAA RID: 19626
			private bool? nestedHasMetadata;
		}
	}
}
