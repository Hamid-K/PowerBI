using System;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BA RID: 4794
	public abstract class CubeValue : TableValue
	{
		// Token: 0x06007DC4 RID: 32196 RVA: 0x001AFEDF File Offset: 0x001AE0DF
		public override Value NewMeta(RecordValue metaValue)
		{
			return CubeValue.New(this, metaValue);
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06007DC5 RID: 32197 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsCube
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06007DC6 RID: 32198 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public sealed override CubeValue AsCube
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06007DC7 RID: 32199
		public abstract Keys DimensionAttributes { get; }

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06007DC8 RID: 32200
		public abstract TableValue DisplayFolders { get; }

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x06007DC9 RID: 32201
		public abstract TableValue MeasureGroups { get; }

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x06007DCA RID: 32202
		public abstract TableValue Dimensions { get; }

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x06007DCB RID: 32203
		public abstract TableValue Measures { get; }

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x06007DCC RID: 32204
		public abstract TableValue Parameters { get; }

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x06007DCD RID: 32205
		public abstract TableValue Properties { get; }

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x06007DCE RID: 32206
		public abstract TableValue MeasureProperties { get; }

		// Token: 0x06007DCF RID: 32207
		public abstract CubeValue ExpandDimensionAttributes(TableValue dimensionTable);

		// Token: 0x06007DD0 RID: 32208
		public abstract CubeValue CollapseDimensionAttributes(int[] columns);

		// Token: 0x06007DD1 RID: 32209
		public abstract CubeValue AddMeasureColumn(string newColumnName, FunctionValue measure);

		// Token: 0x06007DD2 RID: 32210
		public abstract CubeValue ApplyParameter(FunctionValue parameter, Value[] args);

		// Token: 0x06007DD3 RID: 32211 RVA: 0x001AFEE8 File Offset: 0x001AE0E8
		public CubeValue ExpandDimensions(TableValue dimensionTable, Keys attributeKeys, Keys newColumnNames)
		{
			int[] array = TableValue.GetColumns(dimensionTable.Columns, attributeKeys).ComplementWithin(dimensionTable.Columns.Length);
			dimensionTable = dimensionTable.AsCube.CollapseDimensionAttributes(array);
			int[] columns = TableValue.GetColumns(dimensionTable.Columns, attributeKeys);
			dimensionTable = dimensionTable.SelectColumns(new ColumnSelection(newColumnNames, columns));
			return this.ExpandDimensionAttributes(dimensionTable);
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x001AFF44 File Offset: 0x001AE144
		public CubeValue CollapseDimensions(Keys columnKeys)
		{
			int[] columns = TableValue.GetColumns(this.Columns, columnKeys);
			return this.CollapseDimensionAttributes(columns);
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x001AFF65 File Offset: 0x001AE165
		protected static CubeValue New(CubeValue value, RecordValue meta)
		{
			if (!meta.IsEmpty)
			{
				value = new MetaCubeValue(value, meta);
			}
			return value;
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x001AFF79 File Offset: 0x001AE179
		protected static ValueException NewInvalidCubeException()
		{
			return ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
		}

		// Token: 0x0400453B RID: 17723
		public const string CubeCube = "Cube.Cube";

		// Token: 0x0400453C RID: 17724
		public const string CubeDimensionAttribute = "Cube.DimensionAttribute";

		// Token: 0x0400453D RID: 17725
		public const string CubeAttributeMemberId = "Cube.AttributeMemberId";

		// Token: 0x0400453E RID: 17726
		public const string CubeHasAttributeMemberIds = "Cube.HasAttributeMemberIds";

		// Token: 0x0400453F RID: 17727
		public const string CubePropertyKey = "Cube.PropertyKey";

		// Token: 0x04004540 RID: 17728
		public const string CubeHasPropertyKeys = "Cube.HasPropertyKeys";

		// Token: 0x04004541 RID: 17729
		public const string CubeHierarchies = "Cube.Hierarchies";

		// Token: 0x04004542 RID: 17730
		public const string CubeDimensions = "Cube.Dimensions";

		// Token: 0x04004543 RID: 17731
		public const string CubeGroupByKey = "Cube.GroupByKey";

		// Token: 0x04004544 RID: 17732
		public const string CubeDefaultMemberId = "Cube.DefaultMemberId";

		// Token: 0x04004545 RID: 17733
		public const string CubeSourceColumn = "Cube.SourceColumn";
	}
}
