using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x0200007A RID: 122
	public class ExtensionParameter
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000046D8 File Offset: 0x000028D8
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000046E0 File Offset: 0x000028E0
		[Key]
		public string Name { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000046E9 File Offset: 0x000028E9
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000046F1 File Offset: 0x000028F1
		public string DisplayName { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000046FA File Offset: 0x000028FA
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00004702 File Offset: 0x00002902
		public bool Required { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000470B File Offset: 0x0000290B
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00004713 File Offset: 0x00002913
		public bool ReadOnly { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000471C File Offset: 0x0000291C
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00004724 File Offset: 0x00002924
		public string Value { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000472D File Offset: 0x0000292D
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00004735 File Offset: 0x00002935
		public string Error { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000473E File Offset: 0x0000293E
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00004746 File Offset: 0x00002946
		public bool Encrypted { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000474F File Offset: 0x0000294F
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00004757 File Offset: 0x00002957
		public bool IsPassword { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004760 File Offset: 0x00002960
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004768 File Offset: 0x00002968
		public ValidValue[] ValidValues { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00004771 File Offset: 0x00002971
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00004779 File Offset: 0x00002979
		public bool ValidValuesIsNull { get; set; }
	}
}
