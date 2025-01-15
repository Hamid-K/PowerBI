using System;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000113 RID: 275
	public sealed class DsrNames
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0000F288 File Offset: 0x0000D488
		private DsrNames(bool v2)
		{
			if (v2)
			{
				this.Calculations = "C";
				this.DataLimitsExceeded = "DLEx";
				this.DataShapeMessages = "Msg";
				this.DataShapes = "DS";
				this.DataWindows = "DW";
				this.HasAllData = "HAD";
				this.Id = "N";
				this.Instances = "Is";
				this.Intersections = "X";
				this.IsComplete = "IC";
				this.Members = "M";
				this.PrimaryHierarchy = "PH";
				this.RestartFlag = "RF";
				this.RestartKind = "RK";
				this.RestartTokens = "RT";
				this.SecondaryHierarchy = "SH";
				this.ValueUpper = "V";
				return;
			}
			this.Calculations = "Calculations";
			this.DataLimitsExceeded = "DataLimitsExceeded";
			this.DataShapeMessages = "DataShapeMessages";
			this.DataShapes = "DataShapes";
			this.DataWindows = "DataWindows";
			this.HasAllData = "HasAllData";
			this.Id = "Id";
			this.Instances = "Instances";
			this.Intersections = "Intersections";
			this.IsComplete = "IsComplete";
			this.Members = "Members";
			this.PrimaryHierarchy = "PrimaryHierarchy";
			this.RestartFlag = "RestartFlag";
			this.RestartKind = "RestartKind";
			this.RestartTokens = "RestartTokens";
			this.SecondaryHierarchy = "SecondaryHierarchy";
			this.ValueUpper = "Value";
		}

		// Token: 0x0400032D RID: 813
		public static readonly DsrNames V1 = new DsrNames(false);

		// Token: 0x0400032E RID: 814
		public static readonly DsrNames V2 = new DsrNames(true);

		// Token: 0x0400032F RID: 815
		public readonly string AdditionalMessages = "additionalMessages";

		// Token: 0x04000330 RID: 816
		public readonly string AffectedItems = "AffectedItems";

		// Token: 0x04000331 RID: 817
		public readonly string AzureValues = "azure:values";

		// Token: 0x04000332 RID: 818
		public readonly string CalcSchema = "S";

		// Token: 0x04000333 RID: 819
		public readonly string CodeUpper = "Code";

		// Token: 0x04000334 RID: 820
		public readonly string CodeLower = "code";

		// Token: 0x04000335 RID: 821
		public readonly string DataType = "T";

		// Token: 0x04000336 RID: 822
		public readonly string Details = "details";

		// Token: 0x04000337 RID: 823
		public readonly string DictionaryId = "DN";

		// Token: 0x04000338 RID: 824
		public readonly string DictionaryIdPrefix = "D";

		// Token: 0x04000339 RID: 825
		public readonly string Group = "Group";

		// Token: 0x0400033A RID: 826
		public readonly string Index = "I";

		// Token: 0x0400033B RID: 827
		public readonly string Key = "Key";

		// Token: 0x0400033C RID: 828
		public readonly string Lang = "lang";

		// Token: 0x0400033D RID: 829
		public readonly string Line = "Line";

		// Token: 0x0400033E RID: 830
		public readonly string MessageUpper = "Message";

		// Token: 0x0400033F RID: 831
		public readonly string MessageLower = "message";

		// Token: 0x04000340 RID: 832
		public readonly string MinorVersion = "MinorVersion";

		// Token: 0x04000341 RID: 833
		public readonly string MoreInformation = "moreInformation";

		// Token: 0x04000342 RID: 834
		public readonly string NullValues = "Ø";

		// Token: 0x04000343 RID: 835
		public readonly string ObjectName = "ObjectName";

		// Token: 0x04000344 RID: 836
		public readonly string ObjectType = "ObjectType";

		// Token: 0x04000345 RID: 837
		public readonly string OdataError = "odata.error";

		// Token: 0x04000346 RID: 838
		public readonly string ODataPowerBiErrorDetails = "powerBiErrorDetails";

		// Token: 0x04000347 RID: 839
		public readonly string Position = "Position";

		// Token: 0x04000348 RID: 840
		public readonly string PropertyName = "PropertyName";

		// Token: 0x04000349 RID: 841
		public readonly string RepeatedValues = "R";

		// Token: 0x0400034A RID: 842
		public readonly string ScopeId = "ScopeId";

		// Token: 0x0400034B RID: 843
		public readonly string ScopeValues = "ScopeValues";

		// Token: 0x0400034C RID: 844
		public readonly string Severity = "Severity";

		// Token: 0x0400034D RID: 845
		public readonly string Source = "source";

		// Token: 0x0400034E RID: 846
		public readonly string Timestamp = "timestamp";

		// Token: 0x0400034F RID: 847
		public readonly string ValueLower = "value";

		// Token: 0x04000350 RID: 848
		public readonly string ValueDictionaries = "ValueDicts";

		// Token: 0x04000351 RID: 849
		public readonly string Version = "Version";

		// Token: 0x04000352 RID: 850
		public readonly string Calculations;

		// Token: 0x04000353 RID: 851
		public readonly string DataLimitsExceeded;

		// Token: 0x04000354 RID: 852
		public readonly string DataShapeMessages;

		// Token: 0x04000355 RID: 853
		public readonly string DataShapes;

		// Token: 0x04000356 RID: 854
		public readonly string DataWindows;

		// Token: 0x04000357 RID: 855
		public readonly string HasAllData;

		// Token: 0x04000358 RID: 856
		public readonly string Id;

		// Token: 0x04000359 RID: 857
		public readonly string Instances;

		// Token: 0x0400035A RID: 858
		public readonly string Intersections;

		// Token: 0x0400035B RID: 859
		public readonly string IsComplete;

		// Token: 0x0400035C RID: 860
		public readonly string Members;

		// Token: 0x0400035D RID: 861
		public readonly string PrimaryHierarchy;

		// Token: 0x0400035E RID: 862
		public readonly string RestartFlag;

		// Token: 0x0400035F RID: 863
		public readonly string RestartKind;

		// Token: 0x04000360 RID: 864
		public readonly string RestartTokens;

		// Token: 0x04000361 RID: 865
		public readonly string SecondaryHierarchy;

		// Token: 0x04000362 RID: 866
		public readonly string ValueUpper;
	}
}
