using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B0 RID: 176
	[ImmutableObject(true)]
	internal sealed class DataRow : IDataRow
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		internal DataRow(IReadOnlyList<object> columns)
		{
			this._list = new FunctionalList<object>(columns);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
		private DataRow(DataRow other, IReadOnlyList<object> columns)
		{
			this._list = other._list.Append(columns);
			this._nestedColumnAdditionsDepth = other._nestedColumnAdditionsDepth + 1;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DE1A File Offset: 0x0000C01A
		public object GetObject(int index)
		{
			return this._list.GetItem(index);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000DE28 File Offset: 0x0000C028
		public double? GetAsDouble(int index)
		{
			object @object = this.GetObject(index);
			if (!@object.IsNumeric())
			{
				return null;
			}
			return new double?(Convert.ToDouble(@object, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000DE60 File Offset: 0x0000C060
		public long? GetAsInt64(int index)
		{
			object @object = this.GetObject(index);
			if (!@object.IsNumeric())
			{
				return null;
			}
			return new long?(Convert.ToInt64(@object));
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000DE92 File Offset: 0x0000C092
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000DE9F File Offset: 0x0000C09F
		public IDataRow AddColumns(IReadOnlyList<object> columns)
		{
			return new DataRow(this, columns);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04000252 RID: 594
		internal const int MaxNestedColumnAdditionsDepth = 10;

		// Token: 0x04000253 RID: 595
		private readonly FunctionalList<object> _list;

		// Token: 0x04000254 RID: 596
		private int _nestedColumnAdditionsDepth;
	}
}
