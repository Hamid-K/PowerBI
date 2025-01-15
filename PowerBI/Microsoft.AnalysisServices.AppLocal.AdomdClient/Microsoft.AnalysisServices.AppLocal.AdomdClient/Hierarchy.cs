using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000088 RID: 136
	public sealed class Hierarchy : IAdomdBaseObject, IMetadataObject, ISubordinateObject
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0002721C File Offset: 0x0002541C
		internal Hierarchy(AdomdConnection connection, DataTable hierarchyTable, string cubeName, Axis axis, int hierarchyOrdinal)
		{
			this.baseData = new BaseObjectData(connection, false, hierarchyTable, null, null, cubeName, null, null);
			this.axis = axis;
			this.hierarchyOrdinal = hierarchyOrdinal;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002725C File Offset: 0x0002545C
		internal Hierarchy(AdomdConnection connection, DataRow hierarchyRow, Dimension parentDimension, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, hierarchyRow, parentDimension, null, catalog, sessionId);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002728B File Offset: 0x0002548B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00027294 File Offset: 0x00025494
		public string Name
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				string text = AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.hierarchyNameColumn).ToString();
				if (text.Length == 0)
				{
					return ((Dimension)this.baseData.ParentObject).Name;
				}
				return text;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000272E9 File Offset: 0x000254E9
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000272F1 File Offset: 0x000254F1
		public string Description
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				return AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.descriptionColumn).ToString();
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0002731C File Offset: 0x0002551C
		public Dimension ParentDimension
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				if (this.baseData.ParentObject == null)
				{
					this.baseData.ParentObject = this.Connection.GetObjectData(SchemaObjectType.ObjectTypeDimension, this.baseData.CubeName, this.DimensionUniqueName) as IAdomdBaseObject;
				}
				return (Dimension)this.baseData.ParentObject;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00027386 File Offset: 0x00025586
		public string DefaultMember
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				return AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.defaultMemberColumn).ToString();
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000273B0 File Offset: 0x000255B0
		public string DisplayFolder
		{
			get
			{
				if (this.Connection == null)
				{
					throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
				}
				AdomdUtils.CheckConnectionOpened(this.Connection);
				if (!this.Connection.IsPostYukonProvider())
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				return AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.displayFolderColumn).ToString();
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0002741B File Offset: 0x0002561B
		public string Caption
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				return AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.captionColumn).ToString();
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00027448 File Offset: 0x00025648
		public HierarchyOrigin HierarchyOrigin
		{
			get
			{
				if (this.Connection == null)
				{
					throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
				}
				AdomdUtils.CheckConnectionOpened(this.Connection);
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				if (!this.Connection.IsPostYukonProvider())
				{
					int num = Convert.ToInt32(AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.structureColumn), CultureInfo.InvariantCulture);
					if (Hierarchy.structureUnbalanced == num)
					{
						return HierarchyOrigin.ParentChildHierarchy;
					}
					return HierarchyOrigin.UserHierarchy;
				}
				else
				{
					int num2 = (int)Convert.ToInt16(AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.isAttribHierColumn), CultureInfo.InvariantCulture);
					if ((num2 & Hierarchy.PC_MASK) == Hierarchy.PC_MASK)
					{
						return HierarchyOrigin.ParentChildHierarchy;
					}
					if ((num2 & Hierarchy.MD_SYSTEM_ENABLED) == Hierarchy.MD_SYSTEM_ENABLED)
					{
						return HierarchyOrigin.AttributeHierarchy;
					}
					int num3 = num2 & Hierarchy.MD_USER_DEFINED;
					int md_USER_DEFINED = Hierarchy.MD_USER_DEFINED;
					return HierarchyOrigin.UserHierarchy;
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00027504 File Offset: 0x00025704
		public LevelCollection Levels
		{
			get
			{
				if (this.levels != null)
				{
					this.levels.CollectionInternal.CheckCache();
				}
				if (!this.baseData.IsMetadata)
				{
					AdomdUtils.PopulateSymetry(this);
				}
				if (this.levels == null)
				{
					this.levels = new LevelCollection(this.Connection, this);
				}
				return this.levels;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002755C File Offset: 0x0002575C
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					if (!this.baseData.IsMetadata)
					{
						AdomdUtils.PopulateSymetry(this);
					}
					this.propertyCollection = new PropertyCollection(this.HierarchyRow, this);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00027591 File Offset: 0x00025791
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0002759E File Offset: 0x0002579E
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x000275AB File Offset: 0x000257AB
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000275B9 File Offset: 0x000257B9
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x000275C6 File Offset: 0x000257C6
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000275D4 File Offset: 0x000257D4
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x000275E1 File Offset: 0x000257E1
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000275EF File Offset: 0x000257EF
		string IAdomdBaseObject.CubeName
		{
			get
			{
				if (this.baseData.IsMetadata)
				{
					return ((Dimension)this.baseData.ParentObject).ParentCube.Name;
				}
				return this.baseData.CubeName;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00027624 File Offset: 0x00025824
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeHierarchy;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00027627 File Offset: 0x00025827
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				if (!this.baseData.IsMetadata)
				{
					return this.HierarchyTable.TableName;
				}
				return AdomdUtils.GetProperty(this.HierarchyRow, Hierarchy.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00027657 File Offset: 0x00025857
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00027669 File Offset: 0x00025869
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00027680 File Offset: 0x00025880
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00027697 File Offset: 0x00025897
		string IMetadataObject.CubeName
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return this.ParentDimension.ParentCube.Name;
				}
				return null;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x000276B3 File Offset: 0x000258B3
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

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000276C5 File Offset: 0x000258C5
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Hierarchy);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000276D1 File Offset: 0x000258D1
		object ISubordinateObject.Parent
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return null;
				}
				return this.axis;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000276E3 File Offset: 0x000258E3
		int ISubordinateObject.Ordinal
		{
			get
			{
				if (!this.IsFromCellSet)
				{
					return -1;
				}
				return this.hierarchyOrdinal;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000276F5 File Offset: 0x000258F5
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Hierarchy);
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00027701 File Offset: 0x00025901
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				if (this.IsFromCellSet)
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

		// Token: 0x06000878 RID: 2168 RVA: 0x0002773A File Offset: 0x0002593A
		public override bool Equals(object obj)
		{
			if (!this.IsFromCellSet)
			{
				return AdomdUtils.Equals(this, obj as IMetadataObject);
			}
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002775D File Offset: 0x0002595D
		public static bool operator ==(Hierarchy o1, Hierarchy o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00027766 File Offset: 0x00025966
		public static bool operator !=(Hierarchy o1, Hierarchy o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00027772 File Offset: 0x00025972
		internal DataTable HierarchyTable
		{
			get
			{
				return (DataTable)this.baseData.AxisData;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00027784 File Offset: 0x00025984
		internal DataRow HierarchyRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00027796 File Offset: 0x00025996
		internal string DimensionUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.HierarchyRow, Dimension.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000277AD File Offset: 0x000259AD
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000277BA File Offset: 0x000259BA
		private bool IsFromCellSet
		{
			get
			{
				return this.HierarchyTable != null;
			}
		}

		// Token: 0x040005A7 RID: 1447
		private BaseObjectData baseData;

		// Token: 0x040005A8 RID: 1448
		private LevelCollection levels;

		// Token: 0x040005A9 RID: 1449
		private PropertyCollection propertyCollection;

		// Token: 0x040005AA RID: 1450
		private Axis axis;

		// Token: 0x040005AB RID: 1451
		private int hierarchyOrdinal = -1;

		// Token: 0x040005AC RID: 1452
		private int hashCode;

		// Token: 0x040005AD RID: 1453
		private bool hashCodeCalculated;

		// Token: 0x040005AE RID: 1454
		internal static string hierarchyNameColumn = "HIERARCHY_NAME";

		// Token: 0x040005AF RID: 1455
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040005B0 RID: 1456
		internal static string uniqueNameColumn = HierarchyCollectionInternal.hierUNameRest;

		// Token: 0x040005B1 RID: 1457
		internal static string defaultMemberColumn = "DEFAULT_MEMBER";

		// Token: 0x040005B2 RID: 1458
		internal static string captionColumn = "HIERARCHY_CAPTION";

		// Token: 0x040005B3 RID: 1459
		internal static string isAttribHierColumn = "HIERARCHY_ORIGIN";

		// Token: 0x040005B4 RID: 1460
		internal static string displayFolderColumn = "HIERARCHY_DISPLAY_FOLDER";

		// Token: 0x040005B5 RID: 1461
		internal static string structureColumn = "STRUCTURE";

		// Token: 0x040005B6 RID: 1462
		private static int structureUnbalanced = 2;

		// Token: 0x040005B7 RID: 1463
		private static int MD_USER_DEFINED = 1;

		// Token: 0x040005B8 RID: 1464
		private static int MD_SYSTEM_ENABLED = 2;

		// Token: 0x040005B9 RID: 1465
		private static int PC_MASK = Hierarchy.MD_USER_DEFINED | Hierarchy.MD_SYSTEM_ENABLED;
	}
}
