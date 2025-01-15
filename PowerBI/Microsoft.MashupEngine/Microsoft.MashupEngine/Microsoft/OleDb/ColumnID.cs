using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E81 RID: 7809
	public class ColumnID
	{
		// Token: 0x0600C0EB RID: 49387 RVA: 0x0026CE39 File Offset: 0x0026B039
		public ColumnID(string name)
			: this(Guid.Empty, name)
		{
		}

		// Token: 0x0600C0EC RID: 49388 RVA: 0x0026CE47 File Offset: 0x0026B047
		public ColumnID(Guid guid)
			: this(guid, string.Empty)
		{
		}

		// Token: 0x0600C0ED RID: 49389 RVA: 0x0026CE55 File Offset: 0x0026B055
		public ColumnID(Guid guid, string name)
			: this(guid, name, (DBPROPID)0U)
		{
		}

		// Token: 0x0600C0EE RID: 49390 RVA: 0x0026CE60 File Offset: 0x0026B060
		public ColumnID(DBPROPID propertyID)
			: this(Guid.Empty, string.Empty, propertyID)
		{
		}

		// Token: 0x0600C0EF RID: 49391 RVA: 0x0026CE73 File Offset: 0x0026B073
		public ColumnID(Guid guid, DBPROPID propertyID)
			: this(guid, string.Empty, propertyID)
		{
		}

		// Token: 0x0600C0F0 RID: 49392 RVA: 0x0026CE82 File Offset: 0x0026B082
		private ColumnID(Guid guid, string name, DBPROPID propertyID)
		{
			this.name = name;
			this.guid = guid;
			this.propertyID = propertyID;
		}

		// Token: 0x17002F24 RID: 12068
		// (get) Token: 0x0600C0F1 RID: 49393 RVA: 0x0026CE9F File Offset: 0x0026B09F
		public bool HasGuid
		{
			get
			{
				return this.guid != Guid.Empty;
			}
		}

		// Token: 0x17002F25 RID: 12069
		// (get) Token: 0x0600C0F2 RID: 49394 RVA: 0x0026CEB1 File Offset: 0x0026B0B1
		public bool HasName
		{
			get
			{
				return this.name.Length != 0;
			}
		}

		// Token: 0x17002F26 RID: 12070
		// (get) Token: 0x0600C0F3 RID: 49395 RVA: 0x0026CEC1 File Offset: 0x0026B0C1
		public bool HasPropertyID
		{
			get
			{
				return this.propertyID > (DBPROPID)0U;
			}
		}

		// Token: 0x17002F27 RID: 12071
		// (get) Token: 0x0600C0F4 RID: 49396 RVA: 0x0026CECC File Offset: 0x0026B0CC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002F28 RID: 12072
		// (get) Token: 0x0600C0F5 RID: 49397 RVA: 0x0026CED4 File Offset: 0x0026B0D4
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17002F29 RID: 12073
		// (get) Token: 0x0600C0F6 RID: 49398 RVA: 0x0026CEDC File Offset: 0x0026B0DC
		public DBPROPID PropertyID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x0400615B RID: 24923
		private readonly string name;

		// Token: 0x0400615C RID: 24924
		private readonly Guid guid;

		// Token: 0x0400615D RID: 24925
		private readonly DBPROPID propertyID;
	}
}
