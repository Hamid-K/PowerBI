using System;
using System.ComponentModel;

namespace NLog.Targets
{
	// Token: 0x02000042 RID: 66
	public class JsonSerializeOptions
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001071E File Offset: 0x0000E91E
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00010726 File Offset: 0x0000E926
		[DefaultValue(true)]
		public bool QuoteKeys { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001072F File Offset: 0x0000E92F
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00010737 File Offset: 0x0000E937
		public IFormatProvider FormatProvider { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00010740 File Offset: 0x0000E940
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00010748 File Offset: 0x0000E948
		public string Format { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00010751 File Offset: 0x0000E951
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00010759 File Offset: 0x0000E959
		[DefaultValue(false)]
		public bool EscapeUnicode { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00010762 File Offset: 0x0000E962
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001076A File Offset: 0x0000E96A
		[DefaultValue(false)]
		public bool EnumAsInteger { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00010773 File Offset: 0x0000E973
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001077B File Offset: 0x0000E97B
		[DefaultValue(false)]
		public bool SanitizeDictionaryKeys { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00010784 File Offset: 0x0000E984
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001078C File Offset: 0x0000E98C
		[DefaultValue(10)]
		public int MaxRecursionLimit { get; set; }

		// Token: 0x060006A8 RID: 1704 RVA: 0x00010795 File Offset: 0x0000E995
		public JsonSerializeOptions()
		{
			this.QuoteKeys = true;
			this.MaxRecursionLimit = 10;
		}
	}
}
