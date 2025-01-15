using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200003E RID: 62
	public class ReportParameterDefinitionPatch
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00002CE5 File Offset: 0x00000EE5
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00002CED File Offset: 0x00000EED
		public string Name { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00002CF6 File Offset: 0x00000EF6
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00002CFE File Offset: 0x00000EFE
		public IEnumerable<string> DefaultValues { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002D07 File Offset: 0x00000F07
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00002D0F File Offset: 0x00000F0F
		public string Prompt { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00002D20 File Offset: 0x00000F20
		public ReportParameterVisibility ParameterVisibility { get; set; }
	}
}
