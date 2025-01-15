using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C6 RID: 198
	public sealed class MiningService : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0002C834 File Offset: 0x0002AA34
		internal MiningService(DataRow miningServiceRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, miningServiceRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002C863 File Offset: 0x0002AA63
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002C86B File Offset: 0x0002AA6B
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.miningServiceNameColumn).ToString();
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002C882 File Offset: 0x0002AA82
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.descriptionColumn).ToString();
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002C899 File Offset: 0x0002AA99
		public string ViewerType
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.viewerTypeColumn).ToString();
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
		public string DisplayName
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.displayNameColumn).ToString();
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002C8C7 File Offset: 0x0002AAC7
		public string Guid
		{
			get
			{
				return AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.guidColumn).ToString();
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002C8DE File Offset: 0x0002AADE
		public int PredictionLimit
		{
			get
			{
				return Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.predictionLimitColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002C8FC File Offset: 0x0002AAFC
		public MiningServiceTrainingComplexity TrainingComplexity
		{
			get
			{
				return (MiningServiceTrainingComplexity)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.trainingComplexityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002C91A File Offset: 0x0002AB1A
		public MiningServicePredictionComplexity PredictionComplexity
		{
			get
			{
				return (MiningServicePredictionComplexity)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.predictionComplexityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002C938 File Offset: 0x0002AB38
		public MiningServiceExpectedQuality ExpectedQuality
		{
			get
			{
				return (MiningServiceExpectedQuality)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.expectedQualityColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002C956 File Offset: 0x0002AB56
		public MiningServiceScaling Scaling
		{
			get
			{
				return (MiningServiceScaling)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.scalingColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002C974 File Offset: 0x0002AB74
		public MiningServiceControl Control
		{
			get
			{
				return (MiningServiceControl)Convert.ToInt32(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.controlColumn).ToString(), 10);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002C992 File Offset: 0x0002AB92
		public bool AllowsIncrementalInsert
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowIncrementalInsertColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002C9AE File Offset: 0x0002ABAE
		public bool AllowsPMMLInitialization
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowPMMLInitializationColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002C9CA File Offset: 0x0002ABCA
		public bool AllowsDuplicateKey
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.allowDuplicateKeyColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002C9E6 File Offset: 0x0002ABE6
		public bool SupportsDMDimensions
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportsDMDimensionsColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002CA02 File Offset: 0x0002AC02
		public bool SupportsDrillthrough
		{
			get
			{
				return Convert.ToBoolean(AdomdUtils.GetProperty(this.MiningServiceRow, MiningService.supportsDrillthroughColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002CA20 File Offset: 0x0002AC20
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002CA80 File Offset: 0x0002AC80
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002CB30 File Offset: 0x0002AD30
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002CB88 File Offset: 0x0002AD88
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0002CB90 File Offset: 0x0002AD90
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002CBE6 File Offset: 0x0002ADE6
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002CBF3 File Offset: 0x0002ADF3
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0002CC00 File Offset: 0x0002AE00
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0002CC0E File Offset: 0x0002AE0E
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0002CC1B File Offset: 0x0002AE1B
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0002CC29 File Offset: 0x0002AE29
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0002CC2C File Offset: 0x0002AE2C
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0002CC2E File Offset: 0x0002AE2E
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002CC36 File Offset: 0x0002AE36
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002CC39 File Offset: 0x0002AE39
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0002CC41 File Offset: 0x0002AE41
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0002CC49 File Offset: 0x0002AE49
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002CC56 File Offset: 0x0002AE56
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002CC63 File Offset: 0x0002AE63
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002CC6B File Offset: 0x0002AE6B
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002CC73 File Offset: 0x0002AE73
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningService);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002CC7F File Offset: 0x0002AE7F
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002CCA2 File Offset: 0x0002AEA2
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002CCB0 File Offset: 0x0002AEB0
		public static bool operator ==(MiningService o1, MiningService o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CCB9 File Offset: 0x0002AEB9
		public static bool operator !=(MiningService o1, MiningService o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002CCC5 File Offset: 0x0002AEC5
		internal DataRow MiningServiceRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002CCD7 File Offset: 0x0002AED7
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002CCDF File Offset: 0x0002AEDF
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x0400075A RID: 1882
		private BaseObjectData baseData;

		// Token: 0x0400075B RID: 1883
		private DateTime populationTime;

		// Token: 0x0400075C RID: 1884
		private MiningServiceParameterCollection miningServiceParameters;

		// Token: 0x0400075D RID: 1885
		private PropertyCollection propertyCollection;

		// Token: 0x0400075E RID: 1886
		private int hashCode;

		// Token: 0x0400075F RID: 1887
		private bool hashCodeCalculated;

		// Token: 0x04000760 RID: 1888
		internal static string miningServiceNameColumn = "SERVICE_NAME";

		// Token: 0x04000761 RID: 1889
		internal static string miningServiceNameRest = MiningService.miningServiceNameColumn;

		// Token: 0x04000762 RID: 1890
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000763 RID: 1891
		internal static string viewerTypeColumn = "VIEWER_TYPE";

		// Token: 0x04000764 RID: 1892
		internal static string displayNameColumn = "SERVICE_DISPLAY_NAME";

		// Token: 0x04000765 RID: 1893
		internal static string guidColumn = "GUID";

		// Token: 0x04000766 RID: 1894
		internal static string predictionLimitColumn = "PREDICTION_LIMIT";

		// Token: 0x04000767 RID: 1895
		internal static string supportedDistributionFlagsColumn = "SUPPORTED_DISTRIBUTION_FLAGS";

		// Token: 0x04000768 RID: 1896
		internal static string supportedInputContentTypesColumn = "SUPPORTED_INPUT_CONTENT_TYPES";

		// Token: 0x04000769 RID: 1897
		internal static string supportedPredictionContentTypesColumn = "SUPPORTED_PREDICTION_CONTENT_TYPES";

		// Token: 0x0400076A RID: 1898
		internal static string supportedModelingFlagsColumn = "SUPPORTED_MODELING_FLAGS";

		// Token: 0x0400076B RID: 1899
		internal static string trainingComplexityColumn = "TRAINING_COMPLEXITY";

		// Token: 0x0400076C RID: 1900
		internal static string predictionComplexityColumn = "PREDICTION_COMPLEXITY";

		// Token: 0x0400076D RID: 1901
		internal static string expectedQualityColumn = "EXPECTED_QUALITY";

		// Token: 0x0400076E RID: 1902
		internal static string scalingColumn = "SCALING";

		// Token: 0x0400076F RID: 1903
		internal static string controlColumn = "CONTROL";

		// Token: 0x04000770 RID: 1904
		internal static string allowIncrementalInsertColumn = "ALLOW_INCREMENTAL_INSERT";

		// Token: 0x04000771 RID: 1905
		internal static string allowPMMLInitializationColumn = "ALLOW_PMML_INITIALIZATION";

		// Token: 0x04000772 RID: 1906
		internal static string allowDuplicateKeyColumn = "ALLOW_DUPLICATE_KEY";

		// Token: 0x04000773 RID: 1907
		internal static string supportsDMDimensionsColumn = "MSOLAP_SUPPORTS_DATA_MINING_DIMENSIONS";

		// Token: 0x04000774 RID: 1908
		internal static string supportsDrillthroughColumn = "MSOLAP_SUPPORTS_DRILLTHROUGH";
	}
}
