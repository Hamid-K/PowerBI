using System;
using System.Data.Common;
using System.Data.OleDb;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000009 RID: 9
	internal class InfoObjectDataTypeColumn : IComputedColumn
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003818 File Offset: 0x00001A18
		public int Decimals
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000381B File Offset: 0x00001A1B
		public int Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000381E File Offset: 0x00001A1E
		public string Name
		{
			get
			{
				return "OleDbType";
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003825 File Offset: 0x00001A25
		public RfcDataType RfcDataType
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003828 File Offset: 0x00001A28
		public Type Type
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003834 File Offset: 0x00001A34
		public InfoObjectDataTypeColumn(SapBwConnection connection, int rowCount)
		{
			this.connection = connection;
			this.values = new object[rowCount];
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000384F File Offset: 0x00001A4F
		public object GetValue(DbDataReader reader, int currentIndex)
		{
			if (this.values[currentIndex] == null)
			{
				this.values[currentIndex] = this.GetValue(reader);
			}
			return this.values[currentIndex];
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003874 File Offset: 0x00001A74
		private object GetValue(DbDataReader reader)
		{
			int @int = reader.GetInt32(7);
			int int2 = reader.GetInt32(8);
			if (@int == 1 || (int2 > 43200 && int2 <= 86400))
			{
				string @string = reader.GetString(3);
				SapBwDataType sapBwDataType;
				OleDbType oleDbType;
				if (!string.IsNullOrEmpty(@string) && this.connection.TryGetInfoObjectType(@string.Trim(new char[] { '[', ']' }), out sapBwDataType) && MdxColumn.SapBwTypeToOleDbType.TryGetValue(sapBwDataType, out oleDbType))
				{
					return (int)oleDbType;
				}
			}
			return 8;
		}

		// Token: 0x04000012 RID: 18
		private const int DimensionTypeTime = 1;

		// Token: 0x04000013 RID: 19
		private readonly SapBwConnection connection;

		// Token: 0x04000014 RID: 20
		private readonly object[] values;
	}
}
