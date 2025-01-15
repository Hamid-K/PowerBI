using System;
using System.IO;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000157 RID: 343
	public class SchemaColumn
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00009427 File Offset: 0x00007627
		public SchemaColumn(string name)
		{
			this.name = name;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00009436 File Offset: 0x00007636
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000943E File Offset: 0x0000763E
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00009446 File Offset: 0x00007646
		public Type DataType { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000944F File Offset: 0x0000764F
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00009457 File Offset: 0x00007657
		public int? Ordinal { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00009460 File Offset: 0x00007660
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00009468 File Offset: 0x00007668
		public bool Nullable { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00009471 File Offset: 0x00007671
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00009479 File Offset: 0x00007679
		public bool IsKey { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00009482 File Offset: 0x00007682
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000948A File Offset: 0x0000768A
		public string DataTypeName { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00009493 File Offset: 0x00007693
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000949B File Offset: 0x0000769B
		public long? ColumnSize { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000094A4 File Offset: 0x000076A4
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x000094AC File Offset: 0x000076AC
		public int? NumericBase { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000094B5 File Offset: 0x000076B5
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x000094BD File Offset: 0x000076BD
		public int? NumericPrecision { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000094C6 File Offset: 0x000076C6
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x000094CE File Offset: 0x000076CE
		public int? NumericScale { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000094D7 File Offset: 0x000076D7
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000094DF File Offset: 0x000076DF
		public bool? IsUnsigned { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000094E8 File Offset: 0x000076E8
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000094F0 File Offset: 0x000076F0
		public int? ProviderType { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000094F9 File Offset: 0x000076F9
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00009501 File Offset: 0x00007701
		public Type ProviderSpecificDataType { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000950A File Offset: 0x0000770A
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00009512 File Offset: 0x00007712
		public TableSchema ColumnSchema { get; set; }

		// Token: 0x06000614 RID: 1556 RVA: 0x0000951C File Offset: 0x0000771C
		public SchemaColumn Clone(string newName = null)
		{
			SchemaColumn schemaColumn = new SchemaColumn(newName ?? this.name);
			schemaColumn.DataType = this.DataType;
			schemaColumn.Ordinal = this.Ordinal;
			schemaColumn.Nullable = this.Nullable;
			schemaColumn.IsKey = this.IsKey;
			schemaColumn.DataTypeName = this.DataTypeName;
			schemaColumn.ColumnSize = this.ColumnSize;
			schemaColumn.NumericBase = this.NumericBase;
			schemaColumn.NumericPrecision = this.NumericPrecision;
			schemaColumn.NumericScale = this.NumericScale;
			schemaColumn.IsUnsigned = this.IsUnsigned;
			schemaColumn.ProviderType = this.ProviderType;
			schemaColumn.ProviderSpecificDataType = this.ProviderSpecificDataType;
			TableSchema columnSchema = this.ColumnSchema;
			schemaColumn.ColumnSchema = ((columnSchema != null) ? columnSchema.Copy() : null);
			return schemaColumn;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000095E4 File Offset: 0x000077E4
		internal void Serialize(BinaryWriter writer)
		{
			writer.Write(this.name);
			TypeSerialization.Serialize(writer, this.DataType);
			SchemaColumn.Serialize(writer, this.Ordinal);
			writer.Write(this.Nullable);
			writer.Write(this.IsKey);
			SchemaColumn.Serialize(writer, this.DataTypeName);
			SchemaColumn.Serialize(writer, this.ColumnSize);
			SchemaColumn.Serialize(writer, this.NumericBase);
			SchemaColumn.Serialize(writer, this.NumericPrecision);
			SchemaColumn.Serialize(writer, this.NumericScale);
			SchemaColumn.Serialize(writer, this.IsUnsigned);
			SchemaColumn.Serialize(writer, this.ProviderType);
			TypeSerialization.Serialize(writer, this.ProviderSpecificDataType);
			if (this.ColumnSchema != null)
			{
				writer.Write(true);
				this.ColumnSchema.Serialize(writer);
				return;
			}
			writer.Write(false);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000096B0 File Offset: 0x000078B0
		private static void Serialize(BinaryWriter writer, int? value)
		{
			if (value != null)
			{
				writer.Write(true);
				writer.Write(value.Value);
				return;
			}
			writer.Write(false);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000096D7 File Offset: 0x000078D7
		private static void Serialize(BinaryWriter writer, long? value)
		{
			if (value != null)
			{
				writer.Write(true);
				writer.Write(value.Value);
				return;
			}
			writer.Write(false);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000096FE File Offset: 0x000078FE
		private static void Serialize(BinaryWriter writer, bool? value)
		{
			if (value != null)
			{
				writer.Write((value.Value > false) ? 1 : 0);
				return;
			}
			writer.Write(byte.MaxValue);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00009726 File Offset: 0x00007926
		private static void Serialize(BinaryWriter writer, string value)
		{
			if (value != null)
			{
				writer.Write(true);
				writer.Write(value);
				return;
			}
			writer.Write(false);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00009744 File Offset: 0x00007944
		internal static SchemaColumn Deserialize(BinaryReader reader)
		{
			SchemaColumn schemaColumn = new SchemaColumn(reader.ReadString());
			schemaColumn.DataType = TypeSerialization.Deserialize(reader);
			schemaColumn.Ordinal = SchemaColumn.DeserializeInt32(reader);
			schemaColumn.Nullable = reader.ReadBoolean();
			schemaColumn.IsKey = reader.ReadBoolean();
			schemaColumn.DataTypeName = SchemaColumn.DeserializeString(reader);
			schemaColumn.ColumnSize = SchemaColumn.DeserializeInt64(reader);
			schemaColumn.NumericBase = SchemaColumn.DeserializeInt32(reader);
			schemaColumn.NumericPrecision = SchemaColumn.DeserializeInt32(reader);
			schemaColumn.NumericScale = SchemaColumn.DeserializeInt32(reader);
			schemaColumn.IsUnsigned = SchemaColumn.DeserializeBool(reader);
			schemaColumn.ProviderType = SchemaColumn.DeserializeInt32(reader);
			schemaColumn.ProviderSpecificDataType = TypeSerialization.Deserialize(reader);
			if (reader.ReadBoolean())
			{
				schemaColumn.ColumnSchema = TableSchema.Deserialize(reader);
			}
			return schemaColumn;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00009804 File Offset: 0x00007A04
		private static int? DeserializeInt32(BinaryReader reader)
		{
			if (reader.ReadBoolean())
			{
				return new int?(reader.ReadInt32());
			}
			return null;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00009830 File Offset: 0x00007A30
		private static long? DeserializeInt64(BinaryReader reader)
		{
			if (reader.ReadBoolean())
			{
				return new long?(reader.ReadInt64());
			}
			return null;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000985C File Offset: 0x00007A5C
		private static bool? DeserializeBool(BinaryReader reader)
		{
			byte b = reader.ReadByte();
			if (b == 0)
			{
				return new bool?(false);
			}
			if (b != 1)
			{
				return null;
			}
			return new bool?(true);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00009890 File Offset: 0x00007A90
		private static string DeserializeString(BinaryReader reader)
		{
			if (reader.ReadBoolean())
			{
				return reader.ReadString();
			}
			return null;
		}

		// Token: 0x040003D9 RID: 985
		private readonly string name;
	}
}
