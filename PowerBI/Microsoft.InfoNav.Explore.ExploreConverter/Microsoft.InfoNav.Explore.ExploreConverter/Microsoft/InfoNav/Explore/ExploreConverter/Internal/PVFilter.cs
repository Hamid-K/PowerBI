using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001E RID: 30
	[DataContract]
	internal sealed class PVFilter
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003D43 File Offset: 0x00001F43
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003D4B File Offset: 0x00001F4B
		public string Type { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003D54 File Offset: 0x00001F54
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003D5C File Offset: 0x00001F5C
		public string Operator { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003D65 File Offset: 0x00001F65
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003D6D File Offset: 0x00001F6D
		public List<PVFilter> FilterConditions { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003D76 File Offset: 0x00001F76
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003D7E File Offset: 0x00001F7E
		public Formula LeftExpression { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003D87 File Offset: 0x00001F87
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003D8F File Offset: 0x00001F8F
		public FilterValue RightExpression { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003D98 File Offset: 0x00001F98
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public bool Not { get; set; }
	}
}
