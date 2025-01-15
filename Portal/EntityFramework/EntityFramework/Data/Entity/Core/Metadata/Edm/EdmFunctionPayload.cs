using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A4 RID: 1188
	public class EdmFunctionPayload
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000C0D24 File Offset: 0x000BEF24
		// (set) Token: 0x06003A53 RID: 14931 RVA: 0x000C0D2C File Offset: 0x000BEF2C
		public string Schema { get; set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000C0D35 File Offset: 0x000BEF35
		// (set) Token: 0x06003A55 RID: 14933 RVA: 0x000C0D3D File Offset: 0x000BEF3D
		public string StoreFunctionName { get; set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000C0D46 File Offset: 0x000BEF46
		// (set) Token: 0x06003A57 RID: 14935 RVA: 0x000C0D4E File Offset: 0x000BEF4E
		public string CommandText { get; set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000C0D57 File Offset: 0x000BEF57
		// (set) Token: 0x06003A59 RID: 14937 RVA: 0x000C0D5F File Offset: 0x000BEF5F
		public IList<EntitySet> EntitySets { get; set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000C0D68 File Offset: 0x000BEF68
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x000C0D70 File Offset: 0x000BEF70
		public bool? IsAggregate { get; set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000C0D79 File Offset: 0x000BEF79
		// (set) Token: 0x06003A5D RID: 14941 RVA: 0x000C0D81 File Offset: 0x000BEF81
		public bool? IsBuiltIn { get; set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000C0D8A File Offset: 0x000BEF8A
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x000C0D92 File Offset: 0x000BEF92
		public bool? IsNiladic { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000C0D9B File Offset: 0x000BEF9B
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x000C0DA3 File Offset: 0x000BEFA3
		public bool? IsComposable { get; set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000C0DAC File Offset: 0x000BEFAC
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000C0DB4 File Offset: 0x000BEFB4
		public bool? IsFromProviderManifest { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000C0DBD File Offset: 0x000BEFBD
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x000C0DC5 File Offset: 0x000BEFC5
		public bool? IsCachedStoreFunction { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000C0DCE File Offset: 0x000BEFCE
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x000C0DD6 File Offset: 0x000BEFD6
		public bool? IsFunctionImport { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000C0DDF File Offset: 0x000BEFDF
		// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000C0DE7 File Offset: 0x000BEFE7
		public IList<FunctionParameter> ReturnParameters { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
		// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000C0DF8 File Offset: 0x000BEFF8
		public ParameterTypeSemantics? ParameterTypeSemantics { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000C0E01 File Offset: 0x000BF001
		// (set) Token: 0x06003A6D RID: 14957 RVA: 0x000C0E09 File Offset: 0x000BF009
		public IList<FunctionParameter> Parameters { get; set; }
	}
}
