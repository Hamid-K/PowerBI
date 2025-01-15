using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E5A RID: 7770
	internal abstract class DelegatingColumn : Column
	{
		// Token: 0x0600BF03 RID: 48899 RVA: 0x0026A018 File Offset: 0x00268218
		protected DelegatingColumn(Column column)
		{
			this.column = column;
		}

		// Token: 0x17002EF2 RID: 12018
		// (get) Token: 0x0600BF04 RID: 48900 RVA: 0x0026A027 File Offset: 0x00268227
		public override ColumnType Type
		{
			get
			{
				return this.column.Type;
			}
		}

		// Token: 0x17002EF3 RID: 12019
		// (get) Token: 0x0600BF05 RID: 48901 RVA: 0x0026A034 File Offset: 0x00268234
		public override int RowCount
		{
			get
			{
				return this.column.RowCount;
			}
		}

		// Token: 0x17002EF4 RID: 12020
		// (get) Token: 0x0600BF06 RID: 48902 RVA: 0x0026A041 File Offset: 0x00268241
		protected Column Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x0600BF07 RID: 48903 RVA: 0x0026A049 File Offset: 0x00268249
		public override void Clear()
		{
			this.column.Clear();
		}

		// Token: 0x0600BF08 RID: 48904
		protected abstract void AddNotNull();

		// Token: 0x0600BF09 RID: 48905 RVA: 0x0026A056 File Offset: 0x00268256
		public override void AddNull()
		{
			this.column.AddNull();
		}

		// Token: 0x0600BF0A RID: 48906 RVA: 0x0026A063 File Offset: 0x00268263
		public override void AddValue(object value)
		{
			this.column.AddValue(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF0B RID: 48907 RVA: 0x0026A077 File Offset: 0x00268277
		public override void AddBoolean(bool value)
		{
			this.column.AddBoolean(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF0C RID: 48908 RVA: 0x0026A08B File Offset: 0x0026828B
		public override void AddByte(byte value)
		{
			this.column.AddByte(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF0D RID: 48909 RVA: 0x0026A09F File Offset: 0x0026829F
		public override void AddUInt16(ushort value)
		{
			this.column.AddUInt16(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF0E RID: 48910 RVA: 0x0026A0B3 File Offset: 0x002682B3
		public override void AddUInt32(uint value)
		{
			this.column.AddUInt32(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF0F RID: 48911 RVA: 0x0026A0C7 File Offset: 0x002682C7
		public override void AddUInt64(ulong value)
		{
			this.column.AddUInt64(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF10 RID: 48912 RVA: 0x0026A0DB File Offset: 0x002682DB
		public override void AddSByte(sbyte value)
		{
			this.column.AddSByte(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF11 RID: 48913 RVA: 0x0026A0EF File Offset: 0x002682EF
		public override void AddInt16(short value)
		{
			this.column.AddInt16(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF12 RID: 48914 RVA: 0x0026A103 File Offset: 0x00268303
		public override void AddInt32(int value)
		{
			this.column.AddInt32(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF13 RID: 48915 RVA: 0x0026A117 File Offset: 0x00268317
		public override void AddInt64(long value)
		{
			this.column.AddInt64(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF14 RID: 48916 RVA: 0x0026A12B File Offset: 0x0026832B
		public override void AddFloat(float value)
		{
			this.column.AddFloat(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF15 RID: 48917 RVA: 0x0026A13F File Offset: 0x0026833F
		public override void AddDouble(double value)
		{
			this.column.AddDouble(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF16 RID: 48918 RVA: 0x0026A153 File Offset: 0x00268353
		public override void AddDecimal(decimal value)
		{
			this.column.AddDecimal(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF17 RID: 48919 RVA: 0x0026A167 File Offset: 0x00268367
		public override void AddNumber(Number value)
		{
			this.column.AddNumber(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF18 RID: 48920 RVA: 0x0026A17B File Offset: 0x0026837B
		public override void AddCurrency(Currency value)
		{
			this.column.AddCurrency(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF19 RID: 48921 RVA: 0x0026A18F File Offset: 0x0026838F
		public override void AddDate(Date value)
		{
			this.column.AddDate(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1A RID: 48922 RVA: 0x0026A1A3 File Offset: 0x002683A3
		public override void AddTime(Time value)
		{
			this.column.AddTime(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1B RID: 48923 RVA: 0x0026A1B7 File Offset: 0x002683B7
		public override void AddDateTime(DateTime value)
		{
			this.column.AddDateTime(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1C RID: 48924 RVA: 0x0026A1CB File Offset: 0x002683CB
		public override void AddDateTimeOffset(DateTimeOffset value)
		{
			this.column.AddDateTimeOffset(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1D RID: 48925 RVA: 0x0026A1DF File Offset: 0x002683DF
		public override void AddTimeSpan(TimeSpan value)
		{
			this.column.AddTimeSpan(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1E RID: 48926 RVA: 0x0026A1F3 File Offset: 0x002683F3
		public override void AddGuid(Guid value)
		{
			this.column.AddGuid(value);
			this.AddNotNull();
		}

		// Token: 0x0600BF1F RID: 48927 RVA: 0x0026A207 File Offset: 0x00268407
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.column.AddValue(type, value, length);
			this.AddNotNull();
		}

		// Token: 0x0600BF20 RID: 48928 RVA: 0x0026A21D File Offset: 0x0026841D
		public override bool TryAddValue(object value)
		{
			return this.column.TryAddValue(value);
		}

		// Token: 0x0600BF21 RID: 48929 RVA: 0x0026A22B File Offset: 0x0026842B
		public override bool IsNull(int row)
		{
			return this.column.IsNull(row);
		}

		// Token: 0x0600BF22 RID: 48930 RVA: 0x0026A239 File Offset: 0x00268439
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600BF23 RID: 48931 RVA: 0x0026A24D File Offset: 0x0026844D
		public override bool GetBoolean(int row)
		{
			return this.column.GetBoolean(row);
		}

		// Token: 0x0600BF24 RID: 48932 RVA: 0x0026A25B File Offset: 0x0026845B
		public override byte GetByte(int row)
		{
			return this.column.GetByte(row);
		}

		// Token: 0x0600BF25 RID: 48933 RVA: 0x0026A269 File Offset: 0x00268469
		public override short GetInt16(int row)
		{
			return this.column.GetInt16(row);
		}

		// Token: 0x0600BF26 RID: 48934 RVA: 0x0026A277 File Offset: 0x00268477
		public override int GetInt32(int row)
		{
			return this.column.GetInt32(row);
		}

		// Token: 0x0600BF27 RID: 48935 RVA: 0x0026A285 File Offset: 0x00268485
		public override long GetInt64(int row)
		{
			return this.column.GetInt64(row);
		}

		// Token: 0x0600BF28 RID: 48936 RVA: 0x0026A293 File Offset: 0x00268493
		public override float GetFloat(int row)
		{
			return this.column.GetFloat(row);
		}

		// Token: 0x0600BF29 RID: 48937 RVA: 0x0026A2A1 File Offset: 0x002684A1
		public override Guid GetGuid(int row)
		{
			return this.column.GetGuid(row);
		}

		// Token: 0x0600BF2A RID: 48938 RVA: 0x0026A2AF File Offset: 0x002684AF
		public override double GetDouble(int row)
		{
			return this.column.GetDouble(row);
		}

		// Token: 0x0600BF2B RID: 48939 RVA: 0x0026A2BD File Offset: 0x002684BD
		public override decimal GetDecimal(int row)
		{
			return this.column.GetDecimal(row);
		}

		// Token: 0x0600BF2C RID: 48940 RVA: 0x0026A2CB File Offset: 0x002684CB
		public override DateTime GetDateTime(int row)
		{
			return this.column.GetDateTime(row);
		}

		// Token: 0x0600BF2D RID: 48941 RVA: 0x0026A2D9 File Offset: 0x002684D9
		public override string GetString(int row)
		{
			return this.column.GetString(row);
		}

		// Token: 0x0600BF2E RID: 48942 RVA: 0x0026A2E7 File Offset: 0x002684E7
		public override object GetObject(int row)
		{
			return this.column.GetObject(row);
		}

		// Token: 0x0600BF2F RID: 48943 RVA: 0x0026A2F5 File Offset: 0x002684F5
		public override void Serialize(PageWriter writer)
		{
			this.column.Serialize(writer);
		}

		// Token: 0x0600BF30 RID: 48944 RVA: 0x0026A303 File Offset: 0x00268503
		public override void Deserialize(PageReader reader)
		{
			this.column.Deserialize(reader);
		}

		// Token: 0x0400612B RID: 24875
		private readonly Column column;
	}
}
