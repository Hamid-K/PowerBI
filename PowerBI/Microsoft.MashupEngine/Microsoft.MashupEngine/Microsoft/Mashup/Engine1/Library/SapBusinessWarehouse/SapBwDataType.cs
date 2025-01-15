using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004AB RID: 1195
	internal sealed class SapBwDataType
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x000738C4 File Offset: 0x00071AC4
		static SapBwDataType()
		{
			SapBwDataType.AddDataType(new SapBwDataType("ACCP", OleDbType.Integer));
			SapBwDataType.AddDataType(new SapBwDataType("CHAR", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("CLNT", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("CURR", OleDbType.Decimal));
			SapBwDataType.AddDataType(new SapBwDataType("CUKY", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("DATS", OleDbType.Variant));
			SapBwDataType.AddDataType(new SapBwDataType("DEC", OleDbType.Decimal));
			SapBwDataType.AddDataType(new SapBwDataType("FLTP", OleDbType.Double));
			SapBwDataType.AddDataType(new SapBwDataType("INT1", OleDbType.UnsignedTinyInt));
			SapBwDataType.AddDataType(new SapBwDataType("INT2", OleDbType.SmallInt));
			SapBwDataType.AddDataType(new SapBwDataType("INT4", OleDbType.Integer));
			SapBwDataType.AddDataType(new SapBwDataType("LANG", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("LCHR", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("LRAW", OleDbType.Binary));
			SapBwDataType.AddDataType(new SapBwDataType("NUMC", OleDbType.Variant));
			SapBwDataType.AddDataType(new SapBwDataType("PREC", OleDbType.SmallInt));
			SapBwDataType.AddDataType(new SapBwDataType("QUAN", OleDbType.Decimal));
			SapBwDataType.AddDataType(new SapBwDataType("RAW", OleDbType.Binary));
			SapBwDataType.AddDataType(new SapBwDataType("RAWSTRING", OleDbType.Binary));
			SapBwDataType.AddDataType(new SapBwDataType("STRING", OleDbType.VarChar));
			SapBwDataType.AddDataType(new SapBwDataType("TIMS", OleDbType.Variant));
			SapBwDataType.AddDataType(new SapBwDataType("UNIT", OleDbType.VarChar));
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00073A6F File Offset: 0x00071C6F
		private SapBwDataType(string name, OleDbType oleDbType)
		{
			this.name = name;
			this.oleDbType = oleDbType;
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00073A85 File Offset: 0x00071C85
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00073A8D File Offset: 0x00071C8D
		public OleDbType OleDbType
		{
			get
			{
				return this.oleDbType;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x00073A95 File Offset: 0x00071C95
		public TypeValue TypeValue
		{
			get
			{
				return this.OleDbType.GetTypeValue();
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00073AA2 File Offset: 0x00071CA2
		public static bool TryGetByName(string name, out SapBwDataType dataType)
		{
			return SapBwDataType.dataTypesByName.TryGetValue(name, out dataType);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00073AB0 File Offset: 0x00071CB0
		private static void AddDataType(SapBwDataType dataType)
		{
			SapBwDataType.dataTypesByName.Add(dataType.Name, dataType);
		}

		// Token: 0x04001086 RID: 4230
		private static readonly Dictionary<string, SapBwDataType> dataTypesByName = new Dictionary<string, SapBwDataType>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001087 RID: 4231
		private readonly string name;

		// Token: 0x04001088 RID: 4232
		private readonly OleDbType oleDbType;
	}
}
