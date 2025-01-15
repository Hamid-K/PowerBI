using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BC RID: 4796
	internal abstract class WrappingCubeValue : DelegatingCubeValue
	{
		// Token: 0x06007E02 RID: 32258 RVA: 0x00138340 File Offset: 0x00136540
		protected WrappingCubeValue(CubeValue cube)
			: base(cube)
		{
		}

		// Token: 0x06007E03 RID: 32259
		protected abstract CubeValue New(CubeValue cube);

		// Token: 0x06007E04 RID: 32260 RVA: 0x001B01CF File Offset: 0x001AE3CF
		protected virtual TableValue New(TableValue table)
		{
			if (table.IsCube)
			{
				return this.New(table.AsCube);
			}
			return table;
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x001B01E7 File Offset: 0x001AE3E7
		public override Value NewMeta(RecordValue metaValue)
		{
			return this.New(base.Cube.NewMeta(metaValue).AsTable.AsCube);
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x001B0205 File Offset: 0x001AE405
		public override CubeValue ExpandDimensionAttributes(TableValue dimensionTable)
		{
			return this.New(base.Cube.ExpandDimensionAttributes(dimensionTable));
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x001B0219 File Offset: 0x001AE419
		public override CubeValue CollapseDimensionAttributes(int[] columns)
		{
			return this.New(base.Cube.CollapseDimensionAttributes(columns));
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x001B022D File Offset: 0x001AE42D
		public override CubeValue AddMeasureColumn(string newColumnName, FunctionValue measure)
		{
			return this.New(base.Cube.AddMeasureColumn(newColumnName, measure));
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x001B0242 File Offset: 0x001AE442
		public override CubeValue ApplyParameter(FunctionValue parameter, Value[] args)
		{
			return this.New(base.Cube.ApplyParameter(parameter, args));
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x001B0257 File Offset: 0x001AE457
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue tableValue)
		{
			if (base.Cube.TrySelectColumns(columnSelection, out tableValue))
			{
				tableValue = this.New(tableValue);
				return true;
			}
			return false;
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x001B0275 File Offset: 0x001AE475
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.New(base.Cube.SelectRows(condition));
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x001B0289 File Offset: 0x001AE489
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.New(base.Cube.AddColumns(columnGenerator));
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x001B029D File Offset: 0x001AE49D
		public override TableValue Group(Grouping grouping)
		{
			return this.New(base.Cube.Group(grouping));
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x001B02B1 File Offset: 0x001AE4B1
		public override TableValue Skip(RowCount count)
		{
			return this.New(base.Cube.Skip(count));
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x001B02C5 File Offset: 0x001AE4C5
		public override TableValue Take(RowCount count)
		{
			return this.New(base.Cube.Take(count));
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x001B02D9 File Offset: 0x001AE4D9
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.New(base.Cube.Sort(sortOrder));
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x001B02ED File Offset: 0x001AE4ED
		public override TableValue Unordered()
		{
			return this.New(base.Cube.Unordered());
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x001B0300 File Offset: 0x001AE500
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.New(base.Cube.Distinct(distinctCriteria));
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x001B0314 File Offset: 0x001AE514
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.New(base.Cube.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers));
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x001B033D File Offset: 0x001AE53D
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.New(base.Cube.ExpandListColumn(columnIndex, singleOrDefault));
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x001B0352 File Offset: 0x001AE552
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.New(base.Cube.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
		}
	}
}
