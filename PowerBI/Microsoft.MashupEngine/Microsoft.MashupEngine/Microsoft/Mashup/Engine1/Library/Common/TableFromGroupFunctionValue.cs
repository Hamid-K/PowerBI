using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001142 RID: 4418
	internal sealed class TableFromGroupFunctionValue : NativeFunctionValue1<TableValue, TableValue>
	{
		// Token: 0x060073C0 RID: 29632 RVA: 0x0018E258 File Offset: 0x0018C458
		public TableFromGroupFunctionValue(Value aggregatedColumns)
			: base(TypeValue.Table, "aggregatedRows", TypeValue.Table)
		{
			this.aggregatedColumns = aggregatedColumns;
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x0018E276 File Offset: 0x0018C476
		public override TableValue TypedInvoke(TableValue aggregatedRows)
		{
			return new TableFromGroupFunctionValue.AggregateTableValue(aggregatedRows);
		}

		// Token: 0x04003FBC RID: 16316
		private readonly Value aggregatedColumns;

		// Token: 0x02001143 RID: 4419
		private sealed class AggregateTableValue : TableValue
		{
			// Token: 0x060073C2 RID: 29634 RVA: 0x0018E280 File Offset: 0x0018C480
			public AggregateTableValue(TableValue aggregatedRows)
			{
				this.aggregatedRows = aggregatedRows;
				IExpression expression = aggregatedRows.Expression;
				IList<IExpression> list;
				Value value;
				Value value2;
				Value value3;
				if (expression == null || !expression.TryGetInvocation(TableModule.Table.Group, 3, out list) || !list[0].TryGetConstant(out value) || !value.IsTable || !list[1].TryGetConstant(out value2) || !value2.IsList || !value2.AsList.IsEmpty || !list[2].TryGetConstant(out value3) || !value3.IsList)
				{
					throw new InvalidOperationException();
				}
				this.baseTableType = value.Type.AsTableType;
			}

			// Token: 0x17002043 RID: 8259
			// (get) Token: 0x060073C3 RID: 29635 RVA: 0x0018E322 File Offset: 0x0018C522
			public override TypeValue Type
			{
				get
				{
					return this.baseTableType;
				}
			}

			// Token: 0x060073C4 RID: 29636 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060073C5 RID: 29637 RVA: 0x0018E32C File Offset: 0x0018C52C
			public override TableValue Group(Grouping grouping)
			{
				if (grouping.Adjacent || grouping.KeyKeys.Length != 0 || grouping.KeyColumns.Length != 0 || !grouping.ResultKeys.Equals(this.aggregatedRows.Columns))
				{
					throw new InvalidOperationException();
				}
				return this.aggregatedRows;
			}

			// Token: 0x04003FBD RID: 16317
			private readonly TableValue aggregatedRows;

			// Token: 0x04003FBE RID: 16318
			private readonly TableTypeValue baseTableType;
		}
	}
}
