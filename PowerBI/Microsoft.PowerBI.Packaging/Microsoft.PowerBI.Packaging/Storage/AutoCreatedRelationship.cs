using System;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003C RID: 60
	public sealed class AutoCreatedRelationship : IBinarySerializable, IEquatable<AutoCreatedRelationship>
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00006489 File Offset: 0x00004689
		public AutoCreatedRelationship()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006491 File Offset: 0x00004691
		public AutoCreatedRelationship(string fromTable, string fromColumn, string toTable, string toColumn)
		{
			this.FromTable = fromTable;
			this.FromColumn = fromColumn;
			this.ToTable = toTable;
			this.ToColumn = toColumn;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000064B6 File Offset: 0x000046B6
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000064BE File Offset: 0x000046BE
		public string FromTable { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000064C7 File Offset: 0x000046C7
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000064CF File Offset: 0x000046CF
		public string FromColumn { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000064D8 File Offset: 0x000046D8
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x000064E0 File Offset: 0x000046E0
		public string ToTable { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000064E9 File Offset: 0x000046E9
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000064F1 File Offset: 0x000046F1
		public string ToColumn { get; set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x000064FA File Offset: 0x000046FA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AutoCreatedRelationship);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006508 File Offset: 0x00004708
		public override int GetHashCode()
		{
			return this.FromTable.GetHashCode() ^ this.FromColumn.GetHashCode() ^ this.ToTable.GetHashCode() ^ this.ToColumn.GetHashCode();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000653C File Offset: 0x0000473C
		public bool Equals(AutoCreatedRelationship other)
		{
			return (other != null && this.FromTable == other.FromTable && this.FromColumn == other.FromColumn && this.ToTable == other.ToTable && this.ToColumn == other.ToColumn) || (this.FromTable == other.ToTable && this.FromColumn == other.ToColumn && this.ToTable == other.FromTable && this.ToColumn == other.FromColumn);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000065E6 File Offset: 0x000047E6
		public void Deserialize(BinarySerializationReader reader)
		{
			reader.ReadInt();
			this.FromTable = reader.ReadString();
			this.FromColumn = reader.ReadString();
			this.ToTable = reader.ReadString();
			this.ToColumn = reader.ReadString();
		}
	}
}
