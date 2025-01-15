using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000114 RID: 276
	internal struct MultiPartTableName
	{
		// Token: 0x060015BF RID: 5567 RVA: 0x0005EE28 File Offset: 0x0005D028
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0005EE55 File Offset: 0x0005D055
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0005EE7A File Offset: 0x0005D07A
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0005EE88 File Offset: 0x0005D088
		internal string ServerName
		{
			get
			{
				this.ParseMultipartName();
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0005EE91 File Offset: 0x0005D091
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0005EE9F File Offset: 0x0005D09F
		internal string CatalogName
		{
			get
			{
				this.ParseMultipartName();
				return this._catalogName;
			}
			set
			{
				this._catalogName = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x0005EEB6 File Offset: 0x0005D0B6
		internal string SchemaName
		{
			get
			{
				this.ParseMultipartName();
				return this._schemaName;
			}
			set
			{
				this._schemaName = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0005EEBF File Offset: 0x0005D0BF
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0005EECD File Offset: 0x0005D0CD
		internal string TableName
		{
			get
			{
				this.ParseMultipartName();
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		private void ParseMultipartName()
		{
			if (this._multipartName != null)
			{
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this._multipartName, "[\"", "]\"", Strings.SQL_TDSParserTableName, false);
				this._serverName = array[0];
				this._catalogName = array[1];
				this._schemaName = array[2];
				this._tableName = array[3];
				this._multipartName = null;
			}
		}

		// Token: 0x040008C6 RID: 2246
		private string _multipartName;

		// Token: 0x040008C7 RID: 2247
		private string _serverName;

		// Token: 0x040008C8 RID: 2248
		private string _catalogName;

		// Token: 0x040008C9 RID: 2249
		private string _schemaName;

		// Token: 0x040008CA RID: 2250
		private string _tableName;

		// Token: 0x040008CB RID: 2251
		internal static readonly MultiPartTableName Null = new MultiPartTableName(new string[4]);
	}
}
