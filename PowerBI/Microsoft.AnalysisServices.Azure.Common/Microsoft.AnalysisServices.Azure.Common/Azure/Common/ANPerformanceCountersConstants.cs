using System;
using System.IO;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200003A RID: 58
	public static class ANPerformanceCountersConstants
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000EA7E File Offset: 0x0000CC7E
		public static string BiAzurePerformanceCountersCategoryPrefix
		{
			get
			{
				return "BiAzure";
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000EA85 File Offset: 0x0000CC85
		public static string BiAzurePerformanceCounterProviderName
		{
			get
			{
				return "BIAzurePerformanceCounterProvider";
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		public static string BiAzurePerformanceCountersManifestFileName
		{
			get
			{
				return ANPerformanceCountersConstants.c_manifestFileName;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000EA93 File Offset: 0x0000CC93
		public static string BiAzurePerformanceCountersDynamicLibraryFileName
		{
			get
			{
				return ANPerformanceCountersConstants.c_dynamicLibraryFileName;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000EA9A File Offset: 0x0000CC9A
		public static Guid BiAzurePerformanceCountersProvider
		{
			get
			{
				return ANPerformanceCountersConstants.c_performanceCounterProvider;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
		public static string BiAzurePerformanceCounterGeneratedDllName
		{
			get
			{
				return Path.ChangeExtension("BIAzurePerformanceCounters", "dll");
			}
		}

		// Token: 0x040000AB RID: 171
		private const string c_generatedFilesName = "BIAzurePerformanceCounters";

		// Token: 0x040000AC RID: 172
		private static readonly string c_manifestFileName = "{0}.man".FormatWithInvariantCulture(new object[] { "BIAzurePerformanceCounters" });

		// Token: 0x040000AD RID: 173
		private static readonly string c_dynamicLibraryFileName = "{0}.dll".FormatWithInvariantCulture(new object[] { "BIAzurePerformanceCounters" });

		// Token: 0x040000AE RID: 174
		private static readonly Guid c_performanceCounterProvider = Guid.Parse("{0BB04049-2D1F-40E9-B2BB-88626B7A691F}");
	}
}
