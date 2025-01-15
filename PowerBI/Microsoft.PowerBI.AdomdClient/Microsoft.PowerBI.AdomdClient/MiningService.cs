using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C6 RID: 198
	public sealed class MiningService : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0002C504 File Offset: 0x0002A704
		internal MiningService(DataRow miningServiceRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningServiceRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002C533 File Offset: 0x0002A733
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002C53B File Offset: 0x0002A73B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.miningServiceNameColumn).ToString();
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002C552 File Offset: 0x0002A752
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.descriptionColumn).ToString();
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002C569 File Offset: 0x0002A769
		public string ViewerType
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.viewerTypeColumn).ToString();
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002C580 File Offset: 0x0002A780
		public string DisplayName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.displayNameColumn).ToString();
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002C597 File Offset: 0x0002A797
		public string Guid
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.guidColumn).ToString();
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002C5AE File Offset: 0x0002A7AE
		public int PredictionLimit
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.predictionLimitColumn).ToString(), 10);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002C5CC File Offset: 0x0002A7CC
		public MiningServiceTrainingComplexity TrainingComplexity
		{
			get
			{
				return (MiningServiceTrainingComplexity)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.trainingComplexityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002C5EA File Offset: 0x0002A7EA
		public MiningServicePredictionComplexity PredictionComplexity
		{
			get
			{
				return (MiningServicePredictionComplexity)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.predictionComplexityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002C608 File Offset: 0x0002A808
		public MiningServiceExpectedQuality ExpectedQuality
		{
			get
			{
				return (MiningServiceExpectedQuality)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.expectedQualityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002C626 File Offset: 0x0002A826
		public MiningServiceScaling Scaling
		{
			get
			{
				return (MiningServiceScaling)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.scalingColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002C644 File Offset: 0x0002A844
		public MiningServiceControl Control
		{
			get
			{
				return (MiningServiceControl)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.controlColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002C662 File Offset: 0x0002A862
		public bool AllowsIncrementalInsert
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowIncrementalInsertColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002C67E File Offset: 0x0002A87E
		public bool AllowsPMMLInitialization
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowPMMLInitializationColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002C69A File Offset: 0x0002A89A
		public bool AllowsDuplicateKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowDuplicateKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002C6B6 File Offset: 0x0002A8B6
		public bool SupportsDMDimensions
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportsDMDimensionsColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002C6D2 File Offset: 0x0002A8D2
		public bool SupportsDrillthrough
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportsDrillthroughColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
		public MiningColumnDistribution[] SupportedDistributionFlags
		{
			get
			{
				string[] array = AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportedDistributionFlagsColumn).ToString().Split(new char[] { ',' });
				MiningColumnDistribution[] array2 = new MiningColumnDistribution[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array2[i] = MiningModelColumn.DistributionFromString(text.Trim());
				}
				return array2;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002C750 File Offset: 0x0002A950
		public string[] SupportedInputContentTypes
		{
			get
			{
				string[] array = AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportedInputContentTypesColumn).ToString().Split(new char[] { ',' });
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array2[i] = text.Trim();
				}
				return array2;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002C7A8 File Offset: 0x0002A9A8
		public string[] SupportedPredictionContentTypes
		{
			get
			{
				string[] array = AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportedPredictionContentTypesColumn).ToString().Split(new char[] { ',' });
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array2[i] = text.Trim();
				}
				return array2;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002C800 File Offset: 0x0002AA00
		public string[] SupportedModelingFlags
		{
			get
			{
				string[] array = AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportedModelingFlagsColumn).ToString().Split(new char[] { ',' });
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					array2[i] = text.Trim();
				}
				return array2;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002C858 File Offset: 0x0002AA58
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002C860 File Offset: 0x0002AA60
		public MiningServiceParameterCollection AvailableParameters
		{
			get
			{
				if (this.miningServiceParameters == null)
				{
					this.miningServiceParameters = new MiningServiceParameterCollection(this.Connection, this);
				}
				else
				{
					this.miningServiceParameters.CollectionInternal.CheckCache();
				}
				return this.miningServiceParameters;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002C894 File Offset: 0x0002AA94
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new PropertyCollection(this.MiningServiceRow, this);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002C8B6 File Offset: 0x0002AAB6
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002C8C3 File Offset: 0x0002AAC3
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0002C8D0 File Offset: 0x0002AAD0
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002C8DE File Offset: 0x0002AADE
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0002C8EB File Offset: 0x0002AAEB
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002C8F9 File Offset: 0x0002AAF9
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0002C8FC File Offset: 0x0002AAFC
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002C8FE File Offset: 0x0002AAFE
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002C906 File Offset: 0x0002AB06
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002C909 File Offset: 0x0002AB09
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002C911 File Offset: 0x0002AB11
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0002C919 File Offset: 0x0002AB19
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002C926 File Offset: 0x0002AB26
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002C933 File Offset: 0x0002AB33
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002C93B File Offset: 0x0002AB3B
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0002C943 File Offset: 0x0002AB43
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningService);
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002C94F File Offset: 0x0002AB4F
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002C972 File Offset: 0x0002AB72
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002C980 File Offset: 0x0002AB80
		public static bool operator ==(MiningService o1, MiningService o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002C989 File Offset: 0x0002AB89
		public static bool operator !=(MiningService o1, MiningService o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0002C995 File Offset: 0x0002AB95
		internal DataRow MiningServiceRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002C9A7 File Offset: 0x0002ABA7
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002C9AF File Offset: 0x0002ABAF
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x0400074D RID: 1869
		private BaseObjectData baseData;

		// Token: 0x0400074E RID: 1870
		private DateTime populationTime;

		// Token: 0x0400074F RID: 1871
		private MiningServiceParameterCollection miningServiceParameters;

		// Token: 0x04000750 RID: 1872
		private PropertyCollection propertyCollection;

		// Token: 0x04000751 RID: 1873
		private int hashCode;

		// Token: 0x04000752 RID: 1874
		private bool hashCodeCalculated;

		// Token: 0x04000753 RID: 1875
		internal static string miningServiceNameColumn = "SERVICE_NAME";

		// Token: 0x04000754 RID: 1876
		internal static string miningServiceNameRest = MiningService.miningServiceNameColumn;

		// Token: 0x04000755 RID: 1877
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000756 RID: 1878
		internal static string viewerTypeColumn = "VIEWER_TYPE";

		// Token: 0x04000757 RID: 1879
		internal static string displayNameColumn = "SERVICE_DISPLAY_NAME";

		// Token: 0x04000758 RID: 1880
		internal static string guidColumn = "GUID";

		// Token: 0x04000759 RID: 1881
		internal static string predictionLimitColumn = "PREDICTION_LIMIT";

		// Token: 0x0400075A RID: 1882
		internal static string supportedDistributionFlagsColumn = "SUPPORTED_DISTRIBUTION_FLAGS";

		// Token: 0x0400075B RID: 1883
		internal static string supportedInputContentTypesColumn = "SUPPORTED_INPUT_CONTENT_TYPES";

		// Token: 0x0400075C RID: 1884
		internal static string supportedPredictionContentTypesColumn = "SUPPORTED_PREDICTION_CONTENT_TYPES";

		// Token: 0x0400075D RID: 1885
		internal static string supportedModelingFlagsColumn = "SUPPORTED_MODELING_FLAGS";

		// Token: 0x0400075E RID: 1886
		internal static string trainingComplexityColumn = "TRAINING_COMPLEXITY";

		// Token: 0x0400075F RID: 1887
		internal static string predictionComplexityColumn = "PREDICTION_COMPLEXITY";

		// Token: 0x04000760 RID: 1888
		internal static string expectedQualityColumn = "EXPECTED_QUALITY";

		// Token: 0x04000761 RID: 1889
		internal static string scalingColumn = "SCALING";

		// Token: 0x04000762 RID: 1890
		internal static string controlColumn = "CONTROL";

		// Token: 0x04000763 RID: 1891
		internal static string allowIncrementalInsertColumn = "ALLOW_INCREMENTAL_INSERT";

		// Token: 0x04000764 RID: 1892
		internal static string allowPMMLInitializationColumn = "ALLOW_PMML_INITIALIZATION";

		// Token: 0x04000765 RID: 1893
		internal static string allowDuplicateKeyColumn = "ALLOW_DUPLICATE_KEY";

		// Token: 0x04000766 RID: 1894
		internal static string supportsDMDimensionsColumn = "MSOLAP_SUPPORTS_DATA_MINING_DIMENSIONS";

		// Token: 0x04000767 RID: 1895
		internal static string supportsDrillthroughColumn = "MSOLAP_SUPPORTS_DRILLTHROUGH";
	}
}
