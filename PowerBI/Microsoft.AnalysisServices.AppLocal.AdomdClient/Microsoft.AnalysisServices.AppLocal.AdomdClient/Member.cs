using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A6 RID: 166
	public sealed class Member : IAdomdBaseObject, IMetadataObject, ISubordinateObject
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x00028D70 File Offset: 0x00026F70
		internal Member(AdomdConnection connection, DataRow memberRow, Level parentLevel, Member parentMember, MemberOrigin memberOrigin, string cubeName, Tuple parentTuple, int memberOrdinal, string catalog, string sessionId)
		{
			bool flag = memberOrigin == MemberOrigin.Metadata;
			this.baseData = new BaseObjectData(connection, flag, flag ? null : memberRow, flag ? memberRow : null, parentLevel, cubeName, catalog, sessionId);
			this.parentLevel = parentLevel;
			this.parent = parentMember;
			this.memberProperties = null;
			this.parentTuple = parentTuple;
			this.memberOrdinal = memberOrdinal;
			this.memberOrigin = memberOrigin;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00028DE0 File Offset: 0x00026FE0
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00028DE8 File Offset: 0x00026FE8
		public string Name
		{
			get
			{
				DataRow dataRow;
				string text;
				if (this.baseData.IsMetadata)
				{
					dataRow = (DataRow)this.baseData.MetadataData;
					text = ((this.memberOrigin == MemberOrigin.UserQuery) ? "MEMBER_UNIQUE_NAME" : "MEMBER_NAME");
				}
				else
				{
					dataRow = (DataRow)this.baseData.AxisData;
					text = ((this.memberOrigin == MemberOrigin.UserQuery) ? "UName" : "MEMBER_NAME");
				}
				return AdomdUtils.GetProperty(dataRow, text).ToString();
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00028E5E File Offset: 0x0002705E
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00028E68 File Offset: 0x00027068
		public string LevelName
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.levelUNameColumn).ToString();
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("LName"))
				{
					return AdomdUtils.GetProperty(dataRow, "LName").ToString();
				}
				throw new NotSupportedException(SR.Member_MissingLevelName);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00028EE8 File Offset: 0x000270E8
		public int LevelDepth
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return Convert.ToInt32(AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.levelDepthColumn), CultureInfo.InvariantCulture);
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("LNum"))
				{
					return Convert.ToInt32(AdomdUtils.GetProperty(dataRow, "LNum"), CultureInfo.InvariantCulture);
				}
				throw new NotSupportedException(SR.Member_MissingLevelDepth);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00028F6F File Offset: 0x0002716F
		public Level ParentLevel
		{
			get
			{
				if (null == this.parentLevel)
				{
					this.PopulateParentLevel();
				}
				return this.parentLevel;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00028F8C File Offset: 0x0002718C
		public string Caption
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.captionColumn).ToString();
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("Caption"))
				{
					return AdomdUtils.GetProperty(dataRow, "Caption").ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00029004 File Offset: 0x00027204
		public string Description
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.descriptionColumn).ToString();
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("Description"))
				{
					return AdomdUtils.GetProperty(dataRow, "Description").ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0002907C File Offset: 0x0002727C
		public Member Parent
		{
			get
			{
				if (null == this.parent)
				{
					if (this.baseData.CubeName == null)
					{
						throw new NotSupportedException(SR.NotSupportedByProvider);
					}
					if (this.Connection == null)
					{
						throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
					}
					AdomdUtils.CheckConnectionOpened(this.Connection);
					ListDictionary listDictionary = new ListDictionary();
					listDictionary.Add(CubeCollectionInternal.cubeNameRest, this.baseData.CubeName);
					AdomdUtils.AddCubeSourceRestrictionIfApplicable(this.Connection, listDictionary);
					listDictionary.Add("MEMBER_UNIQUE_NAME", this.UniqueName);
					listDictionary.Add("TREE_OP", 4);
					DataRowCollection rows = AdomdUtils.GetRows(this.Connection, "MDSCHEMA_MEMBERS", listDictionary);
					if (rows.Count > 0)
					{
						this.parent = new Member(this.Connection, rows[0], null, null, MemberOrigin.Metadata, this.baseData.CubeName, null, -1, this.baseData.Catalog, this.baseData.SessionID);
					}
				}
				return this.parent;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00029179 File Offset: 0x00027379
		public MemberPropertyCollection MemberProperties
		{
			get
			{
				if (this.memberProperties == null)
				{
					this.memberProperties = new MemberPropertyCollection((DataRow)this.baseData.AxisData, this, this.GetInternallyAddedDimensionPropertyCount());
				}
				return this.memberProperties;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000291AB File Offset: 0x000273AB
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new PropertyCollection((DataRow)this.baseData.MetadataData, this, 0);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000291D8 File Offset: 0x000273D8
		public long ChildCount
		{
			get
			{
				if (this.baseData.AxisData == null)
				{
					return (long)Convert.ToInt32(AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.childCountColumn), CultureInfo.InvariantCulture);
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("DisplayInfo"))
				{
					return (long)(Convert.ToInt32(AdomdUtils.GetProperty(dataRow, "DisplayInfo"), CultureInfo.InvariantCulture) & 65535);
				}
				throw new NotSupportedException(SR.Member_MissingDisplayInfo);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00029268 File Offset: 0x00027468
		public MemberTypeEnum Type
		{
			get
			{
				int num;
				if (this.baseData.IsMetadata)
				{
					DataRow dataRow = (DataRow)this.baseData.MetadataData;
					num = Convert.ToInt32(AdomdUtils.GetProperty(dataRow, Member.typeColumn), CultureInfo.InvariantCulture);
				}
				else
				{
					DataRow dataRow = (DataRow)this.baseData.AxisData;
					if (dataRow.Table.Columns.Contains(Member.typeColumn))
					{
						num = Convert.ToInt32(AdomdUtils.GetProperty(dataRow, Member.typeColumn), CultureInfo.InvariantCulture);
					}
					else
					{
						Hashtable orCreateNamesHashtable = MemberPropertyCollection.GetOrCreateNamesHashtable(dataRow.Table, this.GetInternallyAddedDimensionPropertyCount());
						if (!(orCreateNamesHashtable[Member.typeColumn] is int))
						{
							throw new InvalidOperationException(SR.InvalidOperationPriorToFetchAllProperties);
						}
						num = Convert.ToInt32(AdomdUtils.GetProperty(dataRow, (int)orCreateNamesHashtable[Member.typeColumn]), CultureInfo.InvariantCulture);
					}
				}
				switch (num)
				{
				case 0:
					return MemberTypeEnum.Unknown;
				case 1:
					return MemberTypeEnum.Regular;
				case 2:
					return MemberTypeEnum.All;
				case 3:
					return MemberTypeEnum.Measure;
				case 4:
					return MemberTypeEnum.Formula;
				default:
					return MemberTypeEnum.Unknown;
				}
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002936C File Offset: 0x0002756C
		public bool DrilledDown
		{
			get
			{
				if (this.baseData.AxisData == null || this.memberOrigin != MemberOrigin.UserQuery)
				{
					throw new NotSupportedException(SR.NotSupportedOnNonCellsetMember);
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("DisplayInfo"))
				{
					return (Convert.ToInt32(AdomdUtils.GetProperty(dataRow, "DisplayInfo"), CultureInfo.InvariantCulture) & 65536) != 0;
				}
				throw new NotSupportedException(SR.Member_MissingDisplayInfo);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000293EC File Offset: 0x000275EC
		public bool ParentSameAsPrevious
		{
			get
			{
				if (this.baseData.AxisData == null || this.memberOrigin != MemberOrigin.UserQuery)
				{
					throw new NotSupportedException(SR.NotSupportedOnNonCellsetMember);
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("DisplayInfo"))
				{
					return (Convert.ToInt32(AdomdUtils.GetProperty(dataRow, "DisplayInfo"), CultureInfo.InvariantCulture) & 131072) != 0;
				}
				throw new NotSupportedException(SR.Member_MissingDisplayInfo);
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002946B File Offset: 0x0002766B
		public MemberCollection GetChildren()
		{
			return this.InternalGetChildren(0L, -1L, new string[0], new MemberFilter[0]);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00029483 File Offset: 0x00027683
		public MemberCollection GetChildren(long start, long count)
		{
			return this.GetChildren(start, count, new MemberFilter[0]);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00029493 File Offset: 0x00027693
		public MemberCollection GetChildren(long start, long count, params MemberFilter[] filters)
		{
			return this.GetChildren(start, count, new string[0], filters);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000294A4 File Offset: 0x000276A4
		public MemberCollection GetChildren(long start, long count, string[] properties, params MemberFilter[] filters)
		{
			if (start < 0L)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0L)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return this.InternalGetChildren(start, count, properties, filters);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000294D1 File Offset: 0x000276D1
		public void FetchAllProperties()
		{
			if (!this.baseData.IsMetadata)
			{
				AdomdUtils.PopulateSymetry(this);
				this.propertyCollection = null;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000294F0 File Offset: 0x000276F0
		internal void PopulateParentLevel()
		{
			if (this.Connection == null)
			{
				throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
			}
			AdomdUtils.CheckConnectionOpened(this.Connection);
			string parentLevelUName = this.ParentLevelUName;
			this.parentLevel = this.Connection.GetObjectData(SchemaObjectType.ObjectTypeLevel, this.baseData.CubeName, parentLevelUName) as Level;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00029548 File Offset: 0x00027748
		private string ParentLevelUName
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, Member.levelUNameColumn).ToString();
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("LName"))
				{
					return AdomdUtils.GetProperty(dataRow, "LName").ToString();
				}
				throw new NotSupportedException(SR.Property_UnknownProperty("LName"));
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000295CA File Offset: 0x000277CA
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000295D7 File Offset: 0x000277D7
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x000295E4 File Offset: 0x000277E4
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

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000295F2 File Offset: 0x000277F2
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x000295FF File Offset: 0x000277FF
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002960D File Offset: 0x0002780D
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x0002961A File Offset: 0x0002781A
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

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00029628 File Offset: 0x00027828
		string IAdomdBaseObject.CubeName
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return this.parentLevel.ParentHierarchy.ParentDimension.ParentCube.Name;
				}
				return this.baseData.CubeName;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002965D File Offset: 0x0002785D
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMember;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00029660 File Offset: 0x00027860
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return AdomdUtils.GetProperty((DataRow)this.baseData.MetadataData, "MEMBER_UNIQUE_NAME").ToString();
				}
				DataRow dataRow = (DataRow)this.baseData.AxisData;
				if (dataRow.Table.Columns.Contains("UName"))
				{
					return AdomdUtils.GetProperty(dataRow, "UName").ToString();
				}
				throw new NotSupportedException(SR.Property_UnknownProperty("UName"));
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000296E2 File Offset: 0x000278E2
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.Connection;
				}
				return null;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000296F4 File Offset: 0x000278F4
		string IMetadataObject.Catalog
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.baseData.Catalog;
				}
				return null;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002970B File Offset: 0x0002790B
		string IMetadataObject.SessionId
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.baseData.SessionID;
				}
				return null;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00029722 File Offset: 0x00027922
		string IMetadataObject.CubeName
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.baseData.CubeName;
				}
				return null;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00029739 File Offset: 0x00027939
		string IMetadataObject.UniqueName
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.UniqueName;
				}
				return null;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002974B File Offset: 0x0002794B
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Member);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00029757 File Offset: 0x00027957
		object ISubordinateObject.Parent
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return null;
				}
				return this.parentTuple;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00029769 File Offset: 0x00027969
		int ISubordinateObject.Ordinal
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return -1;
				}
				return this.memberOrdinal;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002977B File Offset: 0x0002797B
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Member);
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00029787 File Offset: 0x00027987
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				if (!this.IsFromCellSet)
				{
					this.hashCode = AdomdUtils.GetHashCode(this);
				}
				else
				{
					this.hashCode = AdomdUtils.GetHashCode(this);
				}
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000297C0 File Offset: 0x000279C0
		public override bool Equals(object obj)
		{
			if (!this.IsFromCellSet)
			{
				return AdomdUtils.Equals(this, obj as IMetadataObject);
			}
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000297E3 File Offset: 0x000279E3
		public static bool operator ==(Member o1, Member o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000297EC File Offset: 0x000279EC
		public static bool operator !=(Member o1, Member o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000297F8 File Offset: 0x000279F8
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00029805 File Offset: 0x00027A05
		private bool IsFromCellSet
		{
			get
			{
				return this.memberOrigin == MemberOrigin.UserQuery;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00029810 File Offset: 0x00027A10
		private MemberCollection InternalGetChildren(long start, long count, string[] properties, params MemberFilter[] filters)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			Hierarchy parentHierarchy = this.ParentLevel.ParentHierarchy;
			CubeDef parentCube = parentHierarchy.ParentDimension.ParentCube;
			string filteredAndRangedMemberSet = MemberQueryGenerator.GetFilteredAndRangedMemberSet(MemberQueryGenerator.GetBaseSetMemberChilden(this), parentHierarchy.UniqueName, start, count, filters);
			return parentCube.GetMembers(filteredAndRangedMemberSet, properties, null, this);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00029870 File Offset: 0x00027A70
		private int GetInternallyAddedDimensionPropertyCount()
		{
			if (this.memberOrigin != MemberOrigin.InternalMemberQuery)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x0400064B RID: 1611
		internal const string memberNameColumn = "MEMBER_NAME";

		// Token: 0x0400064C RID: 1612
		internal const string uniqueNameColumn = "MEMBER_UNIQUE_NAME";

		// Token: 0x0400064D RID: 1613
		private static string captionColumn = "MEMBER_CAPTION";

		// Token: 0x0400064E RID: 1614
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x0400064F RID: 1615
		private static string levelDepthColumn = "LEVEL_NUMBER";

		// Token: 0x04000650 RID: 1616
		private static string typeColumn = "MEMBER_TYPE";

		// Token: 0x04000651 RID: 1617
		private static string childCountColumn = "CHILDREN_CARDINALITY";

		// Token: 0x04000652 RID: 1618
		private static string levelUNameColumn = LevelCollectionInternal.levelUNameRest;

		// Token: 0x04000653 RID: 1619
		internal const string MemberUNameRest = "MEMBER_UNIQUE_NAME";

		// Token: 0x04000654 RID: 1620
		private const int memberUnknown = 0;

		// Token: 0x04000655 RID: 1621
		private const int memberRegular = 1;

		// Token: 0x04000656 RID: 1622
		private const int memberAll = 2;

		// Token: 0x04000657 RID: 1623
		private const int memberMeasure = 3;

		// Token: 0x04000658 RID: 1624
		private const int memberFormula = 4;

		// Token: 0x04000659 RID: 1625
		private const int drilledDownBitMask = 65536;

		// Token: 0x0400065A RID: 1626
		private const int parentAsPrevBitMask = 131072;

		// Token: 0x0400065B RID: 1627
		private const int childCountBitMask = 65535;

		// Token: 0x0400065C RID: 1628
		internal const string SchemaName = "MDSCHEMA_MEMBERS";

		// Token: 0x0400065D RID: 1629
		private const string treeOpRest = "TREE_OP";

		// Token: 0x0400065E RID: 1630
		private const int treeOpParent = 4;

		// Token: 0x0400065F RID: 1631
		private BaseObjectData baseData;

		// Token: 0x04000660 RID: 1632
		private MemberPropertyCollection memberProperties;

		// Token: 0x04000661 RID: 1633
		private Member parent;

		// Token: 0x04000662 RID: 1634
		private Tuple parentTuple;

		// Token: 0x04000663 RID: 1635
		private int memberOrdinal = -1;

		// Token: 0x04000664 RID: 1636
		private Level parentLevel;

		// Token: 0x04000665 RID: 1637
		private PropertyCollection propertyCollection;

		// Token: 0x04000666 RID: 1638
		private MemberOrigin memberOrigin;

		// Token: 0x04000667 RID: 1639
		private int hashCode;

		// Token: 0x04000668 RID: 1640
		private bool hashCodeCalculated;
	}
}
