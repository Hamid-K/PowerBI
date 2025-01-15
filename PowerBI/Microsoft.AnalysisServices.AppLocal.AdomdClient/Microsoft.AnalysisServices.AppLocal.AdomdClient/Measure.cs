using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A3 RID: 163
	public sealed class Measure : IMetadataObject
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00028939 File Offset: 0x00026B39
		internal Measure(AdomdConnection connection, DataRow measureRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.measureRow = measureRow;
			this.parentCube = parentCube;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00028966 File Offset: 0x00026B66
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0002896E File Offset: 0x00026B6E
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.measureNameColumn).ToString();
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00028985 File Offset: 0x00026B85
		public string UniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002899C File Offset: 0x00026B9C
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.captionColumn).ToString();
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x000289B3 File Offset: 0x00026BB3
		public string DisplayFolder
		{
			get
			{
				if (!this.connection.IsPostYukonProvider())
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				return AdomdUtils.GetProperty(this.measureRow, Measure.displayFolderColumn).ToString();
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000289E2 File Offset: 0x00026BE2
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000289F9 File Offset: 0x00026BF9
		public int NumericPrecision
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.measureRow, Measure.precisionColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00028A15 File Offset: 0x00026C15
		public short NumericScale
		{
			get
			{
				return Convert.ToInt16(AdomdUtils.GetProperty(this.measureRow, Measure.scaleColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00028A31 File Offset: 0x00026C31
		public string Units
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.unitsColumn).ToString();
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00028A48 File Offset: 0x00026C48
		public string Expression
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.expressionColumn).ToString();
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00028A5F File Offset: 0x00026C5F
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00028A67 File Offset: 0x00026C67
		public PropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new PropertyCollection(this.measureRow, this);
				}
				return this.properties;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00028A89 File Offset: 0x00026C89
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00028A91 File Offset: 0x00026C91
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00028A99 File Offset: 0x00026C99
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00028AA1 File Offset: 0x00026CA1
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00028AAE File Offset: 0x00026CAE
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00028AB6 File Offset: 0x00026CB6
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Measure);
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028AC2 File Offset: 0x00026CC2
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028AE5 File Offset: 0x00026CE5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00028AF3 File Offset: 0x00026CF3
		public static bool operator ==(Measure o1, Measure o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028AFC File Offset: 0x00026CFC
		public static bool operator !=(Measure o1, Measure o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000637 RID: 1591
		private DataRow measureRow;

		// Token: 0x04000638 RID: 1592
		private CubeDef parentCube;

		// Token: 0x04000639 RID: 1593
		private AdomdConnection connection;

		// Token: 0x0400063A RID: 1594
		private PropertyCollection properties;

		// Token: 0x0400063B RID: 1595
		private string catalog;

		// Token: 0x0400063C RID: 1596
		private string sessionId;

		// Token: 0x0400063D RID: 1597
		private int hashCode;

		// Token: 0x0400063E RID: 1598
		private bool hashCodeCalculated;

		// Token: 0x0400063F RID: 1599
		internal static string measureNameColumn = "MEASURE_NAME";

		// Token: 0x04000640 RID: 1600
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000641 RID: 1601
		internal static string uniqueNameColumn = "MEASURE_UNIQUE_NAME";

		// Token: 0x04000642 RID: 1602
		private static string captionColumn = "MEASURE_CAPTION";

		// Token: 0x04000643 RID: 1603
		private static string displayFolderColumn = "MEASURE_DISPLAY_FOLDER";

		// Token: 0x04000644 RID: 1604
		private static string precisionColumn = "NUMERIC_PRECISION";

		// Token: 0x04000645 RID: 1605
		private static string scaleColumn = "NUMERIC_SCALE";

		// Token: 0x04000646 RID: 1606
		private static string unitsColumn = "MEASURE_UNITS";

		// Token: 0x04000647 RID: 1607
		private static string expressionColumn = "EXPRESSION";
	}
}
