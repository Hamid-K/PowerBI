using System;
using Microsoft.DataShaping.Common;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200008A RID: 138
	internal sealed class ColumnMatchCondition
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000BE51 File Offset: 0x0000A051
		internal ColumnMatchCondition(int fieldIndex, object value, ColumnMatchOperator op)
		{
			this._fieldIndex = fieldIndex;
			this._value = value;
			this._operator = op;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000BE70 File Offset: 0x0000A070
		internal bool Matches(IDataRow dataRow, IDataComparer comparer)
		{
			int num = comparer.Compare(dataRow.GetObject(this._fieldIndex), this._value);
			switch (this._operator)
			{
			case ColumnMatchOperator.Equal:
				return num == 0;
			case ColumnMatchOperator.Greater:
				return num > 0;
			case ColumnMatchOperator.NotEqual:
				return num != 0;
			default:
				Contract.RetailFail("Unrecognized ColumnMatchOpertor {0}", this._operator);
				return false;
			}
		}

		// Token: 0x040001F9 RID: 505
		private readonly int _fieldIndex;

		// Token: 0x040001FA RID: 506
		private readonly object _value;

		// Token: 0x040001FB RID: 507
		private readonly ColumnMatchOperator _operator;
	}
}
