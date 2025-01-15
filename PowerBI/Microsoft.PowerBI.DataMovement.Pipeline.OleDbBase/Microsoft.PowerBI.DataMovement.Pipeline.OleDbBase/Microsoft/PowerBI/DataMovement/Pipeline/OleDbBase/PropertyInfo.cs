using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BC RID: 188
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PropertyInfo
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000957C File Offset: 0x0000777C
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, short defaultValue, bool readOnly = true, bool required = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.I2, PropertyInfo.GetFlags(propertyGroup, readOnly, required))
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000095A4 File Offset: 0x000077A4
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, ushort defaultValue, bool readOnly = true, bool required = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.UI2, PropertyInfo.GetFlags(propertyGroup, readOnly, required))
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000095D0 File Offset: 0x000077D0
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, int defaultValue, bool readOnly = true, bool required = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.I4, PropertyInfo.GetFlags(propertyGroup, readOnly, required))
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000095F8 File Offset: 0x000077F8
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, bool defaultValue, bool readOnly = true, bool required = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.BOOL, PropertyInfo.GetFlags(propertyGroup, readOnly, required))
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009624 File Offset: 0x00007824
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, string defaultValue, bool readOnly = true, bool required = true)
			: this(propertyGroup, propertyID, description, defaultValue, VARTYPE.BSTR, PropertyInfo.GetFlags(propertyGroup, readOnly, required))
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009647 File Offset: 0x00007847
		public PropertyInfo(Guid propertyGroup, DBPROPID propertyID, string description, object defaultValue, VARTYPE type, DBPROPFLAGS flags)
		{
			this.propertyGroup = propertyGroup;
			this.description = description;
			this.propertyID = propertyID;
			this.type = type;
			this.flags = flags;
			this.defaultValue = defaultValue;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000967C File Offset: 0x0000787C
		public Guid Group
		{
			get
			{
				return this.propertyGroup;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009684 File Offset: 0x00007884
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000968C File Offset: 0x0000788C
		public DBPROPID ID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00009694 File Offset: 0x00007894
		public VARTYPE Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000969C File Offset: 0x0000789C
		public bool IsWritable
		{
			get
			{
				return (this.flags & DBPROPFLAGS.WRITE) == DBPROPFLAGS.WRITE;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000096B1 File Offset: 0x000078B1
		public DBPROPFLAGS Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000096B9 File Offset: 0x000078B9
		public object Default
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000096C4 File Offset: 0x000078C4
		private static DBPROPFLAGS GetFlags(Guid guid, bool readOnly, bool required)
		{
			DBPROPFLAGS dbpropflags = PropertyInfo.GetFlags(guid) | DBPROPFLAGS.READ;
			if (!readOnly)
			{
				dbpropflags |= DBPROPFLAGS.WRITE;
			}
			if (required)
			{
				dbpropflags |= DBPROPFLAGS.REQUIRED;
			}
			return dbpropflags;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000096F8 File Offset: 0x000078F8
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

		// Token: 0x0400036E RID: 878
		private Guid propertyGroup;

		// Token: 0x0400036F RID: 879
		private string description;

		// Token: 0x04000370 RID: 880
		private DBPROPID propertyID;

		// Token: 0x04000371 RID: 881
		private VARTYPE type;

		// Token: 0x04000372 RID: 882
		private DBPROPFLAGS flags;

		// Token: 0x04000373 RID: 883
		private object defaultValue;
	}
}
