using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DB RID: 219
	public sealed class NamedSet : IMetadataObject
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0002E0C5 File Offset: 0x0002C2C5
		internal NamedSet(AdomdConnection connection, DataRow namedSetRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.namedSetRow = namedSetRow;
			this.parentCube = parentCube;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0002E101 File Offset: 0x0002C301
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "SET_NAME").ToString();
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002E118 File Offset: 0x0002C318
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "DESCRIPTION").ToString();
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0002E12F File Offset: 0x0002C32F
		public string Expression
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "EXPRESSION").ToString();
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002E146 File Offset: 0x0002C346
		public string Caption
		{
			get
			{
				if (!this.namedSetRow.Table.Columns.Contains("SET_CAPTION"))
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				return AdomdUtils.GetProperty(this.namedSetRow, "SET_CAPTION").ToString();
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0002E184 File Offset: 0x0002C384
		public string DisplayFolder
		{
			get
			{
				if (!this.namedSetRow.Table.Columns.Contains("SET_DISPLAY_FOLDER"))
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				return AdomdUtils.GetProperty(this.namedSetRow, "SET_DISPLAY_FOLDER").ToString();
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002E1C2 File Offset: 0x0002C3C2
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002E1CA File Offset: 0x0002C3CA
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.namedSetRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002E1F4 File Offset: 0x0002C3F4
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002E204 File Offset: 0x0002C404
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002E211 File Offset: 0x0002C411
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002E219 File Offset: 0x0002C419
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(NamedSet);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002E225 File Offset: 0x0002C425
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002E248 File Offset: 0x0002C448
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002E256 File Offset: 0x0002C456
		public static bool operator ==(NamedSet o1, NamedSet o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002E25F File Offset: 0x0002C45F
		public static bool operator !=(NamedSet o1, NamedSet o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040007D1 RID: 2001
		private DataRow namedSetRow;

		// Token: 0x040007D2 RID: 2002
		private CubeDef parentCube;

		// Token: 0x040007D3 RID: 2003
		private AdomdConnection connection;

		// Token: 0x040007D4 RID: 2004
		private PropertyCollection propertiesCollection;

		// Token: 0x040007D5 RID: 2005
		private string catalog;

		// Token: 0x040007D6 RID: 2006
		private string sessionId;

		// Token: 0x040007D7 RID: 2007
		private int hashCode;

		// Token: 0x040007D8 RID: 2008
		private bool hashCodeCalculated;

		// Token: 0x040007D9 RID: 2009
		internal const string namedsetNameColumn = "SET_NAME";

		// Token: 0x040007DA RID: 2010
		private const string descriptionColumn = "DESCRIPTION";

		// Token: 0x040007DB RID: 2011
		private const string expressionColumn = "EXPRESSION";

		// Token: 0x040007DC RID: 2012
		private const string captionColumn = "SET_CAPTION";

		// Token: 0x040007DD RID: 2013
		private const string displayFolderColumn = "SET_DISPLAY_FOLDER";
	}
}
