using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D71 RID: 3441
	internal class SetContextTableValue : WrappingTableValue
	{
		// Token: 0x06005D8C RID: 23948 RVA: 0x00143CD5 File Offset: 0x00141ED5
		public static TableValue New(SetContextQuery query)
		{
			return new SetContextTableValue(query).PromoteIfCube();
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x00143CE2 File Offset: 0x00141EE2
		private SetContextTableValue(SetContextQuery query)
			: base(new QueryTableValue(query))
		{
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x00143CF0 File Offset: 0x00141EF0
		protected override TableValue New(TableValue table)
		{
			SetContextQuery setContextQuery = table.Query as SetContextQuery;
			if (setContextQuery != null)
			{
				table = SetContextTableValue.New(setContextQuery);
			}
			return table;
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x00143D18 File Offset: 0x00141F18
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					TypeValue[] array = new TypeValue[this.Columns.Length];
					IValueReference[] array2 = new IValueReference[array.Length];
					Value[] array3 = new Value[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.Query.GetColumnType(i);
						ICubeObject cubeObject;
						if (this.Query.Cube.TryGetObject(this.Query.Projection.Identifiers[i], out cubeObject))
						{
							array3[i] = TextValue.New(((ICubeObject2)cubeObject).Caption);
						}
						else
						{
							array3[i] = TextValue.New(this.Columns[i]);
						}
						array2[i] = RecordTypeValue.NewField(array[i], null);
					}
					RecordValue recordValue = RecordValue.New(Keys.New("Documentation.FieldCaption"), new Value[] { RecordValue.New(this.Columns, array3) });
					this.type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(this.Columns, array2), false)).NewMeta(recordValue).AsType;
				}
				return this.type;
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x00143E2B File Offset: 0x0014202B
		public new SetContextQuery Query
		{
			get
			{
				return (SetContextQuery)base.Query;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x00143E38 File Offset: 0x00142038
		public override bool IsCube
		{
			get
			{
				return this.Query.Context != null;
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06005D92 RID: 23954 RVA: 0x00143E48 File Offset: 0x00142048
		public override CubeValue AsCube
		{
			get
			{
				if (this.IsCube)
				{
					return new SetContextTableValue.SetContextCubeValue(this);
				}
				return base.AsCube;
			}
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x00143E5F File Offset: 0x0014205F
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue tableValue)
		{
			tableValue = this.New(base.SelectColumns(columnSelection));
			return true;
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x00143E71 File Offset: 0x00142071
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.Query.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x04003379 RID: 13177
		private TypeValue type;

		// Token: 0x02000D72 RID: 3442
		private class SetContextCubeValue : CubeValue
		{
			// Token: 0x06005D95 RID: 23957 RVA: 0x00143E83 File Offset: 0x00142083
			public SetContextCubeValue(SetContextTableValue table)
			{
				this.table = table;
				if (this.table.Query.Context == null)
				{
					throw CubeValue.NewInvalidCubeException();
				}
			}

			// Token: 0x06005D96 RID: 23958 RVA: 0x00143EAA File Offset: 0x001420AA
			private TableValue New(TableValue table)
			{
				return table.PromoteIfCube();
			}

			// Token: 0x17001B9F RID: 7071
			// (get) Token: 0x06005D97 RID: 23959 RVA: 0x000091AE File Offset: 0x000073AE
			public override Keys DimensionAttributes
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001BA0 RID: 7072
			// (get) Token: 0x06005D98 RID: 23960 RVA: 0x00143EB2 File Offset: 0x001420B2
			public override TableValue DisplayFolders
			{
				get
				{
					return this.table.Query.Context.DisplayFolders;
				}
			}

			// Token: 0x17001BA1 RID: 7073
			// (get) Token: 0x06005D99 RID: 23961 RVA: 0x00143EC9 File Offset: 0x001420C9
			public override TableValue MeasureGroups
			{
				get
				{
					return this.table.Query.Context.MeasureGroups;
				}
			}

			// Token: 0x17001BA2 RID: 7074
			// (get) Token: 0x06005D9A RID: 23962 RVA: 0x00143EE0 File Offset: 0x001420E0
			public override TableValue Dimensions
			{
				get
				{
					return this.table.Query.Context.Dimensions;
				}
			}

			// Token: 0x17001BA3 RID: 7075
			// (get) Token: 0x06005D9B RID: 23963 RVA: 0x00143EF7 File Offset: 0x001420F7
			public override TableValue Measures
			{
				get
				{
					return this.table.Query.Context.Measures;
				}
			}

			// Token: 0x17001BA4 RID: 7076
			// (get) Token: 0x06005D9C RID: 23964 RVA: 0x00143F0E File Offset: 0x0014210E
			public override TableValue Properties
			{
				get
				{
					return this.table.Query.Context.GetAvailableProperties();
				}
			}

			// Token: 0x17001BA5 RID: 7077
			// (get) Token: 0x06005D9D RID: 23965 RVA: 0x00143F25 File Offset: 0x00142125
			public override TableValue MeasureProperties
			{
				get
				{
					return this.table.Query.Context.GetAvailableMeasureProperties();
				}
			}

			// Token: 0x17001BA6 RID: 7078
			// (get) Token: 0x06005D9E RID: 23966 RVA: 0x00143F3C File Offset: 0x0014213C
			public override TableValue Parameters
			{
				get
				{
					return this.table.Query.Context.GetParameters(this);
				}
			}

			// Token: 0x17001BA7 RID: 7079
			// (get) Token: 0x06005D9F RID: 23967 RVA: 0x00143F54 File Offset: 0x00142154
			public override Query Query
			{
				get
				{
					return this.table.Query;
				}
			}

			// Token: 0x17001BA8 RID: 7080
			// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x00143F61 File Offset: 0x00142161
			public override Keys Columns
			{
				get
				{
					return this.table.Columns;
				}
			}

			// Token: 0x17001BA9 RID: 7081
			// (get) Token: 0x06005DA1 RID: 23969 RVA: 0x00143F6E File Offset: 0x0014216E
			public override TableSortOrder SortOrder
			{
				get
				{
					return this.table.SortOrder;
				}
			}

			// Token: 0x17001BAA RID: 7082
			// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x00143F7B File Offset: 0x0014217B
			public override TypeValue Type
			{
				get
				{
					return this.table.Type;
				}
			}

			// Token: 0x06005DA3 RID: 23971 RVA: 0x00143F88 File Offset: 0x00142188
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.table.GetEnumerator();
			}

			// Token: 0x06005DA4 RID: 23972 RVA: 0x00143F95 File Offset: 0x00142195
			public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue tableValue)
			{
				if (this.table.TrySelectColumns(columnSelection, out tableValue))
				{
					tableValue = this.New(tableValue);
					return true;
				}
				return false;
			}

			// Token: 0x06005DA5 RID: 23973 RVA: 0x00143FB3 File Offset: 0x001421B3
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				return this.table.TryInvokeAsArgument(function, arguments, index, out result);
			}

			// Token: 0x06005DA6 RID: 23974 RVA: 0x00143FC5 File Offset: 0x001421C5
			public override CubeValue ApplyParameter(FunctionValue parameter, Value[] args)
			{
				return SetContextTableValue.New(this.table.Query.ApplyParameter(parameter, args)).AsCube;
			}

			// Token: 0x06005DA7 RID: 23975 RVA: 0x000091AE File Offset: 0x000073AE
			public override CubeValue ExpandDimensionAttributes(TableValue dimensionTable)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005DA8 RID: 23976 RVA: 0x000091AE File Offset: 0x000073AE
			public override CubeValue CollapseDimensionAttributes(int[] columns)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005DA9 RID: 23977 RVA: 0x000091AE File Offset: 0x000073AE
			public override CubeValue AddMeasureColumn(string columnName, FunctionValue function)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400337A RID: 13178
			private readonly SetContextTableValue table;
		}
	}
}
