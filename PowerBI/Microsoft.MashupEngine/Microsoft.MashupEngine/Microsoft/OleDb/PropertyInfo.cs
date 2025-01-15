using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F21 RID: 7969
	public class PropertyInfo
	{
		// Token: 0x0600C32D RID: 49965 RVA: 0x00271B8D File Offset: 0x0026FD8D
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, int defaultValue, bool readOnly = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.I4, PropertyInfo.GetFlags(propertyGroup, readOnly))
		{
		}

		// Token: 0x0600C32E RID: 49966 RVA: 0x00271BA8 File Offset: 0x0026FDA8
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, bool defaultValue, bool readOnly = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.BOOL, PropertyInfo.GetFlags(propertyGroup, readOnly))
		{
		}

		// Token: 0x0600C32F RID: 49967 RVA: 0x00271BC4 File Offset: 0x0026FDC4
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, string defaultValue, bool readOnly = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.BSTR, PropertyInfo.GetFlags(propertyGroup, readOnly))
		{
		}

		// Token: 0x0600C330 RID: 49968 RVA: 0x00271BDA File Offset: 0x0026FDDA
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, object defaultValue, VARTYPE type, DBPROPFLAGS flags)
		{
			this.propertyGroup = propertyGroup;
			this.description = description;
			this.propertyID = propertyID;
			this.type = type;
			this.flags = flags;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17002FA7 RID: 12199
		// (get) Token: 0x0600C331 RID: 49969 RVA: 0x00271C0F File Offset: 0x0026FE0F
		public Guid Group
		{
			get
			{
				return this.propertyGroup;
			}
		}

		// Token: 0x17002FA8 RID: 12200
		// (get) Token: 0x0600C332 RID: 49970 RVA: 0x00271C17 File Offset: 0x0026FE17
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17002FA9 RID: 12201
		// (get) Token: 0x0600C333 RID: 49971 RVA: 0x00271C1F File Offset: 0x0026FE1F
		public DBPROPID ID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x17002FAA RID: 12202
		// (get) Token: 0x0600C334 RID: 49972 RVA: 0x00271C27 File Offset: 0x0026FE27
		public VARTYPE Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002FAB RID: 12203
		// (get) Token: 0x0600C335 RID: 49973 RVA: 0x00271C2F File Offset: 0x0026FE2F
		public DBPROPFLAGS Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17002FAC RID: 12204
		// (get) Token: 0x0600C336 RID: 49974 RVA: 0x00271C37 File Offset: 0x0026FE37
		public object Default
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0600C337 RID: 49975 RVA: 0x00271C40 File Offset: 0x0026FE40
		private static DBPROPFLAGS GetFlags(Guid guid, bool readOnly)
		{
			DBPROPFLAGS dbpropflags = PropertyInfo.GetFlags(guid) | DBPROPFLAGS.READ;
			if (!readOnly)
			{
				dbpropflags |= DBPROPFLAGS.WRITE;
			}
			return dbpropflags;
		}

		// Token: 0x0600C338 RID: 49976 RVA: 0x00271C68 File Offset: 0x0026FE68
		private static DBPROPFLAGS GetFlags(Guid group)
		{
			if (group == DBPROPGROUP.Column)
			{
				return DBPROPFLAGS.COLUMN;
			}
			if (group == DBPROPGROUP.DataSource)
			{
				return DBPROPFLAGS.DATASOURCE;
			}
			if (group == DBPROPGROUP.DataSourceInfo)
			{
				return DBPROPFLAGS.DATASOURCEINFO;
			}
			if (group == DBPROPGROUP.DBInit)
			{
				return DBPROPFLAGS.DBINIT;
			}
			if (group == DBPROPGROUP.Index)
			{
				return DBPROPFLAGS.INDEX;
			}
			if (group == DBPROPGROUP.Rowset)
			{
				return DBPROPFLAGS.ROWSET;
			}
			if (group == DBPROPGROUP.Table)
			{
				return DBPROPFLAGS.TABLE;
			}
			if (group == DBPROPGROUP.Session)
			{
				return DBPROPFLAGS.SESSION;
			}
			return DBPROPFLAGS.NOTSUPPORTED;
		}

		// Token: 0x0400646E RID: 25710
		private Guid propertyGroup;

		// Token: 0x0400646F RID: 25711
		private string description;

		// Token: 0x04006470 RID: 25712
		private DBPROPID propertyID;

		// Token: 0x04006471 RID: 25713
		private VARTYPE type;

		// Token: 0x04006472 RID: 25714
		private DBPROPFLAGS flags;

		// Token: 0x04006473 RID: 25715
		private object defaultValue;
	}
}
