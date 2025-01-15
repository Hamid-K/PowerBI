using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000099 RID: 153
	public sealed class Kpi : IMetadataObject
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x00027C0F File Offset: 0x00025E0F
		internal Kpi(AdomdConnection connection, DataRow kpiRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.kpiRow = kpiRow;
			this.parentCube = parentCube;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00027C43 File Offset: 0x00025E43
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00027C4B File Offset: 0x00025E4B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.kpiNameColumn).ToString();
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00027C62 File Offset: 0x00025E62
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00027C79 File Offset: 0x00025E79
		public string DisplayFolder
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.displayFolderColumn).ToString();
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00027C90 File Offset: 0x00025E90
		public string TrendGraphic
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.trendGraphicColumn).ToString();
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00027CA7 File Offset: 0x00025EA7
		public string StatusGraphic
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.statusGraphicColumn).ToString();
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00027CBE File Offset: 0x00025EBE
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.kpiCaptionColumn).ToString();
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00027CD8 File Offset: 0x00025ED8
		public Kpi ParentKpi
		{
			get
			{
				string text = null;
				if (this.kpiRow.Table.Columns.Contains(Kpi.parentKpiNameColumn))
				{
					text = AdomdUtils.GetProperty(this.kpiRow, Kpi.parentKpiNameColumn) as string;
				}
				if (text == null || text.Length == 0)
				{
					return null;
				}
				return this.ParentCube.InternalGetSchemaObject(SchemaObjectType.ObjectTypeKpi, text) as Kpi;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00027D38 File Offset: 0x00025F38
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00027D40 File Offset: 0x00025F40
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.kpiRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00027D62 File Offset: 0x00025F62
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00027D6A File Offset: 0x00025F6A
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00027D72 File Offset: 0x00025F72
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00027D7A File Offset: 0x00025F7A
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00027D87 File Offset: 0x00025F87
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00027D8F File Offset: 0x00025F8F
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Kpi);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00027D9B File Offset: 0x00025F9B
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00027DBE File Offset: 0x00025FBE
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00027DCC File Offset: 0x00025FCC
		public static bool operator ==(Kpi o1, Kpi o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00027DD5 File Offset: 0x00025FD5
		public static bool operator !=(Kpi o1, Kpi o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040005DC RID: 1500
		private DataRow kpiRow;

		// Token: 0x040005DD RID: 1501
		private CubeDef parentCube;

		// Token: 0x040005DE RID: 1502
		private AdomdConnection connection;

		// Token: 0x040005DF RID: 1503
		private PropertyCollection propertiesCollection;

		// Token: 0x040005E0 RID: 1504
		private string catalog;

		// Token: 0x040005E1 RID: 1505
		private string sessionId;

		// Token: 0x040005E2 RID: 1506
		private int hashCode;

		// Token: 0x040005E3 RID: 1507
		private bool hashCodeCalculated;

		// Token: 0x040005E4 RID: 1508
		internal static string kpiNameColumn = "KPI_NAME";

		// Token: 0x040005E5 RID: 1509
		private static string descriptionColumn = "KPI_DESCRIPTION";

		// Token: 0x040005E6 RID: 1510
		private static string displayFolderColumn = "KPI_DISPLAY_FOLDER";

		// Token: 0x040005E7 RID: 1511
		private static string trendGraphicColumn = "KPI_TREND_GRAPHIC";

		// Token: 0x040005E8 RID: 1512
		private static string statusGraphicColumn = "KPI_STATUS_GRAPHIC";

		// Token: 0x040005E9 RID: 1513
		private static string parentKpiNameColumn = "KPI_PARENT_KPI_NAME";

		// Token: 0x040005EA RID: 1514
		internal static string kpiCaptionColumn = "KPI_CAPTION";
	}
}
