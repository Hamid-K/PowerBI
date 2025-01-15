using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D4 RID: 212
	public sealed class MiningStructureColumn : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002D708 File Offset: 0x0002B908
		internal MiningStructureColumn(AdomdConnection connection, DataRow miningStructureColumnRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningStructureColumnRow, parentObject, null, catalog, sessionId);
			this.columns = new MiningStructureColumnCollection(connection, this);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002D73D File Offset: 0x0002B93D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002D745 File Offset: 0x0002B945
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnNameColumn).ToString();
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002D75C File Offset: 0x0002B95C
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002D7C5 File Offset: 0x0002B9C5
		public string Flags
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnModelingFlag).ToString();
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002D7F3 File Offset: 0x0002B9F3
		public string Content
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.contentColumn).ToString();
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0002D80A File Offset: 0x0002BA0A
		public MiningColumnType Type
		{
			get
			{
				return MiningModelColumn.DBTYPEToMiningColumnType(Convert.ToUInt32(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.dataTypeColumn).ToString(), 10));
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002D82D File Offset: 0x0002BA2D
		public MiningColumnDistribution Distribution
		{
			get
			{
				return MiningModelColumn.DistributionFromString(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.distributionColumn).ToString());
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0002D849 File Offset: 0x0002BA49
		public bool IsRelatedToKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.isRelatedToKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002D865 File Offset: 0x0002BA65
		public string RelatedAttribute
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.relatedAttributeColumn).ToString();
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002D87C File Offset: 0x0002BA7C
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.isProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002D898 File Offset: 0x0002BA98
		public string ContainingColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.containingColumn).ToString();
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002D8AF File Offset: 0x0002BAAF
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002D8B7 File Offset: 0x0002BAB7
		public DateTime LastUpdated
		{
			get
			{
				return this.ParentMiningStructure.LastUpdated;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002D8C4 File Offset: 0x0002BAC4
		public DateTime LastProcessed
		{
			get
			{
				return this.ParentMiningStructure.LastProcessed;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002D911 File Offset: 0x0002BB11
		public object Parent
		{
			get
			{
				return this.baseData.ParentObject;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002D91E File Offset: 0x0002BB1E
		public MiningStructureColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002D926 File Offset: 0x0002BB26
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002D948 File Offset: 0x0002BB48
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002D955 File Offset: 0x0002BB55
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0002D962 File Offset: 0x0002BB62
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

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002D970 File Offset: 0x0002BB70
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002D97D File Offset: 0x0002BB7D
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002D98B File Offset: 0x0002BB8B
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0002D998 File Offset: 0x0002BB98
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002D9A6 File Offset: 0x0002BBA6
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningStructureColumn;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002D9BC File Offset: 0x0002BBBC
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningStructureColumnRow, MiningStructureColumn.miningStructureColumnNameColumn).ToString();
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002D9D3 File Offset: 0x0002BBD3
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002D9E0 File Offset: 0x0002BBE0
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002D9ED File Offset: 0x0002BBED
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002D9FA File Offset: 0x0002BBFA
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002DA02 File Offset: 0x0002BC02
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002DA0A File Offset: 0x0002BC0A
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningStructureColumn);
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002DA16 File Offset: 0x0002BC16
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002DA39 File Offset: 0x0002BC39
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002DA47 File Offset: 0x0002BC47
		public static bool operator ==(MiningStructureColumn o1, MiningStructureColumn o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002DA50 File Offset: 0x0002BC50
		public static bool operator !=(MiningStructureColumn o1, MiningStructureColumn o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		internal DataRow MiningStructureColumnRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x040007A6 RID: 1958
		private BaseObjectData baseData;

		// Token: 0x040007A7 RID: 1959
		private PropertyCollection propertiesCollection;

		// Token: 0x040007A8 RID: 1960
		private MiningStructureColumnCollection columns;

		// Token: 0x040007A9 RID: 1961
		private int hashCode;

		// Token: 0x040007AA RID: 1962
		private bool hashCodeCalculated;

		// Token: 0x040007AB RID: 1963
		internal static string miningStructureColumnNameColumn = "COLUMN_NAME";

		// Token: 0x040007AC RID: 1964
		internal static string miningStructureColumnModelingFlag = "MODELING_FLAG";

		// Token: 0x040007AD RID: 1965
		private static string contentColumn = "CONTENT_TYPE";

		// Token: 0x040007AE RID: 1966
		private static string dataTypeColumn = "DATA_TYPE";

		// Token: 0x040007AF RID: 1967
		private static string containingColumn = "CONTAINING_COLUMN";

		// Token: 0x040007B0 RID: 1968
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x040007B1 RID: 1969
		private static string distributionColumn = "DISTRIBUTION";

		// Token: 0x040007B2 RID: 1970
		private static string isRelatedToKeyColumn = "IS_RELATED_TO_KEY";

		// Token: 0x040007B3 RID: 1971
		private static string relatedAttributeColumn = "RELATED_ATTRIBUTE";

		// Token: 0x040007B4 RID: 1972
		private static string isProcessedColumn = "IS_POPULATED";
	}
}
