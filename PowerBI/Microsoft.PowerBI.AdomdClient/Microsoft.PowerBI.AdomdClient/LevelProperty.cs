using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009F RID: 159
	public sealed class LevelProperty : ISubordinateObject
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x000282B5 File Offset: 0x000264B5
		internal LevelProperty(AdomdConnection connection, DataRow levelPropRow, Level level, int propertyOrdinal)
		{
			this.connection = connection;
			this.levelPropRow = levelPropRow;
			this.parentLevel = level;
			this.propertyOrdinal = propertyOrdinal;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000282E1 File Offset: 0x000264E1
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000282E9 File Offset: 0x000264E9
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.levelPropNameColumn).ToString();
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00028300 File Offset: 0x00026500
		public string UniqueName
		{
			get
			{
				return this.parentLevel.UniqueName + "." + AdomdUtils.Enquote(this.Name, "[", "]");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002832C File Offset: 0x0002652C
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.levelPropCaptionColumn).ToString();
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00028343 File Offset: 0x00026543
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.levelPropRow, LevelProperty.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002835A File Offset: 0x0002655A
		public Level ParentLevel
		{
			get
			{
				return this.parentLevel;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00028362 File Offset: 0x00026562
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentLevel;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002836A File Offset: 0x0002656A
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propertyOrdinal;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00028372 File Offset: 0x00026572
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(LevelProperty);
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002837E File Offset: 0x0002657E
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000283A1 File Offset: 0x000265A1
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000283AF File Offset: 0x000265AF
		public static bool operator ==(LevelProperty o1, LevelProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000283B8 File Offset: 0x000265B8
		public static bool operator !=(LevelProperty o1, LevelProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040005F2 RID: 1522
		private DataRow levelPropRow;

		// Token: 0x040005F3 RID: 1523
		private Level parentLevel;

		// Token: 0x040005F4 RID: 1524
		private AdomdConnection connection;

		// Token: 0x040005F5 RID: 1525
		private int propertyOrdinal = -1;

		// Token: 0x040005F6 RID: 1526
		private int hashCode;

		// Token: 0x040005F7 RID: 1527
		private bool hashCodeCalculated;

		// Token: 0x040005F8 RID: 1528
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040005F9 RID: 1529
		internal static string levelPropNameColumn = "PROPERTY_NAME";

		// Token: 0x040005FA RID: 1530
		private static string levelPropCaptionColumn = "PROPERTY_CAPTION";
	}
}
