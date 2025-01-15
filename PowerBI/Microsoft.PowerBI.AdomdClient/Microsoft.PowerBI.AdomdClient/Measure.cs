using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A3 RID: 163
	public sealed class Measure : IMetadataObject
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x00028609 File Offset: 0x00026809
		internal Measure(AdomdConnection connection, DataRow measureRow, CubeDef parentCube, string catalog, string sessionId)
		{
			this.connection = connection;
			this.measureRow = measureRow;
			this.parentCube = parentCube;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00028636 File Offset: 0x00026836
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002863E File Offset: 0x0002683E
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.measureNameColumn).ToString();
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00028655 File Offset: 0x00026855
		public string UniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0002866C File Offset: 0x0002686C
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.captionColumn).ToString();
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00028683 File Offset: 0x00026883
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000286B2 File Offset: 0x000268B2
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000286C9 File Offset: 0x000268C9
		public int NumericPrecision
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.measureRow, Measure.precisionColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000286E5 File Offset: 0x000268E5
		public short NumericScale
		{
			get
			{
				return Convert.ToInt16(AdomdUtils.GetProperty(this.measureRow, Measure.scaleColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00028701 File Offset: 0x00026901
		public string Units
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.unitsColumn).ToString();
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00028718 File Offset: 0x00026918
		public string Expression
		{
			get
			{
				return AdomdUtils.GetProperty(this.measureRow, Measure.expressionColumn).ToString();
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002872F File Offset: 0x0002692F
		public CubeDef ParentCube
		{
			get
			{
				return this.parentCube;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00028737 File Offset: 0x00026937
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00028759 File Offset: 0x00026959
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00028761 File Offset: 0x00026961
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00028769 File Offset: 0x00026969
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00028771 File Offset: 0x00026971
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentCube.Name;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002877E File Offset: 0x0002697E
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00028786 File Offset: 0x00026986
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Measure);
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00028792 File Offset: 0x00026992
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000287B5 File Offset: 0x000269B5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000287C3 File Offset: 0x000269C3
		public static bool operator ==(Measure o1, Measure o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000287CC File Offset: 0x000269CC
		public static bool operator !=(Measure o1, Measure o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x0400062A RID: 1578
		private DataRow measureRow;

		// Token: 0x0400062B RID: 1579
		private CubeDef parentCube;

		// Token: 0x0400062C RID: 1580
		private AdomdConnection connection;

		// Token: 0x0400062D RID: 1581
		private PropertyCollection properties;

		// Token: 0x0400062E RID: 1582
		private string catalog;

		// Token: 0x0400062F RID: 1583
		private string sessionId;

		// Token: 0x04000630 RID: 1584
		private int hashCode;

		// Token: 0x04000631 RID: 1585
		private bool hashCodeCalculated;

		// Token: 0x04000632 RID: 1586
		internal static string measureNameColumn = "MEASURE_NAME";

		// Token: 0x04000633 RID: 1587
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000634 RID: 1588
		internal static string uniqueNameColumn = "MEASURE_UNIQUE_NAME";

		// Token: 0x04000635 RID: 1589
		private static string captionColumn = "MEASURE_CAPTION";

		// Token: 0x04000636 RID: 1590
		private static string displayFolderColumn = "MEASURE_DISPLAY_FOLDER";

		// Token: 0x04000637 RID: 1591
		private static string precisionColumn = "NUMERIC_PRECISION";

		// Token: 0x04000638 RID: 1592
		private static string scaleColumn = "NUMERIC_SCALE";

		// Token: 0x04000639 RID: 1593
		private static string unitsColumn = "MEASURE_UNITS";

		// Token: 0x0400063A RID: 1594
		private static string expressionColumn = "EXPRESSION";
	}
}
