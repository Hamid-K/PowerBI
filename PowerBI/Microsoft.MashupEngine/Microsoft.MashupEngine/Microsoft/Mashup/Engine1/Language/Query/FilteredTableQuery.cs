using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D0 RID: 6096
	internal abstract class FilteredTableQuery : TableQuery, IFilteredQuery
	{
		// Token: 0x06009A21 RID: 39457 RVA: 0x001FE94B File Offset: 0x001FCB4B
		public FilteredTableQuery(TableValue table, IEngineHost host)
			: base(table, host)
		{
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x001FE955 File Offset: 0x001FCB55
		public override Query SelectRows(FunctionValue condition)
		{
			return FilteredQuery.New<FilteredTableQuery>(condition, this);
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x001FE95E File Offset: 0x001FCB5E
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.UpdateRows(columnUpdates, ConstantFunctionValue.EachTrue);
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x001FE96C File Offset: 0x001FCB6C
		public override ActionValue DeleteRows()
		{
			return this.DeleteRows(ConstantFunctionValue.EachTrue);
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TrySelectRows(FunctionValue condition, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}

		// Token: 0x06009A27 RID: 39463 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue DeleteRows(FunctionValue selector)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}
	}
}
