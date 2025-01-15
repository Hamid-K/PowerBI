using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002A RID: 42
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class StringColumn : Column
	{
		// Token: 0x06000137 RID: 311 RVA: 0x0000449A File Offset: 0x0000269A
		internal StringColumn(int maxRowCount)
		{
			this.values = new string[maxRowCount];
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000044AE File Offset: 0x000026AE
		public override ColumnType Type
		{
			get
			{
				return ColumnType.String;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000044B2 File Offset: 0x000026B2
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000044BA File Offset: 0x000026BA
		public override void Clear()
		{
			Array.Clear(this.values, 0, this.rowCount);
			this.rowCount = 0;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000044D8 File Offset: 0x000026D8
		public override void AddValue(object value)
		{
			string[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = (string)value;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004504 File Offset: 0x00002704
		[global::System.Runtime.CompilerServices.NullableContext(0)]
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

		// Token: 0x0600013D RID: 317 RVA: 0x00004554 File Offset: 0x00002754
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

		// Token: 0x0600013E RID: 318 RVA: 0x0000458C File Offset: 0x0000278C
		public override void AddNull()
		{
			string[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = string.Empty;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000045B6 File Offset: 0x000027B6
		public override string GetString(int row)
		{
			return this.values[row];
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000045C0 File Offset: 0x000027C0
		public override object GetObject(int row)
		{
			return this.GetString(row);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000045CC File Offset: 0x000027CC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			if (binding.MemoryOwner == DBMEMOWNER.PROVIDEROWNED)
			{
				throw new NotSupportedException();
			}
			string text = this.values[row];
			if (binding.DestType == DBTYPE.WSTR && DbLength.GetLength(text).Value + 2UL <= binding.DestMaxLength.Value)
			{
				for (int i = 0; i < text.Length; i++)
				{
					*(short*)(destValue + (IntPtr)i * 2) = (short)text[i];
				}
				*(short*)(destValue + (IntPtr)text.Length * 2) = 0;
				destLength = DbLength.GetLength(text);
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.BSTR)
			{
				*(IntPtr*)destValue = Marshal.StringToBSTR(text).ToPointer();
				destLength = DbLength.GetLength(text);
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.VARIANT)
			{
				Variant.Init((VARIANT*)destValue);
				((VARIANT*)destValue)->Type = VARTYPE.BSTR;
				((VARIANT*)destValue)->ValuePointer = Marshal.StringToBSTR(text).ToPointer();
				destLength = DbLength.Variant;
				return DBSTATUS.S_OK;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				DBLENGTH length = DbLength.GetLength(text);
				DBSTATUS dbstatus;
				dataConvert.DataConvert(DBTYPE.WSTR, binding.DestType, length, out destLength, (void*)ptr, (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
				return dbstatus;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000470A File Offset: 0x0000290A
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000470D File Offset: 0x0000290D
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000472E File Offset: 0x0000292E
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0400003E RID: 62
		private readonly string[] values;

		// Token: 0x0400003F RID: 63
		private int rowCount;
	}
}
