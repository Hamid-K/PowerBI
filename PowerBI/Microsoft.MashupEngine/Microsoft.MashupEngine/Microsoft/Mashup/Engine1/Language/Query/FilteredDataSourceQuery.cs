using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017CF RID: 6095
	internal abstract class FilteredDataSourceQuery : DataSourceQuery, IFilteredQuery
	{
		// Token: 0x06009A1A RID: 39450 RVA: 0x001FE90C File Offset: 0x001FCB0C
		public override Query SelectRows(FunctionValue condition)
		{
			return FilteredQuery.New<FilteredDataSourceQuery>(condition, this);
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x001FE915 File Offset: 0x001FCB15
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.UpdateRows(columnUpdates, ConstantFunctionValue.EachTrue);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x001FE923 File Offset: 0x001FCB23
		public override ActionValue DeleteRows()
		{
			return this.DeleteRows(ConstantFunctionValue.EachTrue);
		}

		// Token: 0x06009A1D RID: 39453 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TrySelectRows(FunctionValue condition, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue DeleteRows(FunctionValue selector)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}
	}
}
