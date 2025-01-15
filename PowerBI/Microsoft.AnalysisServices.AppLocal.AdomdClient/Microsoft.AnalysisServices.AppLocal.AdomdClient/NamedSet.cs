using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DB RID: 219
	public sealed class NamedSet : IMetadataObject
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0002E3F5 File Offset: 0x0002C5F5
		internal NamedSet(AdomdConnection connection, DataRow namedSetRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.namedSetRow = namedSetRow;
			this.parentCube = parentCube;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002E429 File Offset: 0x0002C629
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002E431 File Offset: 0x0002C631
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "SET_NAME").ToString();
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002E448 File Offset: 0x0002C648
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "DESCRIPTION").ToString();
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002E45F File Offset: 0x0002C65F
		public string Expression
		{
			get
			{
				return AdomdUtils.GetProperty(this.namedSetRow, "EXPRESSION").ToString();
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002E476 File Offset: 0x0002C676
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

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002E4B4 File Offset: 0x0002C6B4
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002E4F2 File Offset: 0x0002C6F2
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002E4FA File Offset: 0x0002C6FA
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002E51C File Offset: 0x0002C71C
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002E524 File Offset: 0x0002C724
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0002E52C File Offset: 0x0002C72C
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002E534 File Offset: 0x0002C734
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0002E541 File Offset: 0x0002C741
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002E549 File Offset: 0x0002C749
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(NamedSet);
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002E555 File Offset: 0x0002C755
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002E578 File Offset: 0x0002C778
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002E586 File Offset: 0x0002C786
		public static bool operator ==(NamedSet o1, NamedSet o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002E58F File Offset: 0x0002C78F
		public static bool operator !=(NamedSet o1, NamedSet o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040007DE RID: 2014
		private DataRow namedSetRow;

		// Token: 0x040007DF RID: 2015
		private CubeDef parentCube;

		// Token: 0x040007E0 RID: 2016
		private AdomdConnection connection;

		// Token: 0x040007E1 RID: 2017
		private PropertyCollection propertiesCollection;

		// Token: 0x040007E2 RID: 2018
		private string catalog;

		// Token: 0x040007E3 RID: 2019
		private string sessionId;

		// Token: 0x040007E4 RID: 2020
		private int hashCode;

		// Token: 0x040007E5 RID: 2021
		private bool hashCodeCalculated;

		// Token: 0x040007E6 RID: 2022
		internal const string namedsetNameColumn = "SET_NAME";

		// Token: 0x040007E7 RID: 2023
		private const string descriptionColumn = "DESCRIPTION";

		// Token: 0x040007E8 RID: 2024
		private const string expressionColumn = "EXPRESSION";

		// Token: 0x040007E9 RID: 2025
		private const string captionColumn = "SET_CAPTION";

		// Token: 0x040007EA RID: 2026
		private const string displayFolderColumn = "SET_DISPLAY_FOLDER";
	}
}
