using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000028 RID: 40
	public sealed class ReportParameterDefinition
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000027A3 File Offset: 0x000009A3
		[Key]
		public string Name { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000027AC File Offset: 0x000009AC
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000027B4 File Offset: 0x000009B4
		public ReportParameterType ParameterType { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000027BD File Offset: 0x000009BD
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000027C5 File Offset: 0x000009C5
		public ReportParameterVisibility ParameterVisibility { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000027CE File Offset: 0x000009CE
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000027D6 File Offset: 0x000009D6
		public ReportParameterState ParameterState { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000027DF File Offset: 0x000009DF
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000027E7 File Offset: 0x000009E7
		public IEnumerable<ValidValue> ValidValues { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000027F8 File Offset: 0x000009F8
		public bool ValidValuesIsNull { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002801 File Offset: 0x00000A01
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002809 File Offset: 0x00000A09
		public bool Nullable { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002812 File Offset: 0x00000A12
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000281A File Offset: 0x00000A1A
		public bool AllowBlank { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002823 File Offset: 0x00000A23
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000282B File Offset: 0x00000A2B
		public bool MultiValue { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002834 File Offset: 0x00000A34
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000283C File Offset: 0x00000A3C
		public string Prompt { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002845 File Offset: 0x00000A45
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000284D File Offset: 0x00000A4D
		public bool PromptUser { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002856 File Offset: 0x00000A56
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000285E File Offset: 0x00000A5E
		public bool QueryParameter { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002867 File Offset: 0x00000A67
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000286F File Offset: 0x00000A6F
		public bool DefaultValuesQueryBased { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002878 File Offset: 0x00000A78
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002880 File Offset: 0x00000A80
		public bool ValidValuesQueryBased { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002889 File Offset: 0x00000A89
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002891 File Offset: 0x00000A91
		public IEnumerable<string> Dependencies { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000289A File Offset: 0x00000A9A
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000028A2 File Offset: 0x00000AA2
		public IEnumerable<string> DefaultValues { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000028AB File Offset: 0x00000AAB
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000028B3 File Offset: 0x00000AB3
		public bool DefaultValuesIsNull { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000028C4 File Offset: 0x00000AC4
		public string ErrorMessage { get; set; }
	}
}
