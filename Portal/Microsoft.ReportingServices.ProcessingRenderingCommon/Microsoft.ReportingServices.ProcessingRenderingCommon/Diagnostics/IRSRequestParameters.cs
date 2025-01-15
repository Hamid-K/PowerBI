using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000089 RID: 137
	public interface IRSRequestParameters
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600041B RID: 1051
		// (set) Token: 0x0600041C RID: 1052
		string AllowNewSessionsParamValue { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600041D RID: 1053
		NameValueCollection BrowserCapabilities { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600041E RID: 1054
		NameValueCollection CatalogParameters { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600041F RID: 1055
		// (set) Token: 0x06000420 RID: 1056
		string ClearSessionParamValue { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000421 RID: 1057
		// (set) Token: 0x06000422 RID: 1058
		string CommandParamValue { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000423 RID: 1059
		// (set) Token: 0x06000424 RID: 1060
		DatasourceCredentialsCollection DatasourcesCred { get; set; }

		// Token: 0x06000425 RID: 1061
		void DetectFormatIfNotPresent();

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000426 RID: 1062
		string DrillTypeValue { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000427 RID: 1063
		string EntityIdValue { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000428 RID: 1064
		// (set) Token: 0x06000429 RID: 1065
		string FormatParamValue { get; set; }

		// Token: 0x0600042A RID: 1066
		NameValueCollection GetAllParameters();

		// Token: 0x0600042B RID: 1067
		string GetImageUrl(bool useSessionId, string imageId, ICatalogItemContext ctx);

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600042C RID: 1068
		// (set) Token: 0x0600042D RID: 1069
		string ImageIDParamValue { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600042E RID: 1070
		// (set) Token: 0x0600042F RID: 1071
		string PaginationModeValue { get; set; }

		// Token: 0x06000430 RID: 1072
		void ParseQueryString(NameValueCollection allParametersCollection, IParametersTranslator paramsTranslator, ExternalItemPath itemPath);

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000431 RID: 1073
		NameValueCollection RenderingParameters { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000432 RID: 1074
		NameValueCollection ReportParameters { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000433 RID: 1075
		string ReportParametersXml { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000434 RID: 1076
		// (set) Token: 0x06000435 RID: 1077
		string ReturnUrlValue { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000436 RID: 1078
		// (set) Token: 0x06000437 RID: 1079
		string ServerVirtualRoot { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		string SessionId { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600043A RID: 1082
		// (set) Token: 0x0600043B RID: 1083
		string SessionIDParamValue { get; set; }

		// Token: 0x0600043C RID: 1084
		void SetBrowserCapabilities(NameValueCollection browserCapabilities);

		// Token: 0x0600043D RID: 1085
		void SetCatalogParameters(string catalogParametersXml);

		// Token: 0x0600043E RID: 1086
		void SetRenderingParameters(NameValueCollection otherParams);

		// Token: 0x0600043F RID: 1087
		void SetRenderingParameters(string renderingParametersXml);

		// Token: 0x06000440 RID: 1088
		void SetReportParameters(NameValueCollection allReportParameters);

		// Token: 0x06000441 RID: 1089
		void SetReportParameters(NameValueCollection allReportParameters, IParametersTranslator paramsTranslator);

		// Token: 0x06000442 RID: 1090
		void SetReportParameters(string reportParametersXml);

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000443 RID: 1091
		// (set) Token: 0x06000444 RID: 1092
		string ShowHideToggleParamValue { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000445 RID: 1093
		// (set) Token: 0x06000446 RID: 1094
		string SnapshotParamValue { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000447 RID: 1095
		// (set) Token: 0x06000448 RID: 1096
		string SortIdParamValue { get; set; }
	}
}
