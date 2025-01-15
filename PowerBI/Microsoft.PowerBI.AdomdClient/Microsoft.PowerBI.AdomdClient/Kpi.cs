using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000099 RID: 153
	public sealed class Kpi : IMetadataObject
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x000278DF File Offset: 0x00025ADF
		internal Kpi(AdomdConnection connection, DataRow kpiRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.kpiRow = kpiRow;
			this.parentCube = parentCube;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00027913 File Offset: 0x00025B13
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0002791B File Offset: 0x00025B1B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.kpiNameColumn).ToString();
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00027932 File Offset: 0x00025B32
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00027949 File Offset: 0x00025B49
		public string DisplayFolder
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.displayFolderColumn).ToString();
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00027960 File Offset: 0x00025B60
		public string TrendGraphic
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.trendGraphicColumn).ToString();
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00027977 File Offset: 0x00025B77
		public string StatusGraphic
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.statusGraphicColumn).ToString();
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0002798E File Offset: 0x00025B8E
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.kpiRow, Kpi.kpiCaptionColumn).ToString();
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000279A8 File Offset: 0x00025BA8
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

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00027A08 File Offset: 0x00025C08
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00027A10 File Offset: 0x00025C10
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00027A32 File Offset: 0x00025C32
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00027A3A File Offset: 0x00025C3A
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00027A42 File Offset: 0x00025C42
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00027A4A File Offset: 0x00025C4A
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00027A57 File Offset: 0x00025C57
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00027A5F File Offset: 0x00025C5F
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Kpi);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00027A6B File Offset: 0x00025C6B
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00027A8E File Offset: 0x00025C8E
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00027A9C File Offset: 0x00025C9C
		public static bool operator ==(Kpi o1, Kpi o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00027AA5 File Offset: 0x00025CA5
		public static bool operator !=(Kpi o1, Kpi o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040005CF RID: 1487
		private DataRow kpiRow;

		// Token: 0x040005D0 RID: 1488
		private CubeDef parentCube;

		// Token: 0x040005D1 RID: 1489
		private AdomdConnection connection;

		// Token: 0x040005D2 RID: 1490
		private PropertyCollection propertiesCollection;

		// Token: 0x040005D3 RID: 1491
		private string catalog;

		// Token: 0x040005D4 RID: 1492
		private string sessionId;

		// Token: 0x040005D5 RID: 1493
		private int hashCode;

		// Token: 0x040005D6 RID: 1494
		private bool hashCodeCalculated;

		// Token: 0x040005D7 RID: 1495
		internal static string kpiNameColumn = "KPI_NAME";

		// Token: 0x040005D8 RID: 1496
		private static string descriptionColumn = "KPI_DESCRIPTION";

		// Token: 0x040005D9 RID: 1497
		private static string displayFolderColumn = "KPI_DISPLAY_FOLDER";

		// Token: 0x040005DA RID: 1498
		private static string trendGraphicColumn = "KPI_TREND_GRAPHIC";

		// Token: 0x040005DB RID: 1499
		private static string statusGraphicColumn = "KPI_STATUS_GRAPHIC";

		// Token: 0x040005DC RID: 1500
		private static string parentKpiNameColumn = "KPI_PARENT_KPI_NAME";

		// Token: 0x040005DD RID: 1501
		internal static string kpiCaptionColumn = "KPI_CAPTION";
	}
}
