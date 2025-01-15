using System;
using System.Runtime.InteropServices;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E77 RID: 7799
	internal class BinaryColumn : Column
	{
		// Token: 0x0600C035 RID: 49205 RVA: 0x0026B8FD File Offset: 0x00269AFD
		public BinaryColumn(int maxRowCount)
		{
			this.values = new byte[maxRowCount][];
		}

		// Token: 0x17002F14 RID: 12052
		// (get) Token: 0x0600C036 RID: 49206 RVA: 0x002435AE File Offset: 0x002417AE
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Binary;
			}
		}

		// Token: 0x17002F15 RID: 12053
		// (get) Token: 0x0600C037 RID: 49207 RVA: 0x0026B911 File Offset: 0x00269B11
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600C038 RID: 49208 RVA: 0x0026B919 File Offset: 0x00269B19
		public override void Clear()
		{
			Array.Clear(this.values, 0, this.rowCount);
			this.rowCount = 0;
		}

		// Token: 0x0600C039 RID: 49209 RVA: 0x0026B934 File Offset: 0x00269B34
		public override void AddValue(object value)
		{
			base.CheckNull(value);
			byte[][] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = (byte[])value;
		}

		// Token: 0x0600C03A RID: 49210 RVA: 0x0026B968 File Offset: 0x00269B68
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

		// Token: 0x0600C03B RID: 49211 RVA: 0x0026B9B8 File Offset: 0x00269BB8
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

		// Token: 0x0600C03C RID: 49212 RVA: 0x0026B9F0 File Offset: 0x00269BF0
		public override void AddNull()
		{
			byte[][] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = new byte[0];
		}

		// Token: 0x0600C03D RID: 49213 RVA: 0x0026BA1C File Offset: 0x00269C1C
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
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
				destLength = DbLength.GetLength(array);
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(array, binding, destValue, out destLength);
		}

		// Token: 0x0600C03E RID: 49214 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600C03F RID: 49215 RVA: 0x0026BA7E File Offset: 0x00269C7E
		public override object GetObject(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600C040 RID: 49216 RVA: 0x0026BA88 File Offset: 0x00269C88
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteArray(this.values, 0, this.rowCount);
		}

		// Token: 0x0600C041 RID: 49217 RVA: 0x0026BAA9 File Offset: 0x00269CA9
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			reader.ReadArray(this.values, 0, this.rowCount);
		}

		// Token: 0x04006141 RID: 24897
		private readonly byte[][] values;

		// Token: 0x04006142 RID: 24898
		private int rowCount;
	}
}
