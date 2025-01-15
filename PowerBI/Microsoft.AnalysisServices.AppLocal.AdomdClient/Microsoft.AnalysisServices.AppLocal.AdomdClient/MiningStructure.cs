using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D1 RID: 209
	public sealed class MiningStructure : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D494 File Offset: 0x0002B694
		internal MiningStructure(DataRow miningStructureRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningStructureRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D4C3 File Offset: 0x0002B6C3
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002D4CB File Offset: 0x0002B6CB
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.miningStructureNameColumn).ToString();
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002D4E2 File Offset: 0x0002B6E2
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.descriptionColumn).ToString();
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D4F9 File Offset: 0x0002B6F9
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.isPopulatedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002D515 File Offset: 0x0002B715
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.lastModifiedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002D531 File Offset: 0x0002B731
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.lastProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002D54D File Offset: 0x0002B74D
		public DateTime Created
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.createdColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002D569 File Offset: 0x0002B769
		public string Caption
		{
			get
			{
				if (this.MiningStructureRow.Table.Columns.Contains(MiningStructure.miningStructureCaption))
				{
					return AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.miningStructureCaption).ToString();
				}
				return this.Name;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002D5A3 File Offset: 0x0002B7A3
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		public MiningStructureColumnCollection Columns
		{
			get
			{
				if (this.miningStructureColumns == null)
				{
					this.miningStructureColumns = new MiningStructureColumnCollection(this.Connection, this);
				}
				else
				{
					this.miningStructureColumns.CollectionInternal.CheckCache();
				}
				return this.miningStructureColumns;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002D5DF File Offset: 0x0002B7DF
		public MiningModelCollection MiningModels
		{
			get
			{
				if (this.miningModels == null)
				{
					this.miningModels = new MiningModelCollection(this);
				}
				else
				{
					this.miningModels.CollectionInternal.CheckCache();
				}
				return this.miningModels;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002D60D File Offset: 0x0002B80D
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new PropertyCollection(this.MiningStructureRow, this);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002D62F File Offset: 0x0002B82F
		public int HoldoutMaxPercent
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutMaxPercentColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002D64B File Offset: 0x0002B84B
		public long HoldoutMaxCases
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutMaxCasesColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002D667 File Offset: 0x0002B867
		public long HoldoutSeed
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutSeedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002D683 File Offset: 0x0002B883
		public long HoldoutActualSize
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutActualSizeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002D69F File Offset: 0x0002B89F
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002D6AC File Offset: 0x0002B8AC
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0002D6B9 File Offset: 0x0002B8B9
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002D6C7 File Offset: 0x0002B8C7
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002D6E2 File Offset: 0x0002B8E2
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0002D6E5 File Offset: 0x0002B8E5
		IAdomdBaseObject IAdomdBaseObject.ParentObject
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002D6E7 File Offset: 0x0002B8E7
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002D6EF File Offset: 0x0002B8EF
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002D6F2 File Offset: 0x0002B8F2
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002D6FA File Offset: 0x0002B8FA
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002D702 File Offset: 0x0002B902
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002D70F File Offset: 0x0002B90F
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002D71C File Offset: 0x0002B91C
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002D724 File Offset: 0x0002B924
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0002D72C File Offset: 0x0002B92C
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningStructure);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002D738 File Offset: 0x0002B938
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002D75B File Offset: 0x0002B95B
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002D769 File Offset: 0x0002B969
		public static bool operator ==(MiningStructure o1, MiningStructure o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002D772 File Offset: 0x0002B972
		public static bool operator !=(MiningStructure o1, MiningStructure o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002D77E File Offset: 0x0002B97E
		internal DataRow MiningStructureRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002D790 File Offset: 0x0002B990
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002D798 File Offset: 0x0002B998
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x0400079D RID: 1949
		private BaseObjectData baseData;

		// Token: 0x0400079E RID: 1950
		private DateTime populationTime;

		// Token: 0x0400079F RID: 1951
		private MiningStructureColumnCollection miningStructureColumns;

		// Token: 0x040007A0 RID: 1952
		private PropertyCollection propertyCollection;

		// Token: 0x040007A1 RID: 1953
		private MiningModelCollection miningModels;

		// Token: 0x040007A2 RID: 1954
		private int hashCode;

		// Token: 0x040007A3 RID: 1955
		private bool hashCodeCalculated;

		// Token: 0x040007A4 RID: 1956
		internal static string miningStructureNameColumn = "STRUCTURE_NAME";

		// Token: 0x040007A5 RID: 1957
		internal static string miningStructureNameRest = MiningStructure.miningStructureNameColumn;

		// Token: 0x040007A6 RID: 1958
		internal static string miningStructureCaption = "STRUCTURE_CAPTION";

		// Token: 0x040007A7 RID: 1959
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040007A8 RID: 1960
		internal static string lastProcessedColumn = "LAST_PROCESSED";

		// Token: 0x040007A9 RID: 1961
		internal static string lastModifiedColumn = "DATE_MODIFIED";

		// Token: 0x040007AA RID: 1962
		internal static string createdColumn = "DATE_CREATED";

		// Token: 0x040007AB RID: 1963
		internal static string isPopulatedColumn = "IS_POPULATED";

		// Token: 0x040007AC RID: 1964
		internal static string holdoutMaxPercentColumn = "HOLDOUT_MAXPERCENT";

		// Token: 0x040007AD RID: 1965
		internal static string holdoutMaxCasesColumn = "HOLDOUT_MAXCASES";

		// Token: 0x040007AE RID: 1966
		internal static string holdoutSeedColumn = "HOLDOUT_SEED";

		// Token: 0x040007AF RID: 1967
		internal static string holdoutActualSizeColumn = "HOLDOUT_ACTUAL_SIZE";
	}
}
