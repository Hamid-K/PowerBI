using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E74 RID: 7796
	internal class StringColumn : Column
	{
		// Token: 0x0600C012 RID: 49170 RVA: 0x0026B575 File Offset: 0x00269775
		public StringColumn(int maxRowCount)
		{
			this.values = new string[maxRowCount];
		}

		// Token: 0x17002F0F RID: 12047
		// (get) Token: 0x0600C013 RID: 49171 RVA: 0x00243592 File Offset: 0x00241792
		public override ColumnType Type
		{
			get
			{
				return ColumnType.String;
			}
		}

		// Token: 0x0600C014 RID: 49172 RVA: 0x0026B589 File Offset: 0x00269789
		public override void Clear()
		{
			Array.Clear(this.values, 0, this.rowCount);
			this.rowCount = 0;
		}

		// Token: 0x17002F10 RID: 12048
		// (get) Token: 0x0600C015 RID: 49173 RVA: 0x0026B5A4 File Offset: 0x002697A4
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600C016 RID: 49174 RVA: 0x0026B5AC File Offset: 0x002697AC
		public override void AddValue(object value)
		{
			base.CheckNull(value);
			string[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = (string)value;
		}

		// Token: 0x0600C017 RID: 49175 RVA: 0x0026B5E0 File Offset: 0x002697E0
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			string text;
			if ((type & DBTYPE.BYREF) == DBTYPE.BYREF)
			{
				text = new string(*(IntPtr*)value, 0, length / 2);
			}
			else
			{
				text = new string((char*)value, 0, length / 2);
			}
			string[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = text;
		}

		// Token: 0x0600C018 RID: 49176 RVA: 0x0026B630 File Offset: 0x00269830
		public override bool TryAddValue(object value)
		{
			if (value is string)
			{
				string[] array = this.values;
				int num = this.rowCount;
				this.rowCount = num + 1;
				array[num] = (string)value;
				return true;
			}
			return false;
		}

		// Token: 0x0600C019 RID: 49177 RVA: 0x0026B668 File Offset: 0x00269868
		public override void AddNull()
		{
			string[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = string.Empty;
		}

		// Token: 0x0600C01A RID: 49178 RVA: 0x0026B692 File Offset: 0x00269892
		public override string GetString(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600C01B RID: 49179 RVA: 0x0026B69C File Offset: 0x0026989C
		public override object GetObject(int row)
		{
			return this.GetString(row);
		}

		// Token: 0x0600C01C RID: 49180 RVA: 0x0026B6A8 File Offset: 0x002698A8
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			if (binding.MemoryOwner == DBMEMOWNER.PROVIDEROWNED)
			{
				throw new NotSupportedException();
			}
			string text = this.values[row];
			DBSTATUS dbstatus = DataConversions.TryConvertStringToNativeString(text, binding, destValue, out destLength);
			if (dbstatus == DBSTATUS.S_OK)
			{
				return dbstatus;
			}
			return dataConvert.DataConvert(text, binding, destValue, out destLength);
		}

		// Token: 0x0600C01D RID: 49181 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600C01E RID: 49182 RVA: 0x0026B6EA File Offset: 0x002698EA
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600C01F RID: 49183 RVA: 0x0026B70B File Offset: 0x0026990B
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0400613E RID: 24894
		private readonly string[] values;

		// Token: 0x0400613F RID: 24895
		private int rowCount;
	}
}
