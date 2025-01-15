using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009F RID: 159
	public sealed class LevelProperty : ISubordinateObject
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x000285E5 File Offset: 0x000267E5
		internal LevelProperty(AdomdConnection connection, DataRow levelPropRow, Level level, int propertyOrdinal)
		{
			this.connection = connection;
			this.levelPropRow = levelPropRow;
			this.parentLevel = level;
			this.propertyOrdinal = propertyOrdinal;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00028611 File Offset: 0x00026811
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00028619 File Offset: 0x00026819
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.levelPropNameColumn).ToString();
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00028630 File Offset: 0x00026830
		public string UniqueName
		{
			get
			{
				return this.parentLevel.UniqueName + "." + AdomdUtils.Enquote(this.Name, "[", "]");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0002865C File Offset: 0x0002685C
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.levelPropCaptionColumn).ToString();
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00028673 File Offset: 0x00026873
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0002868A File Offset: 0x0002688A
		public Level ParentLevel
		{
			get
			{
				return this.parentLevel;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00028692 File Offset: 0x00026892
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentLevel;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0002869A File Offset: 0x0002689A
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propertyOrdinal;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000286A2 File Offset: 0x000268A2
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(LevelProperty);
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000286AE File Offset: 0x000268AE
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000286D1 File Offset: 0x000268D1
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000286DF File Offset: 0x000268DF
		public static bool operator ==(LevelProperty o1, LevelProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000286E8 File Offset: 0x000268E8
		public static bool operator !=(LevelProperty o1, LevelProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040005FF RID: 1535
		private DataRow levelPropRow;

		// Token: 0x04000600 RID: 1536
		private Level parentLevel;

		// Token: 0x04000601 RID: 1537
		private AdomdConnection connection;

		// Token: 0x04000602 RID: 1538
		private int propertyOrdinal = -1;

		// Token: 0x04000603 RID: 1539
		private int hashCode;

		// Token: 0x04000604 RID: 1540
		private bool hashCodeCalculated;

		// Token: 0x04000605 RID: 1541
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000606 RID: 1542
		internal static string levelPropNameColumn = "PROPERTY_NAME";

		// Token: 0x04000607 RID: 1543
		private static string levelPropCaptionColumn = "PROPERTY_CAPTION";
	}
}
