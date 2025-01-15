using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BE RID: 190
	public sealed class MiningModelColumn : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002BCA8 File Offset: 0x00029EA8
		internal static MiningColumnType DBTYPEToMiningColumnType(uint type)
		{
			MiningColumnType miningColumnType = MiningColumnType.Missing;
			if (type <= 7U)
			{
				if (type != 0U)
				{
					if (type != 5U)
					{
						if (type == 7U)
						{
							miningColumnType = MiningColumnType.Date;
						}
					}
					else
					{
						miningColumnType = MiningColumnType.Double;
					}
				}
				else
				{
					miningColumnType = MiningColumnType.Missing;
				}
			}
			else if (type <= 20U)
			{
				if (type != 11U)
				{
					if (type == 20U)
					{
						miningColumnType = MiningColumnType.Long;
					}
				}
				else
				{
					miningColumnType = MiningColumnType.Boolean;
				}
			}
			else if (type != 130U)
			{
				if (type == 136U)
				{
					miningColumnType = MiningColumnType.Table;
				}
			}
			else
			{
				miningColumnType = MiningColumnType.Text;
			}
			return miningColumnType;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002BD08 File Offset: 0x00029F08
		internal MiningModelColumn(AdomdConnection connection, DataRow miningModelColumnRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningModelColumnRow, parentObject, null, catalog, sessionId);
			this.columns = new MiningModelColumnCollection(connection, this);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002BD3D File Offset: 0x00029F3D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002BD45 File Offset: 0x00029F45
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnNameColumn).ToString();
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002BD5C File Offset: 0x00029F5C
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

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002BDC5 File Offset: 0x00029FC5
		public string Flags
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnModelingFlag).ToString();
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002BDDC File Offset: 0x00029FDC
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.descriptionColumn).ToString();
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002BDF3 File Offset: 0x00029FF3
		public MiningColumnDistribution Distribution
		{
			get
			{
				return MiningModelColumn.DistributionFromString(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.distributionColumn).ToString());
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002BE0F File Offset: 0x0002A00F
		public bool IsInput
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isInputColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002BE2B File Offset: 0x0002A02B
		public bool IsPredictable
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isPredictableColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002BE47 File Offset: 0x0002A047
		public bool IsTable
		{
			get
			{
				return this.Type == MiningColumnType.Table;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002BE55 File Offset: 0x0002A055
		public bool IsRelatedToKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isRelatedToKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002BE71 File Offset: 0x0002A071
		public string RelatedAttribute
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.relatedAttributeColumn).ToString();
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002BE88 File Offset: 0x0002A088
		public string Content
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.contentColumn).ToString();
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002BE9F File Offset: 0x0002A09F
		public MiningColumnType Type
		{
			get
			{
				return MiningModelColumn.DBTYPEToMiningColumnType(Convert.ToUInt32(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.dataTypeColumn).ToString(), 10));
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002BEC2 File Offset: 0x0002A0C2
		public string ContainingColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.containingColumn).ToString();
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002BED9 File Offset: 0x0002A0D9
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002BEE1 File Offset: 0x0002A0E1
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002BEFD File Offset: 0x0002A0FD
		public DateTime LastUpdated
		{
			get
			{
				return this.ParentMiningModel.LastUpdated;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002BF0A File Offset: 0x0002A10A
		public DateTime LastProcessed
		{
			get
			{
				return this.ParentMiningModel.LastProcessed;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002BF17 File Offset: 0x0002A117
		public double PredictionScore
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.predictionScoreColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002BF33 File Offset: 0x0002A133
		public string StructureColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.sourceColumn).ToString();
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002BF4A File Offset: 0x0002A14A
		public string Filter
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.filterColumn).ToString();
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002BF64 File Offset: 0x0002A164
		public MiningModel ParentMiningModel
		{
			get
			{
				object parentObject = this.baseData.ParentObject;
				if (parentObject is MiningModel)
				{
					return (MiningModel)parentObject;
				}
				if (parentObject is MiningModelColumn)
				{
					return ((MiningModelColumn)parentObject).ParentMiningModel;
				}
				return null;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002BFA1 File Offset: 0x0002A1A1
		public object Parent
		{
			get
			{
				return this.baseData.ParentObject;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002BFAE File Offset: 0x0002A1AE
		public MiningModelColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002BFB6 File Offset: 0x0002A1B6
		public MiningValueCollection Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new MiningValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002BFD2 File Offset: 0x0002A1D2
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.MiningModelColumnRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002BFF4 File Offset: 0x0002A1F4
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002C001 File Offset: 0x0002A201
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0002C00E File Offset: 0x0002A20E
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002C01C File Offset: 0x0002A21C
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0002C029 File Offset: 0x0002A229
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002C037 File Offset: 0x0002A237
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002C044 File Offset: 0x0002A244
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002C052 File Offset: 0x0002A252
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002C064 File Offset: 0x0002A264
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningModelColumn;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002C068 File Offset: 0x0002A268
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnNameColumn).ToString();
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002C07F File Offset: 0x0002A27F
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002C08C File Offset: 0x0002A28C
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002C099 File Offset: 0x0002A299
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002C0A6 File Offset: 0x0002A2A6
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002C0AE File Offset: 0x0002A2AE
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002C0B6 File Offset: 0x0002A2B6
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningModelColumn);
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002C0C2 File Offset: 0x0002A2C2
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002C0E5 File Offset: 0x0002A2E5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002C0F3 File Offset: 0x0002A2F3
		public static bool operator ==(MiningModelColumn o1, MiningModelColumn o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002C0FC File Offset: 0x0002A2FC
		public static bool operator !=(MiningModelColumn o1, MiningModelColumn o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002C108 File Offset: 0x0002A308
		internal DataRow MiningModelColumnRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002C11C File Offset: 0x0002A31C
		internal static MiningColumnDistribution DistributionFromString(string strDist)
		{
			if (string.Compare(strDist, "Normal", StringComparison.Ordinal) == 0)
			{
				return MiningColumnDistribution.Normal;
			}
			if (string.Compare(strDist, "Missing", StringComparison.Ordinal) == 0 || strDist.Length == 0)
			{
				return MiningColumnDistribution.Missing;
			}
			if (string.Compare(strDist, "Uniform", StringComparison.Ordinal) == 0)
			{
				return MiningColumnDistribution.Uniform;
			}
			if (string.Compare(strDist, "Normal", StringComparison.Ordinal) == 0)
			{
				return MiningColumnDistribution.Normal;
			}
			if (string.Compare(strDist, "LogNormal", StringComparison.Ordinal) == 0)
			{
				return MiningColumnDistribution.LogNormal;
			}
			return MiningColumnDistribution.Custom;
		}

		// Token: 0x04000716 RID: 1814
		private BaseObjectData baseData;

		// Token: 0x04000717 RID: 1815
		private PropertyCollection propertiesCollection;

		// Token: 0x04000718 RID: 1816
		private MiningModelColumnCollection columns;

		// Token: 0x04000719 RID: 1817
		private MiningValueCollection values;

		// Token: 0x0400071A RID: 1818
		private int hashCode;

		// Token: 0x0400071B RID: 1819
		private bool hashCodeCalculated;

		// Token: 0x0400071C RID: 1820
		internal static string miningModelColumnNameColumn = "COLUMN_NAME";

		// Token: 0x0400071D RID: 1821
		internal static string miningModelColumnModelingFlag = "MODELING_FLAG";

		// Token: 0x0400071E RID: 1822
		internal static string isInputColumn = "IS_INPUT";

		// Token: 0x0400071F RID: 1823
		internal static string isPredictableColumn = "IS_PREDICTABLE";

		// Token: 0x04000720 RID: 1824
		private static string contentColumn = "CONTENT_TYPE";

		// Token: 0x04000721 RID: 1825
		private static string dataTypeColumn = "DATA_TYPE";

		// Token: 0x04000722 RID: 1826
		private static string containingColumn = "CONTAINING_COLUMN";

		// Token: 0x04000723 RID: 1827
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000724 RID: 1828
		private static string distributionColumn = "DISTRIBUTION";

		// Token: 0x04000725 RID: 1829
		private static string isRelatedToKeyColumn = "IS_RELATED_TO_KEY";

		// Token: 0x04000726 RID: 1830
		private static string relatedAttributeColumn = "RELATED_ATTRIBUTE";

		// Token: 0x04000727 RID: 1831
		private static string isProcessedColumn = "IS_POPULATED";

		// Token: 0x04000728 RID: 1832
		private static string predictionScoreColumn = "PREDICTION_SCORE";

		// Token: 0x04000729 RID: 1833
		private static string sourceColumn = "SOURCE_COLUMN";

		// Token: 0x0400072A RID: 1834
		private static string filterColumn = "FILTER";
	}
}
