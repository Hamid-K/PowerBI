using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CDB RID: 7387
	internal static class ITableSourceSerializationExtensions
	{
		// Token: 0x0600B86A RID: 47210 RVA: 0x00255B98 File Offset: 0x00253D98
		public static void WriteIRelationship(this BinaryWriter writer, IRelationship relationship)
		{
			writer.WriteArray(ArrayHelpers.NewArray<int>(relationship.KeyColumnCount, new Func<int, int>(relationship.KeyColumn)), delegate(BinaryWriter w, int i)
			{
				w.WriteInt32(i);
			});
			writer.WriteArray(ArrayHelpers.NewArray<IColumnIdentity>(relationship.KeyColumnCount, new Func<int, IColumnIdentity>(relationship.OtherKeyColumn)), new Action<BinaryWriter, IColumnIdentity>(ITableSourceSerializationExtensions.WriteIColumnIdentity));
		}

		// Token: 0x0600B86B RID: 47211 RVA: 0x00255C0C File Offset: 0x00253E0C
		public static IRelationship ReadIRelationship(this BinaryReader reader)
		{
			int[] array = reader.ReadArray((BinaryReader r) => r.ReadInt32());
			IColumnIdentity[] array2 = reader.ReadArray(new Func<BinaryReader, IColumnIdentity>(ITableSourceSerializationExtensions.ReadIColumnIdentity));
			return new ITableSourceSerializationExtensions.SerializedRelationship(array, array2);
		}

		// Token: 0x0600B86C RID: 47212 RVA: 0x00255C57 File Offset: 0x00253E57
		public static void WriteIColumnIdentity(this BinaryWriter writer, IColumnIdentity columnIdentity)
		{
			writer.WriteBool(columnIdentity != null);
			if (columnIdentity != null)
			{
				writer.WriteNullableString(columnIdentity.Identity);
			}
		}

		// Token: 0x0600B86D RID: 47213 RVA: 0x00255C74 File Offset: 0x00253E74
		public static IColumnIdentity ReadIColumnIdentity(this BinaryReader reader)
		{
			IColumnIdentity columnIdentity = null;
			if (reader.ReadBool())
			{
				columnIdentity = new ITableSourceSerializationExtensions.SerializedColumnIdentity(reader.ReadNullableString());
			}
			return columnIdentity;
		}

		// Token: 0x0600B86E RID: 47214 RVA: 0x00255C98 File Offset: 0x00253E98
		public static void WriteITableSource(this BinaryWriter writer, ITableSource tableSource)
		{
			if (tableSource == null)
			{
				writer.WriteInt32(-1);
				return;
			}
			writer.WriteInt32(tableSource.ColumnCount);
			writer.WriteInt32((int)tableSource.ValueShape);
			writer.WriteArray(tableSource.KeyColumnNames.ToArray<string>(), delegate(BinaryWriter w, string k)
			{
				w.WriteString(k);
			});
			writer.WriteArray(tableSource.Relationships.ToArray<IRelationship>(), delegate(BinaryWriter w, IRelationship r)
			{
				w.WriteIRelationship(r);
			});
			IColumnIdentity[] array = ArrayHelpers.NewArray<IColumnIdentity>(tableSource.ColumnCount, new Func<int, IColumnIdentity>(tableSource.ColumnIdentity));
			writer.WriteArray(array, delegate(BinaryWriter w, IColumnIdentity i)
			{
				w.WriteIColumnIdentity(i);
			});
		}

		// Token: 0x0600B86F RID: 47215 RVA: 0x00255D68 File Offset: 0x00253F68
		public static ITableSource ReadITableSource(this BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			ValueShape valueShape = (ValueShape)reader.ReadInt32();
			string[] array = reader.ReadArray((BinaryReader r) => r.ReadString());
			IRelationship[] array2 = reader.ReadArray((BinaryReader r) => r.ReadIRelationship());
			IColumnIdentity[] array3 = reader.ReadArray((BinaryReader r) => r.ReadIColumnIdentity());
			return new ITableSourceSerializationExtensions.SerializedTableSource(valueShape, num, array, array2, array3);
		}

		// Token: 0x02001CDC RID: 7388
		private class SerializedRelationship : IRelationship
		{
			// Token: 0x0600B870 RID: 47216 RVA: 0x00255E03 File Offset: 0x00254003
			public SerializedRelationship(int[] keyColumns, IColumnIdentity[] otherKeyColumns)
			{
				this.keyColumns = keyColumns;
				this.otherKeyColumns = otherKeyColumns;
			}

			// Token: 0x17002DB1 RID: 11697
			// (get) Token: 0x0600B871 RID: 47217 RVA: 0x00255E19 File Offset: 0x00254019
			public int KeyColumnCount
			{
				get
				{
					return this.keyColumns.Length;
				}
			}

			// Token: 0x0600B872 RID: 47218 RVA: 0x00255E23 File Offset: 0x00254023
			public int KeyColumn(int index)
			{
				return this.keyColumns[index];
			}

			// Token: 0x0600B873 RID: 47219 RVA: 0x00255E2D File Offset: 0x0025402D
			public IColumnIdentity OtherKeyColumn(int index)
			{
				return this.otherKeyColumns[index];
			}

			// Token: 0x04005DCF RID: 24015
			private readonly int[] keyColumns;

			// Token: 0x04005DD0 RID: 24016
			private readonly IColumnIdentity[] otherKeyColumns;
		}

		// Token: 0x02001CDD RID: 7389
		private class SerializedColumnIdentity : IColumnIdentity
		{
			// Token: 0x0600B874 RID: 47220 RVA: 0x00255E37 File Offset: 0x00254037
			public SerializedColumnIdentity(string identity)
			{
				this.identity = identity;
			}

			// Token: 0x17002DB2 RID: 11698
			// (get) Token: 0x0600B875 RID: 47221 RVA: 0x00255E46 File Offset: 0x00254046
			public string Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x04005DD1 RID: 24017
			private readonly string identity;
		}

		// Token: 0x02001CDE RID: 7390
		private sealed class SerializedTableSource : ITableSource
		{
			// Token: 0x0600B876 RID: 47222 RVA: 0x00255E4E File Offset: 0x0025404E
			public SerializedTableSource(ValueShape valueShape, int columnCount, string[] keyColumnNames, IRelationship[] relationships, IColumnIdentity[] columnIdentities)
			{
				this.valueShape = valueShape;
				this.columnCount = columnCount;
				this.keyColumnNames = keyColumnNames;
				this.relationships = relationships;
				this.columnIdentities = columnIdentities;
			}

			// Token: 0x17002DB3 RID: 11699
			// (get) Token: 0x0600B877 RID: 47223 RVA: 0x00255E7B File Offset: 0x0025407B
			public ValueShape ValueShape
			{
				get
				{
					return this.valueShape;
				}
			}

			// Token: 0x17002DB4 RID: 11700
			// (get) Token: 0x0600B878 RID: 47224 RVA: 0x00255E83 File Offset: 0x00254083
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x17002DB5 RID: 11701
			// (get) Token: 0x0600B879 RID: 47225 RVA: 0x00255E8B File Offset: 0x0025408B
			public IEnumerable<string> KeyColumnNames
			{
				get
				{
					return this.keyColumnNames;
				}
			}

			// Token: 0x17002DB6 RID: 11702
			// (get) Token: 0x0600B87A RID: 47226 RVA: 0x00255E93 File Offset: 0x00254093
			public IEnumerable<IRelationship> Relationships
			{
				get
				{
					return this.relationships;
				}
			}

			// Token: 0x0600B87B RID: 47227 RVA: 0x00255E9B File Offset: 0x0025409B
			public IColumnIdentity ColumnIdentity(int index)
			{
				return this.columnIdentities[index];
			}

			// Token: 0x04005DD2 RID: 24018
			private readonly ValueShape valueShape;

			// Token: 0x04005DD3 RID: 24019
			private readonly int columnCount;

			// Token: 0x04005DD4 RID: 24020
			private readonly string[] keyColumnNames;

			// Token: 0x04005DD5 RID: 24021
			private readonly IRelationship[] relationships;

			// Token: 0x04005DD6 RID: 24022
			private readonly IColumnIdentity[] columnIdentities;
		}
	}
}
