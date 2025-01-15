using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200115E RID: 4446
	internal class ValueAdapterDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x06007471 RID: 29809 RVA: 0x0018FD4D File Offset: 0x0018DF4D
		public ValueAdapterDbDataReader(DbDataReaderWithTableSchema reader, Type[] types, Func<object, object>[] adapters)
			: base(reader)
		{
			this.adapters = adapters;
			this.types = types;
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x06007472 RID: 29810 RVA: 0x0018FD64 File Offset: 0x0018DF64
		public override TableSchema Schema
		{
			get
			{
				TableSchema schema = base.Schema;
				for (int i = 0; i < schema.ColumnCount; i++)
				{
					Type type = this.types[i];
					if (type != null)
					{
						schema.GetColumn(i).DataType = type;
					}
				}
				return schema;
			}
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x0018FDA9 File Offset: 0x0018DFA9
		public override Type GetFieldType(int ordinal)
		{
			return this.types[ordinal] ?? base.GetFieldType(ordinal);
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x0018FDBE File Offset: 0x0018DFBE
		public override object GetValue(int ordinal)
		{
			return this.AdaptValue(ordinal, base.GetValue(ordinal));
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x0018FDD0 File Offset: 0x0018DFD0
		public override int GetValues(object[] values)
		{
			int values2 = base.GetValues(values);
			for (int i = 0; i < values2; i++)
			{
				values[i] = this.AdaptValue(i, values[i]);
			}
			return values2;
		}

		// Token: 0x1700205B RID: 8283
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x06007477 RID: 29815 RVA: 0x0018FE00 File Offset: 0x0018E000
		private object AdaptValue(int ordinal, object value)
		{
			Func<object, object> func = this.adapters[ordinal];
			if (func != null && value != null && value != DBNull.Value)
			{
				value = func(value);
			}
			return value;
		}

		// Token: 0x0400400E RID: 16398
		private readonly Func<object, object>[] adapters;

		// Token: 0x0400400F RID: 16399
		private readonly Type[] types;
	}
}
