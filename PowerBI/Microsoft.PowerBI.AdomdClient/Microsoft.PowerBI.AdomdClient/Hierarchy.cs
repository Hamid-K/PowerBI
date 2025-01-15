using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000088 RID: 136
	public sealed class Hierarchy : IAdomdBaseObject, IMetadataObject, ISubordinateObject
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x00026EEC File Offset: 0x000250EC
		internal Hierarchy(AdomdConnection connection, DataTable hierarchyTable, string cubeName, Axis axis, int hierarchyOrdinal)
		{
			this.baseData = new BaseObjectData(connection, false, hierarchyTable, null, null, cubeName, null, null);
			this.axis = axis;
			this.hierarchyOrdinal = hierarchyOrdinal;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00026F2C File Offset: 0x0002512C
		internal Hierarchy(AdomdConnection connection, DataRow hierarchyRow, Dimension parentDimension, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, hierarchyRow, parentDimension, null, catalog, sessionId);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00026F5B File Offset: 0x0002515B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00026F64 File Offset: 0x00025164
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00026FB9 File Offset: 0x000251B9
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00026FC1 File Offset: 0x000251C1
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

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00026FEC File Offset: 0x000251EC
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

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00027056 File Offset: 0x00025256
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00027080 File Offset: 0x00025280
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000270EB File Offset: 0x000252EB
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00027118 File Offset: 0x00025318
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000271D4 File Offset: 0x000253D4
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

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0002722C File Offset: 0x0002542C
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

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00027261 File Offset: 0x00025461
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002726E File Offset: 0x0002546E
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0002727B File Offset: 0x0002547B
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

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00027289 File Offset: 0x00025489
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00027296 File Offset: 0x00025496
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000272A4 File Offset: 0x000254A4
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x000272B1 File Offset: 0x000254B1
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000272BF File Offset: 0x000254BF
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000272F4 File Offset: 0x000254F4
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeHierarchy;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000272F7 File Offset: 0x000254F7
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00027327 File Offset: 0x00025527
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00027339 File Offset: 0x00025539
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00027350 File Offset: 0x00025550
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00027367 File Offset: 0x00025567
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00027383 File Offset: 0x00025583
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00027395 File Offset: 0x00025595
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(Hierarchy);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000273A1 File Offset: 0x000255A1
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000273B3 File Offset: 0x000255B3
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000273C5 File Offset: 0x000255C5
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Hierarchy);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000273D1 File Offset: 0x000255D1
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

		// Token: 0x0600086B RID: 2155 RVA: 0x0002740A File Offset: 0x0002560A
		public override bool Equals(object obj)
		{
			if (!this.IsFromCellSet)
			{
				return AdomdUtils.Equals(this, obj as IMetadataObject);
			}
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002742D File Offset: 0x0002562D
		public static bool operator ==(Hierarchy o1, Hierarchy o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00027436 File Offset: 0x00025636
		public static bool operator !=(Hierarchy o1, Hierarchy o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00027442 File Offset: 0x00025642
		internal DataTable HierarchyTable
		{
			get
			{
				return (DataTable)this.baseData.AxisData;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00027454 File Offset: 0x00025654
		internal DataRow HierarchyRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00027466 File Offset: 0x00025666
		internal string DimensionUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.HierarchyRow, Dimension.uniqueNameColumn).ToString();
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0002747D File Offset: 0x0002567D
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002748A File Offset: 0x0002568A
		private bool IsFromCellSet
		{
			get
			{
				return this.HierarchyTable != null;
			}
		}

		// Token: 0x0400059A RID: 1434
		private BaseObjectData baseData;

		// Token: 0x0400059B RID: 1435
		private LevelCollection levels;

		// Token: 0x0400059C RID: 1436
		private PropertyCollection propertyCollection;

		// Token: 0x0400059D RID: 1437
		private Axis axis;

		// Token: 0x0400059E RID: 1438
		private int hierarchyOrdinal = -1;

		// Token: 0x0400059F RID: 1439
		private int hashCode;

		// Token: 0x040005A0 RID: 1440
		private bool hashCodeCalculated;

		// Token: 0x040005A1 RID: 1441
		internal static string hierarchyNameColumn = "HIERARCHY_NAME";

		// Token: 0x040005A2 RID: 1442
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040005A3 RID: 1443
		internal static string uniqueNameColumn = HierarchyCollectionInternal.hierUNameRest;

		// Token: 0x040005A4 RID: 1444
		internal static string defaultMemberColumn = "DEFAULT_MEMBER";

		// Token: 0x040005A5 RID: 1445
		internal static string captionColumn = "HIERARCHY_CAPTION";

		// Token: 0x040005A6 RID: 1446
		internal static string isAttribHierColumn = "HIERARCHY_ORIGIN";

		// Token: 0x040005A7 RID: 1447
		internal static string displayFolderColumn = "HIERARCHY_DISPLAY_FOLDER";

		// Token: 0x040005A8 RID: 1448
		internal static string structureColumn = "STRUCTURE";

		// Token: 0x040005A9 RID: 1449
		private static int structureUnbalanced = 2;

		// Token: 0x040005AA RID: 1450
		private static int MD_USER_DEFINED = 1;

		// Token: 0x040005AB RID: 1451
		private static int MD_SYSTEM_ENABLED = 2;

		// Token: 0x040005AC RID: 1452
		private static int PC_MASK = Hierarchy.MD_USER_DEFINED | Hierarchy.MD_SYSTEM_ENABLED;
	}
}
