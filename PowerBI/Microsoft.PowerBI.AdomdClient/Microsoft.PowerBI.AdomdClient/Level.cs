using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009C RID: 156
	public sealed class Level : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00027D08 File Offset: 0x00025F08
		internal Level(AdomdConnection connection, DataRow levelRow, Hierarchy hierarchy, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, levelRow, hierarchy, null, catalog, sessionId);
			this.parentHierarchy = hierarchy;
			this.levelProperties = new LevelPropertyCollection(connection, this);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00027D44 File Offset: 0x00025F44
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00027D4C File Offset: 0x00025F4C
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.levelNameColumn).ToString();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00027D63 File Offset: 0x00025F63
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00027D6B File Offset: 0x00025F6B
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.descriptionColumn).ToString();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00027D82 File Offset: 0x00025F82
		public Hierarchy ParentHierarchy
		{
			get
			{
				return this.parentHierarchy;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00027D8A File Offset: 0x00025F8A
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.captionColumn).ToString();
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00027DA1 File Offset: 0x00025FA1
		public MemberCollection GetMembers()
		{
			return this.InternalGetMembers(0L, -1L, new string[0], new MemberFilter[0]);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00027DB9 File Offset: 0x00025FB9
		public MemberCollection GetMembers(long start, long count)
		{
			return this.GetMembers(start, count, new MemberFilter[0]);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00027DC9 File Offset: 0x00025FC9
		public MemberCollection GetMembers(long start, long count, params MemberFilter[] filters)
		{
			return this.GetMembers(start, count, new string[0], filters);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00027DDA File Offset: 0x00025FDA
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00027E07 File Offset: 0x00026007
		public long MemberCount
		{
			get
			{
				return (long)Convert.ToInt32(AdomdUtils.GetProperty(this.LevelRow, Level.memberCountColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00027E24 File Offset: 0x00026024
		public int LevelNumber
		{
			get
			{
				return (int)Convert.ToInt16(AdomdUtils.GetProperty(this.LevelRow, Level.levelNumberColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00027E40 File Offset: 0x00026040
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00027E7B File Offset: 0x0002607B
		public LevelPropertyCollection LevelProperties
		{
			get
			{
				return this.levelProperties;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00027E83 File Offset: 0x00026083
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00027EA5 File Offset: 0x000260A5
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00027EB2 File Offset: 0x000260B2
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00027EBF File Offset: 0x000260BF
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00027ECD File Offset: 0x000260CD
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00027EDA File Offset: 0x000260DA
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

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00027EE8 File Offset: 0x000260E8
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00027EF5 File Offset: 0x000260F5
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

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00027F03 File Offset: 0x00026103
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this.parentHierarchy).CubeName;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00027F10 File Offset: 0x00026110
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeLevel;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00027F13 File Offset: 0x00026113
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.LevelRow, Level.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00027F2A File Offset: 0x0002612A
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00027F32 File Offset: 0x00026132
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00027F3F File Offset: 0x0002613F
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00027F4C File Offset: 0x0002614C
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this.parentHierarchy).CubeName;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00027F59 File Offset: 0x00026159
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00027F61 File Offset: 0x00026161
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Level);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00027F6D File Offset: 0x0002616D
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00027F90 File Offset: 0x00026190
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00027F9E File Offset: 0x0002619E
		public static bool operator ==(Level o1, Level o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00027FA7 File Offset: 0x000261A7
		public static bool operator !=(Level o1, Level o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00027FB3 File Offset: 0x000261B3
		internal DataRow LevelRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00027FC5 File Offset: 0x000261C5
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027FD4 File Offset: 0x000261D4
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

		// Token: 0x040005E1 RID: 1505
		private BaseObjectData baseData;

		// Token: 0x040005E2 RID: 1506
		private Hierarchy parentHierarchy;

		// Token: 0x040005E3 RID: 1507
		private LevelPropertyCollection levelProperties;

		// Token: 0x040005E4 RID: 1508
		private PropertyCollection propertiesCollection;

		// Token: 0x040005E5 RID: 1509
		private int hashCode;

		// Token: 0x040005E6 RID: 1510
		private bool hashCodeCalculated;

		// Token: 0x040005E7 RID: 1511
		internal static string levelNameColumn = "LEVEL_NAME";

		// Token: 0x040005E8 RID: 1512
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040005E9 RID: 1513
		internal static string uniqueNameColumn = LevelCollectionInternal.levelUNameRest;

		// Token: 0x040005EA RID: 1514
		private static string typeColumn = "LEVEL_TYPE";

		// Token: 0x040005EB RID: 1515
		private static string captionColumn = "LEVEL_CAPTION";

		// Token: 0x040005EC RID: 1516
		private static string levelNumberColumn = "LEVEL_NUMBER";

		// Token: 0x040005ED RID: 1517
		private static string memberCountColumn = "LEVEL_CARDINALITY";
	}
}
