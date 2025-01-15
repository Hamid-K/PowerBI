using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200025A RID: 602
	internal static class Options
	{
		// Token: 0x060019B6 RID: 6582 RVA: 0x000339A8 File Offset: 0x00031BA8
		public static RecordValue ValidateOptions(RecordValue options, IEnumerable<string> validOptionKeys, string functionName, IEnumerable<string> documentedOptionKeys = null)
		{
			documentedOptionKeys = documentedOptionKeys ?? validOptionKeys;
			foreach (string text in options.Keys)
			{
				if (!validOptionKeys.Contains(text))
				{
					throw ValueException.NewExpressionError(Strings.InvalidOption(text, functionName, string.Join(", ", documentedOptionKeys.OrderBy((string s) => s).ToArray<string>())), Value.Null, null);
				}
			}
			return options;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00033A54 File Offset: 0x00031C54
		public static bool IsNativeQueryFoldingEnabled(Value parameters, Value options)
		{
			RecordValue recordValue = (parameters.IsRecord ? parameters.AsRecord : null);
			ListValue listValue = (parameters.IsList ? parameters.AsList : null);
			bool flag = parameters.IsNull || (recordValue != null && recordValue.IsEmpty) || (listValue != null && listValue.IsEmpty);
			Value value;
			bool flag2 = !options.IsNull && options.IsRecord && options.AsRecord.TryGetValue("EnableFolding", out value) && value.IsLogical && value.AsBoolean;
			return flag && flag2;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00033AE0 File Offset: 0x00031CE0
		private static bool TryGetMaxDegreeOfParallelism(Value optionValue, out object value)
		{
			int num;
			if (optionValue.IsNumber && optionValue.AsNumber.TryGetInt32(out num) && num >= 0 && num <= 1024)
			{
				value = num;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00033B1E File Offset: 0x00031D1E
		private static bool TryGetNavigationPropertyNameGenerator(Value optionValue, out object value)
		{
			if (optionValue.IsFunction)
			{
				value = NavigationPropertiesHelper.CreateNameGenerator(optionValue.AsFunction);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04000702 RID: 1794
		public const string AllowedTypeNames = "AllowedTypeNames";

		// Token: 0x04000703 RID: 1795
		public const string ApiKeyName = "ApiKeyName";

		// Token: 0x04000704 RID: 1796
		public const string ApiVersion = "ApiVersion";

		// Token: 0x04000705 RID: 1797
		public const string BatchSize = "BatchSize";

		// Token: 0x04000706 RID: 1798
		public const string BinaryCodePage = "BinaryCodePage";

		// Token: 0x04000707 RID: 1799
		public const string BinaryDisplayEncoding = "BinaryDisplayEncoding";

		// Token: 0x04000708 RID: 1800
		public const string BlockSize = "BlockSize";

		// Token: 0x04000709 RID: 1801
		public const string BufferMode = "BufferMode";

		// Token: 0x0400070A RID: 1802
		public const string ClientCertificate = "ClientCertificate";

		// Token: 0x0400070B RID: 1803
		public const string CommandTimeout = "CommandTimeout";

		// Token: 0x0400070C RID: 1804
		public const string Concurrent = "Concurrent";

		// Token: 0x0400070D RID: 1805
		public const string ConcurrentRequests = "ConcurrentRequests";

		// Token: 0x0400070E RID: 1806
		public const string ConnectionTimeout = "ConnectionTimeout";

		// Token: 0x0400070F RID: 1807
		public const string Content = "Content";

		// Token: 0x04000710 RID: 1808
		public const string ContextInfo = "ContextInfo";

		// Token: 0x04000711 RID: 1809
		public const string CreateNavigationProperties = "CreateNavigationProperties";

		// Token: 0x04000712 RID: 1810
		public const string CredentialQuery = "CredentialQuery";

		// Token: 0x04000713 RID: 1811
		public const string Culture = "Culture";

		// Token: 0x04000714 RID: 1812
		public const string Distribution = "Distribution";

		// Token: 0x04000715 RID: 1813
		public const string DisableAppendNoteColumns = "DisableAppendNoteColumns";

		// Token: 0x04000716 RID: 1814
		public const string EnableBatch = "EnableBatch";

		// Token: 0x04000717 RID: 1815
		public const string EnableBulkInsert = "EnableBulkInsert";

		// Token: 0x04000718 RID: 1816
		public const string EnableColumnBinding = "EnableColumnBinding";

		// Token: 0x04000719 RID: 1817
		public const string EnableCrossDatabaseFolding = "EnableCrossDatabaseFolding";

		// Token: 0x0400071A RID: 1818
		public const string EnableFolding = "EnableFolding";

		// Token: 0x0400071B RID: 1819
		public const string EnableStructures = "EnableStructures";

		// Token: 0x0400071C RID: 1820
		public const string Encoding = "Encoding";

		// Token: 0x0400071D RID: 1821
		public const string EndPage = "EndPage";

		// Token: 0x0400071E RID: 1822
		public const string EnforceBorderLines = "EnforceBorderLines";

		// Token: 0x0400071F RID: 1823
		public const string ExcludeBuiltins = "ExcludeBuiltins";

		// Token: 0x04000720 RID: 1824
		public const string ExcludedFromCacheKey = "ExcludedFromCacheKey";

		// Token: 0x04000721 RID: 1825
		public const string ExecutionMode = "ExecutionMode";

		// Token: 0x04000722 RID: 1826
		public const string Format = "Format";

		// Token: 0x04000723 RID: 1827
		public const string FunctionOverloads = "FunctionOverloads";

		// Token: 0x04000724 RID: 1828
		public const string Headers = "Headers";

		// Token: 0x04000725 RID: 1829
		public const string HierarchicalNavigation = "HierarchicalNavigation";

		// Token: 0x04000726 RID: 1830
		public const string Implementation = "Implementation";

		// Token: 0x04000727 RID: 1831
		public const string IgnoreCase = "IgnoreCase";

		// Token: 0x04000728 RID: 1832
		public const string IgnoreSpace = "IgnoreSpace";

		// Token: 0x04000729 RID: 1833
		public const string IncludeAnnotations = "IncludeAnnotations";

		// Token: 0x0400072A RID: 1834
		public const string IncludeDeprecated = "IncludeDeprecated";

		// Token: 0x0400072B RID: 1835
		public const string IncludeExtensions = "IncludeExtensions";

		// Token: 0x0400072C RID: 1836
		public const string IncludeFieldCaptions = "IncludeFieldCaptions";

		// Token: 0x0400072D RID: 1837
		public const string IncludeMetadataAnnotations = "IncludeMetadataAnnotations";

		// Token: 0x0400072E RID: 1838
		public const string IncludeMoreColumns = "IncludeMoreColumns";

		// Token: 0x0400072F RID: 1839
		public const string IsAction = "IsAction";

		// Token: 0x04000730 RID: 1840
		public const string IsOneLake = "IsOneLake";

		// Token: 0x04000731 RID: 1841
		public const string IsRetry = "IsRetry";

		// Token: 0x04000732 RID: 1842
		public const string IsWebBrowserContents = "IsWebBrowserContents";

		// Token: 0x04000733 RID: 1843
		public const string LanguageCode = "LanguageCode";

		// Token: 0x04000734 RID: 1844
		public const string LegacyExtension = "LegacyExtension";

		// Token: 0x04000735 RID: 1845
		public const string ManualCredentials = "ManualCredentials";

		// Token: 0x04000736 RID: 1846
		public const string ManualStatusHandling = "ManualStatusHandling";

		// Token: 0x04000737 RID: 1847
		public const string MaxDegreeOfParallelism = "MaxDegreeOfParallelism";

		// Token: 0x04000738 RID: 1848
		public const string MaxDepth = "MaxDepth";

		// Token: 0x04000739 RID: 1849
		public const string MaxRetryCount = "MaxRetryCount";

		// Token: 0x0400073A RID: 1850
		public const string MaxUriLength = "MaxUriLength";

		// Token: 0x0400073B RID: 1851
		public const string MessageDataDisplayEncoding = "MessageDataDisplayEncoding";

		// Token: 0x0400073C RID: 1852
		public const string MoreColumns = "MoreColumns";

		// Token: 0x0400073D RID: 1853
		public const string MultiPageTables = "MultiPageTables";

		// Token: 0x0400073E RID: 1854
		public const string MultiSubnetFailover = "MultiSubnetFailover";

		// Token: 0x0400073F RID: 1855
		public const string NumberOfMatches = "NumberOfMatches";

		// Token: 0x04000740 RID: 1856
		public const string NavigationPropertyNameGenerator = "NavigationPropertyNameGenerator";

		// Token: 0x04000741 RID: 1857
		public const string NavigationTables = "NavigationTables";

		// Token: 0x04000742 RID: 1858
		public const string ODataVersion = "ODataVersion";

		// Token: 0x04000743 RID: 1859
		public const string OldGuids = "OldGuids";

		// Token: 0x04000744 RID: 1860
		public const string OmitSRID = "OmitSRID";

		// Token: 0x04000745 RID: 1861
		public const string OmitValues = "OmitValues";

		// Token: 0x04000746 RID: 1862
		public const string PackageCollection = "PackageCollection";

		// Token: 0x04000747 RID: 1863
		public const string PercentileMode = "PercentileMode";

		// Token: 0x04000748 RID: 1864
		public const string Precision = "Precision";

		// Token: 0x04000749 RID: 1865
		public const string PreserveLastAccessTimes = "PreserveLastAccessTimes";

		// Token: 0x0400074A RID: 1866
		public const string PreserveTypes = "PreserveTypes";

		// Token: 0x0400074B RID: 1867
		public const string Query = "Query";

		// Token: 0x0400074C RID: 1868
		public const string RelativePath = "RelativePath";

		// Token: 0x0400074D RID: 1869
		public const string RequestSize = "RequestSize";

		// Token: 0x0400074E RID: 1870
		public const string Resource = "Resource";

		// Token: 0x0400074F RID: 1871
		public const string RetryInterval = "RetryInterval";

		// Token: 0x04000750 RID: 1872
		public const string ReturnAad = "ReturnAad";

		// Token: 0x04000751 RID: 1873
		public const string ReturnSingleDatabase = "ReturnSingleDatabase";

		// Token: 0x04000752 RID: 1874
		public const string RowSelector = "RowSelector";

		// Token: 0x04000753 RID: 1875
		public const string SafeRequestHeaders = "SafeRequestHeaders";

		// Token: 0x04000754 RID: 1876
		public const string SafeResponseHeaders = "SafeResponseHeaders";

		// Token: 0x04000755 RID: 1877
		public const string ScaleMeasures = "ScaleMeasures";

		// Token: 0x04000756 RID: 1878
		public const string SecurityDefinition = "SecurityDefinition";

		// Token: 0x04000757 RID: 1879
		public const string Selector = "Selector";

		// Token: 0x04000758 RID: 1880
		public const string SimilarityColumnName = "SimilarityColumnName";

		// Token: 0x04000759 RID: 1881
		public const string SqlCompatibleWindowsAuth = "SqlCompatibleWindowsAuth";

		// Token: 0x0400075A RID: 1882
		public const string StartPage = "StartPage";

		// Token: 0x0400075B RID: 1883
		public const string SubQueries = "SubQueries";

		// Token: 0x0400075C RID: 1884
		public const string SupportsProperties = "SupportsProperties";

		// Token: 0x0400075D RID: 1885
		public const string Threshold = "Threshold";

		// Token: 0x0400075E RID: 1886
		public const string Timeout = "Timeout";

		// Token: 0x0400075F RID: 1887
		public const string TraceData = "TraceData";

		// Token: 0x04000760 RID: 1888
		public const string TransformationTable = "TransformationTable";

		// Token: 0x04000761 RID: 1889
		public const string TreatTinyAsBoolean = "TreatTinyAsBoolean";

		// Token: 0x04000762 RID: 1890
		public const string TypedMeasureColumns = "TypedMeasureColumns";

		// Token: 0x04000763 RID: 1891
		public const string TypeMap = "TypeMap";

		// Token: 0x04000764 RID: 1892
		public const string UnsafeTypeConversions = "UnsafeTypeConversions";

		// Token: 0x04000765 RID: 1893
		public const string UseDb2ConnectGateway = "UseDb2ConnectGateway";

		// Token: 0x04000766 RID: 1894
		public const string ViewMode = "ViewMode";

		// Token: 0x04000767 RID: 1895
		public const string WaitFor = "WaitFor";

		// Token: 0x04000768 RID: 1896
		public const string WebMethod = "WebMethod";

		// Token: 0x04000769 RID: 1897
		public static readonly OptionItem MaxDegreeOfParallelismOption = new OptionItem("MaxDegreeOfParallelism", NullableTypeValue.Int32, Value.Null, OptionItemOption.None, new TryConvertOption(Options.TryGetMaxDegreeOfParallelism), null);

		// Token: 0x0400076A RID: 1898
		public static readonly OptionItem NavigationPropertyNameGeneratorOption = new OptionItem("NavigationPropertyNameGenerator", NullableTypeValue.Function, Value.Null, OptionItemOption.None, new TryConvertOption(Options.TryGetNavigationPropertyNameGenerator), null);
	}
}
