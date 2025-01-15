using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x020006F8 RID: 1784
	public sealed class Row
	{
		// Token: 0x060053C3 RID: 21443 RVA: 0x0012D3F1 File Offset: 0x0012B5F1
		public Row(KeyValuePair<string, DbExpression> columnValue, params KeyValuePair<string, DbExpression>[] columnValues)
		{
			this.arguments = new ReadOnlyCollection<KeyValuePair<string, DbExpression>>(Helpers.Prepend<KeyValuePair<string, DbExpression>>(columnValues, columnValue));
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0012D40B File Offset: 0x0012B60B
		public DbNewInstanceExpression ToExpression()
		{
			return DbExpressionBuilder.NewRow(this.arguments);
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0012D418 File Offset: 0x0012B618
		public static implicit operator DbExpression(Row row)
		{
			Check.NotNull<Row>(row, "row");
			return row.ToExpression();
		}

		// Token: 0x04001E03 RID: 7683
		private readonly ReadOnlyCollection<KeyValuePair<string, DbExpression>> arguments;
	}
}
