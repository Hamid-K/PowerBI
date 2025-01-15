using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BE RID: 190
	public sealed class MiningModelColumn : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002B978 File Offset: 0x00029B78
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

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002B9D8 File Offset: 0x00029BD8
		internal MiningModelColumn(AdomdConnection connection, DataRow miningModelColumnRow, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningModelColumnRow, parentObject, null, catalog, sessionId);
			this.columns = new MiningModelColumnCollection(connection, this);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002BA0D File Offset: 0x00029C0D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002BA15 File Offset: 0x00029C15
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnNameColumn).ToString();
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002BA2C File Offset: 0x00029C2C
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0002BA95 File Offset: 0x00029C95
		public string Flags
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnModelingFlag).ToString();
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002BAAC File Offset: 0x00029CAC
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.descriptionColumn).ToString();
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002BAC3 File Offset: 0x00029CC3
		public MiningColumnDistribution Distribution
		{
			get
			{
				return MiningModelColumn.DistributionFromString(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.distributionColumn).ToString());
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002BADF File Offset: 0x00029CDF
		public bool IsInput
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isInputColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002BAFB File Offset: 0x00029CFB
		public bool IsPredictable
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isPredictableColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002BB17 File Offset: 0x00029D17
		public bool IsTable
		{
			get
			{
				return this.Type == MiningColumnType.Table;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002BB25 File Offset: 0x00029D25
		public bool IsRelatedToKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isRelatedToKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002BB41 File Offset: 0x00029D41
		public string RelatedAttribute
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.relatedAttributeColumn).ToString();
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002BB58 File Offset: 0x00029D58
		public string Content
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.contentColumn).ToString();
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002BB6F File Offset: 0x00029D6F
		public MiningColumnType Type
		{
			get
			{
				return MiningModelColumn.DBTYPEToMiningColumnType(Convert.ToUInt32(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.dataTypeColumn).ToString(), 10));
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002BB92 File Offset: 0x00029D92
		public string ContainingColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.containingColumn).ToString();
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002BBA9 File Offset: 0x00029DA9
		public string UniqueName
		{
			get
			{
				return ((IAdomdBaseObject)this).InternalUniqueName;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002BBB1 File Offset: 0x00029DB1
		public bool IsProcessed
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.isProcessedColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002BBCD File Offset: 0x00029DCD
		public DateTime LastUpdated
		{
			get
			{
				return this.ParentMiningModel.LastUpdated;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002BBDA File Offset: 0x00029DDA
		public DateTime LastProcessed
		{
			get
			{
				return this.ParentMiningModel.LastProcessed;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002BBE7 File Offset: 0x00029DE7
		public double PredictionScore
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.predictionScoreColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002BC03 File Offset: 0x00029E03
		public string StructureColumn
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.sourceColumn).ToString();
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002BC1A File Offset: 0x00029E1A
		public string Filter
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.filterColumn).ToString();
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002BC34 File Offset: 0x00029E34
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002BC71 File Offset: 0x00029E71
		public object Parent
		{
			get
			{
				return this.baseData.ParentObject;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002BC7E File Offset: 0x00029E7E
		public MiningModelColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002BC86 File Offset: 0x00029E86
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002BCA2 File Offset: 0x00029EA2
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002BCC4 File Offset: 0x00029EC4
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002BCD1 File Offset: 0x00029ED1
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002BCDE File Offset: 0x00029EDE
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002BCEC File Offset: 0x00029EEC
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002BCF9 File Offset: 0x00029EF9
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002BD07 File Offset: 0x00029F07
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0002BD14 File Offset: 0x00029F14
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

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002BD22 File Offset: 0x00029F22
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.baseData.ParentObject.CubeName;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002BD34 File Offset: 0x00029F34
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeMiningModelColumn;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002BD38 File Offset: 0x00029F38
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningModelColumnRow, MiningModelColumn.miningModelColumnNameColumn).ToString();
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002BD4F File Offset: 0x00029F4F
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002BD5C File Offset: 0x00029F5C
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002BD69 File Offset: 0x00029F69
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002BD76 File Offset: 0x00029F76
		string IMetadataObject.CubeName
		{
			get
			{
				return ((IAdomdBaseObject)this).CubeName;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002BD7E File Offset: 0x00029F7E
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.UniqueName;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0002BD86 File Offset: 0x00029F86
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningModelColumn);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002BD92 File Offset: 0x00029F92
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002BDB5 File Offset: 0x00029FB5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002BDC3 File Offset: 0x00029FC3
		public static bool operator ==(MiningModelColumn o1, MiningModelColumn o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002BDCC File Offset: 0x00029FCC
		public static bool operator !=(MiningModelColumn o1, MiningModelColumn o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		internal DataRow MiningModelColumnRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002BDEC File Offset: 0x00029FEC
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

		// Token: 0x04000709 RID: 1801
		private BaseObjectData baseData;

		// Token: 0x0400070A RID: 1802
		private PropertyCollection propertiesCollection;

		// Token: 0x0400070B RID: 1803
		private MiningModelColumnCollection columns;

		// Token: 0x0400070C RID: 1804
		private MiningValueCollection values;

		// Token: 0x0400070D RID: 1805
		private int hashCode;

		// Token: 0x0400070E RID: 1806
		private bool hashCodeCalculated;

		// Token: 0x0400070F RID: 1807
		internal static string miningModelColumnNameColumn = "COLUMN_NAME";

		// Token: 0x04000710 RID: 1808
		internal static string miningModelColumnModelingFlag = "MODELING_FLAG";

		// Token: 0x04000711 RID: 1809
		internal static string isInputColumn = "IS_INPUT";

		// Token: 0x04000712 RID: 1810
		internal static string isPredictableColumn = "IS_PREDICTABLE";

		// Token: 0x04000713 RID: 1811
		private static string contentColumn = "CONTENT_TYPE";

		// Token: 0x04000714 RID: 1812
		private static string dataTypeColumn = "DATA_TYPE";

		// Token: 0x04000715 RID: 1813
		private static string containingColumn = "CONTAINING_COLUMN";

		// Token: 0x04000716 RID: 1814
		private static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000717 RID: 1815
		private static string distributionColumn = "DISTRIBUTION";

		// Token: 0x04000718 RID: 1816
		private static string isRelatedToKeyColumn = "IS_RELATED_TO_KEY";

		// Token: 0x04000719 RID: 1817
		private static string relatedAttributeColumn = "RELATED_ATTRIBUTE";

		// Token: 0x0400071A RID: 1818
		private static string isProcessedColumn = "IS_POPULATED";

		// Token: 0x0400071B RID: 1819
		private static string predictionScoreColumn = "PREDICTION_SCORE";

		// Token: 0x0400071C RID: 1820
		private static string sourceColumn = "SOURCE_COLUMN";

		// Token: 0x0400071D RID: 1821
		private static string filterColumn = "FILTER";
	}
}
