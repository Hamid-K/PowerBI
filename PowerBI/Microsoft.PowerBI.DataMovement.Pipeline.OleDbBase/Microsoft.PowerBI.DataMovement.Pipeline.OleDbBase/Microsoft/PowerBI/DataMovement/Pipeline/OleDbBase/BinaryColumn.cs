using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002D RID: 45
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class BinaryColumn : Column
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000049E4 File Offset: 0x00002BE4
		internal BinaryColumn(int maxRowCount)
		{
			this.values = new byte[maxRowCount][];
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000049F8 File Offset: 0x00002BF8
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Binary;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000049FC File Offset: 0x00002BFC
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004A04 File Offset: 0x00002C04
		public override void Clear()
		{
			Array.Clear(this.values, 0, this.rowCount);
			this.rowCount = 0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004A20 File Offset: 0x00002C20
		public override void AddValue(object value)
		{
			byte[][] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = (byte[])value;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004A4C File Offset: 0x00002C4C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			void* ptr;
			if ((type & DBTYPE.BYREF) == DBTYPE.BYREF)
			{
				ptr = *(IntPtr*)value;
			}
			else
			{
				ptr = value;
			}
			byte[] array = new byte[length];
			Marshal.Copy(new IntPtr(ptr), array, 0, length);
			byte[][] array2 = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array2[num] = array;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004A9C File Offset: 0x00002C9C
		public override bool TryAddValue(object value)
		{
			if (value is byte[])
			{
				byte[][] array = this.values;
				int num = this.rowCount;
				this.rowCount = num + 1;
				array[num] = (byte[])value;
				return true;
			}
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public override void AddNull()
		{
			byte[][] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = new byte[0];
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004B00 File Offset: 0x00002D00
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			if (binding.MemoryOwner == DBMEMOWNER.PROVIDEROWNED)
			{
				throw new NotSupportedException();
			}
			byte[] array = this.values[row];
			if (binding.DestType == DBTYPE.IUNKNOWN)
			{
				void* ptr = Marshal.GetIUnknownForObject(new ReadOnlySequentialStream(array)).ToPointer();
				*(IntPtr*)destValue = ptr;
				destLength = DbLength.Pointer;
				return DBSTATUS.S_OK;
			}
			byte[] array2;
			byte* ptr2;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			DBLENGTH length = DbLength.GetLength(array);
			byte b;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.BYTES, binding.DestType, length, out destLength, (void*)((ptr2 != null) ? ptr2 : (&b)), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			array2 = null;
			return dbstatus;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004BB9 File Offset: 0x00002DB9
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004BBC File Offset: 0x00002DBC
		public override object GetObject(int row)
		{
			return this.values[row];
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004BC6 File Offset: 0x00002DC6
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04000041 RID: 65
		private readonly byte[][] values;

		// Token: 0x04000042 RID: 66
		private int rowCount;
	}
}
