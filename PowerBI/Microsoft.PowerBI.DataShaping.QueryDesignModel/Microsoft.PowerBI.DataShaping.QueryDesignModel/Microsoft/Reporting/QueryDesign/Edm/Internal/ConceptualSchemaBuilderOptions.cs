using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000251 RID: 593
	public struct ConceptualSchemaBuilderOptions
	{
		// Token: 0x060019F6 RID: 6646 RVA: 0x00047A45 File Offset: 0x00045C45
		public ConceptualSchemaBuilderOptions(bool sparklineDataEnabled)
		{
			this = new ConceptualSchemaBuilderOptions(false, false, sparklineDataEnabled);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00047A50 File Offset: 0x00045C50
		public ConceptualSchemaBuilderOptions(bool buildRowNumberAnnotations, bool skipYearColumnDetectionHeuristics, bool sparklineDataEnabled)
		{
			this.BuildRowNumberAnnotations = buildRowNumberAnnotations;
			this.SkipYearColumnDetectionHeuristics = skipYearColumnDetectionHeuristics;
			this.SparklineDataEnabled = sparklineDataEnabled;
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00047A67 File Offset: 0x00045C67
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x00047A6F File Offset: 0x00045C6F
		public bool SparklineDataEnabled { readonly get; set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00047A78 File Offset: 0x00045C78
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x00047A80 File Offset: 0x00045C80
		public bool BuildRowNumberAnnotations { readonly get; set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00047A89 File Offset: 0x00045C89
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x00047A91 File Offset: 0x00045C91
		public bool SkipYearColumnDetectionHeuristics { readonly get; set; }

		// Token: 0x04000E73 RID: 3699
		public static readonly ConceptualSchemaBuilderOptions DefaultInstance;
	}
}
