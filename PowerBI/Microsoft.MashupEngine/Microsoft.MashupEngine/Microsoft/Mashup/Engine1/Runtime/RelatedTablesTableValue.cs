using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200164C RID: 5708
	internal class RelatedTablesTableValue : WrappingTableValue
	{
		// Token: 0x06008FFF RID: 36863 RVA: 0x001DE72B File Offset: 0x001DC92B
		public static TableValue New(TableValue table, IList<RelatedTable> relatedTables)
		{
			return RelatedTablesTableValue.New(table, relatedTables, null, Microsoft.Mashup.Engine1.Runtime.Relationships.None);
		}

		// Token: 0x06009000 RID: 36864 RVA: 0x001DE73C File Offset: 0x001DC93C
		public static TableValue New(TableValue table, string identity)
		{
			Func<ColumnIdentity[]> func = () => Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.New(table.Columns, table.ColumnIdentities, identity);
			return RelatedTablesTableValue.New(table, Microsoft.Mashup.Engine1.Runtime.RelatedTables.None, func, Microsoft.Mashup.Engine1.Runtime.Relationships.None);
		}

		// Token: 0x06009001 RID: 36865 RVA: 0x001DE77E File Offset: 0x001DC97E
		public static TableValue New(TableValue table, ColumnIdentity[] columnIdentities)
		{
			return RelatedTablesTableValue.New(table, Microsoft.Mashup.Engine1.Runtime.RelatedTables.None, columnIdentities, Microsoft.Mashup.Engine1.Runtime.Relationships.None);
		}

		// Token: 0x06009002 RID: 36866 RVA: 0x001DE791 File Offset: 0x001DC991
		public static TableValue New(TableValue table, IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(table, Microsoft.Mashup.Engine1.Runtime.RelatedTables.None, null, relationships);
		}

		// Token: 0x06009003 RID: 36867 RVA: 0x001DE7A0 File Offset: 0x001DC9A0
		public static TableValue New(TableValue table, IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(table, relatedTables, columnIdentities, relationships);
		}

		// Token: 0x06009004 RID: 36868 RVA: 0x001DE7AB File Offset: 0x001DC9AB
		private static TableValue New(TableValue table, IList<RelatedTable> relatedTables, object columnIdentities, IList<Relationship> relationships)
		{
			if (relatedTables.Count > 0 || columnIdentities != null || relationships.Count > 0)
			{
				return new RelatedTablesTableValue(table, relatedTables, columnIdentities, relationships);
			}
			return table;
		}

		// Token: 0x06009005 RID: 36869 RVA: 0x001DE7CD File Offset: 0x001DC9CD
		private RelatedTablesTableValue(TableValue table, IList<RelatedTable> relatedTables, object columnIdentities, IList<Relationship> relationships)
			: base(table)
		{
			this.relatedTables = relatedTables;
			this.columnIdentities = columnIdentities;
			this.relationships = relationships;
		}

		// Token: 0x06009006 RID: 36870 RVA: 0x001DE7EC File Offset: 0x001DC9EC
		protected override TableValue New(TableValue table)
		{
			return RelatedTablesTableValue.New(table, this.relatedTables, this.columnIdentities, this.relationships);
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x06009007 RID: 36871 RVA: 0x001DE806 File Offset: 0x001DCA06
		public override IList<RelatedTable> RelatedTables
		{
			get
			{
				return this.relatedTables;
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x06009008 RID: 36872 RVA: 0x001DE810 File Offset: 0x001DCA10
		public override ColumnIdentity[] ColumnIdentities
		{
			get
			{
				ColumnIdentity[] array = this.columnIdentities as ColumnIdentity[];
				if (array == null && this.columnIdentities != null)
				{
					array = ((Func<ColumnIdentity[]>)this.columnIdentities)();
					this.columnIdentities = array;
				}
				return array;
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x06009009 RID: 36873 RVA: 0x001DE84D File Offset: 0x001DCA4D
		public override IList<Relationship> Relationships
		{
			get
			{
				return this.relationships;
			}
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x001DE855 File Offset: 0x001DCA55
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return RelatedTablesTableValue.New(base.Table, relatedTables, this.columnIdentities, this.relationships);
		}

		// Token: 0x0600900B RID: 36875 RVA: 0x001DE86F File Offset: 0x001DCA6F
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(base.Table, relatedTables, columnIdentities, relationships);
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x001DE880 File Offset: 0x001DCA80
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			Func<ColumnIdentity[]> func = () => Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.New(this.Columns, this.ColumnIdentities, identity);
			return RelatedTablesTableValue.New(base.Table, this.relatedTables, func, this.relationships);
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x001DE8C4 File Offset: 0x001DCAC4
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return RelatedTablesTableValue.New(base.Table, this.relatedTables, columnIdentities, this.relationships);
		}

		// Token: 0x0600900E RID: 36878 RVA: 0x001DE8DE File Offset: 0x001DCADE
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return RelatedTablesTableValue.New(base.Table, this.relatedTables, this.columnIdentities, relationships);
		}

		// Token: 0x0600900F RID: 36879 RVA: 0x001DE8F8 File Offset: 0x001DCAF8
		public override TableValue SelectColumns(ColumnSelection columnSelection)
		{
			return base.Table.SelectColumns(columnSelection).ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.SelectColumns(this.relatedTables, this.Columns, columnSelection), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.SelectColumns(this.ColumnIdentities, columnSelection), Microsoft.Mashup.Engine1.Runtime.Relationships.SelectColumns(this.relationships, this.Columns, columnSelection));
		}

		// Token: 0x06009010 RID: 36880 RVA: 0x001DE948 File Offset: 0x001DCB48
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			if (base.Table.TrySelectColumns(columnSelection, out table))
			{
				table = table.ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.SelectColumns(this.relatedTables, this.Columns, columnSelection), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.SelectColumns(this.ColumnIdentities, columnSelection), Microsoft.Mashup.Engine1.Runtime.Relationships.SelectColumns(this.relationships, this.Columns, columnSelection));
				return true;
			}
			return false;
		}

		// Token: 0x06009011 RID: 36881 RVA: 0x001DE9A0 File Offset: 0x001DCBA0
		public override TableValue AddColumns(ColumnsConstructor columnsConstructor)
		{
			return base.Table.AddColumns(columnsConstructor).ReplaceRelatedTables(this.relatedTables, Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.AddColumns(this.ColumnIdentities, columnsConstructor.Length), this.relationships);
		}

		// Token: 0x06009012 RID: 36882 RVA: 0x001DE9D0 File Offset: 0x001DCBD0
		public override TableValue TransformColumns(ColumnTransforms columnTransforms)
		{
			return base.Table.TransformColumns(columnTransforms).ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.TransformColumns(this.relatedTables, columnTransforms), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.TransformColumns(this.ColumnIdentities, columnTransforms), Microsoft.Mashup.Engine1.Runtime.Relationships.TransformColumns(this.relationships, columnTransforms));
		}

		// Token: 0x06009013 RID: 36883 RVA: 0x001DEA07 File Offset: 0x001DCC07
		public override TableValue Group(Grouping grouping)
		{
			return base.Table.Group(grouping);
		}

		// Token: 0x06009014 RID: 36884 RVA: 0x001DEA18 File Offset: 0x001DCC18
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return base.Table.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers).ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.NestedJoin(this.relatedTables, this.Columns.Length, rightTable), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.AddColumns(this.ColumnIdentities, 1), Microsoft.Mashup.Engine1.Runtime.Relationships.NestedJoin(this.relationships, leftKeyColumns, rightTable, rightKey));
		}

		// Token: 0x06009015 RID: 36885 RVA: 0x001DEA71 File Offset: 0x001DCC71
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return base.Table.ExpandListColumn(columnIndex, singleOrDefault).ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.ExpandListColumn(this.relatedTables, columnIndex), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.ExpandListColumn(this.ColumnIdentities, columnIndex, singleOrDefault), Microsoft.Mashup.Engine1.Runtime.Relationships.ExpandListColumn(this.relationships, columnIndex, singleOrDefault));
		}

		// Token: 0x06009016 RID: 36886 RVA: 0x001DEAAC File Offset: 0x001DCCAC
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return base.Table.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns).ReplaceRelatedTables(Microsoft.Mashup.Engine1.Runtime.RelatedTables.ExpandRecordColumn(this.relatedTables, columnToExpand, fieldsToProject), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.ExpandRecordColumn(this.relatedTables, this.Columns.Length, this.ColumnIdentities, columnToExpand, fieldsToProject), Microsoft.Mashup.Engine1.Runtime.Relationships.ExpandRecordColumn(this.relatedTables, this.relationships, columnToExpand, fieldsToProject));
		}

		// Token: 0x04004D9C RID: 19868
		private readonly IList<RelatedTable> relatedTables;

		// Token: 0x04004D9D RID: 19869
		private object columnIdentities;

		// Token: 0x04004D9E RID: 19870
		private readonly IList<Relationship> relationships;
	}
}
