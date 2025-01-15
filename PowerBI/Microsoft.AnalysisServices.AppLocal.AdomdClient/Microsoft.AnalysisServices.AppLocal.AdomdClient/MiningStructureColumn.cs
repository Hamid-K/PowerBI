using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D4 RID: 212
	public sealed class MiningStructureColumn : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x0002DA38 File Offset: 0x0002BC38
		internal MiningStructureColumn(AdomdConnection connection, DataRow miningStructureColumnRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningStructureColumnRow, parentObject, null, catalog, sessionId);
			this.columns = new MiningStructureColumnCollection(connection, this);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002DA6D File Offset: 0x0002BC6D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002DA75 File Offset: 0x0002BC75
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnNameColumn).ToString();
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002DA8C File Offset: 0x0002BC8C
		public string FullyQualifiedName
		{
			get
			{
				string text;
				if (this.ContainingColumn.Length == 0)
				{
					text = "[" + this.Name + "]";
				}
				else
				{
					text = string.Concat(new string[] { "[", this.ContainingColumn, "].[", this.Name, "]" });
				}
				return text;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002DAF5 File Offset: 0x0002BCF5
		public string Flags
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnModelingFlag).ToString();
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002DB23 File Offset: 0x0002BD23
		public string Content
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.contentColumn).ToString();
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002DB3A File Offset: 0x0002BD3A
		public MiningColumnType Type
		{
			get
			{
				return MiningModelColumn.DBTYPEToMiningColumnType(Convert.ToUInt32(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.dataTypeColumn).ToString(), 10));
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002DB5D File Offset: 0x0002BD5D
		public MiningColumnDistribution Distribution
		{
			get
			{
				return MiningModelColumn.DistributionFromString(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.distributionColumn).ToString());
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0002DB79 File Offset: 0x0002BD79
		public bool IsRelatedToKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.isRelatedToKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002DB95 File Offset: 0x0002BD95
		public string RelatedAttribute
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.relatedAttributeColumn).ToString();
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.isProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		public string ContainingColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.containingColumn).ToString();
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0002DBDF File Offset: 0x0002BDDF
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002DBE7 File Offset: 0x0002BDE7
		public DateTime LastUpdated
		{
			get
			{
				return this.ParentMiningStructure.LastUpdated;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
		public DateTime LastProcessed
		{
			get
			{
				return this.ParentMiningStructure.LastProcessed;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002DC04 File Offset: 0x0002BE04
		public MiningStructure ParentMiningStructure
		{
			get
			{
				object parentObject = this.baseData.ParentObject;
				if (parentObject is MiningStructure)
				{
					return (MiningStructure)parentObject;
				}
				if (parentObject is MiningStructureColumn)
				{
					return ((MiningStructureColumn)parentObject).ParentMiningStructure;
				}
				return null;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002DC41 File Offset: 0x0002BE41
		public object Parent
		{
			get
			{
				return this.baseData.ParentObject;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002DC4E File Offset: 0x0002BE4E
		public MiningStructureColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002DC56 File Offset: 0x0002BE56
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.MiningStructureColumnRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002DC78 File Offset: 0x0002BE78
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002DC85 File Offset: 0x0002BE85
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0002DC92 File Offset: 0x0002BE92
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0002DCAD File Offset: 0x0002BEAD
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

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002DCBB File Offset: 0x0002BEBB
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0002DCC8 File Offset: 0x0002BEC8
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

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0002DCD6 File Offset: 0x0002BED6
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningStructureColumn;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnNameColumn).ToString();
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002DD03 File Offset: 0x0002BF03
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002DD10 File Offset: 0x0002BF10
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002DD1D File Offset: 0x0002BF1D
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002DD2A File Offset: 0x0002BF2A
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002DD32 File Offset: 0x0002BF32
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002DD3A File Offset: 0x0002BF3A
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningStructureColumn);
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002DD46 File Offset: 0x0002BF46
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002DD69 File Offset: 0x0002BF69
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002DD77 File Offset: 0x0002BF77
		public static bool operator ==(MiningStructureColumn o1, MiningStructureColumn o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002DD80 File Offset: 0x0002BF80
		public static bool operator !=(MiningStructureColumn o1, MiningStructureColumn o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		internal DataRow MiningStructureColumnRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x040007B3 RID: 1971
		private BaseObjectData baseData;

		// Token: 0x040007B4 RID: 1972
		private PropertyCollection propertiesCollection;

		// Token: 0x040007B5 RID: 1973
		private MiningStructureColumnCollection columns;

		// Token: 0x040007B6 RID: 1974
		private int hashCode;

		// Token: 0x040007B7 RID: 1975
		private bool hashCodeCalculated;

		// Token: 0x040007B8 RID: 1976
		internal static string miningStructureColumnNameColumn = "COLUMN_NAME";

		// Token: 0x040007B9 RID: 1977
		internal static string miningStructureColumnModelingFlag = "MODELING_FLAG";

		// Token: 0x040007BA RID: 1978
		private static string contentColumn = "CONTENT_TYPE";

		// Token: 0x040007BB RID: 1979
		private static string dataTypeColumn = "DATA_TYPE";

		// Token: 0x040007BC RID: 1980
		private static string containingColumn = "CONTAINING_COLUMN";

		// Token: 0x040007BD RID: 1981
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040007BE RID: 1982
		private static string distributionColumn = "DISTRIBUTION";

		// Token: 0x040007BF RID: 1983
		private static string isRelatedToKeyColumn = "IS_RELATED_TO_KEY";

		// Token: 0x040007C0 RID: 1984
		private static string relatedAttributeColumn = "RELATED_ATTRIBUTE";

		// Token: 0x040007C1 RID: 1985
		private static string isProcessedColumn = "IS_POPULATED";
	}
}
