using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200113F RID: 4415
	internal class StreamReaderDataReader : DbDataReaderWithTableSchema
	{
		// Token: 0x06007390 RID: 29584 RVA: 0x0018DCFB File Offset: 0x0018BEFB
		public StreamReaderDataReader(Stream stream)
		{
			this.reader = new BinaryReader(stream);
			this.tag = ObjectTag.None;
			this.ReadStartTable();
		}

		// Token: 0x06007391 RID: 29585 RVA: 0x0018DD1C File Offset: 0x0018BF1C
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return base.GetDbDataReader(ordinal);
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x0018DD25 File Offset: 0x0018BF25
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			return base.GetProviderSpecificFieldType(ordinal);
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0018DD2E File Offset: 0x0018BF2E
		public override object GetProviderSpecificValue(int ordinal)
		{
			return base.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x0018DD37 File Offset: 0x0018BF37
		public override void Close()
		{
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x000091AE File Offset: 0x000073AE
		public override int Depth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x0018DD53 File Offset: 0x0018BF53
		public override int FieldCount
		{
			get
			{
				return this.fields.Length;
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06007397 RID: 29591 RVA: 0x0018DD5D File Offset: 0x0018BF5D
		public override TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x0018DD65 File Offset: 0x0018BF65
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.values[ordinal];
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x0018DD74 File Offset: 0x0018BF74
		public override byte GetByte(int ordinal)
		{
			return (byte)this.values[ordinal];
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x000091AE File Offset: 0x000073AE
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600739B RID: 29595 RVA: 0x0018DD83 File Offset: 0x0018BF83
		public override char GetChar(int ordinal)
		{
			return (char)this.values[ordinal];
		}

		// Token: 0x0600739C RID: 29596 RVA: 0x000091AE File Offset: 0x000073AE
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x000091AE File Offset: 0x000073AE
		public override string GetDataTypeName(int ordinal)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x0018DD92 File Offset: 0x0018BF92
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.values[ordinal];
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x0018DDA1 File Offset: 0x0018BFA1
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.values[ordinal];
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x0018DDB0 File Offset: 0x0018BFB0
		public override double GetDouble(int ordinal)
		{
			return (double)this.values[ordinal];
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x000091AE File Offset: 0x000073AE
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060073A2 RID: 29602 RVA: 0x0018DDBF File Offset: 0x0018BFBF
		public override Type GetFieldType(int ordinal)
		{
			return this.values[ordinal].GetType();
		}

		// Token: 0x060073A3 RID: 29603 RVA: 0x0018DDCE File Offset: 0x0018BFCE
		public override float GetFloat(int ordinal)
		{
			return (float)this.values[ordinal];
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x0018DDDD File Offset: 0x0018BFDD
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.values[ordinal];
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x0018DDEC File Offset: 0x0018BFEC
		public override short GetInt16(int ordinal)
		{
			return (short)this.values[ordinal];
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x0018DDFB File Offset: 0x0018BFFB
		public override int GetInt32(int ordinal)
		{
			return (int)this.values[ordinal];
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x0018DE0A File Offset: 0x0018C00A
		public override long GetInt64(int ordinal)
		{
			return (long)this.values[ordinal];
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x0018DE19 File Offset: 0x0018C019
		public override string GetName(int ordinal)
		{
			return this.fields[ordinal];
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x0018DE23 File Offset: 0x0018C023
		public override int GetOrdinal(string name)
		{
			return this.ordinals[name];
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x0018DE31 File Offset: 0x0018C031
		public override string GetString(int ordinal)
		{
			return (string)this.values[ordinal];
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x0018DE40 File Offset: 0x0018C040
		public override object GetValue(int ordinal)
		{
			Exception ex = this.values[ordinal] as Exception;
			if (ex != null)
			{
				throw ex;
			}
			return this.values[ordinal];
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0018DE68 File Offset: 0x0018C068
		public override int GetValues(object[] values)
		{
			int num = Math.Min(this.values.Length, values.Length);
			Array.Copy(this.values, values, num);
			return num;
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x060073AD RID: 29613 RVA: 0x0018DE94 File Offset: 0x0018C094
		public override bool HasRows
		{
			get
			{
				return this.hasRows;
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x060073AE RID: 29614 RVA: 0x0018DE9C File Offset: 0x0018C09C
		public override bool IsClosed
		{
			get
			{
				return this.reader == null;
			}
		}

		// Token: 0x060073AF RID: 29615 RVA: 0x0018DEA7 File Offset: 0x0018C0A7
		public override bool IsDBNull(int ordinal)
		{
			return this.values[ordinal] == DBNull.Value;
		}

		// Token: 0x060073B0 RID: 29616 RVA: 0x0018DEB8 File Offset: 0x0018C0B8
		public override bool NextResult()
		{
			while (this.Read())
			{
			}
			if (this.PeekTag() == ObjectTag.Table)
			{
				this.ReadStartTable();
				return true;
			}
			return false;
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x0018DED4 File Offset: 0x0018C0D4
		public override bool Read()
		{
			ObjectTag objectTag = this.PeekTag();
			if (objectTag == ObjectTag.Table || objectTag == ObjectTag.Eof)
			{
				return false;
			}
			for (int i = 0; i < this.values.Length; i++)
			{
				this.values[i] = this.reader.ReadObject(this.ReadTag());
			}
			return true;
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x0017811C File Offset: 0x0017631C
		public override int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17002041 RID: 8257
		public override object this[string name]
		{
			get
			{
				return this.values[this.ordinals[name]];
			}
		}

		// Token: 0x17002042 RID: 8258
		public override object this[int ordinal]
		{
			get
			{
				return this.values[ordinal];
			}
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x0018DF40 File Offset: 0x0018C140
		private void ReadStartTable()
		{
			if (this.ReadTag() != ObjectTag.Table)
			{
				throw new InvalidOperationException();
			}
			int num = this.reader.ReadInt32();
			this.fields = new string[num];
			this.ordinals = new Dictionary<string, int>(num);
			this.values = new object[num];
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				this.fields[i] = text;
				if (!this.ordinals.ContainsKey(text))
				{
					this.ordinals.Add(text, i);
				}
			}
			this.schema = this.ReadSchemaTable();
			this.hasRows = this.PeekTag() != ObjectTag.Table;
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x0018DFE8 File Offset: 0x0018C1E8
		private ObjectTag ReadTag()
		{
			ObjectTag objectTag = this.tag;
			if (objectTag == ObjectTag.None)
			{
				objectTag = this.reader.ReadObjectTag();
			}
			else
			{
				this.tag = ObjectTag.None;
			}
			return objectTag;
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x0018E015 File Offset: 0x0018C215
		private ObjectTag PeekTag()
		{
			if (this.tag == ObjectTag.None)
			{
				this.tag = this.reader.ReadObjectTag();
			}
			return this.tag;
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x0018E038 File Offset: 0x0018C238
		private TableSchema ReadSchemaTable()
		{
			int num = this.reader.ReadInt32();
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumnCollection columns = dataTable.Columns;
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				Type type = (Type)this.reader.ReadObject(this.ReadTag());
				columns.Add(text, type);
			}
			int num2 = this.reader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < num; k++)
				{
					dataRow[k] = this.reader.ReadObject(this.ReadTag());
				}
				dataTable.Rows.Add(dataRow);
			}
			return TableSchema.FromDataTable(dataTable);
		}

		// Token: 0x04003FAE RID: 16302
		private BinaryReader reader;

		// Token: 0x04003FAF RID: 16303
		private ObjectTag tag;

		// Token: 0x04003FB0 RID: 16304
		private string[] fields;

		// Token: 0x04003FB1 RID: 16305
		private Dictionary<string, int> ordinals;

		// Token: 0x04003FB2 RID: 16306
		private TableSchema schema;

		// Token: 0x04003FB3 RID: 16307
		private bool hasRows;

		// Token: 0x04003FB4 RID: 16308
		private object[] values;
	}
}
