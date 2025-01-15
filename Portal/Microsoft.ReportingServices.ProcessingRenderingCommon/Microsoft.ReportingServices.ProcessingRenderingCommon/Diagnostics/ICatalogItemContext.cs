using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000072 RID: 114
	public interface ICatalogItemContext
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000320 RID: 800
		string ItemPathAsString { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000321 RID: 801
		string ItemName { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000322 RID: 802
		string ParentPath { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000323 RID: 803
		string HostSpecificItemPath { get; }

		// Token: 0x06000324 RID: 804
		string MapUserProvidedPath(string path);

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000325 RID: 805
		string StableItemPath { get; }

		// Token: 0x06000326 RID: 806
		ICatalogItemContext GetSubreportContext(string subreportPath);

		// Token: 0x06000327 RID: 807
		string AdjustSubreportOrDrillthroughReportPath(string reportPath);

		// Token: 0x06000328 RID: 808
		string CombineUrl(string url, bool protocolRestriction, NameValueCollection unparsedParameters, out ICatalogItemContext newContext);

		// Token: 0x06000329 RID: 809
		ICatalogItemContext Combine(string url);

		// Token: 0x0600032A RID: 810
		bool IsReportServerPathOrUrl(string pathOrUrl, bool protocolRestriction, out bool isRelative);

		// Token: 0x0600032B RID: 811
		bool IsSupportedProtocol(string path, bool protocolRestriction);

		// Token: 0x0600032C RID: 812
		bool IsSupportedProtocol(string path, bool protocolRestriction, out bool isRelative);

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600032D RID: 813
		IRSRequestParameters RSRequestParameters { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600032E RID: 814
		IReportParameterLookup ReportParameterLookup { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600032F RID: 815
		string HostRootUri { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000330 RID: 816
		IPathTranslator PathTranslator { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000331 RID: 817
		IPathManager PathManager { get; }
	}
}
