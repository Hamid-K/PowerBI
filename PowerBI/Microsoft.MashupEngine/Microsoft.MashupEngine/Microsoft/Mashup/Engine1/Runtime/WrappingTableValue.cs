using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001643 RID: 5699
	internal abstract class WrappingTableValue : DelegatingTableValue
	{
		// Token: 0x06008F9C RID: 36764 RVA: 0x001DDD3C File Offset: 0x001DBF3C
		protected WrappingTableValue(TableValue table)
			: base(table)
		{
		}

		// Token: 0x06008F9D RID: 36765
		protected abstract TableValue New(TableValue table);

		// Token: 0x06008F9E RID: 36766 RVA: 0x001DDD45 File Offset: 0x001DBF45
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables));
		}

		// Token: 0x06008F9F RID: 36767 RVA: 0x001DDD59 File Offset: 0x001DBF59
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships));
		}

		// Token: 0x06008FA0 RID: 36768 RVA: 0x001DDD6F File Offset: 0x001DBF6F
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			return this.New(base.Table.ReplaceRelationshipIdentity(identity));
		}

		// Token: 0x06008FA1 RID: 36769 RVA: 0x001DDD83 File Offset: 0x001DBF83
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return this.New(base.Table.ReplaceColumnIdentities(columnIdentities));
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x001DDD97 File Offset: 0x001DBF97
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelationships(relationships));
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x001DDDAB File Offset: 0x001DBFAB
		public override TableValue Buffer(Library.BufferMode bufferMode)
		{
			return this.New(base.Buffer(bufferMode));
		}

		// Token: 0x06008FA4 RID: 36772 RVA: 0x001DDDBA File Offset: 0x001DBFBA
		public override TableValue SelectColumns(ColumnSelection columnSelection)
		{
			return this.New(base.SelectColumns(columnSelection));
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x001DDDC9 File Offset: 0x001DBFC9
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			if (base.TrySelectColumns(columnSelection, out table))
			{
				table = this.New(table);
				return true;
			}
			return false;
		}

		// Token: 0x06008FA6 RID: 36774 RVA: 0x001DDDE2 File Offset: 0x001DBFE2
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.New(base.SelectRows(condition));
		}

		// Token: 0x06008FA7 RID: 36775 RVA: 0x001DDDF1 File Offset: 0x001DBFF1
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.New(base.AddColumns(columnGenerator));
		}

		// Token: 0x06008FA8 RID: 36776 RVA: 0x001DDE00 File Offset: 0x001DC000
		public override TableValue TransformColumns(ColumnTransforms columnTransforms)
		{
			return this.New(base.TransformColumns(columnTransforms));
		}

		// Token: 0x06008FA9 RID: 36777 RVA: 0x001DDE0F File Offset: 0x001DC00F
		public override TableValue Group(Grouping grouping)
		{
			return this.New(base.Group(grouping));
		}

		// Token: 0x06008FAA RID: 36778 RVA: 0x001DDE1E File Offset: 0x001DC01E
		public override TableValue Skip(RowCount count)
		{
			return this.New(base.Skip(count));
		}

		// Token: 0x06008FAB RID: 36779 RVA: 0x001DDE2D File Offset: 0x001DC02D
		public override TableValue Take(RowCount count)
		{
			return this.New(base.Take(count));
		}

		// Token: 0x06008FAC RID: 36780 RVA: 0x001DDE3C File Offset: 0x001DC03C
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.New(base.Sort(sortOrder));
		}

		// Token: 0x06008FAD RID: 36781 RVA: 0x001DDE4B File Offset: 0x001DC04B
		public override TableValue Unordered()
		{
			return this.New(base.Unordered());
		}

		// Token: 0x06008FAE RID: 36782 RVA: 0x001DDE59 File Offset: 0x001DC059
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.New(base.Distinct(distinctCriteria));
		}

		// Token: 0x06008FAF RID: 36783 RVA: 0x001DDE68 File Offset: 0x001DC068
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.New(base.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers));
		}

		// Token: 0x06008FB0 RID: 36784 RVA: 0x001DDE8C File Offset: 0x001DC08C
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.New(base.ExpandListColumn(columnIndex, singleOrDefault));
		}

		// Token: 0x06008FB1 RID: 36785 RVA: 0x001DDE9C File Offset: 0x001DC09C
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.New(base.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
		}
	}
}
