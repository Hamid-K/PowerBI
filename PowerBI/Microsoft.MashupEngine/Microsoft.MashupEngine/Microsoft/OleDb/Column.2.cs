using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E60 RID: 7776
	internal abstract class Column<T> : Column
	{
		// Token: 0x0600BF6B RID: 49003 RVA: 0x0026A7E6 File Offset: 0x002689E6
		public Column(int maxRowCount)
		{
			this.values = new T[maxRowCount];
		}

		// Token: 0x17002EFB RID: 12027
		// (get) Token: 0x0600BF6C RID: 49004 RVA: 0x0026A7FA File Offset: 0x002689FA
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600BF6D RID: 49005 RVA: 0x0026A802 File Offset: 0x00268A02
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600BF6E RID: 49006 RVA: 0x0026A80C File Offset: 0x00268A0C
		public override void AddNull()
		{
			this.AddValue(default(T));
		}

		// Token: 0x0600BF6F RID: 49007 RVA: 0x0026A828 File Offset: 0x00268A28
		public override void AddValue(object value)
		{
			this.AddValue((T)((object)value));
		}

		// Token: 0x0600BF70 RID: 49008 RVA: 0x0026A836 File Offset: 0x00268A36
		public override bool TryAddValue(object value)
		{
			if (value is T)
			{
				this.AddValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BF71 RID: 49009 RVA: 0x0026A84A File Offset: 0x00268A4A
		public T GetValue(int row)
		{
			return this.values[row];
		}

		// Token: 0x0600BF72 RID: 49010 RVA: 0x0026A858 File Offset: 0x00268A58
		public sealed override object GetObject(int row)
		{
			return this.GetValue(row);
		}

		// Token: 0x0600BF73 RID: 49011 RVA: 0x0026A868 File Offset: 0x00268A68
		public void AddValue(T value)
		{
			T[] array = this.values;
			int num = this.rowCount;
			this.rowCount = num + 1;
			array[num] = value;
		}

		// Token: 0x0600BF74 RID: 49012 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600BF75 RID: 49013 RVA: 0x0026A892 File Offset: 0x00268A92
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			this.Serialize(writer, this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF76 RID: 49014 RVA: 0x0026A8B4 File Offset: 0x00268AB4
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			this.Deserialize(reader, this.values, 0, this.rowCount);
		}

		// Token: 0x0600BF77 RID: 49015
		protected abstract void Serialize(PageWriter writer, T[] values, int offset, int count);

		// Token: 0x0600BF78 RID: 49016
		protected abstract void Deserialize(PageReader reader, T[] values, int offset, int count);

		// Token: 0x0400613C RID: 24892
		private readonly T[] values;

		// Token: 0x0400613D RID: 24893
		private int rowCount;
	}
}
