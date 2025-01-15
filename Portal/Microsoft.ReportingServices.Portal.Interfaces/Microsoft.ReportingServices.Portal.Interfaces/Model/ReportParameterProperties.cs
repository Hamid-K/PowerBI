using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000029 RID: 41
	public class ReportParameterProperties
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000028CD File Offset: 0x00000ACD
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000028D5 File Offset: 0x00000AD5
		public string ParameterName { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000028DE File Offset: 0x00000ADE
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000028E6 File Offset: 0x00000AE6
		public bool OverrideDefaultValues { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000028EF File Offset: 0x00000AEF
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000028F7 File Offset: 0x00000AF7
		public IEnumerable<string> DefaultValues { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002900 File Offset: 0x00000B00
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002908 File Offset: 0x00000B08
		public string Prompt { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002911 File Offset: 0x00000B11
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002919 File Offset: 0x00000B19
		public ReportParameterVisibility ParameterVisibility { get; set; }
	}
}
