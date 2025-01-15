using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000016 RID: 22
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class Column<[global::System.Runtime.CompilerServices.Nullable(2)] T> : Column
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000386C File Offset: 0x00001A6C
		protected Column(int maxRowCount)
		{
			this.values = new T[maxRowCount];
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003880 File Offset: 0x00001A80
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x060000B5 RID: 181
		protected abstract void Serialize(PageWriter writer, T[] values, int offset, int count);

		// Token: 0x060000B6 RID: 182
		protected abstract void Deserialize(PageReader reader, T[] values, int offset, int count);

		// Token: 0x060000B7 RID: 183 RVA: 0x00003888 File Offset: 0x00001A88
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003894 File Offset: 0x00001A94
		public override void AddNull()
		{
			this.AddValue(default(T));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000038B0 File Offset: 0x00001AB0
		public override void AddValue(object value)
		{
			this.AddValue((T)((object)value));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000038BE File Offset: 0x00001ABE
		public override bool TryAddValue(object value)
		{
			if (value is T)
			{
				this.AddValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000038D2 File Offset: 0x00001AD2
		public T GetValue(int row)
		{
			return this.values[row];
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000038E0 File Offset: 0x00001AE0
		public sealed override object GetObject(int row)
		{
			return this.GetValue(row);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void AddValue(T value)
		{
			T[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000391A File Offset: 0x00001B1A
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000391D File Offset: 0x00001B1D
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			this.Serialize(writer, this.values, 0, this.rowCount);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000393F File Offset: 0x00001B3F
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			this.Deserialize(reader, this.values, 0, this.rowCount);
		}

		// Token: 0x0400003C RID: 60
		private readonly T[] values;

		// Token: 0x0400003D RID: 61
		private int rowCount;
	}
}
