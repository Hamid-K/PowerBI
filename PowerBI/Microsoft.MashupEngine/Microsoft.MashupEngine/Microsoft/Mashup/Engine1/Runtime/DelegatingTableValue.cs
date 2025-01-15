using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001642 RID: 5698
	public abstract class DelegatingTableValue : TableValue
	{
		// Token: 0x06008F67 RID: 36711 RVA: 0x001DDA57 File Offset: 0x001DBC57
		protected DelegatingTableValue(TableValue table)
		{
			this.table = table;
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x06008F68 RID: 36712 RVA: 0x001DDA66 File Offset: 0x001DBC66
		public TableValue Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x06008F69 RID: 36713 RVA: 0x001DDA6E File Offset: 0x001DBC6E
		public override TypeValue Type
		{
			get
			{
				return this.table.Type;
			}
		}

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x06008F6A RID: 36714 RVA: 0x001DDA7B File Offset: 0x001DBC7B
		public override Keys Columns
		{
			get
			{
				return this.table.Columns;
			}
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x001DDA88 File Offset: 0x001DBC88
		public override TypeValue GetColumnType(int index)
		{
			return this.table.GetColumnType(index);
		}

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x06008F6C RID: 36716 RVA: 0x001DDA96 File Offset: 0x001DBC96
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.table.TableKeys;
			}
		}

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x06008F6D RID: 36717 RVA: 0x001DDAA3 File Offset: 0x001DBCA3
		public override RecordValue MetaValue
		{
			get
			{
				return this.table.MetaValue;
			}
		}

		// Token: 0x06008F6E RID: 36718 RVA: 0x001DDAB0 File Offset: 0x001DBCB0
		public override bool TryGetAs<T>(out T contract)
		{
			return this.table.TryGetAs<T>(out contract);
		}

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x06008F6F RID: 36719 RVA: 0x001DDABE File Offset: 0x001DBCBE
		public override bool IsCube
		{
			get
			{
				return this.table.IsCube;
			}
		}

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x06008F70 RID: 36720 RVA: 0x001DDACB File Offset: 0x001DBCCB
		public override CubeValue AsCube
		{
			get
			{
				return this.table.AsCube;
			}
		}

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x06008F71 RID: 36721 RVA: 0x001DDAD8 File Offset: 0x001DBCD8
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.table.SortOrder;
			}
		}

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06008F72 RID: 36722 RVA: 0x001DDAE5 File Offset: 0x001DBCE5
		public override IList<RelatedTable> RelatedTables
		{
			get
			{
				return this.table.RelatedTables;
			}
		}

		// Token: 0x06008F73 RID: 36723 RVA: 0x001DDAF2 File Offset: 0x001DBCF2
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return this.table.ReplaceRelatedTables(relatedTables);
		}

		// Token: 0x06008F74 RID: 36724 RVA: 0x001DDB00 File Offset: 0x001DBD00
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return this.table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships);
		}

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x06008F75 RID: 36725 RVA: 0x001DDB10 File Offset: 0x001DBD10
		public override ColumnIdentity[] ColumnIdentities
		{
			get
			{
				return this.table.ColumnIdentities;
			}
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x001DDB1D File Offset: 0x001DBD1D
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			return this.table.ReplaceRelationshipIdentity(identity);
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x001DDB2B File Offset: 0x001DBD2B
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return this.table.ReplaceColumnIdentities(columnIdentities);
		}

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x06008F78 RID: 36728 RVA: 0x001DDB39 File Offset: 0x001DBD39
		public override IList<Relationship> Relationships
		{
			get
			{
				return this.table.Relationships;
			}
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x001DDB46 File Offset: 0x001DBD46
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return this.table.ReplaceRelationships(relationships);
		}

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x06008F7A RID: 36730 RVA: 0x001DDB54 File Offset: 0x001DBD54
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.table.ComputedColumns;
			}
		}

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x06008F7B RID: 36731 RVA: 0x001DDB61 File Offset: 0x001DBD61
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.table.QueryDomain;
			}
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06008F7C RID: 36732 RVA: 0x001DDB6E File Offset: 0x001DBD6E
		public override IExpression Expression
		{
			get
			{
				return this.table.Expression;
			}
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x001DDB7B File Offset: 0x001DBD7B
		public override TableValue Optimize()
		{
			return this.table.Optimize();
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x001DDB88 File Offset: 0x001DBD88
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x001DDB95 File Offset: 0x001DBD95
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.table.TryGetProcessor(out processor);
		}

		// Token: 0x06008F80 RID: 36736 RVA: 0x001DDBA3 File Offset: 0x001DBDA3
		public override IPageReader GetReader()
		{
			return this.table.GetReader();
		}

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06008F81 RID: 36737 RVA: 0x001DDBB0 File Offset: 0x001DBDB0
		public override Query Query
		{
			get
			{
				return this.table.Query;
			}
		}

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x06008F82 RID: 36738 RVA: 0x001DDBBD File Offset: 0x001DBDBD
		public override long LargeCount
		{
			get
			{
				return this.table.LargeCount;
			}
		}

		// Token: 0x06008F83 RID: 36739 RVA: 0x001DDBCA File Offset: 0x001DBDCA
		public override bool TryGetValue(Value index, out Value value)
		{
			return this.table.TryGetValue(index, out value);
		}

		// Token: 0x06008F84 RID: 36740 RVA: 0x001DDBD9 File Offset: 0x001DBDD9
		public override void TestConnection()
		{
			this.table.TestConnection();
		}

		// Token: 0x06008F85 RID: 36741 RVA: 0x001DDBE6 File Offset: 0x001DBDE6
		public override TableValue Buffer(Library.BufferMode bufferMode)
		{
			return this.table.Buffer(bufferMode);
		}

		// Token: 0x06008F86 RID: 36742 RVA: 0x001DDBF4 File Offset: 0x001DBDF4
		public override TableValue SelectColumns(ColumnSelection columnSelection)
		{
			return this.table.SelectColumns(columnSelection);
		}

		// Token: 0x06008F87 RID: 36743 RVA: 0x001DDC02 File Offset: 0x001DBE02
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			return this.table.TrySelectColumns(columnSelection, out table);
		}

		// Token: 0x06008F88 RID: 36744 RVA: 0x001DDC11 File Offset: 0x001DBE11
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.table.SelectRows(condition);
		}

		// Token: 0x06008F89 RID: 36745 RVA: 0x001DDC1F File Offset: 0x001DBE1F
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.table.AddColumns(columnGenerator);
		}

		// Token: 0x06008F8A RID: 36746 RVA: 0x001DDC2D File Offset: 0x001DBE2D
		public override TableValue TransformColumns(ColumnTransforms columnTransforms)
		{
			return this.table.TransformColumns(columnTransforms);
		}

		// Token: 0x06008F8B RID: 36747 RVA: 0x001DDC3B File Offset: 0x001DBE3B
		public override TableValue Group(Grouping grouping)
		{
			return this.table.Group(grouping);
		}

		// Token: 0x06008F8C RID: 36748 RVA: 0x001DDC49 File Offset: 0x001DBE49
		public override TableValue Skip(RowCount count)
		{
			return this.table.Skip(count);
		}

		// Token: 0x06008F8D RID: 36749 RVA: 0x001DDC57 File Offset: 0x001DBE57
		public override TableValue Take(RowCount count)
		{
			return this.table.Take(count);
		}

		// Token: 0x06008F8E RID: 36750 RVA: 0x001DDC65 File Offset: 0x001DBE65
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.table.Sort(sortOrder);
		}

		// Token: 0x06008F8F RID: 36751 RVA: 0x001DDC73 File Offset: 0x001DBE73
		public override TableValue Unordered()
		{
			return this.table.Unordered();
		}

		// Token: 0x06008F90 RID: 36752 RVA: 0x001DDC80 File Offset: 0x001DBE80
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.table.Distinct(distinctCriteria);
		}

		// Token: 0x06008F91 RID: 36753 RVA: 0x001DDC8E File Offset: 0x001DBE8E
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.table.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06008F92 RID: 36754 RVA: 0x001DDCA6 File Offset: 0x001DBEA6
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.table.ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x06008F93 RID: 36755 RVA: 0x001DDCB5 File Offset: 0x001DBEB5
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.table.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x06008F94 RID: 36756 RVA: 0x001DDCC5 File Offset: 0x001DBEC5
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.table.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x001DDCD7 File Offset: 0x001DBED7
		public override TableValue DeltaSince(Value tag)
		{
			return this.table.DeltaSince(tag);
		}

		// Token: 0x06008F96 RID: 36758 RVA: 0x001DDCE5 File Offset: 0x001DBEE5
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.table.NativeQuery(query, parameters, options);
		}

		// Token: 0x06008F97 RID: 36759 RVA: 0x001DDCF5 File Offset: 0x001DBEF5
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.table.InsertRows(rowsToInsert);
		}

		// Token: 0x06008F98 RID: 36760 RVA: 0x001DDD03 File Offset: 0x001DBF03
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.table.UpdateRows(columnUpdates);
		}

		// Token: 0x06008F99 RID: 36761 RVA: 0x001DDD11 File Offset: 0x001DBF11
		public override ActionValue DeleteRows()
		{
			return this.table.DeleteRows();
		}

		// Token: 0x06008F9A RID: 36762 RVA: 0x001DDD1E File Offset: 0x001DBF1E
		public override ActionValue Replace(Value value)
		{
			return this.table.Replace(value);
		}

		// Token: 0x06008F9B RID: 36763 RVA: 0x001DDD2C File Offset: 0x001DBF2C
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.table.NativeStatement(statement, parameters, options);
		}

		// Token: 0x04004D92 RID: 19858
		private readonly TableValue table;
	}
}
