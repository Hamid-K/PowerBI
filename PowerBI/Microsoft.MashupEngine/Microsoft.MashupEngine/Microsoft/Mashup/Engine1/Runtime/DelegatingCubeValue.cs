using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BB RID: 4795
	internal abstract class DelegatingCubeValue : CubeValue
	{
		// Token: 0x06007DD8 RID: 32216 RVA: 0x001AFF87 File Offset: 0x001AE187
		protected DelegatingCubeValue(CubeValue cube)
		{
			this.cube = cube;
		}

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x001AFF96 File Offset: 0x001AE196
		public CubeValue Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x06007DDA RID: 32218 RVA: 0x001AFF9E File Offset: 0x001AE19E
		public override TypeValue Type
		{
			get
			{
				return this.cube.Type;
			}
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x06007DDB RID: 32219 RVA: 0x001AFFAB File Offset: 0x001AE1AB
		public override Keys Columns
		{
			get
			{
				return this.cube.Columns;
			}
		}

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x06007DDC RID: 32220 RVA: 0x001AFFB8 File Offset: 0x001AE1B8
		public override IExpression Expression
		{
			get
			{
				return this.cube.Expression;
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x001AFFC5 File Offset: 0x001AE1C5
		public override RecordValue MetaValue
		{
			get
			{
				return this.cube.MetaValue;
			}
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x001AFFD2 File Offset: 0x001AE1D2
		public override Value NewMeta(RecordValue metaValue)
		{
			return this.cube.NewMeta(metaValue);
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x001AFFE0 File Offset: 0x001AE1E0
		public override bool TryGetAs<T>(out T contract)
		{
			return this.cube.TryGetAs<T>(out contract);
		}

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x06007DE0 RID: 32224 RVA: 0x001AFFEE File Offset: 0x001AE1EE
		public override Keys DimensionAttributes
		{
			get
			{
				return this.cube.DimensionAttributes;
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06007DE1 RID: 32225 RVA: 0x001AFFFB File Offset: 0x001AE1FB
		public override TableValue DisplayFolders
		{
			get
			{
				return this.cube.DisplayFolders;
			}
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06007DE2 RID: 32226 RVA: 0x001B0008 File Offset: 0x001AE208
		public override TableValue MeasureGroups
		{
			get
			{
				return this.cube.MeasureGroups;
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06007DE3 RID: 32227 RVA: 0x001B0015 File Offset: 0x001AE215
		public override TableValue Dimensions
		{
			get
			{
				return this.cube.Dimensions;
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x06007DE4 RID: 32228 RVA: 0x001B0022 File Offset: 0x001AE222
		public override TableValue Measures
		{
			get
			{
				return this.cube.Measures;
			}
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x001B002F File Offset: 0x001AE22F
		public override TableValue Parameters
		{
			get
			{
				return this.cube.Parameters;
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x001B003C File Offset: 0x001AE23C
		public override TableValue Properties
		{
			get
			{
				return this.cube.Properties;
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06007DE7 RID: 32231 RVA: 0x001B0049 File Offset: 0x001AE249
		public override TableValue MeasureProperties
		{
			get
			{
				return this.cube.MeasureProperties;
			}
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x001B0056 File Offset: 0x001AE256
		public override CubeValue ExpandDimensionAttributes(TableValue dimensionTable)
		{
			return this.cube.ExpandDimensionAttributes(dimensionTable);
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x001B0064 File Offset: 0x001AE264
		public override CubeValue CollapseDimensionAttributes(int[] columns)
		{
			return this.cube.CollapseDimensionAttributes(columns);
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x001B0072 File Offset: 0x001AE272
		public override CubeValue AddMeasureColumn(string newColumnName, FunctionValue measure)
		{
			return this.cube.AddMeasureColumn(newColumnName, measure);
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x001B0081 File Offset: 0x001AE281
		public override CubeValue ApplyParameter(FunctionValue parameter, Value[] args)
		{
			return this.cube.ApplyParameter(parameter, args);
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06007DEC RID: 32236 RVA: 0x001B0090 File Offset: 0x001AE290
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.cube.SortOrder;
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06007DED RID: 32237 RVA: 0x001B009D File Offset: 0x001AE29D
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.cube.ComputedColumns;
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06007DEE RID: 32238 RVA: 0x001B00AA File Offset: 0x001AE2AA
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.cube.QueryDomain;
			}
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x001B00B7 File Offset: 0x001AE2B7
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.cube.GetEnumerator();
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x001B00C4 File Offset: 0x001AE2C4
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.cube.TryGetProcessor(out processor);
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06007DF1 RID: 32241 RVA: 0x001B00D2 File Offset: 0x001AE2D2
		public override Query Query
		{
			get
			{
				return this.cube.Query;
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x06007DF2 RID: 32242 RVA: 0x001B00DF File Offset: 0x001AE2DF
		public override long LargeCount
		{
			get
			{
				return this.cube.LargeCount;
			}
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x001B00EC File Offset: 0x001AE2EC
		public override bool TryGetValue(Value index, out Value value)
		{
			return this.cube.TryGetValue(index, out value);
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x001B00FB File Offset: 0x001AE2FB
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue tableValue)
		{
			return this.cube.TrySelectColumns(columnSelection, out tableValue);
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x001B010A File Offset: 0x001AE30A
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.cube.SelectRows(condition);
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x001B0118 File Offset: 0x001AE318
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.cube.AddColumns(columnGenerator);
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x001B0126 File Offset: 0x001AE326
		public override TableValue Group(Grouping grouping)
		{
			return this.cube.Group(grouping);
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x001B0134 File Offset: 0x001AE334
		public override TableValue Skip(RowCount count)
		{
			return this.cube.Skip(count);
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x001B0142 File Offset: 0x001AE342
		public override TableValue Take(RowCount count)
		{
			return this.cube.Take(count);
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x001B0150 File Offset: 0x001AE350
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.cube.Sort(sortOrder);
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x001B015E File Offset: 0x001AE35E
		public override TableValue Unordered()
		{
			return this.cube.Unordered();
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x001B016B File Offset: 0x001AE36B
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.cube.Distinct(distinctCriteria);
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x001B0179 File Offset: 0x001AE379
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.cube.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x001B0191 File Offset: 0x001AE391
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.cube.ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x001B01A0 File Offset: 0x001AE3A0
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.cube.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x001B01B0 File Offset: 0x001AE3B0
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.cube.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x001B01C2 File Offset: 0x001AE3C2
		public override IPageReader GetReader()
		{
			return this.cube.GetReader();
		}

		// Token: 0x04004546 RID: 17734
		private CubeValue cube;
	}
}
