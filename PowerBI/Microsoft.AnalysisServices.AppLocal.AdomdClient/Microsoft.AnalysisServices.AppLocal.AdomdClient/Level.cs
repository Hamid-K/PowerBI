using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009C RID: 156
	public sealed class Level : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00028038 File Offset: 0x00026238
		internal Level(AdomdConnection connection, DataRow levelRow, Hierarchy hierarchy, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, levelRow, hierarchy, null, catalog, sessionId);
			this.parentHierarchy = hierarchy;
			this.levelProperties = new LevelPropertyCollection(connection, this);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00028074 File Offset: 0x00026274
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0002807C File Offset: 0x0002627C
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.levelNameColumn).ToString();
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00028093 File Offset: 0x00026293
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0002809B File Offset: 0x0002629B
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000280B2 File Offset: 0x000262B2
		public Hierarchy ParentHierarchy
		{
			get
			{
				return this.parentHierarchy;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x000280BA File Offset: 0x000262BA
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.captionColumn).ToString();
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000280D1 File Offset: 0x000262D1
		public MemberCollection GetMembers()
		{
			return this.InternalGetMembers(0L, -1L, new string[0], new MemberFilter[0]);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000280E9 File Offset: 0x000262E9
		public MemberCollection GetMembers(long start, long count)
		{
			return this.GetMembers(start, count, new MemberFilter[0]);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000280F9 File Offset: 0x000262F9
		public MemberCollection GetMembers(long start, long count, params MemberFilter[] filters)
		{
			return this.GetMembers(start, count, new string[0], filters);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002810A File Offset: 0x0002630A
		public MemberCollection GetMembers(long start, long count, string[] properties, params MemberFilter[] filters)
		{
			if (start < 0L)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0L)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return this.InternalGetMembers(start, count, properties, filters);
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00028137 File Offset: 0x00026337
		public long MemberCount
		{
			get
			{
				return (long)Convert.ToInt32(AdomdUtils.GetProperty(this.LevelRow, Level.memberCountColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00028154 File Offset: 0x00026354
		public int LevelNumber
		{
			get
			{
				return (int)Convert.ToInt16(AdomdUtils.GetProperty(this.LevelRow, Level.levelNumberColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00028170 File Offset: 0x00026370
		public LevelTypeEnum LevelType
		{
			get
			{
				long num = (long)Convert.ToInt32(AdomdUtils.GetProperty(this.LevelRow, Level.typeColumn), CultureInfo.InvariantCulture);
				if (num >= 0L && num <= 8200L)
				{
					return (LevelTypeEnum)num;
				}
				return LevelTypeEnum.Regular;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x000281AB File Offset: 0x000263AB
		public LevelPropertyCollection LevelProperties
		{
			get
			{
				return this.levelProperties;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x000281B3 File Offset: 0x000263B3
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.LevelRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000281D5 File Offset: 0x000263D5
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x000281E2 File Offset: 0x000263E2
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x000281EF File Offset: 0x000263EF
		bool IAdomdBaseObject.IsMetadata
		{
			get
			{
				return this.baseData.IsMetadata;
			}
			set
			{
				this.baseData.IsMetadata = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000281FD File Offset: 0x000263FD
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0002820A File Offset: 0x0002640A
		object IAdomdBaseObject.MetadataData
		{
			get
			{
				return this.baseData.MetadataData;
			}
			set
			{
				this.baseData.MetadataData = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00028218 File Offset: 0x00026418
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00028225 File Offset: 0x00026425
		IAdomdBaseObject IAdomdBaseObject.ParentObject
		{
			get
			{
				return this.baseData.ParentObject;
			}
			set
			{
				this.baseData.ParentObject = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00028233 File Offset: 0x00026433
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this.parentHierarchy).CubeName;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00028240 File Offset: 0x00026440
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeLevel;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00028243 File Offset: 0x00026443
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002825A File Offset: 0x0002645A
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00028262 File Offset: 0x00026462
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002826F File Offset: 0x0002646F
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002827C File Offset: 0x0002647C
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this.parentHierarchy).CubeName;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00028289 File Offset: 0x00026489
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00028291 File Offset: 0x00026491
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Level);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002829D File Offset: 0x0002649D
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000282C0 File Offset: 0x000264C0
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000282CE File Offset: 0x000264CE
		public static bool operator ==(Level o1, Level o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000282D7 File Offset: 0x000264D7
		public static bool operator !=(Level o1, Level o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x000282E3 File Offset: 0x000264E3
		internal DataRow LevelRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x000282F5 File Offset: 0x000264F5
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00028304 File Offset: 0x00026504
		private MemberCollection InternalGetMembers(long start, long count, string[] properties, params MemberFilter[] filters)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			CubeDef parentCube = this.ParentHierarchy.ParentDimension.ParentCube;
			string filteredAndRangedMemberSet = MemberQueryGenerator.GetFilteredAndRangedMemberSet(MemberQueryGenerator.GetBaseSetLevelMembers(this), this.ParentHierarchy.UniqueName, start, count, filters);
			return parentCube.GetMembers(filteredAndRangedMemberSet, properties, this, null);
		}

		// Token: 0x040005EE RID: 1518
		private BaseObjectData baseData;

		// Token: 0x040005EF RID: 1519
		private Hierarchy parentHierarchy;

		// Token: 0x040005F0 RID: 1520
		private LevelPropertyCollection levelProperties;

		// Token: 0x040005F1 RID: 1521
		private PropertyCollection propertiesCollection;

		// Token: 0x040005F2 RID: 1522
		private int hashCode;

		// Token: 0x040005F3 RID: 1523
		private bool hashCodeCalculated;

		// Token: 0x040005F4 RID: 1524
		internal static string levelNameColumn = "LEVEL_NAME";

		// Token: 0x040005F5 RID: 1525
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040005F6 RID: 1526
		internal static string uniqueNameColumn = LevelCollectionInternal.levelUNameRest;

		// Token: 0x040005F7 RID: 1527
		private static string typeColumn = "LEVEL_TYPE";

		// Token: 0x040005F8 RID: 1528
		private static string captionColumn = "LEVEL_CAPTION";

		// Token: 0x040005F9 RID: 1529
		private static string levelNumberColumn = "LEVEL_NUMBER";

		// Token: 0x040005FA RID: 1530
		private static string memberCountColumn = "LEVEL_CARDINALITY";
	}
}
