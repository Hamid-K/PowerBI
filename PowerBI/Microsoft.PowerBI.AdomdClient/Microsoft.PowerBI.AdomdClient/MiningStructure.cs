using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D1 RID: 209
	public sealed class MiningStructure : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D164 File Offset: 0x0002B364
		internal MiningStructure(DataRow miningStructureRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningStructureRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D193 File Offset: 0x0002B393
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002D19B File Offset: 0x0002B39B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.miningStructureNameColumn).ToString();
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002D1B2 File Offset: 0x0002B3B2
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002D1C9 File Offset: 0x0002B3C9
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.isPopulatedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002D1E5 File Offset: 0x0002B3E5
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.lastModifiedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002D201 File Offset: 0x0002B401
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.lastProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002D21D File Offset: 0x0002B41D
		public DateTime Created
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.createdColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002D239 File Offset: 0x0002B439
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0002D273 File Offset: 0x0002B473
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002D27B File Offset: 0x0002B47B
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002D2AF File Offset: 0x0002B4AF
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

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002D2DD File Offset: 0x0002B4DD
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002D2FF File Offset: 0x0002B4FF
		public int HoldoutMaxPercent
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutMaxPercentColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002D31B File Offset: 0x0002B51B
		public long HoldoutMaxCases
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutMaxCasesColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002D337 File Offset: 0x0002B537
		public long HoldoutSeed
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutSeedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002D353 File Offset: 0x0002B553
		public long HoldoutActualSize
		{
			get
			{
				return Convert.ToInt64(AdomdUtils.GetProperty(this.MiningStructureRow, MiningStructure.holdoutActualSizeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D36F File Offset: 0x0002B56F
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002D37C File Offset: 0x0002B57C
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0002D389 File Offset: 0x0002B589
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002D397 File Offset: 0x0002B597
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002D3B2 File Offset: 0x0002B5B2
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0002D3B5 File Offset: 0x0002B5B5
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

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002D3B7 File Offset: 0x0002B5B7
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002D3BF File Offset: 0x0002B5BF
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002D3C2 File Offset: 0x0002B5C2
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002D3CA File Offset: 0x0002B5CA
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002D3D2 File Offset: 0x0002B5D2
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002D3DF File Offset: 0x0002B5DF
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002D3EC File Offset: 0x0002B5EC
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002D3FC File Offset: 0x0002B5FC
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningStructure);
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002D408 File Offset: 0x0002B608
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002D42B File Offset: 0x0002B62B
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002D439 File Offset: 0x0002B639
		public static bool operator ==(MiningStructure o1, MiningStructure o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002D442 File Offset: 0x0002B642
		public static bool operator !=(MiningStructure o1, MiningStructure o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002D44E File Offset: 0x0002B64E
		internal DataRow MiningStructureRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002D460 File Offset: 0x0002B660
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002D468 File Offset: 0x0002B668
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x04000790 RID: 1936
		private BaseObjectData baseData;

		// Token: 0x04000791 RID: 1937
		private DateTime populationTime;

		// Token: 0x04000792 RID: 1938
		private MiningStructureColumnCollection miningStructureColumns;

		// Token: 0x04000793 RID: 1939
		private PropertyCollection propertyCollection;

		// Token: 0x04000794 RID: 1940
		private MiningModelCollection miningModels;

		// Token: 0x04000795 RID: 1941
		private int hashCode;

		// Token: 0x04000796 RID: 1942
		private bool hashCodeCalculated;

		// Token: 0x04000797 RID: 1943
		internal static string miningStructureNameColumn = "STRUCTURE_NAME";

		// Token: 0x04000798 RID: 1944
		internal static string miningStructureNameRest = MiningStructure.miningStructureNameColumn;

		// Token: 0x04000799 RID: 1945
		internal static string miningStructureCaption = "STRUCTURE_CAPTION";

		// Token: 0x0400079A RID: 1946
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x0400079B RID: 1947
		internal static string lastProcessedColumn = "LAST_PROCESSED";

		// Token: 0x0400079C RID: 1948
		internal static string lastModifiedColumn = "DATE_MODIFIED";

		// Token: 0x0400079D RID: 1949
		internal static string createdColumn = "DATE_CREATED";

		// Token: 0x0400079E RID: 1950
		internal static string isPopulatedColumn = "IS_POPULATED";

		// Token: 0x0400079F RID: 1951
		internal static string holdoutMaxPercentColumn = "HOLDOUT_MAXPERCENT";

		// Token: 0x040007A0 RID: 1952
		internal static string holdoutMaxCasesColumn = "HOLDOUT_MAXCASES";

		// Token: 0x040007A1 RID: 1953
		internal static string holdoutSeedColumn = "HOLDOUT_SEED";

		// Token: 0x040007A2 RID: 1954
		internal static string holdoutActualSizeColumn = "HOLDOUT_ACTUAL_SIZE";
	}
}
