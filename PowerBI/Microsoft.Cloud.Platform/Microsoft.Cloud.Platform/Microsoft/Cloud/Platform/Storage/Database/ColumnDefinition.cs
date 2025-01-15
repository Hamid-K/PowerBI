using System;
using System.Data;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200001D RID: 29
	public sealed class ColumnDefinition
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003C70 File Offset: 0x00001E70
		public ColumnDefinition(string name, int ord, int length, Type type, bool isIdColumn)
		{
			this.Name = name;
			this.Ordinal = ord;
			this.Length = length;
			this.Type = type;
			this.IsIdentityColumn = isIdColumn;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C9D File Offset: 0x00001E9D
		public ColumnDefinition(string name, int ord, int length, Type type)
			: this(name, ord, length, type, false)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003CAB File Offset: 0x00001EAB
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public string Name { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003CBC File Offset: 0x00001EBC
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public int Ordinal { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003CCD File Offset: 0x00001ECD
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public int Length { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003CDE File Offset: 0x00001EDE
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003CE6 File Offset: 0x00001EE6
		public Type Type { get; private set; }

		// Token: 0x06000098 RID: 152 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public DataColumn GetDataColumn()
		{
			DataColumn dataColumn = new DataColumn(this.Name, this.Type);
			if (this.Type.Equals(typeof(string)) && this.Length != 0)
			{
				dataColumn.MaxLength = this.Length;
			}
			if (this.IsIdentityColumn)
			{
				dataColumn.AutoIncrement = true;
			}
			return dataColumn;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D4A File Offset: 0x00001F4A
		public static ColumnDefinition CreateIdentityColumn(string name, int ordinal)
		{
			return new ColumnDefinition(name, ordinal, 0, typeof(long), true);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D5F File Offset: 0x00001F5F
		public static ColumnDefinition Create<S>(string name, int ordinal) where S : struct
		{
			return new ColumnDefinition(name, ordinal, 0, typeof(S));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D73 File Offset: 0x00001F73
		public static ColumnDefinition CreateString(string name, int ordinal, int length)
		{
			return new ColumnDefinition(name, ordinal, length, typeof(string));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D87 File Offset: 0x00001F87
		public static ColumnDefinition CreateBinary(string name, int ordinal)
		{
			return new ColumnDefinition(name, ordinal, 0, typeof(byte[]));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003D9B File Offset: 0x00001F9B
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003DA3 File Offset: 0x00001FA3
		public bool IsIdentityColumn { get; private set; }
	}
}
