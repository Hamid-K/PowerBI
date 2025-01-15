using System;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000022 RID: 34
	public struct ConceptualSchemaBuilderOptions
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00006881 File Offset: 0x00004A81
		public ConceptualSchemaBuilderOptions(bool sparklineDataEnabled)
		{
			this = new ConceptualSchemaBuilderOptions(false, false, sparklineDataEnabled);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000688C File Offset: 0x00004A8C
		public ConceptualSchemaBuilderOptions(bool buildRowNumberAnnotations, bool skipYearColumnDetectionHeuristics, bool sparklineDataEnabled)
		{
			this.BuildRowNumberAnnotations = buildRowNumberAnnotations;
			this.SkipYearColumnDetectionHeuristics = skipYearColumnDetectionHeuristics;
			this.SparklineDataEnabled = sparklineDataEnabled;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000068A3 File Offset: 0x00004AA3
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000068AB File Offset: 0x00004AAB
		public bool SparklineDataEnabled { readonly get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000068B4 File Offset: 0x00004AB4
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000068BC File Offset: 0x00004ABC
		public bool BuildRowNumberAnnotations { readonly get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000068C5 File Offset: 0x00004AC5
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000068CD File Offset: 0x00004ACD
		public bool SkipYearColumnDetectionHeuristics { readonly get; set; }

		// Token: 0x04000158 RID: 344
		public static readonly ConceptualSchemaBuilderOptions DefaultInstance;
	}
}
